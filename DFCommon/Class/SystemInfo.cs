using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using System.IO;

namespace DFCommon.Class
{
    public class SystemInfo
    {
        private static SystemInfo instance = null;
        private static readonly object syncRoot = new object();

        public string ZYWKIPPORT
        {
            get { return "http://192.168.11.157:7004/"; }
        }

        private string sysytemType = "2D3D";
        private string systemFullName = "";
        private string systemId = "";
        private string systemName = "";
        private string location = "";
        private string version = "";
        public static SystemInfo Instance
        {
            get
            {
                if (SystemInfo.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (SystemInfo.instance == null)
                        {
                            SystemInfo.instance = new SystemInfo();
                        }
                    }
                }
                return SystemInfo.instance;
            }
        }

        public string SystemType
        {
            get
            {
                try
                {
                    sysytemType = Config.GetConfigValue("SystemType");
                    return sysytemType;
                }
                catch (Exception ex)
                {
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                    return sysytemType;
                }
            }
        }

        public string SystemFullName
        {
            get
            {
                if (systemFullName != null && systemFullName != "") return systemFullName;
                systemFullName = "中冶数码—地理信息系统";
                try
                {
                    if (SystemName != "")
                    {
                        return (systemFullName = Location + systemName);
                    }
                    else return systemFullName;
                }
                catch (Exception ex)
                {
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                    return systemFullName;
                }
            }
        }

        public string SystemID
        {
            get { return this.systemId; }
            set { this.systemId = value; }
        }

        public string SystemName
        {
            get
            {
                try
                {
                    systemName = Config.GetConfigValue("SystemName");
                    return systemName;
                }
                catch (Exception ex)
                {
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                    return systemName;
                }
            }
        }

        public string Location
        {
            get
            {
                try
                {
                    location = Config.GetConfigValue("Location");
                    return location;
                }
                catch (Exception ex)
                {
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                    return location;
                }
            }
        }

        public string Version
        {
            get { return this.version; }
            set { this.version = value; }
        }

        public string TextColor
        {
            get
            {
                string temp = "0xFF00FFFF";
                bool b = false;
                try
                {
                    uint u = Convert.ToUInt32(Config.GetConfigValue("TextColor"), 16);
                    b = true;
                }
                catch (Exception ex)
                {
                    b = false;
                }
                if (b) return Config.GetConfigValue("TextColor");
                else return temp;
            }
        }
        public int TextSize
        {
            get
            {
                string ts = Config.GetConfigValue("TextSize");
                int textSize = 11;
                bool bRes = int.TryParse(ts, out textSize);
                if (bRes) return textSize;
                else return 11;
            }
        }
        public int SymbolSize
        {
            get
            {
                string ss = Config.GetConfigValue("SymbolSize");
                int symbolSize = 30;
                bool bRes = int.TryParse(ss, out symbolSize);
                if (bRes) return symbolSize;
                else return 30;
            }
        }

        public string FillColor
        {
            get
            {
                string temp = "0xAAFFFF00";
                bool b = false;
                try
                {
                    uint u = Convert.ToUInt32(Config.GetConfigValue("FillColor"), 16);
                    b = true;
                }
                catch (Exception ex)
                {
                    b = false;
                }
                if (b) return Config.GetConfigValue("FillColor");
                else return temp;
            }
        }
        public string LineColor
        {
            get
            {
                string temp = "0xFF00FFFF";
                bool b = false;
                try
                {
                    uint u = Convert.ToUInt32(Config.GetConfigValue("LineColor"), 16);
                    b = true;
                }
                catch (Exception ex)
                {
                    b = false;
                }
                if (b) return Config.GetConfigValue("LineColor");
                else return temp;
            }
        }
        public string HighlightColor
        {
            get
            {
                string temp = "0xDDFF00FF";
                bool b = false;
                try
                {
                    uint u = Convert.ToUInt32(Config.GetConfigValue("HighlightColor"), 16);
                    b = true;
                }
                catch (Exception ex)
                {
                    b = false;
                }
                if (b) return Config.GetConfigValue("HighlightColor");
                else return temp;
            }
        }

        public string LocalDataPath
        {
            get
            {
                string localDataPath = Config.GetConfigValue("LocalDataPath");
                if (string.IsNullOrEmpty(localDataPath))
                {
                    localDataPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\LocalData\\" + systemFullName + "\\");
                    Config.SetConfigValue("LocalDataPath", localDataPath);
                }
                if (!Directory.Exists(localDataPath))
                {
                    Directory.CreateDirectory(localDataPath);
                }
                return localDataPath;
            }
        }
    }
}
