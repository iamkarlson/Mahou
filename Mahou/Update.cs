using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace Mahou
{
    public partial class Update : Form
    {
        static string[] UpdInfo;
        static bool onceshow = false;
        public Update()
        {
            InitializeComponent();
        }
        private void btDMahou_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(new System.Uri(UpdInfo[3]),
                        // Gets filename from url
                    Regex.Match(UpdInfo[3], @"[^\\\/]+$").Groups[0].Value);
                }
            }
            catch { }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100 && !onceshow)
            {
                MessageBox.Show("Mahou " + UpdInfo[2] +
                                " downloaded to running Mahou directory, please update manually.","Update Info",
                                  MessageBoxButtons.OK,MessageBoxIcon.Information);
                pbStatus.Value = 0;
                onceshow = false;
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
                    string url = "https://github.com/BladeMight/Mahou/releases/latest";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.ServicePoint.SetTcpKeepAlive(true, 5000, 1000);
                    var response = (HttpWebResponse)await Task.Factory
                        .FromAsync<WebResponse>(request.BeginGetResponse,
                        request.EndGetResponse,
                        null);
                    Console.WriteLine(response.StatusCode);
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
                        Info.Add(Regex.Replace(Description,"\n","\r\n")); // Regex needed to properly display new lines.
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
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 5000;
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
            }
            else
            {
                if (flVersion("v" + Application.ProductVersion.ToString()) <
                    flVersion(UpdInfo[2]))
                {
                    btnCheck.Text = "I think you need to update...";
                    tmr.Start();
                    SetUInfo();
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
            return float.Parse(ver[1] + "." + ver.Substring(3).Replace(".", ""), CultureInfo.InvariantCulture);
        }
    }
}
