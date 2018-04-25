using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;

namespace DF3DData.Tree
{
    public class TreeNodeDatabase : GroupLayerClass
    {
        private bool _bTemp;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }

        public TreeNodeDatabase()
            : this("")
        {
            base.ImageIndex = 1;
        }
        public TreeNodeDatabase(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            if (_bTemp) base.AddMenuItems(new string[] { "移除", "" });
            base.AddMenuItems(new string[] { "全部展开", "全部折叠" });
        }

        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "移除":
                    Release();
                    break;
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
