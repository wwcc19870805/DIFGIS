using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using DFWinForms.Class;

namespace DF2DTool.Class
{
    public class clsMapExport
    {
        public GisReadFromWeb gisReader;
        public CadWrite cadWriter;
        private IWorkspace pWorkspace;
        private IArray pLayerArr = new ArrayClass();

        public clsMapExport(IWorkspace m_Workspace, IMap pMap)
        {
            gisReader = new GisReadFromWeb();
            cadWriter = new CadWrite(true);
            pWorkspace = m_Workspace;
            this.AddLayerVisibleToIArray(pMap, ref pLayerArr);
        }

        public clsMapExport(IWorkspace m_Workspace, IMap pMap, string mapScale)
        {
            gisReader = new GisReadFromWeb();
            cadWriter = new CadWrite(true);
            pWorkspace = m_Workspace;
            ArrayList pFeaClassNameList = CommonFunction.GetFeactureClassName_From_AccessWorkSpace(pWorkspace, mapScale);
            for (int i = 0; i < pFeaClassNameList.Count; i++)
            {
                pLayerArr.Add(pFeaClassNameList[i]);
            }
        }

        /// <summary>
        /// 初始化读写类
        /// </summary>
        /// <returns></returns>
        public bool ExportInit()
        {
            try
            {
                bool initOK = false;
                gisReader.InputWorkspace = this.pWorkspace;
                gisReader.LayerArr = this.pLayerArr;
                gisReader.MdbFileName = Path.GetFullPath(Application.StartupPath + @"\..\Support\ConvertSymbol.mdb");
                gisReader.LayerTable = "CadLayerCompar";
                gisReader.SymbolTable = "CadCompar";
                gisReader.StrObjNum = "GEOOBJNUM";
                gisReader.StrAngle = "DIRCTION";
                gisReader.MapScale = 0.5;
                initOK = gisReader.GisReadInit();
                cadWriter.MapScale = 0.5;
                return initOK;
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return false;
            }
           
        }

        /// <summary>
        /// 开始导出
        /// </summary>
        public void ExportRun()
        {
            try
            {
                if (File.Exists(cadWriter.OutputFileName))
                {
                    File.Delete(cadWriter.OutputFileName);
                }
                else
                {
                    gisReader.Read_EntitiesFromWeb();
                    cadWriter.CurrentDs = gisReader.CurrentDs;
                    cadWriter.Process();
                   
                }
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return;
            }
           
        }

        /// <summary>
        /// 将一个地图、图层组中的所有可显示图层加到一个IList中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        private void AddLayerVisibleToIArray(object obj,ref IArray list)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    AddLayerVisibleToIArray(pMap.get_Layer(i), ref list);
                }
                
            }
            else if (obj is IGroupLayer)
            {
                if ((obj as IGroupLayer).Visible)
                {
                    ICompositeLayer comLayer = obj as ICompositeLayer;
                    for (int i = 0; i < comLayer.Count; i++)
                    {
                        AddLayerVisibleToIArray(comLayer.get_Layer(i), ref list);
                    }
                }
            }
            else if (obj is IFeatureLayer)
            {
                if (list != null && (obj as ILayer).Visible) list.Add(obj);
            }
        }
    }
}
