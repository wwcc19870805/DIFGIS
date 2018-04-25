using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DFDataConfig.Logic;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using DF3DControl.Base;
using Gvitech.CityMaker.Controls;
using DFCommon.Class;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeGeometry;
using DF3DData.Class;
using Gvitech.CityMaker.RenderControl;
using DF3DDraw;

namespace DF3DPipe.Analysis.Frm
{
    public  class FrmPipelineCross : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private SimpleButton btn_Close;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private RadioGroup radioGroup1;
        private ListBoxControl lbx_CrossPipe;
        private DevExpress.XtraGrid.GridControl gc_CrossPipeStats;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private SimpleButton btn_analysis;
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
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPipelineCross));
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.lbx_CrossPipe = new DevExpress.XtraEditors.ListBoxControl();
            this.gc_CrossPipeStats = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_analysis = new DevExpress.XtraEditors.SimpleButton();
            this.gc_pipeNumStats = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_Close;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 293);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(137, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // btn_Close
            // 
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Location = new System.Drawing.Point(5, 379);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(133, 22);
            this.btn_Close.StyleController = this.layoutControl1;
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "关闭";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // layoutControl1
            // 
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
            this.layoutControl1.Size = new System.Drawing.Size(650, 406);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(5, 25);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "硬碰撞"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "软碰撞")});
            this.radioGroup1.Size = new System.Drawing.Size(133, 31);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 0;
            // 
            // lbx_CrossPipe
            // 
            this.lbx_CrossPipe.Location = new System.Drawing.Point(484, 25);
            this.lbx_CrossPipe.MultiColumn = true;
            this.lbx_CrossPipe.Name = "lbx_CrossPipe";
            this.lbx_CrossPipe.Size = new System.Drawing.Size(161, 376);
            this.lbx_CrossPipe.StyleController = this.layoutControl1;
            this.lbx_CrossPipe.TabIndex = 5;
            this.lbx_CrossPipe.DoubleClick += new System.EventHandler(this.lbx_CrossPipe_DoubleClick);
            // 
            // gc_CrossPipeStats
            // 
            this.gc_CrossPipeStats.Location = new System.Drawing.Point(148, 25);
            this.gc_CrossPipeStats.MainView = this.gridView2;
            this.gc_CrossPipeStats.Name = "gc_CrossPipeStats";
            this.gc_CrossPipeStats.Size = new System.Drawing.Size(326, 376);
            this.gc_CrossPipeStats.TabIndex = 4;
            this.gc_CrossPipeStats.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gc_CrossPipeStats;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView2_FocusedRowChanged);
            // 
            // btn_analysis
            // 
            this.btn_analysis.Location = new System.Drawing.Point(5, 353);
            this.btn_analysis.Name = "btn_analysis";
            this.btn_analysis.Size = new System.Drawing.Size(133, 22);
            this.btn_analysis.StyleController = this.layoutControl1;
            this.btn_analysis.TabIndex = 2;
            this.btn_analysis.Text = "分析";
            this.btn_analysis.Click += new System.EventHandler(this.btn_analysis_Click);
            // 
            // gc_pipeNumStats
            // 
            this.gc_pipeNumStats.Location = new System.Drawing.Point(5, 86);
            this.gc_pipeNumStats.MainView = this.gridView1;
            this.gc_pipeNumStats.Name = "gc_pipeNumStats";
            this.gc_pipeNumStats.Size = new System.Drawing.Size(133, 263);
            this.gc_pipeNumStats.TabIndex = 1;
            this.gc_pipeNumStats.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gc_pipeNumStats;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "类型";
            this.gridColumn1.FieldName = "MC";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "数量";
            this.gridColumn2.FieldName = "Num";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(650, 406);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "区域内管线数量统计";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 61);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(143, 345);
            this.layoutControlGroup2.Text = "区域内管线数量统计";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gc_pipeNumStats;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(137, 267);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_analysis;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 267);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(137, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "碰撞统计";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(143, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(336, 406);
            this.layoutControlGroup3.Text = "碰撞统计";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gc_CrossPipeStats;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(330, 380);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "碰撞管线列表";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup4.Location = new System.Drawing.Point(479, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(171, 406);
            this.layoutControlGroup4.Text = "碰撞管线列表";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lbx_CrossPipe;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(165, 380);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "碰撞类型";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(143, 61);
            this.layoutControlGroup5.Text = "碰撞类型";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(137, 35);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmPipelineCross
            // 
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(650, 406);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPipelineCross";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "碰撞分析";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPipelineCross_FormClosing);
            this.Load += new System.EventHandler(this.FrmPipelineCross_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private Dictionary<MajorClass, List<IRowBuffer>> _dict;
        private DataTable _dt1;
        private DataTable _dt2;
        private AxRenderControl _3DControl;
        private Dictionary<string, double> dictHorizon;
        private Dictionary<string, double> dictVertical;
        private Dictionary<string, double> dictDepth;
        private string _sysNameDiameter;
        private string _sysNameHLB;
        public FrmPipelineCross(Dictionary<MajorClass, List<IRowBuffer>> dict)
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null)
            {
                this.Enabled = false;
                return;
            }
            this._3DControl = app.Current3DMapControl;
            this._dict = dict;
            _dt1 = new DataTable();
            _dt2 = new DataTable();

            FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("PipeLine");
            if (fac != null)
            {
                this._sysNameDiameter = fac.GetFieldInfoNameBySystemName("Diameter");
                this._sysNameHLB = fac.GetFieldInfoNameBySystemName("HLB");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FrmPipelineCross_Load(object sender, EventArgs e)
        {
            if (this._dict != null && this._dict.Count != null) 
            {
                _dt1.Columns.AddRange(new DataColumn[] { new DataColumn("MC"), new DataColumn("Num") });
                this.gc_pipeNumStats.DataSource = _dt1;

                foreach(KeyValuePair<MajorClass, List<IRowBuffer>> kv in this._dict)
                {
                    DataRow dr = _dt1.NewRow();
                    dr["MC"] = kv.Key.ToString();
                    dr["Num"] = kv.Value.Count;
                    _dt1.Rows.Add(dr);
                }

                _dt2.Columns.Add(new DataColumn("类型"));
                DevExpress.XtraGrid.Columns.GridColumn gridColumn0 = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn0.Caption = "类型";
                gridColumn0.FieldName = "类型";
                gridColumn0.Name = "gcType";
                gridColumn0.OptionsColumn.AllowEdit = false;
                gridColumn0.Visible = true;
                this.gridView2.Columns.Add(gridColumn0);
                foreach (MajorClass mc in this._dict.Keys)
                {
                    DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn.Caption = mc.ToString();
                    gridColumn.FieldName = mc.ToString();
                    gridColumn.Name = mc.Name;
                    gridColumn.OptionsColumn.AllowEdit = false;
                    gridColumn.Visible = true;
                    this.gridView2.Columns.Add(gridColumn);

                    _dt2.Columns.Add(new DataColumn(mc.ToString()));
                }
                DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn1.Caption = "dict";
                gridColumn1.FieldName = "dict";
                gridColumn1.Name = "gcDict";
                gridColumn1.OptionsColumn.AllowEdit = false;
                gridColumn1.Visible = false;
                this.gridView2.Columns.Add(gridColumn1);
                _dt2.Columns.Add(new DataColumn("dict", typeof(object)));
                this.gc_CrossPipeStats.DataSource = _dt2;
            }
        }

        private void btn_analysis_Click(object sender, EventArgs e)
        {
            _dt2.Rows.Clear();
            try
            {
                WaitForm.Start("开始分析...", "请稍后", new Size(350, 40));
                int crossType = this.radioGroup1.SelectedIndex;
                if (crossType == 1)
                {
                    GetCrossDisRules();
                }
                CrossAnalysis(crossType);
                WaitForm.Stop();
            }
            catch (Exception ex) { }
        }

        private void CrossAnalysis(int crossType)
        {
            if (this._dict == null || this._dict.Count == 0) return;
            
            try
            {
                foreach (MajorClass mca in this._dict.Keys)
                {
                    List<IRowBuffer> linesA = this._dict[mca];
                    DataRow dr = _dt2.NewRow();
                    dr["类型"] = mca;
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach (MajorClass mcb in this._dict.Keys)
                    {
                        List<IRowBuffer> linesB = this._dict[mcb];
                        int count = 0;
                        int n = 0;
                        foreach (IRowBuffer la in linesA)
                        {
                            count++;
                            WaitForm.SetCaption("正在分析【" + mca.ToString() + "】与【" + mcb.ToString() + "】，" + count + "/" + linesA.Count);
                            int oidA = GetOid(la);
                            double dA = GetDiameter(la);
                            IPolyline lineA = GetShape(la);
                            IPolyline lineFootA = GetFootPrint(la);
                            int hlbA = GetHLB(la);
                            if (lineA == null || lineFootA == null) continue;
                            foreach (IRowBuffer lb in linesB)
                            {
                                bool bInter = false;
                                int oidB = GetOid(lb);
                                double dB = GetDiameter(lb);
                                IPolyline lineB = GetShape(lb);
                                IPolyline lineFootB = GetFootPrint(lb);
                                int hlbB = GetHLB(lb);
                                if (lineB == null || lineFootB == null) continue;
                                IGeometry geoInter = (lineFootA as ITopologicalOperator2D).Intersection2D(lineFootB);
                                if (geoInter == null) continue;
                                if (geoInter.GeometryType != gviGeometryType.gviGeometryPoint) continue;
                                IPoint intersectPoint = geoInter as IPoint;
                                double z1 = lineA.StartPoint.Z + (lineA.EndPoint.Z - lineA.StartPoint.Z)
                                    * Math.Sqrt((lineFootA.StartPoint.X - intersectPoint.X) * (lineFootA.StartPoint.X - intersectPoint.X)
                                     + (lineFootA.StartPoint.Y - intersectPoint.Z) * (lineFootA.StartPoint.Y - intersectPoint.Y)) / lineFootA.Length;

                                double z2 = lineB.StartPoint.Z + (lineB.EndPoint.Z - lineB.StartPoint.Z)
                                    * Math.Sqrt((lineFootB.StartPoint.X - intersectPoint.X) * (lineFootB.StartPoint.X - intersectPoint.X)
                                     + (lineFootB.StartPoint.Y - intersectPoint.Z) * (lineFootB.StartPoint.Y - intersectPoint.Y)) / lineFootB.Length;
                                double detz = z1 - z2;
                                if (crossType == 1)
                                {
                                    if (dictVertical.ContainsKey(mca.Name + "_" + mcb.Name))
                                        detz = Math.Abs(detz) - dictVertical[mca.Name + "_" + mcb.Name];
                                }
                                if (Math.Abs(detz) < 0.00000001)
                                {
                                    // 添加记录
                                    bInter = true;
                                }
                                else if (dA != double.NaN && dB != double.NaN)
                                {
                                    if (hlbA == 0 || hlbB == 0)
                                    {
                                        if (z1 > z2)
                                        {
                                            detz = z1 - dA - z2;
                                            if (crossType == 1)
                                            {
                                                if (dictVertical.ContainsKey(mca.Name + "_" + mcb.Name)) 
                                                    detz = detz - dictVertical[mca.Name + "_" + mcb.Name];
                                            }
                                            if (detz < 0)
                                            {
                                                // 添加记录
                                                bInter = true;

                                            }
                                        }
                                        else
                                        {
                                            detz = z2 - dB - z1;
                                            if (crossType == 1)
                                            {
                                                if (dictVertical.ContainsKey(mca.Name + "_" + mcb.Name))
                                                    detz = detz - dictVertical[mca.Name + "_" + mcb.Name];
                                            }
                                            if (detz < 0)
                                            {
                                                // 添加记录
                                                bInter = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (hlbA == 1 && hlbB == 1)//内底
                                        {
                                            if (z1 > z2) detz = z1 - (z2 + dB);
                                            else detz = z2 - (z1 + dA);
                                        }
                                        else if (hlbA == 1 && hlbB == -1)
                                        {
                                            if (z1 > z2) detz = z1 - z2;
                                            else detz = z2 - dB - (z1 + dA);
                                        }
                                        else if (hlbA == -1 && hlbB == 1)
                                        {
                                            if (z1 > z2) detz = z1 - dA - (z2 + dB);
                                            else detz = z2 - z1;
                                        }
                                        else if (hlbA == -1 && hlbB == -1)//外顶
                                        {
                                            if (z1 > z2) detz = z1 - dA - z2;
                                            else detz = z2 - dB - z1;
                                        }
                                        if (crossType == 1)
                                        {
                                            if (dictVertical.ContainsKey(mca.Name + "_" + mcb.Name))
                                                detz = detz - dictVertical[mca.Name + "_" + mcb.Name];
                                        }
                                        if (detz < 0)
                                        {
                                            // 添加记录
                                            bInter = true;
                                        }
                                    }
                                }
                                if (bInter)
                                {
                                    string idA = mca.Name + "_" + oidA;
                                    string idB = mcb.Name + "_" + oidB;
                                    if (!dict.ContainsKey(idB))
                                    {
                                        dict[idB] = idA;
                                        if (dict.ContainsKey(idA) && dict[idA] == idB)
                                        {
                                            dict.Remove(idB);
                                        }
                                        else n++;
                                    }
                                }
                            }
                        }
                        dr[mcb.ToString()] = n;
                    }
                    dr["dict"] = dict;
                    _dt2.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private int GetOid(IRowBuffer row)
        {
            if (row == null) return -1;
            int index = row.Fields.IndexOf("oid");
            if (index == -1 || row.IsNull(index)) return -1;
            return int.Parse(row.GetValue(index).ToString());
        }

        private double GetDiameter(IRowBuffer row) 
        {
            if (row == null) return double.NaN;
            int index = row.Fields.IndexOf(this._sysNameDiameter);
            if (index == -1 || row.IsNull(index)) return double.NaN;
            string d = row.GetValue(index).ToString();
            double h = 0.0;
            if (d.Contains("*"))
            {
                int n = d.IndexOf("*");
                string strd = d.Substring(n + 1);
                double.TryParse(strd, out h);
                h = h * 0.001;
            }
            else
            {
                double.TryParse(d, out h);
                h = h * 0.001;
            }
            return h;
        }

        private int GetHLB(IRowBuffer row)
        {
            if (row == null) return 0;
            int index = row.Fields.IndexOf(this._sysNameHLB);
            if (index == -1 || row.IsNull(index)) return 0;
            string d = row.GetValue(index).ToString();
            int h = 0;
            if (d.Contains("外"))
            {
                h = -1;
            }
            else if (d.Contains("内"))
            {
                h = 1;
            }
            return h;
        }

        private IPolyline GetFootPrint(IRowBuffer row)
        {
            if (row == null) return null;
            int index = row.Fields.IndexOf("FootPrint");
            if (index == -1 || row.IsNull(index)) return null;
            IGeometry geo = row.GetValue(index) as IGeometry;
            if (geo.GeometryType == gviGeometryType.gviGeometryPolyline) return geo as IPolyline;
            return null;
        }

        private IPolyline GetShape(IRowBuffer row)
        {
            if (row == null) return null;
            int index = row.Fields.IndexOf("Shape");
            if (index == -1 || row.IsNull(index)) return null;
            IGeometry geo = row.GetValue(index) as IGeometry;
            if (geo.GeometryType == gviGeometryType.gviGeometryPolyline) return geo as IPolyline;
            return null;
        }

        private void GetCrossDisRules()
        {
            string verticalDisRule = Config.GetConfigValue("VerticalDisRule");
            string horizonDisRule = Config.GetConfigValue("HorizonDisRule");
            string depthRule = Config.GetConfigValue("DepthRule");
            dictHorizon = new Dictionary<string, double>();
            dictVertical = new Dictionary<string, double>();
            dictDepth = new Dictionary<string, double>();
            string[] horizonArray = horizonDisRule.Split('|');
            string[] verticalArray = verticalDisRule.Split('|');
            string[] depthArray = depthRule.Split('|');
            foreach (string d in depthArray)
            {
                int index = d.IndexOf(':');
                double temp;
                Double.TryParse(d.Substring(index + 1), out temp);
                switch (d.Substring(0, index - 1))
                {
                    case "电力":
                        dictDepth["DL"] = temp;
                        break;
                    case "通讯":
                        dictDepth["TX"] = temp;
                        break;
                    case "上水":
                        dictDepth["GS"] = temp;
                        break;
                    case "下水":
                        dictDepth["PS"] = temp;
                        break;
                    case "燃气":
                        dictDepth["RQ"] = temp;
                        break;
                    case "热力":
                        dictDepth["RL"] = temp;
                        break;
                    case "工业气体":
                        dictDepth["GYQT"] = temp;
                        break;
                    case "工业水管":
                        dictDepth["GYSG"] = temp;
                        break;
                    case "工业化工":
                        dictDepth["GYHG"] = temp;
                        break;
                    case "工业其他":
                        dictDepth["GYQT"] = temp;
                        break;
                }
            }
            foreach (string v in verticalArray)
            {
                string[] mc = v.Split(',');
                switch (mc[0])
                {
                    case "电力":
                        BuildDict(mc, "DL", ref dictVertical);
                        break;
                    case "通讯":
                        BuildDict(mc, "TX", ref dictVertical);
                        break;
                    case "上水":
                        BuildDict(mc, "GS", ref dictVertical);
                        break;
                    case "下水":
                        BuildDict(mc, "PS", ref dictVertical);
                        break;
                    case "燃气":
                        BuildDict(mc, "RQ", ref dictVertical);
                        break;
                    case "热力":
                        BuildDict(mc, "RL", ref dictVertical);
                        break;
                    case "工业气体":
                        BuildDict(mc, "GYQT", ref dictVertical);
                        break;
                    case "工业水管":
                        BuildDict(mc, "GYSG", ref dictVertical);
                        break;
                    case "工业化工":
                        BuildDict(mc, "GYHG", ref dictVertical);
                        break;
                    case "工业其他":
                        BuildDict(mc, "GYQT", ref dictVertical);
                        break;
                }
            }
            foreach (string h in horizonArray)
            {
                string[] mc = h.Split(',');
                switch (mc[0])
                {
                    case "电力":
                        BuildDict(mc, "DL", ref dictHorizon);
                        break;
                    case "通讯":
                        BuildDict(mc, "TX", ref dictHorizon);
                        break;
                    case "上水":
                        BuildDict(mc, "GS", ref dictHorizon);
                        break;
                    case "下水":
                        BuildDict(mc, "PS", ref dictHorizon);
                        break;
                    case "燃气":
                        BuildDict(mc, "RQ", ref dictHorizon);
                        break;
                    case "热力":
                        BuildDict(mc, "RL", ref dictHorizon);
                        break;
                    case "工业气体":
                        BuildDict(mc, "GYQT", ref dictHorizon);
                        break;
                    case "工业水管":
                        BuildDict(mc, "GYSG", ref dictHorizon);
                        break;
                    case "工业化工":
                        BuildDict(mc, "GYHG", ref dictHorizon);
                        break;
                    case "工业其他":
                        BuildDict(mc, "GYQT", ref dictHorizon);
                        break;
                }
            }
        }

        private void BuildDict(string[] mc, string row, ref Dictionary<string, double> dict)
        {
            string[] Col = new string[] { "DL", "TX", "GS", "PS", "RQ", "RL", "GYQT", "GYSG", "GYHG", "GYQT" };
            for (int i = 1; i < mc.Length; i++)
            {
                double temp = Double.MaxValue;
                Double.TryParse(mc[i], out temp);
                if (dict.ContainsKey(row + "_" + Col[i - 1])) continue;
                dict[row + "_" + Col[i - 1]] = temp;
            }

        }


        private ITableLabel _tl;
        private void lbx_CrossPipe_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._tl != null)
                {
                    this._3DControl.ObjectManager.DeleteObject(this._tl.Guid);
                    this._tl = null;
                }
                if (this.lbx_CrossPipe.SelectedItem == null) return;
                string temp = this.lbx_CrossPipe.SelectedItem.ToString();
                string lineA = temp.Substring(0, (temp.IndexOf(",")));
                string lineB = temp.Substring(temp.IndexOf(",") + 1);
                if (lineA == null || lineB == null) return;
                string mcNameA = lineA.Substring(0, lineA.IndexOf("_"));
                string fOidA = lineA.Substring(lineA.IndexOf("_") + 1);
                string mcNameB = lineB.Substring(0, lineB.IndexOf("_"));
                string fOidB = lineB.Substring(lineB.IndexOf("_") + 1);
                int OIDA = -1;
                int OIDB = -1;
                Int32.TryParse(fOidA, out OIDA);
                Int32.TryParse(fOidB, out OIDB);
                if(OIDA == -1 || OIDB == -1) return;
                MajorClass mcA = LogicDataStructureManage3D.Instance.GetMajorClassByName(mcNameA);
                MajorClass mcB = LogicDataStructureManage3D.Instance.GetMajorClassByName(mcNameB);
                if (mcA == null || mcB == null) return;
                string[] arrFc3DIdA = mcA.Fc3D.Split(';');
                if (arrFc3DIdA == null) return;
                IPolyline lineFootA = null;
                IPolyline lineShapeA = null;
                IModelPoint mpA = null;
                IPolyline lineFootB = null;
                IPolyline lineShapeB = null;
                IModelPoint mpB = null;
                #region
                foreach (string str in arrFc3DIdA)
                {
                    DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(str);
                    if (dffc == null) continue;
                    FacilityClass fac = dffc.GetFacilityClass();
                    if (fac != null && fac.Name == "PipeLine")
                    {
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc != null)
                        {
                            IRowBuffer row = null;
                            IFdeCursor cursor = null;
                            try
                            {
                                IQueryFilter filter = new QueryFilter();
                                filter.WhereClause = "oid=" + OIDA;
                                filter.SubFields = "oid,Shape,FootPrint,Geometry";
                                cursor = fc.Search(filter, true);
                                if (cursor != null)
                                {
                                    row = cursor.NextRow();
                                    if (!row.IsNull(1))
                                    {
                                        IGeometry geo = row.GetValue(1) as IGeometry;
                                        if (geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                                        {
                                            lineShapeA = geo as IPolyline;
                                        }
                                    }
                                    if (!row.IsNull(2))
                                    {
                                        IGeometry geo = row.GetValue(2) as IGeometry;
                                        if (geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                                        {
                                            lineFootA = geo as IPolyline;
                                        }
                                    }
                                    if (!row.IsNull(3))
                                    {
                                        IGeometry geo = row.GetValue(3) as IGeometry;
                                        if (geo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                                        {
                                            mpA = geo as IModelPoint;
                                            IModelPointSymbol mps = new ModelPointSymbol();
                                            mps.SetResourceDataSet(fc.FeatureDataSet);
                                            IRenderGeometry render = this._3DControl.ObjectManager.CreateRenderModelPoint(mpA, mps, this._3DControl.ProjectTree.RootID);
                                            render.Glow(4000);
                                            this._3DControl.ObjectManager.DelayDelete(render.Guid, 5000);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            finally
                            {
                                if (cursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                    cursor = null;
                                }
                                if (row != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                                    row = null;
                                }
                            }
                        }
                        break;
                    }
                }
                string[] arrFc3DIdB = mcB.Fc3D.Split(';');
                if (arrFc3DIdB == null) return;
                foreach (string str in arrFc3DIdB)
                {
                    DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(str);
                    if (dffc == null) continue;
                    FacilityClass fac = dffc.GetFacilityClass();
                    if (fac != null && fac.Name == "PipeLine")
                    {
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc != null)
                        {
                            IRowBuffer row = null;
                            IFdeCursor cursor = null;
                            try
                            {
                                IQueryFilter filter = new QueryFilter();
                                filter.WhereClause = "oid=" + OIDB;
                                filter.SubFields = "oid,Shape,FootPrint,Geometry";
                                cursor = fc.Search(filter, true);
                                if (cursor != null)
                                {
                                    row = cursor.NextRow();
                                    if (!row.IsNull(1))
                                    {
                                        IGeometry geo = row.GetValue(1) as IGeometry;
                                        if (geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                                        {
                                            lineShapeB = geo as IPolyline;
                                        }
                                    }
                                    if (!row.IsNull(2))
                                    {
                                        IGeometry geo = row.GetValue(2) as IGeometry;
                                        if (geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                                        {
                                            lineFootB = geo as IPolyline;
                                        }
                                    }
                                    if (!row.IsNull(3))
                                    {
                                        IGeometry geo = row.GetValue(3) as IGeometry;
                                        if (geo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                                        {
                                            mpB = geo as IModelPoint;
                                            IModelPointSymbol mps = new ModelPointSymbol();
                                            mps.SetResourceDataSet(fc.FeatureDataSet);
                                            IRenderGeometry render = this._3DControl.ObjectManager.CreateRenderModelPoint(mpB, mps, this._3DControl.ProjectTree.RootID);
                                            render.Glow(4000);
                                            this._3DControl.ObjectManager.DelayDelete(render.Guid, 5000);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            finally
                            {
                                if (cursor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                    cursor = null;
                                }
                                if (row != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                                    row = null;
                                }
                            }
                        }
                        break;
                    }
                }
                #endregion
                if (lineFootA == null || lineShapeA == null || lineFootB == null || lineShapeB == null) return;
                IGeometry geoInter = (lineFootA as ITopologicalOperator2D).Intersection2D(lineFootB);
                if (geoInter == null) return;
                if (geoInter.GeometryType != gviGeometryType.gviGeometryPoint) return;
                IPoint intersectPoint = geoInter as IPoint;
                double zA = lineShapeA.StartPoint.Z + (lineShapeA.EndPoint.Z - lineShapeA.StartPoint.Z)
                    * Math.Sqrt((lineFootA.StartPoint.X - intersectPoint.X) * (lineFootA.StartPoint.X - intersectPoint.X)
                     + (lineFootA.StartPoint.Y - intersectPoint.Z) * (lineFootA.StartPoint.Y - intersectPoint.Y)) / lineFootA.Length;

                double zB = lineShapeB.StartPoint.Z + (lineShapeB.EndPoint.Z - lineShapeB.StartPoint.Z)
                    * Math.Sqrt((lineFootB.StartPoint.X - intersectPoint.X) * (lineFootB.StartPoint.X - intersectPoint.X)
                     + (lineFootB.StartPoint.Y - intersectPoint.Z) * (lineFootB.StartPoint.Y - intersectPoint.Y)) / lineFootB.Length;
                double detz = Math.Abs(zA - zB);
                double z = (zA + zB) / 2;
                IPoint pt = intersectPoint.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                pt.Z = z + detz;

                this._tl = DrawTool.CreateTableLabel1(1);               
                this._tl.TitleText = "碰撞点";
                this._tl.SetRecord(0, 0, temp);
                this._tl.Position = pt;
                this._3DControl.Camera.FlyToObject(this._tl.Guid, gviActionCode.gviActionFlyTo);
                this.Location = new System.Drawing.Point(0, 0);
                this.Size = new Size(450, 300);
            }
            catch (Exception ex)
            {

            }

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.lbx_CrossPipe.Items.Clear();
            if (e.FocusedRowHandle == -1) return;
            DataRow dr = this.gridView2.GetDataRow(e.FocusedRowHandle);
            if (dr != null && dr["dict"] != null)
            {
                Dictionary<string, string> dict = dr["dict"] as Dictionary<string, string>;
                foreach (KeyValuePair<string, string> kv in dict)
                {
                    this.lbx_CrossPipe.Items.Add(kv.Key + "," + kv.Value);
                }
            }
        }

        private void FrmPipelineCross_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._tl != null)
            {
                this._3DControl.ObjectManager.DeleteObject(this._tl.Guid);
                this._tl = null;
            }
        }
    }
}
