using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Collections;
using DFWinForms.Class;
using System.Xml;
using System.IO;
using DF2DMDBConvert.Class;
using ESRI.ArcGIS.Geometry;
using System.Runtime.InteropServices;

namespace DF2DMDBConvert
{
    public partial class MdbExport : XtraForm
    {
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        string mdbFileNameGis;
        string mdbFileNameCad;
        string xmlName = null;
        List<IFeatureClass> listFC;
        List<IFeatureClass> listFZ;
        List<Assist> listAssist;
        Dictionary<string, List<Assist>> dicFZ;
        List<IFeatureClass> listPoint;
        List<IFeatureClass> listLine;
        Dictionary<string, List<IField>> dicFields;
        TableManager tableManager;
        List<DF2DMDBConvert.Class.Table> listTables;
        int exportType = 0;
        public MdbExport()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_Gis_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Access file(*.mdb)|*.mdb";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (ofd.FileName.Length > 0)
            {
                mdbFileNameGis = ofd.FileName;
                te_Gis.Text = ofd.FileName;
            }
        }

        private void btn_Cad_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Access file(*.mdb)|*.mdb";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (ofd.FileName.Length > 0)
            {
                mdbFileNameCad = ofd.FileName;
                te_Cad.Text = ofd.FileName;
            }
        }
        private void btn_Xml_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "XML file(*.xml)|*.xml";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (ofd.FileName.Length > 0)
            {
                xmlName = ofd.FileName;
                te_Xml.Text = ofd.FileName;
            }
           
        }

        private void btn_CreateXml_Click(object sender, EventArgs e)
        {            
            sfd = new SaveFileDialog();
            sfd.Filter = "XML file(*.xml)|*.xml";
            if (sfd.ShowDialog() != DialogResult.OK) return;
            if (sfd.FileName.Length > 0)
            {
                xmlName = sfd.FileName;
                te_Xml.Text = sfd.FileName;
            }
            if (this.mdbFileNameCad == null) MessageBox.Show("请选择Cad数据库");
            WaitForm.Start("开始创建XMl数据...");
            ReadTableInMdb();
            CreateXML();
            WaitForm.SetCaption("XMl创建成功");
            WaitForm.Stop();
        }

        private void btn_DelTable_Click(object sender, EventArgs e)
        {
            if (this.mdbFileNameCad == null) MessageBox.Show("请选择Cad数据库");
            DialogResult result = MessageBox.Show("确定删除Cad已有数据表","",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)            
                DeleteCadTable();            
            WaitForm.SetCaption("数据表删除完成");
            WaitForm.Stop();            
        }

        /// <summary>
        /// 删除Cad数据库数据表
        /// </summary>
        private void DeleteCadTable()
        {
            WaitForm.Start("开始删除数据表...");
            IEnumDataset pEnumDs;
            IDataset pDs;
            ITable pTable;
            IFields pFields;
            IField pField;
            string tbName;
            List<IField> listFields;
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库工作空间
            pEnumDs = pWs.get_Datasets(esriDatasetType.esriDTAny);//获得数据库数据集
            pEnumDs.Reset();
            while ((pDs = pEnumDs.Next()) != null)
            {
                //以下四个表不需要删除
                if (pDs.Name == "_CONFIGBLOCK") continue;
                if (pDs.Name == "_CONFIGDICTIONARY") continue;
                if (pDs.Name == "_CONFIGLAYER") continue;
                if (pDs.Name == "_MAPNUMBER") continue;
                pDs.Delete();
                
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            WaitForm.Start("开始导出数据...");         
            if (File.Exists(xmlName))
            {
                ReadXML();
                LoadMxd();
                Excute();
            }
            WaitForm.Stop();
        }

        /// <summary>
        /// 加载Mxd地图文件
        /// </summary>
        private void LoadMxd()
        {
            WaitForm.SetCaption("正在读取GIS数据库...");
            IMapDocument mapDoc = new MapDocument();
            string pFileName = Config.GetConfigValue("2DMxdPipe");
            mapDoc.Open(pFileName, null);//打开mxd地图文档
            IMap pMap = mapDoc.get_Map(0);
            listFC = new List<IFeatureClass>();
            
            for (int i = 0; i < pMap.LayerCount; i++)//对地图图层进行遍历
            {
                ILayer layer = pMap.get_Layer(i);
                ReadGeoLayer(layer, listFC);//读取该图层，并更新字典
            }
            
        }

        /// <summary>
        /// 读取地理信息图层，获取要素类列表
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="list"></param>
        private void ReadGeoLayer(ILayer layer, List<IFeatureClass> list)
        {
            if (layer is ESRI.ArcGIS.Carto.IGroupLayer)
            {
                ICompositeLayer comLayer = layer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    ILayer lyr = comLayer.get_Layer(i);
                    ReadGeoLayer(lyr, list);
                }
            }
            if (layer is IGeoFeatureLayer)
            {
                IGeoFeatureLayer geoFtLayer = layer as IGeoFeatureLayer;
                if (geoFtLayer == null) return;
                if (geoFtLayer.FeatureClass == null) return;
                IFeatureClass fc = geoFtLayer.FeatureClass;
                listFC.Add(fc);//加载地理要素图层
                //int index = fc.AliasName.IndexOf("_");
                //if (index > 0)
                //{
                //    string fcType = fc.AliasName.Substring(index + 1);
                //    if (fcType == "辅助")
                //    {
                //        listFZ.Add(fc);
                //    }
                //    else if (fcType == "点" || fcType == "线")
                //    {
                //        list.Add(fc);
                //    }

                //}
             
            }
            //if (layer is IAnnotationLayer)
            //{
            //    IAnnotationLayer annotationLayer = layer as IAnnotationLayer;
            //    IFeatureLayer featureLayer = annotationLayer as IFeatureLayer;
            //    IFeatureClass fc = featureLayer.FeatureClass;
            //    if (featureLayer != null && fc != null)
            //    {

            //    }
            //}
        }

        /// <summary>
        /// 读取Mdb数据库的数据表
        /// </summary>
        private void ReadTableInMdb()
        {
            WaitForm.SetCaption("开始遍历数据表...");
            IEnumDataset pEnumDs;
            IDataset pDs;
            ITable pTable;
            IFields pFields;
            IField pField;
            string tbName;
            List<IField> listFields;
            
            if (mdbFileNameCad == null) return;
            dicFields = new Dictionary<string, List<IField>>();//初始化（表名-字段列表）字典
            dicFields.Clear();
            try
            {
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库
                pEnumDs = pWs.get_Datasets(esriDatasetType.esriDTAny);//获得所有数据集
                pEnumDs.Reset();
                while ((pDs = pEnumDs.Next()) != null)//遍历数据集
                {
                    tbName = pDs.Name; //获得当前数据集名称                                  
                    WaitForm.SetCaption("正在遍历数据表" + tbName + "字段");
                    pTable = pDs as ITable;//当前数据集转换为数据表

                    pFields = pTable.Fields;//获得当前数据表字段集
                    listFields = new List<IField>();//初始化字段集列表
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        pField = pFields.Field[i];
                        listFields.Add(pField);//将当前字段添加进字段集列表
                    }
                    dicFields[tbName] = listFields;//更新（表名-字段列表）字典           
                }
                
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                MessageBox.Show(ex.Message);
               
            }

        }

        /// <summary>
        /// 创建XML文件
        /// </summary>
        private void CreateXML()
        {
            if (xmlName == null) return;           
            try
            {
                XmlDocument document = new XmlDocument();//创建xml文档
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);//添加子节点
                XmlNode node = document.CreateElement("Tables");//创建名为Tables的节点
                document.AppendChild(node);
                //string[] tableNames = new string[] { "_CONFIGBLOCK", "_CONFIGDICTIONARY", "_CONFIGLAYER", "_MAPNUMBER", "FZ_LINE", "FZ_POINT", "大类_LINE", "大类_POINT", "粘贴错误", "坐标_POINT" };//初始化数据表名数组
                string[] tableNames = new string[] { "gwbh_8", "测量库", "测区信息", "管沟边线", "管沟点", "管点调查表", "管线调查表" };
                foreach (string tableName in tableNames)
                {
                    WaitForm.SetCaption("正在写入" + tableName + "字段");
                    WriteXml(document, node, tableName);//根据当前数据表名写写xml文档
                }
                document.Save(xmlName);//存储该xml文档

            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /// <summary>
        /// 写XML文件
        /// </summary>
        /// <param name="document">xml文档</param>
        /// <param name="node">父节点</param>
        /// <param name="tableName">数据表名</param>
        private void WriteXml(XmlDocument document,XmlNode node,string tableName)
        {
            try
            {
                XmlElement element = document.CreateElement("Table");//创建名为Table的节点
                element.SetAttribute("name", tableName);//为该节点创建name属性，并赋值为数据表名
                element.SetAttribute("alias", "");//为该节点创建alias属性
                element.SetAttribute("fc2D", "");//为该节点创建fc2D属性
                XmlNode fcNode = node.AppendChild(element);//将Table节点添加到其父节点Tables

                if (dicFields.Count == 0) return;  //如果（表名-字段列表）字典没有数据，返回          
                List<IField> fields = dicFields[tableName];//获得当前数据表对应的字段列表
                foreach (IField fi in fields)//遍历字段列表
                {
                    XmlElement ele = document.CreateElement("StdField");//创建字段子节点
                    ele.SetAttribute("gisname", "");//创建gisname属性，gisname为要素类字段名
                    ele.SetAttribute("cadname", fi.Name);//创建cadname属性并赋值，cadname为cad数据表字段名
                    ele.SetAttribute("datatype", fi.Type.ToString());//创建datatype属性，并赋值
                    fcNode.AppendChild(ele);//为Table节点添加子元素
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        /// <summary>
        /// 读取XML字段配置
        /// </summary>
        private void ReadXML()
        {
            tableManager = new TableManager();//初始化数据表管理类
            tableManager.ReadConfig(xmlName);//读取xml配置文件
            this.listTables = tableManager.GetAllTables();//获取所有数据表
        }

        /// <summary>
        /// 初始化辅助类
        /// </summary>
        /// <param name="feature">要素</param>
        /// <param name="fzName">要素对应的大类名</param>
        /// <returns>辅助类</returns>
        private Assist InitAssit(IFeature feature,string fzName)
        {
            DF2DMDBConvert.Class.Table fzTablePnt = tableManager.GetTableByTableName("FZ_POINT");//获得辅助点表
            DF2DMDBConvert.Class.Table fzTableLine = tableManager.GetTableByTableName("FZ_LINE");//获得辅助线表
            int index;
            double floorHeight;
            string mapName;
            string lineType;
            IGeometry fzGeo;
            IPointCollection fzPCol;
            object obj;
            try
            {
                TableField tf = fzTablePnt.GetTableFieldByAliasName("地面高程");//获得地面高程对应的‘表字段’
                if (tf == null) floorHeight = 0;
                index = feature.Fields.FindField(tf.GisName);//根据该‘表字段’GisName 获得当前要素地面高程字段索引
                if (index == -1) floorHeight = 0;//如果不存在该字段，则地面高程取0
                //else floorHeight = Double.Parse(feature.get_Value(index).ToString());
                if (feature.get_Value(index) == null) floorHeight = 0;//若当前要素地面高程字段值为null，则地面高程取0
                else if (!Double.TryParse(feature.get_Value(index).ToString(), out floorHeight))
                    floorHeight = 0;

                tf = fzTablePnt.GetTableFieldByAliasName("图幅号");
                if (tf == null) mapName = null;
                index = feature.Fields.FindField(tf.GisName);
                if (index == -1) mapName = null;
                else mapName = feature.get_Value(index).ToString();//获得当前要素要素图幅号字段值，赋给mapName

                tf = fzTableLine.GetTableFieldByAliasName("线型");
                if (tf == null) lineType = null;
                index = feature.Fields.FindField(tf.GisName);
                if (index == -1) lineType = null;
                else lineType = feature.get_Value(index).ToString();//获得当前要素线型字段值，赋给lineType

                fzGeo = feature.Shape;//获得当前要素的shape值
                if (fzGeo == null) return null;
                fzPCol = fzGeo as IPointCollection;//将图形转为点集合
                string fzNum = "";//要素大类名对应的英文编码
                switch (fzName)//根据要素大类名选择相应的英文编码
                {
                    case"电力":
                        fzNum = "DL";
                        break;
                    case"通信":
                        fzNum = "TX";
                        break;
                    case "给水":
                    case "上水":
                        fzNum = "JS";
                        break;
                    case "排水":
                    case "下水":
                        fzNum = "PS";
                        break;
                    case "热力":
                        fzNum = "RL";
                        break;
                    case "燃气":
                        fzNum = "RQ";
                        break;
                    case "工业":
                    case "工业化工":
                        fzNum = "GY";
                        break;
                    default:
                        fzNum = "Num";
                        break;
                }
               
                Assist ass = new Assist(feature.OID,fzPCol, fzNum, lineType, mapName, floorHeight);//初始化辅助类
                return ass;
            }
            catch (System.Exception ex)
            {
                return null;
            }
           
           
        }

        /// <summary>
        /// 开始数据导出
        /// </summary>
        private void Excute()
        {
            string tableName;
            int index;
            string dlName;
            DF2DMDBConvert.Class.Table dfTable = null;
            string[] fc2DFZ;
            string[] fc2DPnt;
            string[] fc2DLine;
            IFeatureCursor fzCursor;
            IFeature fzFeature;
            IGeometry fzGeo;
            IPointCollection fzPCol;
            dicFZ = new Dictionary<string, List<Assist>>();//初始化（管线大类-辅助）字典
            listFZ = new List<IFeatureClass>();//初始化辅助要素类列表
            listPoint = new List<IFeatureClass>();//初始化点要素类列表
            listLine = new List<IFeatureClass>();//初始化线要素类列表
            try
            {
                DF2DMDBConvert.Class.Table dfTableFZ = tableManager.GetTableByTableName("FZ_POINT");//获得辅助表
                DF2DMDBConvert.Class.Table dfTablePnt = tableManager.GetTableByTableName("大类_POINT");//获得大类点表
                DF2DMDBConvert.Class.Table dfTableLine = tableManager.GetTableByTableName("大类_LINE");//获得大类线表
                if (dfTableFZ == null || dfTablePnt == null || dfTableLine == null) return;
                fc2DFZ = dfTableFZ.Fc2D.Split(';');//获得辅助表对应的要素类ID
                fc2DPnt = dfTablePnt.Fc2D.Split(';');//获得点表对应的要素类ID
                fc2DLine = dfTableLine.Fc2D.Split(';');//获得线表对应的要素类ID
                if (fc2DFZ == null || fc2DPnt == null || fc2DLine == null) return;

                foreach (IFeatureClass fc in listFC)//遍历所有地理要素类
                {
                    foreach (string id in fc2DFZ)//将辅助要素类添加进辅助要素列表
                    {
                        if (fc.FeatureClassID.ToString() == id)
                            listFZ.Add(fc);
                    }
                    foreach (string id in fc2DPnt)//将点要素类添加进点要素类列表
                    {
                        if (fc.FeatureClassID.ToString() == id)
                            listPoint.Add(fc);
                    }
                    foreach (string id in fc2DLine)//将线要素类添加进线要素类列表
                    {
                        if (fc.FeatureClassID.ToString() == id)
                            listLine.Add(fc);
                    }
                    
                }

                foreach (IFeatureClass fc in listFZ)//遍历辅助要素类列表
                {
                    fzCursor = fc.Search(null, true);//获得该辅助要素类所有要素
                    if (fzCursor == null) continue;
                    int count = fc.FeatureCount(null);//获得所有要素类个数
                    int n = 1;
                    string fzName = fc.AliasName.Substring(0, fc.AliasName.IndexOf('_'));//获得要素类大类名
                    listAssist = new List<Assist>();//初始化该要素类所有辅助要素列表
                    while ((fzFeature = fzCursor.NextFeature()) != null)
                    {


                        WaitForm.SetCaption("正在读取辅助信息" + fzName + n + "/" + count);
                        listAssist.Add(InitAssit(fzFeature, fzName));//初始化辅助要素并添加到辅助列表
                        n++;
                       
                    }
                    dicFZ[fzName] = listAssist;//更新（大类-辅助列表）字典
                }
                WriteTableFZPoint(dicFZ, "FZ_POINT");//写辅助点表
                WriteTableFZLine(dicFZ, "FZ_LINE");//写辅助线表

                foreach (IFeatureClass fc in listPoint)//遍历点要素类列表
                {
                    index = fc.AliasName.IndexOf('_');
                    dlName = fc.AliasName.Substring(0, index);//获得点要素类大类名
                    switch (dlName)//选择大类对应的Cad表名
                    {
                        case "电力":
                            tableName = "DL_POINT";
                            break;
                        case "通信":
                            tableName = "TX_POINT";
                            break;
                        case "热力":
                            tableName = "RL_POINT";
                            break;
                        case "燃气":
                            tableName = "RQ_POINT";
                            break;
                        case "给水":
                            tableName = "JS_POINT";
                            break;
                        case "排水":
                            tableName = "PS_POINT";
                            break;
                        case "工业":
                        case "工业化工":
                            tableName = "GY_POINT";
                            break;
                        case "综合":
                        case "综合管线":
                            tableName = "ZH_POINT";
                            break;
                        case "不明":
                        case "不明管线":
                            tableName = "NP_POINT";
                            break;
                        default:
                            tableName = "";
                            break;
                    }
                    if (!File.Exists(mdbFileNameCad)) return;
                    WriteTable(fc, tableName, dfTablePnt);//写点表
                }

                foreach (IFeatureClass fc in listLine)//遍历线要素类列表
                {
                    index = fc.AliasName.IndexOf('_');
                    dlName = fc.AliasName.Substring(0, index);//获得线要素类大类名
                    switch (dlName)//选择线要素类对应的Cad表名
                    {
                        case "电力":
                            tableName = "DL_LINE";
                            break;
                        case "通信":
                            tableName = "TX_LINE";
                            break;
                        case "热力":
                            tableName = "RL_LINE";
                            break;
                        case "给水":
                            tableName = "JS_LINE";
                            break;
                        case "燃气":
                            tableName = "RQ_LINE";
                            break;
                        case "排水":
                            tableName = "PS_LINE";
                            break;
                        case "工业":
                        case "工业化工":
                            tableName = "GY_LINE";
                            break;
                        default:
                            tableName = "";
                            break;
                    }
                    if (!File.Exists(mdbFileNameCad)) return;
                    WriteTable(fc, tableName, dfTableLine);//写线表

                }

                WriteTable("ZH_LINE", dfTableLine);//写综合管线线表
                WriteTable("ZH_POINT", dfTablePnt);//写综合管线点表
                WriteTable("大类_POINT", dfTablePnt);//写点表模板
                WriteTable("大类_LINE", dfTableLine);//写线表模板
                WriteTable("NP_LINE", dfTableLine);//写不明管线线表
                WriteTable("NP_POINT", dfTablePnt);//写不明管线点表
                WriteTable("粘贴错误", tableManager.GetTableByTableName("粘贴错误"));//写粘贴错误表
                WriteTable("坐标_POINT", tableManager.GetTableByTableName("坐标_POINT"));//写坐标点表

        #region  备用代码
                /*DF2DMDBConvert.Class.Table fzTablePnt = tableManager.GetTableByTableName("FZ_POINT");
                DF2DMDBConvert.Class.Table fzTableLine = tableManager.GetTableByTableName("FZ_LINE");
                foreach (IFeatureClass fc in listFZ)
                {
                    fzCursor = fc.Search(null, true);
                    if (fzCursor == null) continue;
                    int count = fc.FeatureCount(null);
                    int n = 1;
                    string fzName = fc.AliasName.Substring(0, fc.AliasName.IndexOf('_'));
                    listAssist = new List<Assist>();
                    while ((fzFeature = fzCursor.NextFeature()) != null)
                    {


                        WaitForm.SetCaption("正在读取辅助信息" + fzName + n + "/" + count);
                        listAssist.Add(InitAssit(fzFeature, fzName));
                        n++;
                        //fzGeo = fzFeature.Shape;
                        //if (fzGeo == null) continue;
                        //fzPCol = fzGeo as IPointCollection;                       
                        //Assist ass = new Assist(fzPCol,fzName);
                        //listAssist.Add(ass);
                    }
                    dicFZ[fzName] = listAssist;
                }
                WriteTableFZPoint(dicFZ, "FZ_POINT");
                WriteTableFZLine(dicFZ, "FZ_LINE");

                foreach (IFeatureClass fc in listFC)
                {
                    if (fc.AliasName != null && fc.AliasName.Contains('点'))
                    {
                        dfTable = tableManager.GetTableByTableName("大类_POINT");
                        if (dfTable == null) return;
                        index = fc.AliasName.IndexOf('_');
                        dlName = fc.AliasName.Substring(0, index);
                        switch (dlName)
                        {
                            case "电力":
                                tableName = "DL_POINT";
                                break;
                            case "通信":
                                tableName = "TX_POINT";
                                break;
                            case "热力":
                                tableName = "RL_POINT";
                                break;
                            case "燃气":
                                tableName = "RQ_POINT";
                                break;
                            case "给水":
                                tableName = "JS_POINT";
                                break;
                            case "排水":
                                tableName = "PS_POINT";
                                break;
                            case "工业":
                            case "工业化工":
                                tableName = "GY_POINT";
                                break;
                            case "综合":
                            case "综合管线":
                                tableName = "ZH_POINT";
                                break;
                            case "不明":
                            case "不明管线":
                                tableName = "NP_POINT";
                                break;
                            default:
                                tableName = "";
                                break;
                        }
                        if (!File.Exists(mdbFileNameCad)) return;
                        WriteTable(fc, tableName, dfTable);



                    }
                    else if (fc.AliasName != null && fc.AliasName.Contains('线'))
                    {
                        dfTable = tableManager.GetTableByTableName("大类_LINE");
                        if (dfTable == null) return;
                        index = fc.AliasName.IndexOf('_');
                        dlName = fc.AliasName.Substring(0, index);
                        switch (dlName)
                        {
                            case "电力":
                                tableName = "DL_LINE";
                                break;
                            case "通信":
                                tableName = "TX_LINE";
                                break;
                            case "热力":
                                tableName = "RL_LINE";
                                break;
                            case "给水":
                                tableName = "JS_LINE";
                                break;
                            case "燃气":
                                tableName = "RQ_LINE";
                                break;
                            case "排水":
                                tableName = "PS_LINE";
                                break;
                            case "工业":
                            case "工业化工":
                                tableName = "GY_LINE";
                                break;
                            default:
                                tableName = "";
                                break;
                        }
                        if (!File.Exists(mdbFileNameCad)) return;
                        WriteTable(fc, tableName, dfTable);

                    }

                }
                WriteTable("ZH_LINE", tableManager.GetTableByTableName("大类_LINE"));
                WriteTable("ZH_POINT", tableManager.GetTableByTableName("大类_POINT"));
                WriteTable("大类_POINT", tableManager.GetTableByTableName("大类_POINT"));
                WriteTable("大类_LINE", tableManager.GetTableByTableName("大类_LINE"));
                WriteTable("NP_LINE", tableManager.GetTableByTableName("大类_LINE"));
                WriteTable("NP_POINT", tableManager.GetTableByTableName("大类_POINT"));
                WriteTable("粘贴错误", tableManager.GetTableByTableName("粘贴错误"));
                WriteTable("坐标_POINT", tableManager.GetTableByTableName("坐标_POINT"));*/

#endregion
               



            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        private void ExcuteToDG()
        {           
            string[] fc2DPnt;
            string[] fc2DLine;            
            dicFZ = new Dictionary<string, List<Assist>>();//初始化（管线大类-辅助）字典
            listFZ = new List<IFeatureClass>();//初始化辅助要素类列表
            listPoint = new List<IFeatureClass>();//初始化点要素类列表
            listLine = new List<IFeatureClass>();//初始化线要素类列表
            try
            {
                //DF2DMDBConvert.Class.Table dfTableFZ = tableManager.GetTableByTableName("FZ_POINT");//获得辅助表
                DF2DMDBConvert.Class.Table dfTablePnt = tableManager.GetTableByTableName("管点调查表");//获得大类点表
                DF2DMDBConvert.Class.Table dfTableLine = tableManager.GetTableByTableName("管线调查表");//获得大类线表
                if (dfTablePnt == null || dfTableLine == null) return;

                fc2DPnt = dfTablePnt.Fc2D.Split(';');//获得点表对应的要素类ID
                fc2DLine = dfTableLine.Fc2D.Split(';');//获得线表对应的要素类ID
                if (fc2DPnt == null || fc2DLine == null) return;

                foreach (IFeatureClass fc in listFC)//遍历所有地理要素类
                {
                    
                    foreach (string id in fc2DPnt)//将点要素类添加进点要素类列表
                    {
                        if (fc.FeatureClassID.ToString() == id)
                            listPoint.Add(fc);
                    }
                    foreach (string id in fc2DLine)//将线要素类添加进线要素类列表
                    {
                        if (fc.FeatureClassID.ToString() == id)
                            listLine.Add(fc);
                    }

                }
                //CreateTableDG(dfTablePnt, listPoint);
                //CreateTableDG(dfTableLine, listLine);
                WriteTable("gwbh_8", tableManager.GetTableByTableName("gwbh_8"));
                WriteTable("测量库", tableManager.GetTableByTableName("测量库"));
                WriteTable("测区信息", tableManager.GetTableByTableName("测区信息"));
                WriteTable("管沟边线", tableManager.GetTableByTableName("管沟边线"));
                WriteTable("管沟点", tableManager.GetTableByTableName("管沟点"));

            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void CreateTableDG(DF2DMDBConvert.Class.Table dfTable, List<IFeatureClass> listFC)
        {
            IFeatureCursor pFeatureCursor;
            IFeature pFeature;
            ITable cadTable = null;
            int index; 

            try
            {
                string tableName = dfTable.Name;
                if (tableName == "") return;
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库
                IEnumDatasetName pEnumDN = pWs.get_DatasetNames(esriDatasetType.esriDTAny);//获得数据库所有数据集
                if (DatasetExist(pEnumDN, tableName))
                {

                    return;//如果目标数据表已存在，返回
                    //IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    //cadTable = pFWs.OpenTable(tableName);//打开目标数据表
                }
                else
                {
                    IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    IFields pFields = new FieldsClass();//创建表字段集合
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                    foreach (TableField tf in dfTable.TableFieldCollection)//遍历目标数据表的字段集，并根据点表字段配置生成目标数据表字段集
                    {
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = pField as IFieldEdit;
                        if (tf.CadName == "") continue;
                        pFieldEdit.Name_2 = tf.CadName;
                        pFieldEdit.AliasName_2 = tf.CadName;
                        switch (tf.DataType)
                        {
                            case "esriFieldTypeOID":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                                break;
                            case "esriFieldTypeString": 
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;
                            case "esriFieldTypeDouble":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                                break;
                            case "esriFieldTypeSmallInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                                break;
                            case "esriFieldTypeInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                                break;
                            case "esriFieldTypeDate":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                                break;
                            default:
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;

                        }
                        pFieldsEdit.AddField(pField);
                    }
                    cadTable = pFWs.CreateTable(tableName, pFields, null, null, null);//创建数据表
                    cadTable = pFWs.OpenTable(tableName);//打开目标数据表
                }
                foreach (IFeatureClass fc in listFC)
                {
                    
                    if (fc.FeatureClassID == 533 || fc.FeatureClassID == 583 ||fc.FeatureClassID == 658 ||fc.FeatureClassID == 653 )continue;                  
                    int count = fc.FeatureCount(null);//获得要素类包含的要素总数
                    pFeatureCursor = fc.Search(null, true);//获得要素类的所有要素游标
                    object obj;
                    int n = 1;
                    while ((pFeature = pFeatureCursor.NextFeature()) != null)//遍历游标，获得当前要素
                    {
                        WaitForm.SetCaption("正在写入" + fc.AliasName + " " + n + "/" + count);
                        if (cadTable == null) return;
                        IRow row = cadTable.CreateRow();//创建一行数据
                        try
                        {
                            for (int i = 0; i < cadTable.Fields.FieldCount; i++)//遍历数据表的字段集
                            {
                                IField pField = cadTable.Fields.Field[i];//获得当前字段                      
                                TableField tf = dfTable.GetTableFieldByAliasName(pField.AliasName);//根据当前字段aliasName获得对应的“表字段”

                                //string type = tf.DataType;
                                index = fc.FindField(tf.GisName);//根据表字段的GisName获得要素类下该字段的索引
                                if (index == -1) continue;//如果要素类不存在该字段，跳到下一个字段
                                obj = pFeature.get_Value(index);//获得当前要素下，该字段的值
                                if (obj == null) row.set_Value(i, null);//将当该字段的值写入目标数据表新建行的对应字段
                                //else if (pFeature.Fields.get_Field(index).Type != row.Fields.get_Field(i).Type) row.set_Value(i, null);
                                else row.set_Value(i, obj);

                            }
                            row.Store();//存储该行数据
                            n++;
                            Marshal.ReleaseComObject(pFeature);
                        }
                        catch
                        {
                            continue;
                        }
                        
                    }
                    Marshal.ReleaseComObject(pFeatureCursor);
                    
                   
                    

                }

                
            }
            catch (System.Exception ex)
            {
               
            }
           
        }

       


        /// <summary>
        /// 生成辅助点表
        /// </summary>
        /// <param name="dicFZ">（大类-辅助要素列表）字典</param>
        /// <param name="tableName">Cad表名</param>
        private void WriteTableFZPoint(Dictionary<string, List<Assist>> dicFZ, string tableName)
        {
            if (tableName == "") return;
            DF2DMDBConvert.Class.Table dfTable = tableManager.GetTableByTableName(tableName);//获得辅助点表
            if (dfTable == null) return;
            ITable cadTable;
            WaitForm.SetCaption("正在写入" + tableName + "表");
            try
            {
                
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库
                IEnumDatasetName pEnumDN = pWs.get_DatasetNames(esriDatasetType.esriDTAny);//获得数据库所有数据集
                if (DatasetExist(pEnumDN, tableName)) return;//如果目标数据表已存在，返回
                else
                {
                    IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    IFields pFields = new FieldsClass();//创建表字段集合
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
                    foreach (TableField tf in dfTable.TableFieldCollection)//遍历辅助点表的字段集，并根据点表字段配置生成目标数据表字段集
                    {
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = pField as IFieldEdit;
                        if (tf.CadName == "") continue;
                        pFieldEdit.Name_2 = tf.CadName;
                        pFieldEdit.AliasName_2 = tf.CadName;
                        switch (tf.DataType)
                        {
                            case "esriFieldTypeOID":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                                break;
                            case "esriFieldTypeString":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;
                            case "esriFieldTypeDouble":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                                break;
                            case "esriFieldTypeSmallInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                                break;
                            case "esriFieldTypeInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                                break;
                            case "esriFieldTypeDate":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                                break;
                            default:
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;

                        }
                        pFieldsEdit.AddField(pField);
                    }
                    pFWs.CreateTable(tableName, pFields, null, null, null);//创建目标数据表
                    cadTable = pFWs.OpenTable(tableName);//打开数据表
                    foreach (KeyValuePair<string, List<Assist>> kv in dicFZ)//遍历（大类-辅助列表）字典
                    {
                        int count = kv.Value.Count;
                        int n = 1;
                        foreach (Assist ass in kv.Value)//遍历该大类对应的辅助列表
                        {
                            WaitForm.SetCaption("写入" + kv.Key + "辅助" + n + "/" + count);
                            foreach (FZPoint p in ass.FZPoints)//遍历该辅助包含的点
                            {
                                IRow row = cadTable.CreateRow();//创建一行新的数据
                                for (int i = 1; i < row.Fields.FieldCount; i++)//遍历目标数据表字段（不包括OID，从1开始）
                                {
                                    string fiName = row.Fields.get_Field(i).Name;////获得当前字段字段名
                                    switch (fiName)//根据字段名填写当前辅助点的属性数据
                                    {
                                        case "成图点号":
                                            row.set_Value(i, p.Name);
                                            break;
                                        case "Y坐标":
                                            row.set_Value(i, p.Y);
                                            break;
                                        case "X坐标":
                                            row.set_Value(i, p.X);
                                            break;
                                        case "地面高程":
                                            row.set_Value(i, ass.FloorHeight);
                                            break;
                                        case "管线小类":
                                            row.set_Value(i, kv.Key);
                                            break;
                                        case "图幅号":
                                            row.set_Value(i, ass.MapName);
                                            break;

                                    }
                                }
                                row.Store();//存储该行数据

                            }
                            n++;


                        }
                    }


                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /// <summary>
        /// 生成辅助线表
        /// </summary>
        /// <param name="dicFZ">（大类-辅助要素列表）字典</param>
        /// <param name="tableName">Cad表名</param>
        private void WriteTableFZLine(Dictionary<string, List<Assist>> dicFZ, string tableName)
        {
            if (tableName == "") return;
            DF2DMDBConvert.Class.Table dfTable = tableManager.GetTableByTableName(tableName);//获得辅助线表
            if (dfTable == null) return;
            ITable cadTable;
            WaitForm.SetCaption("正在写入" + tableName + "表");
            try
            {

                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库
                IEnumDatasetName pEnumDN = pWs.get_DatasetNames(esriDatasetType.esriDTAny);//获得数据库所有数据集
                if (DatasetExist(pEnumDN, tableName)) return;//如果目标数据表已存在，返回
                else
                {
                    IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    IFields pFields = new FieldsClass();//创建表字段集合
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
                    foreach (TableField tf in dfTable.TableFieldCollection)//遍历辅助点表的字段集，并根据点表字段配置生成目标数据表字段集
                    {
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = pField as IFieldEdit;
                        if (tf.CadName == "") continue;
                        pFieldEdit.Name_2 = tf.CadName;
                        pFieldEdit.AliasName_2 = tf.CadName;
                        switch (tf.DataType)
                        {
                            case "esriFieldTypeOID":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                                break;
                            case "esriFieldTypeString":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;
                            case "esriFieldTypeDouble":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                                break;
                            case "esriFieldTypeSmallInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                                break;
                            case "esriFieldTypeInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                                break;
                            case "esriFieldTypeDate":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                                break;
                            default:
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;

                        }
                        pFieldsEdit.AddField(pField);
                    }
                    pFWs.CreateTable(tableName, pFields, null, null, null);//创建目标数据表
                    cadTable = pFWs.OpenTable(tableName);//打开数据表
                    foreach (KeyValuePair<string, List<Assist>> kv in dicFZ)//遍历（大类-辅助列表）字典
                    {
                        foreach (Assist ass in kv.Value)//遍历该大类对应的辅助列表
                        {
                            foreach (FZLine l in ass.FZLines)//遍历该辅助包含的线
                            {
                                IRow row = cadTable.CreateRow();//创建一行新的数据
                                for (int i = 1; i < row.Fields.FieldCount; i++)//遍历目标数据表字段（不包括OID，从1开始）
                                {
                                    string fiName = row.Fields.get_Field(i).Name;//获得当前字段字段名
                                    switch (fiName)//根据字段名填写当前辅助线的属性数据
                                    {
                                        case "起点点号":
                                            row.set_Value(i, l.StartPoint);
                                            break;

                                        case "终点点号":
                                            row.set_Value(i, l.EndPoint);
                                            break;
                                        case "管线小类":
                                            row.set_Value(i, kv.Key);
                                            break;
                                        //case "线型":
                                        //    row.set_Value(i, ass.LineType);
                                        //    break;
                                    }
                                }
                                row.Store();//存储该行数据

                            }

                        }
                    }


                }
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 生成数据表模板
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="dfTable">目标数据表</param>
        private void WriteTable(string tableName, DF2DMDBConvert.Class.Table dfTable)
        {
            WaitForm.SetCaption("正在写入" + tableName + "表");
            try
            {
                if (tableName == "") return;
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库
                IEnumDatasetName pEnumDN = pWs.get_DatasetNames(esriDatasetType.esriDTAny);//获得数据库所有数据集
                if (DatasetExist(pEnumDN, tableName)) return;//如果目标数据表已存在，返回
                else
                {
                    IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    IFields pFields = new FieldsClass();//创建表字段集合
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
                    foreach (TableField tf in dfTable.TableFieldCollection)//遍历目标数据表的字段集，并根据点表字段配置生成目标数据表字段集
                    {
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = pField as IFieldEdit;
                        if (tf.CadName == "") continue;
                        pFieldEdit.Name_2 = tf.CadName;
                        pFieldEdit.AliasName_2 = tf.CadName;
                        switch (tf.DataType)
                        {
                            case "esriFieldTypeOID":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                                break;
                            case "esriFieldTypeString":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;
                            case "esriFieldTypeDouble":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                                break;
                            case "esriFieldTypeSmallInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                                break;
                            case "esriFieldTypeInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                                break;
                            case "esriFieldTypeDate":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                                break;
                            default:
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;

                        }
                        pFieldsEdit.AddField(pField);
                    }

                    ITable cadTable = pFWs.CreateTable(tableName, pFields, null, null, null);//创建数据表
                }
                
                   

                
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /// <summary>
        /// 根据要素类创建数据表
        /// </summary>
        /// <param name="fc">要素类</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="dfTable">目标数据表</param>
        private void WriteTable(IFeatureClass fc,string tableName,DF2DMDBConvert.Class.Table dfTable)
        {
            WaitForm.SetCaption("正在写入" + tableName + "表");
            IFeatureCursor pFeatureCursor;
            IFeature pFeature;
            ITable cadTable = null;
            int index;
            try
            {
                if (tableName == "") return;
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);//打开Cad数据库
                IEnumDatasetName pEnumDN = pWs.get_DatasetNames(esriDatasetType.esriDTAny);//获得数据库所有数据集
                if (DatasetExist(pEnumDN, tableName)) return;//如果目标数据表已存在，返回
                else
                {
                    IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    IFields pFields = new FieldsClass();//创建表字段集合
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                    foreach (TableField tf in dfTable.TableFieldCollection)//遍历目标数据表的字段集，并根据点表字段配置生成目标数据表字段集
                    {
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = pField as IFieldEdit;
                        if (tf.CadName == "") continue;
                        pFieldEdit.Name_2 = tf.CadName;
                        pFieldEdit.AliasName_2 = tf.CadName;
                        switch (tf.DataType)
                        {
                            case "esriFieldTypeOID":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                                break;
                            case "esriFieldTypeString":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;
                            case "esriFieldTypeDouble":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                                break;
                            case "esriFieldTypeSmallInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                                break;
                            case "esriFieldTypeInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                                break;
                            case "esriFieldTypeDate":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                                break;
                            default:
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;

                        }
                        pFieldsEdit.AddField(pField);
                    }

                    cadTable = pFWs.CreateTable(tableName, pFields, null, null, null);//创建数据表
                    cadTable = pFWs.OpenTable(tableName);//打开目标数据表
                }
                IQueryFilter filter = new QueryFilter();
                int count = fc.FeatureCount(null);//获得要素类包含的要素总数

        #region   备用代码
                //int c = 10000;
                //int zoom = count / c + 1;
                //for (int j = 1; j <= zoom; j++)
                //{
                //    filter.WhereClause = "OBJECTID < " + j*c + " and " + "OBJECTID >= " + (j-1)*c ;
                //    pFeatureCursor = fc.Search(filter, true);
                //    int nn = fc.FeatureCount(filter);
                //    object obj;
                //    int n = 1;
                //    while ((pFeature = pFeatureCursor.NextFeature()) != null)//遍历游标，获得当前要素
                //    {


                //        WaitForm.SetCaption("正在写入" + tableName + " " + n + "/" + nn);
                //        if (cadTable == null) return;
                //        IRow row = cadTable.CreateRow();//创建一行数据
                //        for (int i = 1; i < cadTable.Fields.FieldCount; i++)//遍历数据表的字段集
                //        {
                //            IField pField = cadTable.Fields.Field[i];//获得当前字段                      
                //            TableField tf = dfTable.GetTableFieldByAliasName(pField.AliasName);//根据当前字段aliasName获得对应的“表字段”

                //            //string type = tf.DataType;
                //            index = fc.FindField(tf.GisName);//根据表字段的GisName获得要素类下该字段的索引
                //            if (index == -1) continue;//如果要素类不存在该字段，跳到下一个字段
                //            obj = pFeature.get_Value(index);//获得当前要素下，该字段的值
                //            if (obj == null) row.set_Value(i, null);//将当该字段的值写入目标数据表新建行的对应字段
                //            else row.set_Value(i, obj);

                //        }
                //        row.Store();//存储该行数据
                //        n++;
                //        Marshal.ReleaseComObject(pFeature);
                //    }
                //    Marshal.ReleaseComObject(pFeatureCursor);
                //}

#endregion
               

                pFeatureCursor = fc.Search(null, true);//获得要素类的所有要素游标

                object obj;
                int n = 1;
                while ((pFeature = pFeatureCursor.NextFeature()) != null)//遍历游标，获得当前要素
                {


                    WaitForm.SetCaption("正在写入" + tableName + " " + n + "/" + count);
                    if (cadTable == null) return;
                    IRow row = cadTable.CreateRow();//创建一行数据
                    for (int i = 1; i < cadTable.Fields.FieldCount; i++)//遍历数据表的字段集
                    {
                        IField pField = cadTable.Fields.Field[i];//获得当前字段                      
                        TableField tf = dfTable.GetTableFieldByAliasName(pField.AliasName);//根据当前字段aliasName获得对应的“表字段”

                        //string type = tf.DataType;
                        index = fc.FindField(tf.GisName);//根据表字段的GisName获得要素类下该字段的索引
                        if (index == -1) continue;//如果要素类不存在该字段，跳到下一个字段
                        obj = pFeature.get_Value(index);//获得当前要素下，该字段的值
                        if (obj == null) row.set_Value(i, null);//将当该字段的值写入目标数据表新建行的对应字段
                        else row.set_Value(i, obj);

                    }
                    row.Store();//存储该行数据
                    Marshal.ReleaseComObject(pFeature);
                    n++;
                    if (n > 100) break;
                }
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                MessageBox.Show(ex.Message);
                
            }
        }

        /// <summary>
        /// 确定当前数据集是否存在
        /// </summary>
        /// <param name="enumDN">数据集列表</param>
        /// <param name="datasetName">目标数据集名</param>
        /// <returns></returns>
        private bool DatasetExist(IEnumDatasetName enumDN,string datasetName)
        {
            IDatasetName pDN;
            bool exist = false;
            if (enumDN == null) return false;
            enumDN.Reset();//重置数据集游标
            while ((pDN = enumDN.Next()) != null)
            {
                if (pDN.Name == datasetName)//如果当前数据集名称等于目标数据集名，则存在
                    exist =  true;           
            }
            return exist;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_dongguan_Click(object sender, EventArgs e)
        {
            WaitForm.Start("开始导出数据...");
            if (File.Exists(xmlName))
            {
                ReadXML();
                LoadMxd();
                ExcuteToDG();
               
            }
            WaitForm.Stop();
        }

        private void btn_DgToCt_Click(object sender, EventArgs e)
        {
            WaitForm.Start("开始导出数据...");
            List<IDataset> listDsDG = ReadMdb();     
            ReadXML();
            string[] CatalogNames = new string[] { "DL", "GY", "JS", "PS", "RL", "RQ", "TX" };
            foreach (IDataset ds in listDsDG)
            {
                foreach (string str in CatalogNames)
                {
                    WriteTableCT(str, GetCatalog(str), ds);
                }
            }
            WriteTable("ZH_LINE", tableManager.GetTableByTableName("大类_LINE"));//写综合管线线表
            WriteTable("ZH_POINT", tableManager.GetTableByTableName("大类_POINT"));//写综合管线点表
            WriteTable("大类_POINT", tableManager.GetTableByTableName("大类_POINT"));//写点表模板
            WriteTable("大类_LINE", tableManager.GetTableByTableName("大类_LINE"));//写线表模板
            WriteTable("NP_LINE", tableManager.GetTableByTableName("大类_LINE"));//写不明管线线表
            WriteTable("NP_POINT", tableManager.GetTableByTableName("大类_POINT"));//写不明管线点表
            WriteTable("粘贴错误", tableManager.GetTableByTableName("粘贴错误"));//写粘贴错误表
            WriteTable("坐标_POINT", tableManager.GetTableByTableName("坐标_POINT"));//写坐标点表

            WaitForm.Stop();

        }

        private void WriteTableCT(string catalogName, string[] catalog,IDataset ds)
        {
            string tableName = null;
            DF2DMDBConvert.Class.Table dfTable = null;
            ITable cadTable;
            ITable table = ds as ITable;
            if (ds.Name == "管点调查表")
            {
                tableName = catalogName + "_POINT";
                dfTable = tableManager.GetTableByTableName("大类_POINT");
            }
            else if (ds.Name == "管线调查表")
            {
                tableName = catalogName + "_LINE";
                dfTable = tableManager.GetTableByTableName("大类_LINE");
            }
            if (tableName == null || dfTable == null) return;
            try
            {
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameCad, 0);
                IEnumDatasetName pEnumDN = pWs.get_DatasetNames(esriDatasetType.esriDTAny);//获得数据库所有数据集
                if (DatasetExist(pEnumDN, tableName))
                {

                    return;//如果目标数据表已存在，返回
                    //IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    //cadTable = pFWs.OpenTable(tableName);//打开目标数据表
                }
                else
                {
                    IFeatureWorkspace pFWs = pWs as IFeatureWorkspace;
                    IFields pFields = new FieldsClass();//创建表字段集合
                    IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                    foreach (TableField tf in dfTable.TableFieldCollection)//遍历目标数据表的字段集，并根据点表字段配置生成目标数据表字段集
                    {
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = pField as IFieldEdit;
                        if (tf.CadName == "") continue;
                        pFieldEdit.Name_2 = tf.CadName;
                        pFieldEdit.AliasName_2 = tf.CadName;
                        switch (tf.DataType)
                        {
                            case "esriFieldTypeOID":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                                break;
                            case "esriFieldTypeString":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;
                            case "esriFieldTypeDouble":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                                break;
                            case "esriFieldTypeSmallInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                                break;
                            case "esriFieldTypeInteger":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                                break;
                            case "esriFieldTypeDate":
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                                break;
                            default:
                                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                                break;

                        }
                        pFieldsEdit.AddField(pField);
                    }
                    cadTable = pFWs.CreateTable(tableName, pFields, null, null, null);//创建数据表
                    cadTable = pFWs.OpenTable(tableName);//打开目标数据表

                    
                    IQueryFilter filter = new QueryFilter();
                    ICursor cursor;
                    IRow row;
                    int index;
                    object obj;
                    foreach (string str in catalog)
                    {
                        filter.WhereClause = "CODE = '" + str + "'";
                        cursor = table.Search(filter, true);
                        int count = table.RowCount(filter);
                        int n = 1;
                        while ((row = cursor.NextRow()) != null)
                        {
                            WaitForm.SetCaption("正在写入" + tableName + " " + str + n + "/" + count);
                            try
                            {
                                IRow temp = cadTable.CreateRow();
                                for (int i = 0; i < cadTable.Fields.FieldCount; i++)
                                {
                                    IField pField = cadTable.Fields.Field[i];//获得当前字段                      
                                    TableField tf = dfTable.GetTableFieldByAliasName(pField.Name);//根据当前字段aliasName获得对应的“表字段”

                                    //string type = tf.DataType;
                                    index = table.FindField(tf.GisName);//根据表字段的GisName获得要素类下该字段的索引
                                    if (index == -1) continue;//如果要素类不存在该字段，跳到下一个字段
                                    obj = row.get_Value(index);//获得当前要素下，该字段的值
                                    if (obj == null) temp.set_Value(i, null);//将当该字段的值写入目标数据表新建行的对应字段
                                    //else if (pFeature.Fields.get_Field(index).Type != row.Fields.get_Field(i).Type) row.set_Value(i, null);
                                    else temp.set_Value(i, obj);
                                }
                                temp.Store();//存储该行数据
                                n++;
                                Marshal.ReleaseComObject(row);
                            }
                            catch (System.Exception ex)
                            {
                                continue;
                            }

                        }
                        Marshal.ReleaseComObject(cursor);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
                


        }
        private string[] GetCatalog(string catalogName)
        {
            string[] catalog = null;
            switch (catalogName)
            {
                case "DL":
                    catalog = new string[] { "GD", "LD", "XH" };
                    break;
                case "GY":
                    catalog = new string[] { "GY" };
                    break;
                case "JS":
                    catalog = new string[] { "GS", "LH", "SS" };
                    break;
                case "PS":
                    catalog = new string[] { "WS", "YS" };
                    break;
                case "RL":
                    catalog = new string[] { "RS" };
                    break;
                case "RQ":
                    catalog = new string[] { "TR" };
                    break;
                case "TX":
                    catalog = new string[] { "BJ", "DT", "DX", "JK", "LT", "TT", "TV", "WT", "YD", "YT" };
                    break;

            }
            return catalog;
        }

        private List<IDataset> ReadMdb()
        {
            if (mdbFileNameGis == null) return null;
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IWorkspace pWs = pWsF.OpenFromFile(mdbFileNameGis, 0);

            IEnumDataset pEnumDs = pWs.get_Datasets(esriDatasetType.esriDTAny);
            pEnumDs.Reset();
            IDataset pDs = null;
            List<IDataset> listDatasets = new List<IDataset>();
            while ((pDs = pEnumDs.Next()) != null)
            {
                if (pDs.Name == "管点调查表" || pDs.Name == "管线调查表")
                    listDatasets.Add(pDs);
            }
            if (listDatasets.Count == 0)
                MessageBox.Show("请选择正确的数据库");
            return listDatasets;
                
        }
       
       

      
        
    }
}
