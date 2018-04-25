using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFDataConfig.Class;
using DFCommon.Class;
using System.Xml;

namespace DF2DData.Class
{
    public class Dictionary2DTable
    {
        private static Dictionary2DTable instance = null;
        private static readonly object syncRoot = new object();
        private Dictionary<string, string> dict;

        private Dictionary2DTable()
        {
            dict = new Dictionary<string, string>();
            ReadConfig();
        }

        public static Dictionary2DTable Instance
        {
            get
            {
                if (Dictionary2DTable.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Dictionary2DTable.instance == null)
                        {
                            Dictionary2DTable.instance = new Dictionary2DTable();
                        }
                    }
                }
                return Dictionary2DTable.instance;
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
                    if (node.Attributes["name"] == null || node.Attributes["fc2D"] == null) continue;
                    string fcName = node.Attributes["name"].Value;
                    string fc2D = node.Attributes["fc2D"].Value;
                    if (string.IsNullOrEmpty(fcName) || string.IsNullOrEmpty(fc2D)) continue;
                    string[] idArr = fc2D.Split(';');
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
                   FacilityClass fc =   FacilityClassManager.Instance.GetFacilityClassByName(kv.Value);
                   return fc;
                }
            }
            return null;
        }

        public List<DF2DFeatureClass> GetFeatureClassByFacilityClass(FacilityClass fc)
        {
            List<DF2DFeatureClass> list = new List<DF2DFeatureClass>();
            foreach (DF2DFeatureClass temp in DF2DFeatureClassManager.Instance.GetAllFeatureClass())
            {
                if (temp.GetFacilityClass() == fc)
                {
                    list.Add(temp);
                }
            }
            return list;
        }

        public List<DF2DFeatureClass> GetFeatureClassByFacilityClassName(string name)
        {
            List<DF2DFeatureClass> list = new List<DF2DFeatureClass>();
            foreach (DF2DFeatureClass temp in DF2DFeatureClassManager.Instance.GetAllFeatureClass())
            {
                if (temp.GetFacilityClassName() == name)
                {
                    list.Add(temp);
                }
            }
            return list;

        }

        public List<DF2DFeatureClass> GetFeatureClassByFacilityClassName(string[] names)
        {
            List<DF2DFeatureClass> list = new List<DF2DFeatureClass>();
            foreach (DF2DFeatureClass temp in DF2DFeatureClassManager.Instance.GetAllFeatureClass())
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
