using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace DF2DAnalysis.UC
{
    public partial class UCEarthworkCalculation : UserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.RadioGroup rg_digWay;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.SpinEdit se_height;
        private DevExpress.XtraEditors.SpinEdit se_depth;
        private DevExpress.XtraEditors.RadioGroup rg_pointType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraEditors.SimpleButton btn_polygon;
        private DevExpress.XtraEditors.SimpleButton btn_rectangle;
        private DevExpress.XtraEditors.SimpleButton btn_Circle;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    
        public UCEarthworkCalculation()
        {
            InitializeComponent();
            this.rg_digWay.SelectedIndex = 1;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCEarthworkCalculation));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_polygon = new DevExpress.XtraEditors.SimpleButton();
            this.btn_rectangle = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Circle = new DevExpress.XtraEditors.SimpleButton();
            this.se_height = new DevExpress.XtraEditors.SpinEdit();
            this.se_depth = new DevExpress.XtraEditors.SpinEdit();
            this.rg_pointType = new DevExpress.XtraEditors.RadioGroup();
            this.rg_digWay = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.se_height.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.se_depth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_pointType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_digWay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl4);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btn_polygon);
            this.layoutControl1.Controls.Add(this.btn_rectangle);
            this.layoutControl1.Controls.Add(this.btn_Circle);
            this.layoutControl1.Controls.Add(this.se_height);
            this.layoutControl1.Controls.Add(this.se_depth);
            this.layoutControl1.Controls.Add(this.rg_pointType);
            this.layoutControl1.Controls.Add(this.rg_digWay);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(294, 351);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 308);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.StyleController = this.layoutControl1;
            this.labelControl4.TabIndex = 14;
            this.labelControl4.Text = "填方量：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 290);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "挖方量：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 272);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "投影面积：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 254);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "地表面积：";
            // 
            // btn_polygon
            // 
            this.btn_polygon.Image = ((System.Drawing.Image)(resources.GetObject("btn_polygon.Image")));
            this.btn_polygon.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_polygon.Location = new System.Drawing.Point(200, 194);
            this.btn_polygon.Name = "btn_polygon";
            this.btn_polygon.Size = new System.Drawing.Size(89, 30);
            this.btn_polygon.StyleController = this.layoutControl1;
            this.btn_polygon.TabIndex = 10;
            this.btn_polygon.Text = "多边形";
            this.btn_polygon.Click += new System.EventHandler(this.btn_polygon_Click);
            // 
            // btn_rectangle
            // 
            this.btn_rectangle.Image = ((System.Drawing.Image)(resources.GetObject("btn_rectangle.Image")));
            this.btn_rectangle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_rectangle.Location = new System.Drawing.Point(100, 194);
            this.btn_rectangle.Name = "btn_rectangle";
            this.btn_rectangle.Size = new System.Drawing.Size(96, 30);
            this.btn_rectangle.StyleController = this.layoutControl1;
            this.btn_rectangle.TabIndex = 9;
            this.btn_rectangle.Text = "矩形";
            this.btn_rectangle.Click += new System.EventHandler(this.btn_rectangle_Click);
            // 
            // btn_Circle
            // 
            this.btn_Circle.Image = ((System.Drawing.Image)(resources.GetObject("btn_Circle.Image")));
            this.btn_Circle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Circle.Location = new System.Drawing.Point(5, 194);
            this.btn_Circle.Name = "btn_Circle";
            this.btn_Circle.Size = new System.Drawing.Size(91, 30);
            this.btn_Circle.StyleController = this.layoutControl1;
            this.btn_Circle.TabIndex = 8;
            this.btn_Circle.Text = "圆形";
            this.btn_Circle.Click += new System.EventHandler(this.btn_Circle_Click);
            // 
            // se_height
            // 
            this.se_height.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.se_height.Location = new System.Drawing.Point(80, 142);
            this.se_height.Name = "se_height";
            this.se_height.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.se_height.Size = new System.Drawing.Size(209, 22);
            this.se_height.StyleController = this.layoutControl1;
            this.se_height.TabIndex = 7;
            // 
            // se_depth
            // 
            this.se_depth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.se_depth.Location = new System.Drawing.Point(80, 116);
            this.se_depth.Name = "se_depth";
            this.se_depth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.se_depth.Size = new System.Drawing.Size(209, 22);
            this.se_depth.StyleController = this.layoutControl1;
            this.se_depth.TabIndex = 6;
            // 
            // rg_pointType
            // 
            this.rg_pointType.Location = new System.Drawing.Point(5, 81);
            this.rg_pointType.Name = "rg_pointType";
            this.rg_pointType.Properties.Columns = 3;
            this.rg_pointType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "边缘最低点"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "区域中心点"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "区域最低点")});
            this.rg_pointType.Size = new System.Drawing.Size(284, 31);
            this.rg_pointType.StyleController = this.layoutControl1;
            this.rg_pointType.TabIndex = 5;
            this.rg_pointType.SelectedIndexChanged += new System.EventHandler(this.rg_pointType_SelectedIndexChanged);
            // 
            // rg_digWay
            // 
            this.rg_digWay.Location = new System.Drawing.Point(5, 25);
            this.rg_digWay.Name = "rg_digWay";
            this.rg_digWay.Properties.Columns = 2;
            this.rg_digWay.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "参考点+开挖深度"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "绝对参考高度")});
            this.rg_digWay.Size = new System.Drawing.Size(284, 26);
            this.rg_digWay.StyleController = this.layoutControl1;
            this.rg_digWay.TabIndex = 4;
            this.rg_digWay.SelectedIndexChanged += new System.EventHandler(this.rg_digWay_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(294, 351);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(294, 56);
            this.layoutControlGroup2.Text = "开挖方式";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rg_digWay;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(288, 30);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 56);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(294, 113);
            this.layoutControlGroup3.Text = "参数设置";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.rg_pointType;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(288, 35);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.se_depth;
            this.layoutControlItem3.CustomizationFormText = "开挖深度(m):";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(288, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(288, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(288, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "开挖深度(m):";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.se_height;
            this.layoutControlItem4.CustomizationFormText = "参考高度(m):";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 61);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(288, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(288, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(288, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "参考高度(m):";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 169);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(294, 60);
            this.layoutControlGroup4.Text = "区域形状";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Circle;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(95, 34);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_rectangle;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(95, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(100, 34);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_polygon;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(195, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(93, 34);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "计算结果";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 229);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(294, 122);
            this.layoutControlGroup5.Text = "计算结果";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(288, 24);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(288, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(288, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(288, 18);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.labelControl2;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(288, 18);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl3;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(288, 18);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.labelControl4;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(288, 18);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // UCEarthworkCalculation
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEarthworkCalculation";
            this.Size = new System.Drawing.Size(294, 351);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.se_height.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.se_depth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_pointType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_digWay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);

        }

        public int DrawShape
        {
            get { return this.drawShape; }
        }

        public double H
        {
            get
            {
                return Convert.ToDouble(this.se_height.Value);
            }
        }
        public double Depth
        {
            get { return Convert.ToDouble(this.se_depth.Value); }
        }
        public int PointType
        {
            get
            {
                return this.pointType;
            }
        }
        public string SurfaceArea
        {
            get
            {
                return this.labelControl1.Text;
            }
            set
            {
                this.labelControl1.Text = value;
            }
        }

        public string ProjectArea
        {
            get { return this.labelControl2.Text; }
            set { this.labelControl2.Text = value; }
        }
        public string Dig
        {
            get { return this.labelControl3.Text; }
            set { this.labelControl3.Text = value; }
        }
        public string Fill
        {
            get { return this.labelControl4.Text; }
            set { this.labelControl4.Text = value; }
        }

        private int digWay = 1;
        private int pointType = 1;
        private int depth;
        private int height;
        private int drawShape = 1;
        private void rg_digWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_digWay.SelectedIndex == 0)
            {
                digWay = 1;
                se_height.Enabled = false;
                rg_pointType.Enabled = true;
                se_depth.Enabled = true;
            }
            else
            {
                digWay = 2; 
                rg_pointType.Enabled = false;
                se_depth.Enabled = false;
                se_height.Enabled = true;
            }
                
        }

        private void rg_pointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_pointType.SelectedIndex == 0) pointType = 1;
            else if (rg_pointType.SelectedIndex == 1) pointType = 2;
            else pointType = 3;
        }

        private void btn_Circle_Click(object sender, EventArgs e)
        {
            this.drawShape = 1;
        }

        public void RestoreEnv()
        {
           
        }

        private void btn_rectangle_Click(object sender, EventArgs e)
        {
            this.drawShape = 2;
        }

        private void btn_polygon_Click(object sender, EventArgs e)
        {
            this.drawShape = 3;
        }

    }
}
