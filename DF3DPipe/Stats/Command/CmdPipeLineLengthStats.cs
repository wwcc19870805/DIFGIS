using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DPipe.Stats.Frm;
using DFDataConfig.Logic;
using System.Data;
using DF3DData.Class;
using DFDataConfig.Class;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using DevExpress.XtraEditors;
using DFCommon.Class;
using System.IO;

namespace DF3DPipe.Stats.Command
{
    public class CmdPipeLineLengthStats : AbstractCommand
    {
        private DataTable DoStats()
        {
            DataTable dtResult = new DataTable();
            dtResult.TableName = "DataStats";
            dtResult.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});


            string localDataPath = SystemInfo.Instance.LocalDataPath;
            string tmpPath = "";
            tmpPath = Path.Combine(localDataPath, "Stats");
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
            string filePath = tmpPath + "\\全库3D管线统计.xml";
            bool bHaveXml = false;
            if (File.Exists(filePath))
            {
                dtResult.ReadXml(filePath);
                if (dtResult != null && dtResult.Rows.Count != 0)
                {
                    bHaveXml = true;
                    return dtResult;
                }
                else bHaveXml = false;
            }
            

            List<MajorClass> list = LogicDataStructureManage3D.Instance.GetAllMajorClass();
            foreach (MajorClass mc in list)
            {
                string[] arrFc3DId = mc.Fc3D.Split(';');
                if (arrFc3DId == null) continue;
                double majorclasslength = 0.0;
                int indexStart = dtResult.Rows.Count;
                foreach (SubClass sc in mc.SubClasses)
                {
                    double subclasslength = 0.0;
                    bool bHave = false;
                    foreach (string fc3DId in arrFc3DId)
                    {
                        DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                        DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength");
                        if (fiPipeLength == null) continue;
                        int indexPipeLength = fc.GetFields().IndexOf(fiPipeLength.Name);
                        if (indexPipeLength == -1) continue;

                        IQueryFilter filter = new QueryFilter();
                        filter.SubFields = fiPipeLength.Name;
                        filter.WhereClause = "GroupId = " + sc.GroupId;
                        int count = fc.GetCount(filter);
                        if (count == 0) continue;
                        int loop = (int)Math.Ceiling(count / 800.0);
                        for (int k = 1; k <= loop; k++)
                        {
                            if (k == 1)
                            {
                                filter.ResultBeginIndex = 0;
                            }
                            else
                            {
                                filter.ResultBeginIndex = (k - 1) * 800;
                            }
                            filter.ResultLimit = 800;
                            IFdeCursor cur = null;
                            IRowBuffer rowBuf = null;
                            try
                            {
                                cur = fc.Search(filter, true);
                                while ((rowBuf = cur.NextRow()) != null)
                                {
                                    if (!rowBuf.IsNull(0))
                                    {
                                        object tempobj = rowBuf.GetValue(0);
                                        double dtemp = 0.0;
                                        if (tempobj != null && double.TryParse(tempobj.ToString(), out dtemp))
                                        {
                                            subclasslength += dtemp;
                                            bHave = true;
                                        }
                                    }
                                }
                            }
                            catch { }
                            finally
                            {
                                if (cur != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cur);
                                    cur = null;
                                }
                                if (rowBuf != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rowBuf);
                                    rowBuf = null;
                                }
                            }
                        }
                    }
                    if (bHave)
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["PIPELINETYPE"] = mc;
                        dr["FIELDNAME"] = "";
                        dr["PVALUE"] = sc;
                        dr["LENGTH"] = subclasslength.ToString("0.00");
                        majorclasslength += subclasslength;
                        dtResult.Rows.Add(dr);
                    }
                }
                int indexEnd = dtResult.Rows.Count;
                for (int i = indexStart; i < indexEnd; i++)
                {
                    DataRow dr = dtResult.Rows[i];
                    dr["TOTALLENGTH"] = majorclasslength.ToString("0.00");
                }
            }
            if (!bHaveXml) dtResult.WriteXml(filePath);
            return dtResult;
        }

        public override void Run(object sender, EventArgs e)
        {
            try
            {
                WaitForm.Start("正在统计...", "请稍后");
                DataTable dtResult = DoStats();
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("统计结果为空！", "提示");
                    return;
                }
                WaitForm.Stop();
                FrmPipeLineStatsOutput dialog = new FrmPipeLineStatsOutput();
                dialog.SetData1(dtResult);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
            }
        }

    }
}
