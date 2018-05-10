using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DevExpress.XtraEditors;

namespace DF2DDataCheck.Command
{
    class CmdUniqueCheck : AbstractMap2DCommand
    {
        IMapControl2 m_pMapControl;
        IMap2DView mapView;
        DF2DApplication app;

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
            
            IFeatureCursor pFeaCursor;
            IQueryFilter pFilter = new QueryFilterClass();
            IFeature pFea;


            mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            app = (DF2DApplication)this.Hook;
            if (app == null || app.Current2DMapControl == null) return;
            m_pMapControl = app.Current2DMapControl;

            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
            if (list == null) return;
            Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
            WaitForm.Start("开始数据唯一性检查..", "请稍候");
            foreach (DF2DFeatureClass dfcc in list)
            {
                IFeatureClass fc = dfcc.GetFeatureClass();
                if (fc == null) return;
                WaitForm.SetCaption("正在检查图层：" + " " + fc.AliasName);
                FacilityClass fac = dfcc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("detectid");
                if (fi1 == null) continue;

                DataTable dt = GetDataTableByStruture();
                string TableName = (fc as IDataset).Name;
                pFilter.WhereClause = fi1.Name + " in (select " + fi1.Name + " from " + (fc as IDataset).Name + " group by " + fi1.Name + " having count(" + fi1.Name + ") > 1) order by " + fi1.Name + " asc";
               pFeaCursor = fc.Search(pFilter, true);               
                while ((pFea = pFeaCursor.NextFeature()) != null)
               {
                      
                        DataRow dr = dt.NewRow();
                        dr["ErrorFeatureID"] = pFea.OID;
                        dr["FeatureofClass"] = fc.AliasName;
                        dr["FeatureofLayer"] = (fc as IDataset).Name;
                        dr["FeatureClass"] = fc;
                        dr["ErrorType"] = "【物探点名】字段值重复";
                        dt.Rows.Add(dr);
                        //Console.WriteLine(pFea.OID);
              }
   
                if (dt.Rows.Count > 0) dict[fc] = dt;
            }
            //if (dict.Count == 0)
            //{

            //    XtraMessageBox.Show("提示表格数据为空！");
                
            //}
            WaitForm.Stop();
            FormCheckResult dlg = new FormCheckResult(dict, m_pMapControl);
            dlg.Text = this.CommandName;
            dlg.Show();

        }

    }
}
