using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DF3DData.UserControl
{
    public class UCSimpleLabel : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private CheckEdit checkEditDrawLine;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ColorEdit BackColorEdit;
        private CheckEdit UnderLineCheckEdit;
        private CheckEdit ItalicCheckEdit;
        private CheckEdit BoldCheckEdit;
        private ColorEdit outLineColorEdit;
        private SpinEdit spinEditOffSet;
        private SpinEdit spinEditPriority;
        private SpinEdit spinEditMinDis;
        private SpinEdit spinEditMaxDis;
        private ColorEdit fontColorEdit;
        private FontEdit fontEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private SpinEdit spinEditVPixelOff;
        private SpinEdit spinEditHPixelOff;
        private ComboBoxEdit cmbAlign;
        private SpinEdit spinEditFontSize;
        private ComboBoxEdit cmbShowMode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
    
        public UCSimpleLabel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkEditDrawLine = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spinEditVPixelOff = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditHPixelOff = new DevExpress.XtraEditors.SpinEdit();
            this.cmbAlign = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditFontSize = new DevExpress.XtraEditors.SpinEdit();
            this.cmbShowMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.BackColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.UnderLineCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.ItalicCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.BoldCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.outLineColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.spinEditOffSet = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditPriority = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditMinDis = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditMaxDis = new DevExpress.XtraEditors.SpinEdit();
            this.fontColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.fontEdit = new DevExpress.XtraEditors.FontEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
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
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDrawLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditVPixelOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHPixelOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFontSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShowMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackColorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnderLineCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItalicCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoldCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outLineColorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditOffSet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditPriority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMinDis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxDis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.checkEditDrawLine;
            this.layoutControlItem4.CustomizationFormText = "参考线";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 225);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(264, 23);
            this.layoutControlItem4.Text = "参考线";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 14);
            // 
            // checkEditDrawLine
            // 
            this.checkEditDrawLine.Location = new System.Drawing.Point(87, 237);
            this.checkEditDrawLine.Name = "checkEditDrawLine";
            this.checkEditDrawLine.Properties.Caption = "";
            this.checkEditDrawLine.Size = new System.Drawing.Size(185, 19);
            this.checkEditDrawLine.StyleController = this.layoutControl1;
            this.checkEditDrawLine.TabIndex = 9;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spinEditVPixelOff);
            this.layoutControl1.Controls.Add(this.spinEditHPixelOff);
            this.layoutControl1.Controls.Add(this.cmbAlign);
            this.layoutControl1.Controls.Add(this.spinEditFontSize);
            this.layoutControl1.Controls.Add(this.cmbShowMode);
            this.layoutControl1.Controls.Add(this.BackColorEdit);
            this.layoutControl1.Controls.Add(this.UnderLineCheckEdit);
            this.layoutControl1.Controls.Add(this.ItalicCheckEdit);
            this.layoutControl1.Controls.Add(this.BoldCheckEdit);
            this.layoutControl1.Controls.Add(this.outLineColorEdit);
            this.layoutControl1.Controls.Add(this.spinEditOffSet);
            this.layoutControl1.Controls.Add(this.spinEditPriority);
            this.layoutControl1.Controls.Add(this.spinEditMinDis);
            this.layoutControl1.Controls.Add(this.spinEditMaxDis);
            this.layoutControl1.Controls.Add(this.checkEditDrawLine);
            this.layoutControl1.Controls.Add(this.fontColorEdit);
            this.layoutControl1.Controls.Add(this.fontEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 453);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spinEditVPixelOff
            // 
            this.spinEditVPixelOff.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditVPixelOff.Location = new System.Drawing.Point(87, 338);
            this.spinEditVPixelOff.Name = "spinEditVPixelOff";
            this.spinEditVPixelOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditVPixelOff.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditVPixelOff.Properties.IsFloatValue = false;
            this.spinEditVPixelOff.Properties.Mask.EditMask = "N00";
            this.spinEditVPixelOff.Size = new System.Drawing.Size(185, 22);
            this.spinEditVPixelOff.StyleController = this.layoutControl1;
            this.spinEditVPixelOff.TabIndex = 13;
            // 
            // spinEditHPixelOff
            // 
            this.spinEditHPixelOff.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditHPixelOff.Location = new System.Drawing.Point(87, 312);
            this.spinEditHPixelOff.Name = "spinEditHPixelOff";
            this.spinEditHPixelOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditHPixelOff.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditHPixelOff.Properties.IsFloatValue = false;
            this.spinEditHPixelOff.Properties.Mask.EditMask = "N00";
            this.spinEditHPixelOff.Size = new System.Drawing.Size(185, 22);
            this.spinEditHPixelOff.StyleController = this.layoutControl1;
            this.spinEditHPixelOff.TabIndex = 12;
            // 
            // cmbAlign
            // 
            this.cmbAlign.EditValue = "中下";
            this.cmbAlign.Location = new System.Drawing.Point(87, 260);
            this.cmbAlign.Name = "cmbAlign";
            this.cmbAlign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAlign.Properties.Items.AddRange(new object[] {
            "左下",
            "中下",
            "右下",
            "左中",
            "正中",
            "右中",
            "左上",
            "中上",
            "右上"});
            this.cmbAlign.Size = new System.Drawing.Size(185, 22);
            this.cmbAlign.StyleController = this.layoutControl1;
            this.cmbAlign.TabIndex = 10;
            // 
            // spinEditFontSize
            // 
            this.spinEditFontSize.EditValue = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.spinEditFontSize.Location = new System.Drawing.Point(87, 38);
            this.spinEditFontSize.Name = "spinEditFontSize";
            this.spinEditFontSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditFontSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditFontSize.Properties.IsFloatValue = false;
            this.spinEditFontSize.Properties.Mask.EditMask = "N00";
            this.spinEditFontSize.Properties.MaxValue = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.spinEditFontSize.Size = new System.Drawing.Size(185, 22);
            this.spinEditFontSize.StyleController = this.layoutControl1;
            this.spinEditFontSize.TabIndex = 1;
            // 
            // cmbShowMode
            // 
            this.cmbShowMode.EditValue = "面向屏幕";
            this.cmbShowMode.Location = new System.Drawing.Point(87, 142);
            this.cmbShowMode.Name = "cmbShowMode";
            this.cmbShowMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShowMode.Properties.Items.AddRange(new object[] {
            "面向屏幕",
            "固定轴",
            "固定轴躺着"});
            this.cmbShowMode.Size = new System.Drawing.Size(185, 22);
            this.cmbShowMode.StyleController = this.layoutControl1;
            this.cmbShowMode.TabIndex = 5;
            // 
            // BackColorEdit
            // 
            this.BackColorEdit.EditValue = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BackColorEdit.Location = new System.Drawing.Point(87, 64);
            this.BackColorEdit.Name = "BackColorEdit";
            this.BackColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BackColorEdit.Size = new System.Drawing.Size(185, 22);
            this.BackColorEdit.StyleController = this.layoutControl1;
            this.BackColorEdit.TabIndex = 2;
            // 
            // UnderLineCheckEdit
            // 
            this.UnderLineCheckEdit.Location = new System.Drawing.Point(87, 214);
            this.UnderLineCheckEdit.Name = "UnderLineCheckEdit";
            this.UnderLineCheckEdit.Properties.Caption = "";
            this.UnderLineCheckEdit.Size = new System.Drawing.Size(185, 19);
            this.UnderLineCheckEdit.StyleController = this.layoutControl1;
            this.UnderLineCheckEdit.TabIndex = 8;
            // 
            // ItalicCheckEdit
            // 
            this.ItalicCheckEdit.Location = new System.Drawing.Point(87, 191);
            this.ItalicCheckEdit.Name = "ItalicCheckEdit";
            this.ItalicCheckEdit.Properties.Caption = "";
            this.ItalicCheckEdit.Size = new System.Drawing.Size(185, 19);
            this.ItalicCheckEdit.StyleController = this.layoutControl1;
            this.ItalicCheckEdit.TabIndex = 7;
            // 
            // BoldCheckEdit
            // 
            this.BoldCheckEdit.Location = new System.Drawing.Point(87, 168);
            this.BoldCheckEdit.Name = "BoldCheckEdit";
            this.BoldCheckEdit.Properties.Caption = "";
            this.BoldCheckEdit.Size = new System.Drawing.Size(185, 19);
            this.BoldCheckEdit.StyleController = this.layoutControl1;
            this.BoldCheckEdit.TabIndex = 6;
            // 
            // outLineColorEdit
            // 
            this.outLineColorEdit.EditValue = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.outLineColorEdit.Location = new System.Drawing.Point(87, 116);
            this.outLineColorEdit.Name = "outLineColorEdit";
            this.outLineColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.outLineColorEdit.Size = new System.Drawing.Size(185, 22);
            this.outLineColorEdit.StyleController = this.layoutControl1;
            this.outLineColorEdit.TabIndex = 4;
            // 
            // spinEditOffSet
            // 
            this.spinEditOffSet.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditOffSet.Location = new System.Drawing.Point(87, 364);
            this.spinEditOffSet.Name = "spinEditOffSet";
            this.spinEditOffSet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditOffSet.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditOffSet.Size = new System.Drawing.Size(185, 22);
            this.spinEditOffSet.StyleController = this.layoutControl1;
            this.spinEditOffSet.TabIndex = 14;
            // 
            // spinEditPriority
            // 
            this.spinEditPriority.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditPriority.Location = new System.Drawing.Point(87, 286);
            this.spinEditPriority.Name = "spinEditPriority";
            this.spinEditPriority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditPriority.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditPriority.Properties.IsFloatValue = false;
            this.spinEditPriority.Properties.Mask.EditMask = "N00";
            this.spinEditPriority.Properties.MaxLength = 5;
            this.spinEditPriority.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEditPriority.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditPriority.Size = new System.Drawing.Size(185, 22);
            this.spinEditPriority.StyleController = this.layoutControl1;
            this.spinEditPriority.TabIndex = 11;
            // 
            // spinEditMinDis
            // 
            this.spinEditMinDis.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditMinDis.Location = new System.Drawing.Point(87, 416);
            this.spinEditMinDis.Name = "spinEditMinDis";
            this.spinEditMinDis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMinDis.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditMinDis.Size = new System.Drawing.Size(185, 22);
            this.spinEditMinDis.StyleController = this.layoutControl1;
            this.spinEditMinDis.TabIndex = 16;
            // 
            // spinEditMaxDis
            // 
            this.spinEditMaxDis.EditValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spinEditMaxDis.Location = new System.Drawing.Point(87, 390);
            this.spinEditMaxDis.Name = "spinEditMaxDis";
            this.spinEditMaxDis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMaxDis.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditMaxDis.Size = new System.Drawing.Size(185, 22);
            this.spinEditMaxDis.StyleController = this.layoutControl1;
            this.spinEditMaxDis.TabIndex = 15;
            // 
            // fontColorEdit
            // 
            this.fontColorEdit.EditValue = System.Drawing.Color.White;
            this.fontColorEdit.Location = new System.Drawing.Point(87, 90);
            this.fontColorEdit.Name = "fontColorEdit";
            this.fontColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fontColorEdit.Size = new System.Drawing.Size(185, 22);
            this.fontColorEdit.StyleController = this.layoutControl1;
            this.fontColorEdit.TabIndex = 3;
            // 
            // fontEdit
            // 
            this.fontEdit.EditValue = "Arial";
            this.fontEdit.Location = new System.Drawing.Point(87, 12);
            this.fontEdit.Name = "fontEdit";
            this.fontEdit.Properties.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontEdit.Properties.Appearance.Options.UseFont = true;
            this.fontEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fontEdit.Size = new System.Drawing.Size(185, 22);
            this.fontEdit.StyleController = this.layoutControl1;
            this.fontEdit.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
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
            this.layoutControlItem2,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem17});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 453);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.fontEdit;
            this.layoutControlItem1.CustomizationFormText = "字体";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem1.Text = "字体";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.fontColorEdit;
            this.layoutControlItem3.CustomizationFormText = "字体颜色";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem3.Text = "字体颜色";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinEditMaxDis;
            this.layoutControlItem5.CustomizationFormText = "最大可视距离";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 378);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem5.Text = "最大可视距离";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spinEditMinDis;
            this.layoutControlItem6.CustomizationFormText = "最小可视距离";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 404);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(264, 29);
            this.layoutControlItem6.Text = "最小可视距离";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.spinEditPriority;
            this.layoutControlItem7.CustomizationFormText = "优先级";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 274);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem7.Text = "优先级";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.spinEditOffSet;
            this.layoutControlItem8.CustomizationFormText = "高度偏移";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 352);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem8.Text = "高度偏移";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.outLineColorEdit;
            this.layoutControlItem9.CustomizationFormText = "外轮廓颜色";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem9.Text = "外轮廓颜色";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.BoldCheckEdit;
            this.layoutControlItem10.CustomizationFormText = "粗体";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(264, 23);
            this.layoutControlItem10.Text = "粗体";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ItalicCheckEdit;
            this.layoutControlItem11.CustomizationFormText = "斜体";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 179);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(264, 23);
            this.layoutControlItem11.Text = "斜体";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.UnderLineCheckEdit;
            this.layoutControlItem12.CustomizationFormText = "下划线";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 202);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(264, 23);
            this.layoutControlItem12.Text = "下划线";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.BackColorEdit;
            this.layoutControlItem13.CustomizationFormText = "背景色";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem13.Text = "背景色";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.cmbShowMode;
            this.layoutControlItem14.CustomizationFormText = "显示模式";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem14.Text = "显示模式";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEditFontSize;
            this.layoutControlItem2.CustomizationFormText = "字号";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem2.Text = "字号";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.cmbAlign;
            this.layoutControlItem15.CustomizationFormText = "对齐方式";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 248);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem15.Text = "对齐方式";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.spinEditHPixelOff;
            this.layoutControlItem16.CustomizationFormText = "横向像素偏移";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 300);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem16.Text = "横向像素偏移";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.spinEditVPixelOff;
            this.layoutControlItem17.CustomizationFormText = "纵向像素偏移";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 326);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem17.Text = "纵向像素偏移";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(72, 14);
            // 
            // UCSimpleLabel
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCSimpleLabel";
            this.Size = new System.Drawing.Size(284, 453);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDrawLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditVPixelOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHPixelOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFontSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShowMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackColorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnderLineCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItalicCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoldCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outLineColorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditOffSet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditPriority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMinDis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMaxDis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
