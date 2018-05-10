using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DFWinForms.Class;
using DF2DData.Class;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using DF2DPipe.Class;
using ICSharpCode.Core;
using System.Data;
using DevExpress.XtraBars.Docking;
using DF2DPipe.Query.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Display;
using DF2DPipe.Stats.Frm;
namespace DF2DPipe.Stats.Command
{
    class CmdRegionPipeNodeNumberStats2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        DataTable dtstats;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null||app.Workbench == null) return;
            app.Workbench.SetMenuEnable(true);
            m_ActiveView = app.Current2DMapControl.ActiveView;
            IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;

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

                    ISimpleFillSymbol pFillSym = new SimpleFillSymbol();

                    pFillSym.Color = pColor;
                    pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                    pFillSym.Outline = pLineSym;

                    object symbol = pFillSym as object;
                    IRubberBand band = new RubberPolygonClass();
                    IGeometry geo = band.TrackNew(m_Display, null);
                    app.Current2DMapControl.DrawShape(geo, ref symbol);
                    WaitForm.Start("正在查询...", "请稍后");
                    if (geo.IsEmpty)
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        geo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {
                        WaitForm.Start("正在统计...", "请稍后");
                        DataTable dtResult = RegionAnalysis(geo);
                        if (dtResult == null || dtResult.Rows.Count == 0)
                        {
                            WaitForm.Stop();
                            XtraMessageBox.Show("统计结果为空！", "提示");
                            return;
                        }
                        WaitForm.Stop();
                        FrmPipeNodeStatsOutput dialog = new FrmPipeNodeStatsOutput();
                        dialog.SetData1(dtResult);
                        //dialog.SetData1(dtstats);
                        dialog.ShowDialog();
                        if (dialog.DialogResult != System.Windows.Forms.DialogResult.OK)
                        {
                            this.RestoreEnv();
                        }
                    }
                }
            }
            catch 
            {
            	
            }
            
        }

        private DataTable RegionAnalysis(IGeometry geo)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.AddRange(new DataColumn[]{new DataColumn("PIPENODETYPE"),new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
            new DataColumn("NUMBER",typeof(long)),new DataColumn("TOTALNUMBER",typeof(long))});

            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPENODETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("NUMBER",typeof(int))});
            List<MajorClass> list = LogicDataStructureManage2D.Instance.GetAllMajorClass();
            foreach (MajorClass mc in list)
            {
                string[] arrFc2DId = mc.Fc2D.Split(';');
                if (arrFc2DId == null) continue;
                long majorclasscount = 0;
                int indexStart = dtResult.Rows.Count;
                foreach (SubClass sc in mc.SubClasses)
                {
                    long sccount = 0;
                    bool bHave = false;
                    foreach (string fc2DId in arrFc2DId)
                    {
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                        if (dffc == null) continue;
                        FacilityClass facc = dffc.GetFacilityClass();
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null || facc == null || facc.Name != "PipeNode") continue;
                        ISpatialFilter filter = new SpatialFilter();
                        filter.Geometry = geo;
                        filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc) +  mc.ClassifyField + " =  '" + sc.Name + "'";
                        filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        int count = fc.FeatureCount(filter);
                        if (count == 0) continue;
                        bHave = true;
                        sccount += count;
                    }
                    if (bHave)
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["PIPENODETYPE"] = mc;
                        dr["FIELDNAME"] = "";
                        dr["PVALUE"] = sc;
                        dr["NUMBER"] = sccount;
                        majorclasscount += sccount;
                        dtResult.Rows.Add(dr);

                        DataRow dr1 = dtstats.NewRow();
                        dr1["PIPENODETYPE"] = mc;
                        dr1["FIELDNAME"] = sc;
                        dr1["NUMBER"] = sccount;
                        dtstats.Rows.Add(dr1);
                    }
                }
                int indexEnd = dtResult.Rows.Count;
                for (int i = indexStart; i < indexEnd; i++)
                {
                    DataRow dr = dtResult.Rows[i];
                    dr["TOTALNUMBER"] = majorclasscount;
                }
            }
            return dtResult;
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
    }
}
