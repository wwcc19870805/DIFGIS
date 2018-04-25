using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using DF2DFileConvert.Class;

namespace DF2DFileConvert.Class
{
    //转换文件传入的数据类型
    public enum FileType { Dxf, CASS, SMS };

    /// <summary>
    ///  创建属性表的通用类
    /// </summary>
    public class FcOpertion
    {
        public FcOpertion()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 对外的属性

        private IFeatureWorkspace pAccessWorkSpace = null;
        /// <summary>
        /// 工作空间
        /// </summary>
        public IFeatureWorkspace PAccessWorkSpace
        {
            get { return pAccessWorkSpace; }
            set { pAccessWorkSpace = value; }
        }

        private string tableName = "";
        /// <summary>
        /// 新建的表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private ISpatialReference sPR = new UnknownCoordinateSystemClass();
        /// <summary>
        /// 空间参考
        /// </summary>
        public ISpatialReference SPR
        {
            get { return sPR; }
            set { sPR = value; }
        }

        private esriFeatureType featureType = esriFeatureType.esriFTSimple;
        /// <summary>
        /// 要素类型
        /// </summary>
        public esriFeatureType FeatureType
        {
            get { return featureType; }
            set { featureType = value; }
        }

        private esriGeometryType geometryType = esriGeometryType.esriGeometryPoint;
        /// <summary>
        /// 几何类型
        /// </summary>
        public esriGeometryType GeometryType
        {
            get { return geometryType; }
            set { geometryType = value; }
        }

        private IFields fields = null;
        /// <summary>
        /// 字段集合
        /// </summary>
        public IFields Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        private UID uidCLSID = null;
        public UID UidCLSID
        {
            get { return uidCLSID; }
            set { uidCLSID = value; }
        }

        private UID uidCLSEXT = null;
        public UID UidCLSEXT
        {
            get { return uidCLSEXT; }
            set { uidCLSEXT = value; }
        }

        private string configWord = "";
        public string ConfigWord
        {
            get { return configWord; }
            set { configWord = value; }
        }

        #endregion

        #region 建字段集
        /// <summary>
        /// 建字段集
        /// 说明：每个要素集都有两个默认的字段：OBJECTID,SHAPE
        /// 其他的字段由FieldArr[]传入,fieldArr是用户附加的字段
        /// </summary>
        /// <param name="fieldArr"></param>
        /// <returns></returns>
        public IFields CreateFields(string[] fieldArr, esriFieldType[] tArr, string strObjNum, FileType ft)
        {
            #region 字段定义的固定部分
            //以上是每个要素集固定的字段数
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;
            if (fieldArr != null)
                pFieldsEdit.FieldCount_2 = 3 + fieldArr.Length;
            else
                pFieldsEdit.FieldCount_2 = 3;

            IFieldEdit pFieldEdit;
            IField pFieldOID = new FieldClass();
            pFieldEdit = (IFieldEdit)pFieldOID;

            pFieldEdit.Editable_2 = true;
            pFieldEdit.Name_2 = "OBJECTID";
            pFieldEdit.AliasName_2 = "要素ID号";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            pFieldEdit.IsNullable_2 = false;
            pFieldsEdit.set_Field(0, pFieldEdit);

            IGeometryDef pGeomDef = new GeometryDefClass();
            IGeometryDefEdit pGeomDefEdit;
            pGeomDefEdit = (IGeometryDefEdit)pGeomDef;
            pGeomDefEdit.GeometryType_2 = geometryType;		//变量
            pGeomDefEdit.GridCount_2 = 1;
            pGeomDefEdit.set_GridSize(0, 2000);
            //pGeomDefEdit.AvgNumPoints_2 =2;	
            if (ft == FileType.SMS)
            {
                pGeomDefEdit.HasM_2 = true;
                pGeomDefEdit.HasZ_2 = true;
            }
            else
            {
                pGeomDefEdit.HasM_2 = false;
                pGeomDefEdit.HasZ_2 = true;
            }
            pGeomDefEdit.SpatialReference_2 = sPR;

            IField pFieldGeom = new FieldClass();
            pFieldEdit = (IFieldEdit)pFieldGeom;
            pFieldEdit.Name_2 = "SHAPE";
            pFieldEdit.AliasName_2 = "坐标信息";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.GeometryDef_2 = pGeomDef;
            pFieldsEdit.set_Field(1, pFieldGeom);
            //存放符号编码的字段
            IField pFieldExtra;
            pFieldExtra = new FieldClass();
            pFieldEdit = (IFieldEdit)pFieldExtra;
            pFieldEdit.Name_2 = strObjNum;
            pFieldEdit.AliasName_2 = "地物编码";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            pFieldEdit.IsNullable_2 = true;
            pFieldsEdit.set_Field(2, pFieldExtra);
            //固定字段定义结束
            #endregion
            //以下为用户指定的字段内容 从3+i开始			
            if (fieldArr != null)
            {
                for (int i = 0; i <= fieldArr.Length - 1; i++)
                {
                    pFieldExtra = new FieldClass();
                    pFieldEdit = (IFieldEdit)pFieldExtra;
                    pFieldEdit.Name_2 = fieldArr[i];
                    pFieldEdit.Type_2 = tArr[i];
                    pFieldEdit.IsNullable_2 = true;
                    pFieldsEdit.set_Field(3 + i, pFieldExtra);
                }
            }
            //
            return pFields;
        }
        #endregion

        #region 建要素表

        /// <summary>
        /// 建要素表
        /// </summary>
        /// <returns></returns>
        public IFeatureClass CreateFeatureClass(IFeatureDataset featureDs)
        {
            if (pAccessWorkSpace == null)
            {
                throw (new Exception("[pAccessWorkSpace] cannot be null"));
            }
            if (!((pAccessWorkSpace is IWorkspace) ||
                (pAccessWorkSpace is IFeatureDataset)))
            {
                throw (new Exception("[pAccessWorkSpace] must be IWorkspace or IFeatureDataset"));
            }
            if (tableName == "")
            {
                throw (new Exception("[tableName] cannot be empty"));
            }
            if ((pAccessWorkSpace is IWorkspace) && (sPR == null))
            {
                throw (new Exception("[spatialReference] cannot be null for StandAlong FeatureClasses"));
            }

            // Locate Shape Field
            string stringShapeFieldName = "SHAPE";

            IFeatureClass featureClass = null;

            if (featureDs == null)
            {
                // Create a STANDALONE FeatureClass
                IWorkspace workspace = (IWorkspace)pAccessWorkSpace;
                featureClass = CreateStandaloneFeatureClass(workspace, tableName, fields, featureType, stringShapeFieldName);
            }
            else
            {
                IFeatureDataset featureDataset = featureDs;
                featureClass = CreateFeatureDatasetFeatureClass(featureDataset, tableName, fields, featureType, stringShapeFieldName);
            }
            // Return FeatureClass
            return featureClass;
        }

        public IFeatureClass CreateStandaloneFeatureClass(IWorkspace workspace, String featureClassName, IFields fieldsCollection, esriFeatureType featureType, String shapeFieldName)
        {
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            IFeatureClassDescription fcDesc = new FeatureClassDescriptionClass();
            IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;

            // Use IFieldChecker to create a validated fields collection.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = workspace;
            fieldChecker.Validate(fieldsCollection, out enumFieldError, out validatedFields);

            // The enumFieldError enumerator can be inspected at this point to determine 
            // which fields were modified during validation.
            IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, ocDesc.InstanceCLSID,
              ocDesc.ClassExtensionCLSID, esriFeatureType.esriFTSimple, shapeFieldName, "");
            return featureClass;
        }

        public IFeatureClass CreateFeatureDatasetFeatureClass(IFeatureDataset featureDataset, String featureClassName, IFields fieldsCollection, esriFeatureType featureType, String shapeFieldName)
        {
            IFeatureClassDescription fcDesc = new FeatureClassDescriptionClass();
            IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;

            // Use IFieldChecker to create a validated fields collection.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = featureDataset.Workspace;
            fieldChecker.Validate(fieldsCollection, out enumFieldError, out  validatedFields);

            // The enumFieldError enumerator can be inspected at this point to determine 
            // which fields were modified during validation.
            IFeatureClass featureClass = featureDataset.CreateFeatureClass(featureClassName, validatedFields, ocDesc.InstanceCLSID,
              ocDesc.ClassExtensionCLSID, esriFeatureType.esriFTSimple, fcDesc.ShapeFieldName, "");
            return featureClass;
        }

        #endregion

        #region 创建注记类
        public void CreateAnnoFeatureClass(IFeatureDataset featureDs, string strObjNum)
        {
            //更换注记字体 用两个重要的属性
            //IAnnotateLayerPropertiesCollection,ISymbolCollection
            IFeatureWorkspace pFeatureWs = pAccessWorkSpace;

            IFeatureWorkspaceAnno pFeatureWorkspaceAnno;
            pFeatureWorkspaceAnno = (IFeatureWorkspaceAnno)pFeatureWs;

            //说明：要控制图层中的字体必须在图层建立的时候定义好字体
            ITextSymbol pTextSymbol, tTextSymbol;
            pTextSymbol = PublicFun.makeTextSymbol("Arial", 16);
            tTextSymbol = PublicFun.makeTextSymbol("宋体", 50);
            /////textsymbol

            IGraphicsLayerScale pGLS;
            pGLS = new GraphicsLayerScaleClass();
            pGLS.Units = esriUnits.esriMeters;
            pGLS.ReferenceScale = 500;

            IAnnotateLayerProperties pAnnoProps = new LabelEngineLayerPropertiesClass();
            pAnnoProps.FeatureLinked = true;
            pAnnoProps.AddUnplacedToGraphicsContainer = false;
            pAnnoProps.CreateUnplacedElements = false;
            pAnnoProps.DisplayAnnotation = true;
            pAnnoProps.UseOutput = true;

            IAnnotationExpressionEngine aAnnoVBScriptEngine;
            aAnnoVBScriptEngine = new AnnotationVBScriptEngineClass();

            ILabelEngineLayerProperties pLELayerProps;
            pLELayerProps = (ILabelEngineLayerProperties)pAnnoProps;
            pLELayerProps.ExpressionParser = aAnnoVBScriptEngine;
            pLELayerProps.Expression = "[DESCRIPTION]";

            pLELayerProps.IsExpressionSimple = true;
            pLELayerProps.Offset = 0;
            pLELayerProps.SymbolID = 0;
            pLELayerProps.Symbol = pTextSymbol;

            IAnnotateLayerTransformationProperties pATP;
            pATP = (IAnnotateLayerTransformationProperties)pAnnoProps;
            pATP.ReferenceScale = pGLS.ReferenceScale;
            pATP.Units = esriUnits.esriMeters;
            pATP.ScaleRatio = 300;

            IAnnotateLayerPropertiesCollection pAnnoPropsColl;
            pAnnoPropsColl = new AnnotateLayerPropertiesCollectionClass();
            pAnnoPropsColl.Add(pAnnoProps);

            IObjectClassDescription pOCDesc;
            pOCDesc = new AnnotationFeatureClassDescriptionClass();

            IFeatureClassDescription pFDesc;

            pFDesc = (IFeatureClassDescription)pOCDesc;

            ISymbolCollection pSymbolColl;
            pSymbolColl = new SymbolCollectionClass();
            pSymbolColl.set_Symbol(0, (ISymbol)pTextSymbol);
            pSymbolColl.set_Symbol(2, (ISymbol)tTextSymbol);

            IFields pFields = pOCDesc.RequiredFields;

            //添加注记类的附加字段
            this.addAnnoField(ref pFields, strObjNum);

            string featureName = TableName;				//注记要素集的名字
            IFeatureDataset pFeatureDataset;

            if (featureDs == null)
                pFeatureDataset = pFeatureWs.CreateFeatureDataset(featureName + "_Anno", sPR);
            else
                pFeatureDataset = featureDs;

            pFeatureWorkspaceAnno.CreateAnnotationClass(featureName, pFields, pOCDesc.InstanceCLSID,
                pOCDesc.ClassExtensionCLSID, pFDesc.ShapeFieldName, "",
                pFeatureDataset, null, pAnnoPropsColl, pGLS, pSymbolColl, true);
        }

        /// <summary>
        /// 创建Gis用的字体Style
        /// </summary>
        private void createGisFont()
        {

        }
        /// <summary>
        /// 添加注记类的附加字段
        /// </summary>
        /// <param name="pFields"></param>
        /// <param name="strObjNum"></param>
        private void addAnnoField(ref IFields pFields, string strObjNum)
        {
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            IField newField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)newField;
            pFieldEdit.Name_2 = strObjNum;
            pFieldEdit.AliasName_2 = strObjNum;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldsEdit.AddField(newField);

            newField = new FieldClass();
            pFieldEdit = (IFieldEdit)newField;
            pFieldEdit.Name_2 = "SMSCode";
            pFieldEdit.AliasName_2 = "SMSCode";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldsEdit.AddField(newField);

            newField = new FieldClass();
            pFieldEdit = (IFieldEdit)newField;
            pFieldEdit.Name_2 = "SMSSymbol";
            pFieldEdit.AliasName_2 = "SMSSymbol";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;

            pFieldsEdit.AddField(newField);
        }

        #endregion

        #region 建MDB文件

        /// <summary>
        /// 新建工作空间
        /// </summary>
        /// <param name="Location">路径名</param>
        /// <param name="Name">文件名</param>
        public bool CreateWorkspace(string Location, string Name)
        {
            string strSrcdb = Application.StartupPath + @"\..\Template\DataConvertTemplate.mdb";
            //string strCopyPath = Location + @"\" + Name;
            string strCopyPath = Location + Name;
            System.IO.FileInfo fSrcFile = new System.IO.FileInfo(strSrcdb);
            System.IO.FileInfo fileCopy = new System.IO.FileInfo(strCopyPath);
            if (fSrcFile.Exists)
            {
                if (!fileCopy.Exists)
                {
                    fSrcFile.CopyTo(strCopyPath);
                }
                else
                {
                    MessageBox.Show("数据库重名！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                IWorkspaceFactory pWorkspaceFactory = new AccessWorkspaceFactoryClass();
                pWorkspaceFactory.Create(Location, Name, null, 0);
            }

            return true;
        }
        #endregion

        #region//文件备份
        /// <summary>
        /// 文件备份
        /// </summary>
        /// <param name="strSrcFilePath">原始文件路径</param>
        /// <returns>备份文件路径</returns>
        private void FileCopy(string strSrcFilePath, string strCopyPath)
        {
            System.IO.FileInfo fSrcFile = new System.IO.FileInfo(strSrcFilePath);
            System.IO.FileInfo fCopyFile = new System.IO.FileInfo(strCopyPath);

            fSrcFile.CopyTo(strCopyPath);
        }
        #endregion

        #region 创建Dataset
        public void CreateDataset(string dsName)
        {
            IWorkspace pWs = (IWorkspace)pAccessWorkSpace;
            IEnumDatasetName enumDatasetName = pWs.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);

            IDatasetName datasetName = enumDatasetName.Next();	//如果datasetName存在则退出
            while (datasetName != null)
            {
                if (datasetName.Name == dsName)
                    return;
                datasetName = enumDatasetName.Next();
            }
            pAccessWorkSpace.CreateFeatureDataset(dsName, sPR);
        }
        #endregion

        #region 在指定的工作空间内取出一个ITable
        /// <summary>
        /// 获取一个ITable
        /// </summary>
        /// <param name="workFile"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ITable GetFeatureTable(string workFile, string tableName)
        {
            //load 各变量付初值
            ITable pTmpTable;
            IWorkspaceFactory pAccessWorkSpaceFactory;
            IFeatureWorkspace pAccessWorkSpace;
            pAccessWorkSpaceFactory = new AccessWorkspaceFactoryClass();

            pAccessWorkSpace = (IFeatureWorkspace)pAccessWorkSpaceFactory.OpenFromFile(workFile, 0);

            //修改后的图层对比表
            pTmpTable = pAccessWorkSpace.OpenTable(tableName);

            return pTmpTable;
        }
        #endregion

        #region 检查是否为layerTable表

        public static bool CheckIsValidTable(string mdbFile, IDatasetName datasetname, string validField)
        {
            bool rtnValue = false;

            string fieldName = "";
            int fieldCount = 0;
            ITable pTable;
            pTable = PublicFun.GetAccessTable(mdbFile, datasetname.Name.ToString());
            fieldCount = pTable.Fields.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                fieldName = pTable.Fields.get_Field(i).Name;
                if (fieldName == validField)
                    rtnValue = true;
            }
            return rtnValue;
        }

        #endregion

    }
}
