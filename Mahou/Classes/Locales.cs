using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Mahou
{
    class Locales
    {
        public static uint GetCurrentLocale()
        {
            uint tid = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            IntPtr layout = GetKeyboardLayout(tid);
            return (uint)(layout.ToInt32() & 0xFFFF);
        }
        public static Locale[] AllList()
        {
            int count = 0;
            List<Locale> locs = new List<Locale>();
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                count++;
                locs.Add(new Locale
                {
                    Lang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lang.Culture.NativeName.Split(' ')[0].ToLower()),
                    uId = (uint)lang.Culture.KeyboardLayoutId
                });
            }
            return locs.ToArray();
        }
        private static bool wasShown = false;
        public static void IfLessThan2()
        {
            if (AllList().Length < 2 && !wasShown)
            {
                wasShown = true;
                if (MessageBox.Show("This program switches texts by system's layouts(locales/languages), please add at least 2!\nProgram will exit.", "You have too less layouts(locales/languages)!!") == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }
        public struct Locale
        {
            public string Lang { get; set; }
            public uint uId { get; set; }
        }
        #region DLLs
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetKeyboardLayout(uint WindowsThreadProcessID);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);
        #endregion
    }
}
