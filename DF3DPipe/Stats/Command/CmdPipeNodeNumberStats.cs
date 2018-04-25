using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DPipe.Stats.Frm;
using System.Data;
using DFDataConfig.Logic;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DFWinForms.Class;
using DevExpress.XtraEditors;
using System.IO;
using System.Windows.Forms;
using DFCommon.Class;

namespace DF3DPipe.Stats.Command
{
    public class CmdPipeNodeNumberStats : AbstractCommand
    {
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
            string filePath = tmpPath + "\\全库3D管点统计.xml";
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
                long majorclasscount = 0;
                int indexStart = dtResult.Rows.Count;
                foreach (SubClass sc in mc.SubClasses)
                {
                    long sccount = 0;
                    bool bHave = false;
                    foreach (string fc3DId in arrFc3DId)
                    {
                        DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != "PipeNode") continue;
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "GroupId = " + sc.GroupId;
                        int count = fc.GetCount(filter);
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
            catch(Exception ex)
            {
                WaitForm.Stop();
            }
        }

    }
}
