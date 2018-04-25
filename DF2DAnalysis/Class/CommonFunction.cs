using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using System.Data;
using ESRI.ArcGIS.Display;
using System.Collections;
using ESRI.ArcGIS.DataSourcesGDB;

namespace DF2DPipe.Class
{
    class CommonFunction
    {

        public static IArray m_SelectArray = new ArrayClass();
        public static IArray m_OriginArray = new ArrayClass();
        public static IArray m_pFeatureLayerArray = new ArrayClass();//要素图层

        #region//构造函数
        //
        public CommonFunction()
        {

        }
        #endregion

        #region	//计算坐标方位角(以度为单位)
        //
        public static double azimuth(IPoint p1, IPoint p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double a = Math.Atan2(dy, dx) * (180 / Math.PI);

            if (a < 0)
            {
                a = 360 + a;
            }
            return a;
        }
        #endregion

        #region		//计算P1到P2的方位角函数Point1,Point2(以弧度为单位)
        /// <summary>
        /// 计算P1到P2的方位角(以弧度为单位)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
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

        #region		//计算P1到P2的方位角函数(x1,yi && x2,y2)(以弧度为单位)
        /// <summary>
        /// 计算P1到P2的方位角(以弧度为单位)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetAzimuth_P12(double x1, double y1, double x2, double y2)
        {
            double Ap12 = 0;
            if (x2 - x1 != 0)
            {
                Ap12 = Math.Atan((y2 - y1) / (x2 - x1));

                //第一象限
                if (y2 - y1 > 0 && x2 - x1 > 0)
                {
                    Ap12 = Ap12;
                }

                //第二象限
                if (y2 - y1 > 0 && x2 - x1 < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第三象限
                if (y2 - y1 < 0 && x2 - x1 < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第四象限
                if (y2 - y1 < 0 && x2 - x1 > 0)
                {
                    Ap12 = Ap12 + 2 * Math.PI;
                }

                //与X轴平行
                if (y2 - y1 == 0)
                {
                    if (x2 - x1 > 0)
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
                if (y2 - y1 > 0)
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

        #region		//计算P1到P2的方位角函数(x1,yi && x2,y2)(以弧度为单位)
        //
        public static double GetAzimuth_dxdy(double dx, double dy)
        {
            double Ap12 = 0;
            if (dx != 0)
            {
                Ap12 = Math.Atan((dy) / (dx));

                //第一象限
                if (dy > 0 && dx > 0)
                {
                    Ap12 = Ap12;
                }

                //第二象限
                if (dy > 0 && dx < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第三象限
                if (dy < 0 && dx < 0)
                {
                    Ap12 = Ap12 + Math.PI;
                }

                //第四象限
                if (dy < 0 && dx > 0)
                {
                    Ap12 = Ap12 + 2 * Math.PI;
                }

                //与X轴平行
                if (dy == 0)
                {
                    if (dx > 0)
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
                if (dy > 0)
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

        #region		//计算P1到P2的距离函数Point1,Point2
        /// <summary>
        /// 计算P1到P2的距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetDistance_P12(IPoint p1, IPoint p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double s = dx * dx + dy * dy;
            return Math.Sqrt(s);
        }
        #endregion

        #region		//计算P1到P2的距离函数(x1,yi && x2,y2)
        /// <summary>
        /// 计算P1到P2的距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetDistance_P12(double x1, double y1, double x2, double y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            double s = dx * dx + dy * dy;
            return Math.Sqrt(s);
        }
        #endregion

        #region //计算p1,p2,p3组成的小于180度的角a123
        /// <summary>
        /// 计算p1,p2,p3组成的小于180度的角a123
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static double GetAngle_P123(IPoint p1, IPoint p2, IPoint p3)
        {
            double a = GetAzimuth_P12(p2, p1) - GetAzimuth_P12(p2, p3);
            if (a < 0)
            {
                a = a + Math.PI * 2;
            }
            if (a > Math.PI)
            {
                a = 2 * Math.PI - a;
            }
            return a;
        }
        #endregion

        #region     //角度转换成弧度
        /// <summary>
        /// 角度转换成弧度
        /// </summary>
        /// <param name="Deg"></param>
        /// <returns></returns>
        public static double DegToRad1(double Deg)
        {

            return (Deg / 180) * Math.PI;
        }

        #endregion

        #region     //角度转换成弧度
        /// <summary>
        /// 角度转换成弧度
        /// </summary>
        /// <param name="Deg"></param>
        /// <returns></returns>
        public static double DegToRad(double Deg)
        {
            double b = 0;
            double c = 0;
            double d = 0;
            double g;
            double f1;
            double d1;
            double Rad;

            if (Deg < 0)
            {
                Deg = -Deg;
                b = (int)Deg;
                c = (int)((Deg - b) * 100);
                d = Deg * 10000 - b * 10000 - c * 100;
                d1 = c / 60;
                f1 = d / 3600;
                g = b + d1 + f1;
                Rad = -(g * Math.PI / 180);
            }
            else
            {
                b = (int)Deg;
                c = (int)((Deg - b) * 100);
                d = Deg * 10000 - b * 10000 - c * 100;
                d1 = c / 60;
                f1 = d / 3600;
                g = b + d1 + f1;
                Rad = g * Math.PI / 180;
            }
            //MessageBox.Show("b=" + b + ",c=" + c + ",d=" + d + ",d1=" + d1 + ",f1=" + f1);
            return Rad;
        }

        #endregion

        #region     //弧度转换成角度
        /// <summary>
        /// 弧度转换成角度
        /// </summary>
        /// <param name="Rad"></param>
        /// <returns></returns>
        public static double RadToDeg(double Rad)
        {
            double c;
            double d;
            double e;
            double g;
            double Deg;

            if (Rad < 0)
            {
                Rad = -Rad;
                c = 180 * Rad / Math.PI;
                if (c * 10e11 - (System.Int64)(c * 10e10) * 10 >= 5)
                {
                    c = ((System.Int64)(c * 10e10) + 1) / (10e10);
                }
                else
                {
                    c = ((System.Int64)(c * 10e10)) / (10e10);
                }
                d = (int)c;
                e = (int)((c - d) * 60);
                g = ((c - d) * 60 - e) * 600000000;
                if (g * 10 - (int)g >= 5)
                {
                    g = (int)g + 1;
                }
                else
                {
                    g = (int)g;
                }
                Deg = -(d + e / 100 + g / 100000000000);
            }
            else
            {
                c = 180 * Rad / Math.PI;
                if (c * 10e11 - (System.Int64)(c * 10e10) * 10 >= 5)
                {
                    c = ((System.Int64)(c * 10e10) + 1) / (10e10);
                }
                else
                {
                    c = ((System.Int64)(c * 10e10)) / (10e10);
                }
                d = (int)c;
                e = (int)((c - d) * 60);
                g = ((c - d) * 60 - e) * 600000000;
                if (g * 10 - (int)g >= 5)
                {
                    g = (int)g + 1;
                }
                else
                {
                    g = (int)g;
                }
                Deg = d + e / 100 + g / 100000000000;
            }
            //MessageBox.Show("c=" + c + ",d=" + d + ",e=" + e + ",g=" + g );
            return Deg;
        }
        #endregion

        #region     //角度转换成万分之一度
        /// <summary>
        /// 角度转换成弧度
        /// </summary>
        /// <param name="Deg"></param>
        /// <returns></returns>
        public static double DegToDu(double Deg)
        {
            double b = 0;
            double c = 0;
            double d = 0;
            double g;
            double f1;
            double d1;
            double Du;

            if (Deg < 0)
            {
                Deg = -Deg;
                b = (int)Deg;
                c = (int)((Deg - b) * 100);
                d = Deg * 10000 - b * 10000 - c * 100;
                d1 = c / 60;
                f1 = d / 3600;
                g = b + d1 + f1;
                Du = -g;
            }
            else
            {
                b = (int)Deg;
                c = (int)((Deg - b) * 100);
                d = Deg * 10000 - b * 10000 - c * 100;
                d1 = c / 60;
                f1 = d / 3600;
                g = b + d1 + f1;
                Du = g;
            }
            //MessageBox.Show("b=" + b + ",c=" + c + ",d=" + d + ",d1=" + d1 + ",f1=" + f1);
            return Du;
        }
        #endregion

        #region     //万分之一度转换成角度
        /// <summary>
        /// 角度转换成弧度
        /// </summary>
        /// <param name="Deg"></param>
        /// <returns></returns>
        public static double DuToDeg(double Du)
        {
            double Rad = Du * Math.PI / 180;

            double Deg = RadToDeg(Rad);

            return Deg;
        }

        #endregion

        #region		//由三点计算圆心Point1,Point2，Point3
        /// <summary>
        /// 由三点计算圆心
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static IPoint GetCenter_P123(IPoint p1, IPoint p2, IPoint p3)
        {
            double dx21;
            double x21;
            double dy21;
            double y21;
            double dx31;
            double x31;
            double dy31;
            double y31;
            IPoint p0 = new PointClass();

            dx21 = p2.X - p1.X;
            x21 = p2.X + p1.X;
            dy21 = p2.Y - p1.Y;
            y21 = p2.Y + p1.Y;

            dx31 = p3.X - p1.X;
            x31 = p3.X + p1.X;
            dy31 = p3.Y - p1.Y;
            y31 = p3.Y + p1.Y;

            if (dx21 != 0 && dy21 != 0 && dx31 != 0 && dy31 != 0)
            {
                p0.Y = (dx31 * (dx21 * x21 + dy21 * y21) - dx21 * (dx31 * x31 + dy31 * y31)) / (2 * (dx31 * dy21 - dy31 * dx21));
                p0.X = (dx21 * x21 + dy21 * y21 - 2 * dy21 * p0.Y) / (2 * dx21);
            }

            else
            {
                if (dx21 == 0)
                {
                    p0.Y = y21 / 2;
                    p0.X = (x31 + (dy31 / dx31) * (y31 - y21)) / 2;
                }
                else if (dy21 == 0)
                {
                    p0.X = x21 / 2;
                    p0.Y = (y31 + (dx31 / dy31) * (x31 - x21)) / 2;
                }
                else if (dx31 == 0)
                {
                    p0.Y = y31 / 2;
                    p0.X = (x21 + (dy21 / dx21) * (y21 - y31)) / 2;
                }
                else if (dy31 == 0)
                {
                    p0.X = x31 / 2;
                    p0.Y = (y21 + (dx21 / dy21) * (x21 - x31)) / 2;
                }
            }

            return p0;
        }
        #endregion

        #region		//由三点计算圆半径Point1,Point2，Point2
        /// <summary>
        /// 由三点计算圆半径
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static double GetRadii_P123(IPoint p1, IPoint p2, IPoint p3)
        {
            double dx21;
            double x21;
            double dy21;
            double y21;
            double dx31;
            double x31;
            double dy31;
            double y31;
            IPoint p0 = new PointClass();

            dx21 = p2.X - p1.X;
            x21 = p2.X + p1.X;
            dy21 = p2.Y - p1.Y;
            y21 = p2.Y + p1.Y;

            dx31 = p3.X - p1.X;
            x31 = p3.X + p1.X;
            dy31 = p3.Y - p1.Y;
            y31 = p3.Y + p1.Y;

            if (dx21 != 0 && dy21 != 0 && dx31 != 0 && dy31 != 0)
            {
                p0.Y = (dx31 * (dx21 * x21 + dy21 * y21) - dx21 * (dx31 * x31 + dy31 * y31)) / (2 * (dx31 * dy21 - dy31 * dx21));
                p0.X = (dx21 * x21 + dy21 * y21 - 2 * dy21 * p0.Y) / (2 * dx21);
            }

            else
            {
                if (dx21 == 0)
                {
                    p0.Y = y21 / 2;
                    p0.X = (x31 + (dy31 / dx31) * (y31 - y21)) / 2;
                }
                else if (dy21 == 0)
                {
                    p0.X = x21 / 2;
                    p0.Y = (y31 + (dx31 / dy31) * (x31 - x21)) / 2;
                }
                else if (dx31 == 0)
                {
                    p0.Y = y31 / 2;
                    p0.X = (x21 + (dy21 / dx21) * (y21 - y31)) / 2;
                }
                else if (dy31 == 0)
                {
                    p0.X = x31 / 2;
                    p0.Y = (y21 + (dx21 / dy21) * (x21 - x31)) / 2;
                }
            }


            double r = GetDistance_P12(p0, p1);

            return r;
        }
        #endregion

        #region		//由圆心和半径cP,R求圆上另一点mP
        /// <summary>
        /// 由圆心和半径求圆上另一点的坐标
        /// </summary>
        /// <param name="cP"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public static IPoint GetPoingCir_Pcr(IPoint cP, double R)
        {
            IPoint mP = new PointClass();
            mP.X = cP.X + R;
            mP.Y = cP.Y;
            return mP;
        }
        #endregion

        #region		//由圆直径上两点求圆心坐标p1,p2
        /// <summary>
        /// 由圆直径上两点求圆心坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static IPoint GetCircleCenter_P12(IPoint p1, IPoint p2)
        {
            IPoint mP = new PointClass();

            //求p1到p2方位角
            double Ap12;
            Ap12 = GetAzimuth_P12(p1, p2);
            //求直径R
            double R;
            R = GetDistance_P12(p1, p2);

            mP.X = p1.X + (R / 2) * Math.Cos(Ap12);
            mP.Y = p1.Y + (R / 2) * Math.Sin(Ap12);
            return mP;
        }
        #endregion

        #region		//由圆直径上两点p1,p2及和p2相连的边B的长度d,求圆上边B另一点p3
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static IPoint GetCircleP3FormP1P2D(IPoint p1, IPoint p2, double d)
        {
            IPoint tempP = new PointClass();

            //求直径R
            double R;
            R = GetDistance_P12(p1, p2);

            //求p2到p2夹角
            double tempA23;
            tempA23 = Math.Acos(d / R);

            tempP.X = p1.X + R - d * Math.Cos(tempA23);
            tempP.Y = p1.Y + d * Math.Sin(tempA23);
            return tempP;
        }
        #endregion

        #region	//已知相切圆上三点和待画弧上一点求弧圆心坐标
        /// <summary>
        /// 已知相切圆上三点和待画弧上一点求待画弧圆心坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static IPoint GetCenterC(IPoint p1, IPoint p2, IPoint p3, IPoint p4)
        {
            //求已知圆的圆心坐标
            IPoint yzp0 = GetCenter_P123(p1, p2, p3);
            //求已知圆心到端点方位角
            double AP03 = GetAzimuth_P12(yzp0, p3);
            //求切线方位角
            double TA = GetTangentLineAzi(p1, p2, p3);
            //求已知圆端点到弧端点方位角
            double AP34;
            AP34 = GetAzimuth_P12(p3, p4);
            //讨论圆心方位角、圆心角1/2
            double AC = 0;
            double a = 0;
            double dA;
            dA = AP34 - TA;
            if (dA < 0)
            {
                dA = dA + Math.PI * 2;
            }
            if (dA >= 0 && dA < Math.PI)
            {
                //圆心在切线左侧
                AC = TA + Math.PI / 2;
                a = AP34 - TA;
            }
            else if (dA >= Math.PI && dA < Math.PI * 2)
            {
                //圆心在切线右侧
                AC = TA - Math.PI / 2;
                a = TA - AP34;
            }
            if (AC < 0)
            {
                AC = AC + 2 * Math.PI;
            }
            else if (AC > 2 * Math.PI)
            {
                AC = AC - 2 * Math.PI;
            }

            if (a < 0)
            {
                a = a + Math.PI * 2;
            }
            //求弦长
            double DP34;
            DP34 = GetDistance_P12(p3, p4);
            //求半径
            double R;
            R = DP34 / (2 * Math.Sin(a));

            //求圆心坐标
            IPoint p0 = new PointClass();
            p0.X = p3.X + R * Math.Cos(AC);
            p0.Y = p3.Y + R * Math.Sin(AC);

            return p0;
        }
        #endregion

        #region	//已知圆上三点求过最后一点圆切线方位角
        /// <summary>
        /// 已知圆上三点求过最后一点圆切线方位角
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static double GetTangentLineAzi(IPoint p1, IPoint p2, IPoint p3)
        {
            //求已知圆的圆心坐标
            IPoint yzp0 = CommonFunction.GetCenter_P123(p1, p2, p3);
            //求已知圆心到端点方位角
            double AP03 = CommonFunction.GetAzimuth_P12(yzp0, p3);
            //求切线方位角
            double TA1 = AP03 + Math.PI / 2;
            double TA2 = AP03 - Math.PI / 2;
            if (TA2 < 0)
            {
                TA2 = TA2 + 2 * Math.PI;
            }
            if (TA1 > 2 * Math.PI)
            {
                TA1 = TA1 - 2 * Math.PI;
            }

            //计算曲中点到端点的方位角
            double ATE = CommonFunction.GetAzimuth_P12(p2, p3);

            //计算切线方位角与曲中点到端点的方位角的夹角
            double At1e = Math.Abs(TA1 - ATE);
            double At2e = Math.Abs(TA2 - ATE);
            double TA = 0;

            //讨论曲线走向（顺时针、逆时针）
            if (At1e < 0)
            {
                At1e = At1e + 2 * Math.PI;
            }
            if (At2e < 0)
            {
                At2e = At2e + 2 * Math.PI;
            }

            if (At1e < Math.PI / 2)
            {
                //顺时针方向画弧
                TA = TA1;
            }
            if (At2e < Math.PI / 2)
            {
                //逆时针方向画弧
                TA = TA2;
            }

            if (TA < 0)
            {
                TA = TA + 2 * Math.PI;
            }
            if (TA > 2 * Math.PI)
            {
                TA = TA - 2 * Math.PI;
            }

            return TA;
        }
        #endregion

        #region //已知矩形两相邻点p1,p2的坐标和一条边长d时，求取另两个角点的坐标pm,pn
        /// <summary>
        /// 已知矩形两相邻角点p1,p2的坐标和一条边长d时，求取另两个角点的坐标pm,pn
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static IArray GetPointRectangle2(IPoint p1, IPoint p2, double d)
        {
            IArray pointArray = new ESRI.ArcGIS.esriSystem.ArrayClass();
            IPoint pm = new PointClass();
            IPoint pn = new PointClass();
            double A_P2m = 0;
            double A_P1n = 0;

            //计算P1，P2的方位角Ap12
            double Ap12 = 0;
            Ap12 = GetAzimuth_P12(p1, p2);

            //计算方位角A_P2m
            A_P2m = Ap12 + Math.PI / 2;

            if (A_P2m > 2 * Math.PI)
            {
                A_P2m = A_P2m - 2 * Math.PI;
            }
            if (A_P2m < 0)
            {
                A_P2m = A_P2m + 2 * Math.PI;
            }

            //计算Pm坐标
            pm.X = p2.X + d * Math.Cos(A_P2m);
            pm.Y = p2.Y + d * Math.Sin(A_P2m);

            A_P1n = A_P2m;
            //计算Pn坐标
            pn.X = p1.X + d * Math.Cos(A_P1n);
            pn.Y = p1.Y + d * Math.Sin(A_P1n);

            //将点添加进点数组
            pointArray.Add(pm);
            pointArray.Add(pn);
            return pointArray;
        }
        #endregion

        #region//已知矩形两相邻点p1、p2以及第三点p0，计算矩形边长
        /// <summary>
        /// 已知矩形两相邻点p1、p2以及第三点p0，计算矩形边长
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p0"></param>
        /// <returns></returns>
        public static double GetRectangleOfSide_Length(IPoint p1, IPoint p2, IPoint p0)
        {
            double dblSideLength;
            double Ap20 = 0;
            double Ap21 = 0;

            //计算p2到p0的方位角
            Ap20 = GetAzimuth_P12(p2, p0);

            //计算p2到p1的方位角
            Ap21 = GetAzimuth_P12(p2, p1);

            //计算边长d
            dblSideLength = Math.Sqrt((p2.X - p0.X) * (p2.X - p0.X) + (p2.Y - p0.Y) * (p2.Y - p0.Y)) * Math.Cos(Math.PI / 2 - (Ap21 - Ap20));
            return dblSideLength;
        }
        #endregion

        #region //已知矩形两对角点p1,p2的坐标和一条边长d时，求取另两个角点的坐标pm,pn
        /// <summary>
        /// 已知矩形两对角点p1,p2的坐标和一条边长d时，求取另两个角点的坐标pm,pn
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static IArray GetPointRectangleOfRelative_Length(IPoint p1, IPoint p2, double d, bool x)
        {
            IArray pointArray = new ESRI.ArcGIS.esriSystem.ArrayClass();
            IPoint pm = new PointClass();
            IPoint pn = new PointClass();

            //计算P1，P2的方位角Ap12
            double Ap12 = 0;
            Ap12 = GetAzimuth_P12(p1, p2);

            //计算P12与已知边d的夹角aPd
            double aPd = 0;
            aPd = Math.Acos(d / Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)));

            if (x == true)     //已知边是p1的顺时针“邻边”时	
            {
                double A_P1m = 0;
                double A_P2n = 0;

                //计算方位角A_P1m			    
                A_P1m = Ap12 - aPd;
                if (A_P1m < 0)
                {
                    A_P1m = A_P1m + 2 * Math.PI;
                }
                if (A_P1m >= 2 * Math.PI)
                {
                    A_P1m = A_P1m - 2 * Math.PI;
                }

                //计算Pm坐标
                pm.X = p1.X + d * Math.Cos(A_P1m);
                pm.Y = p1.Y + d * Math.Sin(A_P1m);

                //计算方位角A_P2n			    
                A_P2n = A_P1m + Math.PI;
                if (A_P2n >= 2 * Math.PI)
                {
                    A_P2n = A_P2n - 2 * Math.PI;
                }

                //计算Pn坐标
                pn.X = p2.X + d * Math.Cos(A_P2n);
                pn.Y = p2.Y + d * Math.Sin(A_P2n);
            }
            else//已知边是p1的顺时针“对边”时
            {
                double A_P2m = 0;
                double A_P1n = 0;

                //计算方位角A_P1n							
                A_P1n = Ap12 + aPd;
                if (A_P1n < 0)
                {
                    A_P1n = A_P1n + 2 * Math.PI;
                }
                if (A_P1n >= 2 * Math.PI)
                {
                    A_P1n = A_P1n - 2 * Math.PI;
                }

                //计算Pn坐标
                pn.X = p1.X + d * Math.Cos(A_P1n);
                pn.Y = p1.Y + d * Math.Sin(A_P1n);

                //计算方位角A_P2m
                A_P2m = A_P1n + Math.PI;
                if (A_P2m >= 2 * Math.PI)
                {
                    A_P2m = A_P2m - 2 * Math.PI;
                }

                //计算Pm坐标
                pm.X = p2.X + d * Math.Cos(A_P2m);
                pm.Y = p2.Y + d * Math.Sin(A_P2m);
            }

            //将点添加进点数组
            pointArray.Add(pm);
            pointArray.Add(pn);

            return pointArray;
        }
        #endregion

        #region //已知线p12，判断鼠标是否位于P1到P2方向的右边(有错误)
        /// <summary>
        /// 已知线p12，判断鼠标是否位于P1到P2方向的右边
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p0"></param>
        /// <returns></returns>
        public static bool GetRectP0_Right(IPoint p1, IPoint p2, IPoint p0)
        {
            double Ap20 = 0;
            double Ap21 = 0;

            //计算p2到p0的方位角
            Ap20 = GetAzimuth_P12(p2, p0);

            //计算p1到p2的方位角
            Ap21 = GetAzimuth_P12(p2, p1);

            //判断P0是否位于P1到P2方向的左边
            if (Ap20 < Ap21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region	//已知切线上两点和待画弧上一点求弧圆心坐标
        /// <summary>
        /// 已知切线上两点和待画弧上一点求弧圆心坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static IPoint GetCenterL(IPoint p1, IPoint p2, IPoint p3)
        {
            //求直线方位角
            double AP12;
            AP12 = GetAzimuth_P12(p1, p2);
            //求直线端点到弧端点方位角
            double AP23;
            AP23 = GetAzimuth_P12(p2, p3);
            //求圆心角/2
            double a;
            a = AP23 - AP12;
            if (a < 0)
            {
                a = a + Math.PI * 2;
            }
            //求弦长
            double DP23;
            DP23 = GetDistance_P12(p2, p3);
            //求半径
            double R;
            R = DP23 / (2 * Math.Sin(a));
            //求直线端点到圆心方位角
            double AP20;
            AP20 = AP12 + Math.PI / 2;
            if (AP20 > Math.PI * 2)
            {
                AP20 = AP20 - Math.PI * 2;
            }
            //求圆心坐标
            IPoint p0 = new PointClass();
            p0.X = p2.X + R * Math.Cos(AP20);
            p0.Y = p2.Y + R * Math.Sin(AP20);

            return p0;
        }

        #endregion

        #region//获取点集pPointColl关于直线P12的对称点集MpPointColl
        /// <summary>
        /// 获取点集pPointColl关于直线P12的对称点集MpPointColl
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="pPointColl"></param>
        public static void Mirror(IPoint p1, IPoint p2, ref IPointCollection pPointColl)
        {
            double A12 = CommonFunction.GetAzimuth_P12(p1, p2);

            if (pPointColl.PointCount > 0)
            {
                int Count = pPointColl.PointCount;

                for (int i = 0; i < Count; i++)
                {
                    IPoint eP = pPointColl.get_Point(i);

                    IPoint MeP = new PointClass();
                    //获取eP关于直线P12的对称点MeP
                    //求p1e与p12的夹角a
                    double a = 0;
                    a = CommonFunction.GetAngle_P123(eP, p1, p2);
                    //求eP到MeP的距离
                    double Dpp = 0;
                    Dpp = 2 * (CommonFunction.GetDistance_P12(eP, p1) * Math.Sin(a));
                    //求eP到MeP的方位角
                    double App = 0;
                    double A1e = CommonFunction.GetAzimuth_P12(p1, eP);
                    if (Math.Abs(A12 - A1e) > Math.PI)
                    {
                        A12 = A12 - Math.PI;
                        if (A12 < 0)
                        {
                            A12 = A12 + Math.PI * 2;
                        }
                    }
                    if (A12 > A1e)
                    {
                        App = A12 + Math.PI / 2;
                    }
                    else
                    {
                        App = A12 - Math.PI / 2;
                    }
                    //求MeP的坐标
                    MeP.X = eP.X + Dpp * Math.Cos(App);
                    MeP.Y = eP.Y + Dpp * Math.Sin(App);
                    MeP.Z = eP.Z;//将MeP添加到数组中
                    MeP.M = eP.M;

                    pPointColl.UpdatePoint(i, MeP);
                }
            }
        }
        #endregion

        #region//获取点pPoint关于直线P12的对称点MpPoint
        /// <summary>
        /// 获取点pPoint关于直线P12的对称点MpPoint
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="pPoint"></param>
        public static void Mirror(IPoint p1, IPoint p2, ref IPoint pPoint)
        {
            double A12 = CommonFunction.GetAzimuth_P12(p1, p2);

            IPoint eP = pPoint;

            IPoint MeP = new PointClass();
            //获取eP关于直线P12的对称点MeP
            //求p1e与p12的夹角a
            double a = 0;
            a = CommonFunction.GetAngle_P123(eP, p1, p2);
            //求eP到MeP的距离
            double Dpp = 0;
            Dpp = 2 * (CommonFunction.GetDistance_P12(eP, p1) * Math.Sin(a));
            //求eP到MeP的方位角
            double App = 0;
            double A1e = CommonFunction.GetAzimuth_P12(p1, eP);
            if (Math.Abs(A12 - A1e) > Math.PI)
            {
                A12 = A12 - Math.PI;
                if (A12 < 0)
                {
                    A12 = A12 + Math.PI * 2;
                }
            }
            if (A12 > A1e)
            {
                App = A12 + Math.PI / 2;
            }
            else
            {
                App = A12 - Math.PI / 2;
            }
            //求MeP的坐标
            MeP.X = eP.X + Dpp * Math.Cos(App);
            MeP.Y = eP.Y + Dpp * Math.Sin(App);

            pPoint = MeP;

        }
        #endregion

        #region//屏幕单位到地图单位的转换
        /// <summary>
        /// 屏幕单位转换到地图单位
        /// </summary>
        /// <param name="pActiveView"></param>
        /// <param name="pixelUnits"></param>
        /// <returns></returns>
        public static double ConvertPixelsToMapUnits(IActiveView pActiveView, double pixelUnits)
        {
            double realWorldDisplayExtent;
            int pixelExtent;
            double sizeOfOnePixel;

            pixelExtent = pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right -
                pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;

            realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            sizeOfOnePixel = realWorldDisplayExtent / pixelExtent;
            return pixelUnits * sizeOfOnePixel;
        }
        #endregion

        #region//颜色函数
        /// <summary>
        /// 颜色函数
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Green"></param>
        /// <param name="Blue"></param>
        /// <returns></returns>
        public static IRgbColor GetRgbColor(int Red, int Green, int Blue)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = Red;
            rgbColor.Green = Green;
            rgbColor.Blue = Blue;
            return rgbColor;
        }
        #endregion

        #region 根据红、绿、蓝三种颜色的值生成一个IRgbColor对象
        /// <summary>
        /// 根据红、绿、蓝三种颜色的值生成一个IRgbColor对象
        /// </summary>
        /// <param name="m_Red">红色取值范围：0～255</param>
        /// <param name="m_Green">绿色取值范围：0～255</param>
        /// <param name="m_Blue">蓝色取值范围：0～255</param>
        /// <returns>一个IRgbColor对象</returns>
        public static RgbColorClass GetRGBColor(int m_Red, int m_Green, int m_Blue)
        {
            //生成一个RGBColor
            RgbColorClass pRGB = new RgbColorClass();

            pRGB.Red = m_Red;
            pRGB.Green = m_Green;
            pRGB.Blue = m_Blue;
            pRGB.UseWindowsDithering = false;
            return pRGB;
        }
        #endregion

        #region//在地图上用方块闪烁捕捉到的点
        /// <summary>
        /// 在地图上用方块闪烁捕捉到的点
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pPoint"></param>
        public static void DrawSnapSMSSquareSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISymbol pSym = new SimpleMarkerSymbolClass();
            ISimpleMarkerSymbol pMarkerSym = new SimpleMarkerSymbolClass();
            pMarkerSym.Style = esriSimpleMarkerStyle.esriSMSSquare;
            pMarkerSym.Size = 4;
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pMarkerSym.Color = pColor;
            pSym = (ISymbol)pMarkerSym;
            pSym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            object opSym = pSym;
            pMapControl.DrawShape(pPoint, ref opSym);
            System.Threading.Thread.Sleep(50);
            pMapControl.DrawShape(pPoint, ref opSym);

        }
        #endregion

        #region//在图上用方块状的地图元素显示选中的地理要素的节点
        /// <summary>
        /// 在图上用方块状的地图元素显示选中的地理要素的节点
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pPoint"></param>
        public static void DrawPointSMSSquareSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;
            if (pMapControl.MapScale != 0)
            {
                pSymbol.Size = 4 * (pMapControl.MapScale / 500); //Edit By 罗璇 2008-12-05
            }
            else
            {
                pSymbol.Size = 4;
            }
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pSymbol.Color = pColor;
            IElement pElement = new MarkerElementClass();
            pElement.Geometry = (IGeometry)pPoint;

            IMarkerElement pMarkerElement;
            pMarkerElement = (IMarkerElement)pElement;
            pMarkerElement.Symbol = pSymbol;

            pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            IEnvelope enve = new EnvelopeClass();
            enve = NewRect(pPoint, ConvertPixelsToMapUnits(pMapControl.ActiveView, 4));
            pMapControl.Refresh(esriViewDrawPhase.esriViewGraphics, pElement, enve);

        }
        #endregion

        #region//在图上用X状的地图元素显示镜像、旋转、偏移、移动的基点
        /// <summary>
        /// 在图上用X状的地图元素显示镜像、旋转、偏移、移动的基点
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pPoint"></param>
        public static IElement DrawPointSMSXSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
            pSymbol.Style = esriSimpleMarkerStyle.esriSMSX;
            pSymbol.Size = 16;
            pSymbol.OutlineSize = 10;
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pSymbol.Color = pColor;
            IElement pElement = new MarkerElementClass();
            pElement.Geometry = (IGeometry)pPoint;

            IMarkerElement pMarkerElement;
            pMarkerElement = (IMarkerElement)pElement;
            pMarkerElement.Symbol = pSymbol;

            pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            IEnvelope enve = new EnvelopeClass();
            enve = NewRect(pPoint, ConvertPixelsToMapUnits(pMapControl.ActiveView, 4));

            pMapControl.Refresh(esriViewDrawPhase.esriViewGraphics, pElement, enve);

            return pElement;

        }
        #endregion

        #region//在图上用方块显示选中的地理要素的节点
        //
        public static void DrawPointSymbol(IMapControl2 pMapControl, IPoint pPoint)
        {
            ISymbol pSym = new SimpleMarkerSymbolClass();
            ISimpleMarkerSymbol pMarkerSym = new SimpleMarkerSymbolClass();
            pMarkerSym.Style = esriSimpleMarkerStyle.esriSMSSquare;
            pMarkerSym.Size = 4;
            IColor pColor;
            pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pMarkerSym.Color = pColor;
            pSym = (ISymbol)pMarkerSym;
            //pSym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            object opSym = pSym;
            pMapControl.DrawShape(pPoint, ref opSym);
        }
        #endregion

        #region//从选中要素缓冲中获取最近的要素
        /// <summary>
        /// 从选中要素缓冲中获取最近的要素
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pPoint"></param>
        /// <param name="pFeature"></param>
        public static void GetClosestSelectedFeature(IFeatureCache pFeatureCache, IPoint pPoint, out IFeature pFeature)
        {
            if (pFeatureCache.Count == 1)
            {
                pFeature = pFeatureCache.get_Feature(0);
            }
            else if (pFeatureCache.Count > 1)
            {
                double testDist;
                IProximityOperator pProximity;
                IGeometry pGeom;
                IFeature pTestFeature;
                double tempDist;
                pProximity = (IProximityOperator)pPoint;
                pTestFeature = pFeatureCache.get_Feature(0);
                pGeom = pTestFeature.ShapeCopy;
                testDist = pProximity.ReturnDistance(pGeom);
                pFeature = pTestFeature;
                for (int i = 1; i < pFeatureCache.Count; i++)
                {
                    pTestFeature = pFeatureCache.get_Feature(i);
                    pGeom = pTestFeature.Shape;
                    tempDist = pProximity.ReturnDistance(pGeom);

                    if (tempDist < testDist)
                    {
                        testDist = tempDist;
                        pFeature = pTestFeature;
                    }
                }
            }
            else
            {
                pFeature = null;
            }

        }
        #endregion

        #region //分解几何形体
        /// <summary>
        /// 分解几何形体
        /// </summary>
        /// <param name="oGeo"></param>
        /// <param name="pFeatureLayer"></param>
        /// <returns></returns>
        public static IArray DecomposeGeometry(IGeometry oGeo, IFeatureLayer pFeatureLayer)
        {
            if (oGeo == null) return null;

            IArray oArrayGeo = new ArrayClass();
            IGeometryCollection oGeoSet = (IGeometryCollection)oGeo;
            for (int i = 0; i < oGeoSet.GeometryCount; i++)
            {
                IGeometry oPartGeo = oGeoSet.get_Geometry(i);
                switch ((pFeatureLayer.FeatureClass).ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        IPoint oNewPoint = new PointClass();
                        ((IGeometryCollection)oNewPoint).AddGeometries(1, ref oPartGeo);
                        oArrayGeo.Add(oNewPoint);
                        break;

                    case esriGeometryType.esriGeometryPolyline:
                        IPolyline oNewPolyline = new PolylineClass();
                        ((IGeometryCollection)oNewPolyline).AddGeometries(1, ref oPartGeo);
                        oArrayGeo.Add(oNewPolyline);
                        break;

                    case esriGeometryType.esriGeometryPolygon:
                        IPolygon oNewPolygon = new PolygonClass();
                        ((IGeometryCollection)oNewPolygon).AddGeometries(1, ref oPartGeo);
                        oArrayGeo.Add(oNewPolygon);
                        break;
                }

            }
            return oArrayGeo;
        }
        #endregion

        #region//合并两个几何形体
        /// <summary>
        /// 合并两个几何形体
        /// </summary>
        /// <param name="pGeo"></param>
        /// <param name="pOtherGeo"></param>
        /// <returns></returns>
        public static IGeometry UnionGeometry(IGeometry Merge, IGeometry pOtherGeo)
        {
            IGeometryCollection geometryCollection1 = Merge as IGeometryCollection;
            IGeometryCollection geometryCollection2 = pOtherGeo as IGeometryCollection;

            try
            {
                geometryCollection1.AddGeometryCollection(geometryCollection2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return geometryCollection1 as IGeometry;



            //if ((pGeo.GeometryType != esriGeometryType.esriGeometryPolyline)
            //    || (pOtherGeo.GeometryType != esriGeometryType.esriGeometryPolyline)) return null;

            //ITopologicalOperator pTopologicalOperator1 = (ITopologicalOperator)pOtherGeo;
            //pTopologicalOperator1.Simplify();

            //ITopologicalOperator pTopologicalOperator2 = (ITopologicalOperator)pGeo;
            //pTopologicalOperator2.Simplify();

            //IGeometry pRsGeo = null;
            //try
            //{
            //    pRsGeo = pTopologicalOperator2.Union(pOtherGeo);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());

            //    IPointCollection pPointCollection1 = pGeo as IPointCollection;
            //    for (int i = 0; i < pPointCollection1.PointCount; i++)
            //    {
            //        Console.WriteLine(pPointCollection1.get_Point(i).X.ToString() + "= " + pPointCollection1.get_Point(i).Y.ToString());
            //    }
            //    IPointCollection pPointCollection2 = pOtherGeo as IPointCollection;

            //    IPolyline pPolyline1 = pGeo as IPolyline;
            //    IPolyline pPolyline2 = pOtherGeo as IPolyline;

            //    ITopologicalOperator pTopologicalOperator3 = (ITopologicalOperator)pPolyline1;
            //    pRsGeo = pTopologicalOperator3.Union(pPolyline2);

            //}

            //return pRsGeo;


        }
        #endregion

        #region//合并两个几何形体
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGeo"></param>
        /// <returns></returns>
        public static IGeometry UnionGeometry(IGeometry pGeo)
        {
            if ((pGeo == null)) return null;
            ITopologicalOperator pTopologicalOperator = (ITopologicalOperator)pGeo;
            pTopologicalOperator.Simplify();
            return pGeo;
        }
        #endregion

        #region//两个几何形体求交
        /// <summary>
        /// 两个几何形体求交
        /// </summary>
        /// <param name="pGeo"></param>
        /// <param name="pOtherGeo"></param>
        /// <returns></returns>
        public static IGeometry IntersectGeometry(IGeometry pGeo, IGeometry pOtherGeo)
        {
            if ((pGeo == null) || (pOtherGeo == null)) return null;

            ITopologicalOperator pTopologicalOperator = (ITopologicalOperator)pOtherGeo;
            pTopologicalOperator.Simplify();

            pTopologicalOperator = (ITopologicalOperator)pGeo;
            pTopologicalOperator.Simplify();

            IGeometry pRsGeo = pTopologicalOperator.Intersect(pOtherGeo, esriGeometryDimension.esriGeometry2Dimension);

            return pRsGeo;
        }
        #endregion

        #region//两个几何形体求差
        /// <summary>
        /// 两个几何形体求差
        /// </summary>
        /// <param name="pGeo"></param>
        /// <param name="pOtherGeo"></param>
        /// <returns></returns>
        public static IGeometry DiffenceGeometry(IGeometry pGeo, IGeometry pOtherGeo)
        {
            if ((pGeo == null) || (pOtherGeo == null)) return null;

            ITopologicalOperator pTopologicalOperator = (ITopologicalOperator)pOtherGeo;
            pTopologicalOperator.Simplify();

            pTopologicalOperator = (ITopologicalOperator)pGeo;
            pTopologicalOperator.Simplify();

            IGeometry pRsGeo = pTopologicalOperator.Difference(pOtherGeo);

            return pRsGeo;
        }
        #endregion

        #region //开始编辑
        /// <summary>
        /// 开始编辑
        /// </summary>
        /// <param name="pMap"></param>
        public static void StartEditing(IMap pMap)
        {
            if (pMap == null) return;

            if (pMap.LayerCount < 1) return;

            //有多个Workspace的处理
            //IWorkspace pWorkspace 
            int count = 0;
            string compString = "";

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                count += CommonFunction.CheckStartEditing(pMap.get_Layer(i), ref compString);
            }
            if (count == 0)
            {
                System.Windows.Forms.MessageBox.Show("不能编辑任何图层，请检查数据是否已经进行了版本注册或是否有更新权限！",
                    "开始编辑", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }

        }
        #endregion

        #region//检查图层开始编辑状态.
        /// <summary>
        /// 检查图层开始编辑状态
        /// </summary>
        /// <param name="pLayer"></param>待检查的图层
        /// <param name="compString"></param>返回不能开始编辑的图层
        /// <returns></returns>	
        public static int CheckStartEditing(ILayer pLayer, ref string compString)
        {
            int count = 0;
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            ICompositeLayer pComp;
            int i;
            if (pLayer is IGroupLayer)//如果是图层组
            {
                pComp = (ICompositeLayer)pLayer;
                for (i = 0; i < pComp.Count; i++)
                {
                    count += CheckStartEditing(pComp.get_Layer(i), ref compString);
                    //if(count)
                }
            }
            else
            {
                if (pLayer is FeatureLayer)//如果是地理要素图层
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;//跳转到IFeatureLayer接口
                    if (pFeatureLayer.FeatureClass == null) return count;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;//跳转到IDataset接口
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass ||
                        pDataset.Type == esriDatasetType.esriDTFeatureDataset)//如果数据集是要素类或要素数据集
                    {
                        pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;//跳转到IWorkspaceEdit接口
                        if (pDataset.Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)//如果是远程数据库工作空间
                        {
                            if (pDataset.Workspace is IVersionedWorkspace)//如果是版本工作空间
                            {
                                IVersionedObject pVersionObject = pDataset as IVersionedObject;//跳转到IVersionedObject接口
                                if (pVersionObject.IsRegisteredAsVersioned)//若版本对象注册
                                {
                                    if (!pWorkspaceEdit.IsBeingEdited())//若没有开始 编辑
                                    {
                                        try
                                        {
                                            pWorkspaceEdit.StartEditing(true);//试图开始编辑
                                            pWorkspaceEdit.EnableUndoRedo();//开始重作社生效
                                            count++;//计数器累加
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                            }
                            else//如果是非版本工作空间
                            {
                                compString = compString + @"\r\n" + pFeatureLayer.Name;
                            }
                        }
                        else//如果不是远程数据库工作空间
                        {
                            if (!pWorkspaceEdit.IsBeingEdited())//若没有开始编辑
                            {
                                try
                                {
                                    pWorkspaceEdit.StartEditing(true);//试图开始编辑
                                    pWorkspaceEdit.EnableUndoRedo();//开始重作生效
                                    count++;//计数器累加
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }

                    }
                    else//如果数据集不是是要素类或要素数据集
                    {
                        compString = compString + @"\r\n" + pFeatureLayer.Name;
                    }
                }
            }

            return count;
        }

        #endregion

        #region//检查停止编辑状态
        /// <summary>
        /// 检查停止编辑状态
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="saveEdits"></param>
        /// <returns></returns>
        public static bool CheckStopEdits(ILayer pLayer, bool saveEdits)
        {
            bool result = false;
            try
            {
                IFeatureLayer pFeatureLayer;
                IDataset pDataset;
                IWorkspaceEdit pWorkspaceEdit;
                ICompositeLayer pComp;

                if (pLayer is IGroupLayer)
                {
                    pComp = (ICompositeLayer)pLayer;
                    for (int i = 0; i < pComp.Count; i++)
                    {
                        if (CheckStopEdits(pComp.get_Layer(i), saveEdits))
                            result = true;
                    }
                }
                else
                {
                    if (pLayer is IFeatureLayer)
                    {
                        pFeatureLayer = (IFeatureLayer)pLayer;
                        if (pFeatureLayer.FeatureClass == null) return false;
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                            if (pWorkspaceEdit.IsBeingEdited())
                            {
                                if (saveEdits)
                                {
                                    pWorkspaceEdit.HasEdits(ref saveEdits);
                                }
                                pWorkspaceEdit.StopEditing(saveEdits);

                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        #endregion

        #region//停止或保存编辑，传入的pMap不能为空
        /// <summary>
        /// 停止或保存编辑，传入的pMap不能为空
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="saveEdits"></param>
        /// <param name="warnUser"></param>
        /// <returns></returns>
        public static bool StopEditing(IMap pMap, bool saveEdits, bool warnUser)
        {
            if (pMap.LayerCount < 1) return false;
            System.Windows.Forms.DialogResult result;

            bool haveEdits = false;
            if (warnUser)
            {
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    if (CheckWorkspaceEdit(pMap.get_Layer(i), "hasEdits"))
                    {
                        haveEdits = true;
                        break;
                    }
                }
                if (!haveEdits)
                {
                    result = DialogResult.No;
                }
                else
                {
                    result = XtraMessageBox.Show("数据已经被修改过，保存修改吗?", "更改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                if (result == DialogResult.Cancel)
                    return false;
            }
            else
            {
                if (saveEdits)
                {
                    result = DialogResult.Yes;
                }
                else
                {
                    result = DialogResult.No;

                }
            }
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                CheckStopEdits(pMap.get_Layer(i), (result == DialogResult.Yes));
            }

            pMap.ClearSelection();

            ((IActiveView)pMap).Refresh();
            return true;
        }
        #endregion

        #region//检查图层编辑情况
        /// <summary>
        /// 检查图层编辑情况
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static bool CheckWorkspaceEdit(ILayer pLayer, string check)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            bool boolCheck = false;
            ICompositeLayer pComp;
            if (pLayer is IGroupLayer)
            {
                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    if (CheckWorkspaceEdit(pComp.get_Layer(i), check))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = pDataset.Workspace as IWorkspaceEdit;
                            if (pWorkspaceEdit == null) return false;

                            switch (check)
                            {
                                case "IsBeingEdited":
                                    if (pWorkspaceEdit.IsBeingEdited())
                                        return true;
                                    break;
                                case "hasEdits":
                                    pWorkspaceEdit.HasEdits(ref boolCheck);
                                    return boolCheck;
                                case "hasUndos":
                                    pWorkspaceEdit.HasUndos(ref boolCheck);
                                    return boolCheck;
                                case "hasRedos":
                                    pWorkspaceEdit.HasRedos(ref boolCheck);
                                    return boolCheck;
                            }

                        }
                    }
                }

            }
            return false;
        }
        #endregion

        #region//检查撤销、重做情况
        /// <summary>
        /// 检查撤销、重做情况
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="undo"></param>
        /// <returns></returns>
        private static bool CheckUndoReDo(ILayer pLayer, bool undo)
        {

            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            bool hasRedos = false;
            bool hasUndos = false;

            ICompositeLayer pComp;


            if (pLayer is IGroupLayer)
            {

                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    if (CheckUndoReDo(pComp.get_Layer(i), undo))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass == null) return false;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;
                    if (pDataset != null)
                    {
                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                            if (pWorkspaceEdit.IsBeingEdited())
                            {
                                if (undo)
                                {
                                    pWorkspaceEdit.HasUndos(ref hasUndos);
                                    if (hasUndos) pWorkspaceEdit.UndoEditOperation();
                                }
                                else
                                {
                                    pWorkspaceEdit.HasRedos(ref  hasRedos);
                                    if (hasRedos) pWorkspaceEdit.RedoEditOperation();
                                }
                                return true;

                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region//编辑的撤销/重做
        /// <summary>
        /// 编辑的撤销/重做;
        /// </summary>
        /// <param name="pMap">操作的地图对象 pMap;</param>
        /// <param name="undo">undo = true则做撤销 ，undo = false则做重做</param>
        public static void UndoRedoEdit(IMap pMap, bool undo)
        {
            if (pMap.LayerCount < 1) return;

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (CheckUndoReDo(pMap.get_Layer(i), undo)) break;
            }

            IActiveView pActiveView = (IActiveView)pMap;

            pActiveView.Refresh();

        }
        #endregion

        #region//由图层名称查找图层
        /// <summary>
        /// 由图层名称查找图层
        /// </summary>
        /// <param name="oMap"></param>
        /// <param name="sName"></param>
        /// <returns></returns>
        public static ILayer GetLayerByNameFrom_Map(IMap oMap, string sName)
        {
            if ((oMap == null) || (sName == "")) return null;

            ILayer OutLayer = null;
            for (int i = 0; i < oMap.LayerCount; i++)
            {

                GetLayerfromGroupLayers(oMap.get_Layer(i), ref OutLayer, sName);

                if (OutLayer != null)
                {
                    break;
                }
            }

            if (OutLayer != null)
                return OutLayer;
            else
                return null;
        }

        public static void GetLayerfromGroupLayers(ILayer pLayer, ref ILayer outLayer, string layName)
        {
            if (pLayer is ICompositeLayer)//如果是组合图层
            {
                ICompositeLayer groupLayer = (ICompositeLayer)pLayer;

                for (int j = 0; j < groupLayer.Count; j++)
                {	//递归
                    GetLayerfromGroupLayers(groupLayer.get_Layer(j), ref outLayer, layName);
                }
            }
            else if (pLayer is IFeatureLayer)//如果是地理要素图层
            {
                if (pLayer.Name.ToUpper() == layName.ToUpper() || pLayer.Name.ToUpper() == layName.ToUpper() + "_2D")
                {
                    outLayer = pLayer;
                    return;
                }
            }
        }
        #endregion

        #region//由图层名称查找工作空间
        /// <summary>
        /// 由图层名称查找工作空间
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IWorkspace GetLayerWorkspace(ILayer pLayer)
        {
            if (pLayer == null) return null;

            IFeatureLayer pFLayer;
            IFeatureClass pFClass;
            IDataset pDataset;

            pFLayer = (IFeatureLayer)pLayer;
            pFClass = pFLayer.FeatureClass;
            pDataset = (IDataset)pFClass;

            if (pDataset == null)
            {
                return null;
            }
            else
            {
                return pDataset.Workspace;
            }
        }

        #endregion

        #region//删除地理要素
        /// <summary>
        /// 删除地理要素
        /// </summary>
        /// <param name="pMap"></param>
        public static void DeleteFeature(IMap pMap)
        {
            IActiveView pActiveView;
            IFeatureCursor pFCursor;
            IFeature pFeatrue;
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;

            pGeoFeatureLayers = GeoFeatureLayers(pMap);

            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;

            pWorkspaceEdit.StartEditOperation();

            for (int i = 0; i < pGeoFeatureLayers.Count; i++)
            {
                pFCursor = GetSelectedFeatures((ILayer)pGeoFeatureLayers.get_Element(i));

                if (pFCursor == null) continue;

                pFeatrue = pFCursor.NextFeature();
                while (pFeatrue != null)
                {
                    pFeatrue.Delete();
                    pFeatrue = pFCursor.NextFeature();
                }
            }

            pWorkspaceEdit.StopEditOperation();

            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();
        }
        #endregion

//         #region//删除地理要素
//         /// <summary>
//         /// 删除地理要素
//         /// </summary>
//         /// <param name="pMap"></param>
//         public static void DeleteFeature_0(IWorkbench pWorkbench, IMap pMap)
//         {
//             IActiveView pActiveView;
//             IFeatureCursor pFCursor;
//             IFeature pFeatrue;
//             IWorkspaceEdit pWorkspaceEdit;
//             IArray pGeoFeatureLayers;
// 
//             pGeoFeatureLayers = GeoFeatureLayers_0(pMap);
// 
//             if (pGeoFeatureLayers.Count == 0) return;
// 
//             pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
//             if (pWorkspaceEdit == null) return;
//             if (!pWorkspaceEdit.IsBeingEdited()) return;
// 
//             pWorkspaceEdit.StartEditOperation();
// 
//             for (int i = 0; i < pGeoFeatureLayers.Count; i++)
//             {
//                 pFCursor = GetSelectedFeatures((ILayer)pGeoFeatureLayers.get_Element(i));
// 
//                 if (pFCursor == null) continue;
// 
//                 pFeatrue = pFCursor.NextFeature();
//                 while (pFeatrue != null)
//                 {
//                     pFeatrue.Delete();
//                     pFeatrue = pFCursor.NextFeature();
//                 }
// 
//             }
// 
//             pWorkbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;
// 
//             pWorkspaceEdit.StopEditOperation();
// 
//             pActiveView = (IActiveView)pMap;
//             pActiveView.Refresh();
//         }
//         #endregion

        #region//删除原修测地理要素
        /// <summary>
        /// 删除地理要素
        /// </summary>
        /// <param name="pMap"></param>
        public static void DeleteFeature_1(IMap pMap)
        {
            IActiveView pActiveView;
            IFeatureCursor pFCursor;
            IFeature pFeatrue;
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;

            pGeoFeatureLayers = GeoFeatureLayers_0(pMap);

            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
            if (pWorkspaceEdit == null) return;
            //if (!pWorkspaceEdit.IsBeingEdited()) return;

            pWorkspaceEdit.StartEditOperation();

            for (int i = 0; i < pGeoFeatureLayers.Count; i++)
            {
                pFCursor = GetSelectedFeatures((ILayer)pGeoFeatureLayers.get_Element(i));

                if (pFCursor == null) continue;

                pFeatrue = pFCursor.NextFeature();
                while (pFeatrue != null)
                {
                    pFeatrue.Delete();
                    pFeatrue = pFCursor.NextFeature();
                }

            }

            //pWorkbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;

            pWorkspaceEdit.StopEditOperation();

            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();
        }
        #endregion

        #region//删除选中的要素
        /// <summary>
        /// 剪切要素
        /// </summary>
        /// <param name="pMapControl"></param>
        public static void DeleteFeature(IMapControl2 pMapControl, IFeature pFeature)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IMap pMap;

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;
            if (pFeature == null) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            //删除要素
            pFeature.Delete();

            pWorkspaceEdit.StopEditOperation();

            CommonFunction.MapRefresh(pMapControl.ActiveView);

        }
        #endregion

//         #region//删除选中的要素
//         /// <summary>
//         /// 剪切要素
//         /// </summary>
//         /// <param name="pMapControl"></param>
//         public static void DeleteFeature(IWorkbench pWorkbench, IMapControl2 pMapControl, IFeature pFeature)
//         {
//             IWorkspaceEdit pWorkspaceEdit;
//             IArray pGeoFeatureLayers;
//             IMap pMap;
// 
//             if (pMapControl == null) return;
//             if (pMapControl.Map == null) return;
//             if (pFeature == null) return;
// 
//             pMap = pMapControl.Map;
//             pGeoFeatureLayers = GeoFeatureLayers(pMap);
//             if (pGeoFeatureLayers.Count == 0) return;
// 
//             pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
//             if (pWorkspaceEdit == null) return;
//             if (!pWorkspaceEdit.IsBeingEdited()) return;
//             pWorkspaceEdit.StartEditOperation();
// 
//             //删除要素
//             pFeature.Delete();
// 
//             pWorkbench.CommandBarManager.Tools["2dmap.DFEditorTool.Redo"].SharedProps.Enabled = true;
// 
//             pWorkspaceEdit.StopEditOperation();
// 
//             CommonFunction.MapRefresh(pMapControl.ActiveView);
// 
//         }
//         #endregion

        #region//删除存入数组中的要素
        /// <summary>
        /// 剪切要素
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pFeatureArray"></param>

        public static void DelFeaturesFromArray(IMapControl2 pMapControl, ref IArray pFeatureArray)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IFeature pFeature;//IFeature指单个图形对象
            IMap pMap;

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;
            if (pFeatureArray.Count == 0) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            //删除要素
            for (int i = 0; i < pFeatureArray.Count; i++)
            {
                pFeature = (IFeature)pFeatureArray.get_Element(i);
                pFeature.Delete();
            }

            pWorkspaceEdit.StopEditOperation();

            pFeatureArray.RemoveAll();//清空选择数组

            m_pFeatureLayerArray.RemoveAll();//清空选择数组

            //			CommonFunction.MapRefresh(pMapControl.ActiveView); 			

        }
        #endregion

        #region//获取图层选择要素的游标
        /// <summary>
        /// 获取图层选择要素的游标
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        private static IFeatureCursor GetSelectedFeatures(ILayer pLayer)
        {
            ICursor pCursor;
            IFeatureSelection pFSelection;
            ISelectionSet pFSet;

            pFSelection = (IFeatureSelection)pLayer;
            pFSet = pFSelection.SelectionSet;

            if (pFSet.Count > 0)
            {
                pFSet.Search(null, false, out pCursor);
                return (IFeatureCursor)pCursor;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region//将可编辑的地理要素层存入数组中
        /// <summary>
        /// 将可编辑的地理要素层存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GeoFeatureLayers(IMap pMap)
        {
            ILayer pLayer;
            IEnumLayer pLayers;

            if (pMap.LayerCount > 0)
            {
                pLayers = pMap.get_Layers(null, false);
                pLayers.Reset();
                pLayer = pLayers.Next();
                while (pLayer != null)
                {
                    AddLayersTom_FeatureLayerArray(pLayer);
                    pLayer = pLayers.Next();
                }
            }
            return m_pFeatureLayerArray;

        }
        #endregion

        #region//将可编辑的地理要素层存入数组中
        /// <summary>
        /// 将可编辑的地理要素层存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GeoFeatureLayers_0(IMap pMap)
        {
            ILayer pLayer;
            IEnumLayer pLayers;

            if (pMap.LayerCount > 0)
            {
                pLayers = pMap.get_Layers(null, false);
                pLayers.Reset();
                pLayer = pLayers.Next();
                while (pLayer != null)
                {
                    AddLayersTom_FeatureLayerArray_0(pLayer);
                    pLayer = pLayers.Next();
                }
            }
            return m_pFeatureLayerArray;

        }

        private static void AddLayersTom_FeatureLayerArray_0(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;

            if (pLayer is IGroupLayer)//如果是组合图层
            {
                ICompositeLayer groupLayer = (ICompositeLayer)pLayer;

                for (int j = 0; j < groupLayer.Count; j++)
                {
                    //递归
                    AddLayersTom_FeatureLayerArray_0(groupLayer.get_Layer(j));
                }
            }
            else if (pLayer is IFeatureLayer)//如果是地理要素图层
            {
                if (pLayer.Visible == true)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            m_pFeatureLayerArray.Add(pLayer);
                        }
                    }
                }
            }
        }
        #endregion

        # region  //将可以设置数据源的要素层存入数组中
        /// <summary>
        /// 将可以设置数据源的要素层存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GetFeatureLayers_1(IMap pMap)
        {
            ILayer pLayer;
            IEnumLayer pLayers;

            if (pMap.LayerCount > 0)
            {
                pLayers = pMap.get_Layers(null, false);
                pLayers.Reset();
                pLayer = pLayers.Next();
                while (pLayer != null)
                {
                    AddLayersTom_FeatureLayerArray_1(pLayer);
                    pLayer = pLayers.Next();
                }
            }
            return m_pFeatureLayerArray;

        }

        private static void AddLayersTom_FeatureLayerArray_1(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer;

            if (pLayer is IGroupLayer)//如果是组合图层
            {
                ICompositeLayer groupLayer = (ICompositeLayer)pLayer;

                for (int j = 0; j < groupLayer.Count; j++)
                {
                    //递归
                    AddLayersTom_FeatureLayerArray_1(groupLayer.get_Layer(j));
                }
            }
            else if (pLayer is IFeatureLayer)//如果是地理要素图层
            {
                if (pLayer.Visible == true)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;

                    //Console.WriteLine(pFeatureLayer.Name);

                    m_pFeatureLayerArray.Add(pLayer);

                }
            }
        }


        private static void AddLayersTom_FeatureLayerArray(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;

            if (pLayer is ICompositeLayer)//如果是组合图层
            {
                ICompositeLayer groupLayer = (ICompositeLayer)pLayer;

                for (int j = 0; j < groupLayer.Count; j++)
                {
                    //递归
                    AddLayersTom_FeatureLayerArray(groupLayer.get_Layer(j));
                }
            }
            else if (pLayer is IFeatureLayer)//如果是地理要素图层
            {
                if (pLayer.Visible == true)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            m_pFeatureLayerArray.Add(pLayer);
                        }
                    }
                }
            }
        }

        #endregion

        #region //构造存放Property信息的Datatable
        /// <summary>
        /// 构造存放Property信息的Datatable
        /// </summary>
        /// <returns></returns>
        private DataTable CreateNewProTable()
        {
            DataTable dt = new DataTable();
            DataColumn column;

            column = new DataColumn();
            column.ColumnName = "字段名";
            column.DataType = typeof(System.String);
            column.MaxLength = 100;
            dt.Columns.Add(column);
            dt.PrimaryKey = new DataColumn[] { column };

            column = new DataColumn();
            column.ColumnName = "字段值";
            column.DataType = typeof(System.String);
            column.MaxLength = 250;
            dt.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "能否编辑";
            column.DataType = typeof(System.Boolean);
            dt.Columns.Add(column);
            return dt;
        }
        #endregion

        #region //通过图层名称得到图层序号
        /// <summary>
        /// 通过图层名称得到图层序号
        /// </summary>
        /// <param name="pMap">Map</param>
        /// <param name="LayerName">图层名称</param>
        /// <returns>图层序号</returns>
        public static int GetLayerIndex(IMap pMap, string LayerName)
        {
            int i = -1;

            for (int LayerIndex = 0; LayerIndex < pMap.LayerCount; LayerIndex++)
            {
                if (pMap.get_Layer(LayerIndex).Name == LayerName)
                {
                    return LayerIndex;
                }
            }
            return i;
        }
        #endregion

        #region //得到选中要素的属性信息Property
        /// <summary>
        /// 得到一个要素的属性信息
        /// </summary>
        /// <param name="LayerName">   </param>
        /// <param name="oid"> </param>
        private void GetProInfo(IMap pMap, string LayerName, int oid)
        {
            //oid 为要素OID
            IFeatureLayer pFeaLay;
            IFeatureClass pFeaCls;
            ITable pTable;
            IRow pRow;
            IField pField;

            DataTable dtProperty = new DataTable();
            DataRow dr;
            IGeometryCollection pGeoColl;

            esriFieldType type;
            string FldValue;

            int i;
            int layerIndex = GetLayerIndex(pMap, LayerName);

            dtProperty.Rows.Clear();
            if (!(pMap.get_Layer(layerIndex) is IFeatureLayer)) return;
            pFeaLay = (IFeatureLayer)pMap.get_Layer(layerIndex); ;
            pFeaCls = pFeaLay.FeatureClass;

            pTable = (ITable)pFeaCls;
            pRow = pTable.GetRow(oid);
            for (i = 0; i < pRow.Fields.FieldCount; i++)
            {
                pField = pRow.Fields.get_Field(i);
                dr = dtProperty.NewRow();
                dr[0] = pField.Name;
                dr[2] = pField.Editable;
                type = pField.Type;
                if (pRow.get_Value(i) == null)
                {
                    FldValue = "<空>";
                }
                else
                {
                    switch (type)
                    {
                        case esriFieldType.esriFieldTypeGeometry:
                            //取Geometry字段的值，得到一组点信息，写入dtGeometry；
                            try
                            {
                                IGeometry p = (IGeometry)pRow.get_Value(i);
                                if ((p.GeometryType != esriGeometryType.esriGeometryPoint))// && (p.GeometryType != esriGeometryType.esriGeometryMultipoint))
                                {
                                    pGeoColl = (IGeometryCollection)p;
                                }
                                else
                                {
                                    object a = Type.Missing;
                                    pGeoColl = new MultipointClass();
                                    pGeoColl.AddGeometry((IPoint)p, ref a, ref a);
                                }
                                //	GetGeoInfo();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\r\n" + "字段值转换成Geometry失败！");
                            }
                            FldValue = "<几何数据>";
                            dr[2] = false;
                            break;
                        case esriFieldType.esriFieldTypeBlob:
                            FldValue = "<二进制数据>";
                            dr[2] = false;
                            break;
                        case esriFieldType.esriFieldTypeRaster:
                            FldValue = "<栅格数据>";
                            dr[2] = false;
                            break;
                        default:
                            FldValue = pRow.get_Value(i).ToString();
                            //如果字段设置了域domain，用row来取值时get_Value得到的时domain的name，
                            //但是用Feature来get_value时需要循环比较domain的value，取domain的name
                            break;
                    }
                    IDomain pDomain = pRow.Fields.get_Field(i).Domain;
                    if (pDomain != null && (pDomain.Type == esriDomainType.esriDTCodedValue))
                    {
                        ICodedValueDomain pCD = (ICodedValueDomain)pDomain;
                        for (int j = 0; j < pCD.CodeCount; j++)
                        {
                            if (object.Equals(pCD.get_Value(j), pRow.get_Value(i)))
                            {
                                FldValue = pCD.get_Name(j);
                                break;
                            }
                        }
                    }
                }
                dr[1] = FldValue;
                dtProperty.Rows.Add(dr);
            }
        }
        #endregion

        #region//闪烁一个要素
        /// <summary>
        /// 闪烁一个要素
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pActiveView"></param>
        /// <param name="pFeature"></param>
        public static void FlashFeature(IMapControl2 pMapControl, IActiveView pActiveView, IFeature pFeature)
        {
            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbol();
            IMarkerSymbol pMarkSymbol = new SimpleMarkerSymbolClass();
            ISimpleLineSymbol pLineSybol = new SimpleLineSymbolClass();
            ISymbol pSymbol;
            IRgbColor pRGBColor;
            pRGBColor = new RgbColor();
            pRGBColor.Red = 255;

            int pNTime;
            int pTime;

            pNTime = 1;
            pTime = 10;

            if (pFeature == null) return;

            switch (pFeature.Shape.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pMarkSymbol.Size = 10;
                    pMarkSymbol.Color = pRGBColor;
                    pSymbol = (ISymbol)pMarkSymbol;
                    for (int i = 0; i <= pNTime; i++)
                    {
                        pMapControl.FlashShape(pFeature.Shape, pNTime, pTime, pSymbol);
                    }
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    pLineSybol.Width = 10;
                    pLineSybol.Color = pRGBColor;
                    pSymbol = (ISymbol)pLineSybol;
                    for (int i = 0; i <= pNTime; i++)
                    {
                        pMapControl.FlashShape(pFeature.Shape, pNTime, pTime, pSymbol);
                    }
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    pFillSymbol.Outline = null;
                    pFillSymbol.Color = pRGBColor;
                    pSymbol = (ISymbol)pFillSymbol;
                    for (int i = 0; i <= pNTime; i++)
                    {
                        pMapControl.FlashShape(pFeature.Shape, pNTime, pTime, pSymbol);
                        System.Threading.Thread.Sleep(pTime);
                    }
                    break;
            }
            //pDisplay.RemoveAllCaches();
        }
        #endregion

        #region//闪烁一个要素
        /// <summary>
        /// 闪烁一个要素
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pActiveView"></param>
        /// <param name="pFeature"></param>
        public static void FlashFeature(IMapControl2 pMapControl, IActiveView pActiveView, IGeometry pGeometry)
        {
            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbol();
            IMarkerSymbol pMarkSymbol = new SimpleMarkerSymbolClass();
            ISimpleLineSymbol pLineSybol = new SimpleLineSymbolClass();
            ISymbol pSymbol;
            IRgbColor pRGBColor;
            pRGBColor = new RgbColor();
            pRGBColor.Red = 255;

            int pNTime;
            int pTime;

            pNTime = 1;
            pTime = 10;

            if (pGeometry == null) return;

            switch (pGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pMarkSymbol.Size = 10;
                    pMarkSymbol.Color = pRGBColor;
                    pSymbol = (ISymbol)pMarkSymbol;
                    for (int i = 0; i <= pNTime; i++)
                    {
                        pMapControl.FlashShape(pGeometry, pNTime, pTime, pSymbol);
                    }
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    pLineSybol.Width = 10;
                    pLineSybol.Color = pRGBColor;
                    pSymbol = (ISymbol)pLineSybol;
                    for (int i = 0; i <= pNTime; i++)
                    {
                        pMapControl.FlashShape(pGeometry, pNTime, pTime, pSymbol);
                    }
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    pFillSymbol.Outline = null;
                    pFillSymbol.Color = pRGBColor;
                    pSymbol = (ISymbol)pFillSymbol;
                    for (int i = 0; i <= pNTime; i++)
                    {
                        pMapControl.FlashShape(pGeometry, pNTime, pTime, pSymbol);
                        System.Threading.Thread.Sleep(pTime);
                    }
                    break;
            }
            //pDisplay.RemoveAllCaches();
        }
        #endregion

        #region//从选择集合中选择一个地理要素：点、线、面、注记
        /// <summary>
        /// 从选择集合中选择一个地理要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IFeature GetJustOneFeatureFromFeatureSelection(IMap pMap, out ILayer pLayer, IPoint pPoint)
        {
            double tempDist;
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IFeature pTestFeature;
            IProximityOperator pProximity;
            IGeometry pGeom;

            pLayer = null;

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空

            if (pFeature == null) return null;

            pProximity = (IProximityOperator)pPoint;
            pTestFeature = pFeature;

            pGeom = pTestFeature.ShapeCopy;
            double testDist = pProximity.ReturnDistance(pGeom);

            pSelected.Reset();
            pTestFeature = pSelected.Next();
            while (pTestFeature != null)
            {
                pGeom = pTestFeature.Shape;
                tempDist = pProximity.ReturnDistance(pGeom);

                if (tempDist <= testDist)
                {
                    testDist = tempDist;
                    pFeature = pTestFeature;
                }

                pTestFeature = pSelected.Next();
            }

            if (pFeature != null)
            {
                pLayer = GetLayerByNameFrom_Map(pMap, pFeature.Class.AliasName);
                return pFeature;
            }
            else
            {
                pLayer = null;
                return null;
            }

        }
        #endregion

        #region//从选择集合中选择一个地理要素：面
        /// <summary>
        /// 从选择集合中选择一个地理要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IFeature GetJustOneFeatureFromFeatureSelection_2(IMap pMap, out ILayer pLayer, IPoint pPoint)
        {
            double tempDist;
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IFeature pTestFeature;
            IProximityOperator pProximity;
            IGeometry pGeom;

            pLayer = null;

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判断

            if (pFeature == null) return null;
            if (pFeature.Shape.GeometryType != esriGeometryType.esriGeometryPolygon)
            {
                pMap.FeatureSelection.Clear();
                return null;
            }

            pProximity = (IProximityOperator)pPoint;
            pTestFeature = pFeature;

            pGeom = pTestFeature.ShapeCopy;
            double testDist = pProximity.ReturnDistance(pGeom);

            pSelected.Reset();
            pTestFeature = pSelected.Next();
            while (pTestFeature != null)
            {
                pGeom = pTestFeature.Shape;
                tempDist = pProximity.ReturnDistance(pGeom);

                if (tempDist <= testDist)
                {
                    testDist = tempDist;
                    pFeature = pTestFeature;
                }

                pTestFeature = pSelected.Next();
            }

            if (pFeature != null)
            {
                pLayer = GetLayerByNameFrom_Map(pMap, pFeature.Class.AliasName);
                return pFeature;
            }
            else
            {
                pLayer = null;
                return null;
            }
        }

        #endregion

        #region//从选择集合中选择一个地理要素: 线或面
        /// <summary>
        /// 从选择集合中选择一个地理要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IFeature GetJustOneFeatureFromFeatureSelection_3(IMap pMap, out ILayer pLayer, IPoint pPoint)
        {
            double tempDist;
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IFeature pTestFeature;
            IProximityOperator pProximity;
            IGeometry pGeom;

            pLayer = null;

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空

            if (pFeature == null) return null;
            if (pFeature.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                pMap.FeatureSelection.Clear();
                return null;
            }
            if (pFeature.Shape.GeometryType != esriGeometryType.esriGeometryPolygon && pFeature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
            {
                pMap.FeatureSelection.Clear();
                return null;
            }

            pProximity = (IProximityOperator)pPoint;
            pTestFeature = pFeature;

            pGeom = pTestFeature.ShapeCopy;
            double testDist = pProximity.ReturnDistance(pGeom);

            pSelected.Reset();
            pTestFeature = pSelected.Next();
            while (pTestFeature != null)
            {
                pGeom = pTestFeature.Shape;
                tempDist = pProximity.ReturnDistance(pGeom);

                if (tempDist <= testDist)
                {
                    testDist = tempDist;
                    pFeature = pTestFeature;
                }

                pTestFeature = pSelected.Next();
            }

            if (pFeature != null)
            {
                pLayer = GetLayerByNameFrom_Map(pMap, pFeature.Class.AliasName);
                return pFeature;
            }
            else
            {
                pLayer = null;
                return null;
            }

        }
        #endregion

        #region//从选择集合中选择一个地理要素：线
        /// <summary>
        /// 从选择集合中选择一个地理要素
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IFeature GetJustOneFeatureFromFeatureSelection_4(IMap pMap, out ILayer pLayer, IPoint pPoint)
        {
            double tempDist;
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IFeature pTestFeature;
            IProximityOperator pProximity;
            IGeometry pGeom;

            pLayer = null;

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空

            if (pFeature == null) return null;
            if (pFeature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
            {
                pMap.FeatureSelection.Clear();
                return null;
            }

            pProximity = (IProximityOperator)pPoint;
            pTestFeature = pFeature;

            pGeom = pTestFeature.ShapeCopy;
            double testDist = pProximity.ReturnDistance(pGeom);

            pSelected.Reset();
            pTestFeature = pSelected.Next();
            while (pTestFeature != null)
            {
                pGeom = pTestFeature.Shape;
                tempDist = pProximity.ReturnDistance(pGeom);

                if (tempDist <= testDist)
                {
                    testDist = tempDist;
                    pFeature = pTestFeature;
                }

                pTestFeature = pSelected.Next();
            }

            if (pFeature != null)
            {
                pLayer = GetLayerByNameFrom_Map(pMap, pFeature.Class.AliasName);
                return pFeature;
            }
            else
            {
                pLayer = null;
                return null;
            }
        }

        #endregion

        #region //高亮显示选中的要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeature(IMapControl2 pMapControl, IFeature pFeature)
        {
            pMapControl.ActiveView.FocusMap.ClearSelection();//清空地图选择的要素
            pMapControl.ActiveView.GraphicsContainer.DeleteAllElements();

            //分点、线、面，给新的symbol

            if (pFeature == null) return;

            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new MarkerElementClass();
                pElement.Geometry = pFeature.Shape;

                IMarkerElement pMarkerElement;
                pMarkerElement = (IMarkerElement)pElement;
                pMarkerElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new LineElementClass();
                pElement.Geometry = pFeature.Shape;

                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol = (IFillSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new PolygonElementClass();
                pElement.Geometry = pFeature.Shape;

                IFillShapeElement pFillElement;
                pFillElement = (IFillShapeElement)pElement;
                pFillElement.Symbol = (IFillSymbol)pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);
            }

            m_SelectArray.RemoveAll();
            m_SelectArray.Add(pFeature);//将要素添加到选择数组中

            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pMapControl.ActiveView.Extent);

        }
        #endregion

        #region //高亮显示选中的要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeature_2(IMapControl2 pMapControl, IFeature pFeature)
        {
            pMapControl.ActiveView.FocusMap.ClearSelection();//清空地图选择的要素

            //分点、线、面，给新的symbol

            if (pFeature == null) return;

            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new MarkerElementClass();
                pElement.Geometry = pFeature.Shape;

                IMarkerElement pMarkerElement;
                pMarkerElement = (IMarkerElement)pElement;
                pMarkerElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new LineElementClass();
                pElement.Geometry = pFeature.Shape;

                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol = (IFillSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new PolygonElementClass();
                pElement.Geometry = pFeature.Shape;

                IFillShapeElement pFillElement;
                pFillElement = (IFillShapeElement)pElement;
                pFillElement.Symbol = (IFillSymbol)pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);
            }

            m_SelectArray.RemoveAll();
            m_SelectArray.Add(pFeature);//将要素添加到选择数组中

            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pMapControl.ActiveView.Extent);

        }
        #endregion

        #region //高亮显示存入数组中的要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeatureArray(IMapControl2 pMapControl, IArray pFeatureArray)
        {
            //分点、线、面，给新的symbol


            IFeature pFeature;

            m_SelectArray.RemoveAll();//清空选择数组

            for (int index = 0; index < pFeatureArray.Count; index++)
            {
                pFeature = (IFeature)pFeatureArray.get_Element(index);
                if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                    pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                    IElement pElement = new MarkerElementClass();
                    pElement.Geometry = pFeature.Shape;

                    IMarkerElement pMarkerElement;
                    pMarkerElement = (IMarkerElement)pElement;
                    pMarkerElement.Symbol = pSymbol;

                    pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, index);

                }
                else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    ILineSymbol pSymbol = new SimpleLineSymbolClass();
                    pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                    IElement pElement = new LineElementClass();
                    pElement.Geometry = pFeature.Shape;

                    ILineElement pLineElement;
                    pLineElement = (ILineElement)pElement;
                    pLineElement.Symbol = pSymbol;

                    pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, index);

                }
                else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    IElement pElement = new LineElementClass();
                    IPolyline pLine;
                    pLine = GetPolygonBoundary((IPolygon)pFeature.Shape);
                    pElement.Geometry = pLine;

                    ILineSymbol pSymbol = new SimpleLineSymbolClass();
                    pSymbol = (ILineSymbol)GetDefaultSymbol(pLine.GeometryType);

                    ILineElement pLineElement;
                    pLineElement = (ILineElement)pElement;
                    pLineElement.Symbol = pSymbol;

                    pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, index);
                }

                m_SelectArray.Add(pFeature);//将要素添加到选择数组中	

            }

            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pMapControl.ActiveView.Extent);//视图刷新
        }
        #endregion

        #region //高亮显示存入数组中的要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static IGroupElement ShowSelectionFeatureArray_2(IMapControl2 pMapControl, IArray pFeatureArray)
        {
            //分点、线、面，给新的symbol
            IFeature pFeature;
            IGroupElement pGroupElement = new GroupElementClass();

            m_SelectArray.RemoveAll();//清空选择数组

            for (int index = 0; index < pFeatureArray.Count; index++)
            {
                pFeature = (IFeature)pFeatureArray.get_Element(index);
                if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                    pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                    IElement pElement = new MarkerElementClass();
                    pElement.Geometry = pFeature.Shape;

                    IMarkerElement pMarkerElement;
                    pMarkerElement = (IMarkerElement)pElement;
                    pMarkerElement.Symbol = pSymbol;

                    pGroupElement.AddElement(pElement);

                }
                else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    ILineSymbol pSymbol = new SimpleLineSymbolClass();
                    pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                    IElement pElement = new LineElementClass();
                    pElement.Geometry = pFeature.Shape;

                    ILineElement pLineElement;
                    pLineElement = (ILineElement)pElement;
                    pLineElement.Symbol = pSymbol;

                    pGroupElement.AddElement(pElement);
                }
                else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    IElement pElement = new LineElementClass();
                    IPolyline pLine;
                    pLine = GetPolygonBoundary((IPolygon)pFeature.Shape);
                    pElement.Geometry = pLine;

                    ILineSymbol pSymbol = new SimpleLineSymbolClass();
                    pSymbol = (ILineSymbol)GetDefaultSymbol(pLine.GeometryType);

                    ILineElement pLineElement;
                    pLineElement = (ILineElement)pElement;
                    pLineElement.Symbol = pSymbol;

                    pGroupElement.AddElement(pElement);
                }

                m_SelectArray.Add(pFeature);//将要素添加到选择数组中		

            }

            pMapControl.ActiveView.Refresh();

            return pGroupElement;

        }
        #endregion

        #region //根据Geometry类型生成一个默认符号
        /// <summary>
        /// 根据Geometry类型生成一个默认符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISymbol GetDefaultSymbol(esriGeometryType type)
        {
            ISymbol sym = null;

            IRgbColor mCor = new RgbColorClass();
            mCor.Red = 0;
            mCor.Blue = 255;
            mCor.Green = 255;

            IRgbColor lCor = new RgbColorClass();
            mCor.Red = 0;
            lCor.Blue = 255;
            mCor.Green = 255;

            IRgbColor fCor = new RgbColorClass();
            mCor.Red = 0;
            fCor.Blue = 255;
            mCor.Green = 255;

            IMarkerSymbol mark = new SimpleMarkerSymbolClass();
            mark.Color = mCor;
            mark.Size = 8;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 1.5;
            line.Color = lCor;

            IFillSymbol fill = new SimpleFillSymbolClass();
            fill.Color = fCor;
            fill.Outline = line;

            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    sym = (ISymbol)mark;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    sym = (ISymbol)line;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    sym = (ISymbol)fill;
                    //sym = (ISymbol)line;
                    break;
            }

            return sym;
        }
        #endregion

        #region //得到一个Polygon对象的轮廓线
        /// <summary>
        /// 得到一个Polygon对象的轮廓线
        /// </summary>
        /// <param name="pPolygon">一个Polygon对象</param>
        /// <returns>一个Polyline对象</returns>
        public static IPolyline GetPolygonBoundary(IPolygon pPolygon)
        {
            //通过ITopologicalOperator 对象转换成线
            ITopologicalOperator pTopo;
            IPolyline pPolyline;

            pTopo = (ITopologicalOperator)pPolygon;
            pPolyline = (IPolyline)pTopo.Boundary;

            return pPolyline;


        }
        #endregion

//         #region//属性匹配
//         /// <summary>
//         /// 属性匹配
//         /// </summary>
//         /// <param name="pSourceFeature"></param>
//         /// <param name="objectFeatureArray"></param>
//         /// <param name="pMapControl"></param>
//         /// <param name="pCurrentLayer"></param>
//         public static void MatchProperty(IWorkbench pWorkbench, IFeature pSourceFeature, IArray objectFeatureArray, IMapControl2 pMapControl, IFeatureLayer pCurrentLayer)
//         {
//             IWorkspaceEdit pWorkSpaceEdit;
//             IFeature pObjectFeature;
// 
//             pWorkSpaceEdit = (IWorkspaceEdit)GetLayerWorkspace(pCurrentLayer);
// 
//             if (pWorkSpaceEdit != null)
//             {
//                 pWorkSpaceEdit.StartEditOperation();
// 
//                 for (int i = 0; i < objectFeatureArray.Count; i++)
//                 {
//                     pObjectFeature = (IFeature)objectFeatureArray.get_Element(i);
// 
//                     CopyProperty_FromSourceFeatureToObjectFeature(pSourceFeature, pObjectFeature);
// 
//                     pObjectFeature.Store();
// 
//                     FlashFeature(pMapControl, pMapControl.ActiveView, pObjectFeature);//闪烁目标地理要素
// 
//                 }
// 
//                 pWorkbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;
// 
//                 pWorkSpaceEdit.StopEditOperation();
//             }
// 
//         }
//         #endregion

        #region//拷贝属性
        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="pSourceFeature"></param>
        /// <param name="pObjectFeature"></param>
        public static void CopyProperty_FromSourceFeatureToObjectFeature(IFeature pSourceFeature, IFeature pObjectFeature)
        {//从一个要素到另一个要素：将目标要素的属性字段逐一和源要素的字段比较，若字段名称和类型相同，则将源要素的属性拷贝到目标要素，否则保留目标要素的字段属性
            for (int i = 0; i < pObjectFeature.Fields.FieldCount; i++)
            {
                if (pObjectFeature.Fields.get_Field(i).Type != esriFieldType.esriFieldTypeOID)
                {
                    string FieldName = pObjectFeature.Fields.get_Field(i).Name.ToUpper();

                    if (FieldName != "SHAPE" && FieldName != "SHAPE_AREA" && FieldName != "SHAPE_LENGTH")
                    {
                        if (pObjectFeature.Fields.get_Field(i).Editable == true)//目标要素的字段可编辑
                        {
                            for (int j = 0; j < pSourceFeature.Fields.FieldCount; j++)
                            {
                                if ((pSourceFeature.Fields.get_Field(j).Name.ToUpper() == pObjectFeature.Fields.get_Field(i).Name.ToUpper()) &&
                                    (pSourceFeature.Fields.get_Field(j).Type == pObjectFeature.Fields.get_Field(i).Type)) //名称和类型一致
                                {
                                    pObjectFeature.set_Value(i, pSourceFeature.get_Value(j));
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region//拷贝属性
        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="featureBuffer"></param>
        /// <param name="pSourceFeature"></param>
        public static void AddFields_FromSourceFeatureToObjectFeature(IFeatureBuffer featureBuffer, IFeature pSourceFeature)
        {//创建要素缓冲：将目标要素的属性字段逐一和源要素的字段比较，若字段名称和类型相同，则将源要素的属性拷贝到目标要素，否则保留目标要素的字段属性
            IRowBuffer rowBuffer = (IRowBuffer)featureBuffer;
            IFields fieldsNew = rowBuffer.Fields;

            IFields fields = featureBuffer.Fields;
            for (int i = 0; i <= fields.FieldCount - 1; i++)
            {
                IField field = fields.get_Field(i);
                if (field.Editable == true)//目标要素的字段可编辑
                {
                    if (field.Type != esriFieldType.esriFieldTypeOID)
                    {
                        string FieldName = field.Name.ToUpper();

                        if (FieldName != "SHAPE" && FieldName != "SHAPE_AREA" && FieldName != "SHAPE_LENGTH")
                        {
                            for (int j = 0; j < pSourceFeature.Fields.FieldCount; j++)
                            {
                                if ((pSourceFeature.Fields.get_Field(j).Name.ToUpper() == featureBuffer.Fields.get_Field(i).Name.ToUpper()) &&
                                    (pSourceFeature.Fields.get_Field(j).Type == featureBuffer.Fields.get_Field(i).Type)) //名称和类型一致
                                {
                                    try
                                    {
                                        if (pSourceFeature.get_Value(j) != null)
                                        {
                                            featureBuffer.set_Value(i, pSourceFeature.get_Value(j));
                                        }
                                    }
                                    catch { }
                                }
                            }

                        }

                    }
                }
            }
        }
        #endregion

        #region//根据配置文件参数处理线要素，是取起止点坐标的几何平均值，还是增加一条边来闭合线要素
        /// <summary>
        /// 根据配置文件参数处理线要素，是取起止点坐标的几何平均值，还是增加一条边来闭合线要素
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <param name="bAddLine"></param>
        /// <returns></returns>
        public static IPolyline AdjustPolyline(IPolyline pPolyline, bool bAddLine)
        {
            IPoint pPoint = new PointClass();
            IPoint pFromPoint = new PointClass();
            IPoint pToPoint = new PointClass();

            IPointCollection pPointCollection;
            pPointCollection = (IPointCollection)pPolyline;

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            if (bAddLine)//首尾相连
            {
                pPointCollection.AddPoint(pPointCollection.get_Point(0), ref a, ref b);
            }
            else//取几何平均值
            {
                pFromPoint = pPointCollection.get_Point(0);
                pToPoint = pPointCollection.get_Point(pPointCollection.PointCount - 1);
                pPoint.PutCoords((pFromPoint.X + pToPoint.X) / 2, (pFromPoint.Y + pToPoint.Y) / 2);

                pPointCollection.UpdatePoint(0, pPoint);
                pPointCollection.UpdatePoint(pPointCollection.PointCount - 1, pPoint);

            }

            return (IPolyline)pPointCollection;

        }
        #endregion

        #region//拷贝要素
        /// <summary>
        /// 拷贝要素
        /// </summary>
        /// <param name="pMapControl"></param>
        public static void CopyObjects(IMapControl2 pMapControl)
        {
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IMap pMap;

            m_SelectArray.RemoveAll();//清空选择数组

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;

            pMap = pMapControl.Map;
            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象

            //将选中的要素存入选择数组中
            pSelected.Reset();
            pFeature = pSelected.Next();
            while (pFeature != null)
            {
                m_SelectArray.Add(pFeature);
                pFeature = pSelected.Next();
            }
        }
        #endregion

        #region//剪切要素
        /// <summary>
        /// 剪切要素
        /// </summary>
        /// <param name="pMapControl"></param>
        public static void CutObjects(IMapControl2 pMapControl)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IMap pMap;
            IArray cutObjectsArray = new ArrayClass();

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            m_SelectArray.RemoveAll();//清空选择数组

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象

            //将选中的要素存入选择数组和删除数组中
            pSelected.Reset();
            pFeature = pSelected.Next();
            while (pFeature != null)
            {
                cutObjectsArray.Add(pFeature);
                m_SelectArray.Add(pFeature);
                pFeature = pSelected.Next();
            }

            //删除要素
            for (int i = 0; i < cutObjectsArray.Count; i++)
            {
                pFeature = (IFeature)cutObjectsArray.get_Element(i);
                pFeature.Delete();
            }

            pWorkspaceEdit.StopEditOperation();

            pMapControl.ActiveView.Refresh();

        }
        #endregion

        #region//粘贴要素
        /// <summary>
        /// 粘贴要素
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pFeatureLayer"></param>
        /// <param name="bPolylinePolygonChange"></param>
        /// <param name="dblpolylineToPolygonTolerence"></param>
        /// <param name="bCopyPorperty"></param>
        public static void PlastObjects(IMapControl2 pMapControl, IFeatureLayer pFeatureLayer,
            bool bPolylinePolygonChange, double dblpolylineToPolygonTolerence, bool bCopyPorperty)
        {
            IWorkspaceEdit pWorkspaceEdit;
            IArray pGeoFeatureLayers;
            IMap pMap;
            IFeature pFeature;
            IPoint pPoint;
            IPolyline pPolyline;
            IPolygon pPolygon;

            m_OriginArray.RemoveAll();//清空源数组

            if (pMapControl == null) return;
            if (pMapControl.Map == null) return;
            if (m_SelectArray.Count == 0) return;

            pMap = pMapControl.Map;
            pGeoFeatureLayers = GeoFeatureLayers(pMap);
            if (pGeoFeatureLayers.Count == 0) return;

            pWorkspaceEdit = (IWorkspaceEdit)GetLayerWorkspace((ILayer)pGeoFeatureLayers.get_Element(0));
            if (pWorkspaceEdit == null) return;
            if (!pWorkspaceEdit.IsBeingEdited()) return;
            pWorkspaceEdit.StartEditOperation();

            //根据目标图层的类型过滤选择的要素，将待转化或拷贝的要素存入源数组中
            for (int i = 0; i < m_SelectArray.Count; i++)
            {
                pFeature = (IFeature)m_SelectArray.get_Element(i);
                if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint && pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {//若为点图层
                    m_OriginArray.Add((IFeature)m_SelectArray.get_Element(i));
                }
                else if ((pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline || pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    && (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline || pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon))
                {//若为线、面图层，要进行线面转化
                    m_OriginArray.Add((IFeature)m_SelectArray.get_Element(i));
                }
            }

            //转化或拷贝要素到目标图层
            for (int i = 0; i < m_OriginArray.Count; i++)
            {
                pFeature = (IFeature)m_OriginArray.get_Element(i);

                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)//粘贴点到点图层
                {
                    pPoint = (IPoint)pFeature.Shape;
                    //					pPoint.Z = Convert.ToDouble( pFeature.get_Value(pFeature.Fields.FindField("Elevation")));
                    //					CreateFeature(pPoint as IGeometry,pMap,pFeatureLayer);

                    IFeatureClass featureClassIn = pFeatureLayer.FeatureClass;
                    IFeatureCursor featureCursorInsert = featureClassIn.Insert(true);

                    IFeatureBuffer featureBufferInsert = featureClassIn.CreateFeatureBuffer();
                    featureBufferInsert.Shape = pPoint;
                    if (bCopyPorperty)
                    {
                        AddFields_FromSourceFeatureToObjectFeature(featureBufferInsert, pFeature);
                    }

                    featureCursorInsert.InsertFeature(featureBufferInsert);
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)//粘贴线和面到线图层
                {
                    if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)//若将要处理要素为线
                    {
                        pPolyline = (IPolyline)pFeature.Shape;
                        InsertFeature(pFeatureLayer, pFeature, pPolyline, bCopyPorperty);
                    }
                    else if ((pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon) && bPolylinePolygonChange)//若将要处理要素为面
                    {
                        pPolyline = GetPolygonBoundary((IPolygon)pFeature.Shape);
                        InsertFeature(pFeatureLayer, pFeature, pPolyline, bCopyPorperty);
                    }
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)//粘贴线和面到面图层
                {
                    if ((pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline) && bPolylinePolygonChange)//若将要处理要素为线
                    {
                        IPoint pFromPoint = ((IPolyline)pFeature.Shape).FromPoint;
                        IPoint pToPoint = ((IPolyline)pFeature.Shape).ToPoint;
                        double Dist = CommonFunction.GetDistance_P12(pFromPoint, pToPoint);
                        if (Dist <= dblpolylineToPolygonTolerence)
                        {
                            CommonFunction.AdjustPolyline((IPolyline)pFeature.Shape, false);
                            pPolygon = CommonFunction.PolylineToPolygon((IPolyline)pFeature.Shape);
                            InsertFeature(pFeatureLayer, pFeature, pPolygon, bCopyPorperty);
                        }
                    }
                    else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)//若将要处理要素为面
                    {
                        pPolygon = (IPolygon)pFeature.Shape;
                        InsertFeature(pFeatureLayer, pFeature, pPolygon, bCopyPorperty);
                    }
                }
            }

            m_OriginArray.RemoveAll();

            pWorkspaceEdit.StopEditOperation();

            pMapControl.ActiveView.Refresh();


        }
        private static void InsertFeature(IFeatureLayer pFeatureLayer, IFeature pFeature, IPolyline pPolyline, bool bCopyPorperty)
        {
            IFeatureClass featureClassIn = pFeatureLayer.FeatureClass;
            IFeatureCursor featureCursorInsert = featureClassIn.Insert(true);

            IFeatureBuffer featureBufferInsert = featureClassIn.CreateFeatureBuffer();
            featureBufferInsert.Shape = pPolyline;
            if (bCopyPorperty)
            {
                AddFields_FromSourceFeatureToObjectFeature(featureBufferInsert, pFeature);
            }

            featureCursorInsert.InsertFeature(featureBufferInsert);
        }

        private static void InsertFeature(IFeatureLayer pFeatureLayer, IFeature pFeature, IPolygon pPolygon, bool bCopyPorperty)
        {
            IFeatureClass featureClassIn = pFeatureLayer.FeatureClass;
            IFeatureCursor featureCursorInsert = featureClassIn.Insert(true);

            IFeatureBuffer featureBufferInsert = featureClassIn.CreateFeatureBuffer();
            featureBufferInsert.Shape = pPolygon;
            if (bCopyPorperty)
            {
                AddFields_FromSourceFeatureToObjectFeature(featureBufferInsert, pFeature);
            }

            featureCursorInsert.InsertFeature(featureBufferInsert);
        }

        #endregion

        #region//创建地理要素
        /// <summary>
        /// 创建地理要素
        /// </summary>
        /// <param name="pGeom"></param>
        /// <param name="pMap"></param>
        /// <param name="pCurrentLayer"></param>
        public static void CreateFeature(IGeometry pGeom, IMap pMap, ILayer pCurrentLayer)
        {
            if (pGeom != null)
            {
                IFeatureLayer pFeatureLayer;
                IDataset pDataset;
                IWorkspaceEdit pWorkspaceEdit;
                ILayer pLayer;

                pLayer = pCurrentLayer;

                if (pLayer == null) return;

                if (!(pLayer is IFeatureLayer)) return;

                pFeatureLayer = (IFeatureLayer)pLayer;
                pDataset = (IDataset)pFeatureLayer.FeatureClass;
                pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;

                int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
                IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
                if (pGD.HasZ)
                {
                    IZAware pZA = (IZAware)pGeom;
                    pZA.ZAware = true;
                    IZ pZ = pGeom as IZ;
                    double zmin = -1000, zmax = 1000;
                    if (pGD.SpatialReference.HasZPrecision())
                    {
                        pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                    }
                    if (pZ != null)
                    {
                        //pZ.SetConstantZ(zmin);
                        pZ.SetConstantZ(0);
                    }
                    else
                    {
                        IPoint p = (pGeom as IPoint);
                        if (p != null)
                        {
                            if (p.Z.ToString() == "非数字")
                            {
                                //p.Z = zmin;
                                p.Z = 0;
                            }
                        }
                    }
                }

                if (pGD.HasM)
                {
                    IMAware pMA = (IMAware)pGeom;
                    pMA.MAware = true;
                }

                pWorkspaceEdit.StartEditOperation();

                IFeature pFeature;
                pFeature = pFeatureLayer.FeatureClass.CreateFeature();

                try
                {
                    pFeature.Shape = pGeom;
                    pFeature.Store();
                }
                catch
                {

                }

                pWorkspaceEdit.StopEditOperation();

                pMap.ClearSelection();  //清除所有选择

            }

        }
        #endregion

//         #region//创建地理要素
//         /// <summary>
//         /// 创建地理要素
//         /// </summary>
//         /// <param name="m_App"></param>
//         /// <param name="pGeom"></param>
//         /// <param name="pMap"></param>
//         /// <param name="pCurrentLayer"></param>
//         public static void CreateFeature(IWorkbench pWorkbench, IGeometry pGeom, IMap pMap, ILayer pCurrentLayer)
//         {
//             if (pGeom != null)
//             {
//                 IFeatureLayer pFeatureLayer;
//                 IDataset pDataset;
//                 IWorkspaceEdit pWorkspaceEdit;
//                 ILayer pLayer;
// 
//                 pLayer = pCurrentLayer;
// 
//                 if (pLayer == null) return;
// 
//                 if (!(pLayer is IFeatureLayer)) return;
// 
//                 pFeatureLayer = (IFeatureLayer)pLayer;
//                 pDataset = (IDataset)pFeatureLayer.FeatureClass;
//                 pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
// 
//                 int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
//                 IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
//                 if (pGD.HasZ)
//                 {
//                     IZAware pZA = (IZAware)pGeom;
//                     pZA.ZAware = true;
// 
//                     IZ pZ = pGeom as IZ;
// 
//                     double zmin = -1000, zmax = 1000;
//                     if (pGD.SpatialReference.HasZPrecision())
//                     {
//                         pGD.SpatialReference.GetZDomain(out zmin, out zmax);
//                     }
// 
//                     if (pZ != null)
//                     {
//                         //pZ.SetConstantZ(zmin);
//                         pZ.SetConstantZ(0);
//                     }
//                     else
//                     {
//                         IPoint p = (pGeom as IPoint);
//                         if (p != null)
//                         {
//                             if (p.Z.ToString() == "非数字")
//                             {
//                                 //p.Z = zmin;
//                                 p.Z = 0;
//                             }
// 
//                         }
//                     }
//                 }
//                 if (pGD.HasM)
//                 {
//                     IMAware pMA = (IMAware)pGeom;
//                     pMA.MAware = true;
//                 }
// 
//                 pWorkspaceEdit.StartEditOperation();
// 
//                 IFeature pFeature;
//                 pFeature = pFeatureLayer.FeatureClass.CreateFeature();
// 
//                 DFGISCodeTree pDFGISCodeTree = pWorkbench.GetPad(typeof(DFGISCodeTree)) as DFGISCodeTree;
//                 int intIndexOfGeoObjNum = 0;
// 
//                 try
//                 {
//                     pFeature.Shape = pGeom;
//                     //intIndexOfGeoObjNum = pFeature.Fields.FindField("GeoObjNum");//查找地物编码字段的索引
//                     //if (intIndexOfGeoObjNum > 0)
//                     //{
//                     //    pFeature.set_Value(intIndexOfGeoObjNum, pDFGISCodeTree.m_selectedGeoNum);
//                     //}
// 
//                     pFeature.Store();
//                 }
//                 catch
//                 {
//                     MessageBox.Show("创建要素时存贮错误");
//                 }
// 
//                 pWorkbench.CommandBarManager.Tools["2dmap.DFEditorTool.Undo"].SharedProps.Enabled = true;
// 
//                 pWorkspaceEdit.StopEditOperation();
// 
//                 pMap.ClearSelection();  //清除所有选择
// 
//             }
// 
//         }
//         #endregion

        #region //创建地理要素并拷贝要素属性
        /// <summary>
        /// 创建地理要素并拷贝要素属性
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pLayer"></param>
        /// <param name="pOldFeature"></param>
        /// <returns></returns>
        public static bool AddFeature0(IMapControl2 pMapControl, IGeometry pGeometry, ILayer pLayer, IFeature pOldFeature)
        {
            IArray pArray = new ArrayClass();
            pArray = CommonFunction.GeometryToArray(pOldFeature.ShapeCopy);

            //			for(int i=0;i<pArray.Count;i++)
            //			{
            //				Console.WriteLine((pArray.get_Element(i) as Point).Z.ToString());
            //			}

            if ((pGeometry == null) || (pLayer == null)) return false;

            IFeatureLayer pFLayer;
            IFeatureClass pFClass;
            IFeature pFeature;

            pFLayer = (IFeatureLayer)pLayer;
            pFClass = pFLayer.FeatureClass;

            pFeature = pFClass.CreateFeature();

            IField pField;
            object Val;
            int nIndex;

            for (int FieldCount = 0; FieldCount < pFeature.Fields.FieldCount; FieldCount++)
            {
                pField = pFeature.Fields.get_Field(FieldCount);
                if (pField.Type != esriFieldType.esriFieldTypeGeometry && pField.Type != esriFieldType.esriFieldTypeOID
                    && pField.Editable == true && pField.Name.ToUpper() != "SHAPE.LEN" && pField.Name.ToUpper() != "FID")
                {
                    nIndex = pOldFeature.Fields.FindField(pFeature.Fields.get_Field(FieldCount).Name);
                    if (nIndex > -1)
                    {
                        Val = pOldFeature.get_Value(nIndex);
                        pFeature.set_Value(FieldCount, Val);
                    }
                }

            }

            try
            {
                CommonFunction.GeoZM0(pGeometry, pFLayer);
                CommonFunction.AddZMValueForGeometry(ref pGeometry, pArray);
                pFeature.Shape = pGeometry;
            }
            catch
            {
                MessageBox.Show("非法操作");
            }
            pFeature.Store();
            CommonFunction.FlashFeature(pMapControl, pMapControl.ActiveView, pFeature);

            return true;
        }
        #endregion

        #region //创建地理要素并拷贝要素属性
        /// <summary>
        /// 创建地理要素并拷贝要素属性
        /// </summary>
        /// <param name="pMapControl"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pLayer"></param>
        /// <param name="pOldFeature"></param>
        /// <returns></returns>
        public static bool AddFeature(IMapControl2 pMapControl, IGeometry pGeometry, ILayer pLayer, IFeature pOldFeature, IArray pArray)
        {
            if ((pGeometry == null) || (pLayer == null)) return false;

            //			for(int i=0;i<pArray.Count;i++)
            //			{
            //				Console.WriteLine((pArray.get_Element(i) as Point).Z.ToString());
            //			}

            IFeatureLayer pFLayer;
            IFeatureClass pFClass;
            IFeature pFeature;

            pFLayer = (IFeatureLayer)pLayer;
            pFClass = pFLayer.FeatureClass;

            pFeature = pFClass.CreateFeature();

            IField pField;
            object Val;
            int nIndex;

            for (int FieldCount = 0; FieldCount < pFeature.Fields.FieldCount; FieldCount++)
            {
                pField = pFeature.Fields.get_Field(FieldCount);
                if (pField.Type != esriFieldType.esriFieldTypeGeometry && pField.Type != esriFieldType.esriFieldTypeOID
                    && pField.Editable == true && pField.Name.ToUpper() != "SHAPE.LEN" && pField.Name.ToUpper() != "FID")
                {
                    nIndex = pOldFeature.Fields.FindField(pFeature.Fields.get_Field(FieldCount).Name);
                    if (nIndex > -1)
                    {
                        Val = pOldFeature.get_Value(nIndex);
                        pFeature.set_Value(FieldCount, Val);
                    }
                }

            }

            try
            {
                CommonFunction.GeoZM0(pGeometry, pFLayer);
                CommonFunction.AddZMValueForGeometry(ref pGeometry, pArray);
                pFeature.Shape = pGeometry;
            }
            catch
            {
                MessageBox.Show("非法操作");
            }
            pFeature.Store();
            CommonFunction.FlashFeature(pMapControl, pMapControl.ActiveView, pFeature);

            return true;
        }
        #endregion

        #region //检查坐标是否超出地图范围
        public static bool PointIsOutMap(ILayer pLayer, IPoint pPoint)
        {
            double xMin = 0;
            double yMin = 0;
            double xMax = 0;
            double yMax = 0;

            IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;

            int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;

            pGD.SpatialReference.GetDomain(out xMin, out xMax, out yMin, out yMax);

            bool isOut = false;
            if ((pPoint.X >= xMin && pPoint.X <= xMax) && (pPoint.Y >= yMin && pPoint.Y <= yMax))
            {
                isOut = true;
            }
            return isOut;
        }
        #endregion

        #region//将封闭的线要素转化成面要素
        /// <summary>
        /// 将封闭的线要素转化成面要素
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IPolygon PolylineToPolygon(IPolyline pPolyline)
        {
            IPoint pPoint = new PointClass();
            ISegmentCollection pSegsPolygon;
            ISegmentCollection pSegsPolyline;

            IPolygon pPolygon = new PolygonClass();
            int count;

            pSegsPolyline = (ISegmentCollection)pPolyline;
            count = pSegsPolyline.SegmentCount;

            pSegsPolygon = (ISegmentCollection)pPolygon;

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            for (int i = 0; i < count; i++)
            {
                pSegsPolygon.AddSegment(pSegsPolyline.get_Segment(i), ref a, ref b);
            }

            pPolygon.SimplifyPreserveFromTo();

            return pPolygon;

        }
        #endregion

        #region//创建弧Arc转换成Polyline; pFromPoint:弧起点，pCenterPoint：弧圆心，pToPoint：弧终点
        //
        public static IPolyline ArcToPolyline(IPoint pFromPoint, IPoint pCenterPoint, IPoint pToPoint, esriArcOrientation ArcOrientation)
        {
            ICircularArc pCArc = new CircularArcClass();
            pCArc.PutCoords(pCenterPoint, pFromPoint, pToPoint, ArcOrientation);

            ISegmentCollection pSegmentCollection = new PolylineClass();

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            pSegmentCollection.AddSegment((ISegment)pCArc, ref a, ref b);

            return pSegmentCollection as IPolyline;

        }
        #endregion

        #region//鼠标在固定方向上的投影点坐标
        /// <summary>
        /// 鼠标在固定方向上的投影点坐标
        /// </summary>
        /// <param name="FormPoint"></param>
        /// <param name="MousePoint"></param>
        /// <param name="FixDirection"></param>
        /// <returns></returns>
        public static IPoint GetTwoPoint_FormPointMousePointFixDirection(IPoint FormPoint, IPoint MousePoint, double FixDirection)
        {
            double tempA;
            double tempDis;
            double dx;
            double dy;
            double dx1;
            double dy1;

            dx = MousePoint.X - FormPoint.X;
            dy = MousePoint.Y - FormPoint.Y;
            tempA = GetAzimuth_P12(FormPoint, MousePoint);
            tempDis = GetDistance_P12(FormPoint, MousePoint);
            dx1 = tempDis * Math.Cos((90 - FixDirection) * Math.PI / 180);
            dy1 = tempDis * Math.Sin((90 - FixDirection) * Math.PI / 180);

            //			if(FixDirection >= 0 && FixDirection < 90 )//第一象限
            //			{
            //				if (tempA >= 90 + FixDirection && tempA < 270 + FixDirection )
            //				{
            //					dx1 = -dx1;
            //					dy1 = -dy1;
            //				}
            //			}
            //			else if(FixDirection >= 90 && FixDirection < 270 )//第二、三象限
            //			{
            //				if( tempA >= FixDirection - 90 && tempA < FixDirection + 90 )
            //				{
            //				}
            //				else
            //				{
            //					dx1 = -dx1;
            //					dy1 = -dy1;
            //				}
            //			}
            //			else//第四象限
            //			{
            //				if(tempA >= FixDirection - 270 && tempA < FixDirection - 90)
            //				{
            //					dx1 = -dx1;
            //					dy1 = -dy1;
            //				}
            //
            //			}

            dx = FormPoint.X + dx1;
            dy = FormPoint.Y + dy1;

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dx, dy);
            return pPoint;

        }
        #endregion

        #region//鼠标在固定方向上的投影点坐标
        /// <summary>
        /// 鼠标在固定方向上的投影点坐标
        /// </summary>
        /// <param name="FormPoint"></param>
        /// <param name="MousePoint"></param>
        /// <param name="FixDirection"></param>
        /// <returns></returns>
        public static IPoint GetOnePoint_FormPointMousePointFixDirection(IPoint FormPoint, IPoint MousePoint, double FixDirection)
        {
            double tempDis;
            double dx;
            double dy;
            double dx1;
            double dy1;

            tempDis = GetDistance_P12(FormPoint, MousePoint);
            dx1 = tempDis * Math.Cos(FixDirection * Math.PI / 180);
            dy1 = tempDis * Math.Sin(FixDirection * Math.PI / 180);

            dx = FormPoint.X + dx1;
            dy = FormPoint.Y + dy1;

            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dx, dy);
            return pPoint;

        }
        #endregion

        #region//将SegmentCollection显示到屏幕
        /// <summary>
        /// 将SegmentCollection显示到屏幕
        /// </summary>
        /// <param name="MapControl"></param>
        /// <param name="PointArray"></param>
        public static void DisplaypSegmentColToScreen(IMapControl2 MapControl, ref IArray PointArray)
        {
            IActiveView pActiveView = MapControl.ActiveView;
            ISegmentCollection pPolylineCol;
            pPolylineCol = new PolylineClass();
            ISegmentCollection pSegmentCollection = MadeSegmentCollection(ref PointArray);
            pPolylineCol.AddSegmentCollection(pSegmentCollection);

            IPointCollection pPointCollection;
            pPointCollection = (IPointCollection)pPolylineCol;

            pActiveView.ScreenDisplay.ActiveCache = (short)esriScreenCache.esriNoScreenCache;
            ISimpleLineSymbol pLineSym = new SimpleLineSymbolClass();
            pLineSym.Color = CommonFunction.GetRgbColor(0, 0, 0);

            pActiveView.ScreenDisplay.StartDrawing(pActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            pActiveView.ScreenDisplay.SetSymbol((ISymbol)pLineSym);
            pActiveView.ScreenDisplay.DrawPolyline((IPolyline)pPolylineCol);
            pActiveView.ScreenDisplay.FinishDrawing();

            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                CommonFunction.DrawPointSMSSquareSymbol(MapControl, pPointCollection.get_Point(i));
            }
        }
        #endregion

        #region//将数组里的坐标构造SegmentCollection
        /// <summary>
        /// 将数组里的坐标构造SegmentCollection
        /// </summary>
        /// <param name="PointArray"></param>
        /// <returns></returns>
        public static ISegmentCollection MadeSegmentCollection(ref IArray PointArray)
        {
            ISegment pSegment;
            ISegmentCollection pSegmentCollection = new PolylineClass();
            IPoint fromPoint;
            IPoint toPoint;

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            for (int i = 0; i < PointArray.Count - 1; i++)
            {	//获取线起点和端点
                fromPoint = (IPoint)PointArray.get_Element(i);
                toPoint = (IPoint)PointArray.get_Element(i + 1);
                pSegment = new LineClass();
                pSegment = MadeLineSeg_2Point(fromPoint, toPoint);
                pSegmentCollection.AddSegment(pSegment, ref a, ref b);

            }// end for

            return pSegmentCollection;

        }
        #endregion

        #region//两点构造线段的Segment
        /// <summary>
        /// 两点构造线段的Segment
        /// </summary>
        /// <param name="pPoint1"></param>
        /// <param name="pPoint2"></param>
        /// <returns></returns>
        public static ISegment MadeLineSeg_2Point(IPoint pPoint1, IPoint pPoint2)
        {
            ILine pLine = new LineClass();
            pLine.PutCoords(pPoint1, pPoint2);
            return (ISegment)pLine;
        }
        #endregion

        #region //根据Segment集合生成线
        /// <summary>
        /// 根据Segment集合生成线
        /// </summary>
        /// <param name="pSegColl"></param>
        /// <returns></returns>
        public static IPolycurve2 BuildPolyLineFromSegmentCollection(ISegmentCollection pSegColl)
        {
            if (pSegColl == null) return null;
            ISegmentCollection pPolyline = new PolylineClass();
            pPolyline.AddSegmentCollection(pSegColl);

            return (IPolycurve2)pPolyline;
        }
        #endregion

        #region//将数组里的坐标构造贝塞尔曲线
        /// <summary>
        /// 将数组里的坐标构造贝塞尔曲线
        /// </summary>
        /// <param name="PointArray"></param>
        /// <returns></returns>
        public static IPolyline MadeBezierCurve(ref IArray PointArray)
        {
            IPolyline pPolyline;
            INewBezierCurveFeedback pBezierCurveFeedback = new NewBezierCurveFeedbackClass();

            pBezierCurveFeedback.Start((IPoint)PointArray.get_Element(0));
            for (int i = 1; i < PointArray.Count; i++)
            {	//获取线起点和端点
                pBezierCurveFeedback.AddPoint((IPoint)PointArray.get_Element(i));
            }
            pPolyline = pBezierCurveFeedback.Stop();

            IGeometry tempGeo = (IGeometry)pPolyline;
            CommonFunction.AddZMValueForGeometry(ref tempGeo, PointArray);

            return pPolyline;
        }
        #endregion

        #region//将Polyline的坐标存入数组中
        /// <summary>
        /// 将Polyline的坐标存入数组中
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IArray PolylineToArray(IPolyline pPolyline)
        {
            IArray pPointArray = new ArrayClass();
            IPointCollection pPointCollection = (IPointCollection)pPolyline;

            if (pPolyline == null) return null;

            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                pPointArray.Add(pPointCollection.get_Point(i));
            }

            return pPointArray;

        }
        #endregion

        #region//将pGeometry的坐标存入数组中
        /// <summary>
        /// 将Polyline的坐标存入数组中
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IArray GeometryToArray(IGeometry pGeometry)
        {
            IArray pPointArray = new ArrayClass();
            IPointCollection pPointCollection = (IPointCollection)pGeometry;

            if (pGeometry == null) return null;

            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                pPointArray.Add(pPointCollection.get_Point(i));
            }

            return pPointArray;

        }
        #endregion

        #region//将pGeometry的坐标存入数组中
        /// <summary>
        /// 将Polyline的坐标存入数组中
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static void GeometryToArray(IGeometry pGeometry, IArray pPointArray)
        {
            IPointCollection pPointCollection = (IPointCollection)pGeometry;

            if (pGeometry == null) return;

            for (int i = 0; i < pPointCollection.PointCount; i++)
            {
                pPointArray.Add(pPointCollection.get_Point(i));
            }
        }
        #endregion

        #region//获得匹配的目标地理要素，将其存入数组中
        /// <summary>
        /// 获得匹配的目标地理要素，将其存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GetSelectFeatureSaveToArray(IMap pMap)
        {
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IArray FeatureArray = new ArrayClass();

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空
            FeatureArray.RemoveAll();
            while (pFeature != null && pFeature.FeatureType != esriFeatureType.esriFTAnnotation)
            {
                FeatureArray.Add(pFeature);
                pFeature = pSelected.Next();
            }

            return FeatureArray;
        }
        #endregion

        #region//获得匹配的目标地理要素，将其存入数组中
        /// <summary>
        /// 获得匹配的目标地理要素，将其存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GetSelectFeatureSaveToArray_2(IMap pMap)
        {
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IArray FeatureArray = new ArrayClass();

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空
            FeatureArray.RemoveAll();
            while (pFeature != null)
            {
                FeatureArray.Add(pFeature);
                pFeature = pSelected.Next();
            }

            return FeatureArray;
        }
        #endregion

        #region//获得线或面地理要素，将其存入数组中
        /// <summary>
        /// 获得匹配的目标地理要素，将其存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IArray GetSelectFeatureSaveToArray_1(IMap pMap)
        {
            IEnumFeature pSelected;
            IFeature pFeature;//IFeature指单个图形对象
            IArray FeatureArray = new ArrayClass();

            pSelected = (IEnumFeature)pMap.FeatureSelection;//取出选中的对象
            pSelected.Reset();
            pFeature = pSelected.Next();//取得一个对象来判空
            FeatureArray.RemoveAll();
            while (pFeature != null && (pFeature.FeatureType != esriFeatureType.esriFTAnnotation && pFeature.Shape.GeometryType != esriGeometryType.esriGeometryPoint))
            {
                FeatureArray.Add(pFeature);
                pFeature = pSelected.Next();
            }

            return FeatureArray;
        }
        #endregion

        #region//获取当前图层选择要素,将其存入数组中
        /// <summary>
        /// 获取当前图层选择要素,将其存入数组中
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public static IArray GetSelectedFeaturesFromCurrentLayerSaveToArray(ILayer pLayer)
        {
            IFeatureCursor pFCursor;
            IFeature pFeature;
            IArray FeatureArray = new ArrayClass();

            IFeatureLayer pFeatureLayer;

            pFeatureLayer = (IFeatureLayer)pLayer;
            if (pFeatureLayer == null) return FeatureArray;

            pFCursor = GetSelectedFeatures(pFeatureLayer);

            if (pFCursor == null) return FeatureArray;

            pFeature = pFCursor.NextFeature();
            while (pFeature != null)
            {
                FeatureArray.Add(pFeature);
                pFeature = pFCursor.NextFeature();
            }

            return FeatureArray;

        }
        #endregion

        #region//获取点或线或面要素,将其存入数组中
        /// <summary>
        /// 获取点或线或面要素,将其存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IArray GetSelectedFeaturesSaveToArray(IMap pMap, esriGeometryType str)
        {
            IFeatureCursor pFCursor;
            IFeature pFeature;
            IArray FeatureArray = new ArrayClass();

            IFeatureLayer pFeatureLayer;
            IDataset pDataset;

            ILayer pLayer;
            IEnumLayer pLayers;
            pLayers = pMap.get_Layers(null, true);
            pLayers.Reset();
            pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            if (pFeatureLayer.FeatureClass.ShapeType == str && pFeatureLayer.FeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
                            {
                                pFCursor = GetSelectedFeatures(pFeatureLayer);

                                if (pFCursor != null)
                                {
                                    pFeature = pFCursor.NextFeature();
                                    while (pFeature != null)
                                    {
                                        FeatureArray.Add(pFeature);
                                        pFeature = pFCursor.NextFeature();
                                    }
                                }
                            }
                        }
                    }
                }

                pLayer = pLayers.Next();

            }

            pMap.ClearSelection();//清空地图选择的要素

            return FeatureArray;

        }
        #endregion

        #region//获取点或线或面要素,将其存入数组中

        //定义一种数据结构，方便将信息存入数组中
        public struct FeatureStruct
        {
            private IFeature pFeature;
            private IFeatureLayer pFeatureLayer;
            public IFeature Feature
            {
                get
                {
                    return pFeature;
                }
                set
                {
                    pFeature = value;
                }
            }
            public IFeatureLayer FeatureLayer
            {
                get
                {
                    return pFeatureLayer;
                }
                set
                {
                    pFeatureLayer = value;
                }
            }
        }
        /// <summary>
        /// 获取点或线或面要素,将其存入数组中
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IArray GetSelectedFeaturesSaveToArray_2(IMap pMap, esriGeometryType str)
        {
            IFeatureCursor pFCursor;
            IFeature pFeature;
            IArray FeatureArray = new ArrayClass();

            IFeatureLayer pFeatureLayer;
            IDataset pDataset;

            FeatureStruct pFeatureStruct = new FeatureStruct();

            ILayer pLayer;
            IEnumLayer pLayers;
            pLayers = pMap.get_Layers(null, true);
            pLayers.Reset();
            pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            if (pFeatureLayer.FeatureClass.ShapeType == str)
                            {
                                pFCursor = GetSelectedFeatures(pFeatureLayer);

                                if (pFCursor != null)
                                {
                                    pFeature = pFCursor.NextFeature();
                                    while (pFeature != null)
                                    {
                                        pFeatureStruct.Feature = pFeature;
                                        pFeatureStruct.FeatureLayer = pFeatureLayer;
                                        FeatureArray.Add(pFeatureStruct);

                                        pFeature = pFCursor.NextFeature();
                                    }
                                }
                            }
                        }
                    }
                }

                pLayer = pLayers.Next();

            }

            pMap.ClearSelection();//清空地图选择的要素

            return FeatureArray;

        }
        #endregion

        #region//删除数组中重复的元素，使其元素具有惟一性
        /// <summary>
        /// 删除数组中重复的元素，使其元素具有惟一性
        /// </summary>
        /// <param name="FeatureArray"></param>
        public static void MadeFeatureArrayOnlyAloneOID(IArray FeatureArray)
        {
            IArray tempArray = new ArrayClass();
            object pObject;
            object pTObject;
            int tempInt = 0;

            if (FeatureArray.Count < 2) return;

            tempArray.Add(FeatureArray.get_Element(0));
            for (int i = 1; i < FeatureArray.Count; i++)
            {
                bool equal = false;
                pObject = FeatureArray.get_Element(i);

                for (int j = 0; j < tempArray.Count; j++)
                {
                    pTObject = tempArray.get_Element(j);
                    if (pObject.Equals(pTObject)) equal = true;
                    tempInt = j;
                }

                if (!equal)
                {
                    tempArray.Add(pObject);
                }
                else
                {
                    tempArray.Remove(tempInt);

                }

            }

            FeatureArray.RemoveAll();

            for (int i = 0; i < tempArray.Count; i++)
            {
                FeatureArray.Add(tempArray.get_Element(i));
            }

        }
        #endregion

        #region //构造一个矩形
        /// <summary>
        /// 构造一个矩形
        /// </summary>
        /// <param name="pPoint"></param>
        /// <param name="dblConst"></param>
        /// <returns></returns>
        public static IEnvelope NewRect(IPoint pPoint, double dblConst)
        {
            IEnvelope pRect;
            pRect = new EnvelopeClass();
            //调整边界的宽度为16个象素大小
            pRect.XMin = pPoint.X - dblConst;
            pRect.YMin = pPoint.Y - dblConst;
            pRect.XMax = pPoint.X + dblConst;
            pRect.YMax = pPoint.Y + dblConst;

            return pRect;
        }
        #endregion

        #region //刷新地图
        /// <summary>
        /// 构造一个矩形
        /// </summary>
        /// <param name="pActiveView"></param>
        /// <returns></returns>
        public static void MapRefresh(IActiveView pActiveView)
        {
            pActiveView.FocusMap.ClearSelection();
            pActiveView.GraphicsContainer.DeleteAllElements();//删除创建的地图元素

            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pActiveView.Extent);//视图刷新

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pActiveView.Extent);//视图刷新
        }
        #endregion

        #region	//根据线段获取以该线段为直径的圆周上的直角点
        /// <summary>
        /// 根据线段获取以该线段为直径的圆周上的直角点
        /// </summary>
        /// <param name="StarPoint"></param>
        /// <param name="EndPoint"></param>
        /// <param name="ePoint"></param>
        /// <returns></returns>
        public static IPoint SquareEnd(IPoint StarPoint, IPoint EndPoint, IPoint ePoint)
        {
            IPoint CenterPoint = CommonFunction.GetCircleCenter_P12(StarPoint, EndPoint);
            double A = CommonFunction.GetAzimuth_P12(CenterPoint, ePoint);
            double R = CommonFunction.GetDistance_P12(StarPoint, EndPoint) / 2;
            IPoint SquarePoint = new PointClass();
            SquarePoint.X = CenterPoint.X + R * Math.Cos(A);
            SquarePoint.Y = CenterPoint.Y + R * Math.Sin(A);
            return SquarePoint;
        }
        #endregion

        #region//边线捕捉
        /// <summary>
        /// 边线捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void PartBoundarySnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//质心捕捉
        /// <summary>
        /// 质心捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void PartCentroidSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartCentroid,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//节点捕捉
        /// <summary>
        /// 节点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void PartVertexSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//端点捕捉
        /// <summary>
        /// 端点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void EndpointSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartEndpoint,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//中点捕捉
        /// <summary>
        /// 中点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void MidpointSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartMidpoint,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                    bSnap = true;
                }
            }
        }
        #endregion

        #region//交点捕捉
        /// <summary>
        /// 交点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void IntersectionSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();

            object a = System.Reflection.Missing.Value;
            IArray pArray = new ArrayClass();
            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest is IPolyline || pHitTest is IPolygon)
                {
                    if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary, hitPoint, ref dblHitDist,
                        ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                    {
                        pArray.Add(pHitTest);
                    }
                }
            }

            IPointCollection pMultipoint = new MultipointClass();
            IArray pArray1 = new ArrayClass();
            ITopologicalOperator2 pTopoOperator;
            for (int i = 0; i < pArray.Count; i++)
            {
                //交点加入到数组中
                pTopoOperator = (ITopologicalOperator2)pArray.get_Element(i);
                for (int j = 0; j < pArray.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (((IGeometry)pArray.get_Element(i)).GeometryType == ((IGeometry)pArray.get_Element(j)).GeometryType)
                    {
                        IGeometry pGeom = pTopoOperator.Intersect((IGeometry)pArray.get_Element(j), esriGeometryDimension.esriGeometry0Dimension);
                        if (pGeom != null)
                        {
                            if (pGeom is IPointCollection)
                            {
                                pMultipoint.AddPointCollection((IPointCollection)pGeom);
                            }
                            else if (pGeom is IPoint)
                            {
                                pMultipoint.AddPoint((IPoint)pGeom, ref a, ref a);
                            }
                        }
                    }
                    else
                    {
                        IGeometry pGeom = pTopoOperator.IntersectMultidimension((IGeometry)pArray.get_Element(j));
                        if (pGeom != null)
                        {
                            IGeometryCollection pGB = pGeom as IGeometryCollection;

                            if (pGB != null)
                            {
                                for (int k = 0; k < pGB.GeometryCount; k++)
                                {
                                    pGeom = pGB.get_Geometry(k);
                                    if (pGeom is IPointCollection)
                                    {
                                        pMultipoint.AddPointCollection((IPointCollection)pGeom);
                                    }
                                    else if (pGeom is IPoint)
                                    {
                                        pMultipoint.AddPoint((IPoint)pGeom, ref a, ref a);
                                    }

                                }

                            }
                        }
                    }
                }
            }
            //从交点数组来判断是否有捕捉到的情况
            pHitTest = (IHitTest)pMultipoint;
            if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex, hitPoint, ref dblHitDist,
                ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
            {
                pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                bSnap = true;
            }

        }
        #endregion

        #region//垂足捕捉
        /// <summary>
        /// 垂足捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void VerticalSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();	//返回击中位置

            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            if (pGeometry == null) return;
            IPoint firstPt = null;
            if (pGeometry is IPoint)
                firstPt = (IPoint)pGeometry;
            else
                firstPt = ((IPointCollection)pGeometry).get_Point(0);

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    int oldPartIndex = partIndex;
                    int oldsegmentIndex = segmentIndex;
                    //找垂足
                    ILine pLine1 = new LineClass();
                    pLine1.PutCoords(hitPoint, firstPt);
                    ILine pLine2 = ((ISegmentCollection)((IGeometryCollection)pHitTest).get_Geometry(partIndex)).get_Segment(segmentIndex) as ILine;
                    if (pLine2 != null)
                    {
                        double a1 = pLine1.Angle / Math.PI * 180;
                        if (a1 < 0) a1 += 360;

                        double a2 = pLine2.Angle / Math.PI * 180;
                        if (a2 < 0) a2 += 360;

                        double angle = a1 - a2;
                        if (angle < 0) angle += 360;
                        if (angle > 180) angle = 360 - angle;
                        double s = Math.Cos(angle * Math.PI / 180) * pLine1.Length;
                        IPoint pt = new PointClass();
                        pt.X = hitPoint.X + s * Math.Cos(pLine2.Angle);
                        pt.Y = hitPoint.Y + s * Math.Sin(pLine2.Angle);
                        pt.Z = hitPoint.Z;
                        if (pHitTest.HitTest(pt, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                            hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                        {
                            if (oldPartIndex == partIndex && oldsegmentIndex == segmentIndex)
                            {
                                pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                                bSnap = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region//切点捕捉
        /// <summary>
        /// 切点捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void TangentSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            if (pGeometry == null) return;
            IHitTest pHitTest;
            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();
            double dblOffsetDist = 1000;
            object a = System.Reflection.Missing.Value;
            IPoint pCurPoint = pPoint;

            if (pGeometry == null) return;
            IPoint firstPt = null;
            if (pGeometry is IPoint)
                firstPt = (IPoint)pGeometry;
            else
                firstPt = ((IPointCollection)pGeometry).get_Point(0);

            //只处理圆弧和曲线
            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pHitTest = (IHitTest)pFeatureCache.get_Feature(i).Shape;
                if (pHitTest.HitTest(pCurPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    IGeometryCollection pGC = pHitTest as IGeometryCollection;
                    if (pGC != null)
                    {
                        ISegmentCollection pSC = pGC.get_Geometry(partIndex) as ISegmentCollection;
                        if (pSC != null)
                        {
                            ISegment pSeg = pSC.get_Segment(segmentIndex);
                            if (pSeg is ICircularArc)
                            {
                                //计算切线
                                IPoint pCPoint = ((ICircularArc)pSeg).CenterPoint;
                                double tempRadiu = ((ICircularArc)pSeg).Radius;
                                //firstPt pCPoint
                                ILine pLine = new LineClass();
                                pLine.PutCoords(firstPt, pCPoint);

                                double Radius = Math.Sqrt(Math.Abs(pLine.Length * pLine.Length - tempRadiu * tempRadiu));
                                IConstructCircularArc pConstCArc = new CircularArcClass();
                                pConstCArc.ConstructCircle(firstPt, Radius, true); //击中元素构造圆弧

                                IGeometryCollection pPolyline = new PolylineClass();
                                ISegmentCollection pPath = new PathClass();
                                pPath.AddSegment((ISegment)pConstCArc, ref a, ref a);

                                pPolyline.AddGeometry((IGeometry)pPath, ref a, ref a);
                                ((ITopologicalOperator)pPolyline).Simplify();

                                IGeometryCollection pPolyline1 = new PolylineClass();
                                ISegmentCollection pPath1 = new PathClass();
                                pPath1.AddSegment(pSeg, ref a, ref a);

                                pPolyline1.AddGeometry((IGeometry)pPath1, ref a, ref a);
                                ((ITopologicalOperator)pPolyline1).Simplify();
                                //求交
                                IGeometry pGeom = ((ITopologicalOperator)pPolyline).Intersect((IGeometry)pPolyline1, esriGeometryDimension.esriGeometry0Dimension);
                                if (pGeom != null)
                                {
                                    if (pGeom is IPointCollection)
                                    {
                                        ILine pLine1 = new LineClass();
                                        ILine pLine2 = new LineClass();

                                        pLine1.PutCoords(((IPointCollection)pGeom).get_Point(0), pCurPoint);
                                        pLine2.PutCoords(((IPointCollection)pGeom).get_Point(1), pCurPoint);

                                        if (pLine1.Length > pLine2.Length)
                                        {
                                            dblHitDist = pLine2.Length;
                                            hitPoint = ((IPointCollection)pGeom).get_Point(1);
                                        }
                                        else
                                        {
                                            dblHitDist = pLine1.Length;
                                            hitPoint = ((IPointCollection)pGeom).get_Point(0);
                                        }

                                        if (dblOffsetDist > dblHitDist)
                                        {
                                            dblOffsetDist = dblHitDist;
                                            //hitPoint = ((IPointCollection)pGeom).get_Point(0);
                                            pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                                            bSnap = true;
                                        }

                                    }
                                    else
                                    {
                                        ILine pLine1 = new LineClass();
                                        pLine1.PutCoords((IPoint)pGeom, pCurPoint);
                                        if (dblOffsetDist > dblHitDist)
                                        {
                                            dblOffsetDist = dblHitDist;
                                            hitPoint = ((IPointCollection)pGeom).get_Point(0);
                                            pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                                            bSnap = true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        #endregion

        #region//圆心捕捉
        /// <summary>
        /// 圆心捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void CircleCenterSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IGeometry pGeom;
            IPoint pCurPoint = pPoint;
            IPoint hitPoint;
            double dblOffsetDist = 1000;
            ILine pLine = new LineClass();
            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pGeom = pFeatureCache.get_Feature(i).Shape;
                ISegmentCollection pSegColl = (ISegmentCollection)pGeom;
                for (int j = 0; j < pSegColl.SegmentCount; j++)
                {
                    if (pSegColl.get_Segment(j) is ICircularArc)//若为圆弧
                    {
                        hitPoint = ((ICircularArc)pSegColl.get_Segment(j)).CenterPoint;
                        pLine.PutCoords(pCurPoint, hitPoint);
                        if (pLine.Length < tolerance)//若当前点到捕捉到的点的距离在容限内
                        {
                            if (dblOffsetDist > pLine.Length)
                            {
                                dblOffsetDist = pLine.Length;//取距离当前点最近的点作为捕捉的点
                                pPoint.PutCoords(hitPoint.X, hitPoint.Y);
                                bSnap = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region//象限捕捉
        /// <summary>
        /// 象限捕捉
        /// </summary>
        /// <param name="pFeatureCache"></param>
        /// <param name="pGeometry"></param>
        /// <param name="pPoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="bSnap"></param>
        public static void QuadrantPointSnapAgent(IFeatureCache2 pFeatureCache, IGeometry pGeometry, IPoint pPoint, double tolerance, bool bSnap)
        {
            IGeometry pGeom;
            IPoint pCurPoint = pPoint;
            IPoint CenterPoint;
            double dblRadius;
            double dblOffsetDist = 1000;
            ILine pLine = new LineClass();
            IArray ArrayPoint = new ArrayClass();
            IPoint tempPoint0 = new PointClass();
            IPoint tempPoint1 = new PointClass();
            IPoint tempPoint2 = new PointClass();
            IPoint tempPoint3 = new PointClass();

            for (int i = 0; i < pFeatureCache.Count; i++)
            {
                pGeom = pFeatureCache.get_Feature(i).Shape;
                ISegmentCollection pSegColl = (ISegmentCollection)pGeom;
                for (int j = 0; j < pSegColl.SegmentCount; j++)
                {
                    if (pSegColl.get_Segment(j) is ICircularArc)//若为圆弧
                    {
                        CenterPoint = ((ICircularArc)pSegColl.get_Segment(j)).CenterPoint;
                        dblRadius = ((ICircularArc)pSegColl.get_Segment(j)).Radius;

                        if (ArrayPoint.Count > 0) ArrayPoint.RemoveAll();

                        tempPoint0.PutCoords(CenterPoint.X + dblRadius, CenterPoint.Y);
                        ArrayPoint.Add(tempPoint0);

                        tempPoint1.PutCoords(CenterPoint.X, CenterPoint.Y - dblRadius);
                        ArrayPoint.Add(tempPoint1);

                        tempPoint2.PutCoords(CenterPoint.X - dblRadius, CenterPoint.Y);
                        ArrayPoint.Add(tempPoint2);

                        tempPoint3.PutCoords(CenterPoint.X, CenterPoint.Y + dblRadius);
                        ArrayPoint.Add(tempPoint3);

                        for (int k = 0; k < ArrayPoint.Count; k++)
                        {
                            pLine.PutCoords(pCurPoint, (IPoint)ArrayPoint.get_Element(k));
                            if (pLine.Length < tolerance)//若当前点到捕捉到的点的距离在容限内
                            {
                                if (dblOffsetDist > pLine.Length)
                                {
                                    dblOffsetDist = pLine.Length;//取距离当前点最近的点作为捕捉的点
                                    pPoint.PutCoords(((IPoint)ArrayPoint.get_Element(k)).X, ((IPoint)ArrayPoint.get_Element(k)).Y);
                                    bSnap = true;
                                }
                            }
                        }

                    }

                }
            }
        }
        #endregion

//         #region//捕捉
//         /// <summary>
//         /// 捕捉
//         /// </summary>
//         /// <param name="pMapControl"></param>
//         /// <param name="cfgsnapEnvironmentSet"></param>
//         /// <param name="pGeometry"></param>
//         /// <param name="pPoint"></param>
//         /// <returns></returns>
//         public static bool Snap(IMapControl2 pMapControl, CfgSnapEnvironmentSet cfgsnapEnvironmentSet, IGeometry pGeometry, IPoint pPoint)
//         {
//             bool bSnap = false;
// 
//             if (cfgsnapEnvironmentSet.IsOpen == false) return false;
// 
//             if (cfgsnapEnvironmentSet.mapSnap == null) return false;
// 
//             if (cfgsnapEnvironmentSet.mapSnap.LayerCount == 0) return false;
// 
//             if (cfgsnapEnvironmentSet.CurrentSnapType == null && cfgsnapEnvironmentSet.IsUseMixSnap == false) return bSnap;
// 
//             pMapControl.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
// 
//             IPoint pInitPoint = new PointClass();
//             pInitPoint.X = pPoint.X;
//             pInitPoint.Y = pPoint.Y;
// 
//             double tolerance;//捕捉容限
//             tolerance = cfgsnapEnvironmentSet.Tolerence;
//             tolerance = ConvertPixelsToMapUnits(pMapControl.ActiveView, tolerance);//屏幕单位转化到地图单位
//             double dblOffsetDist = 1000;
// 
//             IFeatureCache2 pFeatureCache = new FeatureCacheClass();
//             pFeatureCache.Initialize(pPoint, tolerance);//	
// 
//             pFeatureCache.AddLayers(cfgsnapEnvironmentSet.mapSnap.get_Layers(null, true), pMapControl.Extent);//
// 
//             IArray ArrayPoint = new ArrayClass();
//             IPoint tempPoint0 = new PointClass();
//             tempPoint0 = pPoint;
//             IPoint tempPoint1 = new PointClass();
//             tempPoint1 = pPoint;
//             IPoint tempPoint2 = new PointClass();
//             tempPoint2 = pPoint;
//             IPoint tempPoint3 = new PointClass();
//             tempPoint3 = pPoint;
//             IPoint tempPoint4 = new PointClass();
//             tempPoint4 = pPoint;
//             IPoint tempPoint5 = new PointClass();
//             tempPoint5 = pPoint;
//             IPoint tempPoint6 = new PointClass();
//             tempPoint6 = pPoint;
//             IPoint tempPoint7 = new PointClass();
//             tempPoint7 = pPoint;
//             IPoint tempPoint8 = new PointClass();
//             tempPoint8 = pPoint;
//             IPoint tempPoint9 = new PointClass();
//             tempPoint9 = pPoint;
// 
//             IPoint pCurPoint = new PointClass();
//             pCurPoint = pPoint;
// 
//             if (ArrayPoint.Count > 0) ArrayPoint.RemoveAll();
// 
//             if (cfgsnapEnvironmentSet.IsUseMixSnap == true)
//             {
//                 SnapStruct.BoolSnapMode bSnapMode = cfgsnapEnvironmentSet.SnapMode;
// 
//                 if (bSnapMode.PartBoundary)//边	
//                 {
//                     PartBoundarySnapAgent(pFeatureCache, pGeometry, tempPoint0, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint0);
//                 }
//                 if (bSnapMode.PartCentroid)//质心
//                 {
//                     PartCentroidSnapAgent(pFeatureCache, pGeometry, tempPoint1, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint1);
//                 }
//                 if (bSnapMode.PartVertex)//节点
//                 {
//                     PartVertexSnapAgent(pFeatureCache, pGeometry, tempPoint2, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint2);
//                 }
//                 if (bSnapMode.Endpoint)//端点
//                 {
//                     EndpointSnapAgent(pFeatureCache, pGeometry, tempPoint3, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint3);
//                 }
//                 if (bSnapMode.Midpoint)//中点
//                 {
//                     MidpointSnapAgent(pFeatureCache, pGeometry, tempPoint4, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint4);
//                 }
//                 if (bSnapMode.Intersection)//交点
//                 {
//                     IntersectionSnapAgent(pFeatureCache, pGeometry, tempPoint5, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint5);
//                 }
//                 if (bSnapMode.Vertical)//垂足
//                 {
//                     VerticalSnapAgent(pFeatureCache, pGeometry, tempPoint6, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint6);
//                 }
//                 if (bSnapMode.Tangent)//切点
//                 {
//                     TangentSnapAgent(pFeatureCache, pGeometry, tempPoint7, tolerance, bSnap);
//                     if (!tempPoint7.IsEmpty) ArrayPoint.Add(tempPoint7);
//                 }
//                 if (bSnapMode.CircleCenter)//圆心
//                 {
//                     CircleCenterSnapAgent(pFeatureCache, pGeometry, tempPoint8, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint8);
//                 }
//                 if (bSnapMode.QuadrantPoint)//象限点
//                 {
//                     QuadrantPointSnapAgent(pFeatureCache, pGeometry, tempPoint9, tolerance, bSnap);
//                     if (bSnap) ArrayPoint.Add(tempPoint9);
//                 }
//             }
//             else
//             {
//                 if (cfgsnapEnvironmentSet.CurrentSnapType != null)//只有一种捕捉模式
//                 {
//                     switch (cfgsnapEnvironmentSet.CurrentSnapType)
//                     {
//                         case "partBoundary"://边					
//                             PartBoundarySnapAgent(pFeatureCache, pGeometry, tempPoint0, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint0);
//                             break;
//                         case "partCentroid"://质心
//                             PartCentroidSnapAgent(pFeatureCache, pGeometry, tempPoint1, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint1);
//                             break;
//                         case "partVertex"://节点
//                             PartVertexSnapAgent(pFeatureCache, pGeometry, tempPoint2, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint2);
//                             break;
//                         case "endpoint"://端点
//                             EndpointSnapAgent(pFeatureCache, pGeometry, tempPoint3, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint3);
//                             break;
//                         case "midpoint"://中点
//                             MidpointSnapAgent(pFeatureCache, pGeometry, tempPoint4, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint4);
//                             break;
//                         case "intersection"://交点
//                             IntersectionSnapAgent(pFeatureCache, pGeometry, tempPoint5, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint5);
//                             break;
//                         case "vertical"://垂足
//                             VerticalSnapAgent(pFeatureCache, pGeometry, tempPoint6, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint6);
//                             break;
//                         case "tangent"://切点
//                             TangentSnapAgent(pFeatureCache, pGeometry, tempPoint7, tolerance, bSnap);
//                             if (!tempPoint7.IsEmpty) ArrayPoint.Add(tempPoint7);
//                             break;
//                         case "circleCenter"://圆心
//                             CircleCenterSnapAgent(pFeatureCache, pGeometry, tempPoint8, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint8);
//                             break;
//                         case "quadrantPoint"://象限点
//                             QuadrantPointSnapAgent(pFeatureCache, pGeometry, tempPoint9, tolerance, bSnap);
//                             if (bSnap) ArrayPoint.Add(tempPoint9);
//                             break;
//                     }
//                 }
//             }
// 
//             pFeatureCache = null;
// 
//             ILine pLine = new LineClass();
//             MadeFeatureArrayOnlyAloneOID(ArrayPoint);//保证数组的元素的唯一性
//             for (int i = 0; i < ArrayPoint.Count; i++)//求取距离当前点最近的点作为捕捉的点
//             {
//                 pLine.PutCoords(pCurPoint, (IPoint)ArrayPoint.get_Element(i));
//                 if (pLine.Length < tolerance)//若当前点到捕捉到的点的距离在容限内
//                 {
//                     if (dblOffsetDist > pLine.Length)
//                     {
//                         dblOffsetDist = pLine.Length;
//                         pPoint.PutCoords(((IPoint)ArrayPoint.get_Element(i)).X, ((IPoint)ArrayPoint.get_Element(i)).Y);
// 
//                         bSnap = true;
//                     }
//                 }
//             }
// 
//             if (pInitPoint.X == pPoint.X && pInitPoint.Y == pPoint.Y)
//             {
//                 bSnap = false;
//             }
//             else
//             {
//                 bSnap = true;
//                 DrawSnapSMSSquareSymbol(pMapControl, pPoint);
//             }
// 
//             tempPoint0 = null;
//             tempPoint1 = null;
//             tempPoint2 = null;
//             tempPoint3 = null;
//             tempPoint4 = null;
//             tempPoint5 = null;
//             tempPoint6 = null;
//             tempPoint7 = null;
//             tempPoint8 = null;
//             tempPoint9 = null;
// 
//             return bSnap;
//         }
//         #endregion

        #region//用平行尺方法，修改锚点坐标
        /// <summary>
        /// 用平行尺方法，修改锚点坐标
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="pActiveView"></param>
        /// <param name="dbltolerance"></param>
        /// <param name="ISegment"></param>
        /// <param name="pLastPoint"></param>
        /// <param name="pMouseMovePoint"></param>
        /// <param name="pAnchorPoint"></param>
        public static void ParallelRule(ref bool bkeyCodeP, IActiveView pActiveView, double dbltolerance,
            ref ISegment pSegment, IPoint pLastPoint, IPoint pMouseMovePoint, ref IPoint pAnchorPoint)
        {
            if (pLastPoint == null) return;
            if (pLastPoint.IsEmpty) return;
            if (pMouseMovePoint == null) return;
            if (pMouseMovePoint.IsEmpty) return;

            IConstructPoint pCPoint = new PointClass();
            double dblLength = CommonFunction.GetDistance_P12(pLastPoint, pMouseMovePoint);

            if (pSegment == null && bkeyCodeP == true)
            {
                IFeature pTempFeature;
                UID pId = new UIDClass();
                pId.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";

                IEnumLayer pEnumLayer;
                pEnumLayer = (IEnumLayer)pActiveView.FocusMap.get_Layers(pId, true);
                IFeatureCache2 pFeatureCache = new FeatureCacheClass();
                pFeatureCache.Initialize(pMouseMovePoint, dbltolerance);
                pFeatureCache.AddLayers(pEnumLayer, pActiveView.Extent);

                CommonFunction.GetClosestSelectedFeature((IFeatureCache)pFeatureCache, pMouseMovePoint, out pTempFeature);

                if (pTempFeature == null) return;

                IGeometry pGeometry = pTempFeature.Shape;

                IHitTest pHitTest;
                double dblHitDist = 0;
                int partIndex = 0;
                int segmentIndex = 0;
                bool bRightSide = true;
                IPoint hitPoint = new PointClass();	//返回击中位置		
                pHitTest = (IHitTest)pGeometry;

                if (pHitTest.HitTest(pMouseMovePoint, dbltolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //如果击中该要素
                {
                    pSegment = ((ISegmentCollection)pGeometry).get_Segment(segmentIndex);
                }
            }

            if (pSegment != null)
            {
                pCPoint.ConstructParallel(pSegment, esriSegmentExtension.esriNoExtension, pLastPoint, dblLength);

                double dlbAngle_P123 = CommonFunction.GetAngle_P123((IPoint)pCPoint, pLastPoint, pMouseMovePoint);
                if (dlbAngle_P123 > Math.PI / 2)
                {
                    double dblA = CommonFunction.GetAzimuth_P12(pLastPoint, (IPoint)pCPoint) + Math.PI;
                    double dblD = CommonFunction.GetDistance_P12(pLastPoint, (IPoint)pCPoint);
                    IPoint tempPoint = new PointClass();
                    if (dblA > 2 * Math.PI) dblA = dblA - 2 * Math.PI;
                    tempPoint.X = pLastPoint.X + dblD * Math.Cos(dblA);
                    tempPoint.Y = pLastPoint.Y + dblD * Math.Sin(dblA);
                    pAnchorPoint = tempPoint;
                }
                else
                {
                    pAnchorPoint = (IPoint)pCPoint;
                }

            }

            bkeyCodeP = false;

        }
        #endregion

        #region//正交
        /// <summary>
        /// 正交
        /// </summary>
        /// <param name="StarPoint"></param>
        /// <param name="EndPoint"></param>
        public static void PositiveCross(IPoint pLastPoint, ref IPoint pAnchorPoint, bool bOpen)
        {
            if (!bOpen) return;

            if (pLastPoint == null) return;
            if (pLastPoint.IsEmpty) return;

            double D = CommonFunction.GetDistance_P12(pLastPoint, pAnchorPoint);
            double A = CommonFunction.GetAzimuth_P12(pLastPoint, pAnchorPoint);
            double DX = D * Math.Cos(A);
            double DY = D * Math.Sin(A);
            if (Math.Abs(DX) >= Math.Abs(DY))
            {
                pAnchorPoint.X = pLastPoint.X + DX;
                pAnchorPoint.Y = pLastPoint.Y;
            }
            else
            {
                pAnchorPoint.X = pLastPoint.X;
                pAnchorPoint.Y = pLastPoint.Y + DY;
            }

        }
        #endregion

        #region //添加地图元素
        /// <summary>
        /// 添加地图元素
        /// </summary>
        /// <returns></returns>
        public static void AddElement(IMapControl2 pMapControl, IGeometry pGeometry)
        {
            pMapControl.ActiveView.FocusMap.ClearSelection();//清空地图选择的要素
            pMapControl.ActiveView.GraphicsContainer.DeleteAllElements();

            //分点、线、面，给新的symbol

            IRgbColor color = new RgbColor();
            // 设置颜色属性
            color.Red = 255;
            color.Blue = 0;
            color.Green = 0;

            if (pGeometry == null) return;

            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                pSymbol = (IMarkerSymbol)GetDefaultSymbol(pGeometry.GeometryType);

                IElement pElement = new MarkerElementClass();
                pElement.Geometry = pGeometry;

                IMarkerElement pMarkerElement;
                pMarkerElement = (IMarkerElement)pElement;
                pMarkerElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            }
            else if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline || pGeometry.GeometryType == esriGeometryType.esriGeometryLine)
            {
                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                //pSymbol = (ILineSymbol)GetDefaultSymbol(pGeometry.GeometryType);
                pSymbol.Width = 1.5;
                pSymbol.Color = color;

                IElement pElement = new LineElementClass();
                pElement.Geometry = pGeometry;

                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);

            }
            else if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                // 获取IRGBColor接口

                // 获取ILine符号接口
                ILineSymbol outline = new SimpleLineSymbol();
                // 设置线符号属性
                outline.Width = 1.5;
                outline.Color = color;
                // 获取IFillSymbol接口
                ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                // 设置填充符号属性
                simpleFillSymbol.Outline = outline;
                simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal;


                IFillSymbol pSymbol = new SimpleFillSymbolClass();
                pSymbol = (IFillSymbol)simpleFillSymbol;

                IElement pElement = new PolygonElementClass();
                pElement.Geometry = pGeometry;

                IFillShapeElement pFillElement;
                pFillElement = (IFillShapeElement)pElement;
                pFillElement.Symbol = (IFillSymbol)pSymbol;

                pMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 1);
            }


            pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pMapControl.ActiveView.Extent);

        }
        #endregion

        #region//由圆心，起点，终点构造圆弧的Segment
        /// <summary>
        /// 由圆心，起点，终点构造圆弧的Segment
        /// </summary>
        /// <param name="cPoint"></param>
        /// <param name="fPoint"></param>
        /// <param name="tPoint"></param>
        /// <returns></returns>
        public static ISegment MadeArcSeg_3Point(IPoint cPoint, IPoint fPoint, IPoint tPoint, bool Max)
        {
            esriArcOrientation CArcOrientation;
            CArcOrientation = esriArcOrientation.esriArcClockwise;
            if (Max == true)
            {
                CArcOrientation = esriArcOrientation.esriArcCounterClockwise;
            }
            ICircularArc pCArc = new CircularArcClass();
            pCArc.PutCoords(cPoint, fPoint, tPoint, CArcOrientation);
            return (ISegment)pCArc;
        }
        #endregion

        #region//由圆心半径构造圆的Segment
        /// <summary>
        /// 由圆心半径构造圆的Segment
        /// </summary>
        /// <param name="cPoint"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static ISegment MadeCirSeg_cPointAndR(IPoint cPoint, double r)
        {
            ICircularArc pCArc = new CircularArcClass();
            pCArc.PutCoordsByAngle(cPoint, 0, Math.PI * 2, r);
            return (ISegment)pCArc;
        }
        #endregion

        #region//获取线段的中点
        /// <summary>
        /// 获取线段的中点
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static IPoint GetMidPiont_P12(IPoint P1, IPoint P2)
        {
            double Dp1_p2 = CommonFunction.GetDistance_P12(P1, P2);
            double Ap1_p2 = CommonFunction.GetAzimuth_P12(P1, P2);
            IPoint P0 = new PointClass();
            P0.X = P1.X + (Dp1_p2 / 2) * Math.Cos(Ap1_p2);
            P0.Y = P1.Y + (Dp1_p2 / 2) * Math.Sin(Ap1_p2);
            return P0;
        }
        #endregion

        #region//由对角点够造正方形
        /// <summary>
        /// 由对角点够造正方形
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static ISegmentCollection MadeZhengFangXing_P12(IPoint P1, IPoint P2)
        {
            double Dp1_p2 = CommonFunction.GetDistance_P12(P1, P2);
            double Ap1_p2 = CommonFunction.GetAzimuth_P12(P1, P2);
            double ab = Dp1_p2 * Math.Sin(Math.PI / 4);
            double Ap1_p3 = Ap1_p2 + Math.PI * 7 / 4;
            if (Ap1_p3 > Math.PI * 2)
            {
                Ap1_p3 = Ap1_p3 - Math.PI * 2;
            }
            double Ap1_p4 = Ap1_p2 + Math.PI / 4;
            if (Ap1_p4 > Math.PI * 2)
            {
                Ap1_p4 = Ap1_p4 - Math.PI * 2;
            }

            IPoint P3 = new PointClass();
            P3.X = P1.X + ab * Math.Cos(Ap1_p3);
            P3.Y = P1.Y + ab * Math.Sin(Ap1_p3);

            IPoint P4 = new PointClass();
            P4.X = P1.X + ab * Math.Cos(Ap1_p4);
            P4.Y = P1.Y + ab * Math.Sin(Ap1_p4);

            //由点构造Segment
            ISegment Line1Segment = CommonFunction.MadeLineSeg_2Point(P1, P3);
            ISegment Line2Segment = CommonFunction.MadeLineSeg_2Point(P3, P2);
            ISegment Line3Segment = CommonFunction.MadeLineSeg_2Point(P2, P4);
            ISegment Line4Segment = CommonFunction.MadeLineSeg_2Point(P4, P1);

            //由Segments构造多部件
            ISegmentCollection pPolyline = new PolylineClass();

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            pPolyline.AddSegment(Line1Segment, ref a, ref b);
            pPolyline.AddSegment(Line2Segment, ref a, ref b);
            pPolyline.AddSegment(Line3Segment, ref a, ref b);
            pPolyline.AddSegment(Line4Segment, ref a, ref b);

            return pPolyline;
        }
        #endregion

        #region//由三点够造矩形
        /// <summary>
        /// 由三点够造矩形
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="P3"></param>
        /// <returns></returns>
        public static ISegmentCollection MadeJuXing_P123(IPoint P1, IPoint P2, IPoint P3)
        {
            double Dp2_p3 = CommonFunction.GetDistance_P12(P2, P3);
            double Ap2_p3 = CommonFunction.GetAzimuth_P12(P2, P3);

            IPoint P4 = new PointClass();
            P4 = CommonFunction.GetPiont_PdA(P1, Dp2_p3, Ap2_p3);

            //由点构造Segment
            ISegment Line1Segment = CommonFunction.MadeLineSeg_2Point(P1, P2);
            ISegment Line2Segment = CommonFunction.MadeLineSeg_2Point(P2, P3);
            ISegment Line3Segment = CommonFunction.MadeLineSeg_2Point(P3, P4);
            ISegment Line4Segment = CommonFunction.MadeLineSeg_2Point(P4, P1);

            //由Segments构造多部件
            ISegmentCollection pPolyline = new PolylineClass();

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            pPolyline.AddSegment(Line1Segment, ref a, ref b);
            pPolyline.AddSegment(Line2Segment, ref a, ref b);
            pPolyline.AddSegment(Line3Segment, ref a, ref b);
            pPolyline.AddSegment(Line4Segment, ref a, ref b);

            return pPolyline;
        }
        #endregion

        #region//坐标正算得到点坐标
        /// <summary>
        /// 坐标正算得到点坐标
        /// </summary>
        /// <param name="P"></param>
        /// <param name="d"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static IPoint GetPiont_PdA(IPoint P, double d, double A)
        {
            IPoint P0 = new PointClass();
            P0.X = P.X + d * Math.Cos(A);
            P0.Y = P.Y + d * Math.Sin(A);
            return P0;
        }
        #endregion

        #region//由端点、方位角、定长生成箭头
        /// <summary>
        /// 由端点、方位角、定长生成箭头
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="A"></param>
        /// <param name="L"></param>
        /// <returns></returns>
        public static ISegmentCollection MadeJianTou_PAL(IPoint P, double A, double L, double J)
        {
            double AA = A + Math.PI;
            if (AA > Math.PI * 2)
            {
                AA = AA - Math.PI * 2;
            }

            double Ap1 = AA - J / 2;
            if (Ap1 < 0)
            {
                Ap1 = Ap1 + Math.PI * 2;
            }
            IPoint P1 = new PointClass();
            P1 = CommonFunction.GetPiont_PdA(P, L, Ap1);

            double Ap2 = AA + J / 2;
            if (Ap2 > Math.PI * 2)
            {
                Ap2 = Ap2 - Math.PI * 2;
            }
            IPoint P2 = new PointClass();
            P2 = CommonFunction.GetPiont_PdA(P, L, Ap2);

            //由点构造Segment
            ISegment Line1Segment = CommonFunction.MadeLineSeg_2Point(P1, P);
            ISegment Line2Segment = CommonFunction.MadeLineSeg_2Point(P, P2);

            //由Segments构造多部件
            ISegmentCollection pPolyline = new PolylineClass();

            object a = System.Reflection.Missing.Value;
            object b = System.Reflection.Missing.Value;

            pPolyline.AddSegment(Line1Segment, ref a, ref b);
            pPolyline.AddSegment(Line2Segment, ref a, ref b);

            return pPolyline;
        }
        #endregion

        #region//多部件转换成Geometry
        /// <summary>
        /// 多部件转换成Geometry
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IGeometry SegmentCollectionToGeometry(ISegmentCollection pPolyline)
        {
            //多部件转换成Geometry
            IGeometry pGeometry = (IGeometry)pPolyline;
            ITopologicalOperator pTopologicalOperator = (ITopologicalOperator)pGeometry;
            pTopologicalOperator.Simplify();

            return pGeometry;
        }
        #endregion

        #region//获取正方形的对角点
        /// <summary>
        /// 获取正方形的对角点
        /// </summary>
        /// <param name="p0">中心点</param>
        /// <param name="A">方位角</param>
        /// <param name="a">边长</param>
        /// <param name="p1">返回对角点1</param>
        /// <param name="p2">返回对角点2</param>
        public static void GetDuiJiaoDian(IPoint p0, double A, double a, ref IPoint p1, ref IPoint p2)
        {
            double r = (a / 2) * Math.Pow(2, 0.5);
            double A1 = A + Math.PI * 3 / 4;
            if (A1 > Math.PI * 2)
            {
                A1 = A1 - Math.PI * 2;
            }
            double A2 = A + Math.PI * 7 / 4;
            if (A2 > Math.PI * 2)
            {
                A2 = A2 - Math.PI * 2;
            }
            p1 = CommonFunction.GetPiont_PdA(p0, r, A1);
            p2 = CommonFunction.GetPiont_PdA(p0, r, A2);
        }
        #endregion

        #region //计算p1,p2,p3组成的左角a123
        /// <summary>
        /// 计算p1,p2,p3组成的左角a123
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static double GetAngleZuo_P123(IPoint p1, IPoint p2, IPoint p3)
        {
            double a = GetAzimuth_P12(p2, p1) - GetAzimuth_P12(p2, p3);
            if (a < 0)
            {
                a = a + Math.PI * 2;
            }
            return a;
        }
        #endregion

        #region //由点数组计算平行线点数组
        /// <summary>
        /// 由点数组计算平行线点数组
        /// </summary>
        /// <param name="pPointArray"></param>
        /// <returns></returns>
        public static IArray GetPingXingXianArray(IArray pPointArray, double d)
        {
            IArray pPointArray_2 = new ArrayClass();

            if (pPointArray.Count < 2)
            {
                //少于两个点不能构成围墙
                return pPointArray_2;
            }
            else if (pPointArray.Count == 2)
            {
                //只有一段围墙
                IPoint P1 = (IPoint)pPointArray.get_Element(0);
                IPoint P2 = (IPoint)pPointArray.get_Element(1);

                double Ap1_p2 = CommonFunction.GetAzimuth_P12(P1, P2);
                double Ap1_p11 = Ap1_p2 + Math.PI / 2;
                if (Ap1_p11 > Math.PI * 2)
                {
                    Ap1_p11 = Ap1_p11 - Math.PI * 2;
                }

                IPoint P3 = CommonFunction.GetPiont_PdA(P1, d, Ap1_p11);
                IPoint P4 = CommonFunction.GetPiont_PdA(P2, d, Ap1_p11);
                pPointArray_2.Add(P3);
                pPointArray_2.Add(P4);

            }
            else
            {
                //有两段以上
                for (int i = 1; i < pPointArray.Count - 1; i++)
                {
                    IPoint P_1 = (IPoint)pPointArray.get_Element(i - 1);
                    IPoint P1 = (IPoint)pPointArray.get_Element(i);
                    IPoint P2 = (IPoint)pPointArray.get_Element(i + 1);

                    double Ap_1_p1 = CommonFunction.GetAzimuth_P12(P_1, P1);
                    double Ap1_p2 = CommonFunction.GetAzimuth_P12(P1, P2);

                    if (i == 1)//取第1，2，3个点时
                    {
                        double Ap_1_p_11 = Ap_1_p1 + Math.PI / 2;
                        if (Ap_1_p_11 > Math.PI * 2)
                        {
                            Ap_1_p_11 = Ap_1_p_11 - Math.PI * 2;
                        }
                        double a_123 = CommonFunction.GetAngleZuo_P123(P_1, P1, P2);
                        double Ap1_p11 = Ap1_p2 + a_123 / 2;
                        if (Ap1_p11 > Math.PI * 2)
                        {
                            Ap1_p11 = Ap1_p11 - Math.PI * 2;
                        }
                        IPoint P_11 = CommonFunction.GetPiont_PdA(P_1, d, Ap_1_p_11);
                        IPoint P11 = CommonFunction.GetPiont_PdA(P1, d / Math.Sin(a_123 / 2), Ap1_p11);

                        pPointArray_2.Add(P_11);
                        pPointArray_2.Add(P11);

                    }
                    if (i == pPointArray.Count - 2)//取第n，n-1，n-2个点时
                    {
                        double a_123 = CommonFunction.GetAngleZuo_P123(P_1, P1, P2);
                        double Ap1_p11 = Ap1_p2 + a_123 / 2;
                        if (Ap1_p11 > Math.PI * 2)
                        {
                            Ap1_p11 = Ap1_p11 - Math.PI * 2;
                        }
                        double Ap2_p22 = Ap1_p2 + Math.PI / 2;
                        if (Ap2_p22 > Math.PI * 2)
                        {
                            Ap2_p22 = Ap2_p22 - Math.PI * 2;
                        }
                        IPoint P22 = CommonFunction.GetPiont_PdA(P2, d, Ap2_p22);
                        IPoint P11 = CommonFunction.GetPiont_PdA(P1, d / Math.Sin(a_123 / 2), Ap1_p11);
                        if (pPointArray.Count > 3)
                        {
                            pPointArray_2.Add(P11);
                        }
                        pPointArray_2.Add(P22);

                    }
                    if (i != 1 && i != pPointArray.Count - 2)//取中间点时
                    {
                        double a_123 = CommonFunction.GetAngleZuo_P123(P_1, P1, P2);
                        double Ap1_p11 = Ap1_p2 + a_123 / 2;
                        if (Ap1_p11 > Math.PI * 2)
                        {
                            Ap1_p11 = Ap1_p11 - Math.PI * 2;
                        }

                        IPoint P11 = CommonFunction.GetPiont_PdA(P1, d / Math.Sin(a_123 / 2), Ap1_p11);
                        pPointArray_2.Add(P11);

                    }
                }
            }

            return pPointArray_2;
        }
        #endregion

        #region//判断是否为偶数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool DoubleIsOuShu(double d)
        {
            bool T = false;
            if (Math.IEEERemainder(d, 2) == 0)
            {
                T = true;
            }
            return T;
        }
        #endregion

        #region 为数组里的几何形体增加Z值和M值
        /// <summary>
        /// 为数组里的几何形体增加Z值和M值
        /// </summary>
        /// <param name="pGeoArray">存储几何形体的数组</param>
        /// <param name="pPointArray">用于更新几何形体Z值、M值的点数组</param>
        public static void AddZMValueForGeometry(ref IArray pGeoArray, IArray pPointArray)
        {
            IArray pTempGeoArray = new ArrayClass();

            for (int i = 0; i < pGeoArray.Count; i++)
            {
                IGeometry pGeo = (IGeometry)pGeoArray.get_Element(i);
                IPointCollection pPointCol = (IPointCollection)pGeo;

                for (int j = 0; j < pPointCol.PointCount; j++)
                {
                    IPoint pPoint = pPointCol.get_Point(j);
                    IZAware pZ = (IZAware)pPoint;
                    pZ.ZAware = true;
                    pPoint.Z = 0;
                    IMAware pM = (IMAware)pPoint;
                    pM.MAware = true;
                    pPoint.M = 0;

                    for (int k = 0; k < pPointArray.Count; k++)
                    {
                        IPoint pTempPoint = (IPoint)pPointArray.get_Element(k);
                        if (pPoint.X == pTempPoint.X && pPoint.Y == pTempPoint.Y && pTempPoint.Z != -999)
                        {
                            pPoint.Z = pTempPoint.Z;
                            pPoint.M = pTempPoint.M;
                            break;
                        }
                    }
                    pPointCol.UpdatePoint(j, pPoint);

                }

                IZAware pG = (IZAware)pGeo;
                pG.ZAware = true;

                pGeo = (IGeometry)pPointCol;
                pTempGeoArray.Add(pGeo);
            }

            pGeoArray.RemoveAll();

            for (int j = 0; j < pTempGeoArray.Count; j++)
            {
                pGeoArray.Add(pTempGeoArray.get_Element(j));
            }
        }
        #endregion

        #region 为几何形体增加Z值和M值
        /// <summary>
        /// 为几何形体增加Z值和M值
        /// </summary>
        /// <param name="pGeo">存储几何形体</param>
        /// <param name="pPointArray">用于更新几何形体Z值、M值的点数组</param>
        public static void AddZMValueForGeometry(ref IGeometry pGeo, IArray pPointArray)
        {
            IPointCollection pPointCol = (IPointCollection)pGeo;

            for (int j = 0; j < pPointCol.PointCount; j++)
            {
                IPoint pPoint = pPointCol.get_Point(j);
                IZAware pZ = (IZAware)pPoint;
                pZ.ZAware = true;
                pPoint.Z = 0;
                IMAware pM = (IMAware)pPoint;
                pM.MAware = true;
                pPoint.M = 0;

                for (int k = 0; k < pPointArray.Count; k++)
                {
                    IPoint pTempPoint = (IPoint)pPointArray.get_Element(k);
                    if (Math.Abs(pPoint.X - pTempPoint.X) < 0.001 && Math.Abs(pPoint.Y - pTempPoint.Y) < 0.001 && pTempPoint.Z != -999)
                    {
                        pPoint.Z = pTempPoint.Z;
                        pPoint.M = pTempPoint.M;
                        break;
                    }
                }
                pPointCol.UpdatePoint(j, pPoint);

            }

            IZAware pG = (IZAware)pGeo;
            pG.ZAware = true;
            pGeo = (IGeometry)pPointCol;

        }
        #endregion

        #region//检查图层编辑是否可以回退
        /// <summary>
        /// 检查图层编辑是否可以回退
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static bool CheckWorkspaceHasRedos(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            bool boolCheck = false;
            ICompositeLayer pComp;

            if (pLayer is IGroupLayer)
            {
                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    if (CheckWorkspaceHasRedos(pComp.get_Layer(i)))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = pDataset.Workspace as IWorkspaceEdit;
                            if (pWorkspaceEdit == null) return false;

                            pWorkspaceEdit.HasRedos(ref boolCheck);

                            return boolCheck;

                        }
                    }
                }

            }
            return false;
        }
        #endregion

        #region//检查图层编辑是否可以重做
        /// <summary>
        /// 检查图层编辑是否可以重做
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static bool CheckWorkspaceHasUndos(ILayer pLayer)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            IWorkspaceEdit pWorkspaceEdit;
            bool boolCheck = false;
            ICompositeLayer pComp;

            if (pLayer is IGroupLayer)
            {
                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    if (CheckWorkspaceHasUndos(pComp.get_Layer(i)))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (pLayer is IFeatureLayer)
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;
                    if (pFeatureLayer.FeatureClass != null)
                    {
                        pDataset = (IDataset)pFeatureLayer.FeatureClass;

                        if (pDataset.Type == esriDatasetType.esriDTFeatureClass || pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                        {
                            pWorkspaceEdit = pDataset.Workspace as IWorkspaceEdit;
                            if (pWorkspaceEdit == null) return false;

                            pWorkspaceEdit.HasUndos(ref boolCheck);

                            return boolCheck;

                        }
                    }
                }

            }
            return false;
        }
        #endregion

        #region//开放几何形体的ｚ值和ｍ值
        public static void GeoZM0(IGeometry pGeom, IFeatureLayer pFeatureLayer)
        {
            int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
            if (pGD.HasZ)
            {
                IZAware pZA = (IZAware)pGeom;
                pZA.ZAware = true;

                IZ pZ = pGeom as IZ;
                double zmin = -1000, zmax = 1000;
                if (pGD.SpatialReference.HasZPrecision())
                {

                    pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                }

                if (pZ != null)
                {
                    //pZ.SetConstantZ(zmin);
                    pZ.SetConstantZ(0);
                }
                else
                {
                    IPoint p = pGeom as IPoint;
                    if (p != null)
                    {
                        //p.Z = zmin;
                        p.Z = 0;
                    }
                }

            }
            if (pGD.HasM)
            {
                IMAware pMA = (IMAware)pGeom;
                pMA.MAware = true;
            }

        }
        #endregion

        #region//开放几何形体的ｚ值和ｍ值
        public static void GeoZM(IGeometry pGeom, IFeatureLayer pFeatureLayer)
        {
            IArray pArrayPoint = new ArrayClass();
            pArrayPoint = GeometryToArray(pGeom);
            int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
            if (pGD.HasZ)
            {
                IZAware pZA = (IZAware)pGeom;
                pZA.ZAware = true;

                IZ pZ = pGeom as IZ;
                double zmin = -1000, zmax = 1000;
                if (pGD.SpatialReference.HasZPrecision())
                {

                    pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                }

                if (pZ != null)
                {
                    //pZ.SetConstantZ(zmin);
                    pZ.SetConstantZ(0);
                }
                else
                {
                    IPoint p = pGeom as IPoint;
                    if (p != null)
                    {
                        //p.Z = zmin;
                        p.Z = 0;
                    }
                }

            }
            if (pGD.HasM)
            {
                IMAware pMA = (IMAware)pGeom;
                pMA.MAware = true;
            }

            AddZMValueForGeometry(ref pGeom, pArrayPoint);

        }
        #endregion

        #region//开放几何形体的ｚ值和ｍ值
        public static void GeoZM2(IGeometry pGeom, IFeatureLayer pFeatureLayer, IArray pArrayPoint)
        {
            //			for(int i=0;i<pArrayPoint.Count;i++)
            //			{
            //				Console.WriteLine((pArrayPoint.get_Element(i) as Point).Z.ToString());
            //			}

            int index = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureLayer.FeatureClass.Fields.get_Field(index).GeometryDef;
            if (pGD.HasZ)
            {
                IZAware pZA = (IZAware)pGeom;
                pZA.ZAware = true;

                IZ pZ = pGeom as IZ;
                double zmin = -1000, zmax = 1000;
                if (pGD.SpatialReference.HasZPrecision())
                {

                    pGD.SpatialReference.GetZDomain(out zmin, out zmax);
                }

                if (pZ != null)
                {
                    //pZ.SetConstantZ(zmin);
                    pZ.SetConstantZ(0);
                }
                else
                {
                    IPoint p = pGeom as IPoint;
                    if (p != null)
                    {
                        //p.Z = zmin;
                        p.Z = 0;
                    }
                }

            }
            if (pGD.HasM)
            {
                IMAware pMA = (IMAware)pGeom;
                pMA.MAware = true;
            }

            AddZMValueForGeometry(ref pGeom, pArrayPoint);

        }
        #endregion

        #region //获取包含数组中要素的最小矩形
        /// <summary>
        /// 获取包含数组中要素的最小矩形
        /// </summary>
        /// <param name="pFeatureArray">包含要素的数组</param>
        public static IEnvelope GetMinEnvelopeOfTheFeatures(IArray pFeatureArray)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            if (pFeatureArray.Count != 0)
            {
                pEnvelope = ((IFeature)pFeatureArray.get_Element(0)).Extent;

                for (int i = 1; i < pFeatureArray.Count; i++)
                {
                    pEnvelope.Union(((IFeature)pFeatureArray.get_Element(i)).Extent);

                }
            }

            return pEnvelope;
        }

        #endregion

        #region //获取包含数组点的集合的最小矩形
        /// <summary>
        /// 获取包含数组点的集合的最小矩形
        /// </summary>
        /// <param name="pFeatureArray">包含要素的数组</param>
        public static IEnvelope GetMinEnvelopeOfTheArray(IArray pPointArray)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            IPolyline pPolyline = new PolylineClass();
            pPolyline = MadeBezierCurve(ref pPointArray);
            pEnvelope = pPolyline.Envelope;

            return pEnvelope;
        }

        #endregion

        #region 由dx,dx计算倾角
        /// <summary>
        /// 计算直线的斜角
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double CalAngle(double x, double y)
        {
            double dAngle;
            if (Math.Abs(x) > 0)
            {
                dAngle = Math.Atan(y / x) / Math.PI * 180.0;
                if (dAngle > 0)
                {
                    if (x < 0) dAngle += 180.0;
                }
                else if (dAngle < 0)
                {
                    if (y > 0)
                    {
                        dAngle = 180 + dAngle;
                    }
                    else
                    {
                        dAngle = 360 + dAngle;
                    }
                }
                else
                {
                    if (x < 0)
                    {
                        dAngle = 180;
                    }
                }
            }
            else
            {
                if (y > 0)
                {
                    dAngle = 90.0;
                }
                else
                {
                    dAngle = 270.0;
                }
            }
            return dAngle;
        }

        #endregion

        #region 内插Z值 2007.09.21 TianK 添加
        /// <summary>
        /// 根据线上有高点的高程内插无高点的高程（高为-1000，-999，0都认为是无高）
        /// </summary>
        /// <param name="pPointCollection"></param>
        public static void InterpolationZ(ref IPointCollection pPointCollection)
        {
            int ZCount;
            double ZValue = 0;
            ZCount = 0;

            for (int i = 0; i <= pPointCollection.PointCount - 1; i++) //统计Z值有效的点的个数
            {

                if (pPointCollection.get_Point(i).Z != -1000 && pPointCollection.get_Point(i).Z != -999 && pPointCollection.get_Point(i).Z != 0)
                {
                    ZCount++;
                    ZValue = pPointCollection.get_Point(i).Z;
                }

            }

            IPoint pPt;
            pPt = new Point();
            IZAware pZA;
            pZA = (IZAware)pPt; //使点可以有Z值
            pZA.ZAware = true;
            IMAware pMA;
            pMA = (IMAware)pPt;
            pMA.MAware = true;

            if (ZCount == 1 || ZCount == 0) //只有一个点的Z值是有效的，则将该点的Z值附给其他点；如果整条线都没高则所有点Z值都变成0
            {
                for (int i = 0; i <= pPointCollection.PointCount - 1; i++)
                {
                    pPt.X = pPointCollection.get_Point(i).X;
                    pPt.Y = pPointCollection.get_Point(i).Y;
                    pPt.Z = ZValue;
                    pPt.M = pPointCollection.get_Point(i).M;
                    pPointCollection.UpdatePoint(i, pPt);
                }
            }

            if (ZCount > 1) //只有两个点的或两个以上的点的Z值是有效的，则进行内插
            {
                int FirstPointIndex;
                int LastPointIndex;
                int NextPointIndex;
                FirstPointIndex = FindFirstPointHasZ(pPointCollection);
                NextPointIndex = FindNextPointHasZ(FirstPointIndex, pPointCollection);
                LastPointIndex = FindLastPointHasZ(pPointCollection);

                while (!(ZCount == 1))
                {
                    int index;
                    double dist;
                    double deltaH;
                    double tempDist;
                    tempDist = 0;
                    dist = PointIndex1ToPointIndex2Distance(FirstPointIndex, NextPointIndex, pPointCollection); //求总距离
                    deltaH = pPointCollection.get_Point(NextPointIndex).Z - pPointCollection.get_Point(FirstPointIndex).Z; //求高差

                    for (index = FirstPointIndex + 1; index <= NextPointIndex - 1; index++) //给中间无高点赋高程值
                    {
                        tempDist = tempDist + GetDistance_P12(pPointCollection.get_Point(index - 1), pPointCollection.get_Point(index));
                        IPoint tempPoint;
                        tempPoint = new Point();
                        pZA = (IZAware)tempPoint; //使点可以有Z值
                        pZA.ZAware = true;
                        tempPoint.X = pPointCollection.get_Point(index).X;
                        tempPoint.Y = pPointCollection.get_Point(index).Y;
                        tempPoint.Z = pPointCollection.get_Point(FirstPointIndex).Z + (deltaH / dist) * tempDist;
                        tempPoint.M = pPointCollection.get_Point(index).M;
                        pPointCollection.UpdatePoint(index, tempPoint);

                    }

                    FirstPointIndex = NextPointIndex;
                    NextPointIndex = FindNextPointHasZ(NextPointIndex, pPointCollection);
                    ZCount--;
                }

                //求第一段的Z值
                int FirstPointIndex1;
                FirstPointIndex1 = FindFirstPointHasZ(pPointCollection);

                double dist1;
                double deltaH1;
                double tempDist1;
                tempDist1 = 0;
                dist1 = PointIndex1ToPointIndex2Distance(FirstPointIndex1, FirstPointIndex1 + 1, pPointCollection); //
                deltaH1 = pPointCollection.get_Point(FirstPointIndex1).Z - pPointCollection.get_Point(FirstPointIndex1 + 1).Z; //求高差

                int index1;
                index1 = FirstPointIndex1 - 1;

                while (!(index1 == -1)) //给中间无高点赋高程值
                {
                    tempDist1 = tempDist1 + GetDistance_P12(pPointCollection.get_Point(index1), pPointCollection.get_Point(index1 + 1));
                    IPoint tempPoint1;
                    tempPoint1 = new Point();
                    pZA = (IZAware)tempPoint1; //使点可以有Z值
                    pZA.ZAware = true;
                    tempPoint1.X = pPointCollection.get_Point(index1).X;
                    tempPoint1.Y = pPointCollection.get_Point(index1).Y;
                    tempPoint1.Z = pPointCollection.get_Point(FirstPointIndex1).Z + (deltaH1 / dist1) * tempDist1;
                    tempPoint1.M = pPointCollection.get_Point(index1).M;
                    pPointCollection.UpdatePoint(index1, tempPoint1);

                    index1--;
                }


                //求最后一段的Z值
                int LastPointIndex2;
                LastPointIndex2 = FindLastPointHasZ(pPointCollection);

                double dist2;
                double deltaH2;
                double tempDist2;
                tempDist2 = 0;
                dist2 = PointIndex1ToPointIndex2Distance(LastPointIndex2 - 1, LastPointIndex2, pPointCollection); //求总距离
                deltaH2 = pPointCollection.get_Point(LastPointIndex2).Z - pPointCollection.get_Point(LastPointIndex2 - 1).Z; //求高差

                int index2;
                for (index2 = LastPointIndex2 + 1; index2 <= pPointCollection.PointCount - 1; index2++) //给中间无高点赋高程值
                {
                    tempDist2 = tempDist2 + GetDistance_P12(pPointCollection.get_Point(index2 - 1), pPointCollection.get_Point(index2));
                    IPoint tempPoint2;
                    tempPoint2 = new Point();
                    pZA = (IZAware)tempPoint2; //使点可以有Z值
                    pZA.ZAware = true;
                    tempPoint2.X = pPointCollection.get_Point(index2).X;
                    tempPoint2.Y = pPointCollection.get_Point(index2).Y;
                    tempPoint2.Z = pPointCollection.get_Point(LastPointIndex2).Z + (deltaH2 / dist2) * tempDist2;
                    tempPoint2.M = pPointCollection.get_Point(index2).M;
                    pPointCollection.UpdatePoint(index2, tempPoint2);

                }

            }
        }


        public static double PointIndex1ToPointIndex2Distance(int index1, int index2, IPointCollection pPointCollection)
        {
            double returnValue;
            int i;
            double totalDis;
            totalDis = 0;
            for (i = index1; i <= index2 - 1; i++)
            {
                totalDis = totalDis + GetDistance_P12(pPointCollection.get_Point(i), pPointCollection.get_Point(i + 1));
            }

            returnValue = totalDis;

            return returnValue;
        }


        public static int FindFirstPointHasZ(IPointCollection pPointCollection)
        {
            int returnValue = -1;
            int i;
            for (i = 0; i <= pPointCollection.PointCount - 1; i++)
            {
                if (pPointCollection.get_Point(i).Z != -1000 && pPointCollection.get_Point(i).Z != -999 && pPointCollection.get_Point(i).Z != 0)
                {
                    returnValue = i;
                    break;
                }
            }
            return returnValue;

        }


        public static int FindNextPointHasZ(int i, IPointCollection pPointCollection)
        {
            int returnValue = -1;
            int NextPointIndex;
            for (NextPointIndex = i + 1; NextPointIndex <= pPointCollection.PointCount - 1; NextPointIndex++)
            {
                if (pPointCollection.get_Point(NextPointIndex).Z != -1000 && pPointCollection.get_Point(NextPointIndex).Z != -999 && pPointCollection.get_Point(NextPointIndex).Z != 0)
                {
                    returnValue = NextPointIndex;
                    break;
                }
            }
            return returnValue;
        }


        public static int FindLastPointHasZ(IPointCollection pPointCollection)
        {
            int returnValue = -1;
            int i;
            for (i = pPointCollection.PointCount - 1; i >= 0; i--)
            {
                if (pPointCollection.get_Point(i).Z != -1000 && pPointCollection.get_Point(i).Z != -999 && pPointCollection.get_Point(i).Z != 0)
                {
                    returnValue = i;
                    break;
                }
            }
            return returnValue;
        }
        #endregion

        #region 判断输入的字符串是否是一个数字  2007.10.18 TianK 添加
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClipboardText"></param>
        /// <returns></returns>	
        public static bool MatchNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex objNumberPattern = new System.Text.RegularExpressions.Regex("[^0-9.-]");
            return !objNumberPattern.IsMatch(strNumber);
        }
        #endregion

        #region//根据已有注记类生成相应注记类
        /// <summary>
        ///  根据已有注记类生成相应注记类
        /// </summary>
        /// <param name="pWorkSpace">注记类的保存工作空间</param>
        /// <param name="inFClass">已有注记类</param>
        /// <param name="pFDs">注记类所属要素集</param>
        /// <param name="strAnnoClassName">注记类名称</param>
        /// <returns>生成的注记类(IFeatureClass)</returns>
        public static IFeatureClass NewAnnoClass(IWorkspace pWorkSpace, IFeatureClass inFeatureClass, IFeatureDataset pFDs, string strAnnoClassName)
        {
            IFeatureClass pTempFeatureClass = null;

            ESRI.ArcGIS.Geodatabase.IFeatureWorkspaceAnno pFWSAnno = pWorkSpace as ESRI.ArcGIS.Geodatabase.IFeatureWorkspaceAnno;
            ESRI.ArcGIS.Carto.IAnnoClass pAnnoClass = inFeatureClass.Extension as ESRI.ArcGIS.Carto.IAnnoClass;

            ESRI.ArcGIS.Carto.IGraphicsLayerScale pGLS = new ESRI.ArcGIS.Carto.GraphicsLayerScaleClass();
            pGLS.ReferenceScale = pAnnoClass.ReferenceScale;
            pGLS.Units = pAnnoClass.ReferenceScaleUnits;

            pTempFeatureClass = pFWSAnno.CreateAnnotationClass(
                strAnnoClassName,//inFeatureClass.AliasName, 
                GetFieldsFormFeatureClass(inFeatureClass),
                inFeatureClass.CLSID,
                inFeatureClass.EXTCLSID,
                inFeatureClass.ShapeFieldName,
                "",
                pFDs, null,
                pAnnoClass.AnnoProperties,
                pGLS,
                pAnnoClass.SymbolCollection,
                true);

            return pTempFeatureClass;

        }
        #endregion

        #region //获得地图的工作空间
        public static IFeatureWorkspace GetFeatureWorkspaceFromMap(IMap pMap)
        {
            if (pMap == null) return null;
            if (pMap.LayerCount < 1) return null;

            IFeatureWorkspace pFeatureWorkspace = null;

            for (int i = 0; i < pMap.LayerCount; i++)
            {
                GetFeatureWorkspaceFromLayer(pMap.get_Layer(i), ref pFeatureWorkspace);
            }

            return pFeatureWorkspace;
        }
        #endregion

        #region//获得图层的工作空间
        public static void GetFeatureWorkspaceFromLayer(ILayer pLayer, ref IFeatureWorkspace pFeatureWorkspace)
        {
            IFeatureLayer pFeatureLayer;
            IDataset pDataset;
            ICompositeLayer pComp;

            if (pLayer is IGroupLayer)//如果是图层组
            {
                pComp = (ICompositeLayer)pLayer;
                for (int i = 0; i < pComp.Count; i++)
                {
                    GetFeatureWorkspaceFromLayer(pComp.get_Layer(i), ref pFeatureWorkspace);
                }
            }
            else
            {
                if (pLayer is FeatureLayer)//如果是地理要素图层
                {
                    pFeatureLayer = (IFeatureLayer)pLayer;//跳转到IFeatureLayer接口
                    if (pFeatureLayer.FeatureClass == null) return;
                    pDataset = (IDataset)pFeatureLayer.FeatureClass;//跳转到IDataset接口
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass ||
                        pDataset.Type == esriDatasetType.esriDTFeatureDataset)//如果数据集是要素类或要素数据集
                    {
                        pFeatureWorkspace = (IWorkspaceEdit)pDataset.Workspace as IFeatureWorkspace;//跳转到IFeatureWorkspace接口

                        return;
                    }
                }
            }
        }

        #endregion

        #region//将一个地图、图层组中的所有可显示图层加到一个IList中
        /// <summary>
        ///  将一个地图、图层组中的所有可显示图层加到一个IList中  2007.11.18 TianK 添加
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="list"></param>
        public static void AddLayerVisibleToIArray(object obj, ref IArray list)
        {
            if (obj is IMap)
            {
                IMap pMap = obj as IMap;
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    AddLayerVisibleToIArray(pMap.get_Layer(i), ref list);
                }
            }
            else if (obj is IGroupLayer)
            {
                if ((obj as IGroupLayer).Visible == true)
                {
                    ICompositeLayer comLayer = obj as ICompositeLayer;

                    for (int i = 0; i < comLayer.Count; i++)
                    {
                        AddLayerVisibleToIArray(comLayer.get_Layer(i), ref list);
                    }
                }
            }
            else if (obj is IFeatureLayer)
            {
                if (list != null && (obj as ILayer).Visible == true)
                {
                    list.Add(((obj as IFeatureLayer).FeatureClass as IDataset).Name);
                }
            }
        }
        #endregion

        #region//获得个人地理数据库中和指定比例尺地形图相关的所有要素类的名称   2013.9.6  TianKuo  添加
        /// 获得个人地理数据库中要素类的名称
        /// </summary>
        /// <param name="pAccessWorkSpace">个人地理数据库工作空间</param>
        /// <returns pArrayFeatureClassName>个人地理数据库中要素类的名称</returns>
        public static ArrayList GetFeactureClassName_From_AccessWorkSpace(IWorkspace pAccessWorkSpace, string scale)
        {
            ArrayList pArrayFeatureClassName = new ArrayList();
            string name = "";
            string REGIONPLY500 = "RegionMapPLY500";
            string CARTOBORDER = "Carto";
            string GCD = "GCD";

            //遍历直接位于地理数据库下的要素类FeatureClass
            IEnumDatasetName enumDatasetName = ((IWorkspace)pAccessWorkSpace).get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                name = datasetName.Name.ToString();
                if ((name.IndexOf(scale) > 0 && name != REGIONPLY500 && name.IndexOf(CARTOBORDER) <= -1) || name == GCD)
                {
                    pArrayFeatureClassName.Add(name);
                }

                datasetName = enumDatasetName.Next();//推进器
            }

            //遍历位于地理数据库数据集featuredataset下的要素类
            enumDatasetName = ((IWorkspace)pAccessWorkSpace).get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                IFeatureDatasetName featureDatasetName = (IFeatureDatasetName)datasetName;
                IEnumDatasetName enumDatasetNameFC = featureDatasetName.FeatureClassNames;
                IDatasetName datasetNameFC = enumDatasetNameFC.Next();
                while (datasetNameFC != null)
                {
                    name = datasetNameFC.Name.ToString();
                    if ((name.IndexOf(scale) > 0 && name != REGIONPLY500 && name.IndexOf(CARTOBORDER) <= -1) || name == GCD)
                    {
                        pArrayFeatureClassName.Add(name);
                    }
                    datasetNameFC = enumDatasetNameFC.Next();//推进器
                }
                datasetName = enumDatasetName.Next();
            }

            return pArrayFeatureClassName;
        }
        #endregion

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

        #region //改算到管线中心线高程的修改值
        public static double GetDeltaZOfPipe(IFeature pFeature)
        {
            Console.WriteLine(pFeature.Fields.get_Field(pFeature.Fields.FindField("GeoObjNum")));

            double DeltaZOfPipe = 0;//改算到管线中心线高程的修改值

            int indexOfGeoObjNum = pFeature.Fields.FindField("GeoObjNum");
            string strGeoObjNum = "";
            if (indexOfGeoObjNum > -1)
            {
                strGeoObjNum = pFeature.get_Value(indexOfGeoObjNum).ToString();
            }

            string strMdbFile = Application.StartupPath + @"\..\Support\" + "管线高程类型.mdb";
            ITable pTable = GetAccessTable(strMdbFile, "管线高程类型");
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.SubFields = "编码,高程值类型";
            pQueryFilter.WhereClause = "编码" + "=" + strGeoObjNum;
            ICursor pCursor = pTable.Search(pQueryFilter, false);
            IRow pRow = pCursor.NextRow();

            string strFlag = "";

            if (pRow != null)
            {
                strFlag = pRow.get_Value(pRow.Fields.FindField("高程值类型")).ToString();
            }

            int indexStandard = pFeature.Fields.FindField("Standard");
            double Standard = 0;//管线的规格
            if (indexStandard > -1)
            {
                string strStandard = pFeature.get_Value(indexStandard).ToString();
                int indexX = -1;
                indexX = strStandard.IndexOf("X", 0);
                if (indexX == -1)
                {
                    indexX = strStandard.IndexOf("x", 0);
                }

                if (indexX == -1)
                {
                    indexX = strStandard.IndexOf("*", 0);
                }

                if (indexX > 0) // 为方沟或隧道等表达为“宽X深（高）”格式
                {
                    Standard = double.Parse(strStandard.Substring(indexX + 1, strStandard.Length - indexX - 1));
                }
                else
                {
                    if (strStandard != "")
                    {
                        Standard = double.Parse(pFeature.get_Value(indexStandard).ToString());
                    }
                }

                Standard = Standard / 2 / 1000;
            }


            if (strFlag == "中心")
            {
                DeltaZOfPipe = 0;
            }
            else if (strFlag == "内底")
            {
                DeltaZOfPipe = Standard;

            }
            else if (strFlag == "外顶")
            {
                DeltaZOfPipe = 0 - Standard; ;
            }

            return DeltaZOfPipe;
        }
        #endregion

        #region //计算点到直线的垂足坐标
        /// <summary>
        /// 计算点到直线的垂足坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="pLinePnt1"></param>
        /// <param name="pLinePnt2"></param>
        /// <param name="pPoint"></param>
        public static void Perpendicular(IPoint p1, IPoint pLinePnt1, IPoint pLinePnt2, ref IPoint pPoint)
        {
            double k;
            double X, Y;

            if (pLinePnt1.X == pLinePnt2.X)
            {
                pPoint.PutCoords(pLinePnt1.X, p1.Y);
            }
            else if (pLinePnt1.Y == pLinePnt2.Y)
            {
                pPoint.PutCoords(p1.X, pLinePnt1.Y);
            }
            else
            {
                k = (pLinePnt2.Y - pLinePnt1.Y) / (pLinePnt2.X - pLinePnt1.X);
                X = (k * k * pLinePnt1.X + k * (p1.Y - pLinePnt1.Y) + p1.X) / (k * k + 1);
                Y = k * (X - pLinePnt1.X) + pLinePnt1.Y;

                pPoint.PutCoords(X, Y);
            }
        }
        #endregion

        #region 根据查询条件和结果保存方式刷新图层的选择集
        /// <summary>
        /// 根据查询条件和结果保存方式刷新图层的选择集
        /// </summary>
        /// <param name="pFeaLay"></param>
        /// <param name="pFilter"></param>
        /// <param name="selOption"></param>
        public static void FeatureLayerSelect(IFeatureLayer pFeaLay, IQueryFilter pFilter, esriSelectionResultEnum pResultMethod)
        {
            try
            {
                if (pFeaLay == null) return;


                IFeatureSelection pFeaSel = null;
                ISelectionSet pSelSet = null;
                IEnumIDs IDs;
                ArrayList oldSelection = new ArrayList();
                pFeaSel = pFeaLay as IFeatureSelection;
                pSelSet = pFeaSel.SelectionSet;
                IDs = pSelSet.IDs;

                if (pFeaLay.Visible && pFeaLay.Selectable)
                {
                    IDs.Reset();
                    int tmpFeaOID = IDs.Next();
                    while (tmpFeaOID != -1)
                    {
                        oldSelection.Add(tmpFeaOID);
                        tmpFeaOID = IDs.Next();
                    }

                    IFeatureCursor pFeaCur = pFeaLay.Search(pFilter, false);
                    IFeature pFeature;
                    pFeature = pFeaCur.NextFeature();
                    ArrayList result = new ArrayList();
                    while (pFeature != null)
                    {
                        result.Add(pFeature.OID);
                        pFeature = pFeaCur.NextFeature();
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeaCur);
                    switch (pResultMethod)
                    {
                        case esriSelectionResultEnum.esriSelectionResultNew://新选择集            
                            for (int i = 0; i < oldSelection.Count; i++)
                            {
                                tmpFeaOID = (int)oldSelection[i];
                                pSelSet.RemoveList(1, ref tmpFeaOID);
                                tmpFeaOID = IDs.Next();
                            }
                            for (int i = 0; i < result.Count; i++)
                            {
                                pSelSet.Add((int)result[i]);
                            }
                            break;
                        case esriSelectionResultEnum.esriSelectionResultAdd://加入选择集
                            for (int i = 0; i < result.Count; i++)
                            {
                                pSelSet.Add((int)result[i]);
                            }
                            break;
                        case esriSelectionResultEnum.esriSelectionResultSubtract://从选择集中删除
                            for (int i = 0; i < result.Count; i++)
                            {
                                tmpFeaOID = (int)result[i];
                                pSelSet.RemoveList(1, ref tmpFeaOID);
                            }
                            break;
                        case esriSelectionResultEnum.esriSelectionResultAnd://从选择集中查询
                            ICursor pCur;
                            pSelSet.Search(pFilter, false, out pCur);
                            for (int i = 0; i < oldSelection.Count; i++)
                            {
                                tmpFeaOID = (int)oldSelection[i];
                                pSelSet.RemoveList(1, ref tmpFeaOID);
                                tmpFeaOID = IDs.Next();
                            }
                            IRow pRow = pCur.NextRow();
                            while (pRow != null)
                            {
                                pSelSet.Add(pRow.OID);
                                pRow = pCur.NextRow();
                            }
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCur);

                            break;
                    }
                }
                //pFeaSel.SelectionColor =  PublicFunction.GetSelectionColor(selOption.DefaultColorRGB);
                pFeaSel.SelectionSet = pSelSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FeatureLayerSelect出错！" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        #endregion

        public static IFields GetFieldsFormFeatureClass(IFeatureClass pFeatureClass, ISpatialReference pSr)
        {
            int index = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureClass.Fields.get_Field(index).GeometryDef;

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                IField pSourceField = pFeatureClass.Fields.get_Field(i);
                if (pFeatureClass.Fields.get_Field(i).Name == "shape" || pFeatureClass.Fields.get_Field(i).Name == "SHAPE")
                {
                    //pField_SHAPE
                    IGeometryDef geometryDef = new GeometryDefClass();
                    IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                    geometryDefEdit.GeometryType_2 = pFeatureClass.ShapeType;  //esriGeometryType.esriGeometryPolyline;
                    geometryDefEdit.GridCount_2 = 1;
                    geometryDefEdit.set_GridSize(0, 1000);
                    geometryDefEdit.AvgNumPoints_2 = 2;
                    geometryDefEdit.SpatialReference_2 = pSr;
                    if (pGD.HasM)
                    {
                        geometryDefEdit.HasM_2 = true;
                    }
                    else
                    {
                        geometryDefEdit.HasM_2 = false;
                    }
                    if (pGD.HasZ)
                    {
                        geometryDefEdit.HasZ_2 = true;
                    }
                    else
                    {
                        geometryDefEdit.HasZ_2 = false;
                    }
                    // geometryDefEdit.SpatialReference_2 = m_pSpatialReference ;

                    IFieldEdit pField_SHAPE = new FieldClass();
                    pField_SHAPE.Name_2 = "SHAPE";
                    pField_SHAPE.AliasName_2 = "SHAPE";
                    pField_SHAPE.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    pField_SHAPE.IsNullable_2 = true;
                    pField_SHAPE.GeometryDef_2 = geometryDef;
                    pFieldsEdit.AddField((IField)pField_SHAPE);
                }
                else
                {
                    pFieldsEdit.AddField(pFeatureClass.Fields.get_Field(i));
                }
            }

            return pFields;
        }

        #region 根据要素类返回要素的字段
        public static IFields GetFieldsFormFeatureClass(IFeatureClass pFeatureClass)
        {
            int index = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
            IGeometryDef pGD = pFeatureClass.Fields.get_Field(index).GeometryDef;

            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                IField pSourceField = pFeatureClass.Fields.get_Field(i);
                if (pFeatureClass.Fields.get_Field(i).Name == "shape" || pFeatureClass.Fields.get_Field(i).Name == "SHAPE")
                {
                    //pField_SHAPE
                    IGeometryDef geometryDef = new GeometryDefClass();
                    IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                    geometryDefEdit.GeometryType_2 = pFeatureClass.ShapeType;//esriGeometryType.esriGeometryPolyline;
                    geometryDefEdit.GridCount_2 = 1;
                    geometryDefEdit.set_GridSize(0, 1000);
                    geometryDefEdit.AvgNumPoints_2 = 2;
                    if (pGD.HasM)
                    {
                        geometryDefEdit.HasM_2 = true;
                    }
                    else
                    {
                        geometryDefEdit.HasM_2 = false;
                    }
                    if (pGD.HasZ)
                    {
                        geometryDefEdit.HasZ_2 = true;
                    }
                    else
                    {
                        geometryDefEdit.HasZ_2 = false;
                    }
                    //geometryDefEdit.SpatialReference_2 = m_pSpatialReference;

                    IFieldEdit pField_SHAPE = new FieldClass();
                    pField_SHAPE.Name_2 = "SHAPE";
                    pField_SHAPE.AliasName_2 = "SHAPE";
                    pField_SHAPE.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    pField_SHAPE.IsNullable_2 = true;
                    pField_SHAPE.GeometryDef_2 = geometryDef;
                    pFieldsEdit.AddField((IField)pField_SHAPE);
                }
                else
                {
                    pFieldsEdit.AddField(pFeatureClass.Fields.get_Field(i));
                }
            }

            return pFields;
        }
        # endregion

        #region 得到在pWorkspace中数据集的名称
        /// <summary>
        /// 得到在pWorkspace中数据集的名称
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="pMapControl"></param>
        /// <returns></returns>
        public static string[] getFeatureDataset(IWorkspace pWorkspace)
        {
            ArrayList pArrayList = new ArrayList();

            IEnumDatasetName pEnumDatasetName = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            pEnumDatasetName.Reset();
            IDatasetName pDatasetName = pEnumDatasetName.Next();
            while (pDatasetName != null)
            {
                pArrayList.Add(pDatasetName.Name);
                pDatasetName = pEnumDatasetName.Next();
            }

            return (string[])pArrayList.ToArray(typeof(string));
        }
        #endregion

        #region 得到在pWorkspace中要素类的名称
        /// <summary>
        /// 得到在pWorkspace中要素类的名称
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="pMapControl"></param>
        /// <returns></returns>
        public static string[] getFeatureClassName(IWorkspace pWorkspace)
        {
            ArrayList pArrayList = new ArrayList();

            IEnumDatasetName pEnumDatasetName = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            pEnumDatasetName.Reset();
            IDatasetName pDatasetName = pEnumDatasetName.Next();
            while (pDatasetName != null)
            {
                pArrayList.Add(pDatasetName.Name);
                pDatasetName = pEnumDatasetName.Next();
            }

            return (string[])pArrayList.ToArray(typeof(string));
        }
        #endregion

        #region 得到在pWorkspace中数据集的名称
        /// <summary>
        /// 得到在pWorkspace中数据集的名称
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="pMapControl"></param>
        /// <returns></returns>
        public static string[] getFeatureClass(IWorkspace pWorkspace)
        {
            ArrayList pArrayList = new ArrayList();

            IEnumDatasetName pEnumDatasetName = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            pEnumDatasetName.Reset();
            IDatasetName pDatasetName = pEnumDatasetName.Next();
            while (pDatasetName != null)
            {
                pArrayList.Add(pDatasetName.Name);
                pDatasetName = pEnumDatasetName.Next();
            }

            return (string[])pArrayList.ToArray(typeof(string));
        }
        #endregion

        #region 在指定工作空间内创建图层
        /// <summary> 
        /// 在指定工作空间内创建图层
        /// </summary>
        /// <param name="objectWorkspace">创建图层的工作空间</param>
        /// <param name="name">图层的名称</param>
        /// <param name="spatialReference">图层的空间参考信息</param>
        /// <returns>新建立的要素类(IFeatureClass)</returns>
        public static IFeatureClass CreateFeatureClass(object objectWorkspace, ISpatialReference spatialReference, IFields pFields, string name)
        {
            if (objectWorkspace == null)
            {
                throw (new Exception("[objectWorkspace] cannot be null"));
            }
            if (!((objectWorkspace is IWorkspace) ||
                (objectWorkspace is IFeatureDataset)))
            {
                throw (new Exception("[objectWorkspace] must be IWorkspace or IFeatureDataset"));
            }
            if (name == "")
            {
                throw (new Exception("[name] cannot be empty"));
            }
            if ((objectWorkspace is IWorkspace) && (spatialReference == null))
            {
                throw (new Exception("[spatialReference] cannot be null for StandAlong FeatureClasses"));
            }

            // Locate Shape Field
            string stringShapeFieldName = "";
            for (int i = 0; i <= pFields.FieldCount - 1; i++)
            {
                if (pFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    stringShapeFieldName = pFields.get_Field(i).Name;
                    break;
                }
            }
            if (stringShapeFieldName == "")
            {
                throw (new Exception("Cannot locate geometry field in FIELDS"));
            }

            IFeatureClass featureClass = null;

            if (objectWorkspace is IWorkspace)
            {
                // Create a STANDALONE FeatureClass
                IWorkspace workspace = (IWorkspace)objectWorkspace;
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                featureClass = featureWorkspace.CreateFeatureClass(name, pFields, null, null, esriFeatureType.esriFTSimple, stringShapeFieldName, null);
            }
            else if (objectWorkspace is IFeatureDataset)
            {
                //创建Dataset下的FeatureClass
                IFeatureDataset featureDataset = (IFeatureDataset)objectWorkspace;
                featureClass = featureDataset.CreateFeatureClass(name, pFields, null, null, esriFeatureType.esriFTSimple, stringShapeFieldName, null);
            }

            // Return FeatureClass
            return featureClass;
        }

        #endregion
    }
}
