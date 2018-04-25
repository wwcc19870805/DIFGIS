using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DDraw;
using DF3DControl.Command;
using Gvitech.CityMaker.FdeGeometry;
using DFDataConfig.Logic;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;
using DFDataConfig.Class;
using DF3DControl.Base;
using DevExpress.XtraEditors;
using DF3DPipe.Analysis.Class;
using System.Drawing;
using DFCommon.Class;
using DF3DPipe.Analysis.Frm;
using DFWinForms.Class;

namespace DF3DPipe.Analysis.Command
{
    public class CmdPipeTransect : AbstractMap3DCommand
    {
        private DrawTool _drawTool;

        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);

            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Line);
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
            Map3DCommandManager.Pop();
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
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Line && this._drawTool.GetGeo() != null)
            {
                LineQuery();
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void LineQuery()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IGeometry geo = this._drawTool.GetGeo();
                if (geo == null || geo.GeometryType != gviGeometryType.gviGeometryPolyline) return;
                IPolyline line = geo as IPolyline;
                IPolyline l = line.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
                if (l.Length > 500)
                {
                    XtraMessageBox.Show("横断面线超过500米，分析效率很低，请重新绘制", "提示");
                    return;
                }
                WaitForm.Start("正在进行横断面分析...", "请稍后");
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;

                List<MajorClass> list = LogicDataStructureManage3D.Instance.GetAllMajorClass();
                if (list == null) return;
                string road1 = "";
                string road2 = "";
                bool bAlert = false;
                double hmax = double.MinValue;
                double hmin = double.MaxValue;
                List<PPLine> pplines = new List<PPLine>();
                foreach (MajorClass mc in list)
                {
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible3D) continue;
                        string[] arrFc3DId = mc.Fc3D.Split(';');
                        if (arrFc3DId == null) continue;
                        foreach (string fc3DId in arrFc3DId)
                        {
                            DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                            if (dffc == null) continue;
                            IFeatureClass fc = dffc.GetFeatureClass();
                            FacilityClass fac = dffc.GetFacilityClass();
                            if (fc == null || fac == null || fac.Name != "PipeLine") continue;

                            IFieldInfoCollection fields = fc.GetFields();
                            int indexShape = fields.IndexOf("Shape");
                            if (indexShape == -1) continue;
                            int indexFootPrint = fields.IndexOf("FootPrint");
                            if (indexFootPrint == -1) continue;
                            DFDataConfig.Class.FieldInfo fiDiameter = fac.GetFieldInfoBySystemName("Diameter");
                            if (fiDiameter == null) continue;
                            int indexDiameter = fields.IndexOf(fiDiameter.Name);
                            if (indexDiameter == -1) continue;
                            DFDataConfig.Class.FieldInfo fiRoad = fac.GetFieldInfoBySystemName("Road");
                            int indexRoad = -1;
                            if (fiRoad != null) indexRoad = fields.IndexOf(fiRoad.Name);
                            DFDataConfig.Class.FieldInfo fiHLB = fac.GetFieldInfoBySystemName("HLB");
                            int indexHLB = -1;
                            if (fiHLB != null) indexHLB = fields.IndexOf(fiHLB.Name);
                            int indexClassify = fields.IndexOf(mc.ClassifyField);

                            ISpatialFilter pSpatialFilter = new SpatialFilter();
                            pSpatialFilter.Geometry = l;
                            pSpatialFilter.GeometryField = "FootPrint";
                            pSpatialFilter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                            pSpatialFilter.WhereClause = mc.ClassifyField + " = '" + sc.Name + "'";
                            
                            cursor = fc.Search(pSpatialFilter, false);
                            while ((row = cursor.NextRow()) != null)
                            {
                                if (indexRoad != -1 && !row.IsNull(indexRoad))
                                {
                                    if (road2 == "")
                                    {
                                        road1 = row.GetValue(indexRoad).ToString();
                                        road2 = row.GetValue(indexRoad).ToString();
                                    }
                                    else
                                    {
                                        road1 = row.GetValue(indexRoad).ToString();
                                        if (road1 != road2)
                                        {
                                            if (!bAlert)
                                            {
                                                XtraMessageBox.Show("横断面线跨越多条道路，当前只绘制在【" + road2 + "】上的管线横断面图。", "提示");
                                                bAlert = true;
                                            }
                                            continue;
                                        }
                                    }

                                }


                                double startSurfHeight = double.MaxValue;
                                double endSurfHeight = double.MaxValue;
                                if (!app.Current3DMapControl.Terrain.IsRegistered)
                                {
                                    DFDataConfig.Class.FieldInfo fiStartSurfHeight = fac.GetFieldInfoBySystemName("StartSurfHeight");
                                    if (fiStartSurfHeight == null) continue;
                                    int indexStartSurfHeight = fields.IndexOf(fiStartSurfHeight.Name);
                                    if (indexStartSurfHeight == -1) continue;
                                    DFDataConfig.Class.FieldInfo fiEndSurfHeight = fac.GetFieldInfoBySystemName("EndSurfHeight");
                                    if (fiEndSurfHeight == null) continue;
                                    int indexEndSurfHeight = fields.IndexOf(fiEndSurfHeight.Name);
                                    if (indexEndSurfHeight == -1) continue;
                                    if (!row.IsNull(indexStartSurfHeight)) startSurfHeight = double.Parse(row.GetValue(indexStartSurfHeight).ToString());
                                    if (!row.IsNull(indexEndSurfHeight)) endSurfHeight = double.Parse(row.GetValue(indexEndSurfHeight).ToString());
                                }
                                if (!row.IsNull(indexShape) && !row.IsNull(indexFootPrint))
                                {
                                    object objFootPrint = row.GetValue(indexFootPrint);
                                    object objShape = row.GetValue(indexShape);
                                    if (objFootPrint is IPolyline && objShape is IPolyline)
                                    {
                                        IPolyline polylineFootPrint = objFootPrint as IPolyline;
                                        IPolyline polylineShape = objShape as IPolyline;
                                        IGeometry geoIntersect = (geo as ITopologicalOperator2D).Intersection2D(polylineFootPrint);
                                        if (geoIntersect == null) continue;
                                        PPLine ppline = new PPLine();
                                        if (indexClassify == -1 || row.IsNull(indexClassify)) ppline.facType = mc.Name;
                                        else ppline.facType = row.GetValue(indexClassify).ToString();
                                        if (!row.IsNull(indexDiameter))
                                        {
                                            string diameter = row.GetValue(indexDiameter).ToString();
                                            if (diameter.Trim() == "") continue;
                                            ppline.dia = diameter;
                                            int indexDia = diameter.IndexOf('*');
                                            if (indexDia != -1)
                                            {
                                                ppline.isrect = true;
                                                int iDia1 ;
                                                bool bDia1 = int.TryParse(diameter.Substring(0, indexDia), out iDia1);
                                                if (!bDia1) continue;
                                                int iDia2;
                                                bool bDia2 = int.TryParse(diameter.Substring(indexDia + 1, diameter.Length - indexDia - 1), out iDia2);
                                                if (!bDia2) continue;
                                                ppline.gj.Add(iDia1);
                                                ppline.gj.Add(iDia2);
                                            }
                                            else
                                            {
                                                ppline.isrect = false;
                                                int iDia;
                                                bool bDia = int.TryParse(diameter, out iDia);
                                                if (!bDia) continue;
                                                ppline.gj.Add(iDia);
                                                ppline.gj.Add(iDia);
                                            }
                                        }
                                        int hlb = 0;
                                        if (indexHLB != -1 && !row.IsNull(indexHLB))
                                        {
                                            string strhlb = row.GetValue(indexHLB).ToString();
                                            if (strhlb.Contains("内"))
                                            {
                                                hlb = 1;
                                            }
                                            else if (strhlb.Contains("外"))
                                            {
                                                hlb = -1;
                                            }
                                            else hlb = 0;
                                            ppline.hlb = hlb;
                                        }
                                        #region  交点为一个
                                        if (geoIntersect.GeometryType == gviGeometryType.gviGeometryPoint) //交点为1个
                                        {
                                            IPoint ptIntersect = geoIntersect as IPoint;
                                            ppline.interPoint = new PPPoint(ptIntersect.X, ptIntersect.Y, ptIntersect.Z);
                                            ppline.clh = GetInterPointHeight(ptIntersect, polylineShape, polylineFootPrint);
                                            if (ppline.clh > hmax) hmax = ppline.clh;
                                            if (ppline.clh < hmin) hmin = ppline.clh;

                                            if (app.Current3DMapControl.Terrain.IsRegistered)
                                            {
                                                ppline.cgh = app.Current3DMapControl.Terrain.GetElevation(ptIntersect.X, ptIntersect.Y, Gvitech.CityMaker.RenderControl.gviGetElevationType.gviGetElevationFromDatabase);
                                            }
                                            else
                                            {
                                                ppline.cgh = startSurfHeight + (endSurfHeight - startSurfHeight) 
                                                    * Math.Sqrt((polylineFootPrint.StartPoint.X - ptIntersect.X) * (polylineFootPrint.StartPoint.X - ptIntersect.X)
                                                    + (polylineFootPrint.StartPoint.Y - ptIntersect.Y) * (polylineFootPrint.StartPoint.Y - ptIntersect.Y)) / polylineFootPrint.Length;
                                            }
                                            if (ppline.cgh > hmax) hmax = ppline.cgh;
                                            if (ppline.cgh < hmin) hmin = ppline.cgh;
                                            // 辅助画图
                                            ppline.startPt = new PPPoint(l.StartPoint.X, l.StartPoint.Y, l.StartPoint.Z);
                                            pplines.Add(ppline);
                                        }
                                        #endregion

                                        #region 交点为多个
                                        else if (geoIntersect.GeometryType == gviGeometryType.gviGeometryMultiPoint) //交点为多个
                                        {
                                            IMultiPoint multiPts = geoIntersect as IMultiPoint;
                                            for (int m = 0; m < multiPts.GeometryCount; m++)
                                            {
                                                IPoint ptIntersect = multiPts.GetPoint(m);
                                                ppline.interPoint = new PPPoint(ptIntersect.X, ptIntersect.Y, ptIntersect.Z);
                                                ppline.clh = GetInterPointHeight(ptIntersect, polylineShape, polylineFootPrint);
                                                if (ppline.clh > hmax) hmax = ppline.clh;
                                                if (ppline.clh < hmin) hmin = ppline.clh;

                                                if (app.Current3DMapControl.Terrain.IsRegistered)
                                                {
                                                    ppline.cgh = app.Current3DMapControl.Terrain.GetElevation(ptIntersect.X, ptIntersect.Y, Gvitech.CityMaker.RenderControl.gviGetElevationType.gviGetElevationFromDatabase);
                                                }
                                                else
                                                {
                                                    ppline.cgh = startSurfHeight + (endSurfHeight - startSurfHeight)
                                                        * Math.Sqrt((polylineFootPrint.StartPoint.X - ptIntersect.X) * (polylineFootPrint.StartPoint.X - ptIntersect.X)
                                                        + (polylineFootPrint.StartPoint.Y - ptIntersect.Y) * (polylineFootPrint.StartPoint.Y - ptIntersect.Y)) / polylineFootPrint.Length;
                                                }
                                                if (ppline.cgh > hmax) hmax = ppline.cgh;
                                                if (ppline.cgh < hmin) hmin = ppline.cgh;

                                                // 辅助画图
                                                ppline.startPt = new PPPoint(l.StartPoint.X, l.StartPoint.Y, l.StartPoint.Z);
                                                pplines.Add(ppline);
                                            }
                                        }
                                        #endregion

                                        else continue;
                                    }
                                }
                            }
                        }
                    }
                }
                WaitForm.Stop();
                if (pplines.Count < 2)
                {
                    XtraMessageBox.Show("相交管线少于2个", "提示");
                    return;
                }
                pplines.Sort(new PPLineCompare());
                double spacesum = 0.0;
                for (int i = 1; i < pplines.Count; i++)
                {
                    PPLine line1 = pplines[i - 1];
                    PPLine line2 = pplines[i];
                    line2.space = Math.Sqrt((line1.interPoint.X - line2.interPoint.X) * (line1.interPoint.X - line2.interPoint.X) 
                        + (line1.interPoint.Y - line2.interPoint.Y) * (line1.interPoint.Y - line2.interPoint.Y));
                    spacesum += line2.space;
                };
                var str1 = (pplines[0].interPoint.X / 1000).ToString("0.00");
                var str2 = (pplines[0].interPoint.Y / 1000).ToString("0.00");
                string mapNum = str2 + "-" + str1;
                string mapName = SystemInfo.Instance.SystemFullName + "横断面图";
                FrmSectionAnalysis dialog = new FrmSectionAnalysis("横断面分析结果", 0);
                dialog.SetInfo(mapName, mapNum, pplines, hmax, hmin, spacesum, road2);
                dialog.Show();
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }

        private double GetInterPointHeight(IPoint interPoint, IPolyline geoShape, IPolyline geo)
        {
            if (interPoint == null || geoShape == null || geoShape == null) return 0.0;
            if (geoShape.PointCount == 2)
            {
                return geoShape.StartPoint.Z + (geoShape.EndPoint.Z - geoShape.StartPoint.Z) * Math.Sqrt((geo.StartPoint.X - interPoint.X) * (geo.StartPoint.X - interPoint.X)
                    + (geo.StartPoint.Y - interPoint.Y) * (geo.StartPoint.Y - interPoint.Y)) / geo.Length;
            }
            for (int i = 0; i < geoShape.PointCount - 1; i++)
            {
                IPoint pt1 = geoShape.GetPoint(i);
                IPoint pt2 = geoShape.GetPoint(i + 1);
                IPoint ptFootPrint1 = geo.GetPoint(i);
                IPoint ptFootPrint2 = geo.GetPoint(i + 1);
                double len = Math.Sqrt((ptFootPrint1.X - ptFootPrint2.X) * (ptFootPrint1.X - ptFootPrint2.X)
                    + (ptFootPrint1.Y - ptFootPrint2.Y) * (ptFootPrint1.Y - ptFootPrint2.Y));
                double x = interPoint.X;
                double y = interPoint.Y;
                double x1 = ptFootPrint1.X;
                double y1 = ptFootPrint1.Y;
                double z1 = pt1.Z;
                double x2 = ptFootPrint2.X;
                double y2 = ptFootPrint2.Y;
                double z2 = pt2.Z;
                double det = 0.5;
                bool b1 = (x >= x1 || Math.Abs(x - x1) < det) && (x <= x2 || Math.Abs(x - x2) < det) && (y <= y1 || Math.Abs(y - y1) < det) && (y >= y2 || Math.Abs(y - y2) < det);
                bool b2 = (x <= x1 || Math.Abs(x - x1) < det) && (x >= x2 || Math.Abs(x - x2) < det) && (y >= y1 || Math.Abs(y - y1) < det) && (y <= y2 || Math.Abs(y - y2) < det);
                bool b3 = (x >= x1 || Math.Abs(x - x1) < det) && (x <= x2 || Math.Abs(x - x2) < det) && (y >= y1 || Math.Abs(y - y1) < det) && (y >= y2 || Math.Abs(y - y2) < det);
                bool b4 = (x <= x1 || Math.Abs(x - x1) < det) && (x >= x2 || Math.Abs(x - x2) < det) && (y <= y1 || Math.Abs(y - y1) < det) && (y >= y2 || Math.Abs(y - y2) < det);
                if (b1 || b2 || b3 || b4)
                {
                    double res = z1 + (z2 - z1) * Math.Sqrt((x1 - x) * (x1 - x)
                    + (y1 - y) * (y1 - y)) / len;
                    return res;

                    //            var detx1 = x1 - x;
                    //            var dety1 = y1 - y;
                    //            var detx2 = x2 - x;
                    //            var dety2 = y2 - y;
                    //            var temp = dety2 * detx1 + dety1 * detx2;
                    //            if (Math.abs(temp) < 0.00001) {
                    //            }
                }
            }
            return 0.0;
        }

    }
}
