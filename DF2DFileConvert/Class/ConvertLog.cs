using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DF2DFileConvert.Interface;

namespace DF2DFileConvert.Class
{
    public class ConvertLog : IConvertLog
    {
        public StreamWriter sw;	//log文件写对象

        //开始时间
        private DateTime beginDt;
        public DateTime BeginDt
        {
            set { beginDt = value; }
        }

        //结束时间
        private DateTime endDt;
        public DateTime EndDt
        {
            set { endDt = value; }
        }

        //log文件名
        private string logFile;
        public string LogFile
        {
            get { return logFile; }
        }

        //构造函数
        public ConvertLog()
        {
            string dt;

            DateTime now = new DateTime();
            now = DateTime.Now;

            dt = "文件转换" + now.Year.ToString() + now.Month.ToString("00") + now.Day.ToString("00");
            dt += now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00");


            if (PublicFun.isWebService == false)
            {
                bool exist = System.IO.Directory.Exists(Application.StartupPath + @"\..\Log");
                if (exist == false)
                {
                    logFile = Application.StartupPath + @"\..\Log";
                    Directory.CreateDirectory(logFile);//创建
                }
                logFile = Application.StartupPath + @"\..\Log\" + dt + ".html";
                sw = new StreamWriter(logFile, false, Encoding.Default);
            }
            else
            {
                logFile = "D:\\Style\\" + dt + ".html";
                sw = new StreamWriter(logFile, false, Encoding.Default);
            }
        }

        //构造函数  20131128 TianK  重载
        public ConvertLog(string filePath)
        {
            string dt;
            DateTime now = new DateTime();
            now = DateTime.Now;

            dt = "文件转换" + now.Year.ToString() + now.Month.ToString("00") + now.Day.ToString("00");
            dt += now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00");

            if (PublicFun.isWebService == false)
            {
                bool exist = System.IO.Directory.Exists(Application.StartupPath + @"\..\Log");
                if (exist == false)
                {
                    logFile = Application.StartupPath + @"\..\Log";
                    Directory.CreateDirectory(logFile);//创建
                }
                logFile = Application.StartupPath + @"\..\Log\" + dt + ".html";
                sw = new StreamWriter(logFile, false, Encoding.Default);
            }
            else
            {
                logFile = filePath + dt + ".html";
                sw = new StreamWriter(logFile, false, Encoding.Default);
            }
        }
        //数据集
        private DataSet currentDs;
        public DataSet CurrentDs
        {
            get
            {
                return currentDs;
            }
            set
            {
                currentDs = value;
            }
        }
        #region IConvertLog 成员


        //判断表中有无相同的记录，如果没有则添加
        public void AddErrorLog(string type, string log)
        {
            DataRow[] secTable;
            DataRow tmpRow;
            DataTable logTable = new DataTable();
            logTable = currentDs.Tables["ErrorLog"];
            secTable = logTable.Select("Type='" + type + "' and Log='" + log + "'");
            if (secTable.Length == 0)
            {
                tmpRow = logTable.NewRow();
                tmpRow["Type"] = type;
                tmpRow["Log"] = log;

                logTable.Rows.Add(tmpRow);
            }
        }

        //写log
        public void WriteErrorLog()
        {
            string log = "";
            DataRow[] secTable;
            sw.WriteLine("<html pageEncoding=GB2312>");
            //sw.WriteLine("<meta http-equiv=Content-Type content=text/html; charset=gb2312 />");
            sw.WriteLine("<body>");
            sw.WriteLine("<center><font size=6>错误日志</font></center>");
            sw.WriteLine("<font size=3>");

            DataTable logTable = new DataTable();
            logTable = currentDs.Tables["ErrorLog"];
            secTable = logTable.Select(null, "Type");
            for (int i = 0; i < secTable.Length; i++)
            {
                log = (string)secTable[i]["Log"];
                sw.WriteLine(log);
            }
            sw.WriteLine("开始时间：" + beginDt.ToString() + "<br>");
            sw.WriteLine("结束时间：" + endDt.ToString() + "<br>");
            sw.WriteLine("</font>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");

            sw.Close();
        }

        #endregion
    }
}
