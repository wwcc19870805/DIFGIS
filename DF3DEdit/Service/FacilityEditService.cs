using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Common;
using DF3DPipeCreateTool.Class;
using DFCommon.Class;

namespace DF3DEdit.Service
{
    public class FacilityEditService
    {
        private static FacilityEditService _FacilityEditService;
        public static FacilityEditService Instance()
        {
            if (_FacilityEditService == null)
            {
                _FacilityEditService = new FacilityEditService();
            }
            return _FacilityEditService;
        }

        public List<FacStyleClass> GetFacStyleByFacClassCode(string fcCode)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                if (DF3DPipeCreateApp.App.TemplateLib != null)
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
                else
                    return null;
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
    }
}
