using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using System.Data;
using DF2DData.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using DF2DDataCheck.Frm;
using DFWinForms.Class;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DFWinForms.Service;
using DFCommon;
using DevExpress.XtraEditors;
using DFDataConfig.Logic;

namespace DF2DDataCheck.Command
{
    class CmdTopoCheck : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
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
                List<DF2DFeatureClass> list = new List<DF2DFeatureClass>();
                m_arrPipeType = frmType.PipeType;
                for (int i = 0; i < m_arrPipeType.Count; i++)
                {
                    MajorClass mc = m_arrPipeType[i] as MajorClass;
                    arrFc2DId = mc.Fc2D.Split(';');
                    if (arrFc2DId == null) continue;
                    foreach (string fc2DId in arrFc2DId)
                    {
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                        if (dffc == null || dffc.GetFacilityClassName() != "PipeLine") continue;
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null) continue;
                        //if (fc.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                        //{
                            list.Add(dffc);
                        //}
                    }
                }
                Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
                WaitForm.Start("开始拓扑检查..", "请稍候");
                foreach (DF2DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc == null) continue;
                    FacilityClass fac = dffc.GetFacilityClass();
                    if (fac == null) continue;
                    List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                    DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("StartHeight2D");
                    int index = fc.FindField(fi.Name);
                    WaitForm.SetCaption("正在检查图层：" + " " + fc.AliasName);
                    if (fi == null || index == -1) continue;

                    DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("EndHeight");
                    int index1 = fc.FindField(fi1.Name);
                    if (fi1 == null || index1 == -1) continue;

                    DFDataConfig.Class.FieldInfo fi2 = fac.GetFieldInfoBySystemName("StartDep");
                    int index2 = fc.FindField(fi2.Name);
                    if (fi2 == null || index2 == -1) continue;

                    DFDataConfig.Class.FieldInfo fi3 = fac.GetFieldInfoBySystemName("EndDep");
                    int index3 = fc.FindField(fi3.Name);
                    if (fi3 == null || index3 == -1) continue;

                    DataTable dt = GetDataTableByStruture();
                    IFeatureCursor pFeaCursor = fc.Search(null, true);
                    IFeature pFea = pFeaCursor.NextFeature();
                    while (pFea != null)
                    {
                        //Console.WriteLine(pFea.OID + " " + fc.AliasName);
                        bool b1 = false, b2 = false;
                        string obj = pFea.get_Value(index).ToString();
                        string obj1 = pFea.get_Value(index1).ToString();
                        if (obj != "" && obj1 != "")
                        {
                            double d;
                            if (!double.TryParse(obj1, out d))
                            {
                                d = 0.0;
                            }

                            if (Math.Abs(double.Parse(obj) - double.Parse(obj1)) > 1)
                            {
                                b1 = true;
                            }
                        }
                        string obj2 = pFea.get_Value(index2).ToString();
                        string obj3 = pFea.get_Value(index3).ToString();
                        /* if (obj3 != null) continue;*/
                        if ((obj2 != "") && (obj3 != ""))
                        {
                            double d;
                            if (!double.TryParse(obj2, out d))
                            {
                                d = 0.0;
                            }

                            if (Math.Abs(double.Parse(obj2) - double.Parse(obj3)) > 0.5)
                            {
                                b2 = true;
                            }
                        }
                        if (b1 && b2)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = fc.AliasName;
                            dr["FeatureofLayer"] = (fc as IDataset).Name;
                            dr["FeatureClass"] = fc;
                            if (b1 && b2) dr["ErrorType"] = "管线起止点高程差值超出规范值；管线起止点埋深差值超出规范值";
                            else if (b1 && !b2) dr["ErrorType"] = "管线起止点高程差值超出规范值";
                            else if (b2 && !b1) dr["ErrorType"] = "管线起止点埋深差值超出规范值";
                            dt.Rows.Add(dr);
                        }
                        pFea = pFeaCursor.NextFeature();
                    }
                    if (dt.Rows.Count > 0) dict[fc] = dt;
                }
                WaitForm.Stop();
                FormCheckResult dlg = new FormCheckResult(dict, m_pMapControl);
                dlg.Text = this.CommandName;
                dlg.Show();
            }
        }
    }
}
