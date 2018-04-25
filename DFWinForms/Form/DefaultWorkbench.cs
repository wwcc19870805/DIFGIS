using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DFWinForms.Service;
using ICSharpCode.Core;
using DFCommon.Class;
using DFWinForms.Base;
using DFWinForms.UserControl;
using DFWinForms.Command;
using DevExpress.XtraSplashScreen;

namespace DFWinForms.Form
{
    public class DefaultWorkbench : RibbonForm
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private ApplicationMenu appMenu;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private BarEditItem biStyle;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicStyle;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;
        private BarStaticItem barStatusItemInfo;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private BarStaticItem barOtherInfo;
        private BarButtonItem barButtonItemHelp;

        public DefaultWorkbench()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();
            if (!DevExpress.Skins.SkinManager.AllowFormSkins)
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            }
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultWorkbench));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.appMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItemHelp = new DevExpress.XtraBars.BarButtonItem();
            this.biStyle = new DevExpress.XtraBars.BarEditItem();
            this.riicStyle = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barStatusItemInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barOtherInfo = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.appMenu;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Images = this.imageCollection1;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barButtonItemHelp,
            this.biStyle,
            this.barStatusItemInfo,
            this.barOtherInfo});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 3;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageHeaderItemLinks.Add(this.barButtonItemHelp);
            this.ribbon.PageHeaderItemLinks.Add(this.biStyle);
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riicStyle});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(920, 50);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            this.ribbon.SelectedPageChanged += new System.EventHandler(this.ribbon_SelectedPageChanged);
            // 
            // appMenu
            // 
            this.appMenu.Name = "appMenu";
            this.appMenu.Ribbon = this.ribbon;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("question_16x16.png", "images/support/question_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/support/question_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "question_16x16.png");
            // 
            // barButtonItemHelp
            // 
            this.barButtonItemHelp.Caption = "帮助";
            this.barButtonItemHelp.Id = 2;
            this.barButtonItemHelp.ImageIndex = 0;
            this.barButtonItemHelp.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.barButtonItemHelp.Name = "barButtonItemHelp";
            this.barButtonItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHelp_ItemClick);
            // 
            // biStyle
            // 
            this.biStyle.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.biStyle.Edit = this.riicStyle;
            this.biStyle.EditValue = this.ribbon.RibbonStyle;
            this.biStyle.Id = 1;
            this.biStyle.Name = "biStyle";
            this.biStyle.Width = 130;
            this.biStyle.EditValueChanged += new System.EventHandler(this.biStyle_EditValueChanged);
            // 
            // riicStyle
            // 
            this.riicStyle.AutoHeight = false;
            this.riicStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicStyle.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Default", DevExpress.XtraBars.Ribbon.RibbonControlStyle.Default, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Office 2007 Blue", DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Office 2010 Silver", DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Office 2013", DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013, -1)});
            this.riicStyle.Name = "riicStyle";
            // 
            // barStatusItemInfo
            // 
            this.barStatusItemInfo.Caption = "信息提示";
            this.barStatusItemInfo.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barStatusItemInfo.Id = 1;
            this.barStatusItemInfo.Name = "barStatusItemInfo";
            this.barStatusItemInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barOtherInfo
            // 
            this.barOtherInfo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barOtherInfo.Id = 2;
            this.barOtherInfo.Name = "barOtherInfo";
            this.barOtherInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStatusItemInfo);
            this.ribbonStatusBar.ItemLinks.Add(this.barOtherInfo);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 448);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(920, 31);
            // 
            // dockManager
            // 
            this.dockManager.DockingOptions.HideImmediatelyOnAutoHide = true;
            this.dockManager.DockModeVS2005FadeFramesCount = 1;
            this.dockManager.Form = this;
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager.ActivePanelChanged += new DevExpress.XtraBars.Docking.ActivePanelChangedEventHandler(this.dockManager_ActivePanelChanged);
            this.dockManager.ActiveChildChanged += new DevExpress.XtraBars.Docking.DockPanelEventHandler(this.dockManager_ActiveChildChanged);
            // 
            // documentManager
            // 
            this.documentManager.ContainerControl = this;
            this.documentManager.View = this.tabbedView;
            this.documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView});
            this.documentManager.DocumentActivate += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(this.documentManager_DocumentActivate);
            // 
            // tabbedView
            // 
            this.tabbedView.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // DefaultWorkbench
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 479);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DefaultWorkbench";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "DefaultWorkbench";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DefaultWorkbench_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DefaultWorkbench_FormClosed);
            this.Shown += new System.EventHandler(this.DefaultWorkbench_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            this.ResumeLayout(false);

        }

        private void barButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string fileName = string.Empty;
                fileName = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\Help\\help." + ResourceService.Language + ".chm");
                new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(fileName)
                }.Start();
            }
            catch (Exception)
            {
            }

        }

        private void biStyle_EditValueChanged(object sender, EventArgs e)
        {
            RibbonControlStyle style = (RibbonControlStyle)biStyle.EditValue;
            this.ribbon.RibbonStyle = style;
            if (style == RibbonControlStyle.Office2010 || style == RibbonControlStyle.Office2013)
            {
                //this.ribbon.ApplicationButtonDropDownControl = this.backstageViewControl1;
            }
            else
            {
                this.ribbon.ApplicationButtonDropDownControl = this.appMenu;
            }
            string skinName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultWorkbench));
            switch (style)
            {
                case RibbonControlStyle.Default:
                    skinName = "DevExpress Style";
                    this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
                    break;
                case RibbonControlStyle.Office2007:
                    skinName = "Office 2007 Blue";
                    this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
                    break;
                case RibbonControlStyle.Office2013:
                    skinName = "Office 2013";
                    this.ribbon.ApplicationIcon = null;
                    break;
                case RibbonControlStyle.Office2010:
                default:
                    skinName = "Office 2010 Silver";
                    this.ribbon.ApplicationIcon = null;
                    break;
            }
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        public void SetStatusInfo(string caption)
        {
            this.barStatusItemInfo.Caption = caption;
        }
        public void SetOtherInfo(string caption)
        {
            this.barOtherInfo.Caption = caption;
        }
        public void SetMenuEnable(bool bEnable)
        {
            List<IContent> allConents = UCService.AllContents;
            if (allConents != null)
            {
                foreach (IContent c in allConents)
                {
                    if (c is System.Windows.Forms.UserControl && c is IPadContent)
                    {
                        System.Windows.Forms.UserControl uc = c as System.Windows.Forms.UserControl;
                        uc.Enabled = bEnable;
                    }
                }
            }
            this.ribbon.Enabled = bEnable;
        }
        public void Initialize()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(MainAppSplashScreen), true, true);
                SplashScreenManager.Default.SendCommand(null, "开始启动系统......");
                SplashScreenManager.Default.SendCommand(null, "开始初始化主窗体控件......");
                string systemType = SystemInfo.Instance.SystemType;
                if (systemType == "2D" || systemType == "3D")
                {
                    //先创建View再创建Pad
                    SplashScreenManager.Default.SendCommand(null, "开始初始化视图......");
                    UCService.AddViews(this, "/Workbench/View/" + systemType);
                    UCService.CreateViews(components, dockManager, tabbedView);

                    SplashScreenManager.Default.SendCommand(null, "开始初始化面板......");
                    UCService.AddPads(this, "/Workbench/Pad/" + systemType);
                    UCService.CreatePads(this, dockManager);

                    AutoStartCmds("/Workspace/Autostart/" + systemType);

                    SplashScreenManager.Default.SendCommand(null, "正在加载菜单......");
                    MenuService.AddItemsToMenu(this.ribbon.Pages, this, "/Workbench/MainMenu/" + systemType);
                    MenuService.AddItemsToMenu(this.ribbon.Pages, this, "/Workbench/MainMenu");
                    if (this.ribbon.Pages.Count > 0) this.ribbon.SelectedPage = this.ribbon.Pages[0];
                    MenuService.AddQuickToolBar(this.ribbon.Items, this.ribbon.Toolbar.ItemLinks);
                    MenuService.AddApplicationMenu(this.appMenu.ItemLinks, this, "/Workbench/ApplicationMenu");

                }
                else
                {       
                    //先创建View再创建Pad
                    SplashScreenManager.Default.SendCommand(null, "开始初始化视图......");
                    UCService.AddViews(this, "/Workbench/View/2D");
                    UCService.AddViews(this, "/Workbench/View/3D");
                    UCService.CreateViews(components, dockManager, tabbedView);

                    SplashScreenManager.Default.SendCommand(null, "开始初始化面板......");
                    UCService.AddPads(this, "/Workbench/Pad/2D");
                    UCService.AddPads(this, "/Workbench/Pad/3D");
                    UCService.CreatePads(this, dockManager);

                    AutoStartCmds("/Workspace/Autostart/2D");
                    AutoStartCmds("/Workspace/Autostart/3D");

                    SplashScreenManager.Default.SendCommand(null, "正在加载菜单......");
                    MenuService.AddItemsToMenu(this.ribbon.Pages, this, "/Workbench/MainMenu/2D");
                    MenuService.AddItemsToMenu(this.ribbon.Pages, this, "/Workbench/MainMenu/3D");
                    MenuService.AddItemsToMenu(this.ribbon.Pages, this, "/Workbench/MainMenu");
                    if (this.ribbon.Pages.Count > 0) this.ribbon.SelectedPage = this.ribbon.Pages[0];
                    MenuService.AddQuickToolBar(this.ribbon.Items, this.ribbon.Toolbar.ItemLinks);
                    MenuService.AddApplicationMenu(this.appMenu.ItemLinks, this, "/Workbench/ApplicationMenu");                    
                }
                SplashScreenManager.CloseForm();
                //this.ribbon.Minimized = true;
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void AutoStartCmds(string addInTreePath)
        {
            foreach (ICommand current in AddInTree.BuildItems<ICommand>(addInTreePath, null, false))
            {
                try
                {
                    current.Run(null, null);
                }
                catch (System.Exception ex)
                {
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }

        #region 菜单管理
        public void AddRibbonPageCategory(string addInTreePath)//当需要扩展功能时，激活Category标签页
        {
            RemoveRibbonPageCategory();
            MenuService.AddItemsToMenu(this.ribbon.PageCategories, this, addInTreePath);
        }

        public void RemoveRibbonPageCategory()
        {
            this.ribbon.PageCategories.Clear();
        }

        public void SwitchToMenu(string[] addInTreePaths)
        {
            this.ribbon.SelectedPageChanged -=new EventHandler(ribbon_SelectedPageChanged);
            MenuService.SetToolBarsInvisible(this.ribbon.Toolbar.ItemLinks);
            MenuService.SwitchToMenu(this.ribbon.Pages, this.ribbon.Toolbar.ItemLinks, addInTreePaths);
            #region 初始化
            RibbonPage firstVisiblePage = null;
            foreach (RibbonPage page in this.ribbon.Pages)
            {
                if (page.Visible == true)
                {
                    firstVisiblePage = page;
                    break;
                }
            }
            if (firstVisiblePage != null) this.ribbon.SelectedPage = firstVisiblePage;
            #endregion
            if (DFApplication.Application != null && DFApplication.Application.CurrentContent != null && DFApplication.Application.CurrentContent.CurrentRibbonPage != null)
                this.ribbon.SelectedPage = DFApplication.Application.CurrentContent.CurrentRibbonPage as RibbonPage;
            this.ribbon.SelectedPageChanged += new EventHandler(ribbon_SelectedPageChanged);
        }
        public void UpdateMenu()
        {
            MenuService.UpdateMenu(this.ribbon.Items);
        }

        public void BarPerformClick(string barName)
        {
            foreach (BarItem item in this.ribbon.Items)
            {
                if (item.Name == barName)
                {
                    item.PerformClick();
                    break;
                }
            }
        }
        private void ribbon_SelectedPageChanged(object sender, EventArgs e)
        {
            if (DFApplication.Application != null && DFApplication.Application.CurrentContent != null && DFApplication.Application.CurrentContent.IsActive == true)
                DFApplication.Application.CurrentContent.SetCurrentRibbonPage(this.ribbon.SelectedPage);
        }
        #endregion

        public void ActivateContent(Type type)
        {
            IContent content = UCService.GetContent(type);
            ActivateContent(content);
        }
        public void ActivateContent(string title)
        {
            IContent content = UCService.GetContent(title);
            ActivateContent(content);
        }
        public void ActivateContent(IContent content)
        {
            if (content == null) return;
            UCService.ActiveContent(this.dockManager, content);
        }
        public void ActivatePanel(Type type)
        {
            IContent content = UCService.GetContent(type);
            ActivatePanel(content.Title);
        }
        public void ActivatePanel(IContent content)
        {
            if (content == null) return;
            ActivatePanel(content.Title);
        }
        public void ActivatePanel(string title)
        {
            UCService.ActivatePanel(this.dockManager, UCService.InitActivateViewText);
        }

        public DevExpress.XtraBars.Docking.DockPanel AddPanel(DevExpress.XtraBars.Docking.DockingStyle ds)
        {
            return this.dockManager.AddPanel(ds);
        }

        public void ActivateDockPanel(DevExpress.XtraBars.Docking.DockPanel panel)
        {
            if (panel != null && panel.Controls != null && panel.Controls.Count > 0)
            {
                Control c = panel.Controls[0];
                if (c is DevExpress.XtraBars.Docking.ControlContainer)
                {
                    DevExpress.XtraBars.Docking.ControlContainer cc = c as DevExpress.XtraBars.Docking.ControlContainer;
                    if (cc != null && cc.Controls != null && cc.Controls.Count > 0)
                    {
                        if (cc.Controls[0] is IPadContent)
                        {
                            SetActivePanel(UCService.LastActiveDocPanel);
                        }
                        if (cc.Controls[0] is IContent)
                        {
                            IContent content = cc.Controls[0] as IContent;
                            content.Activate();
                        }                        
                    }
                }
            }
        }


        private void dockManager_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            if (e.Panel == null) return;
            DevExpress.XtraBars.Docking.DockPanel panel = e.Panel;
            ActivateDockPanel(panel);
            if (panel.DockedAsTabbedDocument) UCService.LastActiveDocPanel = panel;
        }

        private void dockManager_ActivePanelChanged(object sender, DevExpress.XtraBars.Docking.ActivePanelChangedEventArgs e)
        {
            if (e.Panel == null) return;
            DevExpress.XtraBars.Docking.DockPanel panel = e.Panel;
            ActivateDockPanel(panel);
            if (panel.DockedAsTabbedDocument) UCService.LastActiveDocPanel = panel;
        }
        public void SetActivePanel(DevExpress.XtraBars.Docking.DockPanel panel)
        {
            if (panel == null) return;
            this.dockManager.ActivePanel = panel;
        }
        private void documentManager_DocumentActivate(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (e.Document == null || e.Document.Control == null) return;
            if (!(e.Document.Control is DevExpress.XtraBars.Docking.DockPanel)) return;
            DevExpress.XtraBars.Docking.DockPanel panel = e.Document.Control as DevExpress.XtraBars.Docking.DockPanel;
            ActivateDockPanel(panel);
            if (panel.DockedAsTabbedDocument) UCService.LastActiveDocPanel = panel;
        }
        
        private void DefaultWorkbench_Shown(object sender, EventArgs e)
        {
            ActivateContent(UCService.InitActivateViewText);
            ActivatePanel(UCService.InitActivateViewText);
            //初始命令
            string systemType = SystemInfo.Instance.SystemType;
            if (systemType == "2D" || systemType == "3D")
                AutoStartCmds("/Workspace/Autoinit/" + systemType);
            else
            {
                AutoStartCmds("/Workspace/Autoinit/2D");
                AutoStartCmds("/Workspace/Autoinit/3D");
                AutoStartCmds("/Workspace/Autoinit");
            }
        }

        private void DefaultWorkbench_FormClosing(object sender, FormClosingEventArgs e)
        {
            string systemType = SystemInfo.Instance.SystemType;
            if (systemType == "2D" || systemType == "3D")
                AutoStartCmds("/Workspace/Autoend/" + systemType);
            else
            {
                AutoStartCmds("/Workspace/Autoend/2D");
                AutoStartCmds("/Workspace/Autoend/3D");
                AutoStartCmds("/Workspace/Autoend");
            }
        }

        private void DefaultWorkbench_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoggingService.Info(SystemInfo.Instance.SystemFullName + "关闭！");
        }

        public void CloseWorkBench()
        {
            base.Close();
        }

        public int GetDocumentCount()
        {
            return this.tabbedView.Documents.Count;
        }

        public int GetDocumentGroupCount()
        {
            return this.tabbedView.DocumentGroups.Count;
        }
    }
}