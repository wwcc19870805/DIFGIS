using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace DF2DAnalysis.Class
{
    public class IntersectPoint 
    {
       
        public IGeometry Shape
        {
            get
            {return this._feature.Shape;}
        }
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
        public IPoint InterPoint
        {
            get { return this._interPoint; }
        }
        public double Distance
        {
            get { return this._distance; }
            set { this._distance = value; }
        }
        public bool IsCircle
        {
            get { return this._isCircle; }
        }
        public string FcName
        {
            get { return this._fcName; }
        }
        public double DistanceF
        {
            get { return this._distanceF; }
        }
        public string Road
        {
            get { return this._road; }
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
        private IPoint _interPoint;
        private double _distance;
        private bool _isCircle;
        private double _distanceF;
        private string _road;
        private double _distanceDraw = 0.0;
        private double _hDraw = 0.0;
        private double _zDraw = 0.0;
        private Color _color;
     
        public IntersectPoint(IFeature feature,IPoint point,string fcName,double dis,double disF)
        {
            _feature = feature;
            int indexdia = feature.Fields.FindField("STANDARD");
            int indexroad = feature.Fields.FindField("PROAD");
            if (feature.get_Value(indexdia).ToString().Contains('*'))
            {
                _diameter = feature.get_Value(indexdia).ToString();
                _isCircle = false;

            }
            else
            {
                _diameter = feature.get_Value(indexdia).ToString();
                _isCircle = true;
            }
            _zPipeTop = GetInsertZByFeature(feature,point);
            _surfaceH = GetInsertHByFeature(feature, point);
            _interPoint = point;
            _fcName = fcName;
            _distance = dis;
            _distanceF = disF;
            _road = feature.get_Value(indexroad).ToString();
            
        }

        private double GetInsertZByFeature(IFeature feature,IPoint interPoint)
        {
            IGeometry geo = feature.Shape;
            double fromTop;
            double endTop;
            int indexfrom = feature.Fields.FindField("STARTTOPGC");
            int indexend = feature.Fields.FindField("ENDTOPGC");
            Double.TryParse(feature.get_Value(indexfrom).ToString(),out fromTop);
            Double.TryParse(feature.get_Value(indexend).ToString(), out endTop);
            if (geo is IPolyline)
            {
                IPolyline line = geo as IPolyline;
                IPoint point1 = line.FromPoint;
                IPoint point2 = line.ToPoint;
                double dis = GetDistanceOfTwoPoints(point1, point2);
                return  fromTop + ((endTop - fromTop) / dis) * GetDistanceOfTwoPoints(point1,interPoint);
            }
            else
            {
                return double.MaxValue;
            }
        }

        private double GetInsertHByFeature(IFeature feature, IPoint interPoint)
        {
            IGeometry geo = feature.Shape;
            double fromSuf;
            double endSuf;
            int indexfrom = feature.Fields.FindField("START_SURF_H");
            int indexend = feature.Fields.FindField("END_SURF_H");
            Double.TryParse(feature.get_Value(indexfrom).ToString(), out fromSuf);
            Double.TryParse(feature.get_Value(indexend).ToString(), out endSuf);
            if (geo is IPolyline)
            {
                IPolyline line = geo as IPolyline;
                IPoint point1 = line.FromPoint;
                IPoint point2 = line.ToPoint;
                double dis = GetDistanceOfTwoPoints(point1, point2);
                return fromSuf + ((endSuf - fromSuf) / dis) * GetDistanceOfTwoPoints(point1, interPoint);
            }
            else
            {
                return double.MaxValue;
            }
        }
        private double GetDistanceOfTwoPoints(IPoint P1, IPoint P2)
        {
            double Result = 0;

            Result = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

            return Result;
        }

      
    }
}
