using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF3DControl.Command;
using ICSharpCode.Core;
using DF3DPlan.UC;
using DevExpress.XtraBars.Docking;
using DFWinForms.Class;

namespace DF3DPlan.Command
{
    class CmdStreetVerticalSection : AbstractMap3DCommand
    {
        private DockPanel _dockPanel;
        private int _height = 600;
        private UIDockPanel _uPanel;
        private int _width = 245;
        private UCStreetVerticalSection _uc;
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                Map3DCommandManager.Push(this);
                this._uPanel = new UIDockPanel("立面出图", "立面出图", this.Location, this._width, this._height);
                this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                this._dockPanel.Visibility = DockVisibility.Visible;
                this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                this._dockPanel.Width = this._width;
                this._dockPanel.Height = this._height;
                this._uc = new UCStreetVerticalSection();
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
