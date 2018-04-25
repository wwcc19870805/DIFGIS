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

namespace DF2DCreate.Command
{
    class CmdCreateLineLabel : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        private IMap m_pMap;
        private IActiveView m_pActiveView;
        private bool m_bSelect = true;
        private IGeometry m_pGeoTrack = null;
        private ArrayList  m_pArrPipes = new ArrayList();
        private ArrayList m_pArrIntersectPnt= new ArrayList();
        private ArrayList m_pArrLineLayers = new ArrayList();

        private double dblStartX;
        private double dblStartY;
        private int nWidth = 8; //标注表格的单位宽高
        private int nHeight = 2;
        DFDataConfig.Class.FieldInfo fi, fi1, fi2, fi3, fi4, fi5, fi6;

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
            List<DF2DFeatureClass> st = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
            if (st == null) return;
            foreach (DF2DFeatureClass dfcc in st)
            {
                IFeatureClass fc = dfcc.GetFeatureClass();
                if (fc == null) continue;
                FacilityClass fac = dfcc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                fi = fac.GetFieldInfoBySystemName("Classify");
                if (fi == null) continue;
                fi1 = fac.GetFieldInfoBySystemName("StartNo");
                if (fi1 == null) continue;
                fi2 = fac.GetFieldInfoBySystemName("EndNo");
                if (fi2 == null) continue;
                fi3 = fac.GetFieldInfoBySystemName("Material");
                if (fi3 == null) continue;
                fi4 = fac.GetFieldInfoBySystemName("Coverstsle");
                if (fi4 == null) continue;
                fi5 = fac.GetFieldInfoBySystemName("Diameter");
                if (fi5 == null) continue;
                fi6 = fac.GetFieldInfoBySystemName("proad");
                if (fi6 == null) continue;
            }


        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            IScreenDisplay pScr = m_pActiveView.ScreenDisplay;
            if (m_pArrPipes.Count != 0 || m_pArrIntersectPnt.Count != 0)
            {
                m_pArrPipes.Clear();
                m_pArrIntersectPnt.Clear();
            }
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

            GetArrPipe();
            if (m_pArrPipes.Count <= 0) return;          
            DrawPipeLabels();
            DoAnalysis();
        }

      

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


        private void DoAnalysis()
        {
            IFeature pFeature;
            IGeometry pGeo;
            IGeometry pPipeLineGeo;
            IFeatureClass pFeatureClass;
            IFeatureCursor pFeaCur;
            ITopologicalOperator pTopo;            
            string scName = "";
            int i = 0;
         
            foreach (MajorClass mc in LogicDataStructureManage.Instance.GetAllMajorClass())
            {
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
                            
                            DrawPipeInfo(fl,pFeature,i);
                            ++i;
                        }
                    }
                }
            }

        }
        private void GetArrPipe()
        {
            //Load_LyrLstToArray();
            IFeature pFeature;
            IGeometry pGeo;
            IGeometry pPipeLineGeo;
            IFeatureClass pFeatureClass;
            IFeatureCursor pFeaCur;
            ITopologicalOperator pTopo;

            m_pArrLineLayers.Clear();
            m_pArrIntersectPnt.Clear();
            m_pArrPipes.Clear();
            string scName = "";
            foreach (MajorClass mc in LogicDataStructureManage.Instance.GetAllMajorClass())
            {
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
                        m_pArrLineLayers.Add(fl);

                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.Geometry = m_pGeoTrack;
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        pSpatialFilter.WhereClause = "SMSCODE =  '" + sc.Name + "'";
                        IFeatureClass fc = dffc.GetFeatureClass();
                        pFeaCur = fc.Search(pSpatialFilter, false);
                        while ((pFeature = pFeaCur.NextFeature()) != null)
                        {
                            m_pArrPipes.Add(pFeature);

                         
                        }
                    }
                }
            }
            //根据交点的X坐标，将管线排序
            double t;
            IFeature f;
            IPointCollection p;
            double[] sortX = new double[m_pArrIntersectPnt.Count];
            for (int k = 0; k < m_pArrIntersectPnt.Count; k++)
            {
                p = m_pArrIntersectPnt[k] as IPointCollection;
                sortX[k] = p.get_Point(0).X;
            }
            for (int j = sortX.Length - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    if (sortX[i] > sortX[i + 1])
                    {
                        t = sortX[i];
                        sortX[i] = sortX[i + 1];
                        sortX[i + 1] = t;

                        f = m_pArrPipes[i] as IFeature;
                        m_pArrPipes[i] = m_pArrPipes[i + 1];
                        m_pArrPipes[i + 1] = f;
                    }
                }
            }
        }

       /* #region 判断可进行操作的图层数据
        /// <summary>
        /// 判断可进行操作的图层数据
        /// </summary>
        private void Load_LyrLstToArray()
        {
            m_pArrLineLayers = new ArrayList();
            ILayer pLayer;

            if (m_pMap.LayerCount == 0)
            {
                return;
            }

            for (int i = 0; i < m_pMap.LayerCount; i++)
            {
                pLayer = m_pMap.get_Layer(i);
                if (pLayer.Visible)
                {
                    Load_LyrLstToArray(pLayer);
                }
            }
        }
        /// <summary>
        /// 递归调用获得所有可操作的数据图层－散点
        /// </summary>
        /// <param name="pLyr"></param> 
        private void Load_LyrLstToArray(ILayer pLyr)
        {
            ICompositeLayer pComp;
            IFeatureLayer pFeatureLayer;
            IFeatureClass pFeatureClass;

            if (pLyr is IGroupLayer)//如果是图层组
            {
                pComp = (ICompositeLayer)pLyr;
                for (int i = 0; i < pComp.Count; i++)
                {
                    pLyr = pComp.get_Layer(i);
                    if (pLyr.Visible)
                    {
                        Load_LyrLstToArray(pLyr);
                    }
                }
            }
            else
            {
                if (pLyr is IGeoFeatureLayer)//如果是地理要素图层
                {
                    pFeatureLayer = pLyr as IFeatureLayer;
                    pFeatureClass = pFeatureLayer.FeatureClass;
                    //  Console.WriteLine(pFeatureLayer.Name);
                    //修改-添加20060913
                    if (pFeatureClass.FindField(fi5.Name) == -1) return;

                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        m_pArrLineLayers.Add(pLyr);
                    }

                    //修改-添加20060913
                }
            }
        }
        #endregion*/

        private void DrawPipeLabels()
        {
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
            p2.Y = dblStartY + nHeight * m_pArrPipes.Count;

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

            //画管线信息
            //DrawPipiInfo();
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
            pAnnoPnt.PutCoords(dblStartX + nWidth, dblStartY + nHeight * m_pArrPipes.Count);

            ITextElement pTextElement = new TextElementClass();
            pTextElement.ScaleText = true;
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            IElement pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //起点点号
            sym.Text = "起点点号";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 2, dblStartY + nHeight * m_pArrPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //终点点号
            sym.Text = "终点点号";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 3, dblStartY + nHeight * m_pArrPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //材质
            sym.Text = "材质";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 4, dblStartY + nHeight * m_pArrPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //埋设方式
            sym.Text = "埋设方式";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 5, dblStartY + nHeight * m_pArrPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //管径
            sym.Text = "管径";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 6, dblStartY + nHeight * m_pArrPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

            pTextElement = new TextElementClass();
            pTextElement.Text = sym.Text;
            pTextElement.Symbol = sym;
            pElement = (IElement)pTextElement;
            pElement.Geometry = pAnnoPnt;

            pGraContainer.AddElement(pElement, 0);

            //所在道路
            sym.Text = "所在道路";
            pAnnoPnt.PutCoords(dblStartX + nWidth * 7, dblStartY + nHeight * m_pArrPipes.Count);//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

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

        private void DrawPipeInfo(IFeatureLayer fealyr,IFeature feature,int i)
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
                IGeoFeatureLayer geolyr = fealyr as IGeoFeatureLayer;
                if (geolyr == null) return;
                FeatureRenderer = geolyr.Renderer;
                //获取此图层的Symbol
                pSimpleLineSymbol = (ISimpleLineSymbol)FeatureRenderer.get_SymbolByFeature(feature);
                sym.Color = pSimpleLineSymbol.Color;

                //类别
                sym.Text = feature.get_Value(feature.Fields.FindField(fi.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                ITextElement pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                IElement pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
                //起点点号
                sym.Text = feature.get_Value(feature.Fields.FindField(fi1.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 2, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
                //终点点号
                sym.Text = feature.get_Value(feature.Fields.FindField(fi2.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 3, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //材质
                sym.Text = feature.get_Value(feature.Fields.FindField(fi3.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 4, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);
                //埋设方式
                sym.Text = feature.get_Value(feature.Fields.FindField(fi4.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 5, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //管径
                sym.Text = feature.get_Value(feature.Fields.FindField(fi5.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 6, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                pTextElement = new TextElementClass();
                pTextElement.ScaleText = true;
                pTextElement.Text = sym.Text;
                pTextElement.Symbol = sym;
                pElement = (IElement)pTextElement;
                pElement.Geometry = pAnnoPnt;
                pGraContainer.AddElement(pElement, 0);

                //所在道路
                sym.Text = feature.get_Value(feature.Fields.FindField(fi6.Name)).ToString();
                if (sym.Text == null) return;
                pAnnoPnt.PutCoords(dblStartX + nWidth * 7, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
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

        private void DrawPipiInfo()
        {
            int i = 0;
            //IFeature pFea;
            //IFeature pFeature;
            //IGraphicsContainer pGraContainer = m_pMapControl.Map as IGraphicsContainer;
            //IFeatureClass pFeatureClass;
            //IFeatureCursor pFeaCur;
            //ISpatialFilter pSpatialFilter = new SpatialFilterClass();

            //pSpatialFilter.Geometry = m_pGeoTrack;
            //pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IPoint pAnnoPnt = new PointClass();
            ITextSymbol sym = new TextSymbolClass();
            stdole.IFontDisp myfont = (stdole.IFontDisp)new stdole.StdFontClass();
            myfont.Name = "华文细黑";
            sym.Font = myfont;
            myfont.Bold = true;
            sym.Size = 8/*SystemInfo.Instance.TextSize*/;

            
            /*for (int n = 0; n < m_pArrLineLayers.Count; n++)
            {

                pFeatureClass = (m_pArrLineLayers[n] as IFeatureLayer).FeatureClass;
                pFeaCur = pFeatureClass.Search(pSpatialFilter, false);//获得与断面相加的所有管线
                int m = pFeatureClass.FeatureCount(pSpatialFilter);
                pFeature = pFeaCur.NextFeature();
                while (pFeature != null)
                {

                    ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                    IFeatureRenderer FeatureRenderer = new SimpleRendererClass();
                    ILayer FLayer;
                    IGeoFeatureLayer Layer;
                    FLayer = m_pArrLineLayers[n] as ILayer;
                    Layer = FLayer as IGeoFeatureLayer;
                    if (Layer == null) return;
                    FeatureRenderer = Layer.Renderer;

                    //获取此图层的Symbol
                    pSimpleLineSymbol = (ISimpleLineSymbol)FeatureRenderer.get_SymbolByFeature(pFeature);
                    sym.Color = pSimpleLineSymbol.Color;

                    //类别
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                    ITextElement pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    IElement pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;
                    pGraContainer.AddElement(pElement, 0);
                    //起点点号
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi1.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth * 2, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3
                    pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;

                    pGraContainer.AddElement(pElement, 0);

                    //终点点号
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi2.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth * 3, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                    pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;
                    pGraContainer.AddElement(pElement, 0);

                    //材质
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi3.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth * 4, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                    pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;
                    pGraContainer.AddElement(pElement, 0);

                    //埋设方式
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi4.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth * 5, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                    pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;
                    pGraContainer.AddElement(pElement, 0);

                    //管径
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi5.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth * 6, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                    pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;
                    pGraContainer.AddElement(pElement, 0);

                    //所在道路
                    sym.Text = pFeature.get_Value(pFeature.Fields.FindField(fi6.Name)).ToString();
                    if (sym.Text == null) return;
                    pAnnoPnt.PutCoords(dblStartX + nWidth * 7, dblStartY + nHeight * (m_pArrPipes.Count - (i + 1)));//居中显示,并位于表格边线以上m_dblTableHeadWidth/5*3

                    pTextElement = new TextElementClass();
                    pTextElement.ScaleText = true;
                    pTextElement.Text = sym.Text;
                    pTextElement.Symbol = sym;
                    pElement = (IElement)pTextElement;
                    pElement.Geometry = pAnnoPnt;

                    pGraContainer.AddElement(pElement, 0);

                    pFeature = pFeaCur.NextFeature();
                    ++i;
                }*/
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

