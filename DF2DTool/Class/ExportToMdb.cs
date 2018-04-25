using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DFWinForms.Class;

namespace DF2DTool.Class
{
    class ExportToMdb
    {
        #region 对外的属性
        /// <summary>
        /// 源数据库
        /// </summary>
        private IWorkspace m_pOrigWorkspace;
        public IWorkspace OrigWorkspace
        {
            set
            {
                m_pOrigWorkspace = value;
            }
        }

        /// <summary>
        /// 目标数据库
        /// </summary>
        private IWorkspace m_pDestWorkspace;
        private string m_strDestWspName;
        public string strDestWorkspaceName
        {
            set
            {
                m_strDestWspName = value;

                IWorkspaceFactory pWspFactory = new AccessWorkspaceFactoryClass();
                string strPath, strName;
                try
                {
                    strPath = m_strDestWspName.Substring(0, m_strDestWspName.LastIndexOf("\\"));
                    strName = m_strDestWspName.Substring(m_strDestWspName.LastIndexOf("\\") + 1);
                    pWspFactory.Create(strPath, strName, null, 0);
                    m_pDestWorkspace = pWspFactory.OpenFromFile(m_strDestWspName, 0);
                }
                catch (System.Exception ex)
                {
                	
                }
               
            }
        }

        /// <summary>
        /// 导出方式，1--从选择集导出 2--从文件导出
        /// </summary>
        private string m_operType = "1";
        public string OperType
        {
            set
            {
                m_operType = value;
            }
        }

        /// <summary>
        /// 导出的DatasetNames
        /// </summary>
        private DataSetNames m_DsNames = null;
        public DataSetNames DsNames
        {
            set
            {
                m_DsNames = value;
            }
        }

        private ISelection m_pSelection = null;
        public ISelection Selection
        {
            set
            {
                m_pSelection = value;
            }
        }

        #endregion

        #region 以原工作空间为模板创建新的工作空间
        /// <summary>
        /// 以原工作空间为模板创建新的工作空间
        /// </summary>
        private void CreatWorkspaceFromOrig()
        {
            WaitForm.SetCaption("正在创建新的工作空间...请稍后");
            IFeatureClassContainer pFcContainer = null;
            IFeatureClass pFcTemp = null;
            IFeatureClass pNewFc = null;
            ISpatialReference pSr = null;
            ISpatialReference pSrTemp = null;
            IFields pflds = null;
            double dblXmin, dblXmax, dblYmin, dblYmax;
            double dblZmin, dblZmax, dblMmin, dblMmax;
            CreateWorkspaceDomains(m_pOrigWorkspace, m_pDestWorkspace);
            IFeatureWorkspace pFeaWorkspace = m_pDestWorkspace as IFeatureWorkspace;
            IEnumDataset enumDs = m_pOrigWorkspace.get_Datasets(esriDatasetType.esriDTAny);
            IDataset pDs = enumDs.Next();

            while (pDs != null)
            {
                if (pDs.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    pSr = new UnknownCoordinateSystemClass();
                    pSrTemp = (pDs as IGeoDataset).SpatialReference;
                    if (pSrTemp.HasXYPrecision())
                    {
                        pSrTemp.GetDomain(out dblXmin, out dblXmax, out dblYmin, out dblYmax);
                        pSr.SetDomain(dblXmin, dblXmax, dblYmin, dblYmax);
                    }
                    if (pSrTemp.HasZPrecision())
                    {
                        pSrTemp.GetZDomain(out dblZmin, out dblZmax);
                        pSr.SetZDomain(dblZmin, dblZmax);

                    }
                    if (pSrTemp.HasMPrecision())
                    {
                        pSrTemp.GetMDomain(out dblMmin, out dblMmax);
                        pSr.SetMDomain(dblMmin, dblMmax);
                    }
                    IFeatureDataset pFeaDs = pFeaWorkspace.CreateFeatureDataset(pDs.Name, pSr);
                    pFcContainer = (IFeatureClassContainer)pDs;
                    if (pFcContainer.ClassCount > 0)
                    {
                        for (int i = 0; i < pFcContainer.ClassCount; i++)
                        {
                            try
                            {
                                pFcTemp = pFcContainer.get_Class(i);
                                pflds = CommonFunction.GetFieldsFormFeatureClass(pFcTemp);

                                //若为注记
                                if (pFcTemp.FeatureType == esriFeatureType.esriFTAnnotation)
                                {
                                    IFeatureWorkspaceAnno pFWSAno = pFeaWorkspace as IFeatureWorkspaceAnno;
                                    IAnnoClass pAnnoClass = pFcTemp.Extension as IAnnoClass;
                                    IGraphicsLayerScale pGLS = new GraphicsLayerScaleClass();
                                    pGLS.ReferenceScale = pAnnoClass.ReferenceScale;
                                    pGLS.Units = pAnnoClass.ReferenceScaleUnits;

                               
                                        pNewFc = pFWSAno.CreateAnnotationClass((pFcTemp as IDataset).Name, 
                                            pflds, pFcTemp.CLSID, pFcTemp.EXTCLSID, pFcTemp.ShapeFieldName,
                                            "", pFeaDs, null, pAnnoClass.AnnoProperties, pGLS, pAnnoClass.SymbolCollection, true);
                                        (pNewFc as IClassSchemaEdit).AlterAliasName(pFcTemp.AliasName);

                                }
                                else//若为地理要素
                                {
                                    try
                                    {
                                        pNewFc = pFeaDs.CreateFeatureClass((pFcTemp as IDataset).Name,
                                            pflds, pFcTemp.CLSID, pFcTemp.EXTCLSID, pFcTemp.FeatureType, pFcTemp.ShapeFieldName, null);
                                        if (pFcTemp.AliasName == "图廓线")
                                        {
                                            int n = 0;
                                        }
                                        (pNewFc as IClassSchemaEdit).AlterAliasName(pFcTemp.AliasName);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        System.Console.WriteLine(ex.Message);
                                    }
                                }
                               
                            }
                            catch (System.Exception ex)
                            {
                            	 System.Console.WriteLine(ex.Message);
                            }
                           

                            
                           
                        }
                    }
                }
                else if (pDs.Type == esriDatasetType.esriDTFeatureClass)
                {
                    pFcTemp = (IFeatureClass)pDs;
                    pflds = CommonFunction.GetFieldsFormFeatureClass(pFcTemp, pSr);
                    try
                    {
                        pNewFc = pFeaWorkspace.CreateFeatureClass(pDs.Name,
                                                                   pflds,
                                                                   pFcTemp.CLSID,
                                                                   pFcTemp.EXTCLSID,
                                                                   pFcTemp.FeatureType,
                                                                   pFcTemp.ShapeFieldName,
                                                                   null);
                    }
                    catch(Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                else if (pDs.Type == esriDatasetType.esriDTTable)
                {
                    ITable pTable = (ITable)pDs;
                    try
                    {
                        ITable pNewTable = pFeaWorkspace.CreateTable(pDs.Name,
                                                                      pTable.Fields,
                                                                      pTable.CLSID,
                                                                      pTable.EXTCLSID,
                                                                      null);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                pDs = enumDs.Next();
            }


        }
        #endregion

        #region 数据导出到新工作空间(mdb)
        public void DataExportToMdb()
        {
            WaitForm.Start("正在准备导出...","请稍后");
            CreatWorkspaceFromOrig();

            if (m_operType == "1")
            {
                ExportSelection();
            }
            else if (m_operType == "2")
            {
                ExportLayers();
            }
            WaitForm.Stop();
        }

        private void ExportLayers()
        {
            WaitForm.SetCaption("正在导出图层...请稍后");
            IName pDsName = null;
            IFeatureClass pOrigFc = null;
            IFeatureClass pDestFc = null;

            for (int i = 0; i < m_DsNames.Count; i++)
            {
                pDsName = (IName)m_DsNames.get_Item(i);
                pOrigFc = (IFeatureClass)pDsName.Open();

                try
                {
                    pDestFc = (m_pDestWorkspace as IFeatureWorkspace).OpenFeatureClass(m_DsNames.get_Item(i).Name);
                    LoadFeatures(pDestFc, pOrigFc);
                    //this.WriteMdbEvent(i + 1, (int)m_DsNames.Count);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        private void ExportSelection()
        {
            WaitForm.SetCaption("正在导出选择集...请稍后");
            int iCount = 0;
            int iAll = 0;
            IFeatureClass pOrigFc = null;
            IFeatureClass pDestFc = null;
            IFeatureCursor featureCursorInsert = null;
            IFeatureBuffer featureBufferInsert = null;

            IEnumFeature pEnumFea = (IEnumFeature)m_pSelection;
            IFeature feature = pEnumFea.Next();
            while (feature != null)
            {
                iAll++;
                feature = pEnumFea.Next();
            }
            pEnumFea.Reset();
            feature = pEnumFea.Next();
            while (feature != null)
            {
                pOrigFc = (IFeatureClass)feature.Class;
                try
                {
                    pDestFc = ((IFeatureWorkspace)m_pDestWorkspace).OpenFeatureClass(((IDataset)pOrigFc).Name);
                    featureCursorInsert = pDestFc.Insert(true);
                    featureBufferInsert = pDestFc.CreateFeatureBuffer();
                    try
                    {
                        featureBufferInsert.Shape = feature.Shape;
                    }
                    catch
                    {

                        try
                        {
                            if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon || feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                            {
                                IPointCollection pPointC;
                                IPoint pPoint;
                                pPointC = (IPointCollection)feature.Shape;
                                for (int i = 0; i < pPointC.PointCount; i++)
                                {
                                    pPoint = pPointC.get_Point(i);
                                    pPoint.M = 0;
                                    pPointC.UpdatePoint(i, pPoint);
                                }
                                feature.Shape = (IGeometry)pPointC;
                                featureBufferInsert.Shape = feature.Shape;
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            //string strFeatureID = feature.OID.ToString();
                            //pArrErrorFeatureClass.Add(featureClassOut.AliasName);
                            //pArrErrorFeature.Add(strFeatureID);
                            //pArrErrorMdb.Add(strMergedb);

                            ////指向下一个要素
                            //feature = featureCursorSearch.NextFeature();
                            //continue;
                        }

                    }

                    AddFields(featureBufferInsert, feature);
                    featureCursorInsert.InsertFeature(featureBufferInsert);
                    //LoadFeatures(pDestFc, pOrigFc);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                feature = pEnumFea.Next();
                iCount++;
                //this.WriteMdbEvent(iCount, iAll);
            }
        }
        #endregion

        #region//使用insert cursor插入要素
        /// <summary>
        /// 使用insert cursor插入要素
        /// </summary>
        /// <param name="stringFeatureClassOut">目标数据库的要素类</param>
        /// <param name="stringFeatureClassIn">待合并数据库的要素类</param>
        private void LoadFeatures(IFeatureClass featureClassOut, IFeatureClass featureClassIn)
        {
            int intFeatureCount = 0;

            IFeatureCursor featureCursorInsert = featureClassOut.Insert(true);
            IFeatureBuffer featureBufferInsert = featureClassOut.CreateFeatureBuffer();

            // 遍历待合并数据库要素类中的所有要素
            IFeatureCursor featureCursorSearch = featureClassIn.Search(null, true);
            IFeature feature = featureCursorSearch.NextFeature();
            while (feature != null)
            {
                try
                {
                    featureBufferInsert.Shape = feature.Shape;
                }
                catch
                {

                    try
                    {
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon || feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            IPointCollection pPointC;
                            IPoint pPoint;
                            pPointC = (IPointCollection)feature.Shape;
                            for (int i = 0; i < pPointC.PointCount; i++)
                            {
                                pPoint = pPointC.get_Point(i);
                                pPoint.M = 0;
                                pPointC.UpdatePoint(i, pPoint);
                            }
                            feature.Shape = (IGeometry)pPointC;
                            featureBufferInsert.Shape = feature.Shape;
                        }
                    }
                    catch
                    {
                        //string strFeatureID = feature.OID.ToString();
                        //pArrErrorFeatureClass.Add(featureClassOut.AliasName);
                        //pArrErrorFeature.Add(strFeatureID);
                        //pArrErrorMdb.Add(strMergedb);

                        ////指向下一个要素
                        //feature = featureCursorSearch.NextFeature();
                        //continue;
                    }

                }

                AddFields(featureBufferInsert, feature);

                featureCursorInsert.InsertFeature(featureBufferInsert);

                // 每添加100个要素刷新一次feature Cursor
                if (++intFeatureCount == 100)
                {
                    featureCursorInsert.Flush();
                    intFeatureCount = 0;
                }

                // 指向下一个要素
                feature = featureCursorSearch.NextFeature();
            }

            // 刷新feature Cursor
            featureCursorInsert.Flush();
        }
        #endregion

        #region//为要素添填充字段
        /// <summary>
        /// 为要素添填充属性
        /// </summary>
        /// <param name="featureBuffer">要素缓存</param>
        /// <param name="feature">待填充的要素</param>
        private static void AddFields(IFeatureBuffer featureBuffer, IFeature feature)
        {
            IRowBuffer rowBuffer = (IRowBuffer)featureBuffer;
            IFields fieldsNew = rowBuffer.Fields;

            int i;
            int intFieldIndex;
            IFields fields = feature.Fields;
            IField field;

            for (i = 0; i < fieldsNew.FieldCount; i++)
            {
                field = fieldsNew.get_Field(i);
                //				if (field.Editable == true && (field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeOID) 
                //					&& (field.Name != "SHAPE_Length") && (field.Name != "SHAPE_Area"))
                if (field.Editable == true)
                {
                    intFieldIndex = feature.Fields.FindField(field.Name);
                    if (intFieldIndex != -1)
                    {
                        featureBuffer.set_Value(i, feature.get_Value(intFieldIndex));
                    }
                }

            }
        }
        #endregion

         # region 复制工作空间的域
        /// <summary>
        /// 复制工作空间的域
        /// </summary>
        /// <param name="pWorkspace1"></param>
        /// <param name="pWorkspace2"></param>
        private void CreateWorkspaceDomains(IWorkspace pWorkspace1, IWorkspace pWorkspace2)
        {
            WaitForm.SetCaption("正在复制工作空间域,请稍后...");
            IWorkspaceDomains pWorkspaceDomains1 = pWorkspace1 as IWorkspaceDomains;
            IWorkspaceDomains pWorkspaceDomains2 = pWorkspace2 as IWorkspaceDomains;

            IEnumDomain pEnumDomain = pWorkspaceDomains1.Domains;
            if (pEnumDomain != null)
            {
                IDomain pDomain1 = pEnumDomain.Next();
                IDomain pDomain2;

                while (pDomain1 != null)
                {
                    if (pDomain1.Type == esriDomainType.esriDTCodedValue)//编码域
                    {
                        ICodedValueDomain tempDomain1 = pDomain1 as ICodedValueDomain;
                        ICodedValueDomain tempDomain2 = new CodedValueDomainClass();

                        for (int i = 0; i < tempDomain1.CodeCount; i++)
                        {
                            tempDomain2.AddCode(tempDomain1.get_Value(i), tempDomain1.get_Name(i));
                        }

                        pDomain2 = tempDomain2 as IDomain;
                        pDomain2.Description = pDomain1.Description;
                        pDomain2.FieldType = pDomain1.FieldType;
                        pDomain2.DomainID = pDomain1.DomainID;
                        pDomain2.MergePolicy = pDomain1.MergePolicy;
                        pDomain2.Name = pDomain1.Name;
                        pDomain2.Owner = pDomain1.Owner;
                        pDomain2.SplitPolicy = pDomain1.SplitPolicy;

                        pWorkspaceDomains2.AddDomain(pDomain2);

                    }
                    else//范围域
                    {
                        IRangeDomain tempDomain1 = pDomain1 as IRangeDomain;
                        IRangeDomain tempDomain2 = new RangeDomainClass();

                        tempDomain2.MaxValue = tempDomain1.MaxValue;
                        tempDomain2.MinValue = tempDomain1.MinValue;

                        pDomain2 = tempDomain2 as IDomain;

                        pWorkspaceDomains2.AddDomain(pDomain2);

                    }

                    pDomain1 = pEnumDomain.Next();
                }
            }
        }
        #endregion 
    }
}
