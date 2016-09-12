namespace Mahou
{
	partial class LangDisplay
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lbLang;
		
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.lbLang = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbLang
			// 
			this.lbLang.BackColor = System.Drawing.Color.Black;
			this.lbLang.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbLang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbLang.ForeColor = System.Drawing.Color.White;
			this.lbLang.Location = new System.Drawing.Point(0, 0);
			this.lbLang.Name = "lbLang";
			this.lbLang.Size = new System.Drawing.Size(20, 15);
			this.lbLang.TabIndex = 0;
			this.lbLang.Text = "RU";
			// 
			// LangDisplay
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Pink;
			this.ClientSize = new System.Drawing.Size(121, 45);
			this.ControlBox = false;
			this.Controls.Add(this.lbLang);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LangDisplay";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "LangDisplay";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Pink;
			this.ResumeLayout(false);
		}
	}
}
