using System;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using DF2DTool.Class;

namespace DF2DTool.Interface
{
    interface IDxfExport
    {
        //选择的dataset
        DataSetNames InputDs { set; }
        //选择集
        IFeatureSelection PFeaSelection { set; }
        //地物字段名
        string StrObjNum { set; }
        //块符号倾斜角度
        string StrAngle { set; }
        //输出的文件名
        string OutputFileName { set; }
        //文件的类型
        string FileType { set; }
        //配置文件文件名
        string MdbFileName { set; }
        //图层对照表
        string LayerTable { set; }
        //符号对照表
        string SymbolTable { set; }
        ////符号定义
        //string BlockTable		{set;}
        ////线形定义
        //string LineTypeTable	{set;}
        ////字体定义
        //string FontTable		{set;}
        //操作类型
        string OperType { set; }
        //传入的地图
        IMap PMap { set; }
        //地图的比例
        double MapScale { set; }
        //初始化
        bool ExportInit();
    }
}
