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
    public class UCPolygonSymbol : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private SpinEdit spinEditLineAlpha;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private CheckEdit chkSunlight;
        private SpinEdit spinEditLineWidth;
        private ColorEdit lineColorEdit;
        private SpinEdit spinEditFillAlpha;
        private ColorEdit fillColorEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private SpinEdit spinEditRepeat;
        private ButtonEdit buttonEditLineTexture;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private SpinEdit spinEditTextureRotateAngle;
        private SpinEdit spinEditTextureV;
        private SpinEdit spinEditTextureU;
        private ButtonEdit buttonEditSurfaceTexture;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
    
        public UCPolygonSymbol()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.spinEditLineAlpha = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spinEditTextureRotateAngle = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditTextureV = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditTextureU = new DevExpress.XtraEditors.SpinEdit();
            this.buttonEditSurfaceTexture = new DevExpress.XtraEditors.ButtonEdit();
            this.spinEditRepeat = new DevExpress.XtraEditors.SpinEdit();
            this.buttonEditLineTexture = new DevExpress.XtraEditors.ButtonEdit();
            this.chkSunlight = new DevExpress.XtraEditors.CheckEdit();
            this.spinEditLineWidth = new DevExpress.XtraEditors.SpinEdit();
            this.lineColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.spinEditFillAlpha = new DevExpress.XtraEditors.SpinEdit();
            this.fillColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLineAlpha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTextureRotateAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTextureV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTextureU.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditSurfaceTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRepeat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditLineTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSunlight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLineWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineColorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFillAlpha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fillColorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spinEditLineAlpha;
            this.layoutControlItem3.CustomizationFormText = "不透明度";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 182);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem3.Text = "不透明度";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // spinEditLineAlpha
            // 
            this.spinEditLineAlpha.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditLineAlpha.Location = new System.Drawing.Point(63, 194);
            this.spinEditLineAlpha.Name = "spinEditLineAlpha";
            this.spinEditLineAlpha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditLineAlpha.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditLineAlpha.Size = new System.Drawing.Size(231, 22);
            this.spinEditLineAlpha.StyleController = this.layoutControl1;
            this.spinEditLineAlpha.TabIndex = 7;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spinEditTextureRotateAngle);
            this.layoutControl1.Controls.Add(this.spinEditTextureV);
            this.layoutControl1.Controls.Add(this.spinEditTextureU);
            this.layoutControl1.Controls.Add(this.buttonEditSurfaceTexture);
            this.layoutControl1.Controls.Add(this.spinEditRepeat);
            this.layoutControl1.Controls.Add(this.buttonEditLineTexture);
            this.layoutControl1.Controls.Add(this.chkSunlight);
            this.layoutControl1.Controls.Add(this.spinEditLineWidth);
            this.layoutControl1.Controls.Add(this.lineColorEdit);
            this.layoutControl1.Controls.Add(this.spinEditLineAlpha);
            this.layoutControl1.Controls.Add(this.spinEditFillAlpha);
            this.layoutControl1.Controls.Add(this.fillColorEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(306, 331);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spinEditTextureRotateAngle
            // 
            this.spinEditTextureRotateAngle.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditTextureRotateAngle.Location = new System.Drawing.Point(63, 142);
            this.spinEditTextureRotateAngle.Name = "spinEditTextureRotateAngle";
            this.spinEditTextureRotateAngle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditTextureRotateAngle.Size = new System.Drawing.Size(231, 22);
            this.spinEditTextureRotateAngle.StyleController = this.layoutControl1;
            this.spinEditTextureRotateAngle.TabIndex = 5;
            // 
            // spinEditTextureV
            // 
            this.spinEditTextureV.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditTextureV.Location = new System.Drawing.Point(63, 116);
            this.spinEditTextureV.Name = "spinEditTextureV";
            this.spinEditTextureV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditTextureV.Size = new System.Drawing.Size(231, 22);
            this.spinEditTextureV.StyleController = this.layoutControl1;
            this.spinEditTextureV.TabIndex = 4;
            // 
            // spinEditTextureU
            // 
            this.spinEditTextureU.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditTextureU.Location = new System.Drawing.Point(63, 90);
            this.spinEditTextureU.Name = "spinEditTextureU";
            this.spinEditTextureU.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditTextureU.Size = new System.Drawing.Size(231, 22);
            this.spinEditTextureU.StyleController = this.layoutControl1;
            this.spinEditTextureU.TabIndex = 3;
            // 
            // buttonEditSurfaceTexture
            // 
            this.buttonEditSurfaceTexture.Location = new System.Drawing.Point(63, 64);
            this.buttonEditSurfaceTexture.Name = "buttonEditSurfaceTexture";
            this.buttonEditSurfaceTexture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditSurfaceTexture.Size = new System.Drawing.Size(231, 22);
            this.buttonEditSurfaceTexture.StyleController = this.layoutControl1;
            this.buttonEditSurfaceTexture.TabIndex = 2;
            // 
            // spinEditRepeat
            // 
            this.spinEditRepeat.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditRepeat.Location = new System.Drawing.Point(63, 272);
            this.spinEditRepeat.Name = "spinEditRepeat";
            this.spinEditRepeat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditRepeat.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditRepeat.Properties.IsFloatValue = false;
            this.spinEditRepeat.Properties.Mask.EditMask = "N00";
            this.spinEditRepeat.Properties.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditRepeat.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditRepeat.Size = new System.Drawing.Size(231, 22);
            this.spinEditRepeat.StyleController = this.layoutControl1;
            this.spinEditRepeat.TabIndex = 10;
            // 
            // buttonEditLineTexture
            // 
            this.buttonEditLineTexture.Location = new System.Drawing.Point(63, 246);
            this.buttonEditLineTexture.Name = "buttonEditLineTexture";
            this.buttonEditLineTexture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditLineTexture.Size = new System.Drawing.Size(231, 22);
            this.buttonEditLineTexture.StyleController = this.layoutControl1;
            this.buttonEditLineTexture.TabIndex = 9;
            // 
            // chkSunlight
            // 
            this.chkSunlight.Location = new System.Drawing.Point(63, 298);
            this.chkSunlight.Name = "chkSunlight";
            this.chkSunlight.Properties.Caption = "";
            this.chkSunlight.Size = new System.Drawing.Size(231, 19);
            this.chkSunlight.StyleController = this.layoutControl1;
            this.chkSunlight.TabIndex = 11;
            // 
            // spinEditLineWidth
            // 
            this.spinEditLineWidth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditLineWidth.Location = new System.Drawing.Point(63, 220);
            this.spinEditLineWidth.Name = "spinEditLineWidth";
            this.spinEditLineWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditLineWidth.Size = new System.Drawing.Size(231, 22);
            this.spinEditLineWidth.StyleController = this.layoutControl1;
            this.spinEditLineWidth.TabIndex = 8;
            // 
            // lineColorEdit
            // 
            this.lineColorEdit.EditValue = System.Drawing.Color.Empty;
            this.lineColorEdit.Location = new System.Drawing.Point(63, 168);
            this.lineColorEdit.Name = "lineColorEdit";
            this.lineColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lineColorEdit.Size = new System.Drawing.Size(231, 22);
            this.lineColorEdit.StyleController = this.layoutControl1;
            this.lineColorEdit.TabIndex = 6;
            // 
            // spinEditFillAlpha
            // 
            this.spinEditFillAlpha.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditFillAlpha.Location = new System.Drawing.Point(63, 38);
            this.spinEditFillAlpha.Name = "spinEditFillAlpha";
            this.spinEditFillAlpha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditFillAlpha.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditFillAlpha.Size = new System.Drawing.Size(231, 22);
            this.spinEditFillAlpha.StyleController = this.layoutControl1;
            this.spinEditFillAlpha.TabIndex = 1;
            // 
            // fillColorEdit
            // 
            this.fillColorEdit.EditValue = System.Drawing.Color.Empty;
            this.fillColorEdit.Location = new System.Drawing.Point(63, 12);
            this.fillColorEdit.Name = "fillColorEdit";
            this.fillColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fillColorEdit.Size = new System.Drawing.Size(231, 22);
            this.fillColorEdit.StyleController = this.layoutControl1;
            this.fillColorEdit.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem10,
            this.layoutControlItem12,
            this.layoutControlItem7,
            this.layoutControlItem11,
            this.layoutControlItem13,
            this.layoutControlItem14});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(306, 331);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.fillColorEdit;
            this.layoutControlItem1.CustomizationFormText = "填充色";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem1.Text = "填充色";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEditFillAlpha;
            this.layoutControlItem2.CustomizationFormText = "不透明度";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem2.Text = "不透明度";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lineColorEdit;
            this.layoutControlItem4.CustomizationFormText = "边框色";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem4.Text = "边框色";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinEditLineWidth;
            this.layoutControlItem5.CustomizationFormText = "线宽";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 208);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem5.Text = "线宽";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkSunlight;
            this.layoutControlItem6.CustomizationFormText = "光照";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 286);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(286, 25);
            this.layoutControlItem6.Text = "光照";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.buttonEditLineTexture;
            this.layoutControlItem10.CustomizationFormText = "纹理";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 234);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem10.Text = "纹理";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.spinEditRepeat;
            this.layoutControlItem12.CustomizationFormText = "重复度";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 260);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem12.Text = "重复度";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.buttonEditSurfaceTexture;
            this.layoutControlItem7.CustomizationFormText = "纹理";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem7.Text = "纹理";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.spinEditTextureU;
            this.layoutControlItem11.CustomizationFormText = "纹理U";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem11.Text = "纹理U";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.spinEditTextureV;
            this.layoutControlItem13.CustomizationFormText = "纹理V";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem13.Text = "纹理V";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.spinEditTextureRotateAngle;
            this.layoutControlItem14.CustomizationFormText = "旋转角度";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem14.Text = "旋转角度";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.buttonEditLineTexture;
            this.layoutControlItem8.CustomizationFormText = "纹理";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem8.Name = "layoutControlItem4";
            this.layoutControlItem8.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem8.Text = "纹理";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(48, 14);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.spinEditRepeat;
            this.layoutControlItem9.CustomizationFormText = "重复度";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem9.Name = "layoutControlItem5";
            this.layoutControlItem9.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem9.Text = "重复度";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(48, 14);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // UCPolygonSymbol
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPolygonSymbol";
            this.Size = new System.Drawing.Size(306, 331);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLineAlpha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTextureRotateAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTextureV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTextureU.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditSurfaceTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRepeat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditLineTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSunlight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLineWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineColorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditFillAlpha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fillColorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
