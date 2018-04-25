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
using System.Collections;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using System.IO;
using System.Drawing.Imaging;
using Gvitech.CityMaker.Resource;
using DFDataConfig.Class;
using DFWinForms.Class;
namespace DF3DPipeCreateTool.UC
{
    public class UCTextureLib : XtraUserControl
    {
        private Bar bar2;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarButtonItem bBtnDelete;
        private BarButtonItem bBtnReName;
        private BarButtonItem bBtnReplace;
        private BarButtonItem bItemDeleteGroup;
        private BarButtonItem bItemReNameGroup;
        private BarButtonItem btnCreateGroup;
        private BarButtonItem btnExport;
        private BarButtonItem btnHideGroup;
        private BarButtonItem btnImport;
        private LayoutViewColumn colComment;
        private LayoutViewColumn colName;
        private LayoutViewColumn colTextureInfo;
        private LayoutViewColumn colThumbnail;
        private IContainer components;
        private GridControl gridControl1;
        private LayoutView layoutView1;
        private LayoutViewCard layoutViewCard1;
        private LayoutViewField layoutViewField_colComment;
        private LayoutViewField layoutViewField_colName;
        private LayoutViewField layoutViewField_colPicture;
        private LayoutViewField layoutViewField_layoutViewColumn1_1;
        private PopupMenu popupMenuGroup;
        private PopupMenu popupMenuTexture;
        private RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private RepositoryItemTextEdit repositoryItemTextEdit1;
        private SplitContainerControl splitContainerControl1;
        private TreeListColumn tl_Name;
        private ImageCollection imageCollection1;
        private TreeListColumn tl_ObjectId;
        private TreeList treeTextureInfo;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTextureLib));
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
            this.bBtnReplace = new DevExpress.XtraBars.BarButtonItem();
            this.bBtnReName = new DevExpress.XtraBars.BarButtonItem();
            this.bBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.treeTextureInfo = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
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
            this.colTextureInfo = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.popupMenuGroup = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuTexture = new DevExpress.XtraBars.PopupMenu(this.components);
            this.tl_ObjectId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeTextureInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTexture)).BeginInit();
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
            this.bBtnReplace,
            this.bBtnReName,
            this.bBtnDelete});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 26;
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
            this.btnImport.Caption = "导入纹理";
            this.btnImport.Enabled = false;
            this.btnImport.Id = 17;
            this.btnImport.ImageIndex = 3;
            this.btnImport.Name = "btnImport";
            this.btnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImport_ItemClick);
            // 
            // btnExport
            // 
            this.btnExport.Caption = "导出纹理";
            this.btnExport.Enabled = false;
            this.btnExport.Id = 20;
            this.btnExport.ImageIndex = 4;
            this.btnExport.Name = "btnExport";
            this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(360, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(360, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(360, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 576);
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
            this.bItemReNameGroup.Id = 21;
            this.bItemReNameGroup.Name = "bItemReNameGroup";
            this.bItemReNameGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemReNameGroup_ItemClick);
            // 
            // bItemDeleteGroup
            // 
            this.bItemDeleteGroup.Caption = "删除";
            this.bItemDeleteGroup.Id = 22;
            this.bItemDeleteGroup.Name = "bItemDeleteGroup";
            this.bItemDeleteGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bItemDeleteGroup_ItemClick);
            // 
            // bBtnReplace
            // 
            this.bBtnReplace.Caption = "替换纹理";
            this.bBtnReplace.Id = 23;
            this.bBtnReplace.Name = "bBtnReplace";
            this.bBtnReplace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bBtnReplace_ItemClick);
            // 
            // bBtnReName
            // 
            this.bBtnReName.Caption = "重命名";
            this.bBtnReName.Id = 24;
            this.bBtnReName.Name = "bBtnReName";
            this.bBtnReName.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bBtnReName_ItemClick);
            // 
            // bBtnDelete
            // 
            this.bBtnDelete.Caption = "删除";
            this.bBtnDelete.Id = 25;
            this.bBtnDelete.Name = "bBtnDelete";
            this.bBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bBtnDelete_ItemClick);
            // 
            // treeTextureInfo
            // 
            this.treeTextureInfo.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeTextureInfo.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeTextureInfo.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeTextureInfo.Appearance.GroupButton.BackColor = System.Drawing.Color.Lime;
            this.treeTextureInfo.Appearance.GroupButton.Options.UseBackColor = true;
            this.treeTextureInfo.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_ObjectId});
            this.treeTextureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTextureInfo.Location = new System.Drawing.Point(0, 0);
            this.treeTextureInfo.Name = "treeTextureInfo";
            this.treeTextureInfo.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.treeTextureInfo.OptionsPrint.PrintPageHeader = false;
            this.treeTextureInfo.OptionsView.ShowColumns = false;
            this.treeTextureInfo.OptionsView.ShowHorzLines = false;
            this.treeTextureInfo.OptionsView.ShowIndicator = false;
            this.treeTextureInfo.Size = new System.Drawing.Size(124, 576);
            this.treeTextureInfo.TabIndex = 4;
            this.treeTextureInfo.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeTextureInfo_FocusedNodeChanged);
            this.treeTextureInfo.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeTextureInfo_CellValueChanged);
            this.treeTextureInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeTextureInfo_MouseUp);
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
            this.gridControl1.Size = new System.Drawing.Size(231, 576);
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
            this.layoutView1.CardMinSize = new System.Drawing.Size(92, 82);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colName,
            this.colThumbnail,
            this.colComment,
            this.colTextureInfo});
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
            this.layoutViewField_colName.EditorPreferredWidth = 88;
            this.layoutViewField_colName.Location = new System.Drawing.Point(0, 62);
            this.layoutViewField_colName.Name = "layoutViewField_colName";
            this.layoutViewField_colName.Size = new System.Drawing.Size(92, 20);
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
            this.layoutViewField_colPicture.EditorPreferredWidth = 88;
            this.layoutViewField_colPicture.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPicture.Name = "layoutViewField_colPicture";
            this.layoutViewField_colPicture.Size = new System.Drawing.Size(92, 62);
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
            this.layoutViewField_colComment.Size = new System.Drawing.Size(102, 46);
            this.layoutViewField_colComment.TextSize = new System.Drawing.Size(65, 20);
            this.layoutViewField_colComment.TextToControlDistance = 0;
            // 
            // colTextureInfo
            // 
            this.colTextureInfo.Caption = "纹理信息";
            this.colTextureInfo.FieldName = "TextureInfo";
            this.colTextureInfo.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.colTextureInfo.Name = "colTextureInfo";
            this.colTextureInfo.OptionsColumn.AllowEdit = false;
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(102, 46);
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
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeTextureInfo);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(360, 576);
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
            // popupMenuTexture
            // 
            this.popupMenuTexture.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bBtnReplace),
            new DevExpress.XtraBars.LinkPersistInfo(this.bBtnReName, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bBtnDelete)});
            this.popupMenuTexture.Manager = this.barManager1;
            this.popupMenuTexture.Name = "popupMenuTexture";
            // 
            // tl_ObjectId
            // 
            this.tl_ObjectId.Caption = "ObjectId";
            this.tl_ObjectId.FieldName = "ObjectId";
            this.tl_ObjectId.Name = "tl_ObjectId";
            // 
            // UCTextureLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCTextureLib";
            this.Size = new System.Drawing.Size(360, 600);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeTextureInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTexture)).EndInit();
            this.ResumeLayout(false);

        }
        private IDataSource _ds;
        private string _curGroupId;
        private int _curNum = 1;
        private Hashtable _groupNameTable;
        private string _oldGroupName;
        private DataTable _dtTextureInfo;
        private bool isEditItemName;
        private string _oldTextureName;
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

        public UCTextureLib()
        {
            this.InitializeComponent();
            this._dtTextureInfo = new DataTable();
            this._dtTextureInfo.Columns.Add("Name", typeof(string));
            this._dtTextureInfo.Columns.Add("Thumbnail", typeof(object));
            this._dtTextureInfo.Columns.Add("Comment", typeof(string));
            this._dtTextureInfo.Columns.Add("TextureInfo", typeof(object));
            this.gridControl1.DataSource = this._dtTextureInfo;
        }
        public void Init()
        {
            try
            {
                this._ds = DF3DPipeCreateApp.App.TemplateLib;
                if (this._ds == null) { this.Enabled = false; return; }

                this.treeTextureInfo.Nodes.Clear();
                if (this._groupNameTable != null) this._groupNameTable.Clear();
                if (this._dtTextureInfo != null) this._dtTextureInfo.Clear();
                WaitForm.Start("正在加载数据...", "请稍后");

                Dictionary<string, string> textureGroup = GetTextureGroup();
                this._groupNameTable = new Hashtable();
                if ((textureGroup != null) && (textureGroup.Count != 0))
                {
                    foreach (KeyValuePair<string, string> pair in textureGroup)
                    {
                        this._groupNameTable.Add(pair.Value, new HashSet<string>());
                        TreeListNode node = this.treeTextureInfo.AppendNode(new object[] { pair.Value, pair.Key }, (TreeListNode)null);
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
        
        private void treeTextureInfo_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            this.treeTextureInfo.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
            this.treeTextureInfo.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            this._dtTextureInfo.Rows.Clear();
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
                    List<TextureClass> groupTextureClass = GetGroupTextureClass(this._curGroupId);
                    if (groupTextureClass != null)
                    {
                        DataRow row = null;
                        string groupname = node.GetValue("Name").ToString();
                        HashSet<string> itemNames = (HashSet<string>)this._groupNameTable[groupname];
                        foreach (TextureClass class2 in groupTextureClass)
                        {
                            row = this._dtTextureInfo.NewRow();
                            row["Name"] = class2.Name;
                            row["Thumbnail"] = class2.Thumbnail;
                            row["Comment"] = class2.Comment;
                            row["TextureInfo"] = class2;
                            this._dtTextureInfo.Rows.Add(row);
                            itemNames.Add(class2.Name);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

        }

        private void treeTextureInfo_MouseUp(object sender, MouseEventArgs e)
        {
            TreeListHitInfo treeListHitInfo = this.treeTextureInfo.CalcHitInfo(e.Location);
            TreeListNode node = treeListHitInfo.Node;
            this.treeTextureInfo.SetFocusedNode(node);
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

        private void treeTextureInfo_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            this.treeTextureInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
            this.treeTextureInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            string name = e.Value.ToString();
            if (this._groupNameTable.ContainsKey(name))
            {
                e.Node.SetValue(0, this._oldGroupName);
                XtraMessageBox.Show("纹理组名称已经存在！", "提示");
            }
            else
            {
                if (!RenameTextureGroup(this._curGroupId, name))
                {
                    e.Node.SetValue(0, this._oldGroupName);
                    XtraMessageBox.Show("纹理组重命名写入数据库出错！", "提示");
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

        private Dictionary<string, string> GetTextureGroup()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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
        private string CreateTextureGroup(string groupName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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
        private List<TextureClass> GetGroupTextureClass(string groupId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "GroupId = '" + groupId + "'"
                };
                cursor = oc.Search(filter, true);
                List<TextureClass> list = new List<TextureClass>();
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
                        TextureClass mc = new TextureClass();
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
        private bool DeleteTextureGroup(string groupId)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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
                            if (this.ResManager != null && this.ResManager.ImageExist(code))
                            {
                                this.ResManager.DeleteImage(code);
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
        private bool RenameTextureGroup(string groupId, string newName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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
                string key = "新建纹理组" + this._curNum;
                while (this._groupNameTable.ContainsKey(key))
                {
                    this._curNum++;
                    key = "新建纹理组" + this._curNum;
                }
                string str = this.CreateTextureGroup(key);
                if (string.IsNullOrEmpty(str))
                {
                    XtraMessageBox.Show("新建纹理组出错！", "提示");
                }
                else
                {
                    TreeListNode node = this.treeTextureInfo.AppendNode(new object[] { key, str }, (TreeListNode)null);
                    this.treeTextureInfo.FocusedNode = node;
                    this._dtTextureInfo.Rows.Clear();
                    this._groupNameTable.Add(key, new HashSet<string>());
                }
            }
            catch (Exception exception)
            {

            }

        }

        private List<TextureClass> ImportTextureClass(string groupid, string[] filenames)
        {
            if (filenames == null || filenames.Length == 0)
            {
                return null;
            }

            IFdeCursor cursor = null;
            IRowBuffer row = null;

            WaitDialogForm form = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
                if (oc == null) return null;
                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                int length = filenames.Length;
                int num2 = 0;
                int num3 = 0;
                List<TextureClass> list = new List<TextureClass>();
                form = new WaitDialogForm("导入纹理，未开始......", "请稍候");
                form.Show();
                IResourceFactory resFactory = new ResourceFactory();
                foreach (string str2 in filenames)
                {
                    form.SetCaption(string.Format(@"({0}\{1}),【{2}】......", num2++, length, Path.GetFileName(str2)));
                    if (!File.Exists(str2))
                    {

                    }
                    else
                    {
                        string mcName = Path.GetFileNameWithoutExtension(str2);

                        TextureClass item = new TextureClass()
                        {
                            Name = mcName,
                            Group = groupid,
                            ObjectId = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant(),
                            Type = Path.GetExtension(str2)
                        };
                        item.Code = item.ObjectId;

                        IImage image = resFactory.CreateImageFromFile(str2);
                        if (image != null)
                        {
                            if (!this.ResManager.ImageExist(item.Code)) this.ResManager.AddImage(item.Code, image);
                            string imageFile = str2.Replace(item.Type, ".thumbnail.png");
                            image.WriteFile(imageFile);
                            Image image2 = Image.FromFile(imageFile);
                            Bitmap image3 = new Bitmap(0x100, 0x100);
                            Graphics graphics = Graphics.FromImage(image3);
                            graphics.DrawImage(image2, 0, 0, 0x100, 0x100);
                            image2.Dispose();
                            if (File.Exists(imageFile)) File.Delete(imageFile);
                            item.Thumbnail = image3;
                            row.SetValue(row.FieldIndex("Name"), item.Name);
                            row.SetValue(row.FieldIndex("ObjectId"), item.ObjectId);
                            row.SetValue(row.FieldIndex("GroupId"), item.Group);
                            row.SetValue(row.FieldIndex("Code"), item.Code);
                            row.SetValue(row.FieldIndex("Type"), item.Type);
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
                    Filter = "所有图片格式(DDS;TIF;JPG;PNG;BMP)|*.DDS;*.TIF;*.JPG;*.PNG;*.BMP",
                    Multiselect = true,
                    RestoreDirectory = true
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] fileNames = dialog.FileNames;
                    if (fileNames != null)
                    {
                        string groupName = this.treeTextureInfo.FocusedNode.GetValue("Name").ToString();
                        HashSet<string> textureNames = (HashSet<string>)this._groupNameTable[groupName];
                        ArrayList arrFiles = new ArrayList();
                        string strtemp = "已存在纹理：";
                        if (textureNames != null)
                        {
                            foreach (string fileName in fileNames)
                            {
                                if (!textureNames.Contains(Path.GetFileNameWithoutExtension(fileName)))
                                    arrFiles.Add(fileName);
                                else strtemp += Path.GetFileNameWithoutExtension(fileName) + "、";
                            }
                            if (strtemp != "已存在纹理：")
                            {
                                strtemp = strtemp.Substring(0, strtemp.Length - 1) + "。";
                                XtraMessageBox.Show(strtemp, "提示");
                            }
                        }
                        string groupid = this.treeTextureInfo.FocusedNode.GetValue("ObjectId").ToString();
                        List<TextureClass> list = ImportTextureClass(groupid, (string[])arrFiles.ToArray(typeof(string)));
                        if (list != null)
                        {
                            DataRow row = null;
                            foreach (TextureClass class2 in list)
                            {
                                row = this._dtTextureInfo.NewRow();
                                row["Name"] = class2.Name;
                                row["Thumbnail"] = class2.Thumbnail;
                                row["Comment"] = class2.Comment;
                                row["TextureInfo"] = class2;
                                this._dtTextureInfo.Rows.Add(row);
                                textureNames.Add(class2.Name);
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
            WaitDialogForm form = null;
            TextureClass class2 = null;
            int num3 = 0;
            int num2 = 0;
            if (this._dtTextureInfo.Rows.Count == 0)
            {
                XtraMessageBox.Show("该组无纹理！", "提示");
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (DialogResult.OK == dialog.ShowDialog())
            {
                try
                {
                    form = new WaitDialogForm("导出纹理，未开始......", "请稍候");
                    form.Show();
                    int count = this._dtTextureInfo.Rows.Count;
                    string folderPath = dialog.SelectedPath;
                    foreach (DataRow row in this._dtTextureInfo.Rows)
                    {
                        form.SetCaption(string.Format(@"({0}\{1}),【{2}】......", num2++, count, row["Name"]));
                        if ((row["TextureInfo"] != null) && ((class2 = row["TextureInfo"] as TextureClass) != null))
                        {
                            IImage img = this.ResManager.GetImage(class2.Code);
                            if (img != null)
                            {
                                string filePath = string.Format(@"{0}\{1}{2}", folderPath, class2.Name, (class2.Type == "") ? ".jpg" : class2.Type);
                                if (img.WriteFile(filePath)) num3++;
                            }
                        }
                    }
                    form.Close();
                    form = null;
                    XtraMessageBox.Show(string.Format("导出纹理完成，总计：{0}个，成功：{1}个！", count, num3), "提示");
                }
                catch (Exception exception)
                {
                    XtraMessageBox.Show("导出纹理出错！", "提示");
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
        }

        private void bItemReNameGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeTextureInfo.FocusedNode != null)
            {
                this.treeTextureInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = true;
                this.treeTextureInfo.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = true;
                this._oldGroupName = this.treeTextureInfo.FocusedNode.GetValue("Name").ToString();
                this.treeTextureInfo.ShowEditor();
            }

        }

        private void bItemDeleteGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.treeTextureInfo.FocusedNode != null)
            {
                try
                {
                    if (DialogResult.OK == XtraMessageBox.Show("确定删除纹理组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        if (!DeleteTextureGroup(this._curGroupId))
                        {
                            XtraMessageBox.Show("纹理组删除出错！", "提示");
                        }
                        else
                        {
                            this._dtTextureInfo.Rows.Clear();
                            string groupname = this.treeTextureInfo.FocusedNode.GetValue("Name").ToString();
                            if (this._groupNameTable[groupname] != null)
                            {
                                HashSet<string> textureNames = (HashSet<string>)this._groupNameTable[groupname];
                                textureNames.Clear();
                                this._groupNameTable.Remove(groupname);
                            }
                            this.treeTextureInfo.DeleteNode(this.treeTextureInfo.FocusedNode);
                        }
                    }
                }
                catch (Exception exception)
                {
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
                this.popupMenuTexture.ShowPopup(Cursor.Position);
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
                DataRow row = this._dtTextureInfo.Rows[focusedRowHandle];
                string textureName = e.Value.ToString();
                HashSet<string> textureNames = (HashSet<string>)this._groupNameTable[this.treeTextureInfo.FocusedNode.GetValue("Name").ToString()];
                if (textureNames.Contains(textureName))
                {
                    row["Name"] = this._oldTextureName;
                    XtraMessageBox.Show("纹理名称已存在！", "提示");
                }
                else
                {
                    if (this.RenameTexture(row["TextureInfo"] as TextureClass, textureName))
                    {
                        row["Name"] = textureName;
                        textureNames.Remove(this._oldTextureName);
                        textureNames.Add(textureName);
                        this._oldTextureName = textureName;
                    }
                    else
                    {
                        row["Name"] = this._oldTextureName;
                        XtraMessageBox.Show("纹理重命名出错！", "提示");
                    }

                }
            }
        }

        private bool RenameTexture(TextureClass mc, string newName)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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
        private bool DeleteTexture(TextureClass mc)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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
        private bool ReplaceTexture(TextureClass mc, Image thumbnail)
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
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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

        private void bBtnReName_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.layoutView1.Columns["Name"].OptionsColumn.AllowEdit = true;
            this.layoutView1.Columns["Name"].OptionsColumn.AllowFocus = true;
            this.layoutView1.FocusedColumn = this.layoutView1.Columns["Name"];
            this.isEditItemName = true;
            DataRow dr = this._dtTextureInfo.Rows[this.layoutView1.FocusedRowHandle];
            if (dr == null) return;
            this._oldTextureName = dr["Name"].ToString();
            this.layoutView1.ShowEditor();

        }

        private void bBtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int focusedRowHandle = this.layoutView1.FocusedRowHandle;
            DataRow row = this._dtTextureInfo.Rows[focusedRowHandle];
            if ((row["TextureInfo"] != null) && (row["TextureInfo"] is TextureClass) && (DialogResult.OK == XtraMessageBox.Show("确定删除该纹理吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)))
            {
                TextureClass tc = row["TextureInfo"] as TextureClass;
                if (!DeleteTexture(tc))
                {
                    XtraMessageBox.Show("删除纹理出错！", "提示");
                }
                else
                {
                    try
                    {
                        if (this.ResManager != null && this.ResManager.ImageExist(tc.Code))
                        {
                            this.ResManager.DeleteImage(tc.Code);
                        }
                        string name = row["Name"].ToString();
                        this._dtTextureInfo.Rows.RemoveAt(focusedRowHandle);
                        HashSet<string> textureNames = (HashSet<string>)this._groupNameTable[this.treeTextureInfo.FocusedNode.GetValue("Name").ToString()];
                        if (textureNames != null) textureNames.Remove(name);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void bBtnReplace_ItemClick(object sender, ItemClickEventArgs e)
        {
            TextureClass class2 = null;
            DataRow row = null;
            try
            {
                int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                row = this._dtTextureInfo.Rows[focusedRowHandle];
                if ((row["TextureInfo"] != null) && ((class2 = row["TextureInfo"] as TextureClass) != null))
                {
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Filter = "所有图片格式(DDS;TIF;JPG;PNG;BMP)|*.DDS;*.TIF;*.JPG;*.PNG;*.BMP",
                        RestoreDirectory = true
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Image thumbnail = Image.FromFile(dialog.FileName);
                        if (!this.ReplaceTexture(class2, thumbnail))
                        {
                            XtraMessageBox.Show("替换纹理出错！", "提示");
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
                XtraMessageBox.Show("替换纹理出错！", "提示");
            }
        }
    }

 

}
