using System;
using System.Collections.Generic;
using System.Linq;
using DFWinForms.Command;
using DF3DPipe.Query.Frm;
using DF3DControl.Command;
using DF3DPipe.Query.UC;
using DFWinForms.Class;
using DevExpress.XtraBars.Docking;
namespace DF3DPipe.Query.Command
{
    public class CmdQueryByDeep : AbstractMap3DCommand
    {
        private DockPanel _dockPanel;
        private int _height = 400;
        private UIDockPanel _uPanel;
        private int _width = 225;
        private UCPropertyInfo _uc;
        private System.Drawing.Point Location
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }

        public override void Run(object sender, EventArgs e)
        {
            FrmPropertyQueryByDepth dialog = new FrmPropertyQueryByDepth(this.CommandName, "PipeLine");
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location, this._width, this._height);
            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
            this._dockPanel.Visibility = DockVisibility.Visible;
            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
            this._dockPanel.Width = this._width;
            this._dockPanel.Height = this._height;
            this._uc = new UCPropertyInfo();
            this._uc.Init(); 
            this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPanel.RegisterEvent(new PanelClose(this.Close));
            this._dockPanel.Controls.Add(this._uc);
            this._uc.SetPropertyInfo(dialog.Dict, "PipeLine");
        }

        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            if (_uc != null)
            {
                this._uc.Dispose();
                this._uc = null;
            }
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
        }
    }
}
