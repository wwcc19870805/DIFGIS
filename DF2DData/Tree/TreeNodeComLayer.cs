using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DF2DControl.Base;
using ESRI.ArcGIS.Carto;



namespace DF2DData.Tree
{
    public class TreeNodeComLayer: DFWinForms.LogicTree.GroupLayerClass
    {
       private bool _bTemp;
        private bool _checkOn = true;
       //private double _minMapScale;
       //private double _maxMapScale;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }

        public TreeNodeComLayer()
            : this("")
        {
            base.ImageIndex = 1;
        }
        public TreeNodeComLayer(string ID)
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

        public bool CheckOn
        {
            get { return this._checkOn; }
            set { this._checkOn = value; }
        }

      
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if (this.CustomValue != null&& this.CustomValue is ILayer)
                {
                    if (value)
                    {
                        (this.CustomValue as ILayer).Visible = true;
                        base.Visible = true;
                    }
                    else
                    {
                        (this.CustomValue as ILayer).Visible = false;
                        base.Visible = false;
                    }
                }
                else base.Visible = false;
                //DF2DApplication app = DF2DApplication.Application;
                //if (app != null && app.Workbench != null) app.Workbench.UpdateMenu();
            }
        }
    }
}
