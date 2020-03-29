using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;
using System.DirectoryServices;
using System.Management;
using System.Threading;
using System.Management.Instrumentation;
using System.IO;
using System.Globalization;




namespace WinApp
{
    public partial class ProcessController : Form
    {
        private string[] domains = { "dcacfcapital" };
        public static string userName;
        public static string password;
        public static string machineName;
        public static string myDomain;
        private string[] columnNames = { "Caption", "ComputerName",
            "Description", "Name", "Priority", "ProcessID", "SessionId" };
        public string[] columnNamesP = { "Name", "InstallDate",
        "Version", "Language", "InstallLocation", "URLInfoAbout", "Description" };
        private Hashtable hs = new Hashtable();
        public ManagementScope myScope;
        public ConnectionOptions connOptions;
        public ManagementObjectSearcher objSearcher;
        public ManagementOperationObserver opsObserver;
        public ManagementClass manageClass;
        private DirectoryEntry entry;
        public DirectorySearcher searcher;
        private DirectorySearcher userSearcher;
        private DataTable dt;
        //public DataTable dt2;
        private DataColumn[] dc = new DataColumn[7];
        //public DataColumn[] dc2 = new DataColumn[7];
        //private Icon;

        //Uri iconUri = new Uri("pack://application:,,,/WPFIcon2.ico", UriKind.RelativeOrAbsolute);
        //public Icon = BitmapFrame.Create(iconUri);


        public ProcessController()
        {
            dt = new DataTable();
            for (int i = 0; i < columnNames.Length; i++)
            {
                dc[i] = new DataColumn(columnNames[i], typeof(string));
            }
            dt.Columns.AddRange(dc);


            //dt2 = new DataTable();
            //for (int i = 0; i < columnNamesP.Length; i++)
            //{
            //    dc2[i] = new DataColumn(columnNamesP[i], typeof(string));
            //}
            //dt2.Columns.AddRange(dc2);

            InitializeComponent();
            foreach (string domain in domains)
            {
                cmbDomainList.Items.Add(domain);
            }
            cmbDomainList.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmdMachinesInDomain.Text == string.Empty)
            {
                MessageBox.Show("Por Favor Seleciona una maquina");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            ConnectToRemoteMachine();
        }

        private void terminateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            try
            {
                string endProc = row.Cells[0].Value.ToString().Trim();
                foreach (ManagementObject obj in objSearcher.Get())
                {
                    string caption = obj.GetText(TextFormat.Mof).Trim();
                    if (caption.Contains(endProc.Trim()))
                    {
                        obj.InvokeMethod(opsObserver, "Terminate", null); // terminate es la instruccion para terminar el proceso
                    }
                }
                dataGridView1.Refresh();
                btnConnect_Click(btnConnect, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void btnGetMachines_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int index = 0;
            // Create an entry for domain
            entry = new DirectoryEntry("LDAP://" + cmbDomainList.Text);
            // Create a user searcher by using filter
            userSearcher = new DirectorySearcher(entry);
            userSearcher.Filter = ("(objectclass=user)");
            SearchResultCollection src = userSearcher.FindAll();

            // Get all computers
            searcher = new DirectorySearcher(entry);
            searcher.Filter = ("(objectclass=computer)");

            try
            {
                SearchResultCollection results = searcher.FindAll();
                foreach (SearchResult sr in results)
                {
                    DirectoryEntry de = sr.GetDirectoryEntry();
                    cmdMachinesInDomain.Items.Add(de.Name.Remove(0, 3));

                    DirectoryEntry de1 = src[index++].GetDirectoryEntry();
                    cmbUsers.Items.Add(de1.Properties["cn"].Value.ToString());
                    if (!hs.ContainsKey(de.Name))
                    {
                        hs.Add(de.Name.Remove(0, 3), de1.Properties["cn"].Value.ToString());
                    }
                }
                cmdMachinesInDomain.SelectedIndex = 0;
                cmbUsers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdMachinesInDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUsers.Items.Clear();
            cmbUsers.Text = hs[cmdMachinesInDomain.Text].ToString();
            cmbUsers.Items.Add(hs[cmdMachinesInDomain.Text].ToString());
            cmbUsers.SelectedIndex = 0;
        }

        private void btnEndProcess_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                terminateToolStripMenuItem_Click(sender, e);
                ConnectToRemoteMachine();
            }
        }

        private void ConnectToRemoteMachine()
        {
            int width = dataGridView1.Width;
            int singleColWidth;

            singleColWidth = width / dt.Columns.Count;

            userName = txtUserName.Text.Trim();
            password = txtPassword.Text.Trim();
            if (cmdMachinesInDomain.SelectedItem != null)
            {
                machineName = cmdMachinesInDomain.SelectedItem.ToString();
            }
            else if (cmdMachinesInDomain.SelectedText != string.Empty)
            {
                machineName = cmdMachinesInDomain.SelectedText;
            }
            else
            {
                machineName = cmdMachinesInDomain.Text;
            }

            myDomain = cmbDomainList.Text;

            try
            {
                connOptions = new ConnectionOptions();
                connOptions.Impersonation = ImpersonationLevel.Impersonate;
                connOptions.EnablePrivileges = true;
                if (machineName.ToUpper() == Environment.MachineName.ToUpper())
                {
                    myScope = new ManagementScope(@"\ROOT\CIMV2", connOptions);
                }
                else
                {
                    if (chkUseDomain.Checked)
                    {
                        connOptions.Username = myDomain + "\\" + userName;
                    }
                    else
                    {
                        connOptions.Username = machineName + "\\" + userName;
                    }
                    connOptions.Password = password;
                    myScope = new ManagementScope(@"\\" + machineName + @"\ROOT\CIMV2", connOptions);
                }

                myScope.Connect();
                objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
                opsObserver = new ManagementOperationObserver();
                objSearcher.Scope = myScope;
                string[] sep = { "\n", "\t" };
                toolStripStatusLabel1.Text = string.Empty;
                toolStripStatusLabel1.Text = "Autenticacion Correcta... Datos Obtenidos";
                dt.Rows.Clear();
                foreach (ManagementObject obj in objSearcher.Get())
                {
                    string caption = obj.GetText(TextFormat.Mof);
                    string[] split = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    DataRow dr = dt.NewRow();
                    // Iterate through the splitter
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i].Split('=').Length > 1)
                        {
                            string[] procDetails = split[i].Split('=');
                            procDetails[1] = procDetails[1].Replace(@"""", "");
                            procDetails[1] = procDetails[1].Replace(';', ' ');
                            switch (procDetails[0].Trim().ToLower())
                            {
                                case "caption":
                                    dr[dc[0]] = procDetails[1];
                                    break;
                                case "csname":
                                    dr[dc[1]] = procDetails[1];
                                    break;
                                case "description":
                                    dr[dc[2]] = procDetails[1];
                                    break;
                                case "name":
                                    dr[dc[3]] = procDetails[1];
                                    break;
                                case "priority":
                                    dr[dc[4]] = procDetails[1];
                                    break;
                                case "processid":
                                    dr[dc[5]] = procDetails[1];
                                    break;
                                case "sessionid":
                                    dr[dc[6]] = procDetails[1];
                                    break;
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
                bindingSource1.DataSource = dt.DefaultView;
                foreach (DataColumn col in dt.Columns)
                {
                    DataGridViewTextBoxColumn dvc = new DataGridViewTextBoxColumn();
                    dvc.ToolTipText = col.ColumnName;
                    dvc.Name = col.ColumnName;
                    dvc.HeaderText = col.ColumnName;
                    dvc.DataPropertyName = col.ColumnName;
                    dvc.Width = singleColWidth;
                    dataGridView1.Columns.Add(dvc);
                }


                grpStartNewProcess.Enabled = true;
                btnEndProcess.Enabled = true;


                if (checkBoxHardware.Checked)
                {
                   btnHardware.Enabled = true;
                    TreeNode xfatherNode;
                    TreeNode xSonNode;                                     

                    //ConnectToRemoteMachine();
                    ////llena  procesos al treeview de datos hardware

                    xfatherNode = TreeViewHardware.Nodes.Add("Procesador:");
                    foreach (String tn in Cpu())
                    {
                        string myStn = tn.ToString();
                        xSonNode = xfatherNode.Nodes.Add(myStn);
                    }
                    xfatherNode = TreeViewHardware.Nodes.Add("Placa Madre:");
                    foreach (String tn in MotherBoard())
                    {
                        string myStn = tn.ToString();
                        xSonNode = xfatherNode.Nodes.Add(myStn);
                    }


                    xfatherNode = TreeViewHardware.Nodes.Add("Ram:");
                    foreach (String tn in RAM())
                    {
                        string myStn = tn.ToString();
                        xSonNode = xfatherNode.Nodes.Add(myStn);
                    }


                    xfatherNode = TreeViewHardware.Nodes.Add("Disco Duro:");
                    foreach (String tn in HDisk())
                    {
                        string myStn = tn.ToString();
                        xSonNode = xfatherNode.Nodes.Add(myStn);
                    }

                    xfatherNode = TreeViewHardware.Nodes.Add("Monitor:");
                    foreach (String tn in Monitor())
                    {
                        string myStn = tn.ToString();
                        xSonNode = xfatherNode.Nodes.Add(myStn);
                    }
                }

                if (checkBoxSoftware.Checked)
                {
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add(new DataColumn("Name", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("InstallLocation", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("InstalDate", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("InstallState", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("Vendor", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Version", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("PackageName", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("InstallSource", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Language", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("LocalPackage", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("PackageCache", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("PackageCode", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("HelpTelephone", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("AssignmentType", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("Caption", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Description", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("IdentifyingNumber", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("ProductID", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("RegOwner", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("RegCompany", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("SKUNumber", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("Transforms", typeof(string)));
                    dt2.Columns.Add(new DataColumn("URLInfoAbout", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("URLUpdateInfo", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("HelpLink", typeof(string)));
                    //dt2.Columns.Add(new DataColumn("WordCount", typeof(string)));

                    myScope.Connect();
                    //objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                    //opsObserver = new ManagementOperationObserver();
                    
                    
                    SelectQuery Sq = new SelectQuery("Win32_Product");
                    ManagementObjectSearcher objSearcher = new ManagementObjectSearcher(Sq);
                    objSearcher.Scope = myScope;
                    ManagementObjectCollection osDetailsCollection = objSearcher.Get();
                    
                    foreach (ManagementObject MO in osDetailsCollection)
                    {

                        CultureInfo invC = CultureInfo.InvariantCulture;
                        DataRow dr2 = dt2.NewRow();                                              

                        if (MO["Name"] != null)
                        {
                            dr2["Name"] = MO["Name"].ToString();
                        }
                        else if (MO["Name"] == null)
                        {
                            dr2["Name"] = "No Disponible";
                        }
                        //if (MO["InstallDate"] != null)
                        //{
                        //    var newDate = DateTime.ParseExact(MO["InstallDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        //    dr2["InstallDate"] = newDate;
                        //}
                        //else if (MO["InstallDate"] == null)
                        //{
                        //    dr2["InstallDate"] = "No Disponible";
                        //}
                        if (MO["Version"] != null)
                        {
                            dr2["Version"] = MO["Version"].ToString();
                        }
                        else if (MO["Version"] == null)
                        {
                            dr2["Version"] = "No Disponible";
                        }
                        if (MO["Language"] != null)
                        {
                            dr2["Language"] = MO["Language"].ToString();
                        }
                        else if (MO["Language"] == null)
                        {
                            dr2["Language"] = "";
                        }
                        if (MO["Description"] != null)
                        {
                            dr2["Description"] = MO["Description"].ToString();
                        }
                        else if (MO["Description"] == null)
                        {
                            dr2["Description"]  = "No Disponible";
                        }
                        if (MO["URLInfoAbout"] != null)
                        {
                            dr2["URLInfoAbout"] = MO["URLInfoAbout"].ToString();
                        }
                        else if (MO["URLInfoAbout"] == null)
                        {
                            dr2["URLInfoAbout"] = "No Disponible";
                        }
                        //if (MO["AssignmentType"] != null)
                        //{
                        //    dr["AssignmentType"] = MO["AssignmentType"].ToString();
                        //}
                        //dr2["AssignmentType"] = MO["AssignmentType"].ToString();
                        //dr2["Caption"] = MO["Caption"];
                        //dr2["Description"] = MO["Description"];
                        //dr2["IdentifyingNumber"] = MO["IdentifyingNumber"];
                        //dr2["InstallLocation"] = MO["InstallLocation"];
                        //var newDate = DateTime.ParseExact(MO["InstallDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                        //dr2["Instal Date"] = newDate;
                        //dr2["InstallState"] = MO["InstallState"];
                        //dr2["HelpLink"] = MO["HelpLink"];
                        //dr2["HelpTelephone"] = MO["HelpTelephone"];
                        //dr2["InstallSource"] = MO["InstallSource"];
                        //dr2["Language"] = MO["Language"];
                        //dr2["LocalPackage"] = MO["LocalPackage"];
                        //dr2["PackageCache"] = MO["PackageCache"];
                        //dr2["PackageCode"] = MO["PackageCode"];
                        //dr2["PackageName"] = MO["PackageName"];
                        //dr2["InstallState"] = MO["InstallState"];
                        //dr2["ProductID"] = MO["ProductID"];
                        //dr2["RegOwner"] = MO["RegOwner"];
                        //dr2["RegCompany"] = MO["RegCompany"];
                        //dr2["SKUNumber"] = MO["SKUNumber"];
                        //dr2["Transforms"] = MO["Transforms"];
                        //dr2["URLInfoAbout"] = MO["URLInfoAbout"];
                        //dr2["URLUpdateInfo"] = MO["URLUpdateInfo"];
                        //dr2["Vendor"] = MO["Vendor"];
                        //dr2["WordCount"] = MO["WordCount"];
                        //dr2["Version"] = MO["Version"];
                        dt2.Rows.Add(dr2);
                    }
                    
                    dataGridView2.DataSource = dt2;



                }












                //int width2 = dataGridView2.Width;
                //int singleColWidth2;

                //singleColWidth2 = width2 / dt2.Columns.Count;                                    

                //myScope.Connect();
                //    objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                //    opsObserver = new ManagementOperationObserver();
                //    objSearcher.Scope = myScope;
                //    string[] sep2 = { "\n", "\t" };

                //dt2.Rows.Clear();
                //foreach (ManagementObject prog in objSearcher.Get())
                //{
                //    string caption2 = prog.GetText(TextFormat.Mof);
                //    string[] split2 = caption2.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
                //    DataRow dr2 = dt2.NewRow();                        
                //    //DataRow dr = dt.NewRow();
                //    // Iterate through the splitter
                //    for (int i = 0; i < split2.Length; i++)
                //    {
                //        if (split2[i].Split('=').Length > 1)
                //        {
                //            string[] progDetails = split2[i].Split('=');
                //            progDetails[1] = progDetails[1].Replace(@"""", "");
                //            progDetails[1] = progDetails[1].Replace(';', ' ');
                //            switch (progDetails[0].Trim().ToLower())
                //            {
                //                case "Name":
                //                    if(prog["Name"] != null)
                //                    {
                //                        dr2[dc2[0]] = progDetails[1];
                //                    }else if (prog["Name"] == null)
                //                    {
                //                        dr2[dc2[0]] = "No disponible";
                //                    }                                            
                //                    break;
                //                case "InstallDate":
                //                    if (prog["InstallDate"] != null)
                //                    {
                //                        dr2[dc2[1]] = progDetails[1];
                //                    }else if(prog["InstallDate"] == null)
                //                    {
                //                        dr2[dc2[1]] = "No disponible";
                //                    }
                //                    break;
                //                case "Version":
                //                    if (prog["Version"] != null)
                //                    {
                //                        dr2[dc2[2]] = progDetails[1];
                //                    }
                //                    else if (prog["Version"] == null)
                //                    {
                //                        dr2[dc2[2]] = "No disponible";
                //                    }
                //                    break;
                //                case "Language":
                //                    if (prog["Language"] != null)
                //                    {
                //                        dr2[dc2[3]] = progDetails[1];
                //                    }else if(prog["Language"] != null)
                //                    {
                //                        dr2[dc2[3]] = "No disponible";
                //                    }                                        
                //                    break;
                //                case "InstallLocation":
                //                    if (prog["InstallLocation"] != null)
                //                    {
                //                        dr2[dc2[4]] = progDetails[1];
                //                    }
                //                    else if (prog["InstallLocation"] != null)
                //                    {
                //                        dr2[dc2[4]] = "No disponible";
                //                    }
                //                    break;                                    
                //                case "URLInfoAbout":
                //                    if (prog["URLInfoAbout"] != null)
                //                    {
                //                        dr2[dc2[5]] = progDetails[1];
                //                    }
                //                    else if (prog["URLInfoAbout"] != null)
                //                    {
                //                        dr2[dc2[5]] = "No disponible";
                //                    }
                //                    break;                                      
                //                case "Description":
                //                    if (prog["Description"] != null)
                //                    {
                //                        dr2[dc2[6]] = progDetails[1];
                //                    }
                //                    else if (prog["Description"] != null)
                //                    {
                //                        dr2[dc2[6]] = "No disponible";
                //                    }
                //                    break;                                      

                //case "Name":
                //    if (obj["Name"] != null)
                //    {
                //        dr2[dc2[0]] = progDetails[1];
                //    }
                //    break;
                //case "InstallDate":
                //    dr2[dc2[1]] = progDetails[1];
                //    break;
                //case "Version":
                //    dr2[dc2[2]] = progDetails[1];
                //    break;
                //case "Language":
                //    dr2[dc2[3]] = progDetails[1];
                //    break;
                //case "InstallLocation":
                //    dr2[dc2[4]] = progDetails[1];
                //    break;
                //case "URLInfoAbout":
                //    dr2[dc2[5]] = progDetails[1];
                //    break;
                //case "Description":
                //    dr2[dc2[6]] = progDetails[1];
                //    break;                                      

                //}
                //            }
                //            }
                //dt2.Rows.Add(dr2);
                //}
                //bindingSource2.DataSource = dt2.DefaultView;
                //foreach (DataColumn col2 in dt2.Columns)
                //{
                //    DataGridViewTextBoxColumn dvc2 = new DataGridViewTextBoxColumn();
                //    dvc2.ToolTipText = col2.ColumnName;
                //    dvc2.Name = col2.ColumnName;
                //    dvc2.HeaderText = col2.ColumnName;
                //    dvc2.DataPropertyName = col2.ColumnName;
                //    dvc2.Width = singleColWidth2;
                //    dataGridView2.Columns.Add(dvc2);
                //}

                //}
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


//grpStartNewProcess.Enabled = true;
//btnEndProcess.Enabled = true;


//dt = new DataTable();
//string[] columnNamesP = { "Name", "InstallDate",
//"Version", "Language", "InstallLocation", "URLInfoAbout", "Description" };
////Hashtable hs = new Hashtable();
//int width2 = dataGridView2.Width;
//int singleColWidth2;
////DataColumn[] dc = new DataColumn[7];
//// DataTable dt;
////InitializeComponent();

//singleColWidth2 = width2 / dt.Columns.Count;

//myScope.Connect();
//objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
//opsObserver = new ManagementOperationObserver();
//objSearcher.Scope = myScope;
////objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
////opsObserver = new ManagementOperationObserver();
////objSearcher.Scope = myScope;
////string[] sep = { "\n", "\t" };
//// toolStripStatusLabel1.Text = string.Empty;
////toolStripStatusLabel1.Text = "Autenticacion Correcta... Datos Obtenidos";

//for (int i = 0; i < columnNames.Length; i++)
//{
//    dc[i] = new DataColumn(columnNames[i], typeof(string));
//}
//dt.Columns.AddRange(dc);


//dt.Rows.Clear();
//foreach (ManagementObject obj in objSearcher.Get())
//{
//    string caption = obj.GetText(TextFormat.Mof);
//    string[] split = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries);
//    DataRow dr = dt.NewRow();
//    // Iterate through the splitter
//    for (int i = 0; i < split.Length; i++)
//    {
//        if (split[i].Split('=').Length > 1)
//        {
//            string[] progDetails = split[i].Split('=');
//            progDetails[1] = progDetails[1].Replace(@"""", "");
//            progDetails[1] = progDetails[1].Replace(';', ' ');
//            switch (progDetails[0].Trim().ToLower())
//            {
//                case "Name":
//                    dr[dc[0]] = progDetails[1];
//                    break;
//                case "InstallDate":
//                    dr[dc[6]] = progDetails[1];
//                    break;
//                case "Version":
//                    dr[dc[1]] = progDetails[1];
//                    break;
//                case "Language":
//                    dr[dc[2]] = progDetails[1];
//                    break;
//                case "InstallLocation":
//                    dr[dc[3]] = progDetails[1];
//                    break;
//                case "URLInfoAbout":
//                    dr[dc[4]] = progDetails[1];
//                    break;
//                case "Description":
//                    dr[dc[5]] = progDetails[1];
//                    break;
//            }
//        }
//    }
//    dt.Rows.Add(dr);
//}

//bindingSource2.DataSource = dt.DefaultView;
//foreach (DataColumn col in dt.Columns)
//{
//    DataGridViewTextBoxColumn dvc = new DataGridViewTextBoxColumn();
//    dvc.ToolTipText = col.ColumnName;
//    dvc.Name = col.ColumnName;
//    dvc.HeaderText = col.ColumnName;
//    dvc.DataPropertyName = col.ColumnName;
//    dvc.Width = singleColWidth;
//    dataGridView2.Columns.Add(dvc);
//}
//Iprograms();
// btnSottware.Enabled = true;
//}}





//     private void Iprograms()
//     {

//         dt = new DataTable();
//         string[] columnNamesP = { "Name", "InstallDate",
//                 "Version", "Language", "InstallLocation", "URLInfoAbout", "Description" };
//         Hashtable hs = new Hashtable();
//         int width = dataGridView2.Width;
//         int singleColWidth;
//         DataColumn[] dc = new DataColumn[7];
//         // DataTable dt;
//         InitializeComponent();

//         singleColWidth = width / dt.Columns.Count;

//         myScope.Connect();
//         objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
//         opsObserver = new ManagementOperationObserver();
//         objSearcher.Scope = myScope;
//         //objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
//         //opsObserver = new ManagementOperationObserver();
//         //objSearcher.Scope = myScope;
//         string[] sep = { "\n", "\t" };
//         // toolStripStatusLabel1.Text = string.Empty;
//         //toolStripStatusLabel1.Text = "Autenticacion Correcta... Datos Obtenidos";

//         for (int i = 0; i < columnNames.Length; i++)
//         {
//             dc[i] = new DataColumn(columnNames[i], typeof(string));
//         }
//         dt.Columns.AddRange(dc);


//         dt.Rows.Clear();
//         foreach (ManagementObject obj in objSearcher.Get())
//         {
//             string caption = obj.GetText(TextFormat.Mof);
//             string[] split = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries);
//             DataRow dr = dt.NewRow();
//             // Iterate through the splitter
//             for (int i = 0; i < split.Length; i++)
//             {
//                 if (split[i].Split('=').Length > 1)
//                 {
//                     string[] progDetails = split[i].Split('=');
//                     progDetails[1] = progDetails[1].Replace(@"""", "");
//                     progDetails[1] = progDetails[1].Replace(';', ' ');
//                     switch (progDetails[0].Trim().ToLower())
//                     {
//                         case "Name":
//                             dr[dc[0]] = progDetails[1];
//                             break;
//                         case "InstallDate":
//                             dr[dc[6]] = progDetails[1];
//                             break;
//                         case "Version":
//                             dr[dc[1]] = progDetails[1];
//                             break;
//                         case "Language":
//                             dr[dc[2]] = progDetails[1];
//                             break;
//                         case "InstallLocation":
//                             dr[dc[3]] = progDetails[1];
//                             break;
//                         case "URLInfoAbout":
//                             dr[dc[4]] = progDetails[1];
//                             break;
//                         case "Description":
//                             dr[dc[5]] = progDetails[1];
//                             break;
//                     }
//                 }
//             }
//             dt.Rows.Add(dr);
//         }





//     bindingSource2.DataSource = dt.DefaultView;
//     foreach (DataColumn col in dt.Columns)
// {
//     DataGridViewTextBoxColumn dvc = new DataGridViewTextBoxColumn();
//     dvc.ToolTipText = col.ColumnName;
//     dvc.Name = col.ColumnName;
//     dvc.HeaderText = col.ColumnName;
//     dvc.DataPropertyName = col.ColumnName;
//     dvc.Width = singleColWidth;
//     dataGridView2.Columns.Add(dvc);
// }
// //grpStartNewProcess.Enabled = true;
// //btnEndProcess.Enabled = true;

// //return DataColumn;
// //}
// //}
//}




//}
private void btnStartNew_Click(object sender, EventArgs e)
        {
            object[] arrParams = { txtNewProcess.Text.Trim() }; //comentarizado para probar llenando combo box
            //comboBox1.Items.AddRange(files);


            try
            {
                manageClass =
                    new ManagementClass(myScope,
                    new ManagementPath("Win32_Process"), new ObjectGetOptions());
                manageClass.InvokeMethod("Create", arrParams);
                btnConnect_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ProcessController_Load(object sender, EventArgs e)
        {

        }

        private void btnEndProcess_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                terminateToolStripMenuItem_Click(sender, e);
                ConnectToRemoteMachine();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnStartNew_Click_1(object sender, EventArgs e)

        {

            //GetSpecs();
            ////public ComboBox1();
            //string xProcess = "";
            //Directory.GetFiles("\\\\" + userName + "\\c:\", "c * ");

            //string[] dirs = Directory.GetFiles(@"c:\", "c*");
            //Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);
            //foreach (string dir in dirs)
            //{
            //    Console.WriteLine(dir);
            //}
            ////string[] Xfiles = Directory.GetFiles(@"c:\", "*.exe");
            ////comboBox1.Items.AddRange(Xfiles);
            ////xProcess = comboBox1.Text.ToString();             

            //xProcess = ComboBox1.SelectedValue.toString();
            //xProcess = comboBox1.SelectedItem();
            //object[] arrParams = { xProcess.Trim() };
            //object[] arrParams = Xfiles;
            // object[] arrParams = { comboBox1.Text.Trim() };
            object[] arrParams = { txtNewProcess.Text.Trim() };
                        
            try
            {
                manageClass =
                    new ManagementClass(myScope,
                    new ManagementPath("Win32_Process"), new ObjectGetOptions());
                manageClass.InvokeMethod("Create", arrParams);
                btnConnect_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
         
        public ArrayList Cpu()
        {

            ArrayList ArrayListCpu = new ArrayList();
            String ProcessorId = "";
            String name = "";
            String CurrentClockSpeed = "";
            String DataWidth = "";
            String DeviceID = "";
            String L2CacheSize = "";
            String L3CacheSpeed = "";
            String Manufacturer = "";
            String NumberOfCores = "";
            String NumberOfLogicalProcessors = "";
            String Status = "";
            myScope.Connect();

            objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            opsObserver = new ManagementOperationObserver();
            objSearcher.Scope = myScope;            

            foreach (ManagementObject CPU in objSearcher.Get())
            {

                CPU.Get();
                ProcessorId=CPU["ProcessorId"].ToString();
                name=CPU["name"].ToString();
                CurrentClockSpeed=CPU["CurrentClockSpeed"].ToString();
                DataWidth=CPU["DataWidth"].ToString();
                DeviceID=CPU["DeviceID"].ToString();
                L2CacheSize=CPU["L2CacheSize"].ToString();
                L3CacheSpeed=CPU["L3CacheSpeed"].ToString();
                Manufacturer=CPU["Manufacturer"].ToString();
                NumberOfCores=CPU["NumberOfCores"].ToString();
                NumberOfLogicalProcessors=CPU["NumberOfLogicalProcessors"].ToString();
                Status=CPU["Status"].ToString();
            }

                ArrayListCpu.Add(ProcessorId);
                ArrayListCpu.Add(name);
                ArrayListCpu.Add(CurrentClockSpeed);
                ArrayListCpu.Add(DataWidth);
                ArrayListCpu.Add(DeviceID);
                ArrayListCpu.Add(L2CacheSize);
                 ArrayListCpu.Add(L3CacheSpeed);
                ArrayListCpu.Add(Manufacturer);
                ArrayListCpu.Add(NumberOfCores);
                ArrayListCpu.Add(NumberOfLogicalProcessors);
                ArrayListCpu.Add(Status);

                return ArrayListCpu;
                        
            
        }


        public ArrayList MotherBoard()
        {          
            
            ArrayList ArrayListMB = new ArrayList();
            String Description = "";
            String Manufacturer = "";
            String Name = "";
            String Product = "";
            String Status = "";

            myScope.Connect();
            objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            opsObserver = new ManagementOperationObserver();
            objSearcher.Scope = myScope;
            foreach (ManagementObject MB in objSearcher.Get())
            {

                MB.Get();
                Description = MB["Description"].ToString();
                Manufacturer = MB["Manufacturer"].ToString();
                Name = MB["Name"].ToString();
                Product = MB["Product"].ToString();
                Status = MB["Status"].ToString();
            }

                ArrayListMB.Add(Description);
                ArrayListMB.Add(Manufacturer);
                ArrayListMB.Add(Name);
                ArrayListMB.Add(Product);
                ArrayListMB.Add(Status);
                              
            return ArrayListMB;
            
        }

        public ArrayList HDisk()
        {
            ArrayList ArrayListHD = new ArrayList();
            Double TotalSize = 0;

            String Mediatype = "";            
            String Manufacturer = "";
            String size = "";
            String Name = "";
            String Description = "";
            String Partitions = "";
            String SerialNumber = "";

            myScope.Connect();
            objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            opsObserver = new ManagementOperationObserver();
            objSearcher.Scope = myScope;
            foreach (ManagementObject HD in objSearcher.Get())
            {
                HD.Get();
                Mediatype = HD["Mediatype"].ToString();
                Manufacturer = HD["Manufacturer"].ToString();
                size = HD["size"].ToString();                
                Name = HD["Name"].ToString();
                Description = HD["Description"].ToString();
                Partitions = HD["Partitions"].ToString();
                SerialNumber = HD["SerialNumber"].ToString();
                
                if (Mediatype != "External hard disk media") // valida que no sea un disco externo
                {

                    double TSize = double.Parse(size); //convierte tamaño disco a double para sumarlo

                    ArrayListHD.Add(Manufacturer);
                    TotalSize = TotalSize + TSize;
                    ArrayListHD.Add(Name);
                    ArrayListHD.Add(Description);
                    ArrayListHD.Add(Partitions);
                    ArrayListHD.Add(SerialNumber);
                                       
                }
             
            }
            string StrSize;
            //double TSize = double.Parse(TotalSize);
            TotalSize = (TotalSize / 1073741824); //// Se divide por 1073741824 cantidad de bytes en un GB
            TotalSize = Math.Round((TotalSize), 0);
            StrSize = System.Convert.ToString(TotalSize);                    
            ArrayListHD.Add(StrSize); // suma total de discos duros

            return ArrayListHD;

        }

        public ArrayList RAM()
        {
            ArrayList ArrayListRam = new ArrayList();

            String cRam = "";
            string StrRam;

            myScope.Connect();
            objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            opsObserver = new ManagementOperationObserver();
            objSearcher.Scope = myScope;

            foreach (ManagementObject RAM in objSearcher.Get())
            {

                RAM.Get();
                cRam = RAM["TotalPhysicalMemory"].ToString();

            }
            
            double TRam= double.Parse(cRam);
            TRam = (TRam / 1073741824); //1gb en bytes (1073741824)
            TRam = Math.Round((TRam), 0);
            StrRam = System.Convert.ToString(TRam);
            ArrayListRam.Add(StrRam);

            return ArrayListRam;
        }

        public ArrayList Monitor()
        {
            ArrayList ArrayListMonitor = new ArrayList();
            String Availability = "";
            String Caption = "";
            String CreationClassName = "";
            String Description = "";
            String DeviceID = "";
            String MonitorType = "";
            String PNPDeviceID = "";
            String Status = "";
            String ScreenHeight = "";
            String ScreenWidth = "";

            myScope.Connect();
            objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Desktopmonitor");
            opsObserver = new ManagementOperationObserver();
            objSearcher.Scope = myScope;

            foreach (ManagementObject Moni in objSearcher.Get())
            {
                             
                Availability = Moni["Availability"].ToString();
                Caption = Moni["Caption"].ToString();
                CreationClassName = Moni["CreationClassName"].ToString();
                Description = Moni["Description"].ToString();
                DeviceID = Moni["DeviceID"].ToString();                               
                MonitorType = Moni["MonitorType"].ToString();
                PNPDeviceID = Moni["PNPDeviceID"].ToString();
                Status = Moni["Status"].ToString();
                ScreenHeight = Moni["ScreenHeight"].ToString();
                ScreenWidth = Moni["ScreenWidth"].ToString();
                               

            }

            if (Availability=="3")
            {
                Availability = "Running-Full Power";
            }
            else if (Availability == "7")
            {
                Availability = "Warning";
            }
            else if (Availability == "7")
            {
                Availability = "Power OFF";
            }
            else if (Availability == "11")
            {
                Availability = "Not Installed";
            }
            else if (Availability == "12")
            {
                Availability = "Install Error"; 
            }
            else if (Availability == "15")
            {
                Availability = "Power Save";
            }
                        
            ArrayListMonitor.Add(Availability);
            ArrayListMonitor.Add(Caption);
            ArrayListMonitor.Add(CreationClassName);
            ArrayListMonitor.Add(Description);
            ArrayListMonitor.Add(DeviceID);
            ArrayListMonitor.Add(MonitorType);
            ArrayListMonitor.Add(PNPDeviceID);
            ArrayListMonitor.Add(Status);
            ArrayListMonitor.Add(ScreenHeight);
            ArrayListMonitor.Add(ScreenWidth);

            return ArrayListMonitor;
}

        private void btnSottware_Click(object sender, EventArgs e)
        {

        }




        //    private void Iprograms()
        //    {
        //        dt = new DataTable();
        //        string[] columnNamesP = { "Name", "InstallDate",
        //        "Version", "Language", "InstallLocation", "URLInfoAbout", "Description" };
        //     Hashtable hs = new Hashtable();
        //     int width = dataGridView2.Width;
        //     int singleColWidth;
        //     DataColumn[] dc = new DataColumn[7];
        //        // DataTable dt;
        //        InitializeComponent();

        //        singleColWidth = width / dt.Columns.Count;

        //        myScope.Connect();            
        //        objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
        //        opsObserver = new ManagementOperationObserver();
        //        objSearcher.Scope = myScope;
        //            //objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
        //            //opsObserver = new ManagementOperationObserver();
        //            //objSearcher.Scope = myScope;
        //            string[] sep = { "\n", "\t" };
        //        // toolStripStatusLabel1.Text = string.Empty;
        //        //toolStripStatusLabel1.Text = "Autenticacion Correcta... Datos Obtenidos";

        //        for (int i = 0; i < columnNames.Length; i++)
        //        {
        //            dc[i] = new DataColumn(columnNames[i], typeof(string));
        //        }
        //        dt.Columns.AddRange(dc);


        //        dt.Rows.Clear();
        //        foreach (ManagementObject obj in objSearcher.Get())
        //        {
        //            string caption = obj.GetText(TextFormat.Mof);
        //            string[] split = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        //            DataRow dr = dt.NewRow();
        //            // Iterate through the splitter
        //            for (int i = 0; i < split.Length; i++)
        //            {
        //                if (split[i].Split('=').Length > 1)
        //                {
        //                    string[] progDetails = split[i].Split('=');
        //                    progDetails[1] = progDetails[1].Replace(@"""", "");
        //                    progDetails[1] = progDetails[1].Replace(';', ' ');
        //                    switch (progDetails[0].Trim().ToLower())
        //                    {
        //                        case "Name":
        //                            dr[dc[0]] = progDetails[1];
        //                            break;
        //                        case "InstallDate":
        //                            dr[dc[6]] = progDetails[1];
        //                            break;
        //                        case "Version":
        //                            dr[dc[1]] = progDetails[1];
        //                            break;
        //                        case "Language":
        //                            dr[dc[2]] = progDetails[1];
        //                            break;
        //                        case "InstallLocation":
        //                            dr[dc[3]] = progDetails[1];
        //                            break;
        //                        case "URLInfoAbout":
        //                            dr[dc[4]] = progDetails[1];
        //                            break;
        //                        case "Description":
        //                            dr[dc[5]] = progDetails[1];
        //                            break;                            
        //                    }
        //                }
        //            }
        //            dt.Rows.Add(dr);
        //        }
        //    }
        //bindingSource2.DataSource = dt.DefaultView
        //foreach (DataColumn col in dt.Columns)
        //{
        //    DataGridViewTextBoxColumn dvc = new DataGridViewTextBoxColumn();
        //    dvc.ToolTipText = col.ColumnName;
        //    dvc.Name = col.ColumnName;
        //    dvc.HeaderText = col.ColumnName;
        //    dvc.DataPropertyName = col.ColumnName;
        //    dvc.Width = singleColWidth;
        //    dataGridView2.Columns.Add(dvc);
        //}
        ////grpStartNewProcess.Enabled = true;
        ////btnEndProcess.Enabled = true;

        //return DataColumn;
        //}
        //}
        //}


        //private void btnHardware_Click(object sender, EventArgs e)
        //    {

        //        TreeNode xfatherNode;
        //        TreeNode xSonNode;

        //        //Cpu();
        //        //MotherBoard();
        //        //HDisk();
        //        //RAM();
        //        //Monitor();

        //        //ConnectToRemoteMachine();
        //        ////llena  procesos al treeview de datos hardware

        //        xfatherNode = TreeViewHardware.Nodes.Add("Procesador:");
        //        foreach (String tn in Cpu())
        //        {
        //            string myStn = tn.ToString();    
        //            xSonNode = xfatherNode.Nodes.Add(myStn);
        //        }
        //        xfatherNode = TreeViewHardware.Nodes.Add("Placa Madre:");
        //        foreach (String tn in MotherBoard())
        //        {
        //            string myStn = tn.ToString();   
        //            xSonNode = xfatherNode.Nodes.Add(myStn);
        //        }


        //        xfatherNode = TreeViewHardware.Nodes.Add("Ram:");
        //        foreach (String tn in RAM())
        //        {
        //            string myStn = tn.ToString();    
        //            xSonNode = xfatherNode.Nodes.Add(myStn);
        //        }


        //        xfatherNode = TreeViewHardware.Nodes.Add("Disco Duro:");
        //        foreach (String tn in HDisk())
        //        {
        //            string myStn = tn.ToString();    
        //            xSonNode = xfatherNode.Nodes.Add(myStn);
        //        }

        //        xfatherNode = TreeViewHardware.Nodes.Add("Monitor:");
        //        foreach (String tn in Monitor())
        //        {
        //            string myStn = tn.ToString();   
        //            xSonNode = xfatherNode.Nodes.Add(myStn);
        //        }

        //    }
        //}


        // static class Global()
        // {
        // //Variable unica para los logs
        // public static String value1 = "";

        // public static string value1
        // {
        //     string Value;
        //     get { return value1; }
        //     set { value1 = value; }
        // }
        // public static String value2 = "hola";
        // public static String value3 ="";
        // public static String value4 = "";
        // public static String value5 = "";
        // }
        ////} 
        //}
    }
}