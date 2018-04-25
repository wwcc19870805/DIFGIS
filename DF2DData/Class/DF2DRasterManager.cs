using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Class;
using DFWinForms.LogicTree;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Tree;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;

namespace DF2DData.Class
{
    class DF2DRasterManager
    {
        private static DF2DRasterManager instance = null;
        private static readonly object syncRoot = new object();
        private List<DF2DRaster> listRaster;
        private List<TreeNodeComLayer> listTreeLayer;//图层对应的树节点

        private DF2DRasterManager()
        {
            this.listRaster = new List<DF2DRaster>();
            this.listTreeLayer = new List<TreeNodeComLayer>();
        }

        public static DF2DRasterManager Instance
        {
            get
            {
                if (DF2DRasterManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (DF2DRasterManager.instance == null)
                        {
                            DF2DRasterManager.instance = new DF2DRasterManager();
                        }
                    }
                }
                return DF2DRasterManager.instance;
            }
        }

        public bool Exists(string name)
        {
            foreach (DF2DRaster rd in this.listRaster)
            {
                if (rd.GetRasterLayerName() == name)
                {
                    return true;
                }
                
            }
            return false;
        }

        public bool ExistTN(string name)
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

        public void Add(DF2DRaster rd)
        {
            if (this.Exists(rd.GetRasterLayerName())) return ;
            this.listRaster.Add(rd);
        }

        public void Add(TreeNodeComLayer node)
        {
            if (this.ExistTN(node.Name)) return;
            this.listTreeLayer.Add(node);
        }

        public List<DF2DRaster> GetAllRaster()
        {
            return this.listRaster;
        }

        public List<TreeNodeComLayer> GetAllTreeLayer()
        {
            return this.listTreeLayer;
        }

    }
}
