using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DTool.Interface;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DFWinForms.Class;

namespace DF2DTool.Class
{
    /// <summary>

    /// </summary>
    public class ImportMain : IDxfImport
    {
        private ICadRead cadr = new CadRead();
        private IGisWriteFromDxf writeDemo = new GisWriteFromDxf();
        //private ConvertLog logWriter = new ConvertLog();
        //private SMSRead qsRead = new SMSRead();
        //private SMSData qsData = new SMSData();
        //private GisWriteFromSMS qsWrite = new GisWriteFromSMS();

        public ImportMain()
        {
            System.GC.Collect();
        }

        #region IDxfImport 成员

        /// <summary>
        /// 返回写Data对象
        /// </summary>
        private CadWriteData cadWriter;
        public CadWriteData CadWriter
        {
            get { return cadWriter; }
            set { cadWriter = value; }
        }
        /// <summary>
        /// 返回写Gis对象
        /// </summary>
        private GisWriteFromDxf gisWriter;
        public GisWriteFromDxf GisWriter
        {
            get { return gisWriter; }
            set { gisWriter = value; }
        }

        private string strObjNum;
        public string StrObjNum
        {
            set { strObjNum = value; }
        }

        /// <summary>
        /// 块符号插入角度字段
        /// </summary>
        private string strAngle;
        public string StrAngle
        {
            set { strAngle = value; }
        }

        /// <summary>
        /// 地图比例尺
        /// </summary>
        private string mapScale;
        public string MapScale
        {
            set { mapScale = value; }
        }

        //输入的文件名
        private string inputFileName;
        public string InputFileName
        {
            set
            {
                inputFileName = value;
            }
        }

        //输出的文件名
        private string outputFileName;
        public string OutputFileName
        {
            set
            {
                outputFileName = value;
            }
        }

        //输入文件类型
        private string fileType;
        public string FileType
        {
            set
            {
                fileType = value;
            }
        }

        //配置文件表名
        private string mdbFileName;
        public string MdbFileName
        {
            set
            {
                mdbFileName = value;
            }
        }

        //图层对照表
        private string layerTable;
        public string LayerTable
        {
            set
            {
                layerTable = value;
            }
        }

        //符号对照表
        private string symbolTable;
        public string SymbolTable
        {
            set
            {
                symbolTable = value;
            }
        }
    

        //字高比例
        private double heightScale;
        public double HeightScale
        {
            set { heightScale = value; }
        }

        //字体间距比例
        private double spaceScale;
        public double SpaceScale
        {
            set { spaceScale = value; }
        }

        //初始化
        public void ImportInit()
        {
            WaitForm.SetCaption("正在初始化参数...");
            switch (fileType)
            {
                case "1":
                    this.initDxf();
                    break;
                case "2":
                    break;
                //case "3":
                //    this.initSMS();
                //    break;
            }
        }

        public void ImportRun()
        {
            if (!PublicFun.initOk) return;
            switch (fileType)
            {
                case "1":
                    this.importDxf();
                    break;
                case "2":
                    break;
            }
        }
        #endregion

        #region 导入主程序
        /// <summary>
        /// 导入Dxf文件
        /// </summary>
        private void importDxf()
        {
            WaitForm.SetCaption("正在导入Dxf文件...");
            //bool delOrAdd=false;			//true=覆盖 false=添加
            DateTime beginDt, endDt;
            //logWriter.CurrentDs = cadr.CurrentDs;
            beginDt = DateTime.Now;
            //logWriter.BeginDt = beginDt;

            bool isCreate = true;
            bool isReadOK = true;	//读取实体是否成功
            cadr.CadReadInit();
            isReadOK = cadr.CadReadRun();

            if (isReadOK)	//读取实体如果成功则开始写实体
            {
                isCreate = writeDemo.InitData();
                writeDemo.CurrentDs = cadr.CurrentDs;		//将数据集内的实体写入到要素
                try
                {
                    writeDemo.WriteFeacture();
                    endDt = DateTime.Now;
                    //logWriter.EndDt = endDt;
                    //logWriter.WriteErrorLog();
                    WaitForm.Stop();
                    DialogResult result;
                    //result = MessageBox.Show("转换完毕，察看日志吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    //if (result == DialogResult.Yes)
                    //{
                    //    System.Diagnostics.Process.Start(logWriter.LogFile);
                    //}
                }
                catch (Exception ex)
                {
                    WaitForm.Stop();
                    Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    endDt = DateTime.Now;
                    //logWriter.EndDt = endDt;
                    //logWriter.WriteErrorLog();
                    MessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

   
        #endregion

        #region 初始化函数
        /// <summary>
        /// 导入dxf文件
        /// </summary>
        private void initDxf()
        {
            WaitForm.SetCaption("正在初始化函数...");
            cadr.InputFileName = inputFileName;
            cadr.MdbFileName = mdbFileName;
            cadr.LayerTable = layerTable;
            cadr.SymbolTable = symbolTable;
            //cadr.LogWriter = logWriter;
            //cadr.FontTable=this.fontTable;
            cadr.CadReadInit();

            this.CadWriter = cadr.CadWriter;

            writeDemo.StrAngle = strAngle;
            writeDemo.StrObjNum = strObjNum;
            writeDemo.OutputFileName = outputFileName;
            writeDemo.MdbFileName = mdbFileName;
            writeDemo.LayerTable = layerTable;
            writeDemo.CurrentDs = cadr.CurrentDs;
            //writeDemo.MinX=minX;
            //writeDemo.MinY=minY;
            //writeDemo.MaxX=maxX;
            //writeDemo.MaxY=maxY;
            //writeDemo.Precision=precision;
            //writeDemo.LogWriter = this.logWriter;

            this.gisWriter = (GisWriteFromDxf)writeDemo;
        }
      
        #endregion
    }
}