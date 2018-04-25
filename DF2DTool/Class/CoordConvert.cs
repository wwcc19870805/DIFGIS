using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using DFCommon.Class;
using DFWinForms.Class;
using DevExpress.XtraEditors;

namespace DF2DTool.Class
{
    public class CoordConvert
    {
        private double m_A1 = 0;
        private double m_B1 = 0;
        private double m_C1 = 0;

        private double m_A2 = 0;
        private double m_B2 = 0;
        private double m_C2 = 0;

        private double m_A3 = 0;
        private double m_C3 = 0;

        private bool m_IsRotationTextAnno = false;
        private bool m_IsRotationNumAnno = false;

        private string m_Path = "";
        private string m_Name = "";

        private ISpatialReference m_SpatialReference;
        private IEnvelope m_Envelope;
        private IMap m_Map;
        private IWorkspace pWs1;
        private IEnvelope m_Envelop;

        public CoordConvert(double[] convertPara,string mdbPath,string mdbName,bool rotationTextAnno,bool rotationNumAnno,IMap map)
        {
            m_A1 = convertPara[0];
            m_B1 = convertPara[1];
            m_C1 = convertPara[2];
            m_A2 = convertPara[3];
            m_B2 = convertPara[4];
            m_C2 = convertPara[5];
            m_A3 = convertPara[6];
            m_C3 = convertPara[7];
            m_Path = mdbPath;
            m_Name = mdbName;
            m_IsRotationTextAnno = rotationTextAnno;
            m_IsRotationNumAnno = rotationNumAnno;
            m_Map = map;
        }

        public void CoordConvertMap()
        {
            try
            {
                WaitForm.Start("开始坐标系转换,请稍后...");
                IWorkspaceFactory pWsF = new AccessWorkspaceFactoryClass();
                m_SpatialReference = new UnknownCoordinateSystemClass();
                m_Envelop = new EnvelopeClass();
                InitEnvelop();
                m_SpatialReference.SetDomain(m_Envelop.XMin, m_Envelop.XMax, m_Envelop.YMin, m_Envelop.YMax);
                m_SpatialReference.SetZDomain(-1000, 2097151002.92969);
                m_SpatialReference.SetMDomain(-1000, 8388607011.71875);
                //pWsF.Create(m_Path, m_Name, null, 0);
                IWorkspace pWs2 = pWsF.OpenFromFile(m_Path + @"\" + m_Name, 0);
                string pWs1Path = Config.GetConfigValue("2DMdbPipe");
                if (pWs1Path == "") return;
                pWs1 = pWsF.OpenFromFile(pWs1Path, 0);
                CreateWorkspaceDomains(pWs1, pWs2);
                for (int i = 0; i < m_Map.LayerCount; i++)
                {
                    ILayer layer = m_Map.get_Layer(i);
                    if (layer.Visible)
                    {
                        //WaitForm.SetCaption("正在转换图层" + layer.Name + ",请稍后...");
                        CoordConvertLayers(layer, m_SpatialReference);
                    }
                }
                WaitForm.SetCaption("坐标系转换成功！");              
                WaitForm.Stop();
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }
           
        }
        //复制工作空间坐标空间域
        private void CreateWorkspaceDomains(IWorkspace pWs1,IWorkspace pWs2)
        {
            try
            {
                WaitForm.SetCaption("正在复制工作空间域,请稍后...");
                IWorkspaceDomains pWsD1 = pWs1 as IWorkspaceDomains;
                IWorkspaceDomains pWsD2 = pWs2 as IWorkspaceDomains;
                IEnumDomain pEnumDomain = pWsD1.Domains;
                IDomain pDomain1 = null;
                IDomain pDomain2 = null;
                if (pEnumDomain == null) return;
                while ((pDomain1 = pEnumDomain.Next()) != null)
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
                        pWsD2.AddDomain(pDomain2);

                    }
                    else//范围域
                    {
                        IRangeDomain tempDomain1 = pDomain1 as IRangeDomain;
                        IRangeDomain tempDomain2 = new RangeDomainClass();
                        tempDomain2.MaxValue = tempDomain1.MaxValue;
                        tempDomain2.MinValue = tempDomain1.MinValue;
                        pDomain2 = tempDomain2 as IDomain;
                        pWsD2.AddDomain(pDomain2);  
                    }
                }   
            }
            catch (System.Exception ex)
            {
                return;
            }
           
        }
        //转换图层
        private void CoordConvertLayers(ILayer layer, ISpatialReference spatialReference)
        {
            if (layer is IGroupLayer)
            {
                ICompositeLayer pComLayer = layer as ICompositeLayer;
                for (int i = 0; i < pComLayer.Count; i++)
                {
                    CoordConvertLayers(pComLayer.get_Layer(i), spatialReference);
                }
            }
            else
            {
                IFeatureClass pTempFc = null;
                IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
                if (pFeatureLayer != null)
                {
                    CoordConvertFeatureClass(pFeatureLayer.FeatureClass, spatialReference, out pTempFc);
                }
            }
        }
        //转换要素类
        private void CoordConvertFeatureClass(IFeatureClass fc, ISpatialReference spatialReference, out IFeatureClass outfc)
        {
            WaitForm.SetCaption("正在转换" + fc.AliasName + ",请稍后...");
            outfc = null;
            IWorkspaceFactory pWsF = new AccessWorkspaceFactoryClass();
            IFeatureWorkspace pFWs = null;
            IFeatureClass pTempFc = null;
            try
            {
                IFields pFields = new FieldsClass();
                pFields = GetFieldsFromFeatureClass(fc, spatialReference);

                pFWs = pWsF.OpenFromFile(m_Path + @"\" + m_Name, 0) as IFeatureWorkspace;

                IDataset pDataset = fc as IDataset;
                string strFCName = pDataset.Name;
                try
                {
                    pTempFc = pFWs.OpenFeatureClass(strFCName);
                }
                catch (System.Exception ex)
                {
                    IFeatureDataset pFeatureDataset = fc.FeatureDataset;
                    IFeatureDataset pFeatureDatasetNew = null;
                    if (pFeatureDataset != null)
                    {
                        try
                        {
                            pFeatureDatasetNew = pFWs.OpenFeatureDataset(pFeatureDataset.Name);
                            pTempFc = pFeatureDatasetNew.CreateFeatureClass(strFCName, pFields, fc.CLSID,fc.EXTCLSID, fc.FeatureType, fc.ShapeFieldName, null);
                            (pTempFc as IClassSchemaEdit).AlterAliasName(fc.AliasName);
                        }
                        catch 
                        {
                            pFeatureDatasetNew = pFWs.CreateFeatureDataset(pFeatureDataset.Name, spatialReference);

                            //若为注记
                            if (fc.FeatureType == esriFeatureType.esriFTAnnotation)
                            {
                                IFeatureWorkspaceAnno pFWsAnno = pFWs as IFeatureWorkspaceAnno;
                                IAnnoClass pAnnoClass = fc.Extension as IAnnoClass;

                                IGraphicsLayerScale pGLS = new GraphicsLayerScaleClass();
                                pGLS.ReferenceScale = pAnnoClass.ReferenceScale;
                                pGLS.Units = pAnnoClass.ReferenceScaleUnits;
                                try
                                {
                                    pTempFc = pFWsAnno.CreateAnnotationClass(strFCName, 
                                        pFields, fc.CLSID, fc.EXTCLSID, fc.ShapeFieldName, "", 
                                        pFeatureDatasetNew, null, pAnnoClass.AnnoProperties, pGLS,
                                        pAnnoClass.SymbolCollection, true);
                                    (pTempFc as IClassSchemaEdit).AlterAliasName(fc.AliasName);
                                }
                                catch 
                                {
                                    WaitForm.Stop();
                                }
                            }
                            else//若为地理要素
                            {
                                try
                                {
                                    pTempFc = pFeatureDatasetNew.CreateFeatureClass(strFCName, pFields, null, null, fc.FeatureType, fc.ShapeFieldName, null);
                                    (pTempFc as IClassSchemaEdit).AlterAliasName(fc.AliasName);
                                }
                                catch 
                                {
                                    WaitForm.Stop();
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (fc.FeatureType == esriFeatureType.esriFTAnnotation)//若为注记
                            {
                                IFeatureWorkspaceAnno pFWsAnno = pFWs as IFeatureWorkspaceAnno;
                                IAnnoClass pAnnoClass = fc.Extension as IAnnoClass;

                                IGraphicsLayerScale pGLS = new GraphicsLayerScaleClass();
                                pGLS.ReferenceScale = pAnnoClass.ReferenceScale;
                                pGLS.Units = pAnnoClass.ReferenceScaleUnits;
                                try
                                {
                                    pTempFc = pFWsAnno.CreateAnnotationClass(strFCName,
                                        pFields, fc.CLSID, fc.EXTCLSID, fc.ShapeFieldName, "",
                                        pFeatureDatasetNew, null, pAnnoClass.AnnoProperties, pGLS,
                                        pAnnoClass.SymbolCollection, true);
                                    (pTempFc as IClassSchemaEdit).AlterAliasName(fc.AliasName);
                                }
                                catch 
                                {
                                    WaitForm.Stop();
                                }
                            }
                            else
                            {
                                try
                                {
                                    pTempFc = pFeatureDatasetNew.CreateFeatureClass(strFCName, pFields, null, null, fc.FeatureType, fc.ShapeFieldName, null);
                                    (pTempFc as IClassSchemaEdit).AlterAliasName(fc.AliasName);
                                }
                                catch 
                                {
                                    WaitForm.Stop();
                                }
                            }

                        }
                        catch 
                        {
                            WaitForm.Stop();
                        }
                    }
                }
                if (fc.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    IFeature outFeature;
                    IFeature pFeature;
                    IFeatureCursor pFeatureCursor = GetSelectedFeatures(GetLayerByNameFromMap(m_Map, strFCName));
                    if (pFeatureCursor == null) return;
                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                    {
                        try
                        {
                            outFeature = pTempFc.CreateFeature();
                            CoordConvertAnnotationFeature(pFeature, ref outFeature);
                            outFeature.Store();
                        }
                        catch (System.Exception ex)
                        {
                            WaitForm.Stop();
                        }

                    }
                    outfc = pTempFc;
                }
                else//若为几何要素
                {
                    IFeature outFeature;
                    IFeature pFeature;
                    IFeatureCursor pFeatureCursor = GetSelectedFeatures(GetLayerByNameFromMap(m_Map, strFCName));
                    if (pFeatureCursor == null) return;
                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                    {
                        try
                        {
                            outFeature = pTempFc.CreateFeature();
                            CoordConvertFeature(pFeature, ref outFeature);
                            outFeature.Store();
                        }
                        catch 
                        {
                            WaitForm.Stop();
                        }

                    }
                    outfc = pTempFc;
                }

               

            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
            }       

        }

        //根据要素类名得到图层
        private ILayer GetLayerByNameFromMap(IMap map,string name)
        {
            if (map == null || name == null) return null;
            ILayer layer = null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                GetLayerFromGroupLayers(map.get_Layer(i), ref layer, name);
                if (layer != null) break;
            }
            return layer;
        }
        //得到该图层中被选择的要素
        private static IFeatureCursor GetSelectedFeatures(ILayer pLayer)
        {
            if (pLayer == null) return null;

            ICursor pCursor;
            IFeatureSelection pFSelection;
            ISelectionSet pFSet;

            pFSelection = (IFeatureSelection)pLayer;
            pFSet = pFSelection.SelectionSet;

            if (pFSet.Count > 0)
            {
                pFSet.Search(null, false, out pCursor);
                return (IFeatureCursor)pCursor;
            }
            else
            {
                return null;
            }
        }

        //生成点注记
        public ITextElement MakeTextElement(ITextElement pTextElement, double ptX, double ptY, int intCharacterWidthIndexValue)
        {
            ITextElement pMyTextElement = new TextElementClass();
            pMyTextElement.Text = pTextElement.Text;

            ITextSymbol pTextSymbol = new TextSymbolClass();

            Console.WriteLine(pMyTextElement.Text);
            //若果要旋转注记
            if (CommonFunction.MatchNumber(pMyTextElement.Text))//如果数字
            {
                if (m_IsRotationNumAnno)
                {
                    pTextSymbol.Angle = pTextElement.Symbol.Angle + Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90;
                }
                else
                {
                    pTextSymbol.Angle = pTextElement.Symbol.Angle;
                }
            }
            else//若果文字
            {

                string str = pMyTextElement.Text;
                if (IsChina(str))
                {
                    if (m_IsRotationTextAnno)
                    {
                        pTextSymbol.Angle = pTextElement.Symbol.Angle + Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90;
                    }
                    else
                    {
                        pTextSymbol.Angle = pTextElement.Symbol.Angle;
                    }
                }
                else
                {
                    if (Char.IsLetter(str[0])) //若果为点号（D12345）或 规格注记(DL2或 DN 12345
                    {
                        if (str.Contains(" ") || Char.IsLetter(str[1]))//规格注记====则必须旋转！
                        {
                            pTextSymbol.Angle = pTextElement.Symbol.Angle + Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90;
                        }
                        else//点号=根据设置来确定是否旋转
                        {
                            if (m_IsRotationNumAnno)
                            {
                                pTextSymbol.Angle = pTextElement.Symbol.Angle + Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90;
                            }
                            else
                            {
                                pTextSymbol.Angle = pTextElement.Symbol.Angle;
                            }
                        }
                    }
                }

            }
            pTextSymbol.Color = pTextElement.Symbol.Color;
            pTextSymbol.Font = pTextElement.Symbol.Font;
            pTextSymbol.HorizontalAlignment = pTextElement.Symbol.HorizontalAlignment;
            pTextSymbol.RightToLeft = pTextElement.Symbol.RightToLeft;
            pTextSymbol.Size = pTextElement.Symbol.Size;
            pTextSymbol.Text = pTextElement.Symbol.Text;
            pTextSymbol.VerticalAlignment = pTextElement.Symbol.VerticalAlignment;

            IFormattedTextSymbol pFormattedTextSymbol = pTextSymbol as IFormattedTextSymbol;
            pFormattedTextSymbol.CharacterWidth = intCharacterWidthIndexValue;

            pMyTextElement.Symbol = pTextSymbol;

            IElement pElement;
            pElement = (IElement)pMyTextElement;

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(ptX, ptY);
            pElement.Geometry = pPoint;


            return pMyTextElement;
        }
        //是否为中文
        public bool IsChina(string CString)
        {

            bool BoolValue = false;

            for (int i = 0; i < CString.Length; i++)
            {

                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {

                    BoolValue = false;

                }

                else
                {

                    BoolValue = true;

                }

            }

            return BoolValue;

        }
        //生成线注记
        public ITextElement MakeTextElement(ITextElement pTextElement, IPolyline pPolyline, int intCharacterWidthIndexValue)
        {
            ITextElement pMyTextElement = new TextElementClass();
            pMyTextElement.Text = pTextElement.Text;

            ITextSymbol pTextSymbol = new TextSymbolClass();

            //若果要旋转注记
            if (!CommonFunction.MatchNumber(pMyTextElement.Text))//如果文字
            {
                if (m_IsRotationTextAnno)
                {
                    pTextSymbol.Angle = pTextElement.Symbol.Angle + Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90;
                }
                else
                {
                    pTextSymbol.Angle = pTextElement.Symbol.Angle;
                }
            }
            else//若果数字
            {
                if (m_IsRotationNumAnno)
                {
                    pTextSymbol.Angle = pTextElement.Symbol.Angle + Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90;
                }
                else
                {
                    pTextSymbol.Angle = pTextElement.Symbol.Angle;
                }
            }

            pTextSymbol.Color = pTextElement.Symbol.Color;
            pTextSymbol.Font = pTextElement.Symbol.Font;
            pTextSymbol.HorizontalAlignment = pTextElement.Symbol.HorizontalAlignment;
            pTextSymbol.RightToLeft = pTextElement.Symbol.RightToLeft;
            pTextSymbol.Size = pTextElement.Symbol.Size;
            pTextSymbol.Text = pTextElement.Symbol.Text;
            pTextSymbol.VerticalAlignment = pTextElement.Symbol.VerticalAlignment;

            IFormattedTextSymbol pFormattedTextSymbol = pTextSymbol as IFormattedTextSymbol;
            pFormattedTextSymbol.CharacterWidth = intCharacterWidthIndexValue;

            pMyTextElement.Symbol = pTextSymbol;

            IElement pElement;
            pElement = (IElement)pMyTextElement;

            pElement.Geometry = pPolyline;


            return pMyTextElement;
        }
        //从图图层层组中得到图层
        private void GetLayerFromGroupLayers(ILayer pLayer, ref ILayer layer, string name)
        {
            if (pLayer is IGroupLayer)
            {
                ICompositeLayer comLayer = pLayer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    GetLayerFromGroupLayers(comLayer.get_Layer(i), ref layer, name);
                }
            }
            else if (pLayer is IFeatureLayer)
            {
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if(pFeatureLayer.Name.ToUpper() == name.ToUpper()||pFeatureLayer.Name.ToUpper() == name.ToUpper()+"_2D"||pFeatureLayer.FeatureClass.AliasName.ToUpper() == name.ToUpper()||pFeatureLayer.FeatureClass.AliasName.ToUpper() == name.ToUpper() + "_2D"||
                    (pFeatureLayer.FeatureClass as IDataset).Name.ToUpper() == name.ToUpper() || (pFeatureLayer.FeatureClass as IDataset).Name.ToUpper() == name.ToUpper() + "_2D")
                {
                    layer = pLayer;
                    return;
                }

                
            }
        }
        //注记要素坐标转换
        private void CoordConvertAnnotationFeature(IFeature inFeature, ref IFeature outFeature)
        {
            try
            {
                IAnnotationFeature afin = inFeature as IAnnotationFeature;
                IAnnotationFeature afout = outFeature as IAnnotationFeature;

                IGeometry pTempGeo = afin.Annotation.Geometry;
                GeometryCoordConvert(ref pTempGeo, m_A1, m_B1, m_C1, m_A2, m_B2, m_C2, m_A3, m_C3);

                IFields fields = inFeature.Fields;
                int shapeIndex = outFeature.Fields.FindField("SHAPE");
                int angleIndex = outFeature.Fields.FindField("Angle");
                int characterWidthIndex = outFeature.Fields.FindField("CHARACTERWIDTH");
                int characterWidthIndexValue = 0;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    int outFeatureFieldIndex = outFeature.Fields.FindField(fields.get_Field(i).Name);
                    if (outFeature.Fields.get_Field(outFeatureFieldIndex).Editable && outFeatureFieldIndex != shapeIndex && outFeatureFieldIndex != angleIndex)
                    {
                        characterWidthIndexValue = Convert.ToInt16(inFeature.get_Value(characterWidthIndex));
                        outFeature.set_Value(outFeatureFieldIndex, inFeature.get_Value(i));
                    }
                }

                try
                {
                    IPoint pTempPoint = pTempGeo as IPoint;
                    ITextElement pTextElement = afin.Annotation as ITextElement;
                    IElement pElement = null;
                    if (pTempGeo.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        pElement = MakeTextElement(pTextElement, pTempPoint.X, pTempPoint.Y, characterWidthIndexValue) as IElement;
                    }
                    else if (pTempGeo.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        pElement = MakeTextElement(pTextElement, pTempGeo as IPolyline, characterWidthIndexValue) as IElement;
                    }
                    afout.Annotation = pElement;
                }
                catch (System.Exception ex)
                {

                }

            }
            catch (System.Exception ex)
            {
            	
            }
            
        }
        //要素坐标转换
        private void CoordConvertFeature(IFeature inFeature, ref IFeature outFeature)
        {
            try
            {
                IGeometry resultGeometry = inFeature.ShapeCopy;

                if (resultGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    PointCoordConvert(ref resultGeometry, m_A1, m_B1, m_C1, m_A2, m_B2, m_C2, m_A3, m_C3);
                }
                else
                {
                    GeometryCoordConvert(ref resultGeometry, m_A1, m_B1, m_C1, m_A2, m_B2, m_C2, m_A3, m_C3);
                }

                IFields fields = inFeature.Fields;
                int shapeIndex = fields.FindField("SHAPE");

                if (resultGeometry.GeometryType == esriGeometryType.esriGeometryPoint)//如果为点要素
                {
                    int GeoObjNumIndex = fields.FindField("GeoObjNum");
                    string GeoObjNumValue = inFeature.get_Value(GeoObjNumIndex).ToString();

                    if (GeoObjNumValue == "211101" || GeoObjNumValue == "238121" || GeoObjNumValue == "322131" || GeoObjNumValue == "322231" || GeoObjNumValue == "322311" || GeoObjNumValue == "322341" || GeoObjNumValue == "322351" || GeoObjNumValue == "323111" || GeoObjNumValue == "324101" || GeoObjNumValue == "381101" ||
                        GeoObjNumValue == "411211" || GeoObjNumValue == "411221" || GeoObjNumValue == "411231" || GeoObjNumValue == "411241" || GeoObjNumValue == "411311" || GeoObjNumValue == "427101" ||
                        GeoObjNumValue == "411321" || GeoObjNumValue == "427001" ||
                        GeoObjNumValue == "517511" || GeoObjNumValue == "517521" || GeoObjNumValue == "515101" || GeoObjNumValue == "516101" || GeoObjNumValue == "515201" || GeoObjNumValue == "515601" || GeoObjNumValue == "515701" ||
                        GeoObjNumValue == "525101" || GeoObjNumValue == "525201" || GeoObjNumValue == "525701" || GeoObjNumValue == "525201" ||
                        GeoObjNumValue == "535701" || GeoObjNumValue == "535801" || GeoObjNumValue == "535901" ||
                        GeoObjNumValue == "545511" || GeoObjNumValue == "545521" || GeoObjNumValue == "545601" || GeoObjNumValue == "545701" || GeoObjNumValue == "545801" || GeoObjNumValue == "545901" || GeoObjNumValue == "546001" ||
                        GeoObjNumValue == "555701" || GeoObjNumValue == "555801" || GeoObjNumValue == "555901" ||
                        GeoObjNumValue == "565701" || GeoObjNumValue == "565801" || GeoObjNumValue == "565901" ||
                        GeoObjNumValue == "571571" || GeoObjNumValue == "571581" || GeoObjNumValue == "571591" ||
                        GeoObjNumValue == "572571" || GeoObjNumValue == "572581" || GeoObjNumValue == "572591" ||
                        GeoObjNumValue == "573571" || GeoObjNumValue == "573581" || GeoObjNumValue == "573591" ||
                        GeoObjNumValue == "574571" || GeoObjNumValue == "574581" || GeoObjNumValue == "574591" || GeoObjNumValue == "574551" ||
                        GeoObjNumValue == "633101" || GeoObjNumValue == "641301" || GeoObjNumValue == "641401" || GeoObjNumValue == "671001")//枚举 有方向的符号
                    {
                        int DirctionIndex = fields.FindField("DIRCTION");//方向字段
                        double dblDirctionValue = Convert.ToDouble(inFeature.get_Value(DirctionIndex));//方向字段的值

                        for (int i = 0; i < fields.FieldCount; i++)
                        {
                            if (fields.get_Field(i).Editable && i != shapeIndex)//如果不为shape字段
                            {
                                if (i != DirctionIndex)
                                {
                                    outFeature.set_Value(i, inFeature.get_Value(i));
                                }
                                else //方向字段
                                {
                                    outFeature.set_Value(i, Convert.ToDouble(inFeature.get_Value(i)) - (Math.Atan(m_A1 / m_A2) * 180 / Math.PI - 90));
                                }
                            }
                        }

                    }
                    else //无方向的符号
                    {
                        SetOutFeatureValue(inFeature, shapeIndex, ref outFeature);
                        //for (int i = 0; i < fields.FieldCount; i++)
                        //{
                        //    if (fields.get_Field(i).Editable && i != shapeIndex)//如果不为shape字段
                        //    {
                        //        outFeature.set_Value(i, inFeature.get_Value(i));
                        //    }
                        //}

                    }

                }
                else//线、面、注记要素
                {
                    SetOutFeatureValue(inFeature, shapeIndex, ref outFeature);
                    //for (int i = 0; i < fields.FieldCount; i++)
                    //{
                    //    if (fields.get_Field(i).Editable && i != shapeIndex)//如果不为shape字段
                    //    {
                    //        outFeature.set_Value(i, inFeature.get_Value(i));
                    //    }
                    //}
                }

                try
                {
                    outFeature.Shape = resultGeometry;
                }
                catch
                {
                    IGeometryDef pGD = fields.get_Field(shapeIndex).GeometryDef;
                    if (pGD.HasZ)
                    {
                        IZAware pZA = (IZAware)resultGeometry;
                        pZA.ZAware = true;
                    }
                    else
                    {
                        IZAware pZA = (IZAware)resultGeometry;
                        pZA.ZAware = false;
                    }

                    if (pGD.HasM)
                    {
                        IMAware pMA = (IMAware)resultGeometry;
                        pMA.MAware = true;
                    }
                    else
                    {
                        IMAware pMA = (IMAware)resultGeometry;
                        pMA.MAware = false;
                    }

                    try
                    {
                        outFeature.Shape = resultGeometry;
                    }
                    catch 
                    {

                    }
                }
            }
            catch
            {
            	
            }
           
        }
        //为转换后的要素字段赋值
        private void SetOutFeatureValue(IFeature inFeature,int shapeIndex, ref IFeature outFeature)
        {
            int inCount = inFeature.Fields.FieldCount;
            int outCount = outFeature.Fields.FieldCount;
            //Dictionary<string, int> infields = new Dictionary<string, int>();
            //Dictionary<string, int> outfields = new Dictionary<string, int>();
            //for (int i = 0; i < inFeature.Fields.FieldCount; i++)
            //{
            //    infields[inFeature.Fields.get_Field(i).Name] = i;
            //    outfields[outFeature.Fields.get_Field(i).Name] = i;
            //}
            for (int i = 0; i < inCount; i++)
            {
                try
                {                  
                    //string inName = inFeature.Fields.get_Field(i).Name;
                    //string inFieldType = inFeature.Fields.get_Field(i).Type.ToString();
                    //string outName = outFeature.Fields.get_Field(i).Name;
                    //string outFieldType = outFeature.Fields.get_Field(i).Type.ToString();

                    string fName = inFeature.Fields.get_Field(i).Name;
                    int index = outFeature.Fields.FindField(fName);
                    if (inFeature.Fields.get_Field(i).Editable && i != shapeIndex)//如果不为shape字段
                    {                      
                        outFeature.set_Value(index, inFeature.get_Value(i));
                    }

                }
                catch (System.Exception ex)
                {
                    continue;
                }

            }
        }
        //几何图形坐标转换
        private void GeometryCoordConvert(ref IGeometry pConvertGeometry, double A1, double B1, double C1, double A2, double B2, double C2, double A3, double C3)
        {
            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;
            bool isRing = false;
            if (pConvertGeometry.GeometryType != esriGeometryType.esriGeometryPoint)//如果为线要素或面要素
            {
                IArray pArrayPoint = new ArrayClass();
                IArray pGeometryArray = new ArrayClass();
                IGeometryCollection pGeometryCollection = pConvertGeometry as IGeometryCollection;
                for (int i = 0; i < pGeometryCollection.GeometryCount; i++)
                {
                    IGeometry pGeometry = pGeometryCollection.get_Geometry(i);
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)//
                    {
                        #region
                        if (pGeometry.GeometryType == esriGeometryType.esriGeometryRing)
                        {
                            isRing = true;
                        }

                        if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            pGeometry = CommonFunction.GetPolygonBoundary((IPolygon)pGeometry);
                        }

                        ISegmentCollection pSegmentCol = (ISegmentCollection)pGeometry;
                        ISegmentCollection pNewSegmentCol = new PolylineClass();
                        for (int k = 0; k < pSegmentCol.SegmentCount; k++)//遍历几何形体的每个节（片断）
                        {
                            //该节为直线段
                            if (pSegmentCol.get_Segment(k).GeometryType == esriGeometryType.esriGeometryLine)
                            {
                                IPointCollection pPointCol1 = new MultipointClass();
                                ILine pLine = (ILine)pSegmentCol.get_Segment(k);

                                IPoint pFromPoint = pLine.FromPoint;
                                pPointCol1.AddPoint((IPoint)pFromPoint, ref a, ref b);
                                IPoint pToPoint = pLine.ToPoint;
                                pPointCol1.AddPoint((IPoint)pToPoint, ref a, ref b);

                                PointCollCoordConvert(A1, B1, C1, A2, B2, C2, A3, C3, ref pPointCol1);//对点集做镜像

                                //修改线段的端点坐标
                                pLine.FromPoint = pPointCol1.get_Point(0);
                                pLine.ToPoint = pPointCol1.get_Point(1);

                                pNewSegmentCol.AddSegment((ISegment)pLine, ref a, ref b);
                            }
                            //该节为圆弧
                            else if (pSegmentCol.get_Segment(k).GeometryType == esriGeometryType.esriGeometryCircularArc)
                            {
                                IPointCollection pPointCol2 = new MultipointClass();

                                ICircularArc pCircularArc = (ICircularArc)pSegmentCol.get_Segment(k);

                                try
                                {
                                    IPoint pCenterPoint = pCircularArc.CenterPoint;
                                    pPointCol2.AddPoint((IPoint)pCenterPoint, ref a, ref b);
                                    IPoint pFromPoint = pCircularArc.FromPoint;
                                    pPointCol2.AddPoint((IPoint)pFromPoint, ref a, ref b);
                                    IPoint pToPoint = pCircularArc.ToPoint;
                                    pPointCol2.AddPoint((IPoint)pToPoint, ref a, ref b);

                                    PointCollCoordConvert(A1, B1, C1, A2, B2, C2, A3, C3, ref pPointCol2);//对点集做镜像

                                    //构造新的圆弧
                                    ICircularArc pArc = new CircularArcClass();
                                    pArc.PutCoords(pPointCol2.get_Point(0), pPointCol2.get_Point(1), pPointCol2.get_Point(2),
                                        (pCircularArc.IsCounterClockwise ? esriArcOrientation.esriArcCounterClockwise : esriArcOrientation.esriArcClockwise));

                                    pNewSegmentCol.AddSegment((ISegment)pArc, ref a, ref b);
                                }
                                catch { }
                            }
                            //该节为贝塞尔曲线
                            else if (pSegmentCol.get_Segment(k).GeometryType == esriGeometryType.esriGeometryBezier3Curve)
                            {
                                IPointCollection pPointCol3 = new MultipointClass();
                                IBezierCurve pBezierCurve = (IBezierCurve)pSegmentCol.get_Segment(k);

                                //记录该节贝塞尔曲线的４个控制点
                                IPoint pFromPoint = new PointClass();
                                pBezierCurve.QueryCoord(0, pFromPoint);
                                pPointCol3.AddPoint(pFromPoint, ref a, ref b);
                                IPoint pFromTangentPoint = new PointClass();
                                pBezierCurve.QueryCoord(1, pFromTangentPoint);
                                pPointCol3.AddPoint(pFromTangentPoint, ref a, ref b);
                                IPoint pToTangentPoint = new PointClass();
                                pBezierCurve.QueryCoord(2, pToTangentPoint);
                                pPointCol3.AddPoint(pToTangentPoint, ref a, ref b);
                                IPoint pToPoint = new PointClass();
                                pBezierCurve.QueryCoord(3, pToPoint);
                                pPointCol3.AddPoint(pToPoint, ref a, ref b);

                                PointCollCoordConvert(A1, B1, C1, A2, B2, C2, A3, C3, ref pPointCol3);//对点集做镜像

                                //修改该节贝塞尔曲线的４个控制点
                                pBezierCurve.PutCoord(0, pPointCol3.get_Point(0));
                                pBezierCurve.PutCoord(1, pPointCol3.get_Point(1));
                                pBezierCurve.PutCoord(2, pPointCol3.get_Point(2));
                                pBezierCurve.PutCoord(3, pPointCol3.get_Point(3));

                                pNewSegmentCol.AddSegment((ISegment)pBezierCurve, ref a, ref b);

                            }

                        }//end for 遍历几何形体的每个节（片断）

                        CommonFunction.GeometryToArray(pNewSegmentCol as IGeometry, pArrayPoint);

                        IPolycurve2 pPolycurve2 = CommonFunction.BuildPolyLineFromSegmentCollection(pNewSegmentCol);

                        pGeometry = (IGeometry)pPolycurve2;

                        #endregion
                    }
                    if (pConvertGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)//由线构成面
                    {
                        pGeometry = CommonFunction.PolylineToPolygon(pGeometry as IPolyline);
                    }
                    pGeometryArray.Add(pGeometry);
                }
                if (pGeometryArray.Count > 1)
                {
                    pConvertGeometry = pGeometryArray.get_Element(0) as IGeometry;
                    if (isRing == true)
                    {
                        for (int i = 1; i < pGeometryArray.Count; i++)
                        {
                            pConvertGeometry = CommonFunction.DiffenceGeometry(pConvertGeometry, pGeometryArray.get_Element(i) as IGeometry);
                        }
                    }
                    else
                    {
                        for (int i = 1; i < pGeometryArray.Count; i++)
                        {
                            pConvertGeometry = CommonFunction.UnionGeometry(pConvertGeometry, pGeometryArray.get_Element(i) as IGeometry);
                        }
                    }
                }
                else
                {
                    pConvertGeometry = pGeometryArray.get_Element(0) as IGeometry;
                }

                CommonFunction.AddZMValueForGeometry(ref pConvertGeometry, pArrayPoint);
            }
            else
            {
                PointCoordConvert(ref pConvertGeometry, A1, B1,C1,A2, B2,C2, A3, C3);
            }
        }
        //点集合坐标转换
        private void PointCollCoordConvert(double A1, double B1, double C1, double A2, double B2, double C2, double A3, double C3, ref IPointCollection pPointCollection)
        {
            IArray pArray = new ArrayClass();
            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                pArray.Add(pPointCollection.get_Point(i));
            }

            double X, Y, Z, M;
            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                X = (pArray.get_Element(i) as Point).X;
                Y = (pArray.get_Element(i) as Point).Y;
                Z = (pArray.get_Element(i) as Point).Z;
                M = (pArray.get_Element(i) as Point).M;


                IPoint pTempPoint = new PointClass();
                pTempPoint.Y = A1 * Y + B1 * X + C1;
                pTempPoint.X = A2 * Y + B2 * X + C2;

                if (Z == 0)
                {
                    C3 = 0;
                }

                pTempPoint.Z = A3 * Z + C3;
                pTempPoint.M = M;

                pPointCollection.UpdatePoint(i, pTempPoint);
            }

            pArray = null;

        }
        //点坐标转换
        private void PointCoordConvert(ref IGeometry pGeometry, double A1, double B1, double C1, double A2, double B2, double C2, double A3, double C3)
        {
            double X, Y, Z, M;

            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IPoint pPoint;
                pPoint = (IPoint)pGeometry;

                X = pPoint.X;
                Y = pPoint.Y;
                Z = pPoint.Z;
                M = pPoint.M;

                IPoint pTempPoint = new PointClass();
                pTempPoint.Y = A1 * Y + B1 * X + C1;
                pTempPoint.X = A2 * Y + B2 * X + C2;
                pTempPoint.Z = A3 * Z + C3;
                pTempPoint.M = M;

                pGeometry = pTempPoint;
            }
        }
        //得到要素类的字段集合
        private IFields GetFieldsFromFeatureClass(IFeatureClass fc, ISpatialReference spatialReference)
        {
            int index = fc.FindField(fc.ShapeFieldName);
            IGeometryDef pGD = fc.Fields.get_Field(index).GeometryDef;

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            for (int i = 0; i < fc.Fields.FieldCount; i++)
            {
                IField pSourceField = fc.Fields.get_Field(i);
                if (fc.Fields.get_Field(i).Name == "shape" || fc.Fields.get_Field(i).Name == "SHAPE")
                {
                    IGeometryDef geometryDef = new GeometryDefClass();
                    IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
                    geometryDefEdit.GeometryType_2 = fc.ShapeType;
                    geometryDefEdit.GridCount_2 = 1;
                    geometryDefEdit.set_GridSize(0, 1000);
                    geometryDefEdit.AvgNumPoints_2 = 2;
                    geometryDefEdit.SpatialReference_2 = spatialReference;
                    geometryDefEdit.HasM_2 = pGD.HasM;
                    geometryDefEdit.HasZ_2 = pGD.HasZ;

                    IFieldEdit pField_SHAPE = new FieldClass();
                    pField_SHAPE.Name_2 = "SHAPE";
                    pField_SHAPE.AliasName_2 = "SHAPE";
                    pField_SHAPE.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    pField_SHAPE.IsNullable_2 = true;
                    pField_SHAPE.GeometryDef_2 = geometryDef;
                    pFieldsEdit.AddField(pField_SHAPE as IField);
                }
                else
                {
                    pFieldsEdit.AddField(fc.Fields.get_Field(i));
                }
            }
            return pFields;
        }
        //初始化地图边界
        private void InitEnvelop()
        {
            if (this.m_Envelop == null) return;
            string location = SystemInfo.Instance.Location;
            switch (location)
            {
                case "湛江钢铁":
                    m_Envelop.PutCoords(46000, 28900, 48600, 30800);
                    break;

            }
        }
    }
}
