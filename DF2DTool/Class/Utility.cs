using System.Collections;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace DF2DTool.Class
{
    public class Utility
    {
        #region//得到在pWorkspace中要素类的名称
        /// <summary>
        /// 得到在pWorkspace中要素类的名称
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="pMapControl"></param>
        /// <returns></returns>
        public static IFeatureClass getFeatureClass(IWorkspace pWorkspace, string strFeatureClassName)
        {
            ArrayList pArrayList = new ArrayList();

            IFeatureClass pFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(strFeatureClassName);

            return pFeatureClass;

        }
        #endregion

        #region//根据一个IGeometry对象选择一个指定图层上的要素，选择环境为类中使用的SelectOption
        /// <summary>
        /// 根据一个IGeometry对象选择一个指定图层上的要素，选择环境为类中使用的SelectOption
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pGeometry"></param>
        public static void FeatureLayerSelect(IFeatureLayer pFeaLay, IGeometry pGeometry)
        {
            if (pGeometry == null) return;
            double dis = 10;// PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, m_Option.Tolerate);
            IGeometry pBuff = PublicFunction.DoBuffer(pGeometry, dis);
            SpatialFilterClass spaFilter = new SpatialFilterClass();
            spaFilter.Geometry = pBuff;
            spaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;//交叉
            PublicFunction.FeatureLayerSelect(pFeaLay, spaFilter as ISpatialFilter, esriSelectionResultEnum.esriSelectionResultNew);
        }
        #endregion

        #region//得到一个SDE数据库中的所有版本名称
        /// <summary>
        /// 得到一个SDE数据库中的所有版本名称
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <returns></returns>
        public static string[] GetAllVersionName(IWorkspace pWorkspace)
        {
            string[] result = null;
            if (pWorkspace != null)
            {
                try
                {
                    ArrayList arr = new ArrayList();
                    IVersionedWorkspace pVerWs = pWorkspace as IVersionedWorkspace;
                    if (pVerWs == null) return null;
                    IEnumVersionInfo pEnumInfo = pVerWs.Versions;
                    pEnumInfo.Reset();
                    IVersionInfo pInfo = pEnumInfo.Next();
                    while (pInfo != null)
                    {
                        arr.Add(pInfo.VersionName.ToString());
                        pInfo = pEnumInfo.Next();
                    }
                    result = (string[])arr.ToArray(typeof(string));
                }
                catch (System.Exception ex)
                {
                	
                }
               
            }
            return result;
        }
        #endregion

        #region//获得个人地理数据库中要素类的名称
        /// 获得个人地理数据库中要素类的名称
        /// </summary>
        /// <param name="pAccessWorkSpace">个人地理数据库工作空间</param>
        /// <returns pArrayFeatureClassName>个人地理数据库中要素类的名称</returns>
        public static IArray GetFeactureClassName_From_AccessWorkSpace(IWorkspace pAccessWorkSpace)
        {
            IArray pArrayFeatureClassName = new ArrayClass();

            //遍历直接位于地理数据库下的要素类FeatureClass
            IEnumDatasetName enumDatasetName = ((IWorkspace)pAccessWorkSpace).get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                pArrayFeatureClassName.Add(datasetName.Name.ToString());
                datasetName = enumDatasetName.Next();//推进器
            }

            //遍历位于地理数据库数据集featuredataset下的要素类
            enumDatasetName = ((IWorkspace)pAccessWorkSpace).get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                IFeatureDatasetName featureDatasetName = (IFeatureDatasetName)datasetName;
                IEnumDatasetName enumDatasetNameFC = featureDatasetName.FeatureClassNames;
                IDatasetName datasetNameFC = enumDatasetNameFC.Next();
                while (datasetNameFC != null)
                {
                    pArrayFeatureClassName.Add(datasetNameFC.Name.ToString());
                    datasetNameFC = enumDatasetNameFC.Next();//推进器
                }
                datasetName = enumDatasetName.Next();
            }

            return pArrayFeatureClassName;
        }
        #endregion

        #region//创建地理要素
        /// <summary>
        /// 创建地理要素
        /// </summary>
        /// <param name="pGeom"></param>
        /// <param name="pMap"></param>
        /// <param name="pCurrentLayer"></param>
        public static void CreateFeature(IGeometry pGeom, IMap pMap, IFeatureClass pFeatureClass)
        {
            if (pGeom != null)
            {
                IDataset pDataset;
                IWorkspaceEdit pWorkspaceEdit;

                if (pFeatureClass == null) return;

                pDataset = (IDataset)pFeatureClass;

                pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                int index = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
                IGeometryDef pGD = pFeatureClass.Fields.get_Field(index).GeometryDef;
                if (pGD.HasZ)
                {
                    IZAware pZA = (IZAware)pGeom;
                    pZA.ZAware = true;
                    IZ pZ = pGeom as IZ;
                    double zmin = -1000, zmax = 1000;
                    if (pGD.SpatialReference.HasZPrecision())
                    {
                        pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                    }
                    if (pZ != null)
                    {
                        //pZ.SetConstantZ(zmin);
                        pZ.SetConstantZ(0);
                    }
                    else
                    {
                        IPoint p = (pGeom as IPoint);
                        if (p != null)
                        {
                            if (p.Z.ToString() == "非数字")
                            {
                                //p.Z = zmin;
                                p.Z = 0;
                            }
                        }
                    }
                }

                if (pGD.HasM)
                {
                    IMAware pMA = (IMAware)pGeom;
                    pMA.MAware = true;
                }

                if (!pWorkspaceEdit.IsBeingEdited())
                {
                    pWorkspaceEdit.StartEditing(true);
                    pWorkspaceEdit.StartEditOperation();
                }

                IFeature pFeature;
                pFeature = pFeatureClass.CreateFeature();

                try
                {
                    pFeature.Shape = pGeom;
                    pFeature.Store();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }

                if (pWorkspaceEdit.IsBeingEdited())
                {
                    pWorkspaceEdit.StopEditOperation();
                    pWorkspaceEdit.StopEditing(true);
                }

                pMap.ClearSelection();  //清除所有选择

            }

        }
        #endregion

        #region//文件备份
        /// <summary>
        /// 文件备份
        /// </summary>
        /// <param name="strSrcFilePath">原始文件路径</param>
        /// <returns>备份文件路径</returns>
        public static void FileCopy(string strSrcFilePath, string strCopyPath)
        {
            FileInfo fSrcFile = new FileInfo(strSrcFilePath);
            FileInfo fCopyFile = new FileInfo(strCopyPath);

            fSrcFile.CopyTo(strCopyPath);
        }
        #endregion


    }
}
