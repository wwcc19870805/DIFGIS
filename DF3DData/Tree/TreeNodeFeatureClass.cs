using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.LogicTree;
using DFDataConfig.Class;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Math;
using DF3DControl.Base;
using Gvitech.CityMaker.Controls;
using DF3DData.Frm;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;
using System.Collections;
using DevExpress.XtraEditors;

namespace DF3DData.Tree
{
    public class TreeNodeFeatureClass : BaseLayerClass, ICamera
    {
        private bool _bTemp;
        public bool Temp
        {
            get { return _bTemp; }
            set { this._bTemp = value; }
        }
        private IFeatureLayer _visualFeauturelayer;
        public TreeNodeFeatureClass()
            : this("")
        {
        }
        public TreeNodeFeatureClass(string ID)
            : base(ID)
        {
        }

        public override void InitPopMenu()
        {
            base.AddMenuItem("查询");
            if (_bTemp) base.AddMenuItem("移除");
            //base.AddMenuItems(new string[] { "", "属性" });
        }

        public override void OnMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "查询":
                    DoQuery();
                    break;
                case "移除":
                    Release();
                    break;
                case "属性":
                    ShowProperty();
                    break;
            }
        }
        public void SetVisualFeatureLayer(IFeatureLayer fl)
        {
            _visualFeauturelayer = fl;
        }
        public override void Release()
        {
            base.Release();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || this._visualFeauturelayer == null) return;
            app.Current3DMapControl.ObjectManager.DeleteObject(this._visualFeauturelayer.Guid);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(this._visualFeauturelayer);
        }

        public void DoQuery()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;

            app.Workbench.SetMenuEnable(false);

            FormPropertyQuery dlg = new FormPropertyQuery(this);
            dlg.Show();
            dlg.FormClosed += new System.Windows.Forms.FormClosedEventHandler(dlg_FormClosed);
        }

        private void dlg_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null) return;

            app.Workbench.SetMenuEnable(true);
        }

        private void ShowProperty()
        {
            if (this.CustomValue != null && this.CustomValue is IFeatureClass)
            {
                IFeatureClass fc = this.CustomValue as IFeatureClass;
                if (fc != null && this._visualFeauturelayer != null)
                {
                    FormFeatureLayerProperty dlg = new FormFeatureLayerProperty(fc, this._visualFeauturelayer);
                    dlg.ShowDialog();
                }
            }
        }

        public void FlyTo()
        {
            if (this._visualFeauturelayer != null)
            {
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;
                AxRenderControl d3 = app.Current3DMapControl;
                d3.Camera.FlyToObject(this._visualFeauturelayer.Guid, gviActionCode.gviActionFlyTo);
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
                if (this.CustomValue != null && this._visualFeauturelayer != null)
                {
                    if (value)
                    {
                        this._visualFeauturelayer.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        base.Visible = true;
                    }
                    else
                    {
                        this._visualFeauturelayer.VisibleMask = gviViewportMask.gviViewNone;
                        base.Visible = false;
                    }
                }
                else base.Visible = false;
            }
        }
    }
}
