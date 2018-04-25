using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace DF3DPipe.Stats.UC
{
    public partial class UCPipeLineStatsChart : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private ChartControl chartControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private ChartControl chartControl1;
    
        public UCPipeLineStatsChart()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PiePointOptions piePointOptions1 = new DevExpress.XtraCharts.PiePointOptions();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartControl2);
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(523, 435);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartControl2
            // 
            this.chartControl2.Location = new System.Drawing.Point(5, 261);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            piePointOptions1.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.General;
            pieSeriesLabel1.PointOptions = piePointOptions1;
            this.chartControl2.SeriesTemplate.Label = pieSeriesLabel1;
            pieSeriesView1.ExplodeMode = DevExpress.XtraCharts.PieExplodeMode.MinValue;
            pieSeriesView1.RuntimeExploding = true;
            this.chartControl2.SeriesTemplate.View = pieSeriesView1;
            this.chartControl2.Size = new System.Drawing.Size(513, 169);
            this.chartControl2.TabIndex = 5;
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(5, 25);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(513, 206);
            this.chartControl1.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(523, 435);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "柱状图";
            this.layoutControlGroup3.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup3.ExpandButtonVisible = true;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(523, 236);
            this.layoutControlGroup3.Text = "柱状图";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chartControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(517, 210);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "饼状图";
            this.layoutControlGroup2.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup2.ExpandButtonVisible = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 236);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(523, 199);
            this.layoutControlGroup2.Text = "饼状图";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chartControl2;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(517, 173);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // UCPipeLineStatsChart
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPipeLineStatsChart";
            this.Size = new System.Drawing.Size(523, 435);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dt;

        public void SetData(DataTable dt)
        {
            this._dt = dt;
            if (this._dt == null || this._dt.Rows.Count == 0) return;
            try
            {
                #region chart1
                this.chartControl1.Titles.Clear();
                this.chartControl1.Series.Clear();
                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = "管线长度统计图 单位（米）";
                chartTitle1.Font = new System.Drawing.Font("宋体", 9);
                this.chartControl1.Titles.Add(chartTitle1);
                HashSet<string> hspvalue = new HashSet<string>();
                HashSet<string> hsptype = new HashSet<string>();
                foreach (DataRow dr in this._dt.Rows)
                {
                    hsptype.Add(dr["PIPELINETYPE"].ToString());
                    hspvalue.Add(dr["PVALUE"].ToString());
                }
                foreach (string pvalue in hspvalue)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add(new DataColumn("PIPELINETYPE", Type.GetType("System.String")));
                    dtTemp.Columns.Add(new DataColumn("LENGTH", Type.GetType("System.Double")));
                    foreach (string ptype in hsptype)
                    {
                        foreach (DataRow dr in this._dt.Rows)
                        {
                            if (dr["PIPELINETYPE"].ToString() == ptype && dr["PVALUE"].ToString() == pvalue)
                            {
                                DataRow dr1 = dtTemp.NewRow();
                                dr1["PIPELINETYPE"] = ptype;
                                dr1["LENGTH"] = Convert.ToDouble(dr["LENGTH"].ToString());
                                dtTemp.Rows.Add(dr1);
                            }
                        }
                    }
                    Series sr1 = new Series(pvalue, ViewType.Bar);
                    sr1.ArgumentScaleType = ScaleType.Qualitative;//定性的
                    sr1.ValueScaleType = ScaleType.Numerical;//数字类型 
                    sr1.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                    sr1.PointOptions.ValueNumericOptions.Precision = 2;//百分号前面的数字不跟小数点

                    //绑定数据源
                    sr1.DataSource = dtTemp;//newdtb是获取到的数据(可以是数据库中的表，也可以是DataTable)
                    sr1.ArgumentDataMember = "PIPELINETYPE";//绑定的文字信息（名称）(坐标横轴)
                    sr1.ValueDataMembers[0] = "LENGTH";//绑定的值（数据）(坐标纵轴)
                    sr1.ShowInLegend = true;
                    this.chartControl1.Series.Add(sr1);
                }
                this.chartControl1.Legend.Visible = true;
                #endregion
                #region chart2
                this.chartControl2.Titles.Clear();
                this.chartControl2.Series.Clear();
                ChartTitle chartTitle2 = new ChartTitle();
                chartTitle2.Text = "管线长度占比图";
                chartTitle2.Font = new System.Drawing.Font("宋体", 9);
                this.chartControl2.Titles.Add(chartTitle2);
                foreach (string ptype in hsptype)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add(new DataColumn("PVALUE", Type.GetType("System.String")));
                    dtTemp.Columns.Add(new DataColumn("LENGTH", Type.GetType("System.Double")));
                    foreach (string pvalue in hspvalue)
                    {
                        foreach (DataRow dr in this._dt.Rows)
                        {
                            if (dr["PIPELINETYPE"].ToString() == ptype && dr["PVALUE"].ToString() == pvalue)
                            {
                                DataRow dr1 = dtTemp.NewRow();
                                dr1["PVALUE"] = pvalue;
                                dr1["LENGTH"] = Convert.ToDouble(dr["LENGTH"].ToString());
                                dtTemp.Rows.Add(dr1);
                            }
                        }
                    }
                    Series sr2 = new Series(ptype, ViewType.Pie);
                    sr2.ArgumentScaleType = ScaleType.Qualitative;//定性的
                    sr2.ValueScaleType = ScaleType.Numerical;//数字类型 
                    sr2.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                    sr2.PointOptions.ValueNumericOptions.Precision = 2;//百分号前面的数字不跟小数点

                    //绑定数据源
                    sr2.DataSource = dtTemp;//newdtb是获取到的数据(可以是数据库中的表，也可以是DataTable)
                    sr2.ArgumentDataMember = "PVALUE";//绑定的文字信息（名称）(坐标横轴)
                    sr2.ValueDataMembers[0] = "LENGTH";//绑定的值（数据）(坐标纵轴)
                    sr2.ShowInLegend = true;
                    this.chartControl2.Series.Add(sr2);
                    SetPieExplode(this.chartControl2, sr2, PieExplodeMode.MinValue, 5, true);
                }
                this.chartControl2.Legend.Visible = false;
                #endregion
            }
            catch (Exception ex) { }

        }
        public void SetPieExplode(ChartControl chart, Series pieSeries, PieExplodeMode explodeMode, int explodedValue, bool dragPie)
        {
            if (pieSeries.View is PieSeriesView)
            {
                //bool _hitTesting = chart.RuntimeHitTesting;
                if (!chart.RuntimeHitTesting)
                    chart.RuntimeHitTesting = true;
                PieSeriesView _pieView = pieSeries.View as PieSeriesView;
                _pieView.ExplodeMode = explodeMode;
                _pieView.ExplodedDistancePercentage = explodedValue;
                _pieView.RuntimeExploding = dragPie;
                // chart.RuntimeHitTesting = _hitTesting;
            }
        }
        public void SetData1(DataTable dt)
        {
            this._dt = dt;
            if (this._dt == null || this._dt.Rows.Count == 0) return;
            try
            {
                #region chart1
                this.chartControl1.Titles.Clear();
                this.chartControl1.Series.Clear();
                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = "全库管线长度统计图 单位（米）";
                chartTitle1.Font = new System.Drawing.Font("宋体", 9);
                this.chartControl1.Titles.Add(chartTitle1);
                Series sr1 = new Series("全库管线长度统计图", ViewType.Bar);
                //设置Series样式
                sr1.ArgumentScaleType = ScaleType.Qualitative;//定性的
                sr1.ValueScaleType = ScaleType.Numerical;//数字类型 
                sr1.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                sr1.PointOptions.ValueNumericOptions.Precision = 2;//百分号前面的数字不跟小数点

                //绑定数据源
                sr1.DataSource = this._dt;//newdtb是获取到的数据(可以是数据库中的表，也可以是DataTable)
                sr1.ArgumentDataMember = "PVALUE";//绑定的文字信息（名称）(坐标横轴)
                sr1.ValueDataMembers[0] = "LENGTH";//绑定的值（数据）(坐标纵轴)
                this.chartControl1.Series.Add(sr1);
                this.chartControl1.Legend.Visible = false;
                #endregion
                #region chart2
                this.chartControl2.Titles.Clear();
                this.chartControl2.Series.Clear();
                ChartTitle chartTitle2 = new ChartTitle();
                chartTitle2.Text = "全库管线长度占比图";
                chartTitle2.Font = new System.Drawing.Font("宋体", 9);
                this.chartControl2.Titles.Add(chartTitle2);
                HashSet<string> hstype = new HashSet<string>();
                HashSet<double> hstt = new HashSet<double>();
                foreach (DataRow dr in this._dt.Rows)
                {
                    hstype.Add(dr["PIPELINETYPE"].ToString());
                    hstt.Add(Convert.ToDouble(dr["TOTALLENGTH"].ToString()));
                }
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add(new DataColumn("PIPELINETYPE", Type.GetType("System.String")));
                dtTemp.Columns.Add(new DataColumn("TOTALLENGTH", Type.GetType("System.Double")));
                List<string> listtype =  hstype.ToList<string>();
                List<double> listtt = hstt.ToList<double>();
                for (int i = 0; i < listtype.Count; i++)
                {
                    DataRow dr = dtTemp.NewRow();
                    dr["PIPELINETYPE"] = listtype[i];
                    dr["TOTALLENGTH"] = listtt[i];
                    dtTemp.Rows.Add(dr);
                }
                Series sr2 = new Series("全库管线长度占比图", ViewType.Pie);
                //设置Series样式
                sr2.ArgumentScaleType = ScaleType.Qualitative;//定性的
                sr2.ValueScaleType = ScaleType.Numerical;//数字类型 
                sr2.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                sr2.PointOptions.ValueNumericOptions.Precision = 2;
                //绑定数据源
                sr2.DataSource = dtTemp;//newdtb是获取到的数据(可以是数据库中的表，也可以是DataTable)
                sr2.ArgumentDataMember = "PIPELINETYPE";//绑定的文字信息（名称）(坐标横轴)
                sr2.ValueDataMembers[0] = "TOTALLENGTH";//绑定的值（数据）(坐标纵轴)
                this.chartControl2.Series.Add(sr2);
                SetPieExplode(this.chartControl2, sr2, PieExplodeMode.MinValue, 5, true);
                #endregion

            }
            catch (Exception ex) { }
        }


    }
}
