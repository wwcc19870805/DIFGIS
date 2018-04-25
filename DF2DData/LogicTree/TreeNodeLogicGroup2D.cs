using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;

namespace DF2DData.LogicTree
{
    public class TreeNodeLogicGroup2D : GroupLayerClass

    {
         public TreeNodeLogicGroup2D()
            : this("")
        {
            base.ImageIndex = 0;
        }
        public TreeNodeLogicGroup2D(string ID)
            : base(ID)
        {
        }
    }
}
