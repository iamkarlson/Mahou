using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Mahou
{
	public partial class LangDisplay : Form
	{
		public LangDisplay()
		{
			InitializeComponent();
			SetVisInvis();
		}
		public void ChangeLD(string to)
		{
			lbLang.Text = to;
			if (MMain.MyConfs.ReadBool("TTipUI", "TransparentBack")) {
				Invalidate();
				Update();
			}
		}
		public void SetVisInvis()
		{
			lbLang.Visible = !MMain.MyConfs.ReadBool("TTipUI", "TransparentBack");
		}
		public void RefreshLang()
		{
			var clangname = new System.Globalization.CultureInfo((int)Locales.GetCurrentLocale());
			ChangeLD(clangname.ThreeLetterISOLanguageName.Substring(0,1).ToUpper() + clangname.ThreeLetterISOLanguageName.Substring(1));
		}
		public void ChangeColors(Color fore, Color back)
		{
			lbLang.ForeColor = fore;
			lbLang.BackColor = back;
		}
		public void ChangeSizes(Font fnt, int height, int width)
		{
			lbLang.Font = fnt;
			lbLang.Height = height;
			lbLang.Width = width;
		}
		public void ShowInactiveTopmost()
		{
			ShowWindow(Handle, SW_SHOWNOACTIVATE);
			SetWindowPos(Handle.ToInt32(), HWND_TOPMOST,
				Left, Top, Width, Height,
				SWP_NOACTIVATE);
		}
		protected override CreateParams CreateParams {
			get {
				var Params = base.CreateParams;
				Params.ExStyle |= 0x80;
				return Params;
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (MMain.MyConfs.ReadBool("TTipUI", "TransparentBack")) {
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
				e.Graphics.DrawString(lbLang.Text, lbLang.Font, new SolidBrush(lbLang.ForeColor), 0, 0);		
			}
			base.OnPaint(e);
		}
		public void HideWnd()
		{
			ShowWindow(Handle, 0);
		}
		#region P/Invoke
		const int SW_SHOWNOACTIVATE = 4;
		const int HWND_TOPMOST = -1;
		const uint SWP_NOACTIVATE = 0x0010;

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		static extern bool SetWindowPos(
			int hWnd,             // Window handle
			int hWndInsertAfter,  // Placement-order handle
			int X,                // Horizontal position
			int Y,                // Vertical position
			int cx,               // Width
			int cy,               // Height
			uint uFlags);
		// Window positioning flags

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		#endregion
	}
}
