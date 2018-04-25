using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.RenderControl;
using DFDataConfig.Logic;
using DF3DData.Class;

namespace DF3DData.LogicTree
{
    public class TreeNodeSubClass: BaseLayerClass
    {
        private Dictionary<string, IFeatureLayer> dictFLs;
        public Dictionary<string,IFeatureLayer> FeatureLayers
        {
            get { return this.dictFLs; }
            set { this.dictFLs = value; }
        }
        private Dictionary<string, DF3DFeatureClass> dictFCs;
        public Dictionary<string, DF3DFeatureClass> FeatureClasses
        {
            get { return this.dictFCs; }
            set { this.dictFCs = value; }
        }
        public TreeNodeSubClass()
            : this("")
        {
            base.ImageIndex = 3;
        }
        public TreeNodeSubClass(string ID)
            : base(ID)
        {
        }
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if (this.CustomValue != null)
                {
                    if (this.dictFLs == null || this.CustomValue == null) return;
                    if (this.CustomValue is SubClass)
                    {
                        (this.CustomValue as SubClass).Visible3D = value;
                    }
                    if (value)
                    {
                        base.Visible = true;
                        foreach (IFeatureLayer fl in this.dictFLs.Values)
                        {
                            fl.SetGroupVisibleMask((this.CustomValue as SubClass).GroupId, gviViewportMask.gviViewAllNormalView);
                        }
                    }
                    else
                    {
                        base.Visible = false;
                        foreach (IFeatureLayer fl in this.dictFLs.Values)
                            fl.SetGroupVisibleMask((this.CustomValue as SubClass).GroupId, gviViewportMask.gviViewNone);
                    }
                }
                else base.Visible = false;
            }
        }
    }
}
