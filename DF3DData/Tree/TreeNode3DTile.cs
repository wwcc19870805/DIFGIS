using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Controls;

namespace DF3DData.Tree
{
    public class TreeNode3DTile : BaseLayerClass, ICamera
    {
        private bool _bTemp;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }

        public TreeNode3DTile()
            : this("")
        {
            base.ImageIndex = 9;
        }
        public TreeNode3DTile(string ID)
            : base(ID)
        {
        }
        public override void InitPopMenu()
        {
            if (_bTemp) base.AddMenuItem("移除");
            base.AddMenuItems(new string[] { "", "属性" });

        }

        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "移除":
                    Release();
                    break;
                case "属性":
                    ShowProperty();
                    break;
            }

        }
        public override void Release()
        {
            base.Release();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || this.CustomValue == null) return;
            if (this.CustomValue is I3DTileLayer)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject((this.CustomValue as I3DTileLayer).Guid);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.CustomValue as I3DTileLayer);
            }
        }
        private void ShowProperty()
        {
        }

        public void FlyTo()
        {
            if (this.CustomValue != null && this.CustomValue is I3DTileLayer)
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                AxRenderControl d3 = app.Current3DMapControl;
                d3.Camera.FlyToObject((this.CustomValue as I3DTileLayer).Guid, gviActionCode.gviActionFlyTo);
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
                if (this.CustomValue != null && this.CustomValue is I3DTileLayer)
                {
                    if (value)
                    {
                        (this.CustomValue as I3DTileLayer).VisibleMask = gviViewportMask.gviViewAllNormalView;
                        base.Visible = true;
                    }
                    else
                    {
                        (this.CustomValue as I3DTileLayer).VisibleMask = gviViewportMask.gviViewNone;
                        base.Visible = false;
                    }
                }
                else base.Visible = false;
            }
        }

    }
}
