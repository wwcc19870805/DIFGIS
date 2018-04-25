using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geometry;
using DF2DPipe.Stats.UC;

namespace DF2DPipe.Class
{
    public class BuildingStats : EconomyStats
    {
        private DataTable _dt;
        private IFeatureClass _fc;
        private IFeatureClass _district;
        private IFeatureCursor disCursor;
        private IFeature disFeature;
        private IFeatureCursor fcCursor;
        private IFeature fcFeature;
        private ISpatialFilter filter;
        private string disName;
        private double disArea;       
        public BuildingStats()
        {
         
            InitDataTable();
        }
        public Dictionary<string, List<IFeature>> Dict
        {
            get { return this._dict; }
        }
        private Dictionary<string, List<IFeature>> _dict;//声明各区域包含的要素列表字典
        private List<IFeature> _listF;//声明各区域包含的要素列表
        public override Dictionary<string, List<IFeature>> GetStatsResult()
        {
            try
            {
                if (this._fc == null || this._district == null) return null;
                disCursor = _district.Search(null, true);
                if (disCursor == null) return null;
                _dict = new Dictionary<string, List<IFeature>>();
                while ((disFeature = disCursor.NextFeature()) != null)
                {
                    IGeometry geo = disFeature.Shape;
                    filter = new SpatialFilter();
                    filter.Geometry = geo;
                    filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    fcCursor = _fc.Search(filter, true);
                    if (fcCursor == null) continue;
                    int buildingCount = _fc.FeatureCount(filter);
                    disName = disFeature.get_Value(IndexOfDisName).ToString();
                    disArea = Convert.ToDouble(disFeature.get_Value(IndexOfDisArea));
                    double buildingArea = 0;
                    double floorArea = 0;
                    _listF = new List<IFeature>();
                    while ((fcFeature = fcCursor.NextFeature()) != null)
                    {
                        buildingArea += Convert.ToDouble(fcFeature.get_Value(IndexOfFcArea));
                        floorArea  += Convert.ToDouble(fcFeature.get_Value(IndexOfFloorArea));
                        _listF.Add(fcFeature);
                    }
                    _dict[disName] = _listF;
                    DataRow row = _dt.NewRow();
                    row["DISTRICTNAME"] = disName;
                    row["DISTRICTAREA"] = Math.Round(disArea,2);
                    row["NUMBER"] = buildingCount;
                    row["AREA"] = Math.Round(buildingArea,2);
                    row["FLOORAREA"] = Math.Round(floorArea,2);
                    _dt.Rows.Add(row);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
            return _dict;
        }
        public override void InitDataTable()
        {
            _dt = new DataTable();
            _dt.Columns.AddRange(new DataColumn[]{new DataColumn("DISTRICTNAME"),
                new DataColumn("DISTRICTAREA",typeof(double)),new DataColumn("NUMBER",typeof(int)),new DataColumn("AREA",typeof(double)),new DataColumn("FLOORAREA",typeof(double))});
        }
        public override void SetFeatureClass(IFeatureClass district,IFeatureClass fc)
        {
            this._district = district;
            this._fc = fc;
        }
        public override void InitUserControl(UCEconomyStatsOutput2D uc)
        {
            uc.SetBuildData(_dt);  
        }
       
    }
}
