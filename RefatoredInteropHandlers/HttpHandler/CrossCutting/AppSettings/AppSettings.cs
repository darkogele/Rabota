using System;
using System.ComponentModel;
using System.Configuration;

namespace CrossCutting.AppSettings
{
    public static class AppSettings
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) throw new Exception("Config key " + key + " doesn't exists.");

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }
    }
}
