using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraGrid.Views.Base;

namespace DF2DDataCheck.Frm
{
    public partial class FormCheckResult : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ComboBoxEdit cmbLayer;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private IMapControl2 m_pMapControl;
        private SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Dictionary<IFeatureClass, DataTable> dict;
        public FormCheckResult()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.cmbLayer);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(857, 511);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(678, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(167, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbLayer
            // 
            this.cmbLayer.Location = new System.Drawing.Point(12, 12);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLayer.Size = new System.Drawing.Size(833, 22);
            this.cmbLayer.StyleController = this.layoutControl1;
            this.cmbLayer.TabIndex = 5;
            this.cmbLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(833, 435);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupFormat = "";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "错误要素ID";
            this.gridColumn1.FieldName = "ErrorFeatureID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "错误类型";
            this.gridColumn2.FieldName = "ErrorType";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "要素所在要素类";
            this.gridColumn3.FieldName = "FeatureofClass";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "要素所在图层";
            this.gridColumn4.FieldName = "FeatureofLayer";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "要素";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(857, 511);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(837, 439);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbLayer;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(837, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(666, 465);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(171, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 465);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(666, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FormCheckResult
            // 
            this.ClientSize = new System.Drawing.Size(857, 511);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FormCheckResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据检查结果";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }
        public FormCheckResult(Dictionary<IFeatureClass, DataTable> dt, IMapControl2 current2DMapControl)
        {
            InitializeComponent();
            dict = dt;
            m_pMapControl = current2DMapControl;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (dict == null) return;
            foreach (IFeatureClass fc in dict.Keys)
            {
                this.cmbLayer.Properties.Items.Add(fc.AliasName);
            }
            if (this.cmbLayer.Properties.Items.Count > 0) this.cmbLayer.SelectedIndex = 0;
            
        }

        private DataTable GetDataTable(string fcname)
        {
            foreach (IFeatureClass fc in dict.Keys)
            {
                if (fc.AliasName == fcname) return dict[fc];
            }
            return null;

        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = GetDataTable(this.cmbLayer.Text);
            this.gridControl1.DataSource = dt;
            gridView1.RefreshData();
            //this.gridControl1.MainView = this.gridView1;
            gridView1.Columns["ErrorFeatureID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns["ErrorFeatureID"].SummaryItem.DisplayFormat = "合计：{0}";
            gridView1.ExpandAllGroups();
        }


        #region  //定位要素

        private void MoveToFeature(IMapControl2 current2DMapControl, IFeature pFeature)
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
            current2DMapControl.Extent = pEnvelope;
            current2DMapControl.ActiveView.Refresh();

        }
        #endregion

        #region //高亮显示选中要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeature(IMapControl2 current2DMapControl, IFeature pFeature)
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

                current2DMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
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

                current2DMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);

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
                current2DMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
            }

            current2DMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, current2DMapControl.ActiveView.Extent);//视图刷新
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
           
            if (this.gridView1.FocusedRowHandle == -1) return;
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);    
            if (dr["FeatureClass"] != null && dr["FeatureClass"] is IFeatureClass)
            {
                IFeatureClass  fc = dr["FeatureClass"] as IFeatureClass;
                //声明ColumnView对象
                var columnView = (ColumnView)gridControl1.FocusedView;
                //得到选中的行索引
                int id = columnView.FocusedRowHandle;
                if (id < 0) return;
                string SID = gridView1.GetRowCellValue(id, "ErrorFeatureID").ToString();//在GridControl中读出要素的ID
                IFeature pFture = null;
                IFeatureCursor pFeaCursor;
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = "OBJECTID = " + SID;
                pFeaCursor = fc.Search(pFilter, true);
                if (pFeaCursor != null)
                {
                    pFture = pFeaCursor.NextFeature();

                    if (pFture != null)
                    {
                        MoveToFeature(m_pMapControl, pFture);//定位
                        ShowSelectionFeature(m_pMapControl, pFture);//高亮
                    }
                }              
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();

        }

       
       

        
      
    }
}
