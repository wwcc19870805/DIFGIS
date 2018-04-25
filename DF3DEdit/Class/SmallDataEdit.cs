using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using ICSharpCode.Core;
using DF3DControl.Base;
using DF3DData.Class;
using DF3DEdit.Service;

namespace DF3DEdit.Class
{
    public class SmallDataEdit : AbstractBatcheEdit
    {
        public override bool BeginEdit(bool bNeedTooltip)
        {
            return true;
        }
        public override bool BeginEdit()
        {
            return true;
        }
        public override void DoWork(EditType editType, EditParameters parameters)
        {
            switch (editType)
            {
                case EditType.ET_DELETE_SELCTION:
                    {
                        this.DeleteSelection(parameters);
                        return;
                    }
                case EditType.ET_DELETE_RECYCLE:
                case EditType.ET_UPDATE_MERGE_SELECTION:
                    {
                        break;
                    }
                case EditType.ET_DELETE_FEATURES:
                    {
                        this.DeleteFeatures(parameters);
                        return;
                    }
                case EditType.ET_UPDATE_ATTRIBUTE:
                    {
                        this.UpdateAttribute(parameters);
                        return;
                    }
                case EditType.ET_UPDATE_GEOMETRY:
                    {
                        this.UpdateGeometry(parameters);
                        return;
                    }
                case EditType.ET_INSERT_FEATURES:
                    {
                        this.InsertFeatures(parameters);
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        public override void EndEdit()
        {
        }
        private void DeleteSelection(EditParameters parameters)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            CommandManagerServices.Instance().StartCommand();
            FDECommand cmd = new FDECommand(false, true);
            foreach (DF3DFeatureClass featureClassInfo in SelectCollection.Instance().FeatureClassInfoMap.Keys)
            {
                ResultSetInfo resultSetInfo = SelectCollection.Instance().FeatureClassInfoMap[featureClassInfo] as ResultSetInfo;
                if (resultSetInfo != null)
                {
                    IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
                    int[] array = new int[resultSetInfo.ResultSetTable.Rows.Count];
                    int num = 0;
                    foreach (DataRow dataRow in resultSetInfo.ResultSetTable.Rows)
                    {
                        int num2 = int.Parse(dataRow[featureClass.FidFieldName].ToString());
                        array[num++] = num2;
                    }
                    if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
                    {
                        TemporalFilter temporalFilter = new TemporalFilterClass();
                        temporalFilter.AddSubField(featureClass.FidFieldName);
                        temporalFilter.IdsFilter = array;
                        ITemporalManager temporalManager = featureClass.TemporalManager;
                        ITemporalCursor temporalCursor = temporalManager.Search(temporalFilter);
                        while (temporalCursor.MoveNext())
                        {
                            temporalCursor.Dead(parameters.TemproalTime);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                        app.Current3DMapControl.FeatureManager.RefreshFeatureClass(featureClass);
                    }
                    else
                    {
                        CommonUtils.Instance().FdeUndoRedoManager.DeleteFeatures(featureClass, array);
                        app.Current3DMapControl.FeatureManager.DeleteFeatures(featureClass, array);
                    }
                }
            }
            SelectCollection.Instance().Clear();
            CommandManagerServices.Instance().CallCommand(cmd);
            app.Workbench.UpdateMenu();
        }
        private void DeleteFeatures(EditParameters parameters)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            string featureClassGuid = parameters.featureClassGuid;
            if (string.IsNullOrEmpty(featureClassGuid))
            {
                return;
            }
            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(featureClassGuid);
            if (featureClassInfo == null)
            {
                return;
            }
            FDECommand cmd = new FDECommand(false, true);
            CommandManagerServices.Instance().StartCommand();
            IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
            if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
            {
                System.DateTime temproalTime = parameters.TemproalTime;
                TemporalFilter temporalFilter = new TemporalFilterClass();
                temporalFilter.AddSubField(featureClass.FidFieldName);
                temporalFilter.IdsFilter = parameters.fidList;
                ITemporalManager temporalManager = featureClass.TemporalManager;
                ITemporalCursor temporalCursor = temporalManager.Search(temporalFilter);
                while (temporalCursor.MoveNext())
                {
                    temporalCursor.Dead(temproalTime);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                app.Current3DMapControl.FeatureManager.RefreshFeatureClass(featureClass);
            }
            else
            {
                CommonUtils.Instance().FdeUndoRedoManager.DeleteFeatures(featureClass, parameters.fidList);
                app.Current3DMapControl.FeatureManager.DeleteFeatures(featureClass, parameters.fidList);
            }
            CommonUtils.Instance().Delete(featureClassInfo, parameters.fidList);
            CommandManagerServices.Instance().CallCommand(cmd);
            app.Workbench.UpdateMenu();
        }
        private void UpdateAttribute(EditParameters paramter)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            string featureClassGuid = paramter.featureClassGuid;
            if (string.IsNullOrEmpty(featureClassGuid))
            {
                return;
            }
            DF3DFeatureClass featureClassInfo = DF3DFeatureClassManager.Instance.GetFeatureClassByID(featureClassGuid);
            if (featureClassInfo == null)
            {
                return;
            }
            IFeatureClass featureClass = featureClassInfo.GetFeatureClass();
            IRowBufferCollection rowBufferCollection = new RowBufferCollectionClass();
            if (featureClass.HasTemporal() && CommonUtils.Instance().EnableTemproalEdit)
            {
                ITemporalManager temporalManager = featureClass.TemporalManager;
                ITemporalCursor temporalCursor = temporalManager.Search(new TemporalFilterClass
                {
                    IdsFilter = paramter.fidList
                });
                while (temporalCursor.MoveNext())
                {
                    bool flag = false;
                    int currentId = temporalCursor.CurrentId;
                    IRowBuffer row = featureClass.GetRow(currentId);
                    base.UpdateRowBuffer(ref row, paramter.colName, paramter.regexDataList);
                    rowBufferCollection.Add(row);
                    ITemporalInstanceCursor temporalInstances = temporalCursor.GetTemporalInstances(false);
                    TemporalInstance temporalInstance;
                    while ((temporalInstance = temporalInstances.NextInstance()) != null)
                    {
                        if (temporalInstance.StartDatetime == paramter.TemproalTime)
                        {
                            flag = true;
                            temporalInstances.Update(row);
                            break;
                        }
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalInstances);
                    if (!flag)
                    {
                        temporalCursor.Insert(paramter.TemproalTime, row);
                    }
                }
                app.Current3DMapControl.FeatureManager.RefreshFeatureClass(featureClass);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
            }
            else
            {
                CommandManagerServices.Instance().StartCommand();
                FDECommand cmd = new FDECommand(false, true);
                for (int i = 0; i < paramter.fidList.Length; i++)
                {
                    int id = paramter.fidList[i];
                    IRowBuffer row2 = featureClass.GetRow(id);
                    if (row2 != null)
                    {
                        base.UpdateRowBuffer(ref row2, paramter.colName, paramter.regexDataList);
                        rowBufferCollection.Add(row2);
                    }
                }
                CommonUtils.Instance().FdeUndoRedoManager.UpdateFeatures(featureClass, rowBufferCollection);
                app.Current3DMapControl.FeatureManager.EditFeatures(featureClass, rowBufferCollection);
                CommandManagerServices.Instance().CallCommand(cmd);
                app.Workbench.UpdateMenu();
            }
            CommonUtils.Instance().Update(featureClassInfo, rowBufferCollection);
        }
        private void UpdateGeometry(EditParameters parameter)
        {
            try
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                if (parameter != null)
                {
                    System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap = parameter.geometryMap;
                    if (geometryMap != null)
                    {
                        CommandManagerServices.Instance().StartCommand();
                        FDECommand cmd = new FDECommand(false, true);
                        foreach (DF3DFeatureClass current in geometryMap.Keys)
                        {
                            IRowBufferCollection rowBufferCollection = geometryMap[current];
                            IFeatureClass featureClass = current.GetFeatureClass();
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
                                while (temporalCursor.MoveNext())
                                {
                                    bool flag = false;
                                    int currentId = temporalCursor.CurrentId;
                                    ITemporalInstanceCursor temporalInstances = temporalCursor.GetTemporalInstances(false);
                                    TemporalInstance temporalInstance;
                                    while ((temporalInstance = temporalInstances.NextInstance()) != null)
                                    {
                                        if (temporalInstance.StartDatetime == parameter.TemproalTime)
                                        {
                                            flag = true;
                                            temporalInstances.Update(dictionary[currentId]);
                                            break;
                                        }
                                    }
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalInstances);
                                    if (!flag)
                                    {
                                        temporalCursor.Insert(parameter.TemproalTime, dictionary[currentId]);
                                    }
                                }
                                app.Current3DMapControl.FeatureManager.RefreshFeatureClass(featureClass);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalCursor);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(temporalManager);
                            }
                            else
                            {
                                CommonUtils.Instance().FdeUndoRedoManager.UpdateFeatures(featureClass, rowBufferCollection);
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
        private void InsertFeatures(EditParameters parameter)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (parameter == null)
            {
                return;
            }
            System.Collections.Generic.Dictionary<DF3DFeatureClass, IRowBufferCollection> geometryMap = parameter.geometryMap;
            if (geometryMap != null)
            {
                CommandManagerServices.Instance().StartCommand();
                FDECommand fDECommand = new FDECommand(true, true);
                SelectCollection.Instance().Clear();
                foreach (DF3DFeatureClass current in geometryMap.Keys)
                {
                    IFeatureClass featureClass = current.GetFeatureClass();
                    IRowBufferCollection rowBufferCollection = geometryMap[current];
                    CommonUtils.Instance().FdeUndoRedoManager.InsertFeatures(featureClass, rowBufferCollection);
                    CommonUtils.Instance().Insert(current, rowBufferCollection, true, false);
                    app.Current3DMapControl.FeatureManager.CreateFeatures(featureClass, rowBufferCollection);
                }
                fDECommand.SetSelectionMap();
                CommandManagerServices.Instance().CallCommand(fDECommand);
                app.Workbench.UpdateMenu();
            }
        }
    }
}
