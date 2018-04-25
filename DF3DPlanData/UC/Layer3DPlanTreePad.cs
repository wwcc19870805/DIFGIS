using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DFWinForms.LogicTree;

namespace DF3DPlanData.UC
{
    public class Layer3DPlanTreePad : LogicBaseTree
    {
        public Layer3DPlanTreePad()
        {
            this.OnLayerDoubleClick += new OnLayerDoubleClickHandler<IBaseLayer>(Layer3DPanTreePad_OnLayerDoubleClick);
        }

        private void Layer3DPanTreePad_OnLayerDoubleClick(IBaseLayer layer)
        {
        }

    }
}
