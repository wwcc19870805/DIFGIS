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
    class CmdBurstAnalysis2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private string _EdgeFCID;
        private int _EdgeOID;
        private string _EdgeId;
        Edge edge;
        Node preNode;
        Node nextNode;
        IRgbColor color;
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
            int preCount = 0;
            int nextCount = 0;
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
                            if (haveone) break;
                            foreach (MajorClass mc in lg.MajorClasses)
                            {
                                if (haveone) break;
                                string[] arrFc2DId = mc.Fc2D.Split(';');
                                if (arrFc2DId == null) continue;
                                IFeatureCursor pFeatureCursor = null;
                                IFeature pFeature = null;
                                //DFDataConfig.Class.FieldInfo fi;
                                //int indexFusu = 0;
                                ////string nodefcId = null;
                                foreach (SubClass sc in mc.SubClasses)
                                {
                                    if (haveone) break;
                                    if (!sc.Visible2D) continue;
                                    foreach (string fc2DId in arrFc2DId)
                                    {
                                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                        if (dffc == null) continue;
                                        IFeatureClass fc = dffc.GetFeatureClass();
                                        FacilityClass facc = dffc.GetFacilityClass();
                                        if (facc.Name != "PipeLine") continue;
                                        ISpatialFilter pSpatialFilter = new SpatialFilter();
                                        pSpatialFilter.Geometry = pGeo;
                                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                        pFeatureCursor = fc.Search(pSpatialFilter, false);
                                        if (pFeatureCursor == null) continue;
                                        pFeature = pFeatureCursor.NextFeature();
                                        if (pFeature == null) continue;
                                        IGeometry pGeometry = pFeature.Shape as IGeometry;
                                        if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)
                                        {
                                            IPolyline pLine = pGeometry as IPolyline;
                                            this._EdgeFCID = fc.FeatureClassID.ToString();
                                            this._EdgeOID = pFeature.OID;
                                            color = GetRGBColor(0, 230, 240);
                                            IElement lineElement = LineElementRenderer(pLine, color);
                                            pGC.AddElement(lineElement, 0);

                                        }
                                        haveone = true;
                                        break;
                                        
                                    }

                                    foreach (string fc2DId in arrFc2DId)
                                    {
                                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                        if (dffc == null) continue;
                                        IFeatureClass fc = dffc.GetFeatureClass();
                                        FacilityClass facc = dffc.GetFacilityClass();
                                        IFeatureLayer fl = dffc.GetFeatureLayer();
                                        if (fc == null || pGeo == null || fl == null) continue;
                                        if (!fl.Visible) continue;
                                        if (facc.Name != "PipeNode") continue;
                                        DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName("Additional");
                                        IFields fiCol = fc.Fields;
                                        int indexFusu = fiCol.FindField(fi.Name);

                                        WaitForm.Start("正在分析...", "请稍后");
                                        TopoClass2D tc = FacilityInfoService2D.GetTopoClassByFeatureClassID(fc2DId);
                                        if (tc == null)
                                        {
                                            WaitForm.Stop();
                                            return;
                                        }
                                        TopoNetwork net = tc.GetNetwork();
                                        if (net == null)
                                        {
                                            WaitForm.Stop();
                                            XtraMessageBox.Show("构建拓扑网络失败！", "提示");
                                            return;
                                        }
                                        else
                                        {
                                            
                                            HashSet<string> valveIds = new HashSet<string>();
                                            if (!string.IsNullOrEmpty(fc2DId) && ValveManager.Instance.Exists(fc2DId))
                                            {
                                                
                                                valveIds = ValveManager.Instance.GetValveIds(fc2DId);
                                            }
                                            else
                                            {
                                                IFeature feature;
                                                string fusu;
                                                //IQueryFilter filter = new QueryFilter();
                                                
                                                //filter.WhereClause = fi.Name + " LIKE '%阀%'";
                                                IFeatureCursor cursor = fc.Search(null, false);
                                                int n = fc.FeatureCount(null);
                                                if (indexFusu == 0) return;
                                                while ((feature = cursor.NextFeature()) != null)
                                                {
                                                    //valveIds.Add(fc2DId + "_" + feature.OID.ToString());
                                                    fusu = feature.get_Value(indexFusu).ToString();
                                                    if (fusu == "阀门" || fusu == "阀门井")
                                                    {
                                                        valveIds.Add(fc2DId + "_" + feature.OID.ToString());
                                                    }
                                                }
                                                ValveManager.Instance.Add(fc2DId, valveIds);
                                            }
                                            //string edgeID = this._EdgeFCID + "_" + this._EdgeOID.ToString();
                                            if (EdgeManager.Instance.Exists(this._EdgeFCID,this._EdgeOID.ToString()))
                                                edge = EdgeManager.Instance.GetEdgeByID(this._EdgeFCID,this._EdgeOID.ToString());
                                            preNode = edge.PreNode;
                                            nextNode = edge.NextNode;                                           
                                            HashSet<string> recordPre = new HashSet<string>();
                                            HashSet<string> recordNext = new HashSet<string>();
                                            color = GetRGBColor(255,0,0);
                                            net.BGFX(preNode.ID, nextNode.ID, valveIds, ref recordPre, ref recordNext);
                                            if (recordPre.Count <= 0 && recordNext.Count <= 0) continue;
                                            preCount = recordPre.Count;
                                            nextCount = recordNext.Count;
                                            foreach (string s in recordPre)
                                            {
                                                int id;
                                                Node n = NodeManager.Instance.GetNodeByID(s);
                                                Int32.TryParse(n.FeatureId, out id);
                                                IFeature feature = fc.GetFeature(id);
                                                IPoint point = feature.Shape as IPoint;
                                                //color = GetRGBColor(255, 0, 0);
                                                IElement elementpPoint = PointElementRenderer(point, color);
                                                pGC.AddElement(elementpPoint, 0);

                                                IElement elementText = AddCallout(app.Current2DMapControl, point, "需关闭阀门", color);
                                                pGC.AddElement(elementText, 1);
                                                app.Current2DMapControl.CenterAt(point);
                                                
                                                
                                            }
                                            foreach (string s in recordNext)
                                            {
                                                int id;
                                                Node n = NodeManager.Instance.GetNodeByID(s);
                                                Int32.TryParse(n.FeatureId, out id);
                                                IFeature feature = fc.GetFeature(id);
                                                IPoint point = feature.Shape as IPoint;
                                                color = GetRGBColor(0, 0, 0);
                                                IElement elementpPoint = PointElementRenderer(point, color);
                                                pGC.AddElement(elementpPoint, 0);

                                                IElement elementText = AddCallout(app.Current2DMapControl, point, "需关闭阀门", color);
                                                pGC.AddElement(elementText, 1);
                                                app.Current2DMapControl.CenterAt(point);
                                                
                                            }
                                            app.Current2DMapControl.MapScale = 500;
                                            app.Current2DMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);                                      
                                        }
                                        if (haveone) break;
                                    }
                                }
                                
                            }
                        }
                    }
                }
               
            }
            catch (System.Exception ex)
            {
            	
            }
            finally
            {
                WaitForm.Stop();
                XtraMessageBox.Show("上游需关闭阀门：" + preCount + "\n下游需关闭阀门：" + nextCount, "提示");
                SuspendCommand();
            }
        }
        private IElement LineElementRenderer(IGeometry geo,IRgbColor color)
        {
            ISimpleLineSymbol pLineSymbol = new SimpleLineSymbol();
            pLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            pLineSymbol.Width = 5;
            pLineSymbol.Color = color;
            IElement element = new LineElement();
            element.Geometry = geo;
            ILineElement pLineElement = element as ILineElement;
            pLineElement.Symbol = pLineSymbol;
            return element;
        }

        private IElement PointElementRenderer(IGeometry geo, IRgbColor color)
        {
            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
            simpleMarkerSymbol.Color = color;
            simpleMarkerSymbol.Outline = false;
            simpleMarkerSymbol.Size = 5;
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

            IMarkerElement pMarkerElement = new MarkerElementClass();
            pMarkerElement.Symbol = simpleMarkerSymbol;
            IElement pElement = pMarkerElement as IElement;
            pElement.Geometry = geo;
            return pElement;
        }
        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = red;
            pRgbColor.Green = green;
            pRgbColor.Blue = blue;
            return pRgbColor;
        }

        public IElement AddCallout(IMapControl2 mapControl,IPoint pPoint, string strText,IRgbColor color)
        {              
            IPoint pPointText = new PointClass();
            pPointText.PutCoords(pPoint.X + 1.5, pPoint.Y + 1.5);

            ITextElement pTextElement = new TextElementClass();
            IFormattedTextSymbol pTextSymbol = new TextSymbolClass();

            IElement pElement = pTextElement as IElement;
            pTextElement.Text = strText;
            pTextElement.ScaleText = true;
            pElement.Geometry = pPointText;

            ISimpleFillSymbol pSmplFill = new SimpleFillSymbolClass();
            pSmplFill.Color = color;
            pSmplFill.Style = esriSimpleFillStyle.esriSFSHollow;

            IBalloonCallout pBalloonCallout = new BalloonCalloutClass();
            pBalloonCallout.Symbol = pSmplFill;
            pBalloonCallout.Style = esriBalloonCalloutStyle.esriBCSOval;
            pBalloonCallout.AnchorPoint = pPointText;
            pBalloonCallout.LeaderTolerance = 5;


            pTextSymbol.Background = pBalloonCallout as ITextBackground;
            pTextSymbol.Color = color;
            pTextSymbol.Size = (mapControl.MapScale / 100) * 5;
            //pTextSymbol.Size = 25;
            pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            pTextElement.Symbol = pTextSymbol;

            return pElement;
        }
        private void SuspendCommand()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
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
    }
}
