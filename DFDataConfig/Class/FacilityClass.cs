using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFDataConfig.Class
{
    public class FacilityClass
    {
        private string name;
        private string alias;
        private List<FieldInfo> fieldInfoCollection;
        private string fc2D;
        private string fc3D;
        public string Fc2D
        {
            get { return fc2D; }
            set { this.fc2D = value; }
        }
        public string Fc3D
        {
            get { return fc3D; }
            set { this.fc3D = value; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Alias
        {
            get { return alias; }
        }

        public List<FieldInfo> FieldInfoCollection
        {
            get { return this.fieldInfoCollection; }
        }

        public FacilityClass(string name)
        {
            this.name = name;
            this.fieldInfoCollection = new List<FieldInfo>();
        }
        public FacilityClass(string name,string alias)
        {
            this.name = name;
            this.alias = alias;
            this.fieldInfoCollection = new List<FieldInfo>();
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(this.alias) ? this.name : this.alias;
        }

        public bool ExistsFieldInfo(FieldInfo fi)
        {
            foreach (FieldInfo temp in fieldInfoCollection)
            {
                if (temp.Name == fi.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExistsFieldInfo(string name)
        {
            foreach (FieldInfo temp in fieldInfoCollection)
            {
                if (temp.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public FieldInfo GetFieldInfoByName(string name)
        {
            foreach (FieldInfo temp in fieldInfoCollection)
            {
                if (temp.Name == name)
                {
                    return temp;
                }
            }
            return null;
        }

        public FieldInfo GetFieldInfoBySystemName(string sysname)
        {
            foreach (FieldInfo temp in fieldInfoCollection)
            {
                if (temp.SystemName == sysname)
                {
                    return temp;
                }
            }
            return null;
        }

        public string GetFieldInfoNameBySystemName(string sysname)
        {
            foreach (FieldInfo temp in fieldInfoCollection)
            {
                if (temp.SystemName == sysname)
                {
                    return temp.Name;
                }
            }
            return "";
        }
        public void AddFieldInfo(FieldInfo fi)
        {
            if (ExistsFieldInfo(fi)) return;
            this.fieldInfoCollection.Add(fi);
        }
    }
}
