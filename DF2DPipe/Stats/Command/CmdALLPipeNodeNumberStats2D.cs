using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF2DPipe.Stats.Frm;
using System.Data;
using DFDataConfig.Logic;
using DF2DData.Class;
using DFDataConfig.Class;
using DFWinForms.Class;
using DevExpress.XtraEditors;
using DFWinForms.Command;
using ESRI.ArcGIS.Geodatabase;
using DFCommon.Class;
using System.IO;

namespace DF2DPipe.Stats.Command
{
    class CmdALLPipeNodeNumberStats2D : AbstractCommand
    {
        DataTable dtstats;
        private DataTable DoStats()
        {
            DataTable dtResult = new DataTable();
            dtResult.TableName = "DataStats";
            dtResult.Columns.AddRange(new DataColumn[]{new DataColumn("PIPENODETYPE"),new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
            new DataColumn("NUMBER",typeof(long)),new DataColumn("TOTALNUMBER",typeof(long))});

            string localDataPath = SystemInfo.Instance.LocalDataPath;
            string tmpPath = "";
            tmpPath = Path.Combine(localDataPath, "Stats");
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
            string filePath = tmpPath + "\\全库2D管点统计.xml";
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
            List<MajorClass> list = LogicDataStructureManage2D.Instance.GetAllMajorClass();
            foreach (MajorClass mc in list)
            {
                string[] arrFc2DId = mc.Fc2D.Split(';');
                if (arrFc2DId == null) continue;
                long majorclasscount = 0;
                int indexStart = dtResult.Rows.Count;
                foreach (SubClass sc in mc.SubClasses)
                {
                    long sccount = 0;
                    bool bHave = false;
                    foreach (string fc2DId in arrFc2DId)
                    {
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != "PipeNode") continue;
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc) + mc.ClassifyField + " =  '" + sc.Name + "'";
                      
                        int count = fc.FeatureCount(filter);
                        if (count == 0) continue;
                        bHave = true;
                        sccount += count;
                    }
                    if (bHave)
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["PIPENODETYPE"] = mc;
                        dr["FIELDNAME"] = "";
                        dr["PVALUE"] = sc;
                        dr["NUMBER"] = sccount;
                        majorclasscount += sccount;
                        dtResult.Rows.Add(dr);
                  
                    }
                }
                int indexEnd = dtResult.Rows.Count;
                for (int i = indexStart; i < indexEnd; i++)
                {
                    DataRow dr = dtResult.Rows[i];
                    dr["TOTALNUMBER"] = majorclasscount;
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
                FrmPipeNodeStatsOutput dialog = new FrmPipeNodeStatsOutput();
                dialog.SetData1(dtResult);
                dialog.ShowDialog();
            }
            catch
            {
                WaitForm.Stop();
            }
        }
    }
}
