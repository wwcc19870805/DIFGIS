using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using DF2DCreate.Class;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using stdole;
using DF2DData.Class;
using DFDataConfig.Class;
using DFCommon.Class;
using System.Drawing;
using DevExpress.XtraEditors;
using DFDataConfig.Logic;
using DFWinForms.Class;
namespace DF2DCreate.Command
{
    class CmdCreateLineLabel2D : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_pMap;
        private IActiveView m_pActiveView;
        private bool m_bSelect = true;
        private IGeometry m_pGeoTrack = null;
        private List<IntersectPipe> m_IntersectPipes;
        private double dblStartX;
        private double dblStartY;
        private int nWidth = 8; //标注表格的单位宽高
        private int nHeight = 2;

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
            IScreenDisplay pScr = m_pActiveView.ScreenDisplay;
            //跟踪画线
            IRubberBand pRB = new RubberLineClass();
            m_pGeoTrack = pRB.TrackNew(pScr, null);
            if ((m_pGeoTrack as IPointCollection).PointCount != 2)
            {
                XtraMessageBox.Show("请使用鼠标制定两点确定扯旗的管线");
                return;
            }
            Element.DeleteElement(m_pActiveView.GraphicsContainer, "cq");
            Element.AddGraphics(m_pActiveView.GraphicsContainer, m_pGeoTrack, GetDefaultSymbol(esriGeometryType.esriGeometryPolyline), "dmx");
            IElement pElement = Element.GetElementByName(m_pMapControl.ActiveView.GraphicsContainer, "cq");
            if (pElement != null)
            {
                m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            base.OnMouseDown(button, shift, x, y, mapX, mapY);

            IFeature pFeature;
            IGeometry pGeo;
            IGeometry pPipeLineGeo;
            IFeatureClass pFeatureClass;
            IFeatureCursor pFeaCur;
            ITopologicalOperator pTopo;
            string classify = "";
            string startNo = "";
            string endNo = "";
            string material = "";
            string coverstyle = "";
            string diameter = "";
            string road = "";
            double distance;
            m_IntersectPipes = new List<IntersectPipe>();
            


            string[] fields = new string[]{"Classify","StartNo","EndNo","Material","Coverstyle","Diameter","proad"};
            foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())
            {
                WaitForm.Start("正在查询...", "请稍后");
                string[] arrFc2DId = mc.Fc2D.Split(';');//将二级大类所对应的要素类ID转换为数组
                if (arrFc2DId == null) continue;

                foreach (SubClass sc in mc.SubClasses)
                {
                    if (!sc.Visible2D) continue;
                    foreach (string fc2DId in arrFc2DId)//遍历二级子类所对应的要素类ID
                    {
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                        if (dffc == null) continue;
                        FacilityClass fcc = dffc.GetFacilityClass();
                        if (fcc.Name != "PipeLine") continue;
                        IFeatureLayer fl = dffc.GetFeatureLayer();                    

                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.Geometry = m_pGeoTrack;
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        pSpatialFilter.WhereClause = "SMSCODE =  '" + sc.Name + "'";
                        IFeatureClass fc = dffc.GetFeatureClass();
                        pFeaCur = fc.Search(pSpatialFilter, false);
                        while ((pFeature = pFeaCur.NextFeature()) != null)
                        {
                            foreach(string field in fields)
                            {
                               DFDataConfig.Class.FieldInfo  fi = fcc.GetFieldInfoBySystemName(field);
                               if(fi == null) continue;
                               int index  = fc.Fields.FindField(fi.Name);
                               object obj = pFeature.get_Value(index);
                                switch(field)
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
                            IPolyline pline = m_pGeoTrack as IPolyline;
                            IPoint point1 = pline.ToPoint;
                            ITopologicalOperator topo = pFeature.Shape as ITopologicalOperator;
                            IGeometry geo = topo.Intersect(m_pGeoTrack,esriGeometryDimension.esriGeometry0Dimension);
                            IPointCollection pointCol = geo as IPointCollection;                          
                            IPoint point2 = pointCol.get_Point(pointCol.PointCount -1);
                            distance = GetDistanceOfTwoPoints(point1,point2);
                            IntersectPipe interPipe = new IntersectPipe(pFeature, fl, distance, classify, startNo, endNo, material, coverstyle, diameter, road);
                            m_IntersectPipes.Add(interPipe);
                        }
                    }
                }
            }
            if (m_IntersectPipes.Count == 0) return;
            List<IntersectPipe> orderList = m_IntersectPipes.OrderBy(i => i.Distance).ToList<IntersectPipe>();
            DrawPipeLabels();
           
            int n = 0;
            foreach (IntersectPipe interPipe in orderList)
            {
                WaitForm.SetCaption("正在输出属性，请稍后...");
                DrawPipeInfo(interPipe,n);
                ++n;
            }
            WaitForm.Stop();
        }
        private void DrawPipeInfo(IntersectPipe interPipe,int i)
        {
            try
            {
                IGraphicsContainer pGraContainer = m_pMapControl.Map as IGraphicsContainer;
                IPoint pAnnoPnt = new PointClass();
                ITextSymbol sym = new TextSymbolClass();
                stdole.IFontDisp myfont = (stdole.IFontDisp)new stdole.StdFontClass();
                myfont.Name = "华文细黑";
                sym.Font = myfont;
                myfont.Bold = true;
                sym.Size = 8/*SystemInfo.Instance.TextSize*/;
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                IFeatureRenderer FeatureRenderer = new SimpleRendererClass();
                IGeoFeatureLayer geolyr = interPipe.FeaLayer as IGeoFeatureLayer;
                if (geolyr == null) return;
                FeatureRenderer = geolyr.Renderer;
                //获取此图层的Symbol
                pSimpleLineSymbol = (ISimpleLineSymbol)FeatureRenderer.get_SymbolByFeature(interPipe.Feature);
                sym.Color = pSimpleLineSymbol.Color;

                //类别
                sym.Text = interPipe.Classify;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                ITextElement pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                IElement pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //起点点号
                sym.Text = interPipe.StartNo;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 2, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
                //终点点号
                sym.Text = interPipe.EndNo;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 3, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //材质
                sym.Text = interPipe.Material;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 4, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
                //埋设方式
                sym.Text = interPipe.Coverstyle;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 5, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //管径
                sym.Text = interPipe.Diameter;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 6, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //所在道路
                sym.Text = interPipe.Road;
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 7, dblStartY + nHeight * (m_IntersectPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void DrawPipeLabels()
        {
            WaitForm.SetCaption("正在绘图，请稍后...");
            //确定画线终点坐标
            IPoint p = (m_pGeoTrack as IPolyline).ToPoint;
            dblStartX = p.X;
            dblStartY = p.Y;

            IPoint p1 = new PointClass();
            IPoint p2 = new PointClass();
            IPoint p3 = new PointClass();

            p3.X = dblStartX;
            p3.Y = dblStartY - 1;
            p1.X = dblStartX + nWidth * 8;
            p1.Y = dblStartY - 1;
            p2.X = dblStartX;
            p2.Y = dblStartY + nHeight * m_IntersectPipes.Count;

            //画线
            IPolyline line1 = new PolylineClass();
            //ILine line1 = new LineClass();
            line1.FromPoint = p3;
            line1.ToPoint = p1;
            DrawLine(line1);

            IPolyline line2 = new PolylineClass();
            line2.FromPoint = p3;
            line2.ToPoint = p2;
            DrawLine(line2);

            //画信息栏列名
            DrawColName();

        }
        private void DrawLine(IPolyline line)
        {
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
            element.Geometry = line;
            IGraphicsContainer graphicsContainer = this.m_pMapControl.Map as IGraphicsContainer;
            graphicsContainer.AddElement(element, 0);
            this.m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void DrawColName()
        {
            IGraphicsContainer pGraContainer = m_pMapControl.Map as IGraphicsContainer;

            IPoint pAnnoPnt = new PointClass();
            ITextSymbol sym = new TextSymbolClass();
            stdole.IFontDisp myfont = (stdole.IFontDisp)new stdole.StdFontClass();
            myfont.Name = "华文细黑";
            sym.Font = myfont;

            myfont.Bold = true;
            sym.Size = SystemInfo.Instance.TextSize;

            Color color = ColorTranslator.FromHtml(SystemInfo.Instance.TextColor);
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            sym.Color = pColor;

            //类别
            sym.Text = "类别";
            pAnnoPnt.PutCoords(dblStartX + nWidth, dblStartY + nHeight * m_IntersectPipes.Count);

            ITextElement pTextElement = new TextElementClass();
            pTextElement.ScaleText = true;
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            IElement pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //起点点号
            sym.Text = "起点点号";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 2, dblStartY + nHeight * m_IntersectPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //终点点号
            sym.Text = "终点点号";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 3, dblStartY + nHeight * m_IntersectPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //材质
            sym.Text = "材质";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 4, dblStartY + nHeight * m_IntersectPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //埋设方式
            sym.Text = "埋设方式";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 5, dblStartY + nHeight * m_IntersectPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //管径
            sym.Text = "管径";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 6, dblStartY + nHeight * m_IntersectPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //所在道路
            sym.Text = "所在道路";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 7, dblStartY + nHeight * m_IntersectPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            (m_pMapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_pMapControl.Extent);

        }
        #region 选色
        /// <summary>
        /// 获得指定红绿蓝值的颜色
        /// </summary>
        /// <param name="red">红</param>
        /// <param name="green">绿</param>
        /// <param name="blue">蓝</param>
        /// <returns>指定红绿蓝值的颜色(IRgbColor)</returns>
        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            //Create rgb color and grab hold of the IRGBColor interface
            IRgbColor rGB = new RgbColorClass();
            //Set rgb color properties
            rGB.Red = red;
            rGB.Green = green;
            rGB.Blue = blue;
            rGB.UseWindowsDithering = true;
            return rGB;
        }
        #endregion
        #region //根据Geometry类型生成一个默认符号
        /// <summary>
        /// 根据Geometry类型生成一个默认符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ISymbol GetDefaultSymbol(esriGeometryType type)
        {
            ISymbol sym = null;

            IRgbColor mCor = new RgbColorClass();
            mCor.Red = 0;
            mCor.Blue = 0;
            mCor.Green = 0;

            IRgbColor lCor = new RgbColorClass();
            mCor.Red = 0;
            lCor.Blue = 0;
            mCor.Green = 0;

            IRgbColor fCor = new RgbColorClass();
            mCor.Red = 0;
            fCor.Blue = 0;
            mCor.Green = 0;

            IMarkerSymbol mark = new SimpleMarkerSymbolClass();
            mark.Color = mCor;
            mark.Size = 8;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 1.5;
            line.Color = lCor;

            IFillSymbol fill = new SimpleFillSymbolClass();
            fill.Color = fCor;
            fill.Outline = line;

            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    sym = (ISymbol)mark;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    sym = (ISymbol)line;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    sym = (ISymbol)fill;
                    //sym = (ISymbol)line;
                    break;
            }

            return sym;
        }
        #endregion

        private double GetDistanceOfTwoPoints(IPoint P1, IPoint P2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

            return Result;
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
