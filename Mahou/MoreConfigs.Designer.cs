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
            this.cbUseLRC.CheckedChanged += new System.EventHandler(this.cbUseLRC_CheckedChanged);
            this.cbUseLRC.MouseHover += new System.EventHandler(this.cbUseLRC_MouseHover);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(10, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnNO
            // 
            this.btnNO.Location = new System.Drawing.Point(192, 110);
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
            this.cbSymIgn.MouseHover += new System.EventHandler(this.cbSymIgn_MouseHover);
            // 
            // MoreConfigs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 144);
            this.Controls.Add(this.cbSymIgn);
            this.Controls.Add(this.tbHKSymIgn);
            this.Controls.Add(this.cbRCLocalesList);
            this.Controls.Add(this.cbLCLocalesList);
            this.Controls.Add(this.lbRCto);
            this.Controls.Add(this.lbLCto);
            this.Controls.Add(this.btnNO);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbUseLRC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoreConfigs";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "More configs";
            this.Activated += new System.EventHandler(this.MoreConfigs_Activated);
            this.Deactivate += new System.EventHandler(this.MoreConfigs_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtCtrls_FormClosing);
            this.Load += new System.EventHandler(this.ExtCtrl_Load);
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
    }
}