using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF2DData.Class;

namespace DF2DData.LogicTree
{
    public class TreeNodeMajorClass2D : GroupLayerClass
    {
        private Dictionary<string, DF2DFeatureClass> dictFCs;
        private bool _checkOn = true;
        public bool CheckOn
        {
            get { return this._checkOn; }
            set { this._checkOn = value; }
        }

        public Dictionary<string, DF2DFeatureClass> FeatureClasses
        {
            get { return this.dictFCs; }
            set { this.dictFCs = value; }
        }
        public TreeNodeMajorClass2D()
            : this("")
        {
            base.ImageIndex = 0;
        }
        public TreeNodeMajorClass2D(string ID)
            : base(ID)
        {
        }
    }
}
