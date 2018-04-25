using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF2DScan.Frm;


namespace DF2DScan.Command
{
    public class CmdLocByCoordinate : AbstractMap2DCommand
    {
        private int _width = 400;
        private int _height = 200;
        public override void Run(object sender, System.EventArgs e)
        {

            FormLocByCoordinate2D.Instance.Show();
            
            
            
        }

        public override void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            FormLocByCoordinate2D.Instance.MapScaleChanged();
        }

        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }
    }
}
