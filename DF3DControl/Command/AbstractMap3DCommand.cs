using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Base;
using DFWinForms.Command;
using Gvitech.CityMaker.Controls;

namespace DF3DControl.Command
{
    public abstract class AbstractMap3DCommand : AbstractCommand, IMap3DCommand
    {
        public AbstractMap3DCommand()
        {
            this.Hook = DF3DApplication.Application;
        }

        public virtual void RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e) { }
        public virtual void RcMouseDragSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseDragSelectEvent e) { }
        public virtual bool RcLButtonUp(object sender, _IRenderControlEvents_RcLButtonUpEvent e) { return false; }
        public virtual bool RcMouseWheel(object sender, _IRenderControlEvents_RcMouseWheelEvent e) { return false; }
        public virtual bool RcMButtonUp(object sender, _IRenderControlEvents_RcMButtonUpEvent e) { return false; }
        public virtual void RcCameraFlyFinished(object sender, _IRenderControlEvents_RcCameraFlyFinishedEvent e) { }
        public virtual bool RcRButtonUp(object sender, _IRenderControlEvents_RcRButtonUpEvent e) { return false; }
        public virtual bool RcKeyUp(object sender, _IRenderControlEvents_RcKeyUpEvent e) { return false; }
        public virtual bool RcLButtonDblClk(object sender, _IRenderControlEvents_RcLButtonDblClkEvent e) { return false; }
        public virtual bool RcRButtonDblClk(object sender, _IRenderControlEvents_RcRButtonDblClkEvent e) { return false; }
        public virtual bool RcMButtonDblClk(object sender, _IRenderControlEvents_RcMButtonDblClkEvent e) { return false; }
        public virtual void RcCameraUndoRedoStatusChanged(object sender, EventArgs e){}
    }
}
