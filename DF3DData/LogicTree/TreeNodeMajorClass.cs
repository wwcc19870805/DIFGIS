using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;

namespace DF3DData.LogicTree
{
    public class TreeNodeMajorClass : GroupLayerClass
    {
        private Dictionary<string, DF3DFeatureClass> dictFCs;
        public Dictionary<string, DF3DFeatureClass> FeatureClasses
        {
            get { return this.dictFCs; }
            set { this.dictFCs = value; }
        }
        public TreeNodeMajorClass()
            : this("")
        {
            base.ImageIndex = 0;
        }
        public TreeNodeMajorClass(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            base.AddMenuItems(new string[] { "全部展开", "全部折叠" });
        }
        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "全部展开":
                    this.ExpandAll();
                    break;
                case "全部折叠":
                    this.CollapseAll();
                    break;
            }
        }
    }
}
