using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DF3DPipeCreateTool.UC;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;

namespace DF3DPipeCreateTool
{
    public class MainForm : DevExpress.XtraEditors.XtraForm
    {
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem nbiTemplateLib;
        private DevExpress.XtraNavBar.NavBarItem nbiPipeLib;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem nbiColorLib;
        private DevExpress.XtraNavBar.NavBarItem nbiModelLib;
        private DevExpress.XtraNavBar.NavBarItem nbiTextureLib;
        private DevExpress.XtraNavBar.NavBarItem nbiTopoLib;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem nbiDefaultField;
        private DevExpress.XtraNavBar.NavBarItem nbiFacilityClass;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup4;
        private DevExpress.XtraNavBar.NavBarItem nbiImportData;
        private DevExpress.XtraNavBar.NavBarItem nbiPipeSet;
        private DevExpress.XtraNavBar.NavBarItem nbiTopoCreate;
        private DevExpress.XtraEditors.PanelControl ucContainer;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem1;
        private DevExpress.XtraNavBar.NavBarItem nbiShow3DPipe;
        private DevExpress.XtraNavBar.NavBarItem nbiCatalogLib;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem3;
        private DevExpress.XtraNavBar.NavBarItem nbiSyncPipeLib;
        private DevExpress.XtraNavBar.NavBarItem nbiTempLib;
        private DevExpress.XtraNavBar.NavBarItem nbiAuto3DCreate;
    
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiTemplateLib = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPipeLib = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTempLib = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiColorLib = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiModelLib = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTextureLib = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.nbiCatalogLib = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTopoLib = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem3 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.nbiSyncPipeLib = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiDefaultField = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiFacilityClass = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup4 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiImportData = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPipeSet = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTopoCreate = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiAuto3DCreate = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.nbiShow3DPipe = new DevExpress.XtraNavBar.NavBarItem();
            this.ucContainer = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ucContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.navBarControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucContainer);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(819, 519);
            this.splitContainerControl1.SplitterPosition = 191;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Appearance.ButtonHotTracked.ForeColor = System.Drawing.Color.Red;
            this.navBarControl1.Appearance.ButtonHotTracked.Options.UseForeColor = true;
            this.navBarControl1.Appearance.ButtonPressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.navBarControl1.Appearance.ButtonPressed.Options.UseForeColor = true;
            this.navBarControl1.Appearance.GroupHeaderActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.navBarControl1.Appearance.GroupHeaderActive.Options.UseForeColor = true;
            this.navBarControl1.Appearance.GroupHeaderHotTracked.ForeColor = System.Drawing.Color.Red;
            this.navBarControl1.Appearance.GroupHeaderHotTracked.Options.UseForeColor = true;
            this.navBarControl1.Appearance.GroupHeaderPressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.navBarControl1.Appearance.GroupHeaderPressed.Options.UseForeColor = true;
            this.navBarControl1.Appearance.ItemActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.navBarControl1.Appearance.ItemActive.Options.UseForeColor = true;
            this.navBarControl1.Appearance.ItemHotTracked.ForeColor = System.Drawing.Color.Red;
            this.navBarControl1.Appearance.ItemHotTracked.Options.UseForeColor = true;
            this.navBarControl1.Appearance.ItemPressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.navBarControl1.Appearance.ItemPressed.Options.UseForeColor = true;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3,
            this.navBarGroup4});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiTemplateLib,
            this.nbiPipeLib,
            this.nbiColorLib,
            this.nbiModelLib,
            this.nbiTextureLib,
            this.nbiTopoLib,
            this.nbiDefaultField,
            this.nbiFacilityClass,
            this.nbiImportData,
            this.nbiPipeSet,
            this.nbiTopoCreate,
            this.nbiAuto3DCreate,
            this.navBarSeparatorItem1,
            this.nbiShow3DPipe,
            this.nbiCatalogLib,
            this.navBarSeparatorItem2,
            this.navBarSeparatorItem3,
            this.nbiSyncPipeLib,
            this.nbiTempLib});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 191;
            this.navBarControl1.Size = new System.Drawing.Size(191, 519);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.Appearance.Image")));
            this.navBarGroup1.Caption = "数据源管理";
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTemplateLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPipeLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTempLib)});
            this.navBarGroup1.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.LargeImage")));
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // nbiTemplateLib
            // 
            this.nbiTemplateLib.Caption = "模板数据库";
            this.nbiTemplateLib.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbiTemplateLib.LargeImage")));
            this.nbiTemplateLib.Name = "nbiTemplateLib";
            this.nbiTemplateLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTemplateLib.SmallImage")));
            this.nbiTemplateLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiTemplateLib_LinkClicked);
            // 
            // nbiPipeLib
            // 
            this.nbiPipeLib.Caption = "管线数据库";
            this.nbiPipeLib.Name = "nbiPipeLib";
            this.nbiPipeLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiPipeLib.SmallImage")));
            this.nbiPipeLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPipeLib_LinkClicked);
            // 
            // nbiTempLib
            // 
            this.nbiTempLib.Caption = "临时数据库";
            this.nbiTempLib.Name = "nbiTempLib";
            this.nbiTempLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTempLib.SmallImage")));
            this.nbiTempLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiTempLib_LinkClicked);
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "模板库管理";
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiColorLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiModelLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTextureLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiCatalogLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTopoLib),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem3),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiSyncPipeLib)});
            this.navBarGroup2.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup2.LargeImage")));
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // nbiColorLib
            // 
            this.nbiColorLib.Caption = "颜色库管理";
            this.nbiColorLib.Name = "nbiColorLib";
            this.nbiColorLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiColorLib.SmallImage")));
            this.nbiColorLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiColorLib_LinkClicked);
            // 
            // nbiModelLib
            // 
            this.nbiModelLib.Caption = "模型库管理";
            this.nbiModelLib.Name = "nbiModelLib";
            this.nbiModelLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiModelLib.SmallImage")));
            this.nbiModelLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiModelLib_LinkClicked);
            // 
            // nbiTextureLib
            // 
            this.nbiTextureLib.Caption = "纹理库管理";
            this.nbiTextureLib.Name = "nbiTextureLib";
            this.nbiTextureLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTextureLib.SmallImage")));
            this.nbiTextureLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiTextureLib_LinkClicked);
            // 
            // navBarSeparatorItem2
            // 
            this.navBarSeparatorItem2.CanDrag = false;
            this.navBarSeparatorItem2.Enabled = false;
            this.navBarSeparatorItem2.Hint = null;
            this.navBarSeparatorItem2.LargeImageIndex = 0;
            this.navBarSeparatorItem2.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem2.Name = "navBarSeparatorItem2";
            this.navBarSeparatorItem2.SmallImageIndex = 0;
            this.navBarSeparatorItem2.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // nbiCatalogLib
            // 
            this.nbiCatalogLib.Caption = "目录树管理";
            this.nbiCatalogLib.Name = "nbiCatalogLib";
            this.nbiCatalogLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiCatalogLib.SmallImage")));
            this.nbiCatalogLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiCatalogLib_LinkClicked);
            // 
            // nbiTopoLib
            // 
            this.nbiTopoLib.Caption = "拓扑层管理";
            this.nbiTopoLib.Name = "nbiTopoLib";
            this.nbiTopoLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTopoLib.SmallImage")));
            this.nbiTopoLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiTopoLib_LinkClicked);
            // 
            // navBarSeparatorItem3
            // 
            this.navBarSeparatorItem3.CanDrag = false;
            this.navBarSeparatorItem3.Enabled = false;
            this.navBarSeparatorItem3.Hint = null;
            this.navBarSeparatorItem3.LargeImageIndex = 0;
            this.navBarSeparatorItem3.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem3.Name = "navBarSeparatorItem3";
            this.navBarSeparatorItem3.SmallImageIndex = 0;
            this.navBarSeparatorItem3.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // nbiSyncPipeLib
            // 
            this.nbiSyncPipeLib.Caption = "同步管线库设施类";
            this.nbiSyncPipeLib.Name = "nbiSyncPipeLib";
            this.nbiSyncPipeLib.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiSyncPipeLib.SmallImage")));
            this.nbiSyncPipeLib.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiSyncPipeLib_LinkClicked);
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "管线库配置";
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDefaultField),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiFacilityClass)});
            this.navBarGroup3.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup3.LargeImage")));
            this.navBarGroup3.Name = "navBarGroup3";
            this.navBarGroup3.Visible = false;
            // 
            // nbiDefaultField
            // 
            this.nbiDefaultField.Caption = "默认字段设置";
            this.nbiDefaultField.Name = "nbiDefaultField";
            this.nbiDefaultField.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDefaultField.SmallImage")));
            // 
            // nbiFacilityClass
            // 
            this.nbiFacilityClass.Caption = "设施类配置";
            this.nbiFacilityClass.Name = "nbiFacilityClass";
            this.nbiFacilityClass.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiFacilityClass.SmallImage")));
            // 
            // navBarGroup4
            // 
            this.navBarGroup4.Caption = "管线三维化";
            this.navBarGroup4.Expanded = true;
            this.navBarGroup4.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiImportData),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPipeSet),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTopoCreate),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiAuto3DCreate),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiShow3DPipe)});
            this.navBarGroup4.LargeImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup4.LargeImage")));
            this.navBarGroup4.Name = "navBarGroup4";
            // 
            // nbiImportData
            // 
            this.nbiImportData.Caption = "数据导入";
            this.nbiImportData.Name = "nbiImportData";
            this.nbiImportData.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiImportData.SmallImage")));
            this.nbiImportData.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiImportData_LinkClicked);
            // 
            // nbiPipeSet
            // 
            this.nbiPipeSet.Caption = "管线指定";
            this.nbiPipeSet.Name = "nbiPipeSet";
            this.nbiPipeSet.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiPipeSet.SmallImage")));
            this.nbiPipeSet.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPipeSet_LinkClicked);
            // 
            // nbiTopoCreate
            // 
            this.nbiTopoCreate.Caption = "拓扑创建";
            this.nbiTopoCreate.Name = "nbiTopoCreate";
            this.nbiTopoCreate.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTopoCreate.SmallImage")));
            this.nbiTopoCreate.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiTopoCreate_LinkClicked);
            // 
            // nbiAuto3DCreate
            // 
            this.nbiAuto3DCreate.Caption = "自动三维化";
            this.nbiAuto3DCreate.Name = "nbiAuto3DCreate";
            this.nbiAuto3DCreate.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiAuto3DCreate.SmallImage")));
            this.nbiAuto3DCreate.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiAuto3DCreate_LinkClicked);
            // 
            // navBarSeparatorItem1
            // 
            this.navBarSeparatorItem1.CanDrag = false;
            this.navBarSeparatorItem1.Enabled = false;
            this.navBarSeparatorItem1.Hint = null;
            this.navBarSeparatorItem1.LargeImageIndex = 0;
            this.navBarSeparatorItem1.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem1.Name = "navBarSeparatorItem1";
            this.navBarSeparatorItem1.SmallImageIndex = 0;
            this.navBarSeparatorItem1.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // nbiShow3DPipe
            // 
            this.nbiShow3DPipe.Caption = "结果展示";
            this.nbiShow3DPipe.Name = "nbiShow3DPipe";
            this.nbiShow3DPipe.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiShow3DPipe.SmallImage")));
            this.nbiShow3DPipe.LinkPressed += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiShow3DPipe_LinkPressed);
            // 
            // ucContainer
            // 
            this.ucContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucContainer.Location = new System.Drawing.Point(0, 0);
            this.ucContainer.Name = "ucContainer";
            this.ucContainer.Size = new System.Drawing.Size(623, 519);
            this.ucContainer.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(819, 519);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三维管线生成工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ucContainer)).EndInit();
            this.ResumeLayout(false);

        }

        private UCColorLib ucColorLib;
        private UCModelLib ucModelLib;
        private UCTextureLib ucTextureLib;
        private UCTemplateLib ucTemplateLib;
        private UCPipeLib ucPipeLib;
        private UCTempLib ucTempLib;
        private UCCatalogLib ucCatalogLib;
        private UCTopoLib ucTopoLib;
        private UCImportData ucImportData;
        private UCPipeSet ucPipeSet;
        private UCTopoCreate ucTopoCreate;
        private UCAuto3DCreate ucAuto3DCreate;
        private UCShow3DPipe ucShow3DPipe;

        private void nbiTemplateLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucTemplateLib == null)
            {
                ucTemplateLib = new UCTemplateLib();
                ucTemplateLib.Dock = DockStyle.Fill;
            }
            this.ucContainer.Controls.Add(ucTemplateLib);
        }

        private void nbiPipeLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucPipeLib == null)
            {
                ucPipeLib = new UCPipeLib();
                ucPipeLib.Dock = DockStyle.Fill;
            }
            this.ucContainer.Controls.Add(ucPipeLib);
        }

        private void nbiTempLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucTempLib == null)
            {
                ucTempLib = new UCTempLib();
                ucTempLib.Dock = DockStyle.Fill;
            }
            this.ucContainer.Controls.Add(ucTempLib);
        }
        private void nbiColorLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucColorLib == null)
            {
                ucColorLib = new UCColorLib();
                ucColorLib.Dock = DockStyle.Fill;
            }
            ucColorLib.Init();
            this.ucContainer.Controls.Add(ucColorLib);
        }

        private void nbiModelLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (this.ucShow3DPipe != null)
            {
                this.ucShow3DPipe.Dispose();
                this.ucShow3DPipe = null;
            }
            if (ucModelLib == null)
            {
                ucModelLib = new UCModelLib();
                ucModelLib.Dock = DockStyle.Fill;
            }
            ucModelLib.Init();
            this.ucContainer.Controls.Add(ucModelLib);
        }

        private void nbiTextureLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucTextureLib == null)
            {
                ucTextureLib = new UCTextureLib();
                ucTextureLib.Dock = DockStyle.Fill;
            }
            ucTextureLib.Init();
            this.ucContainer.Controls.Add(ucTextureLib);
        }

        private void nbiCatalogLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucCatalogLib == null)
            {
                ucCatalogLib = new UCCatalogLib();
                ucCatalogLib.Dock = DockStyle.Fill;
            }
            ucCatalogLib.Init();
            this.ucContainer.Controls.Add(ucCatalogLib);
        }

        private void nbiTopoLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucTopoLib == null)
            {
                ucTopoLib = new UCTopoLib();
                ucTopoLib.Dock = DockStyle.Fill;
            }
            ucTopoLib.Init();
            this.ucContainer.Controls.Add(ucTopoLib);
        }

        private void nbiSyncPipeLib_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (this.ucShow3DPipe != null)
            {
                this.ucShow3DPipe.Dispose();
                this.ucShow3DPipe = null;
            } 
            SyncPipeLib.Instance.Run();
        }

        private void nbiImportData_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (ucImportData == null)
            {
                ucImportData = new UCImportData();
                ucImportData.Dock = DockStyle.Fill;
            }
            ucImportData.Init();
            this.ucContainer.Controls.Add(ucImportData);
        }

        private void nbiPipeSet_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (this.ucShow3DPipe != null)
            {
                this.ucShow3DPipe.Dispose();
                this.ucShow3DPipe = null;
            }
            if (ucPipeSet == null)
            {
                ucPipeSet = new UCPipeSet();
                ucPipeSet.Dock = DockStyle.Fill;
            }
            ucPipeSet.Init();
            this.ucContainer.Controls.Add(ucPipeSet);
        }

        private void nbiTopoCreate_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (this.ucShow3DPipe != null)
            {
                this.ucShow3DPipe.Dispose();
                this.ucShow3DPipe = null;
            }
            if (ucTopoCreate == null)
            {
                ucTopoCreate = new UCTopoCreate();
                ucTopoCreate.Dock = DockStyle.Fill;
            }
            ucTopoCreate.Init();
            this.ucContainer.Controls.Add(ucTopoCreate);
        }

        private void nbiAuto3DCreate_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (this.ucShow3DPipe != null)
            {
                this.ucShow3DPipe.Dispose();
                this.ucShow3DPipe = null;
            } 
            if (ucAuto3DCreate == null)
            {
                ucAuto3DCreate = new UCAuto3DCreate();
                ucAuto3DCreate.Dock = DockStyle.Fill;
            }
            ucAuto3DCreate.Init();
            this.ucContainer.Controls.Add(ucAuto3DCreate);
        }
        private void nbiShow3DPipe_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ucContainer.Controls.Clear();
            if (this.ucModelLib != null)
            {
                this.ucModelLib.Dispose();
                this.ucModelLib = null;
            } 
            if (ucShow3DPipe == null)
            {
                ucShow3DPipe = new UCShow3DPipe();
                ucShow3DPipe.Dock = DockStyle.Fill;
            }
            ucShow3DPipe.Init();
            this.ucContainer.Controls.Add(ucShow3DPipe);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DF3DPipeCreateApp.App.PipeLib != null)
            {
                DF3DPipeCreateApp.App.PipeLib.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.PipeLib);
                DF3DPipeCreateApp.App.PipeLib = null;
            } 
            if (DF3DPipeCreateApp.App.TemplateLib != null)
            {
                DF3DPipeCreateApp.App.TemplateLib.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.TemplateLib);
                DF3DPipeCreateApp.App.TemplateLib = null;
            }
            if (DF3DPipeCreateApp.App.TempLib != null)
            {
                DF3DPipeCreateApp.App.TempLib.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.TempLib);
                DF3DPipeCreateApp.App.TempLib = null;
            }
        }



    }
}
