using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DF2DControl.Command;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using DFWinForms.Service;
using DF2DData.Class;
using DFWinForms.Class;
using DFDataConfig.Class;
using System.Windows.Forms;
using DF2DDataCheck.Frm;
using DFDataConfig.Logic;
using DevExpress.XtraEditors;

namespace DF2DDataCheck.Command
{
    class CmdConsistencyCheck : AbstractMap2DCommand
    {
        IMapControl2 m_pMapControl;
        IMap2DView mapView;
        DF2DApplication app;
        private ArrayList m_arrPipeType;

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
            int i = 0, k;            
            string strDicValue;
            string strPntID;         
            IFeatureClass Pntfc;
            IFeatureClass Arcfc;
            IFeatureCursor pFeaCursor;
            IQueryFilter pFilter = new QueryFilterClass();
            IFeature pFea;
            string[] arrFc2DId;

            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;
            frmSelType frmType = new frmSelType();
            if (frmType.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<DF2DFeatureClass> pPntlist = new List<DF2DFeatureClass>();
                    List<DF2DFeatureClass> pArclist = new List<DF2DFeatureClass>();

                    m_arrPipeType = frmType.PipeType;
                    for (i = 0; i < m_arrPipeType.Count; i++)
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
                                pPntlist.Add(dffc);
                                continue;
                            }
                            if (dffc.GetFacilityClassName() == "PipeLine")
                            {
                                pArclist.Add(dffc);
                                continue;
                            }
                        }
                    }
                    if ((pPntlist == null) || (pArclist == null)) return;
                    Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
                    WaitForm.Start("开始数据一致性检查..", "请稍候");
                    int count = 0;
                    foreach (DF2DFeatureClass Pntdfcc in pPntlist)
                    {
                        DataTable dt = GetDataTableByStruture();
                        DF2DFeatureClass Arcdfcc = pArclist[count];
                        Pntfc = Pntdfcc.GetFeatureClass();
                        Arcfc = Arcdfcc.GetFeatureClass();
                        if (Pntfc == null && Arcfc == null) return;
                        WaitForm.SetCaption("正在检查图层：" + " " + Pntfc.AliasName);
                        FacilityClass fac = Pntdfcc.GetFacilityClass();
                        if (fac == null) continue;
                        FacilityClass facc = Arcdfcc.GetFacilityClass();
                        if (facc == null) continue;
                        //List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                        DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("LinkType");
                        if (fi == null) continue;
                        DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("Detectid");
                        if (fi1 == null) continue;
                        DFDataConfig.Class.FieldInfo fi2 = facc.GetFieldInfoBySystemName("StartNo");
                        if (fi2 == null) continue;
                        DFDataConfig.Class.FieldInfo fi3 = facc.GetFieldInfoBySystemName("EndNo");
                        if (fi3 == null) continue;
                        DFDataConfig.Class.FieldInfo fi4 = fac.GetFieldInfoBySystemName("UState");
                        if (fi4 == null) continue;
                        //                 DFDataConfig.Class.FieldInfo fi5 = facc.GetFieldInfoBySystemName("source");
                        //                 if (fi5 == null) continue;                  
                        pFeaCursor = Pntfc.Search(null, true);

                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                            bool b1 = false, b2 = false;

                            strPntID = pFea.get_Value(Pntfc.FindField(fi1.Name)).ToString();
                            strDicValue = pFea.get_Value(Pntfc.FindField(fi.Name)).ToString();


                            if (strDicValue == "三通" || strDicValue == "三分支")
                            {
                                k = 3;
                            }
                            else if (strDicValue == "四通" || strDicValue == "四分支")
                            {
                                k = 4;
                            }
                            else if (strDicValue == "五通" || strDicValue == "五分支")
                            {
                                k = 5;
                            }
                            else if (strDicValue == "六通" || strDicValue == "六分支")
                            {
                                k = 6;
                            }
                            else if (strDicValue == "七通" || strDicValue == "七分支")
                            {
                                k = 7;
                            }
                            else if (strDicValue == "八通" || strDicValue == "八分支")
                            {
                                k = 8;
                            }
                            else
                            {
                                k = 2;
                            }
                            pFilter.WhereClause = fi2.Name + " = '" + strPntID + "' or " + fi3.Name + " = '" + strPntID + "'";
                            if (Arcfc.FeatureCount(pFilter) != k)
                            {
                                b1 = true;
                            }
                            if (Pntfc.FindField(fi4.Name) != -1 && Arcfc.FindField(fi4.Name) != -1)
                            {

                                pFilter.WhereClause = "(" + fi2.Name + " = '" + strPntID + "' or " + fi3.Name + " = '" + strPntID + "') and " + fi4.Name + " <> '" + strDicValue + "'";
                                if (Arcfc.FeatureCount(pFilter) > 0)
                                {
                                    b2 = true;
                                }
                            }




                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = Pntfc.AliasName;
                            dr["FeatureofLayer"] = (Pntfc as IDataset).Name;
                            dr["FeatureClass"] = Pntfc;

                            if (b1 && b2) dr["ErrorType"] = "多通多分支与管点连接管线数量不一致；管点与相连接管线的使用状态不一致";
                            else if (b1 && !b2) dr["ErrorType"] = "多通多分支与管点连接管线数量不一致";
                            else if (!b1 && b2) dr["ErrorType"] = "管点与相连接管线的使用状态不一致";
                            dt.Rows.Add(dr);
                            //Console.WriteLine(pFea.OID + " " + Pntfc.AliasName);
                        }

                        if (dt.Rows.Count > 0) dict[Pntfc] = dt;
                        count++;
                    }
                    WaitForm.Stop();
                    FormCheckResult dlg = new FormCheckResult(dict, m_pMapControl);
                    dlg.Text = this.CommandName;
                    dlg.Show();
                }
                catch (System.Exception ex)
                {

                }
            }
           
        }



    }
}

