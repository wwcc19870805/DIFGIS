using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraEditors;


namespace DF2DPipe.Class
{
    class OutputToExcel
    {
        private DataTable Table;
        private string BookName;

        public OutputToExcel(DataTable dt, string name)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.Table = dt;
            this.BookName = getRightName(name);
        }

        /// <summary>
        /// 导出表格数据
        /// </summary>
        /// <param name="dt"></param>
        public void ProduceExcel(DataTable dt)
        {
            Excel.ApplicationClass xls = new Excel.ApplicationClass();
            Excel.Workbook xlsBook = xls.Workbooks.Add(true);
            try
            {
                Excel.Worksheet xlsSheet = (Excel.Worksheet)xls.Worksheets["sheet1"];
                xlsSheet.Name = BookName;
                Application.DoEvents();

                xlsSheet.Cells[1, 1] = "数据导出结果";
                xlsSheet.Cells[2, 1] = "要素ID";
                xlsSheet.Cells[2, 2] = "状态";
                xlsSheet.Cells[2, 3] = "使用年限";
                xlsSheet.Cells[2, 4] = "年限预警";
                xlsSheet.Cells[2, 5] = "开始使用时间";
                xlsSheet.Cells[2, 6] = "权属单位";
                xlsSheet.Cells[2, 7] = "所在道路";
                xlsSheet.Cells[2, 8] = "管径规格";

                //写每行的数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {//遍历每列 
                        string val = "";
                        object tmp = dt.Rows[i][j];
                        if (tmp != null)
                        {
                            val = tmp.ToString();
                        }
                        xlsSheet.Cells[i + 3, j + 1] = val;
                    }

                    Application.DoEvents();
                }

                #region 美化界面(边框)

                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders.LineStyle = 1;
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;

                xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]).Borders.LineStyle = 1;
                xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
                xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
                xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;
                xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;


                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, 16]).Merge(null);
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, 16]).Font.Bold = true;

                xlsSheet.Rows.AutoFit();
                xlsSheet.Columns.AutoFit();
                xlsSheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlsSheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Application.DoEvents();
                xls.Visible = true;
                #endregion
                string path = Application.StartupPath + "\\" + BookName + ".xls";
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                xlsBook.SaveAs(path, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                xlsBook.Close(null, null, null);
                xls = null;
            }
        }

//         public void ProduceExcel_1(DataTable dt)
//         {
//             Excel.ApplicationClass xls = new Excel.ApplicationClass();
//             Excel.Workbook xlsBook = xls.Workbooks.Add(true);
//             try
//             {
//                 Excel.Worksheet xlsSheet = (Excel.Worksheet)xls.Worksheets["sheet1"];
//                 xlsSheet.Name = BookName;
//                 Application.DoEvents();
// 
//                 xlsSheet.Cells[1, 1] = "数据检查结果";
//                 xlsSheet.Cells[2, 1] = "错误类型";
//                 xlsSheet.Cells[2, 2] = "要素所在要素类";
//                 xlsSheet.Cells[2, 3] = "要素所在图层";
//                 xlsSheet.Cells[2, 4] = "错误要素ID";
//                 //写每行的数据
//                 for (int i = 0; i < dt.Rows.Count; i++)
//                 {
//                     for (int j = 0; j < dt.Columns.Count; j++)
//                     {//遍历每列 
//                         string val = "";
//                         object tmp = dt.Rows[i][j];
//                         if (tmp != null)
//                         {
//                             val = tmp.ToString();
//                         }
//                         xlsSheet.Cells[i + 3, j + 1] = val;
//                     }
// 
//                     Application.DoEvents();
//                 }
// 
//                 #region 美化界面(边框)
// 
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders.LineStyle = 1;
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;
// 
//                 xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders.LineStyle = 1;
//                 xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
//                 xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
//                 xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;
//                 xlsSheet.get_Range(xlsSheet.Cells[2, 1], xlsSheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;
// 
// 
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, 16]).Merge(null);
//                 //				xlsSheet.get_Range(xlsSheet.Cells[2,2], xlsSheet.Cells[2,3]).Merge(null);
//                 //				xlsSheet.get_Range(xlsSheet.Cells[2,5], xlsSheet.Cells[2,6]).Merge(null);
//                 //				xlsSheet.get_Range(xlsSheet.Cells[2,11], xlsSheet.Cells[2,12]).Merge(null);
//                 //				xlsSheet.get_Range(xlsSheet.Cells[2,13], xlsSheet.Cells[2,14]).Merge(null);
//                 //				xlsSheet.get_Range(xlsSheet.Cells[2,15], xlsSheet.Cells[2,16]).Merge(null);
// 
//                 xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, 16]).Font.Bold = true;
// 
//                 xlsSheet.Rows.AutoFit();
//                 xlsSheet.Columns.AutoFit();
//                 xlsSheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
//                 xlsSheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
// 
//                 Application.DoEvents();
//                 xls.Visible = true;
//                 #endregion
//                 string path = Application.StartupPath + "\\" + BookName + ".xls";
//                 if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
//                 xlsBook.SaveAs(path, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show(ex.Message);
//                 xlsBook.Close(null, null, null);
//                 xls = null;
//             }
//         }

        private string getRightName(string name)
        {
            string a;
            a = name;
            a = a.Replace(@"/", ".");
            a = a.Replace(@"\", ".");
            a = a.Replace(@":", ".");
            a = a.Replace(@"*", ".");
            a = a.Replace(@"?", ".");
            a = a.Replace(@"""", ".");
            a = a.Replace(@"<", ".");
            a = a.Replace(@">", ".");
            a = a.Replace(@"|", ".");
            if (a == "") a = "Book1";

            return a;
        }
    }
}
