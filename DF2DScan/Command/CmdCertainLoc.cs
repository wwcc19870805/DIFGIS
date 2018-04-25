using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.Service;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DF2DControl.Base;
using DF2DData.Class;
using DFDataConfig.Class;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using DevExpress.XtraBars.Docking;
using DFWinForms.Class;
using DF2DScan.UserControl;
using DF2DScan.Frm;

namespace DF2DScan.Command
{
    public class CmdCertainLoc: AbstractMap2DCommand
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


            FrmCertainLoc frmCertainLoc = new FrmCertainLoc();
            frmCertainLoc.Show();
        }
    }
}
