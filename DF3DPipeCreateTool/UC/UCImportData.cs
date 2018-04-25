using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeDataInterop;
using Gvitech.CityMaker.FdeCore;
using System.IO;
using DF3DPipeCreateTool.Class;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Columns;
using System.Runtime.InteropServices;
using DFDataConfig.Class;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCImportData : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_Name;
        private SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private TextEdit txtFcName;
        private TextEdit txtDsName;
        private TextEdit txtDate;
        private TextEdit txtLayerName;
        private ComboBoxEdit cbxGeoType;
        private TextEdit txtSource;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraBars.BarManager barManager1;
        private IContainer components;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiImportShp;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_Object;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private LabelControl lblRecord;
        private SimpleButton btnLast;
        private SimpleButton btnFirst;
        private TextEdit txtNum;
        private SimpleButton btnNext;
        private SimpleButton btnPre;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
    

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCImportData));
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiImportShp = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRecord = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.txtNum = new DevExpress.XtraEditors.TextEdit();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnPre = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.txtFcName = new DevExpress.XtraEditors.TextEdit();
            this.txtDsName = new DevExpress.XtraEditors.TextEdit();
            this.txtDate = new DevExpress.XtraEditors.TextEdit();
            this.txtLayerName = new DevExpress.XtraEditors.TextEdit();
            this.cbxGeoType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSource = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFcName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDsName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGeoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(373, 439);
            this.layoutControlGroup4.Text = "layoutControlGroup4";
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "数据库视图";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem12,
            this.layoutControlItem11,
            this.layoutControlItem10});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(373, 439);
            this.layoutControlGroup2.Text = "数据库视图";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(367, 387);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(5, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(363, 383);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
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
            this.bbiImportShp});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiImportShp)});
            this.bar2.Text = "Main menu";
            // 
            // bbiImportShp
            // 
            this.bbiImportShp.Caption = "导入shp";
            this.bbiImportShp.Id = 0;
            this.bbiImportShp.ImageIndex = 0;
            this.bbiImportShp.Name = "bbiImportShp";
            this.bbiImportShp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiImportShp_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(586, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 463);
            this.barDockControlBottom.Size = new System.Drawing.Size(586, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 439);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(586, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 439);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "btnNewLocation.Glyph.png");
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(232, 387);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(135, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblRecord;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 387);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblRecord
            // 
            this.lblRecord.Location = new System.Drawing.Point(5, 412);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblRecord.Size = new System.Drawing.Size(60, 16);
            this.lblRecord.StyleController = this.layoutControl4;
            this.lblRecord.TabIndex = 15;
            this.lblRecord.Text = "定位记录：";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.lblRecord);
            this.layoutControl4.Controls.Add(this.btnLast);
            this.layoutControl4.Controls.Add(this.btnFirst);
            this.layoutControl4.Controls.Add(this.txtNum);
            this.layoutControl4.Controls.Add(this.btnNext);
            this.layoutControl4.Controls.Add(this.btnPre);
            this.layoutControl4.Controls.Add(this.gridControl1);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(373, 439);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(204, 412);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(29, 22);
            this.btnLast.StyleController = this.layoutControl4;
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = ">>";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(69, 412);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(29, 22);
            this.btnFirst.StyleController = this.layoutControl4;
            this.btnFirst.TabIndex = 13;
            this.btnFirst.Text = "<<";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtNum
            // 
            this.txtNum.EditValue = "0";
            this.txtNum.Location = new System.Drawing.Point(126, 412);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(50, 22);
            this.txtNum.StyleController = this.layoutControl4;
            this.txtNum.TabIndex = 12;
            this.txtNum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNum_KeyUp);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(180, 412);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(20, 22);
            this.btnNext.StyleController = this.layoutControl4;
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = ">";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(102, 412);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(20, 22);
            this.btnPre.StyleController = this.layoutControl4;
            this.btnPre.TabIndex = 10;
            this.btnPre.Text = "<";
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnNext;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(175, 387);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(24, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnPre;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(97, 387);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(24, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtNum;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(121, 387);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(54, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnFirst;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(64, 387);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(33, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnLast;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(199, 387);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(33, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this.treeList1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_Object});
            this.treeList1.Location = new System.Drawing.Point(5, 25);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowRoot = false;
            this.treeList1.Size = new System.Drawing.Size(198, 227);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // tl_Name
            // 
            this.tl_Name.Caption = "名称";
            this.tl_Name.FieldName = "Name";
            this.tl_Name.MinWidth = 49;
            this.tl_Name.Name = "tl_Name";
            this.tl_Name.OptionsColumn.AllowEdit = false;
            this.tl_Name.OptionsColumn.AllowSort = false;
            this.tl_Name.OptionsFilter.AllowFilter = false;
            this.tl_Name.Visible = true;
            this.tl_Name.VisibleIndex = 0;
            // 
            // tl_Object
            // 
            this.tl_Object.Caption = "对象";
            this.tl_Object.FieldName = "Object";
            this.tl_Object.Name = "tl_Object";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl4);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(586, 439);
            this.splitContainerControl1.SplitterPosition = 208;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.treeList1);
            this.layoutControl3.Controls.Add(this.txtFcName);
            this.layoutControl3.Controls.Add(this.txtDsName);
            this.layoutControl3.Controls.Add(this.txtDate);
            this.layoutControl3.Controls.Add(this.txtLayerName);
            this.layoutControl3.Controls.Add(this.cbxGeoType);
            this.layoutControl3.Controls.Add(this.txtSource);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(208, 439);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // txtFcName
            // 
            this.txtFcName.Enabled = false;
            this.txtFcName.Location = new System.Drawing.Point(68, 308);
            this.txtFcName.Name = "txtFcName";
            this.txtFcName.Size = new System.Drawing.Size(135, 22);
            this.txtFcName.StyleController = this.layoutControl3;
            this.txtFcName.TabIndex = 4;
            // 
            // txtDsName
            // 
            this.txtDsName.Enabled = false;
            this.txtDsName.Location = new System.Drawing.Point(68, 282);
            this.txtDsName.Name = "txtDsName";
            this.txtDsName.Size = new System.Drawing.Size(135, 22);
            this.txtDsName.StyleController = this.layoutControl3;
            this.txtDsName.TabIndex = 5;
            // 
            // txtDate
            // 
            this.txtDate.Enabled = false;
            this.txtDate.Location = new System.Drawing.Point(68, 412);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(135, 22);
            this.txtDate.StyleController = this.layoutControl3;
            this.txtDate.TabIndex = 9;
            // 
            // txtLayerName
            // 
            this.txtLayerName.Enabled = false;
            this.txtLayerName.Location = new System.Drawing.Point(68, 334);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new System.Drawing.Size(135, 22);
            this.txtLayerName.StyleController = this.layoutControl3;
            this.txtLayerName.TabIndex = 6;
            // 
            // cbxGeoType
            // 
            this.cbxGeoType.Enabled = false;
            this.cbxGeoType.Location = new System.Drawing.Point(68, 386);
            this.cbxGeoType.Name = "cbxGeoType";
            this.cbxGeoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxGeoType.Size = new System.Drawing.Size(135, 22);
            this.cbxGeoType.StyleController = this.layoutControl3;
            this.cbxGeoType.TabIndex = 10;
            // 
            // txtSource
            // 
            this.txtSource.Enabled = false;
            this.txtSource.Location = new System.Drawing.Point(68, 360);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(135, 22);
            this.txtSource.StyleController = this.layoutControl3;
            this.txtSource.TabIndex = 7;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup6,
            this.layoutControlGroup1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(208, 439);
            this.layoutControlGroup3.Text = "layoutControlGroup3";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "临时图层";
            this.layoutControlGroup6.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(208, 257);
            this.layoutControlGroup6.Text = "临时图层";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.treeList1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(202, 231);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "图层信息";
            this.layoutControlGroup1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup1.ExpandButtonVisible = true;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 257);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(208, 182);
            this.layoutControlGroup1.Text = "图层信息";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtFcName;
            this.layoutControlItem4.CustomizationFormText = "要素类：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(202, 26);
            this.layoutControlItem4.Text = "要素类：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtLayerName;
            this.layoutControlItem6.CustomizationFormText = "图层名：";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(202, 26);
            this.layoutControlItem6.Text = "图层名：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtSource;
            this.layoutControlItem7.CustomizationFormText = "来源：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(202, 26);
            this.layoutControlItem7.Text = "来源：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cbxGeoType;
            this.layoutControlItem8.CustomizationFormText = "几何类型：";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(202, 26);
            this.layoutControlItem8.Text = "几何类型：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtDate;
            this.layoutControlItem9.CustomizationFormText = "创建日期：";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(202, 26);
            this.layoutControlItem9.Text = "创建日期：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtDsName;
            this.layoutControlItem5.CustomizationFormText = "数据集：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(202, 26);
            this.layoutControlItem5.Text = "数据集：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // UCImportData
            // 
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCImportData";
            this.Size = new System.Drawing.Size(586, 463);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFcName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDsName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxGeoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        private IDataSource _ds;
        private ContextMenuStrip _layermenu;
        private DataTable _dt;
        private GvitchCopyCallBack callback;
        private string currentFileName;

        public UCImportData()
        {
            InitializeComponent();
            this._layermenu = new ContextMenuStrip();
            this._layermenu.Items.Add("查看属性").Click += new EventHandler(this.menuItemViewAttr_Click);
            this._layermenu.Items.Add("删除图层").Click += new EventHandler(this.menuItemDel_Click);
            this._layermenu.Items.Add("导出SHP").Click += new EventHandler(this.menuItemExportSHP_Click);
            this.callback = new GvitchCopyCallBack();
            this.callback.Replicating += new CopyReplicatingHandler(this.OnCopyProgress);

        }

        public void Init()
        {
            //try
            //{
            //    ILicenseServer lic = new LicenseServer();
            //    long pval1;
            //    bool pval2;
            //    lic.InternalGetData(out pval1, out pval2);
            //    if (!pval2)
            //    {
            //        this.Enabled = false;
            //        XtraMessageBox.Show("此功能需要授权。", "提示");
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Enabled = false;
            //    XtraMessageBox.Show("此功能需要授权。", "提示");
            //    return;
            //}
            try
            {
                this._ds = DF3DPipeCreateApp.App.TempLib;
                if (this._ds == null) { this.Enabled = false; return; }

                if (this._dt != null) { this._dt.Rows.Clear(); this._dt.Columns.Clear(); this._dt.Clear(); }
                else this._dt = new DataTable();

                WaitForm.Start("正在加载数据...", "请稍后");
                this.treeList1.ClearNodes();
                string[] fdsNames = this._ds.GetFeatureDatasetNames();
                if (fdsNames != null)
                {
                    foreach (string fdsName in fdsNames)
                    {
                        IFeatureDataSet fds = this._ds.OpenFeatureDataset(fdsName);
                        string[] fcNames = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                        if (fcNames != null)
                        {
                            foreach (string fcName in fcNames)
                            {
                                IFeatureClass fc = fds.OpenFeatureClass(fcName);
                                this.treeList1.AppendNode(new object[] { fc.AliasName, fc }, null);
                            }
                        }
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

        private bool OnCopyProgress(GvitechFeatureProgress Progress)
        {
            int curCount = Progress.CurrentFeatureCount;
            int totalFeatureCount = Progress.TotalFeatureCount;
            WaitForm.SetCaption("正在导入【" + currentFileName + "】数据(" + curCount + "/" + totalFeatureCount + ")...");
            return true;
        }

        private void bbiImportShp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Shape File(*.shp)|*.shp",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] fileNames = dialog.FileNames;
                if ((fileNames != null) && (fileNames.Length != 0))
                {
                    WaitForm.Start("开始导入数据...", "请稍后", new Size(300, 50));
                    try
                    {
                        IDataInteropFactory interopFact = new DataInteropFactoryClass();
                        int total = 0;
                        int filecount = 0;
                        foreach (string fileName in fileNames)
                        {
                            IPropertySet ps = new PropertySetClass();
                            ps.SetProperty("FILENAME", fileName);
                            IDataInterop di = interopFact.CreateDataInterop(gviDataConnectionType.gviOgrConnectionShp, ps);//需要标准Runtime授权
                            di.StepValue = 100; 
                            di.OnProcessing = this.callback;
                            long count = di.GetCount(null);
                            if ((count == 0) || (di.LayersInfo.Count == 0))
                            {

                            }
                            else
                            {
                                IFieldInfoCollection fields = di.LayersInfo[0].FieldInfos;
                                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                                currentFileName = fileNameWithoutExtension;
                                WaitForm.SetCaption("正在导入【" + currentFileName + "】数据...");
                                string name = "shp_" + DateTime.Now.Ticks;
                                IFeatureClass oC = this._ds.OpenFeatureDataset("FeatureDataSet").CreateFeatureClass(name, fields);
                                if (oC != null)
                                {
                                    oC.AliasName = fileNameWithoutExtension;
                                    int count1 = di.ImportLayer(oC, "Geometry", null);
                                    total += count1;
                                    filecount++;
                                    oC.LockType = gviLockType.gviLockExclusiveSchema;
                                    if (fields.IndexOf("GroupId") != -1)
                                    {
                                        oC.GetFields().Get(oC.GetFields().IndexOf("GroupId")).Nullable = false;
                                        oC.GetFields().Get(oC.GetFields().IndexOf("GroupId")).RegisteredRenderIndex = true;
                                    }
                                    else
                                    {
                                        IFieldInfo field = new FieldInfoClass
                                        {
                                            Name = "GroupId",
                                            FieldType = gviFieldType.gviFieldInt32,
                                            Nullable = false,
                                            DefaultValue = 0,
                                            RegisteredRenderIndex = true
                                        };
                                        oC.AddField(field);
                                    }
                                    IGridIndexInfo indexInfo = new GridIndexInfoClass
                                    {
                                        L1 = 500.0,
                                        L2 = 2000.0,
                                        L3 = 10000.0,
                                        GeoColumnName = "Geometry"
                                    };
                                    oC.AddSpatialIndex(indexInfo);
                                    IRenderIndexInfo info3 = new RenderIndexInfoClass
                                    {
                                        L1 = 500.0,
                                        GeoColumnName = "Geometry"
                                    };
                                    oC.AddRenderIndex(info3);
                                    oC.LockType = gviLockType.gviLockSharedSchema;

                                    this.treeList1.AppendNode(new object[] { oC.AliasName, oC }, null);
                                }

                            }

                        }
                        XtraMessageBox.Show("导入完成，共导入文件【" + filecount + "/" + fileNames.Length + "】个，记录【" + total + "】条！", "提示");
                    }
                    catch (Exception ex)
                    {
                    }
                    WaitForm.Stop();
                }
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (this.treeList1.FocusedNode == null)
            {
                this.txtDsName.Text = "";
                this.txtFcName.Text = "";
                this.txtLayerName.Text = "";
                this.txtSource.Text = "";
                this.txtDate.Text = "";
            }
            else
            {
                object obj = this.treeList1.FocusedNode.GetValue("Object");
                if (obj is IFeatureClass)
                {
                    IFeatureClass fc = obj as IFeatureClass;
                    this.txtDsName.Text = fc.FeatureDataSet.Alias;
                    this.txtFcName.Text = fc.Name;
                    this.txtLayerName.Text = fc.AliasName;
                    this.txtDate.Text = fc.CreateTime.ToString();
                    this.txtSource.Text = fc.DataSource.ConnectionInfo.ToConnectionString();
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.gridView1.MoveFirst();
            this.txtNum.Text = (this.gridView1.FocusedRowHandle + 1).ToString();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.gridView1.MovePrev();
            this.txtNum.Text = (this.gridView1.FocusedRowHandle + 1).ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.gridView1.MoveNext();
            this.txtNum.Text = (this.gridView1.FocusedRowHandle + 1).ToString();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.gridView1.MoveLast();
            this.txtNum.Text = (this.gridView1.FocusedRowHandle + 1).ToString();
        }

        private void txtNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                int num;
                this.gridView1.ClearSelection();
                this.gridView1.FocusedRowHandle = int.TryParse(this.txtNum.Text, out num) ? num : 0;
                this.txtNum.Text = (this.gridView1.FocusedRowHandle + 1).ToString();
            }
        }

        private void ShowData(IFeatureClass table)
        {
            IFdeCursor cur = null;
            IQueryFilter qf = null;
            IRowBuffer row = null;
            try
            {
                WaitForm.Start("加载临时图层数据未开始...", "请稍候");
                IFieldInfoCollection fiCol = table.GetFields();

                for (int i = 0; i < fiCol.Count; i++)
                {
                    IFieldInfo fi = fiCol.Get(i);                    
                    string finame = fi.Name;
                    string fialias = string.IsNullOrEmpty(fi.Alias) ? fi.Name : fi.Alias;
                    if (fi.GeometryDef != null) finame = "*" + fi.Name;

                    DataColumn column = new DataColumn();
                    column.ColumnName = finame;
                    this._dt.Columns.Add(column);

                    GridColumn gColumn = new GridColumn();
                    gColumn.Caption = fialias;
                    gColumn.Name = finame;
                    gColumn.VisibleIndex = i;
                    gColumn.Visible = true;
                    this.gridView1.Columns.Add(gColumn);
                }

                qf = new QueryFilter();
                qf.WhereClause = "1=1";
                int count = table.GetCount(qf);
                if (count == 0) return;
                cur = table.Search(qf, false);
                row = null;
                int num = 0;
                while ((row = cur.NextRow()) != null)
                {
                    WaitForm.SetCaption(string.Format(@"正在加载第({0}/{1})条数据...", num++, count), table.AliasName);
                    DataRow dr = this._dt.NewRow();
                    for (int i = 0; i < this._dt.Columns.Count; i++)
                    {
                        if (this._dt.Columns[i].ColumnName.Contains("*")) { dr[this._dt.Columns[i].ColumnName] = this._dt.Columns[i].ColumnName; continue; }
                        if (row.GetValue(row.FieldIndex(this._dt.Columns[i].ColumnName)) == null) { dr[this._dt.Columns[i].ColumnName] = "null"; continue; }
                        dr[this._dt.Columns[i].ColumnName] = row.GetValue(row.FieldIndex(this._dt.Columns[i].ColumnName)).ToString();
                    }
                    this._dt.Rows.Add(dr);
                }
                this.gridControl1.DataSource = this._dt;
                this.gridView1.PopulateColumns();
            }
            catch (COMException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            finally
            {
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
                if (cur != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cur);
                    cur = null;
                }
                if (qf != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(qf);
                    qf = null;
                }
                WaitForm.Stop();
            }
        }

        private void menuItemViewAttr_Click(object sender, EventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;
            object obj = this.treeList1.FocusedNode.GetValue("Object");
            if (obj != null && obj is IFeatureClass)
            {
                this._dt.Rows.Clear(); this._dt.Columns.Clear(); this._dt.Clear();
                IFeatureClass fc = obj as IFeatureClass;
                ShowData(fc);
            }
        }
        private bool DeleteFeatureClass(IFeatureClass fc)
        {
            try
            {
                if (this._ds == null) return false;
                IFeatureDataSet fds = this._ds.OpenFeatureDataset(fc.FeatureDataSet.Name);
                if (fds == null) return false;
                return fds.DeleteByName(fc.Name);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void menuItemDel_Click(object sender, EventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;
            object obj = this.treeList1.FocusedNode.GetValue("Object");
            if (obj != null && obj is IFeatureClass)
            {
                IFeatureClass fc = obj as IFeatureClass;
                if (DeleteFeatureClass(fc))
                    this.treeList1.DeleteNode(this.treeList1.FocusedNode);
            }
        }

        private void menuItemExportSHP_Click(object sender, EventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;
            object obj = this.treeList1.FocusedNode.GetValue("Object");
            if (obj != null && obj is IFeatureClass)
            {
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    Filter = "Shp Files(.shp)|*.shp",
                    DefaultExt = "shp",
                    RestoreDirectory = true
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        IFeatureClass fc = obj as IFeatureClass;
                        IDataInteropFactory interopFact = new DataInteropFactory();
                        IPropertySet ps = new PropertySet();
                        ps.SetProperty("FILENAME ", dlg.FileName);
                        IDataInterop di = interopFact.CreateDataInterop(gviDataConnectionType.gviOgrConnectionShp, ps);
                        int count = -1;
                        if ((count = di.ExportLayer(fc, null, "Geometry")) >= 0)
                        {
                            XtraMessageBox.Show("成功导出SHP文件,导出" + count + "个记录！", "提示");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                TreeList control = sender as TreeList;
                if (control != null && e.Button == MouseButtons.Right  && e.Clicks == 1 && Control.ModifierKeys == Keys.None && control.State == TreeListState.Regular)
                {
                    TreeListHitInfo info = control.CalcHitInfo(e.Location);
                    if (info != null)
                    {
                        TreeListNode node = info.Node;
                        this.treeList1.SetFocusedNode(node);
                        if (node != null)
                        {
                            object obj = node.GetValue("Object");
                            if (obj != null && obj is IFeatureClass)
                            {
                                this._layermenu.Show(control, new Point(e.X, e.Y));
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.txtNum.Text = (this.gridView1.FocusedRowHandle + 1).ToString();
        }

    }
}
