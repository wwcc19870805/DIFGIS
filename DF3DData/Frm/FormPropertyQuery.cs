using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DDraw;
using DFWinForms.LogicTree;
using DF3DControl.Base;
using Gvitech.CityMaker.FdeGeometry;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using DFWinForms.Class;
using DF3DData.UserControl;
using DevExpress.XtraBars.Docking;

namespace DF3DData.Frm
{
    public class FormPropertyQuery : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnQuery;
        private SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private SimpleButton Btn_ReSetGeo;
        private ComboBoxEdit cmbUint;
        private SpinEdit spinBuffer;
        private CheckEdit checkBtnBuffer;
        private ComboBoxEdit cmbSpatialRel;
        private RadioGroup radioGroup;
        private SimpleButton btn_Devide;
        private SimpleButton btn_Bracket;
        private SimpleButton btn_In;
        private SimpleButton btn_LessEqauls;
        private SimpleButton btn_Underline;
        private SimpleButton btn_Like;
        private SimpleButton btn_Not;
        private SimpleButton btn_Is;
        private SimpleButton btn_quote;
        private SimpleButton btn_Less;
        private SimpleButton btn_Or;
        private SimpleButton btn_All;
        private SimpleButton btn_MoreEqauls;
        private SimpleButton btn_And;
        private MemoEdit txt_Express;
        private ListBoxControl lb_FieldValue;
        private SimpleButton btn_NotEquals;
        private SimpleButton btn_More;
        private ListBoxControl lb_FieldName;
        private SimpleButton btn_Equals;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        public DevExpress.XtraLayout.LayoutControlItem layout_Layer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private ComboBoxEdit cmb_Actuality;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

        private DF3DApplication app;
        public FormPropertyQuery(IBaseLayer bl)
        {
            InitializeComponent();
            app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || app.Workbench == null)
            {
                this.Enabled = false;
                return;
            }
            this._layer = bl;
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmb_Actuality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Btn_ReSetGeo = new DevExpress.XtraEditors.SimpleButton();
            this.cmbUint = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinBuffer = new DevExpress.XtraEditors.SpinEdit();
            this.checkBtnBuffer = new DevExpress.XtraEditors.CheckEdit();
            this.cmbSpatialRel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.radioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.btn_Devide = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Bracket = new DevExpress.XtraEditors.SimpleButton();
            this.btn_In = new DevExpress.XtraEditors.SimpleButton();
            this.btn_LessEqauls = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Underline = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Like = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Not = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Is = new DevExpress.XtraEditors.SimpleButton();
            this.btn_quote = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Less = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Or = new DevExpress.XtraEditors.SimpleButton();
            this.btn_All = new DevExpress.XtraEditors.SimpleButton();
            this.btn_MoreEqauls = new DevExpress.XtraEditors.SimpleButton();
            this.btn_And = new DevExpress.XtraEditors.SimpleButton();
            this.txt_Express = new DevExpress.XtraEditors.MemoEdit();
            this.lb_FieldValue = new DevExpress.XtraEditors.ListBoxControl();
            this.btn_NotEquals = new DevExpress.XtraEditors.SimpleButton();
            this.btn_More = new DevExpress.XtraEditors.SimpleButton();
            this.lb_FieldName = new DevExpress.XtraEditors.ListBoxControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Equals = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layout_Layer = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_Actuality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinBuffer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBtnBuffer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpatialRel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Express.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_FieldValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_FieldName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Layer)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmb_Actuality);
            this.layoutControl1.Controls.Add(this.Btn_ReSetGeo);
            this.layoutControl1.Controls.Add(this.cmbUint);
            this.layoutControl1.Controls.Add(this.spinBuffer);
            this.layoutControl1.Controls.Add(this.checkBtnBuffer);
            this.layoutControl1.Controls.Add(this.cmbSpatialRel);
            this.layoutControl1.Controls.Add(this.radioGroup);
            this.layoutControl1.Controls.Add(this.btn_Devide);
            this.layoutControl1.Controls.Add(this.btn_Bracket);
            this.layoutControl1.Controls.Add(this.btn_In);
            this.layoutControl1.Controls.Add(this.btn_LessEqauls);
            this.layoutControl1.Controls.Add(this.btn_Underline);
            this.layoutControl1.Controls.Add(this.btn_Like);
            this.layoutControl1.Controls.Add(this.btn_Not);
            this.layoutControl1.Controls.Add(this.btn_Is);
            this.layoutControl1.Controls.Add(this.btn_quote);
            this.layoutControl1.Controls.Add(this.btn_Less);
            this.layoutControl1.Controls.Add(this.btn_Or);
            this.layoutControl1.Controls.Add(this.btn_All);
            this.layoutControl1.Controls.Add(this.btn_MoreEqauls);
            this.layoutControl1.Controls.Add(this.btn_And);
            this.layoutControl1.Controls.Add(this.txt_Express);
            this.layoutControl1.Controls.Add(this.lb_FieldValue);
            this.layoutControl1.Controls.Add(this.btn_NotEquals);
            this.layoutControl1.Controls.Add(this.btn_More);
            this.layoutControl1.Controls.Add(this.lb_FieldName);
            this.layoutControl1.Controls.Add(this.btnQuery);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btn_Equals);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(387, 503);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmb_Actuality
            // 
            this.cmb_Actuality.Location = new System.Drawing.Point(78, 35);
            this.cmb_Actuality.Name = "cmb_Actuality";
            this.cmb_Actuality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_Actuality.Properties.DropDownRows = 10;
            this.cmb_Actuality.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_Actuality.Size = new System.Drawing.Size(294, 22);
            this.cmb_Actuality.StyleController = this.layoutControl1;
            this.cmb_Actuality.TabIndex = 38;
            this.cmb_Actuality.SelectedIndexChanged += new System.EventHandler(this.cmb_Actuality_SelectedIndexChanged);
            // 
            // Btn_ReSetGeo
            // 
            this.Btn_ReSetGeo.Location = new System.Drawing.Point(307, 385);
            this.Btn_ReSetGeo.Name = "Btn_ReSetGeo";
            this.Btn_ReSetGeo.Size = new System.Drawing.Size(65, 22);
            this.Btn_ReSetGeo.StyleController = this.layoutControl1;
            this.Btn_ReSetGeo.TabIndex = 37;
            this.Btn_ReSetGeo.Text = "重置";
            this.Btn_ReSetGeo.Click += new System.EventHandler(this.Btn_ReSetGeo_Click);
            // 
            // cmbUint
            // 
            this.cmbUint.EditValue = "米";
            this.cmbUint.Location = new System.Drawing.Point(322, 440);
            this.cmbUint.Name = "cmbUint";
            this.cmbUint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUint.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbUint.Size = new System.Drawing.Size(50, 22);
            this.cmbUint.StyleController = this.layoutControl1;
            this.cmbUint.TabIndex = 36;
            // 
            // spinBuffer
            // 
            this.spinBuffer.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinBuffer.Location = new System.Drawing.Point(205, 440);
            this.spinBuffer.Name = "spinBuffer";
            this.spinBuffer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinBuffer.Size = new System.Drawing.Size(113, 22);
            this.spinBuffer.StyleController = this.layoutControl1;
            this.spinBuffer.TabIndex = 35;
            this.spinBuffer.EditValueChanged += new System.EventHandler(this.spinBuffer_EditValueChanged);
            // 
            // checkBtnBuffer
            // 
            this.checkBtnBuffer.EditValue = true;
            this.checkBtnBuffer.Location = new System.Drawing.Point(15, 440);
            this.checkBtnBuffer.Name = "checkBtnBuffer";
            this.checkBtnBuffer.Properties.Caption = "使用缓冲距离";
            this.checkBtnBuffer.Size = new System.Drawing.Size(186, 19);
            this.checkBtnBuffer.StyleController = this.layoutControl1;
            this.checkBtnBuffer.TabIndex = 34;
            this.checkBtnBuffer.CheckedChanged += new System.EventHandler(this.checkBtnBuffer_CheckedChanged);
            // 
            // cmbSpatialRel
            // 
            this.cmbSpatialRel.EditValue = "Intersects";
            this.cmbSpatialRel.Location = new System.Drawing.Point(78, 414);
            this.cmbSpatialRel.Name = "cmbSpatialRel";
            this.cmbSpatialRel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSpatialRel.Properties.Items.AddRange(new object[] {
            "Intersects",
            "Envelope",
            "Contains"});
            this.cmbSpatialRel.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSpatialRel.Size = new System.Drawing.Size(294, 22);
            this.cmbSpatialRel.StyleController = this.layoutControl1;
            this.cmbSpatialRel.TabIndex = 33;
            // 
            // radioGroup
            // 
            this.radioGroup.Location = new System.Drawing.Point(78, 385);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Columns = 4;
            this.radioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("面", "面"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("线", "线"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("点", "点")});
            this.radioGroup.Size = new System.Drawing.Size(225, 25);
            this.radioGroup.StyleController = this.layoutControl1;
            this.radioGroup.TabIndex = 32;
            this.radioGroup.SelectedIndexChanged += new System.EventHandler(this.radioGroup_SelectedIndexChanged);
            this.radioGroup.Click += new System.EventHandler(this.radioGroup_Click);
            // 
            // btn_Devide
            // 
            this.btn_Devide.Location = new System.Drawing.Point(129, 244);
            this.btn_Devide.Name = "btn_Devide";
            this.btn_Devide.Size = new System.Drawing.Size(36, 22);
            this.btn_Devide.StyleController = this.layoutControl1;
            this.btn_Devide.TabIndex = 31;
            this.btn_Devide.Text = "%";
            this.btn_Devide.Click += new System.EventHandler(this.btn_Devide_Click);
            // 
            // btn_Bracket
            // 
            this.btn_Bracket.Location = new System.Drawing.Point(129, 166);
            this.btn_Bracket.Name = "btn_Bracket";
            this.btn_Bracket.Size = new System.Drawing.Size(36, 22);
            this.btn_Bracket.StyleController = this.layoutControl1;
            this.btn_Bracket.TabIndex = 21;
            this.btn_Bracket.Text = "()";
            this.btn_Bracket.Click += new System.EventHandler(this.btn_Bracket_Click);
            // 
            // btn_In
            // 
            this.btn_In.Location = new System.Drawing.Point(129, 218);
            this.btn_In.Name = "btn_In";
            this.btn_In.Size = new System.Drawing.Size(36, 22);
            this.btn_In.StyleController = this.layoutControl1;
            this.btn_In.TabIndex = 16;
            this.btn_In.Text = "In";
            this.btn_In.Click += new System.EventHandler(this.btn_In_Click);
            // 
            // btn_LessEqauls
            // 
            this.btn_LessEqauls.Location = new System.Drawing.Point(129, 192);
            this.btn_LessEqauls.Name = "btn_LessEqauls";
            this.btn_LessEqauls.Size = new System.Drawing.Size(36, 22);
            this.btn_LessEqauls.StyleController = this.layoutControl1;
            this.btn_LessEqauls.TabIndex = 28;
            this.btn_LessEqauls.Text = "<=";
            this.btn_LessEqauls.Click += new System.EventHandler(this.btn_LessEqauls_Click);
            // 
            // btn_Underline
            // 
            this.btn_Underline.Location = new System.Drawing.Point(90, 244);
            this.btn_Underline.Name = "btn_Underline";
            this.btn_Underline.Size = new System.Drawing.Size(35, 22);
            this.btn_Underline.StyleController = this.layoutControl1;
            this.btn_Underline.TabIndex = 19;
            this.btn_Underline.Text = "_";
            this.btn_Underline.Click += new System.EventHandler(this.btn_Underline_Click);
            // 
            // btn_Like
            // 
            this.btn_Like.Location = new System.Drawing.Point(52, 244);
            this.btn_Like.Name = "btn_Like";
            this.btn_Like.Size = new System.Drawing.Size(34, 22);
            this.btn_Like.StyleController = this.layoutControl1;
            this.btn_Like.TabIndex = 20;
            this.btn_Like.Text = "Like";
            this.btn_Like.Click += new System.EventHandler(this.btn_Like_Click);
            // 
            // btn_Not
            // 
            this.btn_Not.Location = new System.Drawing.Point(90, 218);
            this.btn_Not.Name = "btn_Not";
            this.btn_Not.Size = new System.Drawing.Size(35, 22);
            this.btn_Not.StyleController = this.layoutControl1;
            this.btn_Not.TabIndex = 22;
            this.btn_Not.Text = "Not";
            this.btn_Not.Click += new System.EventHandler(this.btn_Not_Click);
            // 
            // btn_Is
            // 
            this.btn_Is.Location = new System.Drawing.Point(15, 244);
            this.btn_Is.Name = "btn_Is";
            this.btn_Is.Size = new System.Drawing.Size(33, 22);
            this.btn_Is.StyleController = this.layoutControl1;
            this.btn_Is.TabIndex = 26;
            this.btn_Is.Text = "Is";
            this.btn_Is.Click += new System.EventHandler(this.btn_Is_Click);
            // 
            // btn_quote
            // 
            this.btn_quote.Location = new System.Drawing.Point(90, 166);
            this.btn_quote.Name = "btn_quote";
            this.btn_quote.Size = new System.Drawing.Size(35, 22);
            this.btn_quote.StyleController = this.layoutControl1;
            this.btn_quote.TabIndex = 27;
            this.btn_quote.Text = "\'\'";
            this.btn_quote.Click += new System.EventHandler(this.btn_quote_Click);
            // 
            // btn_Less
            // 
            this.btn_Less.Location = new System.Drawing.Point(90, 192);
            this.btn_Less.Name = "btn_Less";
            this.btn_Less.Size = new System.Drawing.Size(35, 22);
            this.btn_Less.StyleController = this.layoutControl1;
            this.btn_Less.TabIndex = 25;
            this.btn_Less.Text = "<";
            this.btn_Less.Click += new System.EventHandler(this.btn_Less_Click);
            // 
            // btn_Or
            // 
            this.btn_Or.Location = new System.Drawing.Point(52, 218);
            this.btn_Or.Name = "btn_Or";
            this.btn_Or.Size = new System.Drawing.Size(34, 22);
            this.btn_Or.StyleController = this.layoutControl1;
            this.btn_Or.TabIndex = 24;
            this.btn_Or.Text = "Or";
            this.btn_Or.Click += new System.EventHandler(this.btn_Or_Click);
            // 
            // btn_All
            // 
            this.btn_All.Location = new System.Drawing.Point(169, 244);
            this.btn_All.Name = "btn_All";
            this.btn_All.Size = new System.Drawing.Size(203, 22);
            this.btn_All.StyleController = this.layoutControl1;
            this.btn_All.TabIndex = 17;
            this.btn_All.Text = "显示所有";
            this.btn_All.Click += new System.EventHandler(this.btn_All_Click);
            // 
            // btn_MoreEqauls
            // 
            this.btn_MoreEqauls.Location = new System.Drawing.Point(52, 192);
            this.btn_MoreEqauls.Name = "btn_MoreEqauls";
            this.btn_MoreEqauls.Size = new System.Drawing.Size(34, 22);
            this.btn_MoreEqauls.StyleController = this.layoutControl1;
            this.btn_MoreEqauls.TabIndex = 29;
            this.btn_MoreEqauls.Text = ">=";
            this.btn_MoreEqauls.Click += new System.EventHandler(this.btn_MoreEqauls_Click);
            // 
            // btn_And
            // 
            this.btn_And.Location = new System.Drawing.Point(15, 218);
            this.btn_And.Name = "btn_And";
            this.btn_And.Size = new System.Drawing.Size(33, 22);
            this.btn_And.StyleController = this.layoutControl1;
            this.btn_And.TabIndex = 23;
            this.btn_And.Text = "And";
            this.btn_And.Click += new System.EventHandler(this.btn_And_Click);
            // 
            // txt_Express
            // 
            this.txt_Express.Location = new System.Drawing.Point(15, 270);
            this.txt_Express.Name = "txt_Express";
            this.txt_Express.Size = new System.Drawing.Size(357, 85);
            this.txt_Express.StyleController = this.layoutControl1;
            this.txt_Express.TabIndex = 8;
            this.txt_Express.UseOptimizedRendering = true;
            // 
            // lb_FieldValue
            // 
            this.lb_FieldValue.Location = new System.Drawing.Point(169, 166);
            this.lb_FieldValue.Name = "lb_FieldValue";
            this.lb_FieldValue.Size = new System.Drawing.Size(203, 74);
            this.lb_FieldValue.StyleController = this.layoutControl1;
            this.lb_FieldValue.TabIndex = 7;
            this.lb_FieldValue.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lb_FieldValue_MouseDoubleClick);
            // 
            // btn_NotEquals
            // 
            this.btn_NotEquals.Location = new System.Drawing.Point(52, 166);
            this.btn_NotEquals.Name = "btn_NotEquals";
            this.btn_NotEquals.Size = new System.Drawing.Size(34, 22);
            this.btn_NotEquals.StyleController = this.layoutControl1;
            this.btn_NotEquals.TabIndex = 18;
            this.btn_NotEquals.Text = "<>";
            this.btn_NotEquals.Click += new System.EventHandler(this.btn_NotEquals_Click);
            // 
            // btn_More
            // 
            this.btn_More.Location = new System.Drawing.Point(15, 192);
            this.btn_More.Name = "btn_More";
            this.btn_More.Size = new System.Drawing.Size(33, 22);
            this.btn_More.StyleController = this.layoutControl1;
            this.btn_More.TabIndex = 30;
            this.btn_More.Text = ">";
            this.btn_More.Click += new System.EventHandler(this.btn_More_Click);
            // 
            // lb_FieldName
            // 
            this.lb_FieldName.Location = new System.Drawing.Point(15, 61);
            this.lb_FieldName.Name = "lb_FieldName";
            this.lb_FieldName.Size = new System.Drawing.Size(357, 101);
            this.lb_FieldName.StyleController = this.layoutControl1;
            this.lb_FieldName.TabIndex = 6;
            this.lb_FieldName.SelectedIndexChanged += new System.EventHandler(this.lb_FieldName_SelectedIndexChanged);
            this.lb_FieldName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lb_FieldName_MouseDoubleClick);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 469);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(188, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查    询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 469);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(171, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取    消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_Equals
            // 
            this.btn_Equals.Location = new System.Drawing.Point(15, 166);
            this.btn_Equals.Name = "btn_Equals";
            this.btn_Equals.Size = new System.Drawing.Size(33, 22);
            this.btn_Equals.StyleController = this.layoutControl1;
            this.btn_Equals.TabIndex = 17;
            this.btn_Equals.Text = "=";
            this.btn_Equals.Click += new System.EventHandler(this.btn_Equals_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(387, 503);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCancel;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(192, 457);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(175, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnQuery;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 457);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(192, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "属性条件";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(367, 350);
            this.layoutControlGroup2.Text = "属性条件";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lb_FieldName;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(361, 105);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lb_FieldValue;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(154, 131);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(207, 78);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txt_Express;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 235);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(361, 89);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_All;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(154, 209);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btn_Equals;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 131);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(37, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btn_More;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 157);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(37, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btn_And;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 183);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(37, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btn_Is;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 209);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(37, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btn_NotEquals;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(37, 131);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(38, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btn_MoreEqauls;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(37, 157);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(38, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btn_Or;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(37, 183);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(38, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.btn_Like;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(37, 209);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(38, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.btn_quote;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(75, 131);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.btn_Less;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(75, 157);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.btn_Not;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(75, 183);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.btn_Underline;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(75, 209);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem19.Text = "layoutControlItem19";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.btn_Bracket;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(114, 131);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.btn_LessEqauls;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(114, 157);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem21.Text = "layoutControlItem21";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.btn_In;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(114, 183);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem22.Text = "layoutControlItem22";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextToControlDistance = 0;
            this.layoutControlItem22.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.btn_Devide;
            this.layoutControlItem23.CustomizationFormText = "layoutControlItem23";
            this.layoutControlItem23.Location = new System.Drawing.Point(114, 209);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(40, 26);
            this.layoutControlItem23.Text = "layoutControlItem23";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextToControlDistance = 0;
            this.layoutControlItem23.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmb_Actuality;
            this.layoutControlItem4.CustomizationFormText = "要素类：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(361, 26);
            this.layoutControlItem4.Text = "要素类：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "空间条件";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem29,
            this.layoutControlItem24,
            this.layoutControlItem28,
            this.layoutControlItem27,
            this.layoutControlItem25,
            this.layoutControlItem26});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 350);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(367, 107);
            this.layoutControlGroup3.Text = "空间条件";
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.radioGroup;
            this.layoutControlItem29.CustomizationFormText = "查询选择：";
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(292, 29);
            this.layoutControlItem29.Text = "查询选择：";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.Btn_ReSetGeo;
            this.layoutControlItem24.CustomizationFormText = "layoutControlItem24";
            this.layoutControlItem24.Location = new System.Drawing.Point(292, 0);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(69, 29);
            this.layoutControlItem24.Text = "layoutControlItem24";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.cmbSpatialRel;
            this.layoutControlItem28.CustomizationFormText = "查询方式：";
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(361, 26);
            this.layoutControlItem28.Text = "查询方式：";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.checkBtnBuffer;
            this.layoutControlItem27.CustomizationFormText = "layoutControlItem27";
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 55);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(190, 26);
            this.layoutControlItem27.Text = "layoutControlItem27";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextToControlDistance = 0;
            this.layoutControlItem27.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.cmbUint;
            this.layoutControlItem25.CustomizationFormText = "layoutControlItem25";
            this.layoutControlItem25.Location = new System.Drawing.Point(307, 55);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(54, 26);
            this.layoutControlItem25.Text = "layoutControlItem25";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem25.TextToControlDistance = 0;
            this.layoutControlItem25.TextVisible = false;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.spinBuffer;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem26.Location = new System.Drawing.Point(190, 55);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(117, 26);
            this.layoutControlItem26.Text = "layoutControlItem26";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextToControlDistance = 0;
            this.layoutControlItem26.TextVisible = false;
            // 
            // layout_Layer
            // 
            this.layout_Layer.CustomizationFormText = "${res:Interface_要素类}";
            this.layout_Layer.Location = new System.Drawing.Point(0, 0);
            this.layout_Layer.Name = "llayout_Layer";
            this.layout_Layer.Size = new System.Drawing.Size(330, 26);
            this.layout_Layer.Text = "现状类：";
            this.layout_Layer.TextSize = new System.Drawing.Size(60, 14);
            this.layout_Layer.TextToControlDistance = 5;
            // 
            // FormPropertyQuery
            // 
            this.AcceptButton = this.btnQuery;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(387, 503);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPropertyQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查询";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPropertyQuery_FormClosed);
            this.Load += new System.EventHandler(this.FormPropertyQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmb_Actuality.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinBuffer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBtnBuffer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpatialRel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Express.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_FieldValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_FieldName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Layer)).EndInit();
            this.ResumeLayout(false);

        }

        private IBaseLayer _layer;
        private DrawTool _drawTool;
        private DrawType _drawType;
        private IGeometry _geo;

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = this.cmb_Actuality.SelectedItem;
                if (obj == null || !(obj is DF3DFeatureClass)) return;

                DF3DFeatureClass dffc = obj as DF3DFeatureClass;
                IFeatureLayer fl = dffc.GetFeatureLayer();
                if (fl == null) return;
                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = this._geo;
                filter.GeometryField = fl.GeometryFieldName;
                filter.WhereClause = this.txt_Express.Text;
                switch (this.cmbSpatialRel.SelectedIndex)
                {
                    case 0:
                        filter.SpatialRel = gviSpatialRel.gviSpatialRelContains;
                        break;
                    case 1:
                        filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
                        break;
                    case 2:
                        filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                        break;
                    default:
                        filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                        break;
                }
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                int count = fc.GetCount(filter);
                if (count == 0)
                {
                    XtraMessageBox.Show("查询结果为空", "提示");
                    return;
                }
                this.WindowState = FormWindowState.Minimized;
                WaitForm.Start("正在查询...", "请稍后");
                this._uPanel = new UIDockPanel("查询结果", "查询结果", this.Location1, this._width, this._height);
                this._dockPanel = FloatPanelManager.Instance.Add(ref this._uPanel, DockingStyle.Right);
                this._dockPanel.Visibility = DockVisibility.Visible;
                this._dockPanel.FloatSize = new System.Drawing.Size(this._width, this._height);
                this._dockPanel.Width = this._width;
                this._dockPanel.Height = this._height;
                this._uc = new UCPropertyInfo();
                this._uc.Init();
                this._uc.Dock = System.Windows.Forms.DockStyle.Fill;
                this._uPanel.RegisterEvent(new PanelClose(this.Close));
                this._dockPanel.Controls.Add(this._uc);
                this._uc.SetInfo(dffc, filter, count);
                WaitForm.Stop();
            }
            catch (Exception ex)
            {
            }
        }

        private void Close() 
        {
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.End();
                this._drawTool.Close();
                this._drawTool = null;
            } 
            base.Close();
        }

        private DockPanel _dockPanel;
        private int _height = 400;
        private UIDockPanel _uPanel;
        private int _width = 225;
        private UCPropertyInfo _uc;
        private System.Drawing.Point Location1
        {
            get
            {
                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                return new System.Drawing.Point((width - this._width) / 2, (height - this._height) / 2);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            _drawType = DrawType.None;
            switch (this.radioGroup.SelectedIndex)
            {
                case 0:
                    _drawType = DrawType.Polygon;
                    break;
                case 1:
                    _drawType = DrawType.Polyline;
                    break;
                case 2:
                    _drawType = DrawType.Point;
                    break;
                case 3:
                    _drawType = DrawType.SelectOne;
                    break;
                default:
                    return;
            }
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.End();
                this._drawTool.Close();
                this._drawTool = null;
            }
            _drawTool = DrawToolService.Instance.CreateDrawTool(_drawType);

            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.Start();
            }
            this.WindowState = FormWindowState.Minimized;
        }

        private void radioGroup_Click(object sender, EventArgs e)
        {
            _drawType = DrawType.None;
            switch (this.radioGroup.SelectedIndex)
            {
                case 0:
                    _drawType = DrawType.Polygon;
                    break;
                case 1:
                    _drawType = DrawType.Polyline;
                    break;
                case 2:
                    _drawType = DrawType.Point;
                    break;
                case 3:
                    _drawType = DrawType.SelectOne;
                    break;
                default:
                    return;
            }
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.End();
                this._drawTool.Close();
                this._drawTool = null;
            }
            _drawTool = DrawToolService.Instance.CreateDrawTool(_drawType);

            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.Start();
            }
            this.WindowState = FormWindowState.Minimized;

        }

        private void _drawTool_OnFinishedDraw()
        {
            try
            {
                if (this._drawTool != null && this._drawTool.GeoType == this._drawType)
                {
                    this.WindowState = FormWindowState.Normal;
                    this._geo = null;
                    IGeometry geot = this._drawTool.GetGeo();
                    if (this.checkBtnBuffer.Checked)
                    {
                        this._geo = (geot as ITopologicalOperator2D).Buffer2D(Convert.ToDouble(this.spinBuffer.Text), gviBufferStyle.gviBufferCapround);
                    }
                    else
                    {
                        this._geo = geot;
                    }
                    this._drawTool.OnStartDraw -= new OnStartDraw(_drawTool_OnStartDraw);
                    this._drawTool.OnFinishedDraw -= new OnFinishedDraw(_drawTool_OnFinishedDraw);
                    this._drawTool.End();
                    this._drawTool.Close();
                    this._drawTool = null;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void _drawTool_OnStartDraw()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void FormPropertyQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void FormPropertyQuery_Load(object sender, EventArgs e)
        {
            try
            {
                List<DF3DFeatureClass> list = DF3DFeatureClassManager.Instance.GetAllFeatureClass();
                if (list != null)
                {
                    string guid = string.Empty;
                    if (this._layer != null)
                    {
                        IFeatureClass fc = this._layer.CustomValue as IFeatureClass;
                        if (fc != null) guid = fc.GuidString;
                    }
                    int num = 0;
                    int num1 = -1;
                    foreach (DF3DFeatureClass dffc1 in list)
                    {
                        this.cmb_Actuality.Properties.Items.Add(dffc1);
                        IFeatureClass fc1 = dffc1.GetFeatureClass();
                        if (fc1 != null)
                        {
                            if (guid == fc1.GuidString)
                            {
                                num1 = num;
                            }
                        }
                        num++;
                    }
                    this.cmb_Actuality.SelectedIndex = num1;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void spinBuffer_EditValueChanged(object sender, EventArgs e)
        {
            if (this.spinBuffer.Value < 0)
            {
                this.spinBuffer.Value = 1;
            }
        }

        private void btn_Equals_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " = ";
        }

        private void btn_NotEquals_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " <> ";
        }

        private void btn_quote_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " '' ";
        }

        private void btn_Bracket_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text = " ( " + this.txt_Express.Text + " ) ";
        }

        private void btn_More_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " < ";
        }

        private void btn_MoreEqauls_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " >= ";
        }

        private void btn_Less_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " < ";
        }

        private void btn_LessEqauls_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " <= ";
        }

        private void btn_And_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " AND ";
        }

        private void btn_Or_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " OR ";
        }

        private void btn_Not_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " NOT ";
        }

        private void btn_In_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " IN ";
        }

        private void btn_Is_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " IS ";
        }

        private void btn_Like_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " LIKE ";
        }

        private void btn_Underline_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " _ ";
        }

        private void btn_Devide_Click(object sender, EventArgs e)
        {
            this.txt_Express.Text += " % ";
        }

        private void checkBtnBuffer_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBtnBuffer.Checked)
            {
                this.spinBuffer.Enabled = false; this.cmbUint.Enabled = false;
            }
            else
            {
                this.spinBuffer.Enabled = true; this.cmbUint.Enabled = true;
            }
        }

        private void lb_FieldName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.txt_Express.Text += this.lb_FieldName.Text;
            }
        }

        private void lb_FieldValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.txt_Express.Text += this.lb_FieldValue.Text;
            }

        }

        private void lb_FieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.lb_FieldValue.Items.Clear();
                object obj = this.cmb_Actuality.SelectedItem;
                if (obj == null || !(obj is DF3DFeatureClass)) return;

                DF3DFeatureClass dffc = obj as DF3DFeatureClass;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                if (fields == null) return;
                int index = fields.IndexOf(this.lb_FieldName.Text);
                if (index == -1) return;
                IFieldInfo fi = fields.Get(index);

                bool bStr = false;
                switch (fi.FieldType)
                {
                    case gviFieldType.gviFieldUnknown:
                    case gviFieldType.gviFieldGeometry:
                    case gviFieldType.gviFieldBlob:
                        return;
                    case gviFieldType.gviFieldDate:
                    case gviFieldType.gviFieldString:
                        bStr = true;
                        break;
                    default:
                        bStr = false;
                        break;
                }
                IQueryFilter filter = new QueryFilter();
                filter.SubFields = this.lb_FieldName.Text;
                IFdeCursor cursor = null;
                IRowBuffer row = null;
                WaitForm.Start("正在查询...", "请稍后");
                try
                {
                    cursor = fc.Search(filter, true);
                    HashSet<string> hs = new HashSet<string>();
                    while ((row = cursor.NextRow()) != null)
                    {
                        if (!row.IsNull(0))
                        {
                            if (bStr) hs.Add("'" + row.GetValue(0).ToString() + "'");
                            else hs.Add(row.GetValue(0).ToString());
                            if (hs.Count == 10) break;
                        }
                    }
                    foreach (string str in hs)
                    {
                        this.lb_FieldValue.Items.Add(str);
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    if (cursor != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                        cursor = null;
                    }
                }
                WaitForm.Stop();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmb_Actuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.lb_FieldValue.Items.Clear();
                this.lb_FieldName.Items.Clear();
                this.txt_Express.Text = "";
                object obj = this.cmb_Actuality.SelectedItem;
                if (obj == null || !(obj is DF3DFeatureClass)) return;

                DF3DFeatureClass dffc = obj as DF3DFeatureClass;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                if (fields == null) return;
                for (int i = 0; i < fields.Count; i++)
                {
                    IFieldInfo fi = fields.Get(i);
                    switch (fi.FieldType)
                    {
                        case gviFieldType.gviFieldBlob:
                        case gviFieldType.gviFieldGeometry:
                        case gviFieldType.gviFieldUnknown:
                            continue;
                    }
                    this.lb_FieldName.Items.Add(fi.Name);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_All_Click(object sender, EventArgs e)
        {
            try
            {
                this.lb_FieldValue.Items.Clear();
                object obj = this.cmb_Actuality.SelectedItem;
                if (obj == null || !(obj is DF3DFeatureClass)) return;

                DF3DFeatureClass dffc = obj as DF3DFeatureClass;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return;
                IFieldInfoCollection fields = fc.GetFields();
                if (fields == null) return;
                int index = fields.IndexOf(this.lb_FieldName.Text);
                if (index == -1) return;
                IFieldInfo fi = fields.Get(index);

                bool bStr = false;
                switch (fi.FieldType)
                {
                    case gviFieldType.gviFieldUnknown:
                    case gviFieldType.gviFieldGeometry:
                    case gviFieldType.gviFieldBlob:
                        return;
                    case gviFieldType.gviFieldDate:
                    case gviFieldType.gviFieldString:
                        bStr = true;
                        break;
                    default:
                        bStr = false;
                        break;
                }
                IQueryFilter filter = new QueryFilter();
                filter.SubFields = this.lb_FieldName.Text;
                IFdeCursor cursor = null;
                IRowBuffer row = null;
                WaitForm.Start("正在查询...", "请稍后");
                try
                {
                    cursor = fc.Search(filter, true);
                    HashSet<string> hs = new HashSet<string>();
                    while ((row = cursor.NextRow()) != null)
                    {
                        if (!row.IsNull(0))
                        {
                            if (bStr) hs.Add("'" + row.GetValue(0).ToString() + "'");
                            else hs.Add(row.GetValue(0).ToString());
                        }
                    }
                    foreach (string str in hs)
                    {
                        this.lb_FieldValue.Items.Add(str);
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    if (cursor != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                        cursor = null;
                    }
                }
                WaitForm.Stop();
            }
            catch (Exception ex)
            {
            }
        }

        private void Btn_ReSetGeo_Click(object sender, EventArgs e)
        {
            this.radioGroup.SelectedIndex = -1;
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(_drawTool_OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(_drawTool_OnFinishedDraw);
                this._drawTool.End();
                this._drawTool.Close();
                this._drawTool = null;
            }
        }


    }
}
