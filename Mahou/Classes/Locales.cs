using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mahou
{
	static class Locales
	{
		public static uint GetCurrentLocale() //Gets current locale in active window
		{
			uint tid = GetWindowThreadProcessId(ActiveWindow(), IntPtr.Zero);
			IntPtr layout = GetKeyboardLayout(tid);
			return (uint)(layout.ToInt32() & 0xFFFF);
		}
		public static IntPtr ActiveWindow() //Gets active windows(focused) or foreground
		{
			IntPtr awHandle = IntPtr.Zero;
			var gui = new GUITHREADINFO();
			gui.cbSize = Marshal.SizeOf(gui);
			GetGUIThreadInfo(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero), ref gui);

			awHandle = gui.hwndFocus;
			if (awHandle == IntPtr.Zero) {
				awHandle = GetForegroundWindow();
			} 
			return awHandle;
		}
		public static Locale[] AllList() //Gets list of all awaible layouts
		{
			int count = 0;
			var locs = new List<Locale>();
			foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages) {
				count++;
				locs.Add(new Locale {
					Lang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lang.Culture.NativeName.Split(' ')[0].ToLower()),
					uId = (uint)lang.Culture.KeyboardLayoutId
				});
			}
			return locs.ToArray();
		}
		public static void IfLessThan2() //Check if you have enough layouts
		{
			if (AllList().Length < 2) {
				MessageBox.Show("This program switches texts by system's layouts(locales/languages), please add at least 2!\nProgram will exit.",
				                "You have too less layouts(locales/languages)!!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				Application.Exit();
			}
		}
		#region Structs
		public struct Locale
		{
			public string Lang { get; set; }
			public uint uId { get; set; }
		}
		public struct RECT
		{
			public int iLeft;
			public int iTop;
			public int iRight;
			public int iBottom;
		}
		public struct GUITHREADINFO
		{
			public int cbSize;
			public int flags;
			public IntPtr hwndActive;
			public IntPtr hwndFocus;
			public IntPtr hwndCapture;
			public IntPtr hwndMenuOwner;
			public IntPtr hwndMoveSize;
			public IntPtr hwndCaret;
			public RECT rectCaret;
		}
		#endregion
		#region DLLs
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr GetKeyboardLayout(uint WindowsThreadProcessID);
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetGUIThreadInfo(uint hTreadID, ref GUITHREADINFO lpgui);
		#endregion
	}
}
