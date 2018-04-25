using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Resource;
using System;
namespace DF3DPipeCreateTool.ParamModeling
{
    public class DrawWell : DrawScanModel, IDrawWell, IDrawScanModel, IDrawGeometry
    {
        // Fields
        protected double _hBottom;   // 小室底高
        protected double _hTop;      // 小室顶高
        protected double _maxX;
        protected double _maxY;
        protected double _minX;
        protected double _minY;
        protected double[] _vtx;

        // Methods
        public override bool Draw(out IModelPoint mp, out IModel fmodel, out IModel smodel)
        {
            return base.Draw(out mp, out fmodel, out smodel);
        }

        protected void GetTexcoord(double x, double y, out double tx, out double ty)
        {
            tx = (x - this._minX) / (this._maxX - this._minX);
            ty = (this._maxY - y) / (this._maxY - this._minY);
        }
    }
}
