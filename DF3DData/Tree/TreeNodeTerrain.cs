using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;

namespace DF3DData.Tree
{
    public class TreeNodeTerrain : BaseLayerClass, ICamera
    {
        public TreeNodeTerrain()
            : this("")
        {
            base.ImageIndex = 8;
        }
        public TreeNodeTerrain(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {

        }

        public override void OnMenuItemClick(string caption)
        {

        }

        public void FlyTo()
        {
            if (this.CustomValue != null && this.CustomValue is ITerrain)
            {
                (this.CustomValue as ITerrain).FlyTo(gviTerrainActionCode.gviFlyToTerrain);
            }
        }

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if (this.CustomValue != null && this.CustomValue is ITerrain)
                {
                    if (value)
                    {
                        (this.CustomValue as ITerrain).VisibleMask = gviViewportMask.gviViewAllNormalView;
                        base.Visible = true;
                    }
                    else
                    {
                        (this.CustomValue as ITerrain).VisibleMask = gviViewportMask.gviViewNone;
                        base.Visible = false;
                    }
                }
                else base.Visible = false;
                DF3DApplication app = DF3DApplication.Application;
                if (app != null && app.Workbench != null) app.Workbench.UpdateMenu();
            }
        }

    }
}
