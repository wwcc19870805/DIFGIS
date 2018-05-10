using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmStatasByRoadChart2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DataTable _dtstats;
        private string _statsType;
        private ChartTitle chartTitle;
        private DataTable _dtshow;
        private string roadname;
        private HashSet<string> hashSet = new HashSet<string>();
        HashSet<string> seriesNames = new HashSet<string>();
        public FrmStatasByRoadChart2D()
        {
            InitializeComponent();
        }

        public FrmStatasByRoadChart2D(DataTable dt, string statstype)
        {
            InitializeComponent();
            _dtstats = dt;
            this._statsType = statstype;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(920, 491);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(12, 38);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(896, 441);
            this.chartControl1.TabIndex = 5;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(12, 12);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(896, 22);
            this.comboBoxEdit1.StyleController = this.layoutControl1;
            this.comboBoxEdit1.TabIndex = 4;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(920, 491);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxEdit1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(900, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chartControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(900, 445);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // FrmStatasByRoadChart2D
            // 
            this.ClientSize = new System.Drawing.Size(920, 491);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmStatasByRoadChart2D";
            this.ShowIcon = false;
            this.Text = "道路统计图表";
            this.Load += new System.EventHandler(this.FrmStatasByRoadChart2D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        private void FrmStatasByRoadChart2D_Load(object sender, EventArgs e)
        {
            this.comboBoxEdit1.Properties.Items.AddRange(new string[] { "柱状图", "饼状图", "折线图", "3D柱状图", "3D线状图" });
            this.comboBoxEdit1.SelectedIndex = 0;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    {
                        this.LoadChart(ViewType.Bar);
                        break;
                    }
                case 1:
                    {
                        this.LoadChart(ViewType.Pie);
                        break;
                    }
                case 2:
                    {
                        this.LoadChart(ViewType.Line);
                        break;
                    }
                case 3:
                    {
                        this.LoadChart(ViewType.Bar3D);
                        break;
                    }
                case 4:
                    {
                        this.LoadChart(ViewType.Line3D);
                        break;
                    }
            }
        }

        //         private void LoadChart(ViewType viewType)
        //         {
        // 
        //             this.chartControl1.Series.Clear();
        //             chartTitle = new ChartTitle();
        //             chartTitle.Font = new System.Drawing.Font("宋体", 15f);
        //             chartTitle.Text = "管线道路统计图";
        //             this.chartControl1.Titles.Clear();
        //             this.chartControl1.Titles.Add(chartTitle);
        // 
        //             if (_dtstats != null)
        //             {
        //                 _dtshow = ConvertDTForChart(_dtstats);
        // //                 for (int i = 1; i < _dtshow.Columns.Count; i++)
        // //                 {
        //                   foreach (string str in hashSet)
        //                  { 
        //                     if (viewType == ViewType.Bar)
        //                     {
        //                         chartControl1.BeginInit();
        // 
        //                         /*Series series = new Series(str, viewType);*/
        //                         int index = this.chartControl1.Series.Add(str, viewType);
        //                         Series series = this.chartControl1.Series[index];
        //                                 series.DataSource = _dtshow;
        //                                 /*series.LegendText = str;*/
        //                                 series.Visible = true;
        //                                 series.ArgumentScaleType = ScaleType.Qualitative;
        //                                 series.ValueScaleType = ScaleType.Numerical;
        //                                 series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
        //                                 series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
        //                                 // 显示标签
        //                                 //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        //                                 series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
        // 
        //                                 //显示表示的信息和数据
        //                                 series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
        //                                 series.Label.PointOptions.ValueNumericOptions.Precision = 2;
        //                                 this.chartControl1.Series.Add(series);
        //                                 chartControl1.EndInit();
        //                                 break;  
        //                     }
        // 
        //                     else if (viewType == ViewType.Pie)
        //                     {
        //                         try
        //                         {
        //                             this.chartControl1.BeginInit();
        // 
        //                             Series series = new Series(str/*_dtshow.Columns[1].ColumnName*/, viewType);
        //                             series.LegendText = str;
        //                                     series.DataSource = _dtshow;
        //                                     series.Visible = true;
        //                                     series.ArgumentScaleType = ScaleType.Qualitative;
        //                                     series.ValueScaleType = ScaleType.Numerical;
        //                                     series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
        //                                     series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
        //                                     // 显示标签
        //                                     series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        //                                     series.Label.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
        //                                     series.LegendPointOptions.PointView = PointView.Argument;
        //                                     series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
        //                                     series.Label.PointOptions.ValueNumericOptions.Precision = 2;
        //                                     this.chartControl1.Series.Add(series);
        //                                     this.chartControl1.EndInit();
        //                                     break;
        //                            
        // 
        //                         }
        //                         catch { }
        // 
        //                     }
        // 
        //                     else if (viewType == ViewType.Line)
        //                     {
        //                         this.chartControl1.BeginInit();
        //                         Series series = new Series(str, viewType);
        //                                 series.DataSource = _dtshow;
        //                                 series.LegendText = str;
        //                                 series.Visible = true;
        //                                 series.ArgumentScaleType = ScaleType.Qualitative;
        //                                 series.ValueScaleType = ScaleType.Numerical;
        //                                 series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
        //                                 series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
        //                                 this.chartControl1.Series.Add(series);
        //                                 this.chartControl1.EndInit();
        //                                 break;
        //                     }
        //                     else if (viewType == ViewType.Bar3D)
        //                     {
        //                                 this.chartControl1.BeginInit();
        //                                 Series series = new Series(str, viewType);
        //                                 series.LegendText = str;
        //                                 series.DataSource = _dtshow;
        //                                 series.Visible = true;
        //                                 series.ArgumentScaleType = ScaleType.Qualitative;
        //                                 series.ValueScaleType = ScaleType.Numerical;
        //                                 series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
        //                                 series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
        //                                 XYDiagram3D xYDiagram3D = new XYDiagram3D();
        //                                 xYDiagram3D.RuntimeRotation = true;
        //                                 xYDiagram3D.RuntimeScrolling = true;
        //                                 xYDiagram3D.RuntimeZooming = true;
        //                                 xYDiagram3D.PerspectiveEnabled = true;
        //                                 /*  this.chartControl1.EndInit();*/
        // 
        //                                 this.chartControl1.Series.Add(series);
        //                                 this.chartControl1.EndInit();
        //                                 break;
        //                          
        // 
        //                     }
        //                     else if (viewType == ViewType.Line3D)
        //                     {
        //                         this.chartControl1.BeginInit();
        // 
        // 
        //                         Series series = new Series(str, viewType);
        //                         series.LegendText = _dtstats.Columns[2].ColumnName;
        //                                 series.DataSource = _dtshow;
        //                                 series.LegendText = str;
        //                                 series.Visible = true;
        //                                 series.ArgumentScaleType = ScaleType.Qualitative;
        //                                 series.ValueScaleType = ScaleType.Numerical;
        //                                 series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
        //                                 series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
        //                                 XYDiagram3D xYDiagram3D2 = new XYDiagram3D();
        //                                 xYDiagram3D2.RuntimeRotation = true;
        //                                 xYDiagram3D2.RuntimeScrolling = true;
        //                                 xYDiagram3D2.RuntimeZooming = true;
        //                                 xYDiagram3D2.PerspectiveEnabled = true;
        //                                 this.chartControl1.Series.Add(series);
        //                                 this.chartControl1.EndInit();
        //                                 /* this.chartControl1.EndInit();*/
        //                                 break;
        //                             
        //                     }
        // 
        //                 }
        //               }
        //             }
        //         /*}*/

        private void LoadChart(ViewType viewType)
        {

            this.chartControl1.Series.Clear();
            chartTitle = new ChartTitle();

            chartTitle.Text = "管线道路统计图  单位：米";
            chartTitle.Font = new System.Drawing.Font("宋体", 15f);
            this.chartControl1.Titles.Clear();
            this.chartControl1.Titles.Add(chartTitle);

            if (_dtstats != null)
            {
                try
                {
                    _dtshow = ConvertDTForChart(_dtstats);
                    if (viewType == ViewType.Pie)
                    {
                        try
                        {
                            //将_dtshow转置

                            DataTable _dtpie = new DataTable();
                            _dtpie.Columns.Add(_dtshow.Columns[0].ColumnName);
                            for (int i = 0; i < _dtshow.Rows.Count; i++)
                            {
                                _dtpie.Columns.Add(_dtshow.Rows[i][0].ToString(), typeof(double));
                            }
                            for (int i = 1; i < _dtshow.Columns.Count; i++)
                            {
                                DataRow dr = _dtpie.NewRow();
                                dr[0] = _dtshow.Columns[i].ColumnName.ToString();
                                for (int j = 0; j < _dtshow.Rows.Count; j++)
                                {
                                    double temp = 0;
                                    double.TryParse(_dtshow.Rows[j][i].ToString(), out temp);
                                    dr[j + 1] = temp;
                                }
                                _dtpie.Rows.Add(dr);
                            }
                            for (int i = 1; i < _dtpie.Columns.Count; i++)
                            {
                                int index = this.chartControl1.Series.Add(_dtpie.Columns[i].ColumnName, viewType);
                                Series series = this.chartControl1.Series[index];
                                series.DataSource = _dtpie;
                                series.Visible = true;
                                series.ArgumentScaleType = ScaleType.Qualitative;
                                series.ValueScaleType = ScaleType.Numerical;
                                series.ArgumentDataMember = _dtpie.Columns[0].ColumnName;
                                series.ValueDataMembers.AddRange(new string[]
                                {
                                    _dtpie.Columns[i].ColumnName
                                });
                                // 显示标签
                                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                                series.Label.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                                series.LegendPointOptions.PointView = PointView.Argument;
                                //series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
                                //series.Label.PointOptions.ValueNumericOptions.Precision = 2;
                                //显示一个图例
                                if (i > 1)
                                {
                                    series.ShowInLegend = false;
                                }
                            }

                        }
                        catch { }

                    }
                    else if (viewType == ViewType.Bar)
                    {
                        for (int i = 1; i < _dtshow.Columns.Count; i++)
                        {
                            int index = this.chartControl1.Series.Add(_dtshow.Columns[i].ColumnName, viewType);
                            Series series = this.chartControl1.Series[index];
                            series.DataSource = _dtshow;
                            series.Visible = true;
                            series.ArgumentScaleType = ScaleType.Qualitative;
                            series.ValueScaleType = ScaleType.Numerical;
                            series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                            series.ValueDataMembers.AddRange(new string[]
                            {
                                _dtshow.Columns[i].ColumnName
                            });
                            // 显示标签
                            //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                            series.Label.PointOptions.PointView = PointView.ArgumentAndValues;             //显示表示的信息和数据
                            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
                            series.Label.PointOptions.ValueNumericOptions.Precision = 2;

                        }
                    }
                    else if (viewType == ViewType.Line)
                    {
                        for (int i = 1; i < _dtshow.Columns.Count; i++)
                        {
                            int index = this.chartControl1.Series.Add(_dtshow.Columns[i].ColumnName, viewType);
                            Series series = this.chartControl1.Series[index];
                            series.DataSource = _dtshow;
                            series.Visible = true;
                            series.ArgumentScaleType = ScaleType.Qualitative;
                            series.ValueScaleType = ScaleType.Numerical;
                            series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                            series.ValueDataMembers.AddRange(new string[]
                            {
                                _dtshow.Columns[i].ColumnName
                            });
                        }
                    }
                    else if (viewType == ViewType.Bar3D)
                    {
                        for (int i = 1; i < _dtshow.Columns.Count; i++)
                        {
                            int index = this.chartControl1.Series.Add(_dtshow.Columns[i].ColumnName, viewType);
                            Series series = this.chartControl1.Series[index];
                            series.DataSource = _dtshow;
                            series.Visible = true;
                            series.ArgumentScaleType = ScaleType.Qualitative;
                            series.ValueScaleType = ScaleType.Numerical;
                            series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                            series.ValueDataMembers.AddRange(new string[]
                            {
                                _dtshow.Columns[i].ColumnName
                            });
                            XYDiagram3D xYDiagram3D = this.chartControl1.Diagram as XYDiagram3D;
                            xYDiagram3D.RuntimeRotation = true;
                            xYDiagram3D.RuntimeScrolling = true;
                            xYDiagram3D.RuntimeZooming = true;
                            xYDiagram3D.PerspectiveEnabled = true;
                        }
                    }
                    else if (viewType == ViewType.Line3D)
                    {
                        for (int i = 1; i < _dtshow.Columns.Count; i++)
                        {
                            int index = this.chartControl1.Series.Add(_dtshow.Columns[i].ColumnName, viewType);
                            Series series = this.chartControl1.Series[index];
                            series.DataSource = _dtshow;
                            series.Visible = true;
                            series.ArgumentScaleType = ScaleType.Qualitative;
                            series.ValueScaleType = ScaleType.Numerical;
                            series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                            series.ValueDataMembers.AddRange(new string[]
                            {
                                _dtshow.Columns[i].ColumnName
                            });
                            XYDiagram3D xYDiagram3D2 = this.chartControl1.Diagram as XYDiagram3D;
                            xYDiagram3D2.RuntimeRotation = true;
                            xYDiagram3D2.RuntimeScrolling = true;
                            xYDiagram3D2.RuntimeZooming = true;
                            xYDiagram3D2.PerspectiveEnabled = true;
                        }
                    }
                    #region 备用代码
                    /*for (int i = 1; i < _dtshow.Columns.Count; i++)
                    {
                        int index = this.chartControl1.Series.Add(_dtshow.Columns[i].ColumnName, viewType);
                        Series series = this.chartControl1.Series[index];
                        series.DataSource = _dtshow;
                        series.Visible = true;
                        series.ArgumentScaleType = ScaleType.Qualitative;
                        series.ValueScaleType = ScaleType.Numerical;
                        series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                        series.ValueDataMembers.AddRange(new string[]
                        {
                            _dtshow.Columns[i].ColumnName
                        });
                        if (viewType == ViewType.Pie)
                        {
                            try
                            {
                                //将_dtshow转置
                                DataTable _dtpie = new DataTable();
                                _dtpie.Columns.Add(_dtshow.Columns[0].ColumnName);
                                foreach (DataRow dr in _dtshow.Rows)
                                {
                                    _dtpie.Columns.Add(dr[_dtshow.Columns[0].ColumnName].ToString());
                                }
                                for (int j = 1; j < _dtshow.Columns.Count; j++)
                                {
                                    DataRow dr = _dtpie.NewRow();
                                    dr[_dtshow.Columns[0].ColumnName] = _dtshow.Columns[j].ColumnName;
                                    int n = 1;
                                    for (int k = 0; k < _dtshow.Rows.Count; k++)
                                    {                                     
                                        dr[_dtpie.Columns[n].ColumnName] = _dtshow.Rows[k][j].ToString();
                                        n++;                             
                                    }
                                    _dtpie.Rows.Add(dr);
                                }

                            }
                            catch { }                            
                             // 显示标签
                            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                            series.Label.PointOptions.PointView = PointView.ArgumentAndValues;             //显示表示的信息和数据
                            //series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
                            //series.Label.PointOptions.ValueNumericOptions.Precision = 2;
                            //显示一个图例
                            if (i > 1)
                            {
                                series.ShowInLegend = false;
                            }
                        }
                        else if(viewType == ViewType.Bar)
                        {
                            // 显示标签
                            //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                            series.Label.PointOptions.PointView = PointView.ArgumentAndValues;             //显示表示的信息和数据
                            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
                            series.Label.PointOptions.ValueNumericOptions.Precision = 2;
                                
                                
                        }
                        else if (viewType == ViewType.Bar3D)
                        {
                               
                            XYDiagram3D xYDiagram3D = this.chartControl1.Diagram as XYDiagram3D;
                            xYDiagram3D.RuntimeRotation = true;
                            xYDiagram3D.RuntimeScrolling = true;
                            xYDiagram3D.RuntimeZooming = true;
                            xYDiagram3D.PerspectiveEnabled = true;
                                
                        }
                        else if (viewType == ViewType.Line3D)
                        {
                            XYDiagram3D xYDiagram3D2 = this.chartControl1.Diagram as XYDiagram3D;
                            xYDiagram3D2.RuntimeRotation = true;
                            xYDiagram3D2.RuntimeScrolling = true;
                            xYDiagram3D2.RuntimeZooming = true;
                            xYDiagram3D2.PerspectiveEnabled = true;
                        }
                    }*/
                    #endregion

                }
                catch
                {

                }

            }
        }




        private DataTable ConvertDTForChart(DataTable dt)
        {
            DataTable dtshow = new DataTable();
            HashSet<string> pipestatstype = new HashSet<string>();
            foreach (DataRow dr in dt.Rows)
            {
                seriesNames.Add(dr[dt.Columns[2].ColumnName].ToString());
                pipestatstype.Add(dr[dt.Columns[0].ColumnName].ToString());
            }

            {
                dtshow.Columns.Add(new DataColumn(dt.Columns[0].ColumnName));
                foreach (string sn in seriesNames)
                {
                    dtshow.Columns.Add(new DataColumn(sn, typeof(double)));
                }
                foreach (string ps in pipestatstype)
                {
                    DataRow dr = dtshow.NewRow();
                    dr[dt.Columns[0].ColumnName] = ps;
                    dtshow.Rows.Add(dr);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (this._statsType == "pipeline")
                    {
                        foreach (DataRow drtemp in dtshow.Rows)
                        {
                            if (dr[dt.Columns[0].ColumnName] == drtemp[dtshow.Columns[0].ColumnName])
                            {
                                double temp = 0.0;
                                double.TryParse(dr["LENGTH"].ToString(), out temp);
                                drtemp[dr["PVALUE"].ToString()] = temp;

                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow drtemp in dtshow.Rows)
                        {
                            if (dr[dt.Columns[0].ColumnName] == drtemp[dtshow.Columns[0].ColumnName])
                            {
                                int temp = 0;
                                Int32.TryParse(dr["NUMBER"].ToString(), out temp);
                                drtemp[dr["FIELDNAME"].ToString()] = temp;
                            }
                        }
                    }
                }
                /*  }*/

                return dtshow;

            }


        }
    }
}

