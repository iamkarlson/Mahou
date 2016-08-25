namespace Mahou
{
    partial class Update
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
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.lbVer = new System.Windows.Forms.Label();
            this.gpRTitle = new System.Windows.Forms.GroupBox();
            this.lbRDesc = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btDMahou = new System.Windows.Forms.Button();
            this.lbDownloading = new System.Windows.Forms.Label();
            this.gpRTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbStatus
            // 
            this.pbStatus.Enabled = false;
            this.pbStatus.Location = new System.Drawing.Point(12, 226);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(260, 23);
            this.pbStatus.TabIndex = 0;
            // 
            // lbVer
            // 
            this.lbVer.Location = new System.Drawing.Point(12, 36);
            this.lbVer.Name = "lbVer";
            this.lbVer.Size = new System.Drawing.Size(247, 13);
            this.lbVer.TabIndex = 1;
            this.lbVer.Text = "Release Version: ";
            // 
            // gpRTitle
            // 
            this.gpRTitle.Controls.Add(this.lbRDesc);
            this.gpRTitle.Location = new System.Drawing.Point(12, 52);
            this.gpRTitle.Name = "gpRTitle";
            this.gpRTitle.Size = new System.Drawing.Size(260, 140);
            this.gpRTitle.TabIndex = 2;
            this.gpRTitle.TabStop = false;
            this.gpRTitle.Text = "Release Title";
            // 
            // lbRDesc
            // 
            this.lbRDesc.BackColor = System.Drawing.SystemColors.Control;
            this.lbRDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbRDesc.HideSelection = false;
            this.lbRDesc.Location = new System.Drawing.Point(7, 21);
            this.lbRDesc.Multiline = true;
            this.lbRDesc.Name = "lbRDesc";
            this.lbRDesc.ReadOnly = true;
            this.lbRDesc.Size = new System.Drawing.Size(247, 113);
            this.lbRDesc.TabIndex = 0;
            this.lbRDesc.Text = "Release Description";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(12, 10);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(260, 23);
            this.btnCheck.TabIndex = 3;
            this.btnCheck.Text = "Check for Updates";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btDMahou
            // 
            this.btDMahou.Enabled = false;
            this.btDMahou.Location = new System.Drawing.Point(12, 197);
            this.btDMahou.Name = "btDMahou";
            this.btDMahou.Size = new System.Drawing.Size(260, 23);
            this.btDMahou.TabIndex = 4;
            this.btDMahou.Text = "Update Mahou";
            this.btDMahou.UseVisualStyleBackColor = true;
            this.btDMahou.Click += new System.EventHandler(this.btDMahou_Click);
            // 
            // lbDownloading
            // 
            this.lbDownloading.Location = new System.Drawing.Point(12, 202);
            this.lbDownloading.Name = "lbDownloading";
            this.lbDownloading.Size = new System.Drawing.Size(260, 13);
            this.lbDownloading.TabIndex = 5;
            this.lbDownloading.Text = "Downloading...";
            this.lbDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDownloading.Visible = false;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbDownloading);
            this.Controls.Add(this.btDMahou);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.gpRTitle);
            this.Controls.Add(this.lbVer);
            this.Controls.Add(this.pbStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mahou Update";
            this.gpRTitle.ResumeLayout(false);
            this.gpRTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Label lbVer;
        private System.Windows.Forms.GroupBox gpRTitle;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btDMahou;
        private System.Windows.Forms.TextBox lbRDesc;
        private System.Windows.Forms.Label lbDownloading;
    }
}