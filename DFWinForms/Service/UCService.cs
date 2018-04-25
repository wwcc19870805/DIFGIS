using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using System.ComponentModel;
using DFWinForms.Base;
using DFWinForms.UserControl;

namespace DFWinForms.Service
{
    public static class UCService
    {
        private static List<IContent> ContentCollection = new List<IContent>();
        public static string InitActivateViewText;
        private static List<IViewContent> views = new List<IViewContent>();
        private static List<IPadContent> leftPads = new List<IPadContent>();
        private static List<IPadContent> rightPads = new List<IPadContent>();
        private static List<IPadContent> bottomPads = new List<IPadContent>();
        private static List<IPadContent> topPads = new List<IPadContent>();
        private static List<IPadContent> leftAutoHidePads = new List<IPadContent>();
        private static List<IPadContent> rightAutoHidePads = new List<IPadContent>();
        private static List<IPadContent> bottomAutoHidePads = new List<IPadContent>();
        private static List<IPadContent> topAutoHidePads = new List<IPadContent>();
        public static DevExpress.XtraBars.Docking.DockPanel LastActiveDocPanel;

        public static void CreateViews(IContainer components, DockManager dockManager, TabbedView tabbedView)
        {
            if (views != null && views.Count > 0)
            {
                DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(components);
                tabbedView.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] { documentGroup });

                for (int i = 0; i < views.Count; i++)
                {
                    XtraUserControl uc = (XtraUserControl)views[i];

                    DockPanel dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
                    DevExpress.XtraBars.Docking2010.Views.Tabbed.Document dockPanel_Document = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(components);
                    ControlContainer dockPanel_Container = new ControlContainer();

                    dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] { dockPanel });
                    tabbedView.Documents.Add(dockPanel_Document);
                    documentGroup.Items.Add(dockPanel_Document);

                    dockPanel.Controls.Add(dockPanel_Container);
                    dockPanel.DockedAsTabbedDocument = true;
                    dockPanel.Options.AllowDockAsTabbedDocument = true; 
                    dockPanel.Options.AllowDockLeft = false;
                    dockPanel.Options.AllowDockRight = false;
                    dockPanel.Options.AllowDockTop = false;
                    dockPanel.Options.AllowDockBottom = false;
                    dockPanel.Options.AllowDockFill = false;
                    dockPanel.ID = System.Guid.NewGuid();
                    string strName = DateTime.Now.Ticks.ToString();
                    dockPanel.Name = "dockPanel" + strName;
                    dockPanel.OriginalSize = new System.Drawing.Size(300, 300);
                    dockPanel.Text = views[i].Title;
                    dockPanel.SavedIndex = i;
                    dockPanel.SavedMdiDocument = true;
                    dockPanel.FloatLocation = new System.Drawing.Point(0, 0);
                    if (views[i].ShowCloseButton) dockPanel.Options.ShowCloseButton = true;
                    else dockPanel.Options.ShowCloseButton = false;
                    dockPanel_Container.Controls.Add(uc);
                    dockPanel_Container.Location = new System.Drawing.Point(0, 0);
                    dockPanel_Container.Name = "dockPanel_Container" + strName;
                    dockPanel_Container.Size = new System.Drawing.Size(300, 300);
                    dockPanel_Container.TabIndex = 0;

                    uc.Dock = System.Windows.Forms.DockStyle.Fill;
                    uc.Location = new System.Drawing.Point(0, 0);
                    uc.Name = "uc" + strName;
                    uc.Size = new System.Drawing.Size(300, 300);
                    uc.TabIndex = 0;

                    dockPanel_Document.Caption = views[i].Title;
                    dockPanel_Document.ControlName = dockPanel.Name;
                    dockPanel_Document.FloatLocation = new System.Drawing.Point(0, 0);
                    dockPanel_Document.FloatSize = new System.Drawing.Size(300, 300);
                    if (views[i].ShowCloseButton) dockPanel_Document.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
                    else dockPanel_Document.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;

                    if (views[i].IsActive)
                    {
                        InitActivateViewText = views[i].Title;
                    }
                    ContentCollection.Add(views[i]);
                }
            }
        }

        public static void AddViews(System.Windows.Forms.Form form, string addInTreePath)
        {
            List<IViewContent> vt = AddInTree.BuildItems<IViewContent>(addInTreePath, form);
            foreach (IViewContent v in vt) views.Add(v);
        }

        public static void CreatePad(System.Windows.Forms.Form form, DockManager dockManager, List<IPadContent> pads, DockingStyle dockStyle)
        {
            if (pads == null || pads.Count == 0) return;
            if (pads.Count == 1)
            {
                IPadContent pad = pads[0];
                XtraUserControl uc = (XtraUserControl)pad;
                DockPanel dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
                ControlContainer dockPanel_Container = new ControlContainer();
                dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] { dockPanel });

                dockPanel.Controls.Add(dockPanel_Container);
                dockPanel.Dock = dockStyle;
                dockPanel.DockedAsTabbedDocument = false;
                dockPanel.Options.AllowDockAsTabbedDocument = false;
                dockPanel.ID = System.Guid.NewGuid();
                dockPanel.Location = new System.Drawing.Point(0, 0);
                string strName = DateTime.Now.Ticks.ToString();
                dockPanel.Name = "dockPanel" + strName;
                dockPanel.OriginalSize = new System.Drawing.Size(200, 200);
                dockPanel.Size = new System.Drawing.Size(200, 200);
                dockPanel.Text = pad.Title;
                if (pad.ShowCloseButton) dockPanel.Options.ShowCloseButton = true;
                else dockPanel.Options.ShowCloseButton = false;

                dockPanel.Options.ShowMaximizeButton = true;

                dockPanel_Container.Controls.Add(uc);
                dockPanel_Container.Location = new System.Drawing.Point(0, 0);
                dockPanel_Container.Name = "dockPanel_Container" + strName;
                dockPanel_Container.Size = new System.Drawing.Size(200, 200);
                dockPanel_Container.TabIndex = 0;

                uc.Dock = System.Windows.Forms.DockStyle.Fill;
                uc.Location = new System.Drawing.Point(0, 0);
                uc.Name = "uc" + strName;
                uc.Size = new System.Drawing.Size(200, 200);
                uc.TabIndex = 0;

                form.Controls.Add(dockPanel);

                ContentCollection.Add(pads[0]);
            }
            else
            {
                string strName = DateTime.Now.Ticks.ToString();
                DockPanel panelContainer = new DevExpress.XtraBars.Docking.DockPanel();

                dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] { panelContainer });
                panelContainer.Dock = dockStyle;
                panelContainer.ID = System.Guid.NewGuid();
                panelContainer.Location = new System.Drawing.Point(0, 0);
                panelContainer.Name = "panelContainer" + strName;
                panelContainer.OriginalSize = new System.Drawing.Size(200, 200);
                panelContainer.Size = new System.Drawing.Size(200, 200);
                panelContainer.Text = "panelContainer" + strName;
                bool bHaveActive = false;
                List<DockPanel> list = new List<DockPanel>();
                for (int i = 0; i < pads.Count; i++)
                {
                    XtraUserControl uc = (XtraUserControl)pads[i];

                    DockPanel dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
                    ControlContainer dockPanel_Container = new ControlContainer();

                    dockPanel.Controls.Add(dockPanel_Container);
                    dockPanel.DockedAsTabbedDocument = false;
                    dockPanel.Options.AllowDockAsTabbedDocument = false;
                    dockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
                    dockPanel.FloatVertical = true;
                    dockPanel.ID = System.Guid.NewGuid();
                    dockPanel.Location = new System.Drawing.Point(0, 0);
                    strName = DateTime.Now.Ticks.ToString();
                    dockPanel.Name = "dockPanel" + strName;
                    dockPanel.OriginalSize = new System.Drawing.Size(200, 200);
                    dockPanel.Size = new System.Drawing.Size(200, 200);
                    dockPanel.Text = pads[i].Title;
                    if (pads[i].ShowCloseButton) dockPanel.Options.ShowCloseButton = true;
                    else dockPanel.Options.ShowCloseButton = false;
                    dockPanel.Options.ShowMaximizeButton = true;
                    dockPanel.Height = pads[i].PHeight;

                    dockPanel_Container.Controls.Add(uc);
                    dockPanel_Container.Location = new System.Drawing.Point(0, 0);
                    dockPanel_Container.Name = "dockPanel_Container" + strName;
                    dockPanel_Container.Size = new System.Drawing.Size(200, 200);
                    dockPanel_Container.TabIndex = 0;
                    dockPanel_Container.Height = pads[i].PHeight;

                    uc.Dock = System.Windows.Forms.DockStyle.Fill;
                    uc.Location = new System.Drawing.Point(0, 0);
                    uc.Name = "uc" + strName;
                    uc.Size = new System.Drawing.Size(200, 200);
                    uc.TabIndex = 0;
                    uc.Height = pads[i].PHeight;

                    list.Add(dockPanel);

                    if (pads[i].IsActive)
                    {
                        panelContainer.FloatVertical = true;
                        panelContainer.ActiveChild = dockPanel;
                        bHaveActive = true;
                    }
                    ContentCollection.Add(pads[i]);
                }
                panelContainer.Controls.AddRange(list.ToArray());

                if (bHaveActive)
                {
                    panelContainer.Tabbed = true;
                }
                form.Controls.Add(panelContainer);
            }
        }
        public static void CreateAutoHidePad(System.Windows.Forms.Form form, DockManager dockManager, List<IPadContent> pads, System.Windows.Forms.DockStyle dockStyle, DevExpress.XtraBars.Docking.DockingStyle dockStyle1)
        {
            if (pads == null || pads.Count == 0) return;
            string strName = DateTime.Now.Ticks.ToString();
            AutoHideContainer panelContainer = new DevExpress.XtraBars.Docking.AutoHideContainer();

            dockManager.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] { panelContainer });

            panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            panelContainer.Dock = dockStyle;
            panelContainer.Location = new System.Drawing.Point(20, 147);
            panelContainer.Name = "panelContainer" + strName;
            panelContainer.Size = new System.Drawing.Size(864, 20);

            for (int i = 0; i < pads.Count; i++)
            {
                XtraUserControl uc = (XtraUserControl)pads[i];

                DockPanel dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
                ControlContainer dockPanel_Container = new ControlContainer();

                dockPanel.Controls.Add(dockPanel_Container);
                dockPanel.DockedAsTabbedDocument = false;
                dockPanel.Options.AllowDockAsTabbedDocument = false;
                dockPanel.Dock = dockStyle1;
                dockPanel.SavedDock = dockStyle1;
                dockPanel.FloatVertical = true;
                dockPanel.ID = System.Guid.NewGuid();
                dockPanel.Location = new System.Drawing.Point(0, 0);
                strName = DateTime.Now.Ticks.ToString();
                dockPanel.Name = "dockPanel" + strName;
                dockPanel.OriginalSize = new System.Drawing.Size(200, 200);
                dockPanel.Size = new System.Drawing.Size(200, 200);
                dockPanel.Text = pads[i].Title;
                if (pads[i].ShowCloseButton) dockPanel.Options.ShowCloseButton = true;
                else dockPanel.Options.ShowCloseButton = false;
                dockPanel.Visibility = DockVisibility.AutoHide;

                dockPanel.Options.ShowMaximizeButton = true;

                dockPanel_Container.Controls.Add(uc);
                dockPanel_Container.Location = new System.Drawing.Point(0, 0);
                dockPanel_Container.Name = "dockPanel_Container" + strName;
                dockPanel_Container.Size = new System.Drawing.Size(200, 200);
                dockPanel_Container.TabIndex = 0;

                uc.Dock = System.Windows.Forms.DockStyle.Fill;
                uc.Location = new System.Drawing.Point(0, 0);
                uc.Name = "uc" + strName;
                uc.Size = new System.Drawing.Size(200, 200);
                uc.TabIndex = 0;

                panelContainer.Controls.Add(dockPanel);

                ContentCollection.Add(pads[i]);
            }
            form.Controls.Add(panelContainer);
        }

        public static void CreatePads(System.Windows.Forms.Form form, DockManager dockManager)
        {
            CreatePad(form, dockManager, leftPads, DockingStyle.Left);
            CreatePad(form, dockManager, rightPads, DockingStyle.Right);
            CreatePad(form, dockManager, bottomPads, DockingStyle.Bottom);
            CreatePad(form, dockManager, topPads, DockingStyle.Top);
            CreateAutoHidePad(form, dockManager, leftAutoHidePads, System.Windows.Forms.DockStyle.Left, DevExpress.XtraBars.Docking.DockingStyle.Left);
            CreateAutoHidePad(form, dockManager, rightAutoHidePads, System.Windows.Forms.DockStyle.Right, DevExpress.XtraBars.Docking.DockingStyle.Right);
            CreateAutoHidePad(form, dockManager, bottomAutoHidePads, System.Windows.Forms.DockStyle.Bottom, DevExpress.XtraBars.Docking.DockingStyle.Bottom);
            CreateAutoHidePad(form, dockManager, topAutoHidePads, System.Windows.Forms.DockStyle.Top, DevExpress.XtraBars.Docking.DockingStyle.Top);
        }

        private static void SetPadGroupByDock(List<IPadContent> pads)
        {
            if (pads == null || pads.Count == 0) return;
            
            foreach (IPadContent pad in pads)
            {
                if (pad.AutoHide)
                {
                    switch (pad.Pos.ToLower())
                    {
                        case "left":
                            leftAutoHidePads.Add(pad);
                            break;
                        case "bottom":
                            bottomAutoHidePads.Add(pad);
                            break;
                        case "top":
                            topAutoHidePads.Add(pad);
                            break;
                        case "right":
                        default:
                            rightAutoHidePads.Add(pad);
                            break;
                    }
                }
                else
                {
                    switch (pad.Pos.ToLower())
                    {
                        case "left":
                            leftPads.Add(pad);
                            break;
                        case "bottom":
                            bottomPads.Add(pad);
                            break;
                        case "top":
                            topPads.Add(pad);
                            break;
                        case "right":
                        default:
                            rightPads.Add(pad);
                            break;
                    }
                }
            }
        }

        public static void AddPads(System.Windows.Forms.Form form, string addInTreePath)
        {
            List<IPadContent> pads = AddInTree.BuildItems<IPadContent>(addInTreePath, form);
            if (pads == null || pads.Count == 0) return;
            SetPadGroupByDock(pads);
        }
        public static List<IContent> AllContents
        {
            get { return UCService.ContentCollection;  }
        }
        public static IContent GetContent(Type type)
        {
            if (type == null) return null;
            foreach (IContent content in ContentCollection)
            {
                if (content.GetType() == type)
                {
                    return content;
                }
            }
            return null;
        }

        public static IContent GetContent(string title)
        {
            if (title == "") return null;
            foreach (IContent content in ContentCollection)
            {
                if (content.Title == title)
                {
                    return content;
                }
            }
            return null;
        }

        public static void ActiveContent(DockManager dockManager, IContent content)
        {
            if (content == null) return;
            foreach (IContent c in ContentCollection)
            {
                if (c == content)
                {
                    c.IsActive = true;
                    DFApplication.Application.CurrentContent = content;
                    break;
                }                
            }
            foreach (IContent c in ContentCollection)
            {
                if (c != content)
                {
                    c.IsActive = false;
                }
            }
        }

        public static void ActivatePanel(DockManager dockManager, string title)
        {
            ReadOnlyPanelCollection panels = dockManager.Panels;
            foreach (DockPanel panel in panels)
            {
                if (panel.Count >= 2)
                {
                    System.Windows.Forms.Control.ControlCollection controls = panel.Controls;
                    bool bHave = false;
                    foreach (System.Windows.Forms.Control control in controls)
                    {
                        if (control is DockPanel && title == control.Text)
                        {
                            panel.ActiveChild = (DockPanel)control;
                            bHave = true;
                            break;
                        }
                    }
                    if (bHave) break;
                }
                else
                {
                    if (title == panel.Text)
                    {
                        dockManager.ActivePanel = panel;                        
                        break;
                    }
                }
            }
        }
    }
}
