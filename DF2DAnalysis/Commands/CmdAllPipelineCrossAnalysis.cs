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
using DF2DAnalysis.Frm;
using System;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;

namespace DF2DAnalysis.Commands
{
    public class CmdAllPipelineCrossAnalysis : AbstractMap2DCommand
    {
        public override void Run(object sender, System.EventArgs e)
        {
            FrmAllPipelineCross dialog = new FrmAllPipelineCross("Diameter","HLB");
            dialog.Show();
        }
        

     

    }
}
