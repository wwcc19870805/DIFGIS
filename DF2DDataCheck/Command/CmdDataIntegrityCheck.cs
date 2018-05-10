using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using DF2DDataCheck.Frm;
using DFWinForms.Class;
using DF2DData.Class;
using DFDataConfig.Logic;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DDataCheck.Command
{
    class CmdDataIntegrityCheck : AbstractMap2DCommand
    {

        IMapControl2 m_pMapControl;
        IMap2DView mapView;
        DF2DApplication app;
        private ArrayList m_arrPntField;
        private ArrayList m_arrArcField;
        private ArrayList m_arrPipeType;
        Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();

        private DataTable GetDataTableByStruture()
        {
            DataTable dt = new DataTable();
            dt.TableName = "ErrorList";
            DataColumn column;

            column = new DataColumn();
            column.ColumnName = "ErrorFeatureID";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "ErrorType";
            dt.Columns.Add(column);


            column = new DataColumn();
            column.ColumnName = "FeatureofClass";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FeatureofLayer";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FeatureClass";
            column.DataType = typeof(IFeatureClass);
            dt.Columns.Add(column);
            return dt;
        }


         public override void Run(object sender, EventArgs e)
        {
            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;


            frmIntegrityCheck frmDataCheckSet = new frmIntegrityCheck();
            if (frmDataCheckSet.ShowDialog() == DialogResult.OK)
            {
                m_arrPntField = frmDataCheckSet.PntFieldSel;
                m_arrArcField = frmDataCheckSet.ArcFieldSel;
                m_arrPipeType = frmDataCheckSet.PipeType;

                DataIntegrityCheck();//开始检查


                WaitForm.Stop();
                FormCheckResult dlg = new FormCheckResult(dict, m_pMapControl);
                dlg.Text = this.CommandName;
                dlg.Show();
            }

        }

         //程序检查
        private void DataIntegrityCheck()
        {
            int i, j, n;
            string strFieldName;
            string[] arrFc2DId;
            object val;
            IFeature pFea;
            //IFeatureClass pFeaClass;
            IFeatureCursor pCur;

            List<DF2DFeatureClass> listPnt = new List<DF2DFeatureClass>(); 
            List<DF2DFeatureClass> listArc = new List<DF2DFeatureClass>();

            for (i = 0; i < m_arrPipeType.Count; i++ )
            {
                MajorClass mc = m_arrPipeType[i] as MajorClass;
                arrFc2DId = mc.Fc2D.Split(';');
                if (arrFc2DId == null) continue;
                foreach (string fc2DId in arrFc2DId)
                {
                    DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                    if (dffc == null) continue;
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    if (dffc.GetFacilityClassName() == "PipeNode")
                    {
                        listPnt.Add(dffc);
                    }
                    if (dffc.GetFacilityClassName() == "PipeLine")
                    {
                        listArc.Add(dffc);
                    }
                }
            }
            //遍历所有点表检查
            if (m_arrPntField.Count > 0)
            {
                //List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
                if (listPnt == null) return;
                WaitForm.Start("开始数据完整性检查..", "请稍候");
                foreach (DF2DFeatureClass dffc in listPnt)
                {
                    DataTable dt = GetDataTableByStruture();
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    WaitForm.SetCaption("正在检查图层：" + " " + fc.AliasName);
                    pCur = fc.Search(null, true);
                    while ((pFea = pCur.NextFeature()) != null)
                    {
                        for (j = 0; j < m_arrPntField.Count; j++)
                        {
                            strFieldName = m_arrPntField[j].ToString();
                            if (fc.FindField(strFieldName) > 0)
                            {
                                val = pFea.get_Value(fc.FindField(strFieldName));
                                if (val.ToString() == "" || val == DBNull.Value)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["ErrorFeatureID"] = pFea.OID;
                                    dr["FeatureofClass"] = fc.AliasName;
                                    dr["FeatureofLayer"] = (fc as IDataset).Name;
                                    dr["FeatureClass"] = fc;
                                    dr["ErrorType"] = "【" + strFieldName + "】字段为空";
                                    dt.Rows.Add(dr);

                                }

                            }
                        }

                    }
                    if (dt.Rows.Count > 0) dict[fc] = dt;

                }

                //if (dict.Count == 0)
                //{

                //    XtraMessageBox.Show("提示表格数据为空！");
                     
                //}

            }

            //遍历所有线表检查
            if (m_arrArcField.Count > 0)
            {

                //List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                if (listArc == null) return;

                foreach (DF2DFeatureClass dfcc in listArc)
                {
                    DataTable dt = GetDataTableByStruture();
                    IFeatureClass fc = dfcc.GetFeatureClass();
                    if (fc == null) return;
                    WaitForm.SetCaption(fc.AliasName);
                    pCur = fc.Search(null, true);
                    while ((pFea = pCur.NextFeature()) != null)
                    {
                        for (j = 0; j < m_arrArcField.Count; j++)
                        {
                            strFieldName = m_arrArcField[j].ToString();
                            if (fc.FindField(strFieldName) > 0)
                            {

                                val = pFea.get_Value(fc.FindField(strFieldName));
                                if (val.ToString() == "" || val == DBNull.Value)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["ErrorFeatureID"] = pFea.OID;
                                    dr["FeatureofClass"] = fc.AliasName;
                                    dr["FeatureofLayer"] = (fc as IDataset).Name;
                                    dr["FeatureClass"] = fc;
                                    dr["ErrorType"] = "【" + strFieldName + "】字段为空";
                                    dt.Rows.Add(dr);

                                }
  
                            }

                        }

                    }
                    if (dt.Rows.Count > 0) dict[fc] = dt;
                }

                //if (dict.Count == 0)
                //{

                //    XtraMessageBox.Show("提示表格数据为空！");

                //}
            }

        }

    }
}
