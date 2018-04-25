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
    public class UCPointSymbol : XtraUserControl
    {
        private ColorEdit colorEdit;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ComboBoxEdit cmbPivotAlign;
        private ButtonEdit buttonEditFilePath;
        private ComboBoxEdit cmbSysRes;
        private ComboBoxEdit cmbType;
        private SpinEdit spinEditSize;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem sysReslayoutControl;
        private DevExpress.XtraLayout.LayoutControlItem localReslayoutControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControl;
    
        public UCPointSymbol()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.colorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbPivotAlign = new DevExpress.XtraEditors.ComboBoxEdit();
            this.buttonEditFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbSysRes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEditSize = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sysReslayoutControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.localReslayoutControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPivotAlign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSysRes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sysReslayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localReslayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.SuspendLayout();
            // 
            // colorEdit
            // 
            this.colorEdit.EditValue = System.Drawing.Color.Lime;
            this.colorEdit.Location = new System.Drawing.Point(63, 90);
            this.colorEdit.Name = "colorEdit";
            this.colorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit.Size = new System.Drawing.Size(211, 22);
            this.colorEdit.StyleController = this.layoutControl1;
            this.colorEdit.TabIndex = 3;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbPivotAlign);
            this.layoutControl1.Controls.Add(this.buttonEditFilePath);
            this.layoutControl1.Controls.Add(this.cmbSysRes);
            this.layoutControl1.Controls.Add(this.cmbType);
            this.layoutControl1.Controls.Add(this.colorEdit);
            this.layoutControl1.Controls.Add(this.spinEditSize);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(329, 108, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(286, 178);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbPivotAlign
            // 
            this.cmbPivotAlign.EditValue = "中下";
            this.cmbPivotAlign.Location = new System.Drawing.Point(63, 38);
            this.cmbPivotAlign.Name = "cmbPivotAlign";
            this.cmbPivotAlign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPivotAlign.Properties.Items.AddRange(new object[] {
            "左下",
            "中下",
            "右下",
            "左中",
            "正中",
            "右中",
            "左上",
            "中上",
            "右上"});
            this.cmbPivotAlign.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPivotAlign.Size = new System.Drawing.Size(211, 22);
            this.cmbPivotAlign.StyleController = this.layoutControl1;
            this.cmbPivotAlign.TabIndex = 1;
            // 
            // buttonEditFilePath
            // 
            this.buttonEditFilePath.Location = new System.Drawing.Point(63, 142);
            this.buttonEditFilePath.Name = "buttonEditFilePath";
            this.buttonEditFilePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditFilePath.Properties.ReadOnly = true;
            this.buttonEditFilePath.Size = new System.Drawing.Size(211, 22);
            this.buttonEditFilePath.StyleController = this.layoutControl1;
            this.buttonEditFilePath.TabIndex = 5;
            this.buttonEditFilePath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFilePath_ButtonClick);
            // 
            // cmbSysRes
            // 
            this.cmbSysRes.EditValue = "圆形";
            this.cmbSysRes.Location = new System.Drawing.Point(12, 116);
            this.cmbSysRes.Name = "cmbSysRes";
            this.cmbSysRes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSysRes.Properties.Items.AddRange(new object[] {
            "圆形",
            "方形",
            "十字形",
            "叉形",
            "菱形"});
            this.cmbSysRes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSysRes.Size = new System.Drawing.Size(262, 22);
            this.cmbSysRes.StyleController = this.layoutControl1;
            this.cmbSysRes.TabIndex = 4;
            // 
            // cmbType
            // 
            this.cmbType.EditValue = "系统";
            this.cmbType.Location = new System.Drawing.Point(63, 64);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.Items.AddRange(new object[] {
            "系统",
            "本地"});
            this.cmbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbType.Size = new System.Drawing.Size(211, 22);
            this.cmbType.StyleController = this.layoutControl1;
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // spinEditSize
            // 
            this.spinEditSize.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinEditSize.Location = new System.Drawing.Point(63, 12);
            this.spinEditSize.Name = "spinEditSize";
            this.spinEditSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEditSize.Properties.IsFloatValue = false;
            this.spinEditSize.Properties.Mask.EditMask = "N00";
            this.spinEditSize.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEditSize.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditSize.Size = new System.Drawing.Size(211, 22);
            this.spinEditSize.StyleController = this.layoutControl1;
            this.spinEditSize.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.sysReslayoutControl,
            this.localReslayoutControl,
            this.layoutControlItem6,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(286, 178);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.spinEditSize;
            this.layoutControlItem1.CustomizationFormText = "大小";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem1.Text = "大小";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbType;
            this.layoutControlItem4.CustomizationFormText = "符合资源";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem4.Text = "符号资源";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // sysReslayoutControl
            // 
            this.sysReslayoutControl.Control = this.cmbSysRes;
            this.sysReslayoutControl.CustomizationFormText = "sysReslayoutControl";
            this.sysReslayoutControl.Location = new System.Drawing.Point(0, 104);
            this.sysReslayoutControl.Name = "sysReslayoutControl";
            this.sysReslayoutControl.Size = new System.Drawing.Size(266, 26);
            this.sysReslayoutControl.Text = "sysReslayoutControl";
            this.sysReslayoutControl.TextSize = new System.Drawing.Size(0, 0);
            this.sysReslayoutControl.TextToControlDistance = 0;
            this.sysReslayoutControl.TextVisible = false;
            this.sysReslayoutControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // localReslayoutControl
            // 
            this.localReslayoutControl.Control = this.buttonEditFilePath;
            this.localReslayoutControl.CustomizationFormText = "选择文件";
            this.localReslayoutControl.Location = new System.Drawing.Point(0, 130);
            this.localReslayoutControl.Name = "localReslayoutControl";
            this.localReslayoutControl.Size = new System.Drawing.Size(266, 28);
            this.localReslayoutControl.Text = "选择文件";
            this.localReslayoutControl.TextSize = new System.Drawing.Size(48, 14);
            this.localReslayoutControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cmbPivotAlign;
            this.layoutControlItem6.CustomizationFormText = "布局";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem6.Text = "布局";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.colorEdit;
            this.layoutControlItem2.CustomizationFormText = "颜色";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(266, 26);
            this.layoutControlItem2.Text = "颜色";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControl
            // 
            this.layoutControl.Control = this.buttonEditFilePath;
            this.layoutControl.CustomizationFormText = "layoutControlItem6";
            this.layoutControl.Location = new System.Drawing.Point(0, 120);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Size = new System.Drawing.Size(329, 24);
            this.layoutControl.Text = "layoutControl";
            this.layoutControl.TextSize = new System.Drawing.Size(48, 14);
            this.layoutControl.TextToControlDistance = 5;
            // 
            // UCPointSymbol
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPointSymbol";
            this.Size = new System.Drawing.Size(286, 178);
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPivotAlign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSysRes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sysReslayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localReslayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.ResumeLayout(false);

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonEditFilePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}
