using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Controls;
using ICSharpCode.Core;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using DFWinForms.Service;
using DFCommon.Class;
using DF2DControl.Command;
using DF2DControl.UserControl.Pad;
using DFWinForms.UserControl;
using DF2DControl.Base;
using DFWinForms.Command;

namespace DF2DControl.UserControl.View
{
    public class Map2DView : BaseViewContent, IMap2DView
    {
        private IMapControlEvents2_Event mapControlEvents = null;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        public ESRI.ArcGIS.Controls.AxMapControl MapControl
        {
            get
            {
                return axMapControl;
            }
        }

        public Map2DView()
        {
            try
            {
                DF2DApplication app = DF2DApplication.Application;
                InitializeComponent();
                DF2DApplication.Application.Current2DMapControl = (IMapControl2)this.axMapControl.GetOcx();
                mapControlEvents = axMapControl.GetOcx() as IMapControlEvents2_Event;
                mapControlEvents.OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(mapEvent_OnMapReplaced);
                mapControlEvents.OnExtentUpdated += new IMapControlEvents2_OnExtentUpdatedEventHandler(mapControlEvents_OnExtentUpdated);
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void mapControlEvents_OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            EagleEyePad pad = UCService.GetContent(typeof(EagleEyePad)) as EagleEyePad;
            if (pad == null) return;
            IEnvelope pEnvelope = (IEnvelope)newEnvelope;
            pad.DrawEnvelope(pEnvelope);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Workbench == null) return;
            app.Workbench.UpdateMenu();
        }
        private void RecursiveLayer(ILayer layer,AxMapControl mapControl,string eagleEyeLayerName)
        {
            if (layer is ICompositeLayer)
            {
                ICompositeLayer comLayer = layer as ICompositeLayer;
                for(int i = 0; i<comLayer.Count;i++)
                {
                    RecursiveLayer(comLayer.get_Layer(i), mapControl, eagleEyeLayerName);
                }
            }
            else if (layer is IRasterLayer)
            {
                IRasterLayer rasterLyaer = layer as IRasterLayer;
                if (rasterLyaer.Name == eagleEyeLayerName)
                {
                    mapControl.Map.AddLayer(layer);
                    mapControl.Extent = rasterLyaer.VisibleExtent;
                    mapControl.Refresh();
                }
            }
        }
        private void mapEvent_OnMapReplaced(object newMap)
        {
            this.axMapControl.ShowScrollbars = false;
            EagleEyePad pad = UCService.GetContent(typeof(EagleEyePad)) as EagleEyePad;
            if (pad == null) return;
            pad.MapControl.Map.ClearLayers();
            if (this.axMapControl.LayerCount > 0)
            {
                pad.MapControl.Map = new MapClass();
                for (int i = 0; i < this.axMapControl.LayerCount; i++)
                {
                    ILayer layer = this.axMapControl.Map.get_Layer(i);
                    RecursiveLayer(layer, pad.MapControl, Config.GetConfigValue("EagleEyeLayerName"));
                }
            }
        }

        public override bool Bind(ICommand cmd)
        {
            if (cmd == null) return false;
            if (!(cmd is IMap2DCommand)) return false;
            IMap2DCommand map2DCommand = cmd as IMap2DCommand;
            mapControlEvents.OnAfterDraw += new IMapControlEvents2_OnAfterDrawEventHandler(map2DCommand.OnAfterDraw);
            mapControlEvents.OnAfterScreenDraw += new IMapControlEvents2_OnAfterScreenDrawEventHandler(map2DCommand.OnAfterScreenDraw);
            mapControlEvents.OnBeforeScreenDraw += new IMapControlEvents2_OnBeforeScreenDrawEventHandler(map2DCommand.OnBeforeScreenDraw);
            mapControlEvents.OnDoubleClick += new IMapControlEvents2_OnDoubleClickEventHandler(map2DCommand.OnDoubleClick);
            mapControlEvents.OnExtentUpdated += new IMapControlEvents2_OnExtentUpdatedEventHandler(map2DCommand.OnExtentUpdated);
            mapControlEvents.OnFullExtentUpdated += new IMapControlEvents2_OnFullExtentUpdatedEventHandler(map2DCommand.OnFullExtentUpdated);
            mapControlEvents.OnKeyDown += new IMapControlEvents2_OnKeyDownEventHandler(map2DCommand.OnKeyDown);
            mapControlEvents.OnKeyUp += new IMapControlEvents2_OnKeyUpEventHandler(map2DCommand.OnKeyUp);
            mapControlEvents.OnMapReplaced += new IMapControlEvents2_OnMapReplacedEventHandler(map2DCommand.OnMapReplaced);
            mapControlEvents.OnMouseDown += new IMapControlEvents2_OnMouseDownEventHandler(map2DCommand.OnMouseDown);
            mapControlEvents.OnMouseMove += new IMapControlEvents2_OnMouseMoveEventHandler(map2DCommand.OnMouseMove);
            mapControlEvents.OnMouseUp += new IMapControlEvents2_OnMouseUpEventHandler(map2DCommand.OnMouseUp);
            mapControlEvents.OnSelectionChanged += new IMapControlEvents2_OnSelectionChangedEventHandler(map2DCommand.OnSelectionChanged);
            mapControlEvents.OnViewRefreshed += new IMapControlEvents2_OnViewRefreshedEventHandler(map2DCommand.OnViewRefreshed);
            return true;
        }
        public override void UnBind(ICommand cmd)
        {
            if (cmd == null) return;
            if (!(cmd is IMap2DCommand)) return;

            IMap2DCommand map2DCommand = cmd as IMap2DCommand;
            mapControlEvents.OnAfterDraw -= new IMapControlEvents2_OnAfterDrawEventHandler(map2DCommand.OnAfterDraw);
            mapControlEvents.OnAfterScreenDraw -= new IMapControlEvents2_OnAfterScreenDrawEventHandler(map2DCommand.OnAfterScreenDraw);
            mapControlEvents.OnBeforeScreenDraw -= new IMapControlEvents2_OnBeforeScreenDrawEventHandler(map2DCommand.OnBeforeScreenDraw);
            mapControlEvents.OnDoubleClick -= new IMapControlEvents2_OnDoubleClickEventHandler(map2DCommand.OnDoubleClick);
            mapControlEvents.OnExtentUpdated -= new IMapControlEvents2_OnExtentUpdatedEventHandler(map2DCommand.OnExtentUpdated);
            mapControlEvents.OnFullExtentUpdated -= new IMapControlEvents2_OnFullExtentUpdatedEventHandler(map2DCommand.OnFullExtentUpdated);
            mapControlEvents.OnKeyDown -= new IMapControlEvents2_OnKeyDownEventHandler(map2DCommand.OnKeyDown);
            mapControlEvents.OnKeyUp -= new IMapControlEvents2_OnKeyUpEventHandler(map2DCommand.OnKeyUp);
            mapControlEvents.OnMapReplaced -= new IMapControlEvents2_OnMapReplacedEventHandler(map2DCommand.OnMapReplaced);
            mapControlEvents.OnMouseDown -= new IMapControlEvents2_OnMouseDownEventHandler(map2DCommand.OnMouseDown);
            mapControlEvents.OnMouseMove -= new IMapControlEvents2_OnMouseMoveEventHandler(map2DCommand.OnMouseMove);
            mapControlEvents.OnMouseUp -= new IMapControlEvents2_OnMouseUpEventHandler(map2DCommand.OnMouseUp);
            mapControlEvents.OnSelectionChanged -= new IMapControlEvents2_OnSelectionChangedEventHandler(map2DCommand.OnSelectionChanged);
            mapControlEvents.OnViewRefreshed -= new IMapControlEvents2_OnViewRefreshedEventHandler(map2DCommand.OnViewRefreshed);

        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map2DView));
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapControl
            // 
            this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl.Location = new System.Drawing.Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(170, 168);
            this.axMapControl.TabIndex = 0;
            // 
            // Map2DView
            // 
            this.Controls.Add(this.axMapControl);
            this.Name = "Map2DView";
            this.Size = new System.Drawing.Size(170, 168);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.ResumeLayout(false);

        }

        public override void Activate()
        {
            base.Activate();
            DF2DApplication.Application.Workbench.SwitchToMenu(new string[] { "/Workbench/MainMenu/2D", "/Workbench/MainMenu" });
        }
    }
}
