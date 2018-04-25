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

namespace DF2DPipe.Stats.Command
{
    class CmdStatsByOwner2D : AbstractMap2DCommand
    {
       
        public override void Run(object sender, EventArgs e)
        {
            FrmPropertyStats2D dialog = new FrmPropertyStats2D(this.CommandName, "Owner");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            //DataTable dt = dialog.DTTemp;
            //DataTable dtstats = dialog.DTStats;
            ////string chartTitle = "管线材质统计图" + " 单位：米";
            //FrmPipeLineStatsOutput dialog1 = new FrmPipeLineStatsOutput();
            //dialog1.SetData(dt);
            //dialog1.SetData1(dtstats);
            //dialog1.ShowDialog();
        }

        //private void Close()
        //{
        //    this.RestoreEnv();
        //}

        //public override void RestoreEnv()
        //{
        //    if (this._uPanel != null)
        //    {
        //        this._uPanel.GetControlContainer().Controls.Clear();
        //        this._uPanel.Close();
        //        this._uPanel = null;
        //    }
        //    Map2DCommandManager.Pop();
        //}

        //private System.Drawing.Point Location
        //{
        //    get
        //    {
        //        int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        //        int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        //        return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
        //    }
        //}
    }
}
