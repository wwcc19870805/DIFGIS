using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using DFDataConfig.Class;
using DF2DData.Class;
using DevExpress.XtraEditors;
using DFAlgorithm.Network;
using DF2DAnalysis.Class;
using DFCommon.Class;
using System.Windows.Forms;
using DFWinForms.Class;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using DF2DPipe.Class;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Collections;
using System.Drawing;
using DF2DAnalysis.Frm;

namespace DF2DAnalysis.Commands
{
    class CmdPipeTransection : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private List<IntersectPoint> interPoints;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            interPoints = new List<IntersectPoint>();
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null) return;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            IGeometry pGeo = null;
            IPointCollection pIntersectPC;
            IPointCollection pLandSufPC;
            double length = 0.0;
            IPoint fPoint;
            string mapNum = "";
            string mapName = Config.GetConfigValue("SystemName") + "横断面图";
            try
            {
                if (button == 1)
                {
                    ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
                    IRgbColor pColor = new RgbColorClass();
                    pColor.Red = 255;
                    pColor.Green = 255;
                    pColor.Blue = 0;
                    pLineSym.Color = pColor;
                    pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                    pLineSym.Width = 2;

                    IRubberBand pRubberBand;
                    pRubberBand = new RubberLineClass();
                    IGeometry pLine;
                    pLine = pRubberBand.TrackNew(m_Display, null);

                    object symbol = pLineSym as object;
                    app.Current2DMapControl.DrawShape(pLine, ref symbol);

                    WaitForm.Start("正在查询...", "请稍后");
                    //pGeo = PublicFunction.DoBuffer(pLine,PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                    pGeo = pLine;
                    if (pGeo == null) return;
                    length = (pGeo as IPolyline).Length;
                    fPoint = (pGeo as IPolyline).FromPoint;
                    mapNum = getMapNumforPnt(fPoint);

                    if (ready)
                    {
                        double distemp = 0.0;
                        IPoint tempPoint = null;
                        //foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                        foreach(MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                        {                                                       
                            foreach (SubClass sc in mc.SubClasses)
                            {
                                if (!sc.Visible2D) continue;
                                string[] arrFc2DId = mc.Fc2D.Split(';');
                                if (arrFc2DId == null) continue;
                                IFeatureCursor pFeatureCursor = null;
                                IFeature pFeature = null;
                                foreach (string fc2DId in arrFc2DId)
                                {
                                    DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                    if (dffc == null) continue;
                                    IFeatureClass fc = dffc.GetFeatureClass();
                                    FacilityClass facc = dffc.GetFacilityClass();
                                    if (facc.Name != "PipeLine") continue;
                                    if (fc == null || pGeo == null) continue;
                                    ISpatialFilter pSpatialFilter = new SpatialFilter();
                                    pSpatialFilter.Geometry = pGeo;
                                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                    //pSpatialFilter.WhereClause =sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                                    pSpatialFilter.WhereClause = mc.ClassifyField  + " =  '" + sc.Name + "'";
                                    int count = fc.FeatureCount(pSpatialFilter);
                                    if (count == 0) continue;
                                    pFeatureCursor = fc.Search(pSpatialFilter, false);
                                    string fcName = mc.Name;
                                    if (pFeatureCursor == null) continue;
                                    double distance;

                                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                    {
                                        IGeometry pPipeLineGeo = pFeature.Shape;
                                        ITopologicalOperator pTopo = pPipeLineGeo as ITopologicalOperator;
                                        IGeometry geo = pTopo.Intersect(pGeo, esriGeometryDimension.esriGeometry0Dimension);
                                        IPoint intersect;
                                        if (geo is IPoint)
                                        {
                                            double distanceF = double.MaxValue;
                                            intersect = geo as IPoint;
                                            if (pGeo is IPolyline)
                                            {
                                                IPolyline line = pGeo as IPolyline;
                                                IPoint fromPoint = line.FromPoint;
                                                distanceF = GetDistanceOfTwoPoints(intersect, fromPoint);
                                            }
                                            if (distemp == 0.0)
                                            {
                                                distance = distanceF;
                                                distemp = distanceF;
                                            }
                                            else
                                            {
                                                distance = Math.Abs(distanceF - distemp);
                                            }
                                            IntersectPoint interPoint = new IntersectPoint(pFeature, intersect, fcName, distance, distanceF);
                                            interPoints.Add(interPoint);
                                        }
                                        if (geo is IPointCollection)
                                        {
                                            IPointCollection geoCol = geo as IPointCollection;
                                            for (int i = 0; i < geoCol.PointCount; i++)
                                            {
                                                intersect = geoCol.get_Point(i) as IPoint;
                                                double distanceF = double.MaxValue;

                                                if (pGeo is IPolyline)
                                                {
                                                    IPolyline line = pGeo as IPolyline;

                                                    IPoint fromPoint = line.FromPoint;
                                                    IPoint toPoint = line.ToPoint;
                                                    if (distemp == 0.0)
                                                    {
                                                        double distance1 = GetDistanceOfTwoPoints(intersect, fromPoint);
                                                        double distance2 = GetDistanceOfTwoPoints(intersect, toPoint);
                                                        distanceF = distance1 < distance2 ? distance1 : distance2;
                                                        tempPoint = distance1 < distance2 ? fromPoint : toPoint;
                                                    }
                                                    else
                                                    {
                                                        if (tempPoint == null) return;
                                                        distanceF = GetDistanceOfTwoPoints(intersect, tempPoint);
                                                    }


                                                }
                                                if (distemp == 0.0)
                                                {
                                                    distance = distanceF;
                                                    distemp = distanceF;
                                                }
                                                else
                                                {
                                                    distance = Math.Abs(distanceF - distemp);
                                                }
                                                IntersectPoint interPoint = new IntersectPoint(pFeature, intersect, fcName, distance, distanceF);
                                                interPoints.Add(interPoint);
                                            }
                                        }

                                    }
                                }
                                
                            }
                        }
                    }
                }

                DrawPipeTransection(interPoints,mapNum,mapName);
            }
            catch (System.Exception ex)
            {

            }
        }
        private double GetDistanceOfTwoPoints(IPoint P1, IPoint P2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

            return Result;
        }

        private void DrawPipeTransection(List<IntersectPoint> listInterPnts,string mapNum,string mapName)
        {
            if (listInterPnts.Count < 2 || listInterPnts == null) return;
            Bitmap bmp = new Bitmap(1366, 600);
            Graphics g_bmp = Graphics.FromImage(bmp);
            g_bmp.Clear(Color.Wheat);
            if (g_bmp != null)
            {
                Draw(g_bmp, bmp.Width, bmp.Height, listInterPnts,mapNum,mapName);
                //bmp.Save("E:\\1.bmp");
                FrmPipeTransection2D dialog = new FrmPipeTransection2D(bmp);
                dialog.Show();
                WaitForm.Stop();
            }
        }

        private void Draw(Graphics g, int winWidth, int winheight, List<IntersectPoint> listInterPnts,string mapNum,string mapName)
        {
            try
            {
                Pen pen = new Pen(Color.Black, 2);
                Font font = new Font("Times New Roman", 9);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                //按距离画线起点对管线交点进行排序
                List<IntersectPoint> orderList = listInterPnts.OrderBy(i => i.DistanceF).ToList<IntersectPoint>();
                //确定竖向比例
                double minHeight = double.MaxValue;
                double maxHeight = double.MinValue;
                string road = null;
                foreach (IntersectPoint inpnt in orderList)
                {                   
                    if (inpnt.H < minHeight) minHeight = inpnt.H;
                    if (inpnt.H > maxHeight) maxHeight = inpnt.H;
                    if (inpnt.ZPipeTop < minHeight) minHeight = inpnt.ZPipeTop;
                    if (inpnt.ZPipeTop > maxHeight) maxHeight = inpnt.ZPipeTop;
                    if (inpnt.Road != null) road = inpnt.Road;
                    else
                        road = "未知";
                }
                

                //计算管线间的相互距离，并写入IntersectPoint类
                for (int i = 0; i < orderList.Count; i++)
                {
                    if (i == 0)
                        orderList[i].Distance = orderList[i].DistanceF;
                    else
                    {
                        orderList[i].Distance = orderList[i].DistanceF - orderList[i - 1].DistanceF;
                    }
                }
                double constHeight = 20;//高程常数20m
                //double constwidth = 5;//管线截面两边预留长度5m
                double totalYSpan = maxHeight - minHeight + constHeight;//画图区域竖向总高度
                double totalXSpan = orderList[orderList.Count - 1].DistanceF - orderList[0].DistanceF ;//画图区域横向总长度
                double paddingLeft = 30;
                double paddingRight = 30;
                double paddingBottom = 20;
                double paddingTop = 40;
                double canvasTotalWidth = winWidth;//图片横向总像素
                double canvasTotalHeight = winheight;//图片纵向总像素
                
               
                //绘制外图廓
                double constOutlineY = 50;//外图廓据顶部边缘距离
                double outlineXOrigin = paddingLeft;
                double outlineYOrigin = paddingTop + constOutlineY;
                double outlineWidth = canvasTotalWidth - paddingLeft - paddingRight;
                double outlineHeight = canvasTotalHeight - paddingBottom - outlineYOrigin;
                g.DrawRectangle(pen, (float)outlineXOrigin, (float)outlineYOrigin, (float)outlineWidth, (float)outlineHeight);
                Font fontTitle = new Font("宋体", 20);
                g.DrawString(mapName,fontTitle,drawBrush,new PointF((float)(canvasTotalWidth / 2) - 150, 20));
                g.DrawString("所在道路：" + road, font, drawBrush, new PointF((float)outlineXOrigin + 10, (float)outlineYOrigin - 30));
                g.DrawString("断面号：" + mapNum, font, drawBrush, new PointF((float)(canvasTotalWidth - paddingRight - 170), (float)outlineYOrigin - 30));


                //绘制表格
                double constTable = 30;
                double tableHeight = 150;
                int rowCount = 5;
                int rowHeight = (int)tableHeight / rowCount;
                double tableXOrigin = outlineXOrigin + constTable;
                double tableYOrigin = canvasTotalHeight - tableHeight - paddingBottom;
                double tableWidth = outlineWidth - 2*constTable;
                g.DrawRectangle(pen, (int)tableXOrigin, (int)tableYOrigin, (float)tableWidth, (float)tableHeight);
                g.DrawString("地面高程(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 0 + 10);
                g.DrawString("管线高程(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 1 + 10);
                g.DrawString(" 规  格(mm)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 2 + 10);
                g.DrawString(" 间  距(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 3 + 10);
                float constCol = 100;//标题栏右边线
                g.DrawLine(pen, new PointF((float)tableXOrigin + constCol, (float)tableYOrigin), new PointF((float)tableXOrigin + constCol, (float)(tableYOrigin + tableHeight)));
                for (int i = 1; i < rowCount; i++)
                {
                    pen.Width = 1;
                    g.DrawLine(pen, (float)tableXOrigin, (float)tableYOrigin + rowHeight * i, (float)(tableXOrigin + tableWidth), (float)tableYOrigin + rowHeight * i);
                }

                //绘制画图区域
                double constCan = constCol + 50;
                double constCanY = 80;
                double constCanBlank = 100;//第一根管线距左侧边线的屏幕距离
                double canXOrigin = tableXOrigin + constCan;
                double canYOrigin = outlineYOrigin + constCanY;
                double canWidth = canvasTotalWidth - canXOrigin - paddingRight - constCol ;
                //double canHeight = canvasTotalHeight - outlineYOrigin - paddingBottom - constCanY;
                double canHeight = canvasTotalHeight - outlineYOrigin - paddingBottom - constCanY - tableHeight;
                double xRadio = totalXSpan * 1000 / (canWidth - 2*constCanBlank);
                double yRadio = totalYSpan * 1000 / (canHeight);
                //确定管线在画图区域的相对位置,转换为屏幕距离，并写入IntersectPoint类
                for (int i = 0; i < orderList.Count; i++)
                {
                    if (i == 0)
                    {
                        orderList[i].DistanceDraw = constCanBlank;

                    }
                    else
                    {
                        orderList[i].DistanceDraw = orderList[i].Distance * 1000 / xRadio + orderList[i - 1].DistanceDraw;
                    }
                    
                }
                //计算管线交点及地面高在画图区域的相对高程 
                double dH = maxHeight - minHeight;
                foreach (IntersectPoint intPnt in orderList)
                {
                    intPnt.ZDraw = (intPnt.ZPipeTop / maxHeight) * (dH + constHeight);
                    intPnt.HDraw = (intPnt.H / maxHeight) * (dH + constHeight);
                }
                g.DrawString("比例尺", font, drawBrush, (float)(canvasTotalWidth / 2) - 80, (float)outlineYOrigin + 40);
                g.DrawString("水平 1:" + Math.Ceiling(xRadio).ToString(), font, drawBrush, (float)(canvasTotalWidth / 2) - 20, (float)outlineYOrigin + 30);
                g.DrawString("垂直 1:" + Math.Ceiling(yRadio).ToString(), font, drawBrush, (float)(canvasTotalWidth / 2) - 20, (float)outlineYOrigin + 50);
                pen.Width = 2;
                PointF startP = new PointF((float)canXOrigin, (float)canYOrigin);
                PointF endP = new PointF((float)(canXOrigin + canWidth), (float)canYOrigin);
                g.DrawLine(pen, startP, new PointF((float)canXOrigin, (float)(canvasTotalHeight - paddingBottom)));
                g.DrawLine(pen, endP, new PointF((float)(canXOrigin + canWidth), (float)(canvasTotalHeight - paddingBottom)));

                //方向标
                pen.Width = 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                g.DrawLine(pen, new PointF((float)(canXOrigin + canWidth - 160), (float)outlineYOrigin + 40), new PointF((float)(canXOrigin + canWidth - 80), (float)outlineYOrigin + 40));
                g.DrawLine(pen, new PointF((float)(canXOrigin + canWidth - 100), (float)outlineYOrigin + 30), new PointF((float)(canXOrigin + canWidth - 80), (float)outlineYOrigin + 40));
                g.DrawString("S", font, drawBrush, new PointF((float)(canXOrigin + canWidth - 120), (float)outlineYOrigin + 25));

                //绘制地面线
                g.DrawLine(pen, startP, new PointF((float)(orderList[0].DistanceDraw + canXOrigin), (float)((totalYSpan - orderList[0].HDraw) * 1000 / yRadio + canYOrigin)));//竖向因坐标方向与高程方向相反，需另行转化屏幕距离
                g.DrawLine(pen, endP, new PointF((float)(orderList[orderList.Count - 1].DistanceDraw + canXOrigin), (float)((totalYSpan - orderList[orderList.Count - 1].HDraw) * 1000 / yRadio + canYOrigin)));
                double x0, y0, x1, y1;
                for (int i = 1; i < orderList.Count; i++)
                {
                    x0 = orderList[i - 1].DistanceDraw + canXOrigin;
                    x1 = orderList[i].DistanceDraw + canXOrigin;
                    y0 = (totalYSpan - orderList[i - 1].HDraw) * 1000 / yRadio + canYOrigin;
                    y1 = (totalYSpan - orderList[i].HDraw) * 1000 / yRadio + canYOrigin;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    pen.Width = 1;
                    g.DrawLine(pen, (int)x0, (int)y0, (int)x1, (int)y1);
                }
                //绘制横断面
                double zoom = 1;
                float constfc = 20;
                float constStrY = 15;
                float constStrX = 10;
                double radioTemp = xRadio > yRadio ? xRadio : yRadio;
                for (int i = 0; i < orderList.Count;i++ )
                {
                    IntersectPoint intPnt = orderList[i]; 
                    string diameter = intPnt.Diameter;
                    double distanceDraw = intPnt.DistanceDraw;
                    double diaWidth = 0.0;
                    double diaHeight = 0.0;
                    double dia = 0.0;
                    double x;
                    double y;
                    double xline;
                    double yline;

                    RotateText rotate = new RotateText();
                    rotate.Graphics = g;
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    font = new Font("Times New Roman", 7);
                    if (intPnt.IsCircle)
                    {
                        dia = double.Parse(diameter) / zoom;
                        diaWidth = dia;
                        diaHeight = dia;
                        x = intPnt.DistanceDraw - (dia / 2) / radioTemp + canXOrigin;//获得管线截面外接矩形左上角的坐标x
                        y = (totalYSpan - intPnt.ZDraw) * 1000 / radioTemp + canYOrigin; //获得管线截面外接矩形左上角的坐标y
                        pen.Width = 1;
                        pen.Color = intPnt.Color;
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        g.DrawEllipse(pen, (float)x, (float)y, (float)(diaWidth / radioTemp), (float)(diaHeight / radioTemp));
                        pen.Color = Color.Black;
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        xline = intPnt.DistanceDraw + canXOrigin;
                        yline = y + dia / radioTemp;

                        g.DrawLine(pen, (int)xline, (int)yline, (int)xline, (int)(tableYOrigin + rowHeight * 4));

                        g.DrawString(intPnt.FcName, font, drawBrush, (float)(xline - constfc), (float)(tableYOrigin - constStrY));
                        rotate.DrawString(Math.Round(intPnt.H, 3).ToString(), font, drawBrush, new PointF((float)(xline - constStrX), (float)(tableYOrigin + rowHeight * 0 + constStrY)), format, -90f);
                        rotate.DrawString(Math.Round(intPnt.ZPipeTop - (dia * zoom) / 1000, 3).ToString(), font, drawBrush, new PointF((float)(xline - constStrX), (float)(tableYOrigin + rowHeight * 1 + constStrY)), format, -90f);
                        rotate.DrawString("DN" + intPnt.Diameter, font, drawBrush, new PointF((float)(xline - constStrX), (float)(tableYOrigin + rowHeight * 2 + constStrY)), format, -90f);
                        if (i == 0)
                        {
                            rotate.DrawString("", font, drawBrush, new PointF((float)(x - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY + 10)), format, 0f);
                        }
                        else if (intPnt.Distance < 1.0)
                            rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY + 10)), format, 0f);
                        else
                            rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY)), format, 0f);
                    }
                    else
                    {
                        string[] diameters = diameter.Split('*');
                        if (diameters != null && diameters.Length == 2)
                        {
                            diaWidth = double.Parse(diameters[0]) / zoom;
                            diaHeight = double.Parse(diameters[1]) / zoom;
                            x = intPnt.DistanceDraw - (diaWidth / 2) / radioTemp + canXOrigin;//获得管线截面外接矩形左上角的坐标x
                            y = (totalYSpan - intPnt.ZDraw) * 1000 / radioTemp + canYOrigin; //获得管线截面外接矩形左上角的坐标y
                            pen.Width = 1;
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                            pen.Color = intPnt.Color;
                            g.DrawRectangle(pen, (float)x, (float)y, (float)(diaWidth / radioTemp), (float)(diaHeight / radioTemp));
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            pen.Color = Color.Black;
                            xline = intPnt.DistanceDraw + canXOrigin;
                            yline = y + diaHeight / radioTemp;

                            g.DrawLine(pen, (int)xline, (int)yline, (int)xline, (int)(tableYOrigin + rowHeight * 4));
                            g.DrawString(intPnt.FcName, font, drawBrush, (float)(xline - constfc), (float)(tableYOrigin - constStrY));
                            rotate.DrawString(Math.Round(intPnt.H, 3).ToString(), font, drawBrush, new PointF((float)(xline - constStrX), (float)(tableYOrigin + rowHeight * 0 + constStrY)), format, -90f);
                            rotate.DrawString(Math.Round(intPnt.ZPipeTop - (diaHeight * zoom) / 1000, 3).ToString(), font, drawBrush, new PointF((float)(xline - constStrX), (float)(tableYOrigin + rowHeight * 1 + constStrY)), format, -90f);
                            rotate.DrawString(intPnt.Diameter, font, drawBrush, new PointF((float)(xline - constStrX), (float)(tableYOrigin + rowHeight * 2 + constStrY)), format, -90f);
                            if (intPnt.Distance < 1.0)
                                rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY + 10)), format, 0f);
                            else
                                rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY)), format, 0f);
                        }
                    }
                }

                g.DrawString(Math.Round(totalXSpan + 2*constCanBlank*xRadio/1000, 3).ToString(), font, drawBrush, (float)canvasTotalWidth / 2 - 20, (float)(tableYOrigin + rowHeight * 4 + 10));
                
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private string getMapNumforPnt(IPoint pPnt)
        {
            string strMapName = "";
            string strMapNameX, strMapNameY;
            double dblCoor;
            string strCoorX, strCoorY;

            strCoorX = string.Format("{0:f3}", (pPnt.X / 1000.0));
            strCoorY = string.Format("{0:f3}", (pPnt.Y / 1000.0));

            int index = strCoorX.IndexOf(".");
            string Z = strCoorX.Substring(0, index);
            string L = strCoorX.Substring(index + 1);
            dblCoor = Convert.ToInt32(L) / 100.0;

            //图幅Y
            if (dblCoor >= 0 && dblCoor < 25)
            {
                strMapNameY = Z + "." + "00";
            }
            else if (dblCoor >= 25 && dblCoor < 50)
            {
                strMapNameY = Z + "." + "25";
            }
            else if (dblCoor >= 50 && dblCoor < 75)
            {
                strMapNameY = Z + "." + "50";
            }
            else
            {
                strMapNameY = Z + "." + "75";
            }

            index = strCoorY.IndexOf(".");
            Z = strCoorY.Substring(0, index);
            L = strCoorY.Substring(index + 1);
            dblCoor = Convert.ToInt32(L) / 10.0;

            //图幅X
            if (dblCoor >= 0 && dblCoor < 25)
            {
                strMapNameX = Z + "." + "00";
            }
            else if (dblCoor >= 25 && dblCoor < 50)
            {
                strMapNameX = Z + "." + "25";
            }
            else if (dblCoor >= 50 && dblCoor < 75)
            {
                strMapNameX = Z + "." + "50";
            }
            else
            {
                strMapNameX = Z + "." + "75";
            }

            strMapName = strMapNameX + "-" + strMapNameY;
            return strMapName;
        }

        public override void RestoreEnv()
        {

            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
            if (mapView == null) return;
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
    }
}
