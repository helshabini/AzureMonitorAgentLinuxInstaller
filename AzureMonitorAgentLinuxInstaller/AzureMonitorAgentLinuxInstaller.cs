using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureMonitorAgentLinuxInstaller
{
    public partial class AzureMonitorAgentLinuxInstaller : Form
    {

        #region Initialization

        private BindingSource serverlistDataSource;
        private string x64Agent = "omsagent-1.8.1-256.universal.x64.sh";
        private string x86Agent = "omsagent-1.8.1-256.universal.x86.sh";

        public AzureMonitorAgentLinuxInstaller()
        {
            InitializeComponent();
            InitializeServersList();
        }

        private void InitializeServersList()
        {
            serverlistDataSource = new BindingSource();
            serverlistDataSource.Add(new ServerListItem
            {
                Status = ServerListItemStatus.NotStarted,
                ServerName = "autodeploytestvm1",
                IPAddress = "13.80.79.12",
                Port = 22,
                Username = "vmadmin",
                Password = "vmP@ssw0rd123",
                Log = string.Empty
            });
            gvServersList.AutoGenerateColumns = false;
            gvServersList.AutoSize = true;
            gvServersList.DataSource = serverlistDataSource;
            
            // Initialize and add a Progress column
            DataGridViewColumn column = new DataGridViewImageColumn();
            column.DataPropertyName = "Progess";
            column.Name = "Progress";
            gvServersList.Columns.Add(column);

            // Initialize and add a Server Name column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ServerName";
            column.Name = "Server Name";
            gvServersList.Columns.Add(column);

            // Initialize and add a IP Address column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "IPAddress";
            column.Name = "IP Address";
            gvServersList.Columns.Add(column);

            // Initialize and add a Port column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Port";
            column.Name = "Port";
            gvServersList.Columns.Add(column);

            // Initialize and add a Username column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Username";
            column.Name = "Username";
            gvServersList.Columns.Add(column);

            // Initialize and add a Password column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Password";
            column.Name = "Password";
            gvServersList.Columns.Add(column);

            // Initialize and add a Password column.
            column = new DataGridViewLinkColumn();
            column.Name = "Log";
            gvServersList.Columns.Add(column);
        }

        #endregion

        #region UI Manipulation

        private void btnAgentLocationNext_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(1);
        }

        private void tabActionsRequiredNext_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(2);
        }

        private void rdAutoDownload_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAutoDownload.Checked)
                grpFilesLocations.Enabled = false;
        }

        private void rdManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdManual.Checked)
                grpFilesLocations.Enabled = true;
        }

        private void chkJoinLogAnalytics_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJoinLogAnalytics.Checked)
                grpWorkspaceDetails.Enabled = true;
            else
                grpWorkspaceDetails.Enabled = false;
        }

        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseProxy.Checked)
                grpProxyDetails.Enabled = true;
            else
                grpProxyDetails.Enabled = false;
        }

        private void chkProxyAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProxyAuthentication.Checked)
            {
                lblUsername.Enabled = true;
                txtUsername.Enabled = true;
                lblPassword.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                lblUsername.Enabled = false;
                txtUsername.Enabled = false;
                lblPassword.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        #endregion

        #region Functions and Logic

        private ServerDistro CheckDistro(SshClient sshClient)
        {
            var cmd = sshClient.CreateCommand("which rpm > /dev/null 2>&1; echo $?");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Equals("0"))
                    {
                        return ServerDistro.RPM;
                    }
                }
            }
            cmd.EndExecute(result);

            cmd = sshClient.CreateCommand("which dpkg > /dev/null 2>&1; echo $?");
            result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Equals("0"))
                    {
                        return ServerDistro.Debian;
                    }
                }
            }
            cmd.EndExecute(result);

            return ServerDistro.Unknown;
        }

        private void DownloadAgents()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)");
                var response = httpClient.GetStringAsync(new Uri("http://api.github.com/repos/Microsoft/OMS-Agent-for-Linux/releases/latest")).Result;
                dynamic release = JsonConvert.DeserializeObject(response);

                using (WebClient webClient = new WebClient())
                {
                    foreach (var asset in release.assets)
                    {
                        if (!File.Exists(asset.name.ToString()))
                            //Log: Downloading File asset.name.ToString()
                            webClient.DownloadFile(asset.browser_download_url.ToString(), asset.name.ToString());
                        else
                        {
                            //Log: File already exists, skipping download
                        }
                    }
                }
            }
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            //Logic of operation. A little messy but I'm short on time...

            if (rdAutoDownload.Checked)
                DownloadAgents();

            foreach (var item in serverlistDataSource)
            {
                ServerListItem server = item as ServerListItem;
                var connectionInfo = new ConnectionInfo(server.IPAddress, server.Port, server.Username, new PasswordAuthenticationMethod(server.Username, server.Password));
                using (var sshClient = new SshClient(connectionInfo))
                {
                    switch (CheckDistro(sshClient))
                    {
                        case ServerDistro.RPM:
                            if (!CheckPrereqsRPM(sshClient))
                            {
                                //Log: Error, prereqs not ready
                                continue;
                            }
                            break;
                        case ServerDistro.Debian:
                            if (!CheckPrereqsDebian(sshClient))
                            {
                                //Log: Error, prereqs not ready
                                continue;
                            }
                            break;
                    }

                    if (chkInstallPrerequisites.Checked)
                        InstallPrereqs();
                    if (rdAutoDownload.Checked)
                        TransferAgents();
                    if (chkInstallAgent.Checked && chkJoinLogAnalytics.Checked && chkProxyAuthentication.Checked)
                        InstallAgentAndOnboardAndSetProxy();
                    else if (chkInstallAgent.Checked && chkJoinLogAnalytics.Checked)
                        InstallAgentAndOnboard();
                    else if (!chkInstallAgent.Checked && chkJoinLogAnalytics.Checked && chkProxyAuthentication.Checked)
                        SwitchWorkspaceAndSetProxy();
                    else if (!chkInstallAgent.Checked && chkJoinLogAnalytics.Checked && !chkProxyAuthentication.Checked)
                        SwitchWorkspace();
                    else if (!chkInstallAgent.Checked && !chkJoinLogAnalytics.Checked && chkProxyAuthentication.Checked)
                        SetProxy();


            //Loop on servers in the grid view
            

                
                    sshClient.Connect();
                    string agentPath = string.Empty;

                    var cmd = sshClient.CreateCommand("arch;");
                    var result = cmd.BeginExecute();
                    using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
                    {
                        while (!result.IsCompleted || !reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line != null)
                            {
                                if (line.Contains("x86_64"))
                                {
                                    agentPath = x64Agent;

                                }
                                else
                                {
                                    agentPath = x86Agent;
                                }
                            }
                        }
                    }
                    cmd.EndExecute(result);

                    using (var sftpClient = new SftpClient(connectionInfo))
                    {
                        sftpClient.Connect();

                        using (var fileStream = new FileStream(agentPath, FileMode.Open))
                        {
                            Console.WriteLine("Uploading {0} ({1:N0} bytes)", agentPath, fileStream.Length);
                            sftpClient.BufferSize = 4 * 1024; // bypass Payload error large files
                                                              //To save time I am temporarly commenting this! The file should be already there.
                                                              //sftpClient.UploadFile(fileStream, Path.GetFileName(agentPath));
                        }
                    }

                    cmd = sshClient.CreateCommand("chmod +x ./" + Path.GetFileName(agentPath));
                    result = cmd.BeginExecute();
                    using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
                    {
                        while (!result.IsCompleted || !reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line != null)
                            {
                                //Log result
                            }
                        }
                    }
                    cmd.EndExecute(result);
                }


                using (var client = new SshClient(connectionInfo))
                {
                    client.Connect();

                    IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                    termkvp.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);
                    ShellStream shellStream = client.CreateShellStream("xterm", 80, 24, 800, 600, 1024, termkvp);

                    //var cmd = client.CreateCommand("pwd; pwd;");
                    shellStream.WriteLine("sudo pwd;");
                    string rep = shellStream.Expect(new Regex(@"([$#>:])")); //expect password or user prompt

                    server.Log += rep;

                    //check to send password
                    if (rep.Contains(":"))
                    {
                        //send password
                        shellStream.WriteLine(server.Password);
                        rep = shellStream.Expect(new Regex(@"[$#>]")); //expect user or root prompt
                        server.Log += rep;
                    }

                    client.Disconnect();

                    //var result = cmd.BeginExecute();

                    //using (var reader =
                    //           new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
                    //{
                    //    while (!result.IsCompleted || !reader.EndOfStream)
                    //    {
                    //        string line = reader.ReadLine();
                    //        if (line != null)
                    //        {
                    //            server.Log += (line + Environment.NewLine);
                    //        }
                    //    }
                    //}

                    //cmd.EndExecute(result);
                }
            }
            //Check whether server is x64 or x86
            //Install prerequisites using local yum repo for rpm or apt for debian (not important now as most servers would already have those files)
            //SFTP the appropriate file on the server
            //Run the installer using required parameters
        }

        private bool CheckPrereqsRPM(SshClient sshClient)
        {
            throw new NotImplementedException();
        }

        private bool CheckPrereqsDebian(SshClient sshClient)
        {
            throw new NotImplementedException();
        }

        private bool CheckGlibcRPM(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("rpm -qa | grep glibc");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("glibc-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckOpensslRPM(SshClient sshClient)
        {
            var cmd = sshClient.CreateCommand("rpm -qa | grep openssl");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("openssl-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckCurlRPM(SshClient sshClient)
        {
            var cmd = sshClient.CreateCommand("rpm -qa | grep curl");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("curl-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckPythonLibsRPM(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("rpm -qa | grep python-libs");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("python-libs-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckPAMRPM(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("rpm -qa | grep pam");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("pam-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckGlibcDebian(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("dpkg -l | grep glibc");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("glibc-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckOpensslDebian(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("dpkg -l | grep openssl");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("openssl-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckCurlDebian(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("dpkg -l | grep curl");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("curl-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckPythonLibsDebian(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("dpkg -l | grep python-libs");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("python-libs-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private bool CheckPAMDebian(SshClient sshClient)
        {

            var cmd = sshClient.CreateCommand("dpkg -l | grep pam");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null && line.Contains("pam-"))
                    {
                        cmd.EndExecute(result);
                        return true;
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return false;
        }

        private void SetProxy()
        {
            throw new NotImplementedException();
        }

        private void SwitchWorkspace()
        {
            throw new NotImplementedException();
        }

        private void SwitchWorkspaceAndSetProxy()
        {
            throw new NotImplementedException();
        }

        private void InstallAgentAndOnboard()
        {
            throw new NotImplementedException();
        }

        private void InstallAgentAndOnboardAndSetProxy()
        {
            throw new NotImplementedException();
        }

        private void InstallPrereqs()
        {
            throw new NotImplementedException();
        }

        private void TransferAgents()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    #region Classes & Enums

    public class ServerListItem
    {
        public ServerListItemStatus Status { get; set; }
        public string ServerName { get; set; }
        public string IPAddress { get; set; }
        public short Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Log { get; set; }
        public string Architecture { get; set; }
    }

    public enum ServerListItemStatus
    {
        NotStarted,
        InProgress,
        PartiallyCompleted,
        Completed,
        Failed
    }

    public enum ServerDistro
    {
        RPM,
        Debian,
        Unknown
    }

    #endregion
}
