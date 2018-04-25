using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using Gvitech.CityMaker.Controls;

namespace DF3DControl.Command
{
    public interface IMap3DCommand :ICommand
    {
        void RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e);
        void RcMouseDragSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseDragSelectEvent e);
        bool RcLButtonUp(object sender, _IRenderControlEvents_RcLButtonUpEvent e);
        bool RcMouseWheel(object sender, _IRenderControlEvents_RcMouseWheelEvent e);
        bool RcMButtonUp(object sender, _IRenderControlEvents_RcMButtonUpEvent e);
        bool RcRButtonUp(object sender, _IRenderControlEvents_RcRButtonUpEvent e);
        bool RcKeyUp(object sender, _IRenderControlEvents_RcKeyUpEvent e);
        bool RcLButtonDblClk(object sender, _IRenderControlEvents_RcLButtonDblClkEvent e);
        bool RcRButtonDblClk(object sender, _IRenderControlEvents_RcRButtonDblClkEvent e);
        bool RcMButtonDblClk(object sender, _IRenderControlEvents_RcMButtonDblClkEvent e);
        void RcCameraFlyFinished(object sender, _IRenderControlEvents_RcCameraFlyFinishedEvent e);
        void RcCameraUndoRedoStatusChanged(object sender, EventArgs e);
    }
}
