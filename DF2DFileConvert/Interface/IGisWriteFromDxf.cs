using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DF2DFileConvert.Class;

namespace DF2DFileConvert.Interface
{
    interface IGisWriteFromDxf
    {
        //日志对象
        ConvertLog LogWriter { set; }
        //地物字段名

        string StrObjNum { set; }
        //块符号插入角度字段

        string StrAngle { set; }
        //输出的文件名
        string OutputFileName { set; }
        //文件的类型

        string MdbFileName { set; }
        //图层对照表

        string LayerTable { set; }
       

        DataSet CurrentDs { get; set; }
        //初始化

        bool InitData();
        //写要素

        void WriteFeacture();
    }
}
