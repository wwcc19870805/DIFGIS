using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF3DData.Frm;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DF3DData.UserControl.Pad;
using DFWinForms.Service;

namespace DF3DData.Command
{
    public class CmdAdd3DDatabase : AbstractCommand
    {
        public override void Run(object sender, EventArgs e)
        {
            Layer3DTreePad pad = UCService.GetContent(typeof(Layer3DTreePad)) as Layer3DTreePad;
            if (pad == null) return;
            FormAdd3DDatabase dialog = new FormAdd3DDatabase();
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            DataUtils.AddAndVisualizeBaseData(dialog.ConnInfo, "Geometry", pad.TreeList, true);
        }
    }
}
