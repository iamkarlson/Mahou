using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
class KInputs
{
    #region Native Win32
    public const int INPUT_KEYBOARD = 1;
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
    public static INPUT[] AddString(string str, bool down)
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
            INPUT input = new INPUT
            {
                Type = INPUT_KEYBOARD,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = 0,
                        Flags = down ? KEYEVENTF_UNICODE : (KEYEVENTF_UNICODE | KEYEVENTF_KEYUP),
                        Scan = s,
                        ExtraInfo = IntPtr.Zero,
                        Time = 0
                    }
                }
            };
            result.Add(input);
        }
        return result.ToArray();
    }
    public static void MakeInput(INPUT[] inputs, bool shift)
    {
        if (shift)
        {
            SendInput(1, new INPUT[] { AddKey(Keys.ShiftKey, true) }, Marshal.SizeOf(typeof(INPUT)));
        }
        foreach (INPUT INPT in inputs)
        {
            SendInput(1, new INPUT[] { INPT }, Marshal.SizeOf(typeof(INPUT)));
            //Send UP for last input
            //without below, input will skip repeats(aaaaa => a, 00100000 => 010 etc.) 
            INPUT sendlastUP = new INPUT { Type = INPT.Type, Data = INPT.Data };
            sendlastUP.Data.Keyboard.Flags = (KEYEVENTF_UNICODE | KEYEVENTF_KEYUP);
            SendInput(1, new INPUT[] {sendlastUP}, Marshal.SizeOf(typeof(INPUT)));
        }
        SendInput(1, new INPUT[] { AddKey(Keys.ShiftKey, false) }, Marshal.SizeOf(typeof(INPUT)));
    }
    #endregion
    #region DLL
    [DllImport("user32.dll", SetLastError = true)]
    static extern UInt32 SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);
    #endregion
}
