﻿using System;
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
    class CmdPipeLineLengthStats2D : AbstractMap2DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            FrmPipeLineLengthStats2D dialog = new FrmPipeLineLengthStats2D();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            
        }
    }
}
