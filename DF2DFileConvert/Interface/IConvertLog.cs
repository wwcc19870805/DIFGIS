using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DF2DFileConvert.Interface
{
    interface IConvertLog
    {
        //开始时间
        DateTime BeginDt { set; }
        //结束时间
        DateTime EndDt { set; }
        //log文件名
        string LogFile { get; }
        //系统数据集
        DataSet CurrentDs { get; set; }
        //添加错误
        void AddErrorLog(string type, string log);
        //向硬盘写错误日至
        void WriteErrorLog();
    }
}
