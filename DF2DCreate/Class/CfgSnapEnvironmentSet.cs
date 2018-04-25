using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace DF2DCreate.Class
{
    class CfgSnapEnvironmentSet
    {
        public IArray featurLayerSnapArray;
        public IMap mapSnap;

       
        public IActiveView ActiveView { get; set; }
        public string CurrentSnapType { get; set; }
        public bool IsOpen { get; set; }
        public bool IsUseMixSnap { get; set; }
        public SnapStruct.BoolSnapMode SnapMode { get; set; }
        public SnapStruct.EnumSnapType SnapType { get; set; }
        public double Tolerence { get; set; }
    }
}
