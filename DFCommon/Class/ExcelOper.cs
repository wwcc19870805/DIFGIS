using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;

namespace DFCommon.Class
{
    public class ExcelOper
    {
        public static bool ExportExcel(DataTable dt, string filePath, bool bMerge = false)
        {
            bool bRes = false;
            try
            {
                if (!string.IsNullOrEmpty(filePath) && null != dt && dt.Rows.Count > 0)
                {
                    HSSFWorkbook book = new HSSFWorkbook();
                    ISheet sheet = book.CreateSheet(dt.TableName);

                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(dt.Columns[i].Caption);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row2 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                        }
                    }
                    //列宽自适应，只对英文和数字有效
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                    //获取当前列的宽度，然后对比本列的长度，取最大值
                    for (int columnNum = 0; columnNum <= dt.Rows.Count; columnNum++)
                    {

                        int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                        for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                        {
                            IRow currentRow;
                            //当前行未被使用过
                            if (sheet.GetRow(rowNum) == null)
                            {
                                currentRow = sheet.CreateRow(rowNum);
                            }
                            else
                            {
                                currentRow = sheet.GetRow(rowNum);
                            }

                            if (currentRow.GetCell(columnNum) != null)
                            {
                                ICell currentCell = currentRow.GetCell(columnNum);
                                int length = System.Text.Encoding.Default.GetBytes(currentCell.ToString()).Length;
                                if (columnWidth < length)
                                {
                                    columnWidth = length;
                                }
                            }
                        }
                        sheet.SetColumnWidth(columnNum, columnWidth * 256);
                    }
                    if (bMerge)
                    {
                        HSSFCellStyle cellStyle = (HSSFCellStyle)book.CreateCellStyle();
                        cellStyle.VerticalAlignment = VerticalAlignment.Center;

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            int indexs = 0;
                            int indexe = 0;
                            string vtemp = dt.Rows[0][i].ToString();
                            for (int j = 0; j < dt.Rows.Count - 1; j++)
                            {
                                string temp = dt.Rows[j + 1][i].ToString();
                                if (temp != vtemp)
                                {
                                    indexe = j + 1;
                                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(indexs + 1, indexe, i, i));
                                    ICell cell = sheet.GetRow(indexs + 1).GetCell(i);
                                    if(cell != null) cell.CellStyle = cellStyle;
                                    indexs = j + 1;
                                    vtemp = temp;
                                }
                            }
                            if (indexe < dt.Rows.Count)
                            {
                                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(indexs + 1, dt.Rows.Count, i, i));
                                ICell cell = sheet.GetRow(indexs + 1).GetCell(i);
                                if (cell != null) cell.CellStyle = cellStyle;
                            }
                        }
                    }
                    // 写入到客户端  
                    using (MemoryStream ms = new MemoryStream())
                    {
                        book.Write(ms);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            byte[] data = ms.ToArray();
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                        book = null;
                    }
                    bRes = true;
                }
            }
            catch (Exception ex)
            {
                bRes = false;
            }
            return bRes;
        }

    }
}
