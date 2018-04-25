using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DF2DControl.Base;
using DFWinForms.Command;

namespace DF2DControl.Command
{
    public abstract class AbstractMap2DCommand : AbstractCommand, IMap2DCommand
    {
        public AbstractMap2DCommand()
        {
            this.Hook = DF2DApplication.Application;
        }

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

    }
}
