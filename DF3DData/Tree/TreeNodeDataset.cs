using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.FdeCore;

namespace DF3DData.Tree
{
    public class TreeNodeDataset : GroupLayerClass
    {
        private bool _bTemp;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }
        public TreeNodeDataset()
            : this("")
        {
            base.ImageIndex = 2;
        }
        public TreeNodeDataset(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            if (_bTemp) base.AddMenuItems(new string[] { "移除", "" });
            base.AddMenuItems(new string[] {  "全部展开", "全部折叠" });
        }

        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case"移除":
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
