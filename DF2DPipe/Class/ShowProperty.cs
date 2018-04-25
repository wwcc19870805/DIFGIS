using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Class;

namespace DF2DPipe.Class
{
    public class ShowProperty
    {
        private string _sysFieldName;
        private string _disName;
        private DataTable _dt;
        private Dictionary<string, List<IFeature>> _dict;
        public ShowProperty(string sysFieldName,string disName,Dictionary<string,List<IFeature>> dict)
        {
            this._sysFieldName = sysFieldName;
            this._disName = disName;
            this._dict = dict;
        }
        public  DataTable GetDataTableByDistrictName()
        {
            try
            {
                FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName(_sysFieldName);
                if (fac == null) return null;
                List<FieldInfo> listFI = fac.FieldInfoCollection;
                _dt = new DataTable();
                foreach (FieldInfo fi in listFI)
                {
                    if (fi.CanStats)
                    {
                        DataColumn dc = new DataColumn(fi.Alias);
                        if (ColumnExist(_dt, fi.Alias)) continue;
                        _dt.Columns.Add(dc);
                    }
                }
                if (_dict != null && _dict.ContainsKey(_disName))
                {
                    List<IFeature> listF = _dict[_disName];
                    foreach (IFeature fea in listF)
                    {
                        DataRow dr = _dt.NewRow();
                        foreach (FieldInfo fi in listFI)
                        {
                            if (fi.CanStats)
                            {
                                int index = fea.Fields.FindField(fi.Name);
                                if (index == -1) continue;
                                IDomain pDomain = fea.Fields.get_Field(index).Domain;
                                if (pDomain != null && pDomain.Type == esriDomainType.esriDTCodedValue)
                                {
                                    ICodedValueDomain pCD = pDomain as ICodedValueDomain;
                                    for (int i = 0; i < pCD.CodeCount; i++ )
                                    {
                                        if (object.Equals(pCD.get_Value(i), fea.get_Value(index)))
                                        {
                                            dr[fi.Alias] = pCD.get_Name(i);
                                        }
                                    }
                                }
                                else if (fi.DataType == "Decimal")
                                    dr[fi.Alias] = Math.Round(Convert.ToDouble(fea.get_Value(index)), 2).ToString();
                                else
                                    dr[fi.Alias] = fea.get_Value(index).ToString();
                                
                            }

                        }
                        _dt.Rows.Add(dr);
                    }
                }
                return _dt;
            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
        private bool ColumnExist(DataTable dt,string columnName)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == columnName)
                    return true;
            }
            return false;
        }
  
    }
}
