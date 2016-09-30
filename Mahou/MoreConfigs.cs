using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
namespace Mahou
{
	public partial class MoreConfigs : Form
	{
		#region Varibales
		int tmpSIKey = 0;
		string tmpSIMods = "None";
		ColorDialog clrd = new ColorDialog();
		FontDialog fntd = new FontDialog();
		public FontConverter fcv = new FontConverter();
		public string snipfile = Path.Combine(Mahou.Update.nPath, "snippets.txt");
		#endregion
		#region Button/Form/etc. events
		public MoreConfigs()
		{
			InitializeComponent();
		}
		void MoreConfigs_Load(object sender, EventArgs e)
		{
			nudXpos.Minimum = nudYpos.Minimum = -100;
			RefreshLocales();
			load();
			DisEna();
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
			RefreshLanguage();
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
		void BtFontClick(object sender, EventArgs e)
		{
			fntd.Font = btFont.Font;
			if (fntd.ShowDialog() == DialogResult.OK)
				btFont.Font = fntd.Font;
		}
		void DisEnaOnCheckedChanged(object sender, EventArgs e)
		{
			DisEna();
		}
		void tbHKSymIgn_KeyDown(object sender, KeyEventArgs e)
		{
			if (MMain.MyConfs.ReadBool("DoubleKey", "Use")) {
				tbHKSymIgn.Text = MMain.mahou.OemReadable(MMain.mahou.Remake(e.KeyCode));
				tmpSIMods = "None";
				tmpSIKey = (int)e.KeyCode;				
			} else {
				tbHKSymIgn.Text = MMain.mahou.OemReadable((e.Modifiers.ToString().Replace(",", " +") + " + " +
				MMain.mahou.Remake(e.KeyCode)).Replace("None + ", ""));
				tmpSIMods = e.Modifiers.ToString().Replace(",", " +");
				switch ((int)e.KeyCode) {
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
		}
		void BtnEMoreClick(object sender, EventArgs e)
		{
			pEMore.Visible = !pEMore.Visible;
			DisEna();
			RefreshLanguage();
		}
		#endregion
		#region Functions
		void Close(FormClosingEventArgs e) // Closes window without destruction
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
				Visible = false;
			}
			DisEna();
		}
		void DisEna() // Disables or Enables some controls
		{
			MMain.mahou.IfNotExist();
			lbLCto.Enabled = lbRCto.Enabled = cbLCLocalesList.Enabled = cbRCLocalesList.Enabled = cbUseLRC.Checked;
			nudRefreshRate.Enabled = lblRefRate.Enabled = lbColors.Enabled = btCol1.Enabled = btCol2.Enabled = btFont.Enabled =
				lbSize.Enabled = lbPosition.Enabled = nudTTWidth.Enabled = nudTTHeight.Enabled = nudXpos.Enabled = nudYpos.Enabled = cbDisplayLang.Checked;
			lbDDelay.Enabled = nudDoubleDelay.Enabled = cbDoublePress.Checked;
			btCol2.Enabled = !cbTrBLT.Checked;
			tbSnippets.Enabled = cbUseSnippets.Checked;
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
			MMain.MyConfs.Write("Functions", "ExperimentalCSSwitch", cbExCSSwitch.Checked.ToString());
			MMain.MyConfs.Write("Functions", "Snippets", cbUseSnippets.Checked.ToString());
			MMain.MyConfs.Write("TTipUI", "Height", nudTTHeight.Value.ToString());
			MMain.MyConfs.Write("TTipUI", "Width", nudTTWidth.Value.ToString());
			MMain.MyConfs.Write("TTipUI", "Font", fcv.ConvertToString(btFont.Font));
			MMain.MyConfs.Write("TTipUI", "xpos", nudXpos.Value.ToString());
			MMain.MyConfs.Write("TTipUI", "ypos", nudYpos.Value.ToString());
			MMain.MyConfs.Write("TTipUI", "TransparentBack", cbTrBLT.Checked.ToString());
			MMain.mahou.langDisplay.ChangeColors(ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions", "DLForeColor")),
				ColorTranslator.FromHtml(MMain.MyConfs.Read("Functions", "DLBackColor")));
			MMain.mahou.langDisplay.ChangeSizes((Font)fcv.ConvertFromString(MMain.MyConfs.Read("TTipUI", "Font")), 
				MMain.MyConfs.ReadInt("TTipUI", "Height"), 
				MMain.MyConfs.ReadInt("TTipUI", "Width"));
			MMain.MyConfs.Write("DoubleKey", "Delay", nudDoubleDelay.Value.ToString());
			MMain.MyConfs.Write("DoubleKey", "Use", cbDoublePress.Checked.ToString());
			File.WriteAllText(snipfile, tbSnippets.Text);
			KMHook.ReInitSnippets();
			MMain.mahou.langDisplay.SetVisInvis();
			if (tmpSIKey != 0) {
				if (tmpSIKey == MMain.MyConfs.ReadInt("Hotkeys", "HKCLKey") && tmpSIMods == MMain.MyConfs.Read("Hotkeys", "HKCLMods")) {
					MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				if (tmpSIKey == MMain.MyConfs.ReadInt("Hotkeys", "HKCSKey") && tmpSIMods == MMain.MyConfs.Read("Hotkeys", "HKCSMods")) {
					MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				if (tmpSIKey == MMain.MyConfs.ReadInt("Hotkeys", "HKCLineKey") && tmpSIMods == MMain.MyConfs.Read("Hotkeys", "HKCLineMods")) {
					MessageBox.Show(MMain.Msgs[4], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			if (!string.IsNullOrEmpty(tmpSIMods) && tmpSIKey != 0)
				MMain.MyConfs.Write("Hotkeys", "HKSymIgnMods", tmpSIMods);

			if (tmpSIKey != 0)
				MMain.MyConfs.Write("Hotkeys", "HKSymIgnKey", tmpSIKey.ToString());
			else
				MessageBox.Show(MMain.Msgs[6], MMain.Msgs[5], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			if (MMain.MyConfs.ReadBool("Functions", "DisplayLang"))
				MMain.mahou.ICheck.Start();
			else
				MMain.mahou.ICheck.Stop();
			MMain.mahou.RefreshIconAll();
			MMain.mahou.InitializeHotkeys();

		}
		void load() // Loads configurations
		{
			MMain.mahou.IfNotExist();
			try {
				cbLCLocalesList.SelectedIndex = String.IsNullOrEmpty(MMain.MyConfs.Read("ExtCtrls", "LCLocaleName")) ? 0 
					: MMain.lcnmid.IndexOf(MMain.MyConfs.Read("ExtCtrls", "LCLocaleName") + "(" + MMain.MyConfs.Read("ExtCtrls", "LCLocale") + ")");
				cbRCLocalesList.SelectedIndex = String.IsNullOrEmpty(MMain.MyConfs.Read("ExtCtrls", "RCLocaleName")) ? 1 
					: MMain.lcnmid.IndexOf(MMain.MyConfs.Read("ExtCtrls", "RCLocaleName") + "(" + MMain.MyConfs.Read("ExtCtrls", "RCLocale") + ")");
			} catch {
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
			cbExCSSwitch.Checked = MMain.MyConfs.ReadBool("Functions", "ExperimentalCSSwitch");
			cbUseSnippets.Checked = MMain.MyConfs.ReadBool("Functions", "Snippets");
			nudTTHeight.Value = MMain.MyConfs.ReadInt("TTipUI", "Height");
			nudTTWidth.Value = MMain.MyConfs.ReadInt("TTipUI", "Width");
			btFont.Font = (Font)fcv.ConvertFromString(MMain.MyConfs.Read("TTipUI", "Font"));
			nudXpos.Value = MMain.MyConfs.ReadInt("TTipUI", "xpos");
			nudYpos.Value = MMain.MyConfs.ReadInt("TTipUI", "ypos");
			cbTrBLT.Checked = MMain.MyConfs.ReadBool("TTipUI", "TransparentBack");
			nudDoubleDelay.Value = MMain.MyConfs.ReadInt("DoubleKey", "Delay");
			cbDoublePress.Checked = MMain.MyConfs.ReadBool("DoubleKey", "Use");
			if (File.Exists(snipfile)) {
				tbSnippets.Text = File.ReadAllText(snipfile);
				KMHook.ReInitSnippets();
			}
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
			foreach (Locales.Locale lc in MMain.locales) {
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
			btFont.Text = MMain.UI[52];
			lbSize.Text = MMain.UI[53];
			lbPosition.Text = MMain.UI[54];
			btnEMore.Text = !pEMore.Visible ? MMain.UI[55] : MMain.UI[56];
			cbDoublePress.Text = MMain.UI[57];
			lbDDelay.Text = MMain.UI[58];
			cbExCSSwitch.Text = MMain.UI[59];
			cbTrBLT.Text = MMain.UI[60];
			cbUseSnippets.Text = MMain.UI[61];
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
		void LbSizeMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = lbSize.Text;
			HelpTT.Show(MMain.TTips[25], lbSize);
		}
		void LbPositionMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = lbPosition.Text;
			HelpTT.Show(MMain.TTips[26], lbPosition);
		}
		void CbDoublePressMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = cbDoublePress.Text;
			HelpTT.Show(MMain.TTips[27], cbDoublePress);	
		}
		void LbDDelayMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = lbDDelay.Text;
			HelpTT.Show(MMain.TTips[28], lbDDelay);	
		}
		void CbExCSSwitchMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = cbExCSSwitch.Text;
			HelpTT.Show(MMain.TTips[29], cbExCSSwitch);	
		}
		void CbTrBLTMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = cbTrBLT.Text;
			HelpTT.Show(MMain.TTips[30], cbTrBLT);	
		}
		void CbUseSnippetsMouseHover(object sender, EventArgs e)
		{
			HelpTT.ToolTipTitle = cbUseSnippets.Text;
			HelpTT.Show(MMain.TTips[31], cbUseSnippets);	
		}
		#endregion
	}
}
