using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;


namespace DF2DData.Class
{
    public class UpOrDown
    {
        public static int GetIndex(IFeatureClass fc,string sysFieldName)
        {
            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc.FeatureClassID.ToString());
            FacilityClass fac = dffc.GetFacilityClass();         
            FieldInfo fi = fac.GetFieldInfoBySystemName(sysFieldName);
            if (fi == null) return -1;
            int index = fc.Fields.FindField(fi.Name);
            return index;
        }
        public static string DecorateWhereClasuse(IFeatureClass fc)
        {
            string fValue = "2";
            int index = GetIndex(fc, "Upordown");
            if (index == -1) return "";
            IDomain pDomain = fc.Fields.get_Field(index).Domain;
            if (pDomain != null && pDomain.Type == esriDomainType.esriDTCodedValue)
            {
                ICodedValueDomain pCD = pDomain as ICodedValueDomain;
                for (int j = 0; j < pCD.CodeCount; j++)
                {
                    if (pCD.get_Name(j) == "地下")
                    {
                        fValue = pCD.get_Value(j).ToString();
                    }
                }
            }
            string whereClause = "UPORDOWN = " + fValue + " And " ;
            return whereClause;
        }

       
    }
}
