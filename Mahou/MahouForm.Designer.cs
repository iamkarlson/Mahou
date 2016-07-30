namespace Mahou
{
    partial class MahouForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbl1lng = new System.Windows.Forms.Label();
            this.lbl2lng = new System.Windows.Forms.Label();
            this.cbLangOne = new System.Windows.Forms.ComboBox();
            this.cbLangTwo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCLHK = new System.Windows.Forms.TextBox();
            this.tbCSHK = new System.Windows.Forms.TextBox();
            this.TrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.GitHubLink = new System.Windows.Forms.LinkLabel();
            this.cbAutorun = new System.Windows.Forms.CheckBox();
            this.cbSpaceBreak = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCLineHK = new System.Windows.Forms.TextBox();
            this.cbCapsLayoutSwitch = new System.Windows.Forms.CheckBox();
            this.HelpTT = new System.Windows.Forms.ToolTip(this.components);
            this.cbCycleMode = new System.Windows.Forms.CheckBox();
            this.btnUpd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOK.Location = new System.Drawing.Point(87, 222);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnApply.Location = new System.Drawing.Point(6, 222);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(168, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbl1lng
            // 
            this.lbl1lng.AutoSize = true;
            this.lbl1lng.BackColor = System.Drawing.Color.Transparent;
            this.lbl1lng.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl1lng.Location = new System.Drawing.Point(5, 24);
            this.lbl1lng.Name = "lbl1lng";
            this.lbl1lng.Size = new System.Drawing.Size(70, 13);
            this.lbl1lng.TabIndex = 3;
            this.lbl1lng.Text = "Language 1:";
            // 
            // lbl2lng
            // 
            this.lbl2lng.AutoSize = true;
            this.lbl2lng.BackColor = System.Drawing.Color.Transparent;
            this.lbl2lng.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl2lng.Location = new System.Drawing.Point(5, 54);
            this.lbl2lng.Name = "lbl2lng";
            this.lbl2lng.Size = new System.Drawing.Size(70, 13);
            this.lbl2lng.TabIndex = 4;
            this.lbl2lng.Text = "Language 2:";
            // 
            // cbLangOne
            // 
            this.cbLangOne.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLangOne.FormattingEnabled = true;
            this.cbLangOne.Location = new System.Drawing.Point(81, 23);
            this.cbLangOne.Name = "cbLangOne";
            this.cbLangOne.Size = new System.Drawing.Size(102, 21);
            this.cbLangOne.TabIndex = 5;
            this.cbLangOne.SelectedIndexChanged += new System.EventHandler(this.cbLangOne_SelectedIndexChanged);
            // 
            // cbLangTwo
            // 
            this.cbLangTwo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLangTwo.FormattingEnabled = true;
            this.cbLangTwo.Location = new System.Drawing.Point(81, 50);
            this.cbLangTwo.Name = "cbLangTwo";
            this.cbLangTwo.Size = new System.Drawing.Size(102, 21);
            this.cbLangTwo.TabIndex = 6;
            this.cbLangTwo.SelectedIndexChanged += new System.EventHandler(this.cbLangTwo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Convert Last Word:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Convert Selection:";
            // 
            // tbCLHK
            // 
            this.tbCLHK.BackColor = System.Drawing.Color.White;
            this.tbCLHK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCLHK.Location = new System.Drawing.Point(112, 13);
            this.tbCLHK.Name = "tbCLHK";
            this.tbCLHK.ReadOnly = true;
            this.tbCLHK.Size = new System.Drawing.Size(201, 22);
            this.tbCLHK.TabIndex = 11;
            this.tbCLHK.Text = "Pause";
            this.tbCLHK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCLHK_KeyDown);
            this.tbCLHK.MouseHover += new System.EventHandler(this.tbCLHK_MouseHover);
            // 
            // tbCSHK
            // 
            this.tbCSHK.BackColor = System.Drawing.Color.White;
            this.tbCSHK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCSHK.Location = new System.Drawing.Point(112, 38);
            this.tbCSHK.Name = "tbCSHK";
            this.tbCSHK.ReadOnly = true;
            this.tbCSHK.Size = new System.Drawing.Size(201, 22);
            this.tbCSHK.TabIndex = 12;
            this.tbCSHK.Text = "Scroll";
            this.tbCSHK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCSHK_KeyDown);
            this.tbCSHK.MouseHover += new System.EventHandler(this.tbCSHK_MouseHover);
            // 
            // TrayIconCheckBox
            // 
            this.TrayIconCheckBox.AutoSize = true;
            this.TrayIconCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.TrayIconCheckBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TrayIconCheckBox.Location = new System.Drawing.Point(201, 177);
            this.TrayIconCheckBox.Name = "TrayIconCheckBox";
            this.TrayIconCheckBox.Size = new System.Drawing.Size(102, 17);
            this.TrayIconCheckBox.TabIndex = 14;
            this.TrayIconCheckBox.Text = "Show tray icon";
            this.TrayIconCheckBox.UseVisualStyleBackColor = false;
            this.TrayIconCheckBox.CheckedChanged += new System.EventHandler(this.TrayIconCheckBox_CheckedChanged);
            this.TrayIconCheckBox.MouseHover += new System.EventHandler(this.TrayIconCheckBox_MouseHover);
            // 
            // btnHelp
            // 
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnHelp.Location = new System.Drawing.Point(249, 222);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 15;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // GitHubLink
            // 
            this.GitHubLink.AutoSize = true;
            this.GitHubLink.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GitHubLink.Location = new System.Drawing.Point(11, 9);
            this.GitHubLink.Name = "GitHubLink";
            this.GitHubLink.Size = new System.Drawing.Size(89, 13);
            this.GitHubLink.TabIndex = 16;
            this.GitHubLink.TabStop = true;
            this.GitHubLink.Text = "View on GitHub";
            this.GitHubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GitHubLink_LinkClicked);
            this.GitHubLink.MouseHover += new System.EventHandler(this.GitHubLink_MouseHover);
            // 
            // cbAutorun
            // 
            this.cbAutorun.AutoSize = true;
            this.cbAutorun.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbAutorun.Location = new System.Drawing.Point(117, 9);
            this.cbAutorun.Name = "cbAutorun";
            this.cbAutorun.Size = new System.Drawing.Size(152, 17);
            this.cbAutorun.TabIndex = 17;
            this.cbAutorun.Text = "Autostart with Windows";
            this.cbAutorun.UseVisualStyleBackColor = true;
            this.cbAutorun.CheckedChanged += new System.EventHandler(this.cbAutorun_CheckedChanged);
            // 
            // cbSpaceBreak
            // 
            this.cbSpaceBreak.AutoSize = true;
            this.cbSpaceBreak.Enabled = false;
            this.cbSpaceBreak.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSpaceBreak.Location = new System.Drawing.Point(201, 157);
            this.cbSpaceBreak.Name = "cbSpaceBreak";
            this.cbSpaceBreak.Size = new System.Drawing.Size(124, 17);
            this.cbSpaceBreak.TabIndex = 18;
            this.cbSpaceBreak.Text = "Space begins word";
            this.cbSpaceBreak.UseVisualStyleBackColor = true;
            this.cbSpaceBreak.CheckedChanged += new System.EventHandler(this.cbSpaceBreak_CheckedChanged);
            this.cbSpaceBreak.MouseHover += new System.EventHandler(this.cbSpaceBreak_MouseHover);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl1lng);
            this.groupBox1.Controls.Add(this.lbl2lng);
            this.groupBox1.Controls.Add(this.cbLangOne);
            this.groupBox1.Controls.Add(this.cbLangTwo);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(6, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 82);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Switch between layouts";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbCLineHK);
            this.groupBox2.Controls.Add(this.tbCLHK);
            this.groupBox2.Controls.Add(this.tbCSHK);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(6, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 93);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HotKeys";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Convert Line:";
            // 
            // tbCLineHK
            // 
            this.tbCLineHK.BackColor = System.Drawing.Color.White;
            this.tbCLineHK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCLineHK.Location = new System.Drawing.Point(112, 63);
            this.tbCLineHK.Name = "tbCLineHK";
            this.tbCLineHK.ReadOnly = true;
            this.tbCLineHK.Size = new System.Drawing.Size(201, 22);
            this.tbCLineHK.TabIndex = 14;
            this.tbCLineHK.Text = "Control + Pause";
            this.tbCLineHK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCLineHK_KeyDown);
            // 
            // cbCapsLayoutSwitch
            // 
            this.cbCapsLayoutSwitch.AutoSize = true;
            this.cbCapsLayoutSwitch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCapsLayoutSwitch.Location = new System.Drawing.Point(201, 137);
            this.cbCapsLayoutSwitch.Name = "cbCapsLayoutSwitch";
            this.cbCapsLayoutSwitch.Size = new System.Drawing.Size(133, 17);
            this.cbCapsLayoutSwitch.TabIndex = 21;
            this.cbCapsLayoutSwitch.Text = "Caps switches layout";
            this.cbCapsLayoutSwitch.UseVisualStyleBackColor = true;
            this.cbCapsLayoutSwitch.CheckedChanged += new System.EventHandler(this.cbCapsLayoutSwitch_CheckedChanged);
            this.cbCapsLayoutSwitch.MouseHover += new System.EventHandler(this.cbCapsLayoutSwitch_MouseHover);
            // 
            // HelpTT
            // 
            this.HelpTT.AutomaticDelay = 0;
            this.HelpTT.AutoPopDelay = 0;
            this.HelpTT.BackColor = System.Drawing.SystemColors.Highlight;
            this.HelpTT.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.HelpTT.InitialDelay = 0;
            this.HelpTT.ReshowDelay = 1;
            this.HelpTT.ShowAlways = true;
            // 
            // cbCycleMode
            // 
            this.cbCycleMode.AutoSize = true;
            this.cbCycleMode.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCycleMode.Location = new System.Drawing.Point(201, 197);
            this.cbCycleMode.Name = "cbCycleMode";
            this.cbCycleMode.Size = new System.Drawing.Size(120, 17);
            this.cbCycleMode.TabIndex = 22;
            this.cbCycleMode.Text = "Enable cycle mode";
            this.cbCycleMode.UseVisualStyleBackColor = true;
            this.cbCycleMode.CheckedChanged += new System.EventHandler(this.cbCycleMode_CheckedChanged);
            this.cbCycleMode.MouseHover += new System.EventHandler(this.cbCycleMode_MouseHover);
            // 
            // btnUpd
            // 
            this.btnUpd.Location = new System.Drawing.Point(265, 10);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Size = new System.Drawing.Size(59, 23);
            this.btnUpd.TabIndex = 23;
            this.btnUpd.Text = "Update";
            this.btnUpd.UseVisualStyleBackColor = true;
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // MahouForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 251);
            this.Controls.Add(this.btnUpd);
            this.Controls.Add(this.cbCycleMode);
            this.Controls.Add(this.cbCapsLayoutSwitch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbSpaceBreak);
            this.Controls.Add(this.cbAutorun);
            this.Controls.Add(this.GitHubLink);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.TrayIconCheckBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Mahou.Properties.Resources.Mahou;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MahouForm";
            this.Text = "Mahou";
            this.Activated += new System.EventHandler(this.MahouForm_Activated);
            this.Deactivate += new System.EventHandler(this.MahouForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MahouForm_FormClosing);
            this.Load += new System.EventHandler(this.MahouForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MahouForm_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbl1lng;
        private System.Windows.Forms.Label lbl2lng;
        private System.Windows.Forms.ComboBox cbLangOne;
        private System.Windows.Forms.ComboBox cbLangTwo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCLHK;
        private System.Windows.Forms.TextBox tbCSHK;
        private System.Windows.Forms.CheckBox TrayIconCheckBox;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.LinkLabel GitHubLink;
        private System.Windows.Forms.CheckBox cbAutorun;
        private System.Windows.Forms.CheckBox cbSpaceBreak;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbCapsLayoutSwitch;
        private System.Windows.Forms.ToolTip HelpTT;
        private System.Windows.Forms.CheckBox cbCycleMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCLineHK;
        private System.Windows.Forms.Button btnUpd;
    }
}