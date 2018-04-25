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
using DF2DAnalysis.Class;

namespace DF2DAnalysis.Frm
{
    public partial class FrmPipelineCross : XtraForm
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
        Dictionary<string, List<IFeature>> _lines;
        HashSet<string> hsCross;
        CrossAnalysis crossAnalysis;
        string _diameter;
        IGeometry _geo;
        DF2DApplication app = DF2DApplication.Application;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        Dictionary<string, double> dictHorizon;
        Dictionary<string, double> dictVertical;
        Dictionary<string, double> dictDepth;
        
    
        public FrmPipelineCross()
        {
            InitializeComponent();
            
        }
        public FrmPipelineCross(IGeometry geo,string diameter,string sysname,Dictionary<string, List<IFeature>> lines)
        {
            InitializeComponent();
            hsCross = new HashSet<string>();
            crossAnalysis = new CrossAnalysis(diameter, sysname,ref hsCross);
            this._geo = geo;
            this._lines = lines;
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
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
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
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
            this.layoutControl1.Size = new System.Drawing.Size(650, 407);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(15, 35);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "硬碰撞"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "软碰撞")});
            this.radioGroup1.Size = new System.Drawing.Size(166, 54);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 10;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(15, 370);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(166, 22);
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
            this.lbx_CrossPipe.Size = new System.Drawing.Size(129, 357);
            this.lbx_CrossPipe.StyleController = this.layoutControl1;
            this.lbx_CrossPipe.TabIndex = 8;
            this.lbx_CrossPipe.DoubleClick += new System.EventHandler(this.lbx_CrossPipe_DoubleClick);
            // 
            // gc_CrossPipeStats
            // 
            this.gc_CrossPipeStats.Location = new System.Drawing.Point(191, 35);
            this.gc_CrossPipeStats.MainView = this.gridView2;
            this.gc_CrossPipeStats.Name = "gc_CrossPipeStats";
            this.gc_CrossPipeStats.Size = new System.Drawing.Size(305, 357);
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
            this.btn_analysis.Size = new System.Drawing.Size(166, 22);
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
            this.gc_pipeNumStats.Size = new System.Drawing.Size(166, 221);
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
            this.layoutControlGroup2.CustomizationFormText = "区域内管线数量统计";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 84);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(176, 303);
            this.layoutControlGroup2.Text = "区域内管线数量统计";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gc_pipeNumStats;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(170, 225);
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
            this.layoutControlItem3.Size = new System.Drawing.Size(170, 26);
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
            this.layoutControlItem6.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "碰撞统计";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(176, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(315, 387);
            this.layoutControlGroup3.Text = "碰撞统计";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gc_CrossPipeStats;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(309, 361);
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
            this.layoutControlItem5.Size = new System.Drawing.Size(133, 361);
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
            this.layoutControlGroup5.Size = new System.Drawing.Size(176, 84);
            this.layoutControlGroup5.Text = "碰撞类型";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(170, 58);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmPipelineCross
            // 
            this.ClientSize = new System.Drawing.Size(650, 407);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmPipelineCross";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管线碰撞分析";
            this.Load += new System.EventHandler(this.FrmPipelineCross_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmPipelineCross_Load(object sender, EventArgs e)
        {
            DataTable dtNum = new DataTable();
           
            dtNum.Columns.AddRange(new DataColumn[] { new DataColumn("图层"), new DataColumn("管线数量") });
            try
            {
                foreach (KeyValuePair<string, List<IFeature>> v in _lines)
                {
                    DataRow dr = dtNum.NewRow();
                    dr["图层"] = v.Key;
                    dr["管线数量"] = v.Value.Count;
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
            WaitForm.Start("开始分析...请稍后...", new Size(300, 40));
            this.lbx_CrossPipe.Items.Clear();
            hsCross.Clear();
            crossAnalysis.CrossType = this.radioGroup1.SelectedIndex;
            crossAnalysis.GetAllPipelineCrossByGeo(_geo);
            HashSet<string> hsRB = ReBuildHashSet();
            this.gc_CrossPipeStats.DataSource = BuildDataTable(hsRB);
            this.lbx_CrossPipe.Items.AddRange(this.hsCross.ToArray());
            WaitForm.Stop();                 
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
        private DataTable BuildDataTable(HashSet<string> hs)
        {
            DataTable dt = new DataTable();
            int n = 0;
            dt.Columns.Add(new DataColumn("图层"));
            if (_lines == null || hsCross == null) return null;
            foreach (string s in _lines.Keys)
            {
                dt.Columns.Add(new DataColumn(s, typeof(int)));
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
                    int mcblength = cross.LastIndexOf('_') - cross.IndexOf(',') - 1;
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
            return dt;
        }
        private string GetAlias(string name)
        {
            switch (name)
            {
                case "DL":
                    return "电力";
                case "TX":
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
        /*private DataTable DoCrossAnalysis(Dictionary<string, List<IFeature>> dict,int crossType)
        {
            if (dict.Count <= 0) return null;
            //dicCross = new Dictionary<string,string>();
            hsCross = new HashSet<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("图层"));
            foreach (string s in dict.Keys)
            {
                dt.Columns.Add(new DataColumn(s));
            }
            try
            {
                foreach (MajorClass mca in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                {
                    if (!dict.ContainsKey(mca.Alias)) continue;
                    List<IFeature> linesA = dict[mca.Alias];
                    DataRow dr = dt.NewRow();
                    dr["图层"] = mca.Alias;
                    foreach (MajorClass mcb in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                    {
                        if (!dict.ContainsKey(mcb.Alias)) continue;
                        List<IFeature> linesB = dict[mcb.Alias];
                        int count = 0;

                        //if (linesB == linesA) continue;

                        int n = 0;
                        foreach (IFeature la in linesA)
                        {
                            count++;                           
                            WaitForm.SetCaption("正在分析【" + mca.Alias + "】与【" + mcb.Alias + "】，" + count + "/" + linesA.Count);
                            int dA = GetDiameter(la, this._diameter);
                            IPolyline lineA = la.Shape as IPolyline;
                            foreach (IFeature lb in linesB)
                            {
                                if (la == lb) continue;
                                int dB = GetDiameter(lb, this._diameter);
                                IPolyline lineB = lb.Shape as IPolyline;
                                //ITopologicalOperator topo = lineA as ITopologicalOperator;
                                ITopologicalOperator topo = lineA as ITopologicalOperator;
                                topo.Simplify();
                                IGeometry geo = topo.Intersect(lineB, esriGeometryDimension.esriGeometry0Dimension);
                                if (geo.IsEmpty) continue;
                                IPointCollection pc = geo as IPointCollection;
                                IPoint point = pc.get_Point(0);
                                //if (point == lineA.FromPoint || point == lineA.ToPoint || point == lineB.FromPoint || point == lineB.ToPoint)
                                if(IsPointSame(point,lineA.FromPoint)||IsPointSame(point,lineA.ToPoint)||IsPointSame(point,lineB.FromPoint)||IsPointSame(point,lineB.ToPoint))
                                    continue;
                                double disA = double.MaxValue;
                                double zA = double.MaxValue;
                                GetDistanceAndZ(lineA, point, out disA, out zA);
                                double disB = double.MaxValue;
                                double zB = double.MaxValue;
                                GetDistanceAndZ(lineB, point, out disB, out zB);
                                double l = Double.MaxValue;
                                if (crossType == 0)
                                {
                                    l = Convert.ToDouble((dA + dB) / 2000);
                                }
                                else
                                {
                                    if (dictVertical.ContainsKey(mca.Name + "_" + mcb.Name))
                                        

                                        l = (((double)dA +(double) dB) / 2000) + dictVertical[mca.Name + "_" + mcb.Name];
                                }
                                if (l == Double.MaxValue) continue;
                                if (System.Math.Abs(zA - zB) > l) continue;
                                else
                                {
                                    string idA = mca.Name + "_" + la.OID;
                                    string idB = mcb.Name + "_" + lb.OID;
                                    hsCross.Add(idA + "," + idB);
                                    n++;
                                    //if (dicCross.ContainsValue(idA) && dicCross.ContainsKey(idB))
                                    //{
                                    //    if (dicCross[idB] == idA) continue;
                                     
                                    //}
                                    //else
                                    //{
                                    //    if (dicCross.ContainsKey(idA)) continue;
                                    //    dicCross[idA] = idB;
                                    //    n++;

                                    //}
                                    //cross.Add(idA + "," + idB);
                                    //n++;
                                }
                            }
                        }
                        dr[mcb.Alias] = n;

                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }

        private bool IsPointSame(IPoint p1, IPoint p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z) return true;
            else return false;
        }
        private void GetDistanceAndZ(IPolyline line, IPoint point, out double dis, out double z)
        {
            if (line == null || point == null)
            {
                dis = double.MaxValue;
                z = double.MaxValue;
                return;
            }
            else
            {
                dis = System.Math.Sqrt((point.X - line.FromPoint.X) * (point.X - line.FromPoint.X) + (point.Y - line.FromPoint.Y) * (point.Y - line.FromPoint.Y));
                z = dis * (line.ToPoint.Z - line.FromPoint.Z) / line.Length + line.FromPoint.Z;
            }

        }

        private int GetDiameter(IFeature feature, string fieldName)
        {
            IFields fields = feature.Fields;
            int index = fields.FindField(this._diameter);
            string d = feature.get_Value(index).ToString();
            int h;
            if (d.Contains("*"))
            {
                int n = d.IndexOf("*");
                string strd = d.Substring(n + 1);
                System.Int32.TryParse(strd, out h);
            }
            else
            {
                System.Int32.TryParse(d, out h);
            }
            return h;
        }*/

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
                                //app.Current2DMapControl.ActiveView.FocusMap.SelectFeature(fl, feature2);
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

        /*private void GetCrossDisRules()
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
                Double.TryParse(d.Substring(index + 1),out temp);
                string mcName = d.Substring(0, index);
                switch (mcName)
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
                        dictDepth["GYQTT"] = temp;
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
                        BuildDict(mc, "GYQTT", ref dictVertical);
                        break;
                }  
            }
            foreach (string h in horizonArray)
            {
                string[] mc = h.Split(',');
                switch (mc[0])
                {
                    case "电力":
                        BuildDict(mc,"DL",ref dictHorizon);
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
                        BuildDict(mc,"GYQT", ref dictHorizon);
                        break;
                    case "工业水管":
                        BuildDict(mc, "GYSG", ref dictHorizon);
                        break;
                    case "工业化工":
                        BuildDict(mc, "GYHG", ref dictHorizon);
                        break;
                    case "工业其他":
                        BuildDict(mc, "GYQTT", ref dictHorizon);
                        break;
                }                                                   
            }           
        }
       
        private void BuildDict(string[] mc, string row,ref Dictionary<string,double> dict)
        {
            string[] Col = new string[] { "DL", "TX", "GS", "PS", "RQ", "RL", "GYQT", "GYSG", "GYHG", "GYQTT" };
            for(int i = 1; i< mc.Length;i++)
            {
                double temp = Double.MaxValue;
                Double.TryParse(mc[i],out temp);
                if (dict.ContainsKey(row + "_" + Col[i - 1])) continue;
                dict[row + "_" + Col[i - 1]] = temp;
            }
           
        }*/
    }
}
