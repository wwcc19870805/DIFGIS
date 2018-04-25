using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Service;
using DF3DEdit.Frm;
using DF3DEdit.Delegate;

namespace DF3DEdit.Class
{
    public class LarageDataEdit : AbstractBatcheEdit
    {
        private bool bNeedOperator = true;
        private BackgroundWorker _bgWorker;
        private System.Threading.ManualResetEvent _manualResult;
        private ProgressDialog1 _progressDlg;
        private ClearSelectionHandle _clearSelection;
        private DeleteSelectionHandle _deleteSelection;
        private UpdateSelectionHandel _updateSelection;
        private InsertSelectionHandel _InsertSelection;
        //private NextPageHandel _NextPage;
        public override bool BeginEdit(bool bNeedTootip)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            this.bNeedOperator = bNeedTootip;
            if (this.bNeedOperator)
            {
                return this.BeginEdit();
            }
            this._clearSelection = new ClearSelectionHandle(SelectCollection.Instance().Clear);
            this._deleteSelection = new DeleteSelectionHandle(CommonUtils.Instance().Delete);
            this._updateSelection = new UpdateSelectionHandel(SelectCollection.Instance().UpdateSelection);
            this._InsertSelection = new InsertSelectionHandel(CommonUtils.Instance().Insert);
            //this._NextPage = new NextPageHandel(SelectCollection.Instance().NextResultSize);
            this.bNeedOperator = true;
            app.Current3DMapControl.PauseRendering(false);
            return this.bNeedOperator;
        }
        public override bool BeginEdit()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            System.Windows.Forms.DialogResult dialogResult = XtraMessageBox.Show(StringParser.Parse("${res:feature_alert_batch_edit}"), StringParser.Parse("${res:feature_alert_tooltip}"), System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                this._clearSelection = new ClearSelectionHandle(SelectCollection.Instance().Clear);
                this._deleteSelection = new DeleteSelectionHandle(CommonUtils.Instance().Delete);
                this._updateSelection = new UpdateSelectionHandel(SelectCollection.Instance().UpdateSelection);
                this._InsertSelection = new InsertSelectionHandel(CommonUtils.Instance().Insert);
                //this._NextPage = new NextPageHandel(SelectCollection.Instance().NextResultSize);
                this.bNeedOperator = true;
                app.Current3DMapControl.PauseRendering(false);
            }
            else
            {
                this.bNeedOperator = false;
            }
            return this.bNeedOperator;
        }
        public override void DoWork(EditType editType, EditParameters parameters)
        {
            if (!this.bNeedOperator)
            {
                return;
            }
            this._manualResult = new System.Threading.ManualResetEvent(true);
            this._bgWorker = new BackgroundWorker();
            this._bgWorker.WorkerReportsProgress = true;
            this._bgWorker.WorkerSupportsCancellation = true;
            this._bgWorker.DoWork += new DoWorkEventHandler(this.BGWorker_DoWork);
            this._bgWorker.ProgressChanged += new ProgressChangedEventHandler(this.BGWorker_ProgressChanged);
            this._bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BGWorker_RunWorkerCompleted);
            this._progressDlg = new ProgressDialog1(this._bgWorker, this._manualResult);
            this._progressDlg.btnCancle.Visible = false;
            object[] argument = new object[]
			{
				editType, 
				parameters
			};
            this._bgWorker.RunWorkerAsync(argument);
            this._progressDlg.ShowDialog();
        }
        public override void EndEdit()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.FeatureManager.RefreshAll();
            if (!this.bNeedOperator)
            {
                RenderControlEditServices.Instance().SetEditorPosition(SelectCollection.Instance().GetSelectGeometrys());
                return;
            }
            app.Current3DMapControl.ResumeRendering();
        }
        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                XtraMessageBox.Show(e.Error.Message);
            }
            this._progressDlg.Close();
        }
        private void BGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this._progressDlg.labelTooltip.Text = e.UserState.ToString();
            this._progressDlg.progressBarControl.EditValue = e.ProgressPercentage;
        }
        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] array = e.Argument as object[];
            EditType editType = (EditType)array[0];
            object param = array[1];
            switch (editType)
            {
                case EditType.ET_DELETE_SELCTION:
                    {
                        this.DeleteSelection(param);
                        return;
                    }
                case EditType.ET_DELETE_RECYCLE:
                case EditType.ET_UPDATE_MERGE_SELECTION:
                    {
                        break;
                    }
                case EditType.ET_DELETE_FEATURES:
                    {
                        this.DeleteFeatures(param);
                        return;
                    }
                case EditType.ET_UPDATE_ATTRIBUTE:
                    {
                        this.UpdateAttribute(param);
                        return;
                    }
                case EditType.ET_UPDATE_GEOMETRY:
                    {
                        this.UpdateGeometry(param);
                        return;
                    }
                case EditType.ET_INSERT_FEATURES:
                    {
                        this.InsertFeatures(param);
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        private void DeleteSelection(object param)
        {
            EditParameters editParameters = (EditParameters)param;
            if (editParameters == null)
            {
                return;
            }
            try
            {
                int count = SelectCollection.Instance().GetCount(false);
                int num = 0;
                IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSourceByString(editParameters.connectionInfo);
                IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(editParameters.datasetName);
                foreach (DF3DFeatureClass featureClassInfo in SelectCollection.Instance().FeatureClassInfoMap.Keys)
                {
                    if (this._bgWorker.CancellationPending)
                    {
                        break;
                    }
                    ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
                    if (resultSetInfo != null)
                    {
                        string geometryFieldName = featureClassInfo.GetFeatureLayer().GeometryFieldName;
                        IFeatureClass featureClass = featureDataSet.OpenFeatureClass(featureClassInfo.GetFeatureClass().Name);
                        string fidFieldName = featureClass.FidFieldName;
                        System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                        foreach (DataRow dataRow in resultSetInfo.ResultSetTable.Rows)
                        {
                            int item = int.Parse(dataRow[fidFieldName].ToString());
                            list.Add(item);
                        }
                        featureClass.SetRenderIndexEnabled(geometryFieldName, false);
                        if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                        {
                            TemporalFilter temporalFilter = new TemporalFilterClass();
                            temporalFilter.AddSubField(featureClass.FidFieldName);
                            temporalFilter.IdsFilter = list.ToArray();
                            ITemporalManager temporalManager = featureClass.TemporalManager;
                            ITemporalCursor temporalCursor = temporalManager.Search(temporalFilter);
                            while (temporalCursor.MoveNext())
                            {
                                this._manualResult.WaitOne();
                                System.Threading.Thread.Sleep(1);
                                num++;
                                string userState = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, count);
                                int percentProgress = num * 100 / count;
                                this._bgWorker.ReportProgress(percentProgress, userState);
                                temporalCursor.Dead(editParameters.TemproalTime);
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                        }
                        else
                        {
                            featureClass.FeatureDataSet.DataSource.StartEditing();
                            IFdeCursor fdeCursor = featureClass.Update(new QueryFilterClass
                            {
                                IdsFilter = list.ToArray()
                            });
                            while (fdeCursor.NextRow() != null && !this._bgWorker.CancellationPending)
                            {
                                System.Threading.Thread.Sleep(1);
                                fdeCursor.DeleteRow();
                                num++;
                                string userState2 = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, count);
                                int percentProgress2 = num * 100 / count;
                                this._bgWorker.ReportProgress(percentProgress2, userState2);
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                            featureClass.FeatureDataSet.DataSource.StopEditing(true);
                        }
                        if (this._progressDlg.Created)
                        {
                            string userState3 = StringParser.Parse("${res:feature_progress_updateindex}");
                            int percentProgress3 = num * 100 / count;
                            this._bgWorker.ReportProgress(percentProgress3, userState3);
                        }
                        featureClass.SetRenderIndexEnabled(geometryFieldName, true);
                        featureClass.RebuildRenderIndex(geometryFieldName, gviRenderIndexRebuildType.gviRenderIndexRebuildWithData);
                        //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._clearSelection);
                //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
            }
            catch (System.Exception)
            {
            }
        }
        private void DeleteFeatures(object param)
        {
            IFeatureClass featureClass = null;
            IDataSource dataSource = null;
            IFeatureDataSet featureDataSet = null;
            try
            {
                EditParameters editParameters = (EditParameters)param;
                DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(editParameters.featureClassGuid);
                string fcName = editParameters.fcName;
                if (!string.IsNullOrEmpty(fcName) && featureClassInfo != null)
                {
                    dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSourceByString(editParameters.connectionInfo);
                    featureDataSet = dataSource.OpenFeatureDataset(editParameters.datasetName);
                    int nTotalCount = editParameters.nTotalCount;
                    int num = 0;
                    featureClass = featureDataSet.OpenFeatureClass(fcName);
                    string geometryFieldName = featureClassInfo.GetFeatureLayer().GeometryFieldName;
                    featureClass.SetRenderIndexEnabled(geometryFieldName, false);
                    if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                    {
                        System.DateTime temproalTime = editParameters.TemproalTime;
                        TemporalFilter temporalFilter = new TemporalFilterClass();
                        temporalFilter.AddSubField(featureClass.FidFieldName);
                        temporalFilter.IdsFilter = editParameters.fidList;
                        ITemporalManager temporalManager = featureClass.TemporalManager;
                        ITemporalCursor temporalCursor = temporalManager.Search(temporalFilter);
                        while (temporalCursor.MoveNext())
                        {
                            temporalCursor.Dead(temproalTime);
                            num++;
                            string userState = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, nTotalCount);
                            int percentProgress = num * 100 / nTotalCount;
                            this._bgWorker.ReportProgress(percentProgress, userState);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                    }
                    else
                    {
                        featureClass.FeatureDataSet.DataSource.StartEditing();
                        IFdeCursor fdeCursor = featureClass.Update(new QueryFilterClass
                        {
                            IdsFilter = editParameters.fidList
                        });
                        while (fdeCursor.NextRow() != null && !this._bgWorker.CancellationPending)
                        {
                            fdeCursor.DeleteRow();
                            num++;
                            string userState2 = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, nTotalCount);
                            int percentProgress2 = num * 100 / nTotalCount;
                            this._bgWorker.ReportProgress(percentProgress2, userState2);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                        featureClass.FeatureDataSet.DataSource.StopEditing(true);
                    }
                    if (this._progressDlg.Created)
                    {
                        string userState3 = StringParser.Parse("${res:feature_progress_updateindex}");
                        int percentProgress3 = num * 100 / nTotalCount;
                        this._bgWorker.ReportProgress(percentProgress3, userState3);
                    }
                    featureClass.SetRenderIndexEnabled(geometryFieldName, true);
                    featureClass.RebuildRenderIndex(geometryFieldName, gviRenderIndexRebuildType.gviRenderIndexRebuildWithData);
                    object[] args = new object[]
					{
						featureClassInfo, 
						editParameters.fidList
					};
                    //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._deleteSelection, args);
                    //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
                }
            }
            catch (System.Exception)
            {
                if (featureClass != null)
                {
                    featureClass.FeatureDataSet.DataSource.StopEditing(false);
                }
            }
            finally
            {
                //if (featureClass != null)
                //{
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                //    featureClass = null;
                //}
                if (featureDataSet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                    featureDataSet = null;
                }
                if (dataSource != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                    dataSource = null;
                }
            }
        }
        private void UpdateAttribute(object param)
        {
            IFeatureClass featureClass = null;
            try
            {
                EditParameters editParameters = (EditParameters)param;
                if (editParameters != null)
                {
                    DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(editParameters.featureClassGuid);
                    IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSourceByString(editParameters.connectionInfo);
                    IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(editParameters.datasetName);
                    featureClass = featureDataSet.OpenFeatureClass(editParameters.fcName);
                    int nTotalCount = editParameters.nTotalCount;
                    int num = 0;
                    if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                    {
                        ITemporalManager temporalManager = featureClass.TemporalManager;
                        ITemporalCursor temporalCursor = temporalManager.Search(new TemporalFilterClass
                        {
                            IdsFilter = editParameters.fidList
                        });
                        while (temporalCursor.MoveNext())
                        {
                            this._manualResult.WaitOne();
                            num++;
                            string userState = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                            int percentProgress = num * 100 / nTotalCount;
                            this._bgWorker.ReportProgress(percentProgress, userState);
                            bool flag = false;
                            int currentId = temporalCursor.CurrentId;
                            IRowBuffer row = featureClass.GetRow(currentId);
                            base.UpdateRowBuffer(ref row, editParameters.colName, editParameters.regexDataList);
                            ITemporalInstanceCursor temporalInstances = temporalCursor.GetTemporalInstances(false);
                            TemporalInstance temporalInstance;
                            while ((temporalInstance = temporalInstances.NextInstance()) != null)
                            {
                                if (temporalInstance.StartDatetime == editParameters.TemproalTime)
                                {
                                    flag = true;
                                    temporalInstances.Update(row);
                                    break;
                                }
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalInstances);
                            if (!flag)
                            {
                                temporalCursor.Insert(editParameters.TemproalTime, row);
                            }
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                    }
                    else
                    {
                        featureClass.FeatureDataSet.DataSource.StartEditing();
                        IFdeCursor fdeCursor = featureClass.Update(new QueryFilterClass
                        {
                            IdsFilter = editParameters.fidList
                        });
                        IRowBuffer rowBuffer = null;
                        System.Collections.Generic.Dictionary<int, string> dictionary = new System.Collections.Generic.Dictionary<int, string>();
                        while ((rowBuffer = fdeCursor.NextRow()) != null && !this._bgWorker.CancellationPending)
                        {
                            int position = rowBuffer.FieldIndex(featureClass.FidFieldName);
                            int key = int.Parse(rowBuffer.GetValue(position).ToString());
                            string value = base.UpdateRowBuffer(ref rowBuffer, editParameters.colName, editParameters.regexDataList);
                            fdeCursor.UpdateRow(rowBuffer);
                            num++;
                            dictionary[key] = value;
                            string userState2 = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                            int percentProgress2 = num * 100 / nTotalCount;
                            this._bgWorker.ReportProgress(percentProgress2, userState2);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(fdeCursor);
                        featureClass.FeatureDataSet.DataSource.StopEditing(true);
                        object[] args = new object[]
						{
							featureClassInfo, 
							editParameters.colName, 
							dictionary
						};
                        //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._updateSelection, args);
                        //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
                    }
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                }
            }
            catch (System.Exception)
            {
                if (featureClass != null)
                {
                    featureClass.FeatureDataSet.DataSource.StopEditing(true);
                }
            }
        }
        private void UpdateGeometry(object param)
        {
            try
            {
                EditParameters editParameters = (EditParameters)param;
                if (editParameters != null)
                {
                    System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap = editParameters.geometryMap;
                    if (geometryMap != null)
                    {
                        IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSourceByString(editParameters.connectionInfo);
                        IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(editParameters.datasetName);
                        int nTotalCount = editParameters.nTotalCount;
                        int num = 0;
                        foreach (DF3DFeatureClass current in geometryMap.Keys)
                        {
                            if (this._bgWorker.CancellationPending)
                            {
                                break;
                            }
                            IFeatureClass featureClass = featureDataSet.OpenFeatureClass(current.GetFeatureClass().Name);
                            IRowBufferCollection rowBufferCollection = geometryMap[current];
                            if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                            {
                                int position = featureClass.GetFields().IndexOf(featureClass.FidFieldName);
                                System.Collections.Generic.Dictionary<int, IRowBuffer> dictionary = new System.Collections.Generic.Dictionary<int, IRowBuffer>();
                                for (int i = 0; i < rowBufferCollection.Count; i++)
                                {
                                    IRowBuffer rowBuffer = rowBufferCollection.Get(i);
                                    int key = (int)rowBuffer.GetValue(position);
                                    dictionary[key] = rowBuffer;
                                }
                                ITemporalManager temporalManager = featureClass.TemporalManager;
                                ITemporalCursor temporalCursor = temporalManager.Search(new TemporalFilterClass
                                {
                                    IdsFilter = dictionary.Keys.ToArray<int>()
                                });
                                while (temporalCursor.MoveNext() && !this._bgWorker.CancellationPending)
                                {
                                    this._manualResult.WaitOne();
                                    System.Threading.Thread.Sleep(1);
                                    num++;
                                    string userState = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                                    int percentProgress = num * 100 / nTotalCount;
                                    this._bgWorker.ReportProgress(percentProgress, userState);
                                    bool flag = false;
                                    int currentId = temporalCursor.CurrentId;
                                    ITemporalInstanceCursor temporalInstances = temporalCursor.GetTemporalInstances(false);
                                    TemporalInstance temporalInstance;
                                    while ((temporalInstance = temporalInstances.NextInstance()) != null)
                                    {
                                        if (temporalInstance.StartDatetime == editParameters.TemproalTime)
                                        {
                                            flag = true;
                                            temporalInstances.Update(dictionary[currentId]);
                                            break;
                                        }
                                    }
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalInstances);
                                    if (!flag)
                                    {
                                        temporalCursor.Insert(editParameters.TemproalTime, dictionary[currentId]);
                                    }
                                }
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                            }
                            else
                            {
                                IRowBufferCollection rowBufferCollection2 = new RowBufferCollectionClass();
                                int num2 = 0;
                                while (num2 < rowBufferCollection.Count && !this._bgWorker.CancellationPending)
                                {
                                    IRowBuffer value = rowBufferCollection.Get(num2).Clone(true);
                                    rowBufferCollection2.Add(value);
                                    this._manualResult.WaitOne();
                                    System.Threading.Thread.Sleep(1);
                                    num++;
                                    string userState2 = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                                    int percentProgress2 = num * 100 / nTotalCount;
                                    this._bgWorker.ReportProgress(percentProgress2, userState2);
                                    num2++;
                                }
                                featureClass.UpdateRows(rowBufferCollection2, false);
                            }
                            //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            catch (System.UnauthorizedAccessException)
            {
                XtraMessageBox.Show(StringParser.Parse("${res:Dataset_InsufficientPermission}"));
            }
            catch (System.Exception e)
            {
                LoggingService.Error(e.Message);
            }
        }
        private void InsertFeatures(object param)
        {
            EditParameters editParameters = (EditParameters)param;
            if (editParameters == null)
            {
                return;
            }
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap = editParameters.geometryMap;
            if (geometryMap != null)
            {
                IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSourceByString(editParameters.connectionInfo);
                IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(editParameters.datasetName);
                int nTotalCount = editParameters.nTotalCount;
                int num = 0;
                //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._clearSelection);
                //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
                foreach (DF3DFeatureClass current in geometryMap.Keys)
                {
                    if (this._bgWorker.CancellationPending)
                    {
                        break;
                    }
                    IFeatureClass featureClass = featureDataSet.OpenFeatureClass(current.GetFeatureClass().Name);
                    IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                    IRowBufferCollection rowBufferCollection2 = geometryMap[current];
                    int num2 = 0;
                    while (num2 < rowBufferCollection2.Count && !this._bgWorker.CancellationPending)
                    {
                        this._manualResult.WaitOne();
                        IRowBuffer value = rowBufferCollection2.Get(num2);
                        rowBufferCollection.Add(value);
                        num++;
                        string userState = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                        int percentProgress = num * 100 / nTotalCount;
                        this._bgWorker.ReportProgress(percentProgress, userState);
                        num2++;
                    }
                    if (rowBufferCollection.Count > 0)
                    {
                        CommonUtils.Instance().FdeUndoRedoManager.InsertFeatures(featureClass, rowBufferCollection);
                        object[] args = new object[]
						{
							current, 
							rowBufferCollection2, 
							true, 
							false
						};
                        //asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._InsertSelection, args);
                        //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
                    }
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
            }
        }
    }
}
