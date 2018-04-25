using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using System.Data;
using System.Collections;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using DFWinForms.Class;
using DF2DPipe.Class;
using DFDataConfig.Logic;
using DFDataConfig.Class;
using DF2DData.Class;


namespace DF2DAnalysis.Commands
{
    class CmdFlowDirectionAnalysis2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        public override void Run(object sender, EventArgs e)
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
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            bool ready = true;
            if (app == null || app.Current2DMapControl == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            try
            {
                if (button == 1)
                {
                    ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
                    IRgbColor pColor = new RgbColorClass();
                    pColor.Red = 255;
                    pColor.Green = 255;
                    pColor.Blue = 0;
                    pLineSym.Color = pColor;
                    pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSym.Width = 2;

                    ISimpleFillSymbol pFillSym = new SimpleFillSymbol();

                    pFillSym.Color = pColor;
                    pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                    pFillSym.Outline = pLineSym;

                    object symbol = pFillSym as object;
                    IRubberBand band = new RubberRectangularPolygonClass();
                    IGeometry geo = band.TrackNew(m_Display, null);
                    app.Current2DMapControl.DrawShape(geo, ref symbol);
                    WaitForm.Start("正在查询...", "请稍后");

                    if (geo.IsEmpty)
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        geo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {
                        foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                        {
                            if (mc.Alias == "电力" || mc.Alias == "通信" || mc.Alias == "架空") continue;
                            string[] arrFc2DId = mc.Fc2D.Split(';');
                            if (arrFc2DId == null) continue;
                            foreach (SubClass sc in mc.SubClasses)
                            {
                                if (!sc.Visible2D) continue;
                                foreach (string fc2DId in arrFc2DId)
                                {
                                    DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                    if (dffc == null) continue;
                                    FacilityClass facc = dffc.GetFacilityClass();
                                    IFeatureClass fc = dffc.GetFeatureClass();
                                    if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                                    DFDataConfig.Class.FieldInfo fiDirection = facc.GetFieldInfoBySystemName("FlowDirection");
                                    if(fiDirection == null) continue;

                                    IFields pFields = fc.Fields;
                                    //string[] name = new string[pFields.FieldCount];
                                    //for (int i = 0; i < pFields.FieldCount; i++)
                                    //{
                                    //    name[i] = pFields.get_Field(i).Name;
                                    //}
                                    int indexDirection = pFields.FindField(fiDirection.Name);
                                    if(indexDirection < 0) continue;
                                    IField pField = pFields.get_Field(indexDirection);
                                    ISpatialFilter filter = new SpatialFilter();
                                    filter.Geometry = geo;
                                    filter.SubFields = pField.Name;
                                    filter.WhereClause = mc.ClassifyField + " =  '" + sc.Name + "'";
                                    filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                    if (fc == null || geo == null) return ;

                                    IFeatureCursor pFeatureCursor = null;
                                    IFeature pFeature = null;
                                    
                                    
                                    try
                                    {
                                        pFeatureCursor = fc.Search(filter, false);
                                        esriFlowDirection flowDirection = new esriFlowDirection();
                                        while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                        {
                                            object tempobj = pFeature.get_Value(indexDirection);
                                            int dtemp;
                                            if (tempobj != null && Int32.TryParse(tempobj.ToString(), out dtemp))
                                            {
                                                switch (dtemp)
                                                {
                                                    case 0:
                                                        flowDirection = esriFlowDirection.esriFDWithFlow;
                                                        break;
                                                    case 1:
                                                        flowDirection = esriFlowDirection.esriFDAgainstFlow;
                                                      break;                                                       
                                                }
                                            }
                                            else
                                            {
                                                flowDirection = esriFlowDirection.esriFDIndeterminate;
                                            }
                                            IPolyline polyline = pFeature.Shape as IPolyline;
                                            IPoint middlePoint = new PointClass();
                                            polyline.QueryPoint(esriSegmentExtension.esriNoExtension, polyline.Length / 2, false, middlePoint);

                                            IArrowMarkerSymbol arrowMarkerSymbol = new ArrowMarkerSymbolClass();
                                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
                                            IElement element = null;
                                            if (flowDirection == esriFlowDirection.esriFDWithFlow)
                                            {
                                                arrowMarkerSymbol.Angle = GetLineAngleFrom2Points(polyline.FromPoint, polyline.ToPoint);
                                                arrowMarkerSymbol.Color = GetColorByRGBValue(0, 0, 0);
                                                arrowMarkerSymbol.Size = 12;
                                                element = new MarkerElementClass();
                                                element.Geometry = middlePoint;
                                                ((IMarkerElement)element).Symbol = arrowMarkerSymbol;
                                                ((IElementProperties)element).Name = "Flow";
                                            }
                                            else if (flowDirection == esriFlowDirection.esriFDAgainstFlow)
                                            {
                                                simpleMarkerSymbol.Angle = GetLineAngleFrom2Points(polyline.ToPoint, polyline.FromPoint);
                                                arrowMarkerSymbol.Color = GetColorByRGBValue(0, 0, 0);
                                                arrowMarkerSymbol.Size = 12;
                                                element = new MarkerElementClass();
                                                element.Geometry = middlePoint;
                                                ((IMarkerElement)element).Symbol = arrowMarkerSymbol;
                                                ((IElementProperties)element).Name = "Flow";
                                            }
                                            else if (flowDirection == esriFlowDirection.esriFDIndeterminate)
                                            {
                                                
                                                simpleMarkerSymbol.Color = GetColorByRGBValue(0, 0, 0);
                                                simpleMarkerSymbol.Size = 8;
                                                element = new MarkerElementClass();
                                                element.Geometry = middlePoint;
                                                ((IMarkerElement)element).Symbol = simpleMarkerSymbol;
                                                ((IElementProperties)element).Name = "Flow";
                                            }
                                            else
                                            {
                                                simpleMarkerSymbol.Color = GetColorByRGBValue(255, 0, 0);
                                                simpleMarkerSymbol.Size = 8;
                                                element = new MarkerElementClass();
                                                element.Geometry = middlePoint;
                                                ((IMarkerElement)element).Symbol = simpleMarkerSymbol;
                                                ((IElementProperties)element).Name = "Flow";
                                            }
                                            gc.AddElement(element, 0);

                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                    	
                                    }

                                }
                            }
                        }
                        WaitForm.Stop();
                        app.Current2DMapControl.ActiveView.Refresh();
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private double GetLineAngleFrom2Points(IPoint startPoint, IPoint endPoint)
        {
            double angle = -1;

            if (startPoint.Y == endPoint.Y)
            {
                if (startPoint.X < endPoint.X)                
                    angle = 0;               
                else
                    angle = 180;
            }
            else if (startPoint.X == endPoint.X)
            {
                if (startPoint.Y < endPoint.Y)
                    angle = 90;
                else
                    angle = 270;
            }
            else if (startPoint.Y < endPoint.Y)
            {
                if (startPoint.X < endPoint.X)
                    angle = (Math.Atan((endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X))) * 180 / Math.PI;
                else
                    angle = Math.Atan((startPoint.X - endPoint.X) / (endPoint.Y - startPoint.Y)) * 180 / Math.PI + 90;


            }
            else
            {
                if (endPoint.X < startPoint.X)
                    angle = Math.Atan((startPoint.Y - endPoint.Y) / (startPoint.X - endPoint.X)) * 180 / Math.PI + 180;
                else
                    angle = 360 - Math.Atan((startPoint.Y - endPoint.Y) / (endPoint.X - startPoint.X)) * 180 / Math.PI;
            }
            return angle;
        }
        private IRgbColor GetColorByRGBValue(int r, int g, int b)
        {
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = r;
            pColor.Green = g;
            pColor.Blue = b;
            return pColor;
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
