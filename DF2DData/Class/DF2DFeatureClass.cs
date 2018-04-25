using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using DF2DData.Tree;

namespace DF2DData.Class
{
    public class DF2DFeatureClass : DFFeatureClass
    {
        private IFeatureClass fc;
        private ILayer layer;
        private TreeNodeComLayer treeLayer;
        public DF2DFeatureClass(IFeatureClass fc)
        {
            this.fc = fc;
            string temp = Dictionary2DTable.Instance.GetFacilityClassNameByDFFeatureClassID(fc.FeatureClassID.ToString());
            if (temp == null || temp == "") return;
            this.AttachFacilityClassByName(temp);
        }
        public DF2DFeatureClass(IFeatureClass fc, TreeNodeComLayer treeLayer)
        {
            this.fc = fc;
            this.treeLayer = treeLayer;
            string temp = Dictionary2DTable.Instance.GetFacilityClassNameByDFFeatureClassID(fc.FeatureClassID.ToString());
            if (temp == null || temp == "") return;
            this.AttachFacilityClassByName(temp);
        }

        public IFeatureClass GetFeatureClass()
        {
            return this.fc;
        }
        public IFeatureLayer GetFeatureLayer()
        {
            return this.layer as IFeatureLayer;
        }
        public void SetLayer(ILayer lyr)
        {
            this.layer = lyr;
        }
        public ILayer  GetLayer()
        {
            return this.layer;
        }
        public TreeNodeComLayer GetTreeLayer()
        {
            return this.treeLayer;
        }
    }
}
