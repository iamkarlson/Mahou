using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading.Tasks;

namespace Mahou
{
    class Configs
    {
        //Path where Mahou is now + configs.ini
        private string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Mahou.ini");
        public Configs()//Initializes settings, if some of elements or settinhs file, not exists it creates them with default value
        {
            if (!File.Exists(filePath)) //Create an UTF-16 configuration file
            {
                File.WriteAllText(filePath, "!Unicode(✔), Mahou settings file", Encoding.Unicode);
            }
            int it = 0;      //int temp
            uint uit = 0;    //uint temp
            bool bt = false; //bool temp
            //Hotkeys section
            if (!Int32.TryParse(this.Read("Hotkeys", "HKCLKey"), out it))
                this.Write("Hotkeys", "HKCLKey", "19"); //Hotkey convert last word

            if (String.IsNullOrEmpty(this.Read("Hotkeys", "HKCLMods")))
                this.Write("Hotkeys", "HKCLMods", "None"); //Hotkey convert last word modifiers

            if (!Int32.TryParse(this.Read("Hotkeys", "HKCSKey"), out it))
                this.Write("Hotkeys", "HKCSKey", "145"); //Hotkey convert selection

            if (String.IsNullOrEmpty(this.Read("Hotkeys", "HKCSMods")))
                this.Write("Hotkeys", "HKCSMods", "None"); //Hotkey convert selection modifiers

            if (!Int32.TryParse(this.Read("Hotkeys", "HKCLineKey"), out it))
                this.Write("Hotkeys", "HKCLineKey", "19"); //Hotkey convert line

            if (String.IsNullOrEmpty(this.Read("Hotkeys", "HKCLineMods"))) //Hotkey convert line modifiers
                this.Write("Hotkeys", "HKCLineMods", "Shift");

            if (String.IsNullOrEmpty(this.Read("Hotkeys", "OnlyKeyLayoutSwicth")))
                this.Write("Hotkeys", "OnlyKeyLayoutSwicth", "CapsLock"); //One key to switch layout

            //Locales section
            if (!UInt32.TryParse(this.Read("Locales", "locale1uId"), out uit))
                this.Write("Locales", "locale1uId", ""); //Locale 1 id

            if (String.IsNullOrEmpty(this.Read("Locales", "locale1Lang")))
                this.Write("Locales", "locale1Lang", ""); //Locale 1 name

            if (!UInt32.TryParse(this.Read("Locales", "locale2uId"), out uit))
                this.Write("Locales", "locale2uId", ""); //Locale 2 id

            if (String.IsNullOrEmpty(this.Read("Locales", "locale2Lang")))
                this.Write("Locales", "locale2Lang", ""); //Locale 2 name

            //Functions section
            if (!Boolean.TryParse(this.Read("Functions", "IconVisibility"), out bt))
                this.Write("Functions", "IconVisibility", "true"); //Tray icon visibility

            if (!Boolean.TryParse(this.Read("Functions", "CycleMode"), out bt))
                this.Write("Functions", "CycleMode", "false");

            if (!Boolean.TryParse(this.Read("Functions", "EmulateLayoutSwitch"), out bt))
                this.Write("Functions", "EmulateLayoutSwitch", "false");

            if (!Boolean.TryParse(this.Read("Functions", "SwitchLayoutInCS"), out bt))
                this.Write("Functions", "SwitchLayoutInCS", "true");

            if (!Boolean.TryParse(this.Read("Functions", "BlockCTRL"), out bt))
                this.Write("Functions", "BlockCTRL", "false");

            if (!Boolean.TryParse(this.Read("Functions", "RePress"), out bt))
                this.Write("Functions", "RePress", "false");

            //EnabledHotkeys section
            if (!Boolean.TryParse(this.Read("EnabledHotkeys", "HKCLEnabled"), out bt))
                this.Write("EnabledHotkeys", "HKCLEnabled", "true"); //Hotkey convert last word enabled

            if (!Boolean.TryParse(this.Read("EnabledHotkeys", "HKCSEnabled"), out bt))
                this.Write("EnabledHotkeys", "HKCSEnabled", "true"); //Hotkey convert selection enabled

            if (!Boolean.TryParse(this.Read("EnabledHotkeys", "HKCLineEnabled"), out bt))
                this.Write("EnabledHotkeys", "HKCLineEnabled", "true"); //Hotkey convert line enabled

        }
        public void Write(string section, string key, string value) //Writes "value" to "key" in "section"
        {
            WritePrivateProfileString(section, key, value, this.filePath);
        }
        public string Read(string section, string key) //Returns "key" value in "section" as string
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
            return SB.ToString();
        }
        public int ReadInt(string section, string key) //Returns "key" value in "section" as int
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
            return Int32.Parse(SB.ToString());
        }
        public bool ReadBool(string section, string key) //Returns "key" value in "section" as bool
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
            return Boolean.Parse(SB.ToString().ToLower());
        }
        #region Dll imports
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion
    }
}
