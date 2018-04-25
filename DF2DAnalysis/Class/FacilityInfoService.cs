using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using System.Runtime.InteropServices;
using DFDataConfig.Class;

namespace DF3DPipeCreateTool.Service
{
    public static class FacilityInfoService
    {
        private static Dictionary<string, TopoClass> dictTopo = new Dictionary<string, TopoClass>();
        private static Dictionary<string, FacClassReg> dictReg = new Dictionary<string, FacClassReg>();
        
        public static TopoClass GetTopoClassByObjectId(string objectId)
        {
            if (dictTopo.ContainsKey(objectId) && dictTopo[objectId] != null) return dictTopo[objectId];
            if (DF3DPipeCreateApp.App.TemplateLib == null) return null;

            IFdeCursor o = null;
            IRowBuffer row = null;
            IQueryFilter filter = null;

            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.TemplateLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return null;
                filter = new QueryFilterClass
                {
                    WhereClause = string.Format("ObjectId = '{0}'", objectId)
                };
                o = oc.Search(filter, true);
                row = o.NextRow();
                if (row != null)
                {
                    TopoClass tc = new TopoClass();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) tc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) tc.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("TopoLayerName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoLayerName"));
                        if (obj != null) tc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("Tolerance") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Tolerance"));
                        if (obj != null) tc.Tolerance = double.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ToleranceZ") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ToleranceZ"));
                        if (obj != null) tc.ToleranceZ = double.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("IgnoreZ") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("IgnoreZ"));
                        if (obj != null) tc.IgnoreZ = obj.ToString() == "1" ? true : false;
                    }
                    if (row.FieldIndex("TopoTableName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoTableName"));
                        if (obj != null) tc.TopoTable = obj.ToString();
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) tc.Comment = obj.ToString();
                    }
                    dictTopo[tc.ObjectId] = tc;
                    return tc;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (row != null)
                {
                    Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }

        public static TopoClass GetTopoClassByFacClassCode(string facClassCode)
        {
            if(DF3DPipeCreateApp.App.TemplateLib == null) return null;
            IFdeCursor o = null;
            IRowBuffer buffer = null;
            IQueryFilter filter = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.TemplateLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return null;
                filter = new QueryFilterClass
                {
                    WhereClause = string.Format("Code = '{0}'", facClassCode),
                    SubFields = "TopoLayerId"
                };
                o = oc.Search(filter, true);
                buffer = o.NextRow();
                if (buffer != null)
                {
                    if (buffer.IsNull(0))
                    {
                        return null;
                    }
                    return FacilityInfoService.GetTopoClassByObjectId(buffer.GetValue(0).ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (buffer != null)
                {
                    Marshal.ReleaseComObject(buffer);
                    buffer = null;
                }
            }
        }

        public static FacClassReg GetFacClassRegByFeatureClassID(string fcGuid)
        {
            if (dictReg.ContainsKey(fcGuid) && dictReg[fcGuid] != null) return dictReg[fcGuid];
            if (DF3DPipeCreateApp.App.PipeLib == null) return null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "FeatureClassId='" + fcGuid + "'";

                cursor = oc.Search(filter, false);
                if ((row = cursor.NextRow()) != null)
                {
                    FacClassReg fc = new FacClassReg();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("DataSetName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataSetName"));
                        if (obj != null) fc.DataSetName = obj.ToString();
                    }
                    if (row.FieldIndex("FeatureClassId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FeatureClassId"));
                        if (obj != null) fc.FeatureClassId = obj.ToString();
                    }
                    if (row.FieldIndex("FcName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FcName"));
                        if (obj != null) fc.FcName = obj.ToString();
                    }
                    if (row.FieldIndex("DataType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataType"));
                        if (obj != null)
                        {
                            DataLifeCyle ts = 0;
                            if (Enum.TryParse<DataLifeCyle>(obj.ToString(), out ts))
                                fc.DataType = ts;
                            else fc.DataType = 0;
                        }
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    if (row.FieldIndex("TurnerStyle") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TurnerStyle"));
                        if (obj != null)
                        {
                            TurnerStyle ts = 0;
                            if (Enum.TryParse<TurnerStyle>(obj.ToString(), out ts))
                                fc.TurnerStyle = ts;
                            else fc.TurnerStyle = 0;
                        }
                    }
                    if (row.FieldIndex("FacilityType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacilityType"));
                        if (obj != null) fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName(obj.ToString());
                    }
                    if (row.FieldIndex("LocationType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("LocationType"));
                        if (obj != null)
                        {
                            LocationType lt = 0;
                            if (Enum.TryParse<LocationType>(obj.ToString(), out lt))
                                fc.LocationType = lt;
                            else fc.LocationType = 0;
                        }
                    }
                    dictReg[fc.FeatureClassId] = fc;
                    return  fc;
                }
                return null;
            }
            catch (Exception ex)
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
