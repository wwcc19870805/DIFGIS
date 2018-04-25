using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;

namespace DF2DPipe.Stats.UC
{
    public partial class UCEconomyStatsChartOutput2D : UserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckedListBoxControl clbx_disList;
        private DevExpress.XtraEditors.SimpleButton btn_chartOutput;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbx_prop;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btn_Stats;
        private ChartControl chartControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    
        public UCEconomyStatsChartOutput2D()
        {
            InitializeComponent();
        }
        public UCEconomyStatsChartOutput2D(string sysFieldName,DataTable dt)
        {
            InitializeComponent();
            
        }

        private DataTable _dt;
        private DataTable _dtStats;
        private Dictionary<string, DataRow> dictRow;
        private string _sysFieldName;
        private ChartTitle _chartTitle;
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.btn_Stats = new DevExpress.XtraEditors.SimpleButton();
            this.clbx_disList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btn_chartOutput = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbx_prop = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbx_disList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_prop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Controls.Add(this.btn_Stats);
            this.layoutControl1.Controls.Add(this.clbx_disList);
            this.layoutControl1.Controls.Add(this.btn_chartOutput);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.cbx_prop);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(574, 357);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartControl1
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Location = new System.Drawing.Point(158, 25);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            series2.Name = "Series 2";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.chartControl1.Size = new System.Drawing.Size(411, 327);
            this.chartControl1.TabIndex = 12;
            // 
            // btn_Stats
            // 
            this.btn_Stats.Location = new System.Drawing.Point(5, 304);
            this.btn_Stats.Name = "btn_Stats";
            this.btn_Stats.Size = new System.Drawing.Size(143, 22);
            this.btn_Stats.StyleController = this.layoutControl1;
            this.btn_Stats.TabIndex = 11;
            this.btn_Stats.Text = "生成统计";
            this.btn_Stats.Click += new System.EventHandler(this.btn_Stats_Click);
            // 
            // clbx_disList
            // 
            this.clbx_disList.Location = new System.Drawing.Point(5, 25);
            this.clbx_disList.Name = "clbx_disList";
            this.clbx_disList.Size = new System.Drawing.Size(143, 249);
            this.clbx_disList.StyleController = this.layoutControl1;
            this.clbx_disList.TabIndex = 9;
            // 
            // btn_chartOutput
            // 
            this.btn_chartOutput.Location = new System.Drawing.Point(5, 330);
            this.btn_chartOutput.Name = "btn_chartOutput";
            this.btn_chartOutput.Size = new System.Drawing.Size(143, 22);
            this.btn_chartOutput.StyleController = this.layoutControl1;
            this.btn_chartOutput.TabIndex = 8;
            this.btn_chartOutput.Text = "图表输出";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 278);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "属性字段：";
            // 
            // cbx_prop
            // 
            this.cbx_prop.Location = new System.Drawing.Point(69, 278);
            this.cbx_prop.Name = "cbx_prop";
            this.cbx_prop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx_prop.Size = new System.Drawing.Size(79, 22);
            this.cbx_prop.StyleController = this.layoutControl1;
            this.cbx_prop.TabIndex = 6;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(574, 357);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "参数设置";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem7});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(153, 357);
            this.layoutControlGroup3.Text = "参数设置";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbx_prop;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(64, 253);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 253);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_chartOutput;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 305);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.clbx_disList;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(147, 253);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_Stats;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 279);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "柱状图";
            this.layoutControlGroup2.ExpandButtonMode = DevExpress.Utils.Controls.ExpandButtonMode.Inverted;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(153, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(421, 357);
            this.layoutControlGroup2.Text = "柱状图";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chartControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(415, 331);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // UCEconomyStatsChartOutput2D
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCEconomyStatsChartOutput2D";
            this.Size = new System.Drawing.Size(574, 357);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbx_disList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_prop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        public void SetData(string sysFieldName, DataTable dt)
        {
            this._sysFieldName = sysFieldName;
            this._dt = dt;
            Init(_dt, this._sysFieldName);
        }
        private void UCEconomyStatsChartOutput2D_Load(object sender, EventArgs e)
        {
            //this.cbx_chartType.Properties.Items.AddRange(new string[] { "柱状图", "饼状图", "线状图", "3D柱状图", "3D线状图" });
            Init(_dt, this._sysFieldName);
            //this.cbx_chartType.SelectedIndex = 0;


        }

        private void Init(DataTable dt,string sysFieldName)
        {
            if (dt == null) return;
            dictRow = new Dictionary<string, DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string seriesName = dt.Rows[i][0].ToString();
                this.clbx_disList.Items.Add(seriesName);
                dictRow[seriesName] = dt.Rows[i];
            }
            InitCbxProp(sysFieldName);
            this.cbx_prop.SelectedIndex = 0;
            
        }
        private void InitCbxProp(string sysFieldName)
        {
            switch (sysFieldName)
            {
                case"Building":
                    cbx_prop.Properties.Items.AddRange(new string[] { "区域面积", "建筑物数量", "建筑面积", "占地面积" });
                    break;
                case"Structure":
                    cbx_prop.Properties.Items.AddRange(new string[] { "区域面积", "构筑物数量", "占地面积" });
                    break;
                case"Green":
                    cbx_prop.Properties.Items.AddRange(new string[] { "区域面积", "绿化地块数", "绿化面积" });
                    break;
                case"Road":
                    cbx_prop.Properties.Items.AddRange(new string[] { "区域面积", "道路长度" });
                    break;
                case "Railway":
                    cbx_prop.Properties.Items.AddRange(new string[] { "区域面积", "铁路长度" });
                    break;
                
            }
        }
        private int GetColumnIndex(string propName)
        {
            switch (propName)
            {
                case"区域面积":
                    return 1;
                    break;
                case"建筑物数量":
                case"构筑物数量":
                case"绿化地块数":
                case"道路长度":
                case"铁路长度":
                    return 2;
                    break;
                case"建筑面积":
                case"占地面积":
                case "绿化面积":
                    return 3;
                    break;
                    
            }
            return 0;
        }

        //private void cbx_chartType_SelectedIndexChanged(object sender, EventArgs e)
        //{
           
        //    switch (this.cbx_chartType.SelectedIndex)
        //    {
        //        case 0:
        //            {
        //                this.LoadChart(ViewType.Bar);
        //                break;
        //            }
        //        case 1:
        //            {
        //                this.LoadChart(ViewType.Pie);
        //                break;
        //            }
        //        case 2:
        //            {
        //                this.LoadChart(ViewType.Line);
        //                break;
        //            }
        //        case 3:
        //            {
        //                this.LoadChart(ViewType.Bar3D);
        //                break;
        //            }
        //        case 4:
        //            {
        //                this.LoadChart(ViewType.Line3D);
        //                break;
        //            }
        //    }
        //}

        private DataTable SelectRowsForStats()
        {
            try
            {
                DataTable dtStats = new DataTable();
                InitDataColumn(dtStats, this._sysFieldName);
                if (this.clbx_disList.CheckedItems.Count == 0) { /*XtraMessageBox.Show("请选择几个区域"); */return null; }
                if (this.dictRow == null || this.dictRow.Count == 0) return null;
                foreach (object obj in this.clbx_disList.CheckedItems)
                {
                    string seriesName = obj.ToString();
                    if (dictRow.ContainsKey(seriesName))
                    {
                        DataRow dr = dtStats.NewRow();
                        dr[dtStats.Columns[0].ColumnName] = dictRow[seriesName][0];
                        for (int i = 1; i < dtStats.Columns.Count; i++)
                        {
                            dr[dtStats.Columns[i].ColumnName] = decimal.Parse(dictRow[seriesName][i].ToString());
                        }
                        dtStats.Rows.Add(dr);
                    }
                }
                return dtStats;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            
            
        }

        private void InitDataColumn(DataTable dt,string sysFieldName)
        {
            switch (sysFieldName)
            {
                case"Building":
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("区域名称"), new DataColumn("区域面积", typeof(decimal)), new DataColumn("建筑物数量", typeof(decimal)), new DataColumn("建筑面积", typeof(decimal)), new DataColumn("占地面积", typeof(decimal)) });
                    break;
                case"Structure":
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("区域名称"), new DataColumn("区域面积", typeof(decimal)), new DataColumn("构筑物数量", typeof(decimal)), new DataColumn("占地面积", typeof(decimal)) });
                    break;
                case "Green":
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("区域名称"), new DataColumn("区域面积", typeof(decimal)), new DataColumn("绿化地块数", typeof(decimal)), new DataColumn("绿化面积", typeof(decimal)) });
                    break;
                case "Road":
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("区域名称"), new DataColumn("区域面积", typeof(decimal)), new DataColumn("道路长度", typeof(decimal)) });
                    break;
                case "Railway":
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("区域名称"), new DataColumn("区域面积", typeof(decimal)), new DataColumn("铁路长度", typeof(decimal)) });
                    break;

            }
        }
        private ChartTitle GetChartTitle(string seriesName)
        {
            _chartTitle = new ChartTitle();
            switch (seriesName)
            {
                case"区域面积":
                case"建筑面积":
                case"占地面积":
                case"绿化面积":
                    _chartTitle.Text = seriesName + "统计图  单位:平方米";
                    break;
                case"建筑物数":
                case"构筑物数":
                case"绿化地块数":
                    _chartTitle.Text = seriesName + "统计图  单位:个";
                    break;
                case"道路长度":
                case"铁路长度":
                    _chartTitle.Text = seriesName + "统计图  单位:米";
                    break;

            }
            _chartTitle.Font = new System.Drawing.Font("宋体", 15f);
            return _chartTitle;
        }
        private void LoadChart(ViewType viewType)
        {
            string seriesName = this.cbx_prop.Text;
            this.chartControl1.Series.Clear();
            _chartTitle = new ChartTitle();            
            this.chartControl1.Titles.Clear();
            this.chartControl1.Titles.Add(GetChartTitle(seriesName));

            if (_dt == null) return;
            _dtStats = SelectRowsForStats();
            if (_dtStats == null) return;
            
            try
            {
                if (viewType == ViewType.Bar)
                {
                    Series sr1 = new Series(seriesName, ViewType.Bar);
                    sr1.ArgumentScaleType = ScaleType.Qualitative;//定性的
                    sr1.ValueScaleType = ScaleType.Numerical;//数字类型 
                    sr1.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                    sr1.PointOptions.ValueNumericOptions.Precision = 2;//百分号前面的数字不跟小数点

                    //绑定数据源
                    sr1.DataSource = _dtStats;//newdtb是获取到的数据(可以是数据库中的表，也可以是DataTable)
                    sr1.ArgumentDataMember =_dtStats.Columns[0].ColumnName;//绑定的文字信息（名称）(坐标横轴)
                    sr1.ValueDataMembers[0] = seriesName;//绑定的值（数据）(坐标纵轴)
                    sr1.ShowInLegend = true;
                    this.chartControl1.Series.Add(sr1);
                    this.chartControl1.Legend.Visible = true;
                }
            
            }
            catch (System.Exception ex)
            {
            	
            }
          
        }

        private void btn_Stats_Click(object sender, EventArgs e)
        {
            if (this.clbx_disList.CheckedItems.Count == 0) { XtraMessageBox.Show("请选择几个区域");return; }
            LoadChart(ViewType.Bar);
            
        }

        
    }
}
