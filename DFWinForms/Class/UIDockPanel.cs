using DevExpress.XtraBars.Docking;
using System;
using System.Drawing;
using System.Windows.Forms;
using DFWinForms.Base;
using DFWinForms.Service;
namespace DFWinForms.Class
{
    public class UIDockPanel
    {
        // Fields
        private DockPanel _dp;
        private string _funcName;
        private int _height;
        private Point _location;
        private string _panelName;
        private int _width;
        private PanelClose OnClosingPanel;

        // Methods
        public UIDockPanel(string panelName, string funcName)
        {
            this._funcName = funcName;
            this._panelName = panelName;
        }

        public UIDockPanel(string panelName, string funcName, int width)
        {
            this._funcName = funcName;
            this._panelName = panelName;
            this._width = width;
        }

        public UIDockPanel(string panelName, string funcName, Point location, int width, int height)
        {
            this._funcName = funcName;
            this._panelName = panelName;
            this._location = location;
            this._width = width;
            this._height = height;
        }

        public void _pnew_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            if (this.OnClosingPanel != null)
            {
                this._dp.ClosingPanel -= new DockPanelCancelEventHandler(this._pnew_ClosingPanel);
                this.OnClosingPanel();
            }
        }

        public void Close()
        {
            try
            {
                if ((this._dp != null) && ((this._dp != null) && (this._dp.Parent != null)))
                {
                    if (this._dp.Parent.InvokeRequired)
                    {
                        this._dp.Parent.Invoke(new PanelClose(this.Close), null);
                    }
                    else
                    {
                        this._dp.Visibility = DockVisibility.Hidden;
                        DFApplication.Application.Workbench.Refresh();
                    }
                    //DFApplication.Application.Workbench.SetActivePanel(UCService.LastActiveDocPanel);
                }
            }
            catch (Exception)
            {
            }
        }

        public override bool Equals(object floatPanel)
        {
            UIDockPanel panel = floatPanel as UIDockPanel;
            if (panel == null)
            {
                return false;
            }
            if ((this._panelName == null) || (this._funcName == null))
            {
                return false;
            }
            return (this._funcName.Equals(panel.FuncName) && this._panelName.Equals(panel.PanelName));
        }

        public ControlContainer GetControlContainer()
        {
            if ((this.Panel == null) || (this.Panel.Controls.Count == 0))
            {
                return null;
            }
            return (this.Panel.Controls[0] as ControlContainer);
        }

        public Control GetFirstControl()
        {
            if ((this.Panel == null) || (this.Panel.Controls.Count == 0))
            {
                return null;
            }
            ControlContainer container = this.Panel.Controls[0] as ControlContainer;
            if (container != null)
            {
                if (container.Controls.Count > 0)
                {
                    return container.Controls[0];
                }
                return null;
            }
            return this.Panel.Controls[0];
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void RegisterEvent(PanelClose panelClosingEventHandle)
        {
            if (this._dp != null)
            {
                this.OnClosingPanel = panelClosingEventHandle;
                this._dp.ClosingPanel += new DockPanelCancelEventHandler(this._pnew_ClosingPanel);
            }
        }

        // Properties
        public string FuncName
        {
            get
            {
                return this._funcName;
            }
            set
            {
                this._funcName = value;
            }
        }

        public int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public Point Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        public DockPanel Panel
        {
            get
            {
                return this._dp;
            }
            set
            {
                this._dp = value;
            }
        }

        public string PanelName
        {
            get
            {
                return this._panelName;
            }
            set
            {
                this._panelName = value;
            }
        }

        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
    }
}
