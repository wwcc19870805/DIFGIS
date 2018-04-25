using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DAnalysis.Class
{
    public class VerticalPoint
    {
        public string Diameter
        {
            get { return this._diameter; }
        }
        public double ZPipeTop
        {
            get { return this._zPipeTop; }
        }
        public double H
        {
            get { return this._surfaceH; }
        }
        public string FcName
        {
            get { return this._fcName; }
        }
        public string Road
        {
            get { return this._road; }
        }
        public bool IsFromPnt
        {
            get { return this._isFromPnt; }
        }
        public double X
        {
            get { return this._point.X; }
        }
        public double Y
        {
            get { return this._point.Y; }
        }
        public double Length
        {
            get
            {
                if (!IsFromPnt)
                {
                    IPolyline line = geo as IPolyline;
                    return line.Length;
                }
                else
                    return 0.0;
                    
            }
        }

        public double DistanceDraw
        {
            get { return this._distanceDraw; }
            set { this._distanceDraw = value; }
        }
        public double HDraw
        {
            get { return this._hDraw; }
            set { this._hDraw = value; }
        }
        public double ZDraw
        {
            get { return this._zDraw; }
            set { this._zDraw = value; }
        }
        public bool IsCircle
        {
            get
            {
                if (this._diameter.Contains('*'))
                    return false;
                else
                    return true;
            }
        }
        public Color Color
        {
            get
            {
                switch (this._fcName)
                {
                    case "PS":
                        return Color.Brown;
                        break;
                    case "JS":
                        return Color.Blue;
                        break;
                    case "RQ":
                        return Color.Pink;
                        break;
                    case "RL":
                        return Color.Orange;
                        break;
                    case "GY":
                        return Color.Black;
                        break;
                    case "DL":
                        return Color.Red;
                        break;
                    default:
                        return Color.Black;


                }
            }
        }
        private IFeature _feature;
        private string _diameter;
        private double _zPipeTop;
        private double _surfaceH;
        private string _fcName;
        private IPoint _point;        
        private bool _isFromPnt;
        private string _road;
        private IGeometry geo;
        private double _distanceDraw;
        private double _hDraw;
        private double _zDraw;
        private Color _color;
        public VerticalPoint(string fcName,IFeature feature,IPoint point,string diameter,string road,string Z,string H, bool isFromPnt)
        {
            _fcName = fcName;
            _feature = feature;
            _point = point;
            _diameter = diameter;
            _road = road;
            geo = feature.Shape;
            _zPipeTop = double.Parse(Z);
            _surfaceH = double.Parse(H);
            _isFromPnt = isFromPnt;

        }
    }
}
