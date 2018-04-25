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
using System.Collections;
using DF2DData.Class;
using DFDataConfig.Class;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace DF2DAnalysis.Frm
{
    public partial class FrmQuery : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private ListBoxControl listBoxFields;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private ComboBoxEdit comboBoxEdit1;
        private LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private ListBoxControl listBoxvalues;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private SimpleButton simpleButton9;
        private SimpleButton simpleButton8;
        private SimpleButton simpleButton7;
        private SimpleButton simpleButton6;
        private SimpleButton simpleButton5;
        private SimpleButton simpleButton4;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControl layoutControl7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private CheckEdit checkEdit1;
        private SimpleButton simpleButton10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private RichTextBox richTextBox1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private SimpleButton simpleButton13;
        private SimpleButton simpleButton12;
        private SimpleButton ButtonUniqueValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    
        public FrmQuery()
        {
            InitializeComponent();
        }

        private Dictionary<IFeatureClass, DataTable> dict;
        private IMapControl2 m_pMapControl;
        private ArrayList m_arrPntField;
        private ArrayList m_arrArcField;
        private ArrayList m_arrArcCaptionField;
        Dictionary<string, string> dicArc = new Dictionary<string, string>();
        DF2DFeatureClass dfcc;
        FacilityClass fcc;
        string FieldName;
        private string CurrentFieldName;
        private DataTable dtNew;
        private DataTable dt;

        public static string strResult;
        public static string strResult1;



        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton12 = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonUniqueValue = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl7 = new DevExpress.XtraLayout.LayoutControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton10 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.listBoxvalues = new DevExpress.XtraEditors.ListBoxControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.listBoxFields = new DevExpress.XtraEditors.ListBoxControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).BeginInit();
            this.layoutControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxvalues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton13);
            this.layoutControl1.Controls.Add(this.simpleButton12);
            this.layoutControl1.Controls.Add(this.ButtonUniqueValue);
            this.layoutControl1.Controls.Add(this.layoutControl7);
            this.layoutControl1.Controls.Add(this.layoutControl6);
            this.layoutControl1.Controls.Add(this.layoutControl5);
            this.layoutControl1.Controls.Add(this.layoutControl4);
            this.layoutControl1.Controls.Add(this.layoutControl3);
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(587, 509);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButton13
            // 
            this.simpleButton13.Location = new System.Drawing.Point(520, 475);
            this.simpleButton13.Name = "simpleButton13";
            this.simpleButton13.Size = new System.Drawing.Size(45, 22);
            this.simpleButton13.StyleController = this.layoutControl1;
            this.simpleButton13.TabIndex = 12;
            this.simpleButton13.Text = "取消";
            this.simpleButton13.Click += new System.EventHandler(this.simpleButton13_Click);
            // 
            // simpleButton12
            // 
            this.simpleButton12.Location = new System.Drawing.Point(463, 475);
            this.simpleButton12.Name = "simpleButton12";
            this.simpleButton12.Size = new System.Drawing.Size(53, 22);
            this.simpleButton12.StyleController = this.layoutControl1;
            this.simpleButton12.TabIndex = 11;
            this.simpleButton12.Text = "确定";
            this.simpleButton12.Click += new System.EventHandler(this.simpleButton12_Click);
            // 
            // ButtonUniqueValue
            // 
            this.ButtonUniqueValue.Location = new System.Drawing.Point(256, 475);
            this.ButtonUniqueValue.Name = "ButtonUniqueValue";
            this.ButtonUniqueValue.Size = new System.Drawing.Size(203, 22);
            this.ButtonUniqueValue.StyleController = this.layoutControl1;
            this.ButtonUniqueValue.TabIndex = 10;
            this.ButtonUniqueValue.Text = "获取唯一值";
            this.ButtonUniqueValue.Click += new System.EventHandler(this.ButtonUniqueValue_Click);
            // 
            // layoutControl7
            // 
            this.layoutControl7.Controls.Add(this.richTextBox1);
            this.layoutControl7.Location = new System.Drawing.Point(256, 221);
            this.layoutControl7.Name = "layoutControl7";
            this.layoutControl7.Root = this.layoutControlGroup6;
            this.layoutControl7.Size = new System.Drawing.Size(319, 250);
            this.layoutControl7.TabIndex = 9;
            this.layoutControl7.Text = "layoutControl7";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 23);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(313, 224);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "表达式";
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem22});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(319, 250);
            this.layoutControlGroup6.Text = "表达式";
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.richTextBox1;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(317, 228);
            this.layoutControlItem22.Text = "layoutControlItem22";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextToControlDistance = 0;
            this.layoutControlItem22.TextVisible = false;
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.checkEdit1);
            this.layoutControl6.Controls.Add(this.simpleButton10);
            this.layoutControl6.Location = new System.Drawing.Point(256, 162);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.Root = this.layoutControlGroup5;
            this.layoutControl6.Size = new System.Drawing.Size(319, 55);
            this.layoutControl6.TabIndex = 8;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(186, 23);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "编辑SQL";
            this.checkEdit1.Size = new System.Drawing.Size(99, 19);
            this.checkEdit1.StyleController = this.layoutControl6;
            this.checkEdit1.TabIndex = 5;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // simpleButton10
            // 
            this.simpleButton10.Location = new System.Drawing.Point(47, 23);
            this.simpleButton10.Name = "simpleButton10";
            this.simpleButton10.Size = new System.Drawing.Size(40, 22);
            this.simpleButton10.StyleController = this.layoutControl6;
            this.simpleButton10.TabIndex = 4;
            this.simpleButton10.Text = "清除";
            this.simpleButton10.Click += new System.EventHandler(this.simpleButton10_Click);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "操作";
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem19,
            this.emptySpaceItem5,
            this.emptySpaceItem6,
            this.layoutControlItem20,
            this.emptySpaceItem7});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(319, 55);
            this.layoutControlGroup5.Text = "操作";
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.simpleButton10;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(44, 0);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(44, 33);
            this.layoutControlItem19.Text = "layoutControlItem19";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(44, 33);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(286, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(31, 33);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.checkEdit1;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(183, 0);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(103, 33);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.CustomizationFormText = "emptySpaceItem7";
            this.emptySpaceItem7.Location = new System.Drawing.Point(88, 0);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(95, 33);
            this.emptySpaceItem7.Text = "emptySpaceItem7";
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.simpleButton9);
            this.layoutControl5.Controls.Add(this.simpleButton8);
            this.layoutControl5.Controls.Add(this.simpleButton7);
            this.layoutControl5.Controls.Add(this.simpleButton6);
            this.layoutControl5.Controls.Add(this.simpleButton5);
            this.layoutControl5.Controls.Add(this.simpleButton4);
            this.layoutControl5.Controls.Add(this.simpleButton3);
            this.layoutControl5.Controls.Add(this.simpleButton2);
            this.layoutControl5.Controls.Add(this.simpleButton1);
            this.layoutControl5.Location = new System.Drawing.Point(256, 42);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup4;
            this.layoutControl5.Size = new System.Drawing.Size(319, 116);
            this.layoutControl5.TabIndex = 7;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // simpleButton9
            // 
            this.simpleButton9.Location = new System.Drawing.Point(216, 75);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(90, 22);
            this.simpleButton9.StyleController = this.layoutControl5;
            this.simpleButton9.TabIndex = 12;
            this.simpleButton9.Text = "like";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // simpleButton8
            // 
            this.simpleButton8.Location = new System.Drawing.Point(117, 75);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(95, 22);
            this.simpleButton8.StyleController = this.layoutControl5;
            this.simpleButton8.TabIndex = 11;
            this.simpleButton8.Text = "and";
            this.simpleButton8.Click += new System.EventHandler(this.simpleButton8_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(13, 75);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(100, 22);
            this.simpleButton7.StyleController = this.layoutControl5;
            this.simpleButton7.TabIndex = 10;
            this.simpleButton7.Text = "or";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(216, 49);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(90, 22);
            this.simpleButton6.StyleController = this.layoutControl5;
            this.simpleButton6.TabIndex = 9;
            this.simpleButton6.Text = "()";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(117, 49);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(95, 22);
            this.simpleButton5.StyleController = this.layoutControl5;
            this.simpleButton5.TabIndex = 8;
            this.simpleButton5.Text = "<=";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(13, 49);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(100, 22);
            this.simpleButton4.StyleController = this.layoutControl5;
            this.simpleButton4.TabIndex = 7;
            this.simpleButton4.Text = "<";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(217, 23);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(89, 22);
            this.simpleButton3.StyleController = this.layoutControl5;
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = ">=";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(116, 23);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(97, 22);
            this.simpleButton2.StyleController = this.layoutControl5;
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = ">";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(13, 23);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(99, 22);
            this.simpleButton1.StyleController = this.layoutControl5;
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "=";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "操作符";
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.emptySpaceItem4,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem17});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(319, 116);
            this.layoutControlGroup4.Text = "操作符";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.simpleButton1;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.simpleButton2;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(113, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(101, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.simpleButton3;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(214, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(213, 78);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(94, 16);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(307, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 94);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 94);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.simpleButton4;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(10, 26);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(104, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.simpleButton5;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(114, 26);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(99, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.simpleButton6;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(213, 26);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.simpleButton7;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(10, 52);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(104, 42);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.simpleButton8;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(114, 52);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(99, 42);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.simpleButton9;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(213, 52);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.listBoxvalues);
            this.layoutControl4.Location = new System.Drawing.Point(12, 262);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(240, 235);
            this.layoutControl4.TabIndex = 6;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // listBoxvalues
            // 
            this.listBoxvalues.Location = new System.Drawing.Point(3, 23);
            this.listBoxvalues.Name = "listBoxvalues";
            this.listBoxvalues.Size = new System.Drawing.Size(234, 209);
            this.listBoxvalues.StyleController = this.layoutControl4;
            this.listBoxvalues.TabIndex = 4;
            this.listBoxvalues.DoubleClick += new System.EventHandler(this.listBoxvalues_DoubleClick);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "字段值";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(240, 235);
            this.layoutControlGroup3.Text = "字段值";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.listBoxvalues;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(238, 213);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.listBoxFields);
            this.layoutControl3.Location = new System.Drawing.Point(12, 42);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(240, 216);
            this.layoutControl3.TabIndex = 5;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // listBoxFields
            // 
            this.listBoxFields.Location = new System.Drawing.Point(3, 23);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(234, 190);
            this.listBoxFields.StyleController = this.layoutControl3;
            this.listBoxFields.TabIndex = 4;
            this.listBoxFields.DoubleClick += new System.EventHandler(this.listBoxFields_DoubleClick);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "字段";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(240, 216);
            this.layoutControlGroup2.Text = "字段";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.listBoxFields;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(238, 194);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.comboBoxEdit1);
            this.layoutControl2.Controls.Add(this.labelControl1);
            this.layoutControl2.Location = new System.Drawing.Point(12, 12);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(563, 26);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(66, 2);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(495, 22);
            this.comboBoxEdit1.StyleController = this.layoutControl2;
            this.comboBoxEdit1.TabIndex = 5;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.StyleController = this.layoutControl2;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "图层名称：";
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(563, 26);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.comboBoxEdit1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(64, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(499, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem18,
            this.layoutControlItem21,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem25});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(587, 509);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(557, 463);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.layoutControl2;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(567, 30);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.layoutControl3;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(244, 220);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.layoutControl4;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 250);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(244, 239);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.layoutControl5;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(244, 30);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(323, 120);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.layoutControl6;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(244, 150);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(323, 59);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.layoutControl7;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(244, 209);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(323, 254);
            this.layoutControlItem21.Text = "layoutControlItem21";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.ButtonUniqueValue;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem23";
            this.layoutControlItem23.Location = new System.Drawing.Point(244, 463);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem23.Text = "layoutControlItem23";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextToControlDistance = 0;
            this.layoutControlItem23.TextVisible = false;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.simpleButton12;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem24.Location = new System.Drawing.Point(451, 463);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(57, 26);
            this.layoutControlItem24.Text = "layoutControlItem24";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.simpleButton13;
            this.layoutControlItem25.CustomizationFormText = "layoutControlItem25";
            this.layoutControlItem25.Location = new System.Drawing.Point(508, 463);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(49, 26);
            this.layoutControlItem25.Text = "layoutControlItem25";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem25.TextToControlDistance = 0;
            this.layoutControlItem25.TextVisible = false;
            // 
            // FrmQuery
            // 
            this.ClientSize = new System.Drawing.Size(587, 509);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmQuery";
            this.Text = "快捷查询";
            this.Load += new System.EventHandler(this.FrmQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).EndInit();
            this.layoutControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxvalues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            this.ResumeLayout(false);

        }

          public FrmQuery(Dictionary<IFeatureClass, DataTable> dt, IMapControl2 current2DMapControl)
        {
            InitializeComponent();
            dict = dt;
            m_pMapControl = current2DMapControl;
        }

       
        private void FrmQuery_Load(object sender, EventArgs e)
        {
            m_arrPntField = new ArrayList();
            m_arrArcField = new ArrayList();
            m_arrArcCaptionField = new ArrayList();
            if (dict == null) return;
            foreach (IFeatureClass fc in dict.Keys)
            {
                this.comboBoxEdit1.Properties.Items.Add(fc.AliasName);
            }
            if (this.comboBoxEdit1.Properties.Items.Count > 0) this.comboBoxEdit1.SelectedIndex = 0;
         

       
        }


        private DataTable GetDataTable(string fcname)
        {
            foreach (IFeatureClass fc in dict.Keys)
            {
                if (fc.AliasName == fcname) return dict[fc];
            }
            return null;

        }

        //下拉框列表触发事件
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxFields.Items.Clear();
            this.listBoxvalues.Items.Clear();
            DataTable dt = GetDataTable(this.comboBoxEdit1.Text);
            m_arrArcField.Clear();
            dicArc.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if ((i == 0) || (i == 6) || (i == 7))
                {

                    string fieldName = dt.Columns[i].ColumnName.ToString();
                    m_arrArcField.Add(fieldName);
                }
            }
            this.listBoxFields.Items.AddRange(new string[] { "管线材质", "道路名称", "管线规格" });
            for (int j = 0; j < m_arrArcField.Count; j++)
            {
                string Caption = this.listBoxFields.Items[j].ToString();
                dicArc.Add(m_arrArcField[j].ToString(), Caption);

            }

        }

        //字段列表双击
        private void listBoxFields_DoubleClick(object sender, EventArgs e)
        {

            this.listBoxvalues.Items.Clear();

            //设置ButtonUniqueValue按钮为可用状态
            if (ButtonUniqueValue.Enabled == false)
            {
                ButtonUniqueValue.Enabled = true;
            }
            //设置整个窗体可用的字段名称
            DataTable dt = GetDataTable(this.comboBoxEdit1.Text);
            for (int i = 0; i < m_arrArcField.Count; i++)
            {

                if (dicArc[m_arrArcField[i].ToString()] == this.listBoxFields.SelectedItem.ToString())
                {
                    string str = m_arrArcField[i].ToString();

                    CurrentFieldName = str;
                    richTextBox1.Text += str;
                    break;
                }
            }
        }


        //字段值列表双击
        private void listBoxvalues_DoubleClick(object sender, EventArgs e)
        {
            richTextBox1.Text += listBoxvalues.SelectedItem.ToString();
        }

        //点击获取唯一值按钮
        private void ButtonUniqueValue_Click(object sender, EventArgs e)
        {
            foreach (IFeatureClass fc in dict.Keys)
            {
                string name = this.comboBoxEdit1.SelectedItem.ToString();
                string fcname = fc.AliasName;
                if (fcname == name)
                {

                    IDataset Dataset = fc as IDataset;
                    IQueryDef QueryDef = ((IFeatureWorkspace)Dataset.Workspace).CreateQueryDef();
                    QueryDef.Tables = Dataset.Name;
                    QueryDef.SubFields = "DISTINCT(" + CurrentFieldName + ")";
                    ICursor Cursor = QueryDef.Evaluate();
                    IField Field = fc.Fields.get_Field(fc.Fields.FindField(CurrentFieldName));
                    IRow Row = Cursor.NextRow();
                    while (Row != null)
                    {
                        if (Field.Type == esriFieldType.esriFieldTypeString)
                        {
                            listBoxvalues.Items.Add("\'" + Row.get_Value(0).ToString() + "\'");
                        }
                        else
                        {
                            listBoxvalues.Items.Add(Row.get_Value(0).ToString());
                        }
                        Row = Cursor.NextRow();


                    }
                    break;
                }
            }
        }
        //点击清除按钮
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        //点击等号按钮
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton1.Text + " ";
        }
        //点击大于按钮
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton2.Text + " ";
        }
        //点击大于或等于按钮
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton3.Text + " ";
        }
        //点击小于按钮
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton4.Text + " ";
        }
        //点击小于或等于按钮
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton5.Text + " ";
        }
        //点击括号按钮
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton6.Text + " ";
        }
        //点击or按钮
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton7.Text + " ";
        }
        //点击and按钮
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton8.Text + " ";
        }
        //点击like按钮
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton9.Text + " ";
        }
        //编辑SQL按钮
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Enabled = true;
            richTextBox1.Text += " " + " ";
        }
        //点击确定按钮
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            strResult = this.richTextBox1.Text;
            strResult1 = this.comboBoxEdit1.SelectedItem.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        //点击取消按钮
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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


        public string str
        {

            get
            {
                return strResult;
            }

        }

        public string str1
        {

            get
            {
                return strResult1;
            }

        }

        public DataTable dTable
        {

            get
            {
                return dt;
            }

        }



    }
}
