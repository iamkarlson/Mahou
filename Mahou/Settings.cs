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
        /* Starting with v1.0.4.0 this is no more usable,
         * there is new ability "Convert Line"
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool SpaceBreak
        {
            get { return (bool)this["SpaceBreak"]; }
            set { this["SpaceBreak"] = (bool)value; }
        }*/
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
        [DefaultSettingValue("None")]
        public string HKCLMods // Hot Key Convert Last Modifiers
        {
            get { return (string)this["HKCLMods"]; }
            set { this["HKCLMods"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("19")] //Pause Key
        public int HKCLKey // Hot Key Convert Last Key
        {
            get { return (int)this["HKCLKey"]; }
            set { this["HKCLKey"] = (int)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("None")]
        public string HKCSMods // Hot Key Convert Selection Modifiers
        {
            get { return (string)this["HKCSMods"]; }
            set { this["HKCSMods"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("145")] // Scroll Key
        public int HKCSKey // Hot Key Convert Selection Key
        {
            get { return (int)this["HKCSKey"]; }
            set { this["HKCSKey"] = (int)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("Shift")]
        public string HKCLineMods // Hot Key Convert Line Modifiers
        {
            get { return (string)this["HKCLineMods"]; }
            set { this["HKCLineMods"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("19")] //Pause Key
        public int HKCLineKey // Hot Key Convert Line Key
        {
            get { return (int)this["HKCLineKey"]; }
            set { this["HKCLineKey"] = (int)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool SwitchLayoutByCaps
        {
            get { return (bool)this["SwitchLayoutByCaps"]; }
            set { this["SwitchLayoutByCaps"] = (bool)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool CycleMode //Cycle mode for convert last, caps switch
        {
            get { return (bool)this["CycleMode"]; }
            set { this["CycleMode"] = (bool)value; }
        }
    }
}
