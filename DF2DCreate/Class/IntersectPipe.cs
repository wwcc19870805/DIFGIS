using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using ESRI.ArcGIS.Carto;

namespace DF2DCreate.Class
{
    public class IntersectPipe
    {
        IFeature _feature;
        IFeatureLayer _featureLayer;
        double _distance;
        string _classify;
        string _startNo;
        string _endNo;
        string _material;
        string _coverstyle;
        string _diameter;
        string _road;
        
        public IFeature Feature
        {
            get { return this._feature; }
        }
        public IFeatureLayer FeaLayer
        {
            get { return this._featureLayer; }
        }
        public double Distance
        {
            get { return this._distance; }
        }
        public string Classify
        {
            get { return this._classify; }
        }
        public string StartNo
        {
            get { return this._startNo; }
        }
        public string EndNo
        {
            get { return this._endNo; }
        }
        public string Material
        {
            get { return this._material; }
        }
        public string Coverstyle
        {
            get { return this._coverstyle; }
        }
        public string Diameter
        {
            get { return this._diameter; }
        }
        public string Road
        {
            get { return this._road; }
        }

        public IntersectPipe(IFeature feature,IFeatureLayer featureLayer,double distance,string classify,string startNo,string endNo,string material,string coverstyle,string diameter,string road)
        {
            this._feature = feature;
            this._featureLayer = featureLayer;
            this._distance = distance;
            this._classify = classify;
            this._startNo = startNo;
            this._endNo = endNo;
            this._material = material;
            this._coverstyle = coverstyle;
            this._diameter = diameter;
            this._road = road;
        }
    }
}
