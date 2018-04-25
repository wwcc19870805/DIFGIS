using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

namespace DF2DControl.Command
{
    public interface IMap2DCommand : ICommand
    {
        void OnMouseDown(int button, int shift, int x, int y, double mapX, double mapY);

        void OnSelectionChanged();

        void OnMouseUp(int button, int shift, int x, int y, double mapX, double mapY);

        void OnKeyDown(int keyCode, int shift);

        void OnMapReplaced(object newMap);

        void OnKeyUp(int keyCode, int shift);

        void OnMouseMove(int button, int shift, int x, int y, double mapX, double mapY);

        void OnFullExtentUpdated(object displayTransformation, object newEnvelope);

        void OnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope);

        void OnAfterDraw(object display, int viewDrawPhase);

        void OnBeforeScreenDraw(int hdc);

        void OnViewRefreshed(object ActiveView, int viewDrawPhase, object layerOrElement, object envelope);

        void OnDoubleClick(int button, int shift, int x, int y, double mapX, double mapY);

        void OnAfterScreenDraw(int hdc);

    }
}
