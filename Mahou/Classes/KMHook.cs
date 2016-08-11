using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace Mahou
{
    class KMHook // Keyboard & Mouse Hook
    {
        #region Variables
        public enum KMMessages // Keyboard & Mouse Messages
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
            WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104
        }
        public static bool self = false, afterConversion = false, printable = false,
                           win = false, alt = false, ctrl = false, shift = false,
                           PressShiftAgain = false, PressCtrlAgain = false, PressAltAgain = false,
                           awas = false, swas = false, cwas = false,
                           bothnotmatch = false, altnum = false, altnumline = false;
        public static int altnums_word = 0, altnums_line = 0;
        public static Exception notINany = new Exception("Selected text is not in any of selected layouts(locales/languages) in settings\nor contains characters from other than selected layouts(locales/languages).");
        public delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);
        #endregion
        #region Keyboard & Mouse hooks events
        static List<Keys> numpads = new List<Keys>();
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
            Keys Key = (Keys)vkCode; // "Key" will further be used instead of "(Keys)vkCode"
            #region Checks modifiers that are down
            if (Key == Keys.LShiftKey || Key == Keys.RShiftKey || Key == Keys.ShiftKey)
            { shift = (wParam == (IntPtr)KMMessages.WM_KEYDOWN) ? true : false; }
            if (Key == Keys.RControlKey || Key == Keys.LControlKey || Key == Keys.ControlKey)
            { ctrl = (wParam == (IntPtr)KMMessages.WM_KEYDOWN) ? true : false; }
            if (Key == Keys.RMenu || Key == Keys.LMenu || Key == Keys.Menu)
            { alt = (wParam == (IntPtr)KMMessages.WM_SYSKEYDOWN) ? true : false; }
            if (Key == Keys.RWin || Key == Keys.LWin) // Checks if win is down
            { win = (wParam == (IntPtr)KMMessages.WM_KEYDOWN) ? true : false; }
            #endregion
            #region Release Re-Pressed keys
            if (MahouForm.hotkeywithmodsfired && wParam == (IntPtr)KMMessages.WM_KEYUP && !self &&
                (Key == Keys.LShiftKey || Key == Keys.LMenu || Key == Keys.LControlKey))
            {
                MahouForm.hotkeywithmodsfired = false;
                if (swas)
                {
                    swas = false;
                    KeybdEvent(Keys.LShiftKey, 2);
                }
                if (awas)
                {
                    awas = false;
                    KeybdEvent(Keys.LMenu, 2);
                }
                if (cwas)
                {
                    cwas = false;
                    KeybdEvent(Keys.LControlKey, 2);
                }
                Thread.Sleep(20);
            }
            #endregion
            #region Switch only key
            if (!self && MMain.MySetts.OnlyKeyLayoutSwicth == "CapsLock" && Key == Keys.CapsLock && wParam == (IntPtr)KMMessages.WM_KEYUP)
            {
                self = true;
                ChangeLayout();
                self = false;
            }
            if (!self && MMain.MySetts.OnlyKeyLayoutSwicth == "CapsLock" && Key == Keys.CapsLock && wParam == (IntPtr)KMMessages.WM_KEYDOWN)
            {
                self = true;  
                if (Control.IsKeyLocked(Keys.CapsLock)) // Turn off if alraedy on
                {
                    KeybdEvent(Keys.CapsLock, 0);
                    KeybdEvent(Keys.CapsLock, 2);
                }
                //Code below removes CapsLock original action, but if hold will not work and will stuck, press again to off.
                KeybdEvent(Keys.CapsLock, 0);
                KeybdEvent(Keys.CapsLock, 2);
                self = false;
            }
            if (!self && MMain.MySetts.OnlyKeyLayoutSwicth == "Left Control" && Key == Keys.LControlKey && wParam == (IntPtr)KMMessages.WM_KEYUP)
            {
                //The only working way for left control... o_0
                self = true;
                KeybdEvent(Keys.LControlKey, 2);
                KeybdEvent(Keys.LMenu, 0);
                Thread.Sleep(10);
                KeybdEvent(Keys.LShiftKey, 0);
                Thread.Sleep(10);
                KeybdEvent(Keys.LMenu, 2);
                Thread.Sleep(10);
                KeybdEvent(Keys.LShiftKey, 2);
                self = false;
            }
            if (!self && MMain.MySetts.OnlyKeyLayoutSwicth == "Right Control" && Key == Keys.RControlKey && wParam == (IntPtr)KMMessages.WM_KEYUP)
            {
                self = true;
                KeybdEvent(Keys.RControlKey, 2);
                ChangeLayout();
                self = false;
            }
            #endregion
            #region Other, when KeyDown
            if (nCode >= 0 && wParam == (IntPtr)KMMessages.WM_KEYDOWN)
            {
                if (Key == Keys.Space && afterConversion && !self) // && MMain.MySetts.SpaceBreak
                {
                    MMain.c_word.Clear();
                    afterConversion = false;
                }
                if (Key == Keys.Back && !self) //Removes last item from current word when user press Backspace
                {
                    if (MMain.c_word.Count != 0)
                    {
                        MMain.c_word.RemoveAt(MMain.c_word.Count - 1);
                    }
                    if (MMain.c_line.Count != 0)
                    {
                        MMain.c_line.RemoveAt(MMain.c_line.Count - 1);
                    }
                }
                if (Key == Keys.Enter || Key == Keys.Home || Key == Keys.End ||
                    Key == Keys.Tab || Key == Keys.PageDown || Key == Keys.PageUp ||
                    Key == Keys.Left || Key == Keys.Right || Key == Keys.Down || Key == Keys.Up || //Pressing any of these Keys will empty current word
                    (win && Key == Keys.Back)) //Any modifier + Back will clear word too
                {
                    MMain.c_word.Clear();
                    MMain.c_line.Clear();
                    //altnums_word = 0;
                    //altnums_line = 0;
                    //altnum = false;
                    //altnumline = false;
                }
                if (Key == Keys.Space && !self)
                {
                    //altnum = false;
                    //altnums_word = 0;
                    MMain.c_word.Clear();
                    MMain.c_line.Add(new YuKey() { yukey = Keys.Space, upper = false });
                }
                if (
                    (Key >= Keys.D0 && Key <= Keys.Z) ||
                    Key >= Keys.Oem1 && Key <= Keys.OemBackslash
                    )
                {
                    printable = true;
                }
                else { printable = false; }
                if (printable && !self && !win && !alt && !ctrl)
                {
                    if (!shift)
                    {
                        MMain.c_word.Add(new YuKey() { yukey = Key, upper = false });
                        MMain.c_line.Add(new YuKey() { yukey = Key, upper = false });
                    }
                    else
                    {
                        MMain.c_word.Add(new YuKey() { yukey = Key, upper = true });
                        MMain.c_line.Add(new YuKey() { yukey = Key, upper = true });
                    }
                }
            }
            #endregion
            #region Alt+Numpad (not implented yet)
            if (alt && (Key >= Keys.NumPad0 && Key <= Keys.NumPad9))
            {
                //altnums_word += 1;
                //altnums_line += 1;
                //altnum = true;
                //altnumline = true;
            }
            #endregion
            return CallNextHookEx(MMain._hookID, nCode, wParam, lParam);
        }
        public static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if ((KMMessages.WM_LBUTTONDOWN == (KMMessages)wParam) || KMMessages.WM_RBUTTONDOWN == (KMMessages)wParam)
                {
                    MMain.c_word.Clear();
                    MMain.c_line.Clear();
                }
            }
            return CallNextHookEx(MMain._mouse_hookID, nCode, wParam, lParam);
        }
        #endregion
        #region Functions/Struct
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
                    //by pressing "Convert Selection hotkey" without selected text.
                    Clipboard.Clear();
                    //Without Thread.Sleep() below - Clipboard.GetText() will crash,
                    KeybdEvent(Keys.RControlKey, 0);
                    Thread.Sleep(10);
                    KeybdEvent(Keys.Insert, 0);
                    Thread.Sleep(10);
                    KeybdEvent(Keys.RControlKey, 2);
                    Thread.Sleep(10);
                    KeybdEvent(Keys.Insert, 2);
                    Thread.Sleep(10);
                    Exception threadEx = null;
                    //If errored using thread, will not make all app to freeze, instead of just try/catch that actually will...
                    Thread staThread = new Thread(delegate()
                    {
                        try
                        {
                            Console.WriteLine("Try #" + i); ClipStr = Clipboard.GetText();
                        }
                        catch (Exception ex) { threadEx = ex; }
                    });
                    staThread.Name = "GetText";
                    staThread.SetApartmentState(ApartmentState.STA);
                    staThread.Start();
                    staThread.Join();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); ConvertSelection(); }
            finally
            {
                if (!String.IsNullOrEmpty(ClipStr))
                {
                    KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Back, true) }, false);
                    var result = "";
                    result = UltimateUnicodeConverter.InAnother(ClipStr, MMain.MySetts.locale2uId, MMain.MySetts.locale1uId, true);
                    //if same first time try switching locales
                    //Without Regex.Replace below selected text that heve new line will stop converting
                    if (Regex.Replace(result, "\r\\D\n|\n\\D\r", "\n") == Regex.Replace(ClipStr, "\r\\D\n|\n\\D\r", "\n"))
                    {
                        result = UltimateUnicodeConverter.InAnother(ClipStr, MMain.MySetts.locale1uId, MMain.MySetts.locale2uId, true);
                    }
                    //Fix for multiline duplications
                    result = Regex.Replace(result, "\r\\D\n|\n\\D\r", "\n");
                    //Inputs converted text
                    KInputs.MakeInput(KInputs.AddString(result, true), false);
                    //reselects text
                    for (int i = result.Length; i != 0; i--)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Left, true) }, true);
                    }
                    Clipboard.Clear();
                }
                RePress();
                self = false;
                MahouForm.HKCSelection.Register(); //Restores CS hotkey ability
            }
        }
        public static void RePress()
        {
            //Repress's modifiers by Press Again variables
            if (PressShiftAgain)
            {
                swas = true;
                KeybdEvent(Keys.LShiftKey, 0);
                Thread.Sleep(10);
                PressShiftAgain = false;
            }
            if (PressAltAgain)
            {
                awas = true;
                KeybdEvent(Keys.LMenu, 0);
                Thread.Sleep(10);
                PressAltAgain = false;
            }
            if (PressCtrlAgain)
            {
                cwas = true;
                KeybdEvent(Keys.LControlKey, 0);
                Thread.Sleep(10);
                PressCtrlAgain = false;
            }
        }
        public static void ConvertLast(System.Collections.Generic.List<YuKey> c_, bool altn, int altnc)
        {
            Locales.IfLessThan2();
            YuKey[] YuKeys = c_.ToArray();
            if (altn) // Fix if entered alt + numpad
            {/*TODO Make it work with !self, if ever possible? :( :< :[
                Console.WriteLine("I still working");
                self = true;
                Thread staThread = new Thread(delegate()
                {
                    for (int i = YuKeys.Length + altnc; i != 0; i--)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Left, true, true) }, true);
                    }
                    ConvertSelection();
                    for (int i = YuKeys.Length + altnc; i != 0; i--)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Right, true, true) }, true);
                    }
                });
                staThread.Name = "FIX FOR ALTNUM";
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
              */
            }
            else
            {
                if (YuKeys.Length > 0)
                {
                    self = true;
                    ChangeLayout();
                    for (int e = YuKeys.Length; e != 0; e--)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.Back, true) }, false);
                    }
                    foreach (YuKey yk in YuKeys)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(yk.yukey, true) }, yk.upper);
                    }
                    RePress();
                    self = false;
                    afterConversion = true;
                }
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
                        Console.WriteLine(Locales.GetCurrentLocale());
                        //Checking now because of * & **                                                         ↓
                        notnowLocale = nowLocale == MMain.MySetts.locale1uId ? MMain.MySetts.locale2uId : MMain.MySetts.locale1uId;
                        //Some apps blocking PostMessage() so lets try CycleSwtich(),
                        //Applyes for Foobar2000, maybe something else... except metro apps *                    ↓
                        do
                        {
                            CycleSwitch();
                            //Check if abowe worked                       
                            if (Locales.GetCurrentLocale() == notnowLocale) { goto skip; }
                            tryes++;
                            //For return to last layout ***
                            if (tryes == 5 + MMain.locales.Length)
                            {
                                break;
                            }
                        } while (Locales.GetCurrentLocale() == nowLocale);
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
                    skip:
                        Thread.Sleep(5);
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
            KeybdEvent(Keys.LMenu, 0);
            Thread.Sleep(5);
            KeybdEvent(Keys.LShiftKey, 0);
            Thread.Sleep(5);
            KeybdEvent(Keys.LMenu, 2);
            Thread.Sleep(5);
            KeybdEvent(Keys.LShiftKey, 2);
        }
        public struct YuKey // YuKey is struct of key and it state(upper/lower)
        {
            public Keys yukey;
            public bool upper;
        }
        public static void KeybdEvent(Keys key, int flags) // Simplified keybd_event with exteded recongize feature
        {
            var extended = KInputs.IsExtended(key) ? 1 : 0;
            //Console.WriteLine(key + ":" + (extended | flags).ToString());
            //Do not remove this line, it needed for "Left Control Switch Layout" to work properly
            Thread.Sleep(15);
            keybd_event((byte)key, 0, flags | extended, 0);
        }
        #endregion
        #region DLL imports
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int extraInfo);
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