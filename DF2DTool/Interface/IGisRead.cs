using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DF2DTool.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace DF2DTool.Interface
{
    public interface IGisRead
    {
        
        //日志对象
        ConvertLog LogWriter { set; }
        //选择的mxd文件
        DataSetNames InputDs { set; }
        //选择集
        IFeatureSelection PFeaSelection { set; }
        //地物字段名
        string StrObjNum { set; }
        //块符号倾斜角度
        string StrAngle { set; }
        //配置文件文件名
        string MdbFileName { set; }
        //图层对照表
        string LayerTable { set; }
        //符号对照表
        string SymbolTable { set; }
        ////符号定义表名
        //string BlockTable		{set;}
        ////线形定义表名
        //string LineTypeTable	{set;}
        ////字体定义表名
        //string FontTable		{set;}
        //DataSet
        DataSet CurrentDs { get; }
        //传入的地图
        IMap PMap { set; }
        double MapScale { set; }
        //		//图形范围
        //		IEnvelope PEnv          {get;}

        //初始化
        bool GisReadInit();
        //从数据库中取出数据
        void Read_EntitiesFromSelection();
        //从选择集中取出数据
        void Read_EntitiesFromDatabase();

    }
}
