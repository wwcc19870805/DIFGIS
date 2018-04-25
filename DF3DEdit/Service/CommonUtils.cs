//===============================================================================================
//编辑项目变量
//===============================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeUndoRedo;
using ICSharpCode.Core;
using DF3DData.Class;
using DF3DEdit.Class;

namespace DF3DEdit.Service
{
    public class CommonUtils
    {
        private static CommonUtils _CommonUtils;
        public static CommonUtils Instance()
        {
            if (_CommonUtils == null)
            {
                _CommonUtils = new CommonUtils();
            }
            return _CommonUtils;
        }

        private DF3DFeatureClass _CurEditLayer;
        public DF3DFeatureClass CurEditLayer
        {
            get
            {
                return this._CurEditLayer;
            }
            set
            {
                this._CurEditLayer = value;
            }
        }

        private string _CurEditDatasetWkt;
        public string CurEditDatasetWkt
        {
            get
            {
                return _CurEditDatasetWkt;
            }
            set
            {
                _CurEditDatasetWkt = value;
            }
        }

        private Gvitech.CityMaker.FdeUndoRedo.CommandManager fdeUndoRedoManager;
        public Gvitech.CityMaker.FdeUndoRedo.CommandManager FdeUndoRedoManager
        {
            get
            {
                return this.fdeUndoRedoManager;
            }
        }

        private bool enableTemproalEdit;
        public bool EnableTemproalEdit
        {
            get
            {
                return this.enableTemproalEdit;
            }
            set
            {
                this.enableTemproalEdit = value;
            }
        }

        public bool IsServerClientType
        {
            get;
            set;
        }

        public void SetCurEditLayer(DF3DFeatureClass CurEditLayerInfo)
        {
            try
            {
                if (System.IO.Directory.Exists(System.Windows.Forms.Application.LocalUserAppDataPath))
                {
                    string[] files = System.IO.Directory.GetFiles(System.Windows.Forms.Application.LocalUserAppDataPath, "*.fdb");
                    string[] array = files;
                    for (int i = 0; i < array.Length; i++)
                    {
                        string path = array[i];
                        System.IO.File.Delete(path);
                    }
                }
            }
            catch (System.Exception e)
            {
                LoggingService.Error(e.Message);
            }
            if (this.fdeUndoRedoManager != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.fdeUndoRedoManager);
                this.fdeUndoRedoManager = null;
            }
            this._CurEditLayer = CurEditLayerInfo;
            if (this._CurEditLayer != null && this._CurEditLayer.GetFeatureClass().FeatureDataSet != null)
            {
                //this.propertyCanEdit = true;
                this._CurEditDatasetWkt = this._CurEditLayer.GetFeatureClass().FeatureDataSet.SpatialReference.AsWKT();
                string backupDSFile = System.Windows.Forms.Application.LocalUserAppDataPath + "//" + System.Guid.NewGuid().ToString() + ".fdb";
                try
                {
                    ICommandManagerFactory commandManagerFactory = new CommandManagerFactoryClass();
                    this.fdeUndoRedoManager = (commandManagerFactory.CreateCommandManager(this._CurEditLayer.GetFeatureClass().FeatureDataSet, backupDSFile) as Gvitech.CityMaker.FdeUndoRedo.CommandManager);
                }
                catch (System.SystemException e2)
                {
                    LoggingService.Error(e2.Message);
                }
            }
            //MainFrmService.UpdateMenu();
        }

        public IFeatureDataSet GetCurrentFeatureDataset()
        {
            if (this._CurEditLayer == null)
            {
                return null;
            }
            return this._CurEditLayer.GetFeatureClass().FeatureDataSet;
        }

        public bool CheckCurrentFeatureDatasetEncrypted()
        {
            try
            {
                IFeatureDataSet currentFeatureDataset = this.GetCurrentFeatureDataset();
                if (currentFeatureDataset != null)
                {
                    IResourceManager resourceManager = currentFeatureDataset as IResourceManager;
                    if (resourceManager == null)
                    {
                        bool result = false;
                        return result;
                    }
                    if (resourceManager.IsImageEncrypted || resourceManager.IsModelEncrypted)
                    {
                        bool result = true;
                        return result;
                    }
                }
            }
            catch (System.Exception)
            {
                bool result = false;
                return result;
            }
            return false;
        }

        public void Insert(DF3DFeatureClass fcInfo, IRowBufferCollection Rows, bool bClearAll, bool bSetEditorPosition)
        {
            SelectCollection.Instance().InsertSelection(fcInfo, Rows, bClearAll, bSetEditorPosition);
        }

        public void Delete(DF3DFeatureClass fcInfo, int[] OidArray)
        {
            SelectCollection.Instance().DeleteSelection(fcInfo, OidArray);
        }

        public void Update(DF3DFeatureClass fcInfo, IRowBufferCollection Rows)
        {
            SelectCollection.Instance().UpdateSelection(fcInfo, Rows);
        }

        //private bool propertyCanEdit;
        //public bool PropertyCanEdit
        //{
        //    get
        //    {
        //        bool flag = false;
        //        DF3DFeatureClass currentFeatureDatasetInfo = this._CurEditLayer;
        //        if (currentFeatureDatasetInfo != null)
        //        {
        //            flag = currentFeatureDatasetInfo.HasFdbCapModifyData;
        //        }
        //        this.propertyCanEdit &= flag;
        //        return this.propertyCanEdit;
        //    }
        //    set
        //    {
        //        this.propertyCanEdit = value;
        //    }
        //}
    }
}
