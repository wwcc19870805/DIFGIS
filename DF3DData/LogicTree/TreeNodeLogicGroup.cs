using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;

namespace DF3DData.LogicTree
{
    public class TreeNodeLogicGroup : GroupLayerClass
    {
        public TreeNodeLogicGroup()
            : this("")
        {
            base.ImageIndex = 0;
        }
        public TreeNodeLogicGroup(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            base.AddMenuItems(new string[] { "全部展开", "全部折叠" });
        }
        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "全部展开":
                    this.ExpandAll();
                    break;
                case "全部折叠":
                    this.CollapseAll();
                    break;
            }
        }
    }
}
