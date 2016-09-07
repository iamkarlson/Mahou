using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mahou
{
    public partial class MoreConfigs : Form
    {
        #region Varibales
        int tmpSIKey = 0;
        string tmpSIMods = "None";
        #endregion
        #region Button/Form/etc. events
        public MoreConfigs()
        {
            InitializeComponent();
        }
        private void MoreConfigs_Load(object sender, EventArgs e)
        {
            RefreshLocales();
            DisEna();
            load();
            RefreshLanguage();
            tmpRestore();
            tbHKSymIgn.Text = MMain.mahou.OemReadable((MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods").ToString().Replace(",", " +") + " + " +
                MMain.mahou.Remake((Keys)MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey")).Replace("None + ", "")));
        }
        private void MoreConfigs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close(e);
        }
        private void MoreConfigs_Activated(object sender, EventArgs e)
        {
            if (MMain.mahou.HKSIReg)
                MMain.mahou.HKSymIgn.Unregister();
        }
        private void MoreConfigs_Deactivate(object sender, EventArgs e)
        {
            if (MMain.MyConfs.ReadBool("EnabledHotkeys", "HKSymIgnEnabled"))
                MMain.mahou.HKSymIgn.Register();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }
        private void btnNO_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cbUseLRC_CheckedChanged(object sender, EventArgs e)
        {
            DisEna();
        }
        private void tbHKSymIgn_KeyDown(object sender, KeyEventArgs e)
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
        private void Close(FormClosingEventArgs e) // Closes window without destruction
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            DisEna();
        }
        private void DisEna() // Disables or Enables some controls
        {
            if (cbUseLRC.Checked)
                lbLCto.Enabled = lbRCto.Enabled = cbLCLocalesList.Enabled = cbRCLocalesList.Enabled = true;
            else
                lbLCto.Enabled = lbRCto.Enabled = cbLCLocalesList.Enabled = cbRCLocalesList.Enabled = false;
            MMain.mahou.RemoveAddCtrls();
        } 
        private void Save() // Saves configurations
        {
            Regex getname = new Regex("\\w+");
            Regex getUID = new Regex("\\w+\\W(\\d+)");
            MMain.MyConfs.Write("ExtCtrls", "LCLocaleName", getname.Match(cbLCLocalesList.Text).Value);
            MMain.MyConfs.Write("ExtCtrls", "RCLocaleName", getname.Match(cbRCLocalesList.Text).Value);
            MMain.MyConfs.Write("ExtCtrls", "LCLocale", getUID.Match(cbLCLocalesList.Text).Groups[1].Value);
            MMain.MyConfs.Write("ExtCtrls", "RCLocale", getUID.Match(cbRCLocalesList.Text).Groups[1].Value);
            MMain.MyConfs.Write("ExtCtrls", "UseExtCtrls", cbUseLRC.Checked.ToString());
            MMain.MyConfs.Write("EnabledHotkeys", "HKSymIgnEnabled", cbSymIgn.Checked.ToString());
            MMain.MyConfs.Write("Functions", "MoreTries", cbMoreTries.Checked.ToString());
            MMain.MyConfs.Write("Functions", "TriesCount", nudMTCount.Value.ToString());
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
            MMain.mahou.RefreshIconAll();

        }
        private void load() // Loads configurations
        {
            Console.WriteLine(MMain.mahou.HKSIReg);
            try
            {
                if (String.IsNullOrEmpty(MMain.MyConfs.Read("ExtCtrls", "LCLocaleName")))
                    cbLCLocalesList.SelectedIndex = 0;
                else
                    cbLCLocalesList.SelectedIndex = MMain.lcnmid.IndexOf(MMain.MyConfs.Read("ExtCtrls", "LCLocaleName") + "(" + MMain.MyConfs.Read("ExtCtrls", "LCLocale") + ")");
                if (String.IsNullOrEmpty(MMain.MyConfs.Read("ExtCtrls", "RCLocaleName")))
                    cbRCLocalesList.SelectedIndex = 1;
                else
                    cbRCLocalesList.SelectedIndex = MMain.lcnmid.IndexOf(MMain.MyConfs.Read("ExtCtrls", "RCLocaleName") + "(" + MMain.MyConfs.Read("ExtCtrls", "RCLocale") + ")");
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
        }
        private void tmpRestore() // Restores temporaries
        {
            tmpSIKey = MMain.MyConfs.ReadInt("Hotkeys", "HKSymIgnKey");
            tmpSIMods = MMain.MyConfs.Read("Hotkeys", "HKSymIgnMods");
        }
        private void RefreshLocales() // Refreshes locales in comboboxes
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
        private void RefreshLanguage() // Refreshes controls text
        {
            cbUseLRC.Text = MMain.UI[36];
            lbLCto.Text = MMain.UI[37];
            lbRCto.Text = MMain.UI[38];
            this.Text = MMain.UI[39];
            cbSymIgn.Text = MMain.UI[40];
            btnNO.Text = MMain.UI[19];
            cbMoreTries.Text = MMain.UI[41];
        }
        #endregion
        #region Tooltips
        private void cbLCLocalesList_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbLCLocalesList.Text;
            HelpTT.Show(MMain.TTips[16], cbLCLocalesList);
        }
        private void cbRCLocalesList_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbRCLocalesList.Text;
            HelpTT.Show(MMain.TTips[17], cbRCLocalesList);
        }
        private void cbUseLRC_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbUseLRC.Text;
            HelpTT.Show(MMain.TTips[18], cbUseLRC);
        }
        private void cbSymIgn_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbSymIgn.Text;
            HelpTT.Show(MMain.TTips[20], cbSymIgn);
        }
        private void cbMoreTries_MouseHover(object sender, EventArgs e)
        {
            HelpTT.ToolTipTitle = cbMoreTries.Text;
            HelpTT.Show(MMain.TTips[21], cbMoreTries);
        }
        #endregion
    }
}
