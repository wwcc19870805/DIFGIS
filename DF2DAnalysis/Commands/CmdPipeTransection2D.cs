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
    class CmdPipeTransection2D : AbstractMap2DCommand
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

                    if (ready)
                    {
                        double distemp = 0.0;
                        IPoint tempPoint = null;
                        foreach (LogicGroup lg in LogicDataStructureManage.Instance.RootLogicGroups)
                        {
                            foreach (MajorClass mc in lg.MajorClasses)
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
                                        pSpatialFilter.WhereClause = "SMSCODE =  '" + sc.Name + "'";
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
                                                IntersectPoint interPoint = new IntersectPoint(pFeature, intersect, fcName,distance,distanceF);
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
                                                            tempPoint = distance1 < distance2 ? fromPoint:toPoint;
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
                                                    IntersectPoint interPoint = new IntersectPoint(pFeature, intersect, fcName, distance,distanceF);
                                                    interPoints.Add(interPoint);
                                                }
                                            }
                                          
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                DrawPipeTransection(interPoints,length);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            IGraphicsContainer gc = app.Current2DMapControl.Map as IGraphicsContainer;
            gc.DeleteAllElements();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }

        private double GetDistanceOfTwoPoints(IPoint P1, IPoint P2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

            return Result;
        }

        private void Draw(Graphics g, int winWidth, int winheight, List<IntersectPoint> listInterPnts,double length)
        {
            try
            {
                if (length == 0.0) return;
                Pen pen = new Pen(Color.Black, 2);
                Font font = new Font("Times New Roman", 9);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                //比例尺计算

                double totalXSpan = length;
                double minInterval = double.MaxValue;
                double maxInterval = 0.0;
                double minHeight = double.MaxValue;
                double maxHeight = 0.0;
                string road = null;
                foreach (IntersectPoint inpnt in listInterPnts)
                {
                    //totalXSpan += inpnt.Distance;
                    if (inpnt.Distance < minInterval) minInterval = inpnt.Distance;
                    if (inpnt.Distance > maxInterval) maxInterval = inpnt.Distance;
                    if (inpnt.H < minHeight) minHeight = inpnt.H;
                    if (inpnt.H > maxHeight) maxHeight = inpnt.H;
                    if (inpnt.ZPipeTop < minHeight) minHeight = inpnt.ZPipeTop;
                    if (inpnt.ZPipeTop > maxHeight) maxHeight = inpnt.ZPipeTop;
                    if (inpnt.Road != null) road = inpnt.Road;
                    else
                        road = "未知";
                }

                //double totalYSpan = maxHeight - minHeight;
                double totalYSpan = maxHeight + 20;
                double paddingLeft = 30;
                double paddingRight = 30;
                double paddingBottom = 20;
                double paddingTop = 40;
                double canvasTotalWidth = winWidth;
                double canvasTotalHeight = winheight;
                //绘制头
                Font dontHead = new System.Drawing.Font("Tahoma", 15);
                //g.DrawString("横断面图", font, drawBrush, (float)canvasTotalWidth / 2 - 60, 80);

                
                      
 

                //绘制外图廓   
                double outlineXOrigin = paddingLeft;
                double outlineYOrigin = paddingTop + 50;
                double outlineWidth = canvasTotalWidth - paddingLeft - paddingRight;
                double outlineHeight = canvasTotalHeight - paddingBottom - outlineYOrigin;
                g.DrawRectangle(pen, (float)outlineXOrigin, (float)outlineYOrigin, (float)outlineWidth, (float)outlineHeight);
                g.DrawString("所在道路：" + road, font, drawBrush, new PointF((float)outlineXOrigin + 10, (float)outlineYOrigin - 30));
                g.DrawString("断面号：", font, drawBrush, new PointF((float)(canvasTotalWidth - paddingRight - 120), (float)outlineYOrigin - 30));

               #region 绘制表格  
                double tableHeight = 150;  
                int rowCount = 5;
                int rowHeight = (int)tableHeight / rowCount;
                double tableXOrigin = outlineXOrigin + 30;
                double tableYOrigin = canvasTotalHeight - tableHeight - paddingBottom;
                double tableWidth = outlineWidth - 60;
                g.DrawRectangle(pen, (int)tableXOrigin, (int)tableYOrigin, (float)tableWidth, (float)tableHeight);
                g.DrawString("地面高程(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 0 + 10);
                g.DrawString("管线高程(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 1 + 10);
                g.DrawString(" 规  格(mm)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 2 + 10);
                g.DrawString(" 间  距(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 3 + 10);
                g.DrawLine(pen, new PointF((float)tableXOrigin + 100, (float)tableYOrigin), new PointF((float)tableXOrigin + 100, (float)(tableYOrigin + tableHeight)));
                for (int i = 1; i < rowCount; i++)
                {
                    pen.Width = 1;
                    g.DrawLine(pen, (float)tableXOrigin, (float)tableYOrigin + rowHeight * i, (float)(tableXOrigin + tableWidth), (float)tableYOrigin + rowHeight * i);
                }
                #endregion
                //

                //绘制画图区域
                
                double canXOrigin = outlineXOrigin + 200;
                double canYOrigin = outlineYOrigin + 80;
                double canvasWidth = canvasTotalWidth - canXOrigin - paddingRight - 60;
                double canvasHeight = canvasTotalHeight - outlineYOrigin - paddingBottom - 80;
                double xRadio =  totalXSpan *1000/ (canvasWidth);
                double yRadio =  totalYSpan *1000/ (canvasHeight);
                
                g.DrawString("比例尺", font, drawBrush, (float)(canvasTotalWidth / 2) - 80, (float)outlineYOrigin + 40);
                //g.DrawString("水平 1:30", font, drawBrush, (float)(canvasTotalWidth / 2) - 20, (float)outlineYOrigin + 30);
                //g.DrawString("垂直 1:200", font, drawBrush, (float)(canvasTotalWidth / 2) - 20, (float)outlineYOrigin + 50);
                g.DrawString("水平 1:" + Math.Ceiling(xRadio).ToString(), font, drawBrush, (float)(canvasTotalWidth / 2) - 20, (float)outlineYOrigin + 30);
                g.DrawString("垂直 1:" + Math.Ceiling(yRadio).ToString(), font, drawBrush, (float)(canvasTotalWidth / 2) - 20, (float)outlineYOrigin + 50);
                
                pen.Width = 2;
                //g.DrawRectangle(pen, (int)canXOrigin, (int)canYOrigin, (int)canvasWidth, (int)canvasHeight);
                PointF startP = new PointF((float)canXOrigin, (float)canYOrigin);
                PointF endP = new PointF((float)(canXOrigin + canvasWidth), (float)canYOrigin);
                g.DrawLine(pen, startP, new PointF((float)canXOrigin, (float)(canvasTotalHeight - paddingBottom)));
                g.DrawLine(pen, endP, new PointF((float)(canXOrigin + canvasWidth), (float)(canvasTotalHeight - paddingBottom)));
                
               
                
                //方向标
                pen.Width = 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                g.DrawLine(pen, new PointF((float)(canXOrigin + canvasWidth - 160), (float)outlineYOrigin + 40), new PointF((float)(canXOrigin + canvasWidth - 80), (float)outlineYOrigin + 40));
                g.DrawLine(pen, new PointF((float)(canXOrigin + canvasWidth - 100), (float)outlineYOrigin + 30), new PointF((float)(canXOrigin + canvasWidth - 80), (float)outlineYOrigin + 40));
                g.DrawString("S", font, drawBrush, new PointF((float)(canXOrigin + canvasWidth - 120), (float)outlineYOrigin + 25));

                //绘制地面线
                double x0, y0, x1, y1;
                double maxH = double.MinValue;
                foreach (IntersectPoint intPnt in listInterPnts)
                {
                    
                    if (intPnt.H > maxH)
                    {
                        maxH = intPnt.H;
                    }
                    
                }
                List<IntersectPoint> orderList = listInterPnts.OrderBy(i => i.DistanceF).ToList<IntersectPoint>();
                g.DrawLine(pen, startP, new PointF((float)(orderList[0].DistanceF * 1000 / xRadio + canXOrigin), (float)((maxH - orderList[0].H) * 1000 / yRadio + canYOrigin)));
                g.DrawLine(pen, endP, new PointF((float)(orderList[orderList.Count - 1].DistanceF * 1000 / xRadio + canXOrigin), (float)((maxH - orderList[orderList.Count - 1].H) * 1000 / yRadio + canYOrigin)));

                for (int i = 1; i < orderList.Count; i++)
                {
                    x0 = orderList[i - 1].DistanceF * 1000 / xRadio + canXOrigin;
                    x1 = orderList[i].DistanceF * 1000 / xRadio + canXOrigin;
                    y0 = (maxH - orderList[i - 1].H) * 1000 / yRadio + canYOrigin;
                    y1 = (maxH - orderList[i].H) * 1000 / yRadio + canYOrigin;
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    pen.Width = 1;
                    g.DrawLine(pen, (int)x0, (int)y0, (int)x1, (int)y1);
                }

                //绘制横断面
                
                double maxZ = double.MinValue;
                double zoom = 1;
                //获得相交管线中管顶高最大值
                foreach (IntersectPoint intPnt in orderList)
                {
                    double diatemp;
                    double zTop = intPnt.ZPipeTop;
                    string diameter = intPnt.Diameter;
                    string[] diameters = diameter.Split('*');
                    if (diameters.Length == 1)
                    {
                        diatemp = (double.Parse(diameter) / 2)/1000;//单位化为米
                        //if (maxZ < zTop - diatemp)
                        //{
                        //    maxZ = zTop - diatemp;//maxZ为管线中心高
                        //}
                        if (maxZ < zTop)
                        {
                            maxZ = zTop;//maxZ为管线顶高
                        }
                    }
                    else
                    {
                        if (diameters.Length > 2) return;
                        diatemp = (double.Parse(diameters[1]) / 2) / 1000;//矩形截面高
                        //if (maxZ < zTop - diatemp)
                        //{
                        //    maxZ = zTop - diatemp;//同上
                        //}
                        if (maxZ < zTop)
                        {
                            maxZ = zTop;//maxZ为管线顶高
                        }
                    }
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
                foreach (IntersectPoint intPnt in orderList)
                {
                    string diameter = intPnt.Diameter;
                    double distanceF = intPnt.DistanceF;
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
                        dia = double.Parse(diameter)/zoom;
                        diaWidth = dia;
                        diaHeight = dia;
                        x = (intPnt.DistanceF * 1000 - dia / 2) / (xRadio) + canXOrigin;//获得管线截面外接矩形左上角的坐标x
                        y = (maxZ - intPnt.ZPipeTop) * 1000 / yRadio + canYOrigin + 20; //获得管线截面外接矩形左上角的坐标y
                        pen.Width = 1;
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        g.DrawEllipse(pen, (float)x, (float)y, (float)(diaWidth / xRadio), (float)(diaHeight / xRadio));
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        xline = x + (dia / 2) / ( xRadio);
                        yline = y + dia / ( xRadio);
                        g.DrawLine(pen, (int)xline, (int)yline, (int)xline, (int)(tableYOrigin + rowHeight * 4));



                        g.DrawString(intPnt.FcName, font, drawBrush, (float)(xline - 20), (float)(tableYOrigin - 20));
                        rotate.DrawString(Math.Round(intPnt.H, 3).ToString(), font, drawBrush, new PointF((float)(xline - 8), (float)(tableYOrigin + rowHeight * 0 + 15)), format, -90f);
                        rotate.DrawString(Math.Round(intPnt.ZPipeTop - dia / 1000, 3).ToString(), font, drawBrush, new PointF((float)(xline - 8), (float)(tableYOrigin + rowHeight * 1 + 15)), format, -90f);
                        rotate.DrawString("DN" + intPnt.Diameter, font, drawBrush, new PointF((float)(xline - 8), (float)(tableYOrigin + rowHeight * 2 + 15)), format, -90f);
                        if (intPnt.Distance < 1.0)
                            rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 25)), format, 0f);
                        else
                            rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 15)), format, 0f);
                        //if ((float)intPnt.Distance * 1000 / xRadio < font.Height)
                        //{
                        //    rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 20)), format, 0f);
                        //}
                        //else
                        //{
                        //    rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 15)), format, 0f);
                        //}
                        
                        //g.DrawString(Math.Round(intPnt.H, 3).ToString(), font, drawBrush, (float)(x - 40), (float)(tableYOrigin + rowHeight * 0 + 10));                       
                        //g.DrawString(Math.Round(intPnt.ZPipeTop - dia / 1000, 3).ToString(), font, drawBrush, (float)(x - 40), (float)(tableYOrigin + rowHeight * 1 + 10));
                        //g.DrawString("DN" + intPnt.Diameter, font, drawBrush, (float)(x - 40), (float)(tableYOrigin + rowHeight * 2 + 10));
                        //g.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, (float)(x - 40), (float)(tableYOrigin + rowHeight * 3 + 10));

                    }
                    else
                    {
                        string[] diameters = diameter.Split('*');
                        if (diameters != null && diameters.Length == 2)
                        {
                            diaWidth = double.Parse(diameters[0])/zoom;
                            diaHeight = double.Parse(diameters[1])/zoom;
                            x = (intPnt.DistanceF * 1000 - diaWidth / 2) / xRadio + canXOrigin;//获得管线截面矩形左上角的坐标x
                            y = (maxZ - intPnt.ZPipeTop) * 1000 / yRadio + canXOrigin + 20;//获得管线截面矩形左上角的坐标y
                            pen.Width = 1;
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                            g.DrawRectangle(pen, (float)x, (float)y, (float)(diaWidth / xRadio), (float)(diaHeight / xRadio));
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            xline = x + (diaWidth / 2) / xRadio;
                            yline = y + diaHeight / xRadio;
                            g.DrawLine(pen, (int)xline, (int)yline, (int)xline, (int)(tableYOrigin + rowHeight * 4));
                            g.DrawString(intPnt.FcName, font, drawBrush, (float)(xline - 15), (float)(tableYOrigin - 20));
                            rotate.DrawString(Math.Round(intPnt.H, 3).ToString(), font, drawBrush, new PointF((float)(xline - 8), (float)(tableYOrigin + rowHeight * 0 + 15)), format, -90f);
                            rotate.DrawString(Math.Round(intPnt.ZPipeTop - dia / 1000, 3).ToString(), font, drawBrush, new PointF((float)(xline - 8), (float)(tableYOrigin + rowHeight * 1 + 15)), format, -90f);
                            rotate.DrawString(intPnt.Diameter, font, drawBrush, new PointF((float)(xline - 8), (float)(tableYOrigin + rowHeight * 2 + 15)), format, -90f);
                            if (intPnt.Distance < 1.0)
                                rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 25)), format, 0f);
                            else
                                rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 15)), format, 0f);
                            //if ((float)intPnt.Distance * 1000 / xRadio < font.Height)
                            //{
                            //    rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 20)), format, 0f);
                            //}
                            //else
                            //{
                            //    rotate.DrawString(Math.Round(intPnt.Distance, 3).ToString(), font, drawBrush, new PointF((float)(x - 10), (float)(tableYOrigin + rowHeight * 3 + 15)), format, 0f);
                            //}
                            
                        }
                    }

                    }
                g.DrawString(Math.Round(length, 3).ToString(), font, drawBrush, (float)canvasTotalWidth / 2 - 20, (float)(tableYOrigin + rowHeight * 4 + 10));
            }
            catch (System.Exception ex)
            {
            	
            }          
         
        }

        private void DrawPipeTransection(List<IntersectPoint> listInterPnts,double length)
        {
            if (listInterPnts.Count < 2 || listInterPnts == null) return;
            Bitmap bmp = new Bitmap(1366, 600);
            Graphics g_bmp = Graphics.FromImage(bmp);
            g_bmp.Clear(Color.Wheat);
            if (g_bmp != null)
            {
                Draw(g_bmp, bmp.Width, bmp.Height, listInterPnts,length);
                bmp.Save("E:\\1.bmp");
                FrmPipeTransection2D dialog = new FrmPipeTransection2D(bmp);
                dialog.Show();
                WaitForm.Stop();
            }
        }

       
    }
}
