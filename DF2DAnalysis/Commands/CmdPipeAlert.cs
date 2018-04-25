using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using DF2DControl.Base;
using DFDataConfig.Class;
using DF2DData.Class;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DFWinForms.Class;
using DevExpress.XtraEditors;
using DF2DAnalysis.Frm;

namespace DF2DAnalysis.Commands
{
    class CmdPipeAlert : AbstractMap2DCommand
    {
        private IMapControl2 m_pMapControl;
        private IMap2DView mapView;
        private DF2DApplication app;
        public static double yearXtime;
        private DataTable GetDataTableByStruture()
        {
            DataTable dt = new DataTable();
            dt.TableName = "PipeAlert";
            DataColumn column;

            column = new DataColumn();
            column.ColumnName = "Matter";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FeatureID";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "UseState";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName ="StartTime";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "UseTime";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "OwnerBy";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Proad";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Standard";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "TimeAlert";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FeatureClass";
            column.DataType = typeof(IFeatureClass);
            dt.Columns.Add(column);


            column = new DataColumn();
            column.ColumnName = "FeatureofClass";         
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "PipeLength";
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
            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
            Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
            WaitForm.Start("开始管线预警检查..", "请稍后");
            foreach (DF2DFeatureClass dffc in list)
            {
                IQueryFilter pFilter = new QueryFilterClass();
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) continue;
                WaitForm.SetCaption("正在检查图层：" + " " + fc.AliasName);
                FacilityClass fac = dffc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("Material");
                int index = fc.FindField(fi.Name);
                if (fi == null || index == -1) continue;

                DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("UseState");
                int index1 = fc.FindField(fi1.Name);
                if (fi1 == null || index1 == -1) continue;

                DFDataConfig.Class.FieldInfo fi2 = fac.GetFieldInfoBySystemName("BuildYear");              
                int index2 = fc.FindField(fi2.Name);
                if (fi2 == null || index2 == -1) continue;

                DFDataConfig.Class.FieldInfo fi3 = fac.GetFieldInfoBySystemName("Owner");               
                int index3 = fc.FindField(fi3.Name);
                if (fi2 == null || index3 == -1) continue;

                DFDataConfig.Class.FieldInfo fi4 = fac.GetFieldInfoBySystemName("Road");
                int index4 = fc.FindField(fi4.Name);
                if (fi4 == null || index4 == -1) continue;

                DFDataConfig.Class.FieldInfo fi5 = fac.GetFieldInfoBySystemName("Diameter");
                int index5 = fc.FindField(fi5.Name);
                if (fi5 == null || index5 == -1) continue;

                DFDataConfig.Class.FieldInfo fi6 = fac.GetFieldInfoBySystemName("PipeLength2D");
                int index6 = fc.FindField(fi6.Name);
                if (fi6 == null || index6 == -1) continue;

                DataTable dt = GetDataTableByStruture();
                string Strtime=DateTime.Now.Year.ToString(); 
                double time=double.Parse(Strtime);
               
    
                IFeatureCursor pFeaCursor = fc.Search(null, true);
                IFeature pFea  = pFeaCursor.NextFeature();
                while (pFea != null)
                {
                    string stryear=pFea.get_Value(index2).ToString();
                    if (stryear == "")
                    {
                        stryear = "0";
                    }
                    double year = double.Parse(stryear);
                    string matter = pFea.get_Value(index).ToString();
                    string usestate = pFea.get_Value(index1).ToString();
                    string ownerby = pFea.get_Value(index3).ToString();
                    string proad = pFea.get_Value(index4).ToString();
                    string standard = pFea.get_Value(index5).ToString();
                    string pipelength = pFea.get_Value(index6).ToString();
                    double yeartime = time - year;
                    yearXtime = 20 - yeartime;
                    DataRow dr = dt.NewRow();
                    dr["FeatureID"] = pFea.OID;
                    dr["Matter"] = matter;
                    dr["UseState"] = usestate;
                    dr["StartTime"] = year;
                    dr["UseTime"] = yeartime;
                    dr["OwnerBy"] = ownerby;
                    dr["Proad"] = proad;
                    dr["Standard"] = standard;
                    dr["FeatureClass"] = fc;
                    dr["PipeLength"] = pipelength;
                    if (yearXtime > 0 && yearXtime <=2)
                    {

                        dr["TimeAlert"] = yearXtime;
                        dt.Rows.Add(dr);
                    }
                    else if (yearXtime>2&&yearXtime <= 5)
                    {
                        dr["TimeAlert"] = yearXtime;
                        dt.Rows.Add(dr);
                    }
                    else if(yearXtime<0)                    
                    {
                        dr["TimeAlert"] = Math.Abs(yearXtime);
                        dt.Rows.Add(dr);

                    }
                    pFea = pFeaCursor.NextFeature();
                }
                if (dt.Rows.Count > 0) dict[fc] = dt;
 
            }

            if (dict.Count == 0)
            {

                XtraMessageBox.Show("提示表格数据为空！");
                return;
            }
            WaitForm.Stop();
            FrmPipeAlert dlg = new FrmPipeAlert(dict, m_pMapControl);
            dlg.Text = this.CommandName;
            dlg.Show();
        }
    }
}
