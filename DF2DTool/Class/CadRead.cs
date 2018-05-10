using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using System.Text;
using DF2DTool.Interface;
using DFWinForms.Class;

namespace DF2DTool.Class
{
    /// <summary>
    ///
    /// </summary>
    public class CadRead : ICadRead
    {
        StreamReader sr;							//读入的dxf文件

        CadWriteData cwd;							//写入到核心数据集的对象
        double minX, minY, maxX, maxY;					//最大X,最大Y,最小X,最小Y

        public CadRead()
        {
            //
        }

        #region 对外的属性和方法

        /// <summary>
        /// 日志对象
        /// </summary>
        private ConvertLog logWriter;
        public ConvertLog LogWriter
        {
            set { logWriter = value; }
        }

        /// <summary>
        /// 返回写DataSet对象
        /// </summary>
        public CadWriteData CadWriter
        {
            get { return (CadWriteData)cwd; }
        }

        /// <summary>
        /// 输入的文件名称
        /// </summary>
        private string inputFileName = "";
        public string InputFileName
        {
            get { return inputFileName; }
            set { inputFileName = value; }
        }

        //配置文件表名
        private string mdbFileName = "";
        public string MdbFileName
        {
            set
            {
                mdbFileName = value;
            }
        }

        //图层对照表
        private string layerTable = "";
        public string LayerTable
        {
            set
            {
                layerTable = value;
            }
        }

        //符号对照表
        private string symbolTable = "";
        public string SymbolTable
        {
            set
            {
                symbolTable = value;
            }
        }

        ////字体定义文件
        //private string fontTable="";
        //public string FontTable
        //{
        //    set{fontTable=value;}
        //}

        //数据集
        private DataSet currentDs;
        public DataSet CurrentDs
        {
            get { return cwd.CoreDs; }
            set { currentDs = value; }
        }

        //读取初始化
        public void CadReadInit()
        {
            cwd = new CadWriteData();
            cwd.MdbFileName = mdbFileName;
            cwd.LayerTable = layerTable;
            cwd.SymbolTable = symbolTable;
            cwd.LogWriter = logWriter;
            //cwd.FontTable=fontTable;
            cwd.CadWriteInit();
        }

        //开始读取实体
        public bool CadReadRun()
        {
            WaitForm.SetCaption("正在读取Cad实体...");
            getLayer();                        //读取图层表
            bool rtnBool = this.getEntities();   //读取对象实体
            return rtnBool;
        }

        ///// <summary>
        ///// 获取Dxf文件的坐标范围
        ///// </summary>
        ///// <param name="minx"></param>
        ///// <param name="miny"></param>
        ///// <param name="maxx"></param>
        ///// <param name="maxy"></param>
        //public void getDxfLimite(ref double minx,ref double miny,ref double maxx,ref double maxy)
        //{
        //    try{this.getLimite();}
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message+"\r\n"+ex.StackTrace);
        //        MessageBox.Show("打开 "+inputFileName+" 文件错误！");
        //    }
        //    minx=minX;
        //    miny=minY;
        //    maxx=maxX;
        //    maxy=maxY;
        //}

        #endregion

        #region 内部方法
        ///// <summary>
        ///// 获取Dxf文件的坐标范围
        ///// </summary>
        //private void getLimite()
        //{
        //    bool boolMin=false;
        //    bool boolMax=false;
        //    sr=new StreamReader (inputFileName,Encoding.Default );

        //    string [] dxfCode=null;			
        //    dxfCode = readCodes().Split(new char [] {'$'});

        //    while (dxfCode[1]!="EOF")
        //    {
        //        //最小XY
        //        if(dxfCode[1]=="$EXTMIN")
        //        {
        //            dxfCode = readCodes().Split(new char[] { '$' });
        //            if(dxfCode[0]=="10")
        //                minX=double.Parse(dxfCode[1])-1000;

        //            dxfCode = readCodes().Split(new char[] { '$' });
        //            if(dxfCode[0]=="20")
        //                minY=double.Parse(dxfCode[1])-1000;

        //            boolMin=true;
        //        }

        //        //最大XY
        //        if(dxfCode[1]=="$EXTMAX")
        //        {
        //            dxfCode = readCodes().Split(new char[] { '$' });
        //            if(dxfCode[0]=="10")
        //                maxX=double.Parse(dxfCode[1])+1000;

        //            dxfCode = readCodes().Split(new char[] { '$' });
        //            if(dxfCode[0]=="20")
        //                maxY=double.Parse(dxfCode[1])+1000;

        //            boolMax=true;
        //        }

        //        dxfCode = readCodes().Split(new char[] { '$' });
        //        if(boolMin&&boolMax)
        //            break;
        //    }



        //    sr.Close();							
        //}

        /// <summary>
        /// 读取dxf中的Table段中的Layer数据存贮到DataSet中
        /// </summary>
        private void getLayer()
        {
            try
            {
                sr = new StreamReader(inputFileName, Encoding.Default);
                this.readSection("TABLES", "LAYER");
                sr.Close();

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                MessageBox.Show("Layer读取失败！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取dxf中的Table段中的LineType数据存贮到DataSet中
        /// </summary>
        private void getLineType()
        {
            try
            {
                sr = new StreamReader(inputFileName, Encoding.Default);
                this.readSection("TABLES", "LTYPE");
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                MessageBox.Show("Table读取失败！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取dxf中的Blocks段中的Block数据存贮到DataSet中
        /// </summary>
        private void getBlock()
        {
            try
            {
                sr = new StreamReader(inputFileName, Encoding.Default);
                this.readSection("BLOCKS", "BLOCK");
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                MessageBox.Show("Block读取失败！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取dxf中的Entities段中的所有实体数据存贮到DataSet中	
        /// 如果ReadSection函数的第二个参数是空的话则输出所有实体	
        /// </summary>
        private bool getEntities()
        {
            bool rtnBool = true;
            try
            {
                sr = new StreamReader(inputFileName, Encoding.Default);
                this.readSection("ENTITIES", "");
                sr.Close();

                rtnBool = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                MessageBox.Show("读取实体错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtnBool = false;
            }
            return rtnBool;
        }
        #endregion

        #region dxf文件读取操作
        /// <summary>
        /// 读取Dxf文件中指定的section段中的全部内容	
        /// 例：	ReadSection("ENTITIES","LINE")
        /// 读取ENTITIES段中的所有line对象
        /// ReadSection("ENTITIES","") ，返回所有实体
        /// </summary>
        private void readSection(string section, string eleType)
        {
            string[] dxfCode = null;
            dxfCode = readCodes().Split(new char[] { '$' });

            while (dxfCode[1] != "EOF")
            {
                if ((dxfCode[0] == "0") && (dxfCode[1] == "SECTION"))
                {
                    //读入下一编码段
                    dxfCode = readCodes().Split(new char[] { '$' });
                    //指定段的内容
                    if ((dxfCode[0] == "2") && (dxfCode[1] == section))
                    {
                        //说明：由于BLOCK段由BLOCK...ENDBLK包含
                        //而其他内容遇到0就结束，所以作此解释
                        if (section == "TABLES")
                            this.processTables("TABLE", "LAYER");
                        else if (section == "BLOCKS")
                            this.processBlocks(section);
                        else
                            this.processElements(section, eleType);
                    }
                }
                dxfCode = readCodes().Split(new char[] { '$' });
            }
        }

        /// <summary>
        /// 读取指定段内的内容，将每个完整的类型定义写到string中
        /// </summary>
        private void processElements(string section, string eleType)
        {
            string[] dxfCode = null;		//存贮每次读取块的数组（2组）			
            string entStr = "";

            dxfCode = readCodes().Split(new char[] { '$' });
            while (dxfCode[1] != "ENDSEC")
            {
                entStr = entStr + "$[" + dxfCode[0] + "]$" + dxfCode[1];
                dxfCode = readCodes().Split(new char[] { '$' });
                if (dxfCode[0] == "0" && dxfCode[1] != "VERTEX" && dxfCode[1] != "SEQEND")
                {
                    //根据dxf文件的版本不同而不同
                    entStr = entStr.Substring(1, entStr.Length - 1);
                    //判断此段是否是指定的字符串，如LINETYPE,LAYER,起到过滤的作用
                    if (eleType.Length == 0)
                        cwd.AddElement(entStr);
                    else
                    {
                        if (entStr.StartsWith("[0]$" + eleType))
                            cwd.AddElement(entStr);
                    }
                    entStr = "";
                }
            }
        }

        /// <summary>
        /// 读取BLOCKS表段中块的定义
        /// </summary>
        /// <param name="section"></param>
        private void processBlocks(string section)
        {
            string[] dxfCode = null;		//存贮每次读取块的数组（2组）			
            string entStr = "";

            dxfCode = readCodes().Split(new char[] { '$' });

            while (dxfCode[1] != "ENDSEC")
            {
                if (dxfCode[0] == "0" && dxfCode[1] == "BLOCK")
                { entStr = dxfCode[0] + "$" + dxfCode[1]; }
                else if (dxfCode[0] == "0" && dxfCode[1] == "ENDBLK")
                {
                    if (entStr.EndsWith("$"))
                        entStr += "0&ENDBLK";
                    else
                        entStr += "$0$ENDBLK";
                    //Debug.WriteLine(entStr);
                    cwd.AddElement(entStr);
                }
                else if (entStr.IndexOf("0$BLOCK") > -1)
                { entStr = entStr + "$" + dxfCode[0] + "$" + dxfCode[1]; }

                dxfCode = readCodes().Split(new char[] { '$' });
            }
        }

        /// <summary>
        /// 读取TABLES表段中相关表的定义
        /// </summary>
        /// <param name="section"></param>
        private void processTables(string section, string eleType)
        {
            string[] dxfCode = null;		//存贮每次读取块的数组（2组）			

            dxfCode = readCodes().Split(new char[] { '$' });

            while (dxfCode[1] != "ENDSEC")
            {
                if (dxfCode[0] == "0" && dxfCode[1] == section)
                {
                    dxfCode = readCodes().Split(new char[] { '$' });
                    if (dxfCode[0] == "2" && dxfCode[1] == eleType)
                    {
                        processLayer(eleType);
                    }
                }
                dxfCode = readCodes().Split(new char[] { '$' });
            }
        }

        /// <summary>
        /// 处理layer表
        /// </summary>
        private void processLayer(string eleType)
        {
            string[] dxfCode = null;		//存贮每次读取块的数组（2组）			
            string entStr = "";

            dxfCode = readCodes().Split(new char[] { '$' });

            while (dxfCode[1] != "ENDTAB")
            {
                entStr = entStr + "$[" + dxfCode[0] + "]$" + dxfCode[1];
                dxfCode = readCodes().Split(new char[] { '$' });

                if (dxfCode[0] == "0")
                {
                    //根据dxf文件的版本不同而不同
                    entStr = entStr.Substring(1, entStr.Length - 1);
                    if (entStr.StartsWith("[0]$" + eleType))
                    {
                        cwd.AddTable(entStr);
                    }
                    entStr = "";
                }
            }
        }

        /// <summary>
        /// 读取Dxf文件，每次读取两行
        /// 返回组码和值的字符串
        /// </summary>
        /// <returns></returns>
        private string readCodes()
        {
            string codeStr, valStr;
            codeStr = sr.ReadLine().Trim();
            valStr = sr.ReadLine().Trim();
            return codeStr + "$" + valStr;
        }
        #endregion
    }
}
