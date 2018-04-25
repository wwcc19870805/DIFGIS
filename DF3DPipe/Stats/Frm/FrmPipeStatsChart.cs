﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DPipe.Stats.UC;
using DevExpress.XtraCharts;

namespace DF3DPipe.Stats.Frm
{
    public class FrmPipeStatsChart : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ChartControl chartControl1;
        private SimpleButton btnStatsOutput;
        private ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    
       
        public FrmPipeStatsChart()
        {
            InitializeComponent();
            _dtstats = new DataTable();
        }
        public FrmPipeStatsChart(DataTable dt,string statstype)
        {
            InitializeComponent();
            _dtstats = new DataTable();
            _dtstats = dt;
            this._statsType = statstype;
            //this._chartTitle = charttitle; 
            comboBoxEdit1_SelectedIndexChanged(null, null);
        }
       
        

        private void InitializeComponent()
        {
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.btnStatsOutput = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Controls.Add(this.btnStatsOutput);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(895, 161, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(723, 445);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(2, 28);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(719, 415);
            this.chartControl1.TabIndex = 3;
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // btnStatsOutput
            // 
            this.btnStatsOutput.Location = new System.Drawing.Point(544, 2);
            this.btnStatsOutput.Name = "btnStatsOutput";
            this.btnStatsOutput.Size = new System.Drawing.Size(177, 22);
            this.btnStatsOutput.StyleController = this.layoutControl1;
            this.btnStatsOutput.TabIndex = 3;
            this.btnStatsOutput.Text = "输出统计图";
            this.btnStatsOutput.Click += new System.EventHandler(this.btnStatsOutput_Click);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "柱状图";
            this.comboBoxEdit1.Location = new System.Drawing.Point(77, 2);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "柱状图",
            "饼状图",
            "线状图",
            "3D柱状图",
            "3D线状图"});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(282, 22);
            this.comboBoxEdit1.StyleController = this.layoutControl1;
            this.comboBoxEdit1.TabIndex = 1;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem5,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(723, 445);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEdit1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(361, 26);
            this.layoutControlItem2.Text = "统计图样式：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(361, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(181, 26);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnStatsOutput;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(542, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(181, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chartControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(723, 419);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmPipeStatsChart
            // 
            this.ClientSize = new System.Drawing.Size(723, 445);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmPipeStatsChart";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管线长度统计图表";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dtstats;
        private DataTable _dtshow;
        private string _statsType;
        ChartTitle chartTitle;
        HashSet<string> seriesNames = new HashSet<string>();
       
        private DataTable ConvertDTForChart(DataTable dt)
        {
            seriesNames.Clear();
            DataTable dtshow = new DataTable();
            HashSet<string> pipestatstype = new HashSet<string>();
            foreach (DataRow dr in dt.Rows)
            {                                
                seriesNames.Add(dr[dt.Columns[2].ColumnName].ToString());
                pipestatstype.Add(dr[dt.Columns[0].ColumnName].ToString());
            }
            if (seriesNames.Count == 0 || pipestatstype.Count == 0) return dtshow;
            //if (dt.Columns.Count == 3)
            //{
                dtshow.Columns.Add(new DataColumn(dt.Columns[0].ColumnName));
                foreach (string sn in seriesNames)
                {
                    dtshow.Columns.Add(new DataColumn(sn));
                }
                foreach (string ps in pipestatstype)
                {
                    DataRow dr = dtshow.NewRow();
                    dr[dt.Columns[0].ColumnName] = ps;
                    //foreach (string sn1 in seriesNames)
                    //{
                    //    dr[sn1] = 0;
                    //}
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
                                break;
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
                                drtemp[dr["PVALUE"].ToString()] = temp;
                                break;
                            }
                        }
                    }                                 
                }
            //}

            return dtshow;

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

        private void LoadChart(ViewType viewType)
        {

            this.chartControl1.Series.Clear();
            chartTitle = new ChartTitle();
            if (this._statsType.ToLower() == "pipeline")
            {
                chartTitle.Text = "管线长度统计图  单位：米";
            }
            else if (this._statsType.ToLower() == "pipenode")
            {
                chartTitle.Text = "管点数量统计图  单位：个";
            }
            else return;

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
                                    //if (_dtshow.Rows[j][i].ToString() == null)
                                    //    dr[j + 1] = 0.0;
                                    //else
                                    //    dr[j + 1] = _dtshow.Rows[j][i];
                                    
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
                catch (Exception ex)
                {

                }               
                            
            }
        }

        private void btnStatsOutput_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.Filter = "Image Files(*.png)|*.png";
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "png";
                if (System.Windows.Forms.DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    this.chartControl1.ExportToImage(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    XtraMessageBox.Show("导出图片成功！", "提示");
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("导出图片失败！", "提示");
            }
            
        }

    }
}
