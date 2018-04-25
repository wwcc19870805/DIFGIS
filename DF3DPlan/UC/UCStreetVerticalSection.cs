using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DDraw;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using DFWinForms.Class;
using System.IO;
using System.Diagnostics;

namespace DF3DPlan.UC
{
    public class UCStreetVerticalSection : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private LabelControl labelControl1;
        private SimpleButton btnOutput;
        private SimpleButton btnDrawSection;
        private SpinEdit spResolution;
        private SpinEdit seMinHeight;
        private SpinEdit seMaxHeight;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private ColorPickEdit cpeBackGround;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private TrackBarControl tbcClipDis;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private CheckEdit ceShowImage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceShowImage = new DevExpress.XtraEditors.CheckEdit();
            this.tbcClipDis = new DevExpress.XtraEditors.TrackBarControl();
            this.cpeBackGround = new DevExpress.XtraEditors.ColorPickEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnOutput = new DevExpress.XtraEditors.SimpleButton();
            this.btnDrawSection = new DevExpress.XtraEditors.SimpleButton();
            this.spResolution = new DevExpress.XtraEditors.SpinEdit();
            this.seMinHeight = new DevExpress.XtraEditors.SpinEdit();
            this.seMaxHeight = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcClipDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcClipDis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpeBackGround.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spResolution.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMinHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMaxHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceShowImage);
            this.layoutControl1.Controls.Add(this.tbcClipDis);
            this.layoutControl1.Controls.Add(this.cpeBackGround);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnOutput);
            this.layoutControl1.Controls.Add(this.btnDrawSection);
            this.layoutControl1.Controls.Add(this.spResolution);
            this.layoutControl1.Controls.Add(this.seMinHeight);
            this.layoutControl1.Controls.Add(this.seMaxHeight);
            this.layoutControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(244, 366);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceShowImage
            // 
            this.ceShowImage.EditValue = true;
            this.ceShowImage.Location = new System.Drawing.Point(2, 295);
            this.ceShowImage.Name = "ceShowImage";
            this.ceShowImage.Properties.Caption = "自动打开图片";
            this.ceShowImage.Size = new System.Drawing.Size(240, 19);
            this.ceShowImage.StyleController = this.layoutControl1;
            this.ceShowImage.TabIndex = 12;
            // 
            // tbcClipDis
            // 
            this.tbcClipDis.EditValue = 200;
            this.tbcClipDis.Location = new System.Drawing.Point(113, 86);
            this.tbcClipDis.Name = "tbcClipDis";
            this.tbcClipDis.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbcClipDis.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbcClipDis.Properties.Maximum = 500;
            this.tbcClipDis.Properties.Minimum = 10;
            this.tbcClipDis.Size = new System.Drawing.Size(117, 45);
            this.tbcClipDis.StyleController = this.layoutControl1;
            this.tbcClipDis.TabIndex = 2;
            this.tbcClipDis.Value = 200;
            this.tbcClipDis.EditValueChanged += new System.EventHandler(this.tbcClipDis_EditValueChanged);
            // 
            // cpeBackGround
            // 
            this.cpeBackGround.EditValue = System.Drawing.Color.White;
            this.cpeBackGround.Location = new System.Drawing.Point(113, 231);
            this.cpeBackGround.Name = "cpeBackGround";
            this.cpeBackGround.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cpeBackGround.Size = new System.Drawing.Size(117, 22);
            this.cpeBackGround.StyleController = this.layoutControl1;
            this.cpeBackGround.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(177, 205);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "pixel/10m";
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(2, 269);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(240, 22);
            this.btnOutput.StyleController = this.layoutControl1;
            this.btnOutput.TabIndex = 6;
            this.btnOutput.Text = "出      图";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnDrawSection
            // 
            this.btnDrawSection.Location = new System.Drawing.Point(14, 135);
            this.btnDrawSection.Name = "btnDrawSection";
            this.btnDrawSection.Size = new System.Drawing.Size(216, 22);
            this.btnDrawSection.StyleController = this.layoutControl1;
            this.btnDrawSection.TabIndex = 3;
            this.btnDrawSection.Text = "绘制立面";
            this.btnDrawSection.Click += new System.EventHandler(this.btnDrawSection_Click);
            // 
            // spResolution
            // 
            this.spResolution.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spResolution.Location = new System.Drawing.Point(113, 205);
            this.spResolution.Name = "spResolution";
            this.spResolution.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spResolution.Size = new System.Drawing.Size(60, 22);
            this.spResolution.StyleController = this.layoutControl1;
            this.spResolution.TabIndex = 4;
            // 
            // seMinHeight
            // 
            this.seMinHeight.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.seMinHeight.Location = new System.Drawing.Point(113, 60);
            this.seMinHeight.Name = "seMinHeight";
            this.seMinHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seMinHeight.Size = new System.Drawing.Size(117, 22);
            this.seMinHeight.StyleController = this.layoutControl1;
            this.seMinHeight.TabIndex = 1;
            this.seMinHeight.EditValueChanged += new System.EventHandler(this.seMinHeight_EditValueChanged);
            // 
            // seMaxHeight
            // 
            this.seMaxHeight.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.seMaxHeight.Location = new System.Drawing.Point(113, 34);
            this.seMaxHeight.Name = "seMaxHeight";
            this.seMaxHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seMaxHeight.Size = new System.Drawing.Size(117, 22);
            this.seMaxHeight.StyleController = this.layoutControl1;
            this.seMaxHeight.TabIndex = 0;
            this.seMaxHeight.EditValueChanged += new System.EventHandler(this.seMaxHeight_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem3,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem7,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(244, 366);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 316);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(244, 50);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "立面参数设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(244, 171);
            this.layoutControlGroup2.Text = "立面参数";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.seMaxHeight;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem1.Text = "最高相对高度(m):";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seMinHeight;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem2.Text = "最低相对高度(m):";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnDrawSection;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 101);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tbcClipDis;
            this.layoutControlItem3.CustomizationFormText = "远裁距离(m)";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(220, 49);
            this.layoutControlItem3.Text = "远裁距离(m)：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 171);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(244, 96);
            this.layoutControlGroup3.Text = "出图参数";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spResolution;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(163, 26);
            this.layoutControlItem4.Text = "出图分辨率:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(163, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(57, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cpeBackGround;
            this.layoutControlItem5.CustomizationFormText = "背景颜色：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem5.Text = "背景颜色：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(96, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnOutput;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 267);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(244, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.ceShowImage;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 293);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(244, 23);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // UCStreetVerticalSection
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCStreetVerticalSection";
            this.Size = new System.Drawing.Size(244, 366);
            this.Load += new System.EventHandler(this.UCStreetVerticalSection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceShowImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcClipDis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbcClipDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpeBackGround.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spResolution.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMinHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMaxHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        public UCStreetVerticalSection()
        {
            InitializeComponent();
        }

        private DrawTool _drawTool;
        private IRenderMultiPolygon _renderBox;
        private IRenderPolygon _renderPolygon;
        public void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
            }
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.RcPictureExportBegin -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportBeginEventHandler(Current3DMapControl_RcPictureExportBegin);
            app.Current3DMapControl.RcPictureExporting -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportingEventHandler(Current3DMapControl_RcPictureExporting);
            app.Current3DMapControl.RcPictureExportEnd -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(Current3DMapControl_RcPictureExportEnd);
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
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Line && this._drawTool.GetGeo() != null)
            {
                DrawBox();
            }
        }

        private IEnvelope _env;
        private IEulerAngle _ang;
        private IPoint _center;

        private void DrawBox()
        {
            try
            {
                IPolyline line = this._drawTool.GetGeo() as IPolyline;
                if (line == null || line.PointCount != 2) return;
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;


                if (this._renderBox != null)
                {
                    app.Current3DMapControl.ObjectManager.DeleteObject(this._renderBox.Guid);
                    this._renderBox = null;
                }
                if (this._renderPolygon != null)
                {
                    app.Current3DMapControl.ObjectManager.DeleteObject(this._renderPolygon.Guid);
                    this._renderPolygon = null;
                }

                double middleZ = (line.StartPoint.Z + line.EndPoint.Z) / 2;
                double minZ = middleZ + (double)this.seMinHeight.Value;
                double maxZ = middleZ + (double)this.seMaxHeight.Value;
                IPoint startPoint = line.StartPoint;
                IPoint endPoint = line.EndPoint;
                IEulerAngle ang = app.Current3DMapControl.Camera.GetAimingAngles2(startPoint, endPoint);
                IEulerAngle angcz = new EulerAngle();
                angcz.Set(ang.Heading - 90, ang.Tilt, ang.Roll);
                IPoint pt1 = app.Current3DMapControl.Camera.GetAimingPoint2(startPoint, angcz, this.tbcClipDis.Value);
                IPoint pt2 = app.Current3DMapControl.Camera.GetAimingPoint2(endPoint, angcz, this.tbcClipDis.Value);

                ICRSFactory crsFact = new CRSFactory();
                ISpatialCRS crs = crsFact.CreateFromWKT(app.Current3DMapControl.GetCurrentCrsWKT()) as ISpatialCRS; 
                IGeometryFactory geoFact = new GeometryFactoryClass();
                IPoint pointb1 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointb1.SpatialCRS = crs;
                pointb1.SetCoords(startPoint.X, startPoint.Y, minZ, 0, 0);
                IPoint pointb2 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointb2.SpatialCRS = crs;
                pointb2.SetCoords(endPoint.X, endPoint.Y, minZ, 0, 0);
                IPoint pointb3 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointb3.SpatialCRS = crs;
                pointb3.SetCoords(pt2.X, pt2.Y, minZ, 0, 0);
                IPoint pointb4 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointb1.SpatialCRS = crs;
                pointb4.SetCoords(pt1.X, pt1.Y, minZ, 0, 0);
                IPolygon polygonBottom = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygonBottom.SpatialCRS = crs;
                polygonBottom.ExteriorRing.AppendPoint(pointb1);
                polygonBottom.ExteriorRing.AppendPoint(pointb2);
                polygonBottom.ExteriorRing.AppendPoint(pointb3);
                polygonBottom.ExteriorRing.AppendPoint(pointb4);

                IPoint pointt1 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointt1.SpatialCRS = crs;
                pointt1.SetCoords(startPoint.X, startPoint.Y, maxZ, 0, 0);
                IPoint pointt2 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointt2.SpatialCRS = crs;
                pointt2.SetCoords(endPoint.X, endPoint.Y, maxZ, 0, 0);
                IPoint pointt3 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointt3.SpatialCRS = crs;
                pointt3.SetCoords(pt2.X, pt2.Y, maxZ, 0, 0);
                IPoint pointt4 = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointt4.SpatialCRS = crs;
                pointt4.SetCoords(pt1.X, pt1.Y, maxZ, 0, 0);
                IPolygon polygonTop = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygonTop.SpatialCRS = crs;
                polygonTop.ExteriorRing.AppendPoint(pointt1);
                polygonTop.ExteriorRing.AppendPoint(pointt2);
                polygonTop.ExteriorRing.AppendPoint(pointt3);
                polygonTop.ExteriorRing.AppendPoint(pointt4);

                IPolygon polygon1 = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon1.SpatialCRS = crs;
                polygon1.ExteriorRing.AppendPoint(pointb1);
                polygon1.ExteriorRing.AppendPoint(pointb2);
                polygon1.ExteriorRing.AppendPoint(pointt2);
                polygon1.ExteriorRing.AppendPoint(pointt1);

                IPolygon polygon2 = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon2.SpatialCRS = crs;
                polygon2.ExteriorRing.AppendPoint(pointb1);
                polygon2.ExteriorRing.AppendPoint(pointb4);
                polygon2.ExteriorRing.AppendPoint(pointt4);
                polygon2.ExteriorRing.AppendPoint(pointt1);

                IPolygon polygon3 = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon3.SpatialCRS = crs;
                polygon3.ExteriorRing.AppendPoint(pointb2);
                polygon3.ExteriorRing.AppendPoint(pointb3);
                polygon3.ExteriorRing.AppendPoint(pointt3);
                polygon3.ExteriorRing.AppendPoint(pointt2);

                IPolygon polygon4 = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon4.SpatialCRS = crs;
                polygon4.ExteriorRing.AppendPoint(pointb3);
                polygon4.ExteriorRing.AppendPoint(pointb4);
                polygon4.ExteriorRing.AppendPoint(pointt4);
                polygon4.ExteriorRing.AppendPoint(pointt3);

                IMultiPolygon multiPolygon = geoFact.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
                multiPolygon.SpatialCRS = crs;
                multiPolygon.AddPolygon(polygonBottom);
                multiPolygon.AddPolygon(polygonTop);
                //multiPolygon.AddPolygon(polygon1);
                multiPolygon.AddPolygon(polygon2);
                multiPolygon.AddPolygon(polygon3);
                multiPolygon.AddPolygon(polygon4);

                ISurfaceSymbol ss = new SurfaceSymbolClass();
                ss.Color = 0x550000AA;
                ICurveSymbol cs = new CurveSymbolClass();
                cs.Color = 0xff0000AA;
                ss.BoundarySymbol = cs;
                this._renderPolygon = app.Current3DMapControl.ObjectManager.CreateRenderPolygon(polygon1, ss, app.Current3DMapControl.ProjectTree.RootID);

                ISurfaceSymbol ss1 = new SurfaceSymbolClass();
                ss1.Color = 0x88FFFFFF;
                ICurveSymbol cs1 = new CurveSymbolClass();
                cs1.Color = 0xffffffff;
                ss1.BoundarySymbol = cs1;
                this._renderBox = app.Current3DMapControl.ObjectManager.CreateRenderMultiPolygon(multiPolygon, ss1, app.Current3DMapControl.ProjectTree.RootID);

                _env = null;
                _ang = null;
                _center = null;

                _env = new EnvelopeClass();
                double xdis = Math.Sqrt((pointb1.X - pointb2.X) * (pointb1.X - pointb2.X) + (pointb1.Y - pointb2.Y) * (pointb1.Y - pointb2.Y)) / 2;
                double ydis = Math.Sqrt((pointb1.X - pointb4.X) * (pointb1.X - pointb4.X) + (pointb1.Y - pointb4.Y) * (pointb1.Y - pointb4.Y)) / 2;
                _env.MinZ = -ydis;
                _env.MaxZ = ydis;
                _env.MinX = -xdis;
                _env.MaxX = xdis;
                _env.MinY = -(maxZ - minZ) / 2.0;
                _env.MaxY = (maxZ - minZ) / 2.0;

                _center = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                _center.SpatialCRS = crs;
                _center.X = polygonBottom.Centroid.X;
                _center.Y = polygonBottom.Centroid.Y;
                _center.Z = polygon1.Centroid.Z;

                _ang = new EulerAngleClass();
                _ang.Set(angcz.Heading, 0, 0);
            }
            catch (Exception ex)
            {
            } 
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this._renderBox != null)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(this._renderBox.Guid);
                this._renderBox = null;
            }
            if (this._renderPolygon != null)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(this._renderPolygon.Guid);
                this._renderPolygon = null;
            }
        }

        private void btnDrawSection_Click(object sender, EventArgs e)
        {
            Clear();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Line);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private string _fileName;

        private void btnOutput_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;

            if (this._renderPolygon == null)
            {
                XtraMessageBox.Show("请绘制立面", "提示");
                return;
            }
            uint width = (uint)((_env.MaxX - _env.MinX) / 10 * (double.Parse(this.spResolution.Value.ToString())));
            uint height = (uint)((_env.MaxY - _env.MinY) / 10 * (double.Parse(this.spResolution.Value.ToString())));
            if ((width > 65535) || (height > 65535))
            {
                XtraMessageBox.Show("出图尺寸太大,请改变分辨率或者立面尺寸", "提示");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog
                {
                    RestoreDirectory = true,
                    Filter = "JPEG (*.JPG)|*.JPG|PNG (*.PNG)|*.PNG|BMP (*.BMP)|*.BMP"
                };
            if (DialogResult.OK == dialog.ShowDialog())
            {
                this._fileName = dialog.FileName;

                HideBox();

                try
                {
                    uint backColor = (uint)this.cpeBackGround.Color.ToArgb();
                    bool bExport = app.Current3DMapControl.ExportManager.ExportOrthoImage(this._fileName, width, _center, _ang, _env, true, backColor);
                    if (!bExport)
                    {
                        ShowBox();
                        XtraMessageBox.Show("导出失败！", "提示");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void HideBox()
        {
            if (this._renderBox != null)
            {
                this._renderBox.VisibleMask = gviViewportMask.gviViewNone;
            }
            if (this._renderPolygon != null)
            {
                this._renderPolygon.VisibleMask = gviViewportMask.gviViewNone;
            }
        }

        private void ShowBox()
        {
            if (this._renderBox != null)
            {
                this._renderBox.VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
            if (this._renderPolygon != null)
            {
                this._renderPolygon.VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
        }

        private void Current3DMapControl_RcPictureExportEnd(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEvent e)
        {
            WaitForm.SetCaption("完成导出", "请稍后");
            ShowBox();
            WaitForm.Stop();
            if (this.ceShowImage.Checked) ShowImage();
            else XtraMessageBox.Show("导出成功！", "提示");
        }

        private void Current3DMapControl_RcPictureExporting(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportingEvent e)
        {
            WaitForm.SetCaption("正在导出...", "请稍后");
        }

        private void Current3DMapControl_RcPictureExportBegin(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportBeginEvent e)
        {
            WaitForm.Start("正在导出...", "请稍后");
        }

        private void seMaxHeight_EditValueChanged(object sender, EventArgs e)
        {
            DrawBox();
        }

        private void seMinHeight_EditValueChanged(object sender, EventArgs e)
        {
            DrawBox();
        }

        private void tbcClipDis_EditValueChanged(object sender, EventArgs e)
        {
            DrawBox();
        }

        private void UCStreetVerticalSection_Load(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.RcPictureExportBegin += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportBeginEventHandler(Current3DMapControl_RcPictureExportBegin);
            app.Current3DMapControl.RcPictureExporting += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportingEventHandler(Current3DMapControl_RcPictureExporting);
            app.Current3DMapControl.RcPictureExportEnd += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcPictureExportEndEventHandler(Current3DMapControl_RcPictureExportEnd);
        }

        private void ShowImage()
        {
            if (File.Exists(this._fileName))
            {
                try
                {
                    Process.Start("\"" + this._fileName + "\"");
                }
                catch
                {
                    try
                    {
                        Process.Start("Mspaint.exe", "\"" + this._fileName + "\"");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
