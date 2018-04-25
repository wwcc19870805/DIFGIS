using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using DFWinForms.Service;
using DFWinForms.Class;
using DF2DDataCheck.Frm;
using ESRI.ArcGIS.Geometry;
using DF2DData.Class;
using System.Collections;
using System.Windows.Forms;
using DFDataConfig.Class;

namespace DF2DDataCheck.Command
{
    class CmdPipeRoadCheck : AbstractMap2DCommand
    {
        IMapControl2 m_pMapControl;
        IMap2DView mapView;
        DF2DApplication app;
        private ArrayList PolyRoad = new ArrayList();
        private ArrayList IntersectRoad = new ArrayList();
        private ArrayList RoadPtDETEID = new ArrayList();
        private ArrayList IntersectPtRoadID = new ArrayList();
        private ArrayList IntersectArcRoadID = new ArrayList();
        private ArrayList RoadPtID = new ArrayList();
        private Hashtable Hashtable = new Hashtable();
        private IFeatureClass pFeatureClass;
        
        Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
        private DataTable GetDataTableByStruture()
        {
            DataTable dt = new DataTable();
            dt.TableName = "ErrorList";
            DataColumn column;

            column = new DataColumn();
            column.ColumnName = "ErrorFeatureID";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "ErrorType";
            dt.Columns.Add(column);


            column = new DataColumn();
            column.ColumnName = "FeatureofClass";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FeatureofLayer";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FeatureClass";
            column.DataType = typeof(IFeatureClass);
            dt.Columns.Add(column);
            return dt;
        }

        public override void Run(object sender, EventArgs e)
        {
          mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;


            //道路检测
            PipeRoadCheck();

            WaitForm.Stop();
            FormCheckResult dlg = new FormCheckResult(dict, m_pMapControl);
            dlg.Text = this.CommandName;
            dlg.Show();
        }

        private void PipeRoadCheck()
        {
           
            string str;
            DataTable dt = GetDataTableByStruture();
            IFeatureClass pntFeaClass;
            IFeatureClass parcFeaClass;
            IFeature Fea;
            IPolygon pPolygon = null;
            ITopologicalOperator TopologicalOperator = null;
            ISpatialFilter pSpatial = new SpatialFilterClass();
           
            //获取交叉面
            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("TRAPLY500");
            if (list == null) return;
            WaitForm.Start("开始道路检查..", "请稍后");
            foreach (DF2DFeatureClass dfcc in list)
            {
                pFeatureClass = dfcc.GetFeatureClass();
                if (pFeatureClass == null) continue;
                FacilityClass fac = dfcc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("name");
                if (fi == null) continue;
                IFeatureCursor pFeaCursor = pFeatureClass.Search(null, false);
                Fea = pFeaCursor.NextFeature();
                while (Fea != null)
                {
                    string fielName = Fea.get_Value(pFeatureClass.FindField(fi.Name)).ToString();
                    pSpatial.Geometry = Fea.Shape;
                    pSpatial.GeometryField = fi.Name;
                    pSpatial.SpatialRel = esriSpatialRelEnum.esriSpatialRelOverlaps;
                    IFeatureCursor pFeaCur = pFeatureClass.Search(pSpatial, false);
                    IFeature pFea = pFeaCur.NextFeature();
                    while (pFea != null)
                    {
                        TopologicalOperator = pFea.Shape as ITopologicalOperator;
                        pPolygon = (IPolygon)TopologicalOperator.Intersect((IGeometry)Fea.Shape, esriGeometryDimension.esriGeometry2Dimension);

                        if ((pPolygon != null) && (pPolygon.GeometryType == esriGeometryType.esriGeometryPolygon))
                        {
                            string FielName = pFea.get_Value(pFeatureClass.Fields.FindField(fi.Name)).ToString();

                            if (fielName != FielName)
                            {
                                PolyRoad.Add(pPolygon);
                                string roadName = fielName + "与" + FielName + "的交岔口";
                                IntersectRoad.Add(roadName);
                                Hashtable.Add(pPolygon, roadName);
                                Console.WriteLine(pFea.OID + "," + roadName);
                            }
                        }
                        pFea = pFeaCur.NextFeature();
                    }
                    Fea = pFeaCursor.NextFeature();
                }

            }


            //遍历交岔面获取点OID
            int n = 0;
            foreach (IPolygon Polygon in PolyRoad)
            {
                List<DF2DFeatureClass> List = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (List == null) return;
                foreach (DF2DFeatureClass dfcc in List)
                {
                    pntFeaClass = dfcc.GetFeatureClass();
                    if (pntFeaClass == null) continue;
                    FacilityClass fac = dfcc.GetFacilityClass();
                    if (fac == null) continue;
                    List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                    DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("proad");
                    if (fi == null) continue;
                    DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("detectid");
                    if (fi1 == null) continue;
                    WaitForm.SetCaption(pntFeaClass.AliasName);
                    pSpatial.Geometry = Polygon;
                    pSpatial.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    IFeatureCursor FeaCursor = pntFeaClass.Search(pSpatial, true);
                    IFeature pFea = FeaCursor.NextFeature();
                    foreach (DictionaryEntry de in Hashtable)
                    {
                        if (de.Key == Polygon)
                        {
                            string Name = de.Value.ToString();
                            Console.WriteLine(Name);
                        }
                    }
                    while (pFea != null)
                    {
      
                        str = pFea.get_Value(pntFeaClass.Fields.FindField(fi.Name)).ToString();
                        string pFeaID = pFea.get_Value(pntFeaClass.Fields.FindField(fi1.Name)).ToString();
                        RoadPtDETEID.Add(pFeaID);
                        int PtRoadID = pFea.OID;
                        IntersectPtRoadID.Add(PtRoadID);
                        if (IntersectRoad.Contains(str) == false)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = pntFeaClass.AliasName;
                            dr["FeatureofLayer"] = (pntFeaClass as IDataset).Name;
                            dr["FeatureClass"] = pntFeaClass;
                            dr["ErrorType"] = "交叉面点道路名错误";
                            dt.Rows.Add(dr);
                        }
                        pFea = FeaCursor.NextFeature();
                    }
                    if (dt.Rows.Count > 0) dict[pntFeaClass] = dt;
                }
                
                n++;
                Console.WriteLine(n);
                }
                
           



            //遍历线获取OID

            List<DF2DFeatureClass> st = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
            if (st == null) return;
            foreach (DF2DFeatureClass dfcc in st)
            {
                parcFeaClass = dfcc.GetFeatureClass();
                if (parcFeaClass == null) continue;
                FacilityClass fac = dfcc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("proad");
                if (fi == null) continue;
                DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("snodeid");
                if (fi1 == null) continue;
                DFDataConfig.Class.FieldInfo fi2 = fac.GetFieldInfoBySystemName("enodeid");
                if (fi2 == null) continue;

                WaitForm.SetCaption(parcFeaClass.AliasName);
                IFeatureCursor pFeatureCursor = parcFeaClass.Search(null, false);
                IFeature pFeature ;
                while ((pFeature= pFeatureCursor.NextFeature()) != null)
                {
                    string pFeatureName = pFeature.get_Value(parcFeaClass.Fields.FindField(fi.Name)).ToString();
                    string strearc = pFeature.get_Value(parcFeaClass.Fields.FindField(fi1.Name)).ToString();
                    string strarc = pFeature.get_Value(parcFeaClass.Fields.FindField(fi2.Name)).ToString();
                    if (RoadPtDETEID.Contains(strarc))
                    {
                        
                        int ArcID = pFeature.OID;
                        IntersectArcRoadID.Add(ArcID);
                        Console.WriteLine("线：" + pFeature.OID + pFeatureName);
                        if (IntersectRoad.Contains(pFeatureName) == false)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFeature.OID;
                            dr["FeatureofClass"] = parcFeaClass.AliasName;
                            dr["FeatureofLayer"] = (parcFeaClass as IDataset).Name;
                            dr["FeatureClass"] = parcFeaClass;
                            dr["ErrorType"] = "交叉面线道路名错误";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                if (dt.Rows.Count > 0) dict[parcFeaClass] = dt;
            }

             //遍历道路点检查
            int m = 0; 
            IFeatureCursor pFCursor = pFeatureClass.Search(null, false);
            IFeature Feature ;
            while ((Feature = pFCursor.NextFeature()) != null)
            {

                string FielName = Feature.get_Value(pFeatureClass.Fields.FindField("Name")).ToString();
                List<DF2DFeatureClass> List = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (List == null) return;
                foreach (DF2DFeatureClass dfcc in List)
                {
                    pntFeaClass = dfcc.GetFeatureClass();
                    if (pntFeaClass == null) continue;
                    WaitForm.SetCaption(pntFeaClass.AliasName);
                    pSpatial.Geometry = Feature.Shape;
                    pSpatial.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    IFeatureCursor FeaCursor = pntFeaClass.Search(pSpatial, true);
                    IFeature pFea;
                    while ((pFea = FeaCursor.NextFeature()) != null)
                    {
                        FacilityClass fac = dfcc.GetFacilityClass();
                        if (fac == null) continue;
                        List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                        DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("proad");
                        if (fi == null) continue;
                        DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("detectid");
                        if (fi1 == null) continue;
                        string StrName = pFea.get_Value(pntFeaClass.Fields.FindField(fi.Name)).ToString();
                        if ((IntersectPtRoadID.Contains(pFea.OID) == false))
                        {

                            string RidName = pFea.get_Value(pntFeaClass.Fields.FindField(fi1.Name)).ToString();
                            RoadPtID.Add(RidName);
                            if (StrName == null || (!(StrName.Equals(FielName))))
                            {
                                DataRow dr = dt.NewRow();
                                dr["ErrorFeatureID"] = pFea.OID;
                                dr["FeatureofClass"] = pntFeaClass.AliasName;
                                dr["FeatureofLayer"] = (pntFeaClass as IDataset).Name;
                                dr["FeatureClass"] = pntFeaClass;
                                dr["ErrorType"] = "交叉面外点道路名错误";
                                dt.Rows.Add(dr);
                            }
                        }

                    }
                    if (dt.Rows.Count > 0) dict[pntFeaClass] = dt;
                }

                //遍历道路线检查
                List<DF2DFeatureClass> stArc = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                if (stArc == null) return;
                foreach (DF2DFeatureClass dfcc in stArc)
                {

                    parcFeaClass = dfcc.GetFeatureClass();
                    if (parcFeaClass == null) continue;
                    WaitForm.SetCaption("正在检查"+parcFeaClass.AliasName);
                    FacilityClass fac = dfcc.GetFacilityClass();
                    if (fac == null) continue;
                    List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                    DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("proad");
                    if (fi == null) continue;
                    DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("enodeid");
                    if (fi1 == null) continue;
                    DFDataConfig.Class.FieldInfo fi2 = fac.GetFieldInfoBySystemName("snodeid");
                    if (fi2 == null) continue;
                    IFeatureCursor pFeatureCursor = parcFeaClass.Search(null, false);
                    IFeature pFeature ;
                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                    {

                        string strarc = pFeature.get_Value(parcFeaClass.Fields.FindField(fi2.Name)).ToString();
                        string strearc = pFeature.get_Value(parcFeaClass.Fields.FindField(fi1.Name)).ToString();
                        string ArcLineName = pFeature.get_Value(parcFeaClass.Fields.FindField(fi.Name)).ToString();
                        if ((RoadPtID.Contains(strarc) == true) && (IntersectArcRoadID.Contains(pFeature.OID) == false))
                        {
                            if ((ArcLineName == null) || (ArcLineName.Equals(FielName) == false))
                            {

                                DataRow dr = dt.NewRow();
                                dr["ErrorFeatureID"] = pFeature.OID;
                                dr["FeatureofClass"] = parcFeaClass.AliasName;
                                dr["FeatureofLayer"] = (parcFeaClass as IDataset).Name;
                                dr["FeatureClass"] = parcFeaClass;
                                dr["ErrorType"] = "交叉面外线道路名错误";
                                dt.Rows.Add(dr);

                            }
                        }

                    }

                    if (dt.Rows.Count > 0) dict[parcFeaClass] = dt;


                }
                m++;
                Console.WriteLine(m + FielName + Feature.OID);
            }

        }

    }
 }

