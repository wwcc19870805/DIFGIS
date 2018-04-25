using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFDataConfig.Class
{
    public class FieldInfo
    {
        protected string name;
        protected string alias;
        protected string dataType;
        protected string systemName;
        protected string systemAlias;
        protected bool bCanQuery;
        protected bool bNeedCheck;
        protected bool bCanStats;

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
        public string DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }
        public string SystemName
        {
            get { return this.systemName; }
            set { this.systemName = value; }
        }
        public string SystemAlias
        {
            get { return this.systemAlias; }
            set { this.systemAlias = value; }
        }

        public bool CanQuery
        {
            get { return this.bCanQuery; }
            set { this.bCanQuery = value; }
        }
        public bool CanStats
        {
            get { return this.bCanStats; }
            set { this.bCanStats = value; }
        }
        public bool NeedCheck
        {
            get { return this.bNeedCheck; }
            set { this.bNeedCheck = value; }
        }

        public FieldInfo(string name = "", string alias = "", string systemName = "", string systemAlias = "", bool bCanQuery = false, bool bNeedCheck = false, bool bCanStats = false, string dataType = "")
        {
            this.name = name;
            this.alias = alias;
            this.systemName = systemName;
            this.systemAlias = systemAlias;
            this.bCanQuery = bCanQuery;
            this.bNeedCheck = bNeedCheck;
            this.bCanStats = bCanStats;
            this.dataType = dataType;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this.alias) ? this.name : this.alias;
        }
    }
}
