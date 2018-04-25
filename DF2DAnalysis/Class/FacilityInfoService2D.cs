using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DFDataConfig.Class;
using DFCommon.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace DF2DAnalysis.Class
{
    public static class FacilityInfoService2D
    {
        private static Dictionary<string, TopoClass2D> dictTopo = new Dictionary<string, TopoClass2D>();
        private static Dictionary<string, FacClassReg2D> dictReg = new Dictionary<string, FacClassReg2D>();

        public static FacClassReg2D GetFacClassRegByFeatureClassID(string fcID)
        {
            if (dictReg.ContainsKey(fcID) && dictReg[fcID] != null) return dictReg[fcID];
            string path = Config.GetConfigValue("2DMdb");
            ICursor cursor = null;
            IRow row = null;           
            try
            {
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IFeatureWorkspace pFw = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                ITable tb = pFw.OpenTable("tbTopoLayers");
                if (tb == null) return null;
                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "OID = 'Topo_" + fcID + "'";
                cursor = tb.Search(filter, false);
                if ((row = cursor.NextRow()) != null)
                {
                    FacClassReg2D fc = new FacClassReg2D();
                    if (row.Fields.FindField("OID") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("OID"));
                        if (obj != null) fc.OID = obj.ToString();
                    }
                    if (row.Fields.FindField("TopoLayerName") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("TopoLayerName"));
                        if (obj != null) fc.TopoLayerName = obj.ToString();
                    }
                    if (row.Fields.FindField("Tolerance") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("Tolerance"));
                        if (obj != null) fc.Tolerance = obj.ToString();
                    }
                    if (row.Fields.FindField("ToleranceZ") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("ToleranceZ"));
                        if (obj != null) fc.ToleranceZ = obj.ToString();
                    }
                    if (row.Fields.FindField("IgnoreZ") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("IgnoreZ"));
                        if (obj != null) fc.IgnoreZ = Convert.ToBoolean(obj);
                    }
                    if (row.Fields.FindField("IgnoreZ") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("IgnoreZ"));
                        if (obj != null) fc.IgnoreZ = Convert.ToBoolean(obj);
                    }
                    if (row.Fields.FindField("TopoTableName") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("TopoTableName"));
                        if (obj != null) fc.TopoTableName = obj.ToString();
                    }
                    if (row.Fields.FindField("Comment") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    dictReg[fcID] = fc;
                    return fc;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }

        public static TopoClass2D GetTopoClassByFeatureClassID(string fcID)
        {
            if (dictTopo.ContainsKey(fcID) && dictTopo[fcID] != null) return dictTopo[fcID];
            string path = Config.GetConfigValue("2DMdbTopo");
            ICursor cursor = null;
            IRow row = null;
            try
            {
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IFeatureWorkspace pFw = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                ITable tb = pFw.OpenTable("tbTopoLayers");
                if (tb == null) return null;
                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "OID = 'Topo_" + fcID + "'";
                cursor = tb.Search(filter, false);
                if ((row = cursor.NextRow()) != null)
                {
                    TopoClass2D tc = new TopoClass2D();
                    if (row.Fields.FindField("OBJECTID") >= 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("OBJECTID"));
                        if (obj != null) tc.Id = Convert.ToInt32(obj);
                    }
                    if (row.Fields.FindField("OID") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("OID"));
                        if (obj != null) tc.ObjectId = obj.ToString();
                    }
                    if (row.Fields.FindField("TopoLayerName") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("TopoLayerName"));
                        if (obj != null) tc.Name = obj.ToString();
                    }
                    if (row.Fields.FindField("Tolerance") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("Tolerance"));
                        if (obj != null)tc.Tolerance = Convert.ToDouble(obj);
                    }
                    if (row.Fields.FindField("ToleranceZ") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("ToleranceZ"));
                        if (obj != null) tc.ToleranceZ = Convert.ToDouble(obj);
                    }
                    if (row.Fields.FindField("IgnoreZ") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("IgnoreZ"));
                        if (obj != null) tc.IgnoreZ = Convert.ToBoolean(Convert.ToInt32(obj));
                    }
                   
                    if (row.Fields.FindField("TopoTableName") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("TopoTableName"));
                        if (obj != null) tc.TopoTable = obj.ToString();
                    }
                    if (row.Fields.FindField("Comment") > 0)
                    {
                        object obj = row.get_Value(row.Fields.FindField("Comment"));
                        if (obj != null) tc.Comment = obj.ToString();
                    }
                    dictTopo[fcID] = tc;
                    return tc;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
    }
}
