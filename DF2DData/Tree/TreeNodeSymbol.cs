using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using ESRI.ArcGIS.Carto;
using DF2DControl.Base;
using ESRI.ArcGIS.Display;

namespace DF2DData.Tree
{
    public class TreeNodeSymbol : BaseLayerClass
    {
        private bool _bTemp;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }
        public TreeNodeSymbol()
            : this("")
        {
            base.ImageIndex = 2;
        }
        public TreeNodeSymbol(string ID)
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
          
        }

        //public override bool Visible
        //{
        //    get
        //    {
        //        return base.Visible;
        //    }
        //    set
        //    {
        //        if (this.CustomValue != null && this.CustomValue is ISymbol)
        //        {
        //            if (value)
        //            {
        //                (this.CustomValue as ISymbol). = true;
        //                base.Visible = true;
        //            }
        //            else
        //            {
        //                (this.CustomValue as ILayer).Visible = false;
        //                base.Visible = false;
        //            }
        //        }
        //        else base.Visible = false;
        //        //DF2DApplication app = DF2DApplication.Application;
        //        //if (app != null && app.Workbench != null) app.Workbench.UpdateMenu();
        //    }
        //}

    }
}
