using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using DF2DData.Tree;
using DFDataConfig.Logic;
using DF2DData.LogicTree;


namespace DF2DData.Class
{
    class DF2DMajorClass
    {
        private List<DF2DFeatureClass> listFC;
        private TreeNodeMajorClass2D treeLayer;
        private MajorClass majorClass;
        private List<ILayer> listLayer;
        public DF2DMajorClass(MajorClass mj, TreeNodeMajorClass2D treeLayer)
        {
            
            this.treeLayer = treeLayer;
            this.majorClass = mj;
            listLayer = new List<ILayer>();
        }
        public TreeNodeMajorClass2D GetTNByFeatureClassID(string id)
        {
            if (this.majorClass == null|| this.treeLayer == null) return null;
            string[] arrayFc2D = majorClass.Fc2D.Split(';');
            foreach (string fcId in arrayFc2D)
            {
                if (fcId == id)
                    return this.treeLayer;
            }
            return null;
        }
        public MajorClass GetMajorClass()
        {
            return this.majorClass;
        }
        public void AddLayer(ILayer lyr)
        {
            listLayer.Add(lyr);
        }
    }
}
