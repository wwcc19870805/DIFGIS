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
using DF2DData.Class;

namespace DF2DAnalysis.Commands
{
    class CmdPipeTransect : AbstractMap2DCommand
    {
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

        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            IActiveView m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;
            IGeometry pGeo = null;
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
                    IGeometry pLine = pRubberBand.TrackNew(m_Display, null);

                    object symbol = pLineSym as object;
                    app.Current2DMapControl.DrawShape(pLine, ref symbol);

                    
                    if ((pLine as IPolyline).Length > 500)
                    {
                        XtraMessageBox.Show("横断面线超过500米，分析效率很低，请重新绘制", "提示");
                        return;
                    }
                    WaitForm.Start("正在进行横断面分析...", "请稍后");
                    pGeo = pLine;
                    if (pGeo == null) return;

                    string road1 = "";
                    string road2 = "";
                    bool bAlert = false;
                    double hmax = double.MinValue;
                    double hmin = double.MaxValue;
                    List<PPLine2D> pplines = new List<PPLine2D>();                  
                    IFeatureCursor pFeatureCursor = null;
                    IFeature pFeature = null;

                    foreach (MajorClass mc in LogicDataStructureManage2D.Instance.GetAllMajorClass())
                    {
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            if (!sc.Visible2D) continue;
                            string[] arrFc2DId = mc.Fc2D.Split(';');
                            if (arrFc2DId == null) continue;
                            foreach (string fc2DId in arrFc2DId)
                            {
                                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                                if (dffc == null) continue;
                                IFeatureClass fc = dffc.GetFeatureClass();
                                if (fc == null) continue;
                                FacilityClass facc = dffc.GetFacilityClass();
                                if (facc.Name != "PipeLine") continue;

                                //查找管径长宽字段，获得该要素类下字段索引值，若为圆管，则长宽相等
                                DFDataConfig.Class.FieldInfo fiDia = facc.GetFieldInfoBySystemName("Diameter");
                                DFDataConfig.Class.FieldInfo fiDia1 = facc.GetFieldInfoBySystemName("Diameter1");
                                DFDataConfig.Class.FieldInfo fiDia2 = facc.GetFieldInfoBySystemName("Diameter2");
                                int indexDia = fc.Fields.FindField(fiDia.Name);
                                int indexDiaWith = fc.Fields.FindField(fiDia1.Name);
                                int indexDiaHeight = fc.Fields.FindField(fiDia2.Name);
                                if (indexDiaWith == -1 || indexDiaHeight == -1||indexDia == -1) continue;
                                //查找道路字段索引
                                DFDataConfig.Class.FieldInfo fiRoad = facc.GetFieldInfoBySystemName("Road");
                                int indexRoad = fc.Fields.FindField(fiRoad.Name);
                                //查找管线高类别索引
                                DFDataConfig.Class.FieldInfo fiHLB = facc.GetFieldInfoBySystemName("HLB");
                                int indexHLB = fc.Fields.FindField(fiHLB.Name);
                                //二级分类名索引
                                int indexClassify = fc.Fields.FindField(mc.ClassifyField);

                                ISpatialFilter pSpatialFilter = new SpatialFilter();
                                pSpatialFilter.Geometry = pGeo;
                                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                pSpatialFilter.WhereClause = UpOrDown.DecorateWhereClasuse(fc) + mc.ClassifyField + " =  '" + sc.Name + "'";
                                int count = fc.FeatureCount(pSpatialFilter);
                                if (count == 0) continue;
                                pFeatureCursor = fc.Search(pSpatialFilter, true);
                                while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                {
                                    if (indexRoad != -1)
                                    {
                                        if (road2 == "")
                                        {
                                            road1 = pFeature.get_Value(indexRoad).ToString();
                                            road2 = pFeature.get_Value(indexRoad).ToString();
                                        }
                                        else
                                        {
                                            road1 = pFeature.get_Value(indexRoad).ToString();
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
                                    //查找管线的起点终点地面高
                                    double startSurfHeight = double.MaxValue;
                                    double endSurfHeight = double.MaxValue;
                                    DFDataConfig.Class.FieldInfo fiStartSurfHeight = facc.GetFieldInfoBySystemName("StartSurfH");
                                    if (fiStartSurfHeight == null) continue;
                                    int indexStartSurfHeight = pFeature.Fields.FindField(fiStartSurfHeight.Name);
                                    if (indexStartSurfHeight == -1) continue;

                                    DFDataConfig.Class.FieldInfo fiEndSurfHeight = facc.GetFieldInfoBySystemName("EndSurfH");
                                    if (fiEndSurfHeight == null) continue;
                                    int indexEndSurfHeight = pFeature.Fields.FindField(fiEndSurfHeight.Name);
                                    if (indexEndSurfHeight == -1) continue;

                                    //若管线属性地面高字段为null，则从DEM取值
                                    if (pFeature.get_Value(indexStartSurfHeight).ToString() == "" || pFeature.get_Value(indexEndSurfHeight).ToString() == "")
                                    {
                                        startSurfHeight = ReadDemRaster.GetH((pFeature.Shape as IPolyline).FromPoint);
                                        endSurfHeight = ReadDemRaster.GetH((pFeature.Shape as IPolyline).ToPoint);
                                    }
                                    else
                                    {
                                        startSurfHeight = Convert.ToDouble(pFeature.get_Value(indexStartSurfHeight).ToString());
                                        endSurfHeight = Convert.ToDouble(pFeature.get_Value(indexEndSurfHeight).ToString());
                                    }
                                  
                                    //查找管线起点高程和终点高程
                                    double startDepthHeight = double.MaxValue;
                                    double endDepthHeight = double.MaxValue;
                                    DFDataConfig.Class.FieldInfo fiStartDepthHeight = facc.GetFieldInfoBySystemName("StartHeight2D");
                                    if (fiStartDepthHeight == null) continue;
                                    int indexStartDepthHeight = pFeature.Fields.FindField(fiStartDepthHeight.Name);
                                    if (indexStartDepthHeight == -1) continue;

                                    DFDataConfig.Class.FieldInfo fiEndDepthHeight = facc.GetFieldInfoBySystemName("EndHeight");
                                    if (fiEndDepthHeight == null) continue;
                                    int indexEndDepthHeight = pFeature.Fields.FindField(fiEndDepthHeight.Name);
                                    if (indexEndDepthHeight == -1) continue;
                                    startDepthHeight = Convert.ToDouble(pFeature.get_Value(indexStartDepthHeight).ToString());
                                    endDepthHeight = Convert.ToDouble(pFeature.get_Value(indexEndDepthHeight).ToString());


                                    //计算管线和断面的交点
                                    IGeometry ppGeo = pFeature.Shape;
                                    ITopologicalOperator pTopo = ppGeo as ITopologicalOperator;
                                    IGeometry geoIntersect = pTopo.Intersect(pGeo, esriGeometryDimension.esriGeometry0Dimension);
                                    if (geoIntersect == null) continue;
                                    PPLine2D ppline = new PPLine2D();
                                    if (indexClassify == -1) ppline.facType = mc.Name;
                                    else ppline.facType = pFeature.get_Value(indexClassify).ToString();
                                    //查找管线的管径，判断其是方管还是圆管
                                    string diameter = pFeature.get_Value(indexDia).ToString();
                                    string diameter1 = pFeature.get_Value(indexDiaWith).ToString();
                                    string diameter2 = pFeature.get_Value(indexDiaHeight).ToString();

                                    if (diameter.Trim() == "") continue;
                                    ppline.dia = diameter;
                                    int indexSplit = diameter.IndexOf('*');
                                    if (indexSplit != -1)
                                    {
                                        ppline.isrect = true;
                                        int iDia1;
                                        bool bDia1 = int.TryParse(diameter.Substring(0, indexSplit), out iDia1);
                                        if (!bDia1) continue;
                                        int iDia2;
                                        bool bDia2 = int.TryParse(diameter.Substring(indexSplit + 1, diameter.Length - indexSplit - 1), out iDia2);
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
                                    //判断管线高方式
                                    int hlb = 0;
                                    if (indexHLB != -1)
                                    {
                                        string strhlb = pFeature.get_Value(indexHLB).ToString();
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
                                    if (geoIntersect.GeometryType == esriGeometryType.esriGeometryPoint)
                                    {
                                        IPolyline polyline = pFeature.Shape as IPolyline;
                                        IPoint ptIntersect = geoIntersect as IPoint;
                                        ppline.interPoint = new PPPoint(ptIntersect.X, ptIntersect.Y);
                                        ppline.clh = GetInterPointHeight(ptIntersect, polyline, startDepthHeight, endDepthHeight);
                                        if (ppline.clh > hmax) hmax = ppline.clh;
                                        if (ppline.clh < hmin) hmin = ppline.clh;
                                        ppline.cgh = startSurfHeight + (endSurfHeight - startSurfHeight)
                                                       * Math.Sqrt((polyline.FromPoint.X - ptIntersect.X) * (polyline.FromPoint.X - ptIntersect.X)
                                                       + (polyline.FromPoint.Y - ptIntersect.Y) * (polyline.FromPoint.Y - ptIntersect.Y)) / polyline.Length;
                                        if (ppline.cgh > hmax) hmax = ppline.cgh;
                                        if (ppline.cgh < hmin) hmin = ppline.cgh;
                                        // 辅助画图
                                        IPolyline l = pGeo as IPolyline;
                                        ppline.startPt = new PPPoint(l.FromPoint.X, l.FromPoint.Y);
                                        pplines.Add(ppline);
                                    }

#endregion
                                    #region  交点为多个
                                    else if (geoIntersect.GeometryType == esriGeometryType.esriGeometryMultipoint)
                                    {
                                        IPolyline polyline = pFeature.Shape as IPolyline;
                                        IPointCollection geoCol = geoIntersect as IPointCollection;
                                        for (int i = 0; i < geoCol.PointCount; i++)
                                        {
                                            
                                            IPoint ptIntersect = geoCol.get_Point(i);
                                            ppline.interPoint = new PPPoint(ptIntersect.X, ptIntersect.Y);
                                            ppline.clh = GetInterPointHeight(ptIntersect, polyline,startDepthHeight,endDepthHeight);
                                            if (ppline.clh > hmax) hmax = ppline.clh;
                                            if (ppline.clh < hmin) hmin = ppline.clh;
                                            ppline.cgh = startSurfHeight + (endSurfHeight - startSurfHeight)
                                                           * Math.Sqrt((polyline.FromPoint.X - ptIntersect.X) * (polyline.FromPoint.X - ptIntersect.X)
                                                           + (polyline.FromPoint.Y - ptIntersect.Y) * (polyline.FromPoint.Y - ptIntersect.Y)) / polyline.Length;
                                            if (ppline.cgh > hmax) hmax = ppline.cgh;
                                            if (ppline.cgh < hmin) hmin = ppline.cgh;
                                            // 辅助画图
                                            IPolyline l = pGeo as IPolyline;
                                            ppline.startPt = new PPPoint(l.FromPoint.X, l.FromPoint.Y);
                                            pplines.Add(ppline);
                                        }
                                    }
                                    else continue;
#endregion
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
                    pplines.Sort(new PPLineCompare2D());
                    double spacesum = 0.0;
                    for (int i = 1; i < pplines.Count; i++)
                    {
                        PPLine2D line1 = pplines[i - 1];
                        PPLine2D line2 = pplines[i];
                        line2.space = Math.Sqrt((line1.interPoint.X - line2.interPoint.X) * (line1.interPoint.X - line2.interPoint.X)
                            + (line1.interPoint.Y - line2.interPoint.Y) * (line1.interPoint.Y - line2.interPoint.Y));
                        spacesum += line2.space;
                    };
                    var str1 = (pplines[0].interPoint.X / 1000).ToString("0.00");
                    var str2 = (pplines[0].interPoint.Y / 1000).ToString("0.00");
                    string mapNum = str2 + "-" + str1;
                    string mapName = SystemInfo.Instance.SystemFullName + "横断面图";
                    FrmSectionAnalysis dialog = new FrmSectionAnalysis("横断面分析结果",0);
                    dialog.SetInfo(mapName, mapNum, pplines, hmax, hmin, spacesum, road2);
                    dialog.Show();
                }
            }
            catch 
            {
                WaitForm.Stop();
            }
        }

        private double GetInterPointHeight(IPoint point, IPolyline polyline,double startDepthHeight,double endDepthHeight)
        {
            if (point == null || polyline == null) return 0.0;
            double deltaH = endDepthHeight - startDepthHeight;
            double deltaX = polyline.ToPoint.X - polyline.FromPoint.X;
            double deltaIntersectX = point.X - polyline.FromPoint.X;
            double h = (deltaIntersectX/deltaX)*deltaH + startDepthHeight;
            return h;
        }
            
        
        private double GetDistanceOfTwoPoints(IPoint P1, IPoint P2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

            return Result;
        }

    }
}
