using DevExpress.XtraBars.Docking;
using System;
using System.Collections.Generic;
using System.Drawing;
using DFWinForms.Base;
namespace DFWinForms.Class
{
    public class FloatPanelManager
    {
        private static FloatPanelManager instance = null;
        private static readonly object syncRoot = new object();
        // Fields
        private int _defaultHeight = 400;
        private int _defaultWidth = 220;
        private List<UIDockPanel> _panelSet = new List<UIDockPanel>();
        private DockPanel _tabPanel;

        public static FloatPanelManager Instance
        {
            get
            {
                if (FloatPanelManager.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FloatPanelManager.instance == null)
                        {
                            FloatPanelManager.instance = new FloatPanelManager();
                        }
                    }
                }
                return FloatPanelManager.instance;
            }
        }

        // Methods
        public DockPanel Add(ref UIDockPanel newPanel,DockingStyle dockStyle)
        {
            if (dockStyle == DockingStyle.Fill) dockStyle = DockingStyle.Right;
            if (this._panelSet.Contains(newPanel))
            {
                int index = this._panelSet.IndexOf(newPanel);
                newPanel = this._panelSet[index];
                newPanel.Panel.Dock = dockStyle;
                newPanel.Panel.FloatSize = new Size(newPanel.Width, newPanel.Height);
                newPanel.Panel.FloatLocation = newPanel.Location;
                if (newPanel.Panel.Visibility == DockVisibility.AutoHide)
                {
                    newPanel.Panel.Close();
                }
                if (newPanel.Panel.Visibility != DockVisibility.Visible)
                {
                    newPanel.Panel.Visibility = DockVisibility.AutoHide;
                    newPanel.Panel.ShowSliding();
                }
                return newPanel.Panel;
            }
            DockPanel panel = DFApplication.Application.Workbench.AddPanel(dockStyle);
            panel.DockedAsTabbedDocument = false;
            panel.Options.AllowDockAsTabbedDocument = false; 
            panel.Text = newPanel.PanelName;
            int width = 0;
            if (newPanel.Width < 30)
            {
                width = this._defaultWidth;
            }
            else
            {
                width = newPanel.Width;
            }
            int height = 0;
            if (newPanel.Height < 100)
            {
                height = this._defaultHeight;
            }
            else
            {
                height = newPanel.Height;
            }
            panel.FloatSize = new Size(width, height);
            panel.FloatLocation = newPanel.Location;
            if (this._tabPanel == null)
            {
                this._tabPanel = panel;
            }
            panel.Visibility = DockVisibility.AutoHide;
            panel.ShowSliding();
            newPanel.Panel = panel;
            this._panelSet.Add(newPanel);
            return panel;
        }

        private void Dispose()
        {
            this._tabPanel = null;
            this._panelSet.Clear();
        }

        public void RemovePanel(UIDockPanel fpanel)
        {
            fpanel.Panel.Focus();
            fpanel.Panel.Visibility = DockVisibility.Hidden;
            DFApplication.Application.Workbench.Refresh();
        }

        public static void SetPanelDock(DockPanel panel)
        {
            panel.Options.AllowDockTop = false;
            panel.Options.AllowDockRight = false;
            panel.Options.AllowDockBottom = false;
            panel.Options.AllowDockLeft = false;
            panel.DockManager.DockingOptions.ShowCaptionOnMouseHover = false;
        }

        // Properties
        public List<UIDockPanel> PanelSet
        {
            get
            {
                return this._panelSet;
            }
            set
            {
                this._panelSet = value;
            }
        }

        public DockPanel TabPanel
        {
            get
            {
                return this._tabPanel;
            }
            set
            {
                this._tabPanel = value;
            }
        }
    }
}
