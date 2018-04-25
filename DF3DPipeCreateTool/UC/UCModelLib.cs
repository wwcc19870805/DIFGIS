using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using System.Drawing.Imaging;
using Gvitech.CityMaker.Math;
using System.Runtime.InteropServices;
using DFDataConfig.Class;
using DFWinForms.Class;
namespace DF3DPipeCreateTool.UC
{
    public class UCModelLib : XtraUserControl
    {
        private Bar bar2;
        private BarButtonItem barButtonItem1;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarButtonItem bItemDeleteGroup;
        private BarButtonItem bItemDeleteModel;
        private BarButtonItem bItemReNameGroup;
        private BarButtonItem bItemReNameModel;
        private BarButtonItem bItemUpdateModel;
        private BarButtonItem bItemUpdatePic;
        private BarButtonItem bItemViewModel;
        private BarButtonItem btnCreateGroup;
        private BarButtonItem btnExport;
        private BarButtonItem btnHideGroup;
        private BarButtonItem btnImport;
        private IContainer components;
        private PopupMenu popupMenuGroup;
        private PopupMenu popupMenuModel;
        private SplitContainerControl splitContainerControl1;
        private TreeListColumn tl_Name;
        private ImageCollection imageCollection1;
        private TreeListColumn tl_ObjectId;
        private SplitContainerControl splitContainerControl2;
        private GridControl gridControl1;
        private LayoutView layoutView1;
        private LayoutViewColumn colName;
        private RepositoryItemTextEdit repositoryItemTextEdit1;
        private LayoutViewField layoutViewField_colName;
        private LayoutViewColumn colThumbnail;
        private RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private LayoutViewField layoutViewField_colPicture;
        private LayoutViewColumn colComment;
        private LayoutViewField layoutViewField_colComment;
        private LayoutViewColumn colModelInfo;
        private LayoutViewField layoutViewField_layoutViewColumn1_1;
        private LayoutViewCard layoutViewCard1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private TreeList treeModelInfo;
        private Gvitech.CityMaker.Controls.AxRenderControl AxRenderControl3D;

        // Methods

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCModelLib));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnHideGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnCreateGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnImport = new DevExpress.XtraBars.BarButtonItem();
            this.btnExport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.bItemReNameGroup = new DevExpress.XtraBars.BarButtonItem();
            this.bItemDeleteGroup = new DevExpress.XtraBars.BarButtonItem();
            this.bItemUpdateModel = new DevExpress.XtraBars.BarButtonItem();
            this.bItemUpdatePic = new DevExpress.XtraBars.BarButtonItem();
            this.bItemViewModel = new DevExpress.XtraBars.BarButtonItem();
            this.bItemReNameModel = new DevExpress.XtraBars.BarButtonItem();
            this.bItemDeleteModel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.treeModelInfo = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_ObjectId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.colName = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutViewField_colName = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colThumbnail = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewField_colPicture = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colComment = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colComment = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colModelInfo = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.AxRenderControl3D = new Gvitech.CityMaker.Controls.AxRenderControl();
            this.popupMenuGroup = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuModel = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeModelInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AxRenderControl3D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuModel)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnHideGroup,
            this.btnCreateGroup,
            this.btnImport,
            this.btnExport,
            this.bItemReNameGroup,
            this.bItemDeleteGroup,
            this.bItemUpdateModel,
            this.bItemUpdatePic,
            this.bItemViewModel,
            this.bItemReNameModel,
            this.bItemDeleteModel,
            this.barButtonItem1});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 27;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(344, 114);
            this.bar2.FloatSize = new System.Drawing.Size(252, 31);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHideGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCreateGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImport),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExport)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.Text = "Main menu";
            // 
            // btnHideGroup
            // 
            this.btnHideGroup.Caption = "显示隐藏组";
            this.btnHideGroup.Id = 14;
            this.btnHideGroup.ImageIndex = 0;
            this.btnHideGroup.Name = "btnHideGroup";
            this.btnHideGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHideGroup_ItemClick);
            // 
            // btnCreateGroup
            // 
            this.btnCreateGroup.Caption = "创建组";
            this.btnCreateGroup.Id = 15;
            this.btnCreateGroup.ImageIndex = 1;
            this.btnCreateGroup.Name = "btnCreateGroup";
            this.btnCreateGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCreateGroup_ItemClick);
            // 
            // btnImport
            // 
            this.btnImport.Caption = "导入模型";
            this.btnImport.Enabled = false;
            this.btnImport.Id = 17;
            this.btnImport.ImageIndex = 3;
            this.btnImport.Name = "btnImport";
            this.btnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImport_ItemClick);
            // 
            // btnExport
            // 
            this.btnExport.Caption = "导出模型";
            this.btnExport.Enabled = false;
            this.btnExport.Id = 18;
            this.btnExport.ImageIndex = 4;
            this.btnExport.Name = "btnExport";
            this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(533, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 647);
            this.barDockControlBottom.Size = new System.Drawing.Size(533, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 623);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(533, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 623);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "btnShowGroup.Glyph.png");
            this.imageCollection1.Images.SetKeyName(1, "btnNewGroup.Glyph.png");
            this.imageCollection1.Images.SetKeyName(2, "btnNewLocation.Glyph.png");
            this.imageCollection1.Images.SetKeyName(3, "btnImport.Glyph.png");
            this.imageCollection1.Images.SetKeyName(4, "btnExport.Glyph.png");
            // 
            // bItemReNameGroup
            // 
            this.bItemReNameGroup.Caption = "重命名";
            this.bItemReNameGroup.Id = 19;
            this.bItemReNameGroup.Name = "bItemReNameGroup";
            this.bItemReNameGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemReNameGroup_ItemClick);
            // 
            // bItemDeleteGroup
            // 
            this.bItemDeleteGroup.Caption = "删除";
            this.bItemDeleteGroup.Id = 20;
            this.bItemDeleteGroup.Name = "bItemDeleteGroup";
            this.bItemDeleteGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemDeleteGroup_ItemClick);
            // 
            // bItemUpdateModel
            // 
            this.bItemUpdateModel.Caption = "替换模型";
            this.bItemUpdateModel.Id = 21;
            this.bItemUpdateModel.Name = "bItemUpdateModel";
            this.bItemUpdateModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemUpdateModel_ItemClick);
            // 
            // bItemUpdatePic
            // 
            this.bItemUpdatePic.Caption = "替换缩略图";
            this.bItemUpdatePic.Id = 22;
            this.bItemUpdatePic.Name = "bItemUpdatePic";
            this.bItemUpdatePic.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemUpdatePic_ItemClick);
            // 
            // bItemViewModel
            // 
            this.bItemViewModel.Caption = "模型预览";
            this.bItemViewModel.Id = 23;
            this.bItemViewModel.Name = "bItemViewModel";
            this.bItemViewModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemViewModel_ItemClick);
            // 
            // bItemReNameModel
            // 
            this.bItemReNameModel.Caption = "重命名";
            this.bItemReNameModel.Id = 24;
            this.bItemReNameModel.Name = "bItemReNameModel";
            this.bItemReNameModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemReNameModel_ItemClick);
            // 
            // bItemDeleteModel
            // 
            this.bItemDeleteModel.Caption = "删除";
            this.bItemDeleteModel.Id = 25;
            this.bItemDeleteModel.Name = "bItemDeleteModel";
            this.bItemDeleteModel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemDeleteModel_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "截取缩略图";
            this.barButtonItem1.Id = 26;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // treeModelInfo
            // 
            this.treeModelInfo.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeModelInfo.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeModelInfo.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeModelInfo.Appearance.GroupButton.BackColor = System.Drawing.Color.Lime;
            this.treeModelInfo.Appearance.GroupButton.Options.UseBackColor = true;
            this.treeModelInfo.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_ObjectId});
            this.treeModelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeModelInfo.Location = new System.Drawing.Point(0, 0);
            this.treeModelInfo.Name = "treeModelInfo";
            this.treeModelInfo.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.treeModelInfo.OptionsPrint.PrintPageHeader = false;
            this.treeModelInfo.OptionsView.ShowColumns = false;
            this.treeModelInfo.OptionsView.ShowHorzLines = false;
            this.treeModelInfo.OptionsView.ShowIndicator = false;
            this.treeModelInfo.Size = new System.Drawing.Size(124, 623);
            this.treeModelInfo.TabIndex = 4;
            this.treeModelInfo.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeModelInfo_FocusedNodeChanged);
            this.treeModelInfo.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeModelInfo_CellValueChanging);
            this.treeModelInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeModelInfo_MouseUp);
            // 
            // tl_Name
            // 
            this.tl_Name.Caption = "名称";
            this.tl_Name.FieldName = "Name";
            this.tl_Name.Name = "tl_Name";
            this.tl_Name.OptionsColumn.AllowEdit = false;
            this.tl_Name.Visible = true;
            this.tl_Name.VisibleIndex = 0;
            // 
            // tl_ObjectId
            // 
            this.tl_ObjectId.Caption = "ObjectId";
            this.tl_ObjectId.FieldName = "ObjectId";
            this.tl_ObjectId.Name = "tl_ObjectId";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeModelInfo);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.MinSize = 300;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(533, 623);
            this.splitContainerControl1.SplitterPosition = 124;
            this.splitContainerControl1.TabIndex = 14;
            this.splitContainerControl1.Text = "splitContainerControl1";
            this.splitContainerControl1.SplitGroupPanelCollapsed += new DevExpress.XtraEditors.SplitGroupPanelCollapsedEventHandler(this.splitContainerControl1_SplitGroupPanelCollapsed);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Collapsed = true;
            this.splitContainerControl2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.AxRenderControl3D);
            this.splitContainerControl2.Panel2.MinSize = 300;
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(404, 623);
            this.splitContainerControl2.SplitterPosition = 203;
            this.splitContainerControl2.TabIndex = 10;
            this.splitContainerControl2.Text = "splitContainerControl2";
            this.splitContainerControl2.SplitGroupPanelCollapsed += new DevExpress.XtraEditors.SplitGroupPanelCollapsedEventHandler(this.splitContainerControl2_SplitGroupPanelCollapsed);
            // 
            // gridControl1
            // 
            this.gridControl1.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.layoutView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(404, 618);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1,
            this.gridView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.FieldValue.Options.UseTextOptions = true;
            this.layoutView1.Appearance.FieldValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.FieldValue.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutView1.Appearance.SelectionFrame.ForeColor = System.Drawing.Color.Red;
            this.layoutView1.Appearance.SelectionFrame.Options.UseBorderColor = true;
            this.layoutView1.Appearance.SelectionFrame.Options.UseForeColor = true;
            this.layoutView1.CardHorzInterval = 6;
            this.layoutView1.CardMinSize = new System.Drawing.Size(93, 81);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colName,
            this.colThumbnail,
            this.colComment,
            this.colModelInfo});
            this.layoutView1.GridControl = this.gridControl1;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1_1,
            this.layoutViewField_colComment});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.AllowExpandCollapse = false;
            this.layoutView1.OptionsBehavior.AllowPanCards = false;
            this.layoutView1.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.layoutView1.OptionsCustomization.AllowFilter = false;
            this.layoutView1.OptionsCustomization.AllowSort = false;
            this.layoutView1.OptionsHeaderPanel.EnableCarouselModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableColumnModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableMultiColumnModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableMultiRowModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnablePanButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableRowModeButton = false;
            this.layoutView1.OptionsHeaderPanel.EnableSingleModeButton = false;
            this.layoutView1.OptionsItemText.TextToControlDistance = 0;
            this.layoutView1.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
            this.layoutView1.OptionsSelection.MultiSelect = true;
            this.layoutView1.OptionsView.AllowHotTrackFields = false;
            this.layoutView1.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near;
            this.layoutView1.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutView1.OptionsView.ShowCardCaption = false;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowFieldHints = false;
            this.layoutView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.layoutView1_FocusedRowChanged);
            this.layoutView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.layoutView1_CellValueChanged);
            this.layoutView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.layoutView1_MouseUp);
            // 
            // colName
            // 
            this.colName.Caption = "名称";
            this.colName.ColumnEdit = this.repositoryItemTextEdit1;
            this.colName.FieldName = "Name";
            this.colName.LayoutViewField = this.layoutViewField_colName;
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // layoutViewField_colName
            // 
            this.layoutViewField_colName.EditorPreferredWidth = 89;
            this.layoutViewField_colName.Location = new System.Drawing.Point(0, 26);
            this.layoutViewField_colName.Name = "layoutViewField_colName";
            this.layoutViewField_colName.Size = new System.Drawing.Size(93, 20);
            this.layoutViewField_colName.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colName.TextToControlDistance = 0;
            this.layoutViewField_colName.TextVisible = false;
            // 
            // colThumbnail
            // 
            this.colThumbnail.Caption = "缩略图";
            this.colThumbnail.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colThumbnail.FieldName = "Thumbnail";
            this.colThumbnail.LayoutViewField = this.layoutViewField_colPicture;
            this.colThumbnail.Name = "colThumbnail";
            this.colThumbnail.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // layoutViewField_colPicture
            // 
            this.layoutViewField_colPicture.EditorPreferredWidth = 89;
            this.layoutViewField_colPicture.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPicture.Name = "layoutViewField_colPicture";
            this.layoutViewField_colPicture.Size = new System.Drawing.Size(93, 26);
            this.layoutViewField_colPicture.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colPicture.TextToControlDistance = 0;
            this.layoutViewField_colPicture.TextVisible = false;
            // 
            // colComment
            // 
            this.colComment.Caption = "备注";
            this.colComment.FieldName = "Comment";
            this.colComment.LayoutViewField = this.layoutViewField_colComment;
            this.colComment.Name = "colComment";
            this.colComment.OptionsColumn.AllowEdit = false;
            // 
            // layoutViewField_colComment
            // 
            this.layoutViewField_colComment.EditorPreferredWidth = 10;
            this.layoutViewField_colComment.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colComment.Name = "layoutViewField_colComment";
            this.layoutViewField_colComment.Size = new System.Drawing.Size(93, 81);
            this.layoutViewField_colComment.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_colComment.TextToControlDistance = 0;
            // 
            // colModelInfo
            // 
            this.colModelInfo.Caption = "模型信息";
            this.colModelInfo.FieldName = "ModelInfo";
            this.colModelInfo.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.colModelInfo.Name = "colModelInfo";
            this.colModelInfo.OptionsColumn.AllowEdit = false;
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(93, 81);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_layoutViewColumn1_1.TextToControlDistance = 0;
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.GroupBordersVisible = false;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colPicture,
            this.layoutViewField_colName});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // AxRenderControl3D
            // 
            this.AxRenderControl3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AxRenderControl3D.Enabled = true;
            this.AxRenderControl3D.Location = new System.Drawing.Point(0, 0);
            this.AxRenderControl3D.Name = "AxRenderControl3D";
            this.AxRenderControl3D.Size = new System.Drawing.Size(0, 0);
            this.AxRenderControl3D.TabIndex = 0;
            // 
            // popupMenuGroup
            // 
            this.popupMenuGroup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemReNameGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemDeleteGroup)});
            this.popupMenuGroup.Manager = this.barManager1;
            this.popupMenuGroup.Name = "popupMenuGroup";
            // 
            // popupMenuModel
            // 
            this.popupMenuModel.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemViewModel),
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemUpdateModel),
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemUpdatePic, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemReNameModel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemDeleteModel)});
            this.popupMenuModel.Manager = this.barManager1;
            this.popupMenuModel.Name = "popupMenuModel";
            // 
            // UCModelLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCModelLib";
            this.Size = new System.Drawing.Size(533, 647);
            this.Load += new System.EventHandler(this.UCModelLib_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeModelInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AxRenderControl3D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuModel)).EndInit();
            this.ResumeLayout(false);

        }

        private IDataSource _ds;
        private string _curGroupId;
        private int _curNum = 1;
        private Hashtable _groupNameTable;
        private string _oldGroupName;
        private DataTable _dtModelInfo;
        private bool isEditItemName;
        private string _oldModelName;
        private IResourceManager _resManager;
        public IResourceManager ResManager
        {
            get
            {
                if (this._resManager == null)
                {
                    if (this._ds == null)
                    {
                        return null;
                    }
                    IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                    if (fds == null) return null;
                    else
                    {
                        this._resManager = fds as IResourceManager;
                    }
                }
                return this._resManager;
            }
        }

        public UCModelLib()
        {
            this.InitializeComponent();
            this._dtModelInfo = new DataTable();
            this._dtModelInfo.Columns.Add("Name", typeof(string));
            this._dtModelInfo.Columns.Add("Thumbnail", typeof(object));
            this._dtModelInfo.Columns.Add("Comment", typeof(string));
            this._dtModelInfo.Columns.Add("ModelInfo", typeof(object));
            this.gridControl1.DataSource = this._dtModelInfo;
        }

        private bool Init3DControl()
        {
            try
            {
                IPropertySet ps = new PropertySet();
                ps.SetProperty("RenderSystem", gviRenderSystem.gviRenderOpenGL);
                if(!this.AxRenderControl3D.Initialize(true, ps)) return false;
                this.AxRenderControl3D.Camera.FlyTime = 0;
                string tmpSkyboxPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\Resource\Skybox");
                ISkyBox skybox = this.AxRenderControl3D.ObjectManager.GetSkyBox(0);
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, tmpSkyboxPath + "\\13_BK.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, tmpSkyboxPath + "\\13_DN.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, tmpSkyboxPath + "\\13_FR.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, tmpSkyboxPath + "\\13_LF.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, tmpSkyboxPath + "\\13_RT.jpg");
                skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, tmpSkyboxPath + "\\13_UP.jpg");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Init()
        {
            try
            {
                this._ds = DF3DPipeCreateApp.App.TemplateLib;
                if (this._ds == null) { this.Enabled = false; return; }

                this.treeModelInfo.Nodes.Clear();
                if (this._groupNameTable != null) this._groupNameTable.Clear();
                if (this._dtModelInfo != null) this._dtModelInfo.Clear();

                WaitForm.Start("正在加载数据...", "请稍后");

                Dictionary<string, string> modelGroup = GetModelGroup();
                this._groupNameTable = new Hashtable();
                if ((modelGroup != null) && (modelGroup.Count != 0))
                {
                    foreach (KeyValuePair<string, string> pair in modelGroup)
                    {
                        this._groupNameTable.Add(pair.Value, new HashSet<string>());
                        TreeListNode node = this.treeModelInfo.AppendNode(new object[] { pair.Value, pair.Key }, (TreeListNode)null);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private void splitContainerControl1_SplitGroupPanelCollapsed(object sender, SplitGroupPanelCollapsedEventArgs e)
        {
            bool collapsed = this.splitContainerControl1.Collapsed;
            if (collapsed)
            {
                this.btnHideGroup.Caption = "显示组";
                this.btnCreateGroup.Visibility = BarItemVisibility.Never;
            }
            else
            {
                this.btnHideGroup.Caption = "隐藏组";
                this.btnCreateGroup.Visibility = BarItemVisibility.Always;
            }

        }

        private void btnHideGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool collapsed = this.splitContainerControl1.Collapsed;
            this.splitContainerControl1.Collapsed = !collapsed;
            if (collapsed)
            {
                this.btnHideGroup.Caption = "隐藏组";
                this.btnCreateGroup.Visibility = BarItemVisibility.Always;

            }
            else
            {
                this.btnHideGroup.Caption = "显示组";
                this.btnHideGroup.Visibility = BarItemVisibility.Never;
            }
        }

        private void treeModelInfo_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            this.treeModelInfo.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
            this.treeModelInfo.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            this._dtModelInfo.Rows.Clear();
            TreeListNode node = e.Node;
            if (node == null || node.GetValue("ObjectId") == null)
            {
                this.btnExport.Enabled = false;
                this.btnImport.Enabled = false;
            }
            else
            {
                this.btnExport.Enabled = true;
                this.btnImport.Enabled = true;
                try
                {
                    this._curGroupId = node.GetValue("ObjectId").ToString();
                    List<ModelClass> groupModelClass = GetGroupModelClass(this._curGroupId);
                    if (groupModelClass != null)
                    {
                        DataRow row = null;
                        string groupname = node.GetValue("Name").ToString();
                        HashSet<string> itemNames = (HashSet<string>)this._groupNameTable[groupname];
                        foreach (ModelClass class2 in groupModelClass)
                        {
                            row = this._dtModelInfo.NewRow();
                            row["Name"] = class2.Name;
                            row["Thumbnail"] = class2.Thumbnail;
                            row["Comment"] = class2.Comment;
                            row["ModelInfo"] = class2;
                            this._dtModelInfo.Rows.Add(row);
                            itemNames.Add(class2.Name);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void treeModelInfo_MouseUp(object sender, MouseEventArgs e)
        {
            TreeListHitInfo treeListHitInfo = this.treeModelInfo.CalcHitInfo(e.Location);
            TreeListNode node = treeListHitInfo.Node;
            this.treeModelInfo.SetFocusedNode(node);
            if (node != null)
            {
                if ((e.Button == MouseButtons.Right) && (e.Clicks == 1))
                {
                    this.popupMenuGroup.ShowPopup(Cursor.Position);
                }
            }
            else
            {

            }

        }

        private void treeModelInfo_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            this.treeModelInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
            this.treeModelInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            string name = e.Value.ToString();
            if (this._groupNameTable.ContainsKey(name))
            {
                e.Node.SetValue(0, this._oldGroupName);
                XtraMessageBox.Show("模型组名称已经存在！", "提示");
            }
            else
            {
                if (!RenameModelGroup(this._curGroupId, name))
                {
                    e.Node.SetValue(0, this._oldGroupName);
                    XtraMessageBox.Show("模型组重命名写入数据库出错！", "提示");
                }
                else
                {
                    e.Node.SetValue(0, name);
                    HashSet<string> itemNames = (HashSet<string>)this._groupNameTable[this._oldGroupName];
                    this._groupNameTable.Remove(this._oldGroupName);
                    this._groupNameTable.Add(name, itemNames);
                    this._oldGroupName = name;
                }
            }
        }

        private Dictionary<string, string> GetModelGroup()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "GroupId = '-1'",
                    SubFields = "ObjectId,Name"
                };
                cursor = oc.Search(filter, true);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                while ((row = cursor.NextRow()) != null)
                {
                    if (!row.IsNull(0) && !row.IsNull(1))
                    {
                        dictionary.Add(row.GetValue(0).ToString(), row.GetValue(1).ToString());
                    }
                }
                return dictionary;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private string CreateModelGroup(string groupName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return null;

                string newVal = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                row.SetValue(row.FieldIndex("Name"), groupName);
                row.SetValue(row.FieldIndex("ObjectId"), newVal);
                row.SetValue(row.FieldIndex("GroupId"), "-1");
                cursor.InsertRow(row);
                return newVal;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private List<ModelClass> GetGroupModelClass(string groupId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "GroupId = '" + groupId + "'"
                };
                cursor = oc.Search(filter, true);
                List<ModelClass> list = new List<ModelClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    int id = -1;
                    string name = "", objectid = "", groupid = "", code = "", comment = "";
                    Image thumbnail = null;
                    int index = row.FieldIndex("oid");
                    if (index != -1 && !row.IsNull(index))
                    {
                        id = Convert.ToInt32(row.GetValue(index).ToString());
                    }
                    index = row.FieldIndex("Name");
                    if (index != -1 && !row.IsNull(index))
                    {
                        name = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("ObjectId");
                    if (index != -1 && !row.IsNull(index))
                    {
                        objectid = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("GroupId");
                    if (index != -1 && !row.IsNull(index))
                    {
                        groupid = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Code");
                    if (index != -1 && !row.IsNull(index))
                    {
                        code = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Comment");
                    if (index != -1 && !row.IsNull(index))
                    {
                        comment = row.GetValue(index).ToString();
                    }
                    index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            thumbnail = Image.FromStream(stream);
                        }
                    }
                    if (id != -1)
                    {
                        ModelClass mc = new ModelClass();
                        mc.Id = id; mc.Name = name; mc.Group = groupid;
                        mc.ObjectId = objectid; mc.Code = code; mc.Comment = comment;
                        mc.Thumbnail = thumbnail;
                        list.Add(mc);
                    }

                }
                return list;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private bool DeleteModelGroup(string groupId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null; 
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return false;
                IQueryFilter filter = new QueryFilter();
                int index = oc.GetFields().IndexOf("Code");
                if (index != -1)
                {
                    filter.WhereClause = string.Format("GroupId = '{0}'", groupId);
                    cursor = oc.Search(filter, true);
                    while ((row = cursor.NextRow()) != null)
                    {
                        if (!row.IsNull(index))
                        {
                            string code = row.GetValue(index).ToString();
                            if (this.ResManager != null && this.ResManager.ModelExist(code))
                            {
                                IModel m = this.ResManager.GetModel(code);
                                if (m != null)
                                {
                                    string[] imgstrs = m.GetImageNames();
                                    if (imgstrs != null)
                                    {
                                        foreach (string imgstr in imgstrs)
                                        {
                                            this.ResManager.DeleteImage(imgstr);
                                        }
                                    }
                                    this.ResManager.DeleteModel(code);
                                }
                            }
                        }
                    }
                }
                filter.WhereClause = string.Format("ObjectId = '{0}' or GroupId = '{0}'", groupId);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private bool RenameModelGroup(string groupId, string newName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", groupId),
                    SubFields = "oid,Name"
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(1, newName);
                    cursor.UpdateRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private void btnCreateGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string key = "新建模型组" + this._curNum;
                while (this._groupNameTable.ContainsKey(key))
                {
                    this._curNum++;
                    key = "新建模型组" + this._curNum;
                }
                string str = this.CreateModelGroup(key);
                if (string.IsNullOrEmpty(str))
                {
                    XtraMessageBox.Show("新建模型组出错！", "提示");
                }
                else
                {
                    TreeListNode node = this.treeModelInfo.AppendNode(new object[] { key, str }, (TreeListNode)null);
                    this.treeModelInfo.FocusedNode = node;
                    this._dtModelInfo.Rows.Clear();
                    this._groupNameTable.Add(key, new HashSet<string>());
                }
            }
            catch (Exception exception)
            {

            }
        }
       
        private void bItemReNameGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeModelInfo.FocusedNode != null)
            {
                this.treeModelInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = true;
                this.treeModelInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = true;
                this._oldGroupName = this.treeModelInfo.FocusedNode.GetValue("Name").ToString();
                this.treeModelInfo.ShowEditor();
            }

        }

        private void bItemDeleteGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeModelInfo.FocusedNode != null)
            {
                try
                {
                    if (DialogResult.OK == XtraMessageBox.Show("确定删除模型组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        if (!DeleteModelGroup(this._curGroupId))
                        {
                            XtraMessageBox.Show("模型组删除出错！", "提示");
                        }
                        else
                        {
                            this._dtModelInfo.Rows.Clear();
                            string groupname = this.treeModelInfo.FocusedNode.GetValue("Name").ToString();
                            if (this._groupNameTable[groupname] != null)
                            {
                                HashSet<string> modelNames = (HashSet<string>)this._groupNameTable[groupname];
                                modelNames.Clear();
                                this._groupNameTable.Remove(groupname);
                            }
                            this.treeModelInfo.DeleteNode(this.treeModelInfo.FocusedNode);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

        }

        private bool OpenOsgModel(string osgPath, out IModel fmodel, out IModel smodel, out IPropertySet images, out IMatrix mat)
        {
            fmodel = null;
            smodel = null;
            images = null;
            mat = null;
            IResourceFactory resFactory = new ResourceFactoryClass();
            if ((resFactory == null) || !File.Exists(osgPath))
            {
                return false;
            }
            Dictionary<string, string> dictionary = null;
            IDrawGroup group = null;
            IDrawPrimitive primitive = null;
            IPropertySet set = null;
            string str = "";
            IImage property = null;
            try
            {
                resFactory.CreateModelAndImageFromFileEx(osgPath, out images, out smodel, out fmodel, out mat);
                if ((images != null) && (images.Count > 0))
                {
                    set = new PropertySetClass();
                    dictionary = new Dictionary<string, string>();
                    foreach (string str2 in images.GetAllKeys())
                    {
                        property = images.GetProperty(str2) as IImage;
                        IImage temp = null;
                        string filePath = string.Format(string.Format(@"{0}\..\temp\{1}.png", Application.StartupPath, Guid.NewGuid().ToString()), new object[0]);
                        if (property.WriteFile(filePath))
                        {
                            temp = resFactory.CreateImageFromFile(filePath);
                        }
                        str = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant(); 
                        dictionary.Add(str2, str);
                        set.SetProperty(str, temp);
                        if(File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    images = set;
                } 
                if ((fmodel != null) && (fmodel.GroupCount > 0))
                {
                    for (int i = 0; i < fmodel.GroupCount; i++)
                    {
                        group = fmodel.GetGroup(i);
                        if (group != null)
                        {
                            if (!string.IsNullOrEmpty(group.CompleteMapTextureName) && dictionary.ContainsKey(group.CompleteMapTextureName))
                            {
                                group.CompleteMapTextureName = dictionary[group.CompleteMapTextureName];
                            }
                            if (!string.IsNullOrEmpty(group.LightMapTextureName) && dictionary.ContainsKey(group.LightMapTextureName))
                            {
                                group.LightMapTextureName = dictionary[group.LightMapTextureName];
                            }
                            if (group.PrimitiveCount > 0)
                            {
                                for (int j = 0; j < group.PrimitiveCount; j++)
                                {
                                    primitive = group.GetPrimitive(j);
                                    if (((primitive != null) && (primitive.Material != null)) && (!string.IsNullOrEmpty(primitive.Material.TextureName) && dictionary.ContainsKey(primitive.Material.TextureName)))
                                    {
                                        primitive.Material.TextureName = dictionary[primitive.Material.TextureName];
                                    }
                                }
                            }
                        }
                    }
                }
                if ((smodel != null) && (smodel.GroupCount > 0))
                {
                    for (int k = 0; k < smodel.GroupCount; k++)
                    {
                        group = smodel.GetGroup(k);
                        if (group != null)
                        {
                            if (!string.IsNullOrEmpty(group.CompleteMapTextureName) && dictionary.ContainsKey(group.CompleteMapTextureName))
                            {
                                group.CompleteMapTextureName = dictionary[group.CompleteMapTextureName];
                            }
                            if (!string.IsNullOrEmpty(group.LightMapTextureName) && dictionary.ContainsKey(group.LightMapTextureName))
                            {
                                group.LightMapTextureName = dictionary[group.LightMapTextureName];
                            }
                            if (group.PrimitiveCount > 0)
                            {
                                for (int m = 0; m < group.PrimitiveCount; m++)
                                {
                                    primitive = group.GetPrimitive(m);
                                    if (((primitive != null) && (primitive.Material != null)) && (!string.IsNullOrEmpty(primitive.Material.TextureName) && dictionary.ContainsKey(primitive.Material.TextureName)))
                                    {
                                        primitive.Material.TextureName = dictionary[primitive.Material.TextureName];
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private bool AppendModel(string modelname, string filePath, out Image thumbnail)
        {
            thumbnail = null;
            if (this.ResManager == null)
            {
                return false;
            }
            IImage property = null;
            IMatrix mat = null;
            IPropertySet images = null;
            IModel smodel = null;
            IModel fmodel = null;
            try
            {
                if (!this.OpenOsgModel(filePath, out fmodel, out smodel, out images, out mat))
                {
                    return false;
                }                
                if ((images != null) && (images.Count > 0))
                {
                    foreach (string str in images.GetAllKeys())
                    {
                        try
                        {
                            if (!this.ResManager.ImageExist(str))
                            {
                                property = images.GetProperty(str) as IImage;
                                this.ResManager.AddImage(str, property);
                            }
                        }
                        catch (Exception exception)
                        {
                        }
                    }
                }

                this.ResManager.AddModel(modelname, fmodel, smodel);
                string path = filePath.ToLower().Replace(".osg", ".png");
                if (File.Exists(path))
                {
                    thumbnail = Image.FromFile(path);
                }
                else
                {
                    path = filePath.ToLower().Replace(".osg", ".jpg");
                    if (File.Exists(path))
                    {
                        thumbnail = Image.FromFile(path);
                    }
                    else
                    {
                        path = Application.StartupPath + @"\..\Resource\Images\defaultstyle.jpg";
                        thumbnail = Image.FromFile(path);
                    }
                }
                return true;
            }
            catch (Exception exception2)
            {
                return false;
            }
        }

        private List<ModelClass> ImportModelClass(string groupid, string[] filenames)
        {
            if (filenames == null || filenames.Length == 0)
            {
                return null;
            }

            IFdeCursor cursor = null;
            IRowBuffer row = null;

            Image thumbnail = null;
            WaitDialogForm form = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return null;
                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                int length = filenames.Length;
                int num2 = 0;
                int num3 = 0;
                List<ModelClass> list = new List<ModelClass>();
                form = new WaitDialogForm("导入模型，未开始......", "请稍候");
                form.Show();
                foreach (string str2 in filenames)
                {
                    form.SetCaption(string.Format(@"({0}\{1}),【{2}】......", num2++, length, Path.GetFileName(str2)));
                    if (!File.Exists(str2))
                    {

                    }
                    else
                    {
                        string mcName = Path.GetFileNameWithoutExtension(str2);

                        ModelClass item = new ModelClass()
                        {
                            Name = mcName,
                            Group = groupid,
                            ObjectId = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant()
                        };
                        item.Code = item.ObjectId;
                        if (this.AppendModel(item.Code, str2, out thumbnail))
                        {
                            item.Thumbnail = thumbnail;
                        }
                        row.SetValue(row.FieldIndex("Name"), item.Name);
                        row.SetValue(row.FieldIndex("ObjectId"), item.ObjectId);
                        row.SetValue(row.FieldIndex("GroupId"), item.Group);
                        row.SetValue(row.FieldIndex("Code"), item.Code);
                        if (item.Thumbnail != null)
                        {
                            try
                            {
                                IBinaryBuffer newVal = new BinaryBufferClass();
                                MemoryStream stream = new MemoryStream();
                                item.Thumbnail.Save(stream, ImageFormat.Png);
                                newVal.FromByteArray(stream.ToArray());
                                row.SetValue(row.FieldIndex("Thumbnail"), newVal);
                            }
                            catch (Exception exception)
                            {
                            }
                        }
                        row.SetValue(row.FieldIndex("Comment"), item.Comment);
                        cursor.InsertRow(row);
                        item.Id = cursor.LastInsertId;
                        list.Add(item);
                        num3++;
                    }
                }
                form.Close();
                form = null;
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (form != null)
                {
                    form.Close();
                    form = null;
                }
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }

        private void btnImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "三维模型文件(.osg)|*.osg",
                    Multiselect = true,
                    RestoreDirectory = true
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] fileNames = dialog.FileNames;
                    if (fileNames != null)
                    {
                        string groupName = this.treeModelInfo.FocusedNode.GetValue("Name").ToString();
                        HashSet<string> modelNames = (HashSet<string>)this._groupNameTable[groupName];
                        ArrayList arrFiles = new ArrayList();
                        string strtemp = "已存在模型：";
                        if (modelNames != null)
                        {
                            foreach(string fileName in fileNames )
                            {
                                if (!modelNames.Contains(Path.GetFileNameWithoutExtension(fileName)))
                                    arrFiles.Add(fileName);
                                else strtemp += Path.GetFileNameWithoutExtension(fileName) + "、";
                            }
                            if (strtemp != "已存在模型：")
                            {
                                strtemp = strtemp.Substring(0, strtemp.Length - 1) + "。";
                                XtraMessageBox.Show(strtemp, "提示");
                            }
                        }
                        string groupid = this.treeModelInfo.FocusedNode.GetValue("ObjectId").ToString();
                        List<ModelClass> list = ImportModelClass(groupid, (string[])arrFiles.ToArray(typeof(string)));
                        if (list != null)
                        {
                            DataRow row = null;
                            foreach (ModelClass class2 in list)
                            {
                                row = this._dtModelInfo.NewRow();
                                row["Name"] = class2.Name;
                                row["Thumbnail"] = class2.Thumbnail;
                                row["Comment"] = class2.Comment;
                                row["ModelInfo"] = class2;
                                this._dtModelInfo.Rows.Add(row);
                                modelNames.Add(class2.Name);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void btnExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            int count = 0;
            int num2 = 0;
            int num3 = 0;
            string folderPath = "";
            ModelClass class2 = null;
            WaitDialogForm form = null;
            try
            {
                if (this._dtModelInfo.Rows.Count == 0)
                {
                    XtraMessageBox.Show("该组无模型！", "提示");
                }
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog
                    {
                        ShowNewFolderButton = true
                    };
                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        form = new WaitDialogForm("导出模型，未开始......", "请稍候");
                        form.Show();
                        count = this._dtModelInfo.Rows.Count;
                        folderPath = dialog.SelectedPath;
                        foreach (DataRow row in this._dtModelInfo.Rows)
                        {
                            form.SetCaption(string.Format(@"({0}\{1}),【{2}】......", num2++, count, row["Name"]));
                            if ((row["ModelInfo"] != null) && ((class2 = row["ModelInfo"] as ModelClass) != null))
                            {
                                string filePath = string.Format(@"{0}\{1}.osg", folderPath, class2.Name);
                                if (this.ResManager.WriteModelAndImageToFile(class2.Code, filePath)) num3++;
                            }
                        }
                        form.Close();
                        form = null;
                        XtraMessageBox.Show(string.Format("导出模型导出完成，总计：{0}，成功{1}！", count, num3), "提示");
                    }
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("导出模型出错！", "提示");
            }
            finally
            {
                if (form != null)
                {
                    form.Close();
                    form = null;
                }
            }
        }

        private bool ReplaceModel(ModelClass mc, string osgPath)
        {
            if (!File.Exists(osgPath))
            {
                return false;
            }
            Image thumbnail = null;
            try
            {
                this.ResManager.DeleteModel(mc.Code);
                if (!this.AppendModel(mc.Code, osgPath, out thumbnail))
                {
                    return false;
                }
                if (thumbnail != null)
                {
                    return this.ReplaceThumbnail(mc, thumbnail);
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private void bItemUpdateModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ModelClass class2 = null;
            DataRow row = null;
            try
            {
                int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                row = this._dtModelInfo.Rows[focusedRowHandle];
                if ((row["ModelInfo"] != null) && ((class2 = row["ModelInfo"] as ModelClass) != null))
                {
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Filter = "OpenSceneGraph(.osg)|*.osg"
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!this.ReplaceModel(class2, dialog.FileName))
                        {
                            XtraMessageBox.Show("替换模型出错！", "提示");
                        }
                        if (class2.Thumbnail != null)
                        {
                            row["Thumbnail"] = class2.Thumbnail;
                        }
                        XtraMessageBox.Show("替换模型成功！", "提示");
                    }
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("替换模型出错！", "提示");
            }
        }

        private bool ReplaceThumbnail(ModelClass mc, Image thumbnail)
        {
            if (thumbnail == null)
            {
                return false;
            }
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", mc.ObjectId),
                    SubFields = "oid,Thumbnail"
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    try
                    {
                        mc.Thumbnail = thumbnail;
                        IBinaryBuffer newVal = new BinaryBufferClass();
                        MemoryStream stream = new MemoryStream();
                        thumbnail.Save(stream, ImageFormat.Png);
                        newVal.FromByteArray(stream.ToArray());
                        row.SetValue(row.FieldIndex("Thumbnail"), newVal);
                    }
                    catch (Exception exception)
                    {
                    }
                    cursor.UpdateRow(row);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private void bItemUpdatePic_ItemClick(object sender, ItemClickEventArgs e)
        {
            ModelClass class2 = null;
            DataRow row = null;
            try
            {
                int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                row = this._dtModelInfo.Rows[focusedRowHandle];
                if ((row["ModelInfo"] != null) && ((class2 = row["ModelInfo"] as ModelClass) != null))
                {
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Filter = "模型缩略图(.JPG;PNG;BMP)|*.JPG;*.PNG;*.BMP",
                        RestoreDirectory  =true
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Image thumbnail = Image.FromFile(dialog.FileName);
                        if (!this.ReplaceThumbnail(class2, thumbnail))
                        {
                            XtraMessageBox.Show("替换模型缩略图出错！", "提示");
                        }
                        if (class2.Thumbnail != null)
                        {
                            row["Thumbnail"] = class2.Thumbnail;
                            this.layoutView1.UpdateCurrentRow();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("替换模型缩略图出错！", "提示");
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.splitContainerControl2.Collapsed = false;
            if (this._init3DControl)
            {
                ModelClass class2 = null;
                DataRow row = null;
                Image thumbnail = null;
                try
                {
                    int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                    row = this._dtModelInfo.Rows[focusedRowHandle];
                    if ((row["ModelInfo"] != null) && ((class2 = row["ModelInfo"] as ModelClass) != null))
                    {
                        thumbnail = this.GetShortCut();
                        if (thumbnail == null)
                        {
                            XtraMessageBox.Show("截取缩略图出错！", "提示");
                        }
                        else
                        {
                            if (!ReplaceThumbnail(class2,thumbnail))
                            {
                                XtraMessageBox.Show("替换模型缩略图出错！", "提示");
                            }
                            row["Thumbnail"] = class2.Thumbnail;
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

        }

        #region 模型预览
        private IRenderModelPoint _rmpt;
        private bool PreviewModel(string modelname, IFeatureDataSet dataset)
        {
            if (_rmpt != null)
            {
                this.AxRenderControl3D.ObjectManager.DeleteObject(this._rmpt.Guid);
                this._rmpt = null;
            }
            if (string.IsNullOrEmpty(modelname) || (dataset == null))
            {
                return false;
            }
            IResourceManager manager = null;
            IModelPointSymbol symbol = null;
            IModelPoint modelPoint = null;
            IModel model = null;
            double num = 10.0;
            try
            {
                manager = dataset as IResourceManager;
                if (!manager.ModelExist(modelname))
                {
                    return false;
                }
                model = manager.GetModel(modelname);
                modelPoint = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
                modelPoint.SetCoords(0.0, 0.0, 0.0, 0.0, 0);
                modelPoint.ModelEnvelope = model.Envelope;
                modelPoint.ModelName = modelname;
                num = Math.Sqrt((((modelPoint.Envelope.MaxX - modelPoint.Envelope.MinX) * (modelPoint.Envelope.MaxX - modelPoint.Envelope.MinX)) + ((modelPoint.Envelope.MaxX - modelPoint.Envelope.MinX) * (modelPoint.Envelope.MaxX - modelPoint.Envelope.MinX))) + ((modelPoint.Envelope.MaxX - modelPoint.Envelope.MinX) * (modelPoint.Envelope.MaxX - modelPoint.Envelope.MinX)));
                symbol = new ModelPointSymbolClass();
                symbol.SetResourceDataSet(dataset);
                _rmpt = this.AxRenderControl3D.ObjectManager.CreateRenderModelPoint(modelPoint, symbol, this.AxRenderControl3D.ProjectTree.RootID);
                _rmpt.VisibleMask = gviViewportMask.gviViewAllNormalView;
                IEulerAngle angle = new EulerAngleClass();
                angle.Set(45.0, -45.0, 0.0);
                this.AxRenderControl3D.Camera.LookAt(modelPoint.Position, num, angle);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        #region 截取缩略图
        private Image GetShortCut()
        {
            Image image = null;
            Image image2 = null;
            Image image3;
            string filePath = "";
            Graphics graphics = null;
            int width = 0x100;
            int height = 0x100;
            try
            {
                filePath = string.Format(string.Format(@"{0}\..\temp\{1}.png", Application.StartupPath, Guid.NewGuid().ToString()), new object[0]);
                if (this.AxRenderControl3D.ExportManager.ExportImage(filePath, (uint)width, (uint)height, false))
                {
                    goto Label_0086;
                }
                return null;
            Label_006D:
                Thread.Sleep(500);
            Label_0086:
                if (!File.Exists(filePath))
                {
                    goto Label_006D;
                }
                image = Image.FromFile(filePath);
                image2 = new Bitmap(width, height);
                graphics = Graphics.FromImage(image2);
                graphics.DrawImage(image, 0f, 0f, (float)width, (float)height);
                image3 = image2;
            }
            catch (Exception exception)
            {
                image3 = null;
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                    graphics = null;
                }
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            return image3;
        }
        #endregion

        private void bItemViewModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.splitContainerControl2.Collapsed = false;
            if (this._init3DControl)
            {
                ModelClass class2 = null;
                DataRow row = null;
                IFeatureDataSet dataset = this.ResManager as IFeatureDataSet;
                Image thumbnail = null;
                try
                {
                    int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                    row = this._dtModelInfo.Rows[focusedRowHandle];
                    if ((row["ModelInfo"] != null) && ((class2 = row["ModelInfo"] as ModelClass) != null))
                    {
                        if (dataset == null)
                        {
                            XtraMessageBox.Show("预览模型出错,获取模型文件出错！");
                        }
                        else
                        {
                            if (!PreviewModel(class2.Code, dataset))
                            {
                                XtraMessageBox.Show("预览模型出错！");
                            }
                            if (XtraMessageBox.Show("是否截取预览图作为模型缩略图？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
                            {
                                thumbnail = this.GetShortCut();
                                if (thumbnail == null)
                                {
                                    XtraMessageBox.Show("截取缩略图出错！", "提示");
                                }
                                else
                                {
                                    if (!ReplaceThumbnail(class2, thumbnail))
                                    {
                                        XtraMessageBox.Show("替换模型缩略图出错！", "提示");
                                    }
                                    row["Thumbnail"] = class2.Thumbnail;
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private bool RenameModel(ModelClass mc, string newName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", mc.ObjectId),
                    SubFields = "oid,Name"
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(1, newName);
                    cursor.UpdateRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private bool DeleteModel(ModelClass mc)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("ObjectId = '{0}'", mc.ObjectId);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void bItemReNameModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.layoutView1.Columns["Name"].OptionsColumn.AllowEdit = true;
            this.layoutView1.Columns["Name"].OptionsColumn.AllowFocus = true;
            this.layoutView1.FocusedColumn = this.layoutView1.Columns["Name"];
            this.isEditItemName = true;
            DataRow dr = this._dtModelInfo.Rows[this.layoutView1.FocusedRowHandle];
            if (dr == null) return;
            this._oldModelName = dr["Name"].ToString();
            this.layoutView1.ShowEditor();

        }

        private void bItemDeleteModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            int focusedRowHandle = this.layoutView1.FocusedRowHandle;
            DataRow row = this._dtModelInfo.Rows[focusedRowHandle];
            if (((row["ModelInfo"] != null) && ((row["ModelInfo"] as ModelClass) != null)) && (DialogResult.OK == XtraMessageBox.Show("确定删除该模型吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)))
            {
                ModelClass mc = row["ModelInfo"] as ModelClass;
                string code = mc.Code;
                if (!DeleteModel(mc))
                {
                    XtraMessageBox.Show("删除模型出错！", "提示");
                }
                else
                {
                    try
                    {
                        if (this.ResManager != null && this.ResManager.ModelExist(code))
                        {
                            IModel m = this.ResManager.GetModel(code);
                            if (m != null)
                            {
                                string[] imgstrs = m.GetImageNames();
                                if (imgstrs != null)
                                {
                                    foreach (string imgstr in imgstrs)
                                    {
                                        this.ResManager.DeleteImage(imgstr);
                                    }
                                }
                                this.ResManager.DeleteModel(code);
                            }
                        }
                        string name = row["Name"].ToString();
                        this._dtModelInfo.Rows.RemoveAt(focusedRowHandle);
                        HashSet<string> modelNames = (HashSet<string>)this._groupNameTable[this.treeModelInfo.FocusedNode.GetValue("Name").ToString()];
                        if (modelNames != null) modelNames.Remove(name);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void layoutView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.isEditItemName)
            {
                this.layoutView1.Columns["Name"].OptionsColumn.AllowEdit = false;
                this.layoutView1.Columns["Name"].OptionsColumn.AllowFocus = false;
                this.isEditItemName = true;
                int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                DataRow row = this._dtModelInfo.Rows[focusedRowHandle];
                string modelName = e.Value.ToString();
                HashSet<string> modelNames = (HashSet<string>)this._groupNameTable[this.treeModelInfo.FocusedNode.GetValue("Name").ToString()];
                if (modelNames.Contains(modelName))
                {
                    row["Name"] = this._oldModelName;
                    XtraMessageBox.Show("模型名称已存在！", "提示");
                }
                else
                {
                    if (this.RenameModel(row["ModelInfo"] as ModelClass, modelName))
                    {
                        row["Name"] = modelName;
                        modelNames.Remove(this._oldModelName);
                        modelNames.Add(modelName);
                        this._oldModelName = modelName;
                    }
                    else
                    {
                        row["Name"] = this._oldModelName;
                        XtraMessageBox.Show("模型重命名出错！", "提示");
                    }

                }
            }   
        }

        private void layoutView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (this.isEditItemName)
            {
                this.layoutView1.Columns["Name"].OptionsColumn.AllowEdit = false;
                this.layoutView1.Columns["Name"].OptionsColumn.AllowFocus = false;
                this.isEditItemName = false;
            }
        }

        private void layoutView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (((e != null) && (this.layoutView1.CalcHitInfo(e.X, e.Y).RowHandle >= 0)) && ((e.Button == MouseButtons.Right) && (e.Clicks == 1)))
            {
                this.popupMenuModel.ShowPopup(Cursor.Position);
            }
        }

        private void splitContainerControl2_SplitGroupPanelCollapsed(object sender, SplitGroupPanelCollapsedEventArgs e)
        {
            bool collapsed = this.splitContainerControl2.Collapsed;
        }
        private bool _init3DControl;
        private void UCModelLib_Load(object sender, EventArgs e)
        {
            this._init3DControl = Init3DControl();
        }

    }
}
