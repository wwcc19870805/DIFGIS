using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;

namespace DF2DAnalysis.Class
{
    public class ReadDemRaster
    {
        public static IRaster GetDEM()
        {
            try
            {
                string path = Config.GetConfigValue("2DMdbDEM");
                IWorkspaceFactory2 pWsF = new AccessWorkspaceFactoryClass();
                IRasterWorkspaceEx pRws = pWsF.OpenFromFile(path, 0) as IRasterWorkspaceEx;
                IRasterDataset pRDs = pRws.OpenRasterDataset("DEM");
                IRaster raster = pRDs.CreateDefaultRaster();
                return raster;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
        public static double GetH(IPoint point)
        {
            IRaster raster = GetDEM();
            IRaster2 raster2 = raster as IRaster2;
            int row = 0;
            int col = 0;
            raster2.MapToPixel(point.X, point.Y, out col, out row);
            object obj = raster2.GetPixelValue(0, col, row);
            double height = Convert.ToDouble(obj.ToString());
            return height;
        }
    }
}
