using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DTool.Class;

namespace DF2DTool.Interface
{
    interface IDxfImport
    {
        //符号编码字段
        string StrObjNum { set; }
        //块符号插入角度字段

        string StrAngle { set; }
        //地物编码字段
        string MapScale { set; }
        //输入的文件名
        string InputFileName { set; }
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
        //写dataset的类
        CadWriteData CadWriter { get; }
        //写Arcgis的类
        GisWriteFromDxf GisWriter { get; }
        //读青山智慧的类

      

        //string FontTable		{set;}
        //字高比例
        double HeightScale { set; }
        //字体间距比例
        double SpaceScale { set; }
        ////Precision
        //double Precision		{set;}
        //初始化

        void ImportInit();
        //运行
        void ImportRun();

    }
}
