using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using DFDataConfig.Dbf;
using DFDataConfig.Class;

namespace DFDataConfig.UC
{
    public class UCFieldManage : XtraUserControl
    {
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAlias;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSystemName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSystemAlias;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCanQuery;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbi_Add;
        private DevExpress.XtraBars.BarButtonItem bbi_Delete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem bbi_Import;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraBars.BarButtonItem bbi_SaveFieldInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNeedCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCanStats;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup3;
        private IContainer components;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDataType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarButtonItem bbi_Export;
    
        public UCFieldManage()
        {
            InitializeComponent();
            this._dtGrid = new DataTable();
            this._dtGrid.TableName = "属性表";
            System.Data.DataColumn dataColumn1 = new System.Data.DataColumn("Name", typeof(string));
            System.Data.DataColumn dataColumn2 = new System.Data.DataColumn("Alias", typeof(string));
            System.Data.DataColumn dataColumn8 = new System.Data.DataColumn("DataType", typeof(string));
            System.Data.DataColumn dataColumn3 = new System.Data.DataColumn("SystemName", typeof(string));
            System.Data.DataColumn dataColumn4 = new System.Data.DataColumn("SystemAlias", typeof(string));
            System.Data.DataColumn dataColumn5 = new System.Data.DataColumn("CanQuery", typeof(string));
            System.Data.DataColumn dataColumn6 = new System.Data.DataColumn("NeedCheck", typeof(string));
            System.Data.DataColumn dataColumn7 = new System.Data.DataColumn("CanStats", typeof(string));
            this._dtGrid.Columns.AddRange(new System.Data.DataColumn[]
			{
				dataColumn1, 
				dataColumn2, 
                dataColumn8,
				dataColumn3, 
				dataColumn4, 
				dataColumn5,
                dataColumn6,
                dataColumn7
			});
            this.gridControl1.DataSource = this._dtGrid;
            this.bbi_SaveFieldInfo.Enabled = false;
            this._bNeedSave = false;
        }

        public void Init()
        {
            this._dtGrid.Clear();
            this.bbi_SaveFieldInfo.Enabled = false;
            this._bNeedSave = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFieldManage));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAlias = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDataType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnSystemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSystemAlias = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCanQuery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.gridColumnNeedCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup2 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.gridColumnCanStats = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup3 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbi_SaveFieldInfo = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_Add = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_Delete = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_Import = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_Export = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 24);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRadioGroup1,
            this.repositoryItemRadioGroup2,
            this.repositoryItemRadioGroup3,
            this.repositoryItemComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(582, 371);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnAlias,
            this.gridColumnDataType,
            this.gridColumnSystemName,
            this.gridColumnSystemAlias,
            this.gridColumnCanQuery,
            this.gridColumnNeedCheck,
            this.gridColumnCanStats});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "名称";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnName.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnName.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnName.OptionsFilter.AllowFilter = false;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            // 
            // gridColumnAlias
            // 
            this.gridColumnAlias.Caption = "别名";
            this.gridColumnAlias.FieldName = "Alias";
            this.gridColumnAlias.Name = "gridColumnAlias";
            this.gridColumnAlias.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnAlias.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnAlias.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnAlias.OptionsFilter.AllowFilter = false;
            this.gridColumnAlias.Visible = true;
            this.gridColumnAlias.VisibleIndex = 1;
            // 
            // gridColumnDataType
            // 
            this.gridColumnDataType.Caption = "数据类型";
            this.gridColumnDataType.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumnDataType.FieldName = "DataType";
            this.gridColumnDataType.Name = "gridColumnDataType";
            this.gridColumnDataType.Visible = true;
            this.gridColumnDataType.VisibleIndex = 2;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "String",
            "Decimal"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox1.EditValueChanged += new System.EventHandler(this.repositoryItemComboBox1_EditValueChanged);
            // 
            // gridColumnSystemName
            // 
            this.gridColumnSystemName.Caption = "系统名称";
            this.gridColumnSystemName.FieldName = "SystemName";
            this.gridColumnSystemName.Name = "gridColumnSystemName";
            this.gridColumnSystemName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnSystemName.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnSystemName.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnSystemName.OptionsFilter.AllowFilter = false;
            this.gridColumnSystemName.Visible = true;
            this.gridColumnSystemName.VisibleIndex = 3;
            // 
            // gridColumnSystemAlias
            // 
            this.gridColumnSystemAlias.Caption = "系统别名";
            this.gridColumnSystemAlias.FieldName = "SystemAlias";
            this.gridColumnSystemAlias.Name = "gridColumnSystemAlias";
            this.gridColumnSystemAlias.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnSystemAlias.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnSystemAlias.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnSystemAlias.OptionsFilter.AllowFilter = false;
            this.gridColumnSystemAlias.Visible = true;
            this.gridColumnSystemAlias.VisibleIndex = 4;
            // 
            // gridColumnCanQuery
            // 
            this.gridColumnCanQuery.Caption = "能否查询";
            this.gridColumnCanQuery.ColumnEdit = this.repositoryItemRadioGroup1;
            this.gridColumnCanQuery.FieldName = "CanQuery";
            this.gridColumnCanQuery.Name = "gridColumnCanQuery";
            this.gridColumnCanQuery.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnCanQuery.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnCanQuery.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnCanQuery.OptionsFilter.AllowFilter = false;
            this.gridColumnCanQuery.Visible = true;
            this.gridColumnCanQuery.VisibleIndex = 5;
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Columns = 2;
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("true", "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("false", "否")});
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            this.repositoryItemRadioGroup1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemRadioGroup1_SelectedIndexChanged);
            // 
            // gridColumnNeedCheck
            // 
            this.gridColumnNeedCheck.Caption = "能否检查";
            this.gridColumnNeedCheck.ColumnEdit = this.repositoryItemRadioGroup2;
            this.gridColumnNeedCheck.FieldName = "NeedCheck";
            this.gridColumnNeedCheck.Name = "gridColumnNeedCheck";
            this.gridColumnNeedCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnNeedCheck.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnNeedCheck.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnNeedCheck.OptionsFilter.AllowFilter = false;
            this.gridColumnNeedCheck.Visible = true;
            this.gridColumnNeedCheck.VisibleIndex = 7;
            // 
            // repositoryItemRadioGroup2
            // 
            this.repositoryItemRadioGroup2.Columns = 2;
            this.repositoryItemRadioGroup2.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("true", "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("false", "否")});
            this.repositoryItemRadioGroup2.Name = "repositoryItemRadioGroup2";
            this.repositoryItemRadioGroup2.SelectedIndexChanged += new System.EventHandler(this.repositoryItemRadioGroup2_SelectedIndexChanged);
            // 
            // gridColumnCanStats
            // 
            this.gridColumnCanStats.Caption = "能否统计";
            this.gridColumnCanStats.ColumnEdit = this.repositoryItemRadioGroup3;
            this.gridColumnCanStats.FieldName = "CanStats";
            this.gridColumnCanStats.Name = "gridColumnCanStats";
            this.gridColumnCanStats.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnCanStats.OptionsColumn.ShowInCustomizationForm = false;
            this.gridColumnCanStats.OptionsFilter.AllowAutoFilter = false;
            this.gridColumnCanStats.OptionsFilter.AllowFilter = false;
            this.gridColumnCanStats.Visible = true;
            this.gridColumnCanStats.VisibleIndex = 6;
            // 
            // repositoryItemRadioGroup3
            // 
            this.repositoryItemRadioGroup3.Columns = 2;
            this.repositoryItemRadioGroup3.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("true", "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("false", "否")});
            this.repositoryItemRadioGroup3.Name = "repositoryItemRadioGroup3";
            this.repositoryItemRadioGroup3.SelectedIndexChanged += new System.EventHandler(this.repositoryItemRadioGroup3_SelectedIndexChanged);
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbi_Add,
            this.bbi_Delete,
            this.bbi_Import,
            this.bbi_Export,
            this.bbi_SaveFieldInfo});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 6;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_SaveFieldInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_Add, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_Delete),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_Import, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_Export)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DisableCustomization = true;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbi_SaveFieldInfo
            // 
            this.bbi_SaveFieldInfo.Caption = "保存字段信息";
            this.bbi_SaveFieldInfo.Id = 5;
            this.bbi_SaveFieldInfo.ImageIndex = 0;
            this.bbi_SaveFieldInfo.Name = "bbi_SaveFieldInfo";
            this.bbi_SaveFieldInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_SaveFieldInfo_ItemClick);
            // 
            // bbi_Add
            // 
            this.bbi_Add.Caption = "添加";
            this.bbi_Add.Id = 0;
            this.bbi_Add.ImageIndex = 1;
            this.bbi_Add.Name = "bbi_Add";
            this.bbi_Add.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_Add_ItemClick);
            // 
            // bbi_Delete
            // 
            this.bbi_Delete.Caption = "删除";
            this.bbi_Delete.Id = 1;
            this.bbi_Delete.ImageIndex = 2;
            this.bbi_Delete.Name = "bbi_Delete";
            this.bbi_Delete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_Delete_ItemClick);
            // 
            // bbi_Import
            // 
            this.bbi_Import.Caption = "导入";
            this.bbi_Import.Id = 3;
            this.bbi_Import.ImageIndex = 3;
            this.bbi_Import.Name = "bbi_Import";
            this.bbi_Import.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_Import_ItemClick);
            // 
            // bbi_Export
            // 
            this.bbi_Export.Caption = "导出";
            this.bbi_Export.Id = 4;
            this.bbi_Export.ImageIndex = 4;
            this.bbi_Export.Name = "bbi_Export";
            this.bbi_Export.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_Export_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(582, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 395);
            this.barDockControlBottom.Size = new System.Drawing.Size(582, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 371);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(582, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 371);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("save_16x16.png", "images/save/save_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/save/save_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "save_16x16.png");
            this.imageCollection1.InsertGalleryImage("add_16x16.png", "images/actions/add_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/add_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "add_16x16.png");
            this.imageCollection1.InsertGalleryImage("remove_16x16.png", "images/actions/remove_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/remove_16x16.png"), 2);
            this.imageCollection1.Images.SetKeyName(2, "remove_16x16.png");
            this.imageCollection1.InsertGalleryImage("open_16x16.png", "images/actions/open_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/open_16x16.png"), 3);
            this.imageCollection1.Images.SetKeyName(3, "open_16x16.png");
            this.imageCollection1.InsertGalleryImage("loadfrom_16x16.png", "images/actions/loadfrom_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/loadfrom_16x16.png"), 4);
            this.imageCollection1.Images.SetKeyName(4, "loadfrom_16x16.png");
            // 
            // UCFieldManage
            // 
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCFieldManage";
            this.Size = new System.Drawing.Size(582, 395);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dtGrid;
        private FacilityClass _fc;
        private bool _bNeedSave;
        public bool NeedSave
        {
            get { return this._bNeedSave; }
            set { this._bNeedSave = value; }
        }

        public void SetData(FacilityClass fc)
        {
            if (this._fc != null && this._bNeedSave)
            {
                if (XtraMessageBox.Show("是否需要保存当前设施类的字段信息？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    SaveFieldInfo();
                }
                else this._bNeedSave = false;
            }

            this._dtGrid.Clear();

            this._fc = fc;
            if (this._fc != null)
            {
                foreach (FieldInfo fi in this._fc.FieldInfoCollection)
                {
                    DataRow dr = this._dtGrid.NewRow();
                    dr["Name"] = fi.Name;
                    dr["Alias"] = fi.Alias;
                    dr["DataType"] = fi.DataType;
                    dr["SystemName"] = fi.SystemName;
                    dr["SystemAlias"] = fi.SystemAlias;
                    if (fi.CanQuery) dr["CanQuery"] = "true";
                    else dr["CanQuery"] = "false";
                    if (fi.NeedCheck) dr["NeedCheck"] = "true";
                    else dr["NeedCheck"] = "false";
                    if (fi.CanStats) dr["CanStats"] = "true";
                    else dr["CanStats"] = "false";
                    this._dtGrid.Rows.Add(dr);
                }
            }
        }

        private void ReadDBF(string fileName)
        {
            try
            {
                TDbfTable dbf = new TDbfTable(fileName);
                if (dbf != null)
                {
                    DataTable dt = dbf.Table;
                    if (dt != null)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            DataRow row = this._dtGrid.NewRow();
                            row["Name"] = col.ColumnName;
                            row["CanQuery"] = "true";
                            row["NeedCheck"] = "true";
                            row["CanStats"] = "true";
                            row["DataType"] = col.DataType.Name.ToString();
                            this._dtGrid.Rows.Add(row);
                        }
                        this.bbi_SaveFieldInfo.Enabled = true;
                        this._bNeedSave = true;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void bbi_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Shp DBase(*.dbf,*.xml)|*.dbf;*.xml",
                RestoreDirectory = true
            };

            this._dtGrid.Clear();

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = dlg.FileName;
                string ext = fileName.Substring(fileName.Length - 3, 3);
                switch (ext)
                {
                    case "dbf":
                        ReadDBF(fileName);
                        break;
                    case "xls":
                        break;
                    case "xlsx":
                        break;
                    case "xml":
                        this._dtGrid.ReadXml(fileName);
                        break;
                }
                
                    
            }
        }

        private void bbi_Export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog()
            {
                Filter = "Xml File(*.xml)|*.xml",
                DefaultExt = "xml"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this._dtGrid.WriteXml(dlg.FileName);
            }
        }

        private void bbi_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow dr = this._dtGrid.NewRow();
            dr["DataType"] = "String";
            dr["CanQuery"] = "true";
            dr["NeedCheck"] = "true";
            dr["CanStats"] = "true";
            this._dtGrid.Rows.Add(dr);
        }

        private void bbi_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle < this._dtGrid.Rows.Count && this.gridView1.FocusedRowHandle > -1)
            {
                DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
                this._dtGrid.Rows.Remove(dr);
                this.bbi_SaveFieldInfo.Enabled = true;
                this._bNeedSave = true;
            }
        }

        public void SaveFieldInfo()
        {
            if (this._fc == null) return;
            this._fc.FieldInfoCollection.Clear();
            foreach (DataRow dr in this._dtGrid.Rows)
            {
                if (dr["Name"] == null || string.IsNullOrEmpty(dr["Name"].ToString())) continue;
                string fieldName = "", fieldAliasName = "",dataType = "", fieldSystemName = "", fieldSystemAliasName = "", fieldCanQuery = "", fieldNeedCheck = "", fieldCanStats = "";
                if (dr["name"] != null) fieldName = dr["name"].ToString();
                if (dr["alias"] != null) fieldAliasName = dr["alias"].ToString();
                if (dr["datatype"] != null) dataType = dr["datatype"].ToString();
                if (dr["systemname"] != null) fieldSystemName = dr["systemname"].ToString();
                if (dr["systemalias"] != null) fieldSystemAliasName = dr["systemalias"].ToString();
                if (dr["canquery"] != null) fieldCanQuery = dr["canquery"].ToString();
                if (dr["needcheck"] != null) fieldNeedCheck = dr["needcheck"].ToString();
                if (dr["canstats"] != null) fieldCanStats = dr["canstats"].ToString();
                bool bCanQuery = false;
                if (!string.IsNullOrEmpty(fieldCanQuery) && fieldCanQuery == "true") bCanQuery = true;
                bool bNeedCheck = false;
                if (!string.IsNullOrEmpty(fieldNeedCheck) && fieldNeedCheck == "true") bNeedCheck = true;
                bool bCanStats = false;
                if (!string.IsNullOrEmpty(fieldCanStats) && fieldCanStats == "true") bCanStats = true;
                FieldInfo fi = new FieldInfo(fieldName, fieldAliasName, fieldSystemName, fieldSystemAliasName, bCanQuery, bNeedCheck, bCanStats, dataType);
                this._fc.AddFieldInfo(fi);
            }
            this.bbi_SaveFieldInfo.Enabled = false;
            this._bNeedSave = false;
        }

        private void bbi_SaveFieldInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFieldInfo();
        }

        private void repositoryItemRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            RadioGroup rg = sender as RadioGroup;
            dr["CanQuery"] = rg.EditValue;
            this.gridView1.RefreshRow(this.gridView1.FocusedRowHandle);
            this.bbi_SaveFieldInfo.Enabled = true;
            this._bNeedSave = true;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.bbi_SaveFieldInfo.Enabled = true;
            this._bNeedSave = true;
        }

        private void repositoryItemRadioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            RadioGroup rg = sender as RadioGroup;
            dr["NeedCheck"] = rg.EditValue;
            this.gridView1.RefreshRow(this.gridView1.FocusedRowHandle);
            this.bbi_SaveFieldInfo.Enabled = true;
            this._bNeedSave = true;
        }

        private void repositoryItemRadioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            RadioGroup rg = sender as RadioGroup;
            dr["CanStats"] = rg.EditValue;
            this.gridView1.RefreshRow(this.gridView1.FocusedRowHandle);
            this.bbi_SaveFieldInfo.Enabled = true;
            this._bNeedSave = true;
        }

        private void repositoryItemComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            ComboBoxEdit cb = sender as ComboBoxEdit;
            dr["DataType"] = cb.Text;
            this.gridView1.RefreshRow(this.gridView1.FocusedRowHandle);
            this.bbi_SaveFieldInfo.Enabled = true;
            this._bNeedSave = true;
        }

    }
}
