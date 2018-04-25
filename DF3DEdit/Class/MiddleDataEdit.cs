using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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
    public class MiddleDataEdit : AbstractBatcheEdit
    {
        private BackgroundWorker _bgWorker;
        private System.Threading.ManualResetEvent _manualResult;
        private ProgressDialog1 _progressDlg;
        private ClearSelectionHandle _clearSelection;
        private DeleteSelectionHandle _deleteSelection;
        private UpdateSelectionHandel _updateSelection;
        private InsertSelectionHandel _InsertSelection;
        //private NextPageHandel _NextPage;
        public override bool BeginEdit(bool bNeedTooltip)
        {
            return this.BeginEdit();
        }
        public override bool BeginEdit()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return false;
            this._clearSelection = new ClearSelectionHandle(SelectCollection.Instance().Clear);
            this._deleteSelection = new DeleteSelectionHandle(CommonUtils.Instance().Delete);
            this._updateSelection = new UpdateSelectionHandel(SelectCollection.Instance().UpdateSelection);
            this._InsertSelection = new InsertSelectionHandel(CommonUtils.Instance().Insert);
            //this._NextPage = new NextPageHandel(SelectCollection.Instance().NextResultSize);
            app.Current3DMapControl.PauseRendering(false);
            return true;
        }
        public override void DoWork(EditType editType, EditParameters parameters)
        {
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
            if (app != null && app.Current3DMapControl != null)
            {
                app.Current3DMapControl.FeatureManager.RefreshAll();
                app.Current3DMapControl.ResumeRendering();
            }
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
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            EditParameters editParameters = (EditParameters)param;
            if (editParameters == null)
            {
                return;
            }
            CommandManagerServices.Instance().StartCommand();
            FDECommand cmd = new FDECommand(false, true);
            int count = SelectCollection.Instance().GetCount(false);
            int num = 0;
            foreach (DF3DFeatureClass featureClassInfo in SelectCollection.Instance().FeatureClassInfoMap.Keys)
            {
                if (this._bgWorker.CancellationPending)
                {
                    break;
                }
                ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
                if (resultSetInfo != null)
                {
                    System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                    if (featureClassInfo.GetFeatureClass().HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                    {
                        IConnectionInfo connectionInfo = new ConnectionInfoClass();
                        connectionInfo.FromConnectionString(featureClassInfo.GetFeatureClass().DataSource.ConnectionInfo.ToConnectionString());
                        IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSource(connectionInfo);
                        IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(CommonUtils.Instance().GetCurrentFeatureDataset().Name);
                        IFeatureClass featureClass = featureDataSet.OpenFeatureClass(featureClassInfo.GetFeatureClass().Name);
                        foreach (DataRow dataRow in resultSetInfo.ResultSetTable.Rows)
                        {
                            int item = int.Parse(dataRow[featureClass.FidFieldName].ToString());
                            list.Add(item);
                        }
                        TemporalFilter temporalFilter = new TemporalFilterClass();
                        temporalFilter.AddSubField(featureClass.FidFieldName);
                        temporalFilter.IdsFilter = list.ToArray();
                        ITemporalManager temporalManager = featureClass.TemporalManager;
                        ITemporalCursor temporalCursor = temporalManager.Search(temporalFilter);
                        while (temporalCursor.MoveNext())
                        {
                            this._manualResult.WaitOne();
                            System.Threading.Thread.Sleep(1);
                            temporalCursor.Dead(editParameters.TemproalTime);
                            num++;
                            string userState = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, count);
                            int percentProgress = num * 100 / count;
                            this._bgWorker.ReportProgress(percentProgress, userState);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                    }
                    else
                    {
                        IFeatureClass featureClass2 = featureClassInfo.GetFeatureClass();
                        foreach (DataRow dataRow2 in resultSetInfo.ResultSetTable.Rows)
                        {
                            if (this._bgWorker.CancellationPending)
                            {
                                break;
                            }
                            this._manualResult.WaitOne();
                            System.Threading.Thread.Sleep(1);
                            int item2 = int.Parse(dataRow2[featureClass2.FidFieldName].ToString());
                            list.Add(item2);
                            num++;
                            string userState2 = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, count);
                            int percentProgress2 = num * 100 / count;
                            this._bgWorker.ReportProgress(percentProgress2, userState2);
                        }
                        if (list.Count > 0)
                        {
                            CommonUtils.Instance().FdeUndoRedoManager.DeleteFeatures(featureClass2, list.ToArray());
                            list.Clear();
                        }
                    }
                }
            }
            CommandManagerServices.Instance().CallCommand(cmd);
            app.Workbench.UpdateMenu();
            //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._clearSelection);
            //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
        }
        private void DeleteFeatures(object param)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            EditParameters editParameters = (EditParameters)param;
            string featureClassGuid = editParameters.featureClassGuid;
            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(featureClassGuid);
            string fcName = editParameters.fcName;
            if (string.IsNullOrEmpty(fcName) || featureClassInfo == null)
            {
                return;
            }
            int nTotalCount = editParameters.nTotalCount;
            int num = 0;
            CommandManagerServices.Instance().StartCommand();
            object[] args = null;
            if (featureClassInfo.GetFeatureClass().HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
            {
                IConnectionInfo connectionInfo = new ConnectionInfoClass();
                connectionInfo.FromConnectionString(featureClassInfo.GetFeatureClass().DataSource.ConnectionInfo.ToConnectionString());
                IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSource(connectionInfo);
                IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(CommonUtils.Instance().GetCurrentFeatureDataset().Name);
                IFeatureClass featureClass = featureDataSet.OpenFeatureClass(featureClassInfo.GetFeatureClass().Name);
                System.DateTime temproalTime = editParameters.TemproalTime;
                TemporalFilter temporalFilter = new TemporalFilterClass();
                temporalFilter.AddSubField(featureClass.FidFieldName);
                temporalFilter.IdsFilter = editParameters.fidList;
                ITemporalManager temporalManager = featureClass.TemporalManager;
                ITemporalCursor temporalCursor = temporalManager.Search(temporalFilter);
                while (temporalCursor.MoveNext())
                {
                    this._manualResult.WaitOne();
                    System.Threading.Thread.Sleep(1);
                    num++;
                    string userState = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, nTotalCount);
                    int percentProgress = num * 100 / nTotalCount;
                    this._bgWorker.ReportProgress(percentProgress, userState);
                    temporalCursor.Dead(temproalTime);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                args = new object[]
				{
					featureClassInfo, 
					editParameters.fidList
				};
            }
            else
            {
                IFeatureClass featureClass2 = featureClassInfo.GetFeatureClass();
                FDECommand cmd = new FDECommand(false, true);
                System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
                int[] fidList = editParameters.fidList;
                int i = 0;
                while (i < fidList.Length)
                {
                    int item = fidList[i];
                    if (this._bgWorker.CancellationPending)
                    {
                        if (list.Count > 0)
                        {
                            CommonUtils.Instance().Delete(featureClassInfo, list.ToArray());
                            list.Clear();
                            break;
                        }
                        break;
                    }
                    else
                    {
                        this._manualResult.WaitOne();
                        System.Threading.Thread.Sleep(1);
                        num++;
                        string userState2 = string.Format(StringParser.Parse("${res:feature_progress_delete}"), num, nTotalCount);
                        int percentProgress2 = num * 100 / nTotalCount;
                        this._bgWorker.ReportProgress(percentProgress2, userState2);
                        list2.Add(item);
                        list.Add(item);
                        i++;
                    }
                }
                if (list.Count > 0)
                {
                    CommonUtils.Instance().FdeUndoRedoManager.DeleteFeatures(featureClass2, list.ToArray());
                    list.Clear();
                    CommandManagerServices.Instance().CallCommand(cmd);
                    app.Workbench.UpdateMenu();
                }
                args = new object[]
				{
					featureClassInfo, 
					list2.ToArray()
				};
            }
            //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._deleteSelection, args);
            //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
        }
        private void UpdateAttribute(object param)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            EditParameters editParameters = (EditParameters)param;
            if (editParameters == null)
            {
                return;
            }
            string featureClassGuid = editParameters.featureClassGuid;
            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(featureClassGuid);
            if (featureClassInfo == null)
            {
                return;
            }
            CommandManagerServices.Instance().StartCommand();
            FDECommand cmd = new FDECommand(false, true);
            int nTotalCount = editParameters.nTotalCount;
            int num = 0;
            if (featureClassInfo.GetFeatureClass().HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
            {
                IConnectionInfo connectionInfo = new ConnectionInfoClass();
                connectionInfo.FromConnectionString(featureClassInfo.GetFeatureClass().DataSource.ConnectionInfo.ToConnectionString());
                IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSource(connectionInfo);
                IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(CommonUtils.Instance().GetCurrentFeatureDataset().Name);
                IFeatureClass featureClass = featureDataSet.OpenFeatureClass(featureClassInfo.GetFeatureClass().Name);
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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
            }
            else
            {
                IFeatureClass featureClass2 = featureClassInfo.GetFeatureClass();
                System.Collections.Generic.Dictionary<int, string> dictionary = new System.Collections.Generic.Dictionary<int, string>();
                IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                IRowBufferCollection rowBufferCollection2 = new RowBufferCollectionClass();
                for (int i = 0; i < editParameters.fidList.Length; i++)
                {
                    if (this._bgWorker.CancellationPending)
                    {
                        CommonUtils.Instance().FdeUndoRedoManager.UpdateFeatures(featureClass2, rowBufferCollection2);
                        break;
                    }
                    this._manualResult.WaitOne();
                    System.Threading.Thread.Sleep(1);
                    int num2 = editParameters.fidList[i];
                    IRowBuffer row2 = featureClass2.GetRow(num2);
                    if (row2 != null)
                    {
                        string value = base.UpdateRowBuffer(ref row2, editParameters.colName, editParameters.regexDataList);
                        rowBufferCollection.Add(row2);
                        rowBufferCollection2.Add(row2);
                        dictionary[num2] = value;
                        num++;
                        string userState2 = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                        int percentProgress2 = num * 100 / nTotalCount;
                        this._bgWorker.ReportProgress(percentProgress2, userState2);
                    }
                }
                if (dictionary.Count > 0)
                {
                    CommonUtils.Instance().FdeUndoRedoManager.UpdateFeatures(featureClass2, rowBufferCollection2);
                    object[] args = new object[]
					{
						featureClassInfo, 
						editParameters.colName, 
						dictionary
					};
                    //System.IAsyncResult asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._updateSelection, args);
                    //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
                }
            }
            CommandManagerServices.Instance().CallCommand(cmd);
            app.Workbench.UpdateMenu();
        }
        private void UpdateGeometry(object param)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                EditParameters editParameters = (EditParameters)param;
                if (editParameters != null)
                {
                    System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap = editParameters.geometryMap;
                    if (geometryMap != null)
                    {
                        CommandManagerServices.Instance().StartCommand();
                        FDECommand cmd = new FDECommand(false, true);
                        int nTotalCount = editParameters.nTotalCount;
                        int num = 0;
                        foreach (DF3DFeatureClass current in geometryMap.Keys)
                        {
                            if (this._bgWorker.CancellationPending)
                            {
                                break;
                            }
                            IRowBufferCollection rowBufferCollection = geometryMap[current];
                            if (current.GetFeatureClass().HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                            {
                                IConnectionInfo connectionInfo = new ConnectionInfoClass();
                                connectionInfo.FromConnectionString(current.GetFeatureClass().DataSource.ConnectionInfo.ToConnectionString());
                                IDataSource dataSource = ((IDataSourceFactory)new DataSourceFactoryClass()).OpenDataSource(connectionInfo);
                                IFeatureDataSet featureDataSet = dataSource.OpenFeatureDataset(CommonUtils.Instance().GetCurrentFeatureDataset().Name);
                                IFeatureClass featureClass = featureDataSet.OpenFeatureClass(current.GetFeatureClass().Name);
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
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(featureDataSet);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(dataSource);
                            }
                            else
                            {
                                IFeatureClass featureClass2 = current.GetFeatureClass();
                                int num2 = 0;
                                while (num2 < rowBufferCollection.Count && !this._bgWorker.CancellationPending)
                                {
                                    this._manualResult.WaitOne();
                                    System.Threading.Thread.Sleep(1);
                                    num++;
                                    string userState2 = string.Format(StringParser.Parse("${res:feature_progress_finished}"), num, nTotalCount);
                                    int percentProgress2 = num * 100 / nTotalCount;
                                    this._bgWorker.ReportProgress(percentProgress2, userState2);
                                    num2++;
                                }
                                CommonUtils.Instance().FdeUndoRedoManager.UpdateFeatures(featureClass2, rowBufferCollection);
                            }
                        }
                        CommandManagerServices.Instance().CallCommand(cmd);
                        app.Workbench.UpdateMenu();
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
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            EditParameters editParameters = (EditParameters)param;
            if (editParameters == null)
            {
                return;
            }
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap = editParameters.geometryMap;
            if (geometryMap != null)
            {
                CommandManagerServices.Instance().StartCommand();
                FDECommand fDECommand = new FDECommand(true, true);
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
                    IFeatureClass featureClass = current.GetFeatureClass();
                    IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
                    IRowBufferCollection rowBufferCollection2 = geometryMap[current];
                    int num2 = 0;
                    while (num2 < rowBufferCollection2.Count && !this._bgWorker.CancellationPending)
                    {
                        this._manualResult.WaitOne();
                        System.Threading.Thread.Sleep(1);
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
                        //object[] args = new object[]
                        //{
                        //    current, 
                        //    rowBufferCollection2, 
                        //    true, 
                        //    false
                        //};
                        //asyncResult = MainFrmService.ResultSetPanel.BeginInvoke(this._InsertSelection, args);
                        //MainFrmService.ResultSetPanel.EndInvoke(asyncResult);
                        rowBufferCollection.Clear();
                    }
                }
                fDECommand.SetSelectionMap();
                CommandManagerServices.Instance().CallCommand(fDECommand);
                app.Workbench.UpdateMenu();
            }
        }
    }
}
