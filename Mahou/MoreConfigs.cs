using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mahou
{
    public partial class MoreConfigs : Form
    {
        #region Varibales
        int tmpSIKey = 0;
        string tmpSIMods = "None";
		ColorDialog clrd = new ColorDialog();
        #endregion
        #region Button/Form/etc. events
        public MoreConfigs()
        {
            InitializeComponent();
        }
        void MoreConfigs_Load(object sender, EventArgs e)
        {
            RefreshLocales();
            DisEna();
            load();
            RefreshLanguage();
            tmpRestore();
            tbHKSymIgn.Text = MMain.mahou.OemReadable((MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods").ToString().Replace(",", " +") + " + " +
                MMain.mahou.Remake((Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey")).Replace("None + ", "")));
        }
        void MoreConfigs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close(e);
        }
        void MoreConfigs_Activated(object sender, EventArgs e)
        {
            if (MMain.mahou.HKSIReg)
                MMain.mahou.HKSymIgn.Unregister();
        }
        void MoreConfigs_Deactivate(object sender, EventArgs e)
        {
            if (MMain.MyConfs.ReadBool("EnabledHotkeys", "HKSymIgnEnabled"))
                MMain.mahou.HKSymIgn.Register();
        }
        void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }
        void btnNO_Click(object sender, EventArgs e)
        {
            Close();
        }
		void BtCol1Click(object sender, EventArgs e)
		{
			if (clrd.ShowDialog() == DialogResult.OK)
				btCol1.BackColor = clrd.Color;
		}
		void BtCol2Click(object sender, EventArgs e)
		{
			if (clrd.ShowDialog() == DialogResult.OK)
				btCol2.BackColor = clrd.Color;
		}
        void cbUseLRC_CheckedChanged(object sender, EventArgs e)
        {
            DisEna();
        }
        void tbHKSymIgn_KeyDown(object sender, KeyEventArgs e)
        {
            tbHKSymIgn.Text = MMain.mahou.OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " +
                MMain.mahou.Remake(e.KeyCode)).Replace("None + ", ""));
            tmpSIMods = e.Modifiers.ToString().Replace(",", " +");
            switch ((int)e.KeyCode)
            {
                case 16:
                case 17:
                case 18:
                    tmpSIKey = 0;
                    break;
                default:
                    tmpSIKey = (int)e.KeyCode;
                    break;
            }
        }
        #endregion
        #region Functions
        void Close(FormClosingEventArgs e) // Closes window without destruction
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
            DisEna();
        }
        void DisEna() // Disables or Enables some controls
        {
        	MMain.mahou.IfNotExist();
            lbLCto.Enabled = lbRCto.Enabled = cbLCLocalesList.Enabled = cbRCLocalesList.Enabled = cbUseLRC.Checked;
            nudRefreshRate.Enabled = lblRefRate.Enabled = lbColors.Enabled = btCol1.Enabled = btCol2.Enabled = cbDisplayLang.Checked;
            MMain.mahou.RemoveAddCtrls();
        } 
        void Save() // Saves configurations
        {
        	MMain.mahou.IfNotExist();
            var getname = new Regex("\\w+");
            var getUID = new Regex("\\w+\\W(\\d+)");
            MMain.MyConfs.Write("ExtCtrls", "LCLocaleName", getname.Match(cbLCLocalesList.Text).Value);
            MMain.MyConfs.Write("ExtCtrls", "RCLocaleName", getname.Match(cbRCLocalesList.Text).Value);
            MMain.MyConfs.Write("ExtCtrls", "LCLocale", getUID.Match(cbLCLocalesList.Text).Groups[1].Value);
            MMain.MyConfs.Write("ExtCtrls", "RCLocale", getUID.Match(cbRCLocalesList.Text).Groups[1].Value);
            MMain.MyConfs.Write("ExtCtrls", "UseExtCtrls", cbUseLRC.Checked.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKSymIgnEnabled", cbSymIgn.Checked.ToString());
            MMain.MyConfs.Write("Functions", "MoreTries", cbMoreTries.Checked.ToString());
            MMain.MyConfs.Write("Functions", "TriesCount", nudMTCount.Value.ToString());
            MMain.MyConfs.Write("Functions", "DLRefreshRate", nudRefreshRate.Value.ToString());
            MMain.MyConfs.Write("Functions", "DisplayLang", cbDisplayLang.Checked.ToString());
            MMain.MyConfs.Write("Functions", "DLForeColor", ColorTranslator.ToHtml(btCol1.BackColor));
            MMain.MyConfs.Write("Functions", "DLBackColor", ColorTranslator.ToHtml(btCol2.BackColor));
            MMain.mahou.langDisplay.ChangeColors(ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions","DLForeColor")),
                                     ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions","DLBackColor")));
            bool hksymignnotready = false;
            if (tmpSIKey != 0)
            {
                if (tmpSIKey == MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey") && tmpSIMods == MMain.MyConfs.Read("Hotkeys", "HKCLMods"))
                {
                    MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    hksymignnotready = true;
                }
                if (tmpSIKey == MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey") && tmpSIMods == MMain.MyConfs.Read("Hotkeys", "HKCSMods"))
                {
                    MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    hksymignnotready = true;
                }
                if (tmpSIKey == MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey") && tmpSIMods == MMain.MyConfs.Read("Hotkeys", "HKCLineMods"))
                {
                    MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    hksymignnotready = true;
                }
            }

            if (!string.IsNullOrEmpty(tmpSIMods) && tmpSIKey != 0)
                MMain.MyConfs.Write("Hotkeys", "HKSymIgnMods", tmpSIMods);

            if (tmpSIKey != 0)
                MMain.MyConfs.Write("Hotkeys", "HKSymIgnKey", tmpSIKey.ToString());
            else
            {
                hksymignnotready = true;
                MessageBox.Show(MMain.Msgs[6], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (!hksymignnotready && MMain.MyConfs.ReadBool("EnabledHotkeys", "HKSymIgnEnabled"))
            {
                if (MMain.mahou.HKSIReg)
                {
                    MMain.mahou.HKSymIgn.Unregister();
                    MMain.mahou.HKSIReg = false;
                }
                MMain.mahou.HKSymIgn = new HotkeyHandler(MMain.mahou.CheckNGetModifiers(MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods")),
                    (Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey"), MMain.mahou);
                MMain.mahou.HKSymIgn.Register();
                MMain.mahou.HKSIReg = true;
            }
            else
            {
                MMain.mahou.HKSymIgn.Unregister();
                MMain.mahou.HKSIReg = false;
            }
            if (MMain.MyConfs.ReadBool("Functions", "DisplayLang"))
            	MMain.mahou.ICheck.Start();
            else
            	MMain.mahou.ICheck.Stop();
            MMain.mahou.RefreshIconAll();

        }
        void load() // Loads configurations
        {
        	MMain.mahou.IfNotExist();
            try
            {
				cbLCLocalesList.SelectedIndex = String.IsNullOrEmpty(MMain.MyConfs.Read("ExtCtrls", "LCLocaleName")) ? 0 
					: MMain.lcnmid.IndexOf(MMain.MyConfs.Read("ExtCtrls", "LCLocaleName") + "(" + MMain.MyConfs.Read("ExtCtrls", "LCLocale") + ")");
				cbRCLocalesList.SelectedIndex = String.IsNullOrEmpty(MMain.MyConfs.Read("ExtCtrls", "RCLocaleName")) ? 1 
					: MMain.lcnmid.IndexOf(MMain.MyConfs.Read("ExtCtrls", "RCLocaleName") + "(" + MMain.MyConfs.Read("ExtCtrls", "RCLocale") + ")");
            }
            catch
            {
                MessageBox.Show(MMain.Msgs[9], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RefreshLocales();
                cbLCLocalesList.SelectedIndex = 0;
                cbRCLocalesList.SelectedIndex = 1;
            }
            cbUseLRC.Checked = MMain.MyConfs.ReadBool("ExtCtrls", "UseExtCtrls");
            cbSymIgn.Checked = MMain.MyConfs.ReadBool("EnabledHotkeys", "HKSymIgnEnabled");
            cbMoreTries.Checked = MMain.MyConfs.ReadBool("Functions", "MoreTries");
            nudMTCount.Value = MMain.MyConfs.ReadInt("Functions", "TriesCount");
            nudRefreshRate.Value = MMain.MyConfs.ReadInt("Functions", "DLRefreshRate");
            cbDisplayLang.Checked = MMain.MyConfs.ReadBool("Functions", "DisplayLang");
        	btCol1.BackColor = ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions", "DLForeColor"));
        	btCol2.BackColor = ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions", "DLBackColor"));
        }
        void tmpRestore() // Restores temporaries
        {
        	MMain.mahou.IfNotExist();
            tmpSIKey = MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey");
            tmpSIMods = MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods");
        }
        void RefreshLocales() // Refreshes locales in comboboxes
        {
            Locales.IfLessThan2();
            MMain.locales = Locales.AllList();
            cbLCLocalesList.Items.Clear();
            cbRCLocalesList.Items.Clear();
            MMain.lcnmid.Clear();
            foreach (Locales.Locale lc in MMain.locales)
            {
                cbLCLocalesList.Items.Add(lc.Lang + "(" + lc.uId + ")");
                cbRCLocalesList.Items.Add(lc.Lang + "(" + lc.uId + ")");
                MMain.lcnmid.Add(lc.Lang + "(" + lc.uId + ")");
            }
        }
        void RefreshLanguage() // Refreshes controls text
        {
            cbUseLRC.Text = MMain.UI[36];
            lbLCto.Text = MMain.UI[37];
            lbRCto.Text = MMain.UI[38];
            Text = MMain.UI[39];
            cbSymIgn.Text = MMain.UI[40];
            cbMoreTries.Text = MMain.UI[41];
            btnNO.Text = MMain.UI[19];
            cbDisplayLang.Text = MMain.UI[45];
            lblRefRate.Text = MMain.UI[46];
            lbColors.Text = MMain.UI[47];
        }
        #endregion
        #region Tooltips
        void cbLCLocalesList_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbLCLocalesList.Text;
            HelpTT.Show(MMain.TTips[16], cbLCLocalesList);
        }
        void cbRCLocalesList_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbRCLocalesList.Text;
            HelpTT.Show(MMain.TTips[17], cbRCLocalesList);
        }
        void cbUseLRC_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbUseLRC.Text;
            HelpTT.Show(MMain.TTips[18], cbUseLRC);
        }
        void cbSymIgn_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbSymIgn.Text;
            HelpTT.Show(MMain.TTips[20], cbSymIgn);
        }
        void cbMoreTries_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbMoreTries.Text;
            HelpTT.Show(MMain.TTips[21], cbMoreTries);
        }
		void CbDisplayLangMouseHover(object sender, EventArgs e)
		{
            HelpTT.ToolTipTitle = cbDisplayLang.Text;
            HelpTT.Show(MMain.TTips[22], cbDisplayLang);
		}
		void LblRefRateMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = lblRefRate.Text;
            HelpTT.Show(MMain.TTips[23], lblRefRate);
		}
		void LbColorsMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = lbColors.Text;
            HelpTT.Show(MMain.TTips[24], lbColors);
	
		}
        #endregion
    }
}
