using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;

using stdole;

namespace DF2DTool.Class
{
    public class ClassOutline
    {
        #region 内部成员
        private double m_xMin;
        private double m_yMin;

        private double m_xMax;
        private double m_yMax;

        // 应该做成属性的方式读取
        double FRAMEWIDTH = 500.0;//单位毫米
        double FRAMEHEIGHT = 400.0;//
        string m_sMapName;	//地图名称
        string m_sMapNumber;// 图幅编号
        string m_sProjectName = "当阳市中心城区综合地下管线图";	//工程名称

        string m_sCord = "";	    //  坐标系
        string m_sHeightBase = "1";          //高程基准
        string m_sIsoHeightDistance = "0.5";// 等高距
        string m_sMakeMapMethod = "";   //成图方式
        string m_sProductUnit = "";//生产 单位
        string m_sMapDrawer; //绘图员
        string m_sMapSurveyor; // 测量员
        string m_sMapChecker; // 检查员
        string m_sSecrecyType = "秘密 ★ 长期";

        //ESRI.ArcGIS.Controls.IPageLayoutControl2 pPageLayout;
        AxPageLayoutControl pPageLayout;

        string sDir = Application.StartupPath;
        private string sXmlNme = @"\..\Template\ProjectInformation.xml";

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClassOutline()
        {
            m_dScale = 500;
            Load_XmlInfoToTextBox();
        }

        #region 静态成员及属性

        static double m_dScale;
        static double origXMapFrame, origYMapFrame, innerBorderOffset, outBorderOffset;
        static double marginLeft, marginRight, marginTop, marginBottom;
        static double joinTableHeight, joinTableWidth;

        /// <summary>
        /// 设置默认值
        /// </summary>
        static public void SetDefault()
        {
            m_dScale = 500;
            OrigXMapFrame = 2.4;
            OrigYMapFrame = 3;
            innerBorderOffset = 1.2;
            outBorderOffset = 0.8;
            joinTableHeight = 2.4;
            joinTableWidth = 4.5;
        }
        /// <summary>
        /// 清空默认值
        /// </summary>
        static public void SetEmpty()
        {
            OrigXMapFrame = 0;
            OrigYMapFrame = 0;
            innerBorderOffset = 0;
            outBorderOffset = 0;
        }
        /// <summary>
        /// 地图西南角X坐标
        /// </summary>
        static public double OrigXMapFrame
        {
            get
            {
                return origXMapFrame;
            }
            set
            {
                origXMapFrame = value;
            }
        }
        /// <summary>
        /// 地图西南角Y坐标
        /// </summary>
        static public double OrigYMapFrame
        {
            get
            {
                return origYMapFrame;
            }
            set
            {
                origYMapFrame = value;
            }
        }
        /// <summary>
        /// 内图廓偏移
        /// </summary>
        static public double InnerBorderOffset
        {
            get
            {
                return innerBorderOffset;
            }
            set
            {
                innerBorderOffset = value;
            }
        }
        /// <summary>
        /// 外图廓偏移
        /// </summary>
        static public double OutBorderOffset//top & right
        {
            get
            {
                return outBorderOffset;
            }
            set
            {
                outBorderOffset = value;
            }
        }
        /// <summary>
        /// 页边距——左
        /// </summary>
        static public double MarginLeft
        {
            get
            {
                return marginLeft;
            }
            set
            {
                marginLeft = value;
            }
        }
        /// <summary>
        /// 页边距——右
        /// </summary>
        static public double MarginRight
        {
            get
            {
                return marginRight;
            }
            set
            {
                marginRight = value;
            }
        }
        /// <summary>
        /// 页边距——上
        /// </summary>
        static public double MarginTop
        {
            get
            {
                return marginTop;
            }
            set
            {
                marginTop = value;
            }
        }
        /// <summary>
        /// 页边距——下
        /// </summary>
        static public double MarginBottom
        {
            get
            {
                return marginBottom;
            }
            set
            {
                marginBottom = value;
            }
        }
        /// <summary>
        /// 接图表高
        /// </summary>
        static public double JoinTableHeight
        {
            get
            {
                return joinTableHeight;
            }
            set
            {
                joinTableHeight = value;
            }
        }
        /// <summary>
        /// 接图表宽
        /// </summary>
        static public double JoinTableWidth
        {
            get
            {
                return joinTableWidth;
            }
            set
            {
                joinTableHeight = value;
            }
        }

        private MapCode curMapCode = new MapCode();
        /// <summary>
        /// 图幅编号
        /// </summary>
        public MapCode CurMapCode
        {
            get
            {
                return curMapCode;
            }
            set
            {
                curMapCode = value;
            }
        }

        private string xPrefix;
        /// <summary>
        /// X坐标前缀字符串
        /// </summary>
        public string XPrefix
        {
            get
            {
                return xPrefix;
            }
            set
            {
                xPrefix = value;
            }
        }

        private string yPrefix;
        /// <summary>
        /// Y坐标前缀字符串
        /// </summary>
        public string YPrefix
        {
            get
            {
                return yPrefix;
            }
            set
            {
                yPrefix = value;
            }
        }

        #endregion

        /// <summary>
        /// 高程基准
        /// </summary>
        /// <param name="sBase"></param>
        public void SetHeightBase(string sBase)
        {
            m_sHeightBase = sBase;
        }

        /// <summary>
        /// 生产方式
        /// </summary>
        /// <param name="sMethod">生产方式</param>
        public void SetProduceMethod(string sMethod)
        {
            m_sMakeMapMethod = sMethod;
        }

        /// <summary>
        /// 单位
        /// </summary>
        /// <param name="sUnit">单位</param>
        public void SetProductUnit(string sUnit)
        {
            m_sProductUnit = sUnit;
        }

        /// <summary>
        /// 等高距
        /// </summary>
        /// <param name="sDist">等高距</param>
        public void SetIsoDistance(string sDist)
        {
            m_sIsoHeightDistance = sDist;
        }
        /// <summary>
        /// 坐标系统
        /// </summary>
        /// <param name="sCoord">坐标系统</param>
        public void SetCoord(string sCoord)
        {
            m_sCord = sCoord;
        }
        /// <summary>
        /// 图幅名
        /// </summary>
        /// <param name="strName">图幅名</param>
        public void SetMapName(string strName)
        {
            m_sMapName = strName;
        }

        /// <summary>
        /// 图幅编号
        /// </summary>
        /// <param name="strNo">图幅编号</param>
        public void SetMapNumber(string strNo)
        {
            m_sMapNumber = strNo;
        }

        /// <summary>
        /// 测图员
        /// </summary>
        /// <param name="strName">测图员</param>
        public void SetMapSurveyor(string strName)
        {
            m_sMapSurveyor = strName;
        }

        /// <summary>
        /// 制图员
        /// </summary>
        /// <param name="strName">制图员</param>
        public void SetMapMaker(string strName)
        {
            m_sMapDrawer = strName;
        }

        /// <summary>
        /// 检图员
        /// </summary>
        /// <param name="strName">检图员</param>
        public void SetMapChecker(string strName)
        {
            m_sMapChecker = strName;
        }

        /// <summary>
        /// 设置图幅宽高，单位：毫米
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public void SetMapSize(double width, double height)
        {
            FRAMEWIDTH = width;
            FRAMEHEIGHT = height;

            caculateType = CaculateType.MetaData;
        }

        /// <summary>
        /// 设置比例尺
        /// </summary>
        /// <param name="scale">比例尺</param>
        public void SetMapScale(double scale)
        {
            m_dScale = scale;
        }

        /// <summary>
        /// 设置西南角坐标
        /// </summary>
        /// <param name="xmin">X最小值</param>
        /// <param name="ymin">Y最小值</param>
        public void SetOrig(double xmin, double ymin)
        {
            m_xMin = xmin;
            m_yMin = ymin;
        }

        /// <summary>
        /// 设置图幅范围
        /// </summary>
        /// <param name="xmin">X最小值</param>
        /// <param name="ymin">Y最小值</param>
        /// <param name="xmax">X最大值</param>
        /// <param name="ymax">Y最大值</param>
        public void SetEnvelope(double xmin, double ymin, double xmax, double ymax)
        {
            m_xMin = xmin;
            m_yMin = ymin;

            m_xMax = xmax;
            m_yMax = ymax;

            caculateType = CaculateType.Envelope;
        }

        /// <summary>
        /// 绘制图廓方式：标准或任意
        /// </summary>
        private enum CaculateType
        {
            None, Envelope, MetaData
        }
        /// <summary>
        /// 绘制图廓方式
        /// </summary>
        private static CaculateType caculateType = CaculateType.None;

        /// <summary>
        /// 计算图幅信息
        /// </summary>
        /// <returns>参数设置是否正确</returns>
        private bool CaculateMapInfo()
        {
            switch (caculateType)
            {
                case CaculateType.Envelope:
                    FRAMEWIDTH = (m_xMax - m_xMin) / m_dScale * 1000;
                    FRAMEHEIGHT = (m_yMax - m_yMin) / m_dScale * 1000;
                    return true;
                case CaculateType.MetaData:
                    m_xMax = m_xMin + FRAMEWIDTH * m_dScale / 1000;
                    m_yMax = m_yMin + FRAMEHEIGHT * m_dScale / 1000;
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 创建线元素
        /// </summary>
        /// <param name="ptStart">起点</param>
        /// <param name="ptEnd">终点</param>
        /// <returns>线元素</returns>
        private IElement CreateLineElement(IPoint ptStart, IPoint ptEnd)
        {
            INewLineFeedback line = new NewLineFeedbackClass();
            line.Start(ptStart);
            line.AddPoint(ptEnd);

            IPolyline pl = line.Stop();
            IPointCollection pc = (IPointCollection)pl;

            ILineElement lElm = new LineElementClass();
            IElement element = (IElement)lElm;
            element.Geometry = (IGeometry)pc;

            return element;
        }

        /// <summary>
        /// 创建线元素
        /// </summary>
        /// <param name="pFromPoint">起点</param>
        /// <param name="pToPoint">终点</param>
        /// <param name="sElementName">元素名</param>
        /// <param name="dRed">红色</param>
        /// <param name="dGreen">绿色</param>
        /// <param name="dBlue">蓝色</param>
        /// <param name="dSize">大小</param>
        /// <returns>线元素</returns>
        public IElement CreateLineElement(IPoint pFromPoint, IPoint pToPoint, string sElementName, int dRed, int dGreen, int dBlue, double dSize)
        {
            ISimpleLineSymbol pLnSym;
            ILine pLine1;
            ISegment pSeg1;
            ISegmentCollection pPolyline;
            IRgbColor myColor;

            IElement pElement;
            ILineElement pLineElement;
            IElementProperties pElementProp;
            object missing = Type.Missing;

            pElement = new LineElement();

            // Set the line symbol
            pLnSym = new SimpleLineSymbol();
            myColor = new RgbColor();
            myColor.Red = dRed;
            myColor.Green = dGreen;
            myColor.Blue = dBlue;
            pLnSym.Color = myColor;
            pLnSym.Width = dSize;

            // Create a standard polyline (via 2 points)
            pLine1 = new Line();
            pLine1.PutCoords(pFromPoint, pToPoint);
            pSeg1 = pLine1 as ISegment;
            pPolyline = new Polyline() as ISegmentCollection;
            pPolyline.AddSegment(pSeg1, ref missing, ref missing);
            pElement.Geometry = pPolyline as IGeometry;
            pLineElement = pElement as ILineElement;
            pLineElement.Symbol = pLnSym;

            pElementProp = pElement as IElementProperties;
            pElementProp.Name = sElementName;
            return pElement;
        }

        /// <summary>
        /// 创建字体元素
        /// </summary>
        /// <param name="pPoint">坐标</param>
        /// <param name="sText">内容</param>
        /// <param name="sElementName">元素名</param>
        /// <param name="dRed">红色</param>
        /// <param name="dGreen">绿色</param>
        /// <param name="dBlue">蓝色</param>
        /// <param name="dSize">大小</param>
        /// <param name="sFontName">字体</param>
        /// <param name="dAngle">角度</param>
        /// <param name="textHorizontalAlignment">水平对齐方式</param>
        /// <returns>字体元素</returns>
        public IElement CreateTextElement(IPoint pPoint, string sText, string sElementName,
            double dRed, double dGreen, double dBlue,
            double dSize, string sFontName, double dAngle,
            esriTextHorizontalAlignment textHorizontalAlignment)
        {
            ITextSymbol myTxtSym;
            IRgbColor myColor;
            IElement pElement;
            ITextElement pTextElement;
            IElementProperties pElementProp;
            IFontDisp pFont;

            pElement = new TextElement();
            pElement.Geometry = pPoint;
            pTextElement = (ITextElement)pElement;
            if (sFontName.Length > 0)
            {
                pFont = (stdole.IFontDisp)new stdole.StdFontClass();
                pFont.Name = sFontName;
                pFont.Size = (decimal)dSize;
            }
            else
            {
                pFont = (stdole.IFontDisp)new stdole.StdFontClass();
                pFont.Name = "Arial";
                pFont.Size = (decimal)dSize;
            }

            // Create the text symbol
            myTxtSym = new TextSymbol();
            myColor = new RgbColor();
            myColor.Red = (int)dRed;
            myColor.Green = (int)dGreen;
            myColor.Blue = (int)dBlue;
            myTxtSym.Color = myColor;
            myTxtSym.Size = dSize;
            myTxtSym.Font = pFont;
            //myTxtSym.Angle = dAngle;

            myTxtSym.HorizontalAlignment = textHorizontalAlignment;
            pTextElement.Symbol = myTxtSym;
            pTextElement.ScaleText = true;
            pTextElement.Text = sText;

            if (dAngle != 0)
            {
                const double PI = 3.14159265;
                ITransform2D pTransform2D;

                pTransform2D = (ITransform2D)pTextElement;
                pTransform2D.Rotate(pPoint, dAngle * (PI / 180));
            }

            pElementProp = pElement as IElementProperties;

            pElementProp.Name = sElementName;
            return pElement;
        }

        /// <summary>
        /// 初始化PageLayout
        /// </summary>
        /// <param name="pPage">布局控件</param>
        //public void SetPageLayout(ESRI.ArcGIS.Controls.IPageLayoutControl2 pPage)
        //{
        //    pPageLayout = pPage;
        //}
        public void SetPageLayout(AxPageLayoutControl pPage)
        {
            pPageLayout = pPage;
        }

        /// <summary>
        /// 添加到绘制层
        /// </summary>
        /// <param name="element">要添加的元素</param>
        /// <param name="pGraphicsContainer">绘制层</param>
        private void AddElement(IElement element, IGraphicsContainer pGraphicsContainer)
        {
            pGraphicsContainer.AddElement(element, 0);
        }

        /// <summary>
        /// 添加到组合元素
        /// </summary>
        /// <param name="element">要添加的元素</param>
        /// <param name="pGroutElement">组合元素</param>
        private void AddElement(IElement element, IGroupElement pGroutElement)
        {
            pGroutElement.AddElement(element);
        }

        /// <summary>
        /// 绘制图廓
        /// </summary>
        /// <returns>绘制结果</returns>
        public IMapFrame DrawFrame()
        {
            if (!this.CaculateMapInfo())
            {
                return null;
            }

            ClassOutline.origXMapFrame += ClassOutline.marginLeft;
            ClassOutline.origYMapFrame += ClassOutline.marginBottom;

            ESRI.ArcGIS.Carto.IGroupElement groupElement = new ESRI.ArcGIS.Carto.GroupElementClass();

            IPoint ptStart = new PointClass();
            IPoint ptEnd = new PointClass();

            // 内图廓，用MapFrame
            IEnvelope iEnv;
            IElement iEle;
            IMapFrame pFrame;
            IGraphicsContainer graphicsContainer;

            graphicsContainer = pPageLayout.PageLayout as IGraphicsContainer;
            pFrame = graphicsContainer.FindFrame(pPageLayout.ActiveView.FocusMap) as IMapFrame;

            pFrame.MapBounds.PutCoords(m_xMin, m_yMin, m_xMax, m_yMax);
            pFrame.Map.Name = "二维地图";
            pFrame.ExtentType = esriExtentTypeEnum.esriExtentBounds;
            iEnv = new EnvelopeClass();
            iEnv.XMin = OrigXMapFrame;
            iEnv.XMax = OrigXMapFrame + FRAMEWIDTH / 10.0;
            iEnv.YMin = OrigYMapFrame;
            iEnv.YMax = OrigYMapFrame + FRAMEHEIGHT / 10.0;
            iEle = pFrame as IElement;
            iEle.Geometry = iEnv;
            pFrame.ExtentType = ESRI.ArcGIS.Carto.esriExtentTypeEnum.esriExtentScale;
            pFrame.MapScale = m_dScale;


            ESRI.ArcGIS.Geometry.IEnvelope eee = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            //			eee.PutCoords( m_yMin,m_xMin,m_yMax,m_xMax);
            eee.PutCoords(m_xMin, m_yMin, m_xMax, m_yMax);
            ESRI.ArcGIS.Carto.IActiveView mapView = pFrame.Map as ESRI.ArcGIS.Carto.IActiveView;
            mapView.Extent = eee;


            /*
             * 内图廓线
             */
            //下横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame);
            //AddElement(CreateLineElement(ptStart, ptEnd), groupElement);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);


            //上横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);

            //左竖线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);

            //右竖线
            ptStart.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);

            /*
             * 外图廓线
             */
            const double dSize = 1;
            //下横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //上横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //左竖线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //右竖线
            ptStart.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //格网
            this.DrawGrid(groupElement);

            // 注记
            IPoint ptOrigin = new PointClass();
            string strText = "";
            double xOffset;//, yOffset;

            //考虑字体和定位点
            xOffset = 0.8;
            //yOffset = 0.05 ;

            //西南角坐标
            ptOrigin.PutCoords(OrigXMapFrame - xOffset + 0.2, OrigYMapFrame + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter),
                graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame - 0.2, OrigYMapFrame - (12.0 - 0.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //西北角坐标
            ptOrigin.PutCoords(OrigXMapFrame - xOffset + 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame - 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12.0 - 3.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //东北角坐标
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + xOffset - 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 - 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12.0 - 3.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);
            //东南角坐标
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + xOffset - 0.2, OrigYMapFrame + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 - 0.2, OrigYMapFrame - (12.0 - 0.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //图名 中间，外图廓上面
            //ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 0.5) / 10.0);
            //AddElement(CreateTextElement(ptOrigin, m_sMapName, "MapName", 0, 0, 0, 28, "等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //图号
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 5) / 10.0);
            AddElement(CreateTextElement(ptOrigin, m_sMapNumber, "MapName", 0, 0, 0, 28, "长等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //项目名
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 2) / 10.0);
            AddElement(CreateTextElement(ptOrigin, m_sProjectName, "MapName", 0, 0, 0, 28, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //秘密等级
            //ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0 + 23, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 0.5) / 10.0+0.5);
            //AddElement(CreateTextElement(ptOrigin, m_sSecrecyType, "Secret", 0, 0, 0, 24, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);
            // 位置需要计算字高
            // 成图方式,左与内图廓齐，下外图廓3.0毫米
            ptOrigin.PutCoords(OrigXMapFrame, OrigYMapFrame - (12 + 3.0 * 2) / 10.0 + 0.1);//按字高3.0毫米算，本来应该是3.0，现在*2
            AddElement(CreateTextElement(ptOrigin, m_sMakeMapMethod, "MapMethod", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);
            //			AddElement(CreateTextElement(ptOrigin, "成图方式:" + m_sMakeMapMethod, "MapMethod", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            // 坐标系,下移1.0mm
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, this.m_sCord, "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            // 高程基准、等高距
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, this.m_sHeightBase + ",等高距为" + this.m_sIsoHeightDistance, "MapHeight", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            //探测时间
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, "2016年04月09日管线探测", "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            //探测时间
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, "2007年版国家图式", "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            // 比例尺
            ptOrigin.X = OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0;
            ptOrigin.Y = OrigYMapFrame - (12 + 13.0) / 10.0;
            AddElement(CreateTextElement(ptOrigin, "1:" + m_dScale.ToString("F0"), "MapScale", 0, 0, 0, 20, "宋体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //测量员
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 - 4, OrigYMapFrame - (12 + 8.0) / 10.0 + 0.1);
            AddElement(CreateTextElement(ptOrigin, "测量员：" + this.m_sMapSurveyor, "MapSur", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            //绘图员
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, "绘图员：" + this.m_sMapDrawer, "MapDrawer", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);
            //检查员
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, "检查员：" + this.m_sMapChecker, "MapDrawer", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);


            /*
             * 接合图
             */
            //上横线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "top", 0, 0, 0, dSize), groupElement);

            //下横线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "bottom", 0, 0, 0, dSize), groupElement);

            // 左竖线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "left", 0, 0, 0, dSize), groupElement);

            // 右竖线
            ptStart.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "right", 0, 0, 0, dSize), groupElement);

            // 第二横线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (19 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (19 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "bottom", 0, 0, 0, dSize), groupElement);

            // 第三横线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (11 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (11 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "bottom", 0, 0, 0, dSize), groupElement);

            // 第二竖线
            ptStart.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "left", 0, 0, 0, dSize), groupElement);

            // 第三竖线
            ptStart.PutCoords(OrigXMapFrame + 3, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + 3, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd, "left", 0, 0, 0, dSize), groupElement);

            // 填充中间
            IPointCollection pPntColl;
            IPoint pPoint;
            IFillShapeElement pFillShape;
            ISimpleFillSymbol pFillSym;

            object missing = Type.Missing;

            pPntColl = new Polygon();
            pPoint = new Point();
            pPoint.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 3.1);
            pPntColl.AddPoint(pPoint, ref missing, ref missing);
            pPoint = new Point();
            pPoint.PutCoords(OrigXMapFrame + 3.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 3.1);
            pPntColl.AddPoint(pPoint, ref missing, ref missing);
            pPoint = new Point();
            pPoint.PutCoords(OrigXMapFrame + 3.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 2.3);
            pPntColl.AddPoint(pPoint, ref missing, ref missing);
            pPoint = new Point();
            pPoint.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 2.3);
            pPntColl.AddPoint(pPoint, ref missing, ref missing);
            pPoint = new Point();
            pPoint.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 3.1);
            pPntColl.AddPoint(pPoint, ref missing, ref missing);

            IElement pTemp = new PolygonElementClass();
            pTemp.Geometry = pPntColl as IPolygon;

            pFillSym = new SimpleFillSymbolClass();
            pFillSym.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;

            pFillShape = pTemp as IFillShapeElement;
            pFillShape.Symbol = pFillSym;

            AddElement((IElement)pFillShape, groupElement);

            // 输出接图表内容
            double textSize = 5;//11
            ptOrigin.PutCoords(OrigXMapFrame + 15.0 / 2 / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 20 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[1].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + 15.0 / 2 / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 12 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[4].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + 15.0 / 2 / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 4 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[6].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + (15 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 20 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[2].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + (15 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 4 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[7].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + (30 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 20 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[3].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + (30 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 12 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[5].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + (30 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 4 - 1.25) / 10.0);
            AddElement(CreateTextElement(ptOrigin, this.CurMapCode[8].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //生产单位
            ptOrigin.PutCoords(OrigXMapFrame - (12 + 3) / 10.0, OrigYMapFrame + 10.2);//5);
            this.AddDepartment(ptOrigin, m_sProductUnit, graphicsContainer);

            //委托单位
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset + 1, OrigYMapFrame + 7.3);//5);
            this.AddDepartment(ptOrigin, "当阳市住房和城乡建设局", graphicsContainer);

            AddElement(groupElement as ESRI.ArcGIS.Carto.IElement, graphicsContainer);
            //			ESRI.ArcGIS.Carto.IGraphicsContainerSelect gcs = graphicsContainer as ESRI.ArcGIS.Carto.IGraphicsContainerSelect;
            //			gcs.UnselectAllElements();
            //			gcs.SelectElement(groupElement as ESRI.ArcGIS.Carto.IElement);
            //			ESRI.ArcGIS.Carto.IEnumElement ee = gcs.SelectedElements;
            //			ee.Reset();
            //			graphicsContainer.SendBackward(ee);


            pPageLayout.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGraphics, null, null);

            return pFrame;
        }

        //计算矩形范围所占的图幅
        public string[][] CalMapByRegion(IEnvelope pEnvRegion, int iMapScale)
        {
            int i, j, iMapDis;
            int iMapXCount, iMapYCount;
            int iMinMapX, iMinMapY, iMaxMapX, iMaxMapY;
            string strMapNO;
            ArrayList arrMap = new ArrayList();
            string[][] strMaps;

            if (iMapScale == 1000)
            {
                iMapDis = 500;
            }
            else
            {
                iMapDis = 250;
            }
            iMinMapX = Convert.ToInt32(Math.Floor(pEnvRegion.XMin / iMapDis)) * iMapDis;
            iMinMapY = Convert.ToInt32(Math.Floor(pEnvRegion.YMin / iMapDis)) * iMapDis;

            iMaxMapX = Convert.ToInt32(Math.Floor(pEnvRegion.XMax / iMapDis)) * iMapDis;
            iMaxMapY = Convert.ToInt32(Math.Floor(pEnvRegion.YMax / iMapDis)) * iMapDis;

            iMapXCount = (iMaxMapX - iMinMapX) / iMapDis + 1;
            iMapYCount = (iMaxMapY - iMinMapY) / iMapDis + 1;

            strMaps = new string[iMapYCount][];

            for (i = 0; i < iMapYCount; i++)
            {
                for (j = 0; j < iMapXCount; j++)
                {
                    strMapNO = ((iMinMapX + j * iMapDis) / 1000).ToString("F2") + "-" + ((iMinMapY + i * iMapDis) / 1000).ToString("F2");
                    strMaps[i][j] = strMapNO;
                }
            }
            return strMaps;
        }

        //矩形范围，标准图幅图廓
        public IMapFrame DrawRegionFrameUseStandardOutline(string[][] strMaps)
        {
            if (!this.CaculateMapInfo())
            {
                return null;
            }

            const double dSize = 1;
            ClassOutline.origXMapFrame += ClassOutline.marginLeft;
            ClassOutline.origYMapFrame += ClassOutline.marginBottom;

            ESRI.ArcGIS.Carto.IGroupElement groupElement = new ESRI.ArcGIS.Carto.GroupElementClass();

            IPoint ptStart = new PointClass();
            IPoint ptEnd = new PointClass();

            // 内图廓，用MapFrame
            IEnvelope iEnv;
            IElement iEle;
            IMapFrame pFrame;
            IGraphicsContainer graphicsContainer;

            graphicsContainer = pPageLayout.PageLayout as IGraphicsContainer;
            pFrame = graphicsContainer.FindFrame(pPageLayout.ActiveView.FocusMap) as IMapFrame;

            pFrame.MapBounds.PutCoords(m_xMin, m_yMin, m_xMax, m_yMax);
            pFrame.Map.Name = "二维地图";
            pFrame.ExtentType = esriExtentTypeEnum.esriExtentBounds;
            iEnv = new EnvelopeClass();
            iEnv.XMin = OrigXMapFrame;
            iEnv.XMax = OrigXMapFrame + FRAMEWIDTH / 10.0;
            iEnv.YMin = OrigYMapFrame;
            iEnv.YMax = OrigYMapFrame + FRAMEHEIGHT / 10.0;
            iEle = pFrame as IElement;
            iEle.Geometry = iEnv;
            pFrame.ExtentType = ESRI.ArcGIS.Carto.esriExtentTypeEnum.esriExtentScale;
            pFrame.MapScale = m_dScale;


            ESRI.ArcGIS.Geometry.IEnvelope eee = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            //			eee.PutCoords( m_yMin,m_xMin,m_yMax,m_xMax);
            eee.PutCoords(m_xMin, m_yMin, m_xMax, m_yMax);
            ESRI.ArcGIS.Carto.IActiveView mapView = pFrame.Map as ESRI.ArcGIS.Carto.IActiveView;
            mapView.Extent = eee;

            for (int i = 0; i < strMaps.GetLength(1); i++)
            {
                for (int j = 0; j < strMaps.GetLength(2); j++)
                {
                    ClassOutline.origXMapFrame = 500 * j;
                    ClassOutline.origYMapFrame = 500 * i;
                    /*
                     * 内图廓线
                     */
                    //下横线
                    ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame);
                    ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame);
                    //AddElement(CreateLineElement(ptStart, ptEnd), groupElement);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);


                    //上横线
                    ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);

                    //左竖线
                    ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame - InnerBorderOffset);
                    ptEnd.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);

                    //右竖线
                    ptStart.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame - InnerBorderOffset);
                    ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), groupElement);

                    /*
                     * 外图廓线
                     */

                    //下横线
                    ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
                    ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

                    //上横线
                    ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
                    ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

                    //左竖线
                    ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
                    ptEnd.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

                    //右竖线
                    ptStart.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
                    ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
                    AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

                    //格网
                    this.DrawGrid(groupElement);

                    // 注记
                    IPoint ptOrigin = new PointClass();
                    string strText = "";
                    double xOffset;//, yOffset;

                    //考虑字体和定位点
                    xOffset = 0.8;
                    //yOffset = 0.05 ;

                    //西南角坐标
                    ptOrigin.PutCoords(OrigXMapFrame - xOffset + 0.2, OrigYMapFrame + 0.05);
                    strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMin);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter),
                        graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame - 0.2, OrigYMapFrame - (12.0 - 0.5) / 10.0);
                    strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMin);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //西北角坐标
                    ptOrigin.PutCoords(OrigXMapFrame - xOffset + 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 0.05);
                    strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMax);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame - 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12.0 - 3.5) / 10.0);
                    strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMin);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //东北角坐标
                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + xOffset - 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 0.05);
                    strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMax);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 - 0.2, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12.0 - 3.5) / 10.0);
                    strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMax);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);
                    //东南角坐标
                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + xOffset - 0.2, OrigYMapFrame + 0.05);
                    strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMin);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 - 0.2, OrigYMapFrame - (12.0 - 0.5) / 10.0);
                    strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMax);
                    AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //图名 中间，外图廓上面
                    //ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 0.5) / 10.0);
                    //AddElement(CreateTextElement(ptOrigin, m_sMapName, "MapName", 0, 0, 0, 28, "等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //图号
                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 5) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, m_sMapNumber, "MapName", 0, 0, 0, 28, "长等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //项目名
                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 2) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, m_sProjectName, "MapName", 0, 0, 0, 28, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //秘密等级
                    //ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0 + 23, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 0.5) / 10.0+0.5);
                    //AddElement(CreateTextElement(ptOrigin, m_sSecrecyType, "Secret", 0, 0, 0, 24, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);
                    // 位置需要计算字高
                    // 成图方式,左与内图廓齐，下外图廓3.0毫米
                    ptOrigin.PutCoords(OrigXMapFrame, OrigYMapFrame - (12 + 3.0 * 2) / 10.0 + 0.1);//按字高3.0毫米算，本来应该是3.0，现在*2
                    AddElement(CreateTextElement(ptOrigin, m_sMakeMapMethod, "MapMethod", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);
                    //			AddElement(CreateTextElement(ptOrigin, "成图方式:" + m_sMakeMapMethod, "MapMethod", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

                    // 坐标系,下移1.0mm
                    ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
                    AddElement(CreateTextElement(ptOrigin, this.m_sCord, "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

                    // 高程基准、等高距
                    ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
                    AddElement(CreateTextElement(ptOrigin, this.m_sHeightBase + ",等高距为" + this.m_sIsoHeightDistance, "MapHeight", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

                    //探测时间
                    ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
                    AddElement(CreateTextElement(ptOrigin, "2016年04月09日管线探测", "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

                    //探测时间
                    ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
                    AddElement(CreateTextElement(ptOrigin, "2007年版国家图式", "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

                    // 比例尺
                    ptOrigin.X = OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0;
                    ptOrigin.Y = OrigYMapFrame - (12 + 13.0) / 10.0;
                    AddElement(CreateTextElement(ptOrigin, "1:" + m_dScale.ToString("F0"), "MapScale", 0, 0, 0, 20, "宋体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //测量员
                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 - 4, OrigYMapFrame - (12 + 8.0) / 10.0 + 0.1);
                    AddElement(CreateTextElement(ptOrigin, "测量员：" + this.m_sMapSurveyor, "MapSur", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

                    //绘图员
                    ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
                    AddElement(CreateTextElement(ptOrigin, "绘图员：" + this.m_sMapDrawer, "MapDrawer", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);
                    //检查员
                    ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
                    AddElement(CreateTextElement(ptOrigin, "检查员：" + this.m_sMapChecker, "MapDrawer", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);


                    /*
                     * 接合图
                     */
                    //上横线
                    ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "top", 0, 0, 0, dSize), groupElement);

                    //下横线
                    ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "bottom", 0, 0, 0, dSize), groupElement);

                    // 左竖线
                    ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "left", 0, 0, 0, dSize), groupElement);

                    // 右竖线
                    ptStart.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "right", 0, 0, 0, dSize), groupElement);

                    // 第二横线
                    ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (19 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (19 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "bottom", 0, 0, 0, dSize), groupElement);

                    // 第三横线
                    ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (11 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 4.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (11 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "bottom", 0, 0, 0, dSize), groupElement);

                    // 第二竖线
                    ptStart.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "left", 0, 0, 0, dSize), groupElement);

                    // 第三竖线
                    ptStart.PutCoords(OrigXMapFrame + 3, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (27 + 12) / 10.0);
                    ptEnd.PutCoords(OrigXMapFrame + 3, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (3 + 12) / 10.0);
                    AddElement(CreateLineElement(ptStart, ptEnd, "left", 0, 0, 0, dSize), groupElement);

                    // 填充中间
                    IPointCollection pPntColl;
                    IPoint pPoint;
                    IFillShapeElement pFillShape;
                    ISimpleFillSymbol pFillSym;

                    object missing = Type.Missing;

                    pPntColl = new Polygon();
                    pPoint = new Point();
                    pPoint.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 3.1);
                    pPntColl.AddPoint(pPoint, ref missing, ref missing);
                    pPoint = new Point();
                    pPoint.PutCoords(OrigXMapFrame + 3.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 3.1);
                    pPntColl.AddPoint(pPoint, ref missing, ref missing);
                    pPoint = new Point();
                    pPoint.PutCoords(OrigXMapFrame + 3.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 2.3);
                    pPntColl.AddPoint(pPoint, ref missing, ref missing);
                    pPoint = new Point();
                    pPoint.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 2.3);
                    pPntColl.AddPoint(pPoint, ref missing, ref missing);
                    pPoint = new Point();
                    pPoint.PutCoords(OrigXMapFrame + 1.5, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 3.1);
                    pPntColl.AddPoint(pPoint, ref missing, ref missing);

                    IElement pTemp = new PolygonElementClass();
                    pTemp.Geometry = pPntColl as IPolygon;

                    pFillSym = new SimpleFillSymbolClass();
                    pFillSym.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;

                    pFillShape = pTemp as IFillShapeElement;
                    pFillShape.Symbol = pFillSym;

                    AddElement((IElement)pFillShape, groupElement);

                    // 输出接图表内容
                    double textSize = 5;//11
                    ptOrigin.PutCoords(OrigXMapFrame + 15.0 / 2 / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 20 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[1].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + 15.0 / 2 / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 12 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[4].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + 15.0 / 2 / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 4 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[6].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + (15 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 20 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[2].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + (15 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 4 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[7].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + (30 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 20 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[3].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + (30 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 12 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[5].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    ptOrigin.PutCoords(OrigXMapFrame + (30 + 15.0 / 2) / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 3.0 + 4 - 1.25) / 10.0);
                    AddElement(CreateTextElement(ptOrigin, this.CurMapCode[8].ToString(), "MapMethod", 0, 0, 0, textSize, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

                    //生产单位
                    ptOrigin.PutCoords(OrigXMapFrame - (12 + 3) / 10.0, OrigYMapFrame + 10.2);//5);
                    this.AddDepartment(ptOrigin, m_sProductUnit, graphicsContainer);

                    //委托单位
                    ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset + 1, OrigYMapFrame + 7.3);//5);
                    this.AddDepartment(ptOrigin, "当阳市住房和城乡建设局", graphicsContainer);

                    AddElement(groupElement as ESRI.ArcGIS.Carto.IElement, graphicsContainer);
                }

            }
            pPageLayout.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGraphics, null, null);
            return pFrame;
        }

        //矩形范围，自适应比例尺图廓
        public IMapFrame DrawRegionFrame()
        {
            if (!this.CaculateMapInfo())
            {
                return null;
            }

            ClassOutline.origXMapFrame += ClassOutline.marginLeft;
            ClassOutline.origYMapFrame += ClassOutline.marginBottom;

            ESRI.ArcGIS.Carto.IGroupElement groupElement = new ESRI.ArcGIS.Carto.GroupElementClass();

            IPoint ptStart = new PointClass();
            IPoint ptEnd = new PointClass();

            // 内图廓，用MapFrame
            IEnvelope iEnv;
            IElement iEle;
            IMapFrame pFrame;
            IGraphicsContainer graphicsContainer;

            graphicsContainer = pPageLayout.PageLayout as IGraphicsContainer;
            pFrame = graphicsContainer.FindFrame(pPageLayout.ActiveView.FocusMap) as IMapFrame;

            pFrame.MapBounds.PutCoords(m_xMin, m_yMin, m_xMax, m_yMax);
            pFrame.Map.Name = "二维地图";
            pFrame.ExtentType = esriExtentTypeEnum.esriExtentBounds;
            iEnv = new EnvelopeClass();
            iEnv.XMin = OrigXMapFrame;
            iEnv.XMax = OrigXMapFrame + FRAMEWIDTH / 10.0;
            iEnv.YMin = OrigYMapFrame;
            iEnv.YMax = OrigYMapFrame + FRAMEHEIGHT / 10.0;
            iEle = pFrame as IElement;
            iEle.Geometry = iEnv;
            pFrame.ExtentType = ESRI.ArcGIS.Carto.esriExtentTypeEnum.esriExtentScale;
            pFrame.MapScale = m_dScale;


            ESRI.ArcGIS.Geometry.IEnvelope eee = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            //			eee.PutCoords( m_yMin,m_xMin,m_yMax,m_xMax);
            eee.PutCoords(m_xMin, m_yMin, m_xMax, m_yMax);
            ESRI.ArcGIS.Carto.IActiveView mapView = pFrame.Map as ESRI.ArcGIS.Carto.IActiveView;
            mapView.Extent = eee;


            /*
             * 内图廓线
             */
            //下横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame);
            AddElement(CreateLineElement(ptStart, ptEnd), groupElement);

            //上横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0);
            AddElement(CreateLineElement(ptStart, ptEnd), groupElement);

            //左竖线
            ptStart.PutCoords(OrigXMapFrame, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd), groupElement);

            //右竖线
            ptStart.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd), groupElement);

            /*
             * 外图廓线
             */
            const double dSize = 1;
            //下横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //上横线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //左竖线
            ptStart.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame - InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //右竖线
            ptStart.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame - InnerBorderOffset);
            ptEnd.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + InnerBorderOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + InnerBorderOffset);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, dSize), groupElement);

            //格网
            this.DrawGrid(groupElement);

            // 注记
            IPoint ptOrigin = new PointClass();
            string strText = "";
            double xOffset;//, yOffset;

            //考虑字体和定位点
            xOffset = 0.8;
            //yOffset = 0.05 ;

            //西南角坐标
            ptOrigin.PutCoords(OrigXMapFrame - xOffset, OrigYMapFrame + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter),
                graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame, OrigYMapFrame - (12.0 - 0.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //西北角坐标
            ptOrigin.PutCoords(OrigXMapFrame - xOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12.0 - 3.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //东北角坐标
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + xOffset, OrigYMapFrame + FRAMEHEIGHT / 10.0 + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12.0 - 3.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);
            //东南角坐标
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 + xOffset, OrigYMapFrame + 0.05);
            strText = this.XPrefix + this.CurMapCode.CoordToString((int)m_yMin);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0, OrigYMapFrame - (12.0 - 0.5) / 10.0);
            strText = this.YPrefix + this.CurMapCode.CoordToString((int)m_xMax);
            AddElement(CreateTextElement(ptOrigin, strText, "MapName", 0, 0, 0, 8, "正等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //图名 中间，外图廓上面
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 0.5) / 10.0);
            AddElement(CreateTextElement(ptOrigin, m_sMapName, "MapName", 0, 0, 0, 28, "等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //图号
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (12 + 5) / 10.0);
            AddElement(CreateTextElement(ptOrigin, m_sMapNumber, "MapName", 0, 0, 0, 28, "长等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);

            //外图廓右上角面
            ptOrigin.PutCoords(OrigXMapFrame + FRAMEWIDTH / 10.0 / 2.0, OrigYMapFrame + FRAMEHEIGHT / 10.0 + (13.0 + 12 + 0.5) / 10.0 - 0.8);
            AddElement(CreateTextElement(ptOrigin, m_sProjectName, "MapName", 0, 0, 0, 28, "细等线体", 0, esriTextHorizontalAlignment.esriTHACenter), graphicsContainer);


            // 位置需要计算字高
            // 成图方式,左与内图廓齐，下外图廓3.0毫米
            ptOrigin.PutCoords(OrigXMapFrame, OrigYMapFrame - (12 + 3.0 * 2) / 10.0 + 0.1);//按字高3.0毫米算，本来应该是3.0，现在*2
            AddElement(CreateTextElement(ptOrigin, m_sMakeMapMethod, "MapMethod", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);
            //			AddElement(CreateTextElement(ptOrigin, "成图方式:" + m_sMakeMapMethod, "MapMethod", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            // 坐标系,下移1.0mm
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, "1980西安坐标系", "MapCoor", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);

            // 高程基准、等高距
            ptOrigin.Y = ptOrigin.Y - 0.4 - 0.1;
            AddElement(CreateTextElement(ptOrigin, "1985国家高程基准" + ",等高距为" + this.m_sIsoHeightDistance + "m", "MapHeight", 0, 0, 0, 13, "细等线体", 0, esriTextHorizontalAlignment.esriTHALeft), graphicsContainer);
            AddElement(groupElement as ESRI.ArcGIS.Carto.IElement, graphicsContainer);           
            pPageLayout.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGraphics, null, null);

            return pFrame;
        }

        /// <summary>
        /// 绘制图廓的格网
        /// </summary>
        /// <param name="groupElement">组合元素</param>
        private void DrawGrid(ESRI.ArcGIS.Carto.IGroupElement groupElement)
        {
            switch (caculateType)
            {
                case CaculateType.Envelope:
                    //					this.DrawGridRegion(groupElement);
                    break;
                case CaculateType.MetaData:
                    this.DrawGridStandard(groupElement);
                    break;
            }
        }

        /// <summary>
        /// 绘制任意矩形的格网
        /// </summary>
        /// <param name="groupElement">组合元素</param>
        private void DrawGridRegion(ESRI.ArcGIS.Carto.IGroupElement groupElement)
        {
            ESRI.ArcGIS.Geometry.IPoint ptStart = new ESRI.ArcGIS.Geometry.PointClass();
            ESRI.ArcGIS.Geometry.IPoint ptEnd = new ESRI.ArcGIS.Geometry.PointClass();
            double dblStep = 10.0; //单位为cm

            double startX, startY;
            if (m_dScale < 500.001)
            {
                int offsetX = ((int)m_xMin) % 50;
                int offsetY = ((int)m_yMin) % 50;
                startX = offsetX / m_dScale * 100;
                startY = offsetY / m_dScale * 100;
            }
            else
            {
                int offsetX = ((int)m_xMin) % 100;
                int offsetY = ((int)m_yMin) % 100;
                startX = offsetX / m_dScale * 100;
                startY = offsetY / m_dScale * 100;
            }

            int xStep = (int)(FRAMEWIDTH / 10.0 / dblStep + 0.01);//加0.01的目的是去掉浮点运算可能造成的误差，一般也可以不要
            int yStep = (int)(FRAMEHEIGHT / 10.0 / dblStep + 0.01);
            double x, y;
            for (int i = 0; i <= xStep; i++)
            {
                x = OrigXMapFrame + startX + dblStep * i;
                for (int j = 0; j <= yStep; j++)
                {
                    y = OrigYMapFrame + startY + dblStep * j;
                    AddCross(x, y, groupElement);
                }
            }
        }

        /// <summary>
        /// 绘制标准图幅的格网
        /// </summary>
        /// <param name="groupElement">组合元素</param>
        private void DrawGridStandard(ESRI.ArcGIS.Carto.IGroupElement groupElement)
        {
            ESRI.ArcGIS.Geometry.IPoint ptStart = new ESRI.ArcGIS.Geometry.PointClass();
            ESRI.ArcGIS.Geometry.IPoint ptEnd = new ESRI.ArcGIS.Geometry.PointClass();
            double dblStep = 10.0; //单位为cm

            int xStep = (int)(FRAMEWIDTH / 10.0 / dblStep + 0.01);//加0.01的目的是去掉浮点运算可能造成的误差，一般也可以不要
            int yStep = (int)(FRAMEHEIGHT / 10.0 / dblStep + 0.01);
            double x, y;
            for (int i = 0; i <= xStep; i++)
            {
                x = OrigXMapFrame + dblStep * i;
                for (int j = 0; j <= yStep; j++)
                {
                    y = OrigYMapFrame + dblStep * j;
                    AddCross(x, y, groupElement);
                }
            }
        }

        /// <summary>
        /// 十字丝
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="ge">组合元素</param>
        private void AddCross(double x, double y, IGroupElement ge)
        {
            double xmin = OrigXMapFrame;
            double xmax = OrigXMapFrame + FRAMEWIDTH / 10;
            double ymin = OrigYMapFrame;
            double ymax = OrigYMapFrame + FRAMEHEIGHT / 10;

            if (x < xmin || x > xmax || y < ymin || y > ymax)	//点不在图的范围内
            {
                return;
            }

            IPoint ptStart = new PointClass();
            IPoint ptEnd = new PointClass();
            double halfLength = 0.5;// * ScaleFactor;

            if (x - halfLength < xmin)	//或者只比大的
            {
                ptStart.PutCoords(xmin, y);
            }
            else
            {
                ptStart.PutCoords(x - halfLength, y);
            }
            if (x + halfLength > xmax)
            {
                ptEnd.PutCoords(xmax, y);
            }
            else
            {
                ptEnd.PutCoords(x + halfLength, y);
            }
            //AddElement(CreateLineElement(ptStart, ptEnd), ge);
            AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), ge);

            if (y - halfLength < ymin)
            {
                ptStart.PutCoords(x, ymin);
            }
            else
            {
                ptStart.PutCoords(x, y - halfLength);
            }
            if (y + halfLength > ymax)
            {
                ptEnd.PutCoords(x, ymax);
            }
            else
            {
                ptEnd.PutCoords(x, y + halfLength);
                AddElement(CreateLineElement(ptStart, ptEnd, "Bottom", 0, 0, 0, 1), ge);
                //AddElement(CreateLineElement(ptStart, ptEnd), ge);
            }

        }

        /// <summary>
        /// 生产单位
        /// </summary>
        /// <param name="ptOrigin">坐标</param>
        /// <param name="sDepartment">字符串</param>
        /// <param name="pGC">绘制层</param>
        private void AddDepartment(IPoint ptOrigin, string sDepartment, ESRI.ArcGIS.Carto.IGraphicsContainer pGC)
        {
            // 测绘机关 ，需要处理一下，加入\n
            string sNewUnit = "";
            int iLen = sDepartment.Length;

            string sSingle;

            for (int iPos = 0; iPos < iLen; iPos++)
            {
                sSingle = sDepartment.Substring(iPos, 1); ;
                sNewUnit = sNewUnit + sSingle + "\n";
            }
            AddElement(
                CreateTextElement(ptOrigin, sNewUnit, "MapDepartment", 0, 0, 0, 18, "中等线体", 0, esriTextHorizontalAlignment.esriTHARight),
                pGC);
        }


        private void Load_XmlInfoToTextBox()
        {
            if (System.IO.File.Exists(sDir + sXmlNme))
            {
                XmlTextReader xmlRdr = new XmlTextReader(sDir + sXmlNme);
                int i = 0;
                while (xmlRdr.Read())
                {
                    if (xmlRdr.NodeType == XmlNodeType.Text)
                    {
                        i++;
                        switch (i)
                        {
                            case 1:
                                {
                                    break;
                                }
                            case 2:
                                {
                                    break;
                                }
                            case 3:
                                {
                                    m_sCord = xmlRdr.Value;//
                                    break;
                                }
                            case 4:
                                {
                                    m_sHeightBase = xmlRdr.Value;
                                    break;
                                }
                            case 5:
                                {
                                    m_sProductUnit = xmlRdr.Value;
                                    break;
                                }
                            case 6:
                                {

                                    break;
                                }
                            case 7:
                                {

                                    break;
                                }
                            case 8:
                                {
                                    m_sProjectName = xmlRdr.Value;
                                    break;
                                }
                            case 9:
                                {
                                    m_sMakeMapMethod = xmlRdr.Value;
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
                xmlRdr.Close();
            }

        }
    }
}
