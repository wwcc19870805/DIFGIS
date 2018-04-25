using System;
using System.Collections.Generic;
using System.Linq;
using DF3DDraw;
using DF3DControl.Command;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Logic;
using DF3DData.Class;
using System.Data;

namespace DF3DPipe.Query.Command
{
    class CmdRectQuery : AbstractMap3DCommand
    {
        private DrawTool _drawTool;

        public override void Run(object sender, System.EventArgs e)
        {
            Map3DCommandManager.Push(this);

            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Rectangle);
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
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Rectangle && this._drawTool.GetGeo() != null)
            {
                RectQuery();
            }
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void RectQuery()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            IGeometry geo = this._drawTool.GetGeo();
            ISpatialFilter filter = new SpatialFilter();
            filter.Geometry = geo;
            
        }

    }
}
