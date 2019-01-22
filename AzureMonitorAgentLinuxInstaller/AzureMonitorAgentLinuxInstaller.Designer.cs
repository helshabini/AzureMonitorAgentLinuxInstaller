namespace AzureMonitorAgentLinuxInstaller
{
    partial class AzureMonitorAgentLinuxInstaller
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAgentLocation = new System.Windows.Forms.TabPage();
            this.btnAgentLocationNext = new System.Windows.Forms.Button();
            this.grpFilesLocations = new System.Windows.Forms.GroupBox();
            this.btnx86Browse = new System.Windows.Forms.Button();
            this.txtx86 = new System.Windows.Forms.TextBox();
            this.lblx86 = new System.Windows.Forms.Label();
            this.lblx64 = new System.Windows.Forms.Label();
            this.btnx64Browse = new System.Windows.Forms.Button();
            this.txtx64 = new System.Windows.Forms.TextBox();
            this.rdManual = new System.Windows.Forms.RadioButton();
            this.rdAutoDownload = new System.Windows.Forms.RadioButton();
            this.tabActionsRequired = new System.Windows.Forms.TabPage();
            this.tabActionsRequiredNext = new System.Windows.Forms.Button();
            this.grpProxyDetails = new System.Windows.Forms.GroupBox();
            this.cmbAddressProtocol = new System.Windows.Forms.ComboBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.chkProxyAuthentication = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.grpWorkspaceDetails = new System.Windows.Forms.GroupBox();
            this.txtSharedKey = new System.Windows.Forms.TextBox();
            this.txtWorkspaceId = new System.Windows.Forms.TextBox();
            this.lblSharedKey = new System.Windows.Forms.Label();
            this.lblWorkspaceId = new System.Windows.Forms.Label();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.chkJoinLogAnalytics = new System.Windows.Forms.CheckBox();
            this.chkInstallAgent = new System.Windows.Forms.CheckBox();
            this.chkInstallPrerequisites = new System.Windows.Forms.CheckBox();
            this.tabDestinationServers = new System.Windows.Forms.TabPage();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.grpUploadServersList = new System.Windows.Forms.GroupBox();
            this.btnServersListUpload = new System.Windows.Forms.Button();
            this.btnServersListBrowse = new System.Windows.Forms.Button();
            this.txtServersList = new System.Windows.Forms.TextBox();
            this.lblServersList = new System.Windows.Forms.Label();
            this.gvServersList = new System.Windows.Forms.DataGridView();
            this.shFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.csvFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabAgentLocation.SuspendLayout();
            this.grpFilesLocations.SuspendLayout();
            this.tabActionsRequired.SuspendLayout();
            this.grpProxyDetails.SuspendLayout();
            this.grpWorkspaceDetails.SuspendLayout();
            this.tabDestinationServers.SuspendLayout();
            this.grpUploadServersList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvServersList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAgentLocation);
            this.tabControl.Controls.Add(this.tabActionsRequired);
            this.tabControl.Controls.Add(this.tabDestinationServers);
            this.tabControl.Location = new System.Drawing.Point(13, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(718, 325);
            this.tabControl.TabIndex = 0;
            // 
            // tabAgentLocation
            // 
            this.tabAgentLocation.Controls.Add(this.btnAgentLocationNext);
            this.tabAgentLocation.Controls.Add(this.grpFilesLocations);
            this.tabAgentLocation.Controls.Add(this.rdManual);
            this.tabAgentLocation.Controls.Add(this.rdAutoDownload);
            this.tabAgentLocation.Location = new System.Drawing.Point(4, 22);
            this.tabAgentLocation.Name = "tabAgentLocation";
            this.tabAgentLocation.Padding = new System.Windows.Forms.Padding(3);
            this.tabAgentLocation.Size = new System.Drawing.Size(710, 299);
            this.tabAgentLocation.TabIndex = 0;
            this.tabAgentLocation.Text = "Agent Location";
            this.tabAgentLocation.UseVisualStyleBackColor = true;
            // 
            // btnAgentLocationNext
            // 
            this.btnAgentLocationNext.Location = new System.Drawing.Point(629, 270);
            this.btnAgentLocationNext.Name = "btnAgentLocationNext";
            this.btnAgentLocationNext.Size = new System.Drawing.Size(75, 23);
            this.btnAgentLocationNext.TabIndex = 6;
            this.btnAgentLocationNext.Text = "Next >";
            this.btnAgentLocationNext.UseVisualStyleBackColor = true;
            this.btnAgentLocationNext.Click += new System.EventHandler(this.btnAgentLocationNext_Click);
            // 
            // grpFilesLocations
            // 
            this.grpFilesLocations.Controls.Add(this.btnx86Browse);
            this.grpFilesLocations.Controls.Add(this.txtx86);
            this.grpFilesLocations.Controls.Add(this.lblx86);
            this.grpFilesLocations.Controls.Add(this.lblx64);
            this.grpFilesLocations.Controls.Add(this.btnx64Browse);
            this.grpFilesLocations.Controls.Add(this.txtx64);
            this.grpFilesLocations.Enabled = false;
            this.grpFilesLocations.Location = new System.Drawing.Point(7, 53);
            this.grpFilesLocations.Name = "grpFilesLocations";
            this.grpFilesLocations.Size = new System.Drawing.Size(700, 100);
            this.grpFilesLocations.TabIndex = 5;
            this.grpFilesLocations.TabStop = false;
            this.grpFilesLocations.Text = "Files Locations";
            // 
            // btnx86Browse
            // 
            this.btnx86Browse.Location = new System.Drawing.Point(616, 43);
            this.btnx86Browse.Name = "btnx86Browse";
            this.btnx86Browse.Size = new System.Drawing.Size(75, 23);
            this.btnx86Browse.TabIndex = 7;
            this.btnx86Browse.Text = "Browse";
            this.btnx86Browse.UseVisualStyleBackColor = true;
            // 
            // txtx86
            // 
            this.txtx86.Location = new System.Drawing.Point(36, 45);
            this.txtx86.Name = "txtx86";
            this.txtx86.Size = new System.Drawing.Size(574, 20);
            this.txtx86.TabIndex = 6;
            // 
            // lblx86
            // 
            this.lblx86.AutoSize = true;
            this.lblx86.Location = new System.Drawing.Point(6, 48);
            this.lblx86.Name = "lblx86";
            this.lblx86.Size = new System.Drawing.Size(24, 13);
            this.lblx86.TabIndex = 5;
            this.lblx86.Text = "x86";
            // 
            // lblx64
            // 
            this.lblx64.AutoSize = true;
            this.lblx64.Location = new System.Drawing.Point(6, 22);
            this.lblx64.Name = "lblx64";
            this.lblx64.Size = new System.Drawing.Size(24, 13);
            this.lblx64.TabIndex = 4;
            this.lblx64.Text = "x64";
            // 
            // btnx64Browse
            // 
            this.btnx64Browse.Location = new System.Drawing.Point(616, 17);
            this.btnx64Browse.Name = "btnx64Browse";
            this.btnx64Browse.Size = new System.Drawing.Size(75, 23);
            this.btnx64Browse.TabIndex = 3;
            this.btnx64Browse.Text = "Browse";
            this.btnx64Browse.UseVisualStyleBackColor = true;
            // 
            // txtx64
            // 
            this.txtx64.Location = new System.Drawing.Point(36, 19);
            this.txtx64.Name = "txtx64";
            this.txtx64.Size = new System.Drawing.Size(574, 20);
            this.txtx64.TabIndex = 2;
            // 
            // rdManual
            // 
            this.rdManual.AutoSize = true;
            this.rdManual.Location = new System.Drawing.Point(6, 29);
            this.rdManual.Name = "rdManual";
            this.rdManual.Size = new System.Drawing.Size(177, 17);
            this.rdManual.TabIndex = 1;
            this.rdManual.Text = "Manually specify agent files (.sh)";
            this.rdManual.UseVisualStyleBackColor = true;
            this.rdManual.CheckedChanged += new System.EventHandler(this.rdManual_CheckedChanged);
            // 
            // rdAutoDownload
            // 
            this.rdAutoDownload.AutoSize = true;
            this.rdAutoDownload.Checked = true;
            this.rdAutoDownload.Location = new System.Drawing.Point(6, 6);
            this.rdAutoDownload.Name = "rdAutoDownload";
            this.rdAutoDownload.Size = new System.Drawing.Size(259, 17);
            this.rdAutoDownload.TabIndex = 0;
            this.rdAutoDownload.TabStop = true;
            this.rdAutoDownload.Text = "Download and use the latest agents from the web";
            this.rdAutoDownload.UseVisualStyleBackColor = true;
            this.rdAutoDownload.CheckedChanged += new System.EventHandler(this.rdAutoDownload_CheckedChanged);
            // 
            // tabActionsRequired
            // 
            this.tabActionsRequired.Controls.Add(this.tabActionsRequiredNext);
            this.tabActionsRequired.Controls.Add(this.grpProxyDetails);
            this.tabActionsRequired.Controls.Add(this.grpWorkspaceDetails);
            this.tabActionsRequired.Controls.Add(this.chkUseProxy);
            this.tabActionsRequired.Controls.Add(this.chkJoinLogAnalytics);
            this.tabActionsRequired.Controls.Add(this.chkInstallAgent);
            this.tabActionsRequired.Controls.Add(this.chkInstallPrerequisites);
            this.tabActionsRequired.Location = new System.Drawing.Point(4, 22);
            this.tabActionsRequired.Name = "tabActionsRequired";
            this.tabActionsRequired.Padding = new System.Windows.Forms.Padding(3);
            this.tabActionsRequired.Size = new System.Drawing.Size(710, 299);
            this.tabActionsRequired.TabIndex = 1;
            this.tabActionsRequired.Text = "Actions Required";
            this.tabActionsRequired.UseVisualStyleBackColor = true;
            // 
            // tabActionsRequiredNext
            // 
            this.tabActionsRequiredNext.Location = new System.Drawing.Point(629, 270);
            this.tabActionsRequiredNext.Name = "tabActionsRequiredNext";
            this.tabActionsRequiredNext.Size = new System.Drawing.Size(75, 23);
            this.tabActionsRequiredNext.TabIndex = 6;
            this.tabActionsRequiredNext.Text = "Next >";
            this.tabActionsRequiredNext.UseVisualStyleBackColor = true;
            this.tabActionsRequiredNext.Click += new System.EventHandler(this.tabActionsRequiredNext_Click);
            // 
            // grpProxyDetails
            // 
            this.grpProxyDetails.Controls.Add(this.cmbAddressProtocol);
            this.grpProxyDetails.Controls.Add(this.lblPassword);
            this.grpProxyDetails.Controls.Add(this.chkProxyAuthentication);
            this.grpProxyDetails.Controls.Add(this.txtPassword);
            this.grpProxyDetails.Controls.Add(this.txtPort);
            this.grpProxyDetails.Controls.Add(this.txtUsername);
            this.grpProxyDetails.Controls.Add(this.txtAddress);
            this.grpProxyDetails.Controls.Add(this.lblPort);
            this.grpProxyDetails.Controls.Add(this.lblAddress);
            this.grpProxyDetails.Controls.Add(this.lblUsername);
            this.grpProxyDetails.Enabled = false;
            this.grpProxyDetails.Location = new System.Drawing.Point(7, 180);
            this.grpProxyDetails.Name = "grpProxyDetails";
            this.grpProxyDetails.Size = new System.Drawing.Size(700, 77);
            this.grpProxyDetails.TabIndex = 5;
            this.grpProxyDetails.TabStop = false;
            this.grpProxyDetails.Text = "Proxy Details";
            // 
            // cmbAddressProtocol
            // 
            this.cmbAddressProtocol.FormattingEnabled = true;
            this.cmbAddressProtocol.Items.AddRange(new object[] {
            "http",
            "https"});
            this.cmbAddressProtocol.Location = new System.Drawing.Point(58, 19);
            this.cmbAddressProtocol.Name = "cmbAddressProtocol";
            this.cmbAddressProtocol.Size = new System.Drawing.Size(45, 21);
            this.cmbAddressProtocol.TabIndex = 9;
            this.cmbAddressProtocol.Text = "http";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(532, 50);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // chkProxyAuthentication
            // 
            this.chkProxyAuthentication.AutoSize = true;
            this.chkProxyAuthentication.Checked = true;
            this.chkProxyAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProxyAuthentication.Location = new System.Drawing.Point(10, 47);
            this.chkProxyAuthentication.Name = "chkProxyAuthentication";
            this.chkProxyAuthentication.Size = new System.Drawing.Size(168, 17);
            this.chkProxyAuthentication.TabIndex = 8;
            this.chkProxyAuthentication.Text = "Proxy Requires Authentication";
            this.chkProxyAuthentication.UseVisualStyleBackColor = true;
            this.chkProxyAuthentication.CheckedChanged += new System.EventHandler(this.chkProxyAuthentication_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(591, 47);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(654, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(37, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "80";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(426, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 6;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(109, 19);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(507, 20);
            this.txtAddress.TabIndex = 2;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(622, 22);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Port";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(7, 22);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Address";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(365, 50);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username";
            // 
            // grpWorkspaceDetails
            // 
            this.grpWorkspaceDetails.Controls.Add(this.txtSharedKey);
            this.grpWorkspaceDetails.Controls.Add(this.txtWorkspaceId);
            this.grpWorkspaceDetails.Controls.Add(this.lblSharedKey);
            this.grpWorkspaceDetails.Controls.Add(this.lblWorkspaceId);
            this.grpWorkspaceDetails.Location = new System.Drawing.Point(7, 77);
            this.grpWorkspaceDetails.Name = "grpWorkspaceDetails";
            this.grpWorkspaceDetails.Size = new System.Drawing.Size(700, 74);
            this.grpWorkspaceDetails.TabIndex = 4;
            this.grpWorkspaceDetails.TabStop = false;
            this.grpWorkspaceDetails.Text = "Workspace Details";
            // 
            // txtSharedKey
            // 
            this.txtSharedKey.Location = new System.Drawing.Point(87, 43);
            this.txtSharedKey.Name = "txtSharedKey";
            this.txtSharedKey.Size = new System.Drawing.Size(604, 20);
            this.txtSharedKey.TabIndex = 3;
            this.txtSharedKey.Text = "s2d/ApE94rHaEW4V3gRfUM8odEhVxye6w5nIZmz5juBBvH96xxxVNSczpu1UQ4klRYW1hRhdtlBBkfhwr" +
    "hLusQ==";
            // 
            // txtWorkspaceId
            // 
            this.txtWorkspaceId.Location = new System.Drawing.Point(87, 17);
            this.txtWorkspaceId.Name = "txtWorkspaceId";
            this.txtWorkspaceId.Size = new System.Drawing.Size(221, 20);
            this.txtWorkspaceId.TabIndex = 2;
            this.txtWorkspaceId.Text = "73150BE8-965A-407D-BC63-7222687819F0";
            // 
            // lblSharedKey
            // 
            this.lblSharedKey.AutoSize = true;
            this.lblSharedKey.Location = new System.Drawing.Point(7, 46);
            this.lblSharedKey.Name = "lblSharedKey";
            this.lblSharedKey.Size = new System.Drawing.Size(62, 13);
            this.lblSharedKey.TabIndex = 1;
            this.lblSharedKey.Text = "Shared Key";
            // 
            // lblWorkspaceId
            // 
            this.lblWorkspaceId.AutoSize = true;
            this.lblWorkspaceId.Location = new System.Drawing.Point(7, 20);
            this.lblWorkspaceId.Name = "lblWorkspaceId";
            this.lblWorkspaceId.Size = new System.Drawing.Size(74, 13);
            this.lblWorkspaceId.TabIndex = 0;
            this.lblWorkspaceId.Text = "Workspace Id";
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.Location = new System.Drawing.Point(7, 157);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(192, 17);
            this.chkUseProxy.TabIndex = 3;
            this.chkUseProxy.Text = "Use Proxy Server or OMS Gateway";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
            // 
            // chkJoinLogAnalytics
            // 
            this.chkJoinLogAnalytics.AutoSize = true;
            this.chkJoinLogAnalytics.Checked = true;
            this.chkJoinLogAnalytics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJoinLogAnalytics.Location = new System.Drawing.Point(7, 54);
            this.chkJoinLogAnalytics.Name = "chkJoinLogAnalytics";
            this.chkJoinLogAnalytics.Size = new System.Drawing.Size(169, 17);
            this.chkJoinLogAnalytics.TabIndex = 2;
            this.chkJoinLogAnalytics.Text = "Join Log Analytics Workspace";
            this.chkJoinLogAnalytics.UseVisualStyleBackColor = true;
            this.chkJoinLogAnalytics.CheckedChanged += new System.EventHandler(this.chkJoinLogAnalytics_CheckedChanged);
            // 
            // chkInstallAgent
            // 
            this.chkInstallAgent.AutoSize = true;
            this.chkInstallAgent.Checked = true;
            this.chkInstallAgent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInstallAgent.Location = new System.Drawing.Point(7, 30);
            this.chkInstallAgent.Name = "chkInstallAgent";
            this.chkInstallAgent.Size = new System.Drawing.Size(84, 17);
            this.chkInstallAgent.TabIndex = 1;
            this.chkInstallAgent.Text = "Install Agent";
            this.chkInstallAgent.UseVisualStyleBackColor = true;
            // 
            // chkInstallPrerequisites
            // 
            this.chkInstallPrerequisites.AutoSize = true;
            this.chkInstallPrerequisites.Enabled = false;
            this.chkInstallPrerequisites.Location = new System.Drawing.Point(7, 7);
            this.chkInstallPrerequisites.Name = "chkInstallPrerequisites";
            this.chkInstallPrerequisites.Size = new System.Drawing.Size(207, 17);
            this.chkInstallPrerequisites.TabIndex = 0;
            this.chkInstallPrerequisites.Text = "Install Prerequisites (not yet supported)";
            this.chkInstallPrerequisites.UseVisualStyleBackColor = true;
            // 
            // tabDestinationServers
            // 
            this.tabDestinationServers.Controls.Add(this.btnDeploy);
            this.tabDestinationServers.Controls.Add(this.grpUploadServersList);
            this.tabDestinationServers.Controls.Add(this.gvServersList);
            this.tabDestinationServers.Location = new System.Drawing.Point(4, 22);
            this.tabDestinationServers.Name = "tabDestinationServers";
            this.tabDestinationServers.Size = new System.Drawing.Size(710, 299);
            this.tabDestinationServers.TabIndex = 2;
            this.tabDestinationServers.Text = "Destination Servers";
            this.tabDestinationServers.UseVisualStyleBackColor = true;
            // 
            // btnDeploy
            // 
            this.btnDeploy.Location = new System.Drawing.Point(632, 273);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(75, 23);
            this.btnDeploy.TabIndex = 7;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // grpUploadServersList
            // 
            this.grpUploadServersList.Controls.Add(this.btnServersListUpload);
            this.grpUploadServersList.Controls.Add(this.btnServersListBrowse);
            this.grpUploadServersList.Controls.Add(this.txtServersList);
            this.grpUploadServersList.Controls.Add(this.lblServersList);
            this.grpUploadServersList.Location = new System.Drawing.Point(4, 4);
            this.grpUploadServersList.Name = "grpUploadServersList";
            this.grpUploadServersList.Size = new System.Drawing.Size(703, 78);
            this.grpUploadServersList.TabIndex = 1;
            this.grpUploadServersList.TabStop = false;
            this.grpUploadServersList.Text = "Upload Servers List";
            // 
            // btnServersListUpload
            // 
            this.btnServersListUpload.Location = new System.Drawing.Point(619, 47);
            this.btnServersListUpload.Name = "btnServersListUpload";
            this.btnServersListUpload.Size = new System.Drawing.Size(75, 23);
            this.btnServersListUpload.TabIndex = 5;
            this.btnServersListUpload.Text = "Upload";
            this.btnServersListUpload.UseVisualStyleBackColor = true;
            // 
            // btnServersListBrowse
            // 
            this.btnServersListBrowse.Location = new System.Drawing.Point(619, 18);
            this.btnServersListBrowse.Name = "btnServersListBrowse";
            this.btnServersListBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnServersListBrowse.TabIndex = 4;
            this.btnServersListBrowse.Text = "Browse";
            this.btnServersListBrowse.UseVisualStyleBackColor = true;
            // 
            // txtServersList
            // 
            this.txtServersList.Location = new System.Drawing.Point(103, 20);
            this.txtServersList.Name = "txtServersList";
            this.txtServersList.Size = new System.Drawing.Size(510, 20);
            this.txtServersList.TabIndex = 1;
            // 
            // lblServersList
            // 
            this.lblServersList.AutoSize = true;
            this.lblServersList.Location = new System.Drawing.Point(6, 23);
            this.lblServersList.Name = "lblServersList";
            this.lblServersList.Size = new System.Drawing.Size(91, 13);
            this.lblServersList.TabIndex = 0;
            this.lblServersList.Text = "Servers List (.csv)";
            // 
            // gvServersList
            // 
            this.gvServersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvServersList.Location = new System.Drawing.Point(3, 88);
            this.gvServersList.Name = "gvServersList";
            this.gvServersList.Size = new System.Drawing.Size(704, 179);
            this.gvServersList.TabIndex = 0;
            // 
            // shFileDialog
            // 
            this.shFileDialog.FileName = "shFileDialog";
            // 
            // csvFileDialog
            // 
            this.csvFileDialog.FileName = "csvFileDialog";
            // 
            // AzureMonitorAgentLinuxInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 351);
            this.Controls.Add(this.tabControl);
            this.Name = "AzureMonitorAgentLinuxInstaller";
            this.Text = "Azure Monitor Agent Linux Installer";
            this.tabControl.ResumeLayout(false);
            this.tabAgentLocation.ResumeLayout(false);
            this.tabAgentLocation.PerformLayout();
            this.grpFilesLocations.ResumeLayout(false);
            this.grpFilesLocations.PerformLayout();
            this.tabActionsRequired.ResumeLayout(false);
            this.tabActionsRequired.PerformLayout();
            this.grpProxyDetails.ResumeLayout(false);
            this.grpProxyDetails.PerformLayout();
            this.grpWorkspaceDetails.ResumeLayout(false);
            this.grpWorkspaceDetails.PerformLayout();
            this.tabDestinationServers.ResumeLayout(false);
            this.grpUploadServersList.ResumeLayout(false);
            this.grpUploadServersList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvServersList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAgentLocation;
        private System.Windows.Forms.RadioButton rdManual;
        private System.Windows.Forms.RadioButton rdAutoDownload;
        private System.Windows.Forms.TabPage tabActionsRequired;
        private System.Windows.Forms.GroupBox grpProxyDetails;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.GroupBox grpWorkspaceDetails;
        private System.Windows.Forms.TextBox txtSharedKey;
        private System.Windows.Forms.TextBox txtWorkspaceId;
        private System.Windows.Forms.Label lblSharedKey;
        private System.Windows.Forms.Label lblWorkspaceId;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.CheckBox chkJoinLogAnalytics;
        private System.Windows.Forms.CheckBox chkInstallAgent;
        private System.Windows.Forms.CheckBox chkInstallPrerequisites;
        private System.Windows.Forms.TabPage tabDestinationServers;
        private System.Windows.Forms.GroupBox grpFilesLocations;
        private System.Windows.Forms.Label lblx64;
        private System.Windows.Forms.Button btnx64Browse;
        private System.Windows.Forms.TextBox txtx64;
        private System.Windows.Forms.OpenFileDialog shFileDialog;
        private System.Windows.Forms.Button btnx86Browse;
        private System.Windows.Forms.TextBox txtx86;
        private System.Windows.Forms.Label lblx86;
        private System.Windows.Forms.ComboBox cmbAddressProtocol;
        private System.Windows.Forms.CheckBox chkProxyAuthentication;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.DataGridView gvServersList;
        private System.Windows.Forms.Button btnAgentLocationNext;
        private System.Windows.Forms.Button tabActionsRequiredNext;
        private System.Windows.Forms.GroupBox grpUploadServersList;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.Button btnServersListUpload;
        private System.Windows.Forms.Button btnServersListBrowse;
        private System.Windows.Forms.TextBox txtServersList;
        private System.Windows.Forms.Label lblServersList;
        private System.Windows.Forms.OpenFileDialog csvFileDialog;
    }
}

