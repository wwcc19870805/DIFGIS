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
    public class UCModelPointSymbol : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private CheckEdit checkEditEnableColor;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private CheckEdit chkTexture;
        private SpinEdit spinEditAlpha;
        private ColorEdit colorEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    
        public UCModelPointSymbol()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkEditEnableColor = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkTexture = new DevExpress.XtraEditors.CheckEdit();
            this.spinEditAlpha = new DevExpress.XtraEditors.SpinEdit();
            this.colorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditAlpha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.checkEditEnableColor;
            this.layoutControlItem3.CustomizationFormText = "启用颜色";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(132, 26);
            this.layoutControlItem3.Text = "启用颜色";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // checkEditEnableColor
            // 
            this.checkEditEnableColor.Location = new System.Drawing.Point(63, 35);
            this.checkEditEnableColor.Name = "checkEditEnableColor";
            this.checkEditEnableColor.Properties.Caption = "";
            this.checkEditEnableColor.Size = new System.Drawing.Size(77, 19);
            this.checkEditEnableColor.StyleController = this.layoutControl1;
            this.checkEditEnableColor.TabIndex = 1;
            this.checkEditEnableColor.CheckedChanged += new System.EventHandler(this.checkEditEnableColor_CheckedChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkTexture);
            this.layoutControl1.Controls.Add(this.checkEditEnableColor);
            this.layoutControl1.Controls.Add(this.spinEditAlpha);
            this.layoutControl1.Controls.Add(this.colorEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(313, 97);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkTexture
            // 
            this.chkTexture.EditValue = true;
            this.chkTexture.Location = new System.Drawing.Point(63, 12);
            this.chkTexture.Name = "chkTexture";
            this.chkTexture.Properties.Caption = "";
            this.chkTexture.Size = new System.Drawing.Size(238, 19);
            this.chkTexture.StyleController = this.layoutControl1;
            this.chkTexture.TabIndex = 0;
            // 
            // spinEditAlpha
            // 
            this.spinEditAlpha.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditAlpha.Location = new System.Drawing.Point(63, 61);
            this.spinEditAlpha.Name = "spinEditAlpha";
            this.spinEditAlpha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditAlpha.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinEditAlpha.Size = new System.Drawing.Size(238, 22);
            this.spinEditAlpha.StyleController = this.layoutControl1;
            this.spinEditAlpha.TabIndex = 3;
            // 
            // colorEdit
            // 
            this.colorEdit.EditValue = System.Drawing.Color.Empty;
            this.colorEdit.Location = new System.Drawing.Point(195, 35);
            this.colorEdit.Name = "colorEdit";
            this.colorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit.Properties.StoreColorAsInteger = true;
            this.colorEdit.Size = new System.Drawing.Size(106, 22);
            this.colorEdit.StyleController = this.layoutControl1;
            this.colorEdit.TabIndex = 2;
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
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(313, 97);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.colorEdit;
            this.layoutControlItem1.CustomizationFormText = "颜色";
            this.layoutControlItem1.Location = new System.Drawing.Point(132, 23);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(161, 26);
            this.layoutControlItem1.Text = "颜色";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinEditAlpha;
            this.layoutControlItem2.CustomizationFormText = "不透明度";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(293, 28);
            this.layoutControlItem2.Text = "不透明度";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkTexture;
            this.layoutControlItem4.CustomizationFormText = "启用纹理";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(293, 23);
            this.layoutControlItem4.Text = "启用纹理";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // UCModelPointSymbol
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCModelPointSymbol";
            this.Size = new System.Drawing.Size(313, 97);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditEnableColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditAlpha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        private void checkEditEnableColor_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
