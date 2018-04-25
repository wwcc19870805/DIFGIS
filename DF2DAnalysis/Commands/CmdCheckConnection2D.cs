using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using DFDataConfig.Class;
using DF2DData.Class;
using DevExpress.XtraEditors;
using DFAlgorithm.Network;
using DF2DAnalysis.Class;
using DFCommon.Class;
using System.Windows.Forms;
using DFWinForms.Class;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using DF2DPipe.Class;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace DF2DAnalysis.Commands
{
    class CmdCheckConnection2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private int _startOid;
        private string _startFCID;
        private bool _bFinished;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
            _bFinished = true;

        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IGraphicsContainer pGC = m_ActiveView.GraphicsContainer;
            if (this.m_ActiveView.FocusMap.FeatureSelection != null)
                this.m_ActiveView.FocusMap.ClearSelection();
            bool ready = false;
            if (app == null || app.Current2DMapControl == null) return;
            IGeometry pGeo = null;
            try
            {
                if (button == 1)
                {
                    PointClass searchPoint = new PointClass();
                    searchPoint.PutCoords(mapX, mapY);
                    pGeo = PublicFunction.DoBuffer(searchPoint,
                        PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                    if (pGeo == null) return;
                    ready = true;
                    if (ready)
                    {
                        bool haveone = false;

                        foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                        {
                            foreach (MajorClass mc in lg.MajorClasses)
                            {
                                string[] arrFc2DId = mc.Fc2D.Split(';');
                                if (arrFc2DId == null) continue;
                                IFeatureCursor pFeatureCursor = null;
                                IFeature pFeature = null;
                                
                                foreach (string fc2DId in arrFc2DId)
                                {
                                    DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                    if (dffc == null) continue;
                                    IFeatureClass fc = dffc.GetFeatureClass();
                                    FacilityClass facc = dffc.GetFacilityClass();
                                    if (facc.Name != "PipeNode") continue;
                                    IFeatureLayer fl = dffc.GetFeatureLayer();
                                    if (fc == null || pGeo == null|| fl == null) continue;
                                    if (!fl.Visible) continue;

                                    ISpatialFilter pSpatialFilter = new SpatialFilter();
                                    pSpatialFilter.Geometry = pGeo;
                                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                    pFeatureCursor = fc.Search(pSpatialFilter, false);
                                    if (pFeatureCursor == null) continue;
                                    pFeature = pFeatureCursor.NextFeature();
                                    if (pFeature == null) continue;
                                    haveone = true;

                                    IGeometry pGeometry = pFeature.Shape as IGeometry;
                                    if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
                                    {
                                        IPoint pPoint = pGeometry as IPoint;
                                        if (this._bFinished)
                                        {
                                            this._bFinished = false;
                                            this._startFCID = fc.FeatureClassID.ToString();
                                            this._startOid = pFeature.OID;
                                            AddCallout(pPoint, "起点");
                                            app.Current2DMapControl.ActiveView.Refresh();
                                        }
                                        else
                                        {
                                            if (this._startFCID == fc.FeatureClassID.ToString() && this._startOid == pFeature.OID)
                                            {
                                                XtraMessageBox.Show("您选中的是同一个管点设施。", "提示");
                                                return;
                                            }
                                            this._bFinished = true;
                                            AddCallout(pPoint, "终点");
                                            app.Current2DMapControl.ActiveView.Refresh();
                                            if (this._startFCID != fc.FeatureClassID.ToString())
                                            {
                                                XtraMessageBox.Show("您选中的不是同一类管点设施。", "提示");
                                                return;
                                            }
                                            else
                                            {
                                                WaitForm.Start("正在分析...", "请稍后");
                                                TopoClass2D tc = FacilityInfoService2D.GetTopoClassByFeatureClassID(fc.FeatureClassID.ToString());
                                                if (tc == null) return;
                                                TopoNetwork net = tc.GetNetwork();
                                                if (net == null)
                                                {
                                                    WaitForm.Stop();
                                                    XtraMessageBox.Show("构建拓扑网络失败！", "提示");
                                                    return;
                                                }
                                                else
                                                {
                                                    string startId = this._startFCID + "_" + this._startOid.ToString();
                                                    string endId = fc.FeatureClassID.ToString() + "_" + pFeature.OID.ToString();
                                                    List<string> path;
                                                    double shortestLength = net.SPFA(startId, endId, out path);
                                                    if ((shortestLength > 0.0 && shortestLength != double.MaxValue) || (path != null && path.Count > 0))
                                                    {
                                                        List<IPoint> listPt = new List<IPoint>();
                                                        IPointCollection pointCol = new PolylineClass();
                                                        foreach (string nodeId in path)
                                                        {
                                                            int index = nodeId.LastIndexOf("_");
                                                            string fcID = nodeId.Substring(0, index);
                                                            string oid = nodeId.Substring(index + 1, nodeId.Length - index - 1);
                                                            DF2DFeatureClass dffcTemp = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fcID);
                                                            if (dffcTemp == null || dffcTemp.GetFeatureClass() == null) continue;
                                                            if (dffcTemp.GetFacilityClassName() != "PipeNode") continue;
                                                            IQueryFilter filter = new QueryFilter();
                                                            filter.WhereClause = "OBJECTID =" + oid ;
                                                            filter.SubFields = "OBJECTID ,SHAPE";
                                                            IFeature feature = null;
                                                            IFeatureCursor cursor = null;
                                                            try
                                                            {
                                                                cursor = dffcTemp.GetFeatureClass().Search(filter, false);
                                                                while ((feature = cursor.NextFeature()) != null)
                                                                {
                                                                    if (feature.Shape != null && feature.Shape is IGeometry)
                                                                    {
                                                                        IGeometry geo = feature.Shape as IGeometry;
                                                                        switch (geo.GeometryType)
                                                                        {
                                                                            case esriGeometryType.esriGeometryPoint:
                                                                                IPoint pt = geo as IPoint;
                                                                                //pt.Z = pt.Z + 1;
                                                                                listPt.Add(pt);
                                                                                pointCol.AddPoint(pt);
                                                                                break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            catch (System.Exception ex)
                                                            {
                                                            	
                                                            }
                                                            finally
                                                            {
                                                                if (cursor != null)
                                                                {
                                                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                                                    cursor = null;
                                                                }
                                                                if (feature != null)
                                                                {
                                                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(feature);
                                                                    feature = null;
                                                                }
                                                            }
                                                        }
                                                        if (listPt.Count > 0)
                                                        {
                                                            IPolyline polyline = pointCol as IPolyline;
                                                            ISimpleLineSymbol pLineSymbol = new SimpleLineSymbol();
                                                            pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                                                            pLineSymbol.Width = 5;
                                                            pLineSymbol.Color = GetRGBColor(0, 230, 240);
                                                            IElement elementL = new LineElement();
                                                            elementL.Geometry = polyline;
                                                            ILineElement pLineElement = elementL as ILineElement;
                                                            pLineElement.Symbol = pLineSymbol;
                                                            pGC.AddElement(elementL, 0);

                                                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                                                            simpleMarkerSymbol.Color = GetRGBColor(255, 0, 0);
                                                            simpleMarkerSymbol.Outline = false;
                                                            simpleMarkerSymbol.Size = 5;
                                                            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                                                            
                                                            foreach (IPoint pt in listPt)
                                                            {
                                                                try
                                                                {
                                                                    IMarkerElement pMarkerElement = new MarkerElementClass();
                                                                    pMarkerElement.Symbol = simpleMarkerSymbol;
                                                                    IElement pElement = pMarkerElement as IElement;
                                                                    pElement.Geometry = pt;
                                                                    pGC.AddElement(pElement, 0);
                                                                }
                                                                catch (System.Exception ex)
                                                                {
                                                                    continue;
                                                                }
                                                                
                                                            }                                                         
                                                        }
                                                        
                                                        app.Current2DMapControl.ActiveView.Refresh();
                                                    }
                                                    else
                                                    {
                                                        WaitForm.Stop();
                                                        XtraMessageBox.Show("两点不连通！", "提示");
                                                        pGC.DeleteAllElements();
                                                        
                                                    }
                                                }                                                
                                            }
                                        }                                        
                                    }                                                                   
                                }
                                if (haveone) break;
                            }
                            if (haveone) break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("分析出错！", "提示");
            }
            finally
            {
                WaitForm.Stop();
            }
        }

        public void AddCallout(IPoint pPoint, string strText)
        {
            DF2DApplication app = DF2DApplication.Application;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IGraphicsContainer pGraphicsContainer = m_ActiveView.GraphicsContainer;
            IPoint pPointText = new PointClass();
            pPointText.PutCoords(pPoint.X + 1.5, pPoint.Y + 1.5);

            ITextElement pTextElement = new TextElementClass();
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();

            IElement pElement = pTextElement as IElement;
            pTextElement.Text = strText;
            pTextElement.ScaleText = true;
            pElement.Geometry = pPointText;

            IRgbColor pRgbColor = GetRGBColor(255, 255, 0);

            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = pRgbColor;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSHollow;

            IBalloonCallout pBalloonCallout = new BalloonCalloutClass();
            pBalloonCallout.Symbol = pSmplFill;
            pBalloonCallout.Style = esriBalloonCalloutStyle.esriBCSOval;
            pBalloonCallout.AnchorPoint = pPointText;
            pBalloonCallout.LeaderTolerance = 5;

            pRgbColor = GetRGBColor(255, 0,0);

            pTextSymbol.Background = pBalloonCallout as ITextBackground;
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Size = (app.Current2DMapControl.MapScale / 100) * 5;
            //pTextSymbol.Size = 25;
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

            pTextElement.Symbol = pTextSymbol;


            pGraphicsContainer.AddElement(pElement, 1);



        }

        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = red;
            pRgbColor.Green = green;
            pRgbColor.Blue = blue;
            return pRgbColor;
        }


        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(true);
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
         

         
        private void Clear()
        {

        }
        private void ClickQuery()
        {

        }
    }
}
