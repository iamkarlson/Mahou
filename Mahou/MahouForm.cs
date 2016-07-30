using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using IWshRuntimeLibrary;

namespace Mahou
{
    public partial class MahouForm : Form
    {
        #region Variables
        public static HotkeyHandler Mainhk, ExitHk, HKCLast, HKCSelection, HKCLine; // Hotkeys, HKC => HotKey Convert
        static bool HKCLReg = false, HKCSReg = false, HKCLineReg = false; // These to prevent re-registering of same HotKey
        bool shift = false, alt = false, ctrl = false;
        static string tempCLMods = "None", tempCSMods = "None", tempCLineMods = "None"; // Temporary modifiers
        static int tempCLKey = 0, tempCSKey = 0, tempCLineKey = 0; // Temporary keys 
        static bool tempcbCapsS, tempcbSpaceB, tempcbTrayI, tempCycleM, tempAutoR; //Temporary checkboxes value
        static Locales.Locale tempLoc1 = new Locales.Locale { Lang = "dummy", uId = 0 },
                              tempLoc2 = new Locales.Locale { Lang = "dummy", uId = 0 }; // Temporary locales
        TrayIcon icon;
        List<string> lcnmid = new List<string>();
        static Form update = new Update();
        #endregion 
        public MahouForm()
        {
            InitializeComponent();
            icon = new TrayIcon(MMain.MySetts.IconVisibility);
            icon.Exit += exitToolStripMenuItem_Click;
            icon.ShowHide += showHideToolStripMenuItem_Click;
            Mainhk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.Insert, this);
            Mainhk.Register();
            ExitHk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.F12, this);
            ExitHk.Register();
            HKCLast = new HotkeyHandler(CheckNGetModifiers(MMain.MySetts.HKCLMods), (Keys)MMain.MySetts.HKCLKey, this);
            HKCLast.Register();
            HKCLReg = true;
            HKCSelection = new HotkeyHandler(CheckNGetModifiers(MMain.MySetts.HKCSMods), (Keys)MMain.MySetts.HKCSKey, this);
            HKCSelection.Register();
            HKCSReg = true;
            HKCLine = new HotkeyHandler(CheckNGetModifiers(MMain.MySetts.HKCLineMods), (Keys)MMain.MySetts.HKCLineKey, this);
            HKCLine.Register();
            HKCLineReg = true;
        }
        #region Form Events
        private void MahouForm_Load(object sender, EventArgs e)
        {
            this.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tempRestore();
            RefreshControlsData();
        }
        private void MahouForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ToggleVisibility();
            }
            //restore temps
            tempAutoR = System.IO.File.Exists(System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                "Mahou.lnk")) ? true : false;
            tempcbCapsS = MMain.MySetts.SwitchLayoutByCaps;
            //tempcbSpaceB = MMain.MySetts.SpaceBreak;
            tempCycleM = MMain.MySetts.CycleMode;
            tempcbTrayI = MMain.MySetts.IconVisibility;
            tempRestore();
            tempLoc1 = tempLoc2 = new Locales.Locale { Lang = "dummy", uId = 0 };

        }
        private void MahouForm_VisibleChanged(object sender, EventArgs e)
        {
            RefreshControlsData();
        }
        private void MahouForm_Activated(object sender, EventArgs e)
        {
            HKCSelection.Unregister();
            HKCLast.Unregister();
            HKCLine.Unregister();
            MMain.StopHook();
            LocalesRefresh();
        }
        private void MahouForm_Deactivate(object sender, EventArgs e)
        {
            MMain.StartHook();
            LocalesRefresh();
            HKCLine.Register();
            HKCSelection.Register();
            HKCLast.Register();
        }
        #endregion
        #region Textboxes
        private void tbCLHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Last action
        {
            tbCLHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + Remake(e.KeyCode)).Replace("None + ", ""));
            tempCLMods = e.Modifiers.ToString().Replace(",", " +");
            switch ((int)e.KeyCode)
            {
                case 16:
                case 17:
                case 18:
                    tempCLKey = 0;
                    break;
                default:
                    tempCLKey = (int)e.KeyCode;
                    break;
            }
        }
        private void tbCLineHK_KeyDown(object sender, KeyEventArgs e) // Catch hotkey for Convert Line action
        {
            tbCLineHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + Remake(e.KeyCode)).Replace("None + ", ""));
            tempCLineMods = e.Modifiers.ToString().Replace(",", " +");
            switch ((int)e.KeyCode)
            {
                case 16:
                case 17:
                case 18:
                    tempCLineKey = 0;
                    break;
                default:
                    tempCLineKey = (int)e.KeyCode;
                    break;
            }
        }
        private void tbCSHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Selection action
        {
            tbCSHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + Remake(e.KeyCode)).Replace("None + ", ""));
            tempCSMods = e.Modifiers.ToString().Replace(",", " +");
            switch ((int)e.KeyCode)
            {
                case 16:
                case 17:
                case 18:
                    tempCSKey = 0;
                    break;
                default:
                    tempCSKey = (int)e.KeyCode;
                    break;
            }
        }
        #endregion
        #region Comboboxes
        private void cbLangOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Locales.Locale lc in MMain.locales)
            {
                if (cbLangOne.Text == lc.Lang + "(" + lc.uId + ")")
                {
                    tempLoc1 = new Locales.Locale { Lang = lc.Lang, uId = lc.uId };
                }
            }
        }
        private void cbLangTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Locales.Locale lc in MMain.locales)
            {
                if (cbLangTwo.Text == lc.Lang + "(" + lc.uId + ")")
                {
                    tempLoc2 = new Locales.Locale { Lang = lc.Lang, uId = lc.uId };
                }
            }
        }
        #endregion
        #region Checkboxes
        private void cbAutorun_CheckedChanged(object sender, EventArgs e)
        {
            tempAutoR = cbAutorun.Checked;
        }
        private void cbCapsLayoutSwitch_CheckedChanged(object sender, EventArgs e)
        {
            tempcbCapsS = cbCapsLayoutSwitch.Checked;
        }
        private void cbSpaceBreak_CheckedChanged(object sender, EventArgs e)
        {
            tempcbSpaceB = cbSpaceBreak.Checked;
        }
        private void cbCycleMode_CheckedChanged(object sender, EventArgs e)
        {
            tempCycleM = cbCycleMode.Checked;
        }
        private void TrayIconCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tempcbTrayI = TrayIconCheckBox.Checked;
        }
        #endregion
        #region Buttons & link
        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Apply();
            ToggleVisibility();
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Pause(by Default) to Convert last selection.\nPress Scroll(by Default) while selected text is focused to convert it.\nPress Ctrl+Alt+Shift+Insert to show Mahou main window.\nPress Ctrl+Alt+Shift+F12 to shutdown Mahou.\n\n*Note that if you typing in not of selected in settings layouts(locales/languages), pressing \"Pause\" will switch typed text to Language 1.\n\n**If you have problems with symbols conversion(selection) try switching languages (1=>2 & 2=>1).\n\nHover on any control of main window for more info about it.\n\nRegards.", "****Attention****", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void GitHubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BladeMight/Mahou");
        }
        #endregion
        #region Tray Events
        private void mhouIcon_DoubleClick(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        private void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }
        #endregion
        #region Functions & WndProc
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Modifiers.WM_HOTKEY_MSG_ID)
            {
                Console.WriteLine(CheckNGetModifiers(MMain.MySetts.HKCLMods) + "\n" + (Keys)MMain.MySetts.HKCLKey);
                Console.WriteLine(CheckNGetModifiers(MMain.MySetts.HKCSMods) + "\n" + (Keys)MMain.MySetts.HKCSKey);
                Console.WriteLine(CheckNGetModifiers(MMain.MySetts.HKCLineMods) + "\n" + (Keys)MMain.MySetts.HKCLineKey);
                Keys Key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int Modifs = (int)m.LParam & 0xFFFF;
                //This stops hotkeys when main window is visible
                if (!this.Focused)
                {
                    if (Key == (Keys)MMain.MySetts.HKCLKey && Modifs == CheckNGetModifiers(MMain.MySetts.HKCLMods))
                    {
                        SendModsUp();
                        //String below prevents queue converting
                        HKCLast.Unregister(); //Stops hotkey ability
                        KMHook.ActCall(() => KMHook.ConvertLast(MMain.c_word), true); //Asynchronously call Convert Last word,
                        //this will not work for Convert Selection, ThreadStateException will be catched. 
                    }
                    if (Key == (Keys)MMain.MySetts.HKCLineKey && Modifs == CheckNGetModifiers(MMain.MySetts.HKCLineMods))
                    {
                        SendModsUp();
                        //String below prevents queue converting
                        HKCLine.Unregister(); //Stops hotkey ability
                        KMHook.ActCall(() => KMHook.ConvertLast(MMain.c_line), false); //Asynchronously call Convert Line,
                        //this will not work for Convert Selection, ThreadStateException will be catched. 
                    }
                    if (Key == (Keys)MMain.MySetts.HKCSKey && Modifs == CheckNGetModifiers(MMain.MySetts.HKCSMods))
                    {
                        SendModsUp();
                        //Prevents queue converting
                        HKCSelection.Unregister(); //Stops hotkey ability
                        KMHook.ConvertSelection();
                    }
                }
                //these are global, so they don't need to be stoped when window is visible.
                if (Key == Keys.Insert && Modifs == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                {
                    ToggleVisibility();
                }
                if (Key == Keys.F12 && Modifs == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                {
                    ExitProgram();
                }
            }
            if (m.Msg == Mahou.MMain.ao) // ao = Already Opened
            {
                ToggleVisibility();
            }
            base.WndProc(ref m);
        }
        public void SendModsUp()
        {
            //These three below are needed to release all modifiers, so even if you will still hold any of it
            //it will skip them and do as it must.
            KMHook.keybd_event((int)Keys.Menu, (byte)KMHook.MapVirtualKey((int)Keys.Menu, 0), 2, 0); // Alt Up
            KMHook.keybd_event((int)Keys.ShiftKey, (byte)KMHook.MapVirtualKey((int)Keys.ShiftKey, 0), 2, 0); // Shift Up
            KMHook.keybd_event((int)Keys.ControlKey, (byte)KMHook.MapVirtualKey((int)Keys.ControlKey, 0), 2, 0); // Control Up
        }
        public int CheckNGetModifiers(string inpt)
        {
            if (String.IsNullOrEmpty(inpt)) { inpt = "None"; } // inpt can be empty because of replaces so we switch it to "None" to avoid null reference exception.
            shift = inpt.Contains("Shift") ? true : false;
            alt = inpt.Contains("Alt") ? true : false;
            ctrl = inpt.Contains("Control") ? true : false;
            System.Threading.Thread.Sleep(5);
            return (alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000);
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
        public static void tempRestore()
        {
            // Restores temp's for key's/modifier's
            tempCLKey = MMain.MySetts.HKCLKey;
            tempCSKey = MMain.MySetts.HKCSKey;
            tempCLineKey = MMain.MySetts.HKCLineKey;
            tempCLMods = MMain.MySetts.HKCLMods;
            tempCSMods = MMain.MySetts.HKCSMods;
            tempCLineMods = MMain.MySetts.HKCLineMods;
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
        private void Apply()
        {
            bool hkcsnotready = false, hkclnotready = false, hkclinenotready = false;
            if (tempCLKey != 0 && tempCLineKey != 0)
            {
                if (tempCLineKey == tempCLKey && tempCLMods == tempCLineMods)
                {
                    MessageBox.Show("You have assigned same hotkeys for Convert Last & Convert Line, that is impossible!!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    hkclinenotready = true;
                    hkclnotready = true;
                }
            }
            if (!string.IsNullOrEmpty(tempCLMods) && tempCLKey != 0)
            {
                MMain.MySetts.HKCLMods = tempCLMods;
            }
            if (tempCLKey != 0)
            {
                MMain.MySetts.HKCLKey = tempCLKey;
            }
            else
            {
                hkclnotready = true;
                MessageBox.Show("You have pressed just modifiers for Convert Last hotkey!!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (!string.IsNullOrEmpty(tempCSMods) && tempCSKey != 0)
            {
                MMain.MySetts.HKCSMods = tempCSMods;
            }
            if (tempCSKey != 0)
            {
                MMain.MySetts.HKCSKey = tempCSKey;
            }
            else
            {
                hkcsnotready = true;
                MessageBox.Show("You have pressed just modifiers for Convert Selection hotkey!!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (!string.IsNullOrEmpty(tempCLineMods) && tempCLineKey != 0)
            {
                MMain.MySetts.HKCLineMods = tempCLineMods;
            }
            if (tempCLineKey != 0)
            {
                MMain.MySetts.HKCLineKey = tempCLineKey;
            }
            else
            {
                hkclinenotready = true;
                MessageBox.Show("You have pressed just modifiers for Convert Line hotkey!!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (tempAutoR)
            {
                CreateShortcut();
            }
            else
            {
                DeleteShortcut();
            }
            MMain.MySetts.SwitchLayoutByCaps = tempcbCapsS;
            MMain.MySetts.CycleMode = tempCycleM;
            MMain.MySetts.IconVisibility = tempcbTrayI;
            //MMain.MySetts.SpaceBreak = tempcbSpaceB;
            if (tempLoc1.Lang != "dummy" || tempLoc1.uId != 0)
            {
                MMain.MySetts.locale1Lang = tempLoc1.Lang;
                MMain.MySetts.locale1uId = tempLoc1.uId;
            }
            if (tempLoc2.Lang != "dummy" || tempLoc2.uId != 0)
            {
                MMain.MySetts.locale2Lang = tempLoc2.Lang;
                MMain.MySetts.locale2uId = tempLoc2.uId;
            }
            if (!hkclnotready)
            {
                if (HKCLReg)
                {
                    HKCLast.Unregister();
                    HKCLReg = false;
                }
                HKCLast = new HotkeyHandler(CheckNGetModifiers(MMain.MySetts.HKCLMods), (Keys)MMain.MySetts.HKCLKey, this);
                HKCLast.Register();
                HKCLReg = true;
            }
            if (!hkcsnotready)
            {
                if (HKCSReg)
                {
                    HKCSelection.Unregister();
                    HKCSReg = false;
                }
                HKCSelection = new HotkeyHandler(CheckNGetModifiers(MMain.MySetts.HKCSMods), (Keys)MMain.MySetts.HKCSKey, this);
                HKCSelection.Register();
                HKCSReg = true;
            }
            if (!hkclinenotready)
            {
                if (HKCLineReg)
                {
                    HKCLine.Unregister();
                    HKCLineReg = false;
                }
                HKCLine = new HotkeyHandler(CheckNGetModifiers(MMain.MySetts.HKCLineMods), (Keys)MMain.MySetts.HKCLineKey, this);
                HKCLine.Register();
                HKCLineReg = true;
            }
            MMain.MySetts.Save();
            RefreshControlsData();
        }
        public void ToggleVisibility()
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
        private void RefreshControlsData()
        {
            LocalesRefresh();
            RefreshIconVisibility();
            cbCapsLayoutSwitch.Checked = MMain.MySetts.SwitchLayoutByCaps;
            //cbSpaceBreak.Checked = MMain.MySetts.SpaceBreak;
            TrayIconCheckBox.Checked = MMain.MySetts.IconVisibility;
            cbCycleMode.Checked = MMain.MySetts.CycleMode;
            tbCLHK.Text = OemReadable((MMain.MySetts.HKCLMods.Replace(",", " +") + " + " + (Keys)MMain.MySetts.HKCLKey).Replace("None + ", ""));
            tbCSHK.Text = OemReadable((MMain.MySetts.HKCSMods.Replace(",", " +") + " + " + (Keys)MMain.MySetts.HKCSKey).Replace("None + ", ""));
            tbCLineHK.Text = OemReadable((MMain.MySetts.HKCLineMods.Replace(",", " +") + " + " + (Keys)MMain.MySetts.HKCLineKey).Replace("None + ", ""));
            cbAutorun.Checked = System.IO.File.Exists(System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                "Mahou.lnk")) ? true : false;
            try
            {
                cbLangOne.SelectedIndex = lcnmid.IndexOf(MMain.MySetts.locale1Lang + "(" + MMain.MySetts.locale1uId + ")");
                cbLangTwo.SelectedIndex = lcnmid.IndexOf(MMain.MySetts.locale2Lang + "(" + MMain.MySetts.locale2uId + ")");
            }
            catch
            {
                MessageBox.Show("You have removed selected locales,reselect.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LocalesRefresh();
                cbLangOne.SelectedIndex = 0;
                cbLangTwo.SelectedIndex = 1;
            }
        }
        private void LocalesRefresh()
        {
            Locales.IfLessThan2();
            MMain.locales = Locales.AllList();
            cbLangOne.Items.Clear();
            cbLangTwo.Items.Clear();
            lcnmid.Clear();
            foreach (Locales.Locale lc in MMain.locales)
            {
                cbLangOne.Items.Add(lc.Lang + "(" + lc.uId + ")");
                cbLangTwo.Items.Add(lc.Lang + "(" + lc.uId + ")");
                lcnmid.Add(lc.Lang + "(" + lc.uId + ")");
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
            HelpTT.ToolTipTitle = cbCapsLayoutSwitch.Text;
            HelpTT.Show("Pressing CapsLock will toggle between selected, layouts in settings.\nIf Cycle Mode enabled it will just cycle between all installed layouts.", cbCapsLayoutSwitch);
        }
        private void cbSpaceBreak_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbSpaceBreak.Text;
            HelpTT.Show("Pressing Space will clear last word, that is converted by \"Convert Last\", otherwise it will convert all typed text.", cbSpaceBreak);
        }
        private void cbCycleMode_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbCycleMode.Text;
            HelpTT.Show("While this option enabled, \"Convert Last\" will just cycle between all locales\ninstead of cycling between selected in settings.\nThis mode works for almost all programs.\nIf there is program in which \"Convert Last\" not work, try with this option enabled.\nIf you have just 2 layouts(input languages) it is HIGHLY RECOMMENDED to turn it ON.", cbCycleMode);
        }
        private void tbCLHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCLHK.Text;
            HelpTT.Show("This is current hotkey for \"Convert Last\" action.\nPress any key to assign it, or key with modifiers(ALT,CTRL,SHIFT).", tbCLHK);
        }
        private void tbCSHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCSHK.Text;
            HelpTT.Show("This is current hotkey for Convert Selection action.\nPress any key to assign it, or key with modifiers(ALT,CTRL,SHIFT).", tbCSHK);
        }
        private void GitHubLink_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = GitHubLink.Text;
            HelpTT.Show("Go to GitHub repository to view source or report issue.", GitHubLink);
        }
        private void TrayIconCheckBox_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = TrayIconCheckBox.Text;
            HelpTT.Show("Toggles visibility of icon in a tray.\nIf it is hidden, to show configs window hit CTRL+ALT+SHIFT+INSERT or just run Mahou.exe again.", TrayIconCheckBox);
        }
        #endregion

        private void btnUpd_Click(object sender, EventArgs e)
        {
            update.ShowDialog();
        }
    }
}
