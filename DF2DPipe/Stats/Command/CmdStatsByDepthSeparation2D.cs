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
    class CmdStatsByDepthSeparation2D : AbstractMap2DCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            //List<string> systemFieldNames = new List<string>();
            //systemFieldNames.Add("STARTDEPTH");//添加起点埋深字段名
            //systemFieldNames.Add("ENDDEPTH");//添加终点埋深字段名

            //FrmSeparationStats2D dialog = new FrmSeparationStats2D(systemFieldNames);//初始化分段统计窗口
            FrmPropertyStatsByDepth2D dialog = new FrmPropertyStatsByDepth2D("埋深统计","PipeLine");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            //DataTable dt = dialog.DTTemp;//得到统计用数据表
            //DataTable dtstats = dialog.DTStats;//得到统计图表用数据表
            //FrmPipeLineStatsOutput2D dialog1 = new FrmPipeLineStatsOutput2D();//初始化管线长度统计输出窗口
            //dialog1.SetData2(dt);//设置统计窗口数据源
            //dialog1.SetStatsData(dtstats);//设置统计图表数据源
            //dialog1.ShowDialog();


        }
    }
}
