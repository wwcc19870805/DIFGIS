using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using ICSharpCode.Core;
using DevExpress.XtraTreeList;
using System.Xml;
using DevExpress.XtraBars;
using DFWinForms.Base;
using DFWinForms.UserControl;

namespace DFWinForms.LogicTree
{
    public class LogicBaseTree : XtraUserControl, ILogicTree<IBaseLayer>, IPopMenu,IPadContent
    {
        private IContainer components;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar quickSearch_bar;
        private DevExpress.XtraBars.BarEditItem cmb_searchTxt;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmb_searchList;
        private DevExpress.XtraBars.BarButtonItem quickSearch_bbi;
        private DevExpress.XtraBars.BarButtonItem btn_extend;
        private DevExpress.XtraBars.Bar layerControl_bar;
        private DevExpress.XtraBars.BarButtonItem moveUp_bBI;
        private DevExpress.XtraBars.BarButtonItem moveDown_bBI;
        private DevExpress.XtraBars.BarButtonItem importXml_bBI;
        private DevExpress.XtraBars.BarButtonItem exportXml_bBI;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        protected PictureEdit preViewPictureEdit;
        protected TreeList _baseTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_node;
        protected DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_color;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_Custom;
        protected DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        protected DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;

        public LogicBaseTree()
        {
            InitializeComponent();
            this._rootMenu = new System.Windows.Forms.ContextMenuStrip();
        }

        private void InitializeComponent()
        {
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogicBaseTree));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.quickSearch_bar = new DevExpress.XtraBars.Bar();
            this.cmb_searchTxt = new DevExpress.XtraBars.BarEditItem();
            this.cmb_searchList = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.quickSearch_bbi = new DevExpress.XtraBars.BarButtonItem();
            this.btn_extend = new DevExpress.XtraBars.BarButtonItem();
            this.layerControl_bar = new DevExpress.XtraBars.Bar();
            this.moveUp_bBI = new DevExpress.XtraBars.BarButtonItem();
            this.moveDown_bBI = new DevExpress.XtraBars.BarButtonItem();
            this.importXml_bBI = new DevExpress.XtraBars.BarButtonItem();
            this.exportXml_bBI = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._baseTree = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn_node = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn_color = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn_Custom = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.preViewPictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_searchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._baseTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preViewPictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.quickSearch_bar,
            this.layerControl_bar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection2;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.quickSearch_bbi,
            this.moveUp_bBI,
            this.moveDown_bBI,
            this.importXml_bBI,
            this.exportXml_bBI,
            this.btn_extend,
            this.cmb_searchTxt});
            this.barManager1.MaxItemId = 22;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.cmb_searchList});
            // 
            // quickSearch_bar
            // 
            this.quickSearch_bar.BarName = "Custom 2";
            this.quickSearch_bar.DockCol = 0;
            this.quickSearch_bar.DockRow = 0;
            this.quickSearch_bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.quickSearch_bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.cmb_searchTxt),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.quickSearch_bbi, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.btn_extend, false)});
            this.quickSearch_bar.OptionsBar.AllowQuickCustomization = false;
            this.quickSearch_bar.OptionsBar.DisableCustomization = true;
            this.quickSearch_bar.OptionsBar.DrawDragBorder = false;
            this.quickSearch_bar.OptionsBar.UseWholeRow = true;
            this.quickSearch_bar.Text = "Custom 2";
            // 
            // cmb_searchTxt
            // 
            this.cmb_searchTxt.Edit = this.cmb_searchList;
            this.cmb_searchTxt.Id = 20;
            this.cmb_searchTxt.Name = "cmb_searchTxt";
            this.cmb_searchTxt.Width = 121;
            // 
            // cmb_searchList
            // 
            this.cmb_searchList.AutoHeight = false;
            this.cmb_searchList.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_searchList.Name = "cmb_searchList";
            this.cmb_searchList.SelectedIndexChanged += new System.EventHandler(this.cmb_searchList_SelectedIndexChanged);
            // 
            // quickSearch_bbi
            // 
            this.quickSearch_bbi.Caption = "${res:Interface_查询}";
            this.quickSearch_bbi.Id = 13;
            this.quickSearch_bbi.ImageIndex = 0;
            this.quickSearch_bbi.Name = "quickSearch_bbi";
            toolTipItem1.Text = "${res:Interface_图层查询}";
            superToolTip1.Items.Add(toolTipItem1);
            this.quickSearch_bbi.SuperTip = superToolTip1;
            this.quickSearch_bbi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.QuickSearch);
            // 
            // btn_extend
            // 
            this.btn_extend.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btn_extend.Caption = "<<";
            this.btn_extend.Id = 19;
            this.btn_extend.Name = "btn_extend";
            this.btn_extend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Extend_ItemClick);
            // 
            // layerControl_bar
            // 
            this.layerControl_bar.BarName = "Custom 3";
            this.layerControl_bar.DockCol = 0;
            this.layerControl_bar.DockRow = 1;
            this.layerControl_bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.layerControl_bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.moveUp_bBI),
            new DevExpress.XtraBars.LinkPersistInfo(this.moveDown_bBI),
            new DevExpress.XtraBars.LinkPersistInfo(this.importXml_bBI, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.exportXml_bBI)});
            this.layerControl_bar.OptionsBar.AllowQuickCustomization = false;
            this.layerControl_bar.OptionsBar.DisableCustomization = true;
            this.layerControl_bar.OptionsBar.DrawDragBorder = false;
            this.layerControl_bar.OptionsBar.UseWholeRow = true;
            this.layerControl_bar.Text = "Custom 3";
            this.layerControl_bar.Visible = false;
            // 
            // moveUp_bBI
            // 
            this.moveUp_bBI.Caption = "${res:Interface_上移}";
            this.moveUp_bBI.Id = 14;
            this.moveUp_bBI.ImageIndex = 1;
            this.moveUp_bBI.Name = "moveUp_bBI";
            this.moveUp_bBI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.moveUp_bBI_ItemClick);
            // 
            // moveDown_bBI
            // 
            this.moveDown_bBI.Caption = "${res:Interface_下移}";
            this.moveDown_bBI.Id = 15;
            this.moveDown_bBI.ImageIndex = 2;
            this.moveDown_bBI.Name = "moveDown_bBI";
            this.moveDown_bBI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.moveDown_bBI_ItemClick);
            // 
            // importXml_bBI
            // 
            this.importXml_bBI.Caption = "${res:Interface_导入配置}";
            this.importXml_bBI.Id = 16;
            this.importXml_bBI.ImageIndex = 3;
            this.importXml_bBI.Name = "importXml_bBI";
            this.importXml_bBI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.importXml_bBI_ItemClick);
            // 
            // exportXml_bBI
            // 
            this.exportXml_bBI.Caption = "${res:Interface_导出配置}";
            this.exportXml_bBI.Id = 17;
            this.exportXml_bBI.ImageIndex = 4;
            this.exportXml_bBI.Name = "exportXml_bBI";
            this.exportXml_bBI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.exportXml_bBI_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(270, 62);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 484);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(270, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 62);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 422);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(270, 62);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 422);
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.InsertGalleryImage("zoom_16x16.png", "images/zoom/zoom_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/zoom/zoom_16x16.png"), 0);
            this.imageCollection2.Images.SetKeyName(0, "zoom_16x16.png");
            this.imageCollection2.InsertGalleryImage("moveup_16x16.png", "images/arrows/moveup_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/moveup_16x16.png"), 1);
            this.imageCollection2.Images.SetKeyName(1, "moveup_16x16.png");
            this.imageCollection2.InsertGalleryImage("movedown_16x16.png", "images/arrows/movedown_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/movedown_16x16.png"), 2);
            this.imageCollection2.Images.SetKeyName(2, "movedown_16x16.png");
            this.imageCollection2.Images.SetKeyName(3, "btnImport.Glyph.png");
            this.imageCollection2.Images.SetKeyName(4, "btnExport.Glyph.png");
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.barStaticItem1.Id = 11;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem1.Width = 32;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // _baseTree
            // 
            this._baseTree.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this._baseTree.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this._baseTree.Appearance.FocusedCell.Options.UseBackColor = true;
            this._baseTree.Appearance.FocusedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this._baseTree.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this._baseTree.Appearance.FocusedRow.Options.UseBackColor = true;
            this._baseTree.Appearance.SelectedRow.BackColor = System.Drawing.Color.CornflowerBlue;
            this._baseTree.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this._baseTree.Appearance.SelectedRow.Options.UseBackColor = true;
            this._baseTree.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(192)))), ((int)(((byte)(184)))));
            this._baseTree.Appearance.VertLine.Options.UseBackColor = true;
            this._baseTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn_node,
            this.treeListColumn_color,
            this.treeListColumn_Custom});
            this._baseTree.Location = new System.Drawing.Point(2, 2);
            this._baseTree.Name = "_baseTree";
            this._baseTree.OptionsBehavior.AllowIndeterminateCheckState = true;
            this._baseTree.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this._baseTree.OptionsBehavior.AutoChangeParent = false;
            this._baseTree.OptionsBehavior.AutoNodeHeight = false;
            this._baseTree.OptionsBehavior.CanCloneNodesOnDrop = true;
            this._baseTree.OptionsBehavior.CloseEditorOnLostFocus = false;
            this._baseTree.OptionsBehavior.DragNodes = true;
            this._baseTree.OptionsBehavior.KeepSelectedOnClick = false;
            this._baseTree.OptionsBehavior.SmartMouseHover = false;
            this._baseTree.OptionsMenu.EnableColumnMenu = false;
            this._baseTree.OptionsMenu.EnableFooterMenu = false;
            this._baseTree.OptionsMenu.ShowAutoFilterRowItem = false;
            this._baseTree.OptionsView.ShowCheckBoxes = true;
            this._baseTree.OptionsView.ShowColumns = false;
            this._baseTree.OptionsView.ShowIndicator = false;
            this._baseTree.OptionsView.ShowVertLines = false;
            this._baseTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemColorEdit1});
            this._baseTree.Size = new System.Drawing.Size(266, 290);
            this._baseTree.StateImageList = this.imageCollection1;
            this._baseTree.TabIndex = 5;
            this._baseTree.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this._baseTree_NodeCellStyle);
            this._baseTree.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this._baseTree_AfterCheckNode);
            this._baseTree.HiddenEditor += new System.EventHandler(this._baseTree_HiddenEditor);
            this._baseTree.EditorKeyPress += new System.Windows.Forms.KeyPressEventHandler(this._baseTree_EditorKeyPress);
            this._baseTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._baseTree_MouseDoubleClick);
            this._baseTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this._baseTree_MouseDown);
            this._baseTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this._baseTree_MouseUp);
            // 
            // treeListColumn_node
            // 
            this.treeListColumn_node.FieldName = "Name";
            this.treeListColumn_node.MinWidth = 49;
            this.treeListColumn_node.Name = "treeListColumn_node";
            this.treeListColumn_node.OptionsColumn.AllowEdit = false;
            this.treeListColumn_node.OptionsColumn.AllowSort = false;
            this.treeListColumn_node.Visible = true;
            this.treeListColumn_node.VisibleIndex = 0;
            // 
            // treeListColumn_color
            // 
            this.treeListColumn_color.Name = "treeListColumn_color";
            this.treeListColumn_color.OptionsColumn.AllowEdit = false;
            this.treeListColumn_color.OptionsColumn.AllowSize = false;
            this.treeListColumn_color.Width = 100;
            // 
            // treeListColumn_Custom
            // 
            this.treeListColumn_Custom.Caption = "${res:Interface_类型}";
            this.treeListColumn_Custom.FieldName = "Custom";
            this.treeListColumn_Custom.Name = "treeListColumn_Custom";
            this.treeListColumn_Custom.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
            this.repositoryItemPictureEdit1.ErrorImage = null;
            this.repositoryItemPictureEdit1.InitialImage = null;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.NullText = " ";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.NullText = " ";
            this.repositoryItemColorEdit1.ShowColorDialog = false;
            this.repositoryItemColorEdit1.ShowCustomColors = false;
            this.repositoryItemColorEdit1.ShowSystemColors = false;
            this.repositoryItemColorEdit1.ShowWebColors = false;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Group.png");
            this.imageCollection1.Images.SetKeyName(1, "Database.png");
            this.imageCollection1.Images.SetKeyName(2, "Dataset.png");
            this.imageCollection1.Images.SetKeyName(3, "FeatureLayer_model.png");
            this.imageCollection1.Images.SetKeyName(4, "FeatureLayer_point.png");
            this.imageCollection1.Images.SetKeyName(5, "FeatureLayer_line.png");
            this.imageCollection1.Images.SetKeyName(6, "FeatureLayer_polygon.png");
            this.imageCollection1.Images.SetKeyName(7, "Object.png");
            this.imageCollection1.Images.SetKeyName(8, "TerrainLayer.png");
            this.imageCollection1.Images.SetKeyName(9, "3DTileLayer.png");
            this.imageCollection1.Images.SetKeyName(10, "Label_text.png");
            this.imageCollection1.Images.SetKeyName(11, "Label_image.png");
            // 
            // preViewPictureEdit
            // 
            this.preViewPictureEdit.Location = new System.Drawing.Point(5, 319);
            this.preViewPictureEdit.MenuManager = this.barManager1;
            this.preViewPictureEdit.Name = "preViewPictureEdit";
            this.preViewPictureEdit.Size = new System.Drawing.Size(260, 98);
            this.preViewPictureEdit.StyleController = this.layoutControl1;
            this.preViewPictureEdit.TabIndex = 5;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.preViewPictureEdit);
            this.layoutControl1.Controls.Add(this._baseTree);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 62);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(270, 422);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(270, 422);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this._baseTree;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(270, 294);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "preViewInfoGroup";
            this.layoutControlGroup2.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup2.ExpandButtonVisible = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 294);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(270, 128);
            this.layoutControlGroup2.Text = "preViewInfoGroup";
            this.layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.preViewPictureEdit;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(264, 102);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // LogicBaseTree
            // 
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "LogicBaseTree";
            this.Size = new System.Drawing.Size(270, 484);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_searchList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._baseTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preViewPictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void cmb_searchList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
                IBaseLayer baseLayer = comboBoxEdit.SelectedItem as IBaseLayer;
                if (baseLayer != null)
                {
                    if (baseLayer.Parent != null && !baseLayer.Parent.Expanded)
                    {
                        baseLayer.Parent.Expanded = true;
                    }
                    baseLayer.Activate();
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void QuickSearch(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.cmb_searchTxt.EditValue == null)
            {
                return;
            }
            string blurValue = this.cmb_searchTxt.EditValue.ToString().Trim();
            this.cmb_searchList.Items.Clear();
            this.SearchLayers(blurValue, true);
        }
        public System.Collections.Generic.List<IBaseLayer> SearchLayers(string blurValue, bool bAddToCmb)
        {
            System.Collections.Generic.List<IBaseLayer> rootLayers = this.GetRootLayers();
            if (rootLayers == null || rootLayers.Count == 0)
            {
                return null;
            }
            if (string.IsNullOrEmpty(blurValue))
            {
                return null;
            }
            System.Collections.Generic.List<IBaseLayer> result = new System.Collections.Generic.List<IBaseLayer>();
            foreach (IBaseLayer current in rootLayers)
            {
                this.SearchLayer(blurValue, current, bAddToCmb, ref result);
            }
            return result;
        }
        private void SearchLayer(string partName, IBaseLayer layer, bool bAddToCmb, ref System.Collections.Generic.List<IBaseLayer> resLyrList)
        {
            if (layer == null)
            {
                return;
            }
            string text = layer.Name.ToLower();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            if (string.IsNullOrEmpty(partName))
            {
                return;
            }
            partName = partName.ToLower();
            int num = text.IndexOf(partName);
            if (num > -1)
            {
                resLyrList.Add(layer);
                if (bAddToCmb)
                {
                    layer.Activate();
                    this.cmb_searchList.Items.Add(layer);
                }
            }
            if (layer is IGroupLayer)
            {
                IGroupLayer groupLayer = layer as IGroupLayer;
                System.Collections.Generic.List<IBaseLayer> list = groupLayer.SelectAllSubLayers();
                if (list == null || list.Count == 0)
                {
                    return;
                }
                foreach (IBaseLayer current in list)
                {
                    this.SearchLayer(partName, current, bAddToCmb, ref resLyrList);
                }
            }
        }

        public virtual void Extend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void moveUp_bBI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = this._baseTree.FocusedNode;
            if (focusedNode == null)
            {
                return;
            }
            TreeListNode prevNode = focusedNode.PrevNode;
            if (prevNode == null)
            {
                return;
            }
            int nodeIndex = this._baseTree.GetNodeIndex(prevNode);
            int nodeIndex2 = this._baseTree.GetNodeIndex(focusedNode);
            this._baseTree.SetNodeIndex(focusedNode, nodeIndex);
            this._baseTree.SetNodeIndex(prevNode, nodeIndex2);
            this._baseTree.FocusedNode = focusedNode;
        }

        private void moveDown_bBI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = this._baseTree.FocusedNode;
            if (focusedNode == null)
            {
                return;
            }
            TreeListNode nextNode = focusedNode.NextNode;
            if (nextNode == null)
            {
                return;
            }
            int nodeIndex = this._baseTree.GetNodeIndex(nextNode);
            int nodeIndex2 = this._baseTree.GetNodeIndex(focusedNode);
            this._baseTree.SetNodeIndex(focusedNode, nodeIndex);
            this._baseTree.SetNodeIndex(nextNode, nodeIndex2);
            this._baseTree.FocusedNode = focusedNode;

        }
        private void BuildTreeFromXml(XmlElement node, IGroupLayer gl)
        {
            if (gl == null)
            {
                object[] args = new object[]
				{
					node.GetAttribute("Guid")
				};
                gl = (IGroupLayer)System.Reflection.Assembly.Load("DFWinForms").CreateInstance("DFWinForms.LogicTree." + node.Name, false, System.Reflection.BindingFlags.Default, null, args, null, null);
                if (gl != null)
                {
                    gl.LogicTree = this;
                    gl.OwnNode = this._baseTree.AppendNode(new object[]
					{
						gl.Name
					}, null);
                    gl.OwnNode.StateImageIndex = gl.ImageIndex;
                    gl.Name = node.GetAttribute("Name");
                    gl.Visible = true;
                }
            }
            if (node.HasChildNodes)
            {
                foreach (XmlNode xmlNode in node.ChildNodes)
                {
                    lock (this._lockthis)
                    {
                        XmlElement xmlElement = xmlNode as XmlElement;
                        if (xmlElement != null)
                        {
                            object[] args2 = new object[]
							{
								xmlElement.GetAttribute("Guid")
							};
                            IBaseLayer baseLayer = (IBaseLayer)System.Reflection.Assembly.Load("DFWinForms").CreateInstance("DFWinForms.LogicTree." + xmlElement.Name, false, System.Reflection.BindingFlags.Default, null, args2, null, null);
                            if (baseLayer != null)
                            {
                                gl.Add2(baseLayer);
                                baseLayer.Name = xmlElement.GetAttribute("Name");
                                baseLayer.Visible = xmlElement.GetAttribute("Visible").Equals("True");
                                if (baseLayer is IGroupLayer)
                                {
                                    this.BuildTreeFromXml(xmlElement, baseLayer as IGroupLayer);
                                }
                            }
                        }
                    }
                }
            }
        }
        public virtual void IterativeWriteXML(TreeListNode node, XmlWriter xw)
        {
            if (node.Tag != null)
            {
                IBaseLayer baseLayer = node.Tag as IBaseLayer;
                if (baseLayer != null)
                {
                    xw.WriteStartElement(baseLayer.GetType().Name);
                    xw.WriteAttributeString("Name", baseLayer.Name);
                    xw.WriteAttributeString("Guid", baseLayer.ID);
                    xw.WriteAttributeString("Visible", baseLayer.Visible.ToString());
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeListNode node2 in node.Nodes)
                        {
                            this.IterativeWriteXML(node2, xw);
                        }
                    }
                    xw.WriteEndElement();
                }
            }
        }
        public virtual void ExportXML(string filepath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filepath))
                {
                    if (this._baseTree.Nodes.Count > 0 && System.IO.File.Exists(filepath))
                    {
                        this._baseTree.Refresh();
                        XmlWriter xmlWriter = XmlWriter.Create(filepath, new XmlWriterSettings
                        {
                            Indent = true
                        });
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("LogicTree");
                        xmlWriter.WriteAttributeString("Name", this.Caption);
                        xmlWriter.WriteAttributeString("Guid", string.IsNullOrEmpty(this._treeGuid) ? "" : System.Guid.NewGuid().ToString());
                        for (int i = 0; i < this._baseTree.Nodes.Count; i++)
                        {
                            this.IterativeWriteXML(this._baseTree.Nodes[i], xmlWriter);
                        }
                        xmlWriter.WriteEndElement();
                        xmlWriter.Close();
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public virtual void ImportXML(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                this._baseTree.Nodes.Clear();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepath);
                XmlElement documentElement = xmlDocument.DocumentElement;
                this._treeGuid = (string.IsNullOrEmpty(documentElement.GetAttribute("Guid")) ? System.Guid.NewGuid().ToString() : documentElement.GetAttribute("Guid"));
                this._treeName = documentElement.GetAttribute("Name");
                IGroupLayer gl = null;
                for (int i = 0; i < documentElement.ChildNodes.Count; i++)
                {
                    this.BuildTreeFromXml(documentElement.ChildNodes[i] as XmlElement, gl);
                }
                System.Windows.Forms.Application.DoEvents();
                return;
            }
            LoggingService.Error("XML配置文件不存在");
        }

        public virtual void importXml_bBI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Title = StringParser.Parse("${res:Interface_导入配置文件}");
            openFileDialog.Filter = StringParser.Parse("${res:Interface_配置文件}") + "(*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;
            if (System.Windows.Forms.DialogResult.OK == openFileDialog.ShowDialog())
            {
                this.ImportXML(openFileDialog.FileName);
            }
        }

        public virtual void exportXml_bBI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._baseTree.Nodes.Count == 0)
            {
                return;
            }
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = StringParser.Parse("${res:Interface_导出配置文件}");
            saveFileDialog.Filter = StringParser.Parse("${res:Interface_配置文件}") + "(*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;
            if (System.Windows.Forms.DialogResult.OK == saveFileDialog.ShowDialog())
            {
                this.ExportXML(saveFileDialog.FileName);
            }
        }

        private void _baseTree_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.CheckState == System.Windows.Forms.CheckState.Indeterminate)
            {
                e.Node.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            IBaseLayer baseLayer = e.Node.Tag as IBaseLayer;
            if (baseLayer != null)
            {
                baseLayer.Visible = e.Node.Checked;
                if (this._onLayerCheckedStateChangedByMouseClickHandler != null)
                {
                    this._onLayerCheckedStateChangedByMouseClickHandler(baseLayer);
                }
            }            
            this.SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }
        private void SetCheckedParentNodes(TreeListNode node, System.Windows.Forms.CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool flag = false;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    System.Windows.Forms.CheckState checkState = node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(checkState))
                    {
                        flag = !flag;
                        break;
                    }
                }
                node.ParentNode.CheckState = (flag ? System.Windows.Forms.CheckState.Indeterminate : check);
                this.SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private void _baseTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None)
                {
                    bool flag = this.HitTest(e.X, e.Y, ref this._hitLyr);
                    if (flag && this._hitLyr != null && this._onLayerDoubleClick != null)
                    {
                        this._onLayerDoubleClick(this._hitLyr);
                    }
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void _baseTree_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None && this._baseTree.State == TreeListState.Regular)
                {
                    bool flag = this.HitTest(e.X, e.Y, ref this._hitLyr);
                    if (this._onHitTest != null && flag)
                    {
                        this._onHitTest(e.Button, e.X, e.Y, this._hitLyr);
                    }
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
         
        private void _baseTree_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None && this._baseTree.State == TreeListState.Regular)
                {
                    bool flag = this.HitTest(e.X, e.Y, ref this._hitLyr);
                    if (this._onHitTest != null && flag)
                    {
                        this._onHitTest(e.Button, e.X, e.Y, this._hitLyr);
                    }
                    if (this.ShowPopMenu)
                    {
                        if (flag && this._hitLyr != null)
                        {
                            this._hitLyr.HitPoint = this._baseTree.PointToScreen(e.Location);
                            if (this._hitLyr.ShowPopMenu)
                            {
                                this.AuthorityCheck(ref this._hitLyr);
                                this._hitLyr.PopMenu.Items.Clear();
                                this._hitLyr.InitPopMenu();
                                this._hitLyr.PopMenu.Show(this._baseTree, new System.Drawing.Point(e.X + 10, e.Y));
                                System.Windows.Forms.Application.DoEvents();
                            }
                        }
                        else
                        {
                            this._rootMenu.Items.Clear();
                            this.InitPopMenu();
                            this._rootMenu.Show(this._baseTree, new System.Drawing.Point(e.X + 10, e.Y));
                            System.Windows.Forms.Application.DoEvents();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }

        }
        protected virtual void AuthorityCheck(ref IBaseLayer layer)
        {

        }

        private void _baseTree_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node != null && node.Tag != null)
            {
                IBaseLayer baseLayer = node.Tag as IBaseLayer;
                if (baseLayer != null && !baseLayer.IsValid)
                {
                    System.Drawing.Font font = new System.Drawing.Font(e.Appearance.GetFont(), System.Drawing.FontStyle.Strikeout);
                    e.Appearance.Font = font;
                }
            }
        }

        public bool HitTest(int x, int y, ref IBaseLayer layer)
        {
            TreeListHitInfo treeListHitInfo = this._baseTree.CalcHitInfo(new System.Drawing.Point(x, y));
            if (treeListHitInfo.HitInfoType == HitInfoType.Cell)
            {
                this._baseTree.SetFocusedNode(treeListHitInfo.Node);
                if (treeListHitInfo.Node.Tag != null)
                {
                    layer = (treeListHitInfo.Node.Tag as IBaseLayer);
                    if (layer != null)
                    {
                        return true;
                    }
                }
            }
            layer = null;
            return false;
        }
       
        private void _baseTree_HiddenEditor(object sender, System.EventArgs e)
        {
            try
            {
                object value = this._baseTree.FocusedNode.GetValue("Name");
                if (value == null)
                {
                    this._baseTree.CancelCurrentEdit();
                }
                else
                {
                    string text = value.ToString().Trim();
                    if (string.IsNullOrEmpty(text))
                    {
                        this._baseTree.CancelCurrentEdit();
                    }
                    else
                    {
                        IBaseLayer baseLayer = this._baseTree.FocusedNode.Tag as IBaseLayer;
                        if (baseLayer != null)
                        {
                            string name = baseLayer.Name;
                            baseLayer.Name = text;
                            if (this._onRenamed != null && name != text)
                            {
                                this._onRenamed(baseLayer, name);
                            }
                            this.LayerEdit(baseLayer, EditType.Edit_LayerName);
                            this.ExportXML(null);
                        }
                        else
                        {
                            this._baseTree.CancelCurrentEdit();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                this._baseTree.CloseEditor();
                this._baseTree.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowEdit = false;
                this._baseTree.FocusedNode.TreeList.Columns.ColumnByFieldName("Name").OptionsColumn.AllowFocus = false;
            }
        }
        public void LayerEdit(IBaseLayer layer, EditType editType)
        {
            if (this._onLayerEdit != null)
            {
                this._onLayerEdit(layer, editType);
            }
        }

        private void _baseTree_EditorKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private IBaseLayer _hitLyr;
        protected string _treeGuid = "";
        protected string _treeName = "";
        private object _lockthis = new object();
        public ToolStripMenuItem AddMenuItem(string caption)
        {
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._rootMenu.Items.Add(new ToolStripSeparator());
                return null;
            }
            return (this._rootMenu.Items.Add(caption, null, new EventHandler(this.OnMenuItemClick)) as ToolStripMenuItem);
        }

        public ToolStripMenuItem AddMenuItem(string parentCaption, string[] subCaptions)
        {
            if ((string.IsNullOrEmpty(parentCaption.Trim()) || (subCaptions == null)) || (subCaptions.Length == 0))
            {
                return null;
            }
            ToolStripMenuItem item = this._rootMenu.Items.Add(parentCaption) as ToolStripMenuItem;
            if (item != null)
            {
                foreach (string str in subCaptions)
                {
                    if (string.IsNullOrEmpty(str.Trim()))
                    {
                        item.DropDownItems.Add(new ToolStripSeparator());
                    }
                    else
                    {
                        item.DropDownItems.Add(str, null, new EventHandler(this.OnMenuItemClick));
                    }
                }
            }
            return item;
        }

        public ToolStripMenuItem AddMenuItem(string caption, Image image)
        {
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._rootMenu.Items.Add(new ToolStripSeparator());
                return null;
            }
            return (this._rootMenu.Items.Add(caption, image, new EventHandler(this.OnMenuItemClick)) as ToolStripMenuItem);
        }

        public ToolStripMenuItem AddMenuItem(string caption, int index)
        {
            if ((index <= -1) || (index >= this._rootMenu.Items.Count))
            {
                return this.AddMenuItem(caption);
            }
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._rootMenu.Items.Insert(index, new ToolStripSeparator());
                return null;
            }
            ToolStripMenuItem item = new ToolStripMenuItem(caption, null, new EventHandler(this.OnMenuItemClick));
            this._rootMenu.Items.Insert(index, item);
            return item;
        }

        public ToolStripMenuItem AddMenuItem(string caption, int index, Image image)
        {
            if ((index <= -1) || (index >= this._rootMenu.Items.Count))
            {
                return this.AddMenuItem(caption, image);
            }
            if (string.IsNullOrEmpty(caption.Trim()))
            {
                this._rootMenu.Items.Insert(index, new ToolStripSeparator());
                return null;
            }
            ToolStripMenuItem item = new ToolStripMenuItem(caption, image, new EventHandler(this.OnMenuItemClick));
            this._rootMenu.Items.Insert(index, item);
            return item;
        }

        public ToolStripMenuItem AddMenuItem(string parentCaption, int index, string[] subCaptions)
        {
            if ((string.IsNullOrEmpty(parentCaption.Trim()) || (subCaptions == null)) || (subCaptions.Length == 0))
            {
                return null;
            }
            if ((index <= -1) || (index >= this._rootMenu.Items.Count))
            {
                return this.AddMenuItem(parentCaption, subCaptions);
            }
            ToolStripMenuItem item = new ToolStripMenuItem(parentCaption);
            if (item != null)
            {
                this._rootMenu.Items.Insert(index, item);
                foreach (string str in subCaptions)
                {
                    if (string.IsNullOrEmpty(str.Trim()))
                    {
                        item.DropDownItems.Add(new ToolStripSeparator());
                    }
                    else
                    {
                        item.DropDownItems.Add(str, null, new EventHandler(this.OnMenuItemClick));
                    }
                }
            }
            return item;
        }

        public void AddMenuItems(string[] captions)
        {
            if ((captions != null) && (captions.Length > 0))
            {
                foreach (string str in captions)
                {
                    this.AddMenuItem(str);
                }
            }
        }

        protected System.Windows.Forms.ContextMenuStrip _rootMenu;
        public virtual System.Windows.Forms.ContextMenuStrip PopMenu
        {
            get
            {
                return this._rootMenu;
            }
            set
            {
                this._rootMenu = value;
            }
        }
        private bool _showPopMenu = true;
        public bool ShowPopMenu
        {
            get
            {
                return this._showPopMenu;
            }
            set
            {
                this._showPopMenu = value;
            }
        }

        private event OnLayerCheckedStateChangedByMouseClickHandler<IBaseLayer> _onLayerCheckedStateChangedByMouseClickHandler;
        public event OnLayerCheckedStateChangedByMouseClickHandler<IBaseLayer> OnLayerCheckedStateChangedByMouseClick
        {
            add
            {
                this._onLayerCheckedStateChangedByMouseClickHandler += value;
            }
            remove
            {
                this._onLayerCheckedStateChangedByMouseClickHandler -= value;
            }
        }
        private event OnHitTestHandler<IBaseLayer> _onHitTest;
        public event OnHitTestHandler<IBaseLayer> OnHitTest
        {
            add
            {
                this._onHitTest += value;
            }
            remove
            {
                this._onHitTest -= value;
            }
        }
        private event OnLayerDoubleClickHandler<IBaseLayer> _onLayerDoubleClick;
        public event OnLayerDoubleClickHandler<IBaseLayer> OnLayerDoubleClick
        {
            add
            {
                this._onLayerDoubleClick += value;
            }
            remove
            {
                this._onLayerDoubleClick -= value;
            }
        }

        private event OnLayerEditHandler<IBaseLayer> _onLayerEdit;
        public event OnLayerEditHandler<IBaseLayer> OnLayerEdit
        {
            add
            {
                this._onLayerEdit += value;
            }
            remove
            {
                this._onLayerEdit -= value;
            }
        }
        private event OnInitRootPopMenuHandler _onInitRootPopMenu;
        public event OnInitRootPopMenuHandler OnInitRootPopMenu
        {
            add
            {
                this._onInitRootPopMenu += value;
            }
            remove
            {
                this._onInitRootPopMenu -= value;
            }

        }
        private event OnRootPopMenuItemClickHandler _onRootPopMenuItemClick;
        public event OnRootPopMenuItemClickHandler OnRootPopMenuItemClick
        {
            add
            {
                this._onRootPopMenuItemClick += value;
            }
            remove
            {
                this._onRootPopMenuItemClick -= value;
            }

        }
        private bool _extendItemVisible;
        public bool ShowExtendItem
        {
            get
            {
                return this._extendItemVisible;
            }
            set
            {
                this._extendItemVisible = value;
                if (this._extendItemVisible)
                {
                    this.btn_extend.Visibility = BarItemVisibility.Always;
                    return;
                }
                this.btn_extend.Visibility = BarItemVisibility.Never;
            }
        }
        private event OnRenamedHandler _onRenamed;
        public event OnRenamedHandler OnRenamed
        {
            add
            {
                this._onRenamed += value;
            }
            remove
            {
                this._onRenamed -= value;
            }
        }
        public string Caption
        {
            get
            {
                return this.treeListColumn_node.Caption;
            }
            set
            {
                this.treeListColumn_node.Caption = value;
            }
        }
        public System.Windows.Forms.IWin32Window Owner
        {
            get
            {
                return this._baseTree;
            }
        }
        public TreeList TreeList
        {
            get
            {
                return this._baseTree;
            }
        }
        public bool AllowDragLayer
        {
            get
            {
                return this._baseTree.OptionsBehavior.DragNodes;
            }
            set
            {
                this._baseTree.OptionsBehavior.DragNodes = value;
            }
        }
        public bool AllowMultiSelect
        {
            get
            {
                return this._baseTree.OptionsSelection.MultiSelect;
            }
            set
            {
                this._baseTree.OptionsSelection.MultiSelect = value;
            }
        }
        public bool ShowRootLine
        {
            get
            {
                return this._baseTree.OptionsView.ShowRoot;
            }
            set
            {
                this._baseTree.OptionsView.ShowRoot = value;
            }
        }
        public bool ShowLayerControlBar
        {
            get
            {
                return this.layerControl_bar.Visible;
            }
            set
            {
                this.layerControl_bar.Visible = value;
            }
        }
        public bool ShowQuickSearchBar
        {
            get
            {
                return this.quickSearch_bar.Visible;
            }
            set
            {
                this.quickSearch_bar.Visible = value;
            }
        }
        public bool ShowCheckBoxes
        {
            get
            {
                return this._baseTree.OptionsView.ShowCheckBoxes;
            }
            set
            {
                this._baseTree.OptionsView.ShowCheckBoxes = value;
            }
        }
        public bool ShowColorColumn
        {
            get
            {
                return this.treeListColumn_color.Visible;
            }
            set
            {
                this.treeListColumn_color.Visible = value;
            }
        }
        public bool ShowCustomColumn
        {
            get
            {
                return this.treeListColumn_Custom.Visible;
            }
            set
            {
                this.treeListColumn_Custom.Visible = value;
            }
        }
        public string CustomColumnCaption
        {
            get
            {
                return this.treeListColumn_Custom.Caption;
            }
            set
            {
                this.treeListColumn_Custom.Caption = value;
            }
        }
        public bool ShowColumnHeader
        {
            get
            {
                return this._baseTree.OptionsView.ShowColumns;
            }
            set
            {
                this._baseTree.OptionsView.ShowColumns = value;
            }
        }

        //private IBaseLayer RecursiveGetLayerByID(List<IBaseLayer> rLayers, string ID)
        //{
        //    if (rLayers == null) return null;
        //    foreach (IBaseLayer layer in rLayers)
        //    {
        //        if (layer.ID == ID) return layer;
        //        if (layer.LogicTree == null) continue;
        //        return RecursiveGetLayerByID(layer.LogicTree.GetRootLayers(), ID);
        //    }
        //    return null;
        //}

        //public IBaseLayer GetLayerByID(string ID)
        //{
        //    return RecursiveGetLayerByID(this.GetRootLayers(), ID);
        //}

        public System.Collections.Generic.List<IBaseLayer> GetRootLayers()
        {
            System.Collections.Generic.List<IBaseLayer> list = new System.Collections.Generic.List<IBaseLayer>();
            if (this._baseTree.Nodes.Count == 0)
            {
                return null;
            }
            foreach (TreeListNode treeListNode in this._baseTree.Nodes)
            {
                if (treeListNode.Tag != null)
                {
                    IBaseLayer baseLayer = treeListNode.Tag as IBaseLayer;
                    if (baseLayer != null)
                    {
                        list.Add(baseLayer);
                    }
                }
            }
            return list;
        }
        public IBaseLayer GetActiveLayer()
        {
            if (this._baseTree.FocusedNode == null || this._baseTree.FocusedNode.Tag == null)
            {
                return null;
            }
            return this._baseTree.FocusedNode.Tag as IBaseLayer;
        }
        public System.Collections.Generic.List<IBaseLayer> GetSelectedLayers()
        {
            System.Collections.Generic.List<IBaseLayer> list = null;
            TreeListMultiSelection selection = this._baseTree.Selection;
            if (selection != null && selection.Count > 0)
            {
                list = new System.Collections.Generic.List<IBaseLayer>();
                for (int i = 0; i < selection.Count; i++)
                {
                    TreeListNode treeListNode = selection[i];
                    if (treeListNode.Tag != null)
                    {
                        IBaseLayer baseLayer = treeListNode.Tag as IBaseLayer;
                        if (baseLayer != null)
                        {
                            list.Add(baseLayer);
                        }
                    }
                }
            }
            return list;
        }
        public virtual void ClearLayers()
        {
            if (this._baseTree.Nodes.Count < 1)
            {
                return;
            }
            TreeListNode treeListNode = this._baseTree.Nodes[0];
            if (treeListNode != null)
            {
                if (treeListNode.Tag != null)
                {
                    IGroupLayer groupLayer = treeListNode.Tag as IGroupLayer;
                    if (groupLayer != null)
                    {
                        groupLayer.Clear();
                    }
                }
                this._baseTree.Nodes.Clear();
            }
        }

        public virtual void CreateRootLayer(string layerName)
        {
            this.ExportXML(null);
        }
        public virtual void InitPopMenu()
        {
            this._onInitRootPopMenu();
        }
        public virtual void OnMenuItemClick(string caption)
        {
            this._onRootPopMenuItemClick(caption);
        }

        public virtual void OnMenuItemClick(ToolStripMenuItem item)
        {
        }

        public void OnMenuItemClick(object sender, EventArgs e)
        {
            Application.DoEvents();
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                if ((item.Tag == null) || (item.Tag.ToString() == string.Empty))
                {
                    this.OnMenuItemClick(item.Text);
                }
                else
                {
                    this.OnMenuItemClick(item.Tag.ToString());
                }
                this.OnMenuItemClick(item);
            }
        }

        public System.Collections.Generic.List<string> GetMenuItems(out System.Type type)
        {
            type = base.GetType();
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            foreach (System.Windows.Forms.ToolStripItem toolStripItem in this.PopMenu.Items)
            {
                if (!list.Contains(toolStripItem.Text))
                {
                    list.Add(toolStripItem.Text);
                }
            }
            return list;
        }
        public System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<string>> GetAllMenuItems(out System.Type type)
        {
            type = base.GetType();
            System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<string>> result = new System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<string>>();
            System.Collections.Generic.List<IBaseLayer> rootLayers = this.GetRootLayers();
            if (rootLayers == null || rootLayers.Count == 0)
            {
                return null;
            }
            foreach (IBaseLayer current in rootLayers)
            {
                this.GetLayerMenus(current, ref result);
            }
            return result;
        }
        private void GetLayerMenus(IBaseLayer layer, ref System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<string>> menuMap)
        {
            if (layer == null || menuMap == null)
            {
                return;
            }
            System.Type type = null;
            System.Collections.Generic.List<string> menuItems = layer.GetMenuItems(out type);
            if (type != null && menuItems != null && menuItems.Count > 0 && !menuMap.ContainsKey(type))
            {
                menuMap.Add(type, menuItems);
            }
            if (layer is IGroupLayer)
            {
                IGroupLayer groupLayer = layer as IGroupLayer;
                System.Collections.Generic.List<IBaseLayer> list = groupLayer.SelectAllSubLayers();
                if (list != null && list.Count > 0)
                {
                    foreach (IBaseLayer current in list)
                    {
                        this.GetLayerMenus(current, ref menuMap);
                    }
                }
            }
        }

        private string id = null;
        private string title = null;
        protected bool isActive = false;
        protected bool showCloseButton = false;
        private object currentRibbonPage;
        protected bool bAutoHide = false;
        protected string pos = null;

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public virtual bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }
        public bool ShowCloseButton
        {
            get
            {
                return showCloseButton;
            }
            set
            {
                showCloseButton = value;
            }
        }
        public object CurrentRibbonPage
        {
            get { return currentRibbonPage; }
        }
        public bool AutoHide
        {
            get
            {
                return bAutoHide;
            }
            set
            {
                bAutoHide = value;
            }
        }
        public string Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }
        private int pheight;
        public int PHeight
        {
            get
            {
                return pheight;
            }
            set
            {
                pheight = value;
            }
        }
        public virtual void Activate()
        {
            DFApplication.Application.Workbench.ActivateContent(this);
        }

        public virtual void SetCurrentRibbonPage(object ribbonPage)
        {
            this.currentRibbonPage = ribbonPage;
        }
    }
}
