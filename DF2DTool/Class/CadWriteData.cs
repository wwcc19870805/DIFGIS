using System;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text;
using DF2DTool.Interface;

namespace DF2DTool.Class
{
    /// <summary>
    ///
    /// </summary>
    //定义委托
    public delegate void ReadCadHandler(int rEntCount, int wEntCount);
    public delegate void ReadPlHandler(string typestr, int plcount);

    public class CadWriteData : CoreData, ICadWriteData
    {
        //定义委托 读取每一体条实体时激活	
        public event ReadCadHandler ReadEvent;
        //委托 读取polyline中的每个小实体触发事件
        public event ReadPlHandler readPlEvent;

        public CadWriteData()
            : base()
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

        //初始化
        public void CadWriteInit()
        {
            try
            {
                loadLayerTable();
                loadSymbolTable();
                loadAttributeTable();   //2013.11.07  TianK 添加
                //loadFontDefine();
                PublicFun.initOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                MessageBox.Show("读取配置文件错误", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PublicFun.initOk = false;
                return;
            }
        }

        /// <summary>
        /// 按实体的类型加入到不同的实体表中
        /// </summary>
        /// <param name="entCode"></param>
        public void AddElement(string entStr)
        {
            string tmpStr = "", typeOk = "";

            if (entStr.Length > 2)
                tmpStr = entStr.Substring(4, entStr.IndexOf("$", 4) - 4);
            else
                tmpStr = "";

            typeOk = "LINE,SPLINE,POINT,ARC,CIRCLE,HATCH,TEXT,MTEXT,INSERT,POLYLINE,LWPOLYLINE";
            //已处理完毕的实体类型 
            if (typeOk.IndexOf(tmpStr) >= 0)
            {
                //激活事件
                //PublicFun.ReadEntCount++;
                //this.ReadEvent(PublicFun.ReadEntCount, PublicFun.WrtieEntCount);
            }

            switch (tmpStr)
            {

                case "LINE":			//ok
                    this.addLine(entStr);
                    break;
                case "SPLINE":			//需要公式
                    this.addSpline(entStr);
                    break;
                case "POINT":			//ok
                    this.addPoint(entStr, 0);
                    break;
                case "ARC":				//ok
                    this.addArc(entStr);
                    break;
                case "CIRCLE":			//ok
                    this.addCircle(entStr);
                    break;
                case "HATCH":
                    this.addHatch(entStr);
                    break;
                case "TEXT":
                    this.addText(entStr);
                    break;
                case "MTEXT":
                    this.addMText(entStr);
                    break;
                case "INSERT":			//需要进一步确认更加详细的对应关系
                    this.addPoint(entStr, 1);
                    break;
                case "POLYLINE":		//ok
                    this.addPolyline(entStr);
                    break;
                case "LWPOLYLINE":		//ok
                    this.addLwPolyline(entStr);   //2007.05.29 TianK 修改
                    //this.addPolyline(entStr);
                    break;
            }
        }

        #endregion

        #region  把表段中的图层表添加到图层颜色表表中 2013.03.25  TianK 添加
        /// <summary>
        /// 把表中的图层表添加到CAD的图层表中
        /// </summary>
        /// <param name="entCode"></param>
        public void AddTable(string entStr)
        {
            string sName, sColor;

            sName = getValue(entStr, "2");
            sColor = getValue(entStr, "62");


            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["CADLayerTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["LayerName"] = sName;
            tmpRow["Color"] = sColor;

            tmpTable.Rows.Add(tmpRow);
        }

        #endregion

        #region 调入图层、符号、属性对照表
        /// <summary>
        /// 读取layertable表
        /// </summary>
        private void loadLayerTable()
        {
            ITable pLayerTable;
            pLayerTable = PublicFun.GetAccessTable(mdbFileName, layerTable);

            IRow pRow;
            ICursor pCursor;

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = base.CoreDs.Tables["layerTable"];

            pCursor = pLayerTable.Search(null, true);
            pRow = pCursor.NextRow();

            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["CadLayer"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CadLayer")));
                tmpRow["GisLayer"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("GisLayer")));
                tmpRow["GisLayerType"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("GisLayerType")));
                tmpRow["LayerName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("GISLayerName")));
                tmpRow["DfGisLayerCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DFGisLayerCode")));
                tmpTable.Rows.Add(tmpRow);

                pRow = pCursor.NextRow();
            }
        }

        /// <summary>
        /// 读取symboltable表
        /// </summary>
        private void loadSymbolTable()
        {
            ITable pSymbolTable;
            pSymbolTable = PublicFun.GetAccessTable(mdbFileName, symbolTable);

            IRow pRow;
            ICursor pCursor;

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = base.CoreDs.Tables["symbolTable"];

            pCursor = pSymbolTable.Search(null, true);
            pRow = pCursor.NextRow();

            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();

                tmpRow["CassCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CassCode")));  //读取符号对照表中的CASS编码
                tmpRow["SymbolCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("SymbolCode")));
                tmpRow["SymbolName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("SymbolName")));
                tmpRow["DifGISCode"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGISCode")));
                tmpRow["DifGisLayer"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGisLayer")));
                //tmpRow["DFontName"]=getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DFontName")));
                //tmpRow["SymbolType"]=getTableValue((object)pRow.get_Value(pRow.Fields.FindField("SymbolType")));
                //tmpRow["FontName"]=getTableValue((object)pRow.get_Value(pRow.Fields.FindField("FontName")));
                tmpTable.Rows.Add(tmpRow);

                pRow = pCursor.NextRow();
            }
        }

        /// <summary>
        /// 读取AttributeCASStoDifGIS对照表   2013.11.07  TianK 添加
        /// </summary>
        private void loadAttributeTable()
        {
            ITable pLayerTable;
            pLayerTable = PublicFun.GetAccessTable(mdbFileName, "AttributeCASStoDifGIS");

            IRow pRow;
            ICursor pCursor;

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = base.CoreDs.Tables["AttributeCASStoDifGIS"];

            pCursor = pLayerTable.Search(null, true);
            pRow = pCursor.NextRow();

            while (pRow != null)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["CASSAttributeName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("CASSAttributeName"))).ToUpper();
                tmpRow["DifGISAttributeName"] = getTableValue((object)pRow.get_Value(pRow.Fields.FindField("DifGISAttributeName"))).ToUpper();
                tmpTable.Rows.Add(tmpRow);
                pRow = pCursor.NextRow();
            }
        }

        /// <summary>
        /// 获取数据库字段的值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string getTableValue(object obj)
        {
            string rtnValue = "";
            if (obj == System.DBNull.Value)
                rtnValue = "";
            else
                rtnValue = obj.ToString();

            return rtnValue;
        }
        #endregion

        #region 将实体写入到dataset中

        #region 添加point
        private void addPoint(string entStr, int ptORin)
        {
            //type说明:0--点  1--insert
            //说明：Layer,Color和Linetype的定义要通过新的图层定义（layerCompar）表中去找
            //通过源图层的名称+目标图层的类型设置对应关系
            string sName, sSym, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string dAngle;
            string AttriBute = "";
            double angle;

            sName = getValue(entStr, "8");
            dType = "1";		//点图层
            dAngle = getValue(entStr, "50");

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            if (ptORin == 0)       //2009.9.16   TianK 修改
                sSym = "";
            else
            {
                sSym = this.getValue(entStr, "2");
            }

            if (dSym == "" || dSym.Substring(5, 1) != "1")   //如果根据CASS编码找不到则根据块名找
            {
                if (sSym != "")                  //2008.3.1   TianK 修改
                {
                    dSym = this.findSymbol(sSym, "DifGISCode", ref dName, ref strSymbolName);
                    if (dSym != "")  //如果根据线性名找到了则把线形记下来
                        cassCodeOrsSym = sSym;
                }
            }
            if (dAngle == "")
                dAngle = "0";
            if (dAngle != "0")
            {
                angle = 360 - double.Parse(dAngle);
                dAngle = angle.ToString();
            }

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["pointTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["SymbolName"] = strSymbolName;
            tmpRow["X"] = getValue(entStr, "10");
            tmpRow["Y"] = getValue(entStr, "20");
            tmpRow["Z"] = getValue(entStr, "30");
            tmpRow["Dirction"] = dAngle;
            tmpRow["SMSCode"] = sName;
            tmpRow["SMSSymbol"] = cassCodeOrsSym;
            tmpRow["AttriBute"] = AttriBute;
            tmpRow["BlockName"] = sSym;  //2009.9.16   TianK 添加

            tmpTable.Rows.Add(tmpRow);
        }
        #endregion

        #region 添加line
        private void addLine(string entStr)
        {
            //说明：Layer,Color和Linetype的定义要通过新的图层定义（layerCompar）表中去找
            //通过源图层的名称+目标图层的类型设置对应关系
            //20060613线形的符号名称不需要对应关系只取厚度字段就行了
            string sName, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string AttriBute = "";

            sName = getValue(entStr, "8");
            dType = "2";		//线图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["lineTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["SymbolName"] = strSymbolName;
            tmpRow["beginX"] = getValue(entStr, "10");
            tmpRow["beginY"] = getValue(entStr, "20");
            tmpRow["beginZ"] = getValue(entStr, "30");
            tmpRow["endX"] = getValue(entStr, "11");
            tmpRow["endY"] = getValue(entStr, "21");
            tmpRow["endZ"] = getValue(entStr, "31");
            tmpRow["SMSCode"] = sName;
            tmpRow["SMSSymbol"] = cassCodeOrsSym;
            tmpRow["AttriBute"] = AttriBute;
            tmpRow["LineType"] = getValue(entStr, "6");    //2009.9.16   TianK 添加

            tmpTable.Rows.Add(tmpRow);
        }
        #endregion

        #region 添加Arc
        private void addArc(string entStr)
        {
            string sName, sSym, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string AttriBute = "";

            sName = getValue(entStr, "8");
            dType = "2";		//线图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["arcTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["SymbolName"] = strSymbolName;
            tmpRow["cenX"] = getValue(entStr, "10");
            tmpRow["cenY"] = getValue(entStr, "20");
            tmpRow["cenZ"] = getValue(entStr, "30");
            tmpRow["radius"] = getValue(entStr, "40");
            tmpRow["fromAng"] = getValue(entStr, "50");
            tmpRow["toAng"] = getValue(entStr, "51");
            tmpRow["SMSCode"] = sName;
            tmpRow["SMSSymbol"] = cassCodeOrsSym;
            tmpRow["AttriBute"] = AttriBute;
            tmpRow["LineType"] = getValue(entStr, "6");    //2009.9.16   TianK 添加

            tmpTable.Rows.Add(tmpRow);
        }
        #endregion

        #region 添加cricle
        private void addCircle(string entStr)
        {
            string sName, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string AttriBute = "";

            sName = getValue(entStr, "8");
            dType = "2";		//线图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["circleTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["SymbolName"] = strSymbolName;
            tmpRow["cenX"] = getValue(entStr, "10");
            tmpRow["cenY"] = getValue(entStr, "20");
            tmpRow["cenZ"] = getValue(entStr, "30");
            tmpRow["radius"] = getValue(entStr, "40");
            tmpRow["SMSCode"] = sName;
            tmpRow["SMSSymbol"] = cassCodeOrsSym;
            tmpRow["AttriBute"] = AttriBute;
            tmpRow["LineType"] = getValue(entStr, "6");    //2009.9.16   TianK 添加

            tmpTable.Rows.Add(tmpRow);
        }
        #endregion

        #region 添加hatch
        private void addHatch(string entStr)
        {
            Guid uid = new Guid();
            uid = Guid.NewGuid();
            string sName, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string AttriBute = "";

            sName = getValue(entStr, "8");
            dType = "3";		//面图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            int ptCnt = 0;
            ptCnt = Int32.Parse(getValue(entStr, "93"));
            string[] ptX = new string[ptCnt];
            string[] ptY = new string[ptCnt];
            string[] ptZ = new string[ptCnt];
            ptX = this.processHatchStr(entStr, "10", ptCnt);
            ptY = this.processHatchStr(entStr, "20", ptCnt);
            ptZ = this.processHatchStr(entStr, "30", ptCnt);

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = base.CoreDs.Tables["hatchTable"];
            for (int i = 0; i < ptCnt - 1; i++)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["plId"] = uid.ToString();
                tmpRow["dLayer"] = dName;
                tmpRow["DifGISCode"] = dSym;
                tmpRow["SymbolName"] = strSymbolName;
                tmpRow["beginX"] = ptX[i];
                tmpRow["beginY"] = ptY[i];
                tmpRow["beginZ"] = ptZ[i];
                tmpRow["endX"] = ptX[i + 1];
                tmpRow["endY"] = ptY[i + 1];
                tmpRow["endZ"] = ptZ[i + 1];
                tmpRow["SMSCode"] = sName;
                tmpRow["SMSSymbol"] = cassCodeOrsSym;
                tmpRow["AttriBute"] = AttriBute;
                tmpRow["LineType"] = getValue(entStr, "6");    //2009.9.16   TianK 添加

                tmpTable.Rows.Add(tmpRow);
            }
        }
        #endregion

        #region 添加text
        private void addText(string entStr)
        {
            string sName, cassCodeOrsSym = "";
            string dName = "", dType, dHeight, dAngle, dSym = "", strSymbolName = "";
            string dScale;             //2009.915 TianK 增加
            string AttriBute = "";

            dType = "4";
            sName = getValue(entStr, "8");
            dHeight = getValue(entStr, "40");   //字高

            dAngle = getValue(entStr, "50");   //字符旋转角度
            if (dAngle == "")
                dAngle = "0.0";
            dScale = getValue(entStr, "41");   //2009.915 TianK 增加  读出字符宽度比例
            if (dScale == "")
                dScale = "1";

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["textTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["dHeight"] = getValue(entStr, "40");
            tmpRow["Dirction"] = dAngle;
            tmpRow["X1"] = getValue(entStr, "10");
            tmpRow["Y1"] = getValue(entStr, "20");
            tmpRow["Z1"] = getValue(entStr, "30");
            tmpRow["TEXT"] = getValue(entStr, "1");
            tmpRow["SMSCode"] = sName;
            tmpRow["SMSSymbol"] = cassCodeOrsSym;
            tmpRow["AttriBute"] = AttriBute;
            tmpRow["SHeight"] = dHeight;        //2009.9.15 TianK 增加
            tmpRow["SScale"] = dScale;         //2009.9.15 TianK 增加
            tmpRow["SFontName"] = getValue(entStr, "7");        //2009.9.15 TianK 增加

            tmpTable.Rows.Add(tmpRow);
        }
        #endregion

        #region 添加mtext
        //问题，换行符 暂时需要修改
        //矩形宽度
        //字体
        private void addMText(string entStr)
        {
            string sName, cassCodeOrsSym = "";
            string dName = "", dType, dHeight, dAngle, dSym = "", strSymbolName = "";
            string AttriBute = "";

            dType = "4";
            sName = getValue(entStr, "8");
            dHeight = getValue(entStr, "40");
            dAngle = getValue(entStr, "50");
            if (dAngle == "")
                dAngle = "0.0";

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            DataTable tmpTable;
            DataRow tmpRow;

            tmpTable = base.CoreDs.Tables["textTable"];
            tmpRow = tmpTable.NewRow();
            tmpRow["dLayer"] = dName;
            tmpRow["DifGISCode"] = dSym;
            tmpRow["dHeight"] = dHeight;
            tmpRow["Dirction"] = dAngle;
            tmpRow["X1"] = getValue(entStr, "10");
            tmpRow["Y1"] = getValue(entStr, "20");
            tmpRow["Z1"] = getValue(entStr, "30");
            tmpRow["TEXT"] = getValue(entStr, "1");
            tmpRow["SMSCode"] = sName;
            tmpRow["SMSSymbol"] = cassCodeOrsSym;
            tmpRow["AttriBute"] = AttriBute;
            tmpRow["SHeight"] = dHeight;        //2009.9.15 TianK 增加
            tmpRow["SScale"] = "1";            //2009.9.15 TianK 增加
            tmpRow["SFontName"] = getValue(entStr, "7");        //2009.9.15 TianK 增加

            tmpTable.Rows.Add(tmpRow);
        }
        #endregion

        #region 添加spline
        /// <summary>
        /// 添加SPline
        /// </summary>
        /// <param name="entStr"></param>
        private void addSpline(string entStr)
        {
            IArray ptArr = new ArrayClass();
            IPoint tmpPt = new PointClass();

            Guid uid = new Guid();
            uid = Guid.NewGuid();
            string sName, sSym, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string AttriBute = "";

            sName = getValue(entStr, "8");
            dType = "2";		//线图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            sSym = this.getValue(entStr, "6");

            //			//读出10的点
            int ptCnt = 0;
            ptCnt = Int32.Parse(this.getValue(entStr, "73"));
            string[] ptX10 = new string[ptCnt];
            string[] ptY10 = new string[ptCnt];
            string[] ptZ10 = new string[ptCnt];
            ptX10 = this.processPlStr(entStr, "10", ptCnt);
            ptY10 = this.processPlStr(entStr, "20", ptCnt);
            ptZ10 = this.processPlStr(entStr, "30", ptCnt);
            tmpPt = new PointClass();
            tmpPt.X = double.Parse(ptX10[1]);
            tmpPt.Y = double.Parse(ptY10[1]);
            tmpPt.Z = double.Parse(ptZ10[1]);

            ptArr.Add(tmpPt);	//添加10的第二个点

            //读出11的点
            ptCnt = 0;
            ptCnt = Int32.Parse(this.getValue(entStr, "73"));
            string[] ptX11 = new string[ptCnt];
            string[] ptY11 = new string[ptCnt];
            string[] ptZ11 = new string[ptCnt];
            ptX11 = this.processPlStr(entStr, "10", ptCnt);
            ptY11 = this.processPlStr(entStr, "20", ptCnt);
            ptZ11 = this.processPlStr(entStr, "30", ptCnt);
            for (int i = 0; i < ptCnt; i++)
            {
                tmpPt = new PointClass();
                tmpPt.X = double.Parse(ptX11[i]);
                tmpPt.Y = double.Parse(ptY11[i]);
                tmpPt.Z = double.Parse(ptZ11[i]);
                ptArr.Add(tmpPt);	//添加11的所有点
            }

            int pt10_2 = 0;
            pt10_2 = ptX10.Length - 2;
            tmpPt = new PointClass();
            tmpPt.X = double.Parse(ptX10[pt10_2]);
            tmpPt.Y = double.Parse(ptY10[pt10_2]);
            tmpPt.Z = double.Parse(ptZ10[pt10_2]);
            ptArr.Add(tmpPt);		//添加10的倒数第二点

            //将组合的点添加到DataSet中
            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = base.CoreDs.Tables["splineTable"];
            for (int i = 0; i < ptArr.Count; i++)
            {
                tmpPt = (IPoint)ptArr.get_Element(i);

                tmpRow = tmpTable.NewRow();
                tmpRow["plId"] = uid.ToString();
                tmpRow["dLayer"] = dName;
                tmpRow["DifGISCode"] = dSym;
                tmpRow["SymbolName"] = strSymbolName;
                tmpRow["X"] = tmpPt.X.ToString();
                tmpRow["Y"] = tmpPt.Y.ToString();
                tmpRow["Z"] = tmpPt.Z.ToString();
                tmpRow["SMSCode"] = sName;
                tmpRow["SMSSymbol"] = cassCodeOrsSym;
                tmpRow["AttriBute"] = AttriBute;
                tmpRow["LineType"] = sSym;    //2009.9.16   TianK 添加

                tmpTable.Rows.Add(tmpRow);
            }
        }
        #endregion

        #region 添加polyline
        /// <summary>
        /// 绘制各种类型的polyline
        /// </summary>
        /// <param name="entStr"></param>
        private void addPolyline(string entStr)
        {
            Guid uid = new Guid();
            uid = Guid.NewGuid();
            string sName, sSym, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            double dNhType = 0;        //多段线标志
            string isClose = "";		//0--闭合  1--不闭合
            string AttriBute = "";

            sName = getValue(entStr, "8");

            if (getValue(entStr, "70") != "")
            {
                dNhType = Int32.Parse(getValue(entStr, "70"));
            }
            //是否闭合
            if (dNhType == 9 || dNhType == 1 || dNhType == 129 || dNhType == 131 || dNhType == 133 || dNhType == 13) //2007.6.3 TianK 修改
                isClose = "0";
            else
                isClose = "1";
            dType = "2";		//线图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改

            sSym = this.getValue(entStr, "6");

            if (dNhType == 12 || dNhType == 132 || dNhType == 13 || dNhType == 133 || dNhType == 4)
                add_23_Polyline(entStr, dName, dSym, sName, cassCodeOrsSym, sSym, isClose, AttriBute, strSymbolName);
            else
                add_0_Polyline(entStr, dName, dSym, sName, cassCodeOrsSym, sSym, isClose, AttriBute, strSymbolName);
        }

        /// <summary>
        /// 不拟合的曲线
        /// </summary>
        /// <param name="entStr"></param>
        private void add_0_Polyline(string entStr, string layerName, string symbolName, string sName, string cassCodeOrsSym, string sSym, string isclose, string AttriBute, string strSymbolName)
        {
            Guid uid = new Guid();
            uid = Guid.NewGuid();
            string dName, dSym;
            string isClose = "";		//0--闭合  1--不闭合

            dName = layerName;
            dSym = symbolName;
            isClose = isclose;

            int ptCnt = 0;
            ptCnt = this.getPlCount(entStr, "10") - 1;		//点的个数   20120112  TianKuo  修改
            string[] plArray = new string[ptCnt];
            string[] ptArray = null;
            string[] ptX = new string[ptCnt];
            string[] ptY = new string[ptCnt];
            string[] ptZ = new string[ptCnt];
            string[] ptTd = new string[ptCnt];


            //获取polyline点的数组
            plArray = this.getPArray(entStr, ptCnt);
            for (int i = 0; i < ptCnt; i++)
            {
                //获取每个点的坐标
                ptArray = plArray[i].Split(new char[] { '$' });
                ptX[i] = ptArray[0];
                ptY[i] = ptArray[1];
                ptZ[i] = ptArray[2];//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝
                ptTd[i] = ptArray[3];
            }

            //添加到dataset中
            DataTable tmpTable = base.CoreDs.Tables["polylineTable"];
            DataRow tmpRow;
            for (int j = 0; j < ptCnt - 1; j++)
            {
                //添加到dataset中				
                //if(!(ptX[j]=="0.0"&&ptY[j]=="0.0"))
                //{
                tmpRow = tmpTable.NewRow();
                tmpRow["plId"] = uid.ToString();
                tmpRow["dLayer"] = dName;
                tmpRow["DifGISCode"] = dSym;
                tmpRow["SymbolName"] = strSymbolName;
                tmpRow["beginX"] = ptX[j];
                tmpRow["beginY"] = ptY[j];
                tmpRow["beginZ"] = ptZ[j];//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝
                tmpRow["endX"] = ptX[j + 1];
                tmpRow["endY"] = ptY[j + 1];
                tmpRow["endZ"] = ptZ[j + 1];//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝
                tmpRow["td"] = ptTd[j];
                if (j == ptCnt - 2) //如果为最后一点，记下最后一点的凸度   2009.5.11 TianK修改
                {
                    tmpRow["zdtd"] = ptTd[j + 1];
                }
                tmpRow["plIndex"] = j;
                tmpRow["isClose"] = isClose;
                tmpRow["SMSCode"] = sName;
                tmpRow["SMSSymbol"] = cassCodeOrsSym;
                tmpRow["AttriBute"] = AttriBute;
                tmpRow["LineType"] = sSym;    //2009.9.16   TianK 添加

                tmpTable.Rows.Add(tmpRow);
                //}
            }
        }

        /// <summary>
        /// 拟合曲线		/// 
        /// </summary>
        /// <param name="entStr"></param>
        private void add_23_Polyline(string entStr, string layerName, string symbolName, string sName, string cassCodeOrsSym, string sSym, string isclose, string AttriBute, string strSymbolName)
        {
            Guid uid = new Guid();
            uid = Guid.NewGuid();
            string dName, dSym;
            string isClose = "";		//0--闭合  1--不闭合
            IPoint tmpPt = new PointClass();
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            IArray inArr = new ArrayClass();

            dName = layerName;
            dSym = symbolName;
            isClose = isclose;

            int ptCnt = 0;
            ptCnt = this.getPlCount(entStr, "10") - 1;      //点的个数   20120112  TianKuo  修改
            string[] ptX = new string[ptCnt];
            string[] ptY = new string[ptCnt];
            string[] ptZ = new string[ptCnt];
            string[] ptSign = new string[ptCnt];           //2007.05.31 TianK 添加
            ptX = this.processPlStr(entStr, "10", ptCnt);
            ptY = this.processPlStr(entStr, "20", ptCnt);
            ptZ = this.processPlStr(entStr, "30", ptCnt);
            ptSign = this.processPlStr(entStr, "70", ptCnt);  //2007.05.31 TianK 添加

            inArr.RemoveAll();
            for (int i = 0; i < ptCnt; i++)
            {
                if (ptSign[i] != "16" && ptSign[i] != "48")   //2007.05.31 TianK 修改
                {
                    tmpPt = new PointClass();
                    tmpPt.X = double.Parse(ptX[i]);
                    tmpPt.Y = double.Parse(ptY[i]);
                    tmpPt.Z = double.Parse(ptZ[i]);//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝
                    inArr.Add(tmpPt);
                }
            }

            DataTable tmpTable;
            DataRow tmpRow;
            tmpTable = base.CoreDs.Tables["polylineTable"];
            for (int j = 0; j < inArr.Count - 1; j++)
            {
                fromPt = new PointClass();
                toPt = new PointClass();
                fromPt = (IPoint)inArr.get_Element(j);
                toPt = (IPoint)inArr.get_Element(j + 1);

                tmpRow = tmpTable.NewRow();
                tmpRow["plId"] = uid.ToString();
                tmpRow["plindex"] = j;
                tmpRow["dLayer"] = dName;
                tmpRow["DifGISCode"] = dSym;
                tmpRow["SymbolName"] = strSymbolName;
                tmpRow["beginX"] = fromPt.X.ToString();
                tmpRow["beginY"] = fromPt.Y.ToString();
                tmpRow["beginZ"] = fromPt.Z.ToString();//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝	//"";
                tmpRow["endX"] = toPt.X.ToString();
                tmpRow["endY"] = toPt.Y.ToString();
                tmpRow["endZ"] = toPt.Z.ToString();//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝	"";
                tmpRow["isClose"] = isClose;
                tmpRow["SMSCode"] = sName;
                tmpRow["SMSSymbol"] = cassCodeOrsSym;
                tmpRow["AttriBute"] = AttriBute;
                tmpRow["LineType"] = sSym;    //2009.9.16   TianK 添加

                tmpTable.Rows.Add(tmpRow);
            }
        }

        #region 注释掉
        //		/// <summary>
        //		/// 3次拟合曲线
        //		/// </summary>
        //		/// <param name="entStr"></param>
        //		/// <returns></returns>
        //		private void add_3_Polyline(string entStr,string layerName,string symbolName,string sName,string sSym,string isclose)
        //		{
        //			Guid uid=new Guid();
        //			uid=Guid.NewGuid();
        //			string dName,dSym;	
        //			string isClose="";		//0--闭合  1--不闭合
        //			IPoint tmpPt=new PointClass();
        //			IPoint fromPt=new PointClass();
        //			IPoint toPt=new PointClass();
        //			IArray inArr=new ArrayClass();
        //			//IArray outArr=new ArrayClass();              //2007.05.31 TianK 修改
        //
        //			dName  = layerName;
        //			dSym   = symbolName;
        //			isClose= isclose;
        //
        //			int ptCnt=0;
        //			ptCnt=this.getPlCount(entStr,"10");
        //			string []ptX=new string [ptCnt];
        //			string []ptY=new string [ptCnt];
        //			string []ptZ=new string [ptCnt];
        //			string []ptSign=new string [ptCnt];           //2007.05.31 TianK 添加
        //			ptX=this.processPlStr(entStr,"10",ptCnt);
        //			ptY=this.processPlStr(entStr,"20",ptCnt);
        //			ptZ=this.processPlStr(entStr,"30",ptCnt);
        //			ptSign=this.processPlStr(entStr,"70",ptCnt);  //2007.05.31 TianK 添加
        //
        //			inArr.RemoveAll();
        //			for(int i=0;i<ptCnt;i++)
        //			{				
        //				if(ptX[i]!="0.0"&&ptY[i]!="0.0"&&(ptSign[i]=="8"||ptSign[i]=="32"||ptSign[i]=="40"))   //2007.05.31 TianK 修改
        //				{
        //					tmpPt=new PointClass();
        //					tmpPt.X=double.Parse(ptX[i]);
        //					tmpPt.Y=double.Parse(ptY[i]);
        //					tmpPt.Z=double.Parse(ptZ[i]);//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝
        //					inArr.Add(tmpPt);
        //				}
        //			}
        //
        //			//PublicFun.BMLine3_CADpArray(inArr,10, ref outArr);  //2007.05.31 TianK 修改
        //
        //			DataTable tmpTable;
        //			DataRow   tmpRow ;			
        //			tmpTable=base.CoreDs.Tables["polylineTable"];
        //			for(int j=0;j<inArr.Count -1;j++)
        //			{
        //				fromPt=new PointClass();
        //				toPt=new PointClass();
        //				fromPt=(IPoint)inArr.get_Element(j);
        //				toPt=(IPoint)inArr.get_Element(j+1);
        //
        //				tmpRow = tmpTable.NewRow();
        //				tmpRow["plId"]=uid.ToString();
        //				tmpRow["plindex"]=j;
        //				tmpRow["dLayer"]=dName;
        //				tmpRow["DifGISCode"]=dSym;
        //				tmpRow["beginX"]=fromPt.X.ToString();
        //				tmpRow["beginY"]=fromPt.Y.ToString();
        //				tmpRow["beginZ"]=fromPt.Z.ToString();//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝
        //				tmpRow["endX"]=toPt.X.ToString();
        //				tmpRow["endY"]=toPt.Y.ToString();
        //				tmpRow["endZ"]=toPt.Z.ToString();//＝＝＝＝＝＝＝＝＝＝＝＝＝＝袁怀月修改＝＝＝＝＝＝＝＝＝＝＝＝"";
        //				tmpRow["isClose"]=isClose;
        //				tmpRow["SMSCode"]=sName;
        //				tmpRow["SMSSymbol"]=sSym;
        //		
        //				tmpTable.Rows.Add(tmpRow);
        //			}	
        //		}
        #endregion

        #endregion

        #region 添加lwpolyline
        /// <summary>
        /// 绘制LwPolyline
        /// </summary>
        /// <param name="entStr"></param>
        private void addLwPolyline(string entStr)
        {
            Guid uid = new Guid();
            uid = Guid.NewGuid();
            string sName, sSym, cassCodeOrsSym = "";
            string dName = "", dType, dSym = "", strSymbolName = "";
            string isClose = "";
            string AttriBute = "";

            sName = getValue(entStr, "8");
            dType = "2";		//线图层

            findDifGisLayerAndDifGisCode(entStr, sName, dType, ref AttriBute, ref cassCodeOrsSym, ref dName, ref dSym, ref strSymbolName);  //找到对应的图层和地物编码    2013.4.10 TianK 修改
            sSym = this.getValue(entStr, "6");

            //是否闭合
            isClose = getValue(entStr, "70");
            if (isClose == "")
                isClose = "1";
            else
            {
                if (Int32.Parse(isClose) == 129 || Int32.Parse(isClose) == 1)
                    isClose = "0";
                else
                    isClose = "1";
            }

            int ptCnt = 0;                                             //--------------2007.05.30 TianK 修改-----------------//
            ptCnt = Int32.Parse(this.getValue(entStr, "90"));		//点的个数
            string[] plArray = new string[ptCnt];
            string[] ptArray = null;
            string[] ptX = new string[ptCnt];
            string[] ptY = new string[ptCnt];
            string ptZ = this.getValue(entStr, "38");
            string[] ptTd = new string[ptCnt];

            //获取polyline点的数组
            plArray = this.getPlArray(entStr, ptCnt);
            for (int i = 0; i < ptCnt; i++)
            {
                //获取每个点的坐标
                ptArray = plArray[i].Split(new char[] { '$' });
                ptX[i] = ptArray[0];
                ptY[i] = ptArray[1];
                ptTd[i] = ptArray[3];
            }

            //添加到dataset中
            DataTable tmpTable = base.CoreDs.Tables["polylineTable"];
            DataRow tmpRow;
            for (int j = 0; j < ptCnt - 1; j++)
            {
                //添加到dataset中				
                //if(ptX[j]!="0.0"&&ptY[j]!="0.0")
                //{
                tmpRow = tmpTable.NewRow();
                tmpRow["plId"] = uid.ToString();
                tmpRow["dLayer"] = dName;
                tmpRow["DifGISCode"] = dSym;
                tmpRow["SymbolName"] = strSymbolName;
                tmpRow["beginX"] = ptX[j];
                tmpRow["beginY"] = ptY[j];
                tmpRow["beginZ"] = ptZ;
                tmpRow["endX"] = ptX[j + 1];
                tmpRow["endY"] = ptY[j + 1];
                tmpRow["endZ"] = ptZ;
                tmpRow["td"] = ptTd[j];
                if (j == ptCnt - 2) //如果为最后一点，记下最后一点的凸度   2009.5.11 TianK修改
                {
                    tmpRow["zdtd"] = ptTd[j + 1];
                }
                tmpRow["plIndex"] = j;
                tmpRow["isClose"] = isClose;
                tmpRow["SMSCode"] = sName;
                tmpRow["SMSSymbol"] = cassCodeOrsSym;
                tmpRow["AttriBute"] = AttriBute;
                tmpRow["LineType"] = sSym;    //2009.9.16   TianK 添加

                tmpTable.Rows.Add(tmpRow);
                //}
            }			                                                  //--------------2007.05.30 TianK 修改-----------------//
        }
        #endregion

        #endregion

        #region 内部函数--实体相关处理函数
        /// <summary>
        /// 处理Lwpolyline中的字符串获取每个点的坐标和凸点（如果有的话）
        /// </summary>
        /// <param name="entStr"></param>
        /// <returns></returns>
        private string[] getPlArray(string entStr, int ptCount)      //2012.03.19TianK添加
        {
            string[] plArray = new string[ptCount];
            string tmpStr = "";
            string tmpX, tmpY, tmpZ, tmpTd;
            int startPos, endPos;

            for (int i = 0; i < ptCount; i++)
            {
                startPos = entStr.IndexOf("[10]");
                endPos = entStr.IndexOf("[10]", startPos + 4);
                if (endPos < 0)
                    tmpStr = "$" + entStr.Substring(startPos);
                else
                    tmpStr = "$" + entStr.Substring(startPos, endPos - startPos);
                tmpX = this.getValue(tmpStr, "10");
                tmpY = this.getValue(tmpStr, "20");
                tmpZ = this.getValue(tmpStr, "30");
                tmpTd = this.getValue(tmpStr, "42");
                if (endPos > 0)
                    entStr = entStr.Substring(endPos);
                plArray[i] = tmpX + "$" + tmpY + "$" + tmpZ + "$" + tmpTd;

            }

            return plArray;
        }

        /// <summary>
        /// 处理polyline中的字符串获取每个点的坐标和凸点（如果有的话）
        /// </summary>
        /// <param name="entStr"></param>
        /// <returns></returns>
        private string[] getPArray(string entStr, int ptCount)
        {
            string[] plArray = new string[ptCount];
            string tmpStr = "";
            string tmpX, tmpY, tmpZ, tmpTd;
            int startPos, endPos;
            bool bIsFiret = true;

            for (int i = 0; i < ptCount; i++)
            {
                startPos = entStr.IndexOf("[10]");
                endPos = entStr.IndexOf("[10]", startPos + 4);
                if (endPos < 0)
                    tmpStr = "$" + entStr.Substring(startPos);
                else
                    tmpStr = "$" + entStr.Substring(startPos, endPos - startPos);
                tmpX = this.getValue(tmpStr, "10");
                tmpY = this.getValue(tmpStr, "20");
                tmpZ = this.getValue(tmpStr, "30");//袁怀月修改=============================================
                tmpTd = this.getValue(tmpStr, "42");
                if (endPos > 0)
                    entStr = entStr.Substring(endPos);
                if (i == 0 && double.Parse(tmpX) == 0 && double.Parse(tmpY) == 0 && bIsFiret == true)
                {
                    i--;
                    bIsFiret = false;
                    continue;
                }
                plArray[i] = tmpX + "$" + tmpY + "$" + tmpZ + "$" + tmpTd;

            }

            return plArray;
        }

        /// <summary>
        /// 处理Hatch中lwpline的多点
        /// </summary>
        /// <param name="entStr"></param>
        private string[] processHatchStr(string entStr, string xORy, int ptCount)
        {
            string tmpStr = "";
            string qgStr = "[" + xORy + "]";
            string[] tmpArr = new string[ptCount];

            if (entStr.IndexOf("[93]") >= 0)	//判断字符串中有无93组码,如果有把93前面的10删除
            {
                tmpStr = entStr.Substring(entStr.IndexOf("[93]"));
            }

            for (int i = 0; i < ptCount; i++)
            {
                if (entStr.IndexOf(qgStr) >= 0)
                {
                    tmpArr[i] = this.getValue(tmpStr, xORy);
                    tmpStr = "$" + tmpStr.Substring(tmpStr.IndexOf(qgStr) + 4);
                }
                else
                {
                    tmpArr[i] = "0.0";
                }
            }
            return tmpArr;
        }


        /// <summary>
        /// 处理pl中的多点
        /// </summary>
        /// <param name="entStr"></param>
        private string[] processPlStr(string entStr, string xORy, int ptCount)
        {
            string tmpStr;
            string qgStr = "[" + xORy + "]";
            string[] tmpArr = new string[ptCount];

            if (entStr.IndexOf(qgStr) >= 0)
            {
                tmpStr = "$" + entStr.Substring(entStr.IndexOf(qgStr));

                int i = 0;
                while (tmpStr.IndexOf(qgStr) >= 0)
                {
                    tmpArr[i] = this.getValue(tmpStr, xORy);
                    tmpStr = "$" + tmpStr.Substring(tmpStr.IndexOf(qgStr) + 4);
                    i++;
                }
            }
            else
            {
                for (int i = 0; i < ptCount; i++)
                {
                    tmpArr[i] = "0.0";
                }
            }
            return tmpArr;
        }

        /// <summary>
        /// 获取Polyline中点的个数
        /// </summary>
        /// <param name="entStr"></param>
        /// <returns></returns>
        private int getPlCount(string entStr, string ptType)
        {
            string tmpStr = "";
            string tptType = "[" + ptType + "]";

            int ptCount = 0;
            if (entStr.IndexOf(tptType) >= 0)
            {
                tmpStr = entStr.Substring(entStr.IndexOf(tptType));
            }

            while (tmpStr.IndexOf(tptType) >= 0)
            {
                //				x=this.getValue(tmpStr,"10");
                //				y=this.getValue(tmpStr,"20");
                ptCount++;
                tmpStr = tmpStr.Substring(tmpStr.IndexOf(tptType) + 4);
            }
            return ptCount;
        }

        /// <summary>
        /// 在entStr中根据eleCode取得eleCode后中的具体的值
        /// </summary>
        /// <param name="entStr"></param>
        /// <param name="eleCode"></param>
        private string getValue(string entStr, string eleCode)
        {
            int startPos = 0, endPos = 0;
            string rtnValue = "";
            string tmpStr = "$[" + eleCode + "]$";
            int totLength = tmpStr.Length;

            startPos = entStr.IndexOf(tmpStr);
            if (startPos < 0)		//没找到
                rtnValue = "";
            else
            {
                endPos = entStr.IndexOf("$", startPos + totLength);
                if (endPos < 0)		//在最后一位没有逗号时出现
                    rtnValue = entStr.Substring(startPos + totLength);
                else
                    rtnValue = entStr.Substring(startPos + totLength, endPos - startPos - totLength);
            }
            return rtnValue;
        }
        #endregion

        #region 图层、符号设置处理函数

        #region 找到对应的图层和地物编码    2013.4.10 TianK 添加
        private void findDifGisLayerAndDifGisCode(string entStr, string sName, string dType, ref string AttriBute, ref string cassCode, ref string strDifGISLayer, ref string strDifGISCode, ref string strSymbolName)
        {
            cassCode = getCassCode(entStr, ref AttriBute, ref strDifGISLayer, ref strDifGISCode);
            if (strDifGISCode == "" && cassCode != "")
            {
                strDifGISCode = this.findSymbolCassCode(cassCode, dType, ref strDifGISLayer, ref strSymbolName);
            }
            if (strDifGISLayer == "")
            {
                strDifGISLayer = this.findLayer(sName, dType, "GisLayer", strDifGISCode);
            }

        }
        #endregion

        #region 根据提供的参数找具体的图层对照表字段内容-findLayer
        /// <summary>
        /// 根据提供的参数找具体的图层对照表字段内容
        /// </summary>
        /// <param name="sourceName">源图层的名字</param>
        /// <param name="DestType">目标图层的类型</param>
        /// <param name="findField">要找的目标图层的字段</param>
        /// <returns></returns>
        private string findLayer(string sourceName, string destType, string findField, string dSymbol)
        {
            string pSql = "", rtnValue = "", dsName = "", dfGisLayerCode;
            string log = "";

            DataTable tmpDt = base.CoreDs.Tables["layerTable"];
            DataRow[] secTable = null;

            if (dSymbol != "")     //首先根据DifGis编码解析的DifGis图层码来找对应的图层  TianK 2007.7.10 添加
            {
                if (dSymbol.Substring(dSymbol.Length - 1, 1) == "3")
                {
                    destType = "3";
                }
                dfGisLayerCode = dSymbol.Substring(0, 1);
                //对管线进行特殊处理  YuanSong 2011.5.30 修改
                if (dfGisLayerCode == "5")
                {
                    dfGisLayerCode = dSymbol.Substring(0, 5);
                    pSql = "DfGisLayerCode = '" + dfGisLayerCode + "' and GisLayerType ='" + destType + "'";
                    secTable = tmpDt.Select(pSql);
                    for (int i = 4; i > 1; i--)
                    {
                        if (secTable.Length == 0)
                        {
                            dfGisLayerCode = dSymbol.Substring(0, i);
                            pSql = "DfGisLayerCode = '" + dfGisLayerCode + "' and GisLayerType ='" + destType + "'";
                            secTable = tmpDt.Select(pSql);
                        }
                    }
                }
                else
                {
                    pSql = "DfGisLayerCode = '" + dfGisLayerCode + "' and GisLayerType ='" + destType + "'";
                    secTable = tmpDt.Select(pSql);
                }
            }
            else      //如果DifGis编码为空则根据CAD图层来找对应的图层 TianK 2007.7.10 添加
            {
                pSql = "CadLayer = '" + sourceName + "' and GisLayerType ='" + destType + "'";
                secTable = tmpDt.Select(pSql);
            }

            if (secTable.Length > 0)
            {
                rtnValue = (string)secTable[0][findField];
                dsName = (string)secTable[0]["layerName"];	//数据集的名字
            }
            else
            {
                switch (destType)
                {
                    case "1":
                        rtnValue = "tmp_PT";
                        log = "点图层　：[" + sourceName + "]　没找到对应图层，写到tmp_Point图层<br>";
                        break;
                    case "2":
                        rtnValue = "tmp_ARC";
                        log = "线图层　：[" + sourceName + "]　没找到对应图层，写到tmp_ARC图层<br>";
                        break;
                    case "3":
                        rtnValue = "tmp_PLY";
                        log = "面图层　：[" + sourceName + "]　没找到对应图层，写到tmp_PLY图层<br>";
                        break;
                    case "4":
                        rtnValue = "tmp_ANNO";
                        log = "注记图层：[" + sourceName + "]　没找到对应图层，写到tmp_Anno图层<br>";
                        break;
                }
                dsName = "tmp";
                logWriter.AddErrorLog(destType, log);
            }

            this.addUsedLayer(destType, rtnValue, dsName);
            this.addUsedDataset(dsName);
            return rtnValue;
        }
        #endregion

        #region 根据提供的参数找具体的符号对照表字段内容--findSymbol
        /// <summary>
        /// 根据提供的参数找具体的符号对照表字段内容
        /// </summary>
        /// <param name="sourceName">原块名</param>
        /// <param name="DestType">目标图层的类型</param>
        /// <param name="findField">要找的目标图层的字段</param>
        /// <returns></returns>
        private string findSymbol(string sourceName, string findField, ref string strDifGISLayer, ref string strSymbolName)
        {
            string pSql = "", rtnValue = "";
            string log = "";
            DataRow[] secTable;
            DataTable tmpDt = new DataTable();

            tmpDt = base.CoreDs.Tables["symbolTable"];
            pSql = "SymbolCode = '" + sourceName + "'";
            secTable = tmpDt.Select(pSql);
            if (secTable.Length > 0)
            {
                rtnValue = (string)secTable[0][findField];
                strDifGISLayer = (string)secTable[0]["DifGISLayer"];
                strSymbolName = (string)secTable[0]["SymbolName"];
            }
            else
            {
                rtnValue = "";
                //log = "[" + sourceName + "]没找到对应编码！";
                //logWriter.AddErrorLog("1", log);
            }
            return rtnValue;
        }
        #endregion

        #region 添加转换中用到的图层
        /// <summary>
        /// 添加转换中用到的图层
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="symbolName"></param>
        private void addUsedLayer(string layerType, string layerName, string dsName)
        {
            DataTable tmpTable;
            DataRow tmpRow;
            DataRow[] secTable;

            tmpTable = base.CoreDs.Tables["UsedLayer"];

            secTable = tmpTable.Select("layer='" + layerName + "' and layerType='" + layerType + "'");
            if (secTable.Length == 0)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["layer"] = layerName;
                tmpRow["Dataset"] = dsName;
                tmpRow["layerType"] = layerType;

                tmpTable.Rows.Add(tmpRow);
            }
        }
        #endregion

        #region 添加转换过程中用到的dataset
        /// <summary>
        /// 添加转换过程中用到的dataset
        /// </summary>
        /// <param name="dsName"></param>
        private void addUsedDataset(string dsName)
        {
            DataTable tmpTable;
            DataRow tmpRow;
            DataRow[] secTable;

            tmpTable = base.CoreDs.Tables["UsedDataset"];
            secTable = tmpTable.Select("dataset='" + dsName + "'");
            if (secTable.Length == 0)
            {
                tmpRow = tmpTable.NewRow();
                tmpRow["dataset"] = dsName;

                tmpTable.Rows.Add(tmpRow);
            }
        }
        #endregion

        #region 用于得到南方CASS存在组码1000中的编码、扩展属性、要素类名、地物编码   2013.4.10 TianK 修改
        private string getCassCode(string entStr, ref string attribute, ref string strDifGISLayer, ref string strDifGISCode)
        {
            int index = 0;
            string rtnValue = "";

            index = entStr.IndexOf("$[1001]");   //查找扩展属性
            if (index < 0)
            {
                attribute = "";
            }
            else
            {
                attribute = entStr.Substring(index);
                rtnValue = getXdataAttribute(attribute, "$[1001]$SOUTH$[1000]$");
                if (rtnValue == "")
                {
                    rtnValue = getXdataAttribute(attribute, "$[1001]$south$[1000]$");
                }
                strDifGISLayer = getXdataAttribute(attribute, "$[1001]$GISLAYERNAME$[1000]$");
                strDifGISCode = getXdataAttribute(attribute, "$[1001]$GEOOBJNUM$[1000]$");
            }
            return rtnValue;
        }

        #endregion

        #region 得到某一扩展属性的值    2013.4.10 TianK 添加
        private string getXdataAttribute(string attribute, string attriName)
        {
            int startPos = 0, endPos = 0;
            int totLength = attriName.Length;
            string rtnValue = "";

            startPos = attribute.IndexOf(attriName);
            if (startPos < 0)		//没找到
            {
                rtnValue = "";

            }
            else
            {
                endPos = attribute.IndexOf("$", startPos + totLength);
                if (endPos < 0)
                {
                    rtnValue = attribute.Substring(startPos + totLength);
                }
                else
                {
                    rtnValue = attribute.Substring(startPos + totLength, endPos - startPos - totLength);
                }
            }
            return rtnValue;
        }
        #endregion

        #region 用于按照CASS编码查找对应的DifGIS编码
        private string findSymbolCassCode(string sourceName, string dType, ref string strDifGISLayer, ref string strSymbolName)
        {
            string pSql = "", rtnValue = "";
            DataRow[] secTable;
            DataTable tmpDt = new DataTable();

            tmpDt = base.CoreDs.Tables["symbolTable"];
            pSql = "CassCode = '" + sourceName + "'";
            secTable = tmpDt.Select(pSql);
            if (secTable.Length > 0)
            {
                rtnValue = (string)secTable[0]["DifGISCode"];
                strDifGISLayer = (string)secTable[0]["DifGISLayer"];
                strSymbolName = (string)secTable[0]["SymbolName"];
            }
            else
            {
                rtnValue = "";
                strDifGISLayer = "";
                strSymbolName = "";
            }
            if (dType == "4" && strDifGISLayer.IndexOf("ANNO") <= -1)
            {
                rtnValue = "";
                strDifGISLayer = "";
                strSymbolName = "";
            }
            if (dType == "3" && strDifGISLayer.IndexOf("PLY") <= -1)
            {
                rtnValue = "";
                strDifGISLayer = "";
                strSymbolName = "";
            }
            return rtnValue;
        }
        #endregion

        #endregion

    }
}
