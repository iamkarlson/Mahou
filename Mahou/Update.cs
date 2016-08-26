using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
namespace Mahou
{
    public partial class Update : Form
    {
        static string[] UpdInfo;
        static bool updating, was, isold = true;
        static Timer tmr = new Timer();
        static Timer old = new Timer();
        static Timer animate = new Timer();
        static int progress = 0, _progress = 0;
        public Update()
        {
            animate.Interval = 2500;
            tmr.Interval = 3000;
            old.Interval = 7500;
            old.Tick += (_, __) => //Toggles every 7.5 sec
                {
                    Console.WriteLine(isold);
                    if (isold)
                        isold = false;
                    else
                        isold = true;
                };
            InitializeComponent();
        }
        private void btDMahou_Click(object sender, EventArgs e)
        {
            if (!updating)
            {
                updating = true;
                //Downloads latest Mahou
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    // Gets filename from url
                    var BDMText = btDMahou.Text;
                    var fn = Regex.Match(UpdInfo[3], @"[^\\\/]+$").Groups[0].Value;
                    wc.DownloadFileAsync(new System.Uri(UpdInfo[3]), fn);
                    lbDownloading.Text = "Downloading " + fn;
                    animate.Tick += (_, __) =>
                        {
                            lbDownloading.Text += ".";
                        };
                    animate.Start();
                    btDMahou.Visible = false;
                    lbDownloading.Visible = true;
                    tmr.Tick += (_, __) =>
                    {
                        // Checks if progress changed?
                        if (progress == _progress)
                        {
                            old.Stop();
                            isold = true;
                            btDMahou.Visible = true;
                            lbDownloading.Visible = false;
                            animate.Stop();
                            pbStatus.Value = progress = _progress = 0;
                            wc.CancelAsync();
                            updating = false;
                            btDMahou.Text = "Timed out...";
                            tmr.Tick += (o, oo) =>
                            {
                                btDMahou.Text = BDMText;
                                tmr.Stop();
                            };
                            tmr.Interval = 3000;
                            tmr.Start();
                        }
                        else
                        {
                            tmr.Stop();
                        }
                    };
                    old.Start();
                    tmr.Interval = 15000;
                    tmr.Start();
                }
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (isold)
                _progress = e.ProgressPercentage;
            pbStatus.Value = progress = e.ProgressPercentage;
            //Below in "if" is AUTO-UPDATE feature ;)
            if (e.ProgressPercentage == 100 && !was)
            {
                int MahouPID = Process.GetCurrentProcess().Id;
                //Downloaded archive 
                var arch = Regex.Match(UpdInfo[3], @"[^\\\/]+$").Groups[0].Value;
                MahouForm.icon.Hide();
                //Batch script to run powershell script
                const string batPSStart =
@"@ECHO OFF
SET ThisScriptsDirectory=%~dp0
SET PowerShellScriptPath=%ThisScriptsDirectory%Update.ps1
PowerShell.exe -NoProfile -ExecutionPolicy Bypass -WindowStyle Hidden -Command ""& '%PowerShellScriptPath%'""
DEL PSStart.cmd";
                //Save Batch script
                File.WriteAllText("PSStart.cmd", batPSStart);
                //Powershell script to shutdown running Mahou,
                //delete old,
                //unzip downloaded one, and start it.
                var psMahouUpdate =
@"TASKKILL /PID " + MahouPID + @" /F
DEL "".\Mahou.exe""
Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::ExtractToDirectory(""" + arch + @""", '.\')
start Mahou.exe _!_updated_!_
DEL " + arch + @"
DEL "".\Update.ps1""";
                //Save PS script
                File.WriteAllText("Update.ps1", psMahouUpdate);
                ProcessStartInfo PSStart = new ProcessStartInfo();
                PSStart.FileName = "PSStart.cmd";
                //Make PSStart hidden
                PSStart.WindowStyle = ProcessWindowStyle.Hidden;
                //Start updating(unzipping)
                Process.Start(PSStart);
                was = true;
            }
        }

        async private void btnCheck_Click(object sender, EventArgs e)
        {
            btnCheck.Text = "Checking...";
            await Task.Run(async () =>
            {
                List<string> Info = new List<string>(); // Update info
                try
                {
                    // Latest Mahou release url
                    const string url = "https://github.com/BladeMight/Mahou/releases/latest";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.ServicePoint.SetTcpKeepAlive(true, 5000, 1000);
                    var response = (HttpWebResponse)await Task.Factory
                        .FromAsync<WebResponse>(request.BeginGetResponse,
                        request.EndGetResponse,
                        null);
                    //Console.WriteLine(response.StatusCode)
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var data = new StreamReader(response.GetResponseStream(), true).ReadToEnd();
                        response.Close();
                        // Below are REGEX HTML PARSES!!
                        // I'm not kidding...
                        // They really works :)
                        var Title = Regex.Match(data,
                            "<h1 class=\"release-title\">\n.*<a href=\".*\">(.*)</a>").Groups[1].Value;
                        var Description = Regex.Replace(Regex.Match(data,
                            "<div class=\"markdown-body\">\n        (.*)\n      </div>",
                            RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value, "<[^>]*>", "");
                        var Version = Regex.Match(data, "<span class=\"css-truncate-target\">(.*)</span>").Groups[1].Value;
                        var Link = "https://github.com" + Regex.Match(data,
                            "<ul class=\"release-downloads\">\n.*<li>\n.+href=\"(/.*\\.\\w{3})").Groups[1].Value;
                        Info.Add(Title);
                        Info.Add(Regex.Replace(Description, "\n", "\r\n")); // Regex needed to properly display new lines.
                        Info.Add(Version);
                        Info.Add(Link);
                    }
                    else
                    {
                        response.Close();
                    }
                }
                catch (WebException)
                {
                    Info = new List<string> { "Error", "Failed to get Update info, can't connent to 'github.com', check your internet connection", "Error" };
                }
                UpdInfo = Info.ToArray();
            });
            tmr.Tick += (_, __) =>
            {
                btnCheck.Text = "Check for Updates";
                tmr.Stop();
            };
            if (UpdInfo[2] == "Error")
            {
                btnCheck.Text = "Error occured during check...";
                tmr.Start();
                SetUInfo();
                tmr.Tick += (_, __) =>
                {
                    gpRTitle.Text = "Release Title";
                    lbRDesc.Text = "Release Description";
                    lbVer.Text = "Release Version:";
                    tmr.Stop();
                };
                tmr.Start();
            }
            else
            {
                if (flVersion("v" + Application.ProductVersion) <
                    flVersion(UpdInfo[2]))
                {
                    btnCheck.Text = "I think you need to update...";
                    tmr.Start();
                    SetUInfo();
                    btDMahou.Text = "Update Mahou to " + UpdInfo[2];
                    btDMahou.Enabled = true;
                    pbStatus.Enabled = true;
                }
                else
                {
                    btnCheck.Text = "You have latest version.";
                    tmr.Start();
                    SetUInfo();
                }
            }
        }
        void SetUInfo()
        {
            gpRTitle.Text = UpdInfo[0];
            lbRDesc.Text = UpdInfo[1];
            lbVer.Text = UpdInfo[2];
        }
        public float flVersion(string ver) // converts "Mahou version type" to float
        {
            var justdigs = Regex.Replace(ver, "\\D", "");
            return float.Parse(justdigs[0] + "." + justdigs.Substring(1), CultureInfo.InvariantCulture);
        }
    }
}
