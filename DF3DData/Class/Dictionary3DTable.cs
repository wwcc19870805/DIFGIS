using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFDataConfig.Class;
using DFCommon.Class;
using System.IO;
using System.Xml;

namespace DF3DData.Class
{
    public class Dictionary3DTable
    {
        private static Dictionary3DTable instance = null;
        private static readonly object syncRoot = new object();
        private Dictionary<string, string> dict;

        private Dictionary3DTable()
        {
            dict = new Dictionary<string, string>();
            ReadConfig();
        }

        public static Dictionary3DTable Instance
        {
            get
            {
                if (Dictionary3DTable.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Dictionary3DTable.instance == null)
                        {
                            Dictionary3DTable.instance = new Dictionary3DTable();
                        }
                    }
                }
                return Dictionary3DTable.instance;
            }
        }

        private void ReadConfig()
        {
            try
            {
                string xmlFilePath = Config.GetConfigValue("FacilityClassXmlPath");
                if (!System.IO.File.Exists(xmlFilePath)) return;
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                if (doc == null) return;
                XmlNode root = doc.SelectSingleNode("FacilityClassManager");
                if (root == null) return;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name != "FacilityClass") continue;
                    if (node.Attributes["name"] == null || node.Attributes["fc3D"] == null) continue;
                    string fcName = node.Attributes["name"].Value;
                    string fc3D = node.Attributes["fc3D"].Value;
                    if (string.IsNullOrEmpty(fcName) || string.IsNullOrEmpty(fc3D)) continue;
                    string[] idArr = fc3D.Split(';');
                    if (idArr == null || idArr.Length == 0) return;
                    foreach (string id in idArr)
                    {
                        if (string.IsNullOrEmpty(id)) continue;
                        dict[id] = fcName;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public string GetFacilityClassNameByDFFeatureClassID(string id)
        {
            foreach (KeyValuePair<string, string> kv in dict)
            {
                if (kv.Key == id) return kv.Value;
            }
            return "";
        }

        public FacilityClass GetFacilityClassByDFFeatureClassID(string id)
        {
            foreach (KeyValuePair<string, string> kv in dict)
            {
                if (kv.Key == id)
                {
                    return FacilityClassManager.Instance.GetFacilityClassByName(kv.Value);
                }
            }
            return null;
        }

        public List<DF3DFeatureClass> GetFeatureClassByFacilityClass(FacilityClass fc)
        {
            List<DF3DFeatureClass> list = new List<DF3DFeatureClass>();
            foreach (DF3DFeatureClass temp in DF3DFeatureClassManager.Instance.GetAllFeatureClass())
            {
                if (temp.GetFacilityClass() == fc)
                {
                    list.Add(temp);
                }
            }
            return list;
        }

        public List<DF3DFeatureClass> GetFeatureClassByFacilityClassName(string name)
        {
            List<DF3DFeatureClass> list = new List<DF3DFeatureClass>();
            foreach (DF3DFeatureClass temp in DF3DFeatureClassManager.Instance.GetAllFeatureClass())
            {
                if (temp.GetFacilityClassName() == name)
                {
                    list.Add(temp);
                }
            }
            return list;
        }

        public List<DF3DFeatureClass> GetFeatureClassByFacilityClassName(string[] names)
        {
            List<DF3DFeatureClass> list = new List<DF3DFeatureClass>();
            foreach (DF3DFeatureClass temp in DF3DFeatureClassManager.Instance.GetAllFeatureClass())
            {
                if (names.Contains<string>(temp.GetFacilityClassName()))
                {
                    list.Add(temp);
                }
            }
            return list;
        }
    }
}
