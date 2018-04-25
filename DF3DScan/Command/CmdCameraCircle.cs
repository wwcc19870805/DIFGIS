using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF3DDraw;
using Gvitech.CityMaker.FdeGeometry;
using DF3DControl.Command;

namespace DF3DScan.Command
{
    public class CmdCameraCircle : AbstractMap3DCommand
    {
        private DrawTool _drawTool;
        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polygon2D);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        public override void RestoreEnv()
        {
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.End();
            }
            Map3DCommandManager.Pop();
        }

        private void _drawTool_OnStartDraw()
        {
            
        }

        private void _drawTool_OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Line && this._drawTool.GetGeo3D() != null)
            {
                IPolyline line = this._drawTool.GetGeo3D() as IPolyline;
            }
        }


    }
}
