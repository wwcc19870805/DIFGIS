using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICSharpCode.Core;
using DF3DScan.Class;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraVerticalGrid;

namespace DF3DScan.Frm
{
    public class FormEditLocation : XtraForm
    {
        private HashSet<string> _locationNames;
        private CameraProperty cp;
        private PropertyObject obj;
        private string _oldname = string.Empty;
        private System.ComponentModel.IContainer components;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private SimpleButton btnOK;
        private LayoutControlItem layoutControlItem3;
        private PropertyGridControl propertyGridControl1;
        private LayoutControlItem layoutControlItem1;
        private PropertyDescriptionControl propertyDescriptionControl1;
        private LayoutControlItem layoutControlItem2;
        private EmptySpaceItem emptySpaceItem1;
        private RepositoryItemSpinEdit txtDuration;
        private RepositoryItemMemoEdit txtComment;
        private EditorRow row;
        private EditorRow r_name;
        private EditorRow r_comment;
        private EditorRow r_duration;
        private EditorRow r_x;
        private EditorRow r_y;
        private EditorRow r_z;
        private EditorRow row11;
        private RepositoryItemTextEdit txtName;
        private EditorRow r_heading;
        private EditorRow r_tilt;
        private SimpleButton btnCancel;
        private LayoutControlItem layoutControlItem4;
        private EditorRow r_roll;
        public string LocationName
        {
            get
            {
                return this.obj.Name;
            }
        }
        public string Comment
        {
            get
            {
                return this.obj.Comment;
            }
        }
        public double Duration
        {
            get
            {
                return this.obj.Duration;
            }
        }
        public FormEditLocation(string name, string comment, object duration, object location, HashSet<string> locationNames)
        {
            this.InitializeComponent();
            if (this.obj == null)
            {
                this.obj = new PropertyObject();
            }
            this.obj.Name = name;
            this.obj.Comment = comment;
            this.obj.Duration = double.Parse(duration.ToString());
            this.cp = (CameraProperty)location;
            this.obj.X = this.cp.X;
            this.obj.Y = this.cp.Y;
            this.obj.Z = this.cp.Z;
            this.obj.Heading = System.Math.Round(this.cp.Heading, 2);
            this.obj.Tilt = System.Math.Round(this.cp.Tilt, 2);
            this.obj.Roll = System.Math.Round(this.cp.Roll, 2);
            this.propertyGridControl1.SelectedObject = this.obj;
            this._oldname = name;
            this._locationNames = locationNames;
        }
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (this.LocationName.Trim() == "")
            {
                XtraMessageBox.Show("特定场景名称不能为空！", "提示");
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            if (this.LocationName != this._oldname && this._locationNames.Contains(this.LocationName))
            {
                XtraMessageBox.Show("已存在该特定场景！", "提示");
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            if (this.Duration == 0.0)
            {
                XtraMessageBox.Show("播放间隔无效，请查看！", "提示");
                base.DialogResult = System.Windows.Forms.DialogResult.None;
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.propertyDescriptionControl1 = new DevExpress.XtraVerticalGrid.PropertyDescriptionControl();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.txtDuration = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.txtComment = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.txtName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.row = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_name = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_comment = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_duration = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.row11 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_x = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_y = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_z = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_heading = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_tilt = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.r_roll = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.propertyDescriptionControl1);
            this.layoutControl1.Controls.Add(this.propertyGridControl1);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(601, 110, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(312, 374);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(161, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(149, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // propertyDescriptionControl1
            // 
            this.propertyDescriptionControl1.Location = new System.Drawing.Point(2, 282);
            this.propertyDescriptionControl1.Name = "propertyDescriptionControl1";
            this.propertyDescriptionControl1.PropertyGrid = this.propertyGridControl1;
            this.propertyDescriptionControl1.Size = new System.Drawing.Size(308, 64);
            this.propertyDescriptionControl1.TabIndex = 1;
            this.propertyDescriptionControl1.TabStop = false;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Location = new System.Drawing.Point(2, 2);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.OptionsBehavior.ResizeHeaderPanel = false;
            this.propertyGridControl1.OptionsBehavior.ResizeRowHeaders = false;
            this.propertyGridControl1.OptionsBehavior.ResizeRowValues = false;
            this.propertyGridControl1.OptionsBehavior.UseDefaultEditorsCollection = false;
            this.propertyGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtDuration,
            this.txtComment,
            this.txtName});
            this.propertyGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.row,
            this.row11});
            this.propertyGridControl1.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowAlways;
            this.propertyGridControl1.Size = new System.Drawing.Size(308, 265);
            this.propertyGridControl1.TabIndex = 0;
            // 
            // txtDuration
            // 
            this.txtDuration.AutoHeight = false;
            this.txtDuration.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDuration.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.txtDuration.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDuration.Name = "txtDuration";
            // 
            // txtComment
            // 
            this.txtComment.MaxLength = 256;
            this.txtComment.Name = "txtComment";
            // 
            // txtName
            // 
            this.txtName.AutoHeight = false;
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            // 
            // row
            // 
            this.row.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.r_name,
            this.r_comment,
            this.r_duration});
            this.row.Name = "row";
            this.row.Properties.Caption = "编辑";
            this.row.Properties.FieldName = "Property1";
            this.row.Properties.ReadOnly = true;
            // 
            // r_name
            // 
            this.r_name.Name = "r_name";
            this.r_name.Properties.Caption = "名称";
            this.r_name.Properties.FieldName = "Name";
            this.r_name.Properties.RowEdit = this.txtName;
            // 
            // r_comment
            // 
            this.r_comment.Height = 70;
            this.r_comment.Name = "r_comment";
            this.r_comment.Properties.Caption = "说明";
            this.r_comment.Properties.FieldName = "Comment";
            this.r_comment.Properties.RowEdit = this.txtComment;
            // 
            // r_duration
            // 
            this.r_duration.Name = "r_duration";
            this.r_duration.Properties.Caption = "播放间隔(秒)";
            this.r_duration.Properties.FieldName = "Duration";
            this.r_duration.Properties.RowEdit = this.txtDuration;
            // 
            // row11
            // 
            this.row11.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.r_x,
            this.r_y,
            this.r_z,
            this.r_heading,
            this.r_tilt,
            this.r_roll});
            this.row11.Name = "row11";
            this.row11.Properties.Caption = "相机位置";
            this.row11.Properties.FieldName = "Property2";
            this.row11.Properties.ReadOnly = true;
            // 
            // r_x
            // 
            this.r_x.Name = "r_x";
            this.r_x.Properties.Caption = "X";
            this.r_x.Properties.FieldName = "X";
            this.r_x.Properties.ReadOnly = true;
            // 
            // r_y
            // 
            this.r_y.Name = "r_y";
            this.r_y.Properties.Caption = "Y";
            this.r_y.Properties.FieldName = "Y";
            this.r_y.Properties.ReadOnly = true;
            // 
            // r_z
            // 
            this.r_z.Name = "r_z";
            this.r_z.Properties.Caption = "Z";
            this.r_z.Properties.FieldName = "Z";
            this.r_z.Properties.ReadOnly = true;
            // 
            // r_heading
            // 
            this.r_heading.Name = "r_heading";
            this.r_heading.Properties.Caption = "Heading";
            this.r_heading.Properties.FieldName = "Heading";
            this.r_heading.Properties.ReadOnly = true;
            // 
            // r_tilt
            // 
            this.r_tilt.Name = "r_tilt";
            this.r_tilt.Properties.Caption = "Tilt";
            this.r_tilt.Properties.FieldName = "Tilt";
            this.r_tilt.Properties.ReadOnly = true;
            // 
            // r_roll
            // 
            this.r_roll.Name = "r_roll";
            this.r_roll.Properties.Caption = "Roll";
            this.r_roll.Properties.FieldName = "Roll";
            this.r_roll.Properties.ReadOnly = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(2, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(155, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(312, 374);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOK;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 348);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(159, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.propertyGridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(312, 269);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.propertyDescriptionControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 280);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(312, 68);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 269);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(312, 11);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(159, 348);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(153, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FormEditLocation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 374);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormEditLocation";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特定场景属性";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
