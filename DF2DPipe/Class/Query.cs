using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using DF2DData.Class;
using ESRI.ArcGIS.Geometry;
using DFDataConfig.Logic;


namespace DF2DPipe.Class
{
    public class Query
    {
        
        public DataTable DoQuery(string prop, string opera, string value, DF2DFeatureClass dffc)
        {
            DataTable dt = new DataTable();
            IQueryFilter queryFilter = new QueryFilterClass();
            List<int> intlist = new List<int>();
            //List<string> pfname = new List<string>();

            IFeatureClass fc = dffc.GetFeatureClass();
            FacilityClass fac = dffc.GetFacilityClass();
            List<FieldInfo> ficol = fac.FieldInfoCollection;
            //queryFilter.SubFields = prop;
            queryFilter.WhereClause = prop + opera + "'" + value +"'";
            IFeatureCursor featureCursor = fc.Search(queryFilter, false);
            IFeature pfeature = featureCursor.NextFeature();
            if (fc == null || pfeature == null) return null;
            
            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pField = fc.Fields.get_Field(i);
                foreach (FieldInfo f in ficol)
                {
                    if (pField.Name == f.Name && f.CanQuery)
                    {
                        DataColumn dc = new DataColumn(pField.AliasName, Type.GetType("System.String"));

                        intlist.Add(i);
                        //pfname.Add(pField.Name);
                        dt.Columns.Add(dc);
                    }
                    
                }
               
                    
                
                
                
                //DataRow dr = dt.NewRow();
                //dr[dc] = pfeature.get_Value(i).ToString();

                //DataRow dr = dt.NewRow();
                //dr[dc1] = pField.AliasName;
                //dr[dc2] = pfeature.get_Value(i);
                //dt.Rows.Add(dr);

            }

            while (pfeature != null)
            {
                if (dt.Columns.Count > 0&&intlist.Count == dt.Columns.Count)
                {
                    DataRow dr = dt.NewRow();
                    string str;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //int index = pfeature.Fields.FindField(pfname[i]);
                        if (pfeature.get_Value(intlist[i]) == null) continue;
                        else
                            str = pfeature.get_Value(intlist[i]).ToString();
                        dr[dt.Columns[i].ColumnName] = str;
                    }
                    dt.Rows.Add(dr);

                }
                
              
               
               pfeature = featureCursor.NextFeature();
            }
            return dt;
        }

        public DataTable DoQuery(string property,  string propName, DF2DFeatureClass dffc)
        {
            DataTable dt = new DataTable();
            IQueryFilter queryFilter = new QueryFilterClass();
            List<int> intlist = new List<int>();
            //List<string> pfname = new List<string>();

            IFeatureClass fc = dffc.GetFeatureClass();
            FacilityClass fac = dffc.GetFacilityClass();
            List<FieldInfo> ficol = fac.FieldInfoCollection;
            //queryFilter.SubFields = prop;
            queryFilter.WhereClause = propName + "=" + "'" + property + "'";
            IFeatureCursor featureCursor = fc.Search(queryFilter, false);
            IFeature pfeature = featureCursor.NextFeature();
            if (fc == null || pfeature == null) return null;

            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pField = fc.Fields.get_Field(i);
                foreach (FieldInfo f in ficol)
                {
                    if (pField.Name == f.Name && f.CanQuery)
                    {
                        DataColumn dc = new DataColumn(pField.AliasName, Type.GetType("System.String"));

                        intlist.Add(i);
                        //pfname.Add(pField.Name);
                        dt.Columns.Add(dc);
                    }

                }





                //DataRow dr = dt.NewRow();
                //dr[dc] = pfeature.get_Value(i).ToString();

                //DataRow dr = dt.NewRow();
                //dr[dc1] = pField.AliasName;
                //dr[dc2] = pfeature.get_Value(i);
                //dt.Rows.Add(dr);

            }

            while (pfeature != null)
            {
                if (dt.Columns.Count > 0 && intlist.Count == dt.Columns.Count)
                {
                    DataRow dr = dt.NewRow();
                    string str;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //int index = pfeature.Fields.FindField(pfname[i]);
                        if (pfeature.get_Value(intlist[i]) == null) continue;
                        else
                            str = pfeature.get_Value(intlist[i]).ToString();
                        dr[dt.Columns[i].ColumnName] = str;
                    }
                    dt.Rows.Add(dr);

                }



                pfeature = featureCursor.NextFeature();
            }
            return dt;
        }

        public DataTable DoQuery(string where, DF2DFeatureClass dffc)
        {
            DataTable dt = new DataTable();
            IQueryFilter queryFilter = new QueryFilterClass();
            List<int> intlist = new List<int>();
            //List<string> pfname = new List<string>();

            IFeatureClass fc = dffc.GetFeatureClass();
            FacilityClass fac = dffc.GetFacilityClass();
            List<FieldInfo> ficol = fac.FieldInfoCollection;
            //queryFilter.SubFields = prop;
            queryFilter.WhereClause = where;
            IFeatureCursor featureCursor = fc.Search(queryFilter, false);
            IFeature pfeature = featureCursor.NextFeature();
            if (fc == null || pfeature == null) return null;

            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pField = fc.Fields.get_Field(i);
                foreach (FieldInfo f in ficol)
                {
                    if (pField.Name == f.Name && f.CanQuery)
                    {
                        DataColumn dc = new DataColumn(pField.AliasName, Type.GetType("System.String"));

                        intlist.Add(i);
                        //pfname.Add(pField.Name);
                        dt.Columns.Add(dc);
                    }

                }





                //DataRow dr = dt.NewRow();
                //dr[dc] = pfeature.get_Value(i).ToString();

                //DataRow dr = dt.NewRow();
                //dr[dc1] = pField.AliasName;
                //dr[dc2] = pfeature.get_Value(i);
                //dt.Rows.Add(dr);

            }

            while (pfeature != null)
            {
                if (dt.Columns.Count > 0 && intlist.Count == dt.Columns.Count)
                {
                    DataRow dr = dt.NewRow();
                    //string str;
                    object str;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //int index = pfeature.Fields.FindField(pfname[i]);
                        if (pfeature.get_Value(intlist[i]) == null) continue;
                        else
                            //str = pfeature.get_Value(intlist[i]).ToString();
                            str = pfeature.get_Value(intlist[i]);
                        dr[dt.Columns[i].ColumnName] = str;
                    }
                    dt.Rows.Add(dr);

                }



                pfeature = featureCursor.NextFeature();
            }
            return dt;
        }

        public DataTable DoQuery(string where, IFeatureClass fc,DF2DFeatureClass dffc)
        {
            DataTable dt = new DataTable();
            IQueryFilter queryFilter = new QueryFilterClass();
            List<int> intlist = new List<int>();
            

            FacilityClass fac = dffc.GetFacilityClass();
            List<FieldInfo> ficol = fac.FieldInfoCollection;
            //queryFilter.SubFields = prop;
            queryFilter.WhereClause = where;
            IFeatureCursor featureCursor = fc.Search(queryFilter, false);
            IFeature pfeature = featureCursor.NextFeature();
            if (fc == null || pfeature == null) return null;

            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pField = fc.Fields.get_Field(i);
                foreach (FieldInfo f in ficol)
                {
                    if (pField.Name == f.Name && f.CanQuery)
                    {
                        DataColumn dc = new DataColumn(pField.AliasName, Type.GetType("System.String"));

                        intlist.Add(i);
                       
                        dt.Columns.Add(dc);
                    }

                }






            }

            while (pfeature != null)
            {
                if (dt.Columns.Count > 0 && intlist.Count == dt.Columns.Count)
                {
                    DataRow dr = dt.NewRow();
                    string str;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        
                        if (pfeature.get_Value(intlist[i]) == null) continue;
                        else
                            str = pfeature.get_Value(intlist[i]).ToString();
                        dr[dt.Columns[i].ColumnName] = str;
                    }
                    dt.Rows.Add(dr);

                }



                pfeature = featureCursor.NextFeature();
            }
            return dt;
        }

        public DataTable DoQuery(DF2DFeatureClass dffc, IGeometry pGeo)
        {
            List<int> intlist = new List<int>();
            DataTable dt = new DataTable();
            IFeatureClass fc = dffc.GetFeatureClass();
            FacilityClass fac = dffc.GetFacilityClass();
            List<FieldInfo> ficol = fac.FieldInfoCollection;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pGeo;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = fc.Search(pSpatialFilter, false);
            IFeature pfeature = featureCursor.NextFeature();
            if (fc == null || pfeature == null) return null;

            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pField = fc.Fields.get_Field(i);
                foreach (FieldInfo f in ficol)
                {
                    if (pField.Name == f.Name && f.CanQuery)
                    {
                        DataColumn dc = new DataColumn(pField.AliasName, Type.GetType("System.String"));

                        intlist.Add(i);
                        //pfname.Add(pField.Name);
                        dt.Columns.Add(dc);
                    }

                }
            }
            while (pfeature != null)
            {
                if (dt.Columns.Count > 0 && intlist.Count == dt.Columns.Count)
                {
                    DataRow dr = dt.NewRow();
                    string str;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //int index = pfeature.Fields.FindField(pfname[i]);
                        if (pfeature.get_Value(intlist[i]) == null) continue;
                        else
                            str = pfeature.get_Value(intlist[i]).ToString();
                        dr[dt.Columns[i].ColumnName] = str;
                    }
                    dt.Rows.Add(dr);

                }



                pfeature = featureCursor.NextFeature();
            }
            return dt;
        }

        public DataTable DoQueryByMajorClass(string whereclause,IFeatureClass fc,FacilityClass facc)
        {
            List<FieldInfo> ficol = facc.FieldInfoCollection;
            List<int> intlist = new List<int>();
            DataTable dt = new DataTable();
            IQueryFilter pQueryFilter = new QueryFilterClass();

            pQueryFilter.WhereClause = whereclause;
            if (fc == null) return null;
            IFeatureCursor featureCursor = fc.Search(pQueryFilter, false);

            IFeature pfeature = featureCursor.NextFeature();
            if (pfeature == null) return null;

            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pField = fc.Fields.get_Field(i);
                foreach (FieldInfo f in ficol)
                {
                    if (pField.Name == f.Name && f.CanQuery)
                    {
                        DataColumn dc = new DataColumn(pField.AliasName, Type.GetType("System.String"));

                        intlist.Add(i);
                        //pfname.Add(pField.Name);
                        dt.Columns.Add(dc);
                    }
                }

            }

            while (pfeature != null)
            {
                if (dt.Columns.Count > 0 && intlist.Count == dt.Columns.Count)
                {
                    DataRow dr = dt.NewRow();
                    string str;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //int index = pfeature.Fields.FindField(pfname[i]);
                        if (pfeature.get_Value(intlist[i]) == null) continue;
                        else
                            str = pfeature.get_Value(intlist[i]).ToString();
                        dr[dt.Columns[i].ColumnName] = str;
                    }
                    dt.Rows.Add(dr);

                }

                pfeature = featureCursor.NextFeature();
            }
            return dt;
        }
      
    }
}
