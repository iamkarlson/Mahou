using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
class KInputs
{
    #region Native Win32
    public const int INPUT_KEYBOARD = 1;
    public const uint HKL_NEXT = 1;
    public const uint WM_INPUTLANGCHANGEREQUEST = 0x0050;
    public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
    public const uint KEYEVENTF_KEYUP = 0x0002;
    public const uint KEYEVENTF_UNICODE = 0x0004;
    public const uint KEYEVENTF_SCANCODE = 0x0008;
#pragma warning disable 649
    internal struct INPUT
    {
        public UInt32 Type;
        public KEYBOARDMOUSEHARDWARE Data;
    }
    [StructLayout(LayoutKind.Explicit)]
    internal struct KEYBOARDMOUSEHARDWARE
    {
        [FieldOffset(0)]
        public KEYBDINPUT Keyboard;
        [FieldOffset(0)]
        public HARDWAREINPUT Hardware;
        [FieldOffset(0)]
        public MOUSEINPUT Mouse;
    }
    internal struct KEYBDINPUT
    {
        public UInt16 Vk;
        public UInt16 Scan;
        public UInt32 Flags;
        public UInt32 Time;
        public IntPtr ExtraInfo;
    }
    internal struct MOUSEINPUT
    {
        public Int32 X;
        public Int32 Y;
        public UInt32 MouseData;
        public UInt32 Flags;
        public UInt32 Time;
        public IntPtr ExtraInfo;
    }
    internal struct HARDWAREINPUT
    {
        public UInt32 Msg;
        public UInt16 ParamL;
        public UInt16 ParamH;
    }
#pragma warning restore 649
    #endregion
    #region Add Inputs & Make Input & check
    public static INPUT AddKey(Keys key, bool down)
    {
        UInt16 vk = (UInt16)key;
        INPUT input = new INPUT
        {
            Type = INPUT_KEYBOARD,
            Data =
            {
                Keyboard = new KEYBDINPUT
                {
                    Vk = vk,
                    Flags = IsExtended(key) ? (down ? (KEYEVENTF_EXTENDEDKEY) : (KEYEVENTF_KEYUP | KEYEVENTF_EXTENDEDKEY)) : (down ? 0 : KEYEVENTF_KEYUP),
                    Scan = 0,
                    ExtraInfo = IntPtr.Zero,
                    Time = 0
                }
            }
        };
        return input;
    }
    public static bool IsExtended(Keys key)
    {
        if (key == Keys.Menu ||
            key == Keys.LMenu ||
            key == Keys.RMenu ||
            key == Keys.Control ||
            key == Keys.RControlKey ||
            key == Keys.Insert ||
            key == Keys.Delete ||
            key == Keys.Home ||
            key == Keys.End ||
            key == Keys.Prior ||
            key == Keys.Next ||
            key == Keys.Right ||
            key == Keys.Up ||
            key == Keys.Left ||
            key == Keys.Down ||
            key == Keys.NumLock ||
            key == Keys.Cancel ||
            key == Keys.Snapshot ||
            key == Keys.Divide)
        { return true; } else { return false; }
    }
    public static INPUT[] AddString(string str)
    {
        List<INPUT> result = new List<INPUT>();
        char[] inputs = str.ToCharArray();
        List<UInt16> scans = new List<UInt16>();
        foreach (char c in inputs)
        {
            scans.Add(c);
        }
        foreach (var s in scans)
        {
            INPUT down = new INPUT
            {
                Type = INPUT_KEYBOARD,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = 0,
                        Flags = KEYEVENTF_UNICODE,
                        Scan = s,
                        ExtraInfo = IntPtr.Zero,
                        Time = 0
                    }
                }
            };
            INPUT up = new INPUT
            {
                Type = INPUT_KEYBOARD,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = 0,
                        Flags = KEYEVENTF_UNICODE | KEYEVENTF_KEYUP,
                        Scan = s,
                        ExtraInfo = IntPtr.Zero,
                        Time = 0
                    }
                }
            };
            result.Add(down);
            result.Add(up);
        }
        return result.ToArray();
    }
    public static void MakeInput(INPUT[] inputs)
    {
        SendInput((UInt32)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
    #endregion
    #region DLL
    [DllImport("user32.dll", SetLastError = true)]
    static extern UInt32 SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);
    #endregion
}
