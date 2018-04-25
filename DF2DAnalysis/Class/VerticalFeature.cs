using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DAnalysis.Class
{
    class VerticalFeature
    {
        private List<IFeature> _featureList;
        private List<IPoint> _points;
        public VerticalFeature(List<IFeature> featureList)
        {
            this._featureList = featureList;
            this._points = GetPipePoints();
        }
        private List<IPoint> GetPipePoints()
        {
            List<IPoint> points = new List<IPoint>();
            if (this._featureList == null) return null;
            foreach (IFeature ft in _featureList)
            {
                IGeometry geo = ft.Shape;
                if (geo is IPolyline)
                {
                    IPolyline pline = geo as IPolyline;
                    IPoint fromPoint = pline.FromPoint;
                    IPoint toPoint = pline.ToPoint;
                    points.Add(fromPoint);
                    points.Add(toPoint);

                }
            }
            return points;
        }
    }
}
