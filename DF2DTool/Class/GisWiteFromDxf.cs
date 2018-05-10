using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using System.IO;
using ESRI.ArcGIS.Carto;
using System.Text;
using System.Xml;
using DF2DTool.Interface;
using DFWinForms.Class;

namespace DF2DTool.Class
{/// <summary>
    ///
    /// </summary>

    //定义委托
    public delegate void WriteGisFromDxfHandler(int rEntCount, int wEntCount);
    //绘制多义线的细节
    public delegate void WritePlineHandler(int pCount, int nCount);

    public class GisWriteFromDxf : IGisWriteFromDxf
    {
        //读取数据的事件		
        public event WriteGisFromDxfHandler WriteEvent;
        //写入系统的状态
        //public event WriteStateHandler WriteMsgEvent;
        //结束时的处理
        //public event WriteFinishedHandler WriteFinishedEvent;
        //写入pl时的状态
        public event WritePlineHandler WritePlineEvent;

        IFeatureWorkspace pAccessWorkSpace;
        IWorkspaceEdit pWorkspaceEdit;
        object beforeObject = System.Reflection.Missing.Value;
        object afterObject = System.Reflection.Missing.Value;

        public GisWriteFromDxf()
        {
        }

        #region 对外的属性
        /// <summary>
        /// 地物字段名
        /// </summary>
        private string strObjNum;
        public string StrObjNum
        {
            set { strObjNum = value; }
        }

        /// <summary>
        /// 块符号插入角度字段
        /// </summary>
        private string strAngle;
        public string StrAngle
        {
            set { strAngle = value; }
        }

        //输出的文件名
        private string outputFileName;
        public string OutputFileName
        {
            set
            {
                outputFileName = value;
            }
        }

        //配置文件表名
        private string mdbFileName = "";
        public string MdbFileName
        {
            set
            {
                mdbFileName = value;
            }
        }

        //图层对照表
        private string layerTable = "";
        public string LayerTable
        {
            set
            {
                layerTable = value;
            }
        }

        ////MinX
        //private double minX;
        //public double MinX
        //{
        //    set{minX=value;}
        //}

        ////MinY
        //private double minY;
        //public double MinY
        //{
        //    set{minY=value;}
        //}

        ////MaxX
        //private double maxX;
        //public double MaxX
        //{
        //    set{maxX=value;}
        //}

        ////MaxY
        //private double maxY;
        //public double MaxY
        //{
        //    set{maxY=value;}
        //}

        ////MinX
        //private double precision;
        //public double Precision
        //{
        //    set{precision=value;}
        //}

        /// <summary>
        /// 日志对象
        /// </summary>
        private ConvertLog logWriter;
        public ConvertLog LogWriter
        {
            set { logWriter = value; }
        }

        private ISpatialReference mainSpr = new UnknownCoordinateSystemClass();
        ///// <summary>
        ///// 空间参考,建立要素类、注记要素类、向注记要素类写内容用的空间参考
        ///// </summary>
        //public ISpatialReference MainSpr
        //{
        //    get{return mainSpr;}
        //    set{mainSpr = value;}
        //}

        private DataSet currentDs;
        /// <summary>
        /// 操作的实体数据集
        /// </summary>
        public DataSet CurrentDs
        {
            get { return currentDs; }
            set { currentDs = value; }
        }

        //初始化数据
        public bool InitData()
        {
            string workFile = "";
            string sFile = "";
            bool isCreate = false;

            workFile = outputFileName.Substring(0, outputFileName.LastIndexOf("\\") + 1);
            sFile = outputFileName.Substring(outputFileName.LastIndexOf("\\") + 1, outputFileName.Length - outputFileName.LastIndexOf("\\") - 1);

            if (!File.Exists(outputFileName))
                isCreate = this.CreateFcDataset(workFile, sFile);
            else
                isCreate = true;

            if (isCreate)
            {
                IWorkspaceFactory pAccessWorkSpaceFactory = new AccessWorkspaceFactoryClass();
                pAccessWorkSpace = (IFeatureWorkspace)pAccessWorkSpaceFactory.OpenFromFile(workFile + sFile, 0);

                pWorkspaceEdit = (IWorkspaceEdit)pAccessWorkSpace;

                mainSpr.SetDomain(-450359962737.0495, 450359962737.0495, -450359962737.0495, 450359962737.0495);
                mainSpr.SetMDomain(-100000, 900719825474.099);
                mainSpr.SetZDomain(-100000, 900719825474.099);

                //建要素集
                this.CreateDataset();
                //新建要素表
                this.CreateFcTable();
            }
            return isCreate;
        }

        //绘制要素
        public void WriteFeacture()
        {
            try
            {
                pWorkspaceEdit.StartEditing(true);
                WaitForm.SetCaption("正在写入数据库...");
                this.WriteLine();
                this.WritePoint();
                this.WriteCircle();
                this.WriteArc();
                this.WritePolyline();
                this.WriteSpline();
                this.WritePolyGon();
                this.WriteAnno();
                //this.WriteFinishedEvent(PublicFun.ReadEntCount);
                pWorkspaceEdit.StopEditing(true);
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                return;
            }
           
        }
        #endregion

        #region 新建数据库
        private bool CreateFcDataset(string workFile, string sFile)
        {
            WaitForm.SetCaption("正在新建数据库...");
            bool isCreate = false;
            FcOpertion testTable = new FcOpertion();
            isCreate = testTable.CreateWorkspace(workFile, sFile);
            return isCreate;
        }
        #endregion

        #region 新建Dataset
        private void CreateDataset()
        {
            FcOpertion testTable = new FcOpertion();
            testTable.PAccessWorkSpace = pAccessWorkSpace;
            testTable.SPR = mainSpr;
            //读出用过的数据集
            DataTable pLTable = new DataTable();
            pLTable = currentDs.Tables["UsedDataset"];
            ////
            //pWorkspaceEdit.StartEditing(true);
            for (int i = 0; i < pLTable.Rows.Count; i++)
            {
                string dsName = (string)pLTable.Rows[i]["Dataset"];
                testTable.CreateDataset(dsName);
            }
            //pWorkspaceEdit.StopEditing(true);
        }
        #endregion

        #region 新建要素类
        private void CreateFcTable()
        {
            FcOpertion testTable = new FcOpertion();
            testTable.PAccessWorkSpace = pAccessWorkSpace;
            testTable.SPR = mainSpr;
            string layerName, dType, dsName;
            bool isExists = false;

            //二、新建要素类
            //读出用过的图层表
            DataTable pLTable = new DataTable();
            pLTable = currentDs.Tables["UsedLayer"];
            pWorkspaceEdit.StartEditing(true);

            //修改后的图层对比表			
            IFields pFields = new FieldsClass();

            for (int i = 0; i <= pLTable.Rows.Count - 1; i++)
            {
                layerName = (string)pLTable.Rows[i]["layer"];
                dsName = (string)pLTable.Rows[i]["Dataset"];
                dType = (string)pLTable.Rows[i]["layerType"];

                //定义委托
                //string msg="状态：创建图层("+layerName+")";
                //this.WriteMsgEvent(msg);
                //判断工作空间中是否包含此layer如果没有则创建
                isExists = isExistsFeactureClass(layerName);

                if (!isExists)
                {
                    testTable.TableName = layerName;
                    switch (dType)
                    {
                        case "1":
                            testTable.FeatureType = esriFeatureType.esriFTSimple;
                            testTable.GeometryType = esriGeometryType.esriGeometryPoint;
                            break;
                        case "2":
                            testTable.FeatureType = esriFeatureType.esriFTSimple;
                            testTable.GeometryType = esriGeometryType.esriGeometryPolyline;
                            break;
                        case "3":
                            testTable.FeatureType = esriFeatureType.esriFTSimple;
                            testTable.GeometryType = esriGeometryType.esriGeometryPolygon;
                            break;
                        case "4":
                            testTable.FeatureType = esriFeatureType.esriFTAnnotation;
                            break;
                    }
                    if (dType != "4")
                    {
                        if (dType == "1")
                        {
                            //添加块符号倾斜角度字段
                            string[] fArr = new string[4];
                            esriFieldType[] tArr = new esriFieldType[4];
                            fArr[0] = strAngle; tArr[0] = esriFieldType.esriFieldTypeSingle;
                            fArr[1] = "SMSCode"; tArr[1] = esriFieldType.esriFieldTypeString;
                            fArr[2] = "SMSSymbol"; tArr[2] = esriFieldType.esriFieldTypeString;
                            fArr[3] = "BlockName"; tArr[3] = esriFieldType.esriFieldTypeString;
                            pFields = testTable.CreateFields(fArr, tArr, strObjNum, FileType.Dxf);
                        }
                        else
                        {
                            string[] fArr = new string[2];
                            esriFieldType[] tArr = new esriFieldType[2];
                            fArr[0] = "SMSCode"; tArr[0] = esriFieldType.esriFieldTypeString;
                            fArr[1] = "SMSSymbol"; tArr[1] = esriFieldType.esriFieldTypeString;
                            pFields = testTable.CreateFields(fArr, tArr, strObjNum, FileType.Dxf);
                        }
                        testTable.Fields = pFields;

                        IFeatureDataset pDs = pAccessWorkSpace.OpenFeatureDataset(dsName);
                        testTable.CreateFeatureClass(pDs);
                    }
                    else
                    {
                        IFeatureDataset pDs = pAccessWorkSpace.OpenFeatureDataset(dsName);
                        testTable.CreateAnnoFeatureClass(pDs, strObjNum);
                    }
                }
            }
            pWorkspaceEdit.StopEditing(true);
        }

        #endregion

        #region 判断工作空间中是否有指定的featureClass
        private bool isExistsFeactureClass(string strFc)
        {
            bool isExists = false;

            //遍历featureclass
            IEnumDatasetName enumDatasetName = ((IWorkspace)pAccessWorkSpace).get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                if (datasetName.Name == strFc)
                    isExists = true;

                datasetName = enumDatasetName.Next();
            }

            //遍历featuredataset
            enumDatasetName = ((IWorkspace)pAccessWorkSpace).get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                IFeatureDatasetName featureDatasetName = (IFeatureDatasetName)datasetName;
                IEnumDatasetName enumDatasetNameFC = featureDatasetName.FeatureClassNames;
                IDatasetName datasetNameFC = enumDatasetNameFC.Next();
                while (datasetNameFC != null)
                {
                    if (datasetNameFC.Name == strFc)
                        isExists = true;

                    datasetNameFC = enumDatasetNameFC.Next();
                }
                datasetName = enumDatasetName.Next();
            }

            return isExists;
        }
        #endregion

        #region 绘制要素

        #region 绘制点和insert
        public void WritePoint()
        {
            string dLayer, dSym, dAngle, smsCode, smsSymbol, strSymbolName = "";
            double X, Y, Z;
            string pBlock;       //2009.9.16   TianK 添加
            IFeatureClass pFeaC;
            IFeature pFea;

            DataTable lineTable = new DataTable();
            DataRow pRow;
            lineTable = currentDs.Tables["pointTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                //记录写入的实体个数
                PublicFun.WrtieEntCount++;
                //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                pRow = lineTable.Rows[i];
                dLayer = (string)lineTable.Rows[i]["dLayer"];
                dLayer = dLayer.Replace("PLY", "PT");                               //2008.10.6 TianK  添加
                dLayer = dLayer.Replace("ARC", "PT");                               //2008.10.6 TianK  添加
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                dAngle = (string)lineTable.Rows[i]["Dirction"];
                smsCode = (string)lineTable.Rows[i]["SMSCode"];
                smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                pBlock = (string)lineTable.Rows[i]["BlockName"];               //2009.9.16   TianK 添加

                X = double.Parse((string)lineTable.Rows[i]["X"]);
                Y = double.Parse((string)lineTable.Rows[i]["Y"]);
                Z = double.Parse((string)lineTable.Rows[i]["Z"]);
                if (Z <= -999 || Z >= 8848)
                    Z = 0;        //2008.03.03 TianK 添加  使无高点高程为0 

                try
                {
                    pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                    pFea = pFeaC.CreateFeature();
                    //写入属性数据
                    SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加

                    if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                    if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                    if (dAngle != "" && pFea.Fields.FindField(strAngle) >= 0)                         //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField(strAngle), Single.Parse(dAngle));
                    if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                    if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());
                    if (pBlock.Trim() != "" && pFea.Fields.FindField("BlockName") >= 0)          //2009.9.16   TianK 添加
                        pFea.set_Value(pFea.Fields.FindField("BlockName"), pBlock.Trim());
                    if (pFea.Fields.FindField("Altitude") >= 0)                                  //2007.7.28 TianK 添加条件
                        pFea.set_Value(pFea.Fields.FindField("Altitude"), Z);

                    /////
                    IPoint pPoint = new PointClass();
                    IZAware pz = (IZAware)pPoint;
                    pz.ZAware = true;
                    pPoint.X = X;
                    pPoint.Y = Y;
                    pPoint.Z = Z;

                    pFea.Shape = pPoint;
                    pFea.Store();
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入点错误：X=" + X.ToString() + " Y=" + Y.ToString() + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region 写入线要素
        public void WriteLine()
        {
            string dLayer, dSym, smsCode, smsSymbol, strSymbolName = "";
            double beginX, beginY, beginZ, endX, endY, endZ;//dWidth
            string pLineType;                      //2009.9.16   TianK 添加

            IFeatureClass pFeaC;
            IFeature pFea;

            DataTable lineTable = new DataTable();
            DataRow pRow;
            lineTable = currentDs.Tables["lineTable"];
            for (int i = 0; i < lineTable.Rows.Count; i++)
            {
                //记录写入的实体个数
                PublicFun.WrtieEntCount++;
                //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                pRow = lineTable.Rows[i];
                dLayer = (string)lineTable.Rows[i]["dLayer"];

                dLayer = dLayer.Replace("PLY", "ARC");                               //2008.10.6 TianK  添加
                dLayer = dLayer.Replace("PT", "ARC");                               //2008.10.6 TianK  添加
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                smsCode = (string)lineTable.Rows[i]["SMSCode"];
                smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                pLineType = (string)lineTable.Rows[i]["LineType"];     //2009.9.16   TianK 添加
                beginX = double.Parse((string)lineTable.Rows[i]["beginX"]);
                beginY = double.Parse((string)lineTable.Rows[i]["beginY"]);
                beginZ = double.Parse((string)lineTable.Rows[i]["beginZ"]);
                endX = double.Parse((string)lineTable.Rows[i]["endX"]);
                endY = double.Parse((string)lineTable.Rows[i]["endY"]);
                endZ = double.Parse((string)lineTable.Rows[i]["endZ"]);
                if (beginZ <= -999 || beginZ >= 8848)
                    beginZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                if (endZ <= -999 || endZ >= 8848)
                    endZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 

                try
                {
                    pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                    pFea = pFeaC.CreateFeature();
                    //写入属性数据
                    SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加

                    if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                    if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                    if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                    if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());
                    if (pLineType.Trim() != "" && pFea.Fields.FindField("LineType") >= 0)          //2009.9.16   TianK 添加
                        pFea.set_Value(pFea.Fields.FindField("LineType"), pLineType.Trim());

                    ILine pLine = new LineClass();
                    ISegmentCollection pPolyline;

                    IPoint pFromPt = new PointClass();
                    IPoint pToPt = new PointClass();
                    IZAware pz = (IZAware)pFromPt;
                    pz.ZAware = true;
                    IZAware pzz = (IZAware)pToPt;
                    pzz.ZAware = true;

                    pFromPt.X = beginX; pFromPt.Y = beginY;
                    pToPt.X = endX; pToPt.Y = endY;
                    pFromPt.Z = beginZ; pToPt.Z = endZ;

                    pLine.FromPoint = pFromPt;
                    pLine.ToPoint = pToPt;

                    pPolyline = new PolylineClass();
                    pPolyline.AddSegment((ISegment)pLine, ref beforeObject, ref afterObject);

                    //添加z和m
                    IArray pPointArray = new ArrayClass();
                    pPointArray.Add(pFromPt);
                    pPointArray.Add(pToPt);
                    IGeometry pGeo = (IGeometry)pPolyline;
                    CommonFunction.AddZMValueForGeometry(ref pGeo, pPointArray);//添加M值和Z值
                    pFea.Shape = pGeo;
                    pFea.Store();
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入线错误：X=" + beginX + " Y=" + beginY + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region 绘制Circle
        public void WriteCircle()
        {
            string dLayer, dSym, smsCode, smsSymbol, strSymbolName = "";
            double X, Y, Z, radius;
            string pLineType;                      //2009.9.16   TianK 添加
            IFeatureClass pFeaC;
            IFeature pFea;
            IGeometry pGeo;

            DataTable lineTable = new DataTable();
            DataRow pRow;
            lineTable = currentDs.Tables["circleTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                //记录写入的实体个数
                PublicFun.WrtieEntCount++;
                //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                pRow = lineTable.Rows[i];
                dLayer = (string)lineTable.Rows[i]["dLayer"];
                //dLayer = dLayer.Replace("PLY", "ARC");                               //2008.10.6 TianK  添加

                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                smsCode = (string)lineTable.Rows[i]["SMSCode"];
                smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                pLineType = (string)lineTable.Rows[i]["LineType"];     //2009.9.16   TianK 添加
                X = double.Parse((string)lineTable.Rows[i]["cenX"]);
                Y = double.Parse((string)lineTable.Rows[i]["cenY"]);
                if ((string)lineTable.Rows[i]["cenZ"] == "")
                    Z = 0;
                else
                    Z = double.Parse((string)lineTable.Rows[i]["cenZ"]);
                if (Z <= -999 || Z >= 8848)
                    Z = 0;        //2008.03.03 TianK 添加  使无高点高程为0 

                radius = double.Parse((string)lineTable.Rows[i]["radius"]);

                try
                {
                    pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                    pFea = pFeaC.CreateFeature();
                    //写入属性数据
                    SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加

                    if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                    if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                    if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                    if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());

                    if (pLineType.Trim() != "" && pFea.Fields.FindField("LineType") >= 0)          //2009.9.16   TianK 添加
                        pFea.set_Value(pFea.Fields.FindField("LineType"), pLineType.Trim());

                    ICircularArc c = new CircularArcClass();
                    IPoint cenPt = new PointClass();
                    cenPt.PutCoords(X, Y);
                    cenPt.Z = 0;

                    IPoint pt = new PointClass();
                    pt.PutCoords(X + radius, Y);
                    pt.Z = Z;
                    c.PutCoords(cenPt, pt, pt, ESRI.ArcGIS.Geometry.esriArcOrientation.esriArcClockwise);

                    if (dLayer.IndexOf("ARC") >= 0 || dLayer.IndexOf("ASSI") >= 0)   //放到线图层
                    {
                        IPolyline line = new PolylineClass();
                        ISegmentCollection pcol = line as ISegmentCollection;
                        pcol.AddSegment((ISegment)c, ref beforeObject, ref afterObject);
                        pGeo = line;
                    }
                    else     //放到面图层
                    {
                        ISegmentCollection pPloyGon = new PolygonClass();
                        pPloyGon.AddSegment((ISegment)c, ref beforeObject, ref afterObject);
                        ITopologicalOperator pTopo = (ITopologicalOperator)pPloyGon;
                        pTopo.Simplify();
                        pGeo = (IGeometry)pPloyGon;
                    }
                    //设置是否包含Z值
                    SetZValue(pFea, ref pGeo);
                    pFea.Shape = (IGeometry)pGeo;
                    pFea.Store();
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入圆错误：X=" + X.ToString() + " Y=" + Y.ToString() + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region 绘制Arc
        public void WriteArc()
        {
            string dLayer, dSym, smsCode, smsSymbol, strSymbolName = "";
            double X, Y, Z, radius, beginAng, endAng;
            string pLineType;                      //2009.9.16   TianK 添加
            IFeatureClass pFeaC;
            IFeature pFea;

            DataTable lineTable = new DataTable();
            DataRow pRow;
            lineTable = currentDs.Tables["arcTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                //记录写入的实体个数
                PublicFun.WrtieEntCount++;
                //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                pRow = lineTable.Rows[i];
                dLayer = (string)lineTable.Rows[i]["dLayer"];
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                smsCode = (string)lineTable.Rows[i]["SMSCode"];
                smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                pLineType = (string)lineTable.Rows[i]["LineType"];     //2009.9.16   TianK 添加

                X = double.Parse((string)lineTable.Rows[i]["cenX"]);
                Y = double.Parse((string)lineTable.Rows[i]["cenY"]);
                if ((string)lineTable.Rows[i]["cenZ"] == "")
                    Z = 0;
                else
                    Z = double.Parse((string)lineTable.Rows[i]["cenZ"]);
                if (Z <= -999 || Z >= 8848)
                    Z = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                radius = double.Parse((string)lineTable.Rows[i]["radius"]);
                beginAng = double.Parse((string)lineTable.Rows[i]["fromAng"]);
                endAng = double.Parse((string)lineTable.Rows[i]["toAng"]);

                try
                {
                    dLayer = dLayer.Replace("PLY", "ARC");      //2013.05.07 TainK   添加
                    pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                    pFea = pFeaC.CreateFeature();
                    //写入属性数据
                    SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加

                    if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                    if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                    if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                    if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());

                    if (pLineType.Trim() != "" && pFea.Fields.FindField("LineType") >= 0)          //2009.9.16   TianK 添加
                        pFea.set_Value(pFea.Fields.FindField("LineType"), pLineType.Trim());

                    ICircularArc pArc = new CircularArcClass();
                    ISegmentCollection pPolyline;
                    IPoint cenPt = new PointClass();
                    cenPt.PutCoords(X, Y);
                    double endtobeg = 0;
                    endtobeg = endAng - beginAng;
                    if (endtobeg > 0)
                        pArc.PutCoordsByAngle(cenPt, beginAng * Math.PI / 180, (endtobeg) * Math.PI / 180, radius);
                    else
                    {
                        endtobeg = 360 + endtobeg;
                        pArc.PutCoordsByAngle(cenPt, beginAng * Math.PI / 180, (endtobeg) * Math.PI / 180, radius);
                    }

                    pPolyline = new PolylineClass();
                    pPolyline.AddSegment((ISegment)pArc, ref beforeObject, ref afterObject);

                    IArray pPointArray = new ArrayClass();
                    pPointArray.Add(cenPt);
                    IGeometry pGeo = (IGeometry)pPolyline;
                    CommonFunction.AddZMValueForGeometry(ref pGeo, pPointArray);//添加M值和Z值
                    pFea.Shape = pGeo;
                    pFea.Store();
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入弧错误：X=" + X.ToString() + " Y=" + Y.ToString() + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region 绘制Polyline
        /// <summary>
        /// 绘制Polyline
        /// </summary>
        public void WritePolyline()
        {
            string dLayer, dSym, smsCode, smsSymbol, strSymbolName = "";
            double beginX, beginY, beginZ, endX, endY, endZ, td;//dWidth
            string pUid = "", comUid = "";
            string pLineType;                      //2009.9.16   TianK 添加
            string isClose = "";
            IArray pPointArray = new ArrayClass();
            IFeatureClass pFeaC;
            IFeature pFea;
            IGeometry pGeo;
            ISegmentCollection pSegmentCollection;
            IRing pRing;

            DataTable lineTable = new DataTable();
            DataRow[] secTable;
            DataRow pRow;
            lineTable = currentDs.Tables["polylineTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                pRow = lineTable.Rows[i];
                pUid = (string)lineTable.Rows[i]["plId"];
                try
                {
                    if (pUid != comUid)
                    {
                        //记录写入的实体个数
                        PublicFun.WrtieEntCount++;
                        //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                        dLayer = (string)lineTable.Rows[i]["dLayer"];
                        dSym = (string)lineTable.Rows[i]["DifGISCode"];
                        strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                        smsCode = (string)lineTable.Rows[i]["SMSCode"];
                        smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                        pLineType = (string)lineTable.Rows[i]["LineType"];     //2009.9.16   TianK 添加

                        isClose = (string)lineTable.Rows[i]["isClose"];

                        secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");

                        #region   //放入面图层
                        if ((dSym != "" && dSym.Substring(dSym.Length - 1, 1) == "3" && secTable.Length >= 2) || dLayer.IndexOf("CON") > -1)            //2008.3.3 TianK 添加 如果是面放入面图层
                        {
                            dLayer = dLayer.Replace("ARC", "PLY");
                            pSegmentCollection = new RingClass();

                            for (int j = 0; j <= secTable.Length - 1; j++)
                            {
                                beginX = double.Parse((string)secTable[j]["beginX"]);
                                beginY = double.Parse((string)secTable[j]["beginY"]);
                                if ((string)secTable[j]["beginZ"] == "")
                                    beginZ = 0;
                                else
                                    beginZ = double.Parse((string)secTable[j]["beginZ"]);
                                endX = double.Parse((string)secTable[j]["endX"]);
                                endY = double.Parse((string)secTable[j]["endY"]);
                                if ((string)secTable[j]["endZ"] == "")
                                    endZ = 0;
                                else
                                    endZ = double.Parse((string)secTable[j]["endZ"]);
                                if (beginZ <= -999 || beginZ >= 8848)
                                    beginZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                if (endZ <= -999 || endZ >= 8848)
                                    endZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                if (((object)secTable[j]["td"]).ToString().Length == 0)
                                    td = 0;
                                else
                                    td = double.Parse((string)secTable[j]["td"]);

                                IPoint pFromPt = new PointClass();
                                IPoint pToPt = new PointClass();
                                pFromPt.X = beginX; pFromPt.Y = beginY;//pFromPt.Z=beginZ;
                                this.addZValue(ref pFromPt, beginZ);
                                pToPt.X = endX; pToPt.Y = endY;//pToPt.Z=endZ;
                                this.addZValue(ref pToPt, endZ);

                                if (td == 0)	//处理polyline中的直线
                                {
                                    ILine pLine = new LineClass();
                                    pLine.PutCoords(pFromPt, pToPt);
                                    pSegmentCollection.AddSegment((ISegment)pLine, ref beforeObject, ref afterObject);
                                }
                                else	//绘制poline中的arc
                                {
                                    if (!(endX == 0 && endY == 0))	//处理有时在dxf中polyline段的结束有一个无用的42组码的情况
                                    {
                                        IPoint midPt = new PointClass();
                                        PublicFun.GetARCMidPoint(pFromPt, td, pToPt, ref midPt);
                                        IConstructCircularArc pArc = new CircularArcClass();
                                        pArc.ConstructThreePoints(pFromPt, midPt, pToPt, false);
                                        pSegmentCollection.AddSegment((ISegment)pArc, ref beforeObject, ref afterObject);
                                    }
                                }
                                //最后一段线要判断是否闭合
                                if (j == secTable.Length - 1)
                                {
                                    if (isClose == "0")    //若闭合要添加最后一点到第一点的连线
                                    {
                                        double fromZ, toZ;
                                        IPoint pFromPtC = new PointClass();
                                        IPoint pToPtC = new PointClass();
                                        pFromPtC.X = double.Parse((string)secTable[j]["endX"]);
                                        pFromPtC.Y = double.Parse((string)secTable[j]["endY"]);
                                        if (((object)secTable[j]["zdtd"]).ToString().Length == 0)
                                            td = 0;
                                        else
                                            td = double.Parse((string)secTable[j]["zdtd"]);
                                        if ((string)secTable[j]["endZ"] == "")
                                            fromZ = 0;
                                        else
                                            fromZ = double.Parse((string)secTable[j]["endZ"]);
                                        if (fromZ <= -999 || fromZ >= 8848)
                                            fromZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                        this.addZValue(ref pFromPtC, fromZ);  // 2007.06.04 TianK 修改

                                        pToPtC.X = double.Parse((string)secTable[0]["beginX"]);
                                        pToPtC.Y = double.Parse((string)secTable[0]["beginY"]);
                                        if ((string)secTable[j]["beginZ"] == "")
                                            toZ = 0;
                                        else
                                            toZ = double.Parse((string)secTable[j]["beginZ"]);
                                        if (toZ <= -999 || toZ >= 8848)
                                            toZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                        this.addZValue(ref pToPtC, toZ);

                                        if (td == 0)	//处理polyline中的直线
                                        {
                                            ILine pLine = new LineClass();
                                            pLine.PutCoords(pFromPtC, pToPtC);
                                            pSegmentCollection.AddSegment((ISegment)pLine, ref beforeObject, ref afterObject);
                                        }
                                        else	//绘制poline中的arc
                                        {
                                            if (!(endX == 0 && endY == 0))	//处理有时在dxf中polyline段的结束有一个无用的42组码的情况
                                            {
                                                IPoint midPt = new PointClass();
                                                PublicFun.GetARCMidPoint(pFromPtC, td, pToPtC, ref midPt);
                                                IConstructCircularArc pArc = new CircularArcClass();
                                                pArc.ConstructThreePoints(pFromPtC, midPt, pToPtC, false);
                                                pSegmentCollection.AddSegment((ISegment)pArc, ref beforeObject, ref afterObject);
                                            }
                                        }
                                    }
                                }
                                pPointArray.Add(pFromPt);
                                pPointArray.Add(pToPt);
                            }

                            pRing = pSegmentCollection as IRing;
                            ((IRing)pRing).Close();
                            IGeometryCollection pPloyGon;
                            pPloyGon = new PolygonClass();
                            pPloyGon.AddGeometry((IGeometry)pRing, ref beforeObject, ref afterObject);

                            pGeo = (IGeometry)pPloyGon;
                            CommonFunction.AddZMValueForGeometry(ref pGeo, pPointArray);//添加M值和Z值

                            pGeo = (IGeometry)pPloyGon;
                            ITopologicalOperator pTopo = (ITopologicalOperator)pGeo;
                            pTopo.Simplify();
                        }
                        #endregion

                        #region   //放入线图层
                        else
                        {
                            if (dLayer.IndexOf("CON") <= -1)
                            {
                                dLayer = dLayer.Replace("PLY", "ARC");
                            }
                            pSegmentCollection = new PolylineClass();


                            for (int j = 0; j <= secTable.Length - 1; j++)
                            {
                                //写入pline是的状态
                                this.WritePlineEvent(secTable.Length, j + 1);

                                beginX = double.Parse((string)secTable[j]["beginX"]);
                                beginY = double.Parse((string)secTable[j]["beginY"]);
                                if ((string)secTable[j]["beginZ"] == "")
                                    beginZ = 0;
                                else
                                    beginZ = double.Parse((string)secTable[j]["beginZ"]);

                                endX = double.Parse((string)secTable[j]["endX"]);
                                endY = double.Parse((string)secTable[j]["endY"]);
                                if ((string)secTable[j]["endZ"] == "")
                                    endZ = 0;
                                else
                                    endZ = double.Parse((string)secTable[j]["endZ"]);
                                if (beginZ <= -999 || beginZ >= 8848)
                                    beginZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                if (endZ <= -999 || endZ >= 8848)
                                    endZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 


                                if (((object)secTable[j]["td"]).ToString().Length == 0)
                                    td = 0;
                                else
                                    td = double.Parse((string)secTable[j]["td"]);

                                IPoint pFromPt = new PointClass();
                                IPoint pToPt = new PointClass();
                                pFromPt.X = beginX; pFromPt.Y = beginY;//pFromPt.Z=beginZ;
                                this.addZValue(ref pFromPt, beginZ);
                                pToPt.X = endX; pToPt.Y = endY;//pToPt.Z=endZ;
                                this.addZValue(ref pToPt, endZ);

                                //添加点数组
                                //pPointArray.Add(pFromPt);
                                //pPointArray.Add(pToPt);
                                if (td == 0)	//处理polyline中的直线
                                {
                                    ILine pLine = new LineClass();
                                    pLine.PutCoords(pFromPt, pToPt);

                                    pSegmentCollection.AddSegment((ISegment)pLine, ref beforeObject, ref afterObject);
                                }
                                else	//绘制poline中的arc
                                {
                                    if (!(endX == 0 && endY == 0) && !(Math.Abs(beginX - endX) < 0.005 && Math.Abs(beginY - endY) < 0.005))	//处理有时在dxf中polyline段的结束有一个无用的42组码的情况
                                    {
                                        IPoint midPt = new PointClass();

                                        PublicFun.GetARCMidPoint(pFromPt, td, pToPt, ref midPt);

                                        IConstructCircularArc pArc = new CircularArcClass();
                                        pArc.ConstructThreePoints(pFromPt, midPt, pToPt, false);

                                        pSegmentCollection.AddSegment((ISegment)pArc, ref beforeObject, ref afterObject);
                                    }
                                }
                                //最后一段线要判断是否闭合
                                if (j == secTable.Length - 1)
                                {
                                    if (isClose == "0")    //若闭合要添加最后一点到第一点的连线
                                    {
                                        double fromZ, toZ;
                                        IPoint pFromPtC = new PointClass();
                                        IPoint pToPtC = new PointClass();
                                        pFromPtC.X = double.Parse((string)secTable[j]["endX"]);
                                        pFromPtC.Y = double.Parse((string)secTable[j]["endY"]);
                                        if (((object)secTable[j]["zdtd"]).ToString().Length == 0)
                                            td = 0;
                                        else
                                            td = double.Parse((string)secTable[j]["zdtd"]);
                                        if ((string)secTable[j]["endZ"] == "")
                                            fromZ = 0;
                                        else
                                            fromZ = double.Parse((string)secTable[j]["endZ"]);
                                        if (fromZ <= -999 || fromZ >= 8848)
                                            fromZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                        this.addZValue(ref pFromPtC, fromZ);  // 2007.06.04 TianK 修改

                                        pToPtC.X = double.Parse((string)secTable[0]["beginX"]);
                                        pToPtC.Y = double.Parse((string)secTable[0]["beginY"]);
                                        if ((string)secTable[j]["beginZ"] == "")
                                            toZ = 0;
                                        else
                                            toZ = double.Parse((string)secTable[j]["beginZ"]);
                                        if (toZ <= -999 || toZ >= 8848)
                                            toZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                                        this.addZValue(ref pToPtC, toZ);

                                        if (td == 0)	//处理polyline中的直线
                                        {
                                            ILine pLine = new LineClass();
                                            pLine.PutCoords(pFromPtC, pToPtC);
                                            pSegmentCollection.AddSegment((ISegment)pLine, ref beforeObject, ref afterObject);
                                        }
                                        else	//绘制poline中的arc
                                        {
                                            if (!(endX == 0 && endY == 0))	//处理有时在dxf中polyline段的结束有一个无用的42组码的情况
                                            {
                                                IPoint midPt = new PointClass();

                                                PublicFun.GetARCMidPoint(pFromPtC, td, pToPtC, ref midPt);

                                                IConstructCircularArc pArc = new CircularArcClass();
                                                pArc.ConstructThreePoints(pFromPtC, midPt, pToPtC, false);

                                                pSegmentCollection.AddSegment((ISegment)pArc, ref beforeObject, ref afterObject);
                                            }
                                        }
                                    }
                                }
                            }
                            pGeo = (IGeometry)pSegmentCollection;

                            IZAware pz = pGeo as IZAware;
                            pz.ZAware = true;
                        }
                        #endregion

                        comUid = pUid;
                        try
                        {
                            pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                            pFea = pFeaC.CreateFeature();
                            //写入属性数据
                            SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加
                            if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                                pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                            if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                                pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                            if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                                pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                            if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                                pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());

                            if (pLineType.Trim() != "" && pFea.Fields.FindField("LineType") >= 0)          //2009.9.16   TianK 添加
                                pFea.set_Value(pFea.Fields.FindField("LineType"), pLineType.Trim());

                            //设置是否包含Z值
                            SetZValue(pFea, ref pGeo);
                            pFea.Shape = pGeo;
                            pFea.Store();

                        }
                        catch (Exception ex)
                        {
                            WaitForm.Stop();
                            return;
                            //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                            //string log = "写入PolyLine错误：X=" + (string)lineTable.Rows[i]["beginX"] + " Y=" + (string)lineTable.Rows[i]["beginY"] + "<br>";
                            ////logWriter.AddErrorLog("6", log);
                        }

                    }
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入PolyLine错误：X=" + (string)lineTable.Rows[i]["beginX"] + " Y=" + (string)lineTable.Rows[i]["beginY"] + "<br>";
                    ////logWriter.AddErrorLog("6", log);
                }
            }
        }

        #endregion

        #region 绘制spline
        /// <summary>
        /// 绘制Polyline
        /// </summary>
        public void WriteSpline()
        {
            string dLayer, dSym, smsCode, smsSymbol, strSymbolName = "";
            string pUid = "", comUid = "";
            string pLineType;                      //2009.9.16   TianK 添加

            IPoint tmpPt = new PointClass();
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            IArray inArr = new ArrayClass();
            IArray outArr = new ArrayClass();
            IArray pPointArray = new ArrayClass();

            int ptCount = 10;
            IFeatureClass pFeaC;
            IFeature pFea;

            DataTable lineTable = new DataTable();
            DataRow[] secTable;
            DataRow pRow;
            lineTable = currentDs.Tables["splineTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                pRow = lineTable.Rows[i];
                pUid = (string)lineTable.Rows[i]["plId"];

                ISegmentCollection pSegmentCollection = new PolylineClass();   //2007.05.31 TianK 添加

                try
                {
                    if (pUid != comUid)
                    {
                        //记录写入的实体个数
                        PublicFun.WrtieEntCount++;
                        //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                        dLayer = (string)lineTable.Rows[i]["dLayer"];
                        dSym = (string)lineTable.Rows[i]["DifGISCode"];
                        strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                        smsCode = (string)lineTable.Rows[i]["SMSCode"];
                        smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                        pLineType = (string)lineTable.Rows[i]["LineType"];     //2009.9.16   TianK 添加

                        pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                        pFea = pFeaC.CreateFeature();
                        //写入属性数据
                        if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                        if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                        if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                        if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());

                        if (pLineType.Trim() != "" && pFea.Fields.FindField("LineType") >= 0)          //2009.9.16   TianK 添加
                            pFea.set_Value(pFea.Fields.FindField("LineType"), pLineType.Trim());

                        SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加

                        IGeometryCollection pPolyline = new PolylineClass();

                        inArr.RemoveAll();
                        secTable = lineTable.Select("plId='" + pUid + "'");
                        for (int j = 0; j <= secTable.Length - 1; j++)
                        {
                            tmpPt = new PointClass();
                            tmpPt.X = double.Parse((string)secTable[j]["X"]);
                            tmpPt.Y = double.Parse((string)secTable[j]["Y"]);
                            if ((string)secTable[j]["Z"] == "")
                                tmpPt.Z = 0;
                            else
                                tmpPt.Z = double.Parse((string)secTable[j]["Z"]);
                            if (tmpPt.Z <= -999 || tmpPt.Z >= 8848)
                                tmpPt.Z = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                            inArr.Add(tmpPt);
                        }

                        outArr.RemoveAll();
                        PublicFun.Cardinal_CADpArray(inArr, ptCount, ref outArr);
                        //绘制线                                     //2007.05.31 TianK 修改----------
                        fromPt = tmpPt = new PointClass();
                        toPt = tmpPt = new PointClass();

                        for (int j = 0; j < outArr.Count - 1; j++)
                        {
                            fromPt = (IPoint)outArr.get_Element(j);
                            toPt = (IPoint)outArr.get_Element(j + 1);
                            ILine pLineC = new LineClass();
                            pLineC.PutCoords(fromPt, toPt);
                            //添加点数组
                            pPointArray.Add(fromPt);
                            pSegmentCollection.AddSegment((ISegment)pLineC, ref beforeObject, ref afterObject);//2007.05.31 TianK 修改----------
                            comUid = pUid;
                        }
                        pPointArray.Add(toPt);               //把最后一个点添加到点数组
                        ///////////////////////////////////
                        //添加z和m			
                        IGeometry pGeo = (IGeometry)pSegmentCollection;      //2007.05.31 TianK 修改
                        CommonFunction.AddZMValueForGeometry(ref pGeo, pPointArray);//添加M值和Z值
                        pFea.Shape = pGeo;
                        pFea.Store();
                    }
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入spline错误：X=" + (string)lineTable.Rows[i]["X"] + " Y=" + (string)lineTable.Rows[i]["Y"] + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region 绘制PolyGon
        /// <summary>
        /// 绘制PolyGon
        /// </summary>
        public void WritePolyGon()
        {
            string dLayer, dSym, smsCode, smsSymbol, strSymbolName = "";
            double beginX, beginY, beginZ, endX, endY, endZ;
            string pUid = "", comUid = "";
            string pLineType;                      //2009.9.16   TianK 添加
            IArray pPointArray = new ArrayClass();
            IFeatureClass pFeaC;
            IFeature pFea;

            DataTable lineTable = new DataTable();
            DataRow[] secTable;
            DataRow pRow;
            lineTable = currentDs.Tables["hatchTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                pRow = lineTable.Rows[i];
                pUid = (string)lineTable.Rows[i]["plId"];
                try
                {
                    if (pUid != comUid)
                    {
                        comUid = pUid;
                        //记录写入的实体个数
                        PublicFun.WrtieEntCount++;
                        //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                        dLayer = (string)lineTable.Rows[i]["dLayer"];
                        dSym = (string)lineTable.Rows[i]["DifGISCode"];
                        strSymbolName = (string)lineTable.Rows[i]["SymbolName"];          //2013.11.04  TianK 添加
                        smsCode = (string)lineTable.Rows[i]["SMSCode"];
                        smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                        pLineType = (string)lineTable.Rows[i]["LineType"];     //2009.9.16   TianK 添加

                        ISegmentCollection pRing = new RingClass();
                        secTable = lineTable.Select("plId='" + pUid + "'");
                        for (int j = 0; j <= secTable.Length - 1; j++)
                        {
                            beginX = double.Parse((string)secTable[j]["beginX"]);
                            beginY = double.Parse((string)secTable[j]["beginY"]);
                            if ((string)secTable[j]["beginZ"] == "")
                                beginZ = 0;
                            else
                                beginZ = double.Parse((string)secTable[j]["beginZ"]);
                            endX = double.Parse((string)secTable[j]["endX"]);
                            endY = double.Parse((string)secTable[j]["endY"]);
                            if ((string)secTable[j]["endZ"] == "")
                                endZ = 0;
                            else
                                endZ = double.Parse((string)secTable[j]["endZ"]);
                            if (beginZ <= -999 || beginZ >= 8848)
                                beginZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 
                            if (endZ <= -999 || endZ >= 8848)
                                endZ = 0;        //2008.03.03 TianK 添加  使无高点高程为0 

                            ILine pLine = new LineClass();
                            IPoint pFromPt = new PointClass();
                            IPoint pToPt = new PointClass();
                            pFromPt.X = beginX; pFromPt.Y = beginY; pFromPt.Z = beginZ;
                            pToPt.X = endX; pToPt.Y = endY; pToPt.Z = endZ;
                            pLine.FromPoint = pFromPt;
                            pLine.ToPoint = pToPt;
                            pRing.AddSegment((ISegment)pLine, ref beforeObject, ref afterObject);
                            pPointArray.Add(pFromPt);
                            pPointArray.Add(pToPt);
                        }

                        ((IRing)pRing).Close();
                        IGeometryCollection pPloyGon;
                        pPloyGon = new PolygonClass();
                        pPloyGon.AddGeometry((IGeometry)pRing, ref beforeObject, ref afterObject);
                        IGeometry pGeo = (IGeometry)pPloyGon;
                        CommonFunction.AddZMValueForGeometry(ref pGeo, pPointArray);//添加M值和Z值

                        dLayer = dLayer.Replace("ARC", "PLY");
                        pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                        pFea = pFeaC.CreateFeature();
                        //写入属性数据
                        SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加
                        if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                        if (strSymbolName != "" && pFea.Fields.FindField("CLASSIFY") >= 0)                 //2013.11.04 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField("CLASSIFY"), strSymbolName);
                        if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                        if (smsSymbol.Trim() != "" && pFea.Fields.FindField("SMSSymbol") >= 0)          //2008.2.29 TianK 修改
                            pFea.set_Value(pFea.Fields.FindField("SMSSymbol"), smsSymbol.Trim());
                        if (pLineType.Trim() != "" && pFea.Fields.FindField("LineType") >= 0)          //2009.9.16   TianK 添加
                            pFea.set_Value(pFea.Fields.FindField("LineType"), pLineType.Trim());
                        //设置是否包含Z值
                        SetZValue(pFea, ref pGeo);
                        (pGeo as ITopologicalOperator).Simplify();

                        pFea.Shape = pGeo;
                        pFea.Store();
                    }
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入PolyGon错误：X=" + (string)lineTable.Rows[i]["beginX"] + " Y=" + (string)lineTable.Rows[i]["beginY"] + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region	写注记
        public void WriteAnno()
        {
            string dLayer, dSym, dText, smsCode, smsSymbol;
            string fontName = "黑体";
            double X, Y, dHeight, dAngle, dHeightScale;
            string dScale, dFontName;             //2009.915 TianK 增加

            IFeatureClass pFeaC;

            DataTable lineTable = new DataTable();
            DataTable symbolTable = new DataTable();
            DataRow pRow;
            lineTable = currentDs.Tables["textTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                //记录写入的实体个数
                PublicFun.WrtieEntCount++;
                //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);

                pRow = lineTable.Rows[i];
                dLayer = (string)lineTable.Rows[i]["dLayer"];
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                smsCode = (string)lineTable.Rows[i]["SMSCode"];
                smsSymbol = (string)lineTable.Rows[i]["SMSSymbol"];
                dScale = (string)lineTable.Rows[i]["SScale"];
                dFontName = (string)lineTable.Rows[i]["SFontName"];
                try
                {
                    pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                }
                catch
                {
                    dLayer = dLayer.Substring(dLayer.LastIndexOf(".") + 1);
                    pFeaC = pAccessWorkSpace.OpenFeatureClass(dLayer);
                }
                IFeature pFea = pFeaC.CreateFeature();

                IAnnotationFeature pAnno = null;
                pAnno = (IAnnotationFeature)pFea;

                ITextElement pTextElement;

                dHeight = double.Parse((string)pRow["dHeight"]);
                dHeightScale = 6;
                if (dHeight == 0.9)                         //2007.08.09 TianK 添加 如果原字高为0.9则字高比例为8.8 其它为6
                    dHeightScale = 8.8;
                if (dHeight == 1)                         //2007.08.09 TianK 添加 如果原字高为0.9则字高比例为8.8 其它为6
                    dHeightScale = 8;
                dAngle = double.Parse((string)pRow["Dirction"]);
                dText = (string)pRow["Text"];
                dText = dText.Replace("%%c", "φ");
                dText = dText.Replace("%%C", "φ");
                X = double.Parse((string)pRow["X1"]);
                Y = double.Parse((string)pRow["Y1"]);

                try
                {
                    pTextElement = PublicFun.make_Dxf_Text(fontName, dText, dAngle, dHeight * dHeightScale, X, Y);

                    //写入属性数据
                    SaveAttriBute(ref pFea, (string)lineTable.Rows[i]["AttriBute"]);          //2008.10.10 TianK 添加

                    if (dSym != "" && pFea.Fields.FindField(strObjNum) >= 0)                           //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField(strObjNum), Int32.Parse(dSym));
                    if (smsCode.Trim() != "" && pFea.Fields.FindField("SMSCode") >= 0)               //2008.2.29 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SMSCode"), smsCode.Trim());
                    if (dHeight != 0 && pFea.Fields.FindField("SHeight") >= 0)               //2009.9.15 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SHeight"), dHeight);
                    if (dScale.Trim() != "" && pFea.Fields.FindField("SScale") >= 0)               //2009.9.15 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SScale"), dScale.Trim());
                    if (dFontName.Trim() != "" && pFea.Fields.FindField("SFontName") >= 0)               //2009.9.15 TianK 修改
                        pFea.set_Value(pFea.Fields.FindField("SFontName"), dFontName.Trim());

                    pAnno.Annotation = (IElement)pTextElement;
                    pFea.Store();
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    return;
                    //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    //string log = "写入注记错误：X=" + X.ToString() + " Y=" + Y.ToString() + "<br>";
                    //logWriter.AddErrorLog("6", log);
                }
            }
        }
        #endregion

        #region 其他函数
        /// <summary>
        /// 获取字段的值
        /// </summary>
        /// <param name="fValue"></param>
        /// <returns></returns>
        private string getFieldValue(object fValue)
        {
            string rtnValue = "";
            if (fValue != System.DBNull.Value)
                rtnValue = (string)fValue;

            return rtnValue;
        }
        #endregion

        #endregion

        #region 添加Z值
        private void addZValue(ref IPoint pPoint, double tmpZ)
        {
            IZAware pZ = (IZAware)pPoint;
            pZ.ZAware = true;
            pPoint.Z = tmpZ;
        }

        #endregion

        #region 保存属性    2008.10.10  TianK   添加  2009.3.28  TianK 修改
        private void SaveAttriBute(ref IFeature pFea, string AttriBute)
        {
            if (AttriBute == "" || AttriBute == null)
            {
                return;
            }
            string attribute = AttriBute;
            if (attribute[0].ToString() == "$")    //如果有 移除开头的美元分隔符
            {
                attribute = attribute.Remove(0, 1);
            }
            if (attribute[attribute.Length - 1].ToString() == "$")   //如果有 移除结尾的美元分隔符
            {
                attribute = attribute.Remove(attribute.Length - 1, 1);
            }
            try
            {
                string[] attri;
                attri = attribute.Split(new char[] { '$' });
                int index, j;
                string attributeGIS;

                for (int i = 0; i < attri.Length - attri.Length % 4; i = i + 4)
                {
                    attributeGIS = QureyAttribute(attri[i + 1]);    //查找对应的DifGIS属性名
                    if (attributeGIS == "")   //2013.11.07 TianK  修改  如果找到匹配属性字段名用对应的匹配字段名，如果没找到就用原来的字段名
                    {
                        attributeGIS = attri[i + 1];
                    }
                    index = pFea.Fields.FindField(attributeGIS);
                    if (index >= 0 && attri[i + 3] != "" && attri[i + 3] != "<Null>")     //如果在要素类中有对应的字段，且该属性不为空，就保存该属性值
                    {
                        if (pFea.Fields.get_Field(index).Editable == true)
                        {
                            try
                            {
                                if ((pFea.Fields.get_Field(index) as IFieldEdit).Type == esriFieldType.esriFieldTypeString)
                                {
                                    if (attri[i + 3].Length <= pFea.Fields.get_Field(index).Length)
                                    {
                                        pFea.set_Value(index, attri[i + 3]);
                                    }
                                    else
                                    {
                                        pFea.set_Value(index, attri[i + 3].ToString().Substring(0, pFea.Fields.get_Field(index).Length - 1));
                                    }
                                }
                                else if ((pFea.Fields.get_Field(index) as IFieldEdit).Type == esriFieldType.esriFieldTypeSingle || (pFea.Fields.get_Field(index) as IFieldEdit).Type == esriFieldType.esriFieldTypeDouble)
                                    pFea.set_Value(index, double.Parse(attri[i + 3]));
                                else if ((pFea.Fields.get_Field(index) as IFieldEdit).Type == esriFieldType.esriFieldTypeInteger)
                                    pFea.set_Value(index, int.Parse(attri[i + 3]));
                                else if ((pFea.Fields.get_Field(index) as IFieldEdit).Type == esriFieldType.esriFieldTypeSmallInteger)
                                {
                                    if (pFea.Fields.get_Field(index).Domain != null) //&& pFea.Fields.get_Field(index).Domain.Type == esriDomainType.esriDTCodedValue)
                                    {
                                        if (CommonFunction.MatchNumber(attri[i + 3]))
                                        {
                                            pFea.set_Value(index, attri[i + 3]);
                                        }
                                        else
                                        {
                                            string code = PublicFun.GetCodedValueDomainValue(pFea.Fields.get_Field(index).Domain as ICodedValueDomain, attri[i + 3]);
                                            if (code != "<Null>")
                                                pFea.set_Value(index, int.Parse(code));
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            pFea.set_Value(index, attri[i + 3]);
                                        }
                                        catch (Exception ex)
                                        {
                                            //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                                        }

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                WaitForm.Stop();
                                return;
                                //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
                return;
                //Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                
            }
        }
        #endregion

        #region 设置是否包含Z值   //2013.10.30   TianK 添加
        private void SetZValue(IFeature pFea, ref IGeometry pGeo)
        {
            int indexShape;
            IGeometryDef pGeometryDef;

            indexShape = pFea.Fields.FindField("Shape");
            if (indexShape < 0)
            {
                indexShape = pFea.Fields.FindField("SHAPE");
            }
            if (indexShape > -1)
            {
                pGeometryDef = pFea.Fields.get_Field(indexShape).GeometryDef as IGeometryDef;
                if (pGeometryDef.HasZ)
                {
                    IZAware pZAware = (IZAware)pGeo;
                    pZAware.ZAware = true;
                }
                else
                {
                    IZAware pZAware = (IZAware)pGeo;
                    pZAware.ZAware = false;
                }
            }
        }
        #endregion

        #region 由CASS属性名查找对应DifGIS属性名    2013.03.07  TianK   添加
        private string QureyAttribute(string CASSAttribute)
        {
            string rentenString = "";
            DataTable tmpTable;
            DataRow[] secTable;

            tmpTable = this.CurrentDs.Tables["AttributeCASStoDifGIS"];
            secTable = tmpTable.Select("CASSAttributeName= '" + CASSAttribute.ToUpper() + "' ");
            if (secTable.Length > 0)
            {
                rentenString = (string)secTable[0]["DifGISAttributeName"];
            }
            if (rentenString == "")
            {
                rentenString = CASSAttribute;
            }
            return rentenString;
        }
        #endregion

    }
    
}
