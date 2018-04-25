using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using DF3DPipeCreateTool.Class;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using System.IO;
using System.Drawing.Imaging;
using Gvitech.CityMaker.Common;
namespace DF3DPipeCreateTool.Frm
{
    public class FrmEditColor : XtraForm
    {
        // Fields
        private ColorClass _color;
        private bool _isEdit;
        private IDataSource _ds;
        private string _groupId;
        private HashSet<string> _colorNames;
        private SimpleButton btnCancel;
        private SimpleButton btnSave;
        public ComboBoxEdit cbxColorType;
        public ColorEdit colorValue;
        private IContainer components;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private PanelControl panelControl1;
        internal TrackBarControl traktTansparency;
        public TextEdit txtColorName;

        // Methods
        public FrmEditColor(IDataSource ds, string groupId, HashSet<string> colorNames = null)
        {
            this.InitializeComponent();
            this._ds = ds;
            this._groupId = groupId;
            this._isEdit = false;
            this.Text = "添加颜色";
            this.txtColorName.Enabled = true;
            this._color = new ColorClass();
            this._color.Group = groupId;
            this._color.ObjectId = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
        }

        public FrmEditColor(IDataSource ds, ColorClass color, HashSet<string> colorNames = null)
        {
            this.InitializeComponent();
            this._ds = ds;
            this.txtColorName.Enabled = false;
            this._isEdit = true;
            this._color = color;
            this.Text = "编辑颜色";
            if (color == null)
                this._colorNames = new HashSet<string>();
            else
                this._colorNames = colorNames;
            if (this._color != null)
            {
                this.txtColorName.Text = this._color.Name;
                if (!string.IsNullOrEmpty(this._color.Code))
                {
                    this.colorValue.Color =System.Drawing.Color.FromArgb(int.Parse(this._color.Code));
                    this.traktTansparency.Value = this.colorValue.Color.A;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private bool InsertColor()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return false;

                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                row.SetValue(row.FieldIndex("Name"), this._color.Name);
                row.SetValue(row.FieldIndex("ObjectId"), this._color.ObjectId);
                row.SetValue(row.FieldIndex("GroupId"), this._color.Group);
                row.SetValue(row.FieldIndex("Code"), this._color.Code);
                row.SetValue(row.FieldIndex("Type"), this._color.Type);
                if (this._color.Thumbnail != null)
                {
                    try
                    {
                        IBinaryBuffer bb = new BinaryBufferClass();
                        MemoryStream stream = new MemoryStream();
                        this._color.Thumbnail.Save(stream, ImageFormat.Png);
                        bb.FromByteArray(stream.ToArray());
                        row.SetValue(row.FieldIndex("Thumbnail"), bb);
                    }
                    catch (Exception exception)
                    {
                    }
                }
                row.SetValue(row.FieldIndex("Comment"), this._color.Comment);
                cursor.InsertRow(row);
                this._color.Id = cursor.LastInsertId;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }

        private bool UpdateColor()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", this._color.ObjectId)
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("Name"), this._color.Name);
                    row.SetValue(row.FieldIndex("GroupId"), this._color.Group);
                    row.SetValue(row.FieldIndex("Code"), this._color.Code);
                    row.SetValue(row.FieldIndex("Type"), this._color.Type);
                    row.SetValue(row.FieldIndex("Comment"), this._color.Comment);
                    if (this._color.Thumbnail != null)
                    {
                        try
                        {
                            IBinaryBuffer newVal = new BinaryBufferClass();
                            MemoryStream stream = new MemoryStream();
                            this._color.Thumbnail.Save(stream, ImageFormat.Png);
                            newVal.FromByteArray(stream.ToArray());
                            row.SetValue(2, newVal);
                        }
                        catch (Exception ex)
                        {
                        }
                    } 
                    cursor.UpdateRow(row);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (cursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                    cursor = null;
                }
                if (row != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                    row = null;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Color color = this.colorValue.Color;
                Bitmap image = new Bitmap(0x100, 0x100);
                Graphics.FromImage(image).FillRectangle(new SolidBrush(color), new Rectangle(0, 0, 0x100, 0x100));
                this._color.Name = this.txtColorName.Text.Trim();
                this._color.Thumbnail = image;
                this._color.Code = this.colorValue.Color.ToArgb().ToString();

                if (this._colorNames != null && this._colorNames.Contains(this._color.Name))
                {
                    XtraMessageBox.Show("颜色名称已存在！", "提示");
                    return;
                }
                if (!this._isEdit)
                {
                    if (!InsertColor())
                    {
                        XtraMessageBox.Show("颜色入库失败！", "提示");
                        return;
                    }
                }
                if (!UpdateColor())
                {
                    XtraMessageBox.Show("更新颜色出错！", "提示");
                    return;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {

            }
        }

        private void colorAlpha_EditValueChanged(object sender, EventArgs e)
        {
            this.colorValue.EditValueChanged -= new EventHandler(this.ColorValueCE_EditValueChanged);
            System.Drawing.Color color = this.colorValue.Color;
            this.colorValue.Color = System.Drawing.Color.FromArgb(this.traktTansparency.Value, color.R, color.G, color.B);
            this.colorValue.EditValueChanged += new EventHandler(this.ColorValueCE_EditValueChanged);
        }

        private void ColorValueCE_EditValueChanged(object sender, EventArgs e)
        {
            this.traktTansparency.Value = 0xff;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtColorName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.traktTansparency = new DevExpress.XtraEditors.TrackBarControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.colorValue = new DevExpress.XtraEditors.ColorEdit();
            this.cbxColorType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtColorName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traktTansparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.traktTansparency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxColorType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtColorName
            // 
            this.txtColorName.Location = new System.Drawing.Point(87, 18);
            this.txtColorName.Name = "txtColorName";
            this.txtColorName.Size = new System.Drawing.Size(217, 22);
            this.txtColorName.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "颜色名称:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(178, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(79, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.traktTansparency);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.colorValue);
            this.panelControl1.Location = new System.Drawing.Point(20, 87);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(299, 111);
            this.panelControl1.TabIndex = 9;
            // 
            // traktTansparency
            // 
            this.traktTansparency.EditValue = 255;
            this.traktTansparency.Location = new System.Drawing.Point(83, 58);
            this.traktTansparency.Name = "traktTansparency";
            this.traktTansparency.Properties.Maximum = 255;
            this.traktTansparency.Properties.ShowValueToolTip = true;
            this.traktTansparency.Properties.TickFrequency = 10;
            this.traktTansparency.Size = new System.Drawing.Size(201, 45);
            this.traktTansparency.TabIndex = 13;
            this.traktTansparency.Value = 255;
            this.traktTansparency.EditValueChanged += new System.EventHandler(this.colorAlpha_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 14);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "透明度:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "颜色值:";
            // 
            // colorValue
            // 
            this.colorValue.EditValue = System.Drawing.Color.Red;
            this.colorValue.Location = new System.Drawing.Point(83, 22);
            this.colorValue.Name = "colorValue";
            this.colorValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.colorValue.Size = new System.Drawing.Size(201, 22);
            this.colorValue.TabIndex = 4;
            this.colorValue.EditValueChanged += new System.EventHandler(this.ColorValueCE_EditValueChanged);
            // 
            // cbxColorType
            // 
            this.cbxColorType.EditValue = "ARGB";
            this.cbxColorType.Location = new System.Drawing.Point(87, 48);
            this.cbxColorType.Name = "cbxColorType";
            this.cbxColorType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxColorType.Properties.Items.AddRange(new object[] {
            "ARGB"});
            this.cbxColorType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxColorType.Size = new System.Drawing.Size(217, 22);
            this.cbxColorType.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "颜色类型:";
            // 
            // FrmEditColor
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(342, 245);
            this.Controls.Add(this.txtColorName);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.cbxColorType);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmEditColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加颜色";
            ((System.ComponentModel.ISupportInitialize)(this.txtColorName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traktTansparency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.traktTansparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxColorType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //private void InitValidationRules()
        //{
        //    this._validation = new InputValidationProvider();
        //    this._validation.SetNotBlanRule(this.txtColorName, "颜色名称不能为空！");
        //    this._validation.SetNotBlanRule(this.cbxColorType, "请选择颜色类型！");
        //    this._validation.SetNotBlanRule(this.colorValue, "请设置颜色值！");
        //}


        // Properties
        public ColorClass Color
        {
            get
            {
                return this._color;
            }
        }
    }

 

}
