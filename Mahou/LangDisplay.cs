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
			switch (Locales.GetCurrentLocale()) {
				case 1025:
				case 1067:
				case 2049:
				case 3073:
				case 4097:
				case 5121:
				case 6145:
				case 7169:
				case 8193:
				case 9217:
				case 10241:
				case 11265:
				case 12289:
				case 13313:
				case 14337:
				case 15361:
					ChangeLD("Ar");
					break;
				case 1026:
					ChangeLD("Bu");
					break;
				case 1028:
				case 2052:
				case 3076:
				case 4100:
				case 5124:
					ChangeLD("Ch");
					break;
				case 1029:
					ChangeLD("Cz");
					break;
				case 1030:
					ChangeLD("Da");
					break;
				case 1031:
				case 5127:
				case 3079:
				case 4103:
				case 2055:
					ChangeLD("De");
					break;
				case 1032:
					ChangeLD("Gr");
					break;
				case 1033:
				case 2057:
				case 3081:
				case 4105:
				case 5129:
				case 7177:
					ChangeLD("En");
					break;
				case 1034:
				case 3082:
					ChangeLD("Sp");
					break;
				case 1035:
					ChangeLD("Fi");
					break;
				case 1036:
				case 5132:
					ChangeLD("Fr");
					break;
				case 1037:
					ChangeLD("He");
					break;
				case 1038:
					ChangeLD("Hu");
					break;
				case 1039:
					ChangeLD("Ic");
					break;
				case 1040:
				case 2064:
					ChangeLD("It");
					break;
				case 1041:
					ChangeLD("Jp");
					break;
				case 1042:
					ChangeLD("Ko");
					break;
				case 1043:
					ChangeLD("Du");
					break;
				case 1044:
				case 2068:
					ChangeLD("No");
					break;
				case 1045:
				case 1046:
				case 2070:
					ChangeLD("Po");
					break;
				case 1047:
					ChangeLD("Rr");
					break;
				case 1048:
				case 2072:
					ChangeLD("Ro");
					break;
				case 1049:
				case 2073:
					ChangeLD("Ru");
					break;
				case 1050:
				case 4122:
					ChangeLD("Cr");
					break;
				case 1051:
				case 1060:
					ChangeLD("Sl");
					break;
				case 1052:
				case 1156:
					ChangeLD("Al");
					break;
				case 1053:
				case 2077:
					ChangeLD("Sw");
					break;
				case 1054:
					ChangeLD("Th");
					break;
				case 1055:
				case 1090:
					ChangeLD("Tu");
					break;
				case 1056:
				case 2080:
					ChangeLD("Ur");
					break;
				case 1057:
				case 1117:
					ChangeLD("In");
					break;
				case 1058:
					ChangeLD("Ua");
					break;
				case 1059:
				case 1093:
				case 2117:
					ChangeLD("Be");
					break;
				case 1061:
					ChangeLD("Es");
					break;
				case 1062:
				case 1142:
					ChangeLD("La");
					break;
				case 1063:
					ChangeLD("Li");
					break;						
				case 1064:
				case 1092:
				case 1097:
					ChangeLD("Ta");
					break;
				case 1065:
					ChangeLD("Fa");
					break;
				case 1066:
					ChangeLD("Vi");
					break;
				case 1068:
				case 2092:
					ChangeLD("Az");
					break;	
				case 1069:
				case 1133:
					ChangeLD("Ba");
					break;					
				case 1091:
				case 2115:
					ChangeLD("Uz");
					break;
				case 2074:
				case 3098:
					ChangeLD("Se");
					break;
			}
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
