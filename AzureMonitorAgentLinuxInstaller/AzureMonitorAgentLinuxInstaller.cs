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
        private BindingSource serverlistDataSource;

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
                        webClient.DownloadFile(asset.browser_download_url.ToString(), asset.name.ToString());
                    }
                }
            }
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            //DownloadAgents();
            
            //Loop on servers in the grid view
            foreach (var item in serverlistDataSource)
            {
                ServerListItem server = item as ServerListItem;
                var connectionInfo = new ConnectionInfo(server.IPAddress,
                                        server.Username,
                                        new PasswordAuthenticationMethod(server.Username, server.Password));
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
            //SCP the appropriate file on the server
            //Run the installer using required parameters
        }
    }

    public class ServerListItem
    {
        public ServerListItemStatus Status { get; set; }
        public string ServerName { get; set; }
        public string IPAddress { get; set; }
        public short Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Log { get; set; }
    }

    public enum ServerListItemStatus
    {
        NotStarted,
        InProgress,
        PartiallyCompleted,
        Completed,
        Failed
    }
}
