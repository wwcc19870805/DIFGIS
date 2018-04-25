using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;

namespace DF3DDraw
{
    public interface IDrawTool
    {
        // Methods
        void Close();        
        void Start();
        void End();
        IFeatureLayerPickResult GetSelectFeatureLayerPickResult();
        IPoint GetSelectPoint();
        IGeometry GetGeo();
        // Properties
        DrawType GeoType { get; }
        bool IsFinished { get; }
        bool IsStarted { get; }
    }
}
