using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DFileConvert.Interface;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DFWinForms.Class;

namespace DF2DFileConvert.Class
{
    /// <summary>

    /// </summary>
    public class ImportMain : IDxfImport
    {
        private ICadRead cadr = new CadRead();
        private IGisWriteFromDxf writeDemo = new GisWriteFromDxf();
        private ConvertLog logWriter = new ConvertLog();
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

        /// <summary>
        /// 返回读取青山智慧的对象
        /// </summary>
        //public SMSRead SMSReader
        //{
        //    get { return qsRead; }
        //}

        /// <summary>
        /// 返回青山智慧写arcgis的对象
        /// </summary>
        //public GisWriteFromSMS SMSWriter
        //{
        //    get { return qsWrite; }
        //}
        /// <summary>
        /// 地物字段名
        /// </summary>
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

        ////MinX
        //private double minX;
        //public double MinX
        //{
        //    set{minX=value;}
        //}

        ////MinY
        //private double minY;
        //public double MinY
        //{
        //    set{minY=value;}
        //}

        ////MaxX
        //private double maxX;
        //public double MaxX
        //{
        //    set{maxX=value;}
        //}

        ////MaxY
        //private double maxY;
        //public double MaxY
        //{
        //    set{maxY=value;}
        //}

        ////Precision
        //private double precision;
        //public double Precision
        //{
        //    set{precision=value;}
        //}

        //private string fontTable;
        //public string FontTable
        //{
        //    set{fontTable=value;}
        //}

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
                //case "3":
                //    this.importSMS();
                //    break;
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
            logWriter.CurrentDs = cadr.CurrentDs;
            beginDt = DateTime.Now;
            logWriter.BeginDt = beginDt;

            bool isCreate = true;
            bool isReadOK = true;	//读取实体是否成功
            isReadOK = cadr.CadReadRun();

            if (isReadOK)	//读取实体如果成功则开始写实体
            {
                isCreate = writeDemo.InitData();
                writeDemo.CurrentDs = cadr.CurrentDs;		//将数据集内的实体写入到要素
                try
                {
                    writeDemo.WriteFeacture();
                    endDt = DateTime.Now;
                    logWriter.EndDt = endDt;
                    logWriter.WriteErrorLog();
                    WaitForm.Stop();
                    DialogResult result;
                    result = MessageBox.Show("转换完毕，察看日志吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(logWriter.LogFile);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                    endDt = DateTime.Now;
                    logWriter.EndDt = endDt;
                    logWriter.WriteErrorLog();
                    MessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        /// <summary>
        /// 导入青山智慧文件
        /// </summary>
        //private void importSMS()
        //{
        //    bool delOrAdd = true;			//true=覆盖 false=添加

        //    DateTime beginDt, endDt;

        //    logWriter.CurrentDs = qsData.CoreDs;
        //    beginDt = DateTime.Now;
        //    logWriter.BeginDt = beginDt;

        //    bool isCreate = true;

        //    if (File.Exists(outputFileName))	//文件已存在
        //    {
        //        string result = "Yes";
        //        frmFileMessage frmFm = new frmFileMessage(outputFileName);
        //        frmFm.ShowDialog();

        //        result = frmFileMessage.rtnValue;

        //        if (result == "Yes")		//覆盖文件
        //        {
        //            isCreate = true;
        //            try { File.Delete(outputFileName); }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        //                MessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                isCreate = false;
        //            }
        //        }
        //        else if (result == "Add")
        //        {
        //            isCreate = true;					//向文件内添加内容 ，如过存在该表向表中添加记录，如果没有表则新建新表
        //            delOrAdd = false;
        //        }
        //        else
        //            return;
        //    }
        //    else
        //    { isCreate = true; }							//文件未存在

        //    if (isCreate)
        //    {
        //        qsRead.SMSReadRun();
        //        isCreate = qsWrite.InitData(delOrAdd);
        //        try
        //        {
        //            qsWrite.WriteFeacture();
        //            endDt = DateTime.Now;
        //            logWriter.EndDt = endDt;
        //            logWriter.WriteErrorLog();
        //            DialogResult result;
        //            result = MessageBox.Show("转换完毕，察看日志吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
        //            if (result == DialogResult.Yes)
        //            {
        //                System.Diagnostics.Process.Start(logWriter.LogFile);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
        //            MessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        }
        //    }
        //}
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
            cadr.LogWriter = logWriter;
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
            writeDemo.LogWriter = this.logWriter;

            this.gisWriter = (GisWriteFromDxf)writeDemo;
        }
        /// <summary>
        /// 导入青山智慧文件
        /// </summary>
        //private void initSMS()
        //{
        //    logWriter.CurrentDs = qsData.CoreDs;
        //    qsRead.InputFileName = this.inputFileName;
        //    qsRead.LogWriter = this.logWriter;
        //    qsRead.MdbFileName = mdbFileName;
        //    qsRead.LayerTable = layerTable;
        //    qsRead.SymbolTable = symbolTable;
        //    qsRead.CurrentDs = qsData.CoreDs;
        //    qsRead.MapScale = mapScale;
        //    qsRead.SMSReadInit();

        //    qsWrite.StrObjNum = strObjNum;
        //    qsWrite.StrAngle = strAngle;
        //    qsWrite.MapScale = mapScale;
        //    qsWrite.OutputFileName = outputFileName;
        //    qsWrite.MdbFileName = mdbFileName;
        //    qsWrite.LayerTable = layerTable;
        //    qsWrite.CurrentDs = qsData.CoreDs;
        //    //qsWrite.MinX=minX;
        //    //qsWrite.MinY=minY;
        //    //qsWrite.MaxX=maxX;
        //    //qsWrite.MaxY=maxY;					
        //    qsWrite.HeightScale = heightScale;
        //    qsWrite.SpaceScale = spaceScale;
        //    //qsWrite.Precision=precision;
        //    qsWrite.LogWriter = logWriter;
        //}
        #endregion
    }
}