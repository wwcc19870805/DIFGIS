using System;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace DF2DTool.Class
{
    public class CoreData
    {
        public CoreData()
        {
            this.CreateTables();
        }

        #region 对外的方法和属性

        private DataSet coreDs = new DataSet("coreDs");
        public DataSet CoreDs
        {
            get { return coreDs; }
            set { coreDs = value; }
        }
        #endregion

        #region 定义数据结构
        /// <summary>
        /// 添加管理用的表
        /// </summary>
        public void CreateTables()
        {
            //			//添加图层管理的表	(在源图中读出的图层)
            DataTable tmpTable;
            //CAD块表  2013.11.11  TianK  添加
            tmpTable = new DataTable("DefineBlockLineTypeFont");
            tmpTable.Columns.Add("ID", typeof(String));
            tmpTable.Columns.Add("DEFINE", typeof(String));
            tmpTable.Columns.Add("TYPE", typeof(String));
            //tmpTable.Columns.Add("CODE", typeof(String));
            //tmpTable.Columns.Add("DECLARE", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //字段名对照表  2013.11.04  TianK  添加
            tmpTable = new DataTable("AttributeCASStoDifGIS");
            tmpTable.Columns.Add("CASSAttributeName", typeof(String));
            tmpTable.Columns.Add("DifGISAttributeName", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //CAD的图层表  2013.03.25  TianK  添加
            tmpTable = new DataTable("CADLayerTable");
            tmpTable.Columns.Add("LayerName", typeof(String));
            tmpTable.Columns.Add("Color", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //图层对照表
            tmpTable = new DataTable("layerTable");
            tmpTable.Columns.Add("CadLayer", typeof(String));
            tmpTable.Columns.Add("GisLayer", typeof(String));
            tmpTable.Columns.Add("GisLayerType", typeof(String));
            tmpTable.Columns.Add("LayerName", typeof(String));
            tmpTable.Columns.Add("DfGisLayerCode", typeof(String));
            tmpTable.Columns.Add("LCOLOR", typeof(String));   //2007.06.06 TianK 添加  用于存放倒入CAD的颜色号
            //tmpTable.Columns.Add("Dept", typeof(String));     
            coreDs.Tables.Add(tmpTable);
            //符号对照表
            tmpTable = new DataTable("symbolTable");
            tmpTable.Columns.Add("CassCode", typeof(String));   //TianK于20061228增加,用于存放南方CASS编码
            tmpTable.Columns.Add("SymbolCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("DifGisLayer", typeof(String));        //2013.11.01 TianK 添加  用于存放对应的GIS图层名
            //tmpTable.Columns.Add("LCOLOR", typeof(String));
            tmpTable.Columns.Add("LWIDTH", typeof(String));
            //tmpTable.Columns.Add("SymbolType", typeof(String));
            //tmpTable.Columns.Add("FontName", typeof(String));
            //tmpTable.Columns.Add("DFontName", typeof(String));
            //tmpTable.Columns.Add("Dept", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的图层
            tmpTable = new DataTable("UsedLayer");
            tmpTable.Columns.Add("layerType", typeof(String));	//type: 1、点图层 2、线图层 3、面图层 4、注记图层
            tmpTable.Columns.Add("Layer", typeof(String));
            tmpTable.Columns.Add("Dataset", typeof(String));
            tmpTable.Columns.Add("LCOLOR", typeof(String));   //2007.06.06 TianK 添加  用于存放倒入CAD的颜色号
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的数据集
            tmpTable = new DataTable("UsedDataset");
            tmpTable.Columns.Add("Dataset", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的block(用途：读取arcgis或对应到dxf中出现过的符号)
            tmpTable = new DataTable("UsedBlock");
            tmpTable.Columns.Add("SymbolId", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的linetype(用途：读取arcgis或对应到dxf中出现过的线形)
            tmpTable = new DataTable("Usedlinetype");
            tmpTable.Columns.Add("SymbolId", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的hatch(用途：读取arcgis或对应到dxf中出现过的填充)
            tmpTable = new DataTable("UsedHatch");
            tmpTable.Columns.Add("SymbolId", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的font(用途：读取arcgis或对应到dxf中出现过的字体)
            tmpTable = new DataTable("UsedFont");
            tmpTable.Columns.Add("SymbolId", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加错误日至记录表
            tmpTable = new DataTable("ErrorLog");
            tmpTable.Columns.Add("Type", typeof(String));	//type:0、图层 1、点符号 2、线符号 3、面符号 4、注记 5、文件错误
            tmpTable.Columns.Add("Log", typeof(String));
            coreDs.Tables.Add(tmpTable);
            //添加图中用到的属性字段名(用途：读取arcgis时记录中出现过的扩展属性字段名)  2009.1.14 TianK 添加
            tmpTable = new DataTable("AppID");
            tmpTable.Columns.Add("AppID", typeof(String));
            coreDs.Tables.Add(tmpTable);

            //添加各种实体表
            //包括point,line,text,polyline,arc,circle,insert(block),spline每种的实体的结构不同分别处理

            //添加点，基础数据：图层、线型、Block、颜色、线型比例、线宽枚举	
            tmpTable = new DataTable("pointTable");
            tmpTable.Columns.Add("entId", typeof(String));		//实体的ID	与属性表关联
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("Dirction", typeof(String));		//Dirction	Double	-5		
            tmpTable.Columns.Add("X", typeof(String));
            tmpTable.Columns.Add("Y", typeof(String));
            tmpTable.Columns.Add("Z", typeof(String));
            tmpTable.Columns.Add("M", typeof(String));             //属性 2009.8.15  TianK 添加   存储埋深
            tmpTable.Columns.Add("DFGisDataset", typeof(String));		//保存在ArcGis中的数据集名称
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色 2007.06.06 TianK 添加
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("BlockName", typeof(String));			//属性 2008.9.15   TianK 添加

            coreDs.Tables.Add(tmpTable);
            //line			
            tmpTable = new DataTable("lineTable");
            tmpTable.Columns.Add("entId", typeof(String));		//实体的ID 与属性表关联
            tmpTable.Columns.Add("lineId", typeof(String));		//实体的ID 与属性表关联
            tmpTable.Columns.Add("dLayer", typeof(String));
            //			tmpTable.Columns.Add("layerCode", typeof(String));
            //			tmpTable.Columns.Add("sSymId", typeof(String));		//源实体id
            tmpTable.Columns.Add("DifGISCode", typeof(String));		//目标实体id
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("beginX", typeof(String));
            tmpTable.Columns.Add("beginY", typeof(String));
            tmpTable.Columns.Add("beginZ", typeof(String));
            tmpTable.Columns.Add("beginM", typeof(String));
            tmpTable.Columns.Add("endX", typeof(String));
            tmpTable.Columns.Add("endY", typeof(String));
            tmpTable.Columns.Add("endZ", typeof(String));
            tmpTable.Columns.Add("endM", typeof(String));
            tmpTable.Columns.Add("DFGisDataset", typeof(String));		//保存在ArcGis中的数据集名称
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色 2007.06.06 TianK 添加
            tmpTable.Columns.Add("lwidth", typeof(String));			//线宽 2007.06.06 TianK 添加
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("LineType", typeof(String));			//属性 2008.9.15   TianK 添加

            coreDs.Tables.Add(tmpTable);
            //polyline
            tmpTable = new DataTable("polylineTable");
            tmpTable.Columns.Add("entId", typeof(String));		//实体的ID 与属性表关联
            tmpTable.Columns.Add("plId", typeof(String));
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("beginX", typeof(String));
            tmpTable.Columns.Add("beginY", typeof(String));
            tmpTable.Columns.Add("beginZ", typeof(String));
            tmpTable.Columns.Add("endX", typeof(String));
            tmpTable.Columns.Add("endY", typeof(String));
            tmpTable.Columns.Add("endZ", typeof(String));
            tmpTable.Columns.Add("td", typeof(String));				//凸度，如果此字段不为空则表示此点为弧的开始或结束
            tmpTable.Columns.Add("zdtd", typeof(String));		    //终点的凸度，如果此字段不为空则表示此点为弧的开始或结束
            tmpTable.Columns.Add("plIndex", typeof(int));			//线段的顺序
            tmpTable.Columns.Add("isClose", typeof(String));
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色
            tmpTable.Columns.Add("lwidth", typeof(String));			//线宽
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("LineType", typeof(String));			//属性 2008.9.15   TianK 添加

            DataColumn[] keys = new DataColumn[2];
            keys[0] = tmpTable.Columns["plId"];
            keys[1] = tmpTable.Columns["plIndex"];
            tmpTable.PrimaryKey = keys;					//添加主键

            coreDs.Tables.Add(tmpTable);
            //arc
            tmpTable = new DataTable("arcTable");
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("cenX", typeof(String));
            tmpTable.Columns.Add("cenY", typeof(String));
            tmpTable.Columns.Add("cenZ", typeof(String));
            tmpTable.Columns.Add("radius", typeof(String));
            tmpTable.Columns.Add("fromAng", typeof(String));
            tmpTable.Columns.Add("toAng", typeof(String));
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色 2007.06.06 TianK 添加
            tmpTable.Columns.Add("lwidth", typeof(String));			//线宽 2007.06.06 TianK 添加
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("LineType", typeof(String));			//属性 2008.9.15   TianK 添加

            coreDs.Tables.Add(tmpTable);
            //circle			
            tmpTable = new DataTable("circleTable");
            tmpTable.Columns.Add("entId", typeof(String));		//实体的ID  与属性表关联
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("cenX", typeof(String));
            tmpTable.Columns.Add("cenY", typeof(String));
            tmpTable.Columns.Add("cenZ", typeof(String));
            tmpTable.Columns.Add("radius", typeof(String));
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色 2007.06.06 TianK 添加
            tmpTable.Columns.Add("lwidth", typeof(String));			//线宽 2007.06.06 TianK 添加
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("LineType", typeof(String));			//属性 2008.9.15   TianK 添加

            coreDs.Tables.Add(tmpTable);
            //text					
            tmpTable = new DataTable("textTable");
            tmpTable.Columns.Add("entId", typeof(String));		//实体的ID  与属性表关联
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("dHeight", typeof(String));
            tmpTable.Columns.Add("Dirction", typeof(String));
            tmpTable.Columns.Add("X1", typeof(String));
            tmpTable.Columns.Add("Y1", typeof(String));
            tmpTable.Columns.Add("Z1", typeof(String));
            tmpTable.Columns.Add("X2", typeof(String));
            tmpTable.Columns.Add("Y2", typeof(String));
            tmpTable.Columns.Add("Z2", typeof(String));
            tmpTable.Columns.Add("TEXT", typeof(String));
            tmpTable.Columns.Add("DX", typeof(String));
            tmpTable.Columns.Add("DY", typeof(String));
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色 2007.06.06 TianK 添加
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("SHeight", typeof(String));			//原CAD字高 2009.9.15  TianK 添加
            tmpTable.Columns.Add("SScale", typeof(String));			//原CAD字宽比例 2009.9.15  TianK 添加
            tmpTable.Columns.Add("SFontName", typeof(String));			//原CAD字体样式 2009.9.15  TianK 添加

            coreDs.Tables.Add(tmpTable);
            //spline
            tmpTable = new DataTable("splineTable");
            tmpTable.Columns.Add("entId", typeof(String));		//实体的ID  与属性表关联
            tmpTable.Columns.Add("plId", typeof(String));
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("X", typeof(String));
            tmpTable.Columns.Add("Y", typeof(String));
            tmpTable.Columns.Add("Z", typeof(String));
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            //tmpTable.Columns.Add("lcolor", typeof(String));			//颜色 2007.06.06 TianK 添加
            tmpTable.Columns.Add("lwidth", typeof(String));			//线宽 2007.06.06 TianK 添加
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("LineType", typeof(String));			//属性 2008.9.15   TianK 添加

            coreDs.Tables.Add(tmpTable);
            //hatch
            tmpTable = new DataTable("hatchTable");
            tmpTable.Columns.Add("plId", typeof(String));
            tmpTable.Columns.Add("dLayer", typeof(String));
            tmpTable.Columns.Add("DifGISCode", typeof(String));
            tmpTable.Columns.Add("SymbolName", typeof(String));    //地物编码名称 2013.11.04  TianK 添加
            tmpTable.Columns.Add("beginX", typeof(String));
            tmpTable.Columns.Add("beginY", typeof(String));
            tmpTable.Columns.Add("beginZ", typeof(String));
            tmpTable.Columns.Add("endX", typeof(String));
            tmpTable.Columns.Add("endY", typeof(String));
            tmpTable.Columns.Add("endZ", typeof(String));
            tmpTable.Columns.Add("SMSCode", typeof(String));			//保存青山智慧的图层编码
            tmpTable.Columns.Add("SMSSymbol", typeof(String));			//保存青山智慧的符号编码
            tmpTable.Columns.Add("AttriBute", typeof(String));			//属性 2008.10.07  TianK 添加
            tmpTable.Columns.Add("LineType", typeof(String));			//属性 2008.9.15   TianK 添加

            coreDs.Tables.Add(tmpTable);
            //其他类型待加入
        }
        #endregion
    }
}
