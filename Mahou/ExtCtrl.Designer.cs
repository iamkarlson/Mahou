namespace Mahou
{
    partial class ExtCtrls
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
            this.cbUseLRC = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnNO = new System.Windows.Forms.Button();
            this.lbLCto = new System.Windows.Forms.Label();
            this.lbRCto = new System.Windows.Forms.Label();
            this.cbLCLocalesList = new System.Windows.Forms.ComboBox();
            this.cbRCLocalesList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbUseLRC
            // 
            this.cbUseLRC.AutoSize = true;
            this.cbUseLRC.Location = new System.Drawing.Point(12, 12);
            this.cbUseLRC.Name = "cbUseLRC";
            this.cbUseLRC.Size = new System.Drawing.Size(265, 17);
            this.cbUseLRC.TabIndex = 0;
            this.cbUseLRC.Text = "Use specific layout changing by Left/Right CTRLS";
            this.cbUseLRC.UseVisualStyleBackColor = true;
            this.cbUseLRC.CheckedChanged += new System.EventHandler(this.cbUseLRC_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 79);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnNO
            // 
            this.btnNO.Location = new System.Drawing.Point(200, 79);
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
            this.lbLCto.Location = new System.Drawing.Point(12, 32);
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
            this.cbLCLocalesList.Location = new System.Drawing.Point(15, 48);
            this.cbLCLocalesList.Name = "cbLCLocalesList";
            this.cbLCLocalesList.Size = new System.Drawing.Size(117, 21);
            this.cbLCLocalesList.TabIndex = 5;
            // 
            // cbRCLocalesList
            // 
            this.cbRCLocalesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRCLocalesList.FormattingEnabled = true;
            this.cbRCLocalesList.Location = new System.Drawing.Point(151, 48);
            this.cbRCLocalesList.Name = "cbRCLocalesList";
            this.cbRCLocalesList.Size = new System.Drawing.Size(117, 21);
            this.cbRCLocalesList.TabIndex = 6;
            // 
            // ExtCtrls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 114);
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
            this.Name = "ExtCtrls";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extended CTRLs config";
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
    }
}