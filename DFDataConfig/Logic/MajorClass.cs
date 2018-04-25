using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFDataConfig.Logic
{
    public class MajorClass
    {
        private string name;
        private string alias;
        private string classifyField;
        private string fc2D;
        private string fc3D;
        private LogicGroup parent;
        private List<SubClass> listSubClasses;
        public List<SubClass> SubClasses
        {
            get { return this.listSubClasses; }
        }

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
        public string ClassifyField
        {
            get { return classifyField; }
            set { this.classifyField = value; }
        }
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
        public LogicGroup Parent
        {
            get { return this.parent; }
        }
        public MajorClass(string name,string alias="",string classifyField ="",string fc2D="",string fc3D="",LogicGroup parent = null)
        {
            this.name = name;
            this.alias = alias;
            this.classifyField = classifyField;
            this.fc2D = fc2D;
            this.fc3D = fc3D;
            this.listSubClasses = new List<SubClass>();
            this.parent = parent;
        }
        public SubClass GetSubClassByName(string name)
        {
            foreach (SubClass sc in this.listSubClasses)
            {
                if (sc.Name == name) return sc;
            }
            return null;
        }
        public SubClass GetSubClassByGroupId(int groupId)
        {
            foreach (SubClass sc in this.listSubClasses)
            {
                if (sc.GroupId == groupId) return sc;
            }
            return null;
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(this.alias) ? this.name : this.alias;
        }
    }
}
