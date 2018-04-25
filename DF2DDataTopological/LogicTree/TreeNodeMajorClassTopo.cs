using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF2DData.Class;

namespace DF2DDataTopological.LogicTree
{
    class TreeNodeMajorClassTopo : GroupLayerClass
    {
        private Dictionary<string, DF2DFeatureClass> dictFCs;
        public Dictionary<string, DF2DFeatureClass> FeatureClasses
        {
            get { return this.dictFCs; }
            set { this.dictFCs = value; }
        }
        public TreeNodeMajorClassTopo()
            : this("")
        {
            base.ImageIndex = 0;
        }
        public TreeNodeMajorClassTopo(string ID)
            : base(ID)
        {
        }
    }
}
