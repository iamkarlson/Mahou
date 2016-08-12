using System;
using System.Configuration;

namespace Mahou
{
    class Settings : ApplicationSettingsBase
    {
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool IconVisibility
        {
            get { return (bool)this["IconVisibility"]; }
            set { this["IconVisibility"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        public uint locale1uId
        {
            get { return (uint)this["locale1uId"]; }
            set { this["locale1uId"] = (uint)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        public uint locale2uId
        {
            get { return (uint)this["locale2uId"]; }
            set { this["locale2uId"] = (uint)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        public string locale1Lang //Locale 1 name
        {
            get { return (string)this["locale1Lang"]; }
            set { this["locale1Lang"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        public string locale2Lang //Locale 2 name
        {
            get { return (string)this["locale2Lang"]; }
            set { this["locale2Lang"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool HKCLEnabled // Hotkey Convert Last enabled?
        {
            get { return (bool)this["HKCLEnabled"]; }
            set { this["HKCLEnabled"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("None")]
        public string HKCLMods // Hotkey Convert Last Modifiers
        {
            get { return (string)this["HKCLMods"]; }
            set { this["HKCLMods"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("19")] //Pause Key
        public int HKCLKey // Hotkey Convert Last Key
        {
            get { return (int)this["HKCLKey"]; }
            set { this["HKCLKey"] = (int)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool HKCSEnabled // Hotkey Convert Selection enabled?
        {
            get { return (bool)this["HKCSEnabled"]; }
            set { this["HKCSEnabled"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("None")]
        public string HKCSMods // Hotkey Convert Selection Modifiers
        {
            get { return (string)this["HKCSMods"]; }
            set { this["HKCSMods"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("145")] // Scroll Key
        public int HKCSKey // Hotkey Convert Selection Key
        {
            get { return (int)this["HKCSKey"]; }
            set { this["HKCSKey"] = (int)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool HKCLineEnabled // Hotkey Convert Line enabled?
        {
            get { return (bool)this["HKCLineEnabled"]; }
            set { this["HKCLineEnabled"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("Shift")]
        public string HKCLineMods // Hotkey Convert Line Modifiers
        {
            get { return (string)this["HKCLineMods"]; }
            set { this["HKCLineMods"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("19")] //Pause Key
        public int HKCLineKey // Hotkey Convert Line Key
        {
            get { return (int)this["HKCLineKey"]; }
            set { this["HKCLineKey"] = (int)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("CapsLock")] // Default *only* key
        public string OnlyKeyLayoutSwicth
        {
            get { return (string)this["OnlyKeyLayoutSwicth"]; }
            set { this["OnlyKeyLayoutSwicth"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool CycleMode //Cycle mode for convert last/line, Switch by key
        {
            get { return (bool)this["CycleMode"]; }
            set { this["CycleMode"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool EmulateLayoutSwitch //Emulate Layout Switch for Cycle mode
        {
            get { return (bool)this["EmulateLayoutSwitch"]; }
            set { this["EmulateLayoutSwitch"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool SwitchLayoutInCS //Enables layout switchin for Convert Selection
        {
            get { return (bool)this["SwitchLayoutInCS"]; }
            set { this["SwitchLayoutInCS"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool BlockCTRL //Block Ctrl when active
        {
            get { return (bool)this["BlockCTRL"]; }
            set { this["BlockCTRL"] = (bool)value; }
        }
    }
}
