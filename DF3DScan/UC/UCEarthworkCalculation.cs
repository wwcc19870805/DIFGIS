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
using DF3DControl.Base;
using DF3DScan.Class;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using DFCommon.Class;
using DFWinForms.Class;
using DevExpress.Utils;

namespace DF3DScan.UC
{
    public class UCEarthworkCalculation : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private SpinEdit spHeight;
        private SpinEdit spDepth;
        private RadioGroup radioGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private CheckEdit ceFillRegion;
        private CheckEdit ceCutRegion;
        private SimpleButton btnPolygon;
        private SimpleButton btnRectangle;
        private SimpleButton btnCircle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private LabelControl lcInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;

        private AxRenderControl d3;
        public UCEarthworkCalculation()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) { this.Enabled = false; return; }
            d3 = app.Current3DMapControl;
            if (!d3.Terrain.IsRegistered)
            {
                XtraMessageBox.Show("无地形加载，不能进行开挖分析！", "提示");
                this.Enabled = false;
                return;
            }
            _cb.onProcessing = this.OnProcessing;
        }

        public double[] OnProcessing(gviTerrainAnalyseOperation op, double[] ptArray)
        {
            List<double> ret = new List<double>();
            if (ptArray != null)
            {
                for (int i = 0; i < ptArray.Length / 2; i++)
                {
                    double x = ptArray[2 * i];
                    double y = ptArray[2 * i + 1];
                    double z = d3.Terrain.GetElevation(x, y, gviGetElevationType.gviGetElevationFromDatabase);
                    ret.Add(z);
                }
            }
            return ret.ToArray();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCEarthworkCalculation));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lcInfo = new DevExpress.XtraEditors.LabelControl();
            this.ceFillRegion = new DevExpress.XtraEditors.CheckEdit();
            this.ceCutRegion = new DevExpress.XtraEditors.CheckEdit();
            this.btnPolygon = new DevExpress.XtraEditors.SimpleButton();
            this.btnRectangle = new DevExpress.XtraEditors.SimpleButton();
            this.btnCircle = new DevExpress.XtraEditors.SimpleButton();
            this.spHeight = new DevExpress.XtraEditors.SpinEdit();
            this.spDepth = new DevExpress.XtraEditors.SpinEdit();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceFillRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCutRegion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDepth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lcInfo);
            this.layoutControl1.Controls.Add(this.ceFillRegion);
            this.layoutControl1.Controls.Add(this.ceCutRegion);
            this.layoutControl1.Controls.Add(this.btnPolygon);
            this.layoutControl1.Controls.Add(this.btnRectangle);
            this.layoutControl1.Controls.Add(this.btnCircle);
            this.layoutControl1.Controls.Add(this.spHeight);
            this.layoutControl1.Controls.Add(this.spDepth);
            this.layoutControl1.Controls.Add(this.radioGroup2);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(232, 449);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lcInfo
            // 
            this.lcInfo.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lcInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lcInfo.Location = new System.Drawing.Point(14, 360);
            this.lcInfo.Name = "lcInfo";
            this.lcInfo.Size = new System.Drawing.Size(204, 12);
            this.lcInfo.StyleController = this.layoutControl1;
            this.lcInfo.TabIndex = 9;
            // 
            // ceFillRegion
            // 
            this.ceFillRegion.EditValue = true;
            this.ceFillRegion.Location = new System.Drawing.Point(2, 271);
            this.ceFillRegion.Name = "ceFillRegion";
            this.ceFillRegion.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ceFillRegion.Properties.Appearance.Options.UseForeColor = true;
            this.ceFillRegion.Properties.Caption = "显示填方区域(蓝色)";
            this.ceFillRegion.Size = new System.Drawing.Size(228, 19);
            this.ceFillRegion.StyleController = this.layoutControl1;
            this.ceFillRegion.TabIndex = 5;
            this.ceFillRegion.CheckedChanged += new System.EventHandler(this.ceFillRegion_CheckedChanged);
            // 
            // ceCutRegion
            // 
            this.ceCutRegion.EditValue = true;
            this.ceCutRegion.Location = new System.Drawing.Point(2, 248);
            this.ceCutRegion.Name = "ceCutRegion";
            this.ceCutRegion.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ceCutRegion.Properties.Appearance.Options.UseForeColor = true;
            this.ceCutRegion.Properties.Caption = "显示挖方区域(棕色)";
            this.ceCutRegion.Size = new System.Drawing.Size(228, 19);
            this.ceCutRegion.StyleController = this.layoutControl1;
            this.ceCutRegion.TabIndex = 4;
            this.ceCutRegion.CheckedChanged += new System.EventHandler(this.ceCutRegion_CheckedChanged);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnPolygon.Image")));
            this.btnPolygon.Location = new System.Drawing.Point(156, 294);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(74, 30);
            this.btnPolygon.StyleController = this.layoutControl1;
            this.btnPolygon.TabIndex = 8;
            this.btnPolygon.Text = "多边形";
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.Location = new System.Drawing.Point(80, 294);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(72, 30);
            this.btnRectangle.StyleController = this.layoutControl1;
            this.btnRectangle.TabIndex = 7;
            this.btnRectangle.Text = "矩形";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Location = new System.Drawing.Point(2, 294);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(74, 30);
            this.btnCircle.StyleController = this.layoutControl1;
            this.btnCircle.TabIndex = 6;
            this.btnCircle.Text = "圆形";
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // spHeight
            // 
            this.spHeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spHeight.Location = new System.Drawing.Point(97, 210);
            this.spHeight.Name = "spHeight";
            this.spHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spHeight.Size = new System.Drawing.Size(121, 22);
            this.spHeight.StyleController = this.layoutControl1;
            this.spHeight.TabIndex = 3;
            // 
            // spDepth
            // 
            this.spDepth.EditValue = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.spDepth.Location = new System.Drawing.Point(97, 184);
            this.spDepth.Name = "spDepth";
            this.spDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spDepth.Size = new System.Drawing.Size(121, 22);
            this.spDepth.StyleController = this.layoutControl1;
            this.spDepth.TabIndex = 2;
            // 
            // radioGroup2
            // 
            this.radioGroup2.EditValue = "0";
            this.radioGroup2.Location = new System.Drawing.Point(14, 125);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Columns = 1;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "区域中心点"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "边缘最低点"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "区域最低点")});
            this.radioGroup2.Size = new System.Drawing.Size(204, 55);
            this.radioGroup2.StyleController = this.layoutControl1;
            this.radioGroup2.TabIndex = 1;
            this.radioGroup2.SelectedIndexChanged += new System.EventHandler(this.radioGroup2_SelectedIndexChanged);
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "0";
            this.radioGroup1.Location = new System.Drawing.Point(14, 34);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 1;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "参考点+开挖深度"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "绝对参考高度")});
            this.radioGroup1.Size = new System.Drawing.Size(204, 43);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlGroup4,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(232, 449);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 386);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(232, 39);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 425);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(232, 24);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "参数设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 91);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(232, 155);
            this.layoutControlGroup2.Text = "参数设置";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.radioGroup2;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(208, 59);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spDepth;
            this.layoutControlItem3.CustomizationFormText = "开挖深度：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 59);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(208, 26);
            this.layoutControlItem3.Text = "开挖深度(m)：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spHeight;
            this.layoutControlItem4.CustomizationFormText = "参考高度(m)：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 85);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(208, 26);
            this.layoutControlItem4.Text = "参考高度(m)：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(80, 14);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "开挖方式";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(232, 91);
            this.layoutControlGroup3.Text = "开挖方式";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(208, 47);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCircle;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 292);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(78, 34);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnRectangle;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(78, 292);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(76, 34);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnPolygon;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(154, 292);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(78, 34);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ceCutRegion;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 246);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(232, 23);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "计算结果：";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 326);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(232, 60);
            this.layoutControlGroup4.Text = "计算结果：";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lcInfo;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(208, 16);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.ceFillRegion;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 269);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(232, 23);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // UCEarthworkCalculation
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEarthworkCalculation";
            this.Size = new System.Drawing.Size(232, 449);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceFillRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCutRegion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDepth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        private DrawTool _drawTool;
        private ILabel _label;
        private IRenderMultiPolygon _rCutPolygon;
        private IRenderMultiPolygon _rFillPolygon;
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
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ceCutRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (this._rCutPolygon != null)
            {
                if (this.ceCutRegion.Checked) { this._rCutPolygon.VisibleMask = gviViewportMask.gviViewAllNormalView; }
                else { this._rCutPolygon.VisibleMask = gviViewportMask.gviViewNone; }
            }
        }

        private void ceFillRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (this._rFillPolygon != null)
            {
                if (this.ceFillRegion.Checked) { this._rFillPolygon.VisibleMask = gviViewportMask.gviViewAllNormalView; }
                else { this._rFillPolygon.VisibleMask = gviViewportMask.gviViewNone; }
            }
        }

        private void Clear()
        {
            if (_label != null)
            {
                d3.ObjectManager.DeleteObject(_label.Guid);
                _label = null;
            }
            if (_rCutPolygon != null)
            {
                d3.ObjectManager.DeleteObject(_rCutPolygon.Guid);
                _rCutPolygon = null;
            }
            if (_rFillPolygon != null)
            {
                d3.ObjectManager.DeleteObject(_rFillPolygon.Guid);
                _rFillPolygon = null;
            }
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
            if (this._drawTool != null && this._drawTool.GetGeo() != null)
            {
                switch (this._drawTool.GeoType)
                {
                    case DrawType.Circle:
                        break;
                    case DrawType.Rectangle:
                        break;
                    case DrawType.Polygon:
                        break;
                    default:
                        return;
                }
                WaitForm.Start("正在计算...", "请稍后");
                try
                {
                    IGeometry geo = this._drawTool.GetGeo().Clone2(gviVertexAttribute.gviVertexAttributeNone);
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        double minHeight = GetMinHeightInRegion(geo as IPolygon);
                        _label = d3.ObjectManager.CreateLabel(d3.ProjectTree.RootID);
                        IPoint pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        pt.X = (geo as IPolygon).Centroid.X;
                        pt.Y = (geo as IPolygon).Centroid.Y;
                        pt.Z = minHeight;
                        _label.Position = pt;
                        _label.Text = "正在计算，请稍后...";
                        TextSymbol ts = new TextSymbol();
                        TextAttribute ta = new TextAttribute();
                        ta.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                        ta.TextSize = SystemInfo.Instance.TextSize;
                        ts.TextAttribute = ta;
                        _label.TextSymbol = ts;
                        if (minHeight != double.NaN) Calc(geo as IPolygon, minHeight - Convert.ToDouble(this.spDepth.Value));
                    }
                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        _label = d3.ObjectManager.CreateLabel(d3.ProjectTree.RootID);
                        IPoint pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        pt.X = (geo as IPolygon).Centroid.X;
                        pt.Y = (geo as IPolygon).Centroid.Y;
                        pt.Z = (geo as IPolygon).Centroid.Z;
                        _label.Position = pt;
                        _label.Text = "正在计算，请稍后...";
                        TextSymbol ts = new TextSymbol();
                        TextAttribute ta = new TextAttribute();
                        ta.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                        ta.TextSize = SystemInfo.Instance.TextSize;
                        ts.TextAttribute = ta;
                        _label.TextSymbol = ts;
                        Calc(geo as IPolygon, Convert.ToDouble(this.spHeight.Value));
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    WaitForm.Stop();
                }
            }
        }

        private double GetMinHeightInRegion(IPolygon region)
        {
            if (region == null) return double.NaN;
            double hMin = 999999.99;
            if (this.radioGroup2.SelectedIndex == 0)
            {
                hMin = d3.Terrain.GetElevation(region.Centroid.X, region.Centroid.Y, gviGetElevationType.gviGetElevationFromDatabase);
            }
            else if (this.radioGroup2.SelectedIndex == 1)
            {
                for (var i = 0; i < region.ExteriorRing.PointCount; i++)
                {
                    IPoint pointValue = region.ExteriorRing.GetPoint(i);
                    double num3 = d3.Terrain.GetElevation(pointValue.X, pointValue.Y, gviGetElevationType.gviGetElevationFromDatabase);
                    if (num3 < hMin)
                    {
                        hMin = num3;
                    }
                }
            }
            else if (this.radioGroup2.SelectedIndex == 2)
            {
                IPolygon polygon = region.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolygon;
                IEnvelope envelope = polygon.Envelope;

                // 采样点数量 可根据精度修改
                int pickCount = 10000;

                int step = (int)Math.Sqrt(envelope.Width * envelope.Height / pickCount);
                double xStart = envelope.MinX;
                double yStart = envelope.MinY;

                while (xStart <= envelope.MaxX)
                {
                    yStart = envelope.MinY;
                    while (yStart <= envelope.MaxY)
                    {
                        double elevatation = d3.Terrain.GetElevation(xStart, yStart, gviGetElevationType.gviGetElevationFromDatabase);
                        IPoint point =(new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeNone);
                        IVector3 vector = new Vector3();
                        vector.Set(xStart, yStart, 0);
                        point.Position = vector;
                        bool pointOnSurface = polygon.IsPointOnSurface(point);
                        if (hMin > elevatation && pointOnSurface)
                        {
                            hMin = elevatation;
                        }
                        yStart += step;
                    }
                    xStart += step;
                }
            }
            return hMin;
        }

        private TerrainAnalyseCallBack _cb = new TerrainAnalyseCallBack();
        private void Calc(IPolygon polygon, double referenceHeight)
        {
            if (polygon == null) return;
            polygon.Close();
            IGeometryFactory geoFact = new GeometryFactoryClass();
            IMultiPolygon cutPolygon = geoFact.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            IMultiPolygon fillPolygon = geoFact.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            double cutVolumn = 0.0;
            double fillVolumn = 0.0;
            ITerrainAnalyse ta = new TerrainAnalyse();
            ta.OnProcessing = _cb;
            ta.CalculateCutFill(polygon, 0.0, referenceHeight, ref cutPolygon, ref fillPolygon, ref cutVolumn, ref fillVolumn);

            //double surfaceArea = ta.GetSurfaceArea(polygon, 0.0);
            //string res = "挖  方  量：" + cutVolumn.ToString("0.000") + " 立方米\r\n填  方  量：" + fillVolumn.ToString("0.000") + " 立方米\r\n" +
            //    "投影面积：" + polygon.Area().ToString("0.000") + " 平方米\r\n地表面积：" + surfaceArea.ToString("0.000") + " 平方米\r\n";
            string res = "挖方量：" + cutVolumn.ToString("0.000") + " 立方米\r\n填方量：" + fillVolumn.ToString("0.000") + " 立方米";
            if (_label != null) _label.Text = res;
            this.lcInfo.Text = res;

            if (cutPolygon != null)
            {
                SurfaceSymbol ss = new SurfaceSymbol();
                ss.Color = 0xffc04000;
                _rCutPolygon = d3.ObjectManager.CreateRenderMultiPolygon(cutPolygon, ss, d3.ProjectTree.RootID);
                _rCutPolygon.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                if (this.ceCutRegion.Checked)
                {
                    _rCutPolygon.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
                else
                {
                    _rCutPolygon.VisibleMask = gviViewportMask.gviViewNone;
                }
            }
            if (fillPolygon != null)
            {
                SurfaceSymbol ss = new SurfaceSymbol();
                ss.Color = 0xff0000c0;
                _rFillPolygon = d3.ObjectManager.CreateRenderMultiPolygon(fillPolygon, ss, d3.ProjectTree.RootID);
                _rFillPolygon.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                if (this.ceFillRegion.Checked)
                {
                    _rFillPolygon.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
                else
                {
                    _rFillPolygon.VisibleMask = gviViewportMask.gviViewNone;
                }
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
    }
}
