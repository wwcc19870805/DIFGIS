using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars.Docking;
using DFWinForms.Command;
using DF3DControl.Command;
using DFWinForms.Class;
using ICSharpCode.Core;
using DF3DScan.UC;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;
using DF3DData.Class;
using DFWinForms.LogicTree;

namespace DF3DScan.Command
{
    class CmdEarthworkCalculation : AbstractMap3DCommand
    {
        private DockPanel _dockPanel;
        private int _height = 600;
        private UIDockPanel _uPanel;
        private int _width = 225;
        private UCEarthworkCalculation _uc;

        private AxRenderControl d3;
        private Dictionary<IBaseLayer, bool> dict;
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                if (DF3DApplication.Application == null || DF3DApplication.Application.Current3DMapControl == null) return;
                d3 = DF3DApplication.Application.Current3DMapControl;
                if (!d3.Terrain.IsRegistered) return;
                Map3DCommandManager.Push(this);
                d3.Terrain.DemAvailable = true;
                DF3DApplication.Application.Workbench.UpdateMenu();
                List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("DX3DMODEL");
                if (list != null)
                {
                    dict = new Dictionary<IBaseLayer, bool>(); 
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        IBaseLayer bl = dffc.GetTreeLayer();
                        dict[bl] = bl.Visible;
                        if (bl != null)
                        {
                            bl.Visible = false;
                        }
                    }
                }
                this._uPanel = new UIDockPanel("开挖分析", "开挖分析", this.Location, this._width, this._height);
                this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                this._dockPanel.Visibility = DockVisibility.Visible;
                this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                this._dockPanel.Width = this._width;
                this._dockPanel.Height = this._height;
                this._uc = new UCEarthworkCalculation();
                this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
                this._uPanel.RegisterEvent(new PanelClose(this.Close));
                this._dockPanel.Controls.Add(this._uc);
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            if (dict != null)
            {
                foreach (KeyValuePair<IBaseLayer, bool> kv in dict)
                {
                    kv.Key.Visible = kv.Value;
                }
            }
            if (this._uc != null)
            {
                this._uc.RestoreEnv();
                this._uc.Dispose();
                this._uc = null;
            }
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
            Map3DCommandManager.Pop();
        }

        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }
    }
}
