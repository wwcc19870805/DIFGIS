using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using DevExpress.Utils;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeUndoRedo;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Frm;
using DF3DEdit.Service;

namespace DF3DEdit.Class
{
    public class UndoRedoOperator : System.IDisposable
    {
        private delegate void NotifySelection();
        private delegate void RecordsChanged(string strGuid, int nTotal, int nLoad);
        private delegate DataTable CreateDataTableHandel(IFeatureClass fc, string GeometryFieldNam);
        private delegate int InsertRecordHandel(IFeatureClass fc, DataTable dt, IRowBuffer row);
        private delegate void DeleteRecordHandel(DataRow dr);
        private delegate HashMap GetRowbufferMapHandel();
        private delegate void SetEditorPositionHandel(HashMap rowMap);
        private const int _MaxSize = 300;
        private int _Type = -1;
        private int _nTotalCount;
        private int _nCurIndex;
        private System.ComponentModel.BackgroundWorker _bgWorker;
        private ProgressDialog _progressDlg;
        private UndoRedoOperator.NotifySelection _notifySelection;
        private UndoRedoOperator.RecordsChanged _recordsChanged;
        private UndoRedoOperator.CreateDataTableHandel _CreaDataTable;
        private UndoRedoOperator.InsertRecordHandel _InsertRecord;
        private UndoRedoOperator.DeleteRecordHandel _deleteRecord;
        private UndoRedoOperator.GetRowbufferMapHandel _GetRowbufferMap;
        private UndoRedoOperator.SetEditorPositionHandel _setEditorPositionHandel;
        private WaitDialogForm _wfDlg;
        private DF3DApplication app;
        public UndoRedoOperator(int nType)
        {
            this.app = DF3DApplication.Application;
            this._Type = nType;
            if (CommonUtils.Instance().FdeUndoRedoManager != null)
            {
                if (nType == 0)
                {
                    CommonUtils.Instance().FdeUndoRedoManager.UndoStart += new _ICommandManagerEvents_UndoStartEventHandler(this.fdeUndoRedoManager_UndoStart);
                }
                else
                {
                    if (nType == 1)
                    {
                        CommonUtils.Instance().FdeUndoRedoManager.RedoStart += new _ICommandManagerEvents_RedoStartEventHandler(this.fdeUndoRedoManager_RedoStart);
                    }
                }
                this._nTotalCount = (this._nCurIndex = 0);
                this._bgWorker = new System.ComponentModel.BackgroundWorker();
                this._bgWorker.WorkerReportsProgress = true;
                this._bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWorker_DoWork);
                this._bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGWorker_ProgressChanged);
                this._bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWorker_RunWorkerCompleted);
                string text = "初始化,请稍后...";
                this._wfDlg = new WaitDialogForm("", text);
                this._wfDlg.Show();
                this._progressDlg = new ProgressDialog();
            }
        }
        public virtual void Dispose()
        {
            if (this._wfDlg != null)
            {
                this._wfDlg.Close();
                this._wfDlg = null;
            }
            if (this._Type == 0)
            {
                CommonUtils.Instance().FdeUndoRedoManager.UndoStart -= new _ICommandManagerEvents_UndoStartEventHandler(this.fdeUndoRedoManager_UndoStart);
            }
            else
            {
                if (this._Type == 1)
                {
                    CommonUtils.Instance().FdeUndoRedoManager.RedoStart -= new _ICommandManagerEvents_RedoStartEventHandler(this.fdeUndoRedoManager_RedoStart);
                }
            }
            this._bgWorker.DoWork -= new System.ComponentModel.DoWorkEventHandler(this.BGWorker_DoWork);
            this._bgWorker.ProgressChanged -= new System.ComponentModel.ProgressChangedEventHandler(this.BGWorker_ProgressChanged);
            this._bgWorker.RunWorkerCompleted -= new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWorker_RunWorkerCompleted);
        }
        private void fdeUndoRedoManager_UndoStart(IUndoRedoResultCollection Coll)
        {
            this._wfDlg.Close();
            this._wfDlg = null;
            this._nTotalCount = this.GetTotalCount(Coll);
            if (this._nTotalCount > 300)
            {
                try
                {
                    try
                    {
                        this._notifySelection = new UndoRedoOperator.NotifySelection(SelectCollection.Instance().NotifySelection);
                        this._recordsChanged = new UndoRedoOperator.RecordsChanged(SelectCollection.Instance().RecordsChanged);
                        this._CreaDataTable = new UndoRedoOperator.CreateDataTableHandel(SelectCollection.Instance().CreateDataTable);
                        this._InsertRecord = new UndoRedoOperator.InsertRecordHandel(SelectCollection.Instance().InsertRecord);
                        this._deleteRecord = new UndoRedoOperator.DeleteRecordHandel(this.DeleteRow);
                        this._bgWorker.RunWorkerAsync(Coll);
                        this._progressDlg.ShowDialog();
                    }
                    catch (System.Exception)
                    {
                    }
                    return;
                }
                finally
                {
                    if (app != null && app.Current3DMapControl != null)
                    {
                        app.Current3DMapControl.FeatureManager.RefreshAll();
                    }
                }
            }
            this.ExecuteUndoRedo(Coll);
        }
        private void fdeUndoRedoManager_RedoStart(IUndoRedoResultCollection Coll)
        {
            this._wfDlg.Close();
            this._wfDlg = null;
            this._nTotalCount = this.GetTotalCount(Coll);
            if (this._nTotalCount > 300)
            {
                try
                {
                    try
                    {
                        this._notifySelection = new UndoRedoOperator.NotifySelection(SelectCollection.Instance().NotifySelection);
                        this._recordsChanged = new UndoRedoOperator.RecordsChanged(SelectCollection.Instance().RecordsChanged);
                        this._CreaDataTable = new UndoRedoOperator.CreateDataTableHandel(SelectCollection.Instance().CreateDataTable);
                        this._InsertRecord = new UndoRedoOperator.InsertRecordHandel(SelectCollection.Instance().InsertRecord);
                        this._deleteRecord = new UndoRedoOperator.DeleteRecordHandel(this.DeleteRow);
                        this._bgWorker.RunWorkerAsync(Coll);
                        this._progressDlg.ShowDialog();
                    }
                    catch (System.Exception)
                    {
                    }
                    return;
                }
                finally
                {
                    if (app != null && app.Current3DMapControl != null)
                    {
                        app.Current3DMapControl.FeatureManager.RefreshAll();
                    }
                }
            }
            this.ExecuteUndoRedo(Coll);
        }
        private int GetTotalCount(IUndoRedoResultCollection Coll)
        {
            int num = 0;
            if (Coll != null)
            {
                int count = Coll.Count;
                if (count == 0)
                {
                    return num;
                }
                for (int i = 0; i < count; i++)
                {
                    IUndoRedoResult undoRedoResult = Coll[i];
                    if (undoRedoResult != null)
                    {
                        switch (undoRedoResult.Type)
                        {
                            case gviCommandType.gviCommandInsert:
                                {
                                    IRowBufferCollection rowBuffers = undoRedoResult.RowBuffers;
                                    num += rowBuffers.Count;
                                    break;
                                }
                            case gviCommandType.gviCommandDelete:
                                {
                                    int[] fidArray = undoRedoResult.FidArray;
                                    num += fidArray.Length;
                                    break;
                                }
                            case gviCommandType.gviCommandUpdate:
                                {
                                    IRowBufferCollection rowBuffers2 = undoRedoResult.RowBuffers;
                                    num += rowBuffers2.Count;
                                    break;
                                }
                        }
                    }
                }
            }
            return num;
        }
        private void ExecuteUndoRedo(IUndoRedoResultCollection Coll)
        {
            if (Coll != null)
            {
                int count = Coll.Count;
                if (count == 0)
                {
                    return;
                }
                if (app == null || app.Current3DMapControl == null) return;
                app.Current3DMapControl.PauseRendering(false);
                for (int i = 0; i < count; i++)
                {
                    IUndoRedoResult undoRedoResult = Coll[i];
                    if (undoRedoResult != null)
                    {
                        IObjectClass objectClass = undoRedoResult.ObjectClass;
                        IRowBufferCollection rowBuffers = undoRedoResult.RowBuffers;
                        DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(objectClass.GuidString);
                        switch (undoRedoResult.Type)
                        {
                            case gviCommandType.gviCommandInsert:
                                {
                                    CommonUtils.Instance().Insert(featureClassInfo, rowBuffers, false, true);
                                    app.Current3DMapControl.FeatureManager.CreateFeatures(objectClass as IFeatureClass, rowBuffers);
                                    break;
                                }
                            case gviCommandType.gviCommandDelete:
                                {
                                    int[] fidArray = undoRedoResult.FidArray;
                                    app.Current3DMapControl.FeatureManager.DeleteFeatures(objectClass as IFeatureClass, fidArray);
                                    CommonUtils.Instance().Delete(featureClassInfo, fidArray);
                                    break;
                                }
                            case gviCommandType.gviCommandUpdate:
                                {
                                    CommonUtils.Instance().Update(featureClassInfo, rowBuffers);
                                    app.Current3DMapControl.FeatureManager.EditFeatures(objectClass as IFeatureClass, rowBuffers);
                                    break;
                                }
                        }
                        if (rowBuffers != null)
                        {
                            rowBuffers.Clear();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(rowBuffers);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(objectClass);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(undoRedoResult);
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(Coll);
                RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().FcRowBuffersMap);
                app.Current3DMapControl.ResumeRendering();
            }
        }
        private void AsyncExecuteUndoRedo(IUndoRedoResultCollection Coll)
        {
            if (Coll != null)
            {
                int count = Coll.Count;
                if (count == 0)
                {
                    return;
                }
                for (int i = 0; i < count; i++)
                {
                    IUndoRedoResult undoRedoResult = Coll[i];
                    if (undoRedoResult != null)
                    {
                        IObjectClass objectClass = undoRedoResult.ObjectClass;
                        IRowBufferCollection rowBuffers = undoRedoResult.RowBuffers;
                        switch (undoRedoResult.Type)
                        {
                            case gviCommandType.gviCommandInsert:
                                {
                                    this.InsertSelection(objectClass, rowBuffers);
                                    break;
                                }
                            case gviCommandType.gviCommandDelete:
                                {
                                    int[] fidArray = undoRedoResult.FidArray;
                                    this.DeleteSelection(objectClass, fidArray);
                                    break;
                                }
                            case gviCommandType.gviCommandUpdate:
                                {
                                    this.UpdateSelection(objectClass, rowBuffers);
                                    break;
                                }
                        }
                        if (rowBuffers != null)
                        {
                            rowBuffers.Clear();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(rowBuffers);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(objectClass);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(undoRedoResult);
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(Coll);
            }
        }
        private void BGWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._progressDlg.Close();
        }
        private void BGWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this._progressDlg.labelTooltip.Text = e.UserState.ToString();
            this._progressDlg.progressBarControl.EditValue = e.ProgressPercentage;
        }
        private void BGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IUndoRedoResultCollection undoRedoResultCollection = e.Argument as IUndoRedoResultCollection;
            if (undoRedoResultCollection != null)
            {
                this._GetRowbufferMap = (() => SelectCollection.Instance().GetSelectGeometrys());
                this._setEditorPositionHandel = delegate(HashMap rowMap)
                {
                    RenderControlEditServices.Instance().SetEditorPosition(rowMap);
                }
                ;
                this.AsyncExecuteUndoRedo(undoRedoResultCollection);
            }
        }
        private void InsertSelection(IObjectClass oc, IRowBufferCollection rows)
        {
            //FeatureClassInfo featureClassInfo = WorkSpaceServices.Instance().GetFeatureClassInfo(oc.Name, true);
            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(oc.GuidString);
            if (featureClassInfo == null)
            {
                return;
            }
            if (oc.Type == gviDataSetType.gviDataSetFeatureClassTable)
            {
                lock (SelectCollection.Instance().FeatureClassInfoMap)
                {
                    ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
                    if (resultSetInfo == null)
                    {
                        object[] args = new object[]
						{
							oc, 
							//featureClassInfo.GeometryFieldName
                            featureClassInfo.GetFeatureLayer().GeometryFieldName
						};
                        //DataTable dt = MainFrmService.MainFrm.Invoke(this._CreaDataTable, args) as DataTable;
                        //resultSetInfo = new ResultSetInfo(dt, SelectCollection.Instance().GetOIDList(rows));
                        //resultSetInfo.TotalCount = 0;
                        //SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] = resultSetInfo;
                        //MainFrmService.MainFrm.Invoke(this._notifySelection);
                    }
                    //DataTable resultSetTable = resultSetInfo.ResultSetTable;
                    //for (int i = 0; i < rows.Count; i++)
                    //{
                    //    this._nCurIndex++;
                    //    string userState = string.Format(StringParser.Parse("${res:feature_progress_finished}"), this._nCurIndex, this._nTotalCount);
                    //    int percentProgress = this._nCurIndex * 100 / this._nTotalCount;
                    //    this._bgWorker.ReportProgress(percentProgress, userState);
                    //    object[] args2 = new object[]
                    //    {
                    //        oc, 
                    //        resultSetTable, 
                    //        rows.Get(i)
                    //    };
                    //    int num = -1;
                    //    int.TryParse(MainFrmService.MainFrm.Invoke(this._InsertRecord, args2).ToString(), out num);
                    //    if (resultSetInfo.OidList == null)
                    //    {
                    //        resultSetInfo.OidList = new System.Collections.Generic.List<int>();
                    //    }
                    //    if (num != -1 && !resultSetInfo.OidList.Contains(num))
                    //    {
                    //        resultSetInfo.OidList.Add(num);
                    //    }
                    //}
                    //if (resultSetInfo.OidList.Count<int>() > 0)
                    //{
                    //    RenderControlServices.Instance().AxRenderControl.FeatureManager.HighlightFeatures(oc as IFeatureClass, resultSetInfo.OidList.ToArray(), 4294901860u);
                    //}
                    //resultSetInfo.TotalCount += rows.Count;
                    //object[] args3 = new object[]
                    //{
                    //    featureClassInfo.FeatureLayerGuidString, 
                    //    resultSetInfo.TotalCount, 
                    //    resultSetInfo.ResultSetTable.Rows.Count
                    //};
                    //MainFrmService.MainFrm.Invoke(this._recordsChanged, args3);
                    //HashMap hashMap = MainFrmService.MainFrm.Invoke(this._GetRowbufferMap) as HashMap;
                    //MainFrmService.MainFrm.Invoke(this._setEditorPositionHandel, new object[]
                    //{
                    //    hashMap
                    //});
                }
            }
        }
        private void DeleteSelection(IObjectClass oc, int[] oidList)
        {
            //FeatureClassInfo featureClassInfo = WorkSpaceServices.Instance().GetFeatureClassInfo(oc.Name, true);
            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(oc.GuidString);
            if (featureClassInfo == null)
            {
                return;
            }
            if (oc.Type == gviDataSetType.gviDataSetFeatureClassTable)
            {
                lock (SelectCollection.Instance().FeatureClassInfoMap)
                {
                    ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
                    if (resultSetInfo != null)
                    {
                        DataTable resultSetTable = resultSetInfo.ResultSetTable;
                        //for (int i = 0; i < oidList.Length; i++)
                        //{
                        //    this._nCurIndex++;
                        //    DataRow dataRow = resultSetTable.Rows.Find(oidList.GetValue(i));
                        //    if (dataRow != null)
                        //    {
                        //        MainFrmService.MainFrm.Invoke(this._deleteRecord, new object[]
                        //        {
                        //            dataRow
                        //        });
                        //        resultSetInfo.TotalCount--;
                        //    }
                        //    string userState = string.Format(StringParser.Parse("${res:feature_progress_finished}"), this._nCurIndex, this._nTotalCount);
                        //    int percentProgress = this._nCurIndex * 100 / this._nTotalCount;
                        //    this._bgWorker.ReportProgress(percentProgress, userState);
                        //}
                        //object[] args = new object[]
                        //{
                        //    featureClassInfo.FeatureLayerGuidString, 
                        //    resultSetInfo.TotalCount, 
                        //    resultSetInfo.ResultSetTable.Rows.Count
                        //};
                        //MainFrmService.MainFrm.Invoke(this._recordsChanged, args);
                        //HashMap hashMap = MainFrmService.MainFrm.Invoke(this._GetRowbufferMap) as HashMap;
                        //MainFrmService.MainFrm.Invoke(this._setEditorPositionHandel, new object[]
                        //{
                        //    hashMap
                        //});
                    }
                }
            }
        }
        private void UpdateSelection(IObjectClass oc, IRowBufferCollection rows)
        {
            //FeatureClassInfo featureClassInfo = WorkSpaceServices.Instance().GetFeatureClassInfo(oc.Name, true);
            //if (featureClassInfo == null)
            //{
            //    return;
            //}
            //if (oc.Type == gviDataSetType.gviDataSetFeatureClassTable)
            //{
            //    lock (SelectCollection.Instance().FeatureClassInfoMap)
            //    {
            //        ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
            //        if (resultSetInfo != null)
            //        {
            //            DataTable resultSetTable = resultSetInfo.ResultSetTable;
            //            if (rows != null)
            //            {
            //                for (int i = 0; i < rows.Count; i++)
            //                {
            //                    this._nCurIndex++;
            //                    IRowBuffer newRow = rows.Get(i);
            //                    SelectCollection.Instance().UpdateRecord(oc as IFeatureClass, resultSetTable, newRow);
            //                    string userState = string.Format(StringParser.Parse("${res:feature_progress_finished}"), this._nCurIndex, this._nTotalCount);
            //                    int percentProgress = this._nCurIndex * 100 / this._nTotalCount;
            //                    this._bgWorker.ReportProgress(percentProgress, userState);
            //                }
            //            }
            //            object[] args = new object[]
            //            {
            //                featureClassInfo.FeatureLayerGuidString, 
            //                resultSetInfo.TotalCount, 
            //                resultSetInfo.ResultSetTable.Rows.Count
            //            };
            //            MainFrmService.MainFrm.Invoke(this._recordsChanged, args);
            //            HashMap hashMap = MainFrmService.MainFrm.Invoke(this._GetRowbufferMap) as HashMap;
            //            MainFrmService.MainFrm.Invoke(this._setEditorPositionHandel, new object[]
            //            {
            //                hashMap
            //            });
            //        }
            //    }
            //}
        }
        private void DeleteRow(DataRow dr)
        {
            if (dr != null)
            {
                dr.Delete();
            }
        }
    }
}
