using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DF2DPipe.Stats.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using DFWinForms.Class;
using DF2DData.Class;
using DFDataConfig.Class;
using ESRI.ArcGIS.Display;
using DF2DControl.Base;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmSeparationStats2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTreeList.TreeList treelist;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DropDownButton btnSpatialArea;
        private SimpleButton btnLengthStatsBySepara;
        private SimpleButton btnDelrow;
        private SimpleButton btnAddrow;
        private SimpleButton btnDownlimit;
        private SimpleButton btnUplimit;
        private ListBoxControl lbxValueSepara;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private PanelControl panelControl1;
        private PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private LabelControl labelControl1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        string _sysFieldName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private IContainer components;
        private ListBoxControl lbxStatslimits;
        private TextEdit teDownlimit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private ButtonEdit teUplimit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        List<string> _sysFieldNames;
        bool isDiameter;
        private IMapControlEvents2_Event mapControlEvents = null;
        DF2DApplication app;
        public DataTable DTTemp
        {
            get { return this.dttemp; }
        }

        public DataTable DTStats
        {
            get { return this.dtstats; }
        }


        public FrmSeparationStats2D()
        {
            InitializeComponent();
        }

        public FrmSeparationStats2D(List<string> sysFieldNames)
        {
            _sysFieldNames = new List<string>();
            _sysFieldNames = sysFieldNames;
            this._sysFieldName = sysFieldNames[0];
            if (sysFieldNames.Count == 1) isDiameter = true;
            app = DF2DApplication.Application;
            InitializeComponent();
            mapControlEvents = app.Current2DMapControl as IMapControlEvents2_Event;
            mapControlEvents.OnMouseDown += new IMapControlEvents2_OnMouseDownEventHandler(mapControlEvents_OnMouseDown);
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeparationStats2D));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lbxStatslimits = new DevExpress.XtraEditors.ListBoxControl();
            this.teDownlimit = new DevExpress.XtraEditors.TextEdit();
            this.btnSpatialArea = new DevExpress.XtraEditors.DropDownButton();
            this.btnLengthStatsBySepara = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelrow = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddrow = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownlimit = new DevExpress.XtraEditors.SimpleButton();
            this.btnUplimit = new DevExpress.XtraEditors.SimpleButton();
            this.lbxValueSepara = new DevExpress.XtraEditors.ListBoxControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.treelist = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.teUplimit = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbxStatslimits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDownlimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbxValueSepara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUplimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lbxStatslimits);
            this.layoutControl1.Controls.Add(this.teDownlimit);
            this.layoutControl1.Controls.Add(this.btnSpatialArea);
            this.layoutControl1.Controls.Add(this.btnLengthStatsBySepara);
            this.layoutControl1.Controls.Add(this.btnDelrow);
            this.layoutControl1.Controls.Add(this.btnAddrow);
            this.layoutControl1.Controls.Add(this.btnDownlimit);
            this.layoutControl1.Controls.Add(this.btnUplimit);
            this.layoutControl1.Controls.Add(this.lbxValueSepara);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.treelist);
            this.layoutControl1.Controls.Add(this.teUplimit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1110, 196, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(743, 437);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lbxStatslimits
            // 
            this.lbxStatslimits.Location = new System.Drawing.Point(509, 110);
            this.lbxStatslimits.Name = "lbxStatslimits";
            this.lbxStatslimits.Size = new System.Drawing.Size(216, 281);
            this.lbxStatslimits.StyleController = this.layoutControl1;
            this.lbxStatslimits.TabIndex = 26;
            // 
            // teDownlimit
            // 
            this.teDownlimit.Location = new System.Drawing.Point(548, 84);
            this.teDownlimit.Name = "teDownlimit";
            this.teDownlimit.Size = new System.Drawing.Size(177, 22);
            this.teDownlimit.StyleController = this.layoutControl1;
            this.teDownlimit.TabIndex = 24;
            // 
            // btnSpatialArea
            // 
            this.btnSpatialArea.Location = new System.Drawing.Point(595, 398);
            this.btnSpatialArea.Name = "btnSpatialArea";
            this.btnSpatialArea.Size = new System.Drawing.Size(128, 24);
            this.btnSpatialArea.StyleController = this.layoutControl1;
            this.btnSpatialArea.TabIndex = 21;
            this.btnSpatialArea.Text = "范围统计\r\n";
            this.btnSpatialArea.Click += new System.EventHandler(this.btnSpatialArea_Click);
            // 
            // btnLengthStatsBySepara
            // 
            this.btnLengthStatsBySepara.Location = new System.Drawing.Point(454, 398);
            this.btnLengthStatsBySepara.Name = "btnLengthStatsBySepara";
            this.btnLengthStatsBySepara.Size = new System.Drawing.Size(137, 24);
            this.btnLengthStatsBySepara.StyleController = this.layoutControl1;
            this.btnLengthStatsBySepara.TabIndex = 20;
            this.btnLengthStatsBySepara.Text = "分段长度统计";
            this.btnLengthStatsBySepara.Click += new System.EventHandler(this.btnLengthStatsBySepara_Click);
            // 
            // btnDelrow
            // 
            this.btnDelrow.Location = new System.Drawing.Point(356, 303);
            this.btnDelrow.Name = "btnDelrow";
            this.btnDelrow.Size = new System.Drawing.Size(143, 22);
            this.btnDelrow.StyleController = this.layoutControl1;
            this.btnDelrow.TabIndex = 19;
            this.btnDelrow.Text = "删除行";
            this.btnDelrow.Click += new System.EventHandler(this.btnDelrow_Click);
            // 
            // btnAddrow
            // 
            this.btnAddrow.Location = new System.Drawing.Point(356, 225);
            this.btnAddrow.Name = "btnAddrow";
            this.btnAddrow.Size = new System.Drawing.Size(143, 22);
            this.btnAddrow.StyleController = this.layoutControl1;
            this.btnAddrow.TabIndex = 18;
            this.btnAddrow.Text = "增加行";
            this.btnAddrow.Click += new System.EventHandler(this.btnAddrow_Click);
            // 
            // btnDownlimit
            // 
            this.btnDownlimit.Location = new System.Drawing.Point(356, 156);
            this.btnDownlimit.Name = "btnDownlimit";
            this.btnDownlimit.Size = new System.Drawing.Size(143, 22);
            this.btnDownlimit.StyleController = this.layoutControl1;
            this.btnDownlimit.TabIndex = 17;
            this.btnDownlimit.Text = "下限>>";
            this.btnDownlimit.Click += new System.EventHandler(this.btnDownlimit_Click);
            // 
            // btnUplimit
            // 
            this.btnUplimit.Location = new System.Drawing.Point(356, 86);
            this.btnUplimit.Name = "btnUplimit";
            this.btnUplimit.Size = new System.Drawing.Size(143, 22);
            this.btnUplimit.StyleController = this.layoutControl1;
            this.btnUplimit.TabIndex = 16;
            this.btnUplimit.Text = "上限>>";
            this.btnUplimit.Click += new System.EventHandler(this.btnUplimit_Click);
            // 
            // lbxValueSepara
            // 
            this.lbxValueSepara.Location = new System.Drawing.Point(210, 58);
            this.lbxValueSepara.Name = "lbxValueSepara";
            this.lbxValueSepara.Size = new System.Drawing.Size(136, 333);
            this.lbxValueSepara.StyleController = this.layoutControl1;
            this.lbxValueSepara.TabIndex = 14;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(188, 107);
            this.panelControl1.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(93, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "分段统计";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(28, 23);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(59, 52);
            this.pictureEdit1.TabIndex = 0;
            // 
            // treelist
            // 
            this.treelist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treelist.Location = new System.Drawing.Point(15, 146);
            this.treelist.Name = "treelist";
            this.treelist.OptionsView.ShowCheckBoxes = true;
            this.treelist.Size = new System.Drawing.Size(182, 276);
            this.treelist.StateImageList = this.imageCollection1;
            this.treelist.TabIndex = 4;
            this.treelist.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treelist_AfterCheckNode);
            // 
            // NodeName
            // 
            this.NodeName.Caption = "名称";
            this.NodeName.FieldName = "NodeName";
            this.NodeName.MinWidth = 49;
            this.NodeName.Name = "NodeName";
            this.NodeName.OptionsColumn.AllowEdit = false;
            this.NodeName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.NodeName.Visible = true;
            this.NodeName.VisibleIndex = 0;
            // 
            // NodeObject
            // 
            this.NodeObject.Caption = "对象";
            this.NodeObject.FieldName = "NodeObject";
            this.NodeObject.Name = "NodeObject";
            this.NodeObject.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Object;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Group.png");
            this.imageCollection1.Images.SetKeyName(1, "Database.png");
            this.imageCollection1.Images.SetKeyName(2, "FeatureLayer_point.png");
            this.imageCollection1.Images.SetKeyName(3, "FeatureLayer_line.png");
            this.imageCollection1.Images.SetKeyName(4, "FeatureLayer_polygon.png");
            this.imageCollection1.Images.SetKeyName(5, "3DTileLayer.png");
            this.imageCollection1.Images.SetKeyName(6, "Dataset.png");
            this.imageCollection1.Images.SetKeyName(7, "FeatureLayer_model.png");
            this.imageCollection1.Images.SetKeyName(8, "Label_image.png");
            this.imageCollection1.Images.SetKeyName(9, "Label_text.png");
            this.imageCollection1.Images.SetKeyName(10, "Object.png");
            this.imageCollection1.Images.SetKeyName(11, "TerrainLayer.png");
            // 
            // teUplimit
            // 
            this.teUplimit.Location = new System.Drawing.Point(548, 58);
            this.teUplimit.Name = "teUplimit";
            this.teUplimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.teUplimit.Size = new System.Drawing.Size(177, 22);
            this.teUplimit.StyleController = this.layoutControl1;
            this.teUplimit.TabIndex = 25;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(743, 437);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 111);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(192, 306);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treelist;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(186, 280);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(186, 280);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(186, 280);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.emptySpaceItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(192, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(531, 417);
            this.layoutControlGroup3.Text = "参数选择";
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(146, 363);
            this.layoutControlGroup4.Text = "参数范围";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lbxValueSepara;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(140, 337);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem3});
            this.layoutControlGroup5.Location = new System.Drawing.Point(299, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(226, 363);
            this.layoutControlGroup5.Text = "统计范围";
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.teUplimit;
            this.layoutControlItem13.CustomizationFormText = "Uplimit";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem13.Text = "上限：";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.lbxStatslimits;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(220, 285);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teDownlimit;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem3.Text = "下限：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "layoutControlGroup6";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem1,
            this.emptySpaceItem5,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.emptySpaceItem4});
            this.layoutControlGroup6.Location = new System.Drawing.Point(146, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(153, 363);
            this.layoutControlGroup6.Text = "命令行";
            this.layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnUplimit;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnDownlimit;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 118);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnAddrow;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 187);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnDelrow;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 265);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(147, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 291);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(147, 66);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(147, 66);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(147, 66);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(147, 48);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(147, 48);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(147, 48);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 74);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(147, 44);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(147, 44);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(147, 44);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 144);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(147, 43);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(147, 43);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(147, 43);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 213);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(147, 52);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(147, 52);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(147, 52);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnLengthStatsBySepara;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(247, 363);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(141, 28);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(141, 28);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(141, 28);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnSpatialArea;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(388, 363);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(132, 28);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(132, 28);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(137, 28);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 363);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(247, 26);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(247, 26);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(247, 28);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.panelControl1;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(192, 111);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // FrmSeparationStats2D
            // 
            this.ClientSize = new System.Drawing.Size(743, 437);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSeparationStats2D";
            this.Text = "分段统计";
            this.Load += new System.EventHandler(this.FrmSeparationStats2D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbxStatslimits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDownlimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbxValueSepara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUplimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        DataTable dttemp;//声明统计用数据表
        DataTable dtstats;//生命统计图表用数据表
        private void RecursiveBuildTree(TreeListNode parentNode, List<LogicGroup> listLG)
        {
            foreach (LogicGroup lg in listLG)
            {
                TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, parentNode);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible2D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 0;
                    }
                }
            }
        }

        private void BuildTree()
        {
            foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
            {
                TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, null);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible2D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 0;
                    }
                }
            }

            foreach (MajorClass mc in LogicDataStructureManage2D.Instance.RootMajorClasses)
            {
                TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, null);
                mcnode.StateImageIndex = 0;
                foreach (SubClass sc in mc.SubClasses)
                {
                    if (!sc.Visible2D) continue;
                    TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                    scnode.StateImageIndex = 0;
                }
            }
        }

        private void FrmSeparationStats2D_Load(object sender, EventArgs e)
        {
            BuildTree();

        }

        private void treelist_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            this.lbxValueSepara.Items.Clear();//清空分段数据选择列
            IFeatureCursor pfeatureCursor = null;
            IFeature pfeature = null;
            try
            {
                WaitForm.Start("正在加载列表...", "请稍后");
                HashSet<int> list = new HashSet<int>();
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())//获得所有选择的树节点
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;//获得当前二级子类
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');//获得当前二级子类的父类对应的要素类ID数组
                        if (arrFc2DId == null) continue;
                        foreach (string fc2DId in arrFc2DId)//对要素类ID数组进行遍历
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID获得DF2DFC
                            if (dffc == null) continue;
                            FacilityClass facClass = dffc.GetFacilityClass();//获得设施类
                            IFeatureClass fc = dffc.GetFeatureClass();//获得要素类
                            if (fc == null || facClass == null || facClass.Name != "PipeLine") continue;
                            if (this._sysFieldNames != null)
                            {
                                foreach (string dep in _sysFieldNames)
                                {
                                    DFDataConfig.Class.FieldInfo fi = facClass.GetFieldInfoBySystemName(dep);//根据当前字段名获得配置的字段
                                    if (fi == null) continue;

                                    IFields pFields = fc.Fields;//获得当前要素类字段集
                                    int index = pFields.FindField(fi.Name);//根据配置字段名获得要素类中对应字段的索引
                                    if (index < 0) continue;
                                    IField pField = pFields.get_Field(index);//根据索引获得要素类字段

                                    IQueryFilter pQueryFilter = new QueryFilterClass();//初始化过滤类
                                    pQueryFilter.SubFields = pField.Name;
                                    pQueryFilter.WhereClause = sc.Parent.ClassifyField + " = " + "'" + sc.Name + "'";

                                    pfeatureCursor = fc.Search(pQueryFilter, false);//获得过滤后要素的游标
                                    while ((pfeature = pfeatureCursor.NextFeature()) != null)
                                    {
                                        object temp = pfeature.get_Value(index);//获得当前要素对应字段的值
                                        if (temp == null) continue;
                                        if (temp.ToString().Contains("*"))//如果是矩形截面
                                        {
                                            int index1 = temp.ToString().IndexOf("*");
                                            temp = temp.ToString().Substring(0, index1);//获得矩形的长

                                        }
                                        string strtemp = "";
                                        int inttemp;
                                        switch (pField.Type)
                                        {
                                            case esriFieldType.esriFieldTypeDouble:
                                            case esriFieldType.esriFieldTypeInteger:
                                            case esriFieldType.esriFieldTypeOID:
                                            case esriFieldType.esriFieldTypeSingle:
                                            case esriFieldType.esriFieldTypeSmallInteger:
                                                strtemp = temp.ToString();
                                                break;
                                            case esriFieldType.esriFieldTypeDate:
                                            case esriFieldType.esriFieldTypeString:
                                            case esriFieldType.esriFieldTypeGUID:
                                                strtemp = temp.ToString();
                                                break;
                                            case esriFieldType.esriFieldTypeBlob:
                                            case esriFieldType.esriFieldTypeGeometry:
                                            case esriFieldType.esriFieldTypeGlobalID:
                                            case esriFieldType.esriFieldTypeRaster:
                                            case esriFieldType.esriFieldTypeXML:
                                            default:
                                                continue;
                                        }
                                        bool ok = Int32.TryParse(strtemp, out inttemp);
                                        if (ok)
                                            list.Add(inttemp);
                                    }
                                }
                            }

                        }
                    }

                }
                if (isDiameter)//如果是管径分段统计
                {
                    int min = list.Min(); ;
                    int max = list.Max();
                    while (min <= max)
                    {
                        this.lbxValueSepara.Items.Add(min);//为管径分段添加选择项
                        min += 200;
                    }
                    this.lbxValueSepara.Items.Add(max);
                }
                else//如果是埋深分段统计
                {
                    double min = list.Min();
                    double max = list.Max();
                    while (min <= max)
                    {
                        this.lbxValueSepara.Items.Add(min);
                        min += 0.5;
                    }
                    this.lbxValueSepara.Items.Add(max);
                }

            }
            catch { }
            finally
            {
                if (pfeatureCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pfeatureCursor);
                    pfeatureCursor = null;
                }
                if (pfeature != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pfeature);
                    pfeature = null;
                }
                WaitForm.Stop();
            }
        }


        private void btnUplimit_Click(object sender, EventArgs e)
        {

            this.teUplimit.Text = this.lbxValueSepara.SelectedItem.ToString();
        }

        private void btnAddrow_Click(object sender, EventArgs e)
        {
            if (isDiameter)
            {
                int down;
                int up;
                bool ok1 = Int32.TryParse(this.teDownlimit.Text, out down);
                bool ok2 = Int32.TryParse(this.teUplimit.Text, out up);
                if (ok1 && ok2 && down < up)
                {
                    string row = down.ToString() + "～ " + up.ToString();
                    this.lbxStatslimits.Items.Add(row);
                }
                else
                {
                    MessageBox.Show("参数输入有误，请重新输入");
                }
            }
            else
            {
                double down;
                double up;
                bool ok1 = Double.TryParse(this.teDownlimit.Text, out down);
                bool ok2 = Double.TryParse(this.teUplimit.Text, out up);
                if (ok1 && ok2 && down < up)
                {
                    string row = down.ToString() + "～ " + up.ToString();
                    this.lbxStatslimits.Items.Add(row);
                }
                else
                {
                    MessageBox.Show("参数输入有误，请重新输入");
                }
            }
        }

        private void btnDownlimit_Click(object sender, EventArgs e)
        {
            this.teDownlimit.Text = this.lbxValueSepara.SelectedItem.ToString();
        }

        private void btnDelrow_Click(object sender, EventArgs e)
        {
            if (this.lbxStatslimits.Items.Count > 0)
            {
                this.lbxStatslimits.Items.Remove(this.lbxStatslimits.SelectedItem);
            }
            else
            {
                MessageBox.Show("移除行失败");
            }
        }

        private void btnLengthStatsBySepara_Click(object sender, EventArgs e)
        {
            if (this.lbxStatslimits.Items.Count <= 0) MessageBox.Show("请添加行");
            if (isDiameter) DoStatsByDiameter();
            else DoStatsByDepth();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private struct pipe
        {
            public int length;
            public int width;
            public double pipelength;
            public double startdepth;
            public double enddepth;
        }
        private void DoStatsByDiameter()
        {
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});//为统计用数据表添加列
            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("LENGTH",typeof(double))});//为统计图表用数据表添加列

            pipe p;
            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        double subclasslength = 0.0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");
                            if (fi == null || fiPipeLength == null) continue;
                            int index = fc.Fields.FindField(fi.Name);
                            if (index == -1) continue;
                            int indexPipeLength = fc.Fields.FindField(fiPipeLength.Name);
                            if (indexPipeLength == -1) continue;
                            IField fcfi = fc.Fields.get_Field(index);
                            IQueryFilter filter = new QueryFilter();
                            //filter.SubFields = fiPipeLength.Name,fcfi.Name;                          
                            filter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";

                            IFeatureCursor pFeatureCursor = null;
                            IFeature pFeature = null;
                            bool bHave = false;
                            try
                            {
                                for (int i = 0; i < this.lbxStatslimits.Items.Count; i++)
                                {
                                    pFeatureCursor = fc.Search(filter, false);
                                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                    {
                                        object tempobj = pFeature.get_Value(index);
                                        if (tempobj.ToString().Contains("*"))
                                        {
                                            int n = tempobj.ToString().IndexOf("*");
                                            string str1 = tempobj.ToString().Substring(0, n);
                                            string str2 = tempobj.ToString().Substring(n + 1, tempobj.ToString().Length - n - 1);
                                            Int32.TryParse(str1, out p.length);
                                            Int32.TryParse(str2, out p.width);
                                        }
                                        else
                                        {
                                            Int32.TryParse(tempobj.ToString(), out p.length);
                                            Int32.TryParse(tempobj.ToString(), out p.width);
                                        }
                                        object templength = pFeature.get_Value(indexPipeLength);
                                        double.TryParse(templength.ToString(), out p.pipelength);

                                        string whereclause = this.lbxStatslimits.Items[i].ToString();
                                        int n1 = whereclause.IndexOf("～");
                                        //int n2 = whereclause.LastIndexOf("<");
                                        int down;
                                        int up;
                                        Int32.TryParse(whereclause.Substring(0, n1), out down);
                                        Int32.TryParse(whereclause.Substring(n1 + 1, whereclause.Length - n1 - 1), out up);
                                        if ((down <= p.length && p.length < up) || (down <= p.width && p.width < up))
                                        {
                                            subclasslength += p.pipelength;
                                        }
                                        if (subclasslength > 0.0) bHave = true;

                                    }
                                    if (bHave)
                                    {
                                        DataRow dr = dttemp.NewRow();
                                        dr["PIPELINETYPE"] = sc;
                                        dr["PVALUE"] = this.lbxStatslimits.Items[i].ToString();
                                        dr["LENGTH"] = subclasslength.ToString("0.00");
                                        dttemp.Rows.Add(dr);

                                        DataRow dr1 = dtstats.NewRow();
                                        dr1["PIPELINETYPE"] = sc;
                                        dr1["FIELDNAME"] = this.lbxStatslimits.Items[i].ToString();
                                        dr1["LENGTH"] = subclasslength.ToString("0.00");
                                        dtstats.Rows.Add(dr1);
                                        subclasslength = 0.0;
                                        bHave = false;
                                    }
                                }
                            }
                            catch { }
                            finally
                            {
                                if (pFeatureCursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                                    pFeatureCursor = null;
                                }
                                if (pFeature != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                                    pFeature = null;
                                }
                            }
                        }
                    }
                }
            }

        }

        private void DoStatsByDepth()
        {
            if (this._sysFieldNames == null) return;
            dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});
            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("LENGTH",typeof(double))});
            pipe p;
            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        double subclasslength = 0.0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi1 = facc.GetFieldInfoBySystemName(this._sysFieldNames[0]);
                            DFDataConfig.Class.FieldInfo fi2 = facc.GetFieldInfoBySystemName(this._sysFieldNames[1]);
                            DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");
                            if (fi1 == null || fi2 == null || fiPipeLength == null) continue;
                            int index1 = fc.Fields.FindField(fi1.Name);
                            int index2 = fc.Fields.FindField(fi2.Name);
                            if (index1 == -1 || index2 == -1) continue;
                            int indexPipeLength = fc.Fields.FindField(fiPipeLength.Name);
                            if (indexPipeLength == -1) continue;
                            IField fcfi1 = fc.Fields.get_Field(index1);
                            IField fcfi2 = fc.Fields.get_Field(index2);
                            IQueryFilter filter = new QueryFilter();
                            //filter.SubFields = fiPipeLength.Name,fcfi.Name;                          
                            filter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                            IFeatureCursor pFeatureCursor = null;
                            IFeature pFeature = null;
                            bool bHave = false;
                            try
                            {
                                for (int i = 0; i < this.lbxStatslimits.Items.Count; i++)
                                {
                                    pFeatureCursor = fc.Search(filter, false);
                                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                    {
                                        object tempstart = pFeature.get_Value(index1);
                                        object tempend = pFeature.get_Value(index2);

                                        Double.TryParse(tempstart.ToString(), out p.startdepth);
                                        Double.TryParse(tempend.ToString(), out p.enddepth);

                                        object templength = pFeature.get_Value(indexPipeLength);
                                        double.TryParse(templength.ToString(), out p.pipelength);

                                        string whereclause = this.lbxStatslimits.Items[i].ToString();
                                        int n1 = whereclause.IndexOf("～");
                                        //int n2 = whereclause.LastIndexOf("<");
                                        double down;
                                        double up;
                                        Double.TryParse(whereclause.Substring(0, n1), out down);
                                        Double.TryParse(whereclause.Substring(n1 + 1, whereclause.Length - n1 - 1), out up);

                                        if ((down <= p.startdepth && p.startdepth < up) && (down <= p.enddepth && p.enddepth < up))
                                        {
                                            subclasslength += p.pipelength;
                                        }
                                        if (subclasslength > 0.0) bHave = true;
                                    }
                                    if (bHave)
                                    {
                                        DataRow dr = dttemp.NewRow();
                                        dr["PIPELINETYPE"] = sc;
                                        dr["PVALUE"] = this.lbxStatslimits.Items[i].ToString();
                                        dr["LENGTH"] = subclasslength.ToString("0.00");
                                        dttemp.Rows.Add(dr);

                                        DataRow dr1 = dtstats.NewRow();
                                        dr1["PIPELINETYPE"] = sc;
                                        dr1["FIELDNAME"] = this.lbxStatslimits.Items[i].ToString();
                                        dr1["LENGTH"] = subclasslength.ToString("0.00");
                                        dtstats.Rows.Add(dr1);
                                        subclasslength = 0.0;
                                        bHave = false;
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            {
                            }
                            finally
                            {
                                if (pFeatureCursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                                    pFeatureCursor = null;
                                }
                                if (pFeature != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                                    pFeature = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        IGeometry geo = null;
        private void btnSpatialArea_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
        }

        private void DoGeoStatsByDiameter(IGeometry geo)
        {
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});//为统计用数据表添加列
            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("LENGTH",typeof(double))});//为统计图表用数据表添加列

            pipe p;
            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        double subclasslength = 0.0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");
                            if (fi == null || fiPipeLength == null) continue;
                            int index = fc.Fields.FindField(fi.Name);
                            if (index == -1) continue;
                            int indexPipeLength = fc.Fields.FindField(fiPipeLength.Name);
                            if (indexPipeLength == -1) continue;
                            IField fcfi = fc.Fields.get_Field(index);
                            ISpatialFilter filter = new SpatialFilter();
                            //filter.SubFields = fiPipeLength.Name,fcfi.Name;                          
                            filter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                            filter.Geometry = geo;
                            IFeatureCursor pFeatureCursor = null;
                            IFeature pFeature = null;
                            bool bHave = false;
                            try
                            {
                                for (int i = 0; i < this.lbxStatslimits.Items.Count; i++)
                                {
                                    pFeatureCursor = fc.Search(filter, false);
                                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                    {
                                        object tempobj = pFeature.get_Value(index);
                                        if (tempobj.ToString().Contains("*"))
                                        {
                                            int n = tempobj.ToString().IndexOf("*");
                                            string str1 = tempobj.ToString().Substring(0, n);
                                            string str2 = tempobj.ToString().Substring(n + 1, tempobj.ToString().Length - n - 1);
                                            Int32.TryParse(str1, out p.length);
                                            Int32.TryParse(str2, out p.width);
                                        }
                                        else
                                        {
                                            Int32.TryParse(tempobj.ToString(), out p.length);
                                            Int32.TryParse(tempobj.ToString(), out p.width);
                                        }
                                        object templength = pFeature.get_Value(indexPipeLength);
                                        double.TryParse(templength.ToString(), out p.pipelength);

                                        string whereclause = this.lbxStatslimits.Items[i].ToString();
                                        int n1 = whereclause.IndexOf("～");
                                        //int n2 = whereclause.LastIndexOf("<");
                                        int down;
                                        int up;
                                        Int32.TryParse(whereclause.Substring(0, n1), out down);
                                        Int32.TryParse(whereclause.Substring(n1 + 1, whereclause.Length - n1 - 1), out up);
                                        if ((down <= p.length && p.length < up) || (down <= p.width && p.width < up))
                                        {
                                            subclasslength += p.pipelength;
                                        }
                                        if (subclasslength > 0.0) bHave = true;

                                    }
                                    if (bHave)
                                    {
                                        DataRow dr = dttemp.NewRow();
                                        dr["PIPELINETYPE"] = sc;
                                        dr["PVALUE"] = this.lbxStatslimits.Items[i].ToString();
                                        dr["LENGTH"] = subclasslength.ToString("0.00");
                                        dttemp.Rows.Add(dr);

                                        DataRow dr1 = dtstats.NewRow();
                                        dr1["PIPELINETYPE"] = sc;
                                        dr1["FIELDNAME"] = this.lbxStatslimits.Items[i].ToString();
                                        dr1["LENGTH"] = subclasslength.ToString("0.00");
                                        dtstats.Rows.Add(dr1);
                                        subclasslength = 0.0;
                                        bHave = false;
                                    }
                                }
                            }
                            catch { }
                            finally
                            {
                                if (pFeatureCursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                                    pFeatureCursor = null;
                                }
                                if (pFeature != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                                    pFeature = null;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DoGeoStatsByDepth(IGeometry geo)
        {
            if (this._sysFieldNames == null) return;
            dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});
            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("LENGTH",typeof(double))});
            pipe p;
            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        double subclasslength = 0.0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi1 = facc.GetFieldInfoBySystemName(this._sysFieldNames[0]);
                            DFDataConfig.Class.FieldInfo fi2 = facc.GetFieldInfoBySystemName(this._sysFieldNames[1]);
                            DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");
                            if (fi1 == null || fi2 == null || fiPipeLength == null) continue;
                            int index1 = fc.Fields.FindField(fi1.Name);
                            int index2 = fc.Fields.FindField(fi2.Name);
                            if (index1 == -1 || index2 == -1) continue;
                            int indexPipeLength = fc.Fields.FindField(fiPipeLength.Name);
                            if (indexPipeLength == -1) continue;
                            IField fcfi1 = fc.Fields.get_Field(index1);
                            IField fcfi2 = fc.Fields.get_Field(index2);
                            ISpatialFilter filter = new SpatialFilter();
                            //filter.SubFields = fiPipeLength.Name,fcfi.Name;                          
                            filter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                            filter.Geometry = geo;
                            IFeatureCursor pFeatureCursor = null;
                            IFeature pFeature = null;
                            bool bHave = false;
                            try
                            {
                                for (int i = 0; i < this.lbxStatslimits.Items.Count; i++)
                                {
                                    pFeatureCursor = fc.Search(filter, false);
                                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                    {
                                        object tempstart = pFeature.get_Value(index1);
                                        object tempend = pFeature.get_Value(index2);

                                        Double.TryParse(tempstart.ToString(), out p.startdepth);
                                        Double.TryParse(tempend.ToString(), out p.enddepth);

                                        object templength = pFeature.get_Value(indexPipeLength);
                                        double.TryParse(templength.ToString(), out p.pipelength);

                                        string whereclause = this.lbxStatslimits.Items[i].ToString();
                                        int n1 = whereclause.IndexOf("～");
                                        //int n2 = whereclause.LastIndexOf("<");
                                        double down;
                                        double up;
                                        Double.TryParse(whereclause.Substring(0, n1), out down);
                                        Double.TryParse(whereclause.Substring(n1 + 1, whereclause.Length - n1 - 1), out up);

                                        if ((down <= p.startdepth && p.startdepth < up) && (down <= p.enddepth && p.enddepth < up))
                                        {
                                            subclasslength += p.pipelength;
                                        }
                                        if (subclasslength > 0.0) bHave = true;
                                    }
                                    if (bHave)
                                    {
                                        DataRow dr = dttemp.NewRow();
                                        dr["PIPELINETYPE"] = sc;
                                        dr["PVALUE"] = this.lbxStatslimits.Items[i].ToString();
                                        dr["LENGTH"] = subclasslength.ToString("0.00");
                                        dttemp.Rows.Add(dr);

                                        DataRow dr1 = dtstats.NewRow();
                                        dr1["PIPELINETYPE"] = sc;
                                        dr1["FIELDNAME"] = this.lbxStatslimits.Items[i].ToString();
                                        dr1["LENGTH"] = subclasslength.ToString("0.00");
                                        dtstats.Rows.Add(dr1);
                                        subclasslength = 0.0;
                                        bHave = false;
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            {
                            }
                            finally
                            {
                                if (pFeatureCursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                                    pFeatureCursor = null;
                                }
                                if (pFeature != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                                    pFeature = null;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void mapControlEvents_OnMouseDown(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            IActiveView m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;

            ISimpleLineSymbol pLineSym = new SimpleLineSymbol();//设置线样式
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 255;
            pColor.Blue = 0;
            pLineSym.Color = pColor;
            pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
            pLineSym.Width = 2;

            ISimpleFillSymbol pFillSym = new SimpleFillSymbol();//设置平面填充样式
            pFillSym.Color = pColor;
            pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            pFillSym.Outline = pLineSym;
            object symbol = pFillSym as object;
            IRubberBand band = new RubberPolygonClass();
            geo = band.TrackNew(m_Display, null);
            app.Current2DMapControl.DrawShape(geo, ref symbol);
            if (geo == null) return;
            if (isDiameter) DoGeoStatsByDiameter(geo);
            else
                DoGeoStatsByDiameter(geo);
            
            
        }
    }
}

