using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.Core;
using System.Xml;
using DFCommon.Class;

namespace DFDataConfig.Class
{
    public class FacilityClassManager
    {
        private static FacilityClassManager instance = null;
        private static readonly object syncRoot = new object();
        private List<FacilityClass> _listFacilityClass;
        private FacilityClassManager()
        {
            _listFacilityClass = new List<FacilityClass>();
            ReadConfig();
        }

        private void ReadConfig()
        {
            if (_listFacilityClass != null && _listFacilityClass.Count > 0)
            {
                return;
            }
            try
            {
                var xmlFilePath = Config.GetConfigValue("FacilityClassXmlPath");
                if (!System.IO.File.Exists(xmlFilePath))
                {
                    return;
                }
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                if (xmlDoc == null)
                {
                    return;
                }
                var root = xmlDoc.SelectSingleNode("FacilityClassManager");
                if (root == null)
                {
                    return;
                }
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name != "FacilityClass")
                    {
                        continue;
                    }
                    if (node.Attributes["name"] == null)
                    {
                        continue;
                    }
                    var name = string.Empty;
                    var alias = string.Empty;
                    if (node.Attributes["name"] != null)
                    {
                        name = node.Attributes["name"].Value.Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }
                    }
                    if (node.Attributes["alias"] != null)
                    {
                        alias = node.Attributes["alias"].Value.Trim();
                    }
                    var fc = new FacilityClass(name, alias);
                    if (node.Attributes["fc2D"] != null)
                    {
                        fc.Fc2D = node.Attributes["fc2D"].Value.Trim();
                    }
                    if (node.Attributes["fc3D"] != null)
                    {
                        fc.Fc3D = node.Attributes["fc3D"].Value.Trim();
                    }
                    foreach (XmlNode cnode in node.ChildNodes)
                    {
                        if (cnode.Name != "StdField")
                        {
                            continue;
                        }
                        if (cnode.Attributes["name"] == null)
                        {
                            continue;
                        }
                        var fieldName = string.Empty;
                        var fieldAliasName = string.Empty;
                        var dataType = string.Empty;
                        var fieldSystemName = string.Empty;
                        var fieldSystemAliasName = string.Empty;
                        var fieldCanQuery = string.Empty;
                        var fieldNeedCheck = string.Empty;
                        var fieldCanStats = string.Empty;
                        if (cnode.Attributes["name"] != null)
                        {
                            fieldName = cnode.Attributes["name"].Value.Trim();
                            if (string.IsNullOrEmpty(fieldName))
                            {
                                continue;
                            }
                        }
                        if (cnode.Attributes["alias"] != null)
                        {
                            fieldAliasName = cnode.Attributes["alias"].Value.Trim();
                        }
                        if (cnode.Attributes["datatype"] != null)
                        {
                            dataType = cnode.Attributes["datatype"].Value.Trim();
                        }
                        if (cnode.Attributes["systemname"] != null)
                        {
                            fieldSystemName = cnode.Attributes["systemname"].Value.Trim();
                        }
                        if (cnode.Attributes["systemalias"] != null)
                        {
                            fieldSystemAliasName = cnode.Attributes["systemalias"].Value.Trim();
                        }
                        if (cnode.Attributes["canquery"] != null)
                        {
                            fieldCanQuery = cnode.Attributes["canquery"].Value.Trim();
                        }
                        if (cnode.Attributes["needcheck"] != null)
                        {
                            fieldNeedCheck = cnode.Attributes["needcheck"].Value.Trim();
                        }
                        if (cnode.Attributes["canstats"] != null)
                        {
                            fieldCanStats = cnode.Attributes["canstats"].Value.Trim();
                        }
                        var bCanQuery = false;
                        if (!string.IsNullOrEmpty(fieldCanQuery) && fieldCanQuery == "true")
                        {
                            bCanQuery = true;
                        }
                        var bNeedCheck = false;
                        if (!string.IsNullOrEmpty(fieldNeedCheck) && fieldNeedCheck == "true")
                        {
                            bNeedCheck = true;
                        }
                        var bCanStats = false;
                        if (!string.IsNullOrEmpty(fieldCanStats) && fieldCanStats == "true")
                        {
                            bCanStats = true;
                        }
                        var fi = new FieldInfo(fieldName, fieldAliasName, fieldSystemName, fieldSystemAliasName, bCanQuery, bNeedCheck, bCanStats, dataType);
                        fc.AddFieldInfo(fi);
                    }
                    Add(fc);
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static FacilityClassManager Instance
        {
            get
            {
                if (FacilityClassManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FacilityClassManager.instance == null)
                        {
                            FacilityClassManager.instance = new FacilityClassManager();
                        }
                    }
                }
                return FacilityClassManager.instance;
            }
        }

        public bool Exists(string name)
        {
            foreach (FacilityClass fc in _listFacilityClass)
            {
                if (fc.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Exists(FacilityClass facilityClass)
        {
            foreach (FacilityClass fc in _listFacilityClass)
            {
                if (fc.Name == facilityClass.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add(FacilityClass facilityClass)
        {
            if (Exists(facilityClass))
            {
                return;
            }
            _listFacilityClass.Add(facilityClass);
        }

        public FacilityClass GetFacilityClassByName(string name)
        {
            foreach (FacilityClass fc in _listFacilityClass)
            {
                if (fc.Name == name)
                {
                    return fc;
                }
            }
            return null;
        }

        public List<FacilityClass> GetAllFacilityClass()
        {
            return _listFacilityClass;
        }
    }
}
