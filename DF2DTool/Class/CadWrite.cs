using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using ESRI.ArcGIS.Geometry;
using DF2DTool.Interface;

namespace DF2DTool.Class
{
    public class CadWrite : ICadWrite
    {
      
        //写入的文件
        private StreamWriter sw;

        //是否为web转换
        private bool isWeb = false;

        public CadWrite(bool isweb)
        {
            //是否为WEB转换？

            isWeb = isweb;
        }

        #region ICadWrite 成员

        //输出的文件名字
        private string outputFileName = "";
        public string OutputFileName
        {
            set { outputFileName = value; }
            get { return outputFileName; }
        }

        //当前使用的数据集对象
        private DataSet currentDs;
        public DataSet CurrentDs
        {
            set { currentDs = value; }
        }

        //地图比例尺
        private double mapScale = 0.5;
        public double MapScale
        {
            set { mapScale = value; }
        }

        //是否输出高程值
        private bool ifoutputZ = true;
        public bool IfoutputZ
        {
            set { ifoutputZ = value; }
        }

        //		/// <summary>
        //		/// 图形范围
        //		/// </summary>
        //		private IEnvelope pEnv;
        //		public IEnvelope PEnv
        //		{
        //			set{pEnv=value;}
        //		}

        /// <summary>
        /// 打开、读写文件
        /// </summary>
        public void Process()
        {
            if (!(currentDs.Tables["pointTable"].Rows.Count == 0
                && currentDs.Tables["lineTable"].Rows.Count == 0
                && currentDs.Tables["polylineTable"].Rows.Count == 0
                && currentDs.Tables["arcTable"].Rows.Count == 0
                && currentDs.Tables["circleTable"].Rows.Count == 0
                && currentDs.Tables["textTable"].Rows.Count == 0
                && currentDs.Tables["splineTable"].Rows.Count == 0
                && currentDs.Tables["hatchTable"].Rows.Count == 0))
            {
                this.sw = new StreamWriter(outputFileName, false, Encoding.Default);// File.CreateText(outputFileName);

                this.SaveToDxf();
                this.sw.Close();
            }
        }
        #endregion

        #region 文件操作

        /// <summary>
        /// 把内容导出到Dxf文件中
        /// </summary>		
        private void SaveToDxf()
        {
            Write_Header();
            if (currentDs.Tables["Usedlinetype"].Rows.Count > 0 || currentDs.Tables["UsedFont"].Rows.Count > 0 || currentDs.Tables["UsedLayer"].Rows.Count > 0)
                Write_Tables();
            if (currentDs.Tables["UsedBlock"].Rows.Count > 0)
                Write_Blocks();
            Write_Entities();

            sw.WriteLine("0");
            sw.WriteLine("EOF");
            //MessageBox.Show("文件转换成功！","提示信息",MessageBoxButtons.OK ,MessageBoxIcon.Asterisk );
        }
        #endregion

        #region 写入Dxf文件的 Head 节
        private void Write_Header()
        {
            sw.WriteLine("0");
            sw.WriteLine("SECTION");
            sw.WriteLine("2");
            sw.WriteLine("HEADER");

            //			sw.WriteLine("9");
            //			sw.WriteLine("$EXTMIN");
            //			sw.WriteLine("10");
            //			sw.WriteLine(pEnv.XMin .ToString());
            //			sw.WriteLine("20");
            //			sw.WriteLine(pEnv.YMin .ToString());
            //			sw.WriteLine("9");
            //			sw.WriteLine("$EXTMAX");
            //			sw.WriteLine("10");
            //			sw.WriteLine(pEnv.XMax .ToString());
            //			sw.WriteLine("20");
            //			sw.WriteLine(pEnv.YMax .ToString());

            sw.WriteLine("9");
            sw.WriteLine("$LTSCALE");
            sw.WriteLine("40");
            sw.WriteLine(mapScale.ToString());

            sw.WriteLine("0");
            sw.WriteLine("ENDSEC");

        }
        #endregion

        #region 写入Dxf文件的 Table 节
        /// <summary>
        /// 写table段的信息
        /// </summary>
        private void Write_Tables()
        {
            sw.WriteLine("  0");
            sw.WriteLine("SECTION");
            sw.WriteLine("  2");
            sw.WriteLine("TABLES");

            //			Write_VPort_Information();
            if (currentDs.Tables["Usedlinetype"].Rows.Count > 0)
                Write_LineType_Table();
            if (currentDs.Tables["UsedLayer"].Rows.Count > 0)
                Write_UsedLayer_Table();
            if (currentDs.Tables["UsedFont"].Rows.Count > 0)
                Write_Font_Table();
            if (currentDs.Tables["AppID"].Rows.Count > 0)   //TianK 2009.1.15 添加
                Write_AppID_Table();

            sw.WriteLine("  0");
            sw.WriteLine("ENDSEC");
        }

        /// <summary>
        /// 写入图层定义
        /// </summary>
        private void Write_UsedLayer_Table()
        {
            DataTable layerTable = currentDs.Tables["UsedLayer"];
            string usedLayer, color;

            sw.WriteLine("  0");
            sw.WriteLine("TABLE");
            sw.WriteLine("  2");
            sw.WriteLine("LAYER");
            sw.WriteLine(" 70");
            sw.WriteLine(layerTable.Rows.Count);

            for (int i = 0; i < layerTable.Rows.Count; i++)
            {
                usedLayer = (string)layerTable.Rows[i]["Layer"];
                usedLayer = usedLayer.Replace("、", "");
                usedLayer = usedLayer.Replace("(", "");
                usedLayer = usedLayer.Replace(")", "");
                usedLayer = usedLayer.Replace("（", "");
                usedLayer = usedLayer.Replace("）", "");
                color = (string)layerTable.Rows[i]["LCOLOR"];
                sw.WriteLine("  0");
                sw.WriteLine("LAYER");
                sw.WriteLine("  2");
                sw.WriteLine(usedLayer);
                sw.WriteLine(" 70");
                sw.WriteLine("0");
                sw.WriteLine(" 62");
                if (color != "")
                {
                    sw.WriteLine(color);
                }
                else
                    sw.WriteLine("7");
                sw.WriteLine("  6");
                sw.WriteLine("Continuous");
            }

            sw.WriteLine("  0");
            sw.WriteLine("ENDTAB");
        }

        /// <summary>
        /// 写入字体定义
        /// </summary>
        private void Write_Font_Table()
        {
            DataTable fontDefine = currentDs.Tables["DefineBlockLineTypeFont"];
            DataTable fontTable = currentDs.Tables["UsedFont"];
            string usedSymbol, symDefine;
            DataRow[] secTable;
            int count;
            count = fontTable.Rows.Count;

            sw.WriteLine("  0");
            sw.WriteLine("TABLE");
            sw.WriteLine("  2");
            sw.WriteLine("STYLE");
            sw.WriteLine("  5");
            sw.WriteLine("3");
            //			sw.WriteLine("330");
            //			sw.WriteLine("0");
            sw.WriteLine("100");
            sw.WriteLine("AcDbSymbolTable");
            sw.WriteLine(" 70");
            sw.WriteLine(count.ToString());

            for (int i = 0; i < fontTable.Rows.Count; i++)
            {
                usedSymbol = (string)fontTable.Rows[i]["SymbolId"];

                secTable = fontDefine.Select("Id='" + usedSymbol + "'");
                if (secTable.Length > 0)
                {
                    symDefine = (string)secTable[0]["DEFINE"];
                    if (symDefine.IndexOf("STYLE") > -1)    //2013.11.13  Tiank  添加
                    {
                        this.write_Symbol(symDefine);
                    }
                }
            }

            sw.WriteLine("  0");
            sw.WriteLine("ENDTAB");
        }

        /// <summary>
        /// 写入线形定义
        /// </summary>
        private void Write_LineType_Table()
        {
            DataTable linetypeDefine = currentDs.Tables["DefineBlockLineTypeFont"];
            DataTable linetypeTable = currentDs.Tables["Usedlinetype"];
            string usedSymbol, symDefine;
            DataRow[] secTable;

            sw.WriteLine("  0");
            sw.WriteLine("TABLE");
            sw.WriteLine("  2");
            sw.WriteLine("LTYPE");
            sw.WriteLine(" 70");
            sw.WriteLine("166");

            for (int i = 0; i < linetypeTable.Rows.Count; i++)
            {
                usedSymbol = (string)linetypeTable.Rows[i]["SymbolId"];

                secTable = linetypeDefine.Select("ID='" + usedSymbol + "'");
                if (secTable.Length > 0)
                {
                    symDefine = (string)secTable[0]["DEFINE"];
                    if (symDefine.IndexOf("LTYPE") > -1)    //2013.11.13  Tiank  添加
                    {
                        this.write_Symbol(symDefine);
                    }
                }
            }

            sw.WriteLine("  0");
            sw.WriteLine("ENDTAB");
        }

        /// <summary>
        /// 写属性表定义  TianK 2009.1.15 添加
        /// </summary>
        private void Write_AppID_Table()
        {
            DataTable appIDTable = currentDs.Tables["AppID"];

            sw.WriteLine("  0");
            sw.WriteLine("TABLE");
            sw.WriteLine("  2");
            sw.WriteLine("APPID");
            sw.WriteLine(" 70");
            sw.WriteLine(appIDTable.Rows.Count.ToString());

            for (int i = 0; i < appIDTable.Rows.Count; i++)
            {
                sw.WriteLine("  0");
                sw.WriteLine("APPID");
                sw.WriteLine("  2");
                sw.WriteLine(appIDTable.Rows[i]["AppID"].ToString());
                sw.WriteLine(" 70");
                sw.WriteLine("0");
            }

            sw.WriteLine("  0");
            sw.WriteLine("ENDTAB");
        }

        #endregion

        #region 写入到Dxf文件的 Block 节
        private void Write_Blocks()
        {
            DataTable blockDefine = currentDs.Tables["DefineBlockLineTypeFont"];
            DataTable usedBlock = currentDs.Tables["usedBlock"];
            string usedSymbol, symDefine;
            DataRow[] secTable;

            sw.WriteLine("  0");
            sw.WriteLine("SECTION");
            sw.WriteLine("  2");
            sw.WriteLine("BLOCKS");

            for (int i = 0; i < usedBlock.Rows.Count; i++)
            {
                usedSymbol = (string)usedBlock.Rows[i]["SymbolId"];

                secTable = blockDefine.Select("Id='" + usedSymbol + "'");
                if (secTable.Length > 0)		//在sceTable中出现的要素线形定义中肯定有定义，if可以去掉
                {
                    symDefine = (string)secTable[0]["DEFINE"];
                    if (symDefine.IndexOf("BLOCK") > -1)    //2013.11.13  Tiank  添加
                    {
                        this.write_Symbol(symDefine);
                    }
                }
            }

            sw.WriteLine("  0");
            sw.WriteLine("ENDSEC");
        }



        /// <summary>
        /// 写符号定义（符号、线形、字体）到dxf文件中
        /// </summary>
        /// <param name="symbolDefine"></param>
        private void write_Symbol(string symbolDefine)
        {
            string[] dxfCode = null;
            string[] dxfCode1 = null;
            string strHead = "", strValue = "";
            dxfCode = symbolDefine.Split(new char[] { ',' });

            for (int i = 0; i < dxfCode.Length - 1; i++)
            {
                dxfCode1 = dxfCode[i].Split(new char[] { ':' });

                strHead = dxfCode1[0].Substring(1, dxfCode1[0].Length - 1);
                strValue = dxfCode1[1].Substring(0, dxfCode1[1].Length - 1);
                sw.WriteLine(strHead);
                sw.WriteLine(strValue);
            }
        }

        #endregion

        #region 写入到Dxf文件的 Entities节

        #region 绘制实体的主体部分
        /// <summary>
        /// 写入到Dxf文件的 Entities节
        /// </summary>
        private void Write_Entities()
        {
            //写实体节的开始部分
            sw.WriteLine("  0");
            sw.WriteLine("SECTION");
            sw.WriteLine("  2");
            sw.WriteLine("ENTITIES");

            writePoint();
            writePolyline();					//绘制polyline和polygon 以及polyline+弧
            writeArc();					//独立的弧
            writeBezier3Curve();			//贝塞尔曲线
            writeAnno();

            //写实体节的结尾部分
            sw.WriteLine("  0");
            sw.WriteLine("ENDSEC");
        }
        #endregion

        #region 绘制注记
        /// <summary>
        /// 绘制注记对象
        /// </summary>
        private void writeAnno()
        {
            DataTable lineTable = new DataTable();
            string dLayer, dSym;
            string X1, Y1, X2, Y2, text, angle, height, Number = "0123456789";
            string sHeight, sScale, sFontName;               //2009.9.20 TianK  添加
            string font = "HZTXT,HT";                  //2009.9.20 TianK  添加
            double widthScale, mapScale1, height1;
            bool isNumber = false;

            mapScale1 = mapScale * 2;

            lineTable = currentDs.Tables["textTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                text = (string)lineTable.Rows[i]["text"];
                string newText = text.Replace("\r\n", "　");
                newText = newText.Replace("Φ", "%%c");   //2008.1.25   TianK 添加 
                newText = newText.Replace("φ", "%%c");
                newText = newText.Trim();    //2013.11.11   TianK 添加   去除前后的空格
                isNumber = false;
                if (Number.IndexOf(newText[0]) != -1 && Number.IndexOf(newText[newText.Length - 1]) != -1)
                    isNumber = true;
                dLayer = (string)lineTable.Rows[i]["dLayer"];
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                //dColor=(string)lineTable.Rows[i]["lcolor"];  //2007.06.06 TianK 添加
                X1 = (string)lineTable.Rows[i]["X1"];
                Y1 = (string)lineTable.Rows[i]["Y1"];
                Y1 = Convert.ToString((double.Parse(Y1) + 0.3));
                X2 = (string)lineTable.Rows[i]["X2"];
                Y2 = (string)lineTable.Rows[i]["Y2"];
                height = (string)lineTable.Rows[i]["dheight"];
                sHeight = (string)lineTable.Rows[i]["SHeight"];        //2009.9.20 TianK  添加
                sScale = (string)lineTable.Rows[i]["SScale"];        //2009.9.20 TianK  添加
                sFontName = (string)lineTable.Rows[i]["SFontName"];        //2009.9.20 TianK  添加

                if (sHeight != "")                                        //2009.9.20 TianK  修改
                {
                    try
                    {
                        height1 = double.Parse(sHeight) * mapScale1;
                    }
                    catch (Exception ex)            //2013.10.30 TianK  修改
                    {
                        Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                        Console.WriteLine((string)lineTable.Rows[i]["AttriBute"] + "----字高数据格式错误！\r\n");
                        height1 = 0.9 * mapScale1;
                    }
                    if (sScale != "")
                    {
                        widthScale = double.Parse(sScale);
                    }
                    else
                    {
                        switch (height)              //2012.1.10 TianK 添加  用于给定字宽比例
                        {
                            case "6":
                                widthScale = 0.7;
                                break;
                            case "7.5":
                                widthScale = 1;
                                break;
                            case "8":
                                widthScale = 0.8;
                                break;
                            case "8.25":
                                widthScale = 0.8;
                                break;
                            default:
                                widthScale = 1;
                                break;
                        }
                    }
                    dSym = sFontName;
                    if (dSym == "")
                    {
                        dSym = "STANDARD";
                    }
                }
                else
                {
                    switch (height)              //2007.08.09 TianK 添加
                    {
                        case "6":
                            height1 = 1 * mapScale1;
                            widthScale = 0.7;
                            dSym = "STANDARD";
                            break;
                        case "7.5":
                            height1 = 1.25 * mapScale1;
                            widthScale = 1;
                            dSym = "HZTXT";
                            break;
                        case "8":
                            height1 = 0.9 * mapScale1;
                            widthScale = 0.8;
                            dSym = "STANDARD";
                            break;
                        case "8.25":                           //2011.08.01 TianK 添加,把8.25大的注记转为0.9个大
                            height1 = 0.9 * mapScale1;
                            widthScale = 0.8;
                            dSym = "STANDARD";
                            break;
                        case "8.5":
                            height1 = 1 * mapScale1;
                            widthScale = 1;
                            dSym = "STANDARD";
                            break;
                        case "9":
                            height1 = 1.5 * mapScale1;
                            widthScale = 1;
                            dSym = "HZTXT";                  //2013.9.29 TianK 修改，按照邱波建议，如果没有原字体的，所有9个大的字全部转换为HZTXT字体
                            if (isNumber)
                                dSym = "STANDARD";
                            break;
                        case "10.5":
                            height1 = 1.5 * mapScale1;
                            widthScale = 1;
                            dSym = "HT";
                            break;
                        case "12":
                            height1 = 2 * mapScale1;
                            widthScale = 1;
                            dSym = "HT";
                            break;
                        default:
                            height1 = double.Parse(height) / 6 * mapScale1;
                            widthScale = 1;
                            dSym = "HT";
                            break;
                    }
                    if (widthScale >= 0.8 && isNumber)
                        widthScale = 0.8;
                }

                angle = (string)lineTable.Rows[i]["Dirction"];


                sw.WriteLine("  0");
                sw.WriteLine("TEXT");
                sw.WriteLine("  8");
                sw.WriteLine(dLayer);
                //if (dColor!="")                //2007.06.06 TianK 添加
                //{
                //    sw.WriteLine(" 62");
                //    sw.WriteLine(dColor);
                //}
                sw.WriteLine("  7");
                sw.WriteLine(dSym);
                sw.WriteLine("  1");
                sw.WriteLine(newText);
                sw.WriteLine(" 72");
                sw.WriteLine("0");
                sw.WriteLine(" 73");
                sw.WriteLine("0");
                sw.WriteLine(" 50");
                sw.WriteLine(angle);
                sw.WriteLine(" 40");
                sw.WriteLine(height1.ToString());
                sw.WriteLine(" 41");
                sw.WriteLine(widthScale.ToString());
                sw.WriteLine(" 10");
                sw.WriteLine(X1);
                sw.WriteLine(" 20");
                sw.WriteLine(Y1);
                sw.WriteLine(" 11");
                sw.WriteLine(X2);
                sw.WriteLine(" 21");
                sw.WriteLine(Y2);
                WriteAttribute((string)lineTable.Rows[i]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
            }
        }
        #endregion

        #region 绘制Point对象
        /// <summary>
        /// 绘制点
        /// </summary>
        private void writePoint()
        {
            DataTable lineTable = new DataTable();
            string dLayer, dSym, dAngle;
            double X, Y, Z;

            lineTable = currentDs.Tables["pointTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                dLayer = (string)lineTable.Rows[i]["dLayer"];
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                dAngle = (string)lineTable.Rows[i]["Dirction"];
                if (dAngle == "")
                    dAngle = "0";
                double angle = 0;
                if (double.Parse(dAngle) != 0)
                    angle = -double.Parse(dAngle);      //2007.06.18 TianK 添加
                dAngle = angle.ToString();
                //dColor=(string)lineTable.Rows[i]["lcolor"];  //2007.06.06 TianK 添加
                X = double.Parse((string)lineTable.Rows[i]["X"]);
                Y = double.Parse((string)lineTable.Rows[i]["Y"]);
                Z = double.Parse((string)lineTable.Rows[i]["Z"]);
                if (Z > 8848 || Z < -100)
                    Z = 0;

                sw.WriteLine("  0");
                sw.WriteLine("INSERT");
                sw.WriteLine("  8");
                sw.WriteLine(dLayer);
                //if (dColor!="")                //2007.06.06 TianK 添加
                //{
                //    sw.WriteLine(" 62");
                //    sw.WriteLine(dColor);
                //}
                sw.WriteLine("  2");
                sw.WriteLine(dSym);		//Blockname
                if (dAngle != "" && dAngle != "0")   //2007.06.08 TianK 修改 
                {
                    sw.WriteLine(" 50");
                    sw.WriteLine(dAngle);
                }
                sw.WriteLine(" 41");		//插入的x方向比例
                sw.WriteLine(mapScale.ToString());
                sw.WriteLine(" 42");		//插入的y方向比例
                sw.WriteLine(mapScale.ToString());
                sw.WriteLine(" 10");
                sw.WriteLine(X.ToString());
                sw.WriteLine(" 20");
                sw.WriteLine(Y.ToString());
                if (ifoutputZ == true)
                {
                    sw.WriteLine(" 30");
                    sw.WriteLine(Z.ToString());
                }
                WriteAttribute((string)lineTable.Rows[i]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
            }
        }

        #endregion

        #region 绘制polyline
        /// <summary>
        /// 绘制线
        /// </summary>
        private void writePolyline()
        {
            DataTable lineTable = new DataTable();
            string pUid = "", comUid = "";
            string dLayer, dSym, sCode;
            DataRow[] secTable;

            lineTable = currentDs.Tables["polylineTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                pUid = (string)lineTable.Rows[i]["plId"];
                if (pUid != comUid)
                {
                    dLayer = (string)lineTable.Rows[i]["dLayer"];
                    dSym = (string)lineTable.Rows[i]["DifGISCode"];
                    sCode = (string)lineTable.Rows[i]["SMSSymbol"];   //2007.08.16 TianK 添加

                    if (ifoutputZ == true)      //如果导出为三维图形
                    {
                        if (dSym.ToUpper() == "CONTINUOUS" && (string)lineTable.Rows[i]["lwidth"] == "" && sCode.Length == 6 && sCode[0].ToString() == "5")   //如果线型为CONTINUOUS且没设定线宽的管线则按三维多段线处理
                        {
                            //如果一个实体中有凸度则绘制polyline+弧
                            secTable = lineTable.Select("plId='" + pUid + "' and td<>'' and td<>'0'");
                            if (secTable.Length > 0)
                            {
                                secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");
                                write_Line_Arc(secTable, dLayer, dSym);
                            }
                            else
                            {
                                secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");
                                write_Line(secTable, dLayer, dSym);
                            }
                        }
                        else              //否则按二维多段线处理
                        {
                            //如果一个实体中有凸度则绘制polyline+弧
                            secTable = lineTable.Select("plId='" + pUid + "' and td<>''and td<>'0'");
                            if (secTable.Length > 0)
                            {
                                secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");
                                Write_EWPolyLine_Arc(secTable, dLayer, dSym);
                            }
                            else
                            {
                                secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");
                                Write_EWPolyLine_Line(secTable, dLayer, dSym);
                            }
                        }
                    }
                    else                  //否则导出为二维图形，并把编码写到厚度字段中
                    {
                        //如果一个实体中有凸度则绘制polyline+弧
                        secTable = lineTable.Select("plId='" + pUid + "' and td<>''and td<>'0'");
                        if (secTable.Length > 0)
                        {
                            secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");
                            Write_LWPolyLine_Arc(secTable, dLayer, dSym, sCode);
                        }
                        else
                        {
                            secTable = lineTable.Select("plId='" + pUid + "'", "plIndex");
                            Write_LWPolyLine_Line(secTable, dLayer, dSym, sCode);
                        }
                    }
                    comUid = pUid;

                }
            }
        }
        #region 绘制三维polyline，导出为三维图形时

        /// <summary>
        /// 绘制polyline+弧
        /// </summary>
        /// <param name="sTable"></param>
        /// <param name="slayer"></param>
        /// <param name="ssym"></param>
        private void write_Line_Arc(DataRow[] sTable, string slayer, string ssym)
        {
            double beginX, beginY, beginZ, endX, endY, endZ;
            string tmpLayer = slayer, td = "";
            string tmpSym = ssym;
            DataRow[] tmpTable = sTable;
            string LWIDTH = "0";

            for (int j = 0; j <= tmpTable.Length - 1; j++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                beginX = double.Parse((string)tmpTable[j]["beginX"]);
                beginY = double.Parse((string)tmpTable[j]["beginY"]);
                beginZ = double.Parse((string)tmpTable[j]["beginZ"]);
                if (beginZ > 8848 || beginZ < -100)
                    beginZ = 0;
                endX = double.Parse((string)tmpTable[j]["endX"]);
                endY = double.Parse((string)tmpTable[j]["endY"]);
                endZ = double.Parse((string)tmpTable[j]["endZ"]);
                if (endZ > 8848 || endZ < -100)
                    endZ = 0;
                //LCOLOR=(string)tmpTable[j]["lcolor"];
                if ((string)tmpTable[j]["lwidth"] != "")
                    LWIDTH = (string)tmpTable[j]["lwidth"];

                //				index=(string)tmpTable[j]["plIndex"];
                if ((object)tmpTable[j]["td"] == System.DBNull.Value)
                    td = "";
                else
                    td = (string)tmpTable[j]["td"];

                if (j == 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("POLYLINE");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 66");
                    sw.WriteLine("1");
                    //if (LCOLOR!="")            //2007.06.06 TianK 修改
                    //{
                    //    sw.WriteLine(" 62");		//颜色
                    //    sw.WriteLine(LCOLOR);
                    //}
                    sw.WriteLine(" 40");		//起始宽度
                    sw.WriteLine(LWIDTH);
                    sw.WriteLine(" 41");		//中止宽度
                    sw.WriteLine(LWIDTH);

                    if (ssym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(ssym);
                    }
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                    //'起点
                    sw.WriteLine("  0");
                    sw.WriteLine("VERTEX");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    sw.WriteLine(" 30");
                    sw.WriteLine(beginZ.ToString());
                    if (td != "")
                    {
                        sw.WriteLine(" 42");
                        sw.WriteLine(td);
                    }
                }

                if ((j != tmpTable.Length - 1) && j != 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("VERTEX");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    sw.WriteLine(" 30");
                    sw.WriteLine(beginZ.ToString());

                    if (td != "")
                    {
                        sw.WriteLine(" 42");
                        sw.WriteLine(td);
                    }
                }
                if (j == tmpTable.Length - 1)
                {
                    if (endX != 0 && endY != 0)
                    {
                        if (td == "")
                        {
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(beginX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(beginY.ToString());
                            if (ifoutputZ == true)
                            {
                                sw.WriteLine(" 30");
                                sw.WriteLine(beginZ.ToString());
                            }
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(endX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(endY.ToString());
                            sw.WriteLine(" 30");
                            sw.WriteLine(endZ.ToString());
                        }
                        else
                        {
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(beginX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(beginY.ToString());
                            if (ifoutputZ == true)
                            {
                                sw.WriteLine(" 30");
                                sw.WriteLine(beginZ.ToString());
                            }
                            sw.WriteLine(" 42");
                            sw.WriteLine(td.ToString());
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(endX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(endY.ToString());
                            sw.WriteLine(" 30");
                            sw.WriteLine(endZ.ToString());

                        }
                    }
                    sw.WriteLine("  0");
                    sw.WriteLine("SEQEND");
                }
            }
        }

        /// <summary>
        /// 绘制polyline中的线实体
        /// </summary>
        /// <param name="secTable"></param>
        private void write_Line(DataRow[] sTable, string slayer, string ssym)
        {
            double beginX, beginY, beginZ, endX = 0, endY = 0, endZ;
            double x, y;		//记录每条线的终点
            int newJ = 0;
            string tmpLayer = slayer;
            string tmpSym = ssym;
            DataRow[] tmpTable = sTable;
            string LWIDTH = "0";

            for (int j = 0; j <= tmpTable.Length - 1; j++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                newJ = j;
                x = endX; y = endY;		//针对<----的情况

                beginX = double.Parse((string)tmpTable[j]["beginX"]);
                beginY = double.Parse((string)tmpTable[j]["beginY"]);
                beginZ = double.Parse((string)tmpTable[j]["beginZ"]);
                if (beginZ > 8848 || beginZ < -100)
                    beginZ = 0;
                endX = double.Parse((string)tmpTable[j]["endX"]);
                endY = double.Parse((string)tmpTable[j]["endY"]);
                endZ = double.Parse((string)tmpTable[j]["endZ"]);
                if (endZ > 8848 || endZ < -100)
                    endZ = 0;
                //LCOLOR=(string)tmpTable[j]["lcolor"];
                if ((string)tmpTable[j]["lwidth"] != "")
                    LWIDTH = (string)tmpTable[j]["lwidth"];


                //如果上一条线的终点与这一条线的起点不一样，则认为上一条线与这一条线不是一个整体
                if ((beginX != x || beginY != y) && j > 0)
                {
                    newJ = 0;
                    sw.WriteLine("  0");
                    sw.WriteLine("SEQEND");
                }

                if (newJ == 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("POLYLINE");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 66");
                    sw.WriteLine("1");
                    //if (LCOLOR!="")            //2007.06.06 TianK 修改
                    //{
                    //    sw.WriteLine(" 62");		//颜色
                    //    sw.WriteLine(LCOLOR);
                    //}
                    sw.WriteLine(" 40");		//起始宽度
                    sw.WriteLine(LWIDTH);
                    sw.WriteLine(" 41");		//中止宽度
                    sw.WriteLine(LWIDTH);
                    ///////////////////////
                    if (ssym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(ssym);
                    }
                    sw.WriteLine(" 70");
                    sw.WriteLine("8");
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加

                    //'起点
                    sw.WriteLine("  0");
                    sw.WriteLine("VERTEX");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    sw.WriteLine(" 30");
                    sw.WriteLine(beginZ.ToString());
                    sw.WriteLine(" 70");
                    sw.WriteLine("32");
                }

                sw.WriteLine("  0");
                sw.WriteLine("VERTEX");
                sw.WriteLine("  8");
                sw.WriteLine(tmpLayer);
                sw.WriteLine(" 10");
                sw.WriteLine(endX.ToString());
                sw.WriteLine(" 20");
                sw.WriteLine(endY.ToString());
                sw.WriteLine(" 30");
                sw.WriteLine(endZ.ToString());
                sw.WriteLine(" 70");
                sw.WriteLine("32");

                if (j == tmpTable.Length - 1)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("SEQEND");
                }
            }
        }
        #endregion

        #region 绘制二维polyline，导出为三维图形但需要设定线型或线宽时

        /// <summary>
        /// 绘制二维polyline+弧
        /// </summary>
        /// <param name="sTable"></param>
        /// <param name="slayer"></param>
        /// <param name="ssym"></param>
        private void Write_EWPolyLine_Arc(DataRow[] sTable, string slayer, string ssym)
        {
            double beginX, beginY, endX, endY, biaoGao;
            string tmpLayer = slayer, td = "";
            string tmpSym = ssym;
            DataRow[] tmpTable = sTable;
            string LWIDTH = "0";
            biaoGao = getBiaoGaoZ(tmpTable);         //2012.10.18TianK修改

            for (int j = 0; j <= tmpTable.Length - 1; j++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                beginX = double.Parse((string)tmpTable[j]["beginX"]);
                beginY = double.Parse((string)tmpTable[j]["beginY"]);
                endX = double.Parse((string)tmpTable[j]["endX"]);
                endY = double.Parse((string)tmpTable[j]["endY"]);
                //LCOLOR=(string)tmpTable[j]["lcolor"];
                if ((string)tmpTable[j]["lwidth"] != "")
                    LWIDTH = (string)tmpTable[j]["lwidth"];

                //				index=(string)tmpTable[j]["plIndex"];
                if ((object)tmpTable[j]["td"] == System.DBNull.Value)
                    td = "";
                else
                    td = (string)tmpTable[j]["td"];

                if (j == 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("POLYLINE");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 66");
                    sw.WriteLine("1");
                    //if (LCOLOR!="")            //2007.06.06 TianK 修改
                    //{
                    //    sw.WriteLine(" 62");		//颜色
                    //    sw.WriteLine(LCOLOR);
                    //}
                    sw.WriteLine(" 40");		//起始宽度
                    sw.WriteLine(LWIDTH);
                    sw.WriteLine(" 41");		//中止宽度
                    sw.WriteLine(LWIDTH);

                    if (ssym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(ssym);
                    }
                    sw.WriteLine(" 10");
                    sw.WriteLine("0");
                    sw.WriteLine(" 20");
                    sw.WriteLine("0");
                    sw.WriteLine(" 30");
                    sw.WriteLine(biaoGao.ToString());
                    sw.WriteLine(" 70");
                    sw.WriteLine("0");
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                    //'起点
                    sw.WriteLine("  0");
                    sw.WriteLine("VERTEX");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    sw.WriteLine(" 30");
                    sw.WriteLine(biaoGao.ToString());
                    if (td != "")
                    {
                        sw.WriteLine(" 42");
                        sw.WriteLine(td);
                    }
                }

                if ((j != tmpTable.Length - 1) && j != 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("VERTEX");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    sw.WriteLine(" 30");
                    sw.WriteLine(biaoGao.ToString());
                    if (td != "")
                    {
                        sw.WriteLine(" 42");
                        sw.WriteLine(td);
                    }
                }
                if (j == tmpTable.Length - 1)
                {
                    if (endX != 0 && endY != 0)
                    {
                        if (td == "")
                        {
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(beginX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(beginY.ToString());
                            sw.WriteLine(" 30");
                            sw.WriteLine(biaoGao.ToString());
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(endX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(endY.ToString());
                            sw.WriteLine(" 30");
                            sw.WriteLine(biaoGao.ToString());
                        }
                        else
                        {
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(beginX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(beginY.ToString());
                            sw.WriteLine(" 30");
                            sw.WriteLine(biaoGao.ToString());
                            sw.WriteLine(" 42");
                            sw.WriteLine(td.ToString());
                            sw.WriteLine("  0");
                            sw.WriteLine("VERTEX");
                            sw.WriteLine("  8");
                            sw.WriteLine(tmpLayer);
                            sw.WriteLine(" 10");
                            sw.WriteLine(endX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(endY.ToString());
                            sw.WriteLine(" 30");
                            sw.WriteLine(biaoGao.ToString());
                        }
                    }
                    sw.WriteLine("  0");
                    sw.WriteLine("SEQEND");

                }
            }
        }

        /// <summary>
        /// 绘制二维polyline中的线实体
        /// </summary>
        /// <param name="secTable"></param>
        private void Write_EWPolyLine_Line(DataRow[] sTable, string slayer, string ssym)
        {
            double beginX, beginY, endX = 0, endY = 0, biaoGao;
            double x, y;		//记录每条线的终点
            int newJ = 0;
            string tmpLayer = slayer;
            string tmpSym = ssym;
            DataRow[] tmpTable = sTable;
            string LWIDTH = "0";
            biaoGao = getBiaoGaoZ(tmpTable);                     //2012.10.18TianK修改

            for (int j = 0; j <= tmpTable.Length - 1; j++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                newJ = j;
                x = endX; y = endY;		//针对<----的情况

                beginX = double.Parse((string)tmpTable[j]["beginX"]);
                beginY = double.Parse((string)tmpTable[j]["beginY"]);
                endX = double.Parse((string)tmpTable[j]["endX"]);
                endY = double.Parse((string)tmpTable[j]["endY"]);
                //LCOLOR=(string)tmpTable[j]["lcolor"];
                if ((string)tmpTable[j]["lwidth"] != "")
                    LWIDTH = (string)tmpTable[j]["lwidth"];


                //如果上一条线的终点与这一条线的起点不一样，则认为上一条线与这一条线不是一个整体
                if ((beginX != x || beginY != y) && j > 0)
                {
                    newJ = 0;
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                    sw.WriteLine("  0");
                    sw.WriteLine("SEQEND");
                }

                if (newJ == 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("POLYLINE");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    if (ssym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(ssym);
                    }
                    sw.WriteLine(" 66");
                    sw.WriteLine("1");
                    //if (LCOLOR!="")            //2007.06.06 TianK 修改
                    //{
                    //    sw.WriteLine(" 62");		//颜色
                    //    sw.WriteLine(LCOLOR);
                    //}
                    sw.WriteLine(" 40");		//起始宽度
                    sw.WriteLine(LWIDTH);
                    sw.WriteLine(" 41");		//中止宽度
                    sw.WriteLine(LWIDTH);
                    sw.WriteLine(" 10");
                    sw.WriteLine("0");
                    sw.WriteLine(" 20");
                    sw.WriteLine("0");
                    sw.WriteLine(" 30");
                    sw.WriteLine(biaoGao.ToString());
                    sw.WriteLine(" 70");
                    sw.WriteLine("0");
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                    //'起点
                    sw.WriteLine("  0");
                    sw.WriteLine("VERTEX");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    sw.WriteLine(" 30");
                    sw.WriteLine(biaoGao.ToString());
                }

                sw.WriteLine("  0");
                sw.WriteLine("VERTEX");
                sw.WriteLine("  8");
                sw.WriteLine(tmpLayer);
                sw.WriteLine(" 10");
                sw.WriteLine(endX.ToString());
                sw.WriteLine(" 20");
                sw.WriteLine(endY.ToString());
                sw.WriteLine(" 30");
                sw.WriteLine(biaoGao.ToString());
                if (j == tmpTable.Length - 1)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("SEQEND");
                }
            }
        }
        #endregion

        #region 得到有高程点的平均标高

        private double getBiaoGaoZ(DataRow[] sTable)
        {
            double biaoGaoZ = 0, beginZ, endZ;
            int count = 0;
            for (int j = 0; j <= sTable.Length - 1; j++)
            {
                if (j == 0)
                {
                    beginZ = double.Parse((string)sTable[j]["beginZ"]);
                    endZ = double.Parse((string)sTable[j]["endZ"]);
                    if (beginZ != 0 && beginZ > -100)
                    {
                        biaoGaoZ = biaoGaoZ + beginZ;
                        count = count + 1;
                    }
                    if (endZ != 0 && endZ > -100)
                    {
                        biaoGaoZ = biaoGaoZ + endZ;
                        count = count + 1;
                    }
                }
                else
                {
                    endZ = double.Parse((string)sTable[j]["endZ"]);
                    if (endZ != 0 && endZ > -100)
                    {
                        biaoGaoZ = biaoGaoZ + endZ;
                        count = count + 1;
                    }
                }

            }
            if (count != 0)
                return biaoGaoZ / count;
            else
                return 0;
        }

        #endregion

        #region 绘制lwpolyline,导出为二维图形时
        /// <summary>
        /// 绘制lwpolyline+弧
        /// </summary>
        /// <param name="sTable"></param>
        /// <param name="slayer"></param>
        /// <param name="ssym"></param>
        private void Write_LWPolyLine_Arc(DataRow[] sTable, string slayer, string ssym, string sCode)
        {
            double beginX, beginY, endX, endY;
            string tmpLayer = slayer, td = "";
            string tmpSym = ssym;
            DataRow[] tmpTable = sTable;
            string LWIDTH = "";

            #region
            for (int j = 0; j <= tmpTable.Length - 1; j++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                beginX = double.Parse((string)tmpTable[j]["beginX"]);
                beginY = double.Parse((string)tmpTable[j]["beginY"]);
                endX = double.Parse((string)tmpTable[j]["endX"]);
                endY = double.Parse((string)tmpTable[j]["endY"]);
                //LCOLOR=(string)tmpTable[j]["lcolor"];
                if ((string)tmpTable[j]["lwidth"] != "")
                    LWIDTH = (string)tmpTable[j]["lwidth"];

                if ((object)tmpTable[j]["td"] == System.DBNull.Value)
                    td = "";
                else
                    td = (string)tmpTable[j]["td"];

                if (j == 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("LWPOLYLINE");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    //					sw.WriteLine(" 66");
                    //					sw.WriteLine("1");						
                    //if (LCOLOR!="")            //2007.06.06 TianK 修改
                    //{
                    //    sw.WriteLine(" 62");		//颜色
                    //    sw.WriteLine(LCOLOR);
                    //}
                    if (LWIDTH != "")            //2008.01.17 TianK 修改
                    {
                        sw.WriteLine(" 43");		//固定宽度
                        sw.WriteLine(LWIDTH);
                    }
                    if (ssym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(ssym);
                    }
                    sw.WriteLine(" 70");
                    sw.WriteLine("0");
                    //if (sCode!="")            //2007.08.16 TianK 修改
                    //{
                    //    sw.WriteLine(" 39");		//颜色
                    //    sw.WriteLine(sCode);
                    //}
                    //'起点
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    if (td != "")
                    {
                        sw.WriteLine(" 42");
                        sw.WriteLine(td);
                    }
                }

                if ((j != tmpTable.Length - 1) && j != 0)
                {
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                    if (td != "")
                    {
                        sw.WriteLine(" 42");
                        sw.WriteLine(td);
                    }
                }
                if (j == tmpTable.Length - 1)
                {
                    if (endX != 0 && endY != 0)
                    {
                        if (td == "")
                        {
                            sw.WriteLine(" 10");
                            sw.WriteLine(beginX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(beginY.ToString());
                            sw.WriteLine(" 10");
                            sw.WriteLine(endX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(endY.ToString());
                        }
                        else
                        {
                            sw.WriteLine(" 10");
                            sw.WriteLine(beginX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(beginY.ToString());
                            sw.WriteLine(" 42");
                            sw.WriteLine(td.ToString());
                            sw.WriteLine(" 10");
                            sw.WriteLine(endX.ToString());
                            sw.WriteLine(" 20");
                            sw.WriteLine(endY.ToString());
                        }
                    }
                }
                if (j == 0)
                {
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                }
            }
            #endregion
        }

        /// <summary>
        /// 绘制lwpolyline中的线实体
        /// </summary>
        /// <param name="secTable"></param>
        private void Write_LWPolyLine_Line(DataRow[] sTable, string slayer, string ssym, string sCode)
        {
            double beginX, beginY, endX = 0, endY = 0;
            double x, y;		//记录每条线的终点
            int newJ = 0;
            string tmpLayer = slayer;
            string tmpSym = ssym;
            DataRow[] tmpTable = sTable;
            string LWIDTH = "";

            for (int j = 0; j <= tmpTable.Length - 1; j++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                newJ = j;
                x = endX; y = endY;		//针对<----的情况

                beginX = double.Parse((string)tmpTable[j]["beginX"]);
                beginY = double.Parse((string)tmpTable[j]["beginY"]);
                endX = double.Parse((string)tmpTable[j]["endX"]);
                endY = double.Parse((string)tmpTable[j]["endY"]);
                //LCOLOR=(string)tmpTable[j]["lcolor"];
                if ((string)tmpTable[j]["lwidth"] != "")
                    LWIDTH = (string)tmpTable[j]["lwidth"];

                //如果上一条线的终点与这一条线的起点不一样，则认为上一条线与这一条线不是一个整体
                if ((beginX != x || beginY != y) && j > 0)
                {
                    newJ = 0;
                }

                if (newJ == 0)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("LWPOLYLINE");
                    sw.WriteLine("  8");
                    sw.WriteLine(tmpLayer);
                    if (ssym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(ssym);
                    }
                    //					sw.WriteLine(" 66");
                    //					sw.WriteLine("1");	
                    //if (LCOLOR!="")            //2007.06.06 TianK 修改
                    //{
                    //    sw.WriteLine(" 62");		//颜色
                    //    sw.WriteLine(LCOLOR);
                    //}
                    if (sCode != "")            //2008.01.17 TianK 修改
                    {
                        sw.WriteLine(" 43");		//固定宽度
                        sw.WriteLine(LWIDTH);
                    }
                    sw.WriteLine("  6");
                    sw.WriteLine(ssym);
                    sw.WriteLine(" 70");
                    sw.WriteLine("0");
                    //if (sCode!="")            //2007.08.16 TianK 修改
                    //{
                    //    sw.WriteLine(" 39");		//颜色
                    //    sw.WriteLine(sCode);
                    //}
                    //'起点
                    sw.WriteLine(" 10");
                    sw.WriteLine(beginX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(beginY.ToString());
                }

                sw.WriteLine(" 10");
                sw.WriteLine(endX);
                sw.WriteLine(" 20");
                sw.WriteLine(endY);
                if (newJ == 0)
                {
                    WriteAttribute((string)tmpTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                }

            }
        }
        #endregion

        #endregion

        #region 绘制Arc(独立的Arc)
        /// <summary>
        /// 绘制Arc和circle（独立的）
        /// </summary>
        private void writeArc()
        {
            DataTable lineTable = new DataTable();
            string dLayer, dSym, dWidth, sCode;   //2007.06.06 TianK 修改
            double cenX, cenY, cenZ, radius, fromAngle, toAngle;

            lineTable = currentDs.Tables["arcTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                if (!isWeb)
                {
                    //记录写入的实体个数
                    PublicFun.WrtieEntCount++;
                    //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                }

                dLayer = (string)lineTable.Rows[i]["dLayer"];
                dSym = (string)lineTable.Rows[i]["DifGISCode"];
                sCode = (string)lineTable.Rows[i]["SMSSymbol"];   //2007.08.16 TianK 添加
                //dColor=(string)lineTable.Rows[i]["lcolor"];  //2007.06.06 TianK 添加
                dWidth = (string)lineTable.Rows[i]["lwidth"];  //2007.06.06 TianK 添加
                cenX = double.Parse((string)lineTable.Rows[i]["cenX"]);
                cenY = double.Parse((string)lineTable.Rows[i]["cenY"]);
                cenZ = double.Parse((string)lineTable.Rows[i]["cenZ"]);
                radius = double.Parse((string)lineTable.Rows[i]["radius"]);
                fromAngle = double.Parse((string)lineTable.Rows[i]["fromAng"]);
                toAngle = double.Parse((string)lineTable.Rows[i]["toAng"]);
                if (Math.Abs(fromAngle - toAngle) < 0.0001)
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("CIRCLE");
                    sw.WriteLine("  8");
                    sw.WriteLine(dLayer);
                    if (dSym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(dSym);
                    }
                    //if (dColor!="")                //2007.06.06 TianK 添加
                    //{
                    //    sw.WriteLine(" 62");
                    //    sw.WriteLine(dColor);
                    //}
                    if (sCode != "")            //2007.08.16 TianK  添加
                    {
                        sw.WriteLine(" 39");		//颜色
                        sw.WriteLine(sCode);
                    }
                    sw.WriteLine(" 10");
                    sw.WriteLine(cenX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(cenY.ToString());
                    if (ifoutputZ == true)
                    {
                        sw.WriteLine(" 30");
                        sw.WriteLine(cenZ.ToString());
                    }

                    sw.WriteLine(" 40");
                    sw.WriteLine(radius.ToString());
                }
                else
                {
                    sw.WriteLine("  0");
                    sw.WriteLine("ARC");
                    sw.WriteLine("  8");
                    sw.WriteLine(dLayer);
                    if (dSym != "")            //2007.12.20 TianK 修改
                    {
                        sw.WriteLine("  6");
                        sw.WriteLine(dSym);
                    }
                    //if (dColor!="")                //2007.06.06 TianK 添加
                    //{
                    //    sw.WriteLine(" 62");
                    //    sw.WriteLine(dColor);
                    //}
                    if (sCode != "")            //2007.08.16 TianK  添加
                    {
                        sw.WriteLine(" 39");		//颜色
                        sw.WriteLine(sCode);
                    }
                    WriteAttribute((string)lineTable.Rows[i]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                    sw.WriteLine(" 10");
                    sw.WriteLine(cenX.ToString());
                    sw.WriteLine(" 20");
                    sw.WriteLine(cenY.ToString());
                    if (ifoutputZ == true)
                    {
                        sw.WriteLine(" 30");
                        sw.WriteLine(cenZ.ToString());
                    }
                    sw.WriteLine(" 40");
                    sw.WriteLine(radius.ToString());
                    sw.WriteLine(" 50");
                    sw.WriteLine(fromAngle.ToString());
                    sw.WriteLine(" 51");
                    sw.WriteLine(toAngle.ToString());
                }
                WriteAttribute((string)lineTable.Rows[i]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
            }
        }

        #endregion

        #region 绘制Bezier3Curve
        /// <summary>
        /// 绘制贝塞尔曲线，由于没有数据所以暂时没有测试
        /// </summary>
        private void writeBezier3Curve()
        {
            DataTable lineTable = new DataTable();
            DataRow pRow;
            string pUid = "", comUid = "";
            string dLayer, dSym, dWidth, sCode;   //2007.06.06 TianK 修改
            DataRow[] secTable;
            double X, Y, Z;

            lineTable = currentDs.Tables["splineTable"];
            for (int i = 0; i <= lineTable.Rows.Count - 1; i++)
            {
                pRow = lineTable.Rows[i];

                pUid = (string)lineTable.Rows[i]["plId"];
                if (pUid != comUid)
                {
                    if (!isWeb)
                    {
                        //记录写入的实体个数
                        PublicFun.WrtieEntCount++;
                        //this.WriteEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
                    }

                    dLayer = (string)lineTable.Rows[i]["dLayer"];
                    dSym = (string)lineTable.Rows[i]["DifGISCode"];
                    sCode = (string)lineTable.Rows[i]["SMSSymbol"];   //2007.08.16 TianK 添加
                    //dColor=(string)lineTable.Rows[i]["lcolor"];  //2007.06.06 TianK 添加
                    dWidth = (string)lineTable.Rows[i]["lwidth"];  //2007.06.06 TianK 添加

                    secTable = lineTable.Select("plId='" + pUid + "'");

                    if (ifoutputZ == true)
                    {
                        for (int j = 0; j <= secTable.Length - 1; j++)
                        {
                            X = double.Parse((string)secTable[j]["X"]);
                            Y = double.Parse((string)secTable[j]["Y"]);
                            Z = double.Parse((string)secTable[j]["Z"]);
                            if (j == 0)
                            {
                                sw.WriteLine("  0");
                                sw.WriteLine("POLYLINE");
                                sw.WriteLine("  8");
                                sw.WriteLine(dLayer);
                                sw.WriteLine(" 66");
                                sw.WriteLine("1");
                                sw.WriteLine("  6");
                                sw.WriteLine(dSym);
                                //if (dColor!="")                //2007.06.06 TianK 添加
                                //{
                                //    sw.WriteLine(" 62");
                                //    sw.WriteLine(dColor);
                                //}
                                if (dWidth != "")                //2007.06.06 TianK 添加
                                {
                                    sw.WriteLine(" 40");		//起始宽度
                                    sw.WriteLine(dWidth);
                                    sw.WriteLine(" 41");		//中止宽度
                                    sw.WriteLine(dWidth);
                                }
                                sw.WriteLine(" 70");
                                sw.WriteLine("8");
                                WriteAttribute((string)secTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加

                                //'起点
                                sw.WriteLine("  0");
                                sw.WriteLine("VERTEX");
                                sw.WriteLine("  8");
                                sw.WriteLine(dLayer);
                                sw.WriteLine(" 10");
                                sw.WriteLine(X.ToString());
                                sw.WriteLine(" 20");
                                sw.WriteLine(Y.ToString());
                                if (ifoutputZ == true)
                                {
                                    sw.WriteLine(" 30");
                                    sw.WriteLine(Z.ToString());
                                }
                                sw.WriteLine(" 70");
                                sw.WriteLine("32");
                            }
                            else
                            {
                                sw.WriteLine("  0");
                                sw.WriteLine("VERTEX");
                                sw.WriteLine("  8");
                                sw.WriteLine(dLayer);
                                sw.WriteLine(" 10");
                                sw.WriteLine(X.ToString());
                                sw.WriteLine(" 20");
                                sw.WriteLine(Y.ToString());
                                sw.WriteLine(" 30");
                                sw.WriteLine(Z.ToString());
                                sw.WriteLine(" 70");
                                sw.WriteLine("32");
                            }
                            if (j == secTable.Length - 1)
                            {
                                sw.WriteLine("  0");
                                sw.WriteLine("SEQEND");
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j <= secTable.Length - 1; j++)
                        {
                            X = double.Parse((string)secTable[j]["X"]);
                            Y = double.Parse((string)secTable[j]["Y"]);
                            if (j == 0)     								//'起点
                            {
                                sw.WriteLine("  0");
                                sw.WriteLine("LWPOLYLINE");
                                sw.WriteLine("  8");
                                sw.WriteLine(dLayer);
                                //if (dColor!="")            //2007.06.06 TianK 修改
                                //{
                                //    sw.WriteLine(" 62");		//颜色
                                //    sw.WriteLine(dColor);
                                //}
                                sw.WriteLine(" 43");		//固定宽度
                                sw.WriteLine(dWidth);
                                sw.WriteLine("  6");
                                sw.WriteLine(dSym);
                                sw.WriteLine(" 70");
                                sw.WriteLine("0");
                                //if (sCode!="")            //2007.08.16 TianK 修改
                                //{
                                //    sw.WriteLine(" 39");		//颜色
                                //    sw.WriteLine(sCode);
                                //}
                                sw.WriteLine(" 10");
                                sw.WriteLine(X.ToString());
                                sw.WriteLine(" 20");
                                sw.WriteLine(Y.ToString());
                            }
                            else
                            {
                                sw.WriteLine(" 10");
                                sw.WriteLine(X.ToString());
                                sw.WriteLine(" 20");
                                sw.WriteLine(Y.ToString());
                            }
                            if (j == 0)
                            {
                                WriteAttribute((string)secTable[j]["AttriBute"]);  //写入扩展属性 2009.1.15 TianK 添加
                            }
                        }
                    }
                    comUid = pUid;
                }
            }
        }
        #endregion

        #region 写入扩展属性
        private void WriteAttribute(string attribute)
        {
            if (attribute == "")
                return;
            try
            {
                string[] attri = attribute.Split(new char[] { '&' });
                for (int i = 0; i < attri.Length; i = i + 2)
                {
                    sw.WriteLine("1001");
                    sw.WriteLine(attri[i].ToString());
                    sw.WriteLine("1000");
                    sw.WriteLine(attri[i + 1].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return;
            }
        }
        #endregion

        #endregion
    }	
}
