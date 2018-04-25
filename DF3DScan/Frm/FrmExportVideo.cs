using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.Core;

namespace DF3DScan.Frm
{
    public class ExportVideoDlg : XtraForm
    {
        private System.ComponentModel.IContainer components;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private TextEdit txtOpenFile;
        private LayoutControlItem layoutControlItem1;
        private SimpleButton btnBrowse;
        private LayoutControlItem layoutControlItem2;
        private SpinEdit txtFPS;
        private LayoutControlItem layoutControlItem3;
        private SimpleButton btnOK;
        private LayoutControlItem layoutControlItem5;
        private SimpleButton btnCancel;
        private EmptySpaceItem emptySpaceItem1;
        private EmptySpaceItem emptySpaceItem2;
        private LayoutControlItem layoutControlItem6;
        private EmptySpaceItem emptySpaceItem3;
        private DXErrorProvider dxErrorProvider1;
        private SpinEdit spinEditWidth;
        private RadioGroup radioGroup1;
        private SpinEdit spinEditHeight;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem7;
        private LayoutControlItem layoutControlItem8;
        private bool isNeedExport;
        private string strFileter = string.Empty;
        public bool IsNeedExport
        {
            get
            {
                return this.isNeedExport;
            }
            set
            {
                this.isNeedExport = value;
            }
        }
        public string FilePath
        {
            get
            {
                return this.txtOpenFile.Text.Trim();
            }
        }
        public int FPS
        {
            get
            {
                double num = 0.0;
                double.TryParse(this.txtFPS.Text, out num);
                return (int)num;
            }
        }
        public int WidthPx
        {
            get
            {
                return int.Parse(this.spinEditWidth.Text);
            }
        }
        public int HeightPx
        {
            get
            {
                return int.Parse(this.spinEditHeight.Text);
            }
        }
        public int Type
        {
            get
            {
                return this.radioGroup1.SelectedIndex;
            }
        }
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
            this.spinEditWidth = new DevExpress.XtraEditors.SpinEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.txtFPS = new DevExpress.XtraEditors.SpinEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.spinEditHeight = new DevExpress.XtraEditors.SpinEdit();
            this.txtOpenFile = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFPS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpenFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spinEditWidth);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.txtFPS);
            this.layoutControl1.Controls.Add(this.btnBrowse);
            this.layoutControl1.Controls.Add(this.spinEditHeight);
            this.layoutControl1.Controls.Add(this.txtOpenFile);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(434, 178);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spinEditWidth
            // 
            this.spinEditWidth.EditValue = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.spinEditWidth.Enabled = false;
            this.spinEditWidth.Location = new System.Drawing.Point(39, 118);
            this.spinEditWidth.Name = "spinEditWidth";
            this.spinEditWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditWidth.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spinEditWidth.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditWidth.Size = new System.Drawing.Size(176, 22);
            this.spinEditWidth.StyleController = this.layoutControl1;
            this.spinEditWidth.TabIndex = 4;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = 0;
            this.radioGroup1.Location = new System.Drawing.Point(39, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "视频"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "序列帧")});
            this.radioGroup1.Size = new System.Drawing.Size(383, 50);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // txtFPS
            // 
            this.txtFPS.EditValue = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.txtFPS.Location = new System.Drawing.Point(39, 92);
            this.txtFPS.Name = "txtFPS";
            this.txtFPS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFPS.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtFPS.Properties.MinValue = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtFPS.Size = new System.Drawing.Size(383, 22);
            this.txtFPS.StyleController = this.layoutControl1;
            this.txtFPS.TabIndex = 3;
            this.txtFPS.ToolTip = "请输入20~30之间的数";
            this.txtFPS.Validating += new System.ComponentModel.CancelEventHandler(this.txtFPS_Validating);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(351, 66);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(71, 22);
            this.btnBrowse.StyleController = this.layoutControl1;
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // spinEditHeight
            // 
            this.spinEditHeight.EditValue = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.spinEditHeight.Enabled = false;
            this.spinEditHeight.Location = new System.Drawing.Point(246, 118);
            this.spinEditHeight.Name = "spinEditHeight";
            this.spinEditHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditHeight.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spinEditHeight.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditHeight.Size = new System.Drawing.Size(176, 22);
            this.spinEditHeight.StyleController = this.layoutControl1;
            this.spinEditHeight.TabIndex = 5;
            // 
            // txtOpenFile
            // 
            this.txtOpenFile.Location = new System.Drawing.Point(39, 66);
            this.txtOpenFile.Name = "txtOpenFile";
            this.txtOpenFile.Size = new System.Drawing.Size(308, 22);
            this.txtOpenFile.StyleController = this.layoutControl1;
            this.txtOpenFile.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(206, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(306, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem6,
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(434, 178);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtOpenFile;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(339, 26);
            this.layoutControlItem1.Text = "位置";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnBrowse;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(339, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(75, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtFPS;
            this.layoutControlItem3.CustomizationFormText = "帧数";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(414, 26);
            this.layoutControlItem3.Text = "帧数";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOK;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(194, 132);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(90, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 132);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(194, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(284, 132);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(294, 132);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(92, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(386, 132);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(28, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.radioGroup1;
            this.layoutControlItem4.CustomizationFormText = "类型";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(414, 54);
            this.layoutControlItem4.Text = "类型";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.spinEditWidth;
            this.layoutControlItem7.CustomizationFormText = "宽度";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 106);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem7.Text = "宽度";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(24, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.spinEditHeight;
            this.layoutControlItem8.CustomizationFormText = "高度";
            this.layoutControlItem8.Location = new System.Drawing.Point(207, 106);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem8.Text = "高度";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(24, 14);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // ExportVideoDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 178);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportVideoDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出视频(序列帧图)";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFPS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpenFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }
        public ExportVideoDlg()
        {
            this.InitializeComponent();
            this.txtOpenFile.Text = string.Concat(new string[]
			{
				System.Windows.Forms.Application.StartupPath, 
				"\\Video", 
				System.DateTime.Now.ToLongDateString(), 
				System.DateTime.Now.ToLongTimeString().Replace(":", ""), 
				".avi"
			});
            this.txtFPS.Text = "25";
            this.strFileter = "Video Files(*.avi)|*.avi|Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
        }
        private void radioGroup1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.spinEditHeight.Enabled = false;
                this.spinEditWidth.Enabled = false;
                this.txtOpenFile.Text = string.Concat(new string[]
				{
					System.Windows.Forms.Application.StartupPath, 
					"\\Video", 
					System.DateTime.Now.ToLongDateString(), 
					System.DateTime.Now.ToLongTimeString().Replace(":", ""), 
					".avi"
				});
                this.strFileter = "Video Files(*.avi)|*.avi|Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
                return;
            }
            this.spinEditHeight.Enabled = true;
            this.spinEditWidth.Enabled = true;
            this.txtOpenFile.Text = string.Concat(new string[]
			{
				System.Windows.Forms.Application.StartupPath, 
				"\\Video", 
				System.DateTime.Now.ToLongDateString(), 
				System.DateTime.Now.ToLongTimeString().Replace(":", ""), 
				".bmp"
			});
            this.strFileter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|Image Files(*.JPG)|*.JPG";
        }
        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = this.strFileter;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtOpenFile.Text = saveFileDialog.FileName;
            }
        }
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtOpenFile.Text.Trim()) || !System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(this.txtOpenFile.Text)))
                {
                    XtraMessageBox.Show("路径错误！", "提示");
                    this.txtOpenFile.Text = "";
                    this.txtOpenFile.Focus();
                }
                else
                {
                    if (this.txtOpenFile.Text.LastIndexOf(".avi") == -1 && this.txtOpenFile.Text.LastIndexOf(".bmp") == -1 && this.txtOpenFile.Text.LastIndexOf(".png") == -1 && this.txtOpenFile.Text.LastIndexOf(".jpg") == -1 && this.txtOpenFile.Text.LastIndexOf(".AVI") == -1 && this.txtOpenFile.Text.LastIndexOf(".BMP") == -1 && this.txtOpenFile.Text.LastIndexOf(".PNG") == -1 && this.txtOpenFile.Text.LastIndexOf(".JPG") == -1)
                    {
                        XtraMessageBox.Show("文件类型错误！", "提示");
                        this.txtOpenFile.Text = "";
                        this.txtOpenFile.Focus();
                    }
                    else
                    {
                        this.isNeedExport = true;
                        base.Close();
                    }
                }
            }
            catch (System.Exception)
            {
                XtraMessageBox.Show("路径错误！", "提示");
                this.txtOpenFile.Text = "";
                this.txtOpenFile.Focus();
            }
        }
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.txtOpenFile.Text = "";
        }
        private void Validate_LessThanMinRule(BaseEdit control, decimal min)
        {
            if (!(control.EditValue is decimal))
            {
                return;
            }
            if ((decimal)control.EditValue < min)
            {
                string errorText = "输入值不能小于" + min.ToString();
                this.dxErrorProvider1.SetError(control, errorText, ErrorType.Warning);
                return;
            }
            this.dxErrorProvider1.SetError(control, "");
        }
        private void txtFPS_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Validate_LessThanMinRule(sender as BaseEdit, 20m);
        }
    }
}
