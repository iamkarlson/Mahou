using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
namespace Mahou
{
    public static class NativeClipboard
    {
        #region DLL Imports
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
        static extern bool IsClipboardFormatAvailable(uint format);
        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalUnlock(IntPtr hMem);
        #endregion
        enum uFormat
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
        //TODO: Make Clipboard restore work...

        //static extern uint EnumClipboardFormats(uint format);
        //[DllImport("kernel32.dll")]
        //public static ClipboardData GetClipboardDatas()
        //{
        //    ClipboardData cd = new ClipboardData() {
        //        data = new List<IntPtr>(),
        //        format = new List<uint>() };
        //    OpenClipboard((IntPtr)0);
        //    foreach (var fmt in (uint[])Enum.GetValues(typeof(uFormat)))
        //    {
        //        if(IsClipboardFormatAvailable(fmt))
        //        {
        //            Console.WriteLine(fmt + " is awaible in clipboard!!");
        //            var hGlobal = GetClipboardData(fmt);
        //            cd.data.Add(hGlobal);
        //            cd.format.Add(fmt);
        //        }
        //    }
        //    CloseClipboard();
        //    return cd;
        //}
        //public static void RestoreData(ClipboardData datas)
        //{
        //    OpenClipboard((IntPtr)0);
        //    for (int i = 0; i != datas.data.Count; i++)
        //    {
        //        var data = datas.data[i];
        //        var glock = GlobalLock(data);
        //        var fmt = datas.format[i];
        //        Console.WriteLine(Marshal.PtrToStringUni(glock));
        //        Console.WriteLine("data=" + data + "\tformat=" + fmt);
        //        byte[] buf = new byte[512];
        //        Marshal.Copy(buf,
        //    }
        //    CloseClipboard();
        //}
        //public struct ClipboardData
        //{
        //    public List<IntPtr> data;
        //    public List<uint> format;
        //}
        public static void Clear()
        {
            OpenClipboard((IntPtr)0);
            EmptyClipboard();
            CloseClipboard();
        }
        public static string GetText()
        {
            if (!IsClipboardFormatAvailable((uint)uFormat.CF_UNICODETEXT))
                return null;
            int Tries = 0;
            var opened = false;
            string data = null;
            while (true)
            {
                ++Tries;
                opened = OpenClipboard((IntPtr)0);
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
    }
}
