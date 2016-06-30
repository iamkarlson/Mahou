using System;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
namespace Mahou
{
    public class HotkeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int modifier;
        private int key;
        private IntPtr hWnd;
        private int id;
        public HotkeyHandler(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
        }
        public bool Register()
        {
            System.Diagnostics.Debug.WriteLine("HotKey with id " + id + " registered."); 
            return RegisterHotKey(hWnd, id, modifier, key);
        }
        public bool Unregister()
        {
            System.Diagnostics.Debug.WriteLine("HotKey with id " + id + " unregistered."); 
            return UnregisterHotKey(hWnd, id);
        }
        public override int GetHashCode()
        {
            return modifier ^ key ^ hWnd.ToInt32();
        }

    }
}
