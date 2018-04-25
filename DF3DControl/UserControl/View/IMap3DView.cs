using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.UserControl;

namespace DF3DControl.UserControl.View
{
    public interface IMap3DView : IViewContent
    {
        //        IActiveView ActiveView{ get; }
        Gvitech.CityMaker.Controls.AxRenderControl RenderControl3D { get; }
    }
}
