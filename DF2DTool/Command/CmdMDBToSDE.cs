using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DControl.UserControl.View;
using DFWinForms.Service;
using DF2DControl.Base;
using ESRI.ArcGIS.Controls;
using DF2DTool.Frm;
using DFCommon.Class;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Windows.Forms;
using DF2DTool.Class;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace DF2DTool.Command
{
    class CmdMDBToSDE : AbstractMap2DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            string path = Config.GetConfigValue("2DMdbPipe");
            IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
            IWorkspace pWs = pWsF.OpenFromFile(path, 0);

            FrmMDBToSDE m_frmMDBToSDE = new FrmMDBToSDE(pWs);
            m_frmMDBToSDE.ShowDialog();
        }
    }
}
