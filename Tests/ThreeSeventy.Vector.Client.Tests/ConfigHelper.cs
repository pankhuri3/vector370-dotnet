using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace ThreeSeventy.Vector.Client.Tests
{
    public static class ConfigHelper
    {
        public static string GetValue(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
                throw new Exception(String.Format("Configuration {0} not found!", name));

            return value;
        }

        public static T GetValue<T>(string name)
            where T : IConvertible
        {
            string value = GetValue(name);

            var tc = TypeDescriptor.GetConverter(typeof(T));

            return (T)tc.ConvertFromString(value);
        }
    }
}
