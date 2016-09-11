using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Mahou
{
    public static class NativeClipboard
    {
        #region DLL Imports/Constants
        [DllImport("user32.dll")]
        static extern IntPtr GetClipboardData(uint uFormat);
        [DllImport("user32.dll")]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EmptyClipboard();
        [DllImport("user32.dll")]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool CloseClipboard();
        [DllImport("user32.dll")]
        public static extern bool IsClipboardFormatAvailable(uint format);
        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalUnlock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);
        [DllImport("kernel32.dll")]
        public static extern UIntPtr GlobalSize(IntPtr hMem);
        [DllImport("kernel32.dll")]
        static extern uint EnumClipboardFormats(uint format);
        public const uint GMEM_DDESHARE = 0x2000;
        public const uint GMEM_MOVEABLE = 0x2;
        public enum uFormat
        {
            CF_TEXT = 1,
            CF_BITMAP = 2,
            CF_SYLK = 4,
            CF_DIF = 5,
            CF_TIFF = 6,
            CF_OEMTEXT = 7,
            CF_DIB = 8,
            CF_PALETTE = 9,
            CF_PENDATA = 10,
            CF_RIFF = 11,
            CF_WAVE = 12,
            CF_UNICODETEXT = 13
        }
        #endregion
        public static void Clear() // Clears Clipboard
        {
            OpenClipboard(IntPtr.Zero);
            EmptyClipboard();
            CloseClipboard();
        }
        public static string GetText() // Gets text data from clipboard
        {
            if (!IsClipboardFormatAvailable((uint)uFormat.CF_UNICODETEXT))
                return null;
            int Tries = 0;
            var opened = false;
            string data = null;
            while (true)
            {
                ++Tries;
                opened = OpenClipboard(IntPtr.Zero);
                var hGlobal = GetClipboardData((uint)uFormat.CF_UNICODETEXT);
                var lpwcstr = GlobalLock(hGlobal);
                data = Marshal.PtrToStringUni(lpwcstr);
                if (opened)
                {
                    GlobalUnlock(hGlobal);
                    break;
                }
                System.Threading.Thread.Sleep(1);
            }
            CloseClipboard();
            return data;
        }
        public static ClipboardData GetClipboardDatas() // Gets all clipboard datas, but only text-based datas supported...
        {
            ClipboardData cd = new ClipboardData()
            {
                data = new List<byte[]>(),
                format = new List<uint>()
            };
            OpenClipboard(IntPtr.Zero);
            foreach (var fmt in (uint[])Enum.GetValues(typeof(uFormat)))
            {
                IntPtr pos = GetClipboardData(fmt);
                if (pos == IntPtr.Zero)
                    continue;
                UIntPtr lenght = GlobalSize(pos);
                IntPtr gLock = GlobalLock(pos);
                //Console.WriteLine(fmt + " is awaible in clipboard!!");
                byte[] data;
                if ((uint)lenght > 0)
                {
                    //Init a buffer which will contain the clipboard data
                    data = new byte[(uint)lenght];
                    //Console.WriteLine(lenght);
                    int l = Convert.ToInt32(lenght.ToString());
                    //Copy data from clipboard to our byte[] buffer
                    Marshal.Copy(gLock, data, 0, l);
                }
                else
                {
                    data = new byte[0];
                }
                cd.data.Add(data);
                cd.format.Add(fmt);
            }
            CloseClipboard();
            return cd;
        }
        public static void RestoreData(ClipboardData datas) // Places all datas to clipboard, but only text-based datas supported...
        {
            OpenClipboard(IntPtr.Zero);
            EmptyClipboard();
            for (int i = 0; i != datas.data.Count; i++)
            {
                var data = datas.data[i];
                //foreach (var d in data)
                //{
                //    Console.WriteLine("|"+d);
                //}
                //Console.WriteLine(data.GetLength(0));
                IntPtr alloc = GlobalAlloc(GMEM_MOVEABLE | GMEM_DDESHARE, new UIntPtr(Convert.ToUInt32(data.GetLength(0))));
                var glock = GlobalLock(alloc);
                var fmt = datas.format[i];
                Marshal.Copy(data, 0, glock, data.GetLength(0));
                GlobalUnlock(alloc);
                SetClipboardData(fmt, alloc);
            }
            CloseClipboard();
        }
        public struct ClipboardData // Struct of List of byte[](data) and uint(data format)
        {
            public List<byte[]> data;
            public List<uint> format;
        }
    }
}
