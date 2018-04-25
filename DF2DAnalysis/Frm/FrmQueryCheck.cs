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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace DF2DAnalysis.Frm
{
    public partial class FrmQueryCheck : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private ComboBoxEdit comboBoxEdit1;
        private LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private SimpleButton simpleButton13;
        private SimpleButton simpleButton12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private SimpleButton simpleButton11;
        private SimpleButton ButtonUniqueValue;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private RichTextBox richTextBox1;
        private LabelControl labelControl5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private SimpleButton simpleButton9;
        private SimpleButton simpleButton8;
        private SimpleButton simpleButton7;
        private SimpleButton simpleButton6;
        private SimpleButton simpleButton5;
        private SimpleButton simpleButton4;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton1;
        private LabelControl labelControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private ListBoxControl listBoxValues;
        private LabelControl labelControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private ListBoxControl listBoxFields;
        private LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private CheckEdit checkEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    
        public FrmQueryCheck()
        {
            InitializeComponent();
        }


        private Dictionary<IFeatureClass, DataTable> dict;
        private IMapControl2 m_pMapControl;
        private ArrayList m_arrPntField;
        private ArrayList m_arrArcField;
        private ArrayList m_arrArcCaptionField;
        Dictionary<string,string> dicArc=new  Dictionary<string,string>();
        DF2DFeatureClass dfcc;
        FacilityClass fcc;
        string FieldName;
        private string CurrentFieldName;
        private  DataTable dtNew;
        private DataTable dt;

        public static string strResult;
        public static string strResult1;
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton11 = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonUniqueValue = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem17 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem13 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.listBoxValues = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.listBoxFields = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton12 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.checkEdit1);
            this.layoutControl1.Controls.Add(this.simpleButton11);
            this.layoutControl1.Controls.Add(this.ButtonUniqueValue);
            this.layoutControl1.Controls.Add(this.layoutControl6);
            this.layoutControl1.Controls.Add(this.layoutControl5);
            this.layoutControl1.Controls.Add(this.layoutControl4);
            this.layoutControl1.Controls.Add(this.layoutControl3);
            this.layoutControl1.Controls.Add(this.simpleButton13);
            this.layoutControl1.Controls.Add(this.simpleButton12);
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(693, 508);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(331, 199);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "编辑SQL";
            this.checkEdit1.Size = new System.Drawing.Size(215, 19);
            this.checkEdit1.StyleController = this.layoutControl1;
            this.checkEdit1.TabIndex = 17;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // simpleButton11
            // 
            this.simpleButton11.Location = new System.Drawing.Point(511, 448);
            this.simpleButton11.Name = "simpleButton11";
            this.simpleButton11.Size = new System.Drawing.Size(157, 22);
            this.simpleButton11.StyleController = this.layoutControl1;
            this.simpleButton11.TabIndex = 16;
            this.simpleButton11.Text = "清除";
            this.simpleButton11.Click += new System.EventHandler(this.simpleButton11_Click);
            // 
            // ButtonUniqueValue
            // 
            this.ButtonUniqueValue.Location = new System.Drawing.Point(331, 448);
            this.ButtonUniqueValue.Name = "ButtonUniqueValue";
            this.ButtonUniqueValue.Size = new System.Drawing.Size(176, 22);
            this.ButtonUniqueValue.StyleController = this.layoutControl1;
            this.ButtonUniqueValue.TabIndex = 15;
            this.ButtonUniqueValue.Text = "获取唯一值";
            this.ButtonUniqueValue.Click += new System.EventHandler(this.ButtonUniqueValue_Click);
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.richTextBox1);
            this.layoutControl6.Controls.Add(this.labelControl5);
            this.layoutControl6.Location = new System.Drawing.Point(331, 222);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.Root = this.layoutControlGroup5;
            this.layoutControl6.Size = new System.Drawing.Size(337, 222);
            this.layoutControl6.TabIndex = 14;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 30);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(313, 180);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.StyleController = this.layoutControl6;
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "表达式";
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem17,
            this.layoutControlItem22,
            this.layoutControlItem23});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(337, 222);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // emptySpaceItem17
            // 
            this.emptySpaceItem17.AllowHotTrack = false;
            this.emptySpaceItem17.CustomizationFormText = "emptySpaceItem17";
            this.emptySpaceItem17.Location = new System.Drawing.Point(40, 0);
            this.emptySpaceItem17.Name = "emptySpaceItem17";
            this.emptySpaceItem17.Size = new System.Drawing.Size(277, 18);
            this.emptySpaceItem17.Text = "emptySpaceItem17";
            this.emptySpaceItem17.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.labelControl5;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(40, 18);
            this.layoutControlItem22.Text = "layoutControlItem22";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextToControlDistance = 0;
            this.layoutControlItem22.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.richTextBox1;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem23";
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(317, 184);
            this.layoutControlItem23.Text = "layoutControlItem23";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextToControlDistance = 0;
            this.layoutControlItem23.TextVisible = false;
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
            this.layoutControl5.Controls.Add(this.labelControl4);
            this.layoutControl5.Location = new System.Drawing.Point(331, 62);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup4;
            this.layoutControl5.Size = new System.Drawing.Size(337, 133);
            this.layoutControl5.TabIndex = 13;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // simpleButton9
            // 
            this.simpleButton9.Location = new System.Drawing.Point(218, 82);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(96, 22);
            this.simpleButton9.StyleController = this.layoutControl5;
            this.simpleButton9.TabIndex = 13;
            this.simpleButton9.Text = "like";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // simpleButton8
            // 
            this.simpleButton8.Location = new System.Drawing.Point(122, 82);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(92, 22);
            this.simpleButton8.StyleController = this.layoutControl5;
            this.simpleButton8.TabIndex = 12;
            this.simpleButton8.Text = "and";
            this.simpleButton8.Click += new System.EventHandler(this.simpleButton8_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(23, 82);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(95, 22);
            this.simpleButton7.StyleController = this.layoutControl5;
            this.simpleButton7.TabIndex = 11;
            this.simpleButton7.Text = "or";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(218, 56);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(96, 22);
            this.simpleButton6.StyleController = this.layoutControl5;
            this.simpleButton6.TabIndex = 10;
            this.simpleButton6.Text = "()";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(120, 56);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(94, 22);
            this.simpleButton5.StyleController = this.layoutControl5;
            this.simpleButton5.TabIndex = 9;
            this.simpleButton5.Text = "<=";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(23, 56);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(93, 22);
            this.simpleButton4.StyleController = this.layoutControl5;
            this.simpleButton4.TabIndex = 8;
            this.simpleButton4.Text = "<";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(218, 30);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(96, 22);
            this.simpleButton3.StyleController = this.layoutControl5;
            this.simpleButton3.TabIndex = 7;
            this.simpleButton3.Text = ">=";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(120, 30);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(94, 22);
            this.simpleButton2.StyleController = this.layoutControl5;
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = ">";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(23, 30);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 22);
            this.simpleButton1.StyleController = this.layoutControl5;
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "=";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.StyleController = this.layoutControl5;
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "操作符";
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem6,
            this.emptySpaceItem13,
            this.layoutControlItem11,
            this.emptySpaceItem10,
            this.emptySpaceItem14,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(337, 133);
            this.layoutControlGroup4.Text = "layoutControlGroup4";
            this.layoutControlGroup4.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(11, 96);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(295, 17);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem13
            // 
            this.emptySpaceItem13.AllowHotTrack = false;
            this.emptySpaceItem13.CustomizationFormText = "emptySpaceItem13";
            this.emptySpaceItem13.Location = new System.Drawing.Point(40, 0);
            this.emptySpaceItem13.Name = "emptySpaceItem13";
            this.emptySpaceItem13.Size = new System.Drawing.Size(277, 18);
            this.emptySpaceItem13.Text = "emptySpaceItem13";
            this.emptySpaceItem13.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.labelControl4;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(40, 18);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            this.emptySpaceItem10.CustomizationFormText = "emptySpaceItem10";
            this.emptySpaceItem10.Location = new System.Drawing.Point(0, 18);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(11, 95);
            this.emptySpaceItem10.Text = "emptySpaceItem10";
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem14
            // 
            this.emptySpaceItem14.AllowHotTrack = false;
            this.emptySpaceItem14.CustomizationFormText = "emptySpaceItem14";
            this.emptySpaceItem14.Location = new System.Drawing.Point(306, 18);
            this.emptySpaceItem14.Name = "emptySpaceItem14";
            this.emptySpaceItem14.Size = new System.Drawing.Size(11, 95);
            this.emptySpaceItem14.Text = "emptySpaceItem14";
            this.emptySpaceItem14.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.simpleButton1;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(11, 18);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.simpleButton2;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(108, 18);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.simpleButton3;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(206, 18);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.simpleButton4;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(11, 44);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.simpleButton5;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(108, 44);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.simpleButton6;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(206, 44);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.simpleButton7;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(11, 70);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(99, 26);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.simpleButton8;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(110, 70);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(96, 26);
            this.layoutControlItem19.Text = "layoutControlItem19";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.simpleButton9;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(206, 70);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.listBoxValues);
            this.layoutControl4.Controls.Add(this.labelControl3);
            this.layoutControl4.Location = new System.Drawing.Point(33, 231);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(281, 239);
            this.layoutControl4.TabIndex = 12;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // listBoxValues
            // 
            this.listBoxValues.Location = new System.Drawing.Point(12, 30);
            this.listBoxValues.Name = "listBoxValues";
            this.listBoxValues.Size = new System.Drawing.Size(257, 197);
            this.listBoxValues.StyleController = this.layoutControl4;
            this.listBoxValues.TabIndex = 5;
            this.listBoxValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxValues_MouseDoubleClick);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.StyleController = this.layoutControl4;
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "字段值";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem11,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(281, 239);
            this.layoutControlGroup3.Text = "layoutControlGroup3";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            this.emptySpaceItem11.CustomizationFormText = "emptySpaceItem11";
            this.emptySpaceItem11.Location = new System.Drawing.Point(40, 0);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(221, 18);
            this.emptySpaceItem11.Text = "emptySpaceItem11";
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.labelControl3;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(40, 18);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.listBoxValues;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(261, 201);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.listBoxFields);
            this.layoutControl3.Controls.Add(this.labelControl2);
            this.layoutControl3.Location = new System.Drawing.Point(33, 62);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(281, 165);
            this.layoutControl3.TabIndex = 11;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // listBoxFields
            // 
            this.listBoxFields.Location = new System.Drawing.Point(12, 30);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(257, 123);
            this.listBoxFields.StyleController = this.layoutControl3;
            this.listBoxFields.TabIndex = 5;
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.StyleController = this.layoutControl3;
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "字段";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem12,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(281, 165);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // emptySpaceItem12
            // 
            this.emptySpaceItem12.AllowHotTrack = false;
            this.emptySpaceItem12.CustomizationFormText = "emptySpaceItem12";
            this.emptySpaceItem12.Location = new System.Drawing.Point(28, 0);
            this.emptySpaceItem12.Name = "emptySpaceItem12";
            this.emptySpaceItem12.Size = new System.Drawing.Size(233, 18);
            this.emptySpaceItem12.Text = "emptySpaceItem12";
            this.emptySpaceItem12.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl2;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(28, 18);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.listBoxFields;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(261, 127);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // simpleButton13
            // 
            this.simpleButton13.Location = new System.Drawing.Point(510, 474);
            this.simpleButton13.Name = "simpleButton13";
            this.simpleButton13.Size = new System.Drawing.Size(158, 22);
            this.simpleButton13.StyleController = this.layoutControl1;
            this.simpleButton13.TabIndex = 10;
            this.simpleButton13.Text = "取消";
            // 
            // simpleButton12
            // 
            this.simpleButton12.Location = new System.Drawing.Point(331, 474);
            this.simpleButton12.Name = "simpleButton12";
            this.simpleButton12.Size = new System.Drawing.Size(175, 22);
            this.simpleButton12.StyleController = this.layoutControl1;
            this.simpleButton12.TabIndex = 9;
            this.simpleButton12.Text = "确定";
            this.simpleButton12.Click += new System.EventHandler(this.simpleButton12_Click);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.comboBoxEdit1);
            this.layoutControl2.Controls.Add(this.labelControl1);
            this.layoutControl2.Location = new System.Drawing.Point(12, 12);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(669, 46);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(78, 12);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(504, 22);
            this.comboBoxEdit1.StyleController = this.layoutControl2;
            this.comboBoxEdit1.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.StyleController = this.layoutControl2;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "图层名称:";
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem3});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(669, 46);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(574, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Size = new System.Drawing.Size(75, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(56, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.comboBoxEdit1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(66, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(508, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem4,
            this.emptySpaceItem1,
            this.layoutControlItem26,
            this.layoutControlItem27,
            this.emptySpaceItem9,
            this.emptySpaceItem7,
            this.emptySpaceItem8,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem10,
            this.layoutControlItem21,
            this.layoutControlItem24,
            this.layoutControlItem25,
            this.emptySpaceItem5,
            this.layoutControlItem28});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(693, 508);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.layoutControl2;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(673, 50);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 50);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(21, 412);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 462);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(319, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.simpleButton12;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem26.Location = new System.Drawing.Point(319, 462);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(179, 26);
            this.layoutControlItem26.Text = "layoutControlItem26";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextToControlDistance = 0;
            this.layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.simpleButton13;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem27.Location = new System.Drawing.Point(498, 462);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(162, 26);
            this.layoutControlItem27.Text = "layoutControlItem27";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextToControlDistance = 0;
            this.layoutControlItem27.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.CustomizationFormText = "emptySpaceItem9";
            this.emptySpaceItem9.Location = new System.Drawing.Point(660, 462);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(13, 26);
            this.emptySpaceItem9.Text = "emptySpaceItem9";
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.CustomizationFormText = "emptySpaceItem7";
            this.emptySpaceItem7.Location = new System.Drawing.Point(306, 50);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(13, 412);
            this.emptySpaceItem7.Text = "emptySpaceItem7";
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.CustomizationFormText = "emptySpaceItem8";
            this.emptySpaceItem8.Location = new System.Drawing.Point(660, 50);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(13, 412);
            this.emptySpaceItem8.Text = "emptySpaceItem8";
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.layoutControl3;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(21, 50);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(285, 169);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.layoutControl4;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(21, 219);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(285, 243);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.layoutControl5;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(319, 50);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(341, 137);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.layoutControl6;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(319, 210);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(341, 226);
            this.layoutControlItem21.Text = "layoutControlItem21";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.ButtonUniqueValue;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem24.Location = new System.Drawing.Point(319, 436);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem24.Text = "layoutControlItem24";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.simpleButton11;
            this.layoutControlItem25.CustomizationFormText = "layoutControlItem25";
            this.layoutControlItem25.Location = new System.Drawing.Point(499, 436);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(161, 26);
            this.layoutControlItem25.Text = "layoutControlItem25";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem25.TextToControlDistance = 0;
            this.layoutControlItem25.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(538, 187);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(122, 23);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.checkEdit1;
            this.layoutControlItem28.CustomizationFormText = "layoutControlItem28";
            this.layoutControlItem28.Location = new System.Drawing.Point(319, 187);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(219, 23);
            this.layoutControlItem28.Text = "layoutControlItem28";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextToControlDistance = 0;
            this.layoutControlItem28.TextVisible = false;
            // 
            // FrmQueryCheck
            // 
            this.ClientSize = new System.Drawing.Size(693, 508);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmQueryCheck";
            this.Text = "查询";
            this.Load += new System.EventHandler(this.FrmQueryCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            this.ResumeLayout(false);

        }

          public FrmQueryCheck(Dictionary<IFeatureClass, DataTable> dt, IMapControl2 current2DMapControl)
        {
            InitializeComponent();
            dict = dt;
            m_pMapControl = current2DMapControl;
        }


        /// <summary>
        /// 窗体加载
        /// </summary>    
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmQueryCheck_Load(object sender, EventArgs e)
        {

            if (dict == null) return;
            foreach (IFeatureClass fc in dict.Keys)
            {
                this.comboBoxEdit1.Properties.Items.Add(fc.AliasName);
            }
            if (this.comboBoxEdit1.Properties.Items.Count > 0) this.comboBoxEdit1.SelectedIndex = 0;


            //从Datatable读取线表的字段信息，填充界面控件

            m_arrPntField = new ArrayList();
            m_arrArcField = new ArrayList();
            m_arrArcCaptionField = new ArrayList();
            getFieldInfo();
             this.listBoxFields.Items.AddRange(new string[] { "管线材质", "道路名称", "管线规格" });
            for (int j = 0; j <  m_arrArcField.Count; j++)
            {
                string Caption = this.listBoxFields.Items[j].ToString();
                dicArc.Add(m_arrArcField[j].ToString(),Caption);
                /*this.listBoxFields.Items.Add(m_arrArcCaptionField[j]);  */              
            }
        }



        private void getFieldInfo()
        {
            this.listBoxFields.Items.Clear();
            this.listBoxValues.Items.Clear();
            DataTable dt = GetDataTable(this.comboBoxEdit1.Text);
//            dt.Columns.Remove(dt.Columns[1].ColumnName);
//            dt.Columns.Remove(dt.Columns[8].ColumnName);
//               dt.Columns.Remove(dt.Columns[1].ColumnName);
//               dt.Columns.Remove(dt.Columns[3].ColumnName);
//               dt.Columns.Remove(dt.Columns[5].ColumnName);
//             dt.Columns.Remove(dt.Columns[5].ColumnName);
//               dt.Columns.Remove(dt.Columns[5].ColumnName);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if ((i ==0) || (i ==6)||(i==7))
                {
                    
                    string fieldName = dt.Columns[i].ColumnName.ToString();
                    m_arrArcField.Add(fieldName);
                }
               
            }
        }


        //下拉框列表触发事件
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxFields.Items.Clear();
            this.listBoxValues.Items.Clear();
            DataTable dt = GetDataTable(this.comboBoxEdit1.Text);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
               
                string fieldName = dt.Columns[i].ColumnName.ToString();
                
                m_arrArcField.Add(fieldName);
            }
        }


        private DataTable GetDataTable(string fcname)
        {
            foreach (IFeatureClass fc in dict.Keys)
            {
                if (fc.AliasName == fcname) return dict[fc];
            }
            return null;

        }

       


        //字段列表双击
        private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.listBoxValues.Items.Clear();

            //设置ButtonUniqueValue按钮为可用状态
            if (ButtonUniqueValue.Enabled == false)
            {
                ButtonUniqueValue.Enabled = true;
            }
            //设置整个窗体可用的字段名称
            DataTable dt = GetDataTable(this.comboBoxEdit1.Text);
            for (int i = 0; i < m_arrArcField.Count; i++)
            {
                
                   if(dicArc[m_arrArcField[i].ToString()]==this.listBoxFields.SelectedItem.ToString()) 
                {
                    string str = m_arrArcField[i].ToString();

                    CurrentFieldName = str;
                    richTextBox1.Text += str;
                    break;
                }
            }
          /*  String str = listBoxFields.SelectedItem.ToString();*/
            //使用string类中的方法将字段改名称中的两个"字符去掉
//             str = str.Substring(1);
// 
//             str = str.Substring(0, str.Length - 1);
           
        }

        //字段值列表双击
        private void listBoxValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           richTextBox1.Text +=listBoxValues.SelectedItem.ToString();
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
                               listBoxValues.Items.Add("\'" + Row.get_Value(0).ToString() + "\'");
                           }
                           else
                           {
                               listBoxValues.Items.Add(Row.get_Value(0).ToString());
                           }
                           Row = Cursor.NextRow();
                       
                       
                    }
                   break;                   
                }           
        }
     }

        //点击清除按钮
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }


        //点击加号按钮
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton1.Text + " ";
        }


        //点击减号按钮
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton2.Text + " ";
        }
        //点击等号按钮
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton3.Text + " ";
        }
        //点击大于按钮
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton4.Text + " ";
        }
        //点击小于按钮
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton5.Text + " ";
        }
        //点击左括号按钮
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton6.Text + " ";
        }
        //点击右括号按钮
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton7.Text + " ";
        }
        //点击大于或等于按钮
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton8.Text + " ";
        }
        //点击小于或等于按钮
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + simpleButton9.Text + " ";
        }
        //点击确定按钮
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            
              strResult = this.richTextBox1.Text;
              strResult1 = this.comboBoxEdit1.SelectedItem.ToString();
              this.DialogResult = System.Windows.Forms.DialogResult.OK;
              this.Close();
         }

        private void QueryCheck()
        {
            IQueryFilter QueryFilter = new QueryFilterClass();

            QueryFilter.WhereClause = this.richTextBox1.Text;
            foreach (IFeatureClass fc in dict.Keys)
            {
                if (fc.AliasName == this.comboBoxEdit1.SelectedItem.ToString())
                {
                    IFeatureCursor FeatureCursor = fc.Search(QueryFilter, false);
                    IFeature pFeature = FeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        MoveToFeature(m_pMapControl, pFeature);//定位
                        ShowSelectionFeature(m_pMapControl, pFeature);//高亮
                        pFeature = FeatureCursor.NextFeature();
                    }

                    break;
                }
            }
        }


        public string  str
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

        //编辑SQL按钮
         private void checkEdit1_CheckedChanged(object sender, EventArgs e)
         {
             richTextBox1.Enabled = true;
             richTextBox1.Text += " " + " ";
         }

        












    }
}
    
