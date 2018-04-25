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
using System.Data.OleDb;
using DFWinForms.Class;
using System.Windows.Forms;
using DF2DDataCheck.Frm;
using DF2DData.Class;
using DFDataConfig.Class;
using System.Collections;
using DevExpress.XtraEditors;

namespace DF2DDataCheck.Command
{
    class CmdNormativeCheck : AbstractMap2DCommand
    {
        IMapControl2 m_pMapControl;
        IMap2DView mapView;
        DF2DApplication app;
        private DataTable m_dtDic;
        private ArrayList row= new ArrayList();
        Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
        private string[] PntField = { "MATTER", "CHARACTER_", "FUSU", "UseState", "SOURCE" };  //点表中有数据字典定义的字段
        private string[] ArcField = { "MATTER", "COVERSTSLE", "UseState", "PRESSURE", "BranchPipeMaterial", "SOURCE" };  //线表中有数据字典定义的字段

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

            //将数据字典读入内存
            string strMdbPath = System.Windows.Forms.Application.StartupPath + @"\..\Support\管线数据字典.mdb";
            string strTableName = "_CONFIGDICTIONARY";
            bool bSuccess = false;
            m_dtDic = GetDataDictionary(strTableName, strMdbPath, ref bSuccess);

            //点表数据检查
            DataPntCheck();

            //线表数据检查
            DataArcCheck();

            WaitForm.Stop();
            FormCheckResult dlg = new FormCheckResult(dict, m_pMapControl);
            dlg.Text = this.CommandName;
            dlg.Show();
           
        }

            
       
        #region 将数据字典读入内存
        /// <summary>
        /// 将数据字典读入内存
        /// </summary>
        /// <param name="strTable">数据表名</param>
        /// <param name="strMdbPath">数据库路径</param>
        /// <param name="success">读取是否成功</param>
        /// <returns></returns>
        private DataTable GetDataDictionary(string strTable, string strMdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;
                string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strMdbPath + ";";
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                odcConnection.Open();

                OleDbCommand odCommand = odcConnection.CreateCommand();
                odCommand.CommandText = "select * from " + strTable;

                OleDbDataReader odrReader = odCommand.ExecuteReader();
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                    if (odrReader.GetName(i) == "管线大类")
                    {
                        dt.PrimaryKey = new DataColumn[] { dc };
                    }
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int j = 0; j < size; j++)
                    {
                        dr[odrReader.GetName(j)] = odrReader[odrReader.GetName(j)].ToString();
                    }
                    dt.Rows.Add(dr);
                }
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch (Exception ex)
            {
                success = false;
                return dt;
            }

        }
        #endregion

        #region 根据管线类别和字段获取对应的数据字典值
        /// <summary>
        /// 根据管线类别和字段获取对应的数据字典值
        /// </summary>
        /// <param name="strClass">管线大类代码</param>
        /// <param name="indexFld">字段</param>
        /// <returns></returns>
        private string GetValueFromDictionary(string strClass, string strField)
        {
            string strVal;
            strVal = m_dtDic.Rows.Find(new object[] { strClass })[strField].ToString();
            return strVal;
        }
        #endregion

        private void DataPntCheck()
        {


            //管线点表数据规范性检查

            int i, j, k;
            string strDicValue;
            IFeatureClass pFeaClass;
            IFeatureCursor pFeaCursor;
            IQueryFilter pFilter = new QueryFilterClass();
            DataTable dt = GetDataTableByStruture();

            IFeature pFea;
            //Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
            List<DF2DFeatureClass> list = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeNode");
            if (list == null) return;
            WaitForm.Start("开始数据规范性检查..", "请稍后");
            foreach (DF2DFeatureClass dfcc in list)
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;
                string strPipeClass;
                pFeaClass = dfcc.GetFeatureClass();
                if (pFeaClass == null) continue;
                WaitForm.SetCaption("正在检查图层：" + " " + pFeaClass.AliasName);
                FacilityClass fac = dfcc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("floorheight");
                if (fi == null) continue;
                DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("welldepth");
                if (fi1 == null) continue;
                DFDataConfig.Class.FieldInfo fi2 = fac.GetFieldInfoBySystemName("fuse");
                if (fi2 == null) continue;

                if (pFeaClass.AliasName.Contains("燃气"))
                {
                    strPipeClass = "BP";
                }
                else if (pFeaClass.AliasName.Contains("下水"))
                {
                    strPipeClass = "DP";
                }
                else if (pFeaClass.AliasName.Contains("电力"))
                {
                    strPipeClass = "EP";
                }
                else if (pFeaClass.AliasName.Contains("给水"))
                {
                    strPipeClass = "FP";
                }
                else if (pFeaClass.AliasName.Contains("热力"))
                {
                    strPipeClass = "HP";
                }
                else if (pFeaClass.AliasName.Contains("通信"))
                {
                    strPipeClass = "TP";
                }
                else if (pFeaClass.AliasName.Contains("工业"))
                {
                    strPipeClass = "IOP";
                }
                else
                {
                    strPipeClass = "";
                }
                // 数据字典字段值检查
                //遍历字段
                for (j = 0; j < PntField.Length; j++)
                {
                    if (pFeaClass.FindField(PntField[j]) == -1)
                    {
                        continue;
                    }
                    //获取数据字典中的对应值
                    strDicValue = GetValueFromDictionary(strPipeClass, PntField[j]);

                    if (strDicValue != "空")
                    {
                        if (strDicValue.Contains(","))
                        {
                            string strWhere = "(";
                            string[] s = strDicValue.Split(new char[] { ',' });
                            for (k = 0; k < s.Length; k++)
                            {
                                strWhere += "'" + s[k] + "',";
                            }
                            strWhere.Remove(strWhere.LastIndexOf(","));
                            strWhere += ")";
                            pFilter.WhereClause = PntField[j] + " not in" + strWhere;
                        }
                        else
                        {
                            pFilter.WhereClause = PntField[j] + "<>'" + strDicValue + "'";
                        }


                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        pFea = null;

                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                            b1 = true;
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = pFeaClass.AliasName;
                            dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                            dr["FeatureClass"] = pFeaClass;
                            dr["ErrorType"] = "【" + PntField[j] + "】字段值不属于数据字典";
                            dt.Rows.Add(dr);
                            Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);

                        }
                        
                    }

                    if (dt.Rows.Count > 0) dict[pFeaClass] = dt;
                }
                        if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        pFilter.WhereClause = fi.Name + "=-999";
                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        pFea = pFeaCursor.NextFeature();
                        while (pFea != null)
                        {
                            b2 = true;
                           DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = pFeaClass.AliasName;
                            dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                            dr["FeatureClass"] = pFeaClass;
                            dr["ErrorType"] = "【地面高程】字段值不能为-999";
                            dt.Rows.Add(dr);
                            pFea = pFeaCursor.NextFeature();
                        }
                        
                        if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        if (strPipeClass == "BP")
                        {
                            pFilter.WhereClause = fi1.Name + "< 0 or " + fi1.Name + "> 3";
                        }
                        else if (strPipeClass == "IOP")
                        {
                            pFilter.WhereClause = fi1.Name + "< 0 or " + fi1.Name + "> 5";
                        }
                        else
                        {
                            pFilter.WhereClause = fi1.Name + "< 0 or " + fi1.Name + "> 10";
                        }
                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                            b3 = true;
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = pFeaClass.AliasName;
                            dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                            dr["FeatureClass"] = pFeaClass;
                            dr["ErrorType"] = "【井底深】字段值不在规范值范围内";
                            dt.Rows.Add(dr);
                        }
                       
                        if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        pFilter.WhereClause = fi2.Name + " in ('阀门井','水表井','消防井','水源井','净水池','检修井','清水池','雨水检修井','污水检修井','跌水井','雨水篦',"
                                                                + "'测试井','人孔','手孔','排气井','凝水井','通风井') and len(" + fi1.Name + ")=0";

                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                            b4 = true;
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = pFeaClass.AliasName;
                            dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                            dr["FeatureClass"] = pFeaClass;
                            dr["ErrorType"] = "【井底深】字段值不应为空";
                            dt.Rows.Add(dr);
                        }
                        
                        dt = ReturnMergeData(dt);
                    if (dt.Rows.Count > 0) dict[pFeaClass] = dt;
            }
        }

        
        private void DataArcCheck()
        {
        
        //管线线表数据规范性检查
            int i, j, k;   
            string strDicValue;
            IFeatureClass pFeaClass;
            IFeatureCursor pFeaCursor;
            IQueryFilter pFilter = new QueryFilterClass();
            DataTable dt = GetDataTableByStruture();
            //Dictionary<IFeatureClass, DataTable> dict = new Dictionary<IFeatureClass, DataTable>();
            IFeature pFea;

           
            List<DF2DFeatureClass> List = Dictionary2DTable.Instance.GetFeatureClassByFacilityClassName("PipeLine");
            if (List == null) return;
            WaitForm.Start("开始检查..", "请稍后");
            foreach (DF2DFeatureClass dfcc in List)
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;
                string strPipeClass;
                pFeaClass = dfcc.GetFeatureClass();
                if (pFeaClass == null) continue;
                WaitForm.SetCaption("正在检查："+pFeaClass.AliasName);
                FacilityClass fac = dfcc.GetFacilityClass();
                if (fac == null) continue;
                List<DFDataConfig.Class.FieldInfo> listField = fac.FieldInfoCollection;
                DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("startdepthgc");
                if (fi == null) continue;
                DFDataConfig.Class.FieldInfo fi1 = fac.GetFieldInfoBySystemName("enddepthgc");
                if (fi1 == null) continue;
                DFDataConfig.Class.FieldInfo fi2 = fac.GetFieldInfoBySystemName("startdepth");
                if (fi2 == null) continue;
                DFDataConfig.Class.FieldInfo fi3 = fac.GetFieldInfoBySystemName("enddepth");
                if (fi3 == null) continue;
                DFDataConfig.Class.FieldInfo fi4 = fac.GetFieldInfoBySystemName("coverstsle");
                if (fi4 == null) continue;
                DFDataConfig.Class.FieldInfo fi5 = fac.GetFieldInfoBySystemName("standard");
                if (fi5 == null) continue;
                if (pFeaClass.AliasName.Contains("燃气"))
                {
                    strPipeClass = "BP";
                }
                else if (pFeaClass.AliasName.Contains("下水"))
                {
                    strPipeClass = "DP";
                }
                else if (pFeaClass.AliasName.Contains("电力"))
                {
                    strPipeClass = "EP";
                }
                else if (pFeaClass.AliasName.Contains("给水"))
                {
                    strPipeClass = "FP";
                }
                else if (pFeaClass.AliasName.Contains("热力"))
                {
                    strPipeClass = "HP";
                }
                else if (pFeaClass.AliasName.Contains("通信"))
                {
                    strPipeClass = "TP";
                }
                else if (pFeaClass.AliasName.Contains("工业"))
                {
                    strPipeClass = "IOP";
                }
                else
                {
                    strPipeClass = "";
                }
                //数据字典字段值检查
                //遍历字段
                for (j = 0; j < ArcField.Length; j++)
                {
                    if (pFeaClass.FindField(ArcField[j]) == -1)
                    {
                        continue;
                    }
                    //获取数据字典中的对应值
                    strDicValue = GetValueFromDictionary(strPipeClass, ArcField[j]);

                    if (strDicValue != "空")
                    {
                        if (strDicValue.Contains(","))
                        {
                            string strWhere = "(";
                            string[] s = strDicValue.Split(new char[] { ',' });
                            for (k = 0; k < s.Length; k++)
                            {
                                strWhere += "'" + s[k] + "',";
                            }
                            strWhere.Remove(strWhere.LastIndexOf(","));
                            strWhere += ")";
                            pFilter.WhereClause = ArcField[j] + " not in" + strWhere;
                        }
                        else
                        {
                            pFilter.WhereClause = ArcField[j] + "<>'" + strDicValue + "'";
                        }

                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                            
                            DataRow dr = dt.NewRow();
                            dr["ErrorFeatureID"] = pFea.OID;
                            dr["FeatureofClass"] = pFeaClass.AliasName;
                            dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                            dr["FeatureClass"] = pFeaClass;
                            dr["ErrorType"] = "【" + ArcField[j] + "】字段值不属于数据字典";
                            dt.Rows.Add(dr);
                            Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                    }
                    if (dt.Rows.Count > 0) dict[pFeaClass] = dt;
                }
                       if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        pFilter.WhereClause = fi.Name + "= -999 or " + fi1.Name+ "= -999";
                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                                   
                                  DataRow dr = dt.NewRow();
                                  dr["ErrorFeatureID"] = pFea.OID;
                                  dr["FeatureofClass"] = pFeaClass.AliasName;
                                  dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                                  dr["ErrorType"] ="【管线高程】字段值不能为-999";
                                  dr["FeatureClass"] = pFeaClass;
                                  dt.Rows.Add(dr);
                                  Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                        if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        //管线埋深（只针对管线，管点不需要检查）
                        if (strPipeClass == "BP")
                        {
                            pFilter.WhereClause = "(" + fi2.Name + "< 0 or " + fi2.Name + "> 3) or (" + fi3.Name + "< 0 or " + fi3.Name + "> 3)";
                            pFeaCursor = pFeaClass.Search(pFilter, true);
                            while ((pFea = pFeaCursor.NextFeature()) != null)
                            {
                               
                                  DataRow dr = dt.NewRow();
                                  dr["ErrorFeatureID"] = pFea.OID;
                                  dr["FeatureofClass"] = pFeaClass.AliasName;
                                  dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                                  dr["FeatureClass"] = pFeaClass;
                                  dr["ErrorType"] ="【管线埋深】字段值不在规范值范围内";
                                  dt.Rows.Add(dr);
                                  Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                            if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        }
                        else
                        {
                            //'直埋', '管埋', '管块'
                            pFilter.WhereClause = fi4.Name + " in ('直埋','管埋','管块') and (" + fi2.Name + "< 0 or " + fi2.Name + "> 10) or (" + fi3.Name + "< 0 or " + fi3.Name + "> 10)";
                            pFeaCursor = pFeaClass.Search(pFilter, true);
                            while ((pFea = pFeaCursor.NextFeature()) != null)
                            {
                               
                                  DataRow dr = dt.NewRow();
                                  dr["ErrorFeatureID"] = pFea.OID;
                                  dr["FeatureofClass"] = pFeaClass.AliasName;
                                  dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                                  dr["FeatureClass"] = pFeaClass;
                                  dr["ErrorType"] ="【管线埋深】字段值不在规范值范围内";
                                  dt.Rows.Add(dr);
                                  Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                            if (dt.Rows.Count > 0) dict[pFeaClass] = dt;
                        }
                        //'架空'

                        pFilter.WhereClause = fi4.Name + "='架空' and (" + fi2.Name + ">0 or " + fi3.Name + ">0)";
                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                                   
                                  DataRow dr = dt.NewRow();
                                  dr["ErrorFeatureID"] = pFea.OID;
                                  dr["FeatureofClass"] = pFeaClass.AliasName;
                                  dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                                  dr["FeatureClass"] = pFeaClass;
                                  dr["ErrorType"] ="【管线埋深】字段值不在规范值范围内";
                                  dt.Rows.Add(dr);
                                  Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                        if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        pFilter.WhereClause = fi4.Name + "='管沟'";
                        pFeaCursor = pFeaClass.Search(pFilter, true);
                        while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                            string s = pFea.get_Value(pFeaClass.FindField(fi5.Name)).ToString();
                            if (s.Contains("*"))
                            {
                                if (s.Contains("*"))
                                {
                                    double d = Convert.ToDouble(s.Substring(s.IndexOf("*") + 1));
                                    double d1 = Convert.ToDouble(pFea.get_Value(pFeaClass.FindField(fi2.Name)).ToString());
                                    double d2 = Convert.ToDouble(pFea.get_Value(pFeaClass.FindField(fi3.Name)).ToString());
                                    if (d1 < d || d2 < d)
                                    {
                                        
                                      DataRow dr = dt.NewRow();
                                      dr["ErrorFeatureID"] = pFea.OID;
                                      dr["FeatureofClass"] = pFeaClass.AliasName;
                                      dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                                      dr["FeatureClass"] = pFeaClass;
                                      dr["ErrorType"] ="【管线埋深】字段值不在规范值范围内";
                                      dt.Rows.Add(dr);
                                      Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                                    }
                                }
                            }
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                         if (dt.Rows.Count > 0) dict[pFeaClass] = dt;

                        //管径
                        if (strPipeClass == "BP" || strPipeClass == "FP")
                        {
                            pFilter.WhereClause = "ISNUMERIC(" + fi5.Name + ")=1";
                        }
                        else
                        {
                            //直埋、管埋
                            pFilter.WhereClause = fi4.Name + " in ('直埋','管埋') and " + "ISNUMERIC(" + fi5.Name + ")=1";
                            pFeaCursor = pFeaClass.Search(pFilter, true);
                            while ((pFea = pFeaCursor.NextFeature()) != null)
                            {
                               
                                DataRow dr = dt.NewRow();
                               dr["ErrorFeatureID"] = pFea.OID;
                               dr["FeatureofClass"] = pFeaClass.AliasName;
                               dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                               dr["FeatureClass"] = pFeaClass;
                               dr["ErrorType"] ="【管线规格】字段值不在规范值范围内";
                                dt.Rows.Add(dr);
                                Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                            }

                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);                            
                             if (dt.Rows.Count > 0) dict[pFeaClass] = dt;
                        }

                        pFilter.WhereClause = fi4.Name + " in ('管块','管沟') and " + fi5.Name + " like '%*%'";
                        pFeaCursor = pFeaClass.Search(pFilter, true);
                       while ((pFea = pFeaCursor.NextFeature()) != null)
                        {
                           
                            DataRow dr = dt.NewRow();
                        dr["ErrorFeatureID"] = pFea.OID;
                        dr["FeatureofClass"] = pFeaClass.AliasName;
                        dr["FeatureofLayer"] = (pFeaClass as IDataset).Name;
                        dr["FeatureClass"] = pFeaClass;
                        dr["ErrorType"] ="【管线规格】字段值不在规范值范围内";
                         dt.Rows.Add(dr);
                         Console.WriteLine(pFea.OID + " " + pFeaClass.AliasName);
                        }
                       System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCursor);
                       
                         dt =ReturnMergeData(dt);
                if (dt.Rows.Count > 0) dict[pFeaClass] = dt;
            }

            if (dict.Count == 0)
            {

                XtraMessageBox.Show("提示表格数据为空！");
                
            }
            
 }

        //合并重复行
        public DataTable ReturnMergeData(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                //合并  
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    if (ht.ContainsKey(dataTable.Rows[i]["ErrorFeatureID"]))
                    {
                        //获取行索引  
                        int index = (int)ht[dataTable.Rows[i]["ErrorFeatureID"]];
                        if (!dataTable.Rows[i]["ErrorFeatureID"].ToString().Contains(dataTable.Rows[index]["ErrorFeatureID"].ToString()))
                        {
                            dataTable.Rows[index]["ErrorType"] = dataTable.Rows[index]["ErrorType"] + "," + dataTable.Rows[i]["ErrorType"];  
                        }
                        //删除重复行  
                        dataTable.Rows.RemoveAt(i);  
                        //调整索引减1  
                        i--;  
                    }  
                    else  
                    {  
                        //保存名称以及行索引  
                        ht.Add(dataTable.Rows[i]["ErrorFeatureID"], i);  
                    }                  
                 }
             }
            return dataTable;  
          }


    }
}