using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DF2DFileConvert.Class;

namespace DF2DFileConvert.Interface
{
    interface ICadRead
    {
        //日志对象
        ConvertLog LogWriter { set; }
        //输入的文件名
        string InputFileName { set; }
        //配置文件文件名
        string MdbFileName { set; }
        //图层对照表
        string LayerTable { set; }
        //符号对照表
        string SymbolTable { set; }
        ////字体定义文件
        //string FontTable		{set;}
        //写dataset的类
        CadWriteData CadWriter { get; }
        //数据集

        DataSet CurrentDs { get; set; }
        //程序初始化

        void CadReadInit();
        //程序运行
        bool CadReadRun();
    }
}
