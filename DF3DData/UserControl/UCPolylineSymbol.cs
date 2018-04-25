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
    public class UCPolylineSymbol : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private SpinEdit spinEditAlpha;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SpinEdit spinEditRepeat;
        private ButtonEdit buttonEditTexture;
        private SpinEdit spinEditLineWidth;
        private ColorEdit colorEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    
        public UCPolylineSymbol()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.spinEditAlpha = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spinEditRepeat = new DevExpress.XtraEditors.SpinEdit();
            this.buttonEditTexture = new DevExpress.XtraEditors.ButtonEdit();
            this.spinEditLineWidth = new DevExpress.XtraEditors.SpinEdit();
            this.colorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditAlpha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRepeat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLineWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEditAlpha;
            this.layoutControlItem2.CustomizationFormText = "不透明度";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem2.Text = "不透明度";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // spinEditAlpha
            // 
            this.spinEditAlpha.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditAlpha.Location = new System.Drawing.Point(63, 38);
            this.spinEditAlpha.Name = "spinEditAlpha";
            this.spinEditAlpha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditAlpha.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditAlpha.Size = new System.Drawing.Size(211, 22);
            this.spinEditAlpha.StyleController = this.layoutControl1;
            this.spinEditAlpha.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spinEditRepeat);
            this.layoutControl1.Controls.Add(this.buttonEditTexture);
            this.layoutControl1.Controls.Add(this.spinEditLineWidth);
            this.layoutControl1.Controls.Add(this.spinEditAlpha);
            this.layoutControl1.Controls.Add(this.colorEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(286, 150);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spinEditRepeat
            // 
            this.spinEditRepeat.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditRepeat.Location = new System.Drawing.Point(63, 116);
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
            this.spinEditRepeat.Size = new System.Drawing.Size(211, 22);
            this.spinEditRepeat.StyleController = this.layoutControl1;
            this.spinEditRepeat.TabIndex = 4;
            // 
            // buttonEditTexture
            // 
            this.buttonEditTexture.Location = new System.Drawing.Point(63, 90);
            this.buttonEditTexture.Name = "buttonEditTexture";
            this.buttonEditTexture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditTexture.Size = new System.Drawing.Size(211, 22);
            this.buttonEditTexture.StyleController = this.layoutControl1;
            this.buttonEditTexture.TabIndex = 3;
            // 
            // spinEditLineWidth
            // 
            this.spinEditLineWidth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditLineWidth.Location = new System.Drawing.Point(63, 64);
            this.spinEditLineWidth.Name = "spinEditLineWidth";
            this.spinEditLineWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditLineWidth.Size = new System.Drawing.Size(211, 22);
            this.spinEditLineWidth.StyleController = this.layoutControl1;
            this.spinEditLineWidth.TabIndex = 2;
            // 
            // colorEdit
            // 
            this.colorEdit.EditValue = System.Drawing.Color.Lime;
            this.colorEdit.Location = new System.Drawing.Point(63, 12);
            this.colorEdit.Name = "colorEdit";
            this.colorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colorEdit.Size = new System.Drawing.Size(211, 22);
            this.colorEdit.StyleController = this.layoutControl1;
            this.colorEdit.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(286, 150);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.colorEdit;
            this.layoutControlItem1.CustomizationFormText = "颜色";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem1.Text = "颜色";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spinEditLineWidth;
            this.layoutControlItem3.CustomizationFormText = "线宽";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem3.Text = "线宽";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.buttonEditTexture;
            this.layoutControlItem4.CustomizationFormText = "纹理";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem4.Text = "纹理";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinEditRepeat;
            this.layoutControlItem5.CustomizationFormText = "重复度";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem5.Text = "重复度";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14);
            // 
            // UCPolylineSymbol
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPolylineSymbol";
            this.Size = new System.Drawing.Size(286, 150);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditAlpha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditRepeat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditLineWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
