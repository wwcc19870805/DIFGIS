using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF2DMDBConvert.Class
{
    public class Table
    {
        private string name;
        private string alias;
        private string fc2D;
        private List<TableField> tableFieldCollection;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }
        public string Fc2D
        {
            get { return this.fc2D; }
            set { this.fc2D = value; }
        }
        public List<TableField> TableFieldCollection
        {
            get { return this.tableFieldCollection; }
        }
        public Table(string name,string fc2D)
        {
            this.name = name;
            this.fc2D = fc2D;
            tableFieldCollection = new List<TableField>();
        }

        public bool ExistsFieldInfo(TableField tf)
        {
            foreach (TableField temp in tableFieldCollection)
            {
                if (temp.GisName == tf.GisName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExistsFieldInfo(string name)
        {
            foreach (TableField temp in tableFieldCollection)
            {
                if (temp.GisName == name)
                {
                    return true;
                }
            }
            return false;
        }

        public TableField GetTableFieldByAliasName(string aliasName)
        {
            foreach (TableField tf in tableFieldCollection)
            {
                if (tf.CadName == aliasName)
                    return tf;
            }
            return null;
        }


    }
}
