using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using System.IO;
using DevExpress.XtraEditors.Controls;
using System.Drawing.Imaging;

namespace DF3DPipeCreateTool.Frm
{
    public class FrmPipeNodeStyle : XtraForm
    {
        private CheckEdit chkRotateZ;
        private LabelControl labelControl3;
        private HyperLinkEdit hlSelectImg;
        private PictureEdit picThumbnail;
        private LabelControl labelControl2;
        private PanelControl panelControl2;
        private TextEdit txtStyleName;
        private LabelControl labelControl1;
        private PanelControl panelControl3;
        private CheckEdit chkStretchZ;
        private CheckEdit chkBlendPipe;
        private CheckEdit chkFollowDir;
        private CheckEdit chkFollowSurfH;
        private LookUpEdit txtModelId;
        private CheckEdit chkFollowDia;
        private PanelControl panelControl1;
        private SimpleButton btnSave;
        private SimpleButton btnCancel;
    
        private void InitializeComponent()
        {
            this.chkRotateZ = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.hlSelectImg = new DevExpress.XtraEditors.HyperLinkEdit();
            this.picThumbnail = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtStyleName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.chkStretchZ = new DevExpress.XtraEditors.CheckEdit();
            this.chkBlendPipe = new DevExpress.XtraEditors.CheckEdit();
            this.chkFollowDir = new DevExpress.XtraEditors.CheckEdit();
            this.chkFollowSurfH = new DevExpress.XtraEditors.CheckEdit();
            this.txtModelId = new DevExpress.XtraEditors.LookUpEdit();
            this.chkFollowDia = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkRotateZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlSelectImg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkStretchZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBlendPipe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFollowDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFollowSurfH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModelId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFollowDia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkRotateZ
            // 
            this.chkRotateZ.Location = new System.Drawing.Point(184, 128);
            this.chkRotateZ.Name = "chkRotateZ";
            this.chkRotateZ.Properties.Caption = "是否绕Z轴旋转";
            this.chkRotateZ.Size = new System.Drawing.Size(115, 19);
            this.chkRotateZ.TabIndex = 15;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "选择模型：";
            // 
            // hlSelectImg
            // 
            this.hlSelectImg.EditValue = "选择";
            this.hlSelectImg.Location = new System.Drawing.Point(47, 5);
            this.hlSelectImg.Name = "hlSelectImg";
            this.hlSelectImg.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlSelectImg.Properties.Appearance.Options.UseBackColor = true;
            this.hlSelectImg.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlSelectImg.Size = new System.Drawing.Size(36, 20);
            this.hlSelectImg.TabIndex = 2;
            this.hlSelectImg.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hlSelectImg_OpenLink);
            // 
            // picThumbnail
            // 
            this.picThumbnail.Location = new System.Drawing.Point(8, 28);
            this.picThumbnail.Name = "picThumbnail";
            this.picThumbnail.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picThumbnail.Size = new System.Drawing.Size(177, 177);
            this.picThumbnail.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "缩略图";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.hlSelectImg);
            this.panelControl2.Controls.Add(this.picThumbnail);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Location = new System.Drawing.Point(335, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(194, 216);
            this.panelControl2.TabIndex = 17;
            // 
            // txtStyleName
            // 
            this.txtStyleName.Location = new System.Drawing.Point(84, 9);
            this.txtStyleName.Name = "txtStyleName";
            this.txtStyleName.Size = new System.Drawing.Size(215, 22);
            this.txtStyleName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "风格名称：";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.chkRotateZ);
            this.panelControl3.Controls.Add(this.chkStretchZ);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.chkBlendPipe);
            this.panelControl3.Controls.Add(this.chkFollowDir);
            this.panelControl3.Controls.Add(this.chkFollowSurfH);
            this.panelControl3.Controls.Add(this.txtModelId);
            this.panelControl3.Controls.Add(this.chkFollowDia);
            this.panelControl3.Location = new System.Drawing.Point(14, 53);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(315, 175);
            this.panelControl3.TabIndex = 20;
            // 
            // chkStretchZ
            // 
            this.chkStretchZ.Location = new System.Drawing.Point(184, 103);
            this.chkStretchZ.Name = "chkStretchZ";
            this.chkStretchZ.Properties.Caption = "是否沿Z方向拉伸";
            this.chkStretchZ.Size = new System.Drawing.Size(115, 19);
            this.chkStretchZ.TabIndex = 10;
            // 
            // chkBlendPipe
            // 
            this.chkBlendPipe.Location = new System.Drawing.Point(14, 128);
            this.chkBlendPipe.Name = "chkBlendPipe";
            this.chkBlendPipe.Properties.Caption = "是否自动融合连接管线";
            this.chkBlendPipe.Size = new System.Drawing.Size(173, 19);
            this.chkBlendPipe.TabIndex = 11;
            // 
            // chkFollowDir
            // 
            this.chkFollowDir.Location = new System.Drawing.Point(14, 103);
            this.chkFollowDir.Name = "chkFollowDir";
            this.chkFollowDir.Properties.Caption = "是否跟随管线方向";
            this.chkFollowDir.Size = new System.Drawing.Size(127, 19);
            this.chkFollowDir.TabIndex = 9;
            // 
            // chkFollowSurfH
            // 
            this.chkFollowSurfH.Location = new System.Drawing.Point(184, 78);
            this.chkFollowSurfH.Name = "chkFollowSurfH";
            this.chkFollowSurfH.Properties.Caption = "是否贴地显示";
            this.chkFollowSurfH.Size = new System.Drawing.Size(103, 19);
            this.chkFollowSurfH.TabIndex = 8;
            // 
            // txtModelId
            // 
            this.txtModelId.EditValue = "";
            this.txtModelId.Location = new System.Drawing.Point(84, 18);
            this.txtModelId.Name = "txtModelId";
            this.txtModelId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtModelId.Properties.ImmediatePopup = true;
            this.txtModelId.Properties.NullText = "——请选择模型——";
            this.txtModelId.Properties.ShowHeader = false;
            this.txtModelId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtModelId.Size = new System.Drawing.Size(215, 22);
            this.txtModelId.TabIndex = 5;
            this.txtModelId.EditValueChanged += new System.EventHandler(this.txtModelId_EditValueChanged);
            // 
            // chkFollowDia
            // 
            this.chkFollowDia.Location = new System.Drawing.Point(14, 78);
            this.chkFollowDia.Name = "chkFollowDia";
            this.chkFollowDia.Properties.Caption = "是否跟随管径";
            this.chkFollowDia.Size = new System.Drawing.Size(102, 19);
            this.chkFollowDia.TabIndex = 7;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtStyleName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(14, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(315, 35);
            this.panelControl1.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(359, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 22);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "创建";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 22);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmPipeNodeStyle
            // 
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(540, 274);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPipeNodeStyle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建—管点风格";
            this.Load += new System.EventHandler(this.FrmPipeNodeStyle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkRotateZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlSelectImg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkStretchZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBlendPipe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFollowDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFollowSurfH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModelId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFollowDia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        private IDataSource _ds;
        private PipeNodeStyleClass _style;
        private bool _edit;
        public PipeNodeStyleClass Style
        {
            get { return this._style; }
        }

        public FrmPipeNodeStyle()
        {
            InitializeComponent();
            this._style = new PipeNodeStyleClass();
            this._edit = false;
            this.Text = "创建-管点风格";
            this.btnSave.Text = "创建";
        }

        public FrmPipeNodeStyle(PipeNodeStyleClass pnsc)
        {
            InitializeComponent();
            this._style = pnsc;
            this._edit = true;
            this.Text = "编辑-管点风格";
            this.btnSave.Text = "保存";
        }

        private void FrmPipeNodeStyle_Load(object sender, EventArgs e)
        {
            this._ds = DF3DPipeCreateApp.App.TemplateLib;
            if (this._ds == null) { this.Enabled = false; return; }
            DataTable modelInfo = GetModelInfo();
            if (modelInfo != null)
            {
                this.txtModelId.Properties.DataSource = modelInfo;
                this.txtModelId.Properties.DisplayMember = "Name";
                this.txtModelId.Properties.ValueMember = "ObjectId";
                LookUpColumnInfo column = new LookUpColumnInfo("Name");
                this.txtModelId.Properties.Columns.Add(column);
            }
            if (this._edit)
            {
                this.txtStyleName.Text = this._style.Name;
                this.chkBlendPipe.Checked = this._style.IsBlendPipe;
                this.chkFollowDia.Checked = this._style.IsFollowDia;
                this.chkFollowDir.Checked = this._style.IsFollowDir;
                this.chkFollowSurfH.Checked = this._style.IsFollowSurfH;
                this.chkRotateZ.Checked = this._style.IsRotateZ;
                this.chkStretchZ.Checked = this._style.IsStretchZ;
                if (!string.IsNullOrEmpty(this._style.ModelId))
                {
                    this.txtModelId.EditValue = this._style.ModelId;
                }
                this.picThumbnail.Image = this._style.Thumbnail;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void hlSelectImg_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "选择缩略图",
                Filter = "所有图片格式(.JPG;PNG;BMP)|*.JPG;*.PNG;*.BMP"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.picThumbnail.Image = Image.FromFile(dialog.FileName);
            }
        }

        private DataTable GetModelInfo()
        {
            DataTable table = null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                table = new DataTable("ModelInfo");
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("ObjectId", typeof(string));
                table.Columns.Add("Thumbnail", typeof(object));
                
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ModelInfo");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "GroupId != '-1'",
                    SubFields = "Name,ObjectId,Thumbnail",
                    PostfixClause = "order by Name asc"
                };
                cursor = oc.Search(filter, true);
                while ((row = cursor.NextRow()) != null)
                {
                    DataRow dtRow = table.NewRow();
                    dtRow["Name"] = row.GetValue(0).ToString();
                    dtRow["ObjectId"] = row.GetValue(1).ToString();
                    if (!row.IsNull(2))
                    {
                        try
                        {
                            IBinaryBuffer buffer2 = row.GetValue(2) as IBinaryBuffer;
                            if (buffer2 != null)
                            {
                                MemoryStream stream = new MemoryStream(buffer2.AsByteArray());
                                dtRow["Thumbnail"] = Image.FromStream(stream);
                            }
                        }
                        catch (Exception exception)
                        {
                        }
                    }
                    table.Rows.Add(dtRow);
                }
                return table;
            }
            catch (Exception exception)
            {
                return null;
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

        private void txtModelId_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView view = null;
            Image image = null;
            if (((this.txtModelId.GetSelectedDataRow() != null) && 
                ((view = this.txtModelId.GetSelectedDataRow() as DataRowView) != null)) && 
                ((view["Thumbnail"] != null) && ((image = view["Thumbnail"] as Image) != null)))
            {
                this.picThumbnail.Image = image;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtStyleName.Text.Trim()))
            {
                XtraMessageBox.Show("请输入风格名称！", "提示");
                this.txtStyleName.Focus();
                return;
            }
            if (this._edit)
            {
                this._style.Name = this.txtStyleName.Text.Trim();
                this._style.IsBlendPipe = this.chkBlendPipe.Checked;
                this._style.IsFollowDia = this.chkFollowDia.Checked;
                this._style.IsFollowDir = this.chkFollowDir.Checked;
                this._style.IsFollowSurfH = this.chkFollowSurfH.Checked;
                this._style.IsRotateZ = this.chkRotateZ.Checked;
                this._style.IsStretchZ = this.chkStretchZ.Checked;
                this._style.ModelId = this.txtModelId.EditValue.ToString();
                this._style.Thumbnail = this.picThumbnail.Image;
                if(UpdateFacStyleClass(this._style))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("更新管点风格失败！", "提示");
                }
            }
            else
            {
                this._style.Name = this.txtStyleName.Text.Trim();
                this._style.ObjectId = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                this._style.IsBlendPipe = this.chkBlendPipe.Checked;
                this._style.IsFollowDia = this.chkFollowDia.Checked;
                this._style.IsFollowDir = this.chkFollowDir.Checked;
                this._style.IsFollowSurfH = this.chkFollowSurfH.Checked;
                this._style.IsRotateZ = this.chkRotateZ.Checked;
                this._style.IsStretchZ = this.chkStretchZ.Checked;
                this._style.ModelId = this.txtModelId.EditValue.ToString();
                this._style.Thumbnail = this.picThumbnail.Image;
                if (InsertFacStyleClass(this._style))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("创建管点风格失败！", "提示");
                }
            }
        }

        private bool InsertFacStyleClass(FacStyleClass style)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return false;

                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                row.SetValue(row.FieldIndex("Name"), style.Name);
                row.SetValue(row.FieldIndex("FacClassCode"), style.FacClassCode);
                row.SetValue(row.FieldIndex("ObjectId"), style.ObjectId);
                row.SetValue(row.FieldIndex("StyleType"), style.Type.ToString());
                row.SetValue(row.FieldIndex("StyleInfo"), style.ObjectToJson());
                if (style.Thumbnail != null)
                {
                    try
                    {
                        IBinaryBuffer bb = new BinaryBufferClass();
                        MemoryStream stream = new MemoryStream();
                        style.Thumbnail.Save(stream, ImageFormat.Png);
                        bb.FromByteArray(stream.ToArray());
                        row.SetValue(row.FieldIndex("Thumbnail"), bb);
                    }
                    catch (Exception exception)
                    {
                    }
                }
                cursor.InsertRow(row);
                style.Id = cursor.LastInsertId;
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

        private bool UpdateFacStyleClass(FacStyleClass style)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", style.ObjectId)
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("Name"), style.Name);
                    row.SetValue(row.FieldIndex("FacClassCode"), style.FacClassCode);
                    row.SetValue(row.FieldIndex("ObjectId"), style.ObjectId);
                    row.SetValue(row.FieldIndex("StyleType"), style.Type.ToString());
                    row.SetValue(row.FieldIndex("StyleInfo"), style.ObjectToJson());
                    if (style.Thumbnail != null)
                    {
                        try
                        {
                            IBinaryBuffer bb = new BinaryBufferClass();
                            MemoryStream stream = new MemoryStream();
                            style.Thumbnail.Save(stream, ImageFormat.Png);
                            bb.FromByteArray(stream.ToArray());
                            row.SetValue(row.FieldIndex("Thumbnail"), bb);
                        }
                        catch (Exception exception)
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
    }
}
