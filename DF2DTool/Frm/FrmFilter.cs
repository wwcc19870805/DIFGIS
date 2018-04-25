using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using System.Collections;
using ESRI.ArcGIS.esriSystem;

namespace DF2DTool.Frm
{
    public partial class FrmFilter : XtraForm
    {
        private IFeatureLayer featureLayer;
        private DataTable _dt;
        private IField m_FilterFld;
        private string m_strFilter;
        public FrmFilter()
        {
            InitializeComponent();
        }
        public FrmFilter(ILayer layer)
        {
            InitializeComponent();
            featureLayer = layer as FeatureLayer;
        }

        public IField Field
        {
            get { return m_FilterFld; }
        }
        public string Filter
        {
            get { return m_strFilter; }
        }
        private void FrmFilter_Load(object sender, EventArgs e)
        {
            InitControls();
        }
        private void InitControls()
        {
            lbx_Value.Items.Clear();
            //te_Condition.Text = (this.featureLayer as IFeatureLayerDefinition).DefinitionExpression;
            ITableFields tableFields = featureLayer as ITableFields;
            IGeoFeatureLayer geoFL = featureLayer as IGeoFeatureLayer;
            IField field;
            _dt = new DataTable();
            _dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("AliasName"), new DataColumn("Object", typeof(object)) });
            if (geoFL.Renderer is IUniqueValueRenderer)
            {
                field = tableFields.get_Field(tableFields.FindField((geoFL.Renderer as IUniqueValueRenderer).get_Field(0)));
                AddRowToDT(field);
            }
            else if (geoFL.Renderer is ISimpleRenderer)
            {
                for (int i = 0; i < tableFields.FieldCount; i++)
                {
                    if (tableFields.get_FieldInfo(i).Visible)
                    {
                        field = tableFields.get_Field(i);
                        switch (field.Type)
                        {
                            case esriFieldType.esriFieldTypeOID :
                            case esriFieldType.esriFieldTypeSmallInteger:
                            case esriFieldType.esriFieldTypeInteger:
                            case esriFieldType.esriFieldTypeSingle:
                            case esriFieldType.esriFieldTypeDouble:
                            case esriFieldType.esriFieldTypeDate:
                            case esriFieldType.esriFieldTypeString:
                                AddRowToDT(field);
                                break;
                        }
                    }
                }
            }
            this.gridControl1.DataSource = _dt;
        }

        private void AddRowToDT(IField field)
        {
            if (_dt == null) return;
            DataRow dr = _dt.NewRow();
            dr["Name"] = field.Name;
            dr["AliasName"] = field.AliasName;
            dr["Object"] = field;
            _dt.Rows.Add(dr);
        }

        private void btn_Equal_Click(object sender, EventArgs e)
        {
            this.te_Condition.Text += " = ";
        }

        private void btn_Or_Click(object sender, EventArgs e)
        {
            this.te_Condition.Text += " or ";
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (_dt == null) return;
                DataRow row = gridView1.GetDataRow(e.RowHandle);
                ArrayList uniqueValues = GetUniqueValue(featureLayer.FeatureClass as ITable, new string[] { row["Name"].ToString() });
                if (uniqueValues == null) return;
                this.lbx_Value.Items.Clear();

                IField field = row["Object"] as IField;
                if (field.Domain is ICodedValueDomain)
                {
                    for (int i = 0; i < uniqueValues.Count; i++)
                    {
                        if (uniqueValues[i].ToString() != "<Null>")
                        {
                            lbx_Value.Items.Add(GetCodedDescriptionDomainValue(field.Domain as ICodedValueDomain, uniqueValues[i].ToString()));
                        }
                    }
                }
                else
                {
                    if (field.Type == esriFieldType.esriFieldTypeString)
                    {
                        for (int i = 0; i < uniqueValues.Count; i++)
                        {
                            if(uniqueValues[i].ToString() != "<Null>")
                            {
                                lbx_Value.Items.Add("'" + uniqueValues[i].ToString() + "'");
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < uniqueValues.Count; i++)
                        {
                            if (uniqueValues[i].ToString() != "<Null>")
                                lbx_Value.Items.Add(uniqueValues[i].ToString());
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private ArrayList GetUniqueValue(ITable pTable, string[] fieldNames)
        {
            try
            {
                string sep = ", ";
                int[] fieldIndexs = new int[fieldNames.Length];
                ArrayList result = new ArrayList();
                IQueryFilter pFilter = new QueryFilter();
                string strSQL = " distinct ";
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    fieldIndexs[i] = pTable.FindField(fieldNames[i]);
                    strSQL += fieldNames[i] + sep;
                }
                pFilter.SubFields = strSQL.Remove(strSQL.Length - 2, 2);
                ICursor pCur = pTable.Search(pFilter, false);
                IRow pRow;
                while ((pRow = pCur.NextRow()) != null)
                {
                    string val = "";
                    for (int i = 0; i < fieldIndexs.Length; i++)
                    {
                        val += ConvertNull(pRow.get_Value(fieldIndexs[i])) + sep;
                    }
                    val = val.Remove(val.Length - 2, 2);
                    if (!result.Contains(val))
                        result.Add(val);

                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
          

        }

        private string ConvertNull(object obj)
        {
            if (obj == null || obj == System.DBNull.Value) return "";
            else return obj.ToString();
        }

        private string GetCodedDescriptionDomainValue(ICodedValueDomain pDomain, string domainValue)
        {
            string result = "<Null>";
            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                if (pDomain.get_Value(i).ToString() == domainValue)
                {
                    result = pDomain.get_Name(i).ToString();
                    break;
                }
            }
            return result;
        }

        private string GetCodedValueDomainValue(ICodedValueDomain pDomain, string domainDescription)
        {
            string result = "<Null>";
            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                if (pDomain.get_Name(i) == domainDescription)
                {
                    result = pDomain.get_Value(i).ToString();
                    break;
                }
            }
            return result;
        }

        private bool CheckCondition(IFeatureLayer featureLayer, string m_SQL)
        {
            if (featureLayer == null) return false;
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = m_SQL;
            try
            {
                featureLayer.Search(filter, false);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private bool CheckConditionFld(string m_SQL)
        {
            try
            {
                string[] s = null;
                string[] p = null;
                IArray arrFld = new ArrayClass();
                if (m_SQL.IndexOf("or") > -1)
                {
                    m_SQL = m_SQL.Replace("or", "-");
                    s = m_SQL.Split('-');
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i].IndexOf("=") > -1)
                        {
                            p = s[i].Split('=');
                            arrFld.Add(p[0]);
                        }
                    }
                    if (arrFld.Count > 1)
                    {
                        for (int j = 0; j < arrFld.Count; j++)
                        {
                            for (int m = j + 1; m < arrFld.Count; m++)
                            {
                                if (arrFld.get_Element(j).ToString() != arrFld.get_Element(m).ToString())
                                    return false;
                            }
                        }
                    }
                    return true;
                }
                else
                    return true;
            }
            catch 
            {
                return false;
            }
          


        }

        private void lbx_Value_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.lbx_Value.SelectedItem == null) return;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (row == null) return;
                IField field = row["Object"] as IField;
                if (field.Domain is ICodedValueDomain)
                {
                    this.te_Condition.Text += GetCodedValueDomainValue(field.Domain as ICodedValueDomain, this.lbx_Value.SelectedItem.ToString());

                }
                else
                {
                    this.te_Condition.Text += this.lbx_Value.SelectedItem.ToString();
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (row == null) return;
                this.te_Condition.Text += row["Name"].ToString();
                m_FilterFld = row["Object"] as IField;
            }
            catch (System.Exception ex)
            {
            	
            }
        
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!CheckCondition(this.featureLayer, this.te_Condition.Text))
            {
                XtraMessageBox.Show("查询条件错误！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!this.CheckConditionFld(this.te_Condition.Text))
            {
                XtraMessageBox.Show("只能在同一字段中建立组合查询条件！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_strFilter = this.te_Condition.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
