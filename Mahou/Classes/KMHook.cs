using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Text;
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
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105
        }
        public static bool self, win, alt, ctrl, shift,
                           shiftRP, ctrlRP, altRP, //RP = Re-Press
                           awas, swas, cwas, afterEOS; //*was = alt/shift/ctrl was
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
                    KeybdEvent(Keys.Control, 2);
                }
            }
            #endregion
            #region Switch only key
            if (!self && MMain.MyConfs.Read("HotKeys", "OnlyKeyLayoutSwicth") == "CapsLock" && Key == Keys.CapsLock && wParam == (IntPtr)KMMessages.WM_KEYUP)
            {
                self = true;
                ChangeLayout();
                self = false;
            }
            if (!self && MMain.MyConfs.Read("HotKeys", "OnlyKeyLayoutSwicth") == "CapsLock" && Key == Keys.CapsLock && wParam == (IntPtr)KMMessages.WM_KEYDOWN)
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
            if (!self && MMain.MyConfs.Read("HotKeys", "OnlyKeyLayoutSwicth") == "Left Control" && Key == Keys.LControlKey && wParam == (IntPtr)KMMessages.WM_KEYUP)
            {
                self = true;
                if (MMain.MyConfs.ReadBool("Functions", "EmulateLayoutSwitch"))
                {
                    KeybdEvent(Keys.LControlKey, 2); // Sends it up to make it work when using "EmulateLayoutSwitch" 
                }
                ChangeLayout();
                KeybdEvent(Keys.LControlKey, 2); //fix for PostMessage, it somehow o_0 sends another ctrl...

                self = false;
            }
            if (!self && MMain.MyConfs.Read("HotKeys", "OnlyKeyLayoutSwicth") == "Right Control" && Key == Keys.RControlKey && wParam == (IntPtr)KMMessages.WM_KEYUP)
            {
                self = true;
                if (MMain.MyConfs.ReadBool("Functions", "EmulateLayoutSwitch"))
                {
                    KeybdEvent(Keys.RControlKey, 2); // Sends it up to make it work when using "EmulateLayoutSwitch" 
                }
                ChangeLayout();
                self = false;
            }
            #endregion
            #region Other, when KeyDown
            if (nCode >= 0 && wParam == (IntPtr)KMMessages.WM_KEYDOWN)
            {
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
                    //altinline = false;
                    //altinword = false;
                }
                if (Key == Keys.Space && !self)
                {
                    //altinword = false;
                    if (MMain.MyConfs.ReadBool("Functions", "EatOneSpace") && MMain.c_word.Count != 0 && MMain.c_word[MMain.c_word.Count - 1].yukey != Keys.Space)
                    {
                        MMain.c_word.Add(new YuKey() { yukey = Keys.Space, upper = false });
                        afterEOS = true;
                    }
                    else
                    {
                        MMain.c_word.Clear();
                        afterEOS = false;
                    }
                    MMain.c_line.Add(new YuKey() { yukey = Keys.Space, upper = false });
                }
                if (((Key >= Keys.D0 && Key <= Keys.Z) || // This is 0-9 & A-Z
                    Key >= Keys.Oem1 && Key <= Keys.OemBackslash // All other printables
                    ) && !self && !win && !alt && !ctrl)
                {
                    if (afterEOS) //Clears word after Eat ONE space
                    {
                        MMain.c_word.Clear();
                        afterEOS = false;
                    }
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
            #region Alt+Numpad (not fully workable)
            if (!self && alt && (Key >= Keys.NumPad0 && Key <= Keys.NumPad9) && wParam == (IntPtr)KMMessages.WM_SYSKEYUP)
            {
                MMain.c_word.Add(new YuKey() { yukey = Key, altnum = true });
                MMain.c_line.Add(new YuKey() { yukey = Key, altnum = true });
                Console.WriteLine("Added the " + Key);
                //foreach (var cha in MMain.c_word)
                //{
                //    Console.Write(cha.yukey + "/" + cha.altnum);
                //}
                //Console.WriteLine();
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
        public static void ConvertSelection() //Converts selected text
        {
            Locales.IfLessThan2();
            self = true;
            string ClipStr = "";
            // Backup & Restore feature, now only text supported...
            var doBackup = false;
            if (NativeClipboard.IsClipboardFormatAvailable((uint)NativeClipboard.uFormat.CF_UNICODETEXT))
            {
                doBackup = true;
            }
            var datas = new NativeClipboard.ClipboardData()
            {
                data = new List<byte[]>(),
                format = new List<uint>()
            };
            if (doBackup)
            {
                var t = new System.Threading.Tasks.Task(() =>
                    {
                        datas = NativeClipboard.GetClipboardDatas();
                    });
                t.RunSynchronously();
            }
            //This prevents from converting text that alredy exist in Clipboard
            //by pressing "Convert Selection hotkey" without selected text.
            NativeClipboard.Clear();
            KInputs.MakeInput(new KInputs.INPUT[] {
                KInputs.AddKey(Keys.RControlKey,true),
                KInputs.AddKey(Keys.Insert, true),
                KInputs.AddKey(Keys.Insert,false),
                KInputs.AddKey(Keys.RControlKey, false)});
            Thread.Sleep(30);
            ClipStr = NativeClipboard.GetText();
            if (!String.IsNullOrEmpty(ClipStr))
            {
                KInputs.MakeInput(new KInputs.INPUT[] {
                    KInputs.AddKey(Keys.Back,true),
                    KInputs.AddKey(Keys.Back,false) });
                var result = "";
                int items = 0;
                if (MMain.MyConfs.ReadBool("Functions", "CSSwitch"))
                {
                    var nowLocale = Locales.GetCurrentLocale();
                    self = true;
                    ChangeLayout();
                    // Don't even think "Regex.Replace(ClipStr, "\r\\D\n?|\n\\D\r?", "\n")" can't be used as variable...
                    foreach (char c in Regex.Replace(ClipStr, "\r\\D\n?|\n\\D\r?", "\n"))
                    {
                        items++;
                        var yk = new YuKey();
                        var scan = VkKeyScanEx(c, (IntPtr)nowLocale);
                        if (c == '\n')
                        {
                            yk.yukey = Keys.Enter;
                            yk.upper = false;
                        }
                        else
                        {
                            if (scan != -1)
                            {
                                var key = (Keys)(scan & 0xff);
                                var state = (VkKeyScanEx(c, (IntPtr)nowLocale) >> 8) & 0xff;
                                bool upper = false;
                                if (state == 1)
                                    upper = true;
                                yk = new YuKey() { yukey = key, upper = upper };
                                //Console.WriteLine(key + "~" + state);
                            }
                            else { yk = new YuKey() { yukey = Keys.None }; }
                        }
                        if (yk.yukey == Keys.None) // retype unrecognized as unicode
                        {
                            var unrecognized = ClipStr[items - 1].ToString();
                            KInputs.INPUT unr = KInputs.AddString(unrecognized)[0];
                            //Console.WriteLine("Uis = " + unrecognized + " ind = " + (items - 1));
                            //Console.WriteLine(unr.Data.Keyboard.Scan + "~" + unr.Data.Keyboard.Flags + "=>"  + unr.Data.Keyboard.Vk);
                            KInputs.MakeInput(new KInputs.INPUT[] { unr });
                        }
                        else
                        {
                            if (yk.upper)
                                KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.LShiftKey, true) });
                            KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(yk.yukey, true) });
                            KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(yk.yukey, false) });
                            if (yk.upper)
                                KInputs.MakeInput(new KInputs.INPUT[] { KInputs.AddKey(Keys.LShiftKey, false) });
                        }
                    }
                }
                else
                {
                    var l1 = (uint)MMain.MyConfs.ReadInt("Locales", "locale1uId");
                    var l2 = (uint)MMain.MyConfs.ReadInt("Locales", "locale2uId");
                    result = InAnother(ClipStr, l2, l1);
                    //if same first time try switching locales
                    //Without Regex.Replace "==" will fail
                    if (Regex.Replace(result, "\r\\D\n?|\n\\D\r?", "\n") == Regex.Replace(ClipStr, "\r\\D\n?|\n\\D\r?", "\n"))
                    {
                        result = InAnother(ClipStr, l1, l2);

                    }
                    result = Regex.Replace(result, "\r\\D\n?|\n\\D\r?", "\n");
                    //Inputs converted text
                    KInputs.MakeInput(KInputs.AddString(result));
                    items = result.Length;
                }
                if (MMain.MyConfs.ReadBool("Functions", "ReSelect"))
                {
                    //reselects text
                    for (int i = items; i != 0; i--)
                    {
                        KInputs.MakeInput(new KInputs.INPUT[] { 
                            KInputs.AddKey(Keys.LShiftKey, true),
                            KInputs.AddKey(Keys.Left,true),
                            KInputs.AddKey(Keys.Left,false),
                            KInputs.AddKey(Keys.LShiftKey, false) });
                    }
                }
            }
            RePress();
            NativeClipboard.Clear();
            if (doBackup)
            {
                NativeClipboard.RestoreData(datas);
            }
            self = false;
            MahouForm.HKCSelection.Register(); //Restores CS hotkey ability
        }
        public static void RePress() //Re-presses modifiers you hold when hotkey fired(due to SendModsUp())
        {
            //Repress's modifiers by Press Again variables
            if (shiftRP)
            {
                KeybdEvent(Keys.LShiftKey, 0);
                swas = true;
                shiftRP = false;
            }
            if (altRP)
            {
                KeybdEvent(Keys.LMenu, 0);
                awas = true;
                altRP = false;
            }
            if (ctrlRP)
            {
                KeybdEvent(Keys.LControlKey, 0);
                cwas = true;
                ctrlRP = false;
            }
        }
        public static void ConvertLast(List<YuKey> c_, bool type) //Converts last word/line
        {
            Locales.IfLessThan2();
            YuKey[] YuKeys = c_.ToArray();
            {
                self = true;
                ChangeLayout();
                for (int e = 0; e < YuKeys.Length; e++)
                {
                    KInputs.MakeInput(new KInputs.INPUT[] 
                        { KInputs.AddKey(Keys.Back,true),
                          KInputs.AddKey(Keys.Back,false) 
                        });
                }
                List<KInputs.INPUT> yuInpt = new List<KInputs.INPUT>();
                for (int i = 0; i < YuKeys.Length; i++)
                {
                    if (YuKeys[i].upper)
                        yuInpt.Add(KInputs.AddKey(Keys.LShiftKey, true));
                    if (YuKeys[i].altnum)
                        yuInpt.Add(KInputs.AddKey(Keys.LMenu, true));
                    yuInpt.Add(KInputs.AddKey(YuKeys[i].yukey, true));
                    yuInpt.Add(KInputs.AddKey(YuKeys[i].yukey, false));
                    if (YuKeys[i].upper)
                        yuInpt.Add(KInputs.AddKey(Keys.LShiftKey, false));
                    if (YuKeys[i].altnum)
                        yuInpt.Add(KInputs.AddKey(Keys.LMenu, false));
                }
                KInputs.MakeInput(yuInpt.ToArray());
                RePress();
                self = false;
            }
            if (type)
                MahouForm.HKCLast.Register(); //Restores CL hotkey ability
            else
                MahouForm.HKCLine.Register(); //Resorest CLine hotkey ability
        }
        private static void ChangeLayout() //Changes current layout
        {
            var nowLocale = Locales.GetCurrentLocale();
            uint notnowLocale = nowLocale == (uint)MMain.MyConfs.ReadInt("Locales", "locale1uId")
                ? (uint)MMain.MyConfs.ReadInt("Locales", "locale2uId")
                : (uint)MMain.MyConfs.ReadInt("Locales", "locale1uId");
            if (!MMain.MyConfs.ReadBool("Functions", "CycleMode"))
            {
                int tries = 0;
                //Cycles while layout not changed
                while (Locales.GetCurrentLocale() == nowLocale)
                {
                    PostMessage(Locales.ActiveWindow(), KInputs.WM_INPUTLANGCHANGEREQUEST, 0, notnowLocale);
                    Thread.Sleep(50);//Give some time to switch layout
                    tries++;
                    if (tries == 3)
                        break;
                }
            }
            else
                CycleSwitch();
        }
        private static void CycleSwitch() //Switches layout by cycling between installed all in system
        {
            if (MMain.MyConfs.ReadBool("Functions", "EmulateLayoutSwitch"))
            {
                if (MMain.MyConfs.ReadInt("Functions", "ELSType") == 0)
                    //Emulate Alt+Shift
                    KInputs.MakeInput(new KInputs.INPUT[] {
                      KInputs.AddKey(Keys.LMenu, true),
                      KInputs.AddKey(Keys.LShiftKey, true),
                      KInputs.AddKey(Keys.LShiftKey, false),
                      KInputs.AddKey(Keys.LMenu, false)});
                else if (MMain.MyConfs.ReadInt("Functions", "ELSType") == 1)
                {
                    //Emulate Ctrl+Shift
                    KInputs.MakeInput(new KInputs.INPUT[] {
                      KInputs.AddKey(Keys.LControlKey, true),
                      KInputs.AddKey(Keys.LShiftKey, true),
                      KInputs.AddKey(Keys.LShiftKey, false),
                      KInputs.AddKey(Keys.LControlKey, false)});
                }
                else
                {
                    //Emulate Win+Space
                    KInputs.MakeInput(new KInputs.INPUT[] {
                        KInputs.AddKey(Keys.LWin, true),
                        KInputs.AddKey(Keys.Space, true),
                        KInputs.AddKey(Keys.Space, false),
                        KInputs.AddKey(Keys.LWin, false)});
                    Thread.Sleep(100); //Important!
                }
            }
            else
                //Use PostMessage to switch to next layout
                PostMessage(Locales.ActiveWindow(), KInputs.WM_INPUTLANGCHANGEREQUEST, 0, KInputs.HKL_NEXT);
        }
        public static string InAnother(string input, uint uID1, uint uID2)
        {
            var result = "";
            var index = 0;
            foreach (char c in input.ToCharArray())
            {
                var upper = false;
                var cc = c;
                var chsc = VkKeyScanEx(cc, (IntPtr)uID1);
                var state = (chsc >> 8) & 0xff;
                //Checks if 'chsc' have upper state
                if (state == 1)
                    upper = true;
                byte[] byt = new byte[256];
                //it needs just 1 but,anyway let it be 10, i think that's better
                StringBuilder s = new StringBuilder(10);
                if (upper)
                {
                    byt[(int)Keys.ShiftKey] = 0xFF;
                }
                //"Convert magick✩" is the string below
                var ant = ToUnicodeEx((uint)chsc, (uint)chsc, byt, s, s.Capacity, 0, (IntPtr)uID2);
                if (chsc != -1)
                    result += s;
                else
                    result += input[index];
                index++;
            }
            return result;
        }
        public static void KeybdEvent(Keys key, int flags) // Simplified keybd_event with exteded recongize feature
        {
            //Do not remove this line, it needed for "Left Control Switch Layout" to work properly
            Thread.Sleep(15);
            keybd_event((byte)key, 0, flags | (KInputs.IsExtended(key) ? 1 : 0), 0);
        }
        public struct YuKey // YuKey is struct of key and it state(upper/lower) AND if it is Alt+[NumPad]
        {
            public Keys yukey;
            public bool upper;
            public bool altnum;
        }
        //private static void Refocus() // No more needed since Win+Space exist...
        //{
        //    //Another fix for metro apps(if 3 or more languages)
        //    //if all 5 times GetCurrentLocale() == nowLocale & 3 CycleSwitch()'es failed,
        //    //then it is must be metro app, in which GetCurrentLocale() will not return properly id, *
        //    //the only way to fix it is to re-focus app.                                             **
        //    IntPtr lastwindow = Locales.ActiveWindow();
        //    Form f = new Form();
        //    f.ShowInTaskbar = false;
        //    f.TopMost = true;
        //    f.Opacity = 0;
        //    f.Show();
        //    SetForegroundWindow(f.Handle);
        //    //Works perfect :)
        //    //Time has been reduced to 0.1 sec seperately
        //    Thread.Sleep(50);
        //    f.Hide();
        //    SetForegroundWindow(lastwindow);
        //    Thread.Sleep(50);
        //}
        #endregion
        #region DLL imports
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int extraInfo);

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

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hhwnd, uint msg, uint wparam, uint lparam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState,
         StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);
        #endregion
    }
}