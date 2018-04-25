using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using DF2DControl.Base;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;

namespace DF2DEdit.BaseEdit
{
    public class cmdUnSel : AbstractMap2DCommand
    {
        private DF2DApplication m_App;
        private IMapControl2 m_MapControl;
        private IActiveView m_ActiveView;

        public override void Run(object sender, System.EventArgs e)
        {
            Map2DCommandManager.Push(this);
            IMap2DView mapView = UCService.GetContent(typeof(Map2DView)) as Map2DView;
            if (mapView == null) return;
            bool bBind = mapView.Bind(this);
            if (!bBind) return;

            m_App = DF2DApplication.Application;
            if (m_App == null || m_App.Current2DMapControl == null) return;
            m_MapControl = m_App.Current2DMapControl;

            this.m_ActiveView = m_MapControl.ActiveView; 
            this.m_ActiveView.FocusMap.ClearSelection();
            this.m_ActiveView.Refresh();
            m_App.Workbench.UpdateMenu();

        }

    }
}
