using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF2DControl.Command;
using ICSharpCode.Core;
using DFWinForms.Class;
using DevExpress.XtraBars.Docking;
using DF2DScan.UserControl;

namespace DF2DScan.Command
{
    class CmdZoomOutTest: AbstractMap2DCommand
    {
        private DockPanel _dockPanel;
        private int _height = 200;
        private UIDockPanel _uPanel;
        private int _width = 400;
        private UCTest _ucTest;
        public override void Run(object sender, System.EventArgs e)
        {
            try
            {
                Map2DCommandManager.Push(this);
                this._uPanel = new UIDockPanel("测试", "测试", this.Location, this._width, this._height);
                this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Top);
                this._dockPanel.Visibility = DockVisibility.Visible;
                this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                this._dockPanel.Width = this._width;
                this._dockPanel.Height = this._height;
                this._ucTest= new UCTest();
                this._ucTest.Dock = System.Windows.Forms.DockStyle.Fill;
                this._uPanel.RegisterEvent(new PanelClose(this.Close));
                this._dockPanel.Controls.Add(this._ucTest);


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
            if (this._uPanel != null)
            {
                this._uPanel.GetControlContainer().Controls.Clear();
                this._uPanel.Close();
                this._uPanel = null;
            }
            Map2DCommandManager.Pop();
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
