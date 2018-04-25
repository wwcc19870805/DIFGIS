using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using DFWinForms.Service;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geometry;
using System.Collections;
using ESRI.ArcGIS.Display;
using DFCommon.Class;
using System.Drawing;
using DF2DCreate.Class;
using DevExpress.XtraEditors;
using DFWinForms.Class;
using DFDataConfig.Logic;
using DF2DPipe.Class;

namespace DF2DCreate.Command
{
    class CmdLabelLineProperty2D : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_pMap;
        private IActiveView m_pActiveView;
        IScreenDisplay m_Display;
        private bool m_bSelect = true;
        private IGeometry m_pGeoTrack = null;
        private IntersectPipe m_IntersectPipe;
        private double dblStartX;
        private double dblStartY;
        private int nWidth = 8; //标注表格的单位宽高
        private int nHeight = 2;
        string[] fields = new string[] { "Classify", "StartNo", "EndNo", "Material", "Coverstyle", "Diameter", "proad" };

        public override void Run(object sender, EventArgs e)
        {
            Map2DCommandManager.Push(this);
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            m_pActiveView = this.m_pMapControl.ActiveView;
            m_pMap = m_pMapControl.ActiveView.FocusMap;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            if (app == null || app.Current2DMapControl == null) return;
            m_pActiveView = app.Current2DMapControl.ActiveView;
            m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            IFeatureCursor pFeaCur;
            IFeature pFeature;
            string classify = "";
            string startNo = "";
            string endNo = "";
            string material = "";
            string coverstyle = "";
            string diameter = "";
            string road = "";
            bool have = false;
            try
            {
                if (button == 1)
                {
                    IRubberBand band = new RubberRectangularPolygonClass();
                    m_pGeoTrack = band.TrackNew(m_Display, null);

                    if (m_pGeoTrack.IsEmpty)
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        m_pGeoTrack = DF2DPipe.Class.PublicFunction.DoBuffer(searchPoint, DF2DPipe.Class.PublicFunction.ConvertPixelsToMapUnits(m_pActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    WaitForm.Start("正在查询...", "请稍后");
                    foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                    {
                        if (have) break;
                        string[] arrFc2DId = mc.Fc2D.Split(';');//将二级大类所对应的要素类ID转换为数组
                        if (arrFc2DId == null) continue;
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            if (have) break;
                            if (!sc.Visible2D) continue;
                            foreach (string fc2DId in arrFc2DId)//遍历二级子类所对应的要素类ID
                            {
                                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                                if (dffc == null) continue;
                                FacilityClass fcc = dffc.GetFacilityClass();
                                if (fcc.Name != "PipeLine") continue;
                                IFeatureLayer fl = dffc.GetFeatureLayer();
                                IFeatureClass fc = dffc.GetFeatureClass();
                                ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                                pSpatialFilter.Geometry = m_pGeoTrack;
                                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                pSpatialFilter.WhereClause = "SMSCODE =  '" + sc.Name + "'";
                                pFeaCur = fc.Search(pSpatialFilter, false);
                                pFeature = pFeaCur.NextFeature();
                                if (pFeature == null) continue;
                                foreach (string field in fields)
                                {
                                    DFDataConfig.Class.FieldInfo fi = fcc.GetFieldInfoBySystemName(field);
                                    if (fi == null) continue;
                                    int index = fc.Fields.FindField(fi.Name);
                                    object obj = pFeature.get_Value(index);
                                    switch (field)
                                    {
                                        case "Classify":
                                            classify = obj.ToString();
                                            break;
                                        case "StartNo":
                                            startNo = obj.ToString();
                                            break;
                                        case "EndNo":
                                            endNo = obj.ToString();
                                            break;
                                        case "Material":
                                            material = obj.ToString();
                                            break;
                                        case "Coverstyle":
                                            coverstyle = obj.ToString();
                                            break;
                                        case "Diameter":
                                            diameter = obj.ToString();
                                            break;
                                        case "proad":
                                            road = obj.ToString();
                                            break;

                                    }
                                }
                                m_IntersectPipe= new IntersectPipe(pFeature, fl, 0, classify, startNo, endNo, material, coverstyle, diameter, road);
                                have = true;
                            }
                        }
                    }
                }
                if (m_IntersectPipe == null)
                {
                    XtraMessageBox.Show("请重新获取单个管线");
                    WaitForm.Stop();
                }
                WaitForm.SetCaption("正在输出属性，请稍后...");
                DrawNodeLabels(m_IntersectPipe.Feature, m_IntersectPipe);
                WaitForm.Stop();
            }
            catch (System.Exception ex)
            {
            	
            }

        }

        private void DrawNodeLabels(IFeature feature, IntersectPipe ip)
        {
            if (feature == null) return;
            IPolyline pline = feature.Shape as IPolyline;
            IPoint p = pline.FromPoint;
            dblStartX = p.X;
            dblStartY = p.Y;
            IPoint p1 = new PointClass();
            IPoint p2 = new PointClass();
            IPoint p3 = new PointClass();
            IPoint p4 = new PointClass();
            IPoint p5 = new PointClass();
            IPoint p6 = new PointClass();
            IPoint p7 = new PointClass();
            IPoint p8 = new PointClass();
            double yOffset = 1.5;
            double xOffset = 1.0;
            p1.PutCoords(p.X, p.Y + nHeight * fields.Length / 2 - yOffset);
            p2.PutCoords(p.X + xOffset, p.Y + nHeight * fields.Length - yOffset);
            p3.PutCoords(p2.X + nWidth, p2.Y);
            p4.PutCoords(p2.X + 2 * nWidth, p2.Y);
            p7.PutCoords(p2.X, p2.Y - nHeight * fields.Length);
            p6.PutCoords(p3.X, p7.Y);
            p5.PutCoords(p4.X, p7.Y);
            p8.PutCoords(p2.X, p1.Y);

            //画线
            DrawLine(p, p1);
            DrawLine(p1, p8);
            DrawLine(p8, p2);
            DrawLine(p2, p3);
            DrawLine(p3, p4);
            DrawLine(p4, p5);
            DrawLine(p5, p6);
            DrawLine(p6, p7);
            DrawLine(p7, p8);
            DrawLine(p3, p6);

            DrawNodeInfo(p2, ip);

        }

        private void DrawLine(IPoint p1, IPoint p2)
        {
            IPolyline pline = new PolylineClass();
            pline.FromPoint = p1;
            pline.ToPoint = p2;

            ILineElement lineElement;
            lineElement = new LineElementClass();
            IElement element;
            ISimpleLineSymbol pSLnSym;
            IRgbColor pRGB;
            pRGB = new RgbColorClass();
            pRGB.Red = 0;
            pRGB.Green = 0;
            pRGB.Blue = 0;
            pSLnSym = new SimpleLineSymbolClass();
            pSLnSym.Color = pRGB;
            pSLnSym.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid;
            lineElement.Symbol = pSLnSym;
            element = lineElement as IElement;
            element.Geometry = pline;
            IGraphicsContainer graphicsContainer = this.m_pMapControl.Map as IGraphicsContainer;
            graphicsContainer.AddElement(element, 0);
            this.m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void DrawNodeInfo(IPoint point, IntersectPipe ip)
        {
            IGraphicsContainer pGraContainer = m_pMapControl.Map as IGraphicsContainer;
            ITextElement pTextElement;
            IElement pElement;
            IPoint pAnnoPnt = new PointClass();
            ITextSymbol sym = new TextSymbolClass();
            stdole.IFontDisp myfont = (stdole.IFontDisp)new stdole.StdFontClass();
            myfont.Name = "华文细黑";
            sym.Font = myfont;

            myfont.Bold = true;
            sym.Size = 8;//SystemInfo.Instance.TextSize;

            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            sym.Color = pColor;

            double xoffset = 4;
            double yoffset = 1.5;
            int i = 0;
            foreach (string fi in fields)
            {
                sym.Text = fi;
                pAnnoPnt.PutCoords(point.X + xoffset, point.Y - yoffset - i * nHeight);
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                switch (fi)
                {
                    case "Classify":
                        sym.Text = ip.Classify;
                        break;
                    case "StartNo":
                        sym.Text = ip.StartNo;
                        break;
                    case "EndNo":
                        sym.Text = ip.EndNo;
                        break;
                    case "Material":
                        sym.Text = ip.Material;
                        break;
                    case "Coverstyle":
                        sym.Text = ip.Coverstyle;
                        break;
                    case "Diameter":
                        sym.Text = ip.Diameter;
                        break;
                    case "proad":
                        sym.Text = ip.Road;
                        break;
                }
                pAnnoPnt.PutCoords(point.X + xoffset + nWidth, point.Y - yoffset - i * nHeight);
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
                i++;
                (m_pMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pMapControl.Extent);
            }
        }

        public override void RestoreEnv()
        {

            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            Map2DCommandManager.Pop();

        }
    }
}
