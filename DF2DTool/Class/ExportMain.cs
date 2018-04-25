using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DTool.Interface;
using DF2DTool.Class;
using ESRI.ArcGIS.Carto;
using System.IO;
using System.Windows.Forms;

namespace DF2DTool.Class
{
    public class ExportMain : IDxfExport 
    {
        private ConvertLog logWriter;	//日志对象

        public ExportMain()
        {
            gisReader = new GisRead();
            cadWriter = new CadWrite(false);
            logWriter = new ConvertLog();
        }
        #region IDxfExport 成员

        //读取文件的对象
        private IGisRead gisReader;
        public IGisRead GisReader
        {
            get { return gisReader; }
            set { gisReader = value; }
        }

        //写文件的对象
        public ICadWrite cadWriter;
        public ICadWrite CadWriter
        {
            get { return cadWriter; }
            set { cadWriter = value; }
        }

        //输入的数据集
        private DataSetNames inputDs;
        public DataSetNames InputDs
        {
            set { inputDs = value; }
        }

        //输入的选择集
        private IFeatureSelection pFeaSelection;
        public ESRI.ArcGIS.Carto.IFeatureSelection PFeaSelection
        {
            set { pFeaSelection = value; }
        }

        //地物编码对象
        private string strObjNum;
        public string StrObjNum
        {
            set { strObjNum = value; }
        }

        //块对象插入的角度
        private string strAngle;
        public string StrAngle
        {
            set { strAngle = value; }
        }

        //输出的文件名
        private string outputFileName = "";
        public string OutputFileName
        {
            set { outputFileName = value; }
        }

        //导出的文件类型	1、dxf 2、cass 3、青山智绘
        private string fileType = "1";
        public string FileType
        {
            set { fileType = value; }
        }

        //配置文件名
        private string mdbFileName = "";
        public string MdbFileName
        {
            set { mdbFileName = value; }
        }

        //图层对照表名
        private string layerTable = "";
        public string LayerTable
        {
            set { layerTable = value; }
        }

        //符号表名
        private string symbolTable = "";
        public string SymbolTable
        {
            set { symbolTable = value; }
        }

        //操作类型 1、从文件导出 2、从选择集导出
        private string operType = "";
        public string OperType
        {
            set { operType = value; }
        }

        //当前地图
        private IMap pMap = null;
        public ESRI.ArcGIS.Carto.IMap PMap
        {
            set { pMap = value; }
        }

       

        //地图的比例尺
        private double mapScale = 0;
        public double MapScale
        {
            set { mapScale = value; }
        }

        //是否输出高程值
        private bool ifoutputZ;
        public bool IfoutputZ
        {
            set { ifoutputZ = value; }
        }

        //初始化
        public bool ExportInit()
        {
            bool initOk = true;

            gisReader.InputDs = inputDs;
            gisReader.MdbFileName = mdbFileName;
            gisReader.LayerTable = layerTable;
            gisReader.SymbolTable = symbolTable;
            gisReader.PFeaSelection = pFeaSelection;
            gisReader.PMap = pMap;
            //gisReader.BlockTable=blockTable;
            //gisReader.LineTypeTable=linetypeTable;
            //gisReader.FontTable=fontTable;	
            gisReader.StrObjNum = strObjNum;
            gisReader.LogWriter = logWriter;
            gisReader.StrAngle = strAngle;
            gisReader.MapScale = mapScale;

            initOk = gisReader.GisReadInit();

            logWriter.CurrentDs = gisReader.CurrentDs;

            cadWriter.OutputFileName = outputFileName;
            cadWriter.MapScale = mapScale;
            cadWriter.IfoutputZ = ifoutputZ;
            return initOk;
        }
        #endregion

        //开始导出
        public void ExportRun()
        {
            DateTime beginDt = new DateTime();
            DateTime endDt = new DateTime();

            beginDt = DateTime.Now;
            logWriter.BeginDt = beginDt;

            bool isCreate = true;
            if (File.Exists(outputFileName))
            {
                DialogResult result;
                result = MessageBox.Show(outputFileName + " 文件已经存在，覆盖吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    isCreate = true;
                    try { File.Delete(outputFileName); }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        isCreate = false;
                    }
                }
                else
                    isCreate = false;
            }
            else
            { isCreate = true; }

            if (isCreate)
            {
                if (operType == "1")
                    gisReader.Read_EntitiesFromSelection(); //导出选择集
                else //if(operType=="1")
                    gisReader.Read_EntitiesFromDatabase();  //导出整个图层

                cadWriter.CurrentDs = gisReader.CurrentDs;
                //				cadWriter.PEnv=gisReader.PEnv ; 
                cadWriter.Process();

                endDt = DateTime.Now;
                logWriter.EndDt = endDt;
                logWriter.WriteErrorLog();

                DialogResult result;
                result = MessageBox.Show("转换完毕，查看日志吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                    System.Diagnostics.Process.Start(logWriter.LogFile);
            }
        }

    }
}
