using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DFWinForms.Class;
using DF2DData.Class;
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
using DF2DTool.Frm;

namespace DF2DTool.Command
{
    public class CmdMapExportByRectangle : AbstractMap2DCommand
    {
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
        private IActiveView m_ActiveView;
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null) return;
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
                    IRubberBand band = new RubberRectangularPolygonClass();
                    IGeometry pGeo = band.TrackNew(m_Display, null);
                    app.Current2DMapControl.DrawShape(pGeo, ref symbol);
                    WaitForm.Start("正在查询...", "请稍后");
                    if (pGeo.IsEmpty)
                    {
                        IPoint searchPoint = new PointClass();
                        searchPoint.PutCoords(mapX, mapY);
                        pGeo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
                        //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
                    }
                    if (ready)
                    {
                        IMap pMap = app.Current2DMapControl.Map;
                        ISelectionEnvironment selEnv = new SelectionEnvironmentClass();
                        pMap.SelectByShape(pGeo, selEnv, false);
                        ISelection pSelection = pMap.FeatureSelection;
                      
                        FrmDxfExport dialog = new FrmDxfExport(pSelection,pMap);
                        WaitForm.Stop();
                        dialog.ShowDialog();


                    }
                }
            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
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
            app.Current2DMapControl.Map.ClearSelection();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }


    }
}
