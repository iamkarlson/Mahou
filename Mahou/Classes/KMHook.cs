using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
namespace Mahou
{
    class KMHook //Keyboard & Mouse Hook
    {
        #region Variables
        public enum KMMessages //Keyboard & Mouse Messages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101
        }
        public static bool shift = false, self = false, afterConversion = false,
                           other = false,
                           bothnotmatch = false, printable = false;
        public static Exception notINany = new Exception("Selected text is not in any of selected layouts(locales/languages) in settings\nor contains characters from other than selected layouts(locales/languages).");
        public delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);
        #endregion
        #region Keyboard & Mouse hooks events
        public static IntPtr SetHook(LowLevelProc proc)
        {
            using (Process currProcess = Process.GetCurrentProcess())
            using (ProcessModule currModule = currProcess.MainModule)
            {
                return SetWindowsHookEx((int)KMMessages.WH_KEYBOARD_LL, proc,
                    GetModuleHandle(currModule.ModuleName), 0);
            }
        }
        public static IntPtr SetMouseHook(LowLevelProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx((int)KMMessages.WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Keys Key = (Keys)vkCode; //"Key" will further be used instead of "(Keys)vkCode"
            if (Key == Keys.LShiftKey || Key == Keys.RShiftKey ||
                Key == Keys.Shift || Key == Keys.ShiftKey)//Checks if any shift is down
            { shift = (wParam == (IntPtr)KMMessages.WM_KEYDOWN) ? true : false; }
            if (Key == Keys.RControlKey || Key == Keys.LControlKey ||
                Key == Keys.ControlKey || Key == Keys.Control ||
                Key == Keys.RMenu || Key == Keys.LMenu ||
                Key == Keys.RWin || Key == Keys.LWin)//Checks if other modifiers is down
            { other = (wParam == (IntPtr)KMMessages.WM_KEYDOWN) ? true : false; }
            if (nCode >= 0 && wParam == (IntPtr)KMMessages.WM_KEYDOWN)
            {
                if (Key == Keys.CapsLock && !self && MMain.MySetts.SwitchLayoutByCaps)
                {
                    self = true;
                    //Code below removes CapsLock original action
                    if (Control.IsKeyLocked(Keys.CapsLock))
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.CapsLock, true, true) }, false);
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.CapsLock, false, true) }, false);
                    }
                    KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.CapsLock, true, true) }, false);
                    KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.CapsLock, false, true) }, false);
                    ChangeLayout();
                    self = false;
                }
                if (Key == Keys.Space && afterConversion)
                {
                    MMain.c_word.Clear();
                    afterConversion = false;
                }
                if (Key == Keys.Back && !self)// Removes last item from current word when user press Backspace
                {
                    if (MMain.c_word.Count != 0)
                    {
                        MMain.c_word.RemoveAt(MMain.c_word.Count - 1);
                    }
                }
                if (Key == Keys.Enter || Key == Keys.Home || Key == Keys.End ||
                    Key == Keys.Tab || Key == Keys.PageDown || Key == Keys.PageUp ||
                    Key == Keys.Left || Key == Keys.Right || Key == Keys.Down || Key == Keys.Up ||//Pressing any of these Keys will empty current word
                    other && Key == Keys.Back)//Any modifier + Back will clear word too
                {
                    MMain.c_word.Clear();
                }
                if (Key == Keys.Space && !self)
                {
                    if (MMain.MySetts.SpaceBreak)
                    {
                        MMain.c_word.Clear();
                    }
                    else
                    {
                        MMain.c_word.Add(new YuKey() { yukey = Keys.Space, upper = false });
                        //WriteEveryWhere(" ", new YuKey() { yukey = Keys.Space, upper = false });
                    }
                }
                if (
                    (Key >= Keys.D0 && Key <= Keys.Z) ||
                    Key >= Keys.Oem1 && Key <= Keys.OemBackslash
                    )
                {
                    printable = true;
                }
                else { printable = false; }
                if (printable && !self && !other)
                {
                    //uint Cyulocale = Locales.GetCurrentLocale();
                    if (!shift)
                    {
                        MMain.c_word.Add(new YuKey() { yukey = Key, upper = false });
                        //WriteEveryWhere((MakeAnother(vkCode, Cyulocale)),new YuKey() { yukey = Key, upper = false });
                    }
                    else
                    {
                        MMain.c_word.Add(new YuKey() { yukey = Key, upper = true });
                        //WriteEveryWhere((MakeAnother(vkCode, Cyulocale)).ToUpper(),new YuKey() { yukey = Key, upper = true });
                    }
                }
            }
            return CallNextHookEx(MMain._hookID, nCode, wParam, lParam);
        }
        public static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if ((KMMessages.WM_LBUTTONDOWN == (KMMessages)wParam) || KMMessages.WM_RBUTTONDOWN == (KMMessages)wParam)
                {
                    MMain.c_word.Clear();
                }
            }
            return CallNextHookEx(MMain._mouse_hookID, nCode, wParam, lParam);
        }
        #endregion
        #region Functions/Struct
        public static async Task ActCall(Action a)
        {
            await Task.Run(a);
            MahouForm.HKConvertLast.Register(); //Restores CL hotkey ability
        }
        public static void ConvertSelection()
        {
            Locales.IfLessThan2();
            self = true;
            string ClipStr = "";
            try
            {
                for (int i = 1; i != 5; i++)
                {
                    if (!String.IsNullOrEmpty(ClipStr))
                    { break; }
                    //This prevents from converting text that alredy exist in Clipboard
                    //by pressing Scroll without selected text.
                    Clipboard.Clear();
                    //Without Thread.Sleep() below - Clipboard.GetText() will crash,
                    keybd_event((int)Keys.RControlKey, (byte)MapVirtualKey((int)Keys.RControlKey, 0), 1, 0);
                    Thread.Sleep(10);
                    keybd_event((int)Keys.Insert, (byte)MapVirtualKey((int)Keys.Insert, 0), 1, 0);
                    Thread.Sleep(10);
                    keybd_event((int)Keys.RControlKey, (byte)MapVirtualKey((int)Keys.RControlKey, 0), 1 | 2, 0);
                    keybd_event((int)Keys.Insert, (byte)MapVirtualKey((int)Keys.Insert, 0), 1 | 2, 0);
                    Thread.Sleep(10);
                    Exception threadEx = null;
                    //If errored using thread, will not make all app to freeze, instead of just try/catch that actually will...
                    Thread staThread = new Thread(delegate()
                    {
                        try
                        {
                            Console.WriteLine(i); ClipStr = Clipboard.GetText();
                        }
                        catch (Exception ex) { threadEx = ex; }
                    });
                    staThread.Name = "GetText";
                    staThread.SetApartmentState(ApartmentState.STA);
                    staThread.Start();
                    staThread.Join();
                }
            }
            catch { ConvertSelection(); }
            finally
            {
                if (!String.IsNullOrEmpty(ClipStr))
                {
                    KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Back, true, true) }, false);
                    var result = "";
                    do
                    {
                        if (MMain.MySetts.locale1uId == MMain.MySetts.locale2uId)
                        {
                            result = ClipStr;
                            break;
                        }
                        result = UltimateUnicodeConverter.InAnother(ClipStr, MMain.MySetts.locale2uId, MMain.MySetts.locale1uId, true);
                        //if errored first time try switching locales
                        if (result == "ERROR")
                        {
                            result = UltimateUnicodeConverter.InAnother(ClipStr, MMain.MySetts.locale1uId, MMain.MySetts.locale2uId, true);
                            //if errored again throw exception
                            if (result == "ERROR")
                            {
                                bothnotmatch = true;
                                throw notINany;
                            }
                            bothnotmatch = false;
                        }
                        if (result != "ERROR")
                        {
                            break;
                        }
                    } while (result == "ERROR");
                    //Fix for multiline duplications
                    result = System.Text.RegularExpressions.Regex.Replace(result, "\r\\D\n?|\n\\D\r?", "\n");
                    //Inputs converted text
                    KInputs.MakeInput(KInputs.AddString(result, true), false);
                    //reselects text
                    for (int i = result.Length; i != 0; i--)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Left, true, true) }, true);
                    }
                    Clipboard.Clear();
                }
                self = false;
                MahouForm.HKConvertSelection.Register(); //Restores CS hotkey ability
            }
        }
        public static void ConvertLast()
        {
            Locales.IfLessThan2();
            YuKey[] YuKeys = MMain.c_word.ToArray();
            if (YuKeys.Length > 0)
            {
                self = true;
                ChangeLayout();
                for (int e = YuKeys.Length; e != 0; e--)
                {
                    KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Back, true, true) }, false);
                }
                foreach (YuKey yk in YuKeys)
                {
                    KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(yk.yukey, true, false) }, yk.upper);
                }
                self = false;
                afterConversion = true;
            }
        }
        private static void ChangeLayout()
        {
            var nowLocale = Locales.GetCurrentLocale();
            uint notnowLocale = nowLocale == MMain.MySetts.locale1uId ? MMain.MySetts.locale2uId : MMain.MySetts.locale1uId;
            if (!MMain.MySetts.CycleMode)
            {
                int tryes = 0;
                //Cycles while layout not changed
                while (Locales.GetCurrentLocale() == nowLocale)
                {
                    PostMessage(Locales.GetForegroundWindow(), 0x50, 0, notnowLocale);
                    Thread.Sleep(5);//Give some time to switch layout
                    tryes++;
                    if (tryes == 5)
                    {
                        //Checking now because of * & **                                                         ↓
                        notnowLocale = nowLocale == MMain.MySetts.locale1uId ? MMain.MySetts.locale2uId : MMain.MySetts.locale1uId;
                        //Some apps blocking PostMessage() so lets try CycleSwtich(),
                        //Applyes for Foobar2000, maybe something else... except metro apps *                    ↓
                        do 
                        {
                            CycleSwitch();
                            Thread.Sleep(5);
                            //Check if abowe worked                       
                            if (Locales.GetCurrentLocale() == notnowLocale) { break; }
                            tryes++;
                            //For return to last layout ***
                            if (tryes == 5 + MMain.locales.Length)
                            {
                                break;
                            }
                        }while(Locales.GetCurrentLocale() == nowLocale);
                        //Another fix for metro apps(if 3 or more languages)
                        //if all 5 times GetCurrentLocale() == nowLocale & 3 CycleSwitch()'es failed,
                        //then it is must be metro app, in which GetCurrentLocale() will not return properly id, *
                        //the only way to fix it is to re-focus app.                                             **
                        //->  Re-focus
                        IntPtr lastwindow = Locales.GetForegroundWindow();
                        Form f = new Form();
                        f.ShowInTaskbar = false;
                        f.TopMost = true;
                        f.Opacity = 0;
                        f.Show();
                        SetForegroundWindow(f.Handle);
                        //Thanks to ***                ↑
                        //Works perfect :)
                        //Time has been reduced to 0.1 sec seperately
                        Thread.Sleep(50);
                        f.Hide();
                        SetForegroundWindow(lastwindow);
                        Thread.Sleep(50);
                        //<-                                              
                        notnowLocale = nowLocale == MMain.MySetts.locale1uId ? MMain.MySetts.locale2uId : MMain.MySetts.locale1uId;
                        PostMessage(Locales.GetForegroundWindow(), 0x50, 0, notnowLocale);
                        break;
                    }
                }
            }
            else
            {
                CycleSwitch();
            }
        }
        private static void CycleSwitch()
        {
            //Without Sleeps below won't work.
            keybd_event((int)Keys.LMenu, (byte)MapVirtualKey((int)Keys.LMenu, 0), 1, 0);
            Thread.Sleep(10);
            keybd_event((int)Keys.LShiftKey, (byte)MapVirtualKey((int)Keys.LShiftKey, 0), 1, 0);
            Thread.Sleep(10);
            keybd_event((int)Keys.LShiftKey, (byte)MapVirtualKey((int)Keys.LShiftKey, 0), 1 | 2, 0);
            keybd_event((int)Keys.LMenu, (byte)MapVirtualKey((int)Keys.LMenu, 0), 1 | 2, 0);
            Thread.Sleep(10);
        }
        public struct YuKey // YuKey is struct of key and it state(upper/lower)
        {
            public Keys yukey;
            public bool upper;
        }
        /* Debug, uncomment if needed
        public static string MakeAnother(int vkCode, uint uId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(10);
            var lpkst = new byte[256];
            if (shift)
            {
                lpkst[(int)Keys.ShiftKey] = 0xff;
            }
            int rc = ToUnicodeEx((uint)vkCode, (uint)vkCode, lpkst, sb, sb.Capacity, 0, (IntPtr)uId);
            return sb.ToString();
        }
        public static void WriteEveryWhere(string vc, YuKey Yu) //as name says
        {
            //Console.WriteLine(vc);
            MMain.c_word.Add(Yu);
        }*/
        #endregion
        #region DLL imports
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int extraInfo);
        [DllImport("user32.dll")]
        public static extern short MapVirtualKey(int wCode, int wMapType);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode,
            byte[] lpKeyState, System.Text.StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
           LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
           IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hhwnd, uint msg, uint wparam, uint lparam);
        #endregion
    }
}