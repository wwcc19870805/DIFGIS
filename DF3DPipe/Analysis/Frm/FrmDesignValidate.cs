using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using DF3DPipeCreateTool.Class;
using DF3DPipeCreateTool.Service;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.FdeDataInterop;
using System.IO;
using Gvitech.CityMaker.Common;
using DFWinForms.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Resource;
using DF3DPipeCreateTool.UC;
using Gvitech.CityMaker.RenderControl;
using DFCommon.Class;
using DF3DDraw;
using DevExpress.XtraEditors.Controls;

namespace DF3DPipe.Analysis.Frm
{
    public class FrmDesignValidate : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private SimpleButton btnAddList2;
        private SimpleButton btnAddList1;
        private SimpleButton btnDelVertex;
        private SimpleButton btnAddVertex;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private TextEdit txtDia2;
        private TextEdit txtDia1;
        private ComboBoxEdit cmbType;
        private TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private LabelControl labelControl1;
        private SimpleButton btnCancel;
        private SimpleButton btnCreate;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private ImageComboBoxEdit cmbStyle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private IContainer components;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDesignValidate));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbStyle = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddList2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddList1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelVertex = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddVertex = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDia2 = new DevExpress.XtraEditors.TextEdit();
            this.txtDia1 = new DevExpress.XtraEditors.TextEdit();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbStyle);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnCreate);
            this.layoutControl1.Controls.Add(this.gridControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnAddList2);
            this.layoutControl1.Controls.Add(this.btnAddList1);
            this.layoutControl1.Controls.Add(this.btnDelVertex);
            this.layoutControl1.Controls.Add(this.btnAddVertex);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.txtDia2);
            this.layoutControl1.Controls.Add(this.txtDia1);
            this.layoutControl1.Controls.Add(this.cmbType);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(589, 439);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbStyle
            // 
            this.cmbStyle.Location = new System.Drawing.Point(41, 28);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStyle.Properties.DropDownRows = 10;
            this.cmbStyle.Properties.LargeImages = this.imageCollection1;
            this.cmbStyle.Size = new System.Drawing.Size(167, 50);
            this.cmbStyle.StyleController = this.layoutControl1;
            this.cmbStyle.TabIndex = 14;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(487, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "退出";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(390, 412);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(93, 22);
            this.btnCreate.StyleController = this.layoutControl1;
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "生成";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(215, 25);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.repositoryItemButtonEdit1});
            this.gridControl2.Size = new System.Drawing.Size(369, 383);
            this.gridControl2.TabIndex = 11;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn10,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn4});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "名称";
            this.gridColumn5.FieldName = "name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 81;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "类型";
            this.gridColumn7.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn7.FieldName = "type";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 81;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox1_SelectedIndexChanged);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "管径";
            this.gridColumn6.FieldName = "dia";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "样式";
            this.gridColumn10.ColumnEdit = this.repositoryItemComboBox2;
            this.gridColumn10.FieldName = "style";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox2.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox2_SelectedIndexChanged);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "顶点坐标";
            this.gridColumn8.FieldName = "vertexs";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 225;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "操作";
            this.gridColumn9.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            this.gridColumn9.Width = 37;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "删除", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "线";
            this.gridColumn4.FieldName = "line";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 166);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(143, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "管径2空为圆管,不空为方沟";
            // 
            // btnAddList2
            // 
            this.btnAddList2.Location = new System.Drawing.Point(14, 403);
            this.btnAddList2.Name = "btnAddList2";
            this.btnAddList2.Size = new System.Drawing.Size(182, 22);
            this.btnAddList2.StyleController = this.layoutControl1;
            this.btnAddList2.TabIndex = 10;
            this.btnAddList2.Text = "导入文件到列表";
            this.btnAddList2.Click += new System.EventHandler(this.btnAddList2_Click);
            // 
            // btnAddList1
            // 
            this.btnAddList1.Location = new System.Drawing.Point(14, 333);
            this.btnAddList1.Name = "btnAddList1";
            this.btnAddList1.Size = new System.Drawing.Size(182, 22);
            this.btnAddList1.StyleController = this.layoutControl1;
            this.btnAddList1.TabIndex = 9;
            this.btnAddList1.Text = "加入到列表";
            this.btnAddList1.Click += new System.EventHandler(this.btnAddList1_Click);
            // 
            // btnDelVertex
            // 
            this.btnDelVertex.Image = ((System.Drawing.Image)(resources.GetObject("btnDelVertex.Image")));
            this.btnDelVertex.Location = new System.Drawing.Point(44, 184);
            this.btnDelVertex.Name = "btnDelVertex";
            this.btnDelVertex.Size = new System.Drawing.Size(26, 22);
            this.btnDelVertex.StyleController = this.layoutControl1;
            this.btnDelVertex.TabIndex = 7;
            this.btnDelVertex.ToolTip = "移除";
            this.btnDelVertex.Click += new System.EventHandler(this.btnDelVertex_Click);
            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVertex.Image")));
            this.btnAddVertex.Location = new System.Drawing.Point(14, 184);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(26, 22);
            this.btnAddVertex.StyleController = this.layoutControl1;
            this.btnAddVertex.TabIndex = 6;
            this.btnAddVertex.ToolTip = "添加";
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(14, 210);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(182, 119);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "X坐标";
            this.gridColumn1.FieldName = "X";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Y坐标";
            this.gridColumn2.FieldName = "Y";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "高程";
            this.gridColumn3.FieldName = "Z";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // txtDia2
            // 
            this.txtDia2.Location = new System.Drawing.Point(146, 140);
            this.txtDia2.Name = "txtDia2";
            this.txtDia2.Size = new System.Drawing.Size(50, 22);
            this.txtDia2.StyleController = this.layoutControl1;
            this.txtDia2.TabIndex = 4;
            // 
            // txtDia1
            // 
            this.txtDia1.EditValue = "1000";
            this.txtDia1.Location = new System.Drawing.Point(53, 140);
            this.txtDia1.Name = "txtDia1";
            this.txtDia1.Size = new System.Drawing.Size(50, 22);
            this.txtDia1.StyleController = this.layoutControl1;
            this.txtDia1.TabIndex = 3;
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(41, 2);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.DropDownRows = 10;
            this.cmbType.Properties.NullValuePrompt = "请选择一个类型";
            this.cmbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbType.Size = new System.Drawing.Size(167, 22);
            this.cmbType.StyleController = this.layoutControl1;
            this.cmbType.TabIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.EditValue = "SheJiNo1";
            this.txtName.Location = new System.Drawing.Point(53, 114);
            this.txtName.Name = "txtName";
            this.txtName.Properties.NullValuePrompt = "请输入名称";
            this.txtName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtName.Size = new System.Drawing.Size(143, 22);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(589, 439);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "方式二：文件导入";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 369);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(210, 70);
            this.layoutControlGroup2.Text = "方式二：文件导入";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnAddList2;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "方式一：坐标输入";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem9,
            this.layoutControlItem5,
            this.layoutControlItem11,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 80);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(210, 289);
            this.layoutControlGroup3.Text = "方式一：坐标输入";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtName;
            this.layoutControlItem1.CustomizationFormText = "名称：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem1.Text = "名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtDia1;
            this.layoutControlItem4.CustomizationFormText = "管径1:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.Text = "管径1:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridControl1;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(186, 123);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnAddList1;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 219);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtDia2;
            this.layoutControlItem5.CustomizationFormText = "管径2:";
            this.layoutControlItem5.Location = new System.Drawing.Point(93, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem5.Text = "管径2:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.labelControl1;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(186, 18);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnAddVertex;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 70);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnDelVertex;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(30, 70);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(156, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "信息列表";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.emptySpaceItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(210, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(379, 439);
            this.layoutControlGroup4.Text = "信息列表";
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.gridControl2;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(373, 387);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnCreate;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(175, 387);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnCancel;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(272, 387);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(101, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 387);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(175, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbType;
            this.layoutControlItem2.CustomizationFormText = "类型：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(210, 26);
            this.layoutControlItem2.Text = "类型：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbStyle;
            this.layoutControlItem3.CustomizationFormText = "类型：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(210, 54);
            this.layoutControlItem3.Text = "样式：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // FrmDesignValidate
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(589, 439);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDesignValidate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设计验证";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDesignValidate_FormClosed);
            this.Load += new System.EventHandler(this.FrmDesignValidata_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }
        private AxRenderControl _3DControl;
        private IGeometryFactory _geoFactory;
        public FrmDesignValidate()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null)
            {
                this.Enabled = false; return;
            }
            this._3DControl = app.Current3DMapControl;
            this._geoFactory = new GeometryFactory();
            this.callback = new GvitchCopyCallBack();
            this.callback.Replicating += new CopyReplicatingHandler(this.OnCopyProgress);
        }
        private DataTable _dt1;
        private DataTable _dt2;
        private List<Guid> _listRGuid;
        private void FrmDesignValidata_Load(object sender, EventArgs e)
        {
            try
            {
                _listRGuid = new List<Guid>();
                _dt1 = new DataTable();
                _dt1.Columns.AddRange(new DataColumn[]{
                    new DataColumn("X",Type.GetType("System.Double")),
                    new DataColumn("Y",Type.GetType("System.Double")),
                    new DataColumn("Z",Type.GetType("System.Double"))
                });
                this.gridControl1.DataSource = _dt1;

                _dt2 = new DataTable();
                _dt2.Columns.AddRange(new DataColumn[]{
                    new DataColumn("name",Type.GetType("System.String")),
                    new DataColumn("type",Type.GetType("System.Object")),
                    new DataColumn("dia",Type.GetType("System.String")),
                    new DataColumn("style",Type.GetType("System.Object")),
                    new DataColumn("line",Type.GetType("System.Object")),
                    new DataColumn("vertexs",Type.GetType("System.String"))
                });
                this.gridControl2.DataSource = _dt2;
                List<FacClassReg> list = FacilityInfoService.GetFacClassRegsByFacilityType("PipeLine");
                if (list != null)
                {
                    foreach (FacClassReg reg in list)
                    {
                        if (!string.IsNullOrEmpty(reg.Name))
                        {
                            this.cmbType.Properties.Items.Add(reg);
                            this.repositoryItemComboBox1.Items.Add(reg);
                        }
                    }
                    this.cmbType.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmbStyle.Properties.Items.Clear();
                if (this.cmbType.SelectedItem == null) return;
                FacClassReg reg = this.cmbType.SelectedItem as FacClassReg;
                List<FacStyleClass> list = FacilityInfoService.GetFacStyleByFacClassCode(reg.FacClassCode);
                if (list != null)
                {
                    foreach (FacStyleClass fsc in list)
                    {
                        int imageIndex = this.imageCollection1.Images.Add(fsc.Thumbnail);
                        ImageComboBoxItem item = new ImageComboBoxItem();
                        item.Value = fsc;
                        item.Description = fsc.ToString();
                        item.ImageIndex = imageIndex;
                        this.cmbStyle.Properties.Items.Add(item);
                    }
                    for (int i = 0; i < this.cmbStyle.Properties.Items.Count; i++)
                    {
                        this.cmbStyle.Properties.Items[i].ImageIndex = i;
                    }
                    if (this.cmbStyle.Properties.Items.Count > 0) this.cmbStyle.SelectedIndex = 0;
                    else this.cmbStyle.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            this.gridView1.AddNewRow();
            this.gridView2.RefreshData();
        }

        private void btnDelVertex_Click(object sender, EventArgs e)
        {
            this.gridView1.DeleteSelectedRows();
            this.gridView1.RefreshData();
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                XtraMessageBox.Show("请输入管线名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtName.Focus();
                return false;
            }
            if (this.cmbType.SelectedItem == null)
            {
                XtraMessageBox.Show("请选择管线类型！", "提示");
                this.cmbType.Focus();
                return false;
            }
            if (this.cmbStyle.SelectedItem == null)
            {
                XtraMessageBox.Show("请选择管线风格！", "提示");
                this.cmbStyle.Focus();
                return false;
            }
            int d1;
            bool b1 = int.TryParse(this.txtDia1.Text.Trim(), out d1);
            if (!b1)
            {
                XtraMessageBox.Show("管径1须为整数，请查看", "提示");
                return false;
            }
            if (this.txtDia2.Text.Trim() != "")
            {
                int d2;
                bool b2 = int.TryParse(this.txtDia2.Text.Trim(), out d2);
                if (!b2)
                {
                    XtraMessageBox.Show("管径2须为空或整数，请查看", "提示");
                    return false;
                }
            }
            if (this.gridView1.RowCount < 2)
            {
                XtraMessageBox.Show("请输入管线顶点，管线至少有两个顶点", "提示");
                return false;
            }

            this.gridView1.CloseEditor();
            return true;
        }

        private void btnAddList1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateData()) return;
            string vertexs = "";
            IPolyline line = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            for (int i = 0; i < this.gridView1.RowCount; i++)
            {
                try
                {
                    IPoint point = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    point.X = Convert.ToDouble(this.gridView1.GetDataRow(i)[0].ToString());
                    point.Y = Convert.ToDouble(this.gridView1.GetDataRow(i)[1].ToString());
                    point.Z = Convert.ToDouble(this.gridView1.GetDataRow(i)[2].ToString());
                    line.AppendPoint(point);
                    string xyz = point.X.ToString() + "," + point.Y.ToString() + "," + point.Z + ";";
                    vertexs = vertexs + xyz;
                }
                catch
                {
                    XtraMessageBox.Show("请输入正确的管线顶点坐标！", "提示");
                    return;
                }
            }
            vertexs = vertexs.Remove(vertexs.Length - 1);
            DataRow dr = this._dt2.NewRow();
            dr["name"] = this.txtName.Text.Trim();
            string standard = "";
            if (!string.IsNullOrEmpty(this.txtDia1.Text.Trim()))
            {
                if (string.IsNullOrEmpty(this.txtDia2.Text.Trim()))
                {
                    standard = this.txtDia1.Text.Trim();
                }
                else
                {
                    standard = this.txtDia1.Text.Trim() + "*" + this.txtDia2.Text.Trim();
                }
            }
            dr["dia"] = standard;
            dr["type"] = this.cmbType.SelectedItem;
            dr["style"] = this.cmbStyle.EditValue;
            dr["vertexs"] = vertexs;
            dr["line"] = line;
            this._dt2.Rows.Add(dr);
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.Controls.EditorButton btn = e.Button;
            switch (btn.Caption)
            {
                case "删除":
                    int focusedRowHandle = this.gridView2.FocusedRowHandle;
                    if (focusedRowHandle == -1) return;
                    DataRow dr = this.gridView2.GetDataRow(focusedRowHandle);
                    this._dt2.Rows.Remove(dr);
                    this.gridView2.RefreshRow(focusedRowHandle);
                    break;
            }
        }

        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.repositoryItemComboBox2.Items.Clear();
                ComboBoxEdit edit = sender as ComboBoxEdit;
                DataRow dr = this.gridView2.GetDataRow(this.gridView2.FocusedRowHandle);
                dr["type"] = edit.SelectedItem;
                dr["style"] = null;
                this.gridView2.RefreshData();

                if (edit.SelectedItem == null) return;
                FacClassReg reg = edit.SelectedItem as FacClassReg;
                List<FacStyleClass> list = FacilityInfoService.GetFacStyleByFacClassCode(reg.FacClassCode);
                if (list != null)
                {
                    foreach (FacStyleClass style in list)
                    {
                        if (!string.IsNullOrEmpty(style.Name))
                        {
                            this.repositoryItemComboBox2.Items.Add(style);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit edit = sender as ComboBoxEdit;
            DataRow dr = this.gridView2.GetDataRow(this.gridView2.FocusedRowHandle);
            dr["style"] = edit.SelectedItem;
            this.gridView2.RefreshData();
        }
        private GvitchCopyCallBack callback;
        private string currentFileName;
        private bool OnCopyProgress(GvitechFeatureProgress Progress)
        {
            int curCount = Progress.CurrentFeatureCount;
            int totalFeatureCount = Progress.TotalFeatureCount;
            WaitForm.SetCaption("正在导入【" + currentFileName + "】数据(" + curCount + "/" + totalFeatureCount + ")...");
            return true;
        }

        private void btnAddList2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbType.SelectedItem == null)
                {
                    XtraMessageBox.Show("请选择管线类型！", "提示");
                    this.cmbType.Focus();
                    return;
                }
                if (this.cmbStyle.SelectedItem == null)
                {
                    XtraMessageBox.Show("请选择管线风格！", "提示");
                    this.cmbStyle.Focus();
                    return;
                }
                if (DF3DPipeCreateApp.App.TempLib == null) return;

                FacClassReg reg = this.cmbType.SelectedItem as FacClassReg;
                FacStyleClass style = this.cmbStyle.EditValue as FacStyleClass;
                if (reg == null || style == null) return;
                FacilityClass fac = reg.FacilityType;
                if (fac == null) return;
                string diaFieldName = fac.GetFieldInfoNameBySystemName("Diameter");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Shape File(*.shp)|*.shp";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    IPropertySet ps = new PropertySetClass();
                    ps.SetProperty("FILENAME", fileName);
                    IDataInteropFactory dif = new DataInteropFactory();
                    IDataInterop di = dif.CreateDataInterop(gviDataConnectionType.gviOgrConnectionShp, ps);
                    di.StepValue = 10;
                    di.OnProcessing = this.callback;
                    if (di == null)
                    {
                        XtraMessageBox.Show("获取Shp文件信息失败", "提示");
                        return;
                    }
                    if (di.LayersInfo.Count == 0)
                    {
                        XtraMessageBox.Show("数据为空", "提示");
                        return;
                    }
                    IFieldInfoCollection fields = di.LayersInfo[0].FieldInfos;
                    string name = "shp_" + DateTime.Now.Ticks;
                    IFeatureDataSet fds = DF3DPipeCreateApp.App.TempLib.OpenFeatureDataset("FeatureDataSet");
                    if (fds == null)
                    {
                        fds = DF3DPipeCreateApp.App.TempLib.CreateFeatureDataset("FeatureDataSet", null);
                    }
                    if (fds == null) return;
                    IFeatureClass oC = fds.CreateFeatureClass(name, fields);
                    if (oC == null) return;
                    WaitForm.Start("开始导入数据...", "请稍后", new Size(300, 50));
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    currentFileName = fileNameWithoutExtension;
                    if (oC != null)
                    {
                        oC.AliasName = fileNameWithoutExtension;
                        di.ImportLayer(oC, "Geometry", null);
                    }

                    int indexDiameter = oC.GetFields().IndexOf(diaFieldName);
                    int indexGeo = oC.GetFields().IndexOf("Geometry");
                    WaitForm.SetCaption("开始读取数据...");
                    IRowBuffer row = null;
                    IFdeCursor cursor = null;
                    try
                    {
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "1=1";
                        int count = oC.GetCount(filter);
                        int num = 0;
                        cursor = oC.Search(filter, true);
                        while ((row = cursor.NextRow()) != null)
                        {
                            num++;
                            WaitForm.SetCaption("开始读取【" + num + "/" + count + "】数据...");
                            if (indexGeo != -1 && !row.IsNull(indexGeo))
                            {
                                IGeometry geo = row.GetValue(indexGeo) as IGeometry;
                                if (geo != null && geo.GeometryType == gviGeometryType.gviGeometryPolyline)
                                {
                                    IPolyline line = geo as IPolyline;
                                    DataRow dr = this._dt2.NewRow();
                                    dr["name"] = DateTime.Now.Ticks;
                                    string vertexs = "";
                                    for (int k = 0; k < line.PointCount; k++)
                                    {
                                        IPoint point = line.GetPoint(k);
                                        string xyz = point.X.ToString() + "," + point.Y.ToString() + "," + point.Z + ";";
                                        vertexs = vertexs + xyz;
                                    }
                                    dr["type"] = reg;
                                    dr["style"] = style;
                                    dr["vertexs"] = vertexs;
                                    dr["line"] = line;
                                    if (indexDiameter != -1 && !row.IsNull(indexDiameter))
                                    {
                                        dr["dia"] = row.GetValue(indexDiameter);
                                    }
                                    this._dt2.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        if (row == null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                            row = null;
                        }
                        if (cursor == null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                            cursor = null;
                        }
                    }
                    WaitForm.Stop();

                }
            }
            catch (Exception ex) { WaitForm.Stop(); }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (this._dt2 == null || this._dt2.Rows.Count == 0) return;
            try
            {
                WaitForm.Start("开始生成三维管线模型...", "请稍后", new Size(300, 50));
                int count = this._dt2.Rows.Count;
                int num = 0;
                int snum = 0;
                foreach (DataRow dr in this._dt2.Rows)
                {
                    num++;
                    WaitForm.SetCaption("开始生成第" + num + "个，共" + count + "个");

                    object objname = dr["name"];
                    if (objname == null || !(objname is string)) continue;
                    object objtype = dr["type"];
                    if (objtype == null || !(objtype is FacClassReg)) continue;
                    object objdia = dr["dia"];
                    if (objdia == null || !(objdia is string)) continue;
                    object objstyle = dr["style"];
                    if (objstyle == null || !(objstyle is FacStyleClass)) continue;
                    object objline = dr["line"];
                    if (objline == null || !(objline is IPolyline)) continue;
                    FacClassReg reg = objtype as FacClassReg;
                    FacilityClass fac = reg.FacilityType;
                    IFeatureClass objclass = reg.GetFeatureClass();
                    if (objclass == null || fac == null) continue;

                    string classifyFieldName = fac.GetFieldInfoNameBySystemName("Classify");
                    string diameterFieldName = fac.GetFieldInfoNameBySystemName("Diameter");
                    string diameter1FieldName = fac.GetFieldInfoNameBySystemName("Diameter1");
                    string diameter2FieldName = fac.GetFieldInfoNameBySystemName("Diameter2");
                    
                    FacStyleClass style = objstyle as FacStyleClass;
                    string name = objname.ToString();
                    string dia = objdia.ToString();
                    IPolyline line = objline as IPolyline;
                    IRowBuffer rowBuffer = null;
                    IFdeCursor cursor = null;
                    try
                    {
                        IFieldInfoCollection fieldconfig = objclass.GetFields();
                        cursor = objclass.Insert();
                        rowBuffer = objclass.CreateRowBuffer();
                        int pos = fieldconfig.IndexOf(classifyFieldName);
                        if (pos > -1)
                        {
                            rowBuffer.SetValue(pos, this.txtName.Text.Trim());
                        }
                        pos = fieldconfig.IndexOf(diameterFieldName);
                        if (pos > -1)
                        {
                            rowBuffer.SetValue(pos, dia);
                        }
                        string styleid = "";
                        if (style != null) styleid = style.ObjectId;
                        rowBuffer.SetValue(rowBuffer.FieldIndex("FacilityId"), BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant());
                        rowBuffer.SetValue(rowBuffer.FieldIndex("StyleId"), styleid);
                        pos = rowBuffer.FieldIndex("Shape");
                        if (pos > -1)
                        {
                            rowBuffer.SetValue(pos, line);
                        }
                        pos = rowBuffer.FieldIndex("FootPrint");
                        if (pos > -1)
                        {
                            rowBuffer.SetValue(pos, line.Clone2(gviVertexAttribute.gviVertexAttributeNone));
                        }
                        cursor.InsertRow(rowBuffer);
                        int oid = cursor.LastInsertId;
                        if (cursor != null)
                        {
                            Marshal.ReleaseComObject(cursor);
                            cursor = null;
                        }
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "oid=" + oid;
                        cursor = objclass.Update(filter);
                        if ((rowBuffer = cursor.NextRow()) != null)
                        {
                            TopoClass tc = FacilityInfoService.GetTopoClassByFacClassCode(reg.FacClassCode);
                            PipeLineFac facility = new PipeLineFac(reg, style, rowBuffer, tc, false, false);
                            IModelPoint mp = null;
                            IModel fineModel = null;
                            IModel simpleModel = null;
                            string modelName = "";
                            bool bSuccess = false;
                            if (UCAuto3DCreate.RebuildModel(facility, style, out mp, out fineModel, out simpleModel, out modelName))
                            {
                                IResourceManager manager = objclass.FeatureDataSet as IResourceManager;
                                if (!string.IsNullOrEmpty(mp.ModelName))
                                {
                                    if (!manager.ModelExist(mp.ModelName))
                                    {
                                        if (manager.AddModel(mp.ModelName, fineModel, simpleModel))
                                        {
                                            bSuccess = true;
                                        }
                                    }
                                    else
                                    {
                                        if (manager.UpdateModel(mp.ModelName, fineModel) && manager.UpdateSimplifiedModel(mp.ModelName, simpleModel))
                                        {
                                            bSuccess = true;
                                        }
                                    }
                                }
                                if (bSuccess)
                                {
                                    rowBuffer.SetValue(0, oid);
                                    mp.ModelEnvelope = fineModel.Envelope;
                                    pos = rowBuffer.FieldIndex("Geometry");
                                    if (pos > -1) rowBuffer.SetValue(pos, mp);
                                    pos = rowBuffer.FieldIndex("ModelName");
                                    if (pos > -1) rowBuffer.SetValue(pos, mp.ModelName);
                                    cursor.UpdateRow(rowBuffer);

                                    snum++;

                                    this._3DControl.RefreshModel(objclass.FeatureDataSet, mp.ModelName);
                                    this._3DControl.FeatureManager.EditFeature(objclass, rowBuffer);

                                    ITableLabel tl = DrawTool.CreateTableLabel1(1);
                                    tl.TitleText = reg.ToString();
                                    tl.SetRecord(0, 0, name);
                                    tl.Position = line.Midpoint;
                                    this._3DControl.Camera.FlyToObject(tl.Guid, gviActionCode.gviActionFlyTo);
                                    this._listRGuid.Add(tl.Guid);

                                    IModelPointSymbol mps = new ModelPointSymbol();
                                    mps.SetResourceDataSet(objclass.FeatureDataSet);
                                    IRenderModelPoint rmp = this._3DControl.ObjectManager.CreateRenderModelPoint(mp, mps, this._3DControl.ProjectTree.RootID);
                                    rmp.Glow(8000);
                                    this._3DControl.ObjectManager.DelayDelete(rmp.Guid, 8000);
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (cursor != null)
                        {
                            Marshal.ReleaseComObject(cursor);
                            cursor = null;
                        }
                    }
                    WaitForm.SetCaption("成功生成" + snum + "个，共" + count + "个");
                }
                WaitForm.Stop();
                XtraMessageBox.Show("成功生成" + snum + "个，共" + count + "个", "提示");
            }
            catch (Exception ex) {
                WaitForm.Stop();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDesignValidate_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Guid g in this._listRGuid)
            {
                this._3DControl.ObjectManager.DeleteObject(g);
            }
            this._listRGuid.Clear();
        }

    }
}
