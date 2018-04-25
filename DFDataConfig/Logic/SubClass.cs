using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFDataConfig.Logic
{
    public class SubClass
    {
        private string name;
        private int groupid;
        private bool visible2D;
        private bool visible3D;
        private MajorClass parent;
        public string Name
        {
            get { return name; }
        }
        public int GroupId
        {
            get { return groupid; }
        }
        public MajorClass Parent
        {
            get { return this.parent; }
        }

        public bool Visible2D
        {
            get { return this.visible2D; }
            set { this.visible2D = value; }
        }
        public bool Visible3D
        {
            get { return this.visible3D; }
            set { this.visible3D = value; }
        }
        public SubClass(string name, int groupid, MajorClass parent)
        {
            this.name = name;
            this.groupid = groupid;
            this.parent = parent;
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
