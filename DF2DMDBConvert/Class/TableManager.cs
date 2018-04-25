using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace DF2DMDBConvert.Class
{
    public class TableManager
    {
        private List<Table> _listTable;

        public TableManager()
        {
            this._listTable = new List<Table>();
          
        }

        public void ReadConfig(string filePath)
        {
            if (this._listTable != null && this._listTable.Count > 0) return;
            try
            {
                if (!File.Exists(filePath)) return;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                if (xmlDoc == null) return;
                XmlNode root = xmlDoc.SelectSingleNode("Tables");
                if (root == null) return;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name != "Table") continue;
                    if (node.Attributes["name"] == null) continue;
                    string name = "", fc2D = "";
                    if (node.Attributes["name"] != null) { name = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(name)) continue; }
                    if (node.Attributes["fc2D"] != null)
                    { 
                        fc2D = node.Attributes["fc2D"].Value.Trim(); 
                        //if (string.IsNullOrEmpty(fc2D)) continue;
                    }
                    Table table = new Table(name,fc2D);
                    foreach (XmlNode cnode in node.ChildNodes)
                    {
                        if (cnode.Name != "StdField") continue;
                        if (cnode.Attributes["cadname"] == null) continue;
                        string fieldName = "", fieldAliasName = "", dataType = "";
                        if (cnode.Attributes["gisname"] != null) fieldName = cnode.Attributes["gisname"].Value.Trim();
                        if (cnode.Attributes["cadname"] != null) fieldAliasName = cnode.Attributes["cadname"].Value.Trim();
                        if (cnode.Attributes["datatype"] != null) dataType = cnode.Attributes["datatype"].Value.Trim();
                        TableField tf = new TableField(fieldName, fieldAliasName, dataType);
                        table.TableFieldCollection.Add(tf);
                    }
                    this.Add(table);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        public bool Exists(string name)
        {
            foreach (Table tb in _listTable)
            {
                if (tb.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Exists(Table table)
        {
            foreach (Table tb in _listTable)
            {
                if (tb.Name == table.Name)
                {
                    return true;
                }
            }
            return false;
        }
        public void Add(Table table)
        {
            if (this.Exists(table)) return;
            this._listTable.Add(table);
        }

        public List<Table> GetAllTables()
        {
            return this._listTable;
        }

        public Table GetTableByTableName(string tableName)
        {
            foreach (Table tb in this._listTable)
            {
                if (tb.Name == tableName)
                {
                    return tb;
                }
            }
            return null;

        }
    }
}
