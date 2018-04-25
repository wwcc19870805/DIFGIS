using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DF2DPipe.Stats.Frm;
using DevExpress.XtraEditors;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmStatsByRoadOutput2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    
        public FrmStatsByRoadOutput2D()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(970, 509);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(869, 475);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(89, 22);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "报表输出";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(776, 475);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(89, 22);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "统计图表";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(946, 459);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.gridView1_CellMerge);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "管线类型";
            this.gridColumn1.FieldName = "PIPELINETYPE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "属性";
            this.gridColumn2.FieldName = "FIELDNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "道路名称";
            this.gridColumn3.FieldName = "PVALUE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "长度";
            this.gridColumn4.FieldName = "LENGTH";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "总长度";
            this.gridColumn5.FieldName = "TOTALLENGTH";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(970, 509);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(950, 463);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 463);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(764, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.simpleButton1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(764, 463);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton2;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(857, 463);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmStatsByRoadOutput2D
            // 
            this.ClientSize = new System.Drawing.Size(970, 509);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmStatsByRoadOutput2D";
            this.Load += new System.EventHandler(this.FrmStatsByRoadOutput2D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        
        

        private DataTable _dt;
        private DataTable _dtstats;
        private string statsType = "pipeline";

        public void SetData(DataTable dt)
        {
            this._dt = dt;
            this.gridControl1.DataSource = dt;
        }

        public void SetStatsData(DataTable dt)
        {
            this._dtstats = dt;
            //this.chartTitle = title;
        }


        private void FrmStatsByRoadOutput2D_Load(object sender, EventArgs e)
        {

        }

        //统计图表
        private void simpleButton1_Click(object sender, EventArgs e)
         {
             FrmStatasByRoadChart2D dialog = new FrmStatasByRoadChart2D((DataTable)this.gridControl1.DataSource, statsType);
            dialog.ShowDialog();

        }

        //报表输出
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Excel Files(*.xls)|*.xls|Excel Files(*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = saveFileDialog.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string filePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                bool suc = ExportToExcel(_dt, localFilePath);
                if (suc)
                {
                    MessageBox.Show("导出成功！");
                }
            }

        }

        private bool ExportToExcel(DataTable dt, string path)
        {
            bool succeed = false;
            if (dt != null)
            {
                Microsoft.Office.Interop.Excel.Application xlApp = null;
                try
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                    object oMissing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;
                    xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                    //xlSheet.Name = dt.TableName;
                    int rowIndex = 1;
                    int colIndex = 1;
                    int colCount = dt.Columns.Count;
                    int rowCount = dt.Rows.Count;

                    for (int i = 0; i < colCount; i++)
                    {
                        string str = dt.Columns[i].ColumnName;
                        switch (str)
                        {
                            case "PIPELINETYPE":
                                str = "管线类型";
                                break;
                            case "FIELDNAME":
                                str = "属性";
                                break;
                            case "PVALUE":
                                str = "分类";
                                break;
                            case "LENGTH":
                                str = "长度(米)";
                                break;
                            case "TOTALLENGTH":
                                str = "总长度(米)";
                                break;
                        }

                        xlSheet.Cells[rowIndex, colIndex] = str;
                        colIndex++;
                    }
                    //xlSheet.get_Range(xlSheet.Cells[rowIndex, 1], xlSheet.Cells[rowIndex, colCount]).Font.Bold = true;
                    //xlSheet.get_Range(xlSheet.Cells[rowIndex, 1], xlSheet.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                    //xlSheet.get_Range(xlSheet.Cells[rowIndex, 1], xlSheet.Cells[rowCount + 1, colCount]).Font.Size = "10";
                    rowIndex++;

                    for (int i = 0; i < rowCount; i++)
                    {
                        colIndex = 1;
                        for (int j = 0; j < colCount; j++)
                        {
                            xlSheet.Cells[rowIndex, colIndex] = dt.Rows[i][j].ToString();
                            colIndex++;
                        }
                        rowIndex++;
                    }


                    MergeCell_Second(ref xlSheet, 2, dt.Rows.Count, "A");
                    //MergeCell_Second(ref xlSheet, 2, dt.Rows.Count, "B");
                    //MergeCell_Second(ref xlSheet, 2, dt.Rows.Count, "E");
                    xlSheet.Cells.EntireColumn.AutoFit();
                    xlApp.DisplayAlerts = false;
                    xlBook.SaveCopyAs(path);
                    xlBook.Close(false, null, null);
                    xlApp.Workbooks.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                    xlBook = null;
                    succeed = true;

                }
                catch (System.Exception ex)
                {
                    succeed = false;
                }
                finally
                {
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    xlApp = null;

                }
            }
            return succeed;
        }
        private void MergeCell_Second(ref Microsoft.Office.Interop.Excel.Worksheet mySheet, int startLine, int recCount, string col)
        {
            string qy1 = mySheet.get_Range(col + startLine.ToString(), col + startLine.ToString()).Text.ToString();//获得起始行合并列单元格的填充内容
            Microsoft.Office.Interop.Excel.Range rg1;
            Microsoft.Office.Interop.Excel.Range rg2;
            Microsoft.Office.Interop.Excel.Range rg5;
            string strtemp = "";
            bool endCycle = false;

            //从起始行到终止行做循环
            for (int i = startLine; i <= recCount + startLine - 1 && !endCycle; i++)
            {
                for (int j = i + 1; j <= recCount + startLine - 1; j++)
                {
                    rg1 = mySheet.get_Range(col + j.ToString(), col + j.ToString());//获得下一行的填充内容
                    strtemp = rg1.Text.ToString().Trim();
                    if (strtemp.Trim() == qy1.Trim())//内容等于初始内容
                    {
                        rg1 = mySheet.get_Range(col + i.ToString(), col + j.ToString());//选取上条合并位置和当前行的合并区域
                        rg1.ClearContents();//清空要合并的区域
                        rg1.MergeCells = true;
                        if (col == "A") mySheet.Cells[i, 1] = qy1;

                        rg2 = mySheet.get_Range("B" + i.ToString(), "B" + j.ToString());
                        string qy2 = mySheet.get_Range("B" + i.ToString(), "B" + i.ToString()).Text.ToString();
                        rg2.ClearContents();
                        rg2.MergeCells = true;
                        mySheet.Cells[i, 2] = qy2;

                        rg5 = mySheet.get_Range("E" + i.ToString(), "E" + j.ToString());
                        string qy5 = mySheet.get_Range("E" + i.ToString(), "E" + i.ToString()).Text.ToString();
                        rg5.ClearContents();
                        rg5.MergeCells = true;
                        mySheet.Cells[i, 5] = qy5;
                        if (j == recCount + startLine - 1) endCycle = true;
                    }
                    else//内容不等于初始内容
                    {
                        i = j - 1;//i获取新值
                        qy1 = mySheet.get_Range(col + j.ToString(), col + j.ToString()).Text.ToString();
                        break;
                    }
                }
            }

//             if (!this.gridColumn2.Visible && this.gridColumn5.Visible)
//             {
//                 mySheet.Columns[2].Delete();
//             }
//             else if (!this.gridColumn2.Visible && !this.gridColumn5.Visible)
//             {
//                 mySheet.Columns[2].Delete();
//                 mySheet.Columns[4].Delete();
//             }

        }


        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "FIELDNAME" || e.Column.FieldName == "TOTALLENGTH")
                {
                    DataRow dr1 = this.gridView1.GetDataRow(e.RowHandle1);
                    DataRow dr2 = this.gridView1.GetDataRow(e.RowHandle2);
                    if (dr1["PIPELINETYPE"] == dr2["PIPELINETYPE"])
                    {
                        e.Merge = true;
                    }
                    else
                    {
                        e.Merge = false;
                    }
                    e.Handled = true;
                }
            }
            catch
            {

            }

        }
    }
}
