using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.UserControl;

namespace DF2DControl.UserControl.View
{
    public interface IMap2DView : IViewContent
    {
        //        IActiveView ActiveView{ get; }
        ESRI.ArcGIS.Controls.AxMapControl MapControl { get; }
    }
}
