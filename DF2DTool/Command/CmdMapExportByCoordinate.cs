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
    class CmdMapExportByCoordinate : AbstractMap2DCommand
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
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            try
            {
                IPoint pPoint = new PointClass();
                pPoint.PutCoords(mapX, mapY);
                FrmMapExportByCoordinate dialog = new FrmMapExportByCoordinate(pPoint);
                dialog.ShowDialog();

                if (dialog.DialogResult == DialogResult.OK)
                {
                    IPointCollection pColl = new PolygonClass();
                    pColl.AddPoint(pPoint);
                    IPoint point1 = new PointClass();
                    point1.PutCoords(mapX + dialog.Length, mapY);
                    pColl.AddPoint(point1);
                    IPoint point2 = new PointClass();
                    point2.PutCoords(mapX, mapY + dialog.Width);
                    pColl.AddPoint(point2);
                    IPoint point3 = new PointClass();
                    point3.PutCoords(mapX + dialog.Length, mapY + dialog.Width);
                    pColl.AddPoint(point3);
                    IGeometry pGeo = pColl as IGeometry;

                    DF2DApplication app = DF2DApplication.Application;
                    IMap pMap = app.Current2DMapControl.Map;
                    ISelectionEnvironment selEnv = new SelectionEnvironmentClass();
                    pMap.SelectByShape(pGeo, selEnv, false);
                    ISelection pSelection = pMap.FeatureSelection;

                    FrmDxfExport dialog1 = new FrmDxfExport(pSelection, pMap);
                    WaitForm.Stop();
                    dialog1.ShowDialog();
                }
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
            app.Current2DMapControl.Map.ClearSelection();
            app.Current2DMapControl.ActiveView.Refresh();
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
        
    }
}
