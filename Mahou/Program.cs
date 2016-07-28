using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Mahou
{
    class MMain
    {
        #region DLLs
        [DllImport("user32.dll")]
        public static extern uint RegisterWindowMessage(string message);
        #endregion
        #region Prevent another instance variables
        private const string appGUid = "ec511418-1d57-4dbe-a0c3-c6022b33735b";
        public static uint ao = RegisterWindowMessage("AlderyOpenedMahou!");
        #endregion
        #region All Main variables, arrays etc.
        public static List<KMHook.YuKey> c_word = new List<KMHook.YuKey>();
        public static List<KMHook.YuKey> c_line = new List<KMHook.YuKey>();
        public static IntPtr _hookID = IntPtr.Zero;
        public static IntPtr _mouse_hookID = IntPtr.Zero;
        public static KMHook.LowLevelProc _proc = KMHook.HookCallback;
        public static KMHook.LowLevelProc _mouse_proc = KMHook.MouseHookCallback;
        public static Locales.Locale[] locales = Locales.AllList();
        public static Settings MySetts = new Settings();
        public static MahouForm mahou = new MahouForm();
        #endregion
        [STAThread] //DO NOT REMOVE THIS
        public static void Main()
        {
            using (var mutex = new Mutex(false, "Global\\" + appGUid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    KMHook.PostMessage((IntPtr)0xffff, ao, 0, 0);
                    return;
                }
                if (locales.Length < 2)
                {
                    Locales.IfLessThan2();
                }
                else
                {
                    StartHook();
                    //for first run, add your locale 1 & locale 2 to settings
                    if (MySetts.locale1Lang == "" && MySetts.locale2Lang == "")
                    {
                        MySetts.locale1uId = locales[0].uId;
                        MySetts.locale2uId = locales[1].uId;
                        MySetts.locale1Lang = locales[0].Lang;
                        MySetts.locale2Lang = locales[1].Lang;
                    }
                    MySetts.Save();
                    Application.Run();
                    StopHook();
                }
            }
        }
        #region Actions with hooks
        public static void StartHook()
        {
            if (!CheckHook()) { return; }
            _mouse_hookID = KMHook.SetMouseHook(_mouse_proc);
            _hookID = KMHook.SetHook(_proc);
            Thread.Sleep(10); //Give some time for it to apply
        }
        public static void StopHook()
        {
            if (CheckHook()) { return; }
            KMHook.UnhookWindowsHookEx(_hookID);
            KMHook.UnhookWindowsHookEx(_mouse_hookID);
            _hookID = IntPtr.Zero;
            _mouse_hookID = IntPtr.Zero;
            Thread.Sleep(10); //Give some time for it to apply
        }
        public static bool CheckHook()
        {
            return _hookID == IntPtr.Zero;
        }
        #endregion
    }
}