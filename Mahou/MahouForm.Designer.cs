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
            this.label3 = new System.Windows.Forms.Label();
            this.TrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.GitHubLink = new System.Windows.Forms.LinkLabel();
            this.cbAutorun = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOK.Location = new System.Drawing.Point(238, 88);
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
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnApply.Location = new System.Drawing.Point(319, 88);
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
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(400, 88);
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
            this.lbl1lng.Location = new System.Drawing.Point(260, 35);
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
            this.lbl2lng.Location = new System.Drawing.Point(260, 61);
            this.lbl2lng.Name = "lbl2lng";
            this.lbl2lng.Size = new System.Drawing.Size(70, 13);
            this.lbl2lng.TabIndex = 4;
            this.lbl2lng.Text = "Language 2:";
            // 
            // cbLangOne
            // 
            this.cbLangOne.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLangOne.FormattingEnabled = true;
            this.cbLangOne.Location = new System.Drawing.Point(338, 32);
            this.cbLangOne.Name = "cbLangOne";
            this.cbLangOne.Size = new System.Drawing.Size(151, 21);
            this.cbLangOne.TabIndex = 5;
            this.cbLangOne.SelectedIndexChanged += new System.EventHandler(this.cbLangOne_SelectedIndexChanged);
            // 
            // cbLangTwo
            // 
            this.cbLangTwo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLangTwo.FormattingEnabled = true;
            this.cbLangTwo.Location = new System.Drawing.Point(338, 58);
            this.cbLangTwo.Name = "cbLangTwo";
            this.cbLangTwo.Size = new System.Drawing.Size(151, 21);
            this.cbLangTwo.TabIndex = 6;
            this.cbLangTwo.SelectedIndexChanged += new System.EventHandler(this.cbLangTwo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Convert last word hotkey:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Convert selection hotkey:";
            // 
            // tbCLHK
            // 
            this.tbCLHK.Enabled = false;
            this.tbCLHK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCLHK.Location = new System.Drawing.Point(154, 32);
            this.tbCLHK.Name = "tbCLHK";
            this.tbCLHK.Size = new System.Drawing.Size(100, 22);
            this.tbCLHK.TabIndex = 11;
            this.tbCLHK.Text = "Pause";
            // 
            // tbCSHK
            // 
            this.tbCSHK.Enabled = false;
            this.tbCSHK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCSHK.Location = new System.Drawing.Point(154, 58);
            this.tbCSHK.Name = "tbCSHK";
            this.tbCSHK.Size = new System.Drawing.Size(100, 22);
            this.tbCSHK.TabIndex = 12;
            this.tbCSHK.Text = "Scroll";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(285, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Switch beteven languages:";
            // 
            // TrayIconCheckBox
            // 
            this.TrayIconCheckBox.AutoSize = true;
            this.TrayIconCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.TrayIconCheckBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TrayIconCheckBox.Location = new System.Drawing.Point(12, 88);
            this.TrayIconCheckBox.Name = "TrayIconCheckBox";
            this.TrayIconCheckBox.Size = new System.Drawing.Size(110, 17);
            this.TrayIconCheckBox.TabIndex = 14;
            this.TrayIconCheckBox.Text = "Tray icon visible?";
            this.TrayIconCheckBox.UseVisualStyleBackColor = false;
            this.TrayIconCheckBox.CheckedChanged += new System.EventHandler(this.TrayIconCheckBox_CheckedChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(157, 88);
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
            this.GitHubLink.Location = new System.Drawing.Point(12, 9);
            this.GitHubLink.Name = "GitHubLink";
            this.GitHubLink.Size = new System.Drawing.Size(89, 13);
            this.GitHubLink.TabIndex = 16;
            this.GitHubLink.TabStop = true;
            this.GitHubLink.Text = "View on GitHub";
            this.GitHubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GitHubLink_LinkClicked);
            // 
            // cbAutorun
            // 
            this.cbAutorun.AutoSize = true;
            this.cbAutorun.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbAutorun.Location = new System.Drawing.Point(127, 8);
            this.cbAutorun.Name = "cbAutorun";
            this.cbAutorun.Size = new System.Drawing.Size(152, 17);
            this.cbAutorun.TabIndex = 17;
            this.cbAutorun.Text = "Autostart with Windows";
            this.cbAutorun.UseVisualStyleBackColor = true;
            this.cbAutorun.CheckedChanged += new System.EventHandler(this.cbAutorun_CheckedChanged);
            // 
            // MahouForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 120);
            this.Controls.Add(this.cbAutorun);
            this.Controls.Add(this.GitHubLink);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.TrayIconCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbCSHK);
            this.Controls.Add(this.tbCLHK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLangTwo);
            this.Controls.Add(this.cbLangOne);
            this.Controls.Add(this.lbl2lng);
            this.Controls.Add(this.lbl1lng);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Mahou.Properties.Resources.Mahou;
            this.Name = "MahouForm";
            this.Text = "Mahou";
            this.Activated += new System.EventHandler(this.MahouForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MahouForm_FormClosing);
            this.Load += new System.EventHandler(this.MahouForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MahouForm_VisibleChanged);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox TrayIconCheckBox;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.LinkLabel GitHubLink;
        private System.Windows.Forms.CheckBox cbAutorun;
    }
}