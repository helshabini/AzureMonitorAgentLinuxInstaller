using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections;
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
        private string x64Agent = string.Empty;
        private string x86Agent = string.Empty;
        private const string rpmPackages = @"packages\rpm";
        private const string debianPackages = @"packages\debian";

        public AzureMonitorAgentLinuxInstaller()
        {
            InitializeComponent();
            InitializeServersList();
        }

        private void InitializeServersList()
        {
            serverlistDataSource = new BindingSource
            {
                new ServerListItem
                {
                    Status = ServerListItemStatus.NotStarted,
                    ServerName = "oraclelinuxvm",
                    IPAddress = "172.16.100.2",
                    Port = 22,
                    Username = "vmadmin",
                    Password = "vmP@ssw0rd123",
                    Log = string.Empty
                }
            };

            gvServersList.AutoGenerateColumns = false;
            gvServersList.AutoSize = true;
            gvServersList.DataSource = serverlistDataSource;

            // Initialize and add a Progress column
            DataGridViewColumn column = new DataGridViewImageColumn
            {
                DataPropertyName = "Progess",
                Name = "Progress"
            };
            gvServersList.Columns.Add(column);

            // Initialize and add a Server Name column.
            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServerName",
                Name = "Server Name"
            };
            gvServersList.Columns.Add(column);

            // Initialize and add a IP Address column.
            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IPAddress",
                Name = "IP Address"
            };
            gvServersList.Columns.Add(column);

            // Initialize and add a Port column.
            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Port",
                Name = "Port"
            };
            gvServersList.Columns.Add(column);

            // Initialize and add a Username column.
            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                Name = "Username"
            };
            gvServersList.Columns.Add(column);

            // Initialize and add a Password column.
            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Password",
                Name = "Password"
            };
            gvServersList.Columns.Add(column);

            // Initialize and add a Password column.
            column = new DataGridViewLinkColumn
            {
                Name = "Log"
            };
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

                        if (asset.name.ToString().Contains("x64"))
                        {
                            x64Agent = asset.name.ToString();
                        }

                        if (asset.name.ToString().Contains("x86"))
                        {
                            x86Agent = asset.name.ToString();
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

            //Loop on the servers in our list
            foreach (var item in serverlistDataSource)
            {
                ServerListItem server = item as ServerListItem;
                //In the future we should also support cert login
                var connectionInfo = new ConnectionInfo(server.IPAddress, server.Port, server.Username, new PasswordAuthenticationMethod(server.Username, server.Password));
                connectionInfo.Encoding = Encoding.GetEncoding("ISO-8859-1");
                using (var sshClient = new SshClient(connectionInfo))
                {
                    sshClient.Connect();

                    server.Distro = CheckDistro(sshClient);
                    server.Architecture = CheckArchitecture(sshClient);

                    if (chkInstallPrerequisites.Checked)
                    {
                        if (!CheckPrereqs(sshClient, ref server))
                        {
                            //Log: Error, prereqs not ready
                            continue;
                        }
                        TransferPrereqs(sshClient, server.Distro);
                        InstallPrereqs(sshClient, server.Distro);
                    }

                    if (rdAutoDownload.Checked)
                        TransferAgents(sshClient, server.Architecture);
                    if (chkInstallAgent.Checked && chkJoinLogAnalytics.Checked && chkUseProxy.Checked)
                        InstallAgentAndOnboardAndSetProxy();
                    else if (chkInstallAgent.Checked && chkJoinLogAnalytics.Checked)
                        InstallAgentAndOnboard(sshClient, ref server);
                    else if (!chkInstallAgent.Checked && chkJoinLogAnalytics.Checked && chkUseProxy.Checked)
                        SwitchWorkspaceAndSetProxy();
                    else if (!chkInstallAgent.Checked && chkJoinLogAnalytics.Checked && !chkUseProxy.Checked)
                        SwitchWorkspace();
                    else if (!chkInstallAgent.Checked && !chkJoinLogAnalytics.Checked && chkUseProxy.Checked)
                        SetProxy();

                    sshClient.Disconnect();
                }
            }
        }

        private bool CheckPrereqs(SshClient sshClient, ref ServerListItem server)
        {
            server.Prereqs = new List<Package>();

            Package glibc = QueryPackage(sshClient, server.Distro, "glibc");
            if (glibc != null)
            {
                server.Prereqs.Add(glibc);
            }
            else
            {
                return false;
            }

            Package openssl = QueryPackage(sshClient, server.Distro, "openssl");
            if (openssl != null)
            {
                server.Prereqs.Add(openssl);
            }
            else
            {
                return false;
            }

            Package curl = QueryPackage(sshClient, server.Distro, "curl");
            if (curl != null)
            {
                server.Prereqs.Add(curl);
            }
            else
            {
                return false;
            }

            Package pythonlibs = QueryPackage(sshClient, server.Distro, "python-libs");
            if (pythonlibs != null)
            {
                server.Prereqs.Add(pythonlibs);
            }
            else
            {
                return false;
            }

            Package pam = QueryPackage(sshClient, server.Distro, "pam");
            if (pam != null)
            {
                server.Prereqs.Add(pam);
            }
            else
            {
                return false;
            }

            //Log: All good...
            return true;
        }

        private Package QueryPackage(SshClient sshClient, ServerDistro distro, string package)
        {
            string query = string.Empty;
            switch (distro)
            {
                case ServerDistro.RPM:
                    query = "rpm -qa  --queryformat '%{NAME};%{VERSION};%{RELEASE};%{ARCH}\n' | grep '^" + package + ";'";
                    break;
                case ServerDistro.Debian:
                    query = "dpkg-query -W -f='${binary:Package};${Version};${Version};${Architecture}\n' " + package;
                    break;
                default:
                    //Log: Might wanna try both
                    return null;
            }
            
            var cmd = sshClient.CreateCommand(query);
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        cmd.EndExecute(result);
                        return Package.Parse(line);
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Package unavailable
            return null;
        }

        public static List<string> ListPrereqsFiles(string sDir)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        files.Add(f);
                    }
                    ListPrereqsFiles(d);
                }
            }
            catch (Exception ex)
            {
                //Log: Problem with recursion?!
                return null;
            }

            return files;
        }

        private void TransferPrereqs(SshClient sshClient, ServerDistro distro)
        {
            string prereqsPath = (distro == ServerDistro.RPM) ? rpmPackages : debianPackages;

            using (var sftpClient = new SftpClient(sshClient.ConnectionInfo))
            {
                sftpClient.Connect();

                List<string> files = ListPrereqsFiles(prereqsPath);
                foreach (string file in files)
                {
                    using (var fileStream = new FileStream(file, FileMode.Open))
                    {
                        Console.WriteLine("Uploading {0} ({1:N0} bytes)", file, fileStream.Length);
                        sftpClient.BufferSize = 4 * 1024; // bypass Payload error large files
                        string path = file.Remove(file.IndexOf(Path.GetFileName(file))).Replace(@"\", "/");
                        string filename = Path.GetFileName(file);
                        string[] dirs = path.TrimEnd('/').Split('/');
                        foreach (var dir in dirs)
                        {
                            try
                            {
                                if (!sftpClient.Exists(dir))
                                {
                                    sftpClient.CreateDirectory(dir);
                                }
                                sftpClient.ChangeDirectory(dir);
                            }
                            catch
                            {
                                //Log: Directory already exists
                            }
                        }
                        if (sftpClient.Exists(filename))
                        {
                            sftpClient.UploadFile(fileStream, filename);
                        }
                        sftpClient.ChangePermissions(filename, 755);
                        sftpClient.ChangeDirectory("/home/"+sshClient.ConnectionInfo.Username);
                    }
                }
            }

            ////chmod +x for the file to be executable
            //var cmd = sshClient.CreateCommand("chmod +x ./" + Path.GetFileName(agentPath));
            //var result = cmd.BeginExecute();
            //using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            //{
            //    while (!result.IsCompleted || !reader.EndOfStream)
            //    {
            //        string line = reader.ReadLine();
            //        if (line != null)
            //        {
            //            //Log: result
            //        }
            //    }
            //}
            //cmd.EndExecute(result);
        }

        private void InstallPrereqs(SshClient sshClient, ServerDistro distro)
        {
            string prereqsPath = (distro == ServerDistro.RPM) ? rpmPackages : debianPackages;

            List<string> files = ListPrereqsFiles(prereqsPath);
            foreach (string file in files)
            {
                string path = "/home/" + 
                    sshClient.ConnectionInfo.Username + 
                    "/" + 
                    file.Replace(@"\", "/");

                string cmdText = distro == ServerDistro.RPM ?
                    "yum install -y " + path:
                    "apt install -y " + path;

                var cmd = sshClient.CreateCommand(cmdText);
                var result = cmd.BeginExecute();
                using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
                {
                    while (!result.IsCompleted || !reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            //Log: result
                        }
                    }
                }
                cmd.EndExecute(result);
            }
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

        private void InstallAgentAndOnboard(SshClient sshClient, ref ServerListItem server)
        {
            string installcmd = server.Architecture == ServerArchitecture.x64 ?
                "sh /home/" + server.Username + "/" + x64Agent + " --install -w " + txtWorkspaceId.Text + " -s " + txtSharedKey.Text + "\n":
                "sh /home/" + server.Username + "/" + x86Agent + " --install -w " + txtWorkspaceId.Text + " -s " + txtSharedKey.Text + "\n";

            IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>
            {
                { Renci.SshNet.Common.TerminalModes.ECHO, 53 }
            };
            ShellStream shell = sshClient.CreateShellStream("xterm", 800, 240, 1024, 720, 1024, termkvp);

            //ShellStream shell = sshClient.CreateShellStream("Tail", 0, 0, 0, 0, 1024);

            StreamWriter wr = new StreamWriter(shell);
            StreamReader rd = new StreamReader(shell);
            wr.AutoFlush = true;
            
            wr.WriteLine("su");

            string rep = shell.Expect(new Regex(@"([$#>:])"), new TimeSpan(0, 0, 3)); //expect password or user prompt
            //check to send password
            if (rep.EndsWith("Password: "))
            {
                //send password
                wr.WriteLine(server.Password);
                rep = shell.Expect(new Regex(@"[$#>]"), new TimeSpan(0, 0, 3));
                server.Log += rep;
                var cmd = sshClient.CreateCommand(installcmd);
                var result = cmd.BeginExecute();
                using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
                {
                    while (!result.IsCompleted || !reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            server.Log += line;
                        }
                    }
                }
            }
        }

        private void InstallAgentAndOnboardAndSetProxy()
        {
            throw new NotImplementedException();
        }

        private ServerArchitecture CheckArchitecture(SshClient sshClient)
        {
            var cmd = sshClient.CreateCommand("arch;");
            var result = cmd.BeginExecute();
            using (var reader = new StreamReader(cmd.OutputStream, Encoding.UTF8, true, 1024, true))
            {
                while (!result.IsCompleted || !reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        if (line.Equals("x86_64"))
                        {
                            cmd.EndExecute(result);
                            return ServerArchitecture.x64;

                        }
                        else if (line.Equals("i686"))
                        {
                            cmd.EndExecute(result);
                            return ServerArchitecture.x86;
                        }
                    }
                }
            }

            cmd.EndExecute(result);
            //Log: Unknown arch
            return ServerArchitecture.Unknown;
        }

        private void TransferAgents(SshClient sshClient, ServerArchitecture arch)
        {
            string agentPath = (arch == ServerArchitecture.x64) ? x64Agent : x86Agent;

            using (var sftpClient = new SftpClient(sshClient.ConnectionInfo))
            {
                sftpClient.Connect();
                if (sftpClient.Exists(Path.GetFileName(agentPath)))
                {
                    //Log: Agent already exists on destination
                    return;
                }
                using (var fileStream = new FileStream(agentPath, FileMode.Open))
                {
                    string filename = Path.GetFileName(agentPath);
                    Console.WriteLine("Uploading {0} ({1:N0} bytes)", agentPath, fileStream.Length);
                    sftpClient.BufferSize = 4 * 1024; // bypass Payload error large files
                    sftpClient.UploadFile(fileStream, filename);
                    sftpClient.ChangePermissions(filename, 755);
                }
            }
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
        public ServerDistro Distro { get; set; }
        public ServerArchitecture Architecture { get; set; }
        public List<Package> Prereqs { get; set; }
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

    public enum ServerArchitecture
    {
        x64,
        x86,
        Unknown
    }

    public class ServerPrereqs
    {
        public string Glibc { get; set; }
        public string Curl { get; set; }
        public string Openssl { get; set; }
        public string PythonLibs { get; set; }
        public string PAM { get; set; }
    }

    public class Package
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Architecture { get; set; }

        public static Package Parse (string packageString)
        {
            string[] parts = packageString.Split(';');
            return new Package()
            {
                Name = parts[0],
                Version = (parts[1].Split('-'))[0],
                Release = parts[2].Split('-').Count() > 1 ? (parts[2].Split('-'))[1] : parts[2],
                Architecture = parts[3]
            };
        }
    }

    #endregion
}
