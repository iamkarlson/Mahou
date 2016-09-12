using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using IWshRuntimeLibrary;
using System.Threading.Tasks;
namespace Mahou
{
    public partial class MahouForm : Form
    {
        #region Variables
        public HotkeyHandler Mainhk, ExitHk, HKCLast, HKCSelection, HKCLine, HKSymIgn; // Hotkeys, HKC => HotKey Convert
        public bool HKCLReg, HKCSReg, HKCLineReg, HKSIReg; // These to prevent re-registering of same HotKey
        bool shift, alt, ctrl, messagebox;
        string tempCLMods = "None", tempCSMods = "None", tempCLineMods = "None"; // Temporary modifiers
        int tempCLKey, tempCSKey, tempCLineKey; // Temporary keys
        public bool hotkeywithmodsfired;
        Locales.Locale tempLoc1 = new Locales.Locale { Lang = "dummy", uId = 0 },
                               tempLoc2 = new Locales.Locale { Lang = "dummy", uId = 0 }; // Temporary locales
        public TrayIcon icon;
        public Update update = new Update();
        public Timer ICheck = new Timer();
        public LangDisplay langDisplay = new LangDisplay();
        MoreConfigs moreConfigs = new MoreConfigs();
        #endregion
        public MahouForm()
        {
            InitializeComponent();
            ICheck.Tick += (_,__) => 
            {
            	langDisplay.Visible = ICheckings.IsICursor();
				langDisplay.Location = new System.Drawing.Point(Cursor.Position.X + 8, Cursor.Position.Y - 8);
				switch (Locales.GetCurrentLocale()) {
					case 1041:
						langDisplay.ChangeLD("JP");
						break;
					case 1049:
						langDisplay.ChangeLD("RU");
						break;
					case 1033:
						langDisplay.ChangeLD("EN");
						break;
				}
            };
            langDisplay.ChangeColors(ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions","DLForeColor")),
                                     ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions","DLBackColor")));
            ICheck.Interval = MMain.MyConfs.ReadInt("Functions", "DLRefreshRate");
            if (MMain.MyConfs.ReadBool("Functions", "DisplayLang"))
            	ICheck.Start();
            icon = new TrayIcon(MMain.MyConfs.ReadBool("Functions", "IconVisibility"));
            icon.Exit += exitToolStripMenuItem_Click;
            icon.ShowHide += showHideToolStripMenuItem_Click;
            RefreshIconAll();
            InitializeHotkeys();
            //Background startup check for updates
            var uche = new System.Threading.Thread(update.StartupCheck);
            uche.Name = "Startup Check";
            uche.Start();
        }
        #region Form Events
        void MahouForm_Load(object sender, EventArgs e)
        {
            Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tempRestore();
            RefreshControlsData();
            EnableIF();
            RemoveAddCtrls();
            RefreshLanguage();
        }
        void MahouForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ToggleVisibility();
            }
            tempRestore();
        }
        void MahouForm_VisibleChanged(object sender, EventArgs e)
        {
        	IfNotExist();
        	RefreshControlsData();
        }
        void MahouForm_Activated(object sender, EventArgs e)
        {
            if (HKCSReg)
                HKCSelection.Unregister();
            if (HKCLReg)
                HKCLast.Unregister();
            if (HKCLineReg)
                HKCLine.Unregister();
            MMain.StopHook();
            RefreshLocales();
        }
        void MahouForm_Deactivate(object sender, EventArgs e)
        {
            MMain.StartHook();
            RefreshLocales();
            RegisterEnabled();
        }
        #endregion
        #region Textboxes(Hotkeyboxes)
        void tbCLHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Last action
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
        void tbCLineHK_KeyDown(object sender, KeyEventArgs e) // Catch hotkey for Convert Line action
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
        void tbCSHK_KeyDown(object sender, KeyEventArgs e)// Catch hotkey for Convert Selection action
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
        void cbLangOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Locales.Locale lc in MMain.locales)
            {
                if (cbLangOne.Text == lc.Lang + "(" + lc.uId + ")")
                {
                    tempLoc1 = new Locales.Locale { Lang = lc.Lang, uId = lc.uId };
                }
            }
        }
        void cbLangTwo_SelectedIndexChanged(object sender, EventArgs e)
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
        void cbCycleMode_CheckedChanged(object sender, EventArgs e)
        {
            EnableIF();
        }
        void cbCLActive_CheckedChanged(object sender, EventArgs e)
        {
			tbCLHK.Enabled = cbCLActive.Checked;
        }
        void cbCSActive_CheckedChanged(object sender, EventArgs e)
        {
			tbCSHK.Enabled = cbCSActive.Checked;
        }
        void cbCLineActive_CheckedChanged(object sender, EventArgs e)
        {
			tbCLineHK.Enabled = cbCLineActive.Checked;
        }
        void cbUseEmulate_CheckedChanged(object sender, EventArgs e)
        {
			cbELSType.Enabled = cbUseEmulate.Checked;
        }
        #endregion
        #region Buttons & link
        void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            messagebox = false;
            RefreshControlsData();
            tempRestore();
            ToggleVisibility();
        }
        void btnOK_Click(object sender, EventArgs e)
        {
            Apply();
            ToggleVisibility();
        }
        void btnHelp_Click(object sender, EventArgs e)
        {
            messagebox = true;
            MessageBox.Show(MMain.Msgs[2], MMain.Msgs[3], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void GitHubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BladeMight/Mahou");
        }
        void btnUpd_Click(object sender, EventArgs e)
        {
            update.ShowDialog();
        }
        void btnDDD_Click(object sender, EventArgs e)
        {
            moreConfigs.ShowDialog();
        }
        void btnLangChange_Click(object sender, EventArgs e)
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
        void mhouIcon_DoubleClick(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }
        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }
        #endregion
        #region WndProc & Functions
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Modifiers.WM_HOTKEY_MSG_ID)
            {
                var Key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int Modifs = (int)m.LParam & 0xFFFF;
                //This stops hotkeys when main window is visible
                if (!Focused)
                {
                	IfNotExist();
                    if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCSMods")))
                    {
                        if (MMain.MyConfs.ReadBool("Functions", "BlockCTRL") && MMain.MyConfs.Read("Hotkeys", "HKCSMods").Contains("Control")) { }
                        else
                        {
                            if ((!GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCSMods"))[0] &&
                                 !GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCSMods"))[1] &&
                                 !GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCSMods"))[2]) &&
                                 MMain.MyConfs.ReadBool("Functions", "RePress"))
                            {
                                hotkeywithmodsfired = true;
                                RePressAfter(MMain.MyConfs.Read("Hotkeys", "HKCSMods"));
                            }
                            SendModsUp(GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCSMods")));
                            //Prevents queue converting
                            HKCSelection.Unregister(); //Stops hotkey ability
                            var t = new Task(KMHook.ConvertSelection);
                            t.RunSynchronously();
                        }
                    }
                    if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLMods")))
                    {
                        if (MMain.MyConfs.ReadBool("Functions", "BlockCTRL") && MMain.MyConfs.Read("Hotkeys", "HKCLMods").Contains("Control")) { }
                        else
                        {
                            if ((!GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLMods"))[0] &&
                                 !GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLMods"))[1] &&
                                 !GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLMods"))[2]) &&
                                 MMain.MyConfs.ReadBool("Functions", "RePress"))
                            {
                                hotkeywithmodsfired = true;
                                RePressAfter(MMain.MyConfs.Read("Hotkeys", "HKCLMods"));
                            }
                            SendModsUp(GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLMods")));
                            //String below prevents queue converting
                            HKCLast.Unregister(); //Stops hotkey ability
                            var t = new Task(new Action(() => KMHook.ConvertLast(MMain.c_word, true)));
                            t.RunSynchronously();
                        }
                    }
                    if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")))
                    {
                        if (MMain.MyConfs.ReadBool("Functions", "BlockCTRL") && MMain.MyConfs.Read("Hotkeys", "HKCLineMods").Contains("Control")) { }
                        else
                        {
                            if ((!GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLineMods"))[0] &&
                                 !GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLineMods"))[1] &&
                                 !GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLineMods"))[2]) &&
                                 MMain.MyConfs.ReadBool("Functions", "RePress"))
                            {
                                hotkeywithmodsfired = true;
                                RePressAfter(MMain.MyConfs.Read("Hotkeys", "HKCLineMods"));
                            }
                            SendModsUp(GetHKMods(MMain.MyConfs.Read("Hotkeys", "HKCLineMods")));
                            //String below prevents queue converting
                            HKCLine.Unregister(); //Stops hotkey ability
                            var t = new Task(new Action(() => KMHook.ConvertLast(MMain.c_line, false)));
                            t.RunSynchronously();
                        }
                    }
                }
                //these are global, so they don't need to be stoped when window is visible.
                if (Key == Keys.Insert && Modifs == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                    ToggleVisibility();
                if (Key == Keys.F12 && Modifs == Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT)
                    ExitProgram();
                if (Key == (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey") && Modifs == CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods")))
                {
                    if (MMain.MyConfs.ReadBool("Functions", "SymIgnModeEnabled"))
                    {
                        MMain.MyConfs.Write("Functions", "SymIgnModeEnabled", "false");
                        Icon = icon.trIcon.Icon = Properties.Resources.MahouTrayHD;
                    }
                    else
                    {
                        MMain.MyConfs.Write("Functions", "SymIgnModeEnabled", "true");
                        Icon = icon.trIcon.Icon = Properties.Resources.MahouSymbolIgnoreMode;
                    }
                }
            }
            if (m.Msg == Mahou.MMain.ao) // ao = Already Opened
            {
                ToggleVisibility();
            }
            base.WndProc(ref m);
        }

        void RePressAfter(string where) // Sets Press Again variables for modifiers
        {
			KMHook.shiftRP = where.Contains("Shift") ? true : false;
			KMHook.altRP = where.Contains("Alt") ? true : false;
			KMHook.ctrlRP = where.Contains("Control") ? true : false;
        }
        bool[] GetHKMods(string hkmods) //Gets awaible in hotkey modifiers
        {
			ctrl |= hkmods.Contains("Control");
			alt |= hkmods.Contains("Alt");
			shift |= hkmods.Contains("Shift");
            return new bool[] { alt, shift, ctrl };
        }
        public void SendModsUp(bool[] modstoup) //Sends mods up by modstoup array
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
        public int CheckNGetModifiers(string inpt) //Toggles variables(alt,shift,ctrl) by awaible in inpt modifiers
        {
            // inpt can be empty because of replaces so we switch it to "None" to avoid null reference exception.
            if (String.IsNullOrEmpty(inpt)) { inpt = "None"; }
            shift = inpt.Contains("Shift") ? true : false;
            alt = inpt.Contains("Alt") ? true : false;
            ctrl = inpt.Contains("Control") ? true : false;
            System.Threading.Thread.Sleep(1);
            return (alt ? Modifiers.ALT : 0x0000) + (ctrl ? Modifiers.CTRL : 0x0000) + (shift ? Modifiers.SHIFT : 0x0000);
        }
        public string Remake(Keys k) //Make readable some special keys
        {
			switch (k) {
				case Keys.Cancel:
					return k.ToString().Replace("Cancel", "Pause");
				case Keys.Scroll:
					return k.ToString().Replace("Cancel", "Scroll");
				case Keys.ShiftKey:
				case Keys.Menu:
				case Keys.ControlKey:
				case Keys.LWin:
				case Keys.RWin:
					return "";
				case Keys.D0:
				case Keys.D1:
				case Keys.D2:
				case Keys.D3:
				case Keys.D4:
				case Keys.D5:
				case Keys.D6:
				case Keys.D7:
				case Keys.D8:
				case Keys.D9:				
					return k.ToString().Replace("D", "");
				default:
					return k.ToString();
			}
        }
        public string OemReadable(string inpt) //Make readable Oem Keys
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
        void tempRestore() //Restores temporary variables from settings
        {
            //This creates(silently) new config file if existed one disappeared o_O
            IfNotExist();
            // Restores temps
            tempCLKey = MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey");
            tempCSKey = MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey");
            tempCLineKey = MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey");
            tempCLMods = MMain.MyConfs.Read("Hotkeys", "HKCLMods");
            tempCSMods = MMain.MyConfs.Read("Hotkeys", "HKCSMods");
            tempCLineMods = MMain.MyConfs.Read("Hotkeys", "HKCLineMods");
            tempLoc1 = tempLoc2 = new Locales.Locale { Lang = "dummy", uId = 0 };
        }
        void InitializeHotkeys() //Initializes all hotkeys
        {
            Mainhk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.Insert, this);
            Mainhk.Register();
            ExitHk = new HotkeyHandler(Modifiers.ALT + Modifiers.CTRL + Modifiers.SHIFT, Keys.F12, this);
            ExitHk.Register();
            HKSymIgn = new HotkeyHandler(CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods")),
                (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey"), this);
            if (MMain.MyConfs.ReadBool("EnabledHotkeys", "HKSymIgnEnabled"))
            {
                HKSymIgn.Register();
                HKSIReg = true;
            }
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
        }
        void RegisterEnabled() //Registers enabled hotkeys
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
        public void IfNotExist()
        {
        	if (!System.IO.File.Exists(System.IO.Path.Combine(Mahou.Update.nPath, "Mahou.ini")))
        	    {
        	    	MMain.MyConfs = new Configs();
        	    	tempRestore();
        	    }
        }
        void Apply() //Saves current selections to settings
        {
        	IfNotExist();
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
                    messagebox = false;
            }

            if (!string.IsNullOrEmpty(tempCLMods) && tempCLKey != 0)
                MMain.MyConfs.Write("Hotkeys", "HKCLMods", tempCLMods);

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
                MMain.MyConfs.Write("Hotkeys", "HKCSMods", tempCSMods);

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
                MMain.MyConfs.Write("Hotkeys", "HKCLineMods", tempCLineMods);

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
            if (cbAutorun.Checked)
            {
                CreateShortcut();
            }
            else
            {
                DeleteShortcut();
            }
            MMain.MyConfs.Write("Hotkeys", "OnlyKeyLayoutSwicth", cbSwitchLayoutKeys.Text);
            MMain.MyConfs.Write("Functions", "CycleMode", cbCycleMode.Checked.ToString());
            MMain.MyConfs.Write("Functions", "IconVisibility", cbTrayIcon.Checked.ToString());
            MMain.MyConfs.Write("Functions", "BlockCTRL", cbBlockC.Checked.ToString());
            MMain.MyConfs.Write("Functions", "CSSwitch", cbCSSwitch.Checked.ToString());
            MMain.MyConfs.Write("Functions", "EmulateLayoutSwitch", cbUseEmulate.Checked.ToString());
            MMain.MyConfs.Write("Functions", "ELSType", cbELSType.SelectedIndex.ToString());
            MMain.MyConfs.Write("Functions", "RePress", cbRePress.Checked.ToString());
            MMain.MyConfs.Write("Functions", "EatOneSpace", cbEatOneSpace.Checked.ToString());
            MMain.MyConfs.Write("Functions", "ReSelect", cbResel.Checked.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKCLEnabled", cbCLActive.Checked.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKCSEnabled", cbCSActive.Checked.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKCLineEnabled", cbCLineActive.Checked.ToString());
            if (tempLoc1.Lang != "dummy" || tempLoc1.uId != 0)
            {
                MMain.MyConfs.Write("Locales", "locale1Lang", tempLoc1.Lang);
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
        void EnableIF() //Enables controls IF...
        {
            if (!cbCycleMode.Checked)
            {
                gbSBL.Enabled = true;
                cbUseEmulate.Enabled = cbELSType.Enabled = false;
            }
            else
            {
                gbSBL.Enabled = false;
                cbUseEmulate.Enabled = true;
				cbELSType.Enabled &= cbUseEmulate.Checked;
            }
        }
        public void ToggleVisibility() //Toggles visibility of main window
        {
			if (Visible != false)
				Visible = moreConfigs.Visible = update.Visible = false;
			else {
				TopMost = true;
				Visible = true;
				System.Threading.Thread.Sleep(5);
				TopMost = false;
			}
            Refresh();
        }
        public void RemoveAddCtrls() //Removes or adds ctrls to "Switch layout by key" items
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
        void RefreshControlsData() //Refresh all controls state from configs
        {
            RefreshLocales();
            RefreshIconAll();
            cbSwitchLayoutKeys.Text = MMain.MyConfs.Read("Hotkeys", "OnlyKeyLayoutSwicth");
            cbTrayIcon.Checked = MMain.MyConfs.ReadBool("Functions", "IconVisibility");
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
            RefreshLocales();
        }
        void RefreshLocales() //Re-adds existed locales to select boxes
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
                RefreshLocales();
                cbLangOne.SelectedIndex = 0;
                cbLangTwo.SelectedIndex = 1;
            }
        }
        void RefreshLanguage() //Refreshed in realtime all controls text
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
            cbTrayIcon.Text = MMain.UI[12];
            cbCycleMode.Text = MMain.UI[13];
            cbUseEmulate.Text = MMain.UI[14];
            gbSBL.Text = MMain.UI[15];
            lbl1lng.Text = MMain.UI[16] + " 1:";
            lbl2lng.Text = MMain.UI[16] + " 2:";
            btnApply.Text = MMain.UI[17];
            btnOK.Text = MMain.UI[18];
            btnCancel.Text = MMain.UI[19];
            btnHelp.Text = MMain.UI[20];
            icon.RefreshText(MMain.UI[44], MMain.UI[42], MMain.UI[43]);
        }
        public void RefreshIconAll() //Refreshes icon's icon and visibility
        {
            if (MMain.MyConfs.ReadBool("Functions", "SymIgnModeEnabled") && MMain.MyConfs.ReadBool("EnabledHotkeys", "HKSymIgnEnabled"))
                Icon = icon.trIcon.Icon = Properties.Resources.MahouSymbolIgnoreMode;
            else
                Icon = icon.trIcon.Icon = Properties.Resources.MahouTrayHD;
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
        void CreateShortcut() //Creates startup shortcut
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
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = description;
            shortcut.TargetPath = currentPath;
            shortcut.Save();
        }
        void DeleteShortcut() //Deletes startup shortcut
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
        public void ExitProgram()
        {
            icon.Hide();
            Refresh();
            Application.Exit();
        }
        #endregion
        #region TOOLTIPS!!!
        void cbCycleMode_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbCycleMode.Text;
            HelpTT.Show(MMain.TTips[0], cbCycleMode);
        }
        void tbCLHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCLHK.Text;
            HelpTT.Show(MMain.TTips[1], tbCLHK);
        }
        void tbCSHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCSHK.Text;
            HelpTT.Show(MMain.TTips[2], tbCSHK);
        }
        void tbCLineHK_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = tbCLineHK.Text;
            HelpTT.Show(MMain.TTips[3], tbCLineHK);
        }
        void GitHubLink_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = GitHubLink.Text;
            HelpTT.Show(MMain.TTips[4], GitHubLink);
        }
        void TrayIconCheckBox_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbTrayIcon.Text;
            HelpTT.Show(MMain.TTips[5], cbTrayIcon);
        }
        void btnUpd_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = btnUpd.Text;
            HelpTT.Show(MMain.TTips[6], btnUpd);
        }
        void cbBlockAC_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbBlockC.Text;
            HelpTT.Show(MMain.TTips[7], cbBlockC);
        }
        void cbSwitchLayoutKeys_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbSwitchLayoutKeys.Text;
            HelpTT.Show(MMain.TTips[8], cbSwitchLayoutKeys);
        }
        void cbUseEmulate_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbUseEmulate.Text;
            HelpTT.Show(MMain.TTips[9], cbUseEmulate);
        }
        void cbUseCycleForCS_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbCSSwitch.Text;
            HelpTT.Show(MMain.TTips[10], cbCSSwitch);
        }
        void gbSBL_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = gbSBL.Text;
            HelpTT.Show(MMain.TTips[11], gbSBL);
        }
        void cbRePress_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbRePress.Text;
            HelpTT.Show(MMain.TTips[12], cbRePress);
        }
        void cbEatOneSpace_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbEatOneSpace.Text;
            HelpTT.Show(MMain.TTips[13], cbEatOneSpace);

        }
        void cbResel_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbResel.Text;
            HelpTT.Show(MMain.TTips[14], cbResel);
        }
        void cbELSType_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = MMain.TTips[19];
            HelpTT.Show(MMain.TTips[15], cbELSType);
        }
        #endregion
    }
}
