using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.AddIn.Core;
using System.Windows.Forms;
using System.IO;
using Skyline.WorkSpace;
using TerraExplorerX;
using Skyline.Common;
using DevExpress.XtraEditors;
using Skyline.SystemConfig;
using Skyline._3DControl;
using System.Drawing;
using System.Drawing.Drawing2D;
using Skyline.AddIn.WinForm;
namespace Skyline.AddIn.Pipe
{
    class CmdHorizontalSectionAnalysis : AbstractCommand
    {
        private List<ITerrainLabel65> _listLabel;
        private DrawTool _drawTool;
        private const double _maxLength = 100;

        public override bool CheckPermission()
        {
            return WorkSpaceServices.Instance().HasMenuAuth(this.CommandID);
        }
        
        public override void Run(object sender, System.EventArgs e)
        {
            MainFrmService.EscEvent += new EscHandler(RestoreEnv);
            CommandManager.Push(this);
            RenderControlServices.Instance().Clear();

            _listLabel = new List<ITerrainLabel65>();

            this._drawTool = DrawToolServices.Instance().CreateDrawTool(DrawType.Line);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        public override void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.End();
            }
            CommandManager.Pop();
            MainFrmService.EscEvent -= new EscHandler(RestoreEnv);
        }
        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Line && this._drawTool.GetGeo2D() != null)
            {
                HorizontalSectionAnalysis();
            }
        }
        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }

            foreach (ITerrainLabel65 label in _listLabel)
            {
                RenderControlServices.Instance().SGWorld.Creator.DeleteObject(label.ID);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(label);
            }
            _listLabel.Clear();
        }


        // 高度线性内插
        private double GetInterectPointHeight(ICoord2D startPt, double startPtH, ICoord2D endPt, double endPtH, ICoord2D interPt)
        {
            if (startPt == null || endPt == null || interPt == null) return 0.0;
            if (Math.Abs(startPtH - endPtH) < 0.00000001) return startPtH;
            double l1 = (startPt.X - endPt.X) * (startPt.X - endPt.X) + (startPt.Y - endPt.Y) * (startPt.Y - endPt.Y);
            double l2 = (startPt.X - interPt.X) * (startPt.X - interPt.X) + (startPt.Y - interPt.Y) * (startPt.Y - interPt.Y);
            double ratio = Math.Sqrt(l2 / l1);
            return (startPtH + (endPtH - startPtH) * ratio);
        }

        private void HorizontalSectionAnalysis()
        {
            ILineString line = this._drawTool.GetGeo2D() as ILineString;
            if (line.Length > _maxLength)
            {
                XtraMessageBox.Show(string.Format("断面长度不超过{0}米！", _maxLength), "提示");
                return;
            }
            ICoord2D startCoord = null;
            if (line.Points.Count == 2)
            {
                IPoint startPt = line.StartPoint;
                startCoord = RenderControlServices.Instance().CovertCoord(startPt.X, startPt.Y);
            }
            else return;
            WaitForm.Start("正在进行横断面分析...", "请稍后", false);
            List<InterPointInfo> listInter = new List<InterPointInfo>();

            foreach (Layer l in WorkSpaceServices.Instance().PipeLineLayers)
            {
                if (!l.Show && l.ShpLayer == null) continue;
                l.ShowShp = true;
                Field fDiameter = WorkSpaceServices.Instance().GetFieldBySysField(l, SystemField.PIPELINE_DIAMETER[1]);
                Field fClassify = WorkSpaceServices.Instance().GetFieldBySysField(l, SystemField.CLASSIFY[1]);
                Field fMaterial = WorkSpaceServices.Instance().GetFieldBySysField(l, SystemField.MATERIAL[1]);
                Field fHlb = WorkSpaceServices.Instance().GetFieldBySysField(l, SystemField.HLB[1]);
                if (fDiameter == null || fMaterial == null || fHlb == null) continue;

                IFeatures65 fs = l.ShpLayer.ExecuteSpatialQuery(line, IntersectionType.IT_INTERSECT);
                for (int i = 0; i < fs.Count;i++ )
                {
                    IFeature65 f = fs[i] as IFeature65;
                    IGeometry geo = f.Geometry;
                    
                    IGeometry inter = line.SpatialOperator.Intersection(geo);// 平面相交
                    if (inter.GeometryType == SGGeometryTypeId.SG_POINT)
                    {
                        IPoint pt = inter as IPoint;
                        ICoord2D coordInter = RenderControlServices.Instance().CovertCoord(pt.X, pt.Y);
                        double height = 0.0;
                        if (geo.GeometryType == SGGeometryTypeId.SG_LINESTRING)
                        {
                            ILineString geoLine = geo as ILineString;
                            if (geoLine.Points.Count == 2)
                            {
                                double startPtH = geoLine.StartPoint.Z;
                                double endPtH = geoLine.EndPoint.Z;
                                ICoord2D sc = RenderControlServices.Instance().CovertCoord(geoLine.StartPoint.X, geoLine.StartPoint.Y);
                                ICoord2D ec = RenderControlServices.Instance().CovertCoord(geoLine.EndPoint.X, geoLine.EndPoint.Y);
                                height = GetInterectPointHeight(sc, startPtH, ec, endPtH, coordInter);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        IWorldPointInfo65 wpi = RenderControlServices.Instance().SGWorld.Terrain.GetGroundHeightInfo(pt.X, pt.Y, AccuracyLevel.ACCURACY_NORMAL, false);

                        InterPointInfo interPtInfo = new InterPointInfo();
                        interPtInfo.pt = pt;
                        interPtInfo.terrainHeight = wpi.Position.Altitude;
                        interPtInfo.height = height;
                        interPtInfo.coordInter = coordInter;
                        interPtInfo.distance = Math.Sqrt((coordInter.X - startCoord.X) * (coordInter.X - startCoord.X) + (coordInter.Y - startCoord.Y) * (coordInter.Y - startCoord.Y));
                        interPtInfo.color.abgrColor = Convert.ToUInt32(l.ShpLayer.FeatureGroups.Polyline.GetProperty("Line Color").ToString());
                        interPtInfo.layerName = l.Name;
                        interPtInfo.diameter = f.FeatureAttributes.GetFeatureAttribute(fDiameter.Name).Value;
                        interPtInfo.classify = f.FeatureAttributes.GetFeatureAttribute(fClassify.Name).Value;
                        interPtInfo.material = f.FeatureAttributes.GetFeatureAttribute(fMaterial.Name).Value;
                        interPtInfo.hlb = f.FeatureAttributes.GetFeatureAttribute(fHlb.Name).Value;
                        listInter.Add(interPtInfo);
                    }
                }
                l.ShowShp = false;
            } 
            WaitForm.Stop();

            if (listInter.Count > 0)
            {
                listInter.Sort(new ComparerInterPointInfo());

                for (int i = 0; i < listInter.Count; i++)
                {
                    InterPointInfo info = listInter[i];
                    info.no = (i + 1);
                    if (info.no > 1) info.interHDist = info.distance - listInter[i - 1].distance;

                    IPosition65 pos = RenderControlServices.Instance().SGWorld.Creator.CreatePosition(info.pt.X, info.pt.Y);
                    ILabelStyle65 lStyle = RenderControlServices.Instance().SGWorld.Creator.CreateLabelStyle();
                    lStyle.TextColor = info.color;
                    lStyle.FontSize = 10;
                    lStyle.MaxViewingHeight = 1000;
                    ITerrainLabel65 label = RenderControlServices.Instance().SGWorld.Creator.CreateTextLabel(pos, info.no.ToString(), lStyle);
                    label.Visibility.MaxVisibilityDistance = 1000;
                    this._listLabel.Add(label);
                }
                DrawHorizontalSection(listInter);
            }
        }

        private void Draw(Graphics g, int winWidth, int winheight, List<InterPointInfo> listInferPts)
        {
            Pen pen = new Pen(Color.Black, 2);
            Font font = new Font("Tahoma", 9);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            // 比例尺计算
            double totalXSpan = 0.0;
            double minInterval = double.MaxValue;
            double maxInterval = 0.0;
            double minHeight = double.MaxValue;
            double maxHeight = 0.0;
            foreach (InterPointInfo ptInfo in listInferPts)
            {
                totalXSpan += ptInfo.interHDist;
                if (ptInfo.interHDist < minInterval) minInterval = ptInfo.interHDist;
                if (ptInfo.interHDist > maxInterval) maxInterval = ptInfo.interHDist;
                if (ptInfo.height < minHeight) minHeight = ptInfo.height;
                if (ptInfo.height > maxHeight) maxHeight = ptInfo.height;
                if (ptInfo.terrainHeight < minHeight) minHeight = ptInfo.terrainHeight;
                if (ptInfo.terrainHeight > maxHeight) maxHeight = ptInfo.terrainHeight;
            }
            double totalYSpan = maxHeight - minHeight;

            double paddingLeft = 20;
            double paddingRight = 20;
            double paddingBottom = 20;
            double paddingTop = 40;
            double canvasTotalWidth = winWidth;
            double canvasTotalHeight = winheight;
            //绘制头
            Font fontHead = new System.Drawing.Font("Tahoma", 15);
            g.DrawString("横断面图", fontHead, drawBrush, (float)canvasTotalWidth / 2 - 60, 10);

            //计算比例尺
            double tableHeight = 180;
            double disFromAxis = 20;// 与坐标轴的距离
            double canvasWidth = canvasTotalWidth - paddingLeft - paddingRight;
            double canvasHeight = canvasTotalHeight - tableHeight - paddingTop - paddingBottom;

            double xRatio = totalXSpan * 1000 / (canvasWidth - disFromAxis * 2);
            double yRatio = totalYSpan * 1000 / (canvasHeight - disFromAxis * 2);
            //xRatio = ((Math.Floor((xRatio + 10) / 10))) * 10;
            if (xRatio > yRatio)
            {
                xRatio = Math.Ceiling(xRatio);
                yRatio = xRatio;
            }
            else
            {
                yRatio = Math.Ceiling(yRatio);
                xRatio = yRatio;
            }
            g.DrawString("比例尺：1:" + xRatio.ToString("0"), font, drawBrush, (float)(canvasTotalWidth - paddingRight - 100), 25);
            g.DrawString("单位：m", font, drawBrush, (float)(canvasTotalWidth - paddingRight - 100), 10);

            double xOrigin = paddingLeft;
            double yOrigin = canvasTotalHeight - tableHeight - paddingBottom;

            // 绘制表格   
            double disFromDraw = 20;// 与绘图区域的距离
            int rowCount = 7;// 表格行数
            int colCount = listInferPts.Count + 1;// 表格列数
            int colWidth = (int)canvasWidth / colCount > 80 ? 80 : (int)canvasWidth / colCount;
            int rowHeight = (int)(tableHeight - disFromDraw * 2) / rowCount;
            double tableXOrigin = xOrigin;
            double tableYOrigin = yOrigin + disFromDraw;
            g.DrawRectangle(pen, (int)tableXOrigin, (int)tableYOrigin, (int)colWidth * colCount, (int)rowHeight * rowCount);
            g.DrawString("序号", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + 5);
            g.DrawString("图层名称", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 1 + 5);
            g.DrawString(SystemField.CLASSIFY[0], font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 2 + 5);
            g.DrawString("地面高程", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 3 + 5);
            g.DrawString("高程", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 4 + 5);
            g.DrawString(SystemField.PIPELINE_DIAMETER[0] + "(mm)", font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 5 + 5);
            g.DrawString(SystemField.MATERIAL[0], font, drawBrush, (float)tableXOrigin + 5, (float)tableYOrigin + rowHeight * 6 + 5);
            for (int i = 1; i < rowCount; i++)
            {
                pen.Width = 1 ;
                g.DrawLine(pen, (int)tableXOrigin, (int)(tableYOrigin + rowHeight * i), (int)(tableXOrigin + colWidth * colCount), (int)(tableYOrigin + rowHeight * i));
            }
            for (int i = 1; i < colCount; i++)
            {
                pen.Width = 1;
                g.DrawLine(pen, (int)(tableXOrigin + colWidth * i), (int)tableYOrigin, (int)(tableXOrigin + colWidth * i), (int)(tableYOrigin + rowHeight * rowCount));

                double tableCellX = tableXOrigin + colWidth * i + 5;
                double tableCellY = tableYOrigin + rowHeight * 0 + 5;
                g.DrawString(listInferPts[i - 1].no.ToString(), font, drawBrush, (float)tableCellX, (float)tableCellY);
                tableCellY = tableYOrigin + rowHeight * 1 + 5;
                g.DrawString(listInferPts[i - 1].layerName.ToString(), font, drawBrush, (float)tableCellX, (float)tableCellY);
                tableCellY = tableYOrigin + rowHeight * 2 + 5;
                g.DrawString(listInferPts[i - 1].classify.ToString(), font, drawBrush, (float)tableCellX, (float)tableCellY);
                tableCellY = tableYOrigin + rowHeight * 3 + 5;
                g.DrawString(listInferPts[i - 1].terrainHeight.ToString("0.00"), font, drawBrush, (float)tableCellX, (float)tableCellY);
                tableCellY = tableYOrigin + rowHeight * 4 + 5;
                g.DrawString(listInferPts[i - 1].height.ToString("0.00"), font, drawBrush, (float)tableCellX, (float)tableCellY);
                
                g.DrawString(listInferPts[i - 1].diameter.ToString(), font, drawBrush, (float)tableCellX, (float)tableCellY);
                tableCellY = tableYOrigin + rowHeight * 6 + 5;
                g.DrawString(listInferPts[i - 1].material.ToString(), font, drawBrush, (float)tableCellX, (float)tableCellY);
            }

            // 绘制画图区域
            pen.Width = 2;
            g.DrawRectangle(pen, (int)xOrigin, (int)(yOrigin - canvasHeight), (int)canvasWidth, (int)canvasHeight);

            double ySpanReal = totalYSpan * 1000 / yRatio;
            double yStartReal = yOrigin - (canvasHeight - ySpanReal - disFromAxis);
            double xStartReal = xOrigin + disFromAxis;
            // 绘制地面线
            double x0, y0, x1, y1;
            double xrec = xStartReal;
            double yrec = yStartReal - (listInferPts[0].terrainHeight - listInferPts[0].height) * 1000 / yRatio;
            //g.DrawString("地面线", font, drawBrush, (int)(xrec - 30), (int)(yrec - 30));            
            for (int i = 1; i < listInferPts.Count; i++)
            {
                InterPointInfo ptInfo0 = listInferPts[i - 1];
                InterPointInfo ptInfo1 = listInferPts[i];
                x0 = xrec;
                y0 = yrec;
                x1 = x0 + ptInfo1.interHDist * 1000 / xRatio;
                y1 = y0 - (ptInfo1.terrainHeight - ptInfo0.terrainHeight) * 1000 / yRatio;
                pen.DashStyle = DashStyle.Solid;
                pen.Width = 1;
                pen.Color = Color.DarkGreen;
                g.DrawLine(pen, (int)x0, (int)y0, (int)x1, (int)y1);
                pen.Color = Color.Black;
                if (i % 2 == 0)
                {
                    g.DrawLine(pen, (int)x0, (int)(yOrigin - 10), (int)x1, (int)(yOrigin - 10));
                    g.DrawString(ptInfo1.interHDist.ToString("0.00"), font, drawBrush, (int)(x0 + x1) / 2 - 10, (int)(int)(yOrigin - 25));
                }
                else
                {
                    g.DrawLine(pen, (int)x0, (int)(yOrigin - 20), (int)x1, (int)(yOrigin - 20));
                    g.DrawString(ptInfo1.interHDist.ToString("0.00"), font, drawBrush, (int)(x0 + x1) / 2 - 10, (int)(int)(yOrigin - 15));
                }
                xrec = x1;
                yrec = y1;
            }
            // 绘制横断面
            int kuada = 1;
            double totalHDis = listInferPts[0].interHDist;
            double firstHeight = listInferPts[0].height;
            for (int i = 0; i < listInferPts.Count; i++)
            {
                InterPointInfo ptInfo = listInferPts[i];
                double x, y;
                totalHDis += ptInfo.interHDist;
                if (i == 0)
                {
                    x = xStartReal;
                    y = yStartReal;
                }
                else
                {
                    x = xStartReal + totalHDis * 1000 / xRatio;
                    y = yStartReal - (ptInfo.height - firstHeight) * 1000 / yRatio;
                }

                string diameter = ptInfo.diameter;
                string[] diameters = diameter.Split('*');
                bool bRect = false;
                double diaWidth = 0.0;
                double diaHeight = 0.0;
                double dia = 0.0;
                if (diameters != null && diameters.Length == 2)
                {
                    bRect = true;
                    diaWidth = double.Parse(diameters[0]);
                    diaHeight = double.Parse(diameters[1]);
                }
                else dia = double.Parse(diameter);

                uint color = ptInfo.color.abgrColor;
                int red = (int)color & 255;
                int green = (int)color >> 8 & 255;
                int blue = (int)color >> 16 & 255;
                int alpha = (int)color >> 24 & 255;
                Color c = Color.FromArgb(255, red, green, blue);
                pen.Color = c;
                double xoff = 0.0;
                double yoff = 0.0;
                if (bRect)
                {
                    float fWidth = (float)(diaWidth / xRatio * kuada);
                    float fHeight = (float)(diaHeight / yRatio * kuada);
                    if (Math.Abs(fWidth - 1.0) < 0.0000001 || Math.Abs(fHeight - 1.0) < 0.0000001)
                    {
                        fWidth = fHeight = 1.0f;
                    }
                    else
                    {
                        xoff = - fWidth / 2;
                        if (ptInfo.hlb.Contains("外")) yoff = 0;
                        else if (ptInfo.hlb.Contains("内")) yoff = - fHeight;
                        else yoff = -fHeight / 2;
                    }
                    pen.Width = 2;
                    pen.DashStyle = DashStyle.Solid;
                    g.DrawRectangle(pen, (float)(x + xoff), (float)(y + yoff), fWidth, fHeight);
                    pen.Width = 1;
                    pen.DashStyle = DashStyle.Dash;
                    g.DrawLine(pen, (int)x, (int)y, (int)x, (int)yOrigin);
                    g.DrawString(ptInfo.no.ToString(), font, drawBrush, (float)x, (float)((y + yoff) - 10));

                }
                else
                {
                    float fWidth = (float)(dia / xRatio * kuada);
                    float fHeight = (float)(dia / yRatio * kuada);

                    if (Math.Abs(fWidth - 1.0) < 0.0000001 || Math.Abs(fHeight - 1.0) < 0.0000001)
                    {
                        fWidth = fHeight = 1.0f;
                    }
                    else
                    {
                        xoff = -fWidth / 2;
                        if (ptInfo.hlb.Contains("外")) yoff = 0;
                        else if (ptInfo.hlb.Contains("内")) yoff = -fHeight;
                        else yoff = -fHeight / 2;
                    }
                    pen.Width = 2;
                    pen.DashStyle = DashStyle.Solid;
                    g.DrawEllipse(pen, (float)(x + xoff), (float)(y + yoff), fWidth, fHeight);
                    pen.Width = 1;
                    pen.DashStyle = DashStyle.Dash;
                    g.DrawLine(pen, (int)x, (int)y, (int)x, (int)yOrigin);
                    g.DrawString(ptInfo.no.ToString(), font, drawBrush, (float)x, (float)((y + yoff) - 10));
                }
            }
        }

        private void DrawHorizontalSection(List<InterPointInfo> listInferPts)
        {
            if (listInferPts == null || listInferPts.Count < 2) return;
            Bitmap bmp = new Bitmap(1366, 600);
            if (bmp != null)
            {
                Graphics g_bmp = Graphics.FromImage(bmp);
                g_bmp.Clear(Color.Wheat);
                if (g_bmp != null)
                {
                    Draw(g_bmp, bmp.Width, bmp.Height, listInferPts);

                    FrmHorizontalSectionAnalysis dialog = new FrmHorizontalSectionAnalysis("横断面分析结果", bmp);
                    dialog.Show();
                }
            }
        }

        private class ComparerInterPointInfo : IComparer<InterPointInfo>
        {
            public int Compare(InterPointInfo x, InterPointInfo y)
            {
                if (x.distance > y.distance ) return 1;
                else if (x.distance < y.distance) return -1;
                else return 0;
            }
        }
        private class InterPointInfo
        {
            public IPoint pt;
            public ICoord2D coordInter;
            public int no;
            public string layerName;
            public string classify;
            public string diameter;
            public double height;
            public double terrainHeight;
            public string material;
            public string hlb;
            public double interHDist;
            public double distance;// 1、辅助排序；2、计算间隔
            public IColor65 color;

            public InterPointInfo()
            {
                color = RenderControlServices.Instance().SGWorld.Creator.CreateColor();
            }

        }

    }
}
