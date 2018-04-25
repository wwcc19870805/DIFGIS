using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DDataCheck.Frm;
using System.Collections;
using DFWinForms.Service;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using DF2DData.Class;

namespace DF2DDataCheck.Command
{
    class CmdTrueRoadCheck:AbstractMap2DCommand
    {
        IMap2DView mapView;
        DF2DApplication app;
        IFeatureClass pFeatureClass;
        private frmProgress m_frmPro;
        private string FeatureName = "TRAPLY500";
        private string FieldName = "NAME";
        private ArrayList PolyRoad = new ArrayList();
        private ArrayList IntersectPtRoadID = new ArrayList();
        private ArrayList IntersectArcRoadID = new ArrayList();
        private ArrayList RoadPtID = new ArrayList();
        private ArrayList RoadPtDETEID = new ArrayList();
        private Hashtable Hashtable = new Hashtable();
        private string[] PntFeaClass = { "BPPT500", "DPPT500", "EPPT500", "FPPT500", "TPPT500" };
        private string[] ArcFeaClass = { "BPARC500", "DPARC500", "EPARC500", "FPARC500", "TPARC500" };

        public override void Run(object sender, System.EventArgs e)
        {
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;

            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
            if (list == null) return;
            List<DF2DFeatureClass> List = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
            if (List == null) return;
            int nMax;
            nMax = list.Count + List.Count;
            m_frmPro = new frmProgress(nMax);
            m_frmPro.Update();
            m_frmPro.Show();

            checkTrueRoad();

            m_frmPro.Text = "检查完成";
            m_frmPro.Close();
        }
        private void checkTrueRoad()
        {
            //获得交岔路口
            string str;
            
            IFeatureClass pntFeaClass;
            IFeatureClass ArcFeaClass;
            List<string> IntersectRoad = new List<string>();
            ITopologicalOperator TopologicalOperator = null;
            ISpatialFilter pSpatial = new SpatialFilterClass();
            IPolygon pPolygon = null;

            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("TRAPLY500");
            if (list == null) return;
            foreach (DF2DFeatureClass dfcc in list)
            {
                pFeatureClass = dfcc.GetFeatureClass();
                IFeatureCursor pFeaCursor = pFeatureClass.Search(null, false);
                IFeature Fea = pFeaCursor.NextFeature();
                while (Fea != null)
                {
                    string fileName = Fea.get_Value(pFeatureClass.Fields.FindField("NAME")).ToString();
                    TopologicalOperator = Fea.Shape as ITopologicalOperator;

                    pSpatial.Geometry = Fea.Shape;
                    pSpatial.GeometryField = FieldName;
                    pSpatial.SpatialRel = esriSpatialRelEnum.esriSpatialRelOverlaps;
                    IFeatureCursor pFeaCur = pFeatureClass.Search(pSpatial, false);
                    IFeature pFea = pFeaCur.NextFeature();
                    while (pFea != null)
                    {
                        pPolygon = (IPolygon)TopologicalOperator.Intersect((IGeometry)pFea.Shape, esriGeometryDimension.esriGeometry2Dimension);

                        if (pPolygon != null)
                        {

                            string FielName = pFea.get_Value(pFeatureClass.Fields.FindField("NAME")).ToString();
                            if (fileName != FielName)
                            {
                                PolyRoad.Add(pPolygon);
                                string roadName = fileName + "与" + FielName + "的交岔口";
                                IntersectRoad.Add(roadName);
                                Hashtable.Add(pPolygon, roadName);
                            }
                        }
                        pFea = pFeaCur.NextFeature();
                    }
                    Fea = pFeaCursor.NextFeature();
                }
            }

            int m = 0;
            foreach (IPolygon Polygon in PolyRoad)
            {
                //遍历交岔口获取点OID为交岔口的点赋值
                List<DF2DFeatureClass> stPnt = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (stPnt == null) return;
                foreach (DF2DFeatureClass dfcc in stPnt)
                {
                    pntFeaClass = dfcc.GetFeatureClass();
                    m_frmPro.NextStep("正在检查图层'" + pntFeaClass.AliasName + "'，请稍候……");
                    pSpatial.Geometry = Polygon;
                    pSpatial.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    IFeatureCursor FeaCursor = pntFeaClass.Search(pSpatial, false);
                    IFeature pFea = FeaCursor.NextFeature();
                    while (pFea != null)
                    {
                        str = pFea.get_Value(pntFeaClass.FindField("PROAD")).ToString();
                        string PntID = pFea.get_Value(pntFeaClass.FindField("DETECTID")).ToString();
                        RoadPtDETEID.Add(PntID);
                        int PtRoadID = pFea.OID;
                        IntersectPtRoadID.Add(PtRoadID);
                        foreach (DictionaryEntry de in Hashtable)
                        {
                            if (de.Key == Polygon)
                            {
                                str = de.Value.ToString();
                                pFea.set_Value(pntFeaClass.FindField("PROAD"), str);
                                pFea.Store();
                            }
                        }
                        pFea = FeaCursor.NextFeature();
                    }
                }

                //获取线OID并为线要素赋值

                List<DF2DFeatureClass> stArc = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                if (stArc == null) return;
                foreach (DF2DFeatureClass dfcc in stArc)
                {
                    ArcFeaClass = dfcc.GetFeatureClass();
                    m_frmPro.NextStep("正在检查图层'" + ArcFeaClass.AliasName + "'，请稍候……");
                    IFeatureCursor FeatureCursor = ArcFeaClass.Search(null, false);
                    IFeature Feaure = FeatureCursor.NextFeature();
                    while (Feaure != null)
                    {
                        string strarc = Feaure.get_Value(ArcFeaClass.Fields.FindField("SNODEID")).ToString();
                        if (RoadPtDETEID.Contains(strarc))
                        {
                            int ArcID = Feaure.OID;
                            IntersectArcRoadID.Add(ArcID);
                            foreach (DictionaryEntry dt in Hashtable)
                            {
                                if (dt.Key == Polygon)
                                {
                                    string parcName = dt.Value.ToString();
                                    Feaure.set_Value(ArcFeaClass.FindField("PROAD"), parcName);
                                    Feaure.Store();
                                }
                            }
                        }
                        Feaure = FeatureCursor.NextFeature();
                    }
                }
                m++;
                Console.WriteLine(m);
            }
            //遍历道路点检查
            int n = 0;
            IFeatureCursor pFCursor = pFeatureClass.Search(null, false);
            IFeature Feature = pFCursor.NextFeature();
            while (Feature != null)
            {
                string FielName = Feature.get_Value(pFeatureClass.Fields.FindField("NAME")).ToString();

                List<DF2DFeatureClass> stPnt = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (stPnt == null) return;
                foreach (DF2DFeatureClass dfcc in stPnt)
                {
                    pntFeaClass = dfcc.GetFeatureClass();
                    m_frmPro.NextStep("正在检查图层'" + pntFeaClass.AliasName + "'，请稍候……");
                    pSpatial.Geometry = Feature.Shape;
                    pSpatial.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    IFeatureCursor FeCursor = pntFeaClass.Search(pSpatial, false);
                    IFeature pFeature = FeCursor.NextFeature();
                    while (pFeature != null)
                    {
                        string StrName = pFeature.get_Value(pntFeaClass.FindField("PROAD")).ToString();
                        if ((IntersectPtRoadID.Contains(pFeature.OID) == false))
                        {
                            string RidName = pFeature.get_Value(pntFeaClass.Fields.FindField("DETECTID")).ToString();
                            RoadPtID.Add(RidName);

                            pFeature.set_Value(pntFeaClass.FindField("PROAD"), FielName);
                            pFeature.Store();

                            Console.WriteLine(pFeature.OID);

                        }
                        pFeature = FeCursor.NextFeature();
                    }
                }
                //遍历道路线检查
                 List<DF2DFeatureClass> stArc = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                if (stArc == null) return;
                foreach (DF2DFeatureClass dfcc in stArc)
                {
                    ArcFeaClass = dfcc.GetFeatureClass();
                    m_frmPro.NextStep("正在检查图层'" + ArcFeaClass.AliasName + "'，请稍候……");
                    IFeatureCursor pFeatureCursor = ArcFeaClass.Search(null, false);
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        string strarc = pFeature.get_Value(ArcFeaClass.FindField("SNODEID")).ToString();
                        string strearc = pFeature.get_Value(ArcFeaClass.Fields.FindField("ENODEID")).ToString();
                        string ArcLineName = pFeature.get_Value(ArcFeaClass.FindField("PROAD")).ToString();
                        if ((RoadPtID.Contains(strarc) == true) && (IntersectArcRoadID.Contains(pFeature.OID) == false))
                        {
                            pFeature.set_Value(ArcFeaClass.FindField("PROAD"), FielName);
                            pFeature.Store();
                        }

                        Console.WriteLine(pFeature.OID);
                        pFeature = pFeatureCursor.NextFeature();

                    }
                }
                n++;
                Console.WriteLine(n + "" + Feature.OID);
                Feature = pFCursor.NextFeature();
            }









        }






    }
}
