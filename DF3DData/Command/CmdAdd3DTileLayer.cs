using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DData.Frm;
using DF3DData.Class;
using DF3DData.UserControl.Pad;
using DFWinForms.Service;

namespace DF3DData.Command
{
    public class CmdAdd3DTileLayer : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            Layer3DTreePad pad = UCService.GetContent(typeof(Layer3DTreePad)) as Layer3DTreePad;
            if (pad == null) return;
            FormAdd3DTile dlg = new FormAdd3DTile();
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            DataUtils.AddAndVisualize3DTileData(dlg.ConnInfo, dlg.Pwd, pad.TreeList, dlg.TreeNodeName, true);
        }
    }
}
