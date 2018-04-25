using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DData.Class;
using DFDataConfig.Class;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using DF3DDraw;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using DFCommon.Class;
using Gvitech.CityMaker.FdeCore;
using DevExpress.XtraBars.Docking;
using DFWinForms.Class;
using DF3DScan.UC;

namespace DF3DScan.Frm
{
    public class FrmNormalRegionQuery : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ComboBoxEdit cmbLayer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private SimpleButton btnPoint;
        private SimpleButton btnLine;
        private SpinEdit seBufferDis;
        private ComboBoxEdit cmbSpatialRelation;
        private SimpleButton btnPolygon;
        private SimpleButton btnRectangle;
        private SimpleButton btnCircle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNormalRegionQuery));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnPoint = new DevExpress.XtraEditors.SimpleButton();
            this.btnLine = new DevExpress.XtraEditors.SimpleButton();
            this.seBufferDis = new DevExpress.XtraEditors.SpinEdit();
            this.cmbSpatialRelation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnPolygon = new DevExpress.XtraEditors.SimpleButton();
            this.btnRectangle = new DevExpress.XtraEditors.SimpleButton();
            this.btnCircle = new DevExpress.XtraEditors.SimpleButton();
            this.cmbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seBufferDis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpatialRelation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnPoint);
            this.layoutControl1.Controls.Add(this.btnLine);
            this.layoutControl1.Controls.Add(this.seBufferDis);
            this.layoutControl1.Controls.Add(this.cmbSpatialRelation);
            this.layoutControl1.Controls.Add(this.btnPolygon);
            this.layoutControl1.Controls.Add(this.btnRectangle);
            this.layoutControl1.Controls.Add(this.btnCircle);
            this.layoutControl1.Controls.Add(this.cmbLayer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(326, 88);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnPoint
            // 
            this.btnPoint.Image = ((System.Drawing.Image)(resources.GetObject("btnPoint.Image")));
            this.btnPoint.Location = new System.Drawing.Point(2, 28);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(58, 30);
            this.btnPoint.StyleController = this.layoutControl1;
            this.btnPoint.TabIndex = 1;
            this.btnPoint.Text = "点";
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // btnLine
            // 
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.Location = new System.Drawing.Point(64, 28);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(50, 30);
            this.btnLine.StyleController = this.layoutControl1;
            this.btnLine.TabIndex = 2;
            this.btnLine.Text = "线";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // seBufferDis
            // 
            this.seBufferDis.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.seBufferDis.Location = new System.Drawing.Point(85, 62);
            this.seBufferDis.Name = "seBufferDis";
            this.seBufferDis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seBufferDis.Size = new System.Drawing.Size(76, 22);
            this.seBufferDis.StyleController = this.layoutControl1;
            this.seBufferDis.TabIndex = 6;
            this.seBufferDis.EditValueChanged += new System.EventHandler(this.seBufferDis_EditValueChanged);
            // 
            // cmbSpatialRelation
            // 
            this.cmbSpatialRelation.EditValue = "包含";
            this.cmbSpatialRelation.Location = new System.Drawing.Point(230, 62);
            this.cmbSpatialRelation.Name = "cmbSpatialRelation";
            this.cmbSpatialRelation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSpatialRelation.Properties.Items.AddRange(new object[] {
            "包含",
            "相交"});
            this.cmbSpatialRelation.Size = new System.Drawing.Size(94, 22);
            this.cmbSpatialRelation.StyleController = this.layoutControl1;
            this.cmbSpatialRelation.TabIndex = 7;
            // 
            // btnPolygon
            // 
            this.btnPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnPolygon.Image")));
            this.btnPolygon.Location = new System.Drawing.Point(250, 28);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(74, 30);
            this.btnPolygon.StyleController = this.layoutControl1;
            this.btnPolygon.TabIndex = 5;
            this.btnPolygon.Text = "多边形";
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.Location = new System.Drawing.Point(184, 28);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(62, 30);
            this.btnRectangle.StyleController = this.layoutControl1;
            this.btnRectangle.TabIndex = 4;
            this.btnRectangle.Text = "矩形";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Location = new System.Drawing.Point(118, 28);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(62, 30);
            this.btnCircle.StyleController = this.layoutControl1;
            this.btnCircle.TabIndex = 3;
            this.btnCircle.Text = "圆形";
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // cmbLayer
            // 
            this.cmbLayer.Location = new System.Drawing.Point(85, 2);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLayer.Properties.DropDownRows = 10;
            this.cmbLayer.Properties.NullValuePrompt = "(请选择一个图层)";
            this.cmbLayer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLayer.Size = new System.Drawing.Size(239, 22);
            this.cmbLayer.StyleController = this.layoutControl1;
            this.cmbLayer.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(326, 88);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbLayer;
            this.layoutControlItem1.CustomizationFormText = "查询图层：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(326, 26);
            this.layoutControlItem1.Text = "查询图层：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCircle;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(116, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(66, 34);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnRectangle;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(182, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(66, 34);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnPolygon;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(248, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(78, 34);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnLine;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(62, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(54, 34);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnPoint;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(62, 34);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbSpatialRelation;
            this.layoutControlItem5.CustomizationFormText = "相交类型：";
            this.layoutControlItem5.Location = new System.Drawing.Point(163, 60);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(163, 28);
            this.layoutControlItem5.Text = "相交类型：";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.seBufferDis;
            this.layoutControlItem6.CustomizationFormText = "缓冲距离：";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(163, 28);
            this.layoutControlItem6.Text = "缓冲距离(m)：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnLine;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem8.Name = "layoutControlItem7";
            this.layoutControlItem8.Size = new System.Drawing.Size(66, 34);
            this.layoutControlItem8.Text = "layoutControlItem7";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // FrmNormalRegionQuery
            // 
            this.ClientSize = new System.Drawing.Size(326, 88);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(5, 180);
            this.Name = "FrmNormalRegionQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "区域查询";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNormalRegionQuery_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seBufferDis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpatialRelation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        private static FrmNormalRegionQuery instance = null;
        private static readonly object syncRoot = new object();
        public static FrmNormalRegionQuery Instance
        {
            get
            {
                if (FrmNormalRegionQuery.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FrmNormalRegionQuery.instance == null)
                        {
                            FrmNormalRegionQuery.instance = new FrmNormalRegionQuery();
                        }
                    }
                }
                return FrmNormalRegionQuery.instance;
            }
        }

        private AxRenderControl _3DControl;
        protected ICurveSymbol _curveSymbol;
        protected ISurfaceSymbol _surfaceSymbol;

        private FrmNormalRegionQuery()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) { this.Enabled = false; return; }
            this._3DControl = app.Current3DMapControl;
            this._curveSymbol = new CurveSymbol();
            this._curveSymbol.Width = -2;
            this._curveSymbol.Color = Convert.ToUInt32(SystemInfo.Instance.LineColor, 16);
            this._surfaceSymbol = new SurfaceSymbol();
            this._surfaceSymbol.Color = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
            this._surfaceSymbol.BoundarySymbol = this._curveSymbol;
        }

        public void Init()
        {
            try
            {
                this.cmbLayer.Properties.Items.Clear();
                List<DF3DFeatureClass> list = DF3DFeatureClassManager.Instance.GetAllFeatureClass();
                if (list != null)
                {
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        FacilityClass fac = dffc.GetFacilityClass();
                        if (fac != null && (fac.Name == "PipeLine" || fac.Name == "PipeNode" || fac.Name == "PipeBuild" || fac.Name == "PipeBuild1")) break;
                        IBaseLayer treelayer = dffc.GetTreeLayer();
                        if (treelayer != null && treelayer.Visible == false) continue;
                        this.cmbLayer.Properties.Items.Add(dffc);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
                this._drawTool = null;
            }
            if (_uc != null)
            {
                this._uc.Dispose();
                this._uc = null;
            }
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
        }
        private DrawTool _drawTool;
        private IRenderGeometry _rPolygon;

        private void Clear()
        {
            if (_rPolygon != null)
            {
                this._3DControl.ObjectManager.DeleteObject(_rPolygon.Guid);
                _rPolygon = null;
            }
            this._3DControl.HighlightHelper.VisibleMask = 0;
            this._3DControl.HighlightHelper.Color = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
            this._3DControl.HighlightHelper.SetRegion(null);
        }
        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }

        private void OnFinishedDraw()
        {
            try
            {
                if (this._drawTool != null && this._drawTool.GetGeo() != null)
                {
                    switch (this._drawTool.GeoType)
                    {
                        case DrawType.Point:
                            break;
                        case DrawType.Polyline:
                            break;
                        case DrawType.Circle:
                            break;
                        case DrawType.Rectangle:
                            break;
                        case DrawType.Polygon:
                            break;
                        default:
                            return;
                    }
                    
                    IGeometry geo = this._drawTool.GetGeo();
                    if (geo == null) return;
                    IGeometry geoBuffer = (geo as ITopologicalOperator2D).Buffer2D(Convert.ToDouble(this.seBufferDis.Value.ToString()), gviBufferStyle.gviBufferCapround);
                    if (geoBuffer == null) return;
                    if (this._drawTool != null)
                    {
                        this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                        this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                        this._drawTool.Close();
                        this._drawTool.End();
                    }
                    this._3DControl.HighlightHelper.VisibleMask = 1;
                    this._3DControl.HighlightHelper.SetRegion(geoBuffer);

                    if (geoBuffer.GeometryType == gviGeometryType.gviGeometryPolygon)
                    {
                        this._rPolygon = this._3DControl.ObjectManager.CreateRenderPolygon(geoBuffer as IPolygon, this._surfaceSymbol, this._3DControl.ProjectTree.RootID);
                        if (this._3DControl.Terrain.IsRegistered && this._3DControl.Terrain.VisibleMask != gviViewportMask.gviViewNone)
                            (this._rPolygon as IRenderPolygon).HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                        else (this._rPolygon as IRenderPolygon).HeightStyle = gviHeightStyle.gviHeightAbsolute;
                    }
                    else if (geoBuffer.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                    {
                        this._rPolygon = this._3DControl.ObjectManager.CreateRenderMultiPolygon(geoBuffer as IMultiPolygon, this._surfaceSymbol, this._3DControl.ProjectTree.RootID);
                        if (this._3DControl.Terrain.IsRegistered && this._3DControl.Terrain.VisibleMask != gviViewportMask.gviViewNone)
                            (this._rPolygon as IRenderMultiPolygon).HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                        else (this._rPolygon as IRenderMultiPolygon).HeightStyle = gviHeightStyle.gviHeightAbsolute;
                    }
                    else return;

                    if (this.cmbLayer.SelectedItem == null) return;
                    ISpatialFilter filter = new SpatialFilter();
                    filter.Geometry = geoBuffer;
                    if (this.cmbSpatialRelation.Text == "包含") filter.SpatialRel = gviSpatialRel.gviSpatialRelContains;
                    else filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                    DF3DFeatureClass dffc = this.cmbLayer.SelectedItem as DF3DFeatureClass;
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) return;
                    IFeatureLayer fl = dffc.GetFeatureLayer();
                    if (fl == null) return;
                    filter.GeometryField = fl.GeometryFieldName;
                    int count = fc.GetCount(filter);
                    if (count == 0)
                    {
                        XtraMessageBox.Show("查询结果为空", "提示");
                        return;
                    }
                    WaitForm.Start("正在查询...", "请稍后");
                    this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location1, this._width, this._height);
                    this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                    this._dockPanel.Visibility = DockVisibility.Visible;
                    this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                    this._dockPanel.Width = this._width;
                    this._dockPanel.Height = this._height;
                    this._uc = new UCPropertyInfo();
                    this._uc.Init();
                    this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
                    this._uPanel.RegisterEvent(new PanelClose(this.Close));
                    this._dockPanel.Controls.Add(this._uc);
                    this._uc.SetInfo(dffc, filter, count);
                    WaitForm.Stop();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        private DockPanel _dockPanel;
        private int _height = 400;
        private UIDockPanel _uPanel;
        private int _width = 225;
        private UCPropertyInfo _uc;
        private System.Drawing.Point Location1
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }


        private void btnPoint_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Point);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polyline);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Circle);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Rectangle);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polygon);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void seBufferDis_EditValueChanged(object sender, EventArgs e)
        {
            if (this.seBufferDis.Value <= 0) this.seBufferDis.Value = Convert.ToDecimal(0.01);
        }

        private void FrmNormalRegionQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.RestoreEnv();
            instance = null;
        }

    }
}
