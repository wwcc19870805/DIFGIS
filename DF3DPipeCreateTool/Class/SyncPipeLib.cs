using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DevExpress.XtraEditors;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.Class
{
    public class SyncPipeLib
    {
        private static SyncPipeLib instance = null;
        private static readonly object syncRoot = new object();
        private IDataSource _dsTemplate;
        private IDataSource _dsPipe;
        private SyncPipeLib()
        {
            this._dsTemplate = DF3DPipeCreateApp.App.TemplateLib;
            this._dsPipe = DF3DPipeCreateApp.App.PipeLib;
        }

        public static SyncPipeLib Instance
        {
            get
            {
                if (SyncPipeLib.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (SyncPipeLib.instance == null)
                        {
                            SyncPipeLib.instance = new SyncPipeLib();
                        }
                    }
                }
                return SyncPipeLib.instance;
            }
        }

        private List<TopoClass> GetAllTopoClasses()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                cursor = oc.Search(filter, true);
                List<TopoClass> list = new List<TopoClass>();
                while ((row = cursor.NextRow()) != null)
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
                    list.Add(tc);
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
        private IFieldInfoCollection GetTopoInfoFields(out string[] arrDBIndex)
        {
            IFieldInfoCollection infos = new FieldInfoCollectionClass();
            IFieldInfo newVal = null;
            arrDBIndex = null;
            newVal = new FieldInfoClass
            {
                Name = "GroupId",
                Alias = "逻辑组ID",
                FieldType = gviFieldType.gviFieldString,
                Length = 80
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "A_FacClass",
                Alias = "边所在设施类",
                FieldType = gviFieldType.gviFieldString,
                Length = 80
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Edge",
                Alias = "边对应的要素ID",
                FieldType = gviFieldType.gviFieldInt32
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "P_FacClass",
                Alias = "前点所在设施类",
                FieldType = gviFieldType.gviFieldString,
                Length = 80
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "PNode",
                Alias = "前点要素ID",
                FieldType = gviFieldType.gviFieldInt32
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass                
            {
                Name = "PDis",
                Alias = "前点距离",
                FieldType = gviFieldType.gviFieldDouble
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "E_FacClass",
                Alias = "后点所在设施类",
                FieldType = gviFieldType.gviFieldString,
                Length = 80
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "ENode",
                Alias = "后点要素ID",
                FieldType = gviFieldType.gviFieldInt32
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass                  
            {
                Name = "EDis",
                Alias = "后点距离",
                FieldType = gviFieldType.gviFieldDouble
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "ResistanceA",
                Alias = "正向权值",
                FieldType = gviFieldType.gviFieldInt16
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "ResistanceB",
                Alias = "反向权值",
                FieldType = gviFieldType.gviFieldInt16
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Length",
                Alias = "弧长",
                FieldType = gviFieldType.gviFieldDouble
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Topo_Error",
                Alias = "拓扑错误",
                FieldType = gviFieldType.gviFieldString,
                Length = 100
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "LaseUpdate",
                Alias = "最后更新时间",
                FieldType = gviFieldType.gviFieldDate
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Geometry",
                Alias = "空间列",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            IGeometryDef def = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnType.gviGeometryColumnPolyline,
                HasZ = true
            };
            newVal.GeometryDef = def;
            infos.Add(newVal);
            arrDBIndex = new string[] { "GroupId", "A_FacClass", "Edge", "P_FacClass", "PNode", "E_FacClass", "ENode" };
            return infos;
        }

        private void SyncTopoInfo()
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return;

                List<TopoClass> allTC = GetAllTopoClasses();
                if (allTC == null || allTC.Count == 0) return;

                #region 检查删除管线库中的拓扑信息表，有就不删，没有就删除
                WaitForm.SetCaption("检查管线库中的拓扑信息表...");
                string[] namesByType = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if (namesByType == null)return;
                foreach (string str in namesByType)
                {
                    if (str.ToUpper().IndexOf("TOPO_OC_") == 0)
                    {
                        bool flag = true;
                        if ((allTC == null) || (allTC.Count == 0))
                        {
                            flag = true;
                        }
                        else
                        {
                            foreach (TopoClass tc in allTC)
                            {
                                if (tc.TopoTable == str)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (flag)
                        {
                            fds.DeleteByName(str);
                        }
                    }
                }
                #endregion

                #region 创建拓扑信息表
                WaitForm.SetCaption("创建管线库中的拓扑信息表...");
                foreach (TopoClass tc in allTC)
                {
                    string topoTableName = tc.TopoTable;
                    if (Array.IndexOf<string>(namesByType, topoTableName) == -1)
                    {
                        string[] arrDBIndex = null;
                        IFeatureClass fc = fds.CreateFeatureClass(topoTableName, GetTopoInfoFields(out arrDBIndex));
                        if (fc != null) fc.AliasName = tc.Name + "拓扑信息表";
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("同步管线库中的拓扑信息表失败！", "提示");
            }
        }

        private List<FacClass> GetAllFacClasses()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacilityType <> '{0}'", "UnKnown"),
                    PostfixClause = "order by OrderBy asc"
                };
                cursor = oc.Search(filter, true);
                List<FacClass> list = new List<FacClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    FacClass fc = new FacClass();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Code") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Code"));
                        if (obj != null) fc.Code = obj.ToString();
                    }
                    if (row.FieldIndex("PCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("PCode"));
                        if (obj != null) fc.PCode = obj.ToString();
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    if (row.FieldIndex("FacilityType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacilityType"));
                        if (obj != null)
                        {
                            fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName(obj.ToString());
                        }
                    }
                    if (row.FieldIndex("LocationType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("LocationType"));
                        if (obj != null)
                        {
                            fc.LocationType = (LocationType)Enum.Parse(typeof(LocationType), obj.ToString());
                        }
                    }
                    if (row.FieldIndex("TurnerStyle") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TurnerStyle"));
                        if (obj != null)
                        {
                            fc.TurnerStyle = (TurnerStyle)Enum.Parse(typeof(TurnerStyle), obj.ToString());
                        }
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("TopoLayerId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoLayerId"));
                        if (obj != null) fc.TopoLayerId = obj.ToString();
                    }
                    list.Add(fc);
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
        private List<FacClassReg> GetAllFacClassRegs()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "1=1";

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
        private bool DeleteFacClassReg(FacClassReg reg)
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("FacClassCode = '{0}'", reg.FacClassCode);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool DeleteFeatureClass(string dataSetName ,string fcName)
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset(dataSetName);
                if (fds == null) return false;
                return fds.DeleteByName(fcName);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private IFeatureClass OpenFeatureClass(string dataSetName, string fcName)
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset(dataSetName);
                if (fds == null) return null;
                return fds.OpenFeatureClass(fcName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private bool UpdateFacClassReg(FacClassReg reg)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("FacClassCode = '{0}'", reg.FacClassCode),
                    SubFields = "oid,LocationType,TurnerStyle,FacilityType,Comment"
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(1, reg.LocationType.ToString());
                    row.SetValue(2, reg.TurnerStyle.ToString());
                    row.SetValue(3, reg.FacilityType.Name);
                    row.SetValue(4, reg.Comment);
                    cursor.UpdateRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
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
        private List<CMFieldConfig> GetFieldsConfig(string code)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FieldConfig");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacClassCode = '{0}'", code),
                    PostfixClause = "order by OrderBy asc"
                };
                cursor = oc.Search(filter, true);
                List<CMFieldConfig> list = new List<CMFieldConfig>();
                while ((row = cursor.NextRow()) != null)
                {
                    CMFieldConfig fc = new CMFieldConfig();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("Alias") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Alias"));
                        if (obj != null) fc.Alias = obj.ToString();
                    }
                    if (row.FieldIndex("FieldType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FieldType"));
                        if (obj != null)
                        {
                            gviFieldType ts = 0;
                            if (Enum.TryParse<gviFieldType>(obj.ToString(), out ts))
                                fc.FieldType = ts;
                            else fc.FieldType = gviFieldType.gviFieldUnknown;
                        }
                    }
                    if (row.FieldIndex("Length") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Length"));
                        if (obj != null) fc.Length = Convert.ToInt32(obj.ToString());
                    }
                    if (row.FieldIndex("Nullable") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Nullable"));
                        if (obj != null) fc.Nullable = obj.ToString() == "0" ? false : true;
                    }
                    list.Add(fc);
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
        private IFieldInfoCollection CreateFieldInfoCollection(List<CMFieldConfig> list,string facType)
        {
            if (list == null) return null;
            IFieldInfoCollection infos = new FieldInfoCollection();
            
            IFieldInfo newVal = null;
            foreach (CMFieldConfig cmfc in list)
            {
                newVal = new FieldInfoClass();
                newVal.Name = cmfc.Name;
                newVal.Alias = cmfc.Alias;
                newVal.FieldType = cmfc.FieldType;
                newVal.Nullable = cmfc.Nullable;
                if (newVal.FieldType == gviFieldType.gviFieldString)
                {
                    newVal.Length = cmfc.Length;
                }
                if (infos.IndexOf(newVal.Name) == -1)
                {
                    infos.Add(newVal);
                }
                if (cmfc.SystemName == "Diameter")
                {
                    newVal = new FieldInfoClass
                    {
                        Name = cmfc.Name + "1",
                        Alias = "横截面宽",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    if (infos.IndexOf(newVal.Name) == -1)
                    {
                        infos.Add(newVal);
                    }
                    newVal = new FieldInfoClass
                    {
                        Name = cmfc.Name + "2",
                        Alias = "横截面高",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    if (infos.IndexOf(newVal.Name) == -1)
                    {
                        infos.Add(newVal);
                    }
                }

            }
            newVal = new FieldInfoClass
            {
                Name = "FacilityId",
                Alias = "设施编号",
                FieldType = gviFieldType.gviFieldString,
                Length = 50
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "GroupId",
                Alias = "逻辑组ID",
                FieldType = gviFieldType.gviFieldInt32,
                RegisteredRenderIndex = true
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "StyleId",
                Alias = "风格编号",
                FieldType = gviFieldType.gviFieldString,
                Length = 50
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "ModelName",
                Alias = "模型名称",
                FieldType = gviFieldType.gviFieldString,
                Length = 80
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Metadata",
                Alias = "Metadata",
                FieldType = gviFieldType.gviFieldBlob
            };
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Geometry",
                Alias = "三维空间列",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            IGeometryDef def = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnType.gviGeometryColumnModelPoint,
                HasZ = true
            };
            newVal.GeometryDef = def;
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "Shape",
                Alias = "二维空间列",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            gviGeometryColumnType gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnUnknown;
            switch (facType)
            {
                case "PipeNode":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPoint;
                    break;
                case "PipeLine":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPolyline;
                    break;
                case "PipeBuild":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPolygon;
                    break;
                case "PipeBuild1":
                    gviGeometryColumnUnknown = gviGeometryColumnType.gviGeometryColumnPolyline;
                    break;
            }
            IGeometryDef def2 = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnUnknown,
                HasZ = true
            };
            newVal.GeometryDef = def2;
            infos.Add(newVal);
            newVal = new FieldInfoClass
            {
                Name = "FootPrint",
                Alias = "投影二维",
                RegisteredRenderIndex = true,
                FieldType = gviFieldType.gviFieldGeometry
            };
            IGeometryDef def3 = new GeometryDefClass
            {
                GeometryColumnType = gviGeometryColumnUnknown,
                HasZ = false
            };
            newVal.GeometryDef = def3;
            infos.Add(newVal);
            return infos;
        }

        private bool CreateFacClassReg(FacClass fc)
        {
            if (fc == null || fc.FacilityType == null) return false;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IFeatureDataSet fdsActuality = this._dsPipe.OpenFeatureDataset("DataSet_GEO_Actuality");
                if (fdsActuality == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return false;

                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("FacClassCode"), fc.Code);
                    row.SetValue(row.FieldIndex("Name"), fc.Name);
                    row.SetValue(row.FieldIndex("FacilityType"), fc.FacilityType.Name);
                    row.SetValue(row.FieldIndex("LocationType"), fc.LocationType.ToString());
                    row.SetValue(row.FieldIndex("TurnerStyle"), fc.TurnerStyle.ToString());
                    row.SetValue(row.FieldIndex("Comment"), fc.Comment);
                    string fcName = string.Format("FC_{0}_{1}", (int)DataLifeCyle.Actuality, fc.Code);
                    List<CMFieldConfig> fieldConfig= GetFieldsConfig(fc.Code);
                    IFieldInfoCollection fielInfoCol = CreateFieldInfoCollection(fieldConfig, fc.FacilityType.Name);
                    IFeatureClass featureClass = fdsActuality.CreateFeatureClass(fcName, fielInfoCol);
                    if (featureClass == null) return false;
                    featureClass.AliasName = fc.Name;
                    featureClass.LockType = gviLockType.gviLockExclusiveSchema;
                    IGridIndexInfo indexInfo = new GridIndexInfoClass
                    {
                        L1 = 500.0,
                        L2 = 2000.0,
                        L3 = 10000.0,
                        GeoColumnName = "Geometry"
                    };
                    featureClass.AddSpatialIndex(indexInfo);
                    indexInfo.GeoColumnName = "Shape";
                    featureClass.AddSpatialIndex(indexInfo);
                    indexInfo.GeoColumnName = "FootPrint";
                    featureClass.AddSpatialIndex(indexInfo);
                    IRenderIndexInfo info2 = new RenderIndexInfoClass
                    {
                        L1 = 500.0,
                        GeoColumnName = "Geometry"
                    };
                    featureClass.AddRenderIndex(info2);
                    info2.GeoColumnName = "Shape";
                    featureClass.AddRenderIndex(info2);
                    info2.GeoColumnName = "FootPrint";
                    featureClass.AddRenderIndex(info2);
                    featureClass.LockType = gviLockType.gviLockSharedSchema;
                    row.SetValue(row.FieldIndex("FeatureClassId"), featureClass.Guid.ToString());
                    row.SetValue(row.FieldIndex("DataSetName"), "DataSet_GEO_Actuality");
                    row.SetValue(row.FieldIndex("FcName"), featureClass.Name);
                    row.SetValue(row.FieldIndex("DataType"), DataLifeCyle.Actuality.ToString()); 
                    cursor.InsertRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                WaitForm.SetCaption("创建管线库中的设施要素类【" + fc.Name + "】失败！");
                return false;
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

        public void SyncFacilityInfo()
        {
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return;

                WaitForm.SetCaption("检查管线库中的设施要素类...");
                List<FacClass> allFC = GetAllFacClasses();
                Dictionary<string, FacClass> dictionary = new Dictionary<string, FacClass>();
                if (allFC != null)
                {
                    foreach (FacClass fc in allFC)
                    {
                        dictionary.Add(fc.Code, fc);
                        // TODO
                    }
                }
                #region 检查设施要素类,有就更新，没有就删除
                List<FacClassReg> allFCR = GetAllFacClassRegs();
                Dictionary<string, FacClassReg> dictFCR = new Dictionary<string, FacClassReg>();
                if (allFCR != null)
                {
                    foreach (FacClassReg reg in allFCR)
                    {
                        dictFCR.Add(reg.FacClassCode, reg);
                        if (!dictionary.ContainsKey(reg.FacClassCode))
                        {
                            DeleteFacClassReg(reg);
                            WaitForm.SetCaption("正在删除管线库中的设施要素类【" + reg.FcName + "】...");
                            DeleteFeatureClass(reg.DataSetName, reg.FcName);
                        }
                        else
                        {
                            reg.Name = dictionary[reg.FacClassCode].Name;
                            IFeatureClass fc = OpenFeatureClass(reg.DataSetName, reg.FcName);
                            fc.AliasName = reg.Name;
                            reg.LocationType = dictionary[reg.FacClassCode].LocationType;
                            reg.TurnerStyle = dictionary[reg.FacClassCode].TurnerStyle;
                            reg.FacilityType = dictionary[reg.FacClassCode].FacilityType;
                            reg.Comment = dictionary[reg.FacClassCode].Comment;
                            WaitForm.SetCaption("更新管线库中的设施要素类【" + reg.Name + "】...");
                            UpdateFacClassReg(reg);
                        }

                    }
                }
                #endregion

                #region 创建设施要素类
                WaitForm.SetCaption("开始创建管线库中的设施要素类...");
                foreach (FacClass fc in allFC)
                {
                    if (!dictFCR.ContainsKey(fc.Code))
                    {
                        WaitForm.SetCaption("创建管线库中的设施要素类【" + fc.Name + "】...");
                        CreateFacClassReg(fc);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("同步管线库中的设施要素类失败！", "提示");
            }
        }

        public void Run()
        {
            if (this._dsTemplate == null || this._dsPipe == null) return;
            WaitForm.Start("同步管线库设施类启动...", "请稍后", new System.Drawing.Size(340, 50));
            try
            {
                SyncTopoInfo();
                SyncFacilityInfo();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                WaitForm.Stop();
            }
        }
    }
}
