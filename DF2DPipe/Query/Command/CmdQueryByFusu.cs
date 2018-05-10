using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DFWinForms.Command;
using DF2DPipe.Query.Frm;
using DevExpress.XtraBars.Docking;
using DFWinForms.Class;
using DF2DPipe.Query.UC;
using DF2DControl.Command;
using DF2DControl.Base;

namespace DF2DPipe.Query.Command
{
    class CmdQueryByFusu : AbstractMap2DCommand
    {
        private DockPanel _dockPanel;
        private int _height = 400;
        private UIDockPanel _uPanel;
        private int _width = 265;
        private UCPropertyInfo2D _uc;
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
            Map2DCommandManager.Push(this);
            FrmPropertyQuery dialog = new FrmPropertyQuery(this.CommandName, "Additional");
            dialog.Text = "按附属物查询";
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location, this._width, this._height);
            this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
            this._dockPanel.Visibility = DockVisibility.Visible;
            this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
            this._dockPanel.Width = this._width;
            this._dockPanel.Height = this._height;
            this._uc = new UCPropertyInfo2D();
            this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uPanel.RegisterEvent(new PanelClose(this.Close));
            this._dockPanel.Controls.Add(this._uc);
            this._uc.SetPropertyInfo(dialog.Dict);
        }
        private void Close()
        {
            this.RestoreEnv();
        }

        public override void RestoreEnv()
        {
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
            DF2DApplication app = DF2DApplication.Application;
            if (app == null) return;
            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
            Map2DCommandManager.Pop();
        }
    }
}
