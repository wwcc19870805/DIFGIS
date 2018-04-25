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
    class DF2DRaster
    {
        private IRaster raster;
        private ILayer layer;
        private TreeNodeComLayer treeLayer;
        public DF2DRaster(IRaster raster, TreeNodeComLayer treeLayer)
        {
            this.raster = raster;
            this.treeLayer = treeLayer;           
        }

        public IRaster GetRaster()
        {
            return this.raster;
        }
        public IRasterLayer GetRasterLayer()
        {
            return this.layer as IRasterLayer;
        }
        public void SetLayer(ILayer lyr)
        {
            this.layer = lyr;
        }
        public string GetRasterLayerName()
        {
            return this.layer.Name;
        }
        public ILayer GetLayer()
        {
            return this.layer;
        }
        public TreeNodeComLayer GetTreeLayer()
        {
            return this.treeLayer;
        }
    }
}
