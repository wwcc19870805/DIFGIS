using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Gvitech.CityMaker.FdeCore;
using ICSharpCode.Core;
using DF3DEdit.Class;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DF3DEdit.UC
{
    public class UCDomainEdit : XtraUserControl
    {
        public class DomainCollection
        {
            public System.Data.DataTable table;
            public string ft;
            public string dt;
            public object min;
            public object max;
            public string description;
            public string dn;
            public bool isNew = true;
        }
        private FDEProcess instance = new FDEProcess();
        private IDataSource dsrc;
        private System.Collections.Hashtable reftable = new System.Collections.Hashtable();
        private System.Data.DataTable mt = new System.Data.DataTable("MainTable");
        private System.Data.DataTable curtable = new System.Data.DataTable();
        private System.Collections.ArrayList delBuf = new System.Collections.ArrayList();
        private string fieldMin = "最小值";
        private string fieldMax = "最大值";
        private string fieldCode = "描述";
        private string fieldValue = "枚举值";
        private System.ComponentModel.IContainer components;
        private GridControl gridControl2;
        private GridView gridView2;
        private GridControl gridControl1;
        private GridView gridView1;
        private GridColumn DomainName;
        private GridColumn Description;
        private GridColumn FieldType;
        private RepositoryItemComboBox cb_ft;
        private GridColumn DomainType;
        private RepositoryItemComboBox cb_dt;
        private GridColumn GUID;
        private GridColumn IsNew;
        public bool DomainInfoReadOnly
        {
            set
            {
                for (int i = 0; i < this.gridView1.Columns.Count; i++)
                {
                    this.gridView1.Columns[i].OptionsColumn.AllowEdit = value;
                }
            }
        }
        public bool DomainPropReadOnly
        {
            set
            {
                for (int i = 0; i < this.gridView2.Columns.Count; i++)
                {
                    this.gridView2.Columns[i].OptionsColumn.AllowEdit = value;
                }
            }
        }
        public GridView GridView1
        {
            get
            {
                return this.gridView1;
            }
        }
        public GridView GridView2
        {
            get
            {
                return this.GridView2;
            }
        }
        public System.Windows.Forms.DockStyle Docked
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }
        public UCDomainEdit()
        {
            this.InitializeComponent();
        }
        public void BandDataSource(IDataSource ds)
        {
            try
            {
                this.dsrc = ds;
                this.FillControls(ds);
                if (this.gridView1.DataRowCount > 0)
                {
                    this.gridView1.FocusedRowHandle = 0;
                    System.Guid guid = (System.Guid)this.gridView1.GetRowCellValue(0, "GUID");
                    if (this.reftable.ContainsKey(guid))
                    {
                        System.Data.DataTable dataSource = this.reftable[guid] as System.Data.DataTable;
                        this.gridControl2.DataSource = dataSource;
                    }
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public void CanEdit(bool canEdit)
        {
            if (canEdit)
            {
                this.gridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyUp);
            }
        }
        private void cb_ft_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
            string text = comboBoxEdit.SelectedItem.ToString();
            if (text != null)
            {
                if (this.curtable != null)
                {
                    this.curtable.Rows.Clear();
                    this.curtable.Columns.Clear();
                }
                string a = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "dt").ToString();
                if (a == "值域型")
                {
                    this.curtable.Columns.Add(this.fieldMin, this.GetColumnTypeByString(text));
                    this.curtable.Columns.Add(this.fieldMax, this.GetColumnTypeByString(text));
                    System.Data.DataRowCollection arg_C5_0 = this.curtable.Rows;
                    object[] values = new object[2];
                    arg_C5_0.Add(values);
                }
                else
                {
                    if (a == "枚举型")
                    {
                        this.curtable.Columns.Add(this.fieldCode, typeof(string));
                        this.curtable.Columns.Add(this.fieldValue, this.GetColumnTypeByString(text));
                        for (int i = 0; i < 200; i++)
                        {
                            System.Data.DataRowCollection arg_143_0 = this.curtable.Rows;
                            object[] array = new object[2];
                            array[0] = "";
                            arg_143_0.Add(array);
                        }
                    }
                }
                this.gridView2.PopulateColumns(this.curtable);
            }
        }
        private void cb_dt_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
            string text = comboBoxEdit.SelectedItem.ToString();
            string type = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ft").ToString();
            if (text != null)
            {
                if (this.curtable != null)
                {
                    this.curtable.Rows.Clear();
                    this.curtable.Columns.Clear();
                }
                if (text == "值域型")
                {
                    this.curtable.Columns.Add(this.fieldMin, this.GetColumnTypeByString(type));
                    this.curtable.Columns.Add(this.fieldMax, this.GetColumnTypeByString(type));
                    System.Data.DataRowCollection arg_C5_0 = this.curtable.Rows;
                    object[] values = new object[2];
                    arg_C5_0.Add(values);
                }
                else
                {
                    if (text == "枚举型")
                    {
                        this.curtable.Columns.Add(this.fieldCode, typeof(string));
                        this.curtable.Columns.Add(this.fieldValue, this.GetColumnTypeByString(type));
                        for (int i = 0; i < 200; i++)
                        {
                            System.Data.DataRowCollection arg_143_0 = this.curtable.Rows;
                            object[] array = new object[2];
                            array[0] = "";
                            arg_143_0.Add(array);
                        }
                    }
                }
                this.gridView2.PopulateColumns(this.curtable);
            }
        }
        private void gridView1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                System.Data.DataRow focusedDataRow = this.gridView1.GetFocusedDataRow();
                if (focusedDataRow != null)
                {
                    string text = focusedDataRow["name"].ToString();
                    if (this.HasDomain(text, this.dsrc) && !this.delBuf.Contains(text))
                    {
                        this.delBuf.Add(text);
                    }
                    this.gridView1.DeleteSelectedRows();
                    System.Guid guid = (System.Guid)this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "GUID");
                    this.reftable.Remove(guid);
                    this.gridControl2.DataSource = null;
                }
            }
        }
        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                System.Guid guid = (System.Guid)this.gridView1.GetRowCellValue(e.RowHandle, "GUID");
                if (this.reftable.Contains(guid))
                {
                    this.curtable = (this.reftable[guid] as System.Data.DataTable);
                }
                else
                {
                    this.curtable = new System.Data.DataTable();
                    this.reftable.Add(guid, this.curtable);
                }
                this.gridControl2.DataSource = this.curtable;
                this.gridView2.PopulateColumns();
                for (int i = 0; i < this.gridView2.Columns.Count; i++)
                {
                    this.gridView2.Columns[i].OptionsColumn.AllowEdit = false;
                }
                bool arg_F9_0 = (bool)this.gridView1.GetRowCellValue(e.RowHandle, "IsNew");
            }
        }
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value.ToString() == "")
            {
                return;
            }
            if (e.Column.Name.Equals("FieldName"))
            {
                for (int i = 0; i < this.gridView1.DataRowCount; i++)
                {
                    if (i != e.RowHandle && this.gridView1.GetRowCellValue(i, "name").Equals(e.Value))
                    {
                        XtraMessageBox.Show("名称冲突");
                        this.gridView1.CancelUpdateCurrentRow();
                        return;
                    }
                }
                this.mt.Rows[e.RowHandle][e.Column.FieldName] = e.Value;
            }
        }
        private void gridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void gridView2_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                this.curtable.Rows[e.RowHandle][e.Column.FieldName] = e.Value;
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void gridControl2_Leave(object sender, System.EventArgs e)
        {
            System.Guid guid = (System.Guid)this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "GUID");
            if (this.reftable.Contains(guid))
            {
                this.reftable[guid] = this.curtable;
            }
        }
        private void FillControls(IDataSource ds)
        {
            IDomain domain = null;
            IRangeDomain rangeDomain = null;
            ICodedValueDomain codedValueDomain = null;
            try
            {
                this.mt.Columns.Add("name", typeof(string));
                this.mt.Columns.Add("description", typeof(string));
                this.mt.Columns.Add("ft", typeof(string));
                this.mt.Columns.Add("dt", typeof(string));
                this.mt.Columns.Add("GUID", typeof(System.Guid));
                this.mt.Columns.Add("IsNew", typeof(bool));
                string[] domainNames = ds.GetDomainNames();
                if (domainNames != null)
                {
                    for (int i = 0; i < domainNames.Length; i++)
                    {
                        this.curtable = new System.Data.DataTable();
                        domain = ds.GetDomainByName(domainNames[i].ToString());
                        if (domain != null)
                        {
                            string text = (domain.DomainType == gviDomainType.gviDomainRange) ? "值域型": "枚举型";
                            string text2 = this.ConvertFieldTypeByString(domain.FieldType);
                            System.Guid guid = System.Guid.NewGuid();
                            this.mt.Rows.Add(new object[]
							{
								domain.Name, 
								domain.Description, 
								text2, 
								text, 
								guid, 
								false
							});
                            if (text == "值域型")
                            {
                                rangeDomain = (domain as IRangeDomain);
                                this.curtable.Columns.Add(this.fieldMin, this.GetColumnTypeByString(text2));
                                this.curtable.Columns.Add(this.fieldMax, this.GetColumnTypeByString(text2));
                                this.curtable.Rows.Add(new object[]
								{
									rangeDomain.MinValue, 
									rangeDomain.MaxValue
								});
                            }
                            else
                            {
                                this.curtable = new System.Data.DataTable();
                                codedValueDomain = (domain as ICodedValueDomain);
                                this.curtable.Columns.Add(this.fieldValue, this.GetColumnTypeByString(text2));
                                this.curtable.Columns.Add(this.fieldCode, typeof(string));
                                for (int j = 0; j < codedValueDomain.CodeCount; j++)
                                {
                                    this.curtable.Rows.Add(new object[]
									{
										codedValueDomain.GetCodeValue(j), 
										codedValueDomain.GetCodeName(j)
									});
                                }
                                for (int k = 0; k < 200; k++)
                                {
                                    this.curtable.Rows.Add(new object[]
									{
										null, 
										""
									});
                                }
                            }
                            this.reftable.Add(guid, this.curtable);
                        }
                    }
                }
                for (int l = 0; l < 100; l++)
                {
                    this.mt.Rows.Add(new object[]
					{
						"", 
						"", 
						"", 
						"", 
						System.Guid.NewGuid(), 
						true
					});
                }
                this.gridControl1.DataSource = this.mt;
            }
            catch (System.Exception)
            {
            }
            finally
            {
                if (codedValueDomain != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(codedValueDomain);
                    codedValueDomain = null;
                }
                if (rangeDomain != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rangeDomain);
                    rangeDomain = null;
                }
                if (domain != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(domain);
                    domain = null;
                }
            }
        }
        private bool HasDomain(string domainName, IDataSource ds)
        {
            string[] domainNames = ds.GetDomainNames();
            string[] array = domainNames;
            for (int i = 0; i < array.Length; i++)
            {
                string value = array[i];
                if (domainName.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsRightType(string type, string value)
        {
            bool result;
            try
            {
                switch (type)
                {
                    case "Int16":
                        {
                            short.Parse(value);
                            break;
                        }
                    case "Int32":
                        {
                            int.Parse(value);
                            break;
                        }
                    case "Int64":
                        {
                            long.Parse(value);
                            break;
                        }
                    case "Float":
                        {
                            float.Parse(value);
                            break;
                        }
                    case "Double":
                        {
                            double.Parse(value);
                            break;
                        }
                    case "Date":
                        {
                            System.DateTime.Parse(value);
                            break;
                        }
                }
                result = true;
            }
            catch (System.Exception)
            {
                result = false;
            }
            return result;
        }
        private object GetColumnDefaultValue(string type)
        {
            switch (type)
            {
                case "Int16":
                    {
                        return 0;
                    }
                case "Int32":
                    {
                        return 0;
                    }
                case "Int64":
                    {
                        return 0;
                    }
                case "Float":
                    {
                        return 0;
                    }
                case "Double":
                    {
                        return 0;
                    }
                case "Date":
                    {
                        return System.DateTime.Now;
                    }
            }
            return "";
        }
        private System.Type GetColumnTypeByString(string type)
        {
            switch (type)
            {
                case "Int16":
                    {
                        return typeof(short);
                    }
                case "Int32":
                    {
                        return typeof(int);
                    }
                case "Int64":
                    {
                        return typeof(long);
                    }
                case "Float":
                    {
                        return typeof(float);
                    }
                case "Double":
                    {
                        return typeof(double);
                    }
                case "Date":
                    {
                        return typeof(System.DateTime);
                    }
            }
            return typeof(string);
        }
        private gviDomainType GetDomainTypeByString(string domaintype)
        {
            if (domaintype.Equals("值域型"))
            {
                return gviDomainType.gviDomainRange;
            }
            return gviDomainType.gviDomainCodedValue;
        }
        private gviFieldType GetFDEFieldTypeByString(string fieldtype)
        {
            switch (fieldtype)
            {
                case "Int16":
                    {
                        return gviFieldType.gviFieldInt16;
                    }
                case "Int32":
                    {
                        return gviFieldType.gviFieldInt32;
                    }
                case "Int64":
                    {
                        return gviFieldType.gviFieldInt64;
                    }
                case "Float":
                    {
                        return gviFieldType.gviFieldFloat;
                    }
                case "Double":
                    {
                        return gviFieldType.gviFieldDouble;
                    }
                case "Date":
                    {
                        return gviFieldType.gviFieldDate;
                    }
            }
            return gviFieldType.gviFieldString;
        }
        private string ConvertFieldTypeByString(gviFieldType fieldtype)
        {
            switch (fieldtype)
            {
                case gviFieldType.gviFieldInt16:
                    {
                        return "Int16";
                    }
                case gviFieldType.gviFieldInt32:
                    {
                        return "Int32";
                    }
                case gviFieldType.gviFieldInt64:
                    {
                        return "Int64";
                    }
                case gviFieldType.gviFieldFloat:
                    {
                        return "Float";
                    }
                case gviFieldType.gviFieldDouble:
                    {
                        return "Double";
                    }
                case gviFieldType.gviFieldDate:
                    {
                        return "Date";
                    }
            }
            return "String";
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
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DomainName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FieldType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cb_ft = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.DomainType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cb_dt = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.GUID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IsNew = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_ft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_dt)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(2, 207);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(387, 200);
            this.gridControl2.TabIndex = 39;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Leave += new System.EventHandler(this.gridControl2_Leave);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsMenu.EnableColumnMenu = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView2_CellValueChanging);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 1);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cb_ft,
            this.cb_dt});
            this.gridControl1.Size = new System.Drawing.Size(387, 200);
            this.gridControl1.TabIndex = 38;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.DomainName,
            this.Description,
            this.FieldType,
            this.DomainType,
            this.GUID,
            this.IsNew});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // DomainName
            // 
            this.DomainName.Caption = "名称";
            this.DomainName.FieldName = "name";
            this.DomainName.Name = "DomainName";
            this.DomainName.Visible = true;
            this.DomainName.VisibleIndex = 0;
            // 
            // Description
            // 
            this.Description.Caption = "描述";
            this.Description.FieldName = "description";
            this.Description.Name = "Description";
            this.Description.Visible = true;
            this.Description.VisibleIndex = 1;
            // 
            // FieldType
            // 
            this.FieldType.Caption = "字段类型";
            this.FieldType.ColumnEdit = this.cb_ft;
            this.FieldType.FieldName = "ft";
            this.FieldType.Name = "FieldType";
            this.FieldType.Visible = true;
            this.FieldType.VisibleIndex = 2;
            // 
            // cb_ft
            // 
            this.cb_ft.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.True;
            this.cb_ft.AutoHeight = false;
            this.cb_ft.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_ft.Items.AddRange(new object[] {
            "Int16",
            "Int32",
            "Int64",
            "Float",
            "Double",
            "Date",
            "String"});
            this.cb_ft.Name = "cb_ft";
            this.cb_ft.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_ft.SelectedIndexChanged += new System.EventHandler(this.cb_ft_SelectedIndexChanged);
            // 
            // DomainType
            // 
            this.DomainType.Caption = "域类型";
            this.DomainType.ColumnEdit = this.cb_dt;
            this.DomainType.FieldName = "dt";
            this.DomainType.Name = "DomainType";
            this.DomainType.Visible = true;
            this.DomainType.VisibleIndex = 3;
            // 
            // cb_dt
            // 
            this.cb_dt.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.True;
            this.cb_dt.AutoHeight = false;
            this.cb_dt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_dt.Items.AddRange(new object[] {
            "值域型",
            "枚举型"});
            this.cb_dt.Name = "cb_dt";
            this.cb_dt.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_dt.SelectedIndexChanged += new System.EventHandler(this.cb_dt_SelectedIndexChanged);
            // 
            // GUID
            // 
            this.GUID.Caption = "GUID";
            this.GUID.FieldName = "GUID";
            this.GUID.Name = "GUID";
            // 
            // IsNew
            // 
            this.IsNew.Caption = "IsNew";
            this.IsNew.FieldName = "IsNew";
            this.IsNew.Name = "IsNew";
            // 
            // UCDomainEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.gridControl1);
            this.Name = "UCDomainEdit";
            this.Size = new System.Drawing.Size(391, 409);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_ft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_dt)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
