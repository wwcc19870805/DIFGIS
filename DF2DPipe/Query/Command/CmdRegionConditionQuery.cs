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
using DF2DPipe.Query.Frm;

namespace DF2DPipe.Query.Command
{
    class CmdRegionConditionQuery : AbstractMap2DCommand
    {
        private DockPanel _dockPanel;
        private int _height = 400;
        private UIDockPanel _uPanel;
        private int _width = 265;
        private UCPropertyInfo2D _uc;
        private IActiveView m_ActiveView;
        IGeometry _geo;
        bool spatialQuery = false;
        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(false);
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;

        }
        public override void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            DF2DApplication app = DF2DApplication.Application;
            bool ready = true;
            if (app == null || app.Current2DMapControl == null || app.Workbench == null) return;
            //app.Workbench.SetMenuEnable(true);
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
                    _geo = geo;
                    spatialQuery = true;
                    if (spatialQuery)
                    {
                        FrmCompoundConditionQuery dialog = new FrmCompoundConditionQuery();
                        dialog.SetData(LogicDataStructureManage2D.Instance.RootLogicGroups, LogicDataStructureManage2D.Instance.RootMajorClasses, _geo);
                        WaitForm.Stop();
                        IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
                        mapView.UnBind(this);
                        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                        this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location, this._width, this._height);
                        this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                        this._dockPanel.Visibility = DockVisibility.Visible;
                        this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                        this._dockPanel.Width = this._width;
                        this._dockPanel.Height = this._height;
                        this._uc = new UCPropertyInfo2D();
                        this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
                        this._uPanel.RegisterEvent(new PanelClose(this.Close));
                        this._dockPanel.Controls.Add(this._uc);
                        this._uc.SetPropertyInfo(dialog.Dict);
                    }
                }
            }


            catch
            {
                this.RestoreEnv();
            }
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
            if (mapView == null) return;
            mapView.UnBind(this);
            Map2DCommandManager.Pop();
        }
        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }
    }
}
