using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using DFWinForms.LogicTree;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Tree;
using ESRI.ArcGIS.Carto;

namespace DF2DData.Class
{
    public class DF2DFeatureClassManager
    {
        private static DF2DFeatureClassManager instance = null;
        private static readonly object syncRoot = new object();
        private List<DF2DFeatureClass> listFC;
        private List<TreeNodeComLayer> listTreeLayer;//图层对应的树节点

        private DF2DFeatureClassManager()
        {
            this.listFC = new List<DF2DFeatureClass>();
            this.listTreeLayer = new List<TreeNodeComLayer>();
        }

        public static DF2DFeatureClassManager Instance
        {
            get
            {
                if (DF2DFeatureClassManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (DF2DFeatureClassManager.instance == null)
                        {
                            DF2DFeatureClassManager.instance = new DF2DFeatureClassManager();
                        }
                    }
                }
                return DF2DFeatureClassManager.instance;
            }
        }

        public bool Exists(string id)
        {
            foreach (DF2DFeatureClass fc in this.listFC)
            {
                if (fc.GetFeatureClass().FeatureClassID.ToString() == id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ExistBL(string name)
        {
            foreach (TreeNodeComLayer node in this.listTreeLayer)
            {
                if (node.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Exists(DF2DFeatureClass fc)
        {
            return Exists(fc.GetFeatureClass().FeatureClassID.ToString());
        }

        public DF2DFeatureClass GetFeatureClassByID(string id)
        {
            foreach (DF2DFeatureClass fc in this.listFC)
            {
                if (fc.GetFeatureClass().FeatureClassID.ToString() == id)
                {
                    return fc;
                }
            }
            return null;
        }

        public void Add(DF2DFeatureClass fc)
        {
            if (this.Exists(fc)) return;
            this.listFC.Add(fc);
        }

        public void Add(TreeNodeComLayer node)
        {
            if (this.ExistBL(node.Name)) return;
            this.listTreeLayer.Add(node);
        }

        public List<DF2DFeatureClass> GetAllFeatureClass()
        {
            return this.listFC;
        }

        public List<TreeNodeComLayer> GetAllTreeLayer()
        {
            return this.listTreeLayer;
        }
        public TreeNodeComLayer GetTreeLayerByFeatureClassID(int id)
        {
            TreeNodeComLayer treeLayer = null;
            foreach (TreeNodeComLayer tr in listTreeLayer)
            {
                if (tr.CustomValue == null) continue;
                if (tr.CustomValue is IGeoFeatureLayer)
                {
                    IGeoFeatureLayer geoFL = tr.CustomValue as IGeoFeatureLayer;
                    IFeatureClass fc = geoFL.FeatureClass;
                    if (fc.FeatureClassID == id)
                    {
                        treeLayer = tr;
                    }
                }
                else
                {
                    continue;
                }
            }
            return treeLayer;
            
        }
    }
}
