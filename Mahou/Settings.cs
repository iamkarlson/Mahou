using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public string locale1Lang
        {
            get { return (string)this["locale1Lang"]; }
            set { this["locale1Lang"] = (string)value; }
        }
        [SettingsProvider(typeof(PortableSettingsProvider))]
        [UserScopedSetting()]
        public string locale2Lang
        {
            get { return (string)this["locale2Lang"]; }
            set { this["locale2Lang"] = (string)value; }
        }
    }
}
