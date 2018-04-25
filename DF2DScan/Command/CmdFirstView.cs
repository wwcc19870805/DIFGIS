using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DData.Class;
using DFDataConfig.Class;
using DFWinForms.Service;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;



namespace DF2DScan.Command
{
    public class CmdFirstView: AbstractMap2DCommand
    {

        public override void Run(object sender, System.EventArgs e)
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            
            IExtentStack pExtentStack = app.Current2DMapControl.ActiveView.ExtentStack;
            IEnvelope pEnvelope = pExtentStack.get_Item(0);
            app.Current2DMapControl.ActiveView.Extent = pEnvelope;        
            app.Current2DMapControl.ActiveView.Refresh();
        }

        public override void RestoreEnv()
        {
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            mapView.UnBind(this);
            DF2DApplication app = DF2DApplication.Application;
            if (app == null || app.Current2DMapControl == null) return;
            app.Current2DMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            Map2DCommandManager.Pop();
        }
    }
}
