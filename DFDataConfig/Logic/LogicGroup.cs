using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFDataConfig.Logic
{
    public class LogicGroup
    {
        private string name;
        private string alias;
        private List<LogicGroup> listLogicGroup;
        private List<MajorClass> listMajorClass;
        private LogicGroup parent;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { this.alias = value; }
        }

        public List<LogicGroup> LogicGroups
        {
            get { return this.listLogicGroup; }
        }
        public List<MajorClass> MajorClasses
        {
            get { return this.listMajorClass; }
        }
        public LogicGroup Parent
        {
            get { return this.parent; }
        }

        public LogicGroup(string name, string alias = "", LogicGroup parent = null)
        {
            this.name = name;
            this.alias = alias;
            this.listLogicGroup = new List<LogicGroup>();
            this.listMajorClass = new List<MajorClass>();
            this.parent = parent;
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(this.alias) ? this.name : this.alias;
        }
    }
}
