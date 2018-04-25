using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using DF3DData.UserControl;
namespace DF3DData.Frm
{
    public class FormFeatureLayerProperty : XtraForm
    {
        private System.ComponentModel.IContainer components;
        private LayoutControl layoutControl1;
        private XtraTabControl xtraTabControl1;
        private XtraTabPage xtraTabPageProperty;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private SimpleButton btn_Cancel;
        private SimpleButton btn_OK;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControl layoutControl3;
        private LayoutControlGroup layoutControlGroup3;
        private ComboBoxEdit cmbGeoColumn;
        private TextEdit txtLayerName;
        private LayoutControlItem layoutControlItem6;
        private LayoutControlItem layoutControlItem7;
        private SpinEdit spinEditMinDis;
        private SpinEdit spinEditMaxDis;
        private LayoutControlItem layoutControlItem8;
        private LayoutControlItem layoutControlItem9;
        private CheckEdit checkEditCullFace;
        private LayoutControlItem layoutControlItem10;
        private EmptySpaceItem emptySpaceItem4;
        private XtraTabPage xtraTabPageSymbol;
        private LayoutControl layoutControl4;
        private ComboBoxEdit cmbGeoSchemeType;
        private LayoutControlGroup layoutControlGroup4;
        private LayoutControlItem layoutControlItem12;
        private PanelControl GeoSymbolPanelControl;
        private XtraTabPage xtraTabPageLabel;
        private LayoutControlItem layoutControlItem13;
        private LayoutControl layoutControl5;
        private LayoutControlGroup layoutControlGroup5;
        private ComboBoxEdit cmbLabelSchemeType;
        private ComboBoxEdit cmbLabelField;
        private LayoutControlItem layoutControlItem14;
        private LayoutControlItem layoutControlItem15;
        private PanelControl LabelSymbolPanelControl;
        private LayoutControlItem layoutControlItem16;
        private TextEdit txtDataSource;
        private LayoutControlItem layoutControlItem17;
        private ComboBoxEdit cmbCullFaceMode;
        private LayoutControlItem layoutControlItem18;
        private CheckEdit chkAutoAvoid;
        private CheckEdit chkDynamicLabel;
        private LayoutControlItem layoutControlItem21;
        private LayoutControlItem layoutControlItem23;
        private SpinEdit spin_HeightOffSet;
        private ComboBoxEdit cmbHeightStyle;
        private LayoutControlItem layoutControlItem4;
        private CheckEdit chkRemoveSameName;
        private LayoutControlItem layoutControlItem11;
        private LayoutControlItem layoutControlItem5;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageProperty = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbCullFaceMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDataSource = new DevExpress.XtraEditors.TextEdit();
            this.checkEditCullFace = new DevExpress.XtraEditors.CheckEdit();
            this.spinEditMinDis = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditMaxDis = new DevExpress.XtraEditors.SpinEdit();
            this.cmbGeoColumn = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtLayerName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPageSymbol = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.spin_HeightOffSet = new DevExpress.XtraEditors.SpinEdit();
            this.cmbHeightStyle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.GeoSymbolPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.cmbGeoSchemeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabPageLabel = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.chkRemoveSameName = new DevExpress.XtraEditors.CheckEdit();
            this.chkAutoAvoid = new DevExpress.XtraEditors.CheckEdit();
            this.chkDynamicLabel = new DevExpress.XtraEditors.CheckEdit();
            this.LabelSymbolPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.cmbLabelSchemeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbLabelField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCullFaceMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCullFace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMinDis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxDis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGeoColumn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            this.xtraTabPageSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_HeightOffSet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHeightStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeoSymbolPanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGeoSchemeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.xtraTabPageLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemoveSameName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoAvoid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDynamicLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LabelSymbolPanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLabelSchemeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLabelField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_OK);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(374, 497);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(192, 469);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(176, 22);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(6, 469);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(182, 22);
            this.btn_OK.StyleController = this.layoutControl1;
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(6, 6);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageProperty;
            this.xtraTabControl1.Size = new System.Drawing.Size(362, 459);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageProperty,
            this.xtraTabPageSymbol,
            this.xtraTabPageLabel});
            // 
            // xtraTabPageProperty
            // 
            this.xtraTabPageProperty.Controls.Add(this.layoutControl3);
            this.xtraTabPageProperty.Name = "xtraTabPageProperty";
            this.xtraTabPageProperty.Size = new System.Drawing.Size(356, 430);
            this.xtraTabPageProperty.Text = "属性";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.cmbCullFaceMode);
            this.layoutControl3.Controls.Add(this.txtDataSource);
            this.layoutControl3.Controls.Add(this.checkEditCullFace);
            this.layoutControl3.Controls.Add(this.spinEditMinDis);
            this.layoutControl3.Controls.Add(this.spinEditMaxDis);
            this.layoutControl3.Controls.Add(this.cmbGeoColumn);
            this.layoutControl3.Controls.Add(this.txtLayerName);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(393, 249, 250, 350);
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(356, 430);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // cmbCullFaceMode
            // 
            this.cmbCullFaceMode.Location = new System.Drawing.Point(153, 142);
            this.cmbCullFaceMode.Name = "cmbCullFaceMode";
            this.cmbCullFaceMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCullFaceMode.Properties.Items.AddRange(new object[] {
            "显示双面",
            "显示正面",
            "显示背面"});
            this.cmbCullFaceMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbCullFaceMode.Size = new System.Drawing.Size(191, 22);
            this.cmbCullFaceMode.StyleController = this.layoutControl3;
            this.cmbCullFaceMode.TabIndex = 6;
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(87, 12);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Properties.ReadOnly = true;
            this.txtDataSource.Size = new System.Drawing.Size(257, 22);
            this.txtDataSource.StyleController = this.layoutControl3;
            this.txtDataSource.TabIndex = 0;
            // 
            // checkEditCullFace
            // 
            this.checkEditCullFace.Location = new System.Drawing.Point(87, 142);
            this.checkEditCullFace.Name = "checkEditCullFace";
            this.checkEditCullFace.Properties.Caption = "";
            this.checkEditCullFace.Size = new System.Drawing.Size(62, 19);
            this.checkEditCullFace.StyleController = this.layoutControl3;
            this.checkEditCullFace.TabIndex = 5;
            this.checkEditCullFace.CheckedChanged += new System.EventHandler(this.checkEditCullFace_CheckedChanged);
            // 
            // spinEditMinDis
            // 
            this.spinEditMinDis.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditMinDis.Location = new System.Drawing.Point(87, 116);
            this.spinEditMinDis.Name = "spinEditMinDis";
            this.spinEditMinDis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMinDis.Size = new System.Drawing.Size(257, 22);
            this.spinEditMinDis.StyleController = this.layoutControl3;
            this.spinEditMinDis.TabIndex = 4;
            // 
            // spinEditMaxDis
            // 
            this.spinEditMaxDis.EditValue = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.spinEditMaxDis.Location = new System.Drawing.Point(87, 90);
            this.spinEditMaxDis.Name = "spinEditMaxDis";
            this.spinEditMaxDis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMaxDis.Size = new System.Drawing.Size(257, 22);
            this.spinEditMaxDis.StyleController = this.layoutControl3;
            this.spinEditMaxDis.TabIndex = 3;
            // 
            // cmbGeoColumn
            // 
            this.cmbGeoColumn.Location = new System.Drawing.Point(87, 64);
            this.cmbGeoColumn.Name = "cmbGeoColumn";
            this.cmbGeoColumn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGeoColumn.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGeoColumn.Size = new System.Drawing.Size(257, 22);
            this.cmbGeoColumn.StyleController = this.layoutControl3;
            this.cmbGeoColumn.TabIndex = 2;
            // 
            // txtLayerName
            // 
            this.txtLayerName.Location = new System.Drawing.Point(87, 38);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Properties.ReadOnly = true;
            this.txtLayerName.Size = new System.Drawing.Size(257, 22);
            this.txtLayerName.StyleController = this.layoutControl3;
            this.txtLayerName.TabIndex = 1;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.emptySpaceItem4,
            this.layoutControlItem17,
            this.layoutControlItem18});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(356, 430);
            this.layoutControlGroup3.Text = "layoutControlGroup3";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtLayerName;
            this.layoutControlItem6.CustomizationFormText = "图层名称";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem6.Text = "图层名称";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmbGeoColumn;
            this.layoutControlItem7.CustomizationFormText = "空间列";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem7.Text = "空间列";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.spinEditMaxDis;
            this.layoutControlItem8.CustomizationFormText = "最大可视距离";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem8.Text = "最大可视距离";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.spinEditMinDis;
            this.layoutControlItem9.CustomizationFormText = "最小可视距离";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem9.Text = "最小可视距离";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.checkEditCullFace;
            this.layoutControlItem10.CustomizationFormText = "显示模式";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem10.Text = "显示模式";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(72, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 156);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(336, 254);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.txtDataSource;
            this.layoutControlItem17.CustomizationFormText = "数据源";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem17.Text = "数据源";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.cmbCullFaceMode;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(141, 130);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(195, 26);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // xtraTabPageSymbol
            // 
            this.xtraTabPageSymbol.Controls.Add(this.layoutControl4);
            this.xtraTabPageSymbol.Name = "xtraTabPageSymbol";
            this.xtraTabPageSymbol.Size = new System.Drawing.Size(356, 430);
            this.xtraTabPageSymbol.Text = "符号化";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.spin_HeightOffSet);
            this.layoutControl4.Controls.Add(this.cmbHeightStyle);
            this.layoutControl4.Controls.Add(this.GeoSymbolPanelControl);
            this.layoutControl4.Controls.Add(this.cmbGeoSchemeType);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(356, 430);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // spin_HeightOffSet
            // 
            this.spin_HeightOffSet.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spin_HeightOffSet.Location = new System.Drawing.Point(63, 64);
            this.spin_HeightOffSet.Name = "spin_HeightOffSet";
            this.spin_HeightOffSet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spin_HeightOffSet.Size = new System.Drawing.Size(281, 22);
            this.spin_HeightOffSet.StyleController = this.layoutControl4;
            this.spin_HeightOffSet.TabIndex = 7;
            // 
            // cmbHeightStyle
            // 
            this.cmbHeightStyle.Location = new System.Drawing.Point(63, 38);
            this.cmbHeightStyle.Name = "cmbHeightStyle";
            this.cmbHeightStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHeightStyle.Properties.Items.AddRange(new object[] {
            "贴地模式",
            "绝对高程",
            "相对高程",
            "所有对象"});
            this.cmbHeightStyle.Size = new System.Drawing.Size(281, 22);
            this.cmbHeightStyle.StyleController = this.layoutControl4;
            this.cmbHeightStyle.TabIndex = 6;
            this.cmbHeightStyle.SelectedIndexChanged += new System.EventHandler(this.cmbHeightStyle_SelectedIndexChanged);
            // 
            // GeoSymbolPanelControl
            // 
            this.GeoSymbolPanelControl.Location = new System.Drawing.Point(12, 90);
            this.GeoSymbolPanelControl.Name = "GeoSymbolPanelControl";
            this.GeoSymbolPanelControl.Size = new System.Drawing.Size(332, 328);
            this.GeoSymbolPanelControl.TabIndex = 5;
            // 
            // cmbGeoSchemeType
            // 
            this.cmbGeoSchemeType.Location = new System.Drawing.Point(63, 12);
            this.cmbGeoSchemeType.Name = "cmbGeoSchemeType";
            this.cmbGeoSchemeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGeoSchemeType.Properties.Items.AddRange(new object[] {
            "无",
            "单一",
            "枚举",
            "分段"});
            this.cmbGeoSchemeType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGeoSchemeType.Size = new System.Drawing.Size(281, 22);
            this.cmbGeoSchemeType.StyleController = this.layoutControl4;
            this.cmbGeoSchemeType.TabIndex = 4;
            this.cmbGeoSchemeType.SelectedIndexChanged += new System.EventHandler(this.cmbGeoSchemeType_SelectedIndexChanged);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(356, 430);
            this.layoutControlGroup4.Text = "layoutControlGroup4";
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.cmbGeoSchemeType;
            this.layoutControlItem12.CustomizationFormText = "方案类型";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem12.Text = "方案类型";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.GeoSymbolPanelControl;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(336, 332);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbHeightStyle;
            this.layoutControlItem4.CustomizationFormText = "高程模式";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem4.Text = "高程模式";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spin_HeightOffSet;
            this.layoutControlItem5.CustomizationFormText = "高程偏移";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem5.Text = "高程偏移";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14);
            // 
            // xtraTabPageLabel
            // 
            this.xtraTabPageLabel.Controls.Add(this.layoutControl5);
            this.xtraTabPageLabel.Name = "xtraTabPageLabel";
            this.xtraTabPageLabel.Size = new System.Drawing.Size(356, 430);
            this.xtraTabPageLabel.Text = "标注";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.chkRemoveSameName);
            this.layoutControl5.Controls.Add(this.chkAutoAvoid);
            this.layoutControl5.Controls.Add(this.chkDynamicLabel);
            this.layoutControl5.Controls.Add(this.LabelSymbolPanelControl);
            this.layoutControl5.Controls.Add(this.cmbLabelSchemeType);
            this.layoutControl5.Controls.Add(this.cmbLabelField);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(356, 430);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // chkRemoveSameName
            // 
            this.chkRemoveSameName.Location = new System.Drawing.Point(63, 84);
            this.chkRemoveSameName.Name = "chkRemoveSameName";
            this.chkRemoveSameName.Properties.Caption = "";
            this.chkRemoveSameName.Size = new System.Drawing.Size(281, 19);
            this.chkRemoveSameName.StyleController = this.layoutControl5;
            this.chkRemoveSameName.TabIndex = 9;
            // 
            // chkAutoAvoid
            // 
            this.chkAutoAvoid.Location = new System.Drawing.Point(63, 61);
            this.chkAutoAvoid.Name = "chkAutoAvoid";
            this.chkAutoAvoid.Properties.Caption = "";
            this.chkAutoAvoid.Size = new System.Drawing.Size(281, 19);
            this.chkAutoAvoid.StyleController = this.layoutControl5;
            this.chkAutoAvoid.TabIndex = 8;
            // 
            // chkDynamicLabel
            // 
            this.chkDynamicLabel.Location = new System.Drawing.Point(63, 38);
            this.chkDynamicLabel.Name = "chkDynamicLabel";
            this.chkDynamicLabel.Properties.Caption = "";
            this.chkDynamicLabel.Size = new System.Drawing.Size(281, 19);
            this.chkDynamicLabel.StyleController = this.layoutControl5;
            this.chkDynamicLabel.TabIndex = 7;
            // 
            // LabelSymbolPanelControl
            // 
            this.LabelSymbolPanelControl.Location = new System.Drawing.Point(12, 133);
            this.LabelSymbolPanelControl.Name = "LabelSymbolPanelControl";
            this.LabelSymbolPanelControl.Size = new System.Drawing.Size(332, 285);
            this.LabelSymbolPanelControl.TabIndex = 6;
            // 
            // cmbLabelSchemeType
            // 
            this.cmbLabelSchemeType.Location = new System.Drawing.Point(63, 107);
            this.cmbLabelSchemeType.Name = "cmbLabelSchemeType";
            this.cmbLabelSchemeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLabelSchemeType.Properties.Items.AddRange(new object[] {
            "无",
            "单一",
            "枚举",
            "分段"});
            this.cmbLabelSchemeType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLabelSchemeType.Size = new System.Drawing.Size(281, 22);
            this.cmbLabelSchemeType.StyleController = this.layoutControl5;
            this.cmbLabelSchemeType.TabIndex = 5;
            this.cmbLabelSchemeType.SelectedIndexChanged += new System.EventHandler(this.cmbLabelSchemeType_SelectedIndexChanged);
            // 
            // cmbLabelField
            // 
            this.cmbLabelField.Location = new System.Drawing.Point(63, 12);
            this.cmbLabelField.Name = "cmbLabelField";
            this.cmbLabelField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLabelField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLabelField.Size = new System.Drawing.Size(281, 22);
            this.cmbLabelField.StyleController = this.layoutControl5;
            this.cmbLabelField.TabIndex = 4;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem21,
            this.layoutControlItem23,
            this.layoutControlItem11});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(356, 430);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.cmbLabelField;
            this.layoutControlItem14.CustomizationFormText = "标注字段";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem14.Text = "标注字段";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.cmbLabelSchemeType;
            this.layoutControlItem15.CustomizationFormText = "方案类型";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 95);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(336, 26);
            this.layoutControlItem15.Text = "方案类型";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.LabelSymbolPanelControl;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 121);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(336, 289);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.chkDynamicLabel;
            this.layoutControlItem21.CustomizationFormText = "动态标注";
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(336, 23);
            this.layoutControlItem21.Text = "动态标注";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.chkAutoAvoid;
            this.layoutControlItem23.CustomizationFormText = "自动避让";
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(336, 23);
            this.layoutControlItem23.Text = "自动避让";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chkRemoveSameName;
            this.layoutControlItem11.CustomizationFormText = "同名剔除";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(336, 23);
            this.layoutControlItem11.Text = "同名剔除";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlGroup1.Size = new System.Drawing.Size(374, 497);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(366, 463);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Cancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(186, 463);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_OK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 463);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // FormFeatureLayerProperty
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(374, 497);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormFeatureLayerProperty";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性";
            this.Load += new System.EventHandler(this.FormFeatureLayerProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageProperty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCullFaceMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCullFace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMinDis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxDis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGeoColumn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            this.xtraTabPageSymbol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spin_HeightOffSet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHeightStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeoSymbolPanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGeoSchemeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.xtraTabPageLabel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkRemoveSameName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoAvoid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDynamicLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LabelSymbolPanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLabelSchemeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLabelField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }
        private IFeatureClass _featureClass;
        private IFeatureLayer _featrueLayer;
        private gviGeometryColumnType _geoType;
        private UCSimpleLabel _ucSimpleLabel;
        private UCEnumLabel _ucEnumLabel;
        private UCRangeLabel _ucRangeLabel;
        private UCModelPointSymbol _ucModelPointSymbol;
        private UCPointSymbol _ucPointSymbol;
        private UCPolylineSymbol _ucPolylineSymbol;
        private UCPolygonSymbol _ucPolygonSymbol;
        private UCEnumSymbol _ucEnumSymbol;
        private UCRangeSymbol _ucRangeSymbol;

        public FormFeatureLayerProperty(IFeatureClass fc, IFeatureLayer fl)
        {
            this.InitializeComponent();
            this._featureClass = fc;
            this._featrueLayer = fl;
            this._geoType = this._featrueLayer.GeometryType;
        }

        private void FormFeatureLayerProperty_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtDataSource.Text = this._featureClass.DataSource.ConnectionInfo.ToConnectionString();
                this.txtLayerName.Text = this._featureClass.AliasName;

                IFieldInfoCollection fields = this._featureClass.GetFields();
                for (int i = 0; i < fields.Count; i++)
                {
                    IFieldInfo fieldInfo = fields.Get(i);
                    if (fieldInfo.FieldType == gviFieldType.gviFieldGeometry)
                    {
                        string name = fieldInfo.Name;
                        int selectedIndex = this.cmbGeoColumn.Properties.Items.Add(name);
                        if (name == this._featrueLayer.GeometryFieldName)
                        {
                            this.cmbGeoColumn.SelectedIndex = selectedIndex;
                            break;
                        }
                    }
                }
                #region
                this.spinEditMaxDis.Text = this._featrueLayer.MaxVisibleDistance.ToString();
                this.spinEditMinDis.Text = this._featrueLayer.MinVisibleDistance.ToString();
                if (!this._featrueLayer.ForceCullMode)
                {
                    this.checkEditCullFace.Checked = false;
                    this.cmbCullFaceMode.Enabled = false;
                }
                else
                {
                    this.checkEditCullFace.Checked = true;
                    this.cmbCullFaceMode.Enabled = true;
                    switch (this._featrueLayer.CullMode)
                    {
                        case gviCullFaceMode.gviCullFront:
                            this.cmbCullFaceMode.Text = "显示正面";
                            break;
                        case gviCullFaceMode.gviCullNone:
                            this.cmbCullFaceMode.Text = "显示双面";
                            break;
                        case gviCullFaceMode.gviCullBack:
                        default:
                            this.cmbCullFaceMode.Text = "显示背面";
                            break;
                    }
                }
                #endregion

                #region
                IGeometryRender geoRender = this._featrueLayer.GetGeometryRender();
                if (geoRender == null)
                {
                    this.cmbGeoSchemeType.Text = "无";
                    this.cmbHeightStyle.Enabled = false;
                    this.spin_HeightOffSet.Enabled = false;
                }
                else
                {
                    this.cmbHeightStyle.Enabled = true;
                    this.spin_HeightOffSet.Enabled = true;
                    switch (geoRender.HeightStyle)
                    {
                        case gviHeightStyle.gviHeightOnTerrain:
                            this.cmbHeightStyle.Text = "贴地模式";
                            this.spin_HeightOffSet.Text = "0";
                            break;
                        case gviHeightStyle.gviHeightAbsolute:
                            this.cmbHeightStyle.Text = "绝对高程";
                            this.spin_HeightOffSet.Text = geoRender.HeightOffset.ToString();
                            break;
                        case gviHeightStyle.gviHeightRelative:
                            this.cmbHeightStyle.Text = "相对高程";
                            this.spin_HeightOffSet.Text = geoRender.HeightOffset.ToString();
                            break;
                        case gviHeightStyle.gviHeightOnEverything:
                            this.cmbHeightStyle.Text = "所有对象";
                            this.spin_HeightOffSet.Text = "0";
                            break;
                    }
                    switch (geoRender.RenderType)
                    {
                        case gviRenderType.gviRenderSimple:
                            this.cmbGeoSchemeType.Text = "单一";
                            break;
                        case gviRenderType.gviRenderToolTip:
                            this.cmbGeoSchemeType.Text = "枚举";
                            break;
                        case gviRenderType.gviRenderValueMap:
                            this.cmbGeoSchemeType.Text = "分段";
                            break;
                        default:
                            this.cmbGeoSchemeType.Text = "无";
                            break;
                    }
                }
                #endregion

                #region
                ITextRender textRender = this._featrueLayer.GetTextRender();
                if (textRender == null)
                {
                    this.cmbLabelSchemeType.Text = "无";
                    this.cmbLabelField.Enabled = false;
                    this.chkDynamicLabel.Enabled = false;
                    this.chkAutoAvoid.Enabled = false;
                    this.chkRemoveSameName.Enabled = false;
                }
                else
                {
                    this.cmbLabelField.Enabled = true;
                    this.chkDynamicLabel.Enabled = true;
                    this.chkAutoAvoid.Enabled = true;
                    this.chkRemoveSameName.Enabled = true;
                    if (textRender.DynamicPlacement) this.chkDynamicLabel.Checked = true;
                    else this.chkDynamicLabel.Checked = false;
                    if (textRender.MinimizeOverlap) this.chkAutoAvoid.Checked = true;
                    else this.chkAutoAvoid.Checked = false;
                    if (textRender.RemoveDuplicate) this.chkRemoveSameName.Checked = true;
                    else this.chkRemoveSameName.Checked = false;

                    switch (textRender.RenderType)
                    {
                        case gviRenderType.gviRenderSimple:
                            this.cmbLabelSchemeType.Text = "单一";
                            break;
                        case gviRenderType.gviRenderToolTip:
                            this.cmbLabelSchemeType.Text = "枚举";
                            break;
                        case gviRenderType.gviRenderValueMap:
                            this.cmbLabelSchemeType.Text = "分段";
                            break;
                        default:
                            this.cmbLabelSchemeType.Text = "无";
                            break;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void checkEditCullFace_CheckedChanged(object sender, System.EventArgs e)
        {
            this.cmbCullFaceMode.Enabled = this.checkEditCullFace.Checked;
            if (this.checkEditCullFace.Checked) this.cmbCullFaceMode.Text = "显示背面";
            else this.cmbCullFaceMode.Text = "";
        }
        private void cmbHeightStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbHeightStyle.Text == "")
            {
                this.spin_HeightOffSet.Enabled = false;
                return;
            }
            switch (this.cmbHeightStyle.Text)
            {
                case "贴地模式":
                case "所有对象":
                    this.spin_HeightOffSet.Enabled = false;
                    this.spin_HeightOffSet.Text = "0";
                    break;
                case "绝对高程":
                case "相对高程":
                    this.spin_HeightOffSet.Enabled = true;
                    break;
            }
        }
        private void cmbGeoSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cmbGeoSchemeType.Text == "无" || string.IsNullOrEmpty(this.cmbGeoSchemeType.Text))
            {
                this.cmbHeightStyle.Enabled = false;
                this.spin_HeightOffSet.Enabled = false;
                this.spin_HeightOffSet.Text = "0";
                this.GeoSymbolPanelControl.Controls.Clear();
            }
            else
            {
                this.GeoSymbolPanelControl.Controls.Clear();
                this.cmbHeightStyle.Enabled = true;
                this.spin_HeightOffSet.Enabled = true;
                switch (this.cmbGeoSchemeType.Text)
                {
                    case "单一":
                        switch (this._geoType)
                        {
                            case gviGeometryColumnType.gviGeometryColumnPoint:
                                this._ucPointSymbol = new UCPointSymbol();
                                this._ucPointSymbol.Dock = DockStyle.Fill;
                                this.GeoSymbolPanelControl.Controls.Add(this._ucPointSymbol);
                                break;
                            case gviGeometryColumnType.gviGeometryColumnPolygon:
                                this._ucPolygonSymbol = new UCPolygonSymbol();
                                this._ucPolygonSymbol.Dock = DockStyle.Fill;
                                this.GeoSymbolPanelControl.Controls.Add(this._ucPolygonSymbol);
                                break;
                            case gviGeometryColumnType.gviGeometryColumnPolyline:
                                this._ucPolylineSymbol = new UCPolylineSymbol();
                                this._ucPolylineSymbol.Dock = DockStyle.Fill;
                                this.GeoSymbolPanelControl.Controls.Add(this._ucPolylineSymbol);
                                break;
                            case gviGeometryColumnType.gviGeometryColumnModelPoint:
                                this._ucModelPointSymbol = new UCModelPointSymbol();
                                this._ucModelPointSymbol.Dock = DockStyle.Fill;
                                this.GeoSymbolPanelControl.Controls.Add(this._ucModelPointSymbol);
                                break;
                        }
                        break;
                    case "枚举":
                        this._ucEnumSymbol = new UCEnumSymbol(this._geoType);
                        this._ucEnumSymbol.Dock = DockStyle.Fill;
                        this.GeoSymbolPanelControl.Controls.Add(this._ucEnumSymbol);
                        break;
                    case "分段":
                        this._ucRangeSymbol = new UCRangeSymbol(this._geoType);
                        this._ucRangeSymbol.Dock = DockStyle.Fill;
                        this.GeoSymbolPanelControl.Controls.Add(this._ucRangeSymbol);
                        break;
                }
            }
        }
        private void cmbLabelSchemeType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbLabelSchemeType.Text == "无" || string.IsNullOrEmpty(this.cmbLabelSchemeType.Text))
            {
                this.cmbLabelField.Enabled = false;
                this.chkDynamicLabel.Enabled = false;
                this.chkAutoAvoid.Enabled = false;
                this.chkRemoveSameName.Enabled = false;
                this.LabelSymbolPanelControl.Controls.Clear();
            }
            else
            {
                this.LabelSymbolPanelControl.Controls.Clear();
                this.cmbLabelField.Enabled = true;
                this.chkDynamicLabel.Enabled = true;
                this.chkAutoAvoid.Enabled = true;
                this.chkRemoveSameName.Enabled = true;
                switch (this.cmbLabelSchemeType.Text)
                {
                    case "单一":
                        this._ucSimpleLabel = new UCSimpleLabel();
                        this._ucSimpleLabel.Dock = DockStyle.Fill;
                        this.LabelSymbolPanelControl.Controls.Add(this._ucSimpleLabel);
                        break;
                    case "枚举":
                        this._ucEnumLabel = new UCEnumLabel();
                        this._ucEnumLabel.Dock = DockStyle.Fill;
                        this.LabelSymbolPanelControl.Controls.Add(this._ucEnumLabel);
                        break;
                    case "分段":
                        this._ucRangeLabel = new UCRangeLabel();
                        this._ucRangeLabel.Dock = DockStyle.Fill;
                        this.LabelSymbolPanelControl.Controls.Add(this._ucRangeLabel);
                        break;
                }
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            #region
            this._featrueLayer.MaxVisibleDistance = (double)this.spinEditMaxDis.Value;
            this._featrueLayer.MinVisibleDistance = (double)this.spinEditMinDis.Value;
            if (this.checkEditCullFace.Checked)
            {
                this._featrueLayer.ForceCullMode = true;
                switch (this.cmbCullFaceMode.Text)
                {
                    case "显示背面":
                        this._featrueLayer.CullMode = gviCullFaceMode.gviCullBack;
                        break;
                    case "显示正面":
                        this._featrueLayer.CullMode = gviCullFaceMode.gviCullFront;
                        break;
                    case "显示双面":
                        this._featrueLayer.CullMode = gviCullFaceMode.gviCullNone;
                        break;
                }
            }
            else
            {
                this._featrueLayer.ForceCullMode = false;
            }
            #endregion

            #region
            IGeometryRender geoRender = null;
            if (this.cmbGeoSchemeType.Text == "无" || string.IsNullOrEmpty(this.cmbGeoSchemeType.Text))
            {
                geoRender = null;
            }
            else if( this.cmbGeoSchemeType.Text == "单一")
            {
                geoRender = new SimpleGeometryRender();
            }
            else if (this.cmbGeoSchemeType.Text == "分段" || this.cmbGeoSchemeType.Text == "枚举")
            {
                geoRender = new ValueMapGeometryRender();
            }
            #endregion

            #region
            ITextRender textRender = null;
            if (this.cmbLabelSchemeType.Text == "无" || string.IsNullOrEmpty(this.cmbLabelSchemeType.Text))
            {
                textRender = null;
            }
            else if (this.cmbLabelSchemeType.Text == "单一")
            {
                textRender = new SimpleTextRender();
            }
            else if (this.cmbLabelSchemeType.Text == "分段" || this.cmbLabelSchemeType.Text == "枚举")
            {
                textRender = new ValueMapTextRender();
            }
            #endregion

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
