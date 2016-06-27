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
        public static HotkeyHandler Mainhk, ExitHk, HKConvertLast, HKConvertSelection; // Hotkeys
        static bool HKCLReg = false, HKCSReg = false; // These to prevent re-registering of same HotKey
        bool shift = false, alt = false, ctrl = false;
        static string tempCLMods, tempCSMods; // Temporary modifiers
        static int tempCLKey, tempCSKey; // Temporary keys
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
            CheckModifiers(MMain.MySetts.HKCLMods);
            HKConvertLast = new HotkeyHandler((alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000), (Keys)MMain.MySetts.HKCLKey, this);
            HKConvertLast.Register();
            HKCLReg = true;
            CheckModifiers(MMain.MySetts.HKCSMods);
            HKConvertSelection = new HotkeyHandler((alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000), (Keys)MMain.MySetts.HKCSKey, this);
            HKConvertSelection.Register();
            HKCSReg = true;
        }
        public void CheckModifiers(string inpt)
        {
            if (String.IsNullOrEmpty(inpt)) { inpt = "None"; } // inpt can be empty because of replaces so we switch it to "None" to avoid null reference exception.
            shift = inpt.Contains("Shift") ? true : false;
            alt = inpt.Contains("Alt") ? true : false;
            ctrl = inpt.Contains("Control") ? true : false;
            System.Threading.Thread.Sleep(5);
        }
        public static string Remake(Keys k) //Make readable some special keys
        {
            if (k >= Keys.D0 && k <= Keys.D9)
            {
                return k.ToString().Replace("D", "");
            }
            if (k == Keys.ShiftKey ||
                k == Keys.Menu ||
                k == Keys.ControlKey ||
                k == Keys.LWin ||
                k == Keys.RWin)
            {
                return "";
            }
            if (k == Keys.Scroll)
            {
                return k.ToString().Replace("Cancel", "Scroll");
            }
            if (k == Keys.Cancel)
            {
                return k.ToString().Replace("Cancel", "Pause");
            }
            return k.ToString();
        }
        public static string OemReadable(string inpt)//Make readable Oem Keys
        {
            return inpt.Replace("Oemtilde", "`")
                  .Replace("OemMinus", "-")
                  .Replace("Oemplus", "+")
                  .Replace("OemBackslash", "\\")
                  .Replace("Oem5", "\\")
                  .Replace("OemOpenBrackets", "{")
                  .Replace("OemCloseBrackets", "}")
                  .Replace("Oem6", "}")
                  .Replace("OemSemicolon", ";")
                  .Replace("Oem1", ";")
                  .Replace("OemQuotes", "\"")
                  .Replace("Oem7", "\"")
                  .Replace("OemPeriod", ".")
                  .Replace("Oemcomma", ",")
                  .Replace("OemQuestion", "/");
        }
        #region Form/Controls Events
        private void MahouForm_Load(object sender, EventArgs e)
        {
            RefreshLocales();
            tbCLHK.Text = OemReadable((MMain.MySetts.HKCLMods.Replace(",", " +") + " + " + (Keys)MMain.MySetts.HKCLKey).Replace("None + ", ""));
            tbCSHK.Text = OemReadable((MMain.MySetts.HKCSMods.Replace(",", " +") + " + " + (Keys)MMain.MySetts.HKCSKey).Replace("None + ", ""));
            cbLangOne.SelectedIndex = lcnmid.IndexOf(MMain.MySetts.locale1Lang + "(" + MMain.MySetts.locale1uId + ")");
            cbLangTwo.SelectedIndex = lcnmid.IndexOf(MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")");
            Debug.WriteLine(lcnmid.IndexOf(MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")") + MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")");
            TrayIconCheckBox.Checked = MMain.MySetts.IconVisibility;
            cbCapsLayoutSwitch.Checked = MMain.MySetts.SwitchLayoutByCaps;
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
                ToggleVisibility();
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
        private void tbCLHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Last action
        {
            tbCLHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + Remake(e.KeyCode)).Replace("None + ", ""));
            tempCLMods = e.Modifiers.ToString().Replace(",", " +").Replace("None", "");
            tempCLKey = (int)e.KeyCode;
            if (tempCLMods == "")
            {
                tempCLMods = "None";
            }
            Debug.WriteLine("{" + e.Modifiers + "}+(" + e.KeyCode + ")");
            Debug.WriteLine("+{" + tempCLMods + "}+(" + tempCLKey + ")+"); 
        }
        private void tbCSHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Selection action
        {
            tbCSHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + Remake(e.KeyCode)).Replace("None + ", ""));
            tempCSMods = e.Modifiers.ToString().Replace(",", " +").Replace("None", "");
            tempCSKey = (int)e.KeyCode;
            if (tempCSMods == "")
            {
                tempCSMods = "None";
            }
            Debug.WriteLine("{" + e.Modifiers + "}+(" + e.KeyCode + ")");
            Debug.WriteLine("-{" + tempCSMods + "}+(" + tempCSKey + ")-"); 
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
        private void cbCapsLayoutSwitch_CheckedChanged(object sender, EventArgs e)
        {
            MMain.MySetts.SwitchLayoutByCaps = cbCapsLayoutSwitch.Checked;
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
            Apply();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Apply();
            ToggleVisibility();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Pause(by Default) to Convert last selection.\nPress Scroll(by Default) while selected text is focused to convert it.\nPress Ctrl+Alt+Shift+Insert to show Mahou main window.\nPress Ctrl+Alt+Shift+F12 to shutdown Mahou.\n\n*Note that if you typing in not of selected in settings layouts(locales/languages), pressing \"Pause\" will switch typed text to Language 1.\n\n**If you have problems with symbols conversion(selection) try switching languages (1=>2 & 2=>1)\nRegards.", "****Attention****", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mhouIcon_DoubleClick(object sender, EventArgs e)
        {
            Mahou.MMain.mahou.Show();
        }

        private void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
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
                CheckModifiers(MMain.MySetts.HKCLMods);
                if ((Keys)(((int)m.LParam >> 16) & 0xFFFF) == (Keys)MMain.MySetts.HKCLKey && ((int)m.LParam & 0xFFFF) == (alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000))
                {
                    Debug.WriteLine("Hotkey CL Pressed");
                    //These three below are needed to release all modifiers, so even if you will still hold any of it
                    //it will skip them and do as it must.
                    KeyHook.keybd_event((int)Keys.Menu, (byte)KeyHook.MapVirtualKey((int)Keys.Menu, 0), 2, 0); // Alt Up
                    KeyHook.keybd_event((int)Keys.ShiftKey, (byte)KeyHook.MapVirtualKey((int)Keys.ShiftKey, 0), 2, 0); // Shift Up
                    KeyHook.keybd_event((int)Keys.ControlKey, (byte)KeyHook.MapVirtualKey((int)Keys.ControlKey, 0), 2, 0); // Control Up
                    //String below prevents queue converting
                    HKConvertLast.Unregister(); //Stops hotkey ability
                    KeyHook.ConvertLast();
                }
                CheckModifiers(MMain.MySetts.HKCSMods);
                if ((Keys)(((int)m.LParam >> 16) & 0xFFFF) == (Keys)MMain.MySetts.HKCSKey && ((int)m.LParam & 0xFFFF) == (alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000))
                {
                    Debug.WriteLine("Hotkey CS Pressed");
                    //same as above comment
                    KeyHook.keybd_event((int)Keys.Menu, (byte)KeyHook.MapVirtualKey((int)Keys.Menu, 0), 2, 0); // Alt Up
                    KeyHook.keybd_event((int)Keys.ShiftKey, (byte)KeyHook.MapVirtualKey((int)Keys.ShiftKey, 0), 2, 0); // Shift Up
                    KeyHook.keybd_event((int)Keys.ControlKey, (byte)KeyHook.MapVirtualKey((int)Keys.ControlKey, 0), 2, 0); // Control Up
                    //Prevents queue converting
                    HKConvertSelection.Unregister(); //Stops hotkey ability
                    KeyHook.ConvertSelection();
                }
                if ((Keys)(((int)m.LParam >> 16) & 0xFFFF) == Keys.Insert && ((int)m.LParam & 0xFFFF) == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                {
                    ToggleVisibility();
                }
                if ((Keys)(((int)m.LParam >> 16) & 0xFFFF) == Keys.F12 && ((int)m.LParam & 0xFFFF) == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                {
                    ExitProgram();
                }
            }
            if (m.Msg == Mahou.MMain.ao)
            {
                ToggleVisibility();
            }
            base.WndProc(ref m);
        }
        private void Apply()
        {
            if (!string.IsNullOrEmpty(tempCLMods))
            {
                MMain.MySetts.HKCLMods = tempCLMods;
            }
            if (tempCLKey != 0)
            {
                MMain.MySetts.HKCLKey = tempCLKey;
            }
            if (!string.IsNullOrEmpty(tempCSMods))
            {
                MMain.MySetts.HKCSMods = tempCSMods;
            }
            if (tempCSKey != 0)
            {
                MMain.MySetts.HKCSKey = tempCSKey;
            }
            MMain.MySetts.Save();
            if (HKCLReg)
            {
                HKConvertLast.Unregister();
            }
            CheckModifiers(MMain.MySetts.HKCLMods);
            HKConvertLast = new HotkeyHandler((alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000), (Keys)MMain.MySetts.HKCLKey, this);
            HKConvertLast.Register();
            HKCLReg = true;
            if (HKCSReg)
            {
                HKConvertSelection.Unregister();
            }
            CheckModifiers(MMain.MySetts.HKCSMods);
            HKConvertLast = new HotkeyHandler((alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000), (Keys)MMain.MySetts.HKCSKey, this);
            HKConvertLast.Register();
            HKCLReg = true;
            RefreshIconVisibility();
        }
        public void ToggleVisibility()
        {
            if (this.Visible != false)
            {
                MMain.StartHook();
                this.Visible = false;
            }
            else
            {
                MMain.StopHook();
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
        #region TOOLTIPS!!!
        private void cbCapsLayoutSwitch_MouseHover(object sender, EventArgs e)
        {
            HelpTT.Show("Pressing CapsLock will toggle between selected, layouts in settings", cbCapsLayoutSwitch);
        }
        private void cbSpaceBreak_MouseHover(object sender, EventArgs e)
        {
            HelpTT.Show("Pressing Space will clear last word, that is converted by Pause, otherwise it will convert ally typed text.", cbSpaceBreak);
        }
        #endregion
    }
}
