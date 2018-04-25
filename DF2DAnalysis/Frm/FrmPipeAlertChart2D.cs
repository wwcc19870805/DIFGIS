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
using DFCommon.Class;
using System.Collections;

namespace DF2DAnalysis.Frm
{
    public partial class FrmPipeAlertChart2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;


        private DataTable _dtstats;
        private string _statsType;
        private DataTable _dtshow;     
        private ChartTitle chartTitle;
        ArrayList list = new ArrayList();
        Dictionary<string, string> ListName = new Dictionary<string, string>();
        Dictionary<string, int> entry = new Dictionary<string, int>();
        Dictionary<string, int> entry1 = new Dictionary<string, int>();
        Dictionary<string, int> entry2 = new Dictionary<string, int>();
        Dictionary<string, int> entry3 = new Dictionary<string, int>();
        Dictionary<string, double> entryLength = new Dictionary<string, double>();
        Dictionary<string, double> entryLength1 = new Dictionary<string, double>();
        Dictionary<string, double> entryLength2 = new Dictionary<string, double>();
        Dictionary<string, double> entryLength3 = new Dictionary<string, double>();


        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private RadioGroup radioGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        HashSet<string> seriesNames = new HashSet<string>();

        public FrmPipeAlertChart2D(DataTable dt, string statstype)
        {
            InitializeComponent();
            _dtstats = dt;
            this._statsType = statstype;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
           /* _dtshow = ConvertDTForChart(_dtstats);*/
        }
       
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.radioGroup2);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1011, 512);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup2
            // 
            this.radioGroup2.Location = new System.Drawing.Point(12, 47);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Columns = 2;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "个数"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "管线总长度")});
            this.radioGroup2.Size = new System.Drawing.Size(695, 31);
            this.radioGroup2.StyleController = this.layoutControl1;
            this.radioGroup2.TabIndex = 7;
            this.radioGroup2.SelectedIndexChanged += new System.EventHandler(this.radioGroup2_SelectedIndexChanged);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 4;
            this.radioGroup1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "管线材质"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "道路名称"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "权属单位"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "管线规格")});
            this.radioGroup1.Size = new System.Drawing.Size(697, 31);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 6;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(12, 108);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(966, 392);
            this.chartControl1.TabIndex = 5;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(12, 82);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(966, 22);
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
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1011, 512);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxEdit1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 70);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(970, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chartControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(970, 396);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(701, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(269, 35);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.radioGroup1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(701, 35);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(970, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(21, 492);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(699, 35);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(271, 35);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.radioGroup2;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(699, 35);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FrmPipeAlertChart2D
            // 
            this.ClientSize = new System.Drawing.Size(1011, 512);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmPipeAlertChart2D";
            this.Text = "管线预警统计图表";
            this.Load += new System.EventHandler(this.FrmPipeAlertChart2D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }


        
        private void FrmPipeAlertChart2D_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < _dtstats.Columns.Count; i++)
            {
                if (_dtstats.Columns[i].ColumnName.ToString() == "TimeAlert")
                {
                    ListName.Add(_dtstats.Columns[i].ColumnName.ToString(), "超限个数");
                }
                else if (_dtstats.Columns[i].ColumnName.ToString() == "PipeLength")
                {
                    ListName.Add(_dtstats.Columns[i].ColumnName.ToString(), "管线总长度");
                }
                
            }
            this.comboBoxEdit1.Properties.Items.AddRange(new string[] { "柱状图", "饼状图", "折线图", "3D柱状图", "3D线状图" });

            this.comboBoxEdit1.SelectedIndex = 0;
            this.radioGroup1.SelectedIndex = 0;         
            this.radioGroup2.SelectedIndex = 0;  
           
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

                /*_dtshow = ConvertDTForChart(_dtstats);*/
                switch (this.comboBoxEdit1.SelectedIndex)
                {
                    case 0:
                        {
                            if (this.radioGroup1.SelectedIndex == 0)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                               
                            }
                            else if (this.radioGroup1.SelectedIndex == 1)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                            }

                            else if (this.radioGroup1.SelectedIndex == 2)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                            }
                            else if (this.radioGroup1.SelectedIndex == 3)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar);
                                }
                            }

                         
                            break;
                        }

                    case 1:
                        {
                            if (this.radioGroup1.SelectedIndex == 0)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }
                                else if (this.radioGroup2.SelectedIndex ==1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }

                            }

                            if (this.radioGroup1.SelectedIndex == 1)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }
                                else if (this.radioGroup2.SelectedIndex ==1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }

                            }

                            if (this.radioGroup1.SelectedIndex == 2)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }

                            }
                            if (this.radioGroup1.SelectedIndex == 3)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Pie);
                                }

                            }
                            


                            
                            break;
                        }
                    case 2:
                        {
                            if (this.radioGroup1.SelectedIndex == 0)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }

                            }

                            if (this.radioGroup1.SelectedIndex == 1)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }

                            }

                            if (this.radioGroup1.SelectedIndex == 2)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }

                            }

                            if (this.radioGroup1.SelectedIndex == 3)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line);
                                }

                            }


                            break;
                        }
                    case 3:
                        {

                            if (this.radioGroup1.SelectedIndex == 0)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                            }

                            if (this.radioGroup1.SelectedIndex == 1)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                            }

                            if (this.radioGroup1.SelectedIndex == 2)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                            }

                            if (this.radioGroup1.SelectedIndex == 3)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Bar3D);
                                }
                            }

                            break;
                        }
                    case 4:
                        {

                            if (this.radioGroup1.SelectedIndex == 0)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                            }


                            if (this.radioGroup1.SelectedIndex == 1)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                            }

                            if (this.radioGroup1.SelectedIndex == 2)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                            }


                            if (this.radioGroup1.SelectedIndex == 3)
                            {
                                if (this.radioGroup2.SelectedIndex == 0)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                                else if (this.radioGroup2.SelectedIndex == 1)
                                {
                                    _dtshow = ConvertDTForChart(_dtstats);
                                    this.LoadChart(ViewType.Line3D);
                                }
                            }

                           
                            break;
                        }              
            }
        }


        private void LoadChart(ViewType viewType)
        {

            this.chartControl1.Series.Clear();
            chartTitle = new ChartTitle();
            chartTitle.Font = new System.Drawing.Font("宋体", 15f);
            chartTitle.Text = "管线预警统计图";
            this.chartControl1.Titles.Clear();
            this.chartControl1.Titles.Add(chartTitle);

            if (_dtstats != null)
            {
                for (int i = 1; i < _dtshow.Columns.Count; i++)
                {

                            if (viewType == ViewType.Bar)
                            {
                                chartControl1.BeginInit();
                                foreach (string lengent in ListName.Values)
                                {
                                    if (ListName[_dtshow.Columns[1].ColumnName.ToString()] == lengent)
                                    {
                                      
                                      Series series = new Series(lengent, viewType);                                      
                                      series.DataSource = _dtshow;
                                      series.Visible = true;
                                      series.ArgumentScaleType = ScaleType.Qualitative;
                                      series.ValueScaleType = ScaleType.Numerical;
                                      series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                                      series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
                                      // 显示标签
                                      //series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                                      series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
                                     
                                        //显示表示的信息和数据
                                      series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
                                      series.Label.PointOptions.ValueNumericOptions.Precision = 2;
                                      this.chartControl1.Series.Add(series);
                                      chartControl1.EndInit();
                                      break;
                                    }
                                }
                              

                               
                            }

                            else if (viewType == ViewType.Pie)
                            {
                                try
                                {
                                    this.chartControl1.BeginInit();
                                    foreach (string lengent in ListName.Values)
                                    {
                                        if (ListName[_dtshow.Columns[1].ColumnName.ToString()] == lengent)
                                        {
                                            Series series = new Series(lengent, viewType);
                                            series.LegendText = lengent;                                           
                                            series.DataSource = _dtshow;
                                            series.Visible = true;
                                            series.ArgumentScaleType = ScaleType.Qualitative;
                                            series.ValueScaleType = ScaleType.Numerical;
                                            series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                                            series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
                                            // 显示标签
                                            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                                            series.Label.PointOptions.PointView = PointView.ArgumentAndValues;//显示表示的信息和数据
                                            series.LegendPointOptions.PointView = PointView.Argument;
                                            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;  //用百分比表示
                                            series.Label.PointOptions.ValueNumericOptions.Precision = 2;
                                            this.chartControl1.Series.Add(series);
                                            this.chartControl1.EndInit();
                                            break;
                                        }
                                    }
  
                                }
                                catch { }

                            }

                            else if (viewType == ViewType.Line)
                            {

                                this.chartControl1.BeginInit();

                                foreach (string lengent in ListName.Values)
                                {
                                    if (ListName[_dtshow.Columns[1].ColumnName.ToString()] == lengent)
                                    {

                                        Series series = new Series(lengent, viewType);
                                        series.DataSource = _dtshow;
                                        series.Visible = true;
                                        series.ArgumentScaleType = ScaleType.Qualitative;
                                        series.ValueScaleType = ScaleType.Numerical;
                                        series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                                        series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
                                        this.chartControl1.Series.Add(series);
                                        this.chartControl1.EndInit();
                                        break;
                                    }
                                }
                                
                            }
                            else if (viewType == ViewType.Bar3D)
                       {

                           foreach (string lengent in ListName.Values)
                           {
                               if (ListName[_dtshow.Columns[1].ColumnName.ToString()] == lengent)
                               {
                                   
                                   this.chartControl1.BeginInit();
                                   Series series = new Series(lengent, viewType);
                                   series.LegendText = lengent;                    
                                   series.DataSource = _dtshow;
                                   series.Visible = true;
                                   series.ArgumentScaleType = ScaleType.Qualitative;
                                   series.ValueScaleType = ScaleType.Numerical;
                                   series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                                   series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
                                   XYDiagram3D xYDiagram3D = new XYDiagram3D();
                                   xYDiagram3D.RuntimeRotation = true;
                                   xYDiagram3D.RuntimeScrolling = true;
                                   xYDiagram3D.RuntimeZooming = true;
                                   xYDiagram3D.PerspectiveEnabled = true;
                                   /*  this.chartControl1.EndInit();*/

                                   this.chartControl1.Series.Add(series);
                                   this.chartControl1.EndInit();
                                   break;
                               }
                           }
                            
                      }
                            else if (viewType == ViewType.Line3D)
                            {
                                this.chartControl1.BeginInit();

                                foreach (string lengent in ListName.Values)
                                {
                                    if (ListName[_dtshow.Columns[1].ColumnName.ToString()] == lengent)
                                    {
                                        Series series = new Series(lengent, viewType);
                                        /*series.LegendText = lengent;*/
                                        series.DataSource = _dtshow;
                                        series.Visible = true;
                                        series.ArgumentScaleType = ScaleType.Qualitative;
                                        series.ValueScaleType = ScaleType.Numerical;
                                        series.ArgumentDataMember = _dtshow.Columns[0].ColumnName;
                                        series.ValueDataMembers[0] = _dtshow.Columns[1].ColumnName;
                                        XYDiagram3D xYDiagram3D2 = new XYDiagram3D();
                                        xYDiagram3D2.RuntimeRotation = true;
                                        xYDiagram3D2.RuntimeScrolling = true;
                                        xYDiagram3D2.RuntimeZooming = true;
                                        xYDiagram3D2.PerspectiveEnabled = true;
                                        this.chartControl1.Series.Add(series);
                                        this.chartControl1.EndInit();
                                       /* this.chartControl1.EndInit();*/
                                        break;
                                    }
                                }
                            }
                         
                        }
            }
        }


        private DataTable ConvertDTForChart(DataTable dt)
        {
            DataTable dtshow = new DataTable();
            HashSet<string> pipestatstype = new HashSet<string>();
            HashSet<string> pipestatstype1 = new HashSet<string>();
            HashSet<string> pipestatstype2 = new HashSet<string>();
            HashSet<string> pipestatstype3 = new HashSet<string>();
            HashSet<string> pipestatstype4 = new HashSet<string>();
            HashSet<string> seriesStandard= new HashSet<string>();
            foreach (DataRow dr in dt.Rows)
            {                
                pipestatstype.Add(dr[dt.Columns[0].ColumnName].ToString());
                pipestatstype1.Add(dr[dt.Columns[5].ColumnName].ToString());
                pipestatstype2.Add(dr[dt.Columns[6].ColumnName].ToString());
                pipestatstype3.Add(dr[dt.Columns[7].ColumnName].ToString());
                pipestatstype4.Add(dr[dt.Columns[11].ColumnName].ToString());
            }

            //管线材质
            if (this.radioGroup1.SelectedIndex == 0)
            {
                //柱状图
                if(this.comboBoxEdit1.SelectedIndex==0)
                {
                //个数
                if (this.radioGroup2.SelectedIndex == 0)
                {
                    entry.Clear();
                    foreach (string str in pipestatstype)
                    {
                        int n = 0; int Count = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[dt.Columns[0].ColumnName].ToString() == str)
                            {
                                Count = n++;
                            }
                        }
                        entry.Add(str, Count);
                    }
                    dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                    foreach (var var in entry)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        int value = var.Value;
                        dr[dt.Columns[0].ColumnName] = strname;
                        dr[dt.Columns[8].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }
                    //管线总长度
                else if (this.radioGroup2.SelectedIndex == 1)
                {
                    entry.Clear();
                    entryLength.Clear();
                    foreach (string str in pipestatstype)
                    {
                         double Count = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[dt.Columns[0].ColumnName].ToString() == str)
                            {
                                string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                double pipelenggth = double.Parse(pipelenggthName);
                                Count += pipelenggth;
                            }
                        }
                        entryLength.Add(str, Count);
                    }
                    dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                    foreach (var var in entryLength)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                       double value = var.Value;
                        dr[dt.Columns[0].ColumnName] = strname;
                        dr[dt.Columns[11].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }
                
            }
                //饼图
                if (this.comboBoxEdit1.SelectedIndex == 1)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry.Clear();
                        foreach (string str in pipestatstype)
                        {
                            int n = 0; int Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    Count = n++;
                                }
                            }
                            entry.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry.Clear();
                        entryLength.Clear();
                        foreach (string str in pipestatstype)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                }
                //折线图
                if (this.comboBoxEdit1.SelectedIndex == 2)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry.Clear();
                        foreach (string str in pipestatstype)
                        {
                            int n = 0; int Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    Count = n++;
                                }
                            }
                            entry.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry.Clear();
                        entryLength.Clear();
                        foreach (string str in pipestatstype)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                }
                
                //3d柱状图
                if (this.comboBoxEdit1.SelectedIndex == 3)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry.Clear();
                        foreach (string str in pipestatstype)
                        {
                            int n = 0; int Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    Count = n++;
                                }
                            }
                            entry.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry.Clear();
                        entryLength.Clear();
                        foreach (string str in pipestatstype)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                }
                //3d折线图
                if (this.comboBoxEdit1.SelectedIndex == 4)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry.Clear();
                        foreach (string str in pipestatstype)
                        {
                            int n = 0; int Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    Count = n++;
                                }
                            }
                            entry.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry.Clear();
                        entryLength.Clear();
                        foreach (string str in pipestatstype)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[0].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[0].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                }

          }

                //权属单位
            else if (this.radioGroup1.SelectedIndex == 2)
            {
                //柱状图
                if(this.comboBoxEdit1.SelectedIndex==0)
                {
                //个数
                if (this.radioGroup2.SelectedIndex == 0)
                {
                    entry1.Clear();
                    foreach (string str in pipestatstype1)
                    {
                        int n1 = 0, Count1 = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            if (row[dt.Columns[5].ColumnName].ToString() == str)
                            {
                                Count1 = n1++;
                            }
                        }
                        entry1.Add(str, Count1);
                    }
                    dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                    foreach (var var in entry1)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        int value = var.Value;
                        dr[dt.Columns[5].ColumnName] = strname;
                        dr[dt.Columns[8].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }

                    //管线总长度
                else if (this.radioGroup2.SelectedIndex == 1)
                {
                    entry1.Clear();
                    entryLength1.Clear();
                    foreach (string str in pipestatstype1)
                    {
                        double Count = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[dt.Columns[5].ColumnName].ToString() == str)
                            {
                                string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                double pipelenggth = double.Parse(pipelenggthName);
                                Count += pipelenggth;
                            }
                        }
                        entryLength1.Add(str, Count);
                    }
                    dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                    foreach (var var in entryLength1)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        double value = var.Value;
                        dr[dt.Columns[5].ColumnName] = strname;
                        dr[dt.Columns[11].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }               
               }
                //饼状图
                if (this.comboBoxEdit1.SelectedIndex == 1)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            int n1 = 0, Count1 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    Count1 = n1++;
                                }
                            }
                            entry1.Add(str, Count1);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                        //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry1.Clear();
                        entryLength1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength1.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //折线图
                if (this.comboBoxEdit1.SelectedIndex == 2)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            int n1 = 0, Count1 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    Count1 = n1++;
                                }
                            }
                            entry1.Add(str, Count1);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                        //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry1.Clear();
                        entryLength1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength1.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //3d柱状图
                if (this.comboBoxEdit1.SelectedIndex == 3)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            int n1 = 0, Count1 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    Count1 = n1++;
                                }
                            }
                            entry1.Add(str, Count1);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                        //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry1.Clear();
                        entryLength1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength1.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //3d折线图
                if (this.comboBoxEdit1.SelectedIndex == 4)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            int n1 = 0, Count1 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    Count1 = n1++;
                                }
                            }
                            entry1.Add(str, Count1);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }

                        //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry1.Clear();
                        entryLength1.Clear();
                        foreach (string str in pipestatstype1)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[5].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength1.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength1)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[5].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                } 

            }

                //道路名称
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                //柱状图
                if (this.comboBoxEdit1.SelectedIndex == 0)
                {
                    //个数
                if (this.radioGroup2.SelectedIndex == 0)
                {
                    entry2.Clear();
                    foreach (string str in pipestatstype2)
                    {
                        int n2 = 0, Count2 = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            if (row[dt.Columns[6].ColumnName].ToString() == str)
                            {
                                Count2 = n2++;
                            }
                        }
                        entry2.Add(str, Count2);
                    }
                    dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                    foreach (var var in entry2)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        int value = var.Value;
                        dr[dt.Columns[6].ColumnName] = strname;
                        dr[dt.Columns[8].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }
                //管线总长度
                else if (this.radioGroup2.SelectedIndex == 1)
                {
                    entry2.Clear();
                    entryLength2.Clear();
                    foreach (string str in pipestatstype2)
                    {
                        double Count = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[dt.Columns[6].ColumnName].ToString() == str)
                            {
                                string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                double pipelenggth = double.Parse(pipelenggthName);
                                Count += pipelenggth;
                            }
                        }
                        entryLength2.Add(str, Count);
                    }
                    dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                    foreach (var var in entryLength2)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        double value = var.Value;
                        dr[dt.Columns[6].ColumnName] = strname;
                        dr[dt.Columns[11].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }
              }
                //饼状图
                if (this.comboBoxEdit1.SelectedIndex == 1)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            int n2 = 0, Count2 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    Count2 = n2++;
                                }
                            }
                            entry2.Add(str, Count2);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry2.Clear();
                        entryLength2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength2.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //折线图
                if (this.comboBoxEdit1.SelectedIndex == 2)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            int n2 = 0, Count2 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    Count2 = n2++;
                                }
                            }
                            entry2.Add(str, Count2);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry2.Clear();
                        entryLength2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength2.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //3d柱状图
                if (this.comboBoxEdit1.SelectedIndex == 3)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            int n2 = 0, Count2 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    Count2 = n2++;
                                }
                            }
                            entry2.Add(str, Count2);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry2.Clear();
                        entryLength2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength2.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }

                //3d折线图
                if (this.comboBoxEdit1.SelectedIndex == 4)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            int n2 = 0, Count2 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    Count2 = n2++;
                                }
                            }
                            entry2.Add(str, Count2);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry2.Clear();
                        entryLength2.Clear();
                        foreach (string str in pipestatstype2)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[6].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength2.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength2)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[6].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }

            }

                //管线规格
            else if (this.radioGroup1.SelectedIndex == 3)
            {
                //柱状图
                if (this.comboBoxEdit1.SelectedIndex == 0)
                {
                //个数
                if (this.radioGroup2.SelectedIndex == 0)
                {
                    entry3.Clear();
                    foreach (string str in pipestatstype3)
                    {
                        int n3 = 0, Count3 = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            if (row[dt.Columns[7].ColumnName].ToString() == str)
                            {
                                Count3 = n3++;
                            }
                        }
                        entry3.Add(str, Count3);
                    }
                    dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                    foreach (var var in entry3)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        int value = var.Value;
                        dr[dt.Columns[7].ColumnName] = strname;
                        dr[dt.Columns[8].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }
                //管线总长度
                else if (this.radioGroup2.SelectedIndex == 1)
                {
                    entry3.Clear();
                    entryLength3.Clear();
                    foreach (string str in pipestatstype3)
                    {
                        double Count = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[dt.Columns[7].ColumnName].ToString() == str)
                            {
                                string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                double pipelenggth = double.Parse(pipelenggthName);
                                Count += pipelenggth;
                            }
                        }
                        entryLength3.Add(str, Count);
                    }
                    dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                    dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                    foreach (var var in entryLength3)
                    {
                        DataRow dr = dtshow.NewRow();
                        string strname = var.Key;
                        double value = var.Value;
                        dr[dt.Columns[7].ColumnName] = strname;
                        dr[dt.Columns[11].ColumnName] = value;
                        dtshow.Rows.Add(dr);
                    }
                }
              }
                //饼状图
                if (this.comboBoxEdit1.SelectedIndex == 1)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            int n3 = 0, Count3 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    Count3 = n3++;
                                }
                            }
                            entry3.Add(str, Count3);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry3.Clear();
                        entryLength3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength3.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }

                //折线图
                if (this.comboBoxEdit1.SelectedIndex == 1)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            int n3 = 0, Count3 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    Count3 = n3++;
                                }
                            }
                            entry3.Add(str, Count3);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry3.Clear();
                        entryLength3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength3.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //3d柱状图
                if (this.comboBoxEdit1.SelectedIndex == 3)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            int n3 = 0, Count3 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    Count3 = n3++;
                                }
                            }
                            entry3.Add(str, Count3);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry3.Clear();
                        entryLength3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength3.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
                //3d折线图
                if (this.comboBoxEdit1.SelectedIndex == 4)
                {
                    //个数
                    if (this.radioGroup2.SelectedIndex == 0)
                    {
                        entry3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            int n3 = 0, Count3 = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    Count3 = n3++;
                                }
                            }
                            entry3.Add(str, Count3);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                        foreach (var var in entry3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            int value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[8].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                    //管线总长度
                    else if (this.radioGroup2.SelectedIndex == 1)
                    {
                        entry3.Clear();
                        entryLength3.Clear();
                        foreach (string str in pipestatstype3)
                        {
                            double Count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[dt.Columns[7].ColumnName].ToString() == str)
                                {
                                    string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                    double pipelenggth = double.Parse(pipelenggthName);
                                    Count += pipelenggth;
                                }
                            }
                            entryLength3.Add(str, Count);
                        }
                        dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                        dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                        foreach (var var in entryLength3)
                        {
                            DataRow dr = dtshow.NewRow();
                            string strname = var.Key;
                            double value = var.Value;
                            dr[dt.Columns[7].ColumnName] = strname;
                            dr[dt.Columns[11].ColumnName] = value;
                            dtshow.Rows.Add(dr);
                        }
                    }
                }
               
            }
            return dtshow;
        }


        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = _dtstats;
            DataTable dtshow = new DataTable();
            HashSet<string> pipestatstype = new HashSet<string>();
            HashSet<string> pipestatstype1 = new HashSet<string>();
            HashSet<string> pipestatstype2 = new HashSet<string>();
            HashSet<string> pipestatstype3 = new HashSet<string>();
            HashSet<string> pipestatstype4 = new HashSet<string>();
            HashSet<string> seriesStandard = new HashSet<string>();
            foreach (DataRow dr in dt.Rows)
            {
                pipestatstype.Add(dr[dt.Columns[0].ColumnName].ToString());
                pipestatstype1.Add(dr[dt.Columns[5].ColumnName].ToString());
                pipestatstype2.Add(dr[dt.Columns[6].ColumnName].ToString());
                pipestatstype3.Add(dr[dt.Columns[7].ColumnName].ToString());
                pipestatstype4.Add(dr[dt.Columns[11].ColumnName].ToString());
            }
            
            switch(this.radioGroup1.SelectedIndex)
            {
                    //管线材质
                case 0:
                    //柱状图
                    if (this.comboBoxEdit1.SelectedIndex == 0)
                    {
                        //个数
                        if (this.radioGroup2.SelectedIndex == 0)
                        {
                            entry.Clear();
                            foreach (string str in pipestatstype)
                            {
                                int n = 0; int Count = 0;
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row[dt.Columns[0].ColumnName].ToString() == str)
                                    {
                                        Count = n++;
                                    }
                                }
                                entry.Add(str, Count);
                            }
                            dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                            dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                            foreach (var var in entry)
                            {
                                DataRow dr = dtshow.NewRow();
                                string strname = var.Key;
                                int value = var.Value;
                                dr[dt.Columns[0].ColumnName] = strname;
                                dr[dt.Columns[8].ColumnName] = value;
                                dtshow.Rows.Add(dr);
                            }
                            _dtshow = dtshow;
                            this.LoadChart(ViewType.Bar);
                        }
                        //管线总长度
                        else if (this.radioGroup2.SelectedIndex == 1)
                        {
                            _dtshow.Clear();
                            entry.Clear();
                            entryLength.Clear();
                            foreach (string str in pipestatstype)
                            {
                                double Count = 0;
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row[dt.Columns[0].ColumnName].ToString() == str)
                                    {
                                        string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                        double pipelenggth = double.Parse(pipelenggthName);
                                        Count += pipelenggth;
                                    }
                                }
                                entryLength.Add(str, Count);
                            }
                            dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                            dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                            foreach (var var in entryLength)
                            {
                                DataRow dr = dtshow.NewRow();
                                string strname = var.Key;
                                double value = var.Value;
                                dr[dt.Columns[0].ColumnName] = strname;
                                dr[dt.Columns[11].ColumnName] = value;
                                dtshow.Rows.Add(dr);
                            }
                            _dtshow = dtshow;
                            this.LoadChart(ViewType.Bar);
                        }
                        
                        //饼图
                        if (this.comboBoxEdit1.SelectedIndex == 1)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    int n = 0; int Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            Count = n++;
                                        }
                                    }
                                    entry.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                entryLength.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }

                        }
                        //折线图
                        if (this.comboBoxEdit1.SelectedIndex == 2)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    int n = 0; int Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            Count = n++;
                                        }
                                    }
                                    entry.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                entryLength.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }

                        }

                        //3d柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 3)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    int n = 0; int Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            Count = n++;
                                        }
                                    }
                                    entry.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                entryLength.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }

                        }
                        //3d折线图
                        if (this.comboBoxEdit1.SelectedIndex == 4)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    int n = 0; int Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            Count = n++;
                                        }
                                    }
                                    entry.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry.Clear();
                                entryLength.Clear();
                                foreach (string str in pipestatstype)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[0].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[0].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[0].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }

                        }

                    }
                 
                      break;

              //道路名称
                case 1:
                    if (_dtshow != null)
                    {   
                        _dtshow.Clear();
                        //柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 0)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                entry2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    int n2 = 0, Count2 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            Count2 = n2++;
                                        }
                                    }
                                    entry2.Add(str, Count2);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                entryLength2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength2.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar);
                            }
                        }
                        //饼状图
                        if (this.comboBoxEdit1.SelectedIndex == 1)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    int n2 = 0, Count2 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            Count2 = n2++;
                                        }
                                    }
                                    entry2.Add(str, Count2);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                entryLength2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength2.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }
                        }
                        //折线图
                        if (this.comboBoxEdit1.SelectedIndex == 2)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    int n2 = 0, Count2 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            Count2 = n2++;
                                        }
                                    }
                                    entry2.Add(str, Count2);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                entryLength2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength2.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }
                        }
                        //3d柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 3)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    int n2 = 0, Count2 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            Count2 = n2++;
                                        }
                                    }
                                    entry2.Add(str, Count2);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                entryLength2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength2.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }
                        }

                        //3d折线图
                        if (this.comboBoxEdit1.SelectedIndex == 4)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    int n2 = 0, Count2 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            Count2 = n2++;
                                        }
                                    }
                                    entry2.Add(str, Count2);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry2.Clear();
                                entryLength2.Clear();
                                foreach (string str in pipestatstype2)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[6].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength2.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[6].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength2)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[6].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }
                        }
                                         
                    }                   
                    break;

              //权属单位
                case 2:
                    if (_dtshow != null)
                    {
                        _dtshow.Clear();
                        //柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 0)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                entry1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    int n1 = 0, Count1 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            Count1 = n1++;
                                        }
                                    }
                                    entry1.Add(str, Count1);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar);
                            }

                                //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                entryLength1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength1.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar);
                            }
                        }
                        //饼状图
                        if (this.comboBoxEdit1.SelectedIndex == 1)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    int n1 = 0, Count1 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            Count1 = n1++;
                                        }
                                    }
                                    entry1.Add(str, Count1);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }

                                //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                entryLength1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength1.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }
                        }
                        //折线图
                        if (this.comboBoxEdit1.SelectedIndex == 2)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    int n1 = 0, Count1 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            Count1 = n1++;
                                        }
                                    }
                                    entry1.Add(str, Count1);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }

                                //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                entryLength1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength1.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }
                        }
                        //3d柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 3)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    int n1 = 0, Count1 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            Count1 = n1++;
                                        }
                                    }
                                    entry1.Add(str, Count1);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }

                                //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                entryLength1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength1.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }
                        }
                        //3d折线图
                        if (this.comboBoxEdit1.SelectedIndex == 4)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    int n1 = 0, Count1 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            Count1 = n1++;
                                        }
                                    }
                                    entry1.Add(str, Count1);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }

                                //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry1.Clear();
                                entryLength1.Clear();
                                foreach (string str in pipestatstype1)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[5].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength1.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[5].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength1)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[5].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }
                        } 
                    }                   
                    break;

                //管线规格
                case 3:
                    if (_dtshow != null)
                    {
                        _dtshow.Clear();

                        //柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 0)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                entry3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    int n3 = 0, Count3 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            Count3 = n3++;
                                        }
                                    }
                                    entry3.Add(str, Count3);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                entryLength3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength3.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar);
                            }
                        }
                        //饼状图
                        if (this.comboBoxEdit1.SelectedIndex == 1)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    int n3 = 0, Count3 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            Count3 = n3++;
                                        }
                                    }
                                    entry3.Add(str, Count3);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                entryLength3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength3.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Pie);
                            }
                        }

                        //折线图
                        if (this.comboBoxEdit1.SelectedIndex == 1)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    int n3 = 0, Count3 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            Count3 = n3++;
                                        }
                                    }
                                    entry3.Add(str, Count3);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                entryLength3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength3.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line);
                            }
                        }
                        //3d柱状图
                        if (this.comboBoxEdit1.SelectedIndex == 3)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    int n3 = 0, Count3 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            Count3 = n3++;
                                        }
                                    }
                                    entry3.Add(str, Count3);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                entryLength3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength3.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Bar3D);
                            }
                        }
                        //3d折线图
                        if (this.comboBoxEdit1.SelectedIndex == 4)
                        {
                            //个数
                            if (this.radioGroup2.SelectedIndex == 0)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    int n3 = 0, Count3 = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {

                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            Count3 = n3++;
                                        }
                                    }
                                    entry3.Add(str, Count3);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[8].ColumnName, typeof(int));
                                foreach (var var in entry3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    int value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[8].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }
                            //管线总长度
                            else if (this.radioGroup2.SelectedIndex == 1)
                            {
                                _dtshow.Clear();
                                entry3.Clear();
                                entryLength3.Clear();
                                foreach (string str in pipestatstype3)
                                {
                                    double Count = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row[dt.Columns[7].ColumnName].ToString() == str)
                                        {
                                            string pipelenggthName = row[dt.Columns[11].ColumnName].ToString();
                                            double pipelenggth = double.Parse(pipelenggthName);
                                            Count += pipelenggth;
                                        }
                                    }
                                    entryLength3.Add(str, Count);
                                }
                                dtshow.Columns.Add(dt.Columns[7].ColumnName, typeof(String));
                                dtshow.Columns.Add(dt.Columns[11].ColumnName, typeof(double));
                                foreach (var var in entryLength3)
                                {
                                    DataRow dr = dtshow.NewRow();
                                    string strname = var.Key;
                                    double value = var.Value;
                                    dr[dt.Columns[7].ColumnName] = strname;
                                    dr[dt.Columns[11].ColumnName] = value;
                                    dtshow.Rows.Add(dr);
                                }
                                _dtshow = dtshow;
                                this.LoadChart(ViewType.Line3D);
                            }
                        }


                        this.chartControl1.Refresh();
                        
                     }                    
                    break;
           

               }
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(this.radioGroup2.SelectedIndex)
            {
                    //个数
                case 0:
                    if (this.comboBoxEdit1.SelectedIndex == 0)
                    {
                        _dtshow = ConvertDTForChart(_dtstats);
                        this.LoadChart(ViewType.Bar);
                    }
                    else if (this.comboBoxEdit1.SelectedIndex == 1)
                    {
                        _dtshow = ConvertDTForChart(_dtstats);
                        this.LoadChart(ViewType.Pie);
                    }
                    else if (this.comboBoxEdit1.SelectedIndex == 2)
                    {
                        _dtshow = ConvertDTForChart(_dtstats);
                        this.LoadChart(ViewType.Line);
                    }
                    else if (this.comboBoxEdit1.SelectedIndex == 3)
                    {
                        _dtshow = ConvertDTForChart(_dtstats);
                        this.LoadChart(ViewType.Bar3D);
                    }
                    else if (this.comboBoxEdit1.SelectedIndex == 4)
                    {
                        _dtshow = ConvertDTForChart(_dtstats);
                        this.LoadChart(ViewType.Line3D);
                    }                   
                    break;
                    //管线长度
                case 1:

                        if (this.comboBoxEdit1.SelectedIndex == 0)
                        {
                            _dtshow = ConvertDTForChart(_dtstats);
                            this.LoadChart(ViewType.Bar);
                        }
                        else if (this.comboBoxEdit1.SelectedIndex == 1)
                        {
                            _dtshow = ConvertDTForChart(_dtstats);
                            this.LoadChart(ViewType.Pie);
                        }
                        else if (this.comboBoxEdit1.SelectedIndex == 2)
                        {
                            _dtshow = ConvertDTForChart(_dtstats);
                            this.LoadChart(ViewType.Line);
                        }
                        else if (this.comboBoxEdit1.SelectedIndex == 3)
                        {
                            _dtshow = ConvertDTForChart(_dtstats);
                            this.LoadChart(ViewType.Bar3D);
                        }
                        else if (this.comboBoxEdit1.SelectedIndex == 4)
                        {
                            _dtshow = ConvertDTForChart(_dtstats);
                            this.LoadChart(ViewType.Line3D);
                        }
                      
                    break;

            }
               
        }

//         private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
//         {
//             switch (this.radioGroup1.SelectedIndex)
//             {
//                 case 0:
//                     this.chartControl1.Refresh();
//                     break;
// 
//                 case 1:
//                     this.chartControl1.Refresh();
//                     break;
// 
//                 case 2:
//                     this.chartControl1.Refresh();
//                     break;
// 
//                 case 3:
//                     this.chartControl1.Refresh();
//                     break;
//             }
// 
//         }


      

       


    }
}

