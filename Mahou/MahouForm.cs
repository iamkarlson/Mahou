using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using IWshRuntimeLibrary;
using System.Threading.Tasks;
namespace Mahou
{
    public partial class MahouForm : Form
    {
        #region Variables
        public static HotkeyHandler Mainhk, ExitHk, HKCLast, HKCSelection, HKCLine; // Hotkeys, HKC => HotKey Convert
        static bool HKCLReg = false, HKCSReg = false, HKCLineReg = false; // These to prevent re-registering of same HotKey
        bool shift = false, alt = false, ctrl = false, messagebox = false;
        static string tempCLMods = "None", tempCSMods = "None", tempCLineMods = "None", // Temporary modifiers
            tempcbOnlyKey = "None";
        static int tempCLKey = 0, tempCSKey = 0, tempCLineKey = 0, tempELST = 0; // Temporary keys 
        static bool tempTrayI, tempCycleM, tempAutoR, tempBlockCTRL,
            tempCLEnabled, tempCSEnabled, tempCLineEnabled, tempSLinCS,
            tempUseEmulate, tempRePress, tempEOSpace, tempResel;//Temporary checkboxes values
        public static bool hotkeywithmodsfired = false;
        static Locales.Locale tempLoc1 = new Locales.Locale { Lang = "dummy", uId = 0 },
                              tempLoc2 = new Locales.Locale { Lang = "dummy", uId = 0 }; // Temporary locales
        public static TrayIcon icon;
        public Update update = new Update();
        ExtCtrls ExtendedCTRLs = new ExtCtrls();
        #endregion
        public MahouForm()
        {
            InitializeComponent();
            icon = new TrayIcon(MMain.MyConfs.ReadBool("Functions", "IconVisibility"));
            icon.Exit += exitToolStripMenuItem_Click;
            icon.ShowHide += showHideToolStripMenuItem_Click;
            Mainhk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.Insert, this);
            Mainhk.Register();
            ExitHk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.F12, this);
            ExitHk.Register();
            HKCLast = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLMods")),
                (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey"), this);
            if (MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLEnabled"))
            {
                HKCLast.Register();
                HKCLReg = true;
            }
            HKCSelection = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCSMods")),
                (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey"), this);
            if (MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCSEnabled"))
            {
            HKCSelection.Register();
            HKCSReg = true;
            }
            HKCLine = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")),
                (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey"), this);
            if (MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLineEnabled"))
            {
            HKCLine.Register();
            HKCLineReg = true;
            }
            System.Threading.Thread uche = new System.Threading.Thread(() => update.StartupCheck());
            uche.Start();
        }
        #region Form Events
        private void MahouForm_Load(object sender, EventArgs e)
        {
            this.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tempRestore();
            RefreshControlsData();
            if (!cbCycleMode.Checked)
            {
                cbUseEmulate.Enabled = false;
                cbELSType.Enabled = false;
            }
            else
            {
                cbUseEmulate.Enabled = true;
                cbELSType.Enabled = true;
            }
            RemoveAddCtrls();
            RefreshLanguage();
        }
        private void MahouForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ToggleVisibility();
            }
            tempRestore();
        }
        private void MahouForm_VisibleChanged(object sender, EventArgs e)
        {
            RefreshControlsData();
        }
        private void MahouForm_Activated(object sender, EventArgs e)
        {
            if (HKCSReg)
            {
                HKCSelection.Unregister();
            }
            if (HKCLReg)
            {
                HKCLast.Unregister();
            }
            if (HKCLineReg)
            {
                HKCLine.Unregister();
            }
            MMain.StopHook();
            LocalesRefresh();
        }
        private void MahouForm_Deactivate(object sender, EventArgs e)
        {
            MMain.StartHook();
            LocalesRefresh();
            RegisterEnabled();
        }
        #endregion
        #region Textboxes
        private void tbCLHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Last action
        {
            tbCLHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + 
                            Remake(e.KeyCode)).Replace("None + ", ""));
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
            tbCLineHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + 
                               Remake(e.KeyCode)).Replace("None + ", ""));
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
            tbCSHK.Text = OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " + 
                            Remake(e.KeyCode)).Replace("None + ", ""));
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

        private void cbSwitchLayoutKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempcbOnlyKey = cbSwitchLayoutKeys.Text;
        }
        private void cbELSType_SelectedIndexChanged(object sender, EventArgs e)
        {
                tempELST = cbELSType.SelectedIndex;
        }
        #endregion
        #region Checkboxes
        private void cbAutorun_CheckedChanged(object sender, EventArgs e)
        {
            tempAutoR = cbAutorun.Checked;
        }
        private void cbCycleMode_CheckedChanged(object sender, EventArgs e)
        {
            tempCycleM = cbCycleMode.Checked;
            if (!cbCycleMode.Checked)
            { 
                gbSBL.Enabled = true;
                cbUseEmulate.Enabled = false;
                cbELSType.Enabled = false;
            }
            else
            {
                gbSBL.Enabled = false;
                cbUseEmulate.Enabled = true;
                cbELSType.Enabled = true;
            }
        }
        private void TrayIconCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tempTrayI = TrayIconCheckBox.Checked;
        }
        private void cbCLActive_CheckedChanged(object sender, EventArgs e)
        {
            tempCLEnabled = cbCLActive.Checked;
            if (!cbCLActive.Checked)
            { tbCLHK.Enabled = false; }
            else { tbCLHK.Enabled = true; }
        }
        private void cbCSActive_CheckedChanged(object sender, EventArgs e)
        {
            tempCSEnabled = cbCSActive.Checked;
            if (!cbCSActive.Checked)
            { tbCSHK.Enabled = false; }
            else { tbCSHK.Enabled = true; }
        }
        private void cbCLineActive_CheckedChanged(object sender, EventArgs e)
        {
            tempCLineEnabled = cbCLineActive.Checked;
            if (!cbCLineActive.Checked)
            { tbCLineHK.Enabled = false; }
            else { tbCLineHK.Enabled = true; }
        }
        private void cbBlockC_CheckedChanged(object sender, EventArgs e)
        {
            tempBlockCTRL = cbBlockC.Checked;
        }
        private void cbUseEmulate_CheckedChanged(object sender, EventArgs e)
        {
            tempUseEmulate = cbUseEmulate.Checked;
        }
        private void cbCSSwitch_CheckedChanged(object sender, EventArgs e)
        {
            tempSLinCS = cbCSSwitch.Checked;
        }
        private void cbRePress_CheckedChanged(object sender, EventArgs e)
        {
            tempRePress = cbRePress.Checked;
        }
        private void cbEatOneSpace_CheckedChanged(object sender, EventArgs e)
        {
            tempEOSpace = cbEatOneSpace.Checked;
        }
        private void cbResel_CheckedChanged(object sender, EventArgs e)
        {
            tempResel = cbResel.Checked;
        }
        #endregion
        #region Buttons & link
        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            messagebox = false;
            RefreshControlsData();
            tempRestore();
            ToggleVisibility();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Apply();
            ToggleVisibility();
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            messagebox = true;
            MessageBox.Show(MMain.Msgs[2], MMain.Msgs[3], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void GitHubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BladeMight/Mahou");
        }
        private void btnUpd_Click(object sender, EventArgs e)
        {
            update.ShowDialog();
        }
        private void btnDDD_Click(object sender, EventArgs e)
        {
            ExtendedCTRLs.ShowDialog();
        }
        private void btnLangChange_Click(object sender, EventArgs e)
        {
            if (MMain.MyConfs.Read("Locales", "LANGUAGE") == "RU")
            {
                MMain.MyConfs.Write("Locales", "LANGUAGE", "EN");
                btnLangChange.Text = "EN";
            }
            else if (MMain.MyConfs.Read("Locales", "LANGUAGE") == "EN")
            {
                MMain.MyConfs.Write("Locales", "LANGUAGE", "RU");
                btnLangChange.Text = "RU";
            }
            MMain.InitLanguage();
            RefreshLanguage();
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
                Keys Key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int Modifs = (int)m.LParam & 0xFFFF;
                //This stops hotkeys when main window is visible
                if (!this.Focused)
                {
                        if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCSMods")))
                        {
                            if (MMain.MyConfs.ReadBool("Functions", "BlockCTRL") && MMain.MyConfs.Read("Hotkeys", "HKCSMods").Contains("Control")) { }
                            else
                            {
                                if (HKHaveModifiers(MMain.MyConfs.Read("Hotkeys", "HKCSMods")) && MMain.MyConfs.ReadBool("Functions", "RePress"))
                                {
                                    hotkeywithmodsfired = true;
                                    RePressAfter(MMain.MyConfs.Read("Hotkeys", "HKCSMods"));
                                }
                                SendModsUp(GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCSMods")));
                                //Prevents queue converting
                                HKCSelection.Unregister(); //Stops hotkey ability
                                Task t = new Task(KMHook.ConvertSelection);
                                t.RunSynchronously();
                            }
                        }
                        if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLMods")))
                        {
                            if (MMain.MyConfs.ReadBool("Functions", "BlockCTRL") && MMain.MyConfs.Read("Hotkeys", "HKCLMods").Contains("Control")) { }
                            else
                            {
                                if (HKHaveModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLMods")) && MMain.MyConfs.ReadBool("Functions", "RePress"))
                                {
                                    hotkeywithmodsfired = true;
                                    RePressAfter(MMain.MyConfs.Read("Hotkeys", "HKCLMods"));
                                }
                                SendModsUp(GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLMods")));
                                //String below prevents queue converting
                                HKCLast.Unregister(); //Stops hotkey ability
                                Task t = new Task(new Action(() => KMHook.ConvertLast(MMain.c_word, true)));
                                t.RunSynchronously();
                            }
                        }
                    if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")))
                    {
                        if (MMain.MyConfs.ReadBool("Functions", "BlockCTRL") && MMain.MyConfs.Read("Hotkeys", "HKCLineMods").Contains("Control")){}
                        else
                        {
                            if (HKHaveModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")) && MMain.MyConfs.ReadBool("Functions", "RePress"))
                            {
                                hotkeywithmodsfired = true;
                                RePressAfter(MMain.MyConfs.Read("Hotkeys", "HKCLineMods"));
                            }
                            SendModsUp(GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")));
                            //String below prevents queue converting
                            HKCLine.Unregister(); //Stops hotkey ability
                            Task t = new Task(new Action(() => KMHook.ConvertLast(MMain.c_line, false)));
                            t.RunSynchronously();
                        }
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
        public static bool HKHaveModifiers(string mods)
        {
            if (mods.Contains("Alt") ||
                mods.Contains("Shift") ||
                mods.Contains("Control"))
                return true;
            else
                return false;
        }
        private void RePressAfter(string where)
        {
            // Sets Press Again variables for modifiers
            if (where.Contains("Shift"))
                KMHook.shiftRP = true;
            else
                KMHook.shiftRP = false;
            if (where.Contains("Alt"))
                KMHook.altRP = true;
            else
                KMHook.altRP = false;
            if (where.Contains("Control"))
                KMHook.ctrlRP = true;
            else
                KMHook.ctrlRP = false;
        }
        public static bool[] GetHKMods(string hkmods)
        {
            bool alt=false, shift=false, ctrl=false;
            if (hkmods.Contains("Control"))
                ctrl = true;
            if (hkmods.Contains("Alt"))
                alt = true;
            if (hkmods.Contains("Shift"))
                shift = true;
            return new bool[] { alt, shift, ctrl };
        }
        public static void SendModsUp(bool[] modstoup)
        {
            //These three below are needed to release all modifiers, so even if you will still hold any of it
            //it will skip them and do as it must.
            KMHook.self = true;
            if (modstoup[0])
            {
                KMHook.KeybdEvent(Keys.RMenu, 2); // Right Alt Up
                KMHook.KeybdEvent(Keys.LMenu, 2); // Left Alt Up
            }
            if (modstoup[1])
            {
                KMHook.KeybdEvent(Keys.RShiftKey, 2); // Right Shift Up
                KMHook.KeybdEvent(Keys.LShiftKey, 2); // Left Shift Up
            }
            if (modstoup[2])
            {
                KMHook.KeybdEvent(Keys.RControlKey, 2); // Right Control Up
                KMHook.KeybdEvent(Keys.LControlKey, 2); // Left Control Up
            } 
            KMHook.self = false;
        }
        public int CheckNGetModifiers(string inpt)
        {
            if (String.IsNullOrEmpty(inpt)) { inpt = "None"; } // inpt can be empty because of replaces so we switch it to "None" to avoid null reference exception.
            shift = inpt.Contains("Shift") ? true : false;
            alt = inpt.Contains("Alt") ? true : false;
            ctrl = inpt.Contains("Control") ? true : false;
            System.Threading.Thread.Sleep(1);
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
        public static string OemReadable(string inpt)//Make readable Oem Keys
        {
            return inpt
                  .Replace("Oemtilde", "`")
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
        public static void tempRestore()
        {
            try
            {
                // Restores temps
                tempCLKey = MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey");
                tempCSKey = MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey");
                tempCLineKey = MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey");
                tempCLMods = MMain.MyConfs.Read("Hotkeys", "HKCLMods");
                tempCSMods = MMain.MyConfs.Read("Hotkeys", "HKCSMods");
                tempCLineMods = MMain.MyConfs.Read("Hotkeys", "HKCLineMods");
                tempAutoR = System.IO.File.Exists(System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                    "Mahou.lnk")) ? true : false;
                tempcbOnlyKey = MMain.MyConfs.Read("Hotkeys", "OnlyKeyLayoutSwicth");
                tempCycleM = MMain.MyConfs.ReadBool("Functions", "CycleMode");
                tempTrayI = MMain.MyConfs.ReadBool("Functions", "IconVisibility");
                tempSLinCS = MMain.MyConfs.ReadBool("Functions", "CSSwitch");
                tempEOSpace = MMain.MyConfs.ReadBool("Functions", "EatOneSpace");
                tempRePress = MMain.MyConfs.ReadBool("Functions", "RePress");
                tempResel = MMain.MyConfs.ReadBool("Functions", "ReSelect");
                tempELST = MMain.MyConfs.ReadInt("Functions", "ELSType");
                tempUseEmulate = MMain.MyConfs.ReadBool("Functions", "EmulateLayoutSwitch");
                tempCLEnabled = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLEnabled");
                tempCSEnabled = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCSEnabled");
                tempCLineEnabled = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLineEnabled");
                tempLoc1 = tempLoc2 = new Locales.Locale { Lang = "dummy", uId = 0 };
            }
            //This creates(silently) new config file if existed one disappeared o_O
            catch { MMain.MyConfs = new Configs(); tempRestore(); }
        }
        public static void RegisterEnabled()
        {
            if (!MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLEnabled"))
                HKCLast.Unregister();
            else
                HKCLast.Register();
            if (!MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCSEnabled"))
                HKCSelection.Unregister();
            else
                HKCSelection.Register();
            if (!MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLineEnabled"))
                HKCLine.Unregister();
            else
                HKCLine.Register();
        }
        private void Apply()
        {
            bool hkcsnotready = false, hkclnotready = false, hkclinenotready = false;
            if (tempCLKey != 0 && tempCLineKey != 0)
            {
                if (tempCLineKey == tempCLKey && tempCLMods == tempCLineMods)
                {
                    messagebox = true;
                    MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    hkclinenotready = true;
                    hkclnotready = true;
                }
                else
                {
                    messagebox = false;
                }
            }
            if (!string.IsNullOrEmpty(tempCLMods) && tempCLKey != 0)
            {
                MMain.MyConfs.Write("Hotkeys", "HKCLMods", tempCLMods);
            }
            if (tempCLKey != 0)
            {
                MMain.MyConfs.Write("Hotkeys", "HKCLKey", tempCLKey.ToString());
                messagebox = false;
            }
            else
            {
                hkclnotready = true;
                messagebox = true;
                MessageBox.Show(MMain.Msgs[6], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (!string.IsNullOrEmpty(tempCSMods) && tempCSKey != 0)
            {
                MMain.MyConfs.Write("Hotkeys", "HKCSMods", tempCSMods);
            }
            if (tempCSKey != 0)
            {
                MMain.MyConfs.Write("Hotkeys", "HKCSKey", tempCSKey.ToString());
                messagebox = false;
            }
            else
            {
                hkcsnotready = true;
                messagebox = true;
                MessageBox.Show(MMain.Msgs[7], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (!string.IsNullOrEmpty(tempCLineMods) && tempCLineKey != 0)
            {
                MMain.MyConfs.Write("Hotkeys", "HKCLineMods", tempCLineMods);
            }
            if (tempCLineKey != 0)
            {
                MMain.MyConfs.Write("Hotkeys", "HKCLineKey", tempCLineKey.ToString());
                messagebox = false;
            }
            else
            {
                hkclinenotready = true;
                messagebox = true;
                MessageBox.Show(MMain.Msgs[8], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (tempAutoR)
            {
                CreateShortcut();
            }
            else
            {
                DeleteShortcut();
            }
            MMain.MyConfs.Write("Hotkeys", "OnlyKeyLayoutSwicth", tempcbOnlyKey);
            MMain.MyConfs.Write("Functions", "CycleMode", tempCycleM.ToString());
            MMain.MyConfs.Write("Functions", "IconVisibility", tempTrayI.ToString());
            MMain.MyConfs.Write("Functions", "BlockCTRL", tempBlockCTRL.ToString());
            MMain.MyConfs.Write("Functions", "CSSwitch", tempSLinCS.ToString());
            MMain.MyConfs.Write("Functions", "EmulateLayoutSwitch", tempUseEmulate.ToString());
            MMain.MyConfs.Write("Functions", "ELSType", tempELST.ToString());
            MMain.MyConfs.Write("Functions", "RePress", tempRePress.ToString());
            MMain.MyConfs.Write("Functions", "EatOneSpace", tempEOSpace.ToString());
            MMain.MyConfs.Write("Functions", "ReSelect", tempResel.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKCLEnabled", tempCLEnabled.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKCSEnabled", tempCSEnabled.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKCLineEnabled", tempCLineEnabled.ToString());
            if (tempLoc1.Lang != "dummy" || tempLoc1.uId != 0)
            {
                MMain.MyConfs.Write("Locales", "locale1Lang",tempLoc1.Lang);
                MMain.MyConfs.Write("Locales", "locale1uId", tempLoc1.uId.ToString());
            }
            if (tempLoc2.Lang != "dummy" || tempLoc2.uId != 0)
            {
                MMain.MyConfs.Write("Locales", "locale2Lang", tempLoc2.Lang);
                MMain.MyConfs.Write("Locales", "locale2uId", tempLoc2.uId.ToString());
            }
            if (!hkclnotready)
            {
                if (HKCLReg)
                {
                    HKCLast.Unregister();
                    HKCLReg = false;
                }
                HKCLast = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLMods")),
                    (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey"), this);
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
                
                HKCSelection = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCSMods")),
                    (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey"), this);
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
                
                HKCLine = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")),
                    (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey"), this);
                HKCLine.Register();
                HKCLineReg = true;
            }
            RefreshControlsData();
        }
        public void ToggleVisibility()
        {
            if (this.Visible != false)
            {
                this.Visible = ExtendedCTRLs.Visible = update.Visible = false;
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
        public void RemoveAddCtrls()
        {
            if (MMain.MyConfs.ReadBool("ExtCtrls", "UseExtCtrls"))
            {
                cbSwitchLayoutKeys.SelectedIndex = 0;
                cbSwitchLayoutKeys.Items.Remove("Left Control");
                cbSwitchLayoutKeys.Items.Remove("Right Control");
            }
            else
            {
                cbSwitchLayoutKeys.Items.Remove("None");
                if (!cbSwitchLayoutKeys.Items.Contains("Left Control"))
                    cbSwitchLayoutKeys.Items.Add("Left Control");
                if (!cbSwitchLayoutKeys.Items.Contains("Right Control"))
                    cbSwitchLayoutKeys.Items.Add("Right Control");
                cbSwitchLayoutKeys.Items.Add("None");
            }
        }
        public void ExitProgram()
        {
            icon.Hide();
            Refresh();
            Application.Exit();
        }
        private void RefreshControlsData()
        {
            LocalesRefresh();
            RefreshIconVisibility();
            cbSwitchLayoutKeys.Text = MMain.MyConfs.Read("Hotkeys", "OnlyKeyLayoutSwicth");
            TrayIconCheckBox.Checked = MMain.MyConfs.ReadBool("Functions", "IconVisibility");
            cbCycleMode.Checked = MMain.MyConfs.ReadBool("Functions", "CycleMode");
            cbBlockC.Checked = MMain.MyConfs.ReadBool("Functions", "BlockCTRL");
            cbCSSwitch.Checked = MMain.MyConfs.ReadBool("Functions", "CSSwitch");
            cbUseEmulate.Checked = MMain.MyConfs.ReadBool("Functions", "EmulateLayoutSwitch");
            cbRePress.Checked = MMain.MyConfs.ReadBool("Functions", "RePress");
            cbEatOneSpace.Checked = MMain.MyConfs.ReadBool("Functions", "EatOneSpace");
            cbResel.Checked = MMain.MyConfs.ReadBool("Functions", "ReSelect");
            cbCLActive.Checked = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLEnabled");
            cbCLineActive.Checked = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCLineEnabled");
            cbCSActive.Checked = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKCSEnabled");
            cbELSType.SelectedIndex = MMain.MyConfs.ReadInt("Functions", "ELSType");
            btnLangChange.Text = MMain.MyConfs.Read("Locales", "LANGUAGE");
            if (!messagebox)
            {
                tbCLHK.Text = OemReadable((MMain.MyConfs.Read("Hotkeys", "HKCLMods").Replace(",", " +") +
                    " + " + Remake((Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey"))).Replace("None + ", ""));
                tbCSHK.Text = OemReadable((MMain.MyConfs.Read("Hotkeys", "HKCSMods").Replace(",", " +") +
                    " + " + Remake((Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey"))).Replace("None + ", ""));
                tbCLineHK.Text = OemReadable((MMain.MyConfs.Read("Hotkeys", "HKCLineMods").Replace(",", " +") + 
                    " + " + Remake((Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey"))).Replace("None + ", ""));
            }
            cbAutorun.Checked = System.IO.File.Exists(System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                "Mahou.lnk")) ? true : false;
            LocalesRefresh();
        }
        private void LocalesRefresh()
        {
            Locales.IfLessThan2();
            MMain.locales = Locales.AllList();
            cbLangOne.Items.Clear();
            cbLangTwo.Items.Clear();
            MMain.lcnmid.Clear();
            foreach (Locales.Locale lc in MMain.locales)
            {
                cbLangOne.Items.Add(lc.Lang + "(" + lc.uId + ")");
                cbLangTwo.Items.Add(lc.Lang + "(" + lc.uId + ")");
                MMain.lcnmid.Add(lc.Lang + "(" + lc.uId + ")");
            } 
            try
            {
                cbLangOne.SelectedIndex = MMain.lcnmid.IndexOf(MMain.MyConfs.Read("Locales", "locale1Lang") + "(" + MMain.MyConfs.Read("Locales", "locale1uId") + ")");
                cbLangTwo.SelectedIndex = MMain.lcnmid.IndexOf(MMain.MyConfs.Read("Locales", "locale2Lang") + "(" + MMain.MyConfs.Read("Locales", "locale2uId") + ")");
            }
            catch
            {
                MessageBox.Show(MMain.Msgs[9], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LocalesRefresh();
                cbLangOne.SelectedIndex = 0;
                cbLangTwo.SelectedIndex = 1;
            }
        }
        private void RefreshLanguage()
        {
            GitHubLink.Text = MMain.UI[0];
            cbAutorun.Text = MMain.UI[1];
            btnUpd.Text = MMain.UI[2];
            gbHK.Text = MMain.UI[3];
            cbCLActive.Text = MMain.UI[4] + ":";
            cbCSActive.Text = MMain.UI[5] + ":";
            cbCLineActive.Text = MMain.UI[6] + ":";
            cbCSSwitch.Text = MMain.UI[7];
            cbRePress.Text = MMain.UI[8];
            cbResel.Text = MMain.UI[9];
            lbswithlayout.Text = MMain.UI[10];
            cbBlockC.Text = MMain.UI[11];
            TrayIconCheckBox.Text = MMain.UI[12];
            cbCycleMode.Text = MMain.UI[13];
            cbUseEmulate.Text = MMain.UI[14];
            gbSBL.Text = MMain.UI[15];
            lbl1lng.Text = MMain.UI[16] + " 1:";
            lbl2lng.Text = MMain.UI[16] + " 2:";
            btnApply.Text = MMain.UI[17];
            btnOK.Text = MMain.UI[18];
            btnCancel.Text = MMain.UI[19];
            btnHelp.Text = MMain.UI[20];
            icon.RefreshText(MMain.UI[42], MMain.UI[40], MMain.UI[41]);
        }
        private void RefreshIconVisibility()
        {
            if (MMain.MyConfs.ReadBool("Functions", "IconVisibility"))
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
        private void cbCycleMode_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbCycleMode.Text;
            HelpTT.Show(MMain.TTips[0], cbCycleMode);
        }
        private void tbCLHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCLHK.Text;
            HelpTT.Show(MMain.TTips[1], tbCLHK);
        }
        private void tbCSHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCSHK.Text;
            HelpTT.Show(MMain.TTips[2], tbCSHK);
        }
        private void tbCLineHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCLineHK.Text;
            HelpTT.Show(MMain.TTips[3], tbCLineHK);
        }
        private void GitHubLink_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = GitHubLink.Text;
            HelpTT.Show(MMain.TTips[4], GitHubLink);
        }
        private void TrayIconCheckBox_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = TrayIconCheckBox.Text;
            HelpTT.Show(MMain.TTips[5], TrayIconCheckBox);
        }
        private void btnUpd_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = btnUpd.Text;
            HelpTT.Show(MMain.TTips[6], btnUpd);
        }
        private void cbBlockAC_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbBlockC.Text;
            HelpTT.Show(MMain.TTips[7], cbBlockC);
        }
        private void cbSwitchLayoutKeys_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbSwitchLayoutKeys.Text;
            HelpTT.Show(MMain.TTips[8], cbSwitchLayoutKeys);
        }
        private void cbUseEmulate_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbUseEmulate.Text;
            HelpTT.Show(MMain.TTips[9], cbUseEmulate);
        }
        private void cbUseCycleForCS_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbCSSwitch.Text;
            HelpTT.Show(MMain.TTips[10], cbCSSwitch);
        }
        private void gbSBL_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = gbSBL.Text;
            HelpTT.Show(MMain.TTips[11], gbSBL);
        }
        private void cbRePress_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbRePress.Text;
            HelpTT.Show(MMain.TTips[12], cbRePress);
        }
        private void cbEatOneSpace_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbEatOneSpace.Text;
            HelpTT.Show(MMain.TTips[13], cbEatOneSpace);

        }
        private void cbResel_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbResel.Text;
            HelpTT.Show(MMain.TTips[14], cbResel);
        }
        private void cbELSType_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = MMain.TTips[19];
            HelpTT.Show(MMain.TTips[15], cbELSType);
        }
        #endregion
    }
}
