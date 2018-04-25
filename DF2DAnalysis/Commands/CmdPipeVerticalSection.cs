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
    class CmdPipeVerticalSection : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        private List<IFeature> _featureList;
        private List<VerticalPoint> _vertiPoints;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            m_ActiveView = app.Current2DMapControl.ActiveView;
            if (this.m_ActiveView.FocusMap.FeatureSelection != null)
                this.m_ActiveView.FocusMap.ClearSelection();
            bool ready = false;
            if (app == null || app.Current2DMapControl == null) return;
            IGeometry pGeo = null;
            _featureList = new List<IFeature>();
            _vertiPoints = new List<VerticalPoint>();
            string mapNum = "";
            string mapName = Config.GetConfigValue("SystemName") + "纵断面图";
            double totalLength = 0;
            try
            {
                if (button == 1)
                {
                    WaitForm.Start("正在查询...", "请稍后");
                    PointClass searchPoint = new PointClass();
                    searchPoint.PutCoords(mapX, mapY);
                    pGeo = PublicFunction.DoBuffer(searchPoint,
                        PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                    if (pGeo == null) return;
                    ready = true;
                    
                    if (ready)
                    {
                        bool haveone = false;
                        foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                        {
                            if (haveone) break;
                            foreach (MajorClass mc in lg.MajorClasses)
                            {
                                if (haveone) break;
                                foreach (SubClass sc in mc.SubClasses)
                                {
                                    if (haveone) break;
                                    if (!sc.Visible2D) continue;
                                    string[] arrFc2DId = mc.Fc2D.Split(';');
                                    if (arrFc2DId == null) continue;
                                    IFeatureCursor pFeatureCursor = null;
                                    IFeature pFeature = null;
                                    string fcName = mc.Name;
                                    foreach (string fc2DId in arrFc2DId)
                                    {
                                        if (haveone) break;
                                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                        if (dffc == null) continue;
                                        IFeatureClass fc = dffc.GetFeatureClass();
                                        FacilityClass facc = dffc.GetFacilityClass();
                                        if (facc.Name != "PipeLine") continue;
                                        if (fc == null || pGeo == null) continue;
                                        ISpatialFilter pSpatialFilter = new SpatialFilter();
                                        pSpatialFilter.Geometry = pGeo;
                                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                        pSpatialFilter.WhereClause = sc.Parent.ClassifyField + " =  '" + sc.Name + "'";
                                        pFeatureCursor = fc.Search(pSpatialFilter, false);
                                        if (pFeatureCursor == null) continue;
                                        while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                        {
                                            
                                            if (pFeature.Shape is IPolyline)
                                            {
                                                string diameter;
                                                string road;
                                                string startZ;
                                                string endZ;
                                                string startH;
                                                string endH;
                                                int indexdia = pFeature.Fields.FindField("STANDARD");
                                                int indexroad = pFeature.Fields.FindField("PROAD");
                                                int indexSZ = pFeature.Fields.FindField("STARTTOPGC");
                                                int indexEZ = pFeature.Fields.FindField("ENDTOPGC");
                                                int indexSH = pFeature.Fields.FindField("START_SURF_H");
                                                int indexEH = pFeature.Fields.FindField("END_SURF_H");

                                                diameter = pFeature.get_Value(indexdia).ToString();
                                                road = pFeature.get_Value(indexroad).ToString();
                                                startZ = pFeature.get_Value(indexSZ).ToString();
                                                endZ = pFeature.get_Value(indexEZ).ToString();
                                                startH = pFeature.get_Value(indexSH).ToString();
                                                endH = pFeature.get_Value(indexEH).ToString();
                                                
                                                IPolyline pline = pFeature.Shape as IPolyline;
                                                IPoint fPoint = pline.FromPoint;
                                                IPoint tPoint = pline.ToPoint;
                                                totalLength += pline.Length;
                                                mapNum = getMapNumforPnt(fPoint);
                                                VerticalPoint vertiPoint1 = new VerticalPoint(fcName,pFeature,fPoint,diameter,road,startZ,startH,true);
                                                _vertiPoints.Add(vertiPoint1);
                                                VerticalPoint vertiPoint2 = new VerticalPoint(fcName,pFeature,tPoint,diameter,road,endZ,endH,false);
                                                _vertiPoints.Add(vertiPoint2);
                                                haveone = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                DrawPipeVerticalSection(_vertiPoints, mapNum, mapName,totalLength);

            }
            catch
            {

            }
        }
        private void Draw(Graphics g, int winWidth, int winheight, List<VerticalPoint> listVertiPnts, string mapNum, string mapName,double totalLength)
        {
            try
            {
                Pen pen = new Pen(Color.Black, 2);
                Font font = new Font("Times New Roman", 9);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                //按照管线点的X值对管线点进行排序
                //List<VerticalPoint> orderList = listVertiPnts.OrderBy(i => i.X).ToList<VerticalPoint>();
                List<VerticalPoint> orderList = listVertiPnts;
                //确定竖向比例
                double minHeight = double.MaxValue;
                double maxHeight = double.MinValue;
                string road = null;
                foreach (VerticalPoint inpnt in orderList)
                {
                    if (inpnt.H < minHeight) minHeight = inpnt.H;
                    if (inpnt.H > maxHeight) maxHeight = inpnt.H;
                    if (inpnt.ZPipeTop < minHeight) minHeight = inpnt.ZPipeTop;
                    if (inpnt.ZPipeTop > maxHeight) maxHeight = inpnt.ZPipeTop;
                    if (inpnt.Road != null) road = inpnt.Road;
                    else
                        road = "未知";
                }

                double constHeight = 20;//高程常数50m
                //double constwidth = 5;//管线截面两边预留长度5m
                double totalYSpan = maxHeight - minHeight + constHeight;//画图区域竖向总高度
                double totalXSpan = 0.0;//画图区域横向总长度
                foreach (VerticalPoint inpnt in orderList)
                {
                    totalXSpan += inpnt.Length;
                }
               
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
                g.DrawString(mapName, fontTitle, drawBrush, new PointF((float)(canvasTotalWidth / 2) - 150, 20));
                g.DrawString("所在道路：" + road, font, drawBrush, new PointF((float)outlineXOrigin + 10, (float)outlineYOrigin - 30));
                g.DrawString("断面号：" + mapNum, font, drawBrush, new PointF((float)(canvasTotalWidth - paddingRight - 170), (float)outlineYOrigin - 30));

                //绘制表格
                double constTable = 30;
                double tableHeight = 150;
                int rowCount = 5;
                int rowHeight = (int)tableHeight / rowCount;
                double tableXOrigin = outlineXOrigin + constTable;
                double tableYOrigin = canvasTotalHeight - tableHeight - paddingBottom;
                double tableWidth = outlineWidth - 2 * constTable;
                g.DrawRectangle(pen, (int)tableXOrigin, (int)tableYOrigin, (float)tableWidth, (float)tableHeight);
                g.DrawString("地面高程(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 0 + 10);
                g.DrawString("管线高程(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 1 + 10);
                g.DrawString(" 规  格(mm)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 2 + 10);
                g.DrawString("管线长度(m)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 3 + 10);
                g.DrawString("总长度(m)",font,drawBrush,(float)tableXOrigin + 5,(float)tableYOrigin + rowHeight * 4 + 10);
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
                double canWidth = canvasTotalWidth - canXOrigin - paddingRight - constCol;
                double canHeight = canvasTotalHeight - outlineYOrigin - paddingBottom - constCanY;
                double xRadio = totalXSpan * 1000 / (canWidth - 2 * constCanBlank);
                double yRadio = totalYSpan * 1000 / (canHeight);
                //确定管线在画图区域的相对位置,转换为屏幕距离，并写入VerticalPoint类
                for (int i = 0; i < orderList.Count; i++)
                {
                    if (i == 0)
                    {
                        orderList[i].DistanceDraw = constCanBlank;

                    }
                    else
                    {
                        orderList[i].DistanceDraw = orderList[i].Length * 1000 / xRadio + orderList[i - 1].DistanceDraw;
                    }

                }
                //计算管线交点及地面高在画图区域的相对高程 
                double dH = maxHeight - minHeight;
                foreach (VerticalPoint intPnt in orderList)
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
                //绘制纵断面
                double zoom = 1;
                float constfc = 20;
                float constStrY = 15;
                float constStrX = 10;
               
                RotateText rotate = new RotateText();
                rotate.Graphics = g;
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                font = new Font("Times New Roman", 7);
                double radioTemp = xRadio > yRadio ? xRadio : yRadio;
                for (int i = 0; i < orderList.Count; i++)
                {
                    if (i == 0)
                    {
                        VerticalPoint verPnt0 = orderList[i];
                        double dia0 = 0;
                        if (verPnt0.IsCircle)
                        {
                            dia0 = double.Parse(verPnt0.Diameter);
                        }
                        else
                        {
                            string[] dia = verPnt0.Diameter.Split('*');
                            dia0 = double.Parse(dia[dia.Length - 1]);
                        }
                        pen.Width = 1;
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        g.DrawEllipse(pen, (float)(verPnt0.DistanceDraw + canXOrigin - 5), (float)(verPnt0.ZDraw + canYOrigin - 2.5), 5, 5);
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        double xline0 = verPnt0.DistanceDraw + canXOrigin - 2.5;
                        double yline0 = verPnt0.ZDraw + canYOrigin + 5;
                        g.DrawLine(pen, new PointF((float)xline0, (float)yline0), new PointF((float)xline0, (float)(tableYOrigin + rowHeight * 4)));
                        g.DrawString(verPnt0.FcName, font, drawBrush, (float)(xline0 - constfc), (float)(tableYOrigin - constStrY));
                        rotate.DrawString(Math.Round(verPnt0.H, 3).ToString(), font, drawBrush, new PointF((float)(xline0 - constStrX), (float)(tableYOrigin + rowHeight * 0 + constStrY)), format, -90f);
                        rotate.DrawString(Math.Round(verPnt0.ZPipeTop - dia0 / 1000, 3).ToString(), font, drawBrush, new PointF((float)(xline0 - constStrX), (float)(tableYOrigin + rowHeight * 1 + constStrY)), format, -90f);
                        rotate.DrawString("DN" + verPnt0.Diameter, font, drawBrush, new PointF((float)(xline0 - constStrX), (float)(tableYOrigin + rowHeight * 2 + constStrY)), format, -90f);
                        rotate.DrawString("", font, drawBrush, new PointF((float)(xline0 - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY)), format, -90f);

                    }
                    else
                    {
                        if (!orderList[i].IsFromPnt)
                        {
                            VerticalPoint verPnt1 = orderList[i - 1];
                            VerticalPoint verPnt2 = orderList[i];
                            double dia2 = 0;
                            if (verPnt2.IsCircle)
                            {
                                dia2 = double.Parse(verPnt2.Diameter);
                            }
                            else
                            {
                                string[] dia = verPnt2.Diameter.Split('*');
                                dia2 = double.Parse(dia[dia.Length - 1]);
                            }
                           
                            pen.Width = 1;
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                            g.DrawLine(pen, new PointF((float)(verPnt1.DistanceDraw + canXOrigin), (float)(verPnt1.ZDraw + canYOrigin)), new PointF((float)(verPnt2.DistanceDraw + canXOrigin), (float)(verPnt2.ZDraw + canYOrigin)));
                            //g.DrawEllipse(pen, (float)(verPnt1.DistanceDraw + canXOrigin - 5), (float)(verPnt1.ZDraw + canYOrigin - 2.5), 5, 5);
                            g.DrawEllipse(pen, (float)(verPnt2.DistanceDraw + canXOrigin), (float)(verPnt2.ZDraw + canYOrigin - 2.5), 5, 5);
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            //double xline1 = verPnt1.DistanceDraw + canXOrigin - 2.5;
                            //double yline1 = verPnt1.ZDraw + canYOrigin + 5;
                            double xline2 = verPnt2.DistanceDraw + canXOrigin + 2.5;
                            double yline2 = verPnt2.ZDraw + canYOrigin + 5;
                            g.DrawLine(pen, new PointF((float)xline2, (float)yline2), new PointF((float)xline2, (float)(tableYOrigin + rowHeight * 4)));
                            g.DrawString(verPnt2.FcName, font, drawBrush, (float)(xline2 - constfc), (float)(tableYOrigin - constStrY));
                            rotate.DrawString(Math.Round(verPnt2.H, 3).ToString(), font, drawBrush, new PointF((float)(xline2 - constStrX), (float)(tableYOrigin + rowHeight * 0 + constStrY)), format, -90f);
                            rotate.DrawString(Math.Round(verPnt2.ZPipeTop - dia2 / 1000, 3).ToString(), font, drawBrush, new PointF((float)(xline2 - constStrX), (float)(tableYOrigin + rowHeight * 1 + constStrY)), format, -90f);
                            rotate.DrawString("DN" + verPnt2.Diameter, font, drawBrush, new PointF((float)(xline2 - constStrX), (float)(tableYOrigin + rowHeight * 2 + constStrY)), format, -90f);
                            rotate.DrawString(Math.Round(verPnt2.Length, 3).ToString(), font, drawBrush, new PointF((float)(xline2 - constStrX), (float)(tableYOrigin + rowHeight * 3 + constStrY)), format, -90f);
                        }
                        
                    }
                   
                }
                g.DrawString(Math.Round(totalLength,3).ToString(), font, drawBrush, (float)canvasTotalWidth / 2 - 20, (float)(tableYOrigin + rowHeight * 4 + 10));
            }
            catch
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

        private void DrawPipeVerticalSection(List<VerticalPoint> listVertiPnts, string mapNum, string mapName,double totalLength)
        {
            if (listVertiPnts == null) return;
            Bitmap bmp = new Bitmap(1366, 600);
            Graphics g_bmp = Graphics.FromImage(bmp);
            g_bmp.Clear(Color.Wheat);
            if (g_bmp != null)
            {
                Draw(g_bmp, bmp.Width, bmp.Height, listVertiPnts, mapNum, mapName,totalLength);
                bmp.Save("E:\\1.bmp");
                FrmPipeTransection2D dialog = new FrmPipeTransection2D(bmp);
                dialog.Show();
                WaitForm.Stop();
            }
        }

        public override void RestoreEnv()
        {
           
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(true);
            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
            if (mapView == null) return;
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
      
    }
}
