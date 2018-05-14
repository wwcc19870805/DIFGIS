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
using DFDataConfig.Logic;
using DFWinForms.Class;
using ESRI.ArcGIS.Geometry;
using DF2DData.Class;
using DFDataConfig.Class;
using ESRI.ArcGIS.Carto;
using DF2DControl.Base;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using DF2DAnalysis.Class;
using System.IO;
using ESRI.ArcGIS.esriSystem;

namespace DF2DAnalysis.Frm
{
    public partial class FrmAllPipelineCross : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ListBoxControl lbx_CrossPipe;
        private DevExpress.XtraGrid.GridControl gc_CrossPipeStats;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btn_analysis;
        private DevExpress.XtraGrid.GridControl gc_pipeNumStats;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btn_Close;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        Dictionary<string,string> _lines;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        DF2DApplication app = DF2DApplication.Application;
        Dictionary<string, double> dictHorizon;
        Dictionary<string, double> dictVertical;
        private SimpleButton btn_save;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        Dictionary<string, double> dictDepth;
        CrossAnalysis crossAnalysis;
        HashSet<string> hsCross;
        private LabelControl labelControl1;
        private SpinEdit se_Min;
        private SpinEdit se_Max;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        HashSet<string> hsTxt;
        IFeatureClass fcMetadata;
        int count;
        
    
        public FrmAllPipelineCross()
        {
            InitializeComponent();
            
        }
        public FrmAllPipelineCross(string diameter,string sysname)
        {
            InitializeComponent();
            hsCross = new HashSet<string>();
            hsTxt = new HashSet<string>();
            crossAnalysis = new CrossAnalysis(diameter,sysname,ref hsCross,ref hsTxt);
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.se_Min = new DevExpress.XtraEditors.SpinEdit();
            this.se_Max = new DevExpress.XtraEditors.SpinEdit();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.lbx_CrossPipe = new DevExpress.XtraEditors.ListBoxControl();
            this.gc_CrossPipeStats = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_analysis = new DevExpress.XtraEditors.SimpleButton();
            this.gc_pipeNumStats = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.se_Min.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.se_Max.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_CrossPipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_CrossPipeStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_pipeNumStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.se_Min);
            this.layoutControl1.Controls.Add(this.se_Max);
            this.layoutControl1.Controls.Add(this.btn_save);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.btn_Close);
            this.layoutControl1.Controls.Add(this.lbx_CrossPipe);
            this.layoutControl1.Controls.Add(this.gc_CrossPipeStats);
            this.layoutControl1.Controls.Add(this.btn_analysis);
            this.layoutControl1.Controls.Add(this.gc_pipeNumStats);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(650, 407);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 274);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "总分区数：";
            // 
            // se_Min
            // 
            this.se_Min.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.se_Min.Location = new System.Drawing.Point(102, 318);
            this.se_Min.Name = "se_Min";
            this.se_Min.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.se_Min.Size = new System.Drawing.Size(50, 22);
            this.se_Min.StyleController = this.layoutControl1;
            this.se_Min.TabIndex = 13;
            // 
            // se_Max
            // 
            this.se_Max.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.se_Max.Location = new System.Drawing.Point(102, 292);
            this.se_Max.Name = "se_Max";
            this.se_Max.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.se_Max.Size = new System.Drawing.Size(50, 22);
            this.se_Max.StyleController = this.layoutControl1;
            this.se_Max.TabIndex = 12;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(506, 370);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(129, 22);
            this.btn_save.StyleController = this.layoutControl1;
            this.btn_save.TabIndex = 11;
            this.btn_save.Text = "保存结果";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(15, 35);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "硬碰撞"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "软碰撞")});
            this.radioGroup1.Size = new System.Drawing.Size(137, 54);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 10;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(15, 370);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(137, 22);
            this.btn_Close.StyleController = this.layoutControl1;
            this.btn_Close.TabIndex = 9;
            this.btn_Close.Text = "关闭";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbx_CrossPipe
            // 
            this.lbx_CrossPipe.Location = new System.Drawing.Point(506, 35);
            this.lbx_CrossPipe.MultiColumn = true;
            this.lbx_CrossPipe.Name = "lbx_CrossPipe";
            this.lbx_CrossPipe.Size = new System.Drawing.Size(129, 331);
            this.lbx_CrossPipe.StyleController = this.layoutControl1;
            this.lbx_CrossPipe.TabIndex = 8;
            this.lbx_CrossPipe.DoubleClick += new System.EventHandler(this.lbx_CrossPipe_DoubleClick);
            // 
            // gc_CrossPipeStats
            // 
            this.gc_CrossPipeStats.Location = new System.Drawing.Point(162, 35);
            this.gc_CrossPipeStats.MainView = this.gridView2;
            this.gc_CrossPipeStats.Name = "gc_CrossPipeStats";
            this.gc_CrossPipeStats.Size = new System.Drawing.Size(334, 357);
            this.gc_CrossPipeStats.TabIndex = 7;
            this.gc_CrossPipeStats.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gc_CrossPipeStats;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btn_analysis
            // 
            this.btn_analysis.Location = new System.Drawing.Point(15, 344);
            this.btn_analysis.Name = "btn_analysis";
            this.btn_analysis.Size = new System.Drawing.Size(137, 22);
            this.btn_analysis.StyleController = this.layoutControl1;
            this.btn_analysis.TabIndex = 6;
            this.btn_analysis.Text = "分析";
            this.btn_analysis.Click += new System.EventHandler(this.btn_analysis_Click);
            // 
            // gc_pipeNumStats
            // 
            this.gc_pipeNumStats.Location = new System.Drawing.Point(15, 119);
            this.gc_pipeNumStats.MainView = this.gridView1;
            this.gc_pipeNumStats.Name = "gc_pipeNumStats";
            this.gc_pipeNumStats.Size = new System.Drawing.Size(137, 151);
            this.gc_pipeNumStats.TabIndex = 5;
            this.gc_pipeNumStats.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gc_pipeNumStats;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(650, 407);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "全库管线数量统计";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 84);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(147, 303);
            this.layoutControlGroup2.Text = "全库内管线数量统计";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gc_pipeNumStats;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(141, 155);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_analysis;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 225);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_Close;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 251);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.se_Max;
            this.layoutControlItem8.CustomizationFormText = "分区编号上限：";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 173);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(141, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "分区编号上限：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.se_Min;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 199);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(141, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "分区编号下限：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl1;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 155);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(141, 18);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "碰撞统计";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(147, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(344, 387);
            this.layoutControlGroup3.Text = "碰撞统计";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gc_CrossPipeStats;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(338, 361);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "碰撞管线列表";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem7});
            this.layoutControlGroup4.Location = new System.Drawing.Point(491, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(139, 387);
            this.layoutControlGroup4.Text = "碰撞管线列表";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lbx_CrossPipe;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(133, 335);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_save;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 335);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(133, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "碰撞类型";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(147, 84);
            this.layoutControlGroup5.Text = "碰撞类型";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(141, 58);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmAllPipelineCross
            // 
            this.ClientSize = new System.Drawing.Size(650, 407);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmAllPipelineCross";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管线碰撞分析";
            this.Load += new System.EventHandler(this.FrmPipelineCross_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.se_Min.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.se_Max.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_CrossPipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_CrossPipeStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc_pipeNumStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmPipelineCross_Load(object sender, EventArgs e)
        {
            if (crossAnalysis == null) return;
            _lines = crossAnalysis.GetAllPipeLines();
            if (_lines == null) return;
            DataTable dtNum = new DataTable();
            fcMetadata = LoadMetaData();
            if (fcMetadata == null) return;
            count = fcMetadata.FeatureCount(null);
            labelControl1.Text += count;
            dtNum.Columns.AddRange(new DataColumn[] { new DataColumn("图层"), new DataColumn("管线数量") });
            try
            {
                foreach (KeyValuePair<string, string> v in _lines)
                {
                    DataRow dr = dtNum.NewRow();
                    dr["图层"] = v.Key;
                    dr["管线数量"] = v.Value;
                    dtNum.Rows.Add(dr);
                }
                this.gc_pipeNumStats.DataSource = dtNum;
            }
            catch (System.Exception ex)
            {
            	
            }         
        }

        private void btn_analysis_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsCountNumberRight(se_Max.Value) || !IsCountNumberRight(se_Min.Value))
                {
                    XtraMessageBox.Show("分区必须大于0，小于分区总数","提示");
                    return;
                }
                if (se_Max.Value <= se_Min.Value)
                {
                    XtraMessageBox.Show("分区上限必须大于分区下线");
                    return;
                } 
                WaitForm.Start("开始分析...", "请稍后",new Size(300, 40));
                this.lbx_CrossPipe.Items.Clear();
                hsCross.Clear();
                crossAnalysis.CrossType = this.radioGroup1.SelectedIndex;
                //IFeatureClass fc = LoadMetaData();
                if (fcMetadata == null) return;
                IFeature feature;
                IFeatureCursor cursor = fcMetadata.Search(null, true);
                if (cursor == null) return;
                int n = 0;
                while ((feature = cursor.NextFeature()) != null)
                {
                    n++;
                    if (n < Convert.ToInt16(se_Min.Value)) continue;
                    WaitForm.SetCaption("正在分析区域" + n + "/" + count + "中的管线","请稍后");
                    IGeometry geo = feature.Shape;
                    IPolygon polygon = null;     
                    if (geo.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        polygon = CreatePolygonByPoints(geo as IPointCollection);
                    }
                    if (polygon == null) continue;
                    crossAnalysis.GetAllPipelineCrossByGeo(polygon);
                    if (n >= Convert.ToInt16(se_Max.Value)) break;
                   
                }
                HashSet<string> hsRB = ReBuildHashSet();
                this.gc_CrossPipeStats.DataSource = BuildDataTable(hsRB);
                this.lbx_CrossPipe.Items.AddRange(this.hsCross.ToArray());
                WaitForm.Stop();
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示");
                WaitForm.Stop();
            }
           
            
        }

        private bool IsCountNumberRight(Decimal value)
        {
            Int16 num = Convert.ToInt16(value);
            if (0 < num && num <= count) return true;
            else return false;
        }
        private IPolygon CreatePolygonByPoints(IPointCollection pCol)
        {
            IGeometryBridge2 gb2 = new GeometryEnvironmentClass();
            IPointCollection4 pPolygon = new PolygonClass();
            WKSPoint[] pWKSPoint = new WKSPoint[pCol.PointCount];
            for (int i = 0; i < pCol.PointCount;i++ )
            {
                pWKSPoint[i].X = pCol.get_Point(i).X;
                pWKSPoint[i].Y = pCol.get_Point(i).Y;       

            }   
            gb2.SetWKSPoints(pPolygon, ref pWKSPoint);
            IPolygon poly = pPolygon as IPolygon;
            poly.Close();
            return poly;

        }

        private HashSet<string> ReBuildHashSet()
        {
            if (hsCross == null) return null;
            HashSet<string> hsRB = new HashSet<string>();
           
            foreach (string cross in hsCross)
            {
                string idA = cross.Substring(0, cross.IndexOf(','));
                string idB = cross.Substring(cross.IndexOf(',') + 1);
                string crossCtr = idB + ',' + idA;
                hsRB.Add(cross);
                hsRB.Add(crossCtr);
            }
            return hsRB;
        }
        private DataTable  BuildDataTable(HashSet<string> hs)
        {
            DataTable dt = new DataTable();
            int n = 0;
            dt.Columns.Add(new DataColumn("图层"));
            if (_lines == null||hsCross == null) return null;
            foreach (string s in _lines.Keys)
            {
                dt.Columns.Add(new DataColumn(s,typeof(int)));
            }
            foreach (string s in _lines.Keys)
            {
                DataRow dr = dt.NewRow();
                dr["图层"] = s;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName == "图层") continue;
                    dr[dc.ColumnName] = 0;
                }
                foreach (string cross in hs)
                {
                    string mcaName = cross.Substring(0, cross.IndexOf('_'));
                    int mcblength = cross.LastIndexOf('_') - cross.IndexOf(',')-1;
                    string mcbName = cross.Substring(cross.IndexOf(',') + 1, mcblength);
                    string mcaAlias = GetAlias(mcaName);
                    if (mcaAlias != s) continue;
                    string mcbAlias = GetAlias(mcbName);
                    n = Convert.ToInt32(dr[mcbAlias]);
                    n++;
                    dr[mcbAlias] = n;
                    //if (Int32.TryParse(dr[mcbAlias].ToString(), out n))
                    //{
                    //    n++;
                    //    dr[mcbAlias] = n;
                    //}

                }
                dt.Rows.Add(dr);
            }
            
            //填充矩阵下半部分  
            return dt;
        }
        private string GetAlias(string name)
        {
            switch(name)
            {
                case "DL":
                    return "电力";
                case"TX":
                    return "通讯";
                case "GS":
                    return "给水";
                case "PS":
                    return "排水";
                case "RQ":
                    return "燃气";
                case "RL":
                    return "热力";
                case "GYQT":
                    return "工业气体";
                case "GYSG":
                    return "工业水管";
                case "GYHG":
                    return "工业化工";
                case "GYQTT":
                    return "工业其他";

            }
            return null;
               

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private IFeatureClass LoadMetaData()
        {
            string path = Config.GetConfigValue("2DMdbPipe");
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
            if (pFWs == null) return null;
            IFeatureDataset pFDs = pFWs.OpenFeatureDataset("Assi_10");
            if (pFDs == null) return null;
            IEnumDataset pEnumDs = pFDs.Subsets;
            IDataset fDs;
            IFeatureClass fc = null;
            while ((fDs = pEnumDs.Next()) != null) 
            {
                if (fDs.Name == "Metadata")
                {
                    fc = fDs as IFeatureClass;
                }

            }
            return fc;

        }
        private void lbx_CrossPipe_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
                string temp = this.lbx_CrossPipe.SelectedItem.ToString();
                string lineA = temp.Substring(0, (temp.IndexOf(",")));
                string lineB = temp.Substring(temp.IndexOf(",") + 1);
                if (lineA == null || lineB == null) return;
                string mcNameA = lineA.Substring(0, lineA.IndexOf("_"));
                string fOidA = lineA.Substring(lineA.IndexOf("_") + 1);
                string mcNameB = lineB.Substring(0, lineB.IndexOf("_"));
                string fOidB = lineB.Substring(lineB.IndexOf("_") + 1);
                int OIDA;
                int OIDB;
                Int32.TryParse(fOidA, out OIDA);
                Int32.TryParse(fOidB, out OIDB);

                foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                {
                    
                    if (mc.Name == mcNameA)
                    {
                        bool have = false;
                        string[] arrFc2DId = mc.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            if (!sc.Visible2D) continue;
                            foreach (string fc2DId in arrFc2DId)
                            {
                                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                                if (dffc == null) continue;
                                FacilityClass facc = dffc.GetFacilityClass();//得到设施类
                                IFeatureClass fc = dffc.GetFeatureClass();//得到要素类
                                IFeatureLayer fl = dffc.GetFeatureLayer();
                                if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                                if (fl == null) return;
                                IFeature feature1 = fc.GetFeature(OIDA);
                                if (feature1 == null) return;
                                app.Current2DMapControl.ActiveView.FocusMap.SelectFeature(fl, feature1);
                                //app.Current2DMapControl.Active    View.FocusMap.SelectFeature(fl, feature2);
                                IPolyline polyline = feature1.Shape as IPolyline;
                                IPoint point = polyline.ToPoint;
                                app.Current2DMapControl.MapScale = 500;
                                app.Current2DMapControl.CenterAt(point);
                                have = true;
                            }
                            if(have) break;
                        }
                    }
                    if (mc.Name == mcNameB)
                    {
                        bool have = false;
                        string[] arrFc2DId = mc.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            if (!sc.Visible2D) continue;
                            foreach (string fc2DId in arrFc2DId)
                            {
                                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);//根据要素类ID得到DF2DFC
                                if (dffc == null) continue;
                                FacilityClass facc = dffc.GetFacilityClass();//得到设施类
                                IFeatureClass fc = dffc.GetFeatureClass();//得到要素类
                                IFeatureLayer fl = dffc.GetFeatureLayer();
                                if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                                if (fl == null) return;
                                IFeature feature = fc.GetFeature(OIDB);
                                app.Current2DMapControl.ActiveView.FocusMap.SelectFeature(fl, feature);
                                IPolyline polyline = feature.Shape as IPolyline;
                                IPoint point = polyline.FromPoint;
                                app.Current2DMapControl.MapScale = 500;
                                app.Current2DMapControl.CenterAt(point);
                                
                                have = true;
                            }
                            if (have) break;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt Files(*.txt)|*.txt";
            sfd.DefaultExt = "txt";
            sfd.RestoreDirectory = true;
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString();
                bool suc = ExportData(localFilePath);
                if (suc)
                {
                    XtraMessageBox.Show("导出成功！", "提示");
                }
                else
                {
                    XtraMessageBox.Show("导出失败！", "提示");
                }
            }
            
        }
        private bool ExportData(string path)
        {
            if (hsTxt == null) return false;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                foreach (string cross in hsTxt)
                {
                    sw.WriteLine(cross);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
           
            
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                crossAnalysis.CrossType = 0;
            }
            else
                crossAnalysis.CrossType = 1;
        }

      
    }
}
