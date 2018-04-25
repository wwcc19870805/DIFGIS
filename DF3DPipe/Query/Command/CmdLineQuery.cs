using System;
using System.Collections.Generic;
using System.Linq;
using DF3DDraw;
using DF3DControl.Command;

namespace DF3DPipe.Query.Command
{
    class CmdLineQuery : AbstractMap3DCommand
    {
        private DrawTool _drawTool;

        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);

            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Line);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        public override void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
            }
            Map3DCommandManager.Pop();
        }
        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Line && this._drawTool.GetGeo() != null)
            {
                LineQuery();
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void LineQuery()
        {

        }

    }
}
