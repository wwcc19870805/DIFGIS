using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using ICSharpCode.Core;

namespace DFCommon.Class
{
    public class Config
    {
        // Fields
        private static System.Configuration.Configuration _cfgInfo;
        private static string _systemConfigPath;
        // Methods
        public static System.Configuration.Configuration GetConfig()
        {
            ReadDllConfigFile();
            return _cfgInfo;
        }

        public static void SetConfigPath(string path)
        {
            _systemConfigPath = path;
        }

        public static string GetConfigValue(string name)
        {
            try
            {
                ReadDllConfigFile();
                if (Array.IndexOf<string>(_cfgInfo.AppSettings.Settings.AllKeys, name) == -1)
                {
                    _cfgInfo.AppSettings.Settings.Add(name, string.Empty);
                    //_cfgInfo.Save();
                }
                return _cfgInfo.AppSettings.Settings[name].Value;
            }
            catch(Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                return "";
            }
        }

        private static void ReadDllConfigFile()
        {
            if (_cfgInfo == null)
            {
                string str;
                if (string.IsNullOrEmpty(_systemConfigPath))
                    str = Application.StartupPath + @"\..\Config\System.config";
                else str = _systemConfigPath;
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = str
                };
                _cfgInfo = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            }
        }

        public static void SetConfigValue(string name, string value)
        {
            try
            {
                ReadDllConfigFile();
                if (Array.IndexOf<string>(_cfgInfo.AppSettings.Settings.AllKeys, name) == -1)
                {
                    _cfgInfo.AppSettings.Settings.Add(name, value);
                }
                else
                {
                    _cfgInfo.AppSettings.Settings[name].Value = value;
                }
                _cfgInfo.Save();
            }
            catch(Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
