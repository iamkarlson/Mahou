namespace Mahou
{
    partial class MoreConfigs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	this.cbUseLRC = new System.Windows.Forms.CheckBox();
        	this.btnOK = new System.Windows.Forms.Button();
        	this.btnNO = new System.Windows.Forms.Button();
        	this.lbLCto = new System.Windows.Forms.Label();
        	this.lbRCto = new System.Windows.Forms.Label();
        	this.cbLCLocalesList = new System.Windows.Forms.ComboBox();
        	this.cbRCLocalesList = new System.Windows.Forms.ComboBox();
        	this.HelpTT = new System.Windows.Forms.ToolTip(this.components);
        	this.tbHKSymIgn = new System.Windows.Forms.TextBox();
        	this.cbSymIgn = new System.Windows.Forms.CheckBox();
        	this.cbMoreTries = new System.Windows.Forms.CheckBox();
        	this.nudMTCount = new System.Windows.Forms.NumericUpDown();
        	this.cbDisplayLang = new System.Windows.Forms.CheckBox();
        	this.nudRefreshRate = new System.Windows.Forms.NumericUpDown();
        	this.lblRefRate = new System.Windows.Forms.Label();
        	this.btCol2 = new System.Windows.Forms.Button();
        	this.btCol1 = new System.Windows.Forms.Button();
        	this.lbColors = new System.Windows.Forms.Label();
        	this.nudTTWidth = new System.Windows.Forms.NumericUpDown();
        	this.nudTTHeight = new System.Windows.Forms.NumericUpDown();
        	this.lbSize = new System.Windows.Forms.Label();
        	this.lbH = new System.Windows.Forms.Label();
        	this.lbW = new System.Windows.Forms.Label();
        	this.btFont = new System.Windows.Forms.Button();
        	this.nudYpos = new System.Windows.Forms.NumericUpDown();
        	this.lbY = new System.Windows.Forms.Label();
        	this.nudXpos = new System.Windows.Forms.NumericUpDown();
        	this.lbX = new System.Windows.Forms.Label();
        	this.lbPosition = new System.Windows.Forms.Label();
        	this.pEMore = new System.Windows.Forms.Panel();
        	this.cbUseSnippets = new System.Windows.Forms.CheckBox();
        	this.tbSnippets = new System.Windows.Forms.TextBox();
        	this.cbTrBLT = new System.Windows.Forms.CheckBox();
        	this.cbExCSSwitch = new System.Windows.Forms.CheckBox();
        	this.lbDDelay = new System.Windows.Forms.Label();
        	this.nudDoubleDelay = new System.Windows.Forms.NumericUpDown();
        	this.cbDoublePress = new System.Windows.Forms.CheckBox();
        	this.btnEMore = new System.Windows.Forms.Button();
        	this.cbOnChange = new System.Windows.Forms.CheckBox();
        	this.pEExtra = new System.Windows.Forms.Panel();
        	this.cbScrollLight = new System.Windows.Forms.CheckBox();
        	((System.ComponentModel.ISupportInitialize)(this.nudMTCount)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudRefreshRate)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudTTWidth)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudTTHeight)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudYpos)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudXpos)).BeginInit();
        	this.pEMore.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.nudDoubleDelay)).BeginInit();
        	this.pEExtra.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// cbUseLRC
        	// 
        	this.cbUseLRC.AutoSize = true;
        	this.cbUseLRC.Location = new System.Drawing.Point(10, 12);
        	this.cbUseLRC.Name = "cbUseLRC";
        	this.cbUseLRC.Size = new System.Drawing.Size(265, 17);
        	this.cbUseLRC.TabIndex = 0;
        	this.cbUseLRC.Text = "Use specific layout changing by Left/Right CTRLS";
        	this.cbUseLRC.UseVisualStyleBackColor = true;
        	this.cbUseLRC.CheckedChanged += new System.EventHandler(this.DisEnaOnCheckedChanged);
        	this.cbUseLRC.MouseHover += new System.EventHandler(this.cbUseLRC_MouseHover);
        	// 
        	// btnOK
        	// 
        	this.btnOK.Location = new System.Drawing.Point(10, 234);
        	this.btnOK.Name = "btnOK";
        	this.btnOK.Size = new System.Drawing.Size(75, 23);
        	this.btnOK.TabIndex = 1;
        	this.btnOK.Text = "OK";
        	this.btnOK.UseVisualStyleBackColor = true;
        	this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        	// 
        	// btnNO
        	// 
        	this.btnNO.Location = new System.Drawing.Point(193, 234);
        	this.btnNO.Name = "btnNO";
        	this.btnNO.Size = new System.Drawing.Size(75, 23);
        	this.btnNO.TabIndex = 2;
        	this.btnNO.Text = "Cancel";
        	this.btnNO.UseVisualStyleBackColor = true;
        	this.btnNO.Click += new System.EventHandler(this.btnNO_Click);
        	// 
        	// lbLCto
        	// 
        	this.lbLCto.AutoSize = true;
        	this.lbLCto.Location = new System.Drawing.Point(7, 32);
        	this.lbLCto.Name = "lbLCto";
        	this.lbLCto.Size = new System.Drawing.Size(120, 13);
        	this.lbLCto.TabIndex = 3;
        	this.lbLCto.Text = "Left Control switches to:";
        	// 
        	// lbRCto
        	// 
        	this.lbRCto.AutoSize = true;
        	this.lbRCto.Location = new System.Drawing.Point(148, 32);
        	this.lbRCto.Name = "lbRCto";
        	this.lbRCto.Size = new System.Drawing.Size(127, 13);
        	this.lbRCto.TabIndex = 4;
        	this.lbRCto.Text = "Right Control switches to:";
        	// 
        	// cbLCLocalesList
        	// 
        	this.cbLCLocalesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cbLCLocalesList.FormattingEnabled = true;
        	this.cbLCLocalesList.Location = new System.Drawing.Point(10, 48);
        	this.cbLCLocalesList.Name = "cbLCLocalesList";
        	this.cbLCLocalesList.Size = new System.Drawing.Size(117, 21);
        	this.cbLCLocalesList.TabIndex = 5;
        	this.cbLCLocalesList.MouseHover += new System.EventHandler(this.cbLCLocalesList_MouseHover);
        	// 
        	// cbRCLocalesList
        	// 
        	this.cbRCLocalesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cbRCLocalesList.FormattingEnabled = true;
        	this.cbRCLocalesList.Location = new System.Drawing.Point(151, 48);
        	this.cbRCLocalesList.Name = "cbRCLocalesList";
        	this.cbRCLocalesList.Size = new System.Drawing.Size(117, 21);
        	this.cbRCLocalesList.TabIndex = 6;
        	this.cbRCLocalesList.MouseHover += new System.EventHandler(this.cbRCLocalesList_MouseHover);
        	// 
        	// HelpTT
        	// 
        	this.HelpTT.AutoPopDelay = 25000;
        	this.HelpTT.InitialDelay = 500;
        	this.HelpTT.ReshowDelay = 100;
        	// 
        	// tbHKSymIgn
        	// 
        	this.tbHKSymIgn.Location = new System.Drawing.Point(127, 75);
        	this.tbHKSymIgn.Name = "tbHKSymIgn";
        	this.tbHKSymIgn.Size = new System.Drawing.Size(141, 20);
        	this.tbHKSymIgn.TabIndex = 7;
        	this.tbHKSymIgn.Text = "Shift + Control + Alt + F11";
        	this.tbHKSymIgn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHKSymIgn_KeyDown);
        	// 
        	// cbSymIgn
        	// 
        	this.cbSymIgn.AutoSize = true;
        	this.cbSymIgn.Location = new System.Drawing.Point(10, 78);
        	this.cbSymIgn.Name = "cbSymIgn";
        	this.cbSymIgn.Size = new System.Drawing.Size(96, 17);
        	this.cbSymIgn.TabIndex = 8;
        	this.cbSymIgn.Text = "Symbol Ignore:";
        	this.cbSymIgn.UseVisualStyleBackColor = true;
        	this.cbSymIgn.CheckedChanged += new System.EventHandler(this.DisEnaOnCheckedChanged);
        	this.cbSymIgn.MouseHover += new System.EventHandler(this.cbSymIgn_MouseHover);
        	// 
        	// cbMoreTries
        	// 
        	this.cbMoreTries.AutoSize = true;
        	this.cbMoreTries.Location = new System.Drawing.Point(10, 102);
        	this.cbMoreTries.Name = "cbMoreTries";
        	this.cbMoreTries.Size = new System.Drawing.Size(79, 17);
        	this.cbMoreTries.TabIndex = 9;
        	this.cbMoreTries.Text = "More Tries:";
        	this.cbMoreTries.UseVisualStyleBackColor = true;
        	this.cbMoreTries.MouseHover += new System.EventHandler(this.cbMoreTries_MouseHover);
        	// 
        	// nudMTCount
        	// 
        	this.nudMTCount.Location = new System.Drawing.Point(127, 99);
        	this.nudMTCount.Name = "nudMTCount";
        	this.nudMTCount.Size = new System.Drawing.Size(141, 20);
        	this.nudMTCount.TabIndex = 10;
        	// 
        	// cbDisplayLang
        	// 
        	this.cbDisplayLang.AutoSize = true;
        	this.cbDisplayLang.Location = new System.Drawing.Point(10, 124);
        	this.cbDisplayLang.Name = "cbDisplayLang";
        	this.cbDisplayLang.Size = new System.Drawing.Size(110, 17);
        	this.cbDisplayLang.TabIndex = 11;
        	this.cbDisplayLang.Text = "Display language:";
        	this.cbDisplayLang.UseVisualStyleBackColor = true;
        	this.cbDisplayLang.CheckedChanged += new System.EventHandler(this.DisEnaOnCheckedChanged);
        	this.cbDisplayLang.MouseHover += new System.EventHandler(this.CbDisplayLangMouseHover);
        	// 
        	// nudRefreshRate
        	// 
        	this.nudRefreshRate.Location = new System.Drawing.Point(214, 124);
        	this.nudRefreshRate.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
        	this.nudRefreshRate.Name = "nudRefreshRate";
        	this.nudRefreshRate.Size = new System.Drawing.Size(54, 20);
        	this.nudRefreshRate.TabIndex = 12;
        	this.nudRefreshRate.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
        	// 
        	// lblRefRate
        	// 
        	this.lblRefRate.AutoSize = true;
        	this.lblRefRate.Location = new System.Drawing.Point(116, 126);
        	this.lblRefRate.Name = "lblRefRate";
        	this.lblRefRate.Size = new System.Drawing.Size(87, 13);
        	this.lblRefRate.TabIndex = 13;
        	this.lblRefRate.Text = "Refresh rate(ms):";
        	this.lblRefRate.MouseHover += new System.EventHandler(this.LblRefRateMouseHover);
        	// 
        	// btCol2
        	// 
        	this.btCol2.Location = new System.Drawing.Point(245, 150);
        	this.btCol2.Name = "btCol2";
        	this.btCol2.Size = new System.Drawing.Size(23, 23);
        	this.btCol2.TabIndex = 14;
        	this.btCol2.UseVisualStyleBackColor = true;
        	this.btCol2.Click += new System.EventHandler(this.BtCol2Click);
        	// 
        	// btCol1
        	// 
        	this.btCol1.Location = new System.Drawing.Point(216, 150);
        	this.btCol1.Name = "btCol1";
        	this.btCol1.Size = new System.Drawing.Size(23, 23);
        	this.btCol1.TabIndex = 15;
        	this.btCol1.UseVisualStyleBackColor = true;
        	this.btCol1.Click += new System.EventHandler(this.BtCol1Click);
        	// 
        	// lbColors
        	// 
        	this.lbColors.AutoSize = true;
        	this.lbColors.Location = new System.Drawing.Point(114, 155);
        	this.lbColors.Name = "lbColors";
        	this.lbColors.Size = new System.Drawing.Size(39, 13);
        	this.lbColors.TabIndex = 16;
        	this.lbColors.Text = "Colors:";
        	this.lbColors.MouseHover += new System.EventHandler(this.LbColorsMouseHover);
        	// 
        	// nudTTWidth
        	// 
        	this.nudTTWidth.Location = new System.Drawing.Point(233, 179);
        	this.nudTTWidth.Name = "nudTTWidth";
        	this.nudTTWidth.Size = new System.Drawing.Size(35, 20);
        	this.nudTTWidth.TabIndex = 17;
        	// 
        	// nudTTHeight
        	// 
        	this.nudTTHeight.Location = new System.Drawing.Point(178, 179);
        	this.nudTTHeight.Name = "nudTTHeight";
        	this.nudTTHeight.Size = new System.Drawing.Size(35, 20);
        	this.nudTTHeight.TabIndex = 18;
        	// 
        	// lbSize
        	// 
        	this.lbSize.AutoSize = true;
        	this.lbSize.Location = new System.Drawing.Point(114, 182);
        	this.lbSize.Name = "lbSize";
        	this.lbSize.Size = new System.Drawing.Size(30, 13);
        	this.lbSize.TabIndex = 19;
        	this.lbSize.Text = "Size:";
        	this.lbSize.MouseHover += new System.EventHandler(this.LbSizeMouseHover);
        	// 
        	// lbH
        	// 
        	this.lbH.AutoSize = true;
        	this.lbH.Location = new System.Drawing.Point(162, 182);
        	this.lbH.Name = "lbH";
        	this.lbH.Size = new System.Drawing.Size(18, 13);
        	this.lbH.TabIndex = 20;
        	this.lbH.Text = "H:";
        	// 
        	// lbW
        	// 
        	this.lbW.AutoSize = true;
        	this.lbW.Location = new System.Drawing.Point(214, 182);
        	this.lbW.Name = "lbW";
        	this.lbW.Size = new System.Drawing.Size(21, 13);
        	this.lbW.TabIndex = 21;
        	this.lbW.Text = "W:";
        	// 
        	// btFont
        	// 
        	this.btFont.Location = new System.Drawing.Point(162, 150);
        	this.btFont.Name = "btFont";
        	this.btFont.Size = new System.Drawing.Size(49, 23);
        	this.btFont.TabIndex = 22;
        	this.btFont.Text = "Font";
        	this.btFont.UseVisualStyleBackColor = true;
        	this.btFont.Click += new System.EventHandler(this.BtFontClick);
        	// 
        	// nudYpos
        	// 
        	this.nudYpos.Location = new System.Drawing.Point(233, 205);
        	this.nudYpos.Name = "nudYpos";
        	this.nudYpos.Size = new System.Drawing.Size(35, 20);
        	this.nudYpos.TabIndex = 23;
        	// 
        	// lbY
        	// 
        	this.lbY.AutoSize = true;
        	this.lbY.Location = new System.Drawing.Point(214, 208);
        	this.lbY.Name = "lbY";
        	this.lbY.Size = new System.Drawing.Size(17, 13);
        	this.lbY.TabIndex = 27;
        	this.lbY.Text = "Y:";
        	// 
        	// nudXpos
        	// 
        	this.nudXpos.Location = new System.Drawing.Point(178, 205);
        	this.nudXpos.Name = "nudXpos";
        	this.nudXpos.Size = new System.Drawing.Size(35, 20);
        	this.nudXpos.TabIndex = 24;
        	// 
        	// lbX
        	// 
        	this.lbX.AutoSize = true;
        	this.lbX.Location = new System.Drawing.Point(162, 208);
        	this.lbX.Name = "lbX";
        	this.lbX.Size = new System.Drawing.Size(17, 13);
        	this.lbX.TabIndex = 26;
        	this.lbX.Text = "X:";
        	// 
        	// lbPosition
        	// 
        	this.lbPosition.AutoSize = true;
        	this.lbPosition.Location = new System.Drawing.Point(114, 208);
        	this.lbPosition.Name = "lbPosition";
        	this.lbPosition.Size = new System.Drawing.Size(47, 13);
        	this.lbPosition.TabIndex = 25;
        	this.lbPosition.Text = "Position:";
        	this.lbPosition.MouseHover += new System.EventHandler(this.LbPositionMouseHover);
        	// 
        	// pEMore
        	// 
        	this.pEMore.AutoSize = true;
        	this.pEMore.Controls.Add(this.cbUseSnippets);
        	this.pEMore.Controls.Add(this.tbSnippets);
        	this.pEMore.Controls.Add(this.cbTrBLT);
        	this.pEMore.Controls.Add(this.cbExCSSwitch);
        	this.pEMore.Controls.Add(this.lbDDelay);
        	this.pEMore.Controls.Add(this.nudDoubleDelay);
        	this.pEMore.Controls.Add(this.cbDoublePress);
        	this.pEMore.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.pEMore.Location = new System.Drawing.Point(0, 0);
        	this.pEMore.Name = "pEMore";
        	this.pEMore.Size = new System.Drawing.Size(279, 268);
        	this.pEMore.TabIndex = 28;
        	this.pEMore.Visible = false;
        	// 
        	// cbUseSnippets
        	// 
        	this.cbUseSnippets.AutoSize = true;
        	this.cbUseSnippets.Location = new System.Drawing.Point(10, 81);
        	this.cbUseSnippets.Name = "cbUseSnippets";
        	this.cbUseSnippets.Size = new System.Drawing.Size(87, 17);
        	this.cbUseSnippets.TabIndex = 34;
        	this.cbUseSnippets.Text = "Use snippets";
        	this.cbUseSnippets.UseVisualStyleBackColor = true;
        	this.cbUseSnippets.CheckedChanged += new System.EventHandler(this.DisEnaOnCheckedChanged);
        	this.cbUseSnippets.MouseHover += new System.EventHandler(this.CbUseSnippetsMouseHover);
        	// 
        	// tbSnippets
        	// 
        	this.tbSnippets.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.tbSnippets.Location = new System.Drawing.Point(10, 104);
        	this.tbSnippets.Multiline = true;
        	this.tbSnippets.Name = "tbSnippets";
        	this.tbSnippets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        	this.tbSnippets.Size = new System.Drawing.Size(258, 121);
        	this.tbSnippets.TabIndex = 33;
        	this.tbSnippets.Text = "->mahou\r\n====>\r\nMahou (魔法) - Magical layout switcher.\r\n<====\r\n->eml\r\n====>BladeMi" +
	"ght@gmail.com<====\r\n";
        	this.tbSnippets.WordWrap = false;
        	// 
        	// cbTrBLT
        	// 
        	this.cbTrBLT.AutoSize = true;
        	this.cbTrBLT.Location = new System.Drawing.Point(10, 58);
        	this.cbTrBLT.Name = "cbTrBLT";
        	this.cbTrBLT.Size = new System.Drawing.Size(232, 17);
        	this.cbTrBLT.TabIndex = 32;
        	this.cbTrBLT.Text = "Transparent background in language tooltip";
        	this.cbTrBLT.UseVisualStyleBackColor = true;
        	this.cbTrBLT.MouseHover += new System.EventHandler(this.CbTrBLTMouseHover);
        	// 
        	// cbExCSSwitch
        	// 
        	this.cbExCSSwitch.AutoSize = true;
        	this.cbExCSSwitch.Location = new System.Drawing.Point(10, 35);
        	this.cbExCSSwitch.Name = "cbExCSSwitch";
        	this.cbExCSSwitch.Size = new System.Drawing.Size(144, 17);
        	this.cbExCSSwitch.TabIndex = 31;
        	this.cbExCSSwitch.Text = "Experimental CS-Switch+";
        	this.cbExCSSwitch.UseVisualStyleBackColor = true;
        	this.cbExCSSwitch.MouseHover += new System.EventHandler(this.CbExCSSwitchMouseHover);
        	// 
        	// lbDDelay
        	// 
        	this.lbDDelay.AutoSize = true;
        	this.lbDDelay.Location = new System.Drawing.Point(164, 14);
        	this.lbDDelay.Name = "lbDDelay";
        	this.lbDDelay.Size = new System.Drawing.Size(37, 13);
        	this.lbDDelay.TabIndex = 30;
        	this.lbDDelay.Text = "Delay:";
        	this.lbDDelay.MouseHover += new System.EventHandler(this.LbDDelayMouseHover);
        	// 
        	// nudDoubleDelay
        	// 
        	this.nudDoubleDelay.Increment = new decimal(new int[] {
			5,
			0,
			0,
			0});
        	this.nudDoubleDelay.Location = new System.Drawing.Point(226, 11);
        	this.nudDoubleDelay.Maximum = new decimal(new int[] {
			5000,
			0,
			0,
			0});
        	this.nudDoubleDelay.Name = "nudDoubleDelay";
        	this.nudDoubleDelay.Size = new System.Drawing.Size(41, 20);
        	this.nudDoubleDelay.TabIndex = 2;
        	// 
        	// cbDoublePress
        	// 
        	this.cbDoublePress.AutoSize = true;
        	this.cbDoublePress.Location = new System.Drawing.Point(10, 12);
        	this.cbDoublePress.Name = "cbDoublePress";
        	this.cbDoublePress.Size = new System.Drawing.Size(103, 17);
        	this.cbDoublePress.TabIndex = 0;
        	this.cbDoublePress.Text = "Double hotkeys:";
        	this.cbDoublePress.UseVisualStyleBackColor = true;
        	this.cbDoublePress.CheckedChanged += new System.EventHandler(this.DisEnaOnCheckedChanged);
        	this.cbDoublePress.MouseHover += new System.EventHandler(this.CbDoublePressMouseHover);
        	// 
        	// btnEMore
        	// 
        	this.btnEMore.Location = new System.Drawing.Point(101, 234);
        	this.btnEMore.Name = "btnEMore";
        	this.btnEMore.Size = new System.Drawing.Size(76, 23);
        	this.btnEMore.TabIndex = 29;
        	this.btnEMore.Text = "More";
        	this.btnEMore.UseVisualStyleBackColor = true;
        	this.btnEMore.Click += new System.EventHandler(this.BtnEMoreClick);
        	// 
        	// cbOnChange
        	// 
        	this.cbOnChange.AutoSize = true;
        	this.cbOnChange.Location = new System.Drawing.Point(10, 148);
        	this.cbOnChange.Name = "cbOnChange";
        	this.cbOnChange.Size = new System.Drawing.Size(79, 17);
        	this.cbOnChange.TabIndex = 30;
        	this.cbOnChange.Text = "On change";
        	this.cbOnChange.UseVisualStyleBackColor = true;
        	this.cbOnChange.MouseHover += new System.EventHandler(this.CbOnChangeMouseHover);
        	// 
        	// pEExtra
        	// 
        	this.pEExtra.Controls.Add(this.cbScrollLight);
        	this.pEExtra.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.pEExtra.Location = new System.Drawing.Point(0, 0);
        	this.pEExtra.Name = "pEExtra";
        	this.pEExtra.Size = new System.Drawing.Size(279, 268);
        	this.pEExtra.TabIndex = 31;
        	this.pEExtra.Visible = false;
        	// 
        	// cbScrollLight
        	// 
        	this.cbScrollLight.AutoSize = true;
        	this.cbScrollLight.Location = new System.Drawing.Point(10, 12);
        	this.cbScrollLight.Name = "cbScrollLight";
        	this.cbScrollLight.Size = new System.Drawing.Size(250, 17);
        	this.cbScrollLight.TabIndex = 0;
        	this.cbScrollLight.Text = "Highlight ScrollLock when language 1 is active.";
        	this.cbScrollLight.UseVisualStyleBackColor = true;
        	this.cbScrollLight.MouseHover += new System.EventHandler(this.CbScrollLightMouseHover);
        	// 
        	// MoreConfigs
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(279, 268);
        	this.Controls.Add(this.btnEMore);
        	this.Controls.Add(this.btnOK);
        	this.Controls.Add(this.btnNO);
        	this.Controls.Add(this.pEExtra);
        	this.Controls.Add(this.pEMore);
        	this.Controls.Add(this.nudYpos);
        	this.Controls.Add(this.lbY);
        	this.Controls.Add(this.nudXpos);
        	this.Controls.Add(this.lbX);
        	this.Controls.Add(this.lbPosition);
        	this.Controls.Add(this.lblRefRate);
        	this.Controls.Add(this.cbDisplayLang);
        	this.Controls.Add(this.btFont);
        	this.Controls.Add(this.nudTTWidth);
        	this.Controls.Add(this.lbW);
        	this.Controls.Add(this.nudTTHeight);
        	this.Controls.Add(this.lbH);
        	this.Controls.Add(this.lbSize);
        	this.Controls.Add(this.lbColors);
        	this.Controls.Add(this.btCol1);
        	this.Controls.Add(this.btCol2);
        	this.Controls.Add(this.nudRefreshRate);
        	this.Controls.Add(this.nudMTCount);
        	this.Controls.Add(this.cbMoreTries);
        	this.Controls.Add(this.cbSymIgn);
        	this.Controls.Add(this.tbHKSymIgn);
        	this.Controls.Add(this.cbRCLocalesList);
        	this.Controls.Add(this.cbLCLocalesList);
        	this.Controls.Add(this.lbRCto);
        	this.Controls.Add(this.lbLCto);
        	this.Controls.Add(this.cbUseLRC);
        	this.Controls.Add(this.cbOnChange);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "MoreConfigs";
        	this.ShowIcon = false;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "More configs";
        	this.Activated += new System.EventHandler(this.MoreConfigs_Activated);
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoreConfigs_FormClosing);
        	this.Load += new System.EventHandler(this.MoreConfigs_Load);
        	((System.ComponentModel.ISupportInitialize)(this.nudMTCount)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudRefreshRate)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudTTWidth)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudTTHeight)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudYpos)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.nudXpos)).EndInit();
        	this.pEMore.ResumeLayout(false);
        	this.pEMore.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.nudDoubleDelay)).EndInit();
        	this.pEExtra.ResumeLayout(false);
        	this.pEExtra.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.CheckBox cbUseLRC;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnNO;
        private System.Windows.Forms.Label lbLCto;
        private System.Windows.Forms.Label lbRCto;
        private System.Windows.Forms.ComboBox cbLCLocalesList;
        private System.Windows.Forms.ComboBox cbRCLocalesList;
        private System.Windows.Forms.ToolTip HelpTT;
        private System.Windows.Forms.TextBox tbHKSymIgn;
        private System.Windows.Forms.CheckBox cbSymIgn;
        private System.Windows.Forms.CheckBox cbMoreTries;
        private System.Windows.Forms.NumericUpDown nudMTCount;
        private System.Windows.Forms.CheckBox cbDisplayLang;
        private System.Windows.Forms.NumericUpDown nudRefreshRate;
        private System.Windows.Forms.Label lblRefRate;
        private System.Windows.Forms.Button btCol2;
        private System.Windows.Forms.Button btCol1;
        private System.Windows.Forms.Label lbColors;
        private System.Windows.Forms.NumericUpDown nudTTWidth;
        private System.Windows.Forms.NumericUpDown nudTTHeight;
        private System.Windows.Forms.Label lbSize;
        private System.Windows.Forms.Label lbH;
        private System.Windows.Forms.Label lbW;
        private System.Windows.Forms.Button btFont;
        private System.Windows.Forms.NumericUpDown nudYpos;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.NumericUpDown nudXpos;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Label lbPosition;
        private System.Windows.Forms.Panel pEMore;
        private System.Windows.Forms.Button btnEMore;
        private System.Windows.Forms.NumericUpDown nudDoubleDelay;
        private System.Windows.Forms.CheckBox cbDoublePress;
        private System.Windows.Forms.Label lbDDelay;
        private System.Windows.Forms.CheckBox cbExCSSwitch;
        private System.Windows.Forms.CheckBox cbTrBLT;
        public System.Windows.Forms.TextBox tbSnippets;
        private System.Windows.Forms.CheckBox cbUseSnippets;
        private System.Windows.Forms.CheckBox cbOnChange;
        private System.Windows.Forms.Panel pEExtra;
        private System.Windows.Forms.CheckBox cbScrollLight;
    }
}