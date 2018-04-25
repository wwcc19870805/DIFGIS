using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using DF3DControl.Base;
using ICSharpCode.Core;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Delegate;
using DF3DPipeCreateTool.Class;

namespace DF3DEdit.Class
{
    public class SelectCollection
    {
        private static SelectCollection _sc;
        private static HashMap featureClassInfoMap = null;
        private static HashMap fcRowBuffersMap = null;
        private event SelectionChangedEventHandler selectionChangedEvent;
        //private event RecordsChangedEventHandler recordsChangedEvent;
        private event PropertyTableSelectionChangedEventHandler propertyTableSelectionChangedEvent;
        private event FacStyleClassChangedHandle facStyleClassChangedEvent;
        //private event RemoveFromSelectionEventHandler removeFromSelectionEvent;
        //private event PickUpSingleRecordEventHandler pickUpSingleRecordEvent;
        private event System.Action<IVector3> selectionMovingEvent;
        public event System.Action<IVector3> SelectionMovingEvent
        {
            add
            {
                this.selectionMovingEvent += value;
            }
            remove
            {
                this.selectionMovingEvent -= value;
            }
        }
        //public event PickUpSingleRecordEventHandler PickUpSingleRecordEvent
        //{
        //    add
        //    {
        //        this.pickUpSingleRecordEvent += value;
        //    }
        //    remove
        //    {
        //        this.pickUpSingleRecordEvent -= value;
        //    }
        //}
        //public event RemoveFromSelectionEventHandler RemoveFromSelectionEvent
        //{
        //    add
        //    {
        //        this.removeFromSelectionEvent += value;
        //    }
        //    remove
        //    {
        //        this.removeFromSelectionEvent -= value;
        //    }
        //}
        public event SelectionChangedEventHandler SelectionChangedEvent
        {
            add
            {
                this.selectionChangedEvent += value;
            }
            remove
            {
                this.selectionChangedEvent -= value;
            }
        }
        //public event RecordsChangedEventHandler RecordsChangedEvent
        //{
        //    add
        //    {
        //        this.recordsChangedEvent += value;
        //    }
        //    remove
        //    {
        //        this.recordsChangedEvent -= value;
        //    }
        //}
        public event FacStyleClassChangedHandle FacStyleClassChangedEvent
        {
            add
            {
                this.facStyleClassChangedEvent += value;
            }
            remove
            {
                this.facStyleClassChangedEvent -= value;
            }
        }

        public event PropertyTableSelectionChangedEventHandler PropertyTableSelectionChangedEvent
        {
            add
            {
                this.propertyTableSelectionChangedEvent += value;
            }
            remove
            {
                this.propertyTableSelectionChangedEvent -= value;
            }
        }
        public HashMap FeatureClassInfoMap
        {
            get
            {
                return SelectCollection.featureClassInfoMap;
            }
        }
        public HashMap FcRowBuffersMap
        {
            get
            {
                return SelectCollection.fcRowBuffersMap;
            }
        }
        public void SelectionMoving(IVector3 vec)
        {
            if (this.selectionMovingEvent != null)
            {
                this.selectionMovingEvent(vec);
            }
        }
        public void PropertyTableSelectionChanged(DataRow dr)
        {
            if (this.propertyTableSelectionChangedEvent != null)
            {
                this.propertyTableSelectionChangedEvent(dr);
            }
        }
        public void FacStyleClassChanged(FacStyleClass style)
        {
            if (this.facStyleClassChangedEvent != null)
            {
                this.facStyleClassChangedEvent(style);
            }
        }
        //public void PickUpSingleRecord(FeatureClassInfo conn, int oid)
        //{
        //    if (this.pickUpSingleRecordEvent != null)
        //    {
        //        this.pickUpSingleRecordEvent(conn, oid);
        //    }
        //}
        public static SelectCollection Instance()
        {
            if (SelectCollection._sc == null)
            {
                SelectCollection._sc = new SelectCollection();
                SelectCollection.featureClassInfoMap = new HashMap();
                SelectCollection.fcRowBuffersMap = new HashMap();
            }
            return SelectCollection._sc;
        }
        public bool IsEmpty()
        {
            return SelectCollection.featureClassInfoMap == null || SelectCollection.featureClassInfoMap.Count == 0 || this.GetCount(false) == 0;
        }
        public int GetCount(bool bOnlyModelPoint)
        {
            int num = 0;
            if (SelectCollection.featureClassInfoMap == null || SelectCollection.featureClassInfoMap.Keys.Count == 0)
            {
                return num;
            }
            foreach (DF3DFeatureClass featureClassInfo in SelectCollection.featureClassInfoMap.Keys)
            {
                ResultSetInfo resultSetInfo = SelectCollection.featureClassInfoMap[featureClassInfo] as ResultSetInfo;
                if (resultSetInfo != null && (!bOnlyModelPoint || featureClassInfo.GetFeatureLayer().GeometryType == gviGeometryColumnType.gviGeometryColumnModelPoint))
                {
                    num += resultSetInfo.ResultSetTable.Rows.Count;
                }
            }
            return num;
        }
        public void UpdateSelection(System.Collections.Generic.List<DF3DFeatureClass> fcList, ISpatialFilter filter, int nSize)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;

                lock (SelectCollection.featureClassInfoMap)
                {
                    SelectCollection.featureClassInfoMap.Clear();
                    app.Current3DMapControl.FeatureManager.UnhighlightAll();
                    if (fcList != null)
                    {
                        foreach (DF3DFeatureClass current in fcList)
                        {
                            IFeatureClass featureClass = current.GetFeatureClass();
                            if (featureClass == null)
                            {
                                return;
                            }
                            if (filter == null)
                            {
                                filter = new SpatialFilterClass();
                            }
                            else
                            {
                                //if (filter.Geometry != null)
                                //{
                                //    filter.GeometryField = current.GeometryFieldName;
                                //}
                            }
                            filter.SubFields = featureClass.FidFieldName;
                            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                            try
                            {
                                IFdeCursor fdeCursor = featureClass.Search(filter, true);
                                IRowBuffer rowBuffer;
                                while ((rowBuffer = fdeCursor.NextRow()) != null)
                                {
                                    int item = int.Parse(rowBuffer.GetValue(0).ToString());
                                    list.Add(item);
                                }
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                            }
                            catch (System.Runtime.InteropServices.COMException)
                            {
                            }
                            if (list.Count == 0)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                            }
                            else
                            {
                                System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
                                int num = (list.Count > nSize) ? nSize : list.Count;
                                for (int i = 0; i < num; i++)
                                {
                                    list2.Add(list[i]);
                                }
                                //DataTable dt = this.CreateDataTable(featureClass, current.GeometryFieldName);
                                //this.GetResultSet(featureClass, list2, dt);
                                //SelectCollection.featureClassInfoMap[current] = new ResultSetInfo(dt, list);
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                                //featureClass = null;
                            }
                        }
                        this.NotifySelection();
                        //MainFrmService.UpdateMenu();
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                //XtraMessageBox.Show(Logger.GetErrorMessage(ex));
            }
            catch (System.UnauthorizedAccessException)
            {
                //XtraMessageBox.Show(StringParser.Parse("${res:Dataset_InsufficientPermission}"));
            }
        }
        public void UpdateSelection(System.Collections.Hashtable tables)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;

            lock (SelectCollection.featureClassInfoMap)
            {
                if (SelectCollection.featureClassInfoMap != null && SelectCollection.featureClassInfoMap.Count > 0)
                {
                    SelectCollection.featureClassInfoMap.Clear();
                    app.Current3DMapControl.FeatureManager.UnhighlightAll();
                    this.ClearRowBuffers();
                }
                if (tables != null)
                {
                    foreach (DF3DFeatureClass featureClassInfo in tables.Keys)
                    {
                        System.Collections.Generic.List<int> list = tables[featureClassInfo] as System.Collections.Generic.List<int>;
                        if (list != null && list.Count != 0)
                        {
                            list = list.Distinct<int>().ToList<int>();
                            IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                            if (featureClass != null)
                            {
                                DataTable dt = this.CreateDataTable(featureClass, featureClassInfo.GetFeatureLayer().GeometryFieldName);
                                if (RenderControlEditServices.Instance().RenderEditorType != RenderEditorType.UnKnownType)
                                {
                                    IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                                    this.GetResultSet(featureClass, list, dt, rowBufferCollection);
                                    SelectCollection.fcRowBuffersMap[featureClassInfo] = rowBufferCollection;
                                }
                                else
                                {
                                    this.GetResultSet(featureClass, list, dt, null);
                                }
                                SelectCollection.featureClassInfoMap[featureClassInfo] = new ResultSetInfo(dt, list);
                            }
                        }
                    }
                    this.NotifySelection();
                    //MainFrmService.UpdateMenu();
                }
            }
        }
        public void UpdateSelection(DF3DFeatureClass fcInfo, IRowBufferCollection rows)
        {
            lock (SelectCollection.featureClassInfoMap)
            {
                ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[fcInfo] as ResultSetInfo;
                if (resultSetInfo != null)
                {
                    IRowBufferCollection rowBufferCollection = null;
                    if (SelectCollection.fcRowBuffersMap != null && SelectCollection.fcRowBuffersMap.Contains(fcInfo))
                    {
                        rowBufferCollection = (SelectCollection.fcRowBuffersMap[fcInfo] as IRowBufferCollection);
                    }
                    DataTable resultSetTable = resultSetInfo.ResultSetTable;
                    IFeatureClass featureClass = fcInfo.GetFeatureClass();
                    if (featureClass != null)
                    {
                        if (rows != null)
                        {
                            for (int i = 0; i < rows.Count; i++)
                            {
                                IRowBuffer rowBuffer = rows.Get(i);
                                this.UpdateRecord(featureClass, resultSetTable, rowBuffer);
                                if (rowBufferCollection != null)
                                {
                                    this.ReplaceRow(rowBufferCollection, rowBuffer.Clone(true));
                                }
                            }
                        }
                        //功能面板变更
                        //this.RecordsChanged(fcInfo.GetFeatureLayer().Guid.ToString(), resultSetInfo.TotalCount, resultSetInfo.ResultSetTable.Rows.Count);
                    }
                }
            }
        }
        public void UpdateSelection(DF3DFeatureClass fcInfo, string colName, System.Collections.Generic.Dictionary<int, string> values)
        {
            lock (SelectCollection.featureClassInfoMap)
            {
                ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[fcInfo] as ResultSetInfo;
                if (resultSetInfo != null)
                {
                    DataTable resultSetTable = resultSetInfo.ResultSetTable;
                    if (values != null)
                    {
                        foreach (int current in values.Keys)
                        {
                            string value = values[current];
                            DataRow dataRow = resultSetTable.Rows.Find(current);
                            if (dataRow != null)
                            {
                                if (string.IsNullOrEmpty(value))
                                {
                                    dataRow[colName] = System.DBNull.Value;
                                }
                                else
                                {
                                    dataRow[colName] = value;
                                }
                            }
                        }
                    }
                    this.RecordsChanged(fcInfo.GetFeatureLayer().Guid.ToString(), resultSetInfo.TotalCount, resultSetInfo.ResultSetTable.Rows.Count);
                }
            }
        }
        public void NextResultSize(DF3DFeatureClass fcInfo, int nSize)
        {
            try
            {
                //CommandManager.Pop();
                //lock (SelectCollection.featureClassInfoMap)
                //{
                //    ResultSetInfo resultSetInfo = (ResultSetInfo)SelectCollection.featureClassInfoMap[fcInfo];
                //    DataTable resultSetTable = resultSetInfo.ResultSetTable;
                //    IFeatureClass featureClass = fcInfo.OpenFeatureClass();
                //    if (featureClass != null)
                //    {
                //        if (resultSetTable.Rows.Count < resultSetInfo.TotalCount)
                //        {
                //            int count = resultSetTable.Rows.Count;
                //            int num = resultSetInfo.TotalCount - count;
                //            int num2 = (num > nSize) ? nSize : num;
                //            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                //            for (int i = count; i < count + num2; i++)
                //            {
                //                list.Add(resultSetInfo.OidList[i]);
                //            }
                //            this.GetResultSet(featureClass, list, resultSetTable);
                //        }
                //        this.RecordsChanged(fcInfo.FeatureLayerGuidString, resultSetInfo.TotalCount, resultSetInfo.ResultSetTable.Rows.Count);
                //        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                //    }
                //}
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public void RemoveFromSelection(DF3DFeatureClass fcInfo, System.Collections.Generic.List<int> oidList)
        {
            //if (fcInfo == null)
            //{
            //    return;
            //}
            //RenderControlEditServices.Instance().StopGeometryEdit(true);
            //try
            //{
            //    RenderControlServices.Instance().AxRenderControl.PauseRendering(false);
            //    if (SelectCollection.fcRowBuffersMap != null && SelectCollection.fcRowBuffersMap.Contains(fcInfo))
            //    {
            //        IRowBufferCollection rowBufferCollection = SelectCollection.fcRowBuffersMap[fcInfo] as IRowBufferCollection;
            //        if (oidList == null)
            //        {
            //            if (rowBufferCollection != null)
            //            {
            //                rowBufferCollection.Clear();
            //            }
            //            SelectCollection.fcRowBuffersMap.Remove(fcInfo);
            //        }
            //        else
            //        {
            //            if (rowBufferCollection != null)
            //            {
            //                for (int i = 0; i < rowBufferCollection.Count; i++)
            //                {
            //                    IRowBuffer rowBuffer = rowBufferCollection.Get(i);
            //                    if (rowBuffer != null)
            //                    {
            //                        int num = rowBuffer.FieldIndex("oid");
            //                        if (num != -1)
            //                        {
            //                            int item = (int)rowBuffer.GetValue(num);
            //                            if (oidList.Contains(item))
            //                            {
            //                                rowBufferCollection.RemoveAt(i);
            //                                i = 0;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    if (SelectCollection.featureClassInfoMap != null && SelectCollection.featureClassInfoMap.Contains(fcInfo))
            //    {
            //        IFeatureClass featureClass = fcInfo.OpenFeatureClass();
            //        if (oidList == null)
            //        {
            //            RenderControlServices.Instance().AxRenderControl.FeatureManager.UnhighlightFeatureClass(featureClass);
            //            SelectCollection.featureClassInfoMap.Remove(fcInfo);
            //        }
            //        else
            //        {
            //            ResultSetInfo resultSetInfo = SelectCollection.featureClassInfoMap[fcInfo] as ResultSetInfo;
            //            if (resultSetInfo == null)
            //            {
            //                return;
            //            }
            //            DataTable resultSetTable = resultSetInfo.ResultSetTable;
            //            for (int j = 0; j < oidList.Count; j++)
            //            {
            //                int num2 = oidList[j];
            //                DataRow dataRow = resultSetTable.Rows.Find(num2);
            //                if (dataRow != null)
            //                {
            //                    dataRow.Delete();
            //                    resultSetInfo.TotalCount--;
            //                    if (resultSetInfo.OidList != null)
            //                    {
            //                        resultSetInfo.OidList.Remove(num2);
            //                    }
            //                }
            //                RenderControlServices.Instance().AxRenderControl.FeatureManager.UnhighlightFeature(featureClass, num2);
            //            }
            //        }
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
            //        MainFrmService.UpdateMenu();
            //        RenderControlEditServices.Instance().SetEditorPosition(this.GetSelectGeometrys());
            //        if (this.removeFromSelectionEvent != null)
            //        {
            //            this.removeFromSelectionEvent();
            //        }
            //    }
            //}
            //catch (System.Runtime.InteropServices.COMException ex)
            //{
            //    XtraMessageBox.Show(Logger.GetErrorMessage(ex));
            //}
            //finally
            //{
            //    RenderControlServices.Instance().AxRenderControl.ResumeRendering();
            //}
        }
        public void DeleteSelection(DF3DFeatureClass fcInfo, System.Array oidList)
        {
            lock (SelectCollection.featureClassInfoMap)
            {
                ResultSetInfo resultSetInfo = SelectCollection.featureClassInfoMap[fcInfo] as ResultSetInfo;
                if (resultSetInfo != null)
                {
                    DataTable resultSetTable = resultSetInfo.ResultSetTable;
                    for (int i = 0; i < oidList.Length; i++)
                    {
                        DataRow dataRow = resultSetTable.Rows.Find(oidList.GetValue(i));
                        if (dataRow != null)
                        {
                            dataRow.Delete();
                            resultSetInfo.TotalCount--;
                        }
                    }
                    this.RecordsChanged(fcInfo.GetFeatureLayer().Guid.ToString(), resultSetInfo.TotalCount, resultSetInfo.ResultSetTable.Rows.Count);
                    //MainFrmService.UpdateMenu();
                    if (RenderControlEditServices.Instance().RenderEditorType != RenderEditorType.UnKnownType)
                    {
                        RenderControlEditServices.Instance().SetEditorPosition(this.GetSelectGeometrys());
                    }
                }
            }
        }
        public void InsertSelection(DF3DFeatureClass fcInfo, IRowBufferCollection rows, bool bClearAll, bool bSetEditorPosition)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            lock (SelectCollection.featureClassInfoMap)
            {
                IFeatureClass featureClass = fcInfo.GetFeatureClass();
                ResultSetInfo resultSetInfo = SelectCollection.featureClassInfoMap[fcInfo] as ResultSetInfo;
                if (resultSetInfo == null)
                {
                    DataTable dt = this.CreateDataTable(featureClass, fcInfo.GetFeatureLayer().GeometryFieldName);
                    resultSetInfo = new ResultSetInfo(dt, this.GetOIDList(rows));
                    SelectCollection.featureClassInfoMap[fcInfo] = resultSetInfo;
                    this.NotifySelection();
                }
                else
                {
                    if (bClearAll)
                    {
                        app.Current3DMapControl.FeatureManager.UnhighlightFeatureClass(featureClass);
                        resultSetInfo.ResultSetTable.Rows.Clear();
                    }
                }
                resultSetInfo.TotalCount = resultSetInfo.ResultSetTable.Rows.Count;
                DataTable resultSetTable = resultSetInfo.ResultSetTable;
                for (int i = 0; i < rows.Count; i++)
                {
                    int num = this.InsertRecord(featureClass, resultSetTable, rows.Get(i));
                    if (resultSetInfo.OidList == null)
                    {
                        resultSetInfo.OidList = new System.Collections.Generic.List<int>();
                    }
                    if (num != -1)
                    {
                        resultSetInfo.TotalCount++;
                        if (!resultSetInfo.OidList.Contains(num))
                        {
                            resultSetInfo.OidList.Add(num);
                        }
                    }
                }
                if (resultSetInfo.OidList.Count<int>() > 0)
                {
                    app.Current3DMapControl.FeatureManager.HighlightFeatures(featureClass, resultSetInfo.OidList.ToArray(), 4294901860u);
                }
                this.RecordsChanged(fcInfo.GetFeatureLayer().Guid.ToString(), resultSetInfo.TotalCount, resultSetInfo.ResultSetTable.Rows.Count);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                //MainFrmService.UpdateMenu();
                if (bSetEditorPosition)
                {
                    RenderControlEditServices.Instance().SetEditorPosition(this.GetSelectGeometrys());
                }
            }
        }
        public void Clear()
        {
            lock (SelectCollection.featureClassInfoMap)
            {

                SelectCollection.featureClassInfoMap.Clear();
                this.ClearRowBuffers();
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                app.Current3DMapControl.FeatureManager.UnhighlightAll();
                this.NotifySelection();
                //MainFrmService.UpdateMenu();
                RenderControlEditServices.Instance().SetEditorPosition(this.GetSelectGeometrys());
            }
        }
        public void ClearRowBuffers()
        {
            if (SelectCollection.fcRowBuffersMap != null)
            {
                foreach (DF3DFeatureClass key in SelectCollection.fcRowBuffersMap.Keys)
                {
                    IRowBufferCollection rowBufferCollection = SelectCollection.fcRowBuffersMap[key] as IRowBufferCollection;
                    if (rowBufferCollection != null)
                    {
                        rowBufferCollection.Clear();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(rowBufferCollection);
                    }
                }
                SelectCollection.fcRowBuffersMap.Clear();
                RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.fcRowBuffersMap);
            }
        }
        public System.Collections.Generic.List<int> GetOIDList(IRowBufferCollection rows)
        {
            if (rows == null)
            {
                return null;
            }
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
            for (int i = 0; i < rows.Count; i++)
            {
                object value = rows.Get(i).GetValue(0);
                int item;
                if (value != null && int.TryParse(value.ToString(), out item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public void NotifySelection()
        {
            if (this.selectionChangedEvent != null)
            {
                this.selectionChangedEvent();
            }
        }
        public void RecordsChanged(string strGuid, int nTotal, int nLoad)
        {
            // 面板变更
            //if (this.recordsChangedEvent != null)
            //{
            //    this.recordsChangedEvent(strGuid, nTotal, nLoad);
            //}
        }
        public DataTable CreateDataTable(IFeatureClass fc, string GeometryFieldName)
        {
            DataTable dataTable = new DataTable();
            if (fc != null)
            {
                IFieldInfoCollection fields = fc.GetFields();
                for (int i = 0; i < fields.Count; i++)
                {
                    IFieldInfo fieldInfo = fields.Get(i);
                    if (fieldInfo.FieldType != gviFieldType.gviFieldBlob && (fieldInfo.FieldType != gviFieldType.gviFieldGeometry || !(fieldInfo.Name != GeometryFieldName)) && !(fieldInfo.Name == fc.TemporalColumnName))
                    {
                        DataColumn dataColumn = new DataColumn(fieldInfo.Name);
                        if (!string.IsNullOrEmpty(fieldInfo.Alias))
                        {
                            dataColumn.Caption = fieldInfo.Alias;
                        }
                        else
                        {
                            dataColumn.Caption = fieldInfo.Name;
                        }
                        gviFieldType fieldType = fieldInfo.FieldType;
                        switch (fieldType)
                        {
                            case gviFieldType.gviFieldInt16:
                                {
                                    dataColumn.DataType = typeof(short);
                                    break;
                                }
                            case gviFieldType.gviFieldInt32:
                                {
                                    dataColumn.DataType = typeof(int);
                                    break;
                                }
                            case gviFieldType.gviFieldInt64:
                                {
                                    dataColumn.DataType = typeof(long);
                                    break;
                                }
                            case gviFieldType.gviFieldFloat:
                                {
                                    dataColumn.DataType = typeof(float);
                                    break;
                                }
                            case gviFieldType.gviFieldDouble:
                                {
                                    dataColumn.DataType = typeof(double);
                                    break;
                                }
                            case gviFieldType.gviFieldString:
                            case gviFieldType.gviFieldUUID:
                                {
                                    dataColumn.DataType = typeof(string);
                                    break;
                                }
                            case gviFieldType.gviFieldDate:
                                {
                                    dataColumn.DataType = typeof(System.DateTime);
                                    break;
                                }
                            case gviFieldType.gviFieldBlob:
                                {
                                    goto IL_195;
                                }
                            case gviFieldType.gviFieldFID:
                                {
                                    dataColumn.DataType = typeof(int);
                                    dataColumn.ReadOnly = true;
                                    break;
                                }
                            default:
                                {
                                    if (fieldType != gviFieldType.gviFieldGeometry)
                                    {
                                        goto IL_195;
                                    }
                                    dataColumn.DataType = typeof(string);
                                    break;
                                }
                        }
                    IL_1A5:
                        dataTable.Columns.Add(dataColumn);
                        if (fieldInfo.Name == fc.FidFieldName)
                        {
                            dataTable.PrimaryKey = new DataColumn[]
							{
								dataColumn
							};
                            goto IL_1DA;
                        }
                        goto IL_1DA;
                    IL_195:
                        dataColumn.DataType = typeof(string);
                        goto IL_1A5;
                    }
                IL_1DA: ;
                }
                dataTable.DefaultView.Sort = string.Format("{0} asc", fc.FidFieldName);
            }
            return dataTable;
        }
        private void GetResultSet(IFeatureClass fc, System.Collections.Generic.List<int> oidList, DataTable dt)
        {
            if (fc == null || oidList.Count == 0)
            {
                return;
            }
            try
            {
                IFdeCursor rows = fc.GetRows(oidList.ToArray(), false);
                this.GetResultSet(fc, rows, dt, null);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(rows);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                //XtraMessageBox.Show(Logger.GetErrorMessage(ex));
            }
        }
        private void GetResultSet(IFeatureClass fc, System.Collections.Generic.List<int> fidList, DataTable dt, IRowBufferCollection rows)
        {
            if (fc != null)
            {
                try
                {
                    IFdeCursor fdeCursor = null;
                    if (rows == null)
                    {
                        fdeCursor = fc.GetRows(fidList.ToArray(), true);
                    }
                    else
                    {
                        fdeCursor = fc.GetRows(fidList.ToArray(), false);
                    }
                    this.GetResultSet(fc, fdeCursor, dt, rows);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    //XtraMessageBox.Show(Logger.GetErrorMessage(ex));
                }
            }
        }
        private void GetResultSet(IFeatureClass fc, IFdeCursor cursor, DataTable dt, IRowBufferCollection rows)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;

                if (cursor != null)
                {
                    dt.BeginLoadData();
                    System.Collections.Generic.IList<int> list = new System.Collections.Generic.List<int>();
                    IRowBuffer rowBuffer;
                    while ((rowBuffer = cursor.NextRow()) != null)
                    {
                        if (rows != null)
                        {
                            rows.Add(rowBuffer);
                        }
                        int num = -1;
                        DataRow dataRow = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            string columnName = dt.Columns[i].ColumnName;
                            int num2 = rowBuffer.FieldIndex(columnName);
                            if (num2 != -1 && !rowBuffer.IsNull(num2))
                            {
                                object value = rowBuffer.GetValue(num2);
                                if (num2 == 0)
                                {
                                    num = int.Parse(value.ToString());
                                }
                                if (value is IGeometry)
                                {
                                    IGeometry geometry = value as IGeometry;
                                    if (geometry != null)
                                    {
                                        gviGeometryType geometryType = geometry.GeometryType;
                                        if (geometryType <= gviGeometryType.gviGeometryPolyline)
                                        {
                                            switch (geometryType)
                                            {
                                                case gviGeometryType.gviGeometryPoint:
                                                    {
                                                        dataRow[i] = "Point";
                                                        break;
                                                    }
                                                case gviGeometryType.gviGeometryModelPoint:
                                                    {
                                                        dataRow[i] = "ModelPoint";
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        if (geometryType == gviGeometryType.gviGeometryPolyline)
                                                        {
                                                            dataRow[i] = "Polyline";
                                                        }
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            if (geometryType != gviGeometryType.gviGeometryPolygon)
                                            {
                                                switch (geometryType)
                                                {
                                                    case gviGeometryType.gviGeometryMultiPoint:
                                                        {
                                                            dataRow[i] = "MultiPoint";
                                                            break;
                                                        }
                                                    case gviGeometryType.gviGeometryMultiPolyline:
                                                        {
                                                            dataRow[i] = "MultiPolyline";
                                                            break;
                                                        }
                                                    case gviGeometryType.gviGeometryMultiPolygon:
                                                        {
                                                            dataRow[i] = "MultiPolygon";
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            if (geometryType == gviGeometryType.gviGeometryPointCloud)
                                                            {
                                                                dataRow[i] = "PointCloud";
                                                            }
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                            {
                                                dataRow[i] = "Polygon";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    dataRow[i] = value;
                                }
                            }
                        }
                        if (num != -1 && !dt.Rows.Contains(num))
                        {
                            dt.Rows.Add(dataRow);
                            list.Add(num);
                        }
                    }
                    dt.EndLoadData();
                    if (list.Count<int>() > 0)
                    {
                        app.Current3DMapControl.FeatureManager.HighlightFeatures(fc, list.ToArray<int>(), 4294901860u);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                XtraMessageBox.Show(StringParser.Parse("${res:View_QueryFailed}"));
            }
        }
        public void UpdateRecord(IFeatureClass fc, DataTable dt, IRowBuffer newRow)
        {
            int num = newRow.FieldIndex(fc.FidFieldName);
            if (num == -1)
            {
                return;
            }
            int num2 = int.Parse(newRow.GetValue(num).ToString());
            DataRow dataRow = dt.Rows.Find(num2);
            if (dataRow == null)
            {
                return;
            }
            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                string columnName = dataColumn.ColumnName;
                if (!dataColumn.ReadOnly)
                {
                    num = newRow.FieldIndex(columnName);
                    if (num != -1 && newRow.IsChanged(num))
                    {
                        if (newRow.IsNull(num))
                        {
                            dataRow[dataColumn] = System.DBNull.Value;
                        }
                        else
                        {
                            if (newRow.GetValue(num) is IGeometry)
                            {
                                IGeometry geometry = newRow.GetValue(num) as IGeometry;
                                if (geometry != null)
                                {
                                    gviGeometryType geometryType = geometry.GeometryType;
                                    if (geometryType <= gviGeometryType.gviGeometryPolyline)
                                    {
                                        switch (geometryType)
                                        {
                                            case gviGeometryType.gviGeometryPoint:
                                                {
                                                    dataRow[dataColumn] = "Point";
                                                    break;
                                                }
                                            case gviGeometryType.gviGeometryModelPoint:
                                                {
                                                    dataRow[dataColumn] = "ModelPoint";
                                                    break;
                                                }
                                            default:
                                                {
                                                    if (geometryType == gviGeometryType.gviGeometryPolyline)
                                                    {
                                                        dataRow[dataColumn] = "Polyline";
                                                    }
                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        if (geometryType != gviGeometryType.gviGeometryPolygon)
                                        {
                                            switch (geometryType)
                                            {
                                                case gviGeometryType.gviGeometryMultiPoint:
                                                    {
                                                        dataRow[dataColumn] = "MultiPoint";
                                                        break;
                                                    }
                                                case gviGeometryType.gviGeometryMultiPolyline:
                                                    {
                                                        dataRow[dataColumn] = "MultiPolyline";
                                                        break;
                                                    }
                                                case gviGeometryType.gviGeometryMultiPolygon:
                                                    {
                                                        dataRow[dataColumn] = "MultiPolygon";
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        if (geometryType == gviGeometryType.gviGeometryPointCloud)
                                                        {
                                                            dataRow[dataColumn] = "PointCloud";
                                                        }
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            dataRow[dataColumn] = "Polygon";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dataRow[dataColumn] = newRow.GetValue(num);
                            }
                        }
                    }
                }
            }
        }
        public int InsertRecord(IFeatureClass fc, DataTable dt, IRowBuffer newRow)
        {
            DataRow dataRow = dt.NewRow();
            int num = -1;
            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                int num2 = newRow.FieldIndex(dataColumn.ColumnName);
                if (num2 != -1 && !newRow.IsNull(num2))
                {
                    object obj = newRow.GetValue(num2);
                    if (num2 == 0)
                    {
                        num = int.Parse(obj.ToString());
                    }
                    if (obj is IGeometry)
                    {
                        IGeometry geometry = newRow.GetValue(num2) as IGeometry;
                        if (geometry != null)
                        {
                            gviGeometryType geometryType = geometry.GeometryType;
                            if (geometryType <= gviGeometryType.gviGeometryPolyline)
                            {
                                switch (geometryType)
                                {
                                    case gviGeometryType.gviGeometryPoint:
                                        {
                                            obj = "Point";
                                            break;
                                        }
                                    case gviGeometryType.gviGeometryModelPoint:
                                        {
                                            obj = "ModelPoint";
                                            break;
                                        }
                                    default:
                                        {
                                            if (geometryType == gviGeometryType.gviGeometryPolyline)
                                            {
                                                obj = "Polyline";
                                            }
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                if (geometryType != gviGeometryType.gviGeometryPolygon)
                                {
                                    switch (geometryType)
                                    {
                                        case gviGeometryType.gviGeometryMultiPoint:
                                            {
                                                obj = "MultiPoint";
                                                break;
                                            }
                                        case gviGeometryType.gviGeometryMultiPolyline:
                                            {
                                                obj = "MultiPolyline";
                                                break;
                                            }
                                        case gviGeometryType.gviGeometryMultiPolygon:
                                            {
                                                obj = "MultiPolygon";
                                                break;
                                            }
                                        default:
                                            {
                                                if (geometryType == gviGeometryType.gviGeometryPointCloud)
                                                {
                                                    dataRow[dataColumn] = "PointCloud";
                                                }
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    obj = "Polygon";
                                }
                            }
                        }
                    }
                    dataRow[dataColumn] = obj;
                }
            }
            if (num != -1)
            {
                dt.Rows.Add(dataRow);
            }
            return num;
        }
        public IEnvelope GetSelectEnvelope(HashMap rowbufferMap)
        {
            //if (!WorkSpaceServices.Instance().PropertyCanEdit)
            //{
            //    return null;
            //}
            if (rowbufferMap == null || rowbufferMap.Count == 0)
            {
                return null;
            }
            IEnvelope envelope = new EnvelopeClass();
            foreach (DF3DFeatureClass featureClassInfo in rowbufferMap.Keys)
            {
                IRowBufferCollection rowBufferCollection = rowbufferMap[featureClassInfo] as IRowBufferCollection;
                if (rowBufferCollection != null && rowBufferCollection.Count != 0)
                {
                    for (int i = 0; i < rowBufferCollection.Count; i++)
                    {
                        IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                        int num = rowBuffer.FieldIndex(featureClassInfo.GetFeatureLayer().GeometryFieldName);
                        if (num != -1)
                        {
                            IGeometry geometry = rowBuffer.GetValue(num) as IGeometry;
                            if (geometry != null)
                            {
                                envelope.ExpandByEnvelope(geometry.Envelope);
                            }
                        }
                    }
                }
            }
            return envelope;
        }
        public IEnvelope GetSelectEnvelope(out string strWkt)
        {
            strWkt = string.Empty;
            HashMap hashMap = SelectCollection.Instance().FeatureClassInfoMap;
            if (hashMap == null || hashMap.Count == 0)
            {
                return null;
            }
            IEnvelope envelope = new EnvelopeClass();
            foreach (DF3DFeatureClass featureClassInfo in hashMap.Keys)
            {
                try
                {
                    IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                    if (featureClass != null)
                    {
                        //strWkt = featureClass.FeatureDataSet.SpatialReference.AsWKT();
                        //ResultSetInfo resultSetInfo = hashMap[featureClassInfo] as ResultSetInfo;
                        //DataTable resultSetTable = resultSetInfo.ResultSetTable;
                        //System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                        //foreach (DataRow dataRow in resultSetTable.Rows)
                        //{
                        //    int item = int.Parse(dataRow[featureClass.FidFieldName].ToString());
                        //    if (!list.Contains(item))
                        //    {
                        //        list.Add(item);
                        //    }
                        //}
                        //envelope.ExpandByEnvelope(featureClass.GetFeaturesEnvelope(list.ToArray(), featureClassInfo.GeometryFieldName));
                        //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    }
                }
                catch (System.Exception e)
                {
                    LoggingService.Error(e.Message);
                }
            }
            return envelope;
        }
        public HashMap GetSelectGeometrys()
        {
            //if (!WorkSpaceServices.Instance().PropertyCanEdit)
            //{
            //    return null;
            //}
            if (SelectCollection.featureClassInfoMap == null || SelectCollection.featureClassInfoMap.Count == 0)
            {
                return null;
            }
            try
            {
                if (SelectCollection.fcRowBuffersMap != null)
                {
                    SelectCollection.fcRowBuffersMap.Clear();
                }
                foreach (DF3DFeatureClass featureClassInfo in SelectCollection.featureClassInfoMap.Keys)
                {
                    IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                    System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                    ResultSetInfo resultSetInfo = SelectCollection.featureClassInfoMap[featureClassInfo] as ResultSetInfo;
                    IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                    int item = -1;
                    foreach (DataRow dataRow in resultSetInfo.ResultSetTable.Rows)
                    {
                        if (int.TryParse(dataRow[featureClass.FidFieldName].ToString(), out item))
                        {
                            list.Add(item);
                        }
                    }
                    IFdeCursor rows = featureClass.GetRows(list.ToArray(), false);
                    IRowBuffer value;
                    while ((value = rows.NextRow()) != null)
                    {
                        rowBufferCollection.Add(value);
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rows);
                    SelectCollection.fcRowBuffersMap[featureClassInfo] = rowBufferCollection;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
            return SelectCollection.fcRowBuffersMap;
        }

        public System.DateTime GetMaxBirthDay()
        {
            System.DateTime dateTime = System.DateTime.MinValue;
            if (SelectCollection.featureClassInfoMap == null || SelectCollection.featureClassInfoMap.Count == 0)
            {
                return dateTime;
            }
            try
            {
                foreach (DF3DFeatureClass featureClassInfo in SelectCollection.featureClassInfoMap.Keys)
                {
                    System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                    ResultSetInfo resultSetInfo = SelectCollection.featureClassInfoMap[featureClassInfo] as ResultSetInfo;
                    IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                    if (!featureClass.HasTemporal())
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    }
                    else
                    {
                        int item = -1;
                        foreach (DataRow dataRow in resultSetInfo.ResultSetTable.Rows)
                        {
                            if (int.TryParse(dataRow[featureClass.FidFieldName].ToString(), out item))
                            {
                                list.Add(item);
                            }
                        }
                        System.DateTime maxBirthDay = this.GetMaxBirthDay(featureClass, list.ToArray());
                        if (maxBirthDay > dateTime)
                        {
                            dateTime = maxBirthDay;
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    }
                }
            }
            catch (System.Exception)
            {
                return dateTime;
            }
            return dateTime;
        }
        public System.DateTime GetMaxBirthDay(IFeatureClass fc, int[] oidList)
        {
            System.DateTime dateTime = System.DateTime.MinValue;
            //if (fc == null || !fc.HasTemporal() || oidList == null || oidList.Length == 0)
            //{
            //    return dateTime;
            //}
            //try
            //{
            //    IQueryFilter queryFilter = new QueryFilterClass();
            //    queryFilter.AddSubField(fc.TemporalColumnName);
            //    queryFilter.IdsFilter = oidList.ToArray<int>();
            //    IFdeCursor fdeCursor = fc.Search(queryFilter, false);
            //    IRowBuffer rowBuffer;
            //    while ((rowBuffer = fdeCursor.NextRow()) != null)
            //    {
            //        long njd = (long)rowBuffer.GetValue(0);
            //        System.DateTime dateTime2 = TimeConvert.FromJDDate(njd);
            //        if (dateTime2 > dateTime)
            //        {
            //            dateTime = dateTime2;
            //        }
            //    }
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
            //}
            //catch (System.Exception)
            //{
            //    return dateTime;
            //}
            return dateTime;
        }
        public HashMap Clone(HashMap fcRowBuffers)
        {
            if (fcRowBuffers == null)
            {
                return null;
            }
            HashMap hashMap = new HashMap();
            foreach (DF3DFeatureClass key in fcRowBuffers.Keys)
            {
                IRowBufferCollection rowBufferCollection = fcRowBuffers[key] as IRowBufferCollection;
                IRowBufferCollection rowBufferCollection2 = new RowBufferCollectionClass();
                for (int i = 0; i < rowBufferCollection.Count; i++)
                {
                    IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                    rowBufferCollection2.Add(rowBuffer.Clone(false));
                }
                hashMap[key] = rowBufferCollection2;
            }
            return hashMap;
        }
        private void ReplaceRow(IRowBufferCollection rows, IRowBuffer row)
        {
            if (rows != null && row != null)
            {
                int num = (int)row.GetValue(0);
                for (int i = 0; i < rows.Count; i++)
                {
                    IRowBuffer rowBuffer = rows.Get(i);
                    int num2 = (int)rowBuffer.GetValue(0);
                    if (num == num2)
                    {
                        rows.Set(i, row);
                        return;
                    }
                }
            }
        }
    }
}
