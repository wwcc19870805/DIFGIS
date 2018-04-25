using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ICSharpCode.Core;
using DFWinForms.Class;
using DevExpress.XtraBars.Docking;
using DF2DPipe.Query.UC;
using DF2DPipe.Frm;
using DFWinForms.Command;
using DF2DControl.Command;
using ESRI.ArcGIS.Geodatabase;
using System.Data;
using System.Collections;
using DF2DPipe.Class;
using DF2DPipe.Stats.Frm;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking;
namespace DF2DPipe.Stats.Command
{
    class CmdStatsByMaterial2D : AbstractMap2DCommand
    {
       
        public override void Run(object sender, EventArgs e)
        {
            FrmPropertyStats2D dialog = new FrmPropertyStats2D(this.CommandName, "Material");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            //DataTable dt = dialog.DTTemp;
            //DataTable dtstats = dialog.DTStats;
            //string chartTitle = "管线材质统计图" + " 单位：米";
            //FrmPipeLineStatsOutput dialog1 = new FrmPipeLineStatsOutput();
            //dialog1.SetData(dt);
            ////dialog1.SetData1(dtstats);
            //dialog1.ShowDialog();

           
        }

        
    }
}
