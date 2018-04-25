using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

using DevExpress.Utils;
using DevExpress.XtraEditors;


namespace DF2DDataCheck.Frm
{
    public partial class frmCheckResult : XtraForm
    {
        private List<DataTable> m_dtList;
        private IMapControl2 m_pMapControl;
        private IFeatureWorkspace m_pWsp;
        public frmCheckResult()
        {
            InitializeComponent();
        }
        public frmCheckResult(List<DataTable> dtList, IMapControl2 pMapControl, IFeatureWorkspace pWsp)
        {
            InitializeComponent();

            m_dtList = dtList;
            m_pMapControl = pMapControl;
            m_pWsp = pWsp;
        }

        private void frmCheckResult_Load(object sender, EventArgs e)
        {
            string strTableName;
            DataTable tempTable;

            for (int i = 0; i < m_dtList.Count; i++)
            {
                tempTable = m_dtList[i];
                strTableName = getTableName(tempTable.TableName);
                if (strTableName != "")
                {
                    this.tabControl1.TabPages.Add(strTableName);
                    DataGrid grid = new DataGrid();
                    grid.Name = "grid";
                    grid.DataSource = tempTable;
                    grid.Dock = DockStyle.Fill;
                    grid.PreferredColumnWidth = 200;
                    grid.ReadOnly = true;
                    this.tabControl1.TabPages[i].Controls.Add(grid);
                }
            }
        }
        private string getTableName(string strDatasetName)
        {
            string strTableName = "";

            switch (strDatasetName)
            {
                case "EP500":
                    strTableName = "电力管线";
                    break;
                case "BP500":
                    strTableName = "燃气管线";
                    break;
                case "DP500":
                    strTableName = "排水管线";
                    break;
                case "FP500":
                    strTableName = "给水管线";
                    break;
                case "HP500":
                    strTableName = "热力管线";
                    break;
                case "TP500":
                    strTableName = "通讯管线";
                    break;
                default:
                    strTableName = "地下管线";
                    break;
            }
            return strTableName;
        }

       

       

        #region //定位要素
        private void MoveToFeature(IMapControl2 pMapControl, IFeature pFeature)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            IEnvelope pGeoEnv;
            IPoint pPoint = new PointClass();
            IGeometry pGeometry = pFeature.Shape;
            pGeoEnv = pGeometry.Envelope;
            pPoint.X = (pGeoEnv.XMin + pGeoEnv.XMax) / 2;
            pPoint.Y = (pGeoEnv.YMin + pGeoEnv.YMax) / 2;
            double xWidth;
            double yWidth;
            xWidth = (pGeoEnv.XMax - pGeoEnv.XMin);
            yWidth = (pGeoEnv.XMax - pGeoEnv.XMin);
            pEnvelope.XMin = pPoint.X - (xWidth / 2);
            pEnvelope.XMax = pPoint.X + (xWidth / 2);
            pEnvelope.YMin = pPoint.Y - (yWidth / 2);
            pEnvelope.YMax = pPoint.Y + (yWidth / 2);
            pEnvelope.Expand(5, 5, false);
            pMapControl.Extent = pEnvelope;
            pMapControl.ActiveView.Refresh();

        }
        #endregion
        #region //高亮显示选中要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeature(IMapControl2 pMapControl, IFeature pFeature)
        {
            //分点、线、面，给新的symbol
            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new MarkerElementClass();
                pElement.Geometry = pFeature.Shape;

                IMarkerElement pMarkerElement;
                pMarkerElement = (IMarkerElement)pElement;
                pMarkerElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new LineElementClass();
                pElement.Geometry = pFeature.Shape;

                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);

            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IElement pElement = new LineElementClass();
                IPolyline pLine;
                pLine = GetPolygonBoundary((IPolygon)pFeature.Shape);
                pElement.Geometry = pLine;

                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                pSymbol = (ILineSymbol)GetDefaultSymbol(pLine.GeometryType);

                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
            }

            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pMapControl.ActiveView.Extent);//视图刷新
        }
        #endregion

        #region //得到一个Polygon对象的轮廓线
        /// <summary>
        /// 得到一个Polygon对象的轮廓线
        /// </summary>
        /// <param name="pPolygon">一个Polygon对象</param>
        /// <returns>一个Polyline对象</returns>
        public static IPolyline GetPolygonBoundary(IPolygon pPolygon)
        {
            //通过ITopologicalOperator 对象转换成线
            ITopologicalOperator pTopo;
            IPolyline pPolyline;

            pTopo = (ITopologicalOperator)pPolygon;
            pPolyline = (IPolyline)pTopo.Boundary;
            return pPolyline;
        }
        #endregion

        #region 根据Geometry类型生成一个默认符号
        /// <summary>
        /// 根据Geometry类型生成一个默认符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISymbol GetDefaultSymbol(esriGeometryType type)
        {
            ISymbol sym = null;

            IRgbColor mCor = new RgbColorClass();
            mCor.Green = 255;
            mCor.Blue = 255;
            IRgbColor lCor = new RgbColorClass();
            lCor.Red = 0;
            lCor.Green = 0;
            lCor.Blue = 255;
            IRgbColor fCor = new RgbColorClass();
            fCor.Green = 255;
            fCor.Blue = 255;

            IMarkerSymbol mark = new SimpleMarkerSymbolClass();
            mark.Color = mCor;
            mark.Size = 16;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 4;
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
                    break;
            }

            return sym;
        }
        #endregion

       

        private void btnMap_Click_1(object sender, EventArgs e)
        {
            DataGrid curGrid;
            curGrid = (this.tabControl1.SelectedTab.Controls.Find("grid", true))[0] as DataGrid;
            m_pMapControl.Map.ClearSelection();
            int id = curGrid.CurrentRowIndex;
            if (id < 0) return;

            string SID = curGrid[id, 0].ToString();//在DataGrid中读出要素的ID
            string strClassName = curGrid[id, 3].ToString();//读出要素所在层名
            //string strTopoClass = strClassName + "_Topo";

            IFeature pFture = null;
            IFeatureCursor pFeaCursor;
            IFeatureClass pCurFC;
            IQueryFilter pFilter = new QueryFilterClass();
            pFilter.WhereClause = "OBJECTID = " + SID;

            pCurFC = m_pWsp.OpenFeatureClass(strClassName);
            pFeaCursor = pCurFC.Search(pFilter, true);
            if (pFeaCursor != null)
            {
                pFture = pFeaCursor.NextFeature();

                if (pFture != null)
                {
                    MoveToFeature(m_pMapControl, pFture);
                    ShowSelectionFeature(m_pMapControl, pFture);
                }
            }
        }

        private void btnput_Click_1(object sender, EventArgs e)
        {
            /*DataTable dtOutput;
            DataGrid grid;
            grid = (this.tabControl1.SelectedTab.Controls.Find("grid", true))[0] as DataGrid;
            dtOutput = grid.DataSource as DataTable;
            string strDataCheck = "DataCheckResult";
            OutputToExcel ToExcel = new OutputToExcel(dtOutput, strDataCheck);
            ToExcel.ProduceExcel_1(dtOutput);*/
        }

        private void close_Click_1(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

    }
}
