using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DFCommon.Class;

namespace DFDataConfig.Logic
{
    public class LogicDataStructureManage2D
    {
        private static LogicDataStructureManage2D instance = null;
        private static readonly object syncRoot = new object();
        private List<LogicGroup> _rootLogicGroups;
        private List<MajorClass> _rootMajorClasses;
        public List<LogicGroup> RootLogicGroups
        {
            get { return this._rootLogicGroups; }
        }
        public List<MajorClass> RootMajorClasses
        {
            get { return this._rootMajorClasses; }
        }
        private List<MajorClass> _allMajorClass;
        private List<SubClass> _allSubClass;
        private LogicDataStructureManage2D()
        {
            this._rootLogicGroups = new List<LogicGroup>();
            this._rootMajorClasses = new List<MajorClass>();
            this._allMajorClass = new List<MajorClass>();
            this._allSubClass = new List<SubClass>();
            ReadConfig();
        }

        private void RecursiveReadConfig(XmlNodeList list,LogicGroup parent)
        {
            if (list == null) return;
            foreach (XmlNode node in list)
            {
                if (node.Name == "LogicGroup")
                {
                    string fieldName = "", fieldAliasName = "";
                    if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                    if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                    LogicGroup lg = new LogicGroup(fieldName, fieldAliasName, parent);
                    parent.LogicGroups.Add(lg);
                    RecursiveReadConfig(node.ChildNodes, lg);
                }
                if (node.Name == "MajorClass")
                {
                    string fieldName = "", fieldAliasName = "", classifyFieldName = "", fc2D = "", fc3D = "";
                    if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                    if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                    if (node.Attributes["classifyfield"] != null) classifyFieldName = node.Attributes["classifyfield"].Value.Trim();
                    if (node.Attributes["fc2D"] != null) fc2D = node.Attributes["fc2D"].Value.Trim();
                    if (node.Attributes["fc3D"] != null) fc3D = node.Attributes["fc3D"].Value.Trim();
                    MajorClass mc = new MajorClass(fieldName, fieldAliasName, classifyFieldName, fc2D, fc3D, parent);
                    parent.MajorClasses.Add(mc);
                    this._allMajorClass.Add(mc);
                    foreach (XmlNode cnode in node.ChildNodes)
                    {
                        if (cnode.Name != "SubClass") continue;
                        string scname = "", scgroupid = "";
                        if (cnode.Attributes["name"] != null) { scname = cnode.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(scname)) continue; }
                        if (cnode.Attributes["groupid"] != null) scgroupid = cnode.Attributes["groupid"].Value.Trim();
                        int groupid = 0;
                        if (!int.TryParse(scgroupid, out groupid)) groupid = 0;
                        SubClass sc = new SubClass(scname, groupid, mc);
                        mc.SubClasses.Add(sc);
                        this._allSubClass.Add(sc);
                    }
                }
            }
        }

        private void ReadConfig()
        {
            try
            {
                string xmlFilePath = Config.GetConfigValue("LogicDataStructureXmlPath2D");
                if (!System.IO.File.Exists(xmlFilePath)) return;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                if (xmlDoc == null) return;
                XmlNode root = xmlDoc.SelectSingleNode("LogicDataStructure");
                if (root == null) return;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "LogicGroup")
                    {
                        string fieldName ="",fieldAliasName ="";
                        if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                        if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                        LogicGroup lg = new LogicGroup(fieldName, fieldAliasName, null);
                        this._rootLogicGroups.Add(lg);
                        RecursiveReadConfig(node.ChildNodes, lg);
                    }
                    if (node.Name == "MajorClass")
                    {
                        string fieldName = "", fieldAliasName = "", classifyFieldName = "", fc2D = "", fc3D = "";
                        if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                        if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                        if (node.Attributes["classifyfield"] != null) classifyFieldName = node.Attributes["classifyfield"].Value.Trim();
                        if (node.Attributes["fc2D"] != null) fc2D = node.Attributes["fc2D"].Value.Trim();
                        if (node.Attributes["fc3D"] != null) fc3D = node.Attributes["fc3D"].Value.Trim();
                        MajorClass mc = new MajorClass(fieldName, fieldAliasName, classifyFieldName, fc2D, fc3D, null);
                        this._rootMajorClasses.Add(mc);
                        this._allMajorClass.Add(mc); 
                        foreach (XmlNode cnode in node.ChildNodes)
                        {
                            if (cnode.Name != "SubClass") continue;
                            string scname = "", scgroupid = "";
                            if (cnode.Attributes["name"] != null) { scname = cnode.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(scname)) continue; }
                            if (cnode.Attributes["groupid"] != null) scgroupid = cnode.Attributes["groupid"].Value.Trim();
                            int groupid = 0;
                            if (!int.TryParse(scgroupid, out groupid)) groupid = 0;
                            SubClass sc = new SubClass(scname, groupid, mc);
                            mc.SubClasses.Add(sc);
                            this._allSubClass.Add(sc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static LogicDataStructureManage2D Instance
        {
            get
            {
                if (LogicDataStructureManage2D.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (LogicDataStructureManage2D.instance == null)
                        {
                            LogicDataStructureManage2D.instance = new LogicDataStructureManage2D();
                        }
                    }
                }
                return LogicDataStructureManage2D.instance;
            }
        }

        public List<MajorClass> GetAllMajorClass()
        {
            return this._allMajorClass;
        }

        public MajorClass GetMajorClassByName(string name)
        {
            if( this._allMajorClass == null) return null;
            foreach (MajorClass mc in this._allMajorClass)
            {
                if (mc.Name == name) return mc;
            }
            return null;
        }
        public MajorClass GetMajorClassBySubClassName(string name)
        {
            foreach (SubClass sc in this._allSubClass)
            {
                if (sc.Name == name) return sc.Parent;
            }
            return null;
        }
        public List<SubClass> GetAllSubClass()
        {
            return this._allSubClass;
        }

        public MajorClass GetMajorClassByDFFeatureClassID(string id)
        {
            if(_allMajorClass.Count == 0) GetAllMajorClass();

            foreach (MajorClass mc in this._allMajorClass)
            {
                if (mc.Fc2D.Contains(id)) return mc;
            }
            return null;
        }
    }
}
