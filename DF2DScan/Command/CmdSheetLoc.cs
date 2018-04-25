using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DScan.Frm;
using System.Windows.Forms;

namespace DF2DScan.Command
{
    class CmdSheetLoc : AbstractMap2DCommand
    {
        private int _height = 400;      
        private int _width = 265;
        public override void Run(object sender, System.EventArgs e)
        {
            FrmSheetLoc dialog = new FrmSheetLoc();
            dialog.Show();
            
        }

        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point(width / 2, height  / 2);
            }
        }
    }
}
