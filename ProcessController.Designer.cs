namespace WinApp
{
    partial class ProcessController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.chkUseDomain = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnGetMachines = new System.Windows.Forms.Button();
            this.cmdMachinesInDomain = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDomainList = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.terminateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxSoftware = new System.Windows.Forms.CheckBox();
            this.checkBoxHardware = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpStartNewProcess = new System.Windows.Forms.GroupBox();
            this.lblNewProcess = new System.Windows.Forms.Label();
            this.btnStartNew = new System.Windows.Forms.Button();
            this.txtNewProcess = new System.Windows.Forms.TextBox();
            this.btnEndProcess = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnHardware = new System.Windows.Forms.Button();
            this.TreeViewHardware = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnSottware = new System.Windows.Forms.Button();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpStartNewProcess.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbUsers);
            this.groupBox1.Controls.Add(this.chkUseDomain);
            this.groupBox1.Controls.Add(this.statusStrip1);
            this.groupBox1.Controls.Add(this.btnGetMachines);
            this.groupBox1.Controls.Add(this.cmdMachinesInDomain);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDomainList);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Location = new System.Drawing.Point(6, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 272);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acceso Procesos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Usuarios";
            this.label5.Visible = false;
            // 
            // cmbUsers
            // 
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(390, 109);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(65, 21);
            this.cmbUsers.Sorted = true;
            this.cmbUsers.TabIndex = 3;
            this.cmbUsers.Visible = false;
            // 
            // chkUseDomain
            // 
            this.chkUseDomain.AutoSize = true;
            this.chkUseDomain.Checked = true;
            this.chkUseDomain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDomain.Location = new System.Drawing.Point(390, 138);
            this.chkUseDomain.Name = "chkUseDomain";
            this.chkUseDomain.Size = new System.Drawing.Size(143, 17);
            this.chkUseDomain.TabIndex = 6;
            this.chkUseDomain.Text = "Credenciales de Dominio";
            this.chkUseDomain.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 247);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(573, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // btnGetMachines
            // 
            this.btnGetMachines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetMachines.Location = new System.Drawing.Point(159, 47);
            this.btnGetMachines.Name = "btnGetMachines";
            this.btnGetMachines.Size = new System.Drawing.Size(120, 23);
            this.btnGetMachines.TabIndex = 1;
            this.btnGetMachines.Text = "Obtener Equipos";
            this.btnGetMachines.UseVisualStyleBackColor = true;
            this.btnGetMachines.Click += new System.EventHandler(this.btnGetMachines_Click);
            // 
            // cmdMachinesInDomain
            // 
            this.cmdMachinesInDomain.FormattingEnabled = true;
            this.cmdMachinesInDomain.Location = new System.Drawing.Point(159, 76);
            this.cmdMachinesInDomain.Name = "cmdMachinesInDomain";
            this.cmdMachinesInDomain.Size = new System.Drawing.Size(209, 21);
            this.cmdMachinesInDomain.Sorted = true;
            this.cmdMachinesInDomain.TabIndex = 2;
            this.cmdMachinesInDomain.SelectedIndexChanged += new System.EventHandler(this.cmdMachinesInDomain_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nombre Maquina:";
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(159, 162);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Loguear";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(159, 136);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '^';
            this.txtPassword.Size = new System.Drawing.Size(198, 20);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Text = "mbt123";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dominio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre Usuario:";
            // 
            // cmbDomainList
            // 
            this.cmbDomainList.FormattingEnabled = true;
            this.cmbDomainList.Location = new System.Drawing.Point(159, 20);
            this.cmbDomainList.Name = "cmbDomainList";
            this.cmbDomainList.Size = new System.Drawing.Size(198, 21);
            this.cmbDomainList.Sorted = true;
            this.cmbDomainList.TabIndex = 0;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(159, 106);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(198, 20);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.Text = "administrador";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 26);
            // 
            // terminateToolStripMenuItem
            // 
            this.terminateToolStripMenuItem.Name = "terminateToolStripMenuItem";
            this.terminateToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.terminateToolStripMenuItem.Text = "Terminate";
            this.terminateToolStripMenuItem.Click += new System.EventHandler(this.terminateToolStripMenuItem_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Controls.Add(this.tabPage4);
            this.TabControl.Location = new System.Drawing.Point(14, 22);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(613, 435);
            this.TabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxSoftware);
            this.tabPage1.Controls.Add(this.checkBoxHardware);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(605, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Acceso";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxSoftware
            // 
            this.checkBoxSoftware.AutoSize = true;
            this.checkBoxSoftware.Checked = true;
            this.checkBoxSoftware.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSoftware.Location = new System.Drawing.Point(335, 321);
            this.checkBoxSoftware.Name = "checkBoxSoftware";
            this.checkBoxSoftware.Size = new System.Drawing.Size(117, 17);
            this.checkBoxSoftware.TabIndex = 15;
            this.checkBoxSoftware.Text = "Obtener Programas";
            this.checkBoxSoftware.UseVisualStyleBackColor = true;
            // 
            // checkBoxHardware
            // 
            this.checkBoxHardware.AutoSize = true;
            this.checkBoxHardware.Checked = true;
            this.checkBoxHardware.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHardware.Location = new System.Drawing.Point(92, 321);
            this.checkBoxHardware.Name = "checkBoxHardware";
            this.checkBoxHardware.Size = new System.Drawing.Size(111, 17);
            this.checkBoxHardware.TabIndex = 16;
            this.checkBoxHardware.Text = "Obtener hardware";
            this.checkBoxHardware.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpStartNewProcess);
            this.tabPage2.Controls.Add(this.btnEndProcess);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(605, 409);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Procesos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpStartNewProcess
            // 
            this.grpStartNewProcess.Controls.Add(this.lblNewProcess);
            this.grpStartNewProcess.Controls.Add(this.btnStartNew);
            this.grpStartNewProcess.Controls.Add(this.txtNewProcess);
            this.grpStartNewProcess.Enabled = false;
            this.grpStartNewProcess.Location = new System.Drawing.Point(20, 331);
            this.grpStartNewProcess.Name = "grpStartNewProcess";
            this.grpStartNewProcess.Size = new System.Drawing.Size(566, 58);
            this.grpStartNewProcess.TabIndex = 12;
            this.grpStartNewProcess.TabStop = false;
            // 
            // lblNewProcess
            // 
            this.lblNewProcess.AutoSize = true;
            this.lblNewProcess.Location = new System.Drawing.Point(6, 25);
            this.lblNewProcess.Name = "lblNewProcess";
            this.lblNewProcess.Size = new System.Drawing.Size(89, 13);
            this.lblNewProcess.TabIndex = 2;
            this.lblNewProcess.Text = "Lanzar Procesos:";
            // 
            // btnStartNew
            // 
            this.btnStartNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartNew.Location = new System.Drawing.Point(457, 17);
            this.btnStartNew.Name = "btnStartNew";
            this.btnStartNew.Size = new System.Drawing.Size(103, 23);
            this.btnStartNew.TabIndex = 1;
            this.btnStartNew.Text = "Lanzar";
            this.btnStartNew.UseVisualStyleBackColor = true;
            this.btnStartNew.Click += new System.EventHandler(this.btnStartNew_Click_1);
            // 
            // txtNewProcess
            // 
            this.txtNewProcess.Location = new System.Drawing.Point(101, 19);
            this.txtNewProcess.Name = "txtNewProcess";
            this.txtNewProcess.Size = new System.Drawing.Size(328, 20);
            this.txtNewProcess.TabIndex = 0;
            this.txtNewProcess.Text = "notepad.exe";
            // 
            // btnEndProcess
            // 
            this.btnEndProcess.Enabled = false;
            this.btnEndProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEndProcess.Location = new System.Drawing.Point(477, 6);
            this.btnEndProcess.Name = "btnEndProcess";
            this.btnEndProcess.Size = new System.Drawing.Size(106, 23);
            this.btnEndProcess.TabIndex = 11;
            this.btnEndProcess.Text = "Terminar Proceso";
            this.btnEndProcess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEndProcess.UseVisualStyleBackColor = true;
            this.btnEndProcess.Click += new System.EventHandler(this.btnEndProcess_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(17, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 303);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Procesos de la maquina consultada:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.bindingSource1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(563, 284);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnHardware);
            this.tabPage3.Controls.Add(this.TreeViewHardware);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(605, 409);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hardware";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnHardware
            // 
            this.btnHardware.Enabled = false;
            this.btnHardware.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHardware.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnHardware.Location = new System.Drawing.Point(493, 4);
            this.btnHardware.Name = "btnHardware";
            this.btnHardware.Size = new System.Drawing.Size(106, 23);
            this.btnHardware.TabIndex = 12;
            this.btnHardware.Text = "Obtener Datos";
            this.btnHardware.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHardware.UseVisualStyleBackColor = true;
            // 
            // TreeViewHardware
            // 
            this.TreeViewHardware.Location = new System.Drawing.Point(6, 33);
            this.TreeViewHardware.Name = "TreeViewHardware";
            this.TreeViewHardware.Size = new System.Drawing.Size(593, 351);
            this.TreeViewHardware.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView2);
            this.tabPage4.Controls.Add(this.btnSottware);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(605, 409);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Programas";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 49);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(593, 335);
            this.dataGridView2.TabIndex = 14;
            // 
            // btnSottware
            // 
            this.btnSottware.Enabled = false;
            this.btnSottware.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSottware.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSottware.Location = new System.Drawing.Point(465, 6);
            this.btnSottware.Name = "btnSottware";
            this.btnSottware.Size = new System.Drawing.Size(134, 23);
            this.btnSottware.TabIndex = 13;
            this.btnSottware.Text = "Desinstalar";
            this.btnSottware.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSottware.UseVisualStyleBackColor = true;
            this.btnSottware.Click += new System.EventHandler(this.btnSottware_Click);
            // 
            // ProcessController
            // 
            this.AcceptButton = this.btnGetMachines;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 470);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ProcessController";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Controlador de Procesos";
            this.Load += new System.EventHandler(this.ProcessController_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.grpStartNewProcess.ResumeLayout(false);
            this.grpStartNewProcess.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDomainList;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem terminateToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmdMachinesInDomain;
        private System.Windows.Forms.Button btnGetMachines;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox chkUseDomain;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grpStartNewProcess;
        private System.Windows.Forms.Label lblNewProcess;
        private System.Windows.Forms.Button btnStartNew;
        private System.Windows.Forms.TextBox txtNewProcess;
        private System.Windows.Forms.Button btnEndProcess;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView TreeViewHardware;
        private System.Windows.Forms.Button btnHardware;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnSottware;
        private System.Windows.Forms.CheckBox checkBoxSoftware;
        private System.Windows.Forms.CheckBox checkBoxHardware;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource bindingSource2;
    }
}

