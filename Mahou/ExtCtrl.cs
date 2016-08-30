using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mahou
{
    public partial class ExtCtrls : Form
    {
        #region Button/Form/etc. events
        public ExtCtrls()
        {
            InitializeComponent();
        }
        private void ExtCtrl_Load(object sender, EventArgs e)
        {
            RefreshLocales();
            DisEna();
            load();
            RefreshLanguage();
        }
        private void ExtCtrls_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close(e);
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
        #endregion
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
        }
        private void load() // Loads configurations
        {
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
        }
        private void RefreshLanguage()
        {
            cbUseLRC.Text = MMain.UI[36];
            lbLCto.Text = MMain.UI[37];
            lbRCto.Text = MMain.UI[38];
            this.Text = MMain.UI[39];
        }
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
        #endregion
    }
}
