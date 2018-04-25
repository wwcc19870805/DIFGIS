using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using System.IO;
using System.Reflection;
using System.Drawing.Imaging;

namespace DF3DPipeCreateTool.Frm
{
    public class FrmPipeLineStyle : XtraForm
    {
        private SimpleButton btnSave;
        private TextEdit txtStyleName;
        private LabelControl labelControl1;
        private PanelControl panelControl1;
        private HyperLinkEdit hlSelectImg;
        private PictureEdit picThumbnail;
        private LabelControl labelControl2;
        private PanelControl panelControl2;
        private RadioGroup rgRenderType;
        private LabelControl labelControl3;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField3;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField2;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField4;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn4;
        private SimpleButton btnCancel;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit3;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn3;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn1;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn2;
        private DevExpress.XtraGrid.GridControl gridRenderInfo;
        private LookUpEdit txtSelRenderInfo;
        private ComboBoxEdit cbxHeightParam;
        private LabelControl labelControl5;
        private LabelControl labelControl4;
        private PanelControl panelControl3;
        private SpinEdit txtThick;
        private ComboBoxEdit cbxHeightMode;
    
        private void InitializeComponent()
        {
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtStyleName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.hlSelectImg = new DevExpress.XtraEditors.HyperLinkEdit();
            this.picThumbnail = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.rgRenderType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.layoutViewField3 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField2 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField4 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewColumn4 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.repositoryItemPictureEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewColumn3 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewColumn1 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.layoutViewColumn2 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.gridRenderInfo = new DevExpress.XtraGrid.GridControl();
            this.txtSelRenderInfo = new DevExpress.XtraEditors.LookUpEdit();
            this.cbxHeightParam = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.cbxHeightMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtThick = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hlSelectImg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgRenderType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRenderInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelRenderInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHeightParam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHeightMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThick.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(402, 272);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 22);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "创建";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtStyleName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(14, 9);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(315, 35);
            this.panelControl1.TabIndex = 26;
            // 
            // hlSelectImg
            // 
            this.hlSelectImg.EditValue = "选择";
            this.hlSelectImg.Location = new System.Drawing.Point(47, 4);
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
            this.picThumbnail.Location = new System.Drawing.Point(8, 30);
            this.picThumbnail.Name = "picThumbnail";
            this.picThumbnail.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picThumbnail.Size = new System.Drawing.Size(220, 220);
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
            this.panelControl2.Location = new System.Drawing.Point(335, 9);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(236, 257);
            this.panelControl2.TabIndex = 27;
            // 
            // rgRenderType
            // 
            this.rgRenderType.EditValue = 1;
            this.rgRenderType.Location = new System.Drawing.Point(75, 39);
            this.rgRenderType.Name = "rgRenderType";
            this.rgRenderType.Size = new System.Drawing.Size(106, 24);
            this.rgRenderType.TabIndex = 2;
            this.rgRenderType.SelectedIndexChanged += new System.EventHandler(this.rgRenderType_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 44);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "渲染方式：";
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "layoutViewTemplateCard";
            this.layoutViewCard1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.GroupBordersVisible = false;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField3,
            this.layoutViewField1,
            this.layoutViewField2});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 5;
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // layoutViewField3
            // 
            this.layoutViewField3.EditorPreferredWidth = 96;
            this.layoutViewField3.Location = new System.Drawing.Point(0, 18);
            this.layoutViewField3.MaxSize = new System.Drawing.Size(98, 108);
            this.layoutViewField3.MinSize = new System.Drawing.Size(98, 108);
            this.layoutViewField3.Name = "layoutViewField3";
            this.layoutViewField3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField3.Size = new System.Drawing.Size(98, 108);
            this.layoutViewField3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutViewField3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField3.TextToControlDistance = 0;
            this.layoutViewField3.TextVisible = false;
            // 
            // layoutViewField1
            // 
            this.layoutViewField1.EditorPreferredWidth = 96;
            this.layoutViewField1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField1.Name = "layoutViewField1";
            this.layoutViewField1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField1.Size = new System.Drawing.Size(98, 18);
            this.layoutViewField1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutViewField1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField1.TextToControlDistance = 0;
            this.layoutViewField1.TextVisible = false;
            // 
            // layoutViewField2
            // 
            this.layoutViewField2.EditorPreferredWidth = 96;
            this.layoutViewField2.Location = new System.Drawing.Point(0, 126);
            this.layoutViewField2.Name = "layoutViewField2";
            this.layoutViewField2.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField2.Size = new System.Drawing.Size(98, 18);
            this.layoutViewField2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField2.TextToControlDistance = 0;
            this.layoutViewField2.TextVisible = false;
            // 
            // layoutViewField4
            // 
            this.layoutViewField4.EditorPreferredWidth = 20;
            this.layoutViewField4.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField4.Name = "layoutViewField4";
            this.layoutViewField4.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField4.Size = new System.Drawing.Size(99, 126);
            this.layoutViewField4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField4.TextToControlDistance = 0;
            this.layoutViewField4.TextVisible = false;
            // 
            // layoutViewColumn4
            // 
            this.layoutViewColumn4.Caption = "对象信息";
            this.layoutViewColumn4.FieldName = "Info";
            this.layoutViewColumn4.LayoutViewField = this.layoutViewField4;
            this.layoutViewColumn4.Name = "layoutViewColumn4";
            this.layoutViewColumn4.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(499, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 22);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // repositoryItemPictureEdit3
            // 
            this.repositoryItemPictureEdit3.Name = "repositoryItemPictureEdit3";
            this.repositoryItemPictureEdit3.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // layoutViewColumn3
            // 
            this.layoutViewColumn3.Caption = "缩略图";
            this.layoutViewColumn3.ColumnEdit = this.repositoryItemPictureEdit3;
            this.layoutViewColumn3.FieldName = "Thumbnail";
            this.layoutViewColumn3.LayoutViewField = this.layoutViewField3;
            this.layoutViewColumn3.Name = "layoutViewColumn3";
            // 
            // layoutViewColumn1
            // 
            this.layoutViewColumn1.Caption = "名称";
            this.layoutViewColumn1.FieldName = "Name";
            this.layoutViewColumn1.LayoutViewField = this.layoutViewField1;
            this.layoutViewColumn1.Name = "layoutViewColumn1";
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.CardCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutView1.Appearance.CardCaption.Options.UseFont = true;
            this.layoutView1.CardMinSize = new System.Drawing.Size(100, 124);
            this.layoutView1.CardVertInterval = 1;
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.layoutViewColumn1,
            this.layoutViewColumn3,
            this.layoutViewColumn2,
            this.layoutViewColumn4});
            this.layoutView1.GridControl = this.gridRenderInfo;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField4});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.Editable = false;
            this.layoutView1.OptionsCustomization.AllowFilter = false;
            this.layoutView1.OptionsCustomization.AllowSort = false;
            this.layoutView1.OptionsCustomization.ShowGroupLayout = false;
            this.layoutView1.OptionsMultiRecordMode.StretchCardToViewHeight = true;
            this.layoutView1.OptionsMultiRecordMode.StretchCardToViewWidth = true;
            this.layoutView1.OptionsView.ShowCardBorderIfCaptionHidden = false;
            this.layoutView1.OptionsView.ShowCardCaption = false;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowCardLines = false;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Row;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.Click += new System.EventHandler(this.layoutView1_Click);
            // 
            // layoutViewColumn2
            // 
            this.layoutViewColumn2.Caption = "备注";
            this.layoutViewColumn2.FieldName = "Comment";
            this.layoutViewColumn2.LayoutViewField = this.layoutViewField2;
            this.layoutViewColumn2.Name = "layoutViewColumn2";
            // 
            // gridRenderInfo
            // 
            this.gridRenderInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridRenderInfo.Location = new System.Drawing.Point(8, 75);
            this.gridRenderInfo.MainView = this.layoutView1;
            this.gridRenderInfo.Name = "gridRenderInfo";
            this.gridRenderInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit3});
            this.gridRenderInfo.ShowOnlyPredefinedDetails = true;
            this.gridRenderInfo.Size = new System.Drawing.Size(300, 134);
            this.gridRenderInfo.TabIndex = 7;
            this.gridRenderInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // txtSelRenderInfo
            // 
            this.txtSelRenderInfo.EditValue = "";
            this.txtSelRenderInfo.Location = new System.Drawing.Point(75, 147);
            this.txtSelRenderInfo.Name = "txtSelRenderInfo";
            this.txtSelRenderInfo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSelRenderInfo.Properties.NullText = "请选择颜色";
            this.txtSelRenderInfo.Properties.ShowHeader = false;
            this.txtSelRenderInfo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtSelRenderInfo.Size = new System.Drawing.Size(80, 22);
            this.txtSelRenderInfo.TabIndex = 26;
            this.txtSelRenderInfo.Visible = false;
            this.txtSelRenderInfo.EditValueChanged += new System.EventHandler(this.txtSelRenderInfo_EditValueChanged);
            this.txtSelRenderInfo.Leave += new System.EventHandler(this.txtSelRenderInfo_Leave);
            // 
            // cbxHeightParam
            // 
            this.cbxHeightParam.EditValue = "";
            this.cbxHeightParam.Location = new System.Drawing.Point(75, 11);
            this.cbxHeightParam.Name = "cbxHeightParam";
            this.cbxHeightParam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxHeightParam.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxHeightParam.Size = new System.Drawing.Size(138, 22);
            this.cbxHeightParam.TabIndex = 12;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(16, 17);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "高程选项：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(187, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "管壁厚度:";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.txtSelRenderInfo);
            this.panelControl3.Controls.Add(this.cbxHeightParam);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.gridRenderInfo);
            this.panelControl3.Controls.Add(this.rgRenderType);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.cbxHeightMode);
            this.panelControl3.Controls.Add(this.txtThick);
            this.panelControl3.Location = new System.Drawing.Point(14, 50);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(315, 216);
            this.panelControl3.TabIndex = 30;
            // 
            // cbxHeightMode
            // 
            this.cbxHeightMode.EditValue = "";
            this.cbxHeightMode.Location = new System.Drawing.Point(219, 11);
            this.cbxHeightMode.Name = "cbxHeightMode";
            this.cbxHeightMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxHeightMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxHeightMode.Size = new System.Drawing.Size(80, 22);
            this.cbxHeightMode.TabIndex = 10;
            // 
            // txtThick
            // 
            this.txtThick.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtThick.Location = new System.Drawing.Point(245, 42);
            this.txtThick.Name = "txtThick";
            this.txtThick.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtThick.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtThick.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtThick.Size = new System.Drawing.Size(54, 22);
            this.txtThick.TabIndex = 5;
            // 
            // FrmPipeLineStyle
            // 
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(583, 304);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPipeLineStyle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建—管线风格";
            this.Load += new System.EventHandler(this.FrmPipeLineStyle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hlSelectImg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgRenderType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRenderInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelRenderInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHeightParam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxHeightMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThick.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dt;
        private IDataSource _ds;
        private bool _edit;
        private PipeLineStyleClass _style;
        public PipeLineStyleClass Style
        {
            get { return this._style; }
        }
        public FrmPipeLineStyle()
        {
            InitializeComponent();
            this._style = new PipeLineStyleClass();
            this._edit = false;
            this.Text = "创建-管线风格";
            this.btnSave.Text = "创建";
        }

        public FrmPipeLineStyle(PipeLineStyleClass plsc)
        {
            InitializeComponent();
            this._style = plsc;
            this._edit = true;
            this.Text = "编辑-管线风格";
            this.btnSave.Text = "保存";
        }

        public static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        public static Dictionary<string, string> GetEnumItemDesc(Type enumType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            System.Reflection.FieldInfo[] fieldinfos = enumType.GetFields();
            foreach (System.Reflection.FieldInfo field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    dic.Add(field.Name, ((DescriptionAttribute)objs[0]).Description);
                }
            }
            return dic;
        }

        private void FrmPipeLineStyle_Load(object sender, EventArgs e)
        {
            this._ds = DF3DPipeCreateApp.App.TemplateLib;
            if (this._ds == null) { this.Enabled = false; return; }

            Dictionary<string, string> dict = GetEnumItemDesc(typeof(HeightMode));
            foreach (KeyValuePair<string,string> kv in dict)
            {
                this.cbxHeightMode.Properties.Items.Add(kv.Value);
            }
            if (this.cbxHeightMode.Properties.Items.Count >= 0) this.cbxHeightMode.SelectedIndex = 0;
            if (this._edit) this.cbxHeightMode.EditValue = dict[this._style.HeightMode.ToString()];

            dict = GetEnumItemDesc(typeof(HeightParam));
            foreach (KeyValuePair<string, string> kv in dict)
            {
                this.cbxHeightParam.Properties.Items.Add(kv.Value);
            }
            if (this.cbxHeightParam.Properties.Items.Count >= 0) this.cbxHeightParam.SelectedIndex = 0;
            if (this._edit) this.cbxHeightParam.EditValue = dict[this._style.HeightParam.ToString()];

            dict = GetEnumItemDesc(typeof(RenderType));
            this.rgRenderType.Properties.Columns = dict.Count;
            this.rgRenderType.Properties.Items.AddEnum(typeof(RenderType));
            if (this.rgRenderType.Properties.Items.Count >= 0) this.rgRenderType.SelectedIndex = 0;
            rgRenderType_SelectedIndexChanged(null, null);
            if (this._edit)
            {
                this.rgRenderType.EditValue = this._style.RenderType;
                this.txtStyleName.Text = this._style.Name;
                this.txtThick.Text = this._style.PipeThick.ToString();
                this._dt = this._style.GetRenderInfo();
                this.gridRenderInfo.DataSource = this._dt;
                this.picThumbnail.Image = this._style.Thumbnail;
            }
            else
            {
                this._dt = new DataTable();
                this._dt.Columns.Add("Name", typeof(string));
                this._dt.Columns.Add("Thumbnail", typeof(object));
                this._dt.Columns.Add("Comment", typeof(string));
                this._dt.Columns.Add("Info", typeof(object));
                this.gridRenderInfo.DataSource = this._dt;

                DataRow row = null;
                row = this._dt.NewRow();
                row["Name"] = "外壁";
                row["Thumbnail"] = null;
                row["Comment"] = "";
                row["Info"] = null;
                this._dt.Rows.Add(row);
                row = this._dt.NewRow();
                row["Name"] = "内壁";
                row["Thumbnail"] = null;
                row["Comment"] = "";
                row["Info"] = null;
                this._dt.Rows.Add(row);

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

        private DataTable GetColorInfo()
        {
            DataTable table = null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                table = new DataTable("ColorInfo");
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("ObjectId", typeof(string));
                table.Columns.Add("Thumbnail", typeof(object));

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_ColorInfo");
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

        private DataTable GetTextureInfo()
        {
            DataTable table = null;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                table = new DataTable("TextureInfo");
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("ObjectId", typeof(string));
                table.Columns.Add("Thumbnail", typeof(object));

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TextureInfo");
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

        private void rgRenderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable colorInfo = null;
            try
            {
                object obj = this.rgRenderType.EditValue;
                if (obj is RenderType)
                {
                    RenderType rt = (RenderType)obj;
                    if (rt == RenderType.Color)
                    {
                        this.txtSelRenderInfo.Properties.NullText = "请选择颜色";
                        colorInfo = GetColorInfo();
                    }
                    else if (rt == RenderType.Texture)
                    {
                        this.txtSelRenderInfo.Properties.NullText = "请选择材质";
                        colorInfo = GetTextureInfo();
                    }
                    if (colorInfo == null)
                    {
                        this.txtSelRenderInfo.Properties.DataSource = null;
                    }
                    this.txtSelRenderInfo.Properties.DataSource = colorInfo;
                    this.txtSelRenderInfo.Properties.DisplayMember = "Name";
                    this.txtSelRenderInfo.Properties.ValueMember = "ObjectId";
                    LookUpColumnInfo column = new LookUpColumnInfo("Name");
                    this.txtSelRenderInfo.Properties.Columns.Clear();
                    this.txtSelRenderInfo.Properties.Columns.Add(column);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void txtSelRenderInfo_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView view = null;
            Image image = null;
            try
            {
                int focusedRowHandle = this.layoutView1.FocusedRowHandle;
                if (((focusedRowHandle != -2147483648) && (this.txtSelRenderInfo.EditValue != null)) 
                    && (((this.txtSelRenderInfo.GetSelectedDataRow() != null) 
                    && ((view = this.txtSelRenderInfo.GetSelectedDataRow() as DataRowView) != null)) 
                    && ((view["Thumbnail"] != null) && ((image = view["Thumbnail"] as Image) != null))))
                {
                    DataRow row = this._dt.Rows[focusedRowHandle];
                    row["Thumbnail"] = image;
                    row["Info"] = view["ObjectId"];
                    this.picThumbnail.Image = image;
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void txtSelRenderInfo_Leave(object sender, EventArgs e)
        {
            this.txtSelRenderInfo.Visible = false;
        }

        private void layoutView1_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = null;
            args = e as MouseEventArgs;
            if ((args != null) && (this.layoutView1.CalcHitInfo(args.X, args.Y).RowHandle != -2147483648))
            {
                Point point = new Point(this.gridRenderInfo.Location.X + args.X, this.gridRenderInfo.Location.Y + args.Y);
                this.txtSelRenderInfo.EditValue = null;
                this.txtSelRenderInfo.Location = point;
                this.txtSelRenderInfo.Visible = true;
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
                this._style.RenderType = (RenderType)this.rgRenderType.SelectedIndex;
                this._style.PipeThick = Convert.ToDouble(this.txtThick.Value);
                this._style.HeightMode = (HeightMode)this.cbxHeightMode.SelectedIndex;
                this._style.HeightParam = (HeightParam)this.cbxHeightParam.SelectedIndex;
                this._style.TextureOutside = this._dt.Rows[0]["ObjectId"].ToString();
                this._style.TextureInside = this._dt.Rows[1]["ObjectId"].ToString();
                this._style.Thumbnail = this.picThumbnail.Image;
                if (UpdateFacStyleClass(this._style))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("更新管线风格失败！", "提示");
                }
            }
            else
            {
                this._style.Name = this.txtStyleName.Text.Trim();
                this._style.ObjectId = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                this._style.RenderType = (RenderType)this.rgRenderType.SelectedIndex;
                this._style.PipeThick = Convert.ToDouble(this.txtThick.Value);
                this._style.HeightMode = (HeightMode)this.cbxHeightMode.SelectedIndex;
                this._style.HeightParam = (HeightParam)this.cbxHeightParam.SelectedIndex;
                this._style.TextureOutside = this._dt.Rows[0]["ObjectId"].ToString();
                this._style.TextureInside = this._dt.Rows[1]["ObjectId"].ToString();
                this._style.Thumbnail = this.picThumbnail.Image;
                if (InsertFacStyleClass(this._style))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("创建管线风格失败！", "提示");
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
