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
using System.IO;
using DFCommon.Class;
namespace DF2DPipe.Stats.Command
{
    class CmdALLPipeLineLengthStats2D : AbstractCommand
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
            string filePath = tmpPath + "\\全库2D管线统计.xml";
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
                double majorclasslength = 0.0;
                int indexStart = dtResult.Rows.Count;
                double subclasslength = 0.0;
                foreach (SubClass sc in mc.SubClasses)
                {                   
                    foreach (string fc2DId in arrFc2DId)
                    {
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                        DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");
                        if (fiPipeLength == null) continue;
                        int indexPipeLength = fc.FindField(fiPipeLength.Name);
                        if (indexPipeLength == -1) continue;
                        IField fcfi = fc.Fields.get_Field(indexPipeLength);
                        switch (fcfi.Type)
                        {
                            case esriFieldType.esriFieldTypeBlob:
                            case esriFieldType.esriFieldTypeGeometry:
                            case esriFieldType.esriFieldTypeRaster:
                                continue;
                        }
                        IQueryFilter filter = new QueryFilter();
                        filter.SubFields = fcfi.Name;
                        filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc) +  mc.ClassifyField + " =  '" + sc.Name + "'";
                        IFeatureCursor pFeatureCursor = null;
                        IFeature pFeature = null;
                        double subfieldlength = 0.0;
                        bool bHave = false;
                        #region
                        try
                        {
                            pFeatureCursor = fc.Search(filter, true);
                            while ((pFeature = pFeatureCursor.NextFeature()) != null)
                            {

                                object tempobj = pFeature.get_Value(indexPipeLength);
                                double dtemp = 0.0;
                                if (tempobj != null && double.TryParse(tempobj.ToString(), out dtemp))
                                {
                                    bHave = true;
                                    subfieldlength += dtemp;
                                }
                            }
                        }


                        catch { }
                        finally
                        {
                            if (pFeatureCursor != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                                pFeatureCursor = null;
                            }
                            if (pFeature != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                                pFeature = null;
                            }
                        }
                        #endregion
                        if (bHave)
                        {
                            DataRow dr = dtResult.NewRow();
                            dr["PIPELINETYPE"] = mc;
                            dr["FIELDNAME"] = "";
                            dr["PVALUE"] = sc;
                            subclasslength += subfieldlength;
                            dr["LENGTH"] = subfieldlength.ToString("0.00");
                            dtResult.Rows.Add(dr);                          
                        }
                        
                    }
                    
                }
                int indexEnd = dtResult.Rows.Count;
                for (int i = indexStart; i < indexEnd; i++)
                {
                    DataRow dr = dtResult.Rows[i];
                    dr["TOTALLENGTH"] = subclasslength.ToString("0.00");
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
            catch
            {
                WaitForm.Stop();
            }
        }
    }
}
