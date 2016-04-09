using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using IWshRuntimeLibrary;

namespace Mahou
{
    public partial class MahouForm : Form
    {
        HotkeyHandler Mainhk, ExitHk;
        TrayIcon icon;
        List<string> lcnmid = new List<string>();
        public MahouForm()
        {
            InitializeComponent();
            foreach (Locales.Locale lc in MMain.locales)
            {
                lcnmid.Add(lc.Lang + "(" + lc.uId + ")");
            }
            icon = new TrayIcon(MMain.MySetts.IconVisibility);
            icon.Exit += exitToolStripMenuItem_Click;
            icon.ShowHide += showHideToolStripMenuItem_Click;
            Mainhk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.Insert, this);
            Mainhk.Register();
            ExitHk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.F12, this);
            ExitHk.Register();
        }
        #region Form/Controls Events
        private void MahouForm_Load(object sender, EventArgs e)
        {
            RefreshLocales();
            cbLangOne.SelectedIndex = lcnmid.IndexOf(MMain.MySetts.locale1Lang + "(" + MMain.MySetts.locale1uId + ")");
            cbLangTwo.SelectedIndex = lcnmid.IndexOf(MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")");
            Debug.WriteLine(lcnmid.IndexOf(MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")") + MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")");
            TrayIconCheckBox.Checked = MMain.MySetts.IconVisibility;
            cbAutorun.Checked =
            System.IO.File.Exists(System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Startup),
            "Mahou.lnk")) ? true : false;
            cbSpaceBreak.Checked = MMain.MySetts.SpaceBreak;
        }
        
        private void MahouForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
        private void MahouForm_VisibleChanged(object sender, EventArgs e)
        {
            RefreshLocales();
        }
        private void MahouForm_Activated(object sender, EventArgs e)
        {
            RefreshLocales();
        }
        private void cbAutorun_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutorun.Checked)
            {
                CreateShortcut();
            }
            else
            {
                DeleteShortcut();
            }
        }
        private void cbLangOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Locales.Locale lc in MMain.locales)
            {
                if (cbLangOne.Text == lc.Lang + "(" + lc.uId + ")")
                {
                    MMain.MySetts.locale1Lang = lc.Lang;
                    MMain.MySetts.locale1uId = lc.uId;
                }
            }
        }
        private void cbLangTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Locales.Locale lc in MMain.locales)
            {
                if (cbLangTwo.Text == lc.Lang + "(" + lc.uId + ")")
                {
                    MMain.MySetts.locale2Lang = lc.Lang;
                    MMain.MySetts.locale2uId = lc.uId;
                }
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            MMain.MySetts.Save();
            RefreshIconVisibility();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            MMain.MySetts.Save();
            RefreshIconVisibility();
            this.Visible = false;
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Pause to Convert last selection.\nPress Scroll while selected text is focused to convert it.\nPress Ctrl+Alt+Shift+Insert to show Mahou main window.\nPress Ctrl+Alt+Shift+F12 to shutdown Mahou.\n\n*Note that if you typing in not of selected in settings layouts(locales/languages), pressing \"Pause\" will switch typed text to Language 1.", "****Attention****", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mhouIcon_DoubleClick(object sender, EventArgs e)
        {
            Mahou.MMain.mahou.Show();
        }

        private void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleHotkey();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void TrayIconCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MMain.MySetts.IconVisibility = TrayIconCheckBox.Checked;
        }

        private void GitHubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/BladeMight/Mahou");
        }

        private void cbSpaceBreak_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSpaceBreak.Checked)
            {
                MMain.MySetts.SpaceBreak = true;
            }
            else
            {
                MMain.MySetts.SpaceBreak = false;
            }
        }
        #endregion
        #region Functions & WndProc
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Modifiers.WM_HOTKEY_MSG_ID)
            {
                if ((Keys)(((int)m.LParam >> 16) & 0xFFFF) == Keys.Insert && ((int)m.LParam & 0xFFFF) == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                {
                    HandleHotkey();
                }
                if ((Keys)(((int)m.LParam >> 16) & 0xFFFF) == Keys.F12 && ((int)m.LParam & 0xFFFF) == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                {
                    ExitProgram();
                }
            }
            if (m.Msg == Mahou.MMain.ao)
            {
                HandleHotkey();
            }
            base.WndProc(ref m);
        }

        public void HandleHotkey()
        {
            if (this.Visible != false)
            {
                this.Visible = false;
            }
            else
            {
                this.TopMost = true;
                this.Visible = true;
                System.Threading.Thread.Sleep(5);
                this.TopMost = false;
            }
            Refresh();
        }

        public void ExitProgram()
        {
            MMain.MySetts.Save();
            icon.Hide();
            Refresh();
            Application.Exit();
        }

        private void RefreshLocales()
        {
            MMain.locales = Locales.AllList();
            cbLangOne.Items.Clear();
            cbLangTwo.Items.Clear();
            Locales.IfLessThan2();
            foreach (Locales.Locale lc in MMain.locales)
            {
                cbLangOne.Items.Add(lc.Lang + "(" + lc.uId + ")");
                cbLangTwo.Items.Add(lc.Lang + "(" + lc.uId + ")");
            }
        }
        private void RefreshIconVisibility()
        {
            if (MMain.MySetts.IconVisibility)
            {
                icon.Show();
                Refresh();
            }
            else
            {
                icon.Hide();
                Refresh();
            }
        }
        public static void CreateShortcut()
        {
            var currentPath = Assembly.GetExecutingAssembly().Location;
            var shortcutLocation = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                "Mahou.lnk");
            var description = "Mahou - Magick layout switcher";
            if (System.IO.File.Exists(shortcutLocation))
            {
                return;
            }
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = description;
            shortcut.TargetPath = currentPath;
            shortcut.Save();
        }
        public static void DeleteShortcut()
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Startup),
            "Mahou.lnk")))
            {
                System.IO.File.Delete(System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                    "Mahou.lnk"));
            }
        }
        #endregion

    }
}
