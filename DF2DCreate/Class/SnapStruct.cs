using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;

namespace DF2DCreate.Class
{
    class SnapStruct
    {
       

        public struct BoolSnapMode
        {
            public bool CircleCenter { get; set; }
            public bool Cosse { get; set; }
            public bool Endpoint { get; set; }
            public bool Intersection { get; set; }
            public bool Midpoint { get; set; }
            public bool PartBoundary { get; set; }
            public bool PartCentroid { get; set; }
            public bool PartVertex { get; set; }
            public bool QuadrantPoint { get; set; }
            public bool Tangent { get; set; }
            public bool Vertical { get; set; }
        }

        public struct EnumSnapType
        {

            public string CircleCenter { get; set; }
            public string Cosse { get; set; }
            public string Endpoint { get; set; }
            public string Intersection { get; set; }
            public string Midpoint { get; set; }
            public string PartBoundary { get; set; }
            public string PartCentroid { get; set; }
            public string PartVertex { get; set; }
            public string QuadrantPoint { get; set; }
            public string Tangent { get; set; }
            public string Vertical { get; set; }
        }

        public struct FeatureLayerSnap
        {
            public bool bSnap;
            public IFeatureLayer pFeatureLayer;

            public IFeatureLayer FeatureLayer { get; set; }
            public bool IsSnap { get; set; }
        }


    }
}
