using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using System.Runtime.InteropServices;
using DFDataConfig.Class;
using DFAlgorithm.Network;
using Gvitech.CityMaker.Common;
using DFCommon.Class;
using System.IO;
using System.Drawing;

namespace DF3DPipeCreateTool.Service
{
    public static class FacilityInfoService
    {
        private static Dictionary<string, TopoClass> dictTopo = new Dictionary<string, TopoClass>();
        private static Dictionary<string, FacClassReg> dictReg = new Dictionary<string, FacClassReg>();
        private static Dictionary<string, IFeatureClass> dictFC = new Dictionary<string, IFeatureClass>();

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
        public static HashSet<string> GetValveIdsByFCGuid(string fcGuid)
        {
            if (ValveManager.Instance.Exists(fcGuid)) return ValveManager.Instance.GetValveIds(fcGuid);
            if (DF3DPipeCreateApp.App.PipeLib == null) return null;
            IFeatureClass fc = null;
            IQueryFilter filter = null;
            IFdeCursor cursor = null;
            IRowBuffer buffer = null;
            try
            {
                if (dictFC.ContainsKey(fcGuid) && dictFC[fcGuid] != null) fc = dictFC[fcGuid];
                else
                {
                    IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_GEO_Actuality");
                    if (fds == null) return null;
                    string[] fcNames = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                    if (fcNames == null) return null;
                    foreach (string fcName in fcNames)
                    {
                        IFeatureClass fcTemp = fds.OpenFeatureClass(fcName);
                        if (fcTemp.GuidString == fcGuid)
                        {
                            fc = fcTemp;
                            break;
                        }
                    }
                    if (fc == null) return null;
                    FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("PipeNode");
                    if (fac == null) return null;
                    DFDataConfig.Class.FieldInfo fi = fac.GetFieldInfoBySystemName("Additional");
                    if (fi == null) return null;

                    filter = new QueryFilterClass
                    {
                        SubFields = "oid," + fi.Name,
                        WhereClause = fi.Name + " LIKE '%阀%'"//改
                    };
                    int count = fc.GetCount(filter);
                    if (count == 0) return null;
                    cursor = fc.Search(filter, false);
                    HashSet<string> hsRes = new HashSet<string>();
                    while ((buffer = cursor.NextRow()) != null)
                    {
                        hsRes.Add(fc.GuidString + "_" + buffer.GetValue(0).ToString());
                    }
                    ValveManager.Instance.Add(fcGuid, hsRes);
                    return hsRes;
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
                if (buffer != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(buffer);
                    buffer = null;
                }
            }
        }

        public static List<FacStyleClass> GetFacStyleByFacClassCode(string fcCode)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = DF3DPipeCreateApp.App.TemplateLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacClassCode = '{0}'", fcCode)
                };
                cursor = oc.Search(filter, true);
                List<FacStyleClass> list = new List<FacStyleClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    StyleType type;
                    FacStyleClass fs = null;
                    if (row.FieldIndex("StyleType") >= 0 && Enum.TryParse<StyleType>(row.GetValue(row.FieldIndex("StyleType")).ToString(), out type))
                    {
                        Dictionary<string, string> dictionary = null;
                        if (row.FieldIndex("StyleInfo") >= 0)
                        {
                            object obj = row.GetValue(row.FieldIndex("StyleInfo"));
                            if (obj != null)
                            {
                                IBinaryBuffer buffer2 = row.GetValue(row.FieldIndex("StyleInfo")) as IBinaryBuffer;
                                if (buffer2 != null)
                                {
                                    dictionary = JsonTool.JsonToObject<Dictionary<string, string>>(Encoding.UTF8.GetString(buffer2.AsByteArray()));
                                }
                            }
                        }
                        switch (type)
                        {
                            case StyleType.PipeNodeStyle:
                                fs = new PipeNodeStyleClass(dictionary);
                                break;
                            case StyleType.PipeLineStyle:
                                fs = new PipeLineStyleClass(dictionary);
                                break;
                            case StyleType.PipeBuildStyle:
                                fs = new PipeBuildStyleClass(dictionary);
                                break;
                        }
                    }
                    if (fs == null) continue;
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fs.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) fs.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fs.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fs.Name = obj.ToString();
                    }
                    int index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            fs.Thumbnail = Image.FromStream(stream);
                        }
                    }
                    list.Add(fs);
                }
                return list;
            }
            catch (Exception exception)
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

        public static List<FacClassReg> GetFacClassRegsByFacilityType(string str)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "FacilityType='" + str + "'";

                cursor = oc.Search(filter, false);
                List<FacClassReg> list = new List<FacClassReg>();
                while ((row = cursor.NextRow()) != null)
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
                    list.Add(fc);
                }
                return list;
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
