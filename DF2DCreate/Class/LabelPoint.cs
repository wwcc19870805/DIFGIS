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
    public class LabelPoint
    {
        IFeature _feature;
        IFeatureLayer _featureLayer;
        string _classify;
        string _x;
        string _y;
        string _detectNo;
        string _fusu;
        string _standard;
        string _road;
        public IFeature Feature
        {
            get { return this._feature; }
        }
        public IFeatureLayer FeaLayer
        {
            get { return this._featureLayer; }
        }
        public string Classify
        {
            get { return this._classify; }
        }
        public string X
        {
            get { return this._x; }
        }
        public string Y
        {
            get { return this._y; }
        }
        public string DetectNo
        {
            get { return this._detectNo; }
        }
        public string Fusu
        {
            get { return this._fusu; }
        }
        public string Standard
        {
            get { return this._standard; }
        }
        public string Road
        {
            get { return this._road; }
        }

        public LabelPoint(IFeature feature,IFeatureLayer featureLayer,string classify,string x,string y,string detectNo,string fusu,string standard,string road)
        {
            this._feature = feature;
            this._featureLayer = featureLayer;
            this._classify = classify;
            this._x = x;
            this._y = y;
            this._detectNo = detectNo;
            this._fusu = fusu;
            this._standard = standard;
            this._road = road;
        }
    }
}
