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
    class CmdCompoundConditionStats2D : AbstractMap2DCommand
    {
        private IActiveView m_ActiveView;
        IGeometry _geo;
        bool spatialStats = false;
        public override void Run(object sender, System.EventArgs e)
        {
            //Map2DCommandManager.Push(this);
            //IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            //if (mapView == null) return;
            //bool bBind = mapView.Bind(this);
            //if (!bBind) return;
            //DF2DApplication app = DF2DApplication.Application;
            //if (app == null || app.Current2DMapControl == null) return;
            //app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
            FrmCompoundConditionStats2D dialog = new FrmCompoundConditionStats2D();
            dialog.SetData(LogicDataStructureManage2D.Instance.RootLogicGroups, LogicDataStructureManage2D.Instance.RootMajorClasses, null);
            dialog.ShowDialog();
            
            
        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            //DF2DApplication app = DF2DApplication.Application;
            //bool ready = true;
            //if (app == null || app.Current2DMapControl == null) return;
            //m_ActiveView = app.Current2DMapControl.ActiveView;
            //IScreenDisplay m_Display = app.Current2DMapControl.ActiveView.ScreenDisplay;

            //try
            //{
            //    if (button == 1)
            //    {
            //        ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
            //        IRgbColor pColor = new RgbColorClass();
            //        pColor.Red = 255;
            //        pColor.Green = 255;
            //        pColor.Blue = 0;
            //        pLineSym.Color = pColor;
            //        pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
            //        pLineSym.Width = 2;

            //        ISimpleFillSymbol pFillSym = new SimpleFillSymbol();

            //        pFillSym.Color = pColor;
            //        pFillSym.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            //        pFillSym.Outline = pLineSym;

            //        object symbol = pFillSym as object;
            //        IRubberBand band = new RubberPolygonClass();
            //        IGeometry geo = band.TrackNew(m_Display, null);
            //        app.Current2DMapControl.DrawShape(geo, ref symbol);
            //        WaitForm.Start("正在查询...", "请稍后");

            //        if (geo.IsEmpty)
            //        {
            //            IPoint searchPoint = new PointClass();
            //            searchPoint.PutCoords(mapX, mapY);
            //            geo = PublicFunction.DoBuffer(searchPoint, PublicFunction.ConvertPixelsToMapUnits(m_ActiveView, GlobalValue.System_Selection_Option().Tolerate));
            //            //m_ActiveView.FocusMap.SelectByShape(geo, s, false);
            //        }
            //        _geo = geo;
            //        spatialStats = true;
            //        if (spatialStats)
            //        {
            //            FrmCompoundConditionStats2D dialog = new FrmCompoundConditionStats2D(_geo);
            //            dialog.SetData(LogicDataStructureManage2D.Instance.RootLogicGroups, LogicDataStructureManage2D.Instance.RootMajorClasses, null);
            //            dialog.ShowDialog();
            //        }
                    
            //    }
            //}
            //catch
            //{
            //}
        }
                    
  
    }
}
