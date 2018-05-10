using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DFWinForms.Class;

namespace DF2DTool.Class
{
    public class GisReadFromWeb
    {
        private const string CARTOANNO = "CartoANNO500";
        private const string CARTOBORDER = "CartoBORDER500";
        private const string GISLAYERNAME = "GISLAYERNAME";
        readonly static string GEOOBJNUM = "GEOOBJNUM";
        readonly static string DIRECTIONLINE = "867012,866012,865012,853002,852202,852102,843202,843102,842012,841202,841102,655202,655102,654002,652002,648022,646102642012,642022,643002,644102,644202,644302,645102,645202,645302,645402,646012,646022,633002,634102,634202,453102,453202,454102,454112,431012,362402,325202,325102,318032,245002,244202,243202,242002,241402,241102";
        private CoreData currentData = new CoreData();
        private const double PI = Math.PI;

        #region  对外的属性和方法
        //输入图幅名称
        private string tuFuName = "";
        public string TuFuName
        {
            set { tuFuName = value; }
        }

        //输入的图层名称
        private IArray layerArr = null;
        public IArray LayerArr
        {
            set { layerArr = value; }
        }

        //输入的工作空间名称
        private IWorkspace inputWorkspace = null;
        public IWorkspace InputWorkspace
        {
            set { inputWorkspace = value; }
        }

        //输入的坐标序列
        private IGeometry pRegion;
        public IArray PtArray
        {
            set
            {
                pRegion = createRect(value as IArray);
                (pRegion as IPolygon2).Close();
                (pRegion as ITopologicalOperator).Simplify();
            }
        }

        //输入要转换的裁切面
        public IGeometry pGeo
        {
            set
            {
                pRegion = value;
                (pRegion as IPolygon2).Close();
                (pRegion as ITopologicalOperator).Simplify();
            }
        }

        //地物字段名
        private string strObjNum;
        public string StrObjNum
        {
            set { strObjNum = value; }
        }

        //块符号倾斜角度
        private string strAngle;
        public string StrAngle
        {
            set { strAngle = value; }
        }

        //配置的mdb文件名
        private string mdbFileName;
        public string MdbFileName
        {
            set { mdbFileName = value; }
        }

        //图层对应表
        private string layerTable;
        public string LayerTable
        {
            set { layerTable = value; }
        }

        //符号对应表
        private string symbolTable;
        public string SymbolTable
        {
            set { symbolTable = value; }
        }

        //当前的数据集
        private DataSet currentDs;
        public DataSet CurrentDs
        {
            get { return currentDs; }
            set { currentDs = value; }
        }

        //地图比例尺
        private double mapScale = 0.5;
        public double MapScale
        {
            set { mapScale = value; }
        }

        //图形范围
        private IEnvelope pEnv = new EnvelopeClass();
        public IEnvelope PEnv
        { get { return pEnv; } }

        public bool GisReadInit()
        {
            bool initOK = true;
            currentDs = currentData.CoreDs;
            this.addUsed3Fonts();
            try
            {
                this.loadLayerTable();
                this.loadSymbolTable();
                this.loadAttributeTable();
                this.loadBlockLineTypeFontTable();
            }
            catch (System.Exception ex)
            {
                initOK = false;
                WaitForm.Stop();
            }
            return initOK;
        }

        #endregion

        #region 读取配置文件的内容到dataset
        private void loadLayerTable()
        {
            ITable pLayerTable;
            pLayerTable = PublicFun.GetAccessTable(mdbFileName, layerTable);
            if (pLayerTable == null) return;

            IRow pRow;
            ICursor pCursor;
            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = this.CurrentDs.Tables["layerTable"];
            pCursor = pLayerTable.Search(null, true);
            pRow = pCursor.NextRow();
            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["CadLayer"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CadLayer")));
                tmpRow["LCOLOR"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("LColor")));
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();

            }
        }

        //读取symboltable表
        private void loadSymbolTable()
        {
            ITable pSymbolTable;
            pSymbolTable = PublicFun.GetAccessTable(mdbFileName, symbolTable);
            if (pSymbolTable == null) return;
            IRow pRow;
            ICursor pCursor;
            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = this.CurrentDs.Tables["symbolTable"];
            pCursor = pSymbolTable.Search(null, true);
            pRow = pCursor.NextRow();
            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["DifGISCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGISCode")));
                tmpRow["SymbolCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("SymbolCode")));
                tmpRow["LWIDTH"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("LWidth")));
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();
            }
        }

        //读取AttributeCASStoDifGIS对照表
        private void loadAttributeTable()
        {
            ITable pLayerTable;
            pLayerTable = PublicFun.GetAccessTable(mdbFileName, "AttributeCASStoDifGIS");
            if (pLayerTable == null) return;
            IRow pRow;
            ICursor pCursor;
            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = this.CurrentDs.Tables["AttributeCASStoDifGIS"];
            pCursor = pLayerTable.Search(null, true);
            pRow = pCursor.NextRow();
            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["CASSAttributeName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CASSAttributeName")));
                tmpRow["DifGISAttributeName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGISAttributeName")));
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();
            }
        }

        //读取块、线型、字体定义表
        private void loadBlockLineTypeFontTable()
        {
            ITable pLayerTable;
            pLayerTable = PublicFun.GetAccessTable(mdbFileName,"DefineBlockLineTypeFont");
            if (pLayerTable == null) return;
            IRow pRow;
            ICursor pCursor;
            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = this.CurrentDs.Tables["DefineBlockLineTypeFont"];
            pCursor = pLayerTable.Search(null, true);
            pRow = pCursor.NextRow();
            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["ID"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("ID")));
                tmpRow["DEFINE"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DEFINE")));
                tmpRow["TYPE"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("TYPE")));
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();
            }
        }

        //获取数据库字段的值
        private string getTableValue(object obj)
        {
            string rtnValue = "";
            if(obj == System.DBNull.Value) rtnValue = "";
            else
                rtnValue = obj.ToString();
            return rtnValue;
        }

        #endregion


        #region 读取实体主程序

        public void Read_EntitiesFromWeb()
        {
            string layerName;
            IFeatureClass pFeatureClass = null;
            ISpatialFilter spaFilter = new SpatialFilter();
            IFeatureCursor pFeaCur;
            IFeature pFeat;
            IGeometry outGeometry = null;
            IPoint pPoint;
            IEnvelope pEnvelope;
            string definitionQuery = "";

            for (int i = 0; i < this.layerArr.Count;i++ )
            {
                if (PublicFun.isWebService == false)
                {
                    try
                    {
                        layerName = ((layerArr.get_Element(i) as IFeatureLayer).FeatureClass as IDataset).Name;
                        pFeatureClass = (layerArr.get_Element(i) as IFeatureLayer).FeatureClass;
                        definitionQuery = (layerArr.get_Element(i) as IFeatureLayerDefinition).DefinitionExpression;
                    }
                    catch (System.Exception ex)
                    {
                        layerName = layerArr.get_Element(i).ToString();
                        pFeatureClass = this.getFeatureClass(layerName);
                        definitionQuery = "";
                    }
                }
                else
                {
                    layerName = layerArr.get_Element(i).ToString();
                    pFeatureClass = this.getFeatureClass(layerName);
                    definitionQuery = "";
                }
                if (pFeatureClass == null) continue;
                writeAppidTable(pFeatureClass.Fields);
                WaitForm.SetCaption("正在读取" + pFeatureClass.AliasName + "数据，请稍后...");
                if ((layerName.IndexOf(CARTOANNO) != -1 || layerName.IndexOf(CARTOBORDER) != -1) && this.tuFuName != "")
                {
                    IQueryFilter pQFilter = new QueryFilter();
                    pQFilter.WhereClause = "MapNO='" + this.tuFuName + "'";
                    pFeaCur = pFeatureClass.Search(pQFilter, true);
                    pFeat = pFeaCur.NextFeature();
                    while (pFeat != null)
                    {
                        this.Read_Entity_0(pFeat, layerName, pFeat.Shape);
                        pFeat = pFeaCur.NextFeature();
                    }
                }
                else
                {
                    pRegion.SpatialReference = (pFeatureClass as IGeoDataset).SpatialReference;
                    spaFilter.Geometry = pRegion;
                    spaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                    spaFilter.GeometryField = pFeatureClass.ShapeFieldName;
                    spaFilter.WhereClause = definitionQuery;
                    pFeaCur = pFeatureClass.Search(spaFilter, true);
                    pFeat = pFeaCur.NextFeature();

                    int k = 0;
                    while (pFeat != null)
                    {
                        k++;
                        if (pFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            IRelationalOperator ro = pRegion as IRelationalOperator;
                            IAnnotationFeature afin = pFeat as IAnnotationFeature;
                            pPoint = new PointClass();
                            pEnvelope = afin.Annotation.Geometry.Envelope;
                            pPoint.PutCoords((pEnvelope.XMin + pEnvelope.XMax)/2,(pEnvelope.YMin + pEnvelope.YMax)/2);
                            if (ro.Contains(pPoint))
                            {
                                this.Read_Entity_0(pFeat, layerName, pFeat.Shape);
                            }
                        }
                        else
                        {
                            outGeometry = null;
                            try
                            {
                                ClipFeature(pFeat,pFeatureClass.ShapeType,pRegion as IPolygon,out outGeometry);
                                if (outGeometry != null) this.Read_Entity_0(pFeat, layerName, outGeometry);
                            }
                            catch (System.Exception ex)
                            {
                            	
                            }
                        }
                        pFeat = pFeaCur.NextFeature();
                    }
                }

            }
        }
        #region 读取实体
        /// <summary>
        /// 读取实体的内容
        /// </summary>
        /// <param name="pFeature"></param>
        /// <param name="layerName"></param>
        private void Read_Entity_0(IFeature pFeature, string layerName, IGeometry pGeometry)
        {
            string dName, dAngle = "0.0";
            string sSymId, m_LinTypeorSymbolorFont = "", m_LineWidth = "";
            //string              SMSCADLayer="";
            int fieldIndex;

            //取出源图层，和原符号
            sSymId = this.getSSymId(pFeature);
            //对比出目标图层和目标符号
            m_LinTypeorSymbolorFont = this.findSymbol(layerName, sSymId, pFeature, ref m_LineWidth);
            dName = this.findLayer(pFeature, layerName);

            if (pFeature is IAnnotationFeature)
            {
                if (pFeature.Shape != null)
                {
                    this.Read_anno(pFeature, dName, m_LinTypeorSymbolorFont, layerName);
                }
            }
            else
            {
                if (pFeature.Shape != null)
                {
                    //查找字段中有无角度字段
                    if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        fieldIndex = pFeature.Fields.FindField(strAngle);
                        if (fieldIndex > 0)
                            dAngle = pFeature.get_Value(fieldIndex).ToString();
                    }
                    this.Read_Entity(pFeature, pGeometry, m_LinTypeorSymbolorFont, dName, dAngle, sSymId, m_LineWidth, layerName);
                }
            }
        }

        /// <summary>
        /// 读实体   
        /// </summary>
        /// <param name="pGeometry"></param>
        /// <param name="strGeoObjNum"></param>
        /// <param name="strLayName"></param>
        private void Read_Entity(IFeature pfeature, IGeometry pGeometry, string strGeoObjNum, string strLayName, string dangle, string sSymId, string m_LineWidth, string layerName)
        {
            //pEnv.Union(pGeometry.Envelope);
            switch (pGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    Read_point(pfeature, pGeometry, strGeoObjNum, strLayName, dangle, layerName);
                    break;
                case esriGeometryType.esriGeometryPolyline:
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryCircularArc:
                case esriGeometryType.esriGeometryPolygon:		//填充
                    ReadPolyline(pfeature, pGeometry, strGeoObjNum, strLayName, sSymId, m_LineWidth, layerName);
                    break;
            }
        }

        #region 绘制点
        private void Read_point(IFeature pfeature, IGeometry pGeometry, string strGeoObjNum, string strLayName, string dangle, string layerName)
        {
            //PublicFun.ReadEntCount++;
            //this.ReadEvent(PublicFun.ReadEntCount,PublicFun.WrtieEntCount);

            IPoint pPoint;
            pPoint = (IPoint)pGeometry;

            string dName, dSym, dAngle = "0";
            string X, Y, Z;

            dName = strLayName;
            dSym = strGeoObjNum;
            dAngle = dangle;

            X = pPoint.X.ToString();
            Y = pPoint.Y.ToString();
            Z = pPoint.Z.ToString();
            if (Z == "非数字")
                Z = "0.0";

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = this.CurrentDs.Tables["pointTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            //tmpRow["lcolor"] = m_Color;
            tmpRow["Dirction"] = dAngle;
            tmpRow["X"] = X;
            tmpRow["Y"] = Y;
            tmpRow["Z"] = Z;
            tmpRow["AttriBute"] = getAttriBute(pfeature, layerName);

            tmpTable.Rows.Add(tmpRow);
        }

        #endregion

        #region 绘制Polyline
        /// <summary>
        /// 向dxf文件中写入线要素，将每个线要素拆分成为最小的绘图单元
        /// </summary>
        /// <param name="pGeometry"></param>
        /// <param name="strGeoObjNum"></param>
        /// <param name="strLayName"></param>
        private void ReadPolyline(IFeature pfeature, IGeometry pGeometry, string dSym, string dName, string sSymId, string m_LineWidth, string layerName)
        {
            IGeometryCollection pGeoColl;
            ISegmentCollection pSegColl;
            ISegment pSeg;
            DataTable polyLineTable;
            Guid puid = new Guid();
            int pointCount = 0, length;
            IPoint pPoint = new PointClass();
            IPoint pPoint1 = new PointClass();
            ICircularArc pCa;
            IPoint cenPt = new PointClass();
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            double fromAngle, toAngle, td = 0;
            double dirction;
            string attribute = getAttriBute(pfeature, layerName);

            polyLineTable = this.CurrentDs.Tables["polylineTable"];

            pGeoColl = (IGeometryCollection)pGeometry;
            for (int j = 0; j < pGeoColl.GeometryCount; j++)
            {
                pSegColl = (ISegmentCollection)pGeoColl.get_Geometry(j);
                puid = Guid.NewGuid();
                pointCount = 0;
                for (int i = 0; i < pSegColl.SegmentCount; i++)
                {
                    pSeg = pSegColl.get_Segment(i);
                    switch (pSeg.GeometryType)
                    {
                        case esriGeometryType.esriGeometryLine:			//Pline中的Line		
                            AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pSeg.FromPoint, pSeg.ToPoint, 0, m_LineWidth, attribute);
                            break;
                        case esriGeometryType.esriGeometryCircularArc:	//Pline中的Arc	圆弧曲线
                            pCa = (ICircularArc)pSeg;
                            cenPt = pCa.CenterPoint;
                            fromPt = pCa.FromPoint;
                            toPt = pCa.ToPoint;

                            if (pCa.IsCounterClockwise)
                            {
                                fromAngle = pCa.FromAngle;
                                toAngle = pCa.ToAngle;
                                dirction = 1;
                            }
                            else
                            {
                                fromAngle = pCa.ToAngle;
                                toAngle = pCa.FromAngle;
                                dirction = -1;
                            }
                            try
                            {
                                //PublicFun.GetARC_u(cenPt,fromPt,toPt,fromAngle,toAngle,ref td);				
                                //转换弧
                                if (pCa.IsClosed && cenPt != null)	//独立的圆	
                                {
                                    fromPt.X = cenPt.X;
                                    fromPt.Y = cenPt.Y - pCa.Radius;
                                    toPt.X = cenPt.X;
                                    toPt.Y = cenPt.Y + pCa.Radius;
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, fromPt, toPt, 1 * dirction, m_LineWidth, attribute);
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, toPt, fromPt, 1 * dirction, m_LineWidth, attribute);
                                }
                                else						//polyLine中的弧
                                {
                                    if (cenPt == null) td = 0;
                                    else PublicFun.GetARC_u(cenPt, fromPt, toPt, fromAngle, toAngle, dirction, ref td);
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, fromPt, toPt, td, m_LineWidth, attribute);
                                }
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                            break;
                        case esriGeometryType.esriGeometryBezier3Curve:	//贝塞尔曲线
                            if (pSeg.Length <= 1)  //如果线长度小于1m按直线处理
                            {
                                AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pSeg.FromPoint, pSeg.ToPoint, 0, m_LineWidth, attribute);
                            }
                            else  //否则长度大于1m要内插点
                            {
                                length = 0;
                                //处理贝塞尔曲线的点（每隔1米内插一个点）
                                while (length < pSeg.Length - 1)
                                {
                                    pSeg.QueryPoint(esriSegmentExtension.esriExtendAtFrom, length, false, pPoint);
                                    pSeg.QueryPoint(esriSegmentExtension.esriExtendAtFrom, length + 1, false, pPoint1);

                                    if (length != 0)
                                        pPoint.Z = pSeg.FromPoint.Z;
                                    pPoint1.Z = pSeg.FromPoint.Z;

                                    length = length + 1;
                                    AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pPoint, pPoint1, 0, m_LineWidth, attribute);
                                }
                                //贝塞尔曲线部分终点信息
                                pSeg.QueryPoint(esriSegmentExtension.esriExtendAtFrom, length, false, pPoint);
                                pPoint.Z = 0;
                                AddToPolyLineTable(polyLineTable, puid, pointCount++, dName, dSym, sSymId, pPoint, pSeg.ToPoint, 0, m_LineWidth, attribute);
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 添加到多段线表中
        /// </summary>
        /// <param name="polyLineTable"></param>
        /// <param name="puid"></param>
        /// <param name="pointCount"></param>
        /// <param name="dName"></param>
        /// <param name="dSym"></param>
        /// <param name="sSym"></param>
        /// <param name="fromPt"></param>
        /// <param name="toPt"></param>
        /// <param name="td"></param>
        /// <param name="rwidth"></param>
        /// <param name="rcolor"></param>
        private void AddToPolyLineTable(DataTable polyLineTable, Guid puid, int pointCount, string dName, string dSym, string sSymId, IPoint fromPt, IPoint toPt, double td, string rwidth, string attribute)
        {
            DataRow tmpRow;
            tmpRow = polyLineTable.NewRow();
            tmpRow["plId"] = puid;
            tmpRow["plIndex"] = pointCount;
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["SMSSymbol"] = sSymId;  //2007.08.15  TianK 添加 用于导出编码
            tmpRow["beginX"] = fromPt.X.ToString();
            tmpRow["beginY"] = fromPt.Y.ToString();
            tmpRow["beginZ"] = fromPt.Z.ToString();
            if (tmpRow["beginZ"].ToString() == "非数字")
                tmpRow["beginZ"] = "0.0";
            tmpRow["endX"] = toPt.X.ToString();
            tmpRow["endY"] = toPt.Y.ToString();
            tmpRow["endZ"] = toPt.Z.ToString();
            if (tmpRow["endZ"].ToString() == "非数字")
                tmpRow["endZ"] = "0.0";
            tmpRow["td"] = td.ToString();
            if (tmpRow["td"].ToString() == "非数字")
                tmpRow["td"] = "0.0";
            //tmpRow["lcolor"] = rcolor;
            tmpRow["lwidth"] = rwidth;
            tmpRow["AttriBute"] = attribute;

            polyLineTable.Rows.Add(tmpRow);
        }

        #endregion

        #region 写注记图层
        /// <summary>
        /// 写注记图层
        /// </summary>
        /// <param name="pFeat"></param>
        private void Read_anno(IFeature pFeat, string strDXFLayName, string symbolId, string layerName)
        {
            string dName, DifGISCode;
            string strAngle, sHeight, sScale, sFontName;
            int fieldIndex;

            dName = strDXFLayName;
            DifGISCode = symbolId;

            IAnnotationFeature pAnno;
            IGeometry pShape;
            IPolygon pPoly;

            pAnno = (IAnnotationFeature)pFeat;
            pShape = pFeat.ShapeCopy;
            pPoly = (IPolygon)pShape;

            IElement pElem;
            ITextElement pTextEl;

            pElem = pAnno.Annotation;

            if (pElem is ITextElement)
            {
                //PublicFun.ReadEntCount++;
                //this.ReadEvent(PublicFun.ReadEntCount,PublicFun.WrtieEntCount);

                pTextEl = (ITextElement)pElem;
                IAnnoClass pAnnoClass;
                pAnnoClass = (IAnnoClass)pFeat.Class.Extension;

                IPointCollection pPoints;
                pPoints = (IPointCollection)pFeat.ShapeCopy;

                if (pPoints.PointCount > 0 && pTextEl.Text != "")
                {
                    double dAngle, dHeight;

                    dHeight = pTextEl.Symbol.Size;

                    fieldIndex = pFeat.Fields.FindField("angle");
                    if (fieldIndex > 0)
                    {
                        strAngle = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else strAngle = "0";
                    dAngle = double.Parse(strAngle);

                    fieldIndex = pFeat.Fields.FindField("SHeight");                //2009.9.20 TianK  添加
                    if (fieldIndex > 0)
                    {
                        sHeight = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else sHeight = "";

                    fieldIndex = pFeat.Fields.FindField("SScale");                //2009.9.20 TianK  添加
                    if (fieldIndex > 0)
                    {
                        sScale = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else sScale = "";

                    fieldIndex = pFeat.Fields.FindField("SFontName");                //2009.9.20 TianK  添加
                    if (fieldIndex > 0)
                    {
                        sFontName = pFeat.get_Value(fieldIndex).ToString();
                    }
                    else sFontName = "";

                    DataTable tmpTable;
                    DataRow tmpRow;

                    tmpTable = this.CurrentDs.Tables["textTable"];
                    tmpRow = tmpTable.NewRow();
                    tmpRow["dLayer"] = dName;
                    tmpRow["DifGISCode"] = DifGISCode;
                    //tmpRow["lcolor"] = m_color;
                    tmpRow["X1"] = pPoints.get_Point(0).X.ToString();
                    tmpRow["Y1"] = pPoints.get_Point(0).Y.ToString();
                    tmpRow["X2"] = pPoints.get_Point(pPoints.PointCount - 2).X.ToString();
                    tmpRow["Y2"] = pPoints.get_Point(pPoints.PointCount - 2).Y.ToString();
                    tmpRow["Dirction"] = dAngle.ToString();
                    tmpRow["dHeight"] = dHeight.ToString();
                    tmpRow["SHeight"] = sHeight;                 //2009.9.20 TianK  添加
                    tmpRow["SScale"] = sScale;                 //2009.9.20 TianK  添加
                    tmpRow["SFontName"] = sFontName;                 //2009.9.20 TianK  添加
                    tmpRow["text"] = pTextEl.Text;
                    tmpRow["AttriBute"] = getAttriBute(pFeat, layerName);

                    tmpTable.Rows.Add(tmpRow);
                }
            }
        }
        #endregion

        /// <summary>
		/// 遍历workspace返回名字为layerStr的图层
		/// </summary>
		/// <param name="layerStr"></param>
		/// <returns></returns>
		private IFeatureClass getFeatureClass(string layerStr)
		{
			IFeatureClass pFeaClass=null;
			IFeatureWorkspace pWs;
			
			pWs=(IFeatureWorkspace)inputWorkspace;

			int indexOfDot = layerStr.IndexOf(".");
			layerStr = layerStr.Substring(indexOfDot+1,layerStr.Length-indexOfDot-1);

			pFeaClass=pWs.OpenFeatureClass(layerStr);

			return pFeaClass;
		}
        #endregion

        private IGeometry createRect(IArray ptArr)
        {
            object o = Type.Missing;
            IPointCollection pPC;
            ITopologicalOperator pTopo;
            pPC = new PolygonClass();
            IGeometry pGeo = null;
            for (int i = 0; i < ptArr.Count;i++ )
            {
                IPoint pt = new PointClass();
                pt = (IPoint)ptArr.get_Element(i);
                pPC.AddPoint(pt, ref o, ref o);
            }
            pTopo = pPC as ITopologicalOperator;
            pTopo.Simplify();
            pGeo = pTopo as IGeometry;
            return pGeo;

        }

        private bool ClipFeature(IFeature inFeature, esriGeometryType geometryType, IPolygon clipPolygon, out IGeometry outGeometry)
        {
            bool returnValue = false;
            int index;
            string GeoObjNum = "";
            IGeometry resultGeometry = null;
            try
            {
                index = inFeature.Fields.FindField(GEOOBJNUM);
                if (index != -1)
                {
                    GeoObjNum = inFeature.get_Value(index).ToString();
                    returnValue = ClipGeometry(inFeature.ShapeCopy, geometryType, clipPolygon, out resultGeometry, GeoObjNum);
                }
            }
            catch (System.Exception ex)
            {
                outGeometry = null;
                return false;
            }
            outGeometry = resultGeometry;
            return returnValue;
        }

        /// <summary>
        /// 裁切一个几何对象
        /// </summary>
        /// <param name="inGeometry">传入要被裁切的几何对象</param>
        /// <param name="geometryType">几何对象的类型</param>
        /// <param name="clipPolygon">用于裁切的多边形</param>
        /// <param name="outGeometry">付出裁切后的几何对象</param>
        /// <returns>是否有被裁切的对象</returns>
        private bool ClipGeometry(IGeometry inGeometry, esriGeometryType geometryType, IPolygon clipPolygon, out IGeometry outGeometry, string GeoObjNum)
        {
            if (inGeometry == null)
            {
                outGeometry = null;
                return false;
            }
            IPolycurve pPolyCurve;

            bool returnValue = false;
            IGeometry tempGeometry = null;
            IGeometry pGeo = null, pGeoOut = null;
            ITopologicalOperator topologyOper = clipPolygon as ITopologicalOperator;
            topologyOper.Simplify();
            IRelationalOperator relationOper = clipPolygon as IRelationalOperator;

            IZAware zIn = inGeometry as IZAware;
            IMAware mAware;
            IPoint pPoint, pPoint1;
            IPointCollection pPtC;
            ICurve pCurve;
            IGeometryCollection pGeoC;
            IGeometryCollection pGeoCollection;
            bool HasM = true;

            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置
            IPath pPath;
            IPointCollection pPointC;
            IPoint P1, P2;

            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    //如果点包含在多边形中
                    if (relationOper.Contains(inGeometry))
                    {
                        tempGeometry = inGeometry;
                    }
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    //'如果多义线穿越多边形或包含在多边形中
                    if (relationOper.Contains(inGeometry))
                        tempGeometry = inGeometry;
                    else //if(relationOper.Crosses(inGeometry))    2008.1.29 TianK 注释掉  解决了裁切丢数据的问题
                    {
                        pPolyCurve = inGeometry as IPolycurve;
                        pPolyCurve.Generalize(0.01);

                        #region 有方向的线性地物
                        if (DIRECTIONLINE.IndexOf(GeoObjNum) != -1 && GeoObjNum != "")     //如果是有方向的线性地物
                        {
                            pHitTest = (IHitTest)inGeometry;
                            mAware = inGeometry as IMAware;                   ////2008.1.27TianK 修改 确保裁切后的线不反向
                            if (mAware.MAware == true)
                            {
                                HasM = true;
                                pPtC = inGeometry as IPointCollection;
                                for (int i = 0; i < pPtC.PointCount; i++)
                                {
                                    pPoint = pPtC.get_Point(i);
                                    pPoint.M = i;
                                    pPtC.UpdatePoint(i, pPoint);
                                }
                            }
                            else
                            {
                                HasM = false;
                                mAware.MAware = true;
                                pPtC = inGeometry as IPointCollection;
                                for (int i = 0; i < pPtC.PointCount; i++)
                                {
                                    pPoint = pPtC.get_Point(i);
                                    pPoint.M = i;
                                    pPtC.UpdatePoint(i, pPoint);
                                }
                            }
                            tempGeometry = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry1Dimension);
                            ////2008.1.27TianK 修改 确保裁切后的线不反向
                            mAware = tempGeometry as IMAware;
                            mAware.MAware = true;
                            pGeoC = tempGeometry as IGeometryCollection;
                            for (int j = 0; j < pGeoC.GeometryCount; j++)
                            {
                                pPtC = pGeoC.get_Geometry(j) as IPointCollection;
                                pPoint = pPtC.get_Point(0);
                                pPoint1 = pPtC.get_Point(pPtC.PointCount - 1);
                                if (double.IsNaN(pPoint.M))
                                {
                                    if (pHitTest.HitTest(pPoint, 0.01, esriGeometryHitPartType.esriGeometryPartBoundary,
                                        hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                                    {
                                        pPath = (IPath)(inGeometry as IGeometryCollection).get_Geometry(partIndex);
                                        pPointC = (IPointCollection)pPath;//接口跳转

                                        P1 = pPointC.get_Point(segmentIndex);
                                        P2 = pPointC.get_Point(segmentIndex + 1);
                                        AddM_P1_P2(P1, P2, ref pPoint);
                                        pPtC.UpdatePoint(0, pPoint);
                                    }
                                }
                                if (double.IsNaN(pPoint1.M))
                                {
                                    if (pHitTest.HitTest(pPoint1, 0.01, esriGeometryHitPartType.esriGeometryPartBoundary,
                                        hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                                    {
                                        pPath = (IPath)(inGeometry as IGeometryCollection).get_Geometry(partIndex);
                                        pPointC = (IPointCollection)pPath;//接口跳转

                                        P1 = pPointC.get_Point(segmentIndex);
                                        P2 = pPointC.get_Point(segmentIndex + 1);
                                        AddM_P1_P2(P1, P2, ref pPoint1);
                                        pPtC.UpdatePoint(pPtC.PointCount - 1, pPoint1);
                                    }
                                }
                                pPoint = pPtC.get_Point(0);
                                pPoint1 = pPtC.get_Point(pPtC.PointCount - 1);
                                if (pPoint.M >= pPoint1.M)
                                {
                                    pCurve = pGeoC.get_Geometry(j) as ICurve;
                                    pCurve.ReverseOrientation();
                                }
                                if (HasM)
                                {
                                    for (int i = 0; i < pPtC.PointCount; i++)
                                    {
                                        pPoint = pPtC.get_Point(i);
                                        pPoint.M = 0;
                                        pPtC.UpdatePoint(i, pPoint);
                                    }
                                }
                                else
                                {
                                    mAware.MAware = false;
                                }
                            }
                        }
                        #endregion

                        #region    无方向的线
                        else
                        {
                            pGeoCollection = inGeometry as IGeometryCollection;
                            if (pGeoCollection.GeometryCount == 1)
                            {
                                tempGeometry = null;
                                try
                                {
                                    tempGeometry = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry1Dimension);
                                }
                                catch
                                {
                                    tempGeometry = inGeometry;
                                    IPoint pPt = (inGeometry as IPointCollection).get_Point(0);
                                    Console.WriteLine("首点坐标" + pPt.X.ToString() + "," + pPt.Y.ToString() + "的物体裁切出错！");
                                }
                            }
                            else
                            {
                                tempGeometry = new PolylineClass();
                                for (int i = 0; i < pGeoCollection.GeometryCount; i++)
                                {
                                    pGeo = new PolylineClass();
                                    (pGeo as IZAware).ZAware = true;
                                    (pGeo as ISegmentCollection).AddSegmentCollection(pGeoCollection.get_Geometry(i) as ISegmentCollection);
                                    if (pGeo != null && !pGeo.IsEmpty)
                                    {
                                        (pGeo as ITopologicalOperator).Simplify();
                                        pGeoOut = topologyOper.Intersect(pGeo, esriGeometryDimension.esriGeometry1Dimension);
                                        if (pGeoOut != null && !pGeoOut.IsEmpty)
                                        {
                                            (tempGeometry as IGeometryCollection).AddGeometryCollection(pGeoOut as IGeometryCollection);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (zIn.ZAware)
                        {
                            IZAware zOut = tempGeometry as IZAware;
                            zOut.ZAware = zIn.ZAware;

                            IZ iz = tempGeometry as IZ;
                            try
                            {
                                iz.CalculateNonSimpleZs();
                            }
                            catch
                            {
                                //该值应该传入，表示如果栽得的线如果不能插值出Z值赋一个默认值，或再次计算
                                //可以根据inGeometry计算
                                iz.SetConstantZ(0);
                            }
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                case esriGeometryType.esriGeometryPolygon:
                    //如果多边形与裁剪多边形相交或在裁剪多边形中
                    if (relationOper.Contains(inGeometry))
                        tempGeometry = inGeometry;
                    else //if( relationOper.Overlaps(inGeometry))     2008.1.29 TianK 注释掉
                    {
                        pPolyCurve = inGeometry as IPolycurve;
                        pPolyCurve.Generalize(0.03);
                        tempGeometry = topologyOper.Intersect(inGeometry, esriGeometryDimension.esriGeometry2Dimension);

                        if (zIn.ZAware)
                        {
                            IZAware zOut = tempGeometry as IZAware;
                            zOut.ZAware = zIn.ZAware;
                            IZ iz = tempGeometry as IZ;
                            try
                            {
                                iz.CalculateNonSimpleZs();
                            }
                            catch
                            {
                                //该值应该传入，表示如果栽得的线如果不能插值出Z值赋一个默认值，或再次计算
                                //可以根据inGeometry计算
                                iz.SetConstantZ(0);
                            }

                        }
                    }
                    break;
            }
            if (tempGeometry != null && !tempGeometry.IsEmpty)
            {
                returnValue = true;
            }
            outGeometry = tempGeometry;
            return returnValue;
        }
        #endregion

        #region 内部辅助函数
        /// <summary>
        /// 读出源图层的源符号名字
        /// </summary>
        /// <param name="pfea"></param>
        /// <returns></returns>
        private string getSSymId(IFeature pFeature)
        {
            int fieldIndex = 0;
            string rtnValue = "";
            //读出源图层的源符号编码
            if (pFeature is IAnnotationFeature)		//注记
                fieldIndex = pFeature.Fields.FindField(strObjNum);
            else
                fieldIndex = pFeature.Fields.FindField(strObjNum);

            if (fieldIndex <= -1)
                rtnValue = "";
            else
            {
                if (pFeature.get_Value(fieldIndex) == System.DBNull.Value)
                    rtnValue = "";
                else
                    rtnValue = ((object)pFeature.get_Value(fieldIndex)).ToString();
            }
            return rtnValue;
        }

        /// <summary>
        /// 在图层对照表中查找对应的图层  //2009.03.21 TianK 修改
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="findField"></param>
        /// <returns></returns>
        private string findLayer(IFeature pFea, string sourceName)
        {
            string pSql = "", rtnValue = "", m_Color = "", strSMSCode = ""; //2007.06.06 TianK 修改
            DataTable tmpDt = currentDs.Tables["layerTable"];
            DataRow[] secTable;

            int index;
            bool find = false;
            index = pFea.Fields.FindField("SMSCode");
            if (index <= -1)
            {
                find = false;
            }
            else
            {
                strSMSCode = pFea.get_Value(index).ToString().Trim();
                if (strSMSCode == "")
                {
                    find = false;
                }
                else
                {
                    rtnValue = strSMSCode;
                    pSql = String.Format("CadLayer = '{0}' ", strSMSCode);
                    secTable = tmpDt.Select(pSql);
                    if (secTable.Length > 0)
                    {
                        m_Color = (string)secTable[0]["LCOLOR"];
                        find = true;
                    }
                    else
                    {
                        find = false;
                    }
                }
            }
            if (find == false)
            {
                if (strSMSCode != "")                   //2012.02.15  TianK修改  
                {
                    rtnValue = strSMSCode;
                    m_Color = "1";
                }
                else
                {
                    rtnValue = "tmpLayer";
                    m_Color = "1";
                }
            }

            rtnValue = rtnValue.Replace("、", "");          //TianK 2009.8.13   添加 
            this.addUsedLayer(rtnValue, m_Color);           //2007.06.06 TianK 添加
            return rtnValue;
        }

        /// <summary>
        /// 由原符号名通过符号对应表和符号定义文件计算出目标符号的名字，
        /// 并且将出现过的符号添加到usedsymbol表中
        private string findSymbol(string sourceLayerName, string sourceName, IFeature pFeature, ref string m_LineWidth)
        {
            //步骤0：预定义参数
            string pSql = "", rtnValue = "", tmpValue = "", tmpValue1 = "";
            string defSym = "";				//如果不符合条件缺省的符号名字
            string symTable = "DefineBlockLineTypeFont";				//符号、线形、字体在dataset中的表名
            string usedTable = "";			//在系统中用到的符号的集合
            string type = "", ctype = "", log = "";
            double width = 0;
            int fieldIndex, index;
            string LAYERPIPE = "DPARC500,FPARC500,BPARC500,HPARC500,ICPARC500,IGPARC500,IWPARC500,IOPARC500";

            m_LineWidth = "";
            //m_Color="";
            DataRow[] secTable;
            DataTable tmpDt = new DataTable();

            if (pFeature is IAnnotationFeature)
            {
                defSym = "HZTXT";
                //symTable="FONT";		//暂时没有字体定义
                usedTable = "usedFont";
                type = "4";
                ctype = "字体";
                index = pFeature.Fields.FindField("SFontName");    //Tiank   2013.11.06 添加
            }
            else
            {
                if (pFeature.Shape == null)
                    return "";
                switch (pFeature.Shape.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        defSym = "defaultBlock";
                        //symTable="BLOCK";
                        usedTable = "usedBlock";
                        type = "1";
                        ctype = "点符号";
                        index = pFeature.Fields.FindField("BlockName");    //Tiank   2013.11.06 添加
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        defSym = "Continuous";			//无面的填充符号，要用线
                        //symTable="LINETYPE";
                        usedTable = "Usedlinetype";
                        type = "2";
                        ctype = "面符号";
                        index = pFeature.Fields.FindField("LineType");    //Tiank   2013.11.06 添加
                        break;
                    default:
                        defSym = "Continuous";
                        //symTable="LINETYPE";
                        usedTable = "Usedlinetype";
                        type = "2";
                        ctype = "线符号";
                        index = pFeature.Fields.FindField("LineType");    //Tiank   2013.11.06 添加
                        break;
                }
            }
            //步骤1：原符号通过对应表对应出目标符号，如果没有对应关系则符号定义为defaultSymbol
            tmpDt = currentDs.Tables["symbolTable"];
            if (sourceName != "")   //如果原地物编码不为空则查找
            {
                pSql = "DifGISCode = '" + sourceName + "'";
                secTable = tmpDt.Select(pSql);
                if (secTable.Length > 0)
                {
                    tmpValue = (string)secTable[0]["SymbolCode"];
                    m_LineWidth = (string)secTable[0]["LWIDTH"];
                    //m_Color=(string)secTable[0]["LCOLOR"];
                    tmpValue1 = tmpValue;         //TianK  2013.11.13  修改
                }
                if (sourceLayerName.LastIndexOf(".") > -1)
                {
                    sourceLayerName = sourceLayerName.Substring(sourceLayerName.LastIndexOf(".") + 1);
                }
                if (LAYERPIPE.IndexOf(sourceLayerName) != -1)
                {
                    fieldIndex = pFeature.Fields.FindField("UPORDOWN");
                    if (fieldIndex > -1)
                        if (pFeature.get_Value(fieldIndex).ToString() == "1")
                            m_LineWidth = "0.15";
                }
                if (m_LineWidth != "")
                {
                    width = double.Parse(m_LineWidth) * 2 * mapScale;
                    m_LineWidth = width.ToString();
                }
            }
            if (index > -1 && pFeature.get_Value(index).ToString() != "")   //如果有原线型、块名或字体  Tiank   2013.11.06 添加
            {
                tmpValue = pFeature.get_Value(index).ToString();
            }
            if (tmpValue == "")   //如果没查找到对应符号则使用默认的
            {
                tmpValue = defSym;
            }

            //步骤2：由目标符号检查符号定义表该符号的定义记录，如果没有则设定为defaultsymbol
            tmpDt = currentDs.Tables[symTable];
            secTable = tmpDt.Select("ID='" + tmpValue + "' and TYPE ='" + type + "'");
            if (secTable.Length <= 0)
            {
                secTable = tmpDt.Select("ID='" + tmpValue1 + "' and TYPE ='" + type + "'");
                if (secTable.Length > 0)
                    tmpValue = tmpValue1;
                else
                {
                    tmpValue = defSym;
                }
            }
            rtnValue = tmpValue;
            //步骤3：将用过的符号添加到usedsymbol表中
            this.addUsedSymbol(usedTable, rtnValue);
            return rtnValue;
        }

        //向UsedBlock表中添加出现过的Block
        private void addUsedSymbol(string tableName, string symbolName)
        {
            DataTable tmpTable;
            DataRow tmpRow;
            DataRow[] secTable;

            tmpTable = this.CurrentDs.Tables[tableName];

            secTable = tmpTable.Select("SymbolId='" + symbolName + "'");
            if (secTable.Length == 0)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["SymbolId"] = symbolName;

                tmpTable.Rows.Add(tmpRow);
            }
        }

        //向UsedLayer表中添加出现过的图层    20007.06.06 TianK 添加
        private void addUsedLayer(string layerName, string color)
        {
            DataTable tmpTable;
            DataRow tmpRow;
            DataRow[] secTable;

            tmpTable = this.CurrentDs.Tables["UsedLayer"];

            secTable = tmpTable.Select("Layer='" + layerName + "'");
            if (secTable.Length == 0)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["Layer"] = layerName;
                tmpRow["LCOLOR"] = color;

                tmpTable.Rows.Add(tmpRow);
            }
        }

        //向UsedFont表中添加固定的四种字体  2007.08.10 TianK 添加 
        private void addUsed3Fonts()
        {
            string font;
            font = "HZTXT";
            this.addUsedSymbol("usedFont", font);
            font = "HT";
            this.addUsedSymbol("usedFont", font);
            font = "STANDARD";
            this.addUsedSymbol("usedFont", font);
            font = "SX";
            this.addUsedSymbol("usedFont", font);
        }

        //////2008.1.30 TianK 添加  用于读出SMS图层配置文件
        //private void readSYSTEMSFile()
        //{
        //    string strFilePath="";
        //    if(PublicFun.isWebService == false)
        //    {
        //        strFilePath = System.Windows.Forms.Application.StartupPath + @"\..\Support\Systems.lay";
        //    }
        //    else
        //    {
        //        strFilePath = "D:\\Style\\Systems.lay";
        //    }
        //    if(!File.Exists(strFilePath))
        //    {
        //        this.ExistSYSTEMSFile =false;
        //        return;
        //    }
        //    try
        //    {
        //        SMSSYSTEMSTable = new DataTable();			
        //        SMSSYSTEMSTable.Columns.Add("SMSCode", typeof(String));
        //        SMSSYSTEMSTable.Columns.Add("SMSCADColer", typeof(String));
        //        SMSSYSTEMSTable.Columns.Add("SMSCADLayer", typeof(String));
        //        DataRow tempRow;
        //        StreamReader sr=new StreamReader (strFilePath,System.Text.Encoding.Default );
        //        char[] charSep = {',','，'};
        //        string strInfo;
        //        string[] strCode = new string[4];
        //        strInfo=sr.ReadLine();
        //        while (strInfo!=null) 
        //        {
        //            if(strInfo.Substring (0,2)!="//")
        //            {
        //                strCode = strInfo.Split(charSep,4);
        //                if(strCode[0]!="" && strCode[2]!="" && strCode[3]!="")
        //                {
        //                    tempRow=SMSSYSTEMSTable.NewRow ();
        //                    tempRow["SMSCode"]=strCode[0];	
        //                    tempRow["SMSCADColer"]=strCode[2];
        //                    tempRow["SMSCADLayer"]=strCode[3].Replace ("、","");
        //                    SMSSYSTEMSTable.Rows.Add(tempRow);	
        //                }
        //            }
        //            strInfo = sr.ReadLine();
        //        }
        //        sr.Close();
        //        this.ExistSYSTEMSFile =true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        //        this.ExistSYSTEMSFile =false;
        //        return;
        //    }
        //}

        ////2008.1.30 TianK 添加  用于查找出SMS图层配置文件中代码对应的层颜色和层名
        //private bool findSMSLayerAndSMSColer(IFeature pFea,ref string smsCADLayer)
        //{
        //    if(this.ExistSYSTEMSFile ==false)
        //    {
        //        return false;
        //    }
        //    int index;
        //    string coler;
        //    index=pFea.Fields.FindField("SMSCode");
        //    if(index<=-1)
        //    {
        //        return false;
        //    }
        //    string strSMSCode=pFea.get_Value (index).ToString ().Trim ();
        //    if(strSMSCode=="")
        //    {
        //        return false;
        //    }
        //    DataRow [] secTable;	
        //    string pSql="SMSCode = '" + strSMSCode + "' " ;
        //    secTable=SMSSYSTEMSTable.Select(pSql);
        //    if(secTable.Length>0)
        //    {
        //        coler=(string)secTable[0]["SMSCADColer"];
        //        smsCADLayer=(string)secTable[0]["SMSCADLayer"];
        //        this.addUsedLayer(smsCADLayer,coler);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //2009.1.14 TianK 添加  用于记录读出的属性名表
        private void writeAppidTable(IFields pfields)
        {
            DataTable tmpTable;
            DataRow tmpRow;
            DataRow[] secTable;
            string attributeName;
            string attributeNameCAD;

            tmpTable = this.CurrentDs.Tables["AppID"];

            secTable = tmpTable.Select("AppID='" + GISLAYERNAME + "'");
            if (secTable.Length == 0)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["AppID"] = GISLAYERNAME;
                tmpTable.Rows.Add(tmpRow);
            }

            for (int i = 0; i < pfields.FieldCount; i++)
            {
                if (pfields.get_Field(i).Type == esriFieldType.esriFieldTypeOID ||
                    (pfields.get_Field(i).Editable
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeBlob
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeGlobalID
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeGUID
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeRaster
                    && pfields.get_Field(i).Type != esriFieldType.esriFieldTypeXML))
                {
                    attributeName = pfields.get_Field(i).Name;
                    attributeNameCAD = QureyAttribute(attributeName);   //查找对应属性    2013.03.07  TianK   添加
                    secTable = tmpTable.Select("AppID='" + attributeNameCAD + "'");
                    if (secTable.Length == 0)
                    {
                        tmpRow = tmpTable.NewRow();
                        tmpRow["AppID"] = attributeNameCAD;
                        tmpTable.Rows.Add(tmpRow);
                    }
                }
            }
        }

        //2009.1.15 TianK 添加  用于读出要素的属性
        private string getAttriBute(IFeature pFea, string layerName)
        {
            string retValue = "";
            string strValue = "";     //2012.1.10添加
            string attributeNameCAD;
            retValue = retValue + GISLAYERNAME + "&" + layerName + "&";   //2013.4.9  TianK  添加  
            for (int i = 0; i < pFea.Fields.FieldCount; i++)
            {
                if (pFea.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeOID ||
                    (pFea.Fields.get_Field(i).Editable
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGeometry
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeBlob
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGlobalID
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeGUID
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeRaster
                    && pFea.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeXML))
                {
                    attributeNameCAD = QureyAttribute(pFea.Fields.get_Field(i).Name);   //查找对应属性    2013.03.07  TianK   添加

                    if (pFea.Fields.get_Field(i).Domain is ICodedValueDomain)
                    {
                        retValue = retValue + attributeNameCAD + "&" + PublicFun.GetCodedDescriptionDomainValue(pFea.Fields.get_Field(i).Domain as ICodedValueDomain, pFea.get_Value(i).ToString()) + "&";
                    }
                    else
                    {
                        strValue = pFea.get_Value(i).ToString().Replace("\r\n", "");    //2012.1.10添加，替换掉部分属性字段里的换行符
                        retValue = retValue + attributeNameCAD + "&" + strValue + "&";
                    }
                }
            }
            if (retValue.Length > 1 && retValue.Substring(retValue.Length - 1) == "&")  //移除最后一位分隔符号
            {
                retValue = retValue.Remove(retValue.Length - 1, 1);
            }
            return retValue;
        }

        #region 由DifGIS属性名查找对应CASS属性名    2013.03.07  TianK   添加
        private string QureyAttribute(string arcgisAttribute)
        {
            string rentenString = "";
            DataTable tmpTable;
            DataRow[] secTable;

            tmpTable = this.CurrentDs.Tables["AttributeCASStoDifGIS"];
            secTable = tmpTable.Select("DifGISAttributeName = '" + arcgisAttribute.ToUpper() + "' ");
            if (secTable.Length > 0)
            {
                rentenString = (string)secTable[0]["CASSAttributeName"];
            }
            if (rentenString == "")
            {
                rentenString = arcgisAttribute;
            }
            return rentenString;
        }
        #endregion
        #region// 根据P1和P2插入一点Pm的M值  2008.1.29 TianK 添加
        private static void AddM_P1_P2(IPoint P1, IPoint P2, ref IPoint Pm)
        {
            try
            {
                double dm1 = GetDistance_P12(P1, Pm);
                double dm2 = GetDistance_P12(P2, Pm);

                Pm.M = P1.M + (P2.M - P1.M) * (dm1 / (dm1 + dm2));
            }
            catch
            {
            }
        }
        #endregion

        #region		//计算P1到P2的距离函数Point1,Point2
        /// <summary>
        /// 计算P1到P2的距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistance_P12(IPoint p1, IPoint p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double s = dx * dx + dy * dy;
            return Math.Sqrt(s);
        }
        #endregion

        #endregion

   
    }
}
