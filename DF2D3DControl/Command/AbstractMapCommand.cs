using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using DF3DControl.Command;
using DFWinForms.Command;
using Gvitech.CityMaker.Controls;

namespace DF2D3DControl.Command
{
    public abstract class AbstractMapCommand : AbstractCommand, IMap2DCommand, IMap3DCommand
    {
        #region 二维地图事件
        public virtual void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY) { }

        public virtual void OnSelectionChanged() { }

        public virtual void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY) { }

        public virtual void OnKeyDown(int keyCode, int shift) { }

        public virtual void OnMapReplaced(object newMap) { }

        public virtual void OnKeyUp(int keyCode, int shift) { }

        public virtual void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY) { }

        public virtual void OnFullExtentUpdated(object displayTransformation, object newEnvelope) { }

        public virtual void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope) { }

        public virtual void OnAfterDraw(object display, int viewDrawPhase) { }

        public virtual void OnBeforeScreenDraw(int hdc) { }

        public virtual void OnViewRefreshed(object ActiveView, int viewDrawPhase, object layerOrElement, object envelope) { }

        public virtual void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY) { }

        public virtual void OnAfterScreenDraw(int hdc) { }
        #endregion

        #region 三维地图事件
        public virtual void RcMouseClickSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEvent e) { }
        public virtual void RcMouseDragSelect(object sender, Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseDragSelectEvent e) { }
        public virtual bool RcLButtonUp(object sender, _IRenderControlEvents_RcLButtonUpEvent e) { return false; }
        public virtual bool RcMButtonUp(object sender, _IRenderControlEvents_RcMButtonUpEvent e) { return false; }
        public virtual bool RcMouseWheel(object sender, _IRenderControlEvents_RcMouseWheelEvent e) { return false; }
        public virtual void RcCameraFlyFinished(object sender, _IRenderControlEvents_RcCameraFlyFinishedEvent e) { }
        public virtual bool RcRButtonUp(object sender, _IRenderControlEvents_RcRButtonUpEvent e) { return false; }
        public virtual bool RcKeyUp(object sender, _IRenderControlEvents_RcKeyUpEvent e) { return false; }
        public virtual bool RcLButtonDblClk(object sender, _IRenderControlEvents_RcLButtonDblClkEvent e) { return false; }
        public virtual bool RcRButtonDblClk(object sender, _IRenderControlEvents_RcRButtonDblClkEvent e) { return false; }
        public virtual bool RcMButtonDblClk(object sender, _IRenderControlEvents_RcMButtonDblClkEvent e) { return false; }
        public virtual void RcCameraUndoRedoStatusChanged(object sender, EventArgs e) { }
        #endregion

    }
}
