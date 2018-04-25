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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using System.IO;
using System.Collections;
using DF3DPipeCreateTool.Frm;
using DFDataConfig.Class;
using DFWinForms.Class;
namespace DF3DPipeCreateTool.UC
{
    public class UCColorLib : XtraUserControl
    {
        // Fields
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarButtonItem bBtnDelete;
        private BarButtonItem bBtnEdit;
        private BarButtonItem bBtnReName;
        private BarButtonItem bItemDeleteGroup;
        private BarButtonItem bItemReNameGroup;
        private LayoutViewColumn colColorInfo;
        private LayoutViewColumn colComment;
        private LayoutViewColumn colName;
        private LayoutViewColumn colThumbnail;
        private IContainer components;
        private GridControl gridControl1;
        private LayoutView layoutView1;
        private LayoutViewCard layoutViewCard1;
        private LayoutViewField layoutViewField_colComment;
        private LayoutViewField layoutViewField_colName;
        private LayoutViewField layoutViewField_colPicture;
        private LayoutViewField layoutViewField_layoutViewColumn1_1;
        private PopupMenu popupMenuColor;
        private PopupMenu popupMenuGroup;
        private RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private RepositoryItemTextEdit repositoryItemTextEdit1;
        private SplitContainerControl splitContainerControl1;
        private TreeListColumn tl_Name;
        private ImageCollection imageCollection1;
        private Bar bar1;
        private BarButtonItem btnHideGroup;
        private BarButtonItem btnCreateGroup;
        private BarButtonItem btnCreate;
        private TreeListColumn tl_ObjectId;
        private TreeList treeColorInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCColorLib));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnHideGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnCreateGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnCreate = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.bBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bBtnReName = new DevExpress.XtraBars.BarButtonItem();
            this.bBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bItemReNameGroup = new DevExpress.XtraBars.BarButtonItem();
            this.bItemDeleteGroup = new DevExpress.XtraBars.BarButtonItem();
            this.treeColorInfo = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_ObjectId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.colName = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutViewField_colName = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colThumbnail = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewField_colPicture = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colColorInfo = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colComment = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colComment = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.popupMenuGroup = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuColor = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeColorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuColor)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bBtnEdit,
            this.bBtnReName,
            this.bBtnDelete,
            this.bItemReNameGroup,
            this.bItemDeleteGroup,
            this.btnHideGroup,
            this.btnCreateGroup,
            this.btnCreate});
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 30;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHideGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCreateGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCreate)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Custom 2";
            // 
            // btnHideGroup
            // 
            this.btnHideGroup.Caption = "显示隐藏组";
            this.btnHideGroup.Id = 27;
            this.btnHideGroup.ImageIndex = 0;
            this.btnHideGroup.Name = "btnHideGroup";
            this.btnHideGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHideGroup_ItemClick);
            // 
            // btnCreateGroup
            // 
            this.btnCreateGroup.Caption = "创建组";
            this.btnCreateGroup.Id = 28;
            this.btnCreateGroup.ImageIndex = 1;
            this.btnCreateGroup.Name = "btnCreateGroup";
            this.btnCreateGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCreateGroup_ItemClick);
            // 
            // btnCreate
            // 
            this.btnCreate.Caption = "创建";
            this.btnCreate.Id = 29;
            this.btnCreate.ImageIndex = 2;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCreate_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(250, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(250, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 576);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(250, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 576);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "btnShowGroup.Glyph.png");
            this.imageCollection1.Images.SetKeyName(1, "btnNewGroup.Glyph.png");
            this.imageCollection1.Images.SetKeyName(2, "btnNewLocation.Glyph.png");
            // 
            // bBtnEdit
            // 
            this.bBtnEdit.Caption = "编辑";
            this.bBtnEdit.Id = 17;
            this.bBtnEdit.Name = "bBtnEdit";
            this.bBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bBtnEdit_ItemClick);
            // 
            // bBtnReName
            // 
            this.bBtnReName.Caption = "重命名";
            this.bBtnReName.Id = 18;
            this.bBtnReName.Name = "bBtnReName";
            this.bBtnReName.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bBtnReName_ItemClick);
            // 
            // bBtnDelete
            // 
            this.bBtnDelete.Caption = "删除";
            this.bBtnDelete.Id = 19;
            this.bBtnDelete.Name = "bBtnDelete";
            this.bBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bBtnDelete_ItemClick);
            // 
            // bItemReNameGroup
            // 
            this.bItemReNameGroup.Caption = "重命名";
            this.bItemReNameGroup.Id = 22;
            this.bItemReNameGroup.Name = "bItemReNameGroup";
            this.bItemReNameGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemReNameGroup_ItemClick);
            // 
            // bItemDeleteGroup
            // 
            this.bItemDeleteGroup.Caption = "删除";
            this.bItemDeleteGroup.Id = 23;
            this.bItemDeleteGroup.Name = "bItemDeleteGroup";
            this.bItemDeleteGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemDeleteGroup_ItemClick);
            // 
            // treeColorInfo
            // 
            this.treeColorInfo.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeColorInfo.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeColorInfo.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeColorInfo.Appearance.GroupButton.BackColor = System.Drawing.Color.Lime;
            this.treeColorInfo.Appearance.GroupButton.Options.UseBackColor = true;
            this.treeColorInfo.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_ObjectId});
            this.treeColorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeColorInfo.Location = new System.Drawing.Point(0, 0);
            this.treeColorInfo.Name = "treeColorInfo";
            this.treeColorInfo.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.treeColorInfo.OptionsPrint.PrintPageHeader = false;
            this.treeColorInfo.OptionsView.ShowColumns = false;
            this.treeColorInfo.OptionsView.ShowHorzLines = false;
            this.treeColorInfo.OptionsView.ShowIndicator = false;
            this.treeColorInfo.Size = new System.Drawing.Size(124, 576);
            this.treeColorInfo.TabIndex = 4;
            this.treeColorInfo.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeColorInfo_FocusedNodeChanged);
            this.treeColorInfo.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeColorInfo_CellValueChanged);
            this.treeColorInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeColorInfo_MouseUp);
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
            this.gridControl1.Size = new System.Drawing.Size(121, 576);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
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
            this.layoutView1.CardMinSize = new System.Drawing.Size(95, 84);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colName,
            this.colThumbnail,
            this.colColorInfo,
            this.colComment});
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
            this.layoutViewField_colName.EditorPreferredWidth = 91;
            this.layoutViewField_colName.Location = new System.Drawing.Point(0, 26);
            this.layoutViewField_colName.Name = "layoutViewField_colName";
            this.layoutViewField_colName.Size = new System.Drawing.Size(95, 20);
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
            this.layoutViewField_colPicture.EditorPreferredWidth = 91;
            this.layoutViewField_colPicture.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPicture.Name = "layoutViewField_colPicture";
            this.layoutViewField_colPicture.Size = new System.Drawing.Size(95, 26);
            this.layoutViewField_colPicture.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colPicture.TextToControlDistance = 0;
            this.layoutViewField_colPicture.TextVisible = false;
            // 
            // colColorInfo
            // 
            this.colColorInfo.Caption = "颜色信息";
            this.colColorInfo.FieldName = "ColorInfo";
            this.colColorInfo.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.colColorInfo.Name = "colColorInfo";
            this.colColorInfo.OptionsColumn.AllowEdit = false;
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(106, 46);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_layoutViewColumn1_1.TextToControlDistance = 0;
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
            this.layoutViewField_colComment.Size = new System.Drawing.Size(106, 46);
            this.layoutViewField_colComment.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_colComment.TextToControlDistance = 0;
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
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeColorInfo);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(250, 576);
            this.splitContainerControl1.SplitterPosition = 124;
            this.splitContainerControl1.TabIndex = 14;
            this.splitContainerControl1.Text = "splitContainerControl1";
            this.splitContainerControl1.SplitGroupPanelCollapsed += new DevExpress.XtraEditors.SplitGroupPanelCollapsedEventHandler(this.splitContainerControl1_SplitGroupPanelCollapsed);
            // 
            // popupMenuGroup
            // 
            this.popupMenuGroup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemReNameGroup),
            new DevExpress.XtraBars.LinkPersistInfo(this.bItemDeleteGroup)});
            this.popupMenuGroup.Manager = this.barManager1;
            this.popupMenuGroup.Name = "popupMenuGroup";
            // 
            // popupMenuColor
            // 
            this.popupMenuColor.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bBtnReName),
            new DevExpress.XtraBars.LinkPersistInfo(this.bBtnDelete)});
            this.popupMenuColor.Manager = this.barManager1;
            this.popupMenuColor.Name = "popupMenuColor";
            // 
            // UCColorLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCColorLib";
            this.Size = new System.Drawing.Size(250, 600);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeColorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuColor)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dtColorInfo;
        private IDataSource _ds;
        private string _curGroupId;
        private int _curNum = 1;
        private Hashtable _groupNameTable;
        private string _oldGroupName;
        private bool isEditItemName;
        private string _oldColorName;

        public UCColorLib()
        {
            this.InitializeComponent();
            this._dtColorInfo = new DataTable();
            this._dtColorInfo.Columns.Add("Name", typeof(string));
            this._dtColorInfo.Columns.Add("Thumbnail", typeof(object));
            this._dtColorInfo.Columns.Add("Comment", typeof(string));
            this._dtColorInfo.Columns.Add("ColorInfo", typeof(object));
            this.gridControl1.DataSource = this._dtColorInfo;
        }

        public void Init()
        {
            try
            {
                this._ds = DF3DPipeCreateApp.App.TemplateLib;
                if (this._ds == null) { this.Enabled = false; return; }
                
                this.treeColorInfo.Nodes.Clear();
                if(this._groupNameTable != null)this._groupNameTable.Clear();
                if (this._dtColorInfo != null) this._dtColorInfo.Clear();
                WaitForm.Start("正在加载数据...", "请稍后");

                Dictionary<string, string> colorGroup = GetColorGroup();
                this._groupNameTable = new Hashtable();
                if ((colorGroup != null) && (colorGroup.Count != 0))
                {
                    foreach (KeyValuePair<string, string> pair in colorGroup)
                    {
                        this._groupNameTable.Add(pair.Value, new HashSet<string>());
                        TreeListNode node = this.treeColorInfo.AppendNode(new object[] { pair.Value, pair.Key }, (TreeListNode)null);
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
                this.btnCreateGroup.Visibility = BarItemVisibility.Never;
            }
        }

        private void treeColorInfo_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            this.treeColorInfo.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
            this.treeColorInfo.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            this._dtColorInfo.Rows.Clear();
            TreeListNode node = e.Node;
            if (node == null || node.GetValue("ObjectId") == null)
            {
                this.btnCreate.Enabled = false;
            }
            else
            {
                this.btnCreate.Enabled = true;
                try
                {
                    this._curGroupId = node.GetValue("ObjectId").ToString();
                    List<ColorClass> groupColorClass = GetGroupColorClass(this._curGroupId);
                    if (groupColorClass != null)
                    {
                        DataRow row = null;
                        string groupname = node.GetValue("Name").ToString();
                        HashSet<string> itemNames = (HashSet<string>)this._groupNameTable[groupname];
                        foreach (ColorClass class2 in groupColorClass)
                        {
                            row = this._dtColorInfo.NewRow();
                            row["Name"] = class2.Name;
                            row["Thumbnail"] = class2.Thumbnail;
                            row["Comment"] = class2.Comment;
                            row["ColorInfo"] = class2;
                            this._dtColorInfo.Rows.Add(row);
                            itemNames.Add(class2.Name);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void treeColorInfo_MouseUp(object sender, MouseEventArgs e)
        {
            TreeListHitInfo treeListHitInfo = this.treeColorInfo.CalcHitInfo(e.Location);
            TreeListNode node = treeListHitInfo.Node;
            this.treeColorInfo.SetFocusedNode(node);
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

        private void treeColorInfo_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            this.treeColorInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
            this.treeColorInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            string name = e.Value.ToString();
            if (this._groupNameTable.ContainsKey(name))
            {
                e.Node.SetValue(0, this._oldGroupName);
                XtraMessageBox.Show("颜色组名称已经存在！", "提示");
            }
            else
            {
                if (!RenameColorGroup(this._curGroupId, name))
                {
                    e.Node.SetValue(0, this._oldGroupName);
                    XtraMessageBox.Show("颜色组重命名写入数据库出错！", "提示");
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

        private Dictionary<string, string> GetColorGroup()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
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
        private string CreateColorGroup(string groupName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
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
        private List<ColorClass> GetGroupColorClass(string groupId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "GroupId = '" + groupId + "'"
                };
                cursor = oc.Search(filter, true);
                List<ColorClass> list = new List<ColorClass>();
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
                        ColorClass cc = new ColorClass();
                        cc.Id = id; cc.Name = name; cc.Group = groupid;
                        cc.ObjectId = objectid; cc.Code = code; cc.Comment = comment;
                        cc.Thumbnail = thumbnail;
                        list.Add(cc);
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
        private bool DeleteColorGroup(string groupId)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("ObjectId = '{0}' or GroupId = '{0}'", groupId);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool RenameColorGroup(string groupId, string newName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
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
                string key = "新建颜色组" + this._curNum;
                while (this._groupNameTable.ContainsKey(key))
                {
                    this._curNum++;
                    key = "新建颜色组" + this._curNum;
                }
                string str = this.CreateColorGroup(key);
                if (string.IsNullOrEmpty(str))
                {
                    XtraMessageBox.Show("新建颜色组出错！", "提示");
                }
                else
                {
                    TreeListNode node = this.treeColorInfo.AppendNode(new object[] { key, str }, (TreeListNode)null);
                    this.treeColorInfo.FocusedNode = node;
                    this._dtColorInfo.Rows.Clear();
                    this._groupNameTable.Add(key, new HashSet<string>());
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void bItemReNameGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeColorInfo.FocusedNode != null)
            {
                this.treeColorInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = true;
                this.treeColorInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = true;
                this._oldGroupName = this.treeColorInfo.FocusedNode.GetValue("Name").ToString();
                this.treeColorInfo.ShowEditor();
            }
        }

        private void bItemDeleteGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeColorInfo.FocusedNode != null)
            {
                try
                {
                    if (DialogResult.OK == XtraMessageBox.Show("确定删除颜色组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        if (!DeleteColorGroup(this._curGroupId))
                        {
                            XtraMessageBox.Show("颜色组删除出错！", "提示");
                        }
                        else
                        {
                            this._dtColorInfo.Rows.Clear();
                            string groupname = this.treeColorInfo.FocusedNode.GetValue("Name").ToString();
                            if (this._groupNameTable[groupname] != null)
                            {
                                HashSet<string> colorNames = (HashSet<string>)this._groupNameTable[groupname];
                                colorNames.Clear();
                                this._groupNameTable.Remove(groupname);
                            }
                            this.treeColorInfo.DeleteNode(this.treeColorInfo.FocusedNode);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private bool RenameColor(ColorClass cc, string newName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", cc.ObjectId),
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
        private bool DeleteColor(ColorClass cc)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("ObjectId = '{0}'", cc.ObjectId);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private void btnCreate_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string groupName = this.treeColorInfo.FocusedNode.GetValue("Name").ToString();
                HashSet<string> colorNames = (HashSet<string>)this._groupNameTable[groupName];
                FrmEditColor dlg = new FrmEditColor(this._ds, this._curGroupId, colorNames);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataRow row = this._dtColorInfo.NewRow();
                    row["Name"] = dlg.Color.Name;
                    row["Thumbnail"] = dlg.Color.Thumbnail;
                    row["Comment"] = dlg.Color.Comment;
                    row["ColorInfo"] = dlg.Color;
                    this._dtColorInfo.Rows.Add(row);
                    colorNames.Add(dlg.Color.Name);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void bBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            ColorClass class2 = null;
            DataRow row = null;
            try
            {
                int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                row = this.layoutView1.GetDataRow(focusedRowHandle);
                if ((row["ColorInfo"] != null) && ((class2 = row["ColorInfo"] as ColorClass) != null))
                {
                    string groupName = this.treeColorInfo.FocusedNode.GetValue("Name").ToString();
                    HashSet<string> colorNames = (HashSet<string>)this._groupNameTable[groupName];
                    FrmEditColor dlg = new FrmEditColor(this._ds, class2, colorNames);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        row["Thumbnail"] = dlg.Color.Thumbnail;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("编辑颜色出错！", "提示");
            }
        }

        private void bBtnReName_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.layoutView1.Columns["Name"].OptionsColumn.AllowEdit = true;
            this.layoutView1.Columns["Name"].OptionsColumn.AllowFocus = true;
            this.layoutView1.FocusedColumn = this.layoutView1.Columns["Name"];
            this.isEditItemName = true;
            DataRow dr = this._dtColorInfo.Rows[this.layoutView1.FocusedRowHandle];
            if (dr == null) return;
            this._oldColorName = dr["Name"].ToString();
            this.layoutView1.ShowEditor();
        }

        private void bBtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int focusedRowHandle = this.layoutView1.FocusedRowHandle;
            DataRow row = this._dtColorInfo.Rows[focusedRowHandle];
            if (((row["ColorInfo"] != null) && ((row["ColorInfo"] as ColorClass) != null)) && (DialogResult.OK == XtraMessageBox.Show("确定删除该颜色吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)))
            {
                if (!DeleteColor(row["ColorInfo"] as ColorClass))
                {
                    XtraMessageBox.Show("删除颜色出错！");
                }
                else
                {
                    string name = row["Name"].ToString();
                    this._dtColorInfo.Rows.RemoveAt(focusedRowHandle);
                    HashSet<string> colorNames = (HashSet<string>)this._groupNameTable[this.treeColorInfo.FocusedNode.GetValue("Name").ToString()];
                    if (colorNames != null) colorNames.Remove(name);
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
                this.popupMenuColor.ShowPopup(Cursor.Position);
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
                DataRow row = this._dtColorInfo.Rows[focusedRowHandle];
                string colorName = e.Value.ToString();
                HashSet<string> colorNames = (HashSet<string>)this._groupNameTable[this.treeColorInfo.FocusedNode.GetValue("Name").ToString()];
                if (colorNames.Contains(colorName))
                {
                    row["Name"] = this._oldColorName;
                    XtraMessageBox.Show("颜色名称已存在！", "提示");
                }
                else
                {
                    if (this.RenameColor(row["ColorInfo"] as ColorClass, colorName))
                    {
                        row["Name"] = colorName;
                        colorNames.Remove(this._oldColorName);
                        colorNames.Add(colorName);
                        this._oldColorName = colorName;
                    }
                    else
                    {
                        row["Name"] = this._oldColorName;
                        XtraMessageBox.Show("颜色重命名出错！", "提示");
                    }

                }
            }   
        }
    }
}
