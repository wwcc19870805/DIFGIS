using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using stdole;
using ESRI.ArcGIS.ADF.COMSupport;  

namespace DF2DFileConvert.Class
{
    public class PublicFun
    {
        public static int ReadEntCount = 0;			//读出的实体个数
        public static int WrtieEntCount = 0;			//写入的实体个数
        public static int plCount = 0;				//处理的pl点的个数
        public static bool initOk = true;				//是否初始化成功
        public static bool isWebService = false;

        public PublicFun()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 工作空间及表操作

        /// <summary>
        /// 获取指定的工作空间（Access类型）
        /// </summary>
        /// <param name="accessFile">Access文件名</param>
        /// <returns></returns>
        public static IWorkspace GetAccessWs(string accessFile)
        {
            IWorkspaceFactory pAccessWorkSpaceFactory;
            IWorkspace pAccessWorkSpace;
            pAccessWorkSpaceFactory = new AccessWorkspaceFactoryClass();

            pAccessWorkSpace = pAccessWorkSpaceFactory.OpenFromFile(accessFile, 0);

            return pAccessWorkSpace;
        }

        /// <summary>
        /// 获取指定工作空间内的指定的Table
        /// </summary>
        /// <param name="mdbFile">Access文件名</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static ITable GetAccessTable(string mdbFile, string tableName)
        {
            ITable pTmpTable;
            IWorkspaceFactory pAccessWorkSpaceFactory;
            IFeatureWorkspace pAccessWorkSpace;
            pAccessWorkSpaceFactory = new AccessWorkspaceFactoryClass();

            pAccessWorkSpace = (IFeatureWorkspace)pAccessWorkSpaceFactory.OpenFromFile(mdbFile, 0);

            pTmpTable = pAccessWorkSpace.OpenTable(tableName);

            return pTmpTable;
        }

        #region 建图层对应表
        //public static void CreateLayerTable(IWorkspace pws,string tablename)
        //{
        //    IFeatureWorkspace pFws;
        //    IFields pFields=new FieldsClass();
        //    IFieldsEdit pFieldsEdit;
        //    pFieldsEdit=(IFieldsEdit) pFields;
        //    pFieldsEdit.FieldCount_2=6;

        //    IFieldEdit pFieldEdit;
        //    IField pFieldOID = new FieldClass();
        //    pFieldEdit = (IFieldEdit)pFieldOID;

        //    pFieldEdit.Editable_2  = true;
        //    pFieldEdit.Name_2 ="OBJECTID";
        //    pFieldEdit.AliasName_2="OBJECTID";
        //    pFieldEdit.Type_2 =esriFieldType.esriFieldTypeOID;
        //    pFieldEdit.IsNullable_2 =false;
        //    pFieldsEdit.set_Field(0,pFieldEdit);

        //    //存放CadLayer编码的字段
        //    IField pFieldExtra;
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="CadLayer";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(1,pFieldExtra);

        //    //存放GisLayer编码的字段			
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="GisLayer";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(2,pFieldExtra);

        //    //存放GisLayerType编码的字段			
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="GisLayerType";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeInteger;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(3,pFieldExtra);

        //    ////存放Dept编码的字段			
        //    //pFieldExtra=new FieldClass();
        //    //pFieldEdit=(IFieldEdit)pFieldExtra;
        //    //pFieldEdit.Name_2="Dept";
        //    //pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    //pFieldEdit.IsNullable_2=true;
        //    //pFieldsEdit.set_Field(4,pFieldExtra);

        //    //存放LayerName编码的字段
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="LayerName";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(5,pFieldExtra);

        //    pFws=(IFeatureWorkspace)pws;
        //    pFws.CreateTable(tablename,pFields,null,null,null);
        //}
        //#endregion

        //#region 建符号对应表
        //public static void CreateSymbolTable(IWorkspace pws,string tablename)
        //{
        //    IFeatureWorkspace pFws;
        //    IFields pFields=new FieldsClass();
        //    IFieldsEdit pFieldsEdit;
        //    pFieldsEdit=(IFieldsEdit) pFields;
        //    pFieldsEdit.FieldCount_2=8;

        //    IFieldEdit pFieldEdit;
        //    IField pFieldOID = new FieldClass();
        //    pFieldEdit = (IFieldEdit)pFieldOID;

        //    pFieldEdit.Editable_2  = true;
        //    pFieldEdit.Name_2 ="OBJECTID";
        //    pFieldEdit.AliasName_2="OBJECTID";
        //    pFieldEdit.Type_2 =esriFieldType.esriFieldTypeOID;
        //    pFieldEdit.IsNullable_2 =false;
        //    pFieldsEdit.set_Field(0,pFieldEdit);			
        //    //存放SymbolType编码的字段
        //    IField pFieldExtra;
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="SymbolType";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeInteger;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(1,pFieldExtra);
        //    //存放SymbolCode编码的字段			
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="SymbolCode";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(2,pFieldExtra);
        //    //存放SymbolName编码的字段			
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="SymbolName";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(3,pFieldExtra);
        //    //存放FontName编码的字段			
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="FontName";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(4,pFieldExtra);
        //    //存放DifGISCode编码的字段
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="DifGISCode";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(5,pFieldExtra);
        //    //存放DFontName编码的字段
        //    pFieldExtra=new FieldClass();
        //    pFieldEdit=(IFieldEdit)pFieldExtra;
        //    pFieldEdit.Name_2="DFontName";
        //    pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    pFieldEdit.IsNullable_2=true;
        //    pFieldsEdit.set_Field(6,pFieldExtra);
        //    ////存放Dept编码的字段
        //    //pFieldExtra=new FieldClass();
        //    //pFieldEdit=(IFieldEdit)pFieldExtra;
        //    //pFieldEdit.Name_2="Dept";
        //    //pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
        //    //pFieldEdit.IsNullable_2=true;
        //    //pFieldsEdit.set_Field(7,pFieldExtra);

        //    pFws=(IFeatureWorkspace)pws;
        //    pFws.CreateTable(tablename,pFields,null,null,null);
        //}
        #endregion

        #endregion

        #region  数学方法
        #region		//计算P1到P2的距离函数Point1,Point2
        //
        public static double GetDistance_P12(IPoint p1, IPoint p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double s = dx * dx + dy * dy;
            return Math.Sqrt(s);
        }
        #endregion

        #region		//计算P1到P2的方位角函数Point1,Point2
        //
        public static double GetAzimuth_P12(IPoint p1, IPoint p2)
        {
            double Ap12 = 0;
            if (p2.X - p1.X != 0)
            {
                Ap12 = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X));

                //第一象限
                if (p2.Y - p1.Y > 0 && p2.X - p1.X > 0)
                {
                    Ap12 = Ap12;
                }

                //第二象限
                if (p2.Y - p1.Y > 0 && p2.X - p1.X < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第三象限
                if (p2.Y - p1.Y < 0 && p2.X - p1.X < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第四象限
                if (p2.Y - p1.Y < 0 && p2.X - p1.X > 0)
                {
                    Ap12 = Ap12 + 2 * Math.PI;
                }

                //与X轴平行
                if (p2.Y - p1.Y == 0)
                {
                    if (p2.X - p1.X > 0)
                    {
                        Ap12 = 0;
                    }
                    else
                    {
                        Ap12 = Math.PI;
                    }
                }
            }

            else //与Y轴平行
            {
                if (p2.Y - p1.Y > 0)
                {
                    Ap12 = Math.PI / 2;
                }
                else
                {
                    Ap12 = 3 * Math.PI / 2;
                }
            }
            return Ap12;
        }
        #endregion

        #region		//由p1,p2,p3,p4生成p2到p3三次样条曲线点数组
        //
        public static void Cardinal_P1234(IPoint pk_1, IPoint pk, IPoint pk1, IPoint pk2, int Sum, ref IArray pArray)
        {
            //pArray.RemoveAll();

            double xk_1 = pk_1.X;
            double yk_1 = pk_1.Y;
            double zk_1 = pk_1.Z;

            double xk = pk.X;
            double yk = pk.Y;
            double zk = pk.Z;

            double xk1 = pk1.X;
            double yk1 = pk1.Y;
            double zk1 = pk1.Z;

            double xk2 = pk2.X;
            double yk2 = pk2.Y;
            double zk2 = pk2.Z;

            //			double u = 0;
            double t = 0;
            double CAR0 = 0;
            double CAR1 = 0;
            double CAR2 = 0;
            double CAR3 = 0;

            //			double s = (1 - t)/2;

            for (t = 0; t <= 1; t = t + 1.0 / Sum)
            {
                //				double CAR0 = 2*s * Math.Pow(u,2) - s * Math.Pow(u,3) - s*u;
                //				double CAR1 = (2 - s) * Math.Pow(u,3) + (s - 3) * Math.Pow(u,2) + 1;
                //				double CAR2 = (s - 2) * Math.Pow(u,3) + (3 - 2*s) * Math.Pow(u,2) + s*u;
                //				double CAR3 = s * Math.Pow(u,3) - s * Math.Pow(u,2);

                CAR0 = (3 * Math.Pow(t, 2) - Math.Pow(t, 3) - 3 * t + 1) / 6;
                CAR1 = (3 * Math.Pow(t, 3) - 6 * Math.Pow(t, 2) + 4) / 6;
                CAR2 = (3 * Math.Pow(t, 2) - 3 * Math.Pow(t, 3) + 3 * t + 1) / 6;
                CAR0 = Math.Pow(t, 3) / 6;

                double x = xk_1 * CAR0 + xk * CAR1 + xk1 * CAR2 + xk2 * CAR3;
                double y = yk_1 * CAR0 + yk * CAR1 + yk1 * CAR2 + yk2 * CAR3;
                double z = zk_1 * CAR0 + zk * CAR1 + zk1 * CAR2 + zk2 * CAR3;

                IPoint p = new PointClass();

                IZAware pZA = (IZAware)p;
                pZA.ZAware = true;

                p.X = x;
                p.Y = y;
                p.Z = z;

                pArray.Add(p);
            }
        }
        #endregion

        #region		//由CAD样条曲线特征点数组生成三次样条曲线点数组 error

        //
        public static void Cardinal_CADpArray(IArray CADpArray, int Sum, ref IArray mpArray)
        {
            IPoint pk_1 = new PointClass();
            IPoint pk = new PointClass();
            IPoint pk1 = new PointClass();
            IPoint pkn_1 = new PointClass();
            IPoint pkn = new PointClass();
            IPoint pkn1 = new PointClass();

            IZAware pZA = (IZAware)pk_1;
            pZA.ZAware = true;
            IZAware pZA1 = (IZAware)pkn1;
            pZA1.ZAware = true;

            pk = (IPoint)CADpArray.get_Element(0);
            pk1 = (IPoint)CADpArray.get_Element(1);

            pkn_1 = (IPoint)CADpArray.get_Element(CADpArray.Count - 2);
            pkn = (IPoint)CADpArray.get_Element(CADpArray.Count - 1);

            //计算点pk_1和点pkn1
            pk_1.X = pk1.X + GetDistance_P12(pk, pk1) * Math.Cos(GetAzimuth_P12(pk, pk1));
            pk_1.Y = pk1.Y + GetDistance_P12(pk, pk1) * Math.Sin(GetAzimuth_P12(pk, pk1));
            pk_1.Z = pk.Z;

            pkn1.X = pkn_1.X + GetDistance_P12(pkn, pkn_1) * Math.Cos(GetAzimuth_P12(pkn, pkn_1));
            pkn1.Y = pkn_1.Y + GetDistance_P12(pkn, pkn_1) * Math.Sin(GetAzimuth_P12(pkn, pkn_1));
            pkn1.Z = pkn.Z;

            CADpArray.Remove(0);
            CADpArray.Insert(0, pk_1);
            CADpArray.Remove(CADpArray.Count - 1);
            CADpArray.Add(pkn1);

            //循环求曲线数组
            for (int i = 0; i < CADpArray.Count - 2; i++)
            {
                Cardinal_P123((IPoint)CADpArray.get_Element(i), (IPoint)CADpArray.get_Element(i + 1), (IPoint)CADpArray.get_Element(i + 2), Sum, ref mpArray);
            }
        }
        #endregion

        /*
		#region		//由CAD二次曲线特征点数组生成二次B样条曲线点数组 Cad->Arcgis
		//
		
		 public static void BMLine2_CADpArray(IArray CADpArray, int Sum, ref IArray BmpArray)
		{
			//循环求曲线数组
			for (int i = 0; i < CADpArray.Count - 2; i++)
			{
				BMLine2_P123((IPoint)CADpArray.get_Element(i), (IPoint)CADpArray.get_Element(i+1), (IPoint)CADpArray.get_Element(i+2), Sum, ref BmpArray);
			}
		}
		#endregion
        
		#region		//由CAD三次曲线特征点数组生成三次B样条曲线点数组 Cad->Arcgis
		//
		public static void BMLine3_CADpArray(IArray CADpArray, int Sum, ref IArray BmpArray)
		{
			//循环求曲线数组
			for (int i = 0; i < CADpArray.Count - 3; i++)
			{
				BMLine3_P123((IPoint)CADpArray.get_Element(i), (IPoint)CADpArray.get_Element(i+1), (IPoint)CADpArray.get_Element(i+2),(IPoint)CADpArray.get_Element(i+3), Sum, ref BmpArray);
			}
		}
		#endregion
		*/

        #region		//由p1,p2,p3生成p1到p3二次B样条曲线点数组
        //
        public static void Cardinal_P123(IPoint pk_1, IPoint pk, IPoint pk1, int Sum, ref IArray pArray)
        {
            //pArray.RemoveAll();

            double xk_1 = pk_1.X;
            double yk_1 = pk_1.Y;
            double zk_1 = pk_1.Z;

            double xk = pk.X;
            double yk = pk.Y;
            double zk = pk.Z;

            double xk1 = pk1.X;
            double yk1 = pk1.Y;
            double zk1 = pk1.Z;

            for (double u = 0; u <= 1; u = u + 1.0 / Sum)
            {
                double B0 = u * u - 2 * u + 1;
                double B1 = 2 * u * u - 2 * u - 1;
                double B2 = u * u;

                double x = xk_1 * B0 - xk * B1 + xk1 * B2;
                double y = yk_1 * B0 - yk * B1 + yk1 * B2;
                double z = zk_1 * B0 - zk * B1 + zk1 * B2;

                IPoint p = new PointClass();
                p.X = x / 2;
                p.Y = y / 2;
                p.Z = z / 2;

                pArray.Add(p);
            }
        }
        #endregion

        /*
		#region		//由p1,p2,p3,p4生成三次B样条曲线点数组
		//
		public static void BMLine3_P123(IPoint pk_1, IPoint pk, IPoint pk1, IPoint pk2, int Sum, ref IArray pArray)
		{
			//pArray.RemoveAll();

			double xk_1 = pk_1.X ;
			double yk_1 = pk_1.Y ;
			double zk_1 = pk_1.Z ;
				
			double xk = pk.X ;
			double yk = pk.Y ;
			double zk = pk.Z ;
				
			double xk1 = pk1.X ;
			double yk1 = pk1.Y ;
			double zk1 = pk1.Z ;

			double xk2 = pk2.X ;
			double yk2 = pk2.Y ;
			double zk2 = pk2.Z ;

			for (double u = 0; u <= 1; u = u + 1.0/Sum)
			{	
				double B0 = -(u * u * u) + 3 * u * u - 3 * u + 1;
				double B1 = 3 * u * u * u - 6 * u * u + 4;
				double B2 = -(3 * u * u * u) + 3 * u * u + 3 * u + 1;
				double B3 = u * u * u;

				double x = xk_1 * B0 + xk * B1 + xk1 * B2 + xk2 * B3;
				double y = yk_1 * B0 + yk * B1 + yk1 * B2 + yk2 * B3;
				double z = zk_1 * B0 + zk * B1 + zk1 * B2 + zk2 * B3;

				IPoint p = new PointClass() ;
				p.X = x/6;
				p.Y = y/6;
				p.Z = z/6;
 
				pArray.Add(p);
			}	
		}
		#endregion

		*/

        #region 由(圆弧起点，凸度，终点)计算该圆弧的（曲中点）
        public static void GetARCMidPoint(IPoint FormPt, double u, IPoint ToPt, ref IPoint MidPt)
        {
            //计算弦长的一半
            double L = GetDistance_P12(FormPt, ToPt) / 2;
            //计算半径
            double Radius = Math.Abs((L + u * u * L) / (2 * u));
            //计算圆心角的四分之一
            double a = Math.Abs(Math.Atan(u));
            //计算起点到曲中点方位角
            double AFtoMid = 0;
            double AFtoT = GetAzimuth_P12(FormPt, ToPt);
            if (u < 0)
            {
                AFtoMid = AFtoT + a;
            }
            else
            {
                AFtoMid = AFtoT - a;
            }

            if (AFtoMid >= Math.PI * 2)
            {
                AFtoMid = AFtoMid - Math.PI * 2;
            }
            if (AFtoMid < 0)
            {
                AFtoMid = AFtoMid + Math.PI * 2;
            }
            //计算起点到曲中点距离
            double DFtoCen = 2 * Radius * Math.Sin(a);
            MidPt.X = FormPt.X + DFtoCen * Math.Cos(AFtoMid);
            MidPt.Y = FormPt.Y + DFtoCen * Math.Sin(AFtoMid);

        }
        #endregion

        #region		//由(圆弧起点，凸度，终点)计算该圆弧的（半径，圆心坐标，起角度，终角度）
        //
        public static void GetARCFeature(IPoint FormPt, double u, IPoint ToPt, ref double Radius, ref IPoint CenterPt, ref double FromAng, ref double ToAng)
        {
            //计算弦长的一半
            double L = GetDistance_P12(FormPt, ToPt) / 2;
            //计算半径
            Radius = Math.Abs((L + u * u * L) / (2 * u));
            //计算圆心角的四分之一
            double a = Math.Abs(Math.Atan(u));
            //计算起点到圆心方位角
            double b = Math.PI / 2 - 2 * a;
            double AFtoCen = 0;
            double AFtoT = GetAzimuth_P12(FormPt, ToPt);
            if (u < 0)
            {
                AFtoCen = AFtoT - b;
            }
            else
            {
                AFtoCen = AFtoT + b;
            }

            if (AFtoCen >= Math.PI * 2)
            {
                AFtoCen = AFtoCen - Math.PI * 2;
            }
            if (AFtoCen < 0)
            {
                AFtoCen = AFtoCen + Math.PI * 2;
            }

            CenterPt.X = FormPt.X + Radius * Math.Cos(AFtoCen);
            CenterPt.Y = FormPt.Y + Radius * Math.Sin(AFtoCen);
            //计算起角度和终角度
            FromAng = GetAzimuth_P12(CenterPt, FormPt);
            ToAng = GetAzimuth_P12(CenterPt, ToPt);
        }
        #endregion

        #region		//由(圆心坐标，起点，终点，起角度，终角度)计算该圆弧的（凸度）
        //
        public static void GetARC_u(IPoint CenterPt, IPoint FormPt, IPoint ToPt, double FromAng, double ToAng, double dirction, ref double u)
        {
            //计算弦长的一半
            double L = GetDistance_P12(FormPt, ToPt) / 2;
            //计算半径
            double R = GetDistance_P12(FormPt, CenterPt);
            //计算凸度
            double h = R * R - L * L;
            if (h < 0) h = 0;

            double u1 = (R + Math.Pow(h, 0.5)) / L;
            double u2 = (R - Math.Pow(h, 0.5)) / L;
            double a = ToAng - FromAng;
            if (a < 0) a = a + Math.PI * 2;
            //判断是否小于半圆
            if (a < Math.PI)
            {
                u = u2;
            }
            else
            {
                u = u1;
            }
            u = u * dirction;
            //			//判断u的正负
            //			double Aft = GetAzimuth_P12(FormPt,ToPt); 
            //			double Afc = GetAzimuth_P12(FormPt,CenterPt); 
            //			double dA = Aft - Afc;
            //			if ((dA > 0 && dA < Math.PI) || (dA < 0 && Math.Abs(dA) > Math.PI))
            //			{
            //				//c在ft的右边
            //				//MessageBox.Show("右");
            //				if (u <= 1)
            //				{
            //					u = -u;
            //				}
            //				else
            //				{
            //					u = u;
            //				}
            //				
            //			}
            //			else
            //			{
            //				//c在ft的左边
            //				//MessageBox.Show("左");
            //				if (u < 1)
            //				{
            //					u = u;
            //				}
            //				else
            //				{
            //					u = -u;
            //				}
            //			}

        }
        #endregion

        #endregion

        #region 创建TextSymbol
        public static ITextSymbol makeTextSymbol(string fontName, int fontSize)
        {
            ITextSymbol textSymbol = new TextSymbolClass();
            System.Drawing.Font drawFont = new System.Drawing.Font(fontName, fontSize, FontStyle.Bold);
            textSymbol.Font = (stdole.IFontDisp)OLE.GetIFontDispFromFont(drawFont);
            textSymbol.Color = GetRGBColor(0, 0, 0);
            ITextPath textPath = new BezierTextPathClass();  //to spline the text
            ISimpleTextSymbol simpleTextSymbol = (ISimpleTextSymbol)textSymbol;
            simpleTextSymbol.TextPath = textPath;

            return textSymbol;
        }
        #endregion

        #region 创建RgbColor
        public static IRgbColor GetRGBColor(int red, int green, int blue)
        {
            //Create rgb color and grab hold of the IRGBColor interface
            IRgbColor rGB = new RgbColorClass();
            //Set rgb color properties
            rGB.Red = red;
            rGB.Green = green;
            rGB.Blue = blue;
            rGB.UseWindowsDithering = true;
            return rGB;
        }
        #endregion

        #region 创建Text用的text
        /// <summary>
        /// 创建字体 angType=0 弧度 angType=1 度
        /// </summary>
        /// <param name="sText"></param>
        /// <param name="Dirction"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="angType"></param>
        /// <returns></returns>
        public static ITextElement make_Qs_Text(string sText, double dHeight, double dAngle, double dX, double dY, double deltaX, double deltaY)
        {
            string newText;
            newText = sText.Replace(@"\P", "\n");
            ITextElement pTextElement = new TextElementClass();
            pTextElement.ScaleText = true;
            pTextElement.Text = newText;

            IFormattedTextSymbol myTextSym = new TextSymbolClass();
            stdole.IFontDisp myFont = (stdole.IFontDisp)new stdole.StdFontClass();
            myFont.Name = "黑体";
            myFont.Size = decimal.Parse(dHeight.ToString());
            myTextSym.Font = myFont;
            myTextSym.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

            myTextSym.Angle = dAngle;

            System.Text.RegularExpressions.Regex objNumberPattern = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9.-]");
            if (!(newText == "") && !objNumberPattern.IsMatch(newText[0].ToString()) && !objNumberPattern.IsMatch(newText[newText.Length - 1].ToString()))
            {
                myTextSym.CharacterWidth = 75;
            }

            //			double frameL;	//边框的长度			
            //			if(newText.Length>1)
            //			{			
            //				frameL=Math.Sqrt(deltaX*deltaX+deltaY*deltaY);
            //				//myTextSym.CharacterSpacing  =(frameL-double.Parse(myTextSym.CharacterWidth.ToString())*newText.Length)/(newText.Length-1);
            //				myTextSym.CharacterSpacing=0;
            //			}

            pTextElement.Symbol = myTextSym;

            IElement pElement;
            pElement = (IElement)pTextElement;

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dX, dY);
            pElement.Geometry = pPoint;

            return pTextElement;
            //选用设置好的字体style							
            //			IGroupSymbolElement pGroupSymbolElement;
            //			pGroupSymbolElement=(IGroupSymbolElement)pTextElement;
            //			pGroupSymbolElement.SymbolID=0;
        }

        #region 创建Text用的text
        public static ITextElement make_Dxf_Text(string fName, string sText, double dAngle, double dH, double dX, double dY)
        {
            string newText;
            newText = sText.Replace(@"\P", "\n");
            ITextElement pTextElement = new TextElementClass();
            pTextElement.ScaleText = true;
            pTextElement.Text = newText;

            IFormattedTextSymbol myTextSym = new TextSymbolClass();
            stdole.IFontDisp myFont = (stdole.IFontDisp)new stdole.StdFontClass();
            myFont.Name = fName;
            myFont.Size = decimal.Parse(dH.ToString());
            myTextSym.Font = myFont;
            myTextSym.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

            myTextSym.Angle = dAngle;

            System.Text.RegularExpressions.Regex objNumberPattern = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9.-]");   //2008.2.2 TianK 添加  使数字和字母的宽度为75
            if (!(newText == "") && !objNumberPattern.IsMatch(newText[0].ToString()) && !objNumberPattern.IsMatch(newText[newText.Length - 1].ToString()))
            {
                myTextSym.CharacterWidth = 75;
            }

            pTextElement.Symbol = myTextSym;

            IElement pElement;
            pElement = (IElement)pTextElement;

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dX, dY);
            pElement.Geometry = pPoint;

            return pTextElement;

            //			ts.Text =newText;
            //			ts.Font=myfont;
            ////			if(dAngle!=0)
            ////				ts.Angle=dAngle*(Math.PI/180);
            ////			ts.Size=dH;
            //			pTextElement.Symbol=ts;														
            //			pTextElement.ScaleText=false;
            //		
            //			IElement pElement;
            //			pElement=(IElement)pTextElement;	
            //			
            //			IPoint pPoint=new PointClass();
            //			pPoint.PutCoords(dX,dY);
            //			pElement.Geometry=pPoint;
            //					
            //			IGroupSymbolElement pGroupSymbolElement;
            //			pGroupSymbolElement=(IGroupSymbolElement)pTextElement;
            //			pGroupSymbolElement.SymbolID=0;	
            //			if(dAngle!=0)							
            //			{
            //				ITransform2D pTransform2D;
            //				pTransform2D =(ITransform2D) pTextElement;
            //				pTransform2D.Rotate (pPoint,dAngle*(Math.PI/180));
            //			}
            //							
            //			return pTextElement;
        }
        #endregion
        #endregion

        #region 获得Domain的描述  2009.3.28  TianK   添加
        /// <summary>
        /// 获得Domain的描述
        /// </summary>
        /// <param name="pDomain"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static string GetCodedDescriptionDomainValue(ICodedValueDomain pDomain, string domainValue)
        {
            string result = "<Null>";
            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                if (pDomain.get_Value(i).ToString() == domainValue)
                {
                    result = pDomain.get_Name(i).ToString();
                    break;
                }
            }
            return result;

        }
        #endregion

        #region 获得Domain的值   2009.3.28  TianK   添加
        /// <summary>
        /// 获得Domain的值
        /// </summary>
        /// <param name="pDomain"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static string GetCodedValueDomainValue(ICodedValueDomain pDomain, string domainDescription)
        {
            string result = "<Null>";
            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                if (pDomain.get_Name(i) == domainDescription)
                {
                    result = pDomain.get_Value(i).ToString();
                    break;
                }
            }
            return result;

        }
        #endregion
    }
}
