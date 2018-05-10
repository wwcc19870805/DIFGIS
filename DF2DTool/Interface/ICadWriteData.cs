using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DF2DTool.Class
{
    interface ICadWriteData
    {
        //委托
        //		event Handler ReadHandler ;

        //配置文件文件名
        string MdbFileName { set; }
        //图层对照表
        string LayerTable { set; }
        //符号对照表
        string SymbolTable { set; }
        //Dataset
        DataSet CoreDs { get; }
        //初始化数据
        void CadWriteInit();
        //添加元素
        void AddElement(string entStr);
    }
}
