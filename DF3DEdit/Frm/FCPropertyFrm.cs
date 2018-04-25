using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using DF3DEdit.Class;
using ICSharpCode.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DF3DEdit.Frm
{
    public partial class FCPropertyFrm : XtraForm
    {
        private string fcname = "";
        private string fcalias = "";
        private string propname = "属性名";
        private string propvalue = "属性值";
        private bool isServer;
        private bool isArcSDE;
        private bool hasDelFieldCap;
        private bool hasMdiFieldCap;
        private bool canEdit;
        private bool bActiveInfo;
        private bool bActiveIndex;
        private bool bActiveSubtype;
        private bool bActiveEnvelope;
        private IFeatureDataSet fds;
        private IFeatureClass fc;
        private IFieldInfoCollection fcFields;
        private IDbIndexInfoCollection DbIndexInfos;
        private IIndexInfoCollection GridIndexInfos;
        private IIndexInfoCollection RenderIndexInfos;
        private IDbIndexInfo DbIndex;
        private IGridIndexInfo GridIndex;
        private IRenderIndexInfo RenderIndex;
        private DbIndexBuffer dbbuffer = new DbIndexBuffer();
        private GridIndexBuffer gibuffer = new GridIndexBuffer();
        private RenderIndexBuffer ribuffer = new RenderIndexBuffer();
        private System.Collections.Hashtable PropHash;
        private System.Collections.Generic.List<string> DelList = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.List<string> GeoColumnList = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.Dictionary<string, DbIndexBuffer> DbIndexHash = new System.Collections.Generic.Dictionary<string, DbIndexBuffer>();
        private System.Collections.Generic.Dictionary<string, GridIndexBuffer> GridIndexHash = new System.Collections.Generic.Dictionary<string, GridIndexBuffer>();
        private System.Collections.Generic.Dictionary<string, RenderIndexBuffer> RenderIndexHash = new System.Collections.Generic.Dictionary<string, RenderIndexBuffer>();
        private System.Guid guid = System.Guid.Empty;
        private System.Data.DataTable Fdt;
        private RepositoryItemComboBox cbe_Domain;
        private RepositoryItemComboBox cbe_NotNull;
        private RepositoryItemComboBox cbe_GeoType;
        private RepositoryItemDateEdit de_Datetime;
        private readonly string strNone = "---------None---------";
        private SubTypeInfo subtype;
        private System.Collections.Hashtable subtypeHash;
        private RepositoryItemComboBox cbe_subtypedom;
        private readonly string strYse = "是";
        private readonly string strNo = "否";
        private System.Collections.ArrayList delSubtypes = new System.Collections.ArrayList();
        private string Colfn = "字段名称";
        private string Colft = "字段类型";
        private string Coldefvalue = "默认值";
        private string Coldomain = "域约束";


        public FCPropertyFrm()
        {
            InitializeComponent();
        }

        public FCPropertyFrm(IDataSource ds, string dsetName, IFeatureClass fc, bool inProjectTree, bool isCheckOut)
            : this()
        {
            try
            {
                this.PropHash = new System.Collections.Hashtable();
                this.hasDelFieldCap = ds.HasCapability(gviFdbCapability.gviFdbCapDeleteField);
                this.hasMdiFieldCap = ds.HasCapability(gviFdbCapability.gviFdbCapModifyField);
                if (ds.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionArcSDE10x)
                {
                    this.isArcSDE = true;
                }
                this.sbtn_Del.Enabled = false;
                try
                {
                    this.fds = ds.OpenFeatureDataset(dsetName);
                    if (this.fds != null)
                    {
                        this.fc = fc;
                        this.te_fcName.Text = this.fc.Name;
                        this.te_fcAlias.Text = this.fc.AliasName;
                        this.fcFields = this.fc.GetFields();
                        this.Fdt = new System.Data.DataTable();
                        this.Fdt.Columns.Add("FieldName", typeof(string));
                        this.Fdt.Columns.Add("FieldType", typeof(string));
                        this.Fdt.Columns.Add("FieldGUID", typeof(System.Guid));
                        this.Fdt.Columns.Add("FieldIsNew", typeof(bool));
                        for (int i = 0; i < this.fcFields.Count; i++)
                        {
                            this.guid = System.Guid.NewGuid();
                            IFieldInfo fieldInfo = this.fcFields.Get(i);
                            if (fieldInfo.FieldType != gviFieldType.gviFieldFID)
                            {
                                if (fieldInfo.FieldType == gviFieldType.gviFieldGeometry)
                                {
                                    this.GeoColumnList.Add(fieldInfo.Name);
                                }
                                if (!fieldInfo.Required || fieldInfo.IsSystemField || fieldInfo.FieldType != gviFieldType.gviFieldInt64)
                                {
                                    this.Fdt.Rows.Add(new object[]
									{
										fieldInfo.Name, 
										this.GetGviFieldType2String(fieldInfo.FieldType), 
										this.guid, 
										false
									});
                                    this.PropHash.Add(this.guid, this.FillPropertyTable(fieldInfo));
                                }
                            }
                        }
                        for (int j = 0; j < 100; j++)
                        {
                            this.Fdt.Rows.Add(new object[]
							{
								"", 
								"", 
								System.Guid.NewGuid(), 
								true
							});
                        }
                        this.gridControl1.DataSource = this.Fdt;
                        if (this.PropHash.Count > 0)
                        {
                            this.gridControl2.DataSource = this.PropHash[0] as System.Data.DataTable;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                }
                bool flag = false;
                try
                {
                    if (this.hasMdiFieldCap)
                    {
                        this.fc.LockType = gviLockType.gviLockExclusiveSchema;
                        flag = true;
                    }
                }
                catch (System.Exception e)
                {
                    LoggingService.Error(e.Message + "\r\n" + e.StackTrace);
                }
                if (inProjectTree || isCheckOut || !flag || this.isServer || this.isArcSDE)
                {
                    this.canEdit = false;
                    this.te_fcAlias.Enabled = false;
                    this.gvFieldList.OptionsBehavior.ReadOnly = true;
                    this.gvFieldProperty.OptionsBehavior.ReadOnly = true;
                    this.sbtn_OK.Enabled = false;
                }
                else
                {
                    this.canEdit = true;
                    this.te_fcAlias.Enabled = true;
                    this.gvFieldList.OptionsBehavior.ReadOnly = false;
                    this.gvFieldProperty.OptionsBehavior.ReadOnly = false;
                    this.sbtn_OK.Enabled = true;
                }
                if (ds.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionArcSDE10x)
                {
                    this.sbtn_CalculateEnvelope.Enabled = false;
                    this.cbe_defsubtype.Enabled = false;
                    this.cbe_SubtypeField.Enabled = false;
                    this.sbtn_CreateDBIndex.Enabled = false;
                    this.sbtn_DeleteDBIndex.Enabled = false;
                    this.lbc_DBIndex.Enabled = false;
                    this.lbc_IndexFields.Enabled = false;
                    this.sbtn_CalculateGridIndex.Enabled = false;
                    this.sbtn_DeleteGridIndex.Enabled = false;
                    this.cbe_GridGeoColumn.Enabled = false;
                    this.te_L1.Enabled = false;
                    this.te_L2.Enabled = false;
                    this.te_L3.Enabled = false;
                    this.sbtn_OK.Enabled = true;
                    if (inProjectTree)
                    {
                        this.sbtn_OK.Enabled = false;
                    }
                    else
                    {
                        this.sbtn_OK.Enabled = true;
                    }
                }
                this.cbe_NotNull = new RepositoryItemComboBox();
                this.cbe_NotNull.Name = "notnull";
                this.cbe_NotNull.Items.AddRange(new object[]
				{
					"是", 
					"否"
				});
                this.cbe_NotNull.AllowDropDownWhenReadOnly = DefaultBoolean.True;
                this.cbe_NotNull.TextEditStyle = TextEditStyles.DisableTextEditor;
                this.cbe_NotNull.CycleOnDblClick = true;
                this.cbe_GeoType = new RepositoryItemComboBox();
                this.cbe_GeoType.Name = "geotype";
                this.cbe_GeoType.Items.AddRange(new object[]
				{
					"点", 
					"多边形", 
					"折线", 
					"点云", 
					"模型"
				});
                this.cbe_GeoType.AllowDropDownWhenReadOnly = DefaultBoolean.True;
                this.cbe_GeoType.TextEditStyle = TextEditStyles.DisableTextEditor;
                this.cbe_GeoType.CycleOnDblClick = true;
                this.cbe_Domain = new RepositoryItemComboBox();
                this.cbe_Domain.Name = "domain";
                this.cbe_Domain.AllowDropDownWhenReadOnly = DefaultBoolean.True;
                this.cbe_Domain.TextEditStyle = TextEditStyles.DisableTextEditor;
                this.cbe_Domain.CycleOnDblClick = true;
                this.gridControl2.RepositoryItems.AddRange(new RepositoryItem[]
				{
					this.cbe_NotNull, 
					this.cbe_GeoType, 
					this.cbe_Domain
				});
                this.de_Datetime = new RepositoryItemDateEdit();
                this.de_Datetime.TextEditStyle = TextEditStyles.DisableTextEditor;
                this.gridControl4.RepositoryItems.Add(this.de_Datetime);
                this.DbIndexInfos = this.fc.GetDbIndexInfos();
                if (this.DbIndexInfos != null)
                {
                    int k = 0;
                    while (k < this.DbIndexInfos.Count)
                    {
                        this.DbIndex = this.DbIndexInfos[k];
                        if (this.DbIndex.FieldCount != 1)
                        {
                            goto IL_6BA;
                        }
                        string fieldName = this.DbIndex.GetFieldName(0);
                        int num = this.fcFields.IndexOf(fieldName);
                        if (num >= 0)
                        {
                            IFieldInfo fieldInfo2 = this.fcFields.Get(num);
                            if (fieldInfo2.FieldType != gviFieldType.gviFieldFID && fieldInfo2.FieldType != gviFieldType.gviFieldGeometry && fieldInfo2.FieldType != gviFieldType.gviFieldBlob)
                            {
                                goto IL_6BA;
                            }
                        }
                    IL_712:
                        k++;
                        continue;
                    IL_6BA:
                        DbIndexBuffer dbIndexBuffer = new DbIndexBuffer();
                        dbIndexBuffer.data = this.DbIndex;
                        dbIndexBuffer.et = IndexEditType.Normal;
                        dbIndexBuffer.bDatabase = true;
                        this.DbIndexHash.Add(this.DbIndex.Name, dbIndexBuffer);
                        this.lbc_DBIndex.Items.Add(this.DbIndex.Name);
                        goto IL_712;
                    }
                }
                for (int l = 0; l < this.GeoColumnList.Count; l++)
                {
                    this.cbe_GridGeoColumn.Properties.Items.Add(this.GeoColumnList[l]);
                    this.cbe_RenderGeoColumn.Properties.Items.Add(this.GeoColumnList[l]);
                    this.cbe_geotypeOfEnvelope.Properties.Items.Add(this.GeoColumnList[l]);
                }
                this.GridIndexInfos = this.fc.GetSpatialIndexInfos();
                if (this.GridIndexInfos != null)
                {
                    for (int m = 0; m < this.GridIndexInfos.Count; m++)
                    {
                        this.GridIndex = (this.GridIndexInfos[m] as IGridIndexInfo);
                        if (this.GridIndex != null)
                        {
                            GridIndexBuffer gridIndexBuffer = new GridIndexBuffer();
                            gridIndexBuffer.data = this.GridIndex;
                            gridIndexBuffer.et = IndexEditType.Normal;
                            gridIndexBuffer.bDatabase = true;
                            this.GridIndexHash.Add(this.GridIndex.GeoColumnName, gridIndexBuffer);
                        }
                    }
                }
                if (this.cbe_GridGeoColumn.Properties.Items.Count > 0)
                {
                    this.cbe_GridGeoColumn.SelectedItem = this.cbe_GridGeoColumn.Properties.Items[0].ToString();
                }
                this.RenderIndexInfos = this.fc.GetRenderIndexInfos();
                if (this.RenderIndexInfos != null)
                {
                    for (int n = 0; n < this.RenderIndexInfos.Count; n++)
                    {
                        this.RenderIndex = (this.RenderIndexInfos[n] as IRenderIndexInfo);
                        if (this.RenderIndex != null)
                        {
                            RenderIndexBuffer renderIndexBuffer = new RenderIndexBuffer();
                            renderIndexBuffer.data = this.RenderIndex;
                            renderIndexBuffer.et = IndexEditType.Normal;
                            renderIndexBuffer.bDatabase = true;
                            this.RenderIndexHash.Add(this.RenderIndex.GeoColumnName, renderIndexBuffer);
                        }
                    }
                    for (int num2 = 0; num2 < this.fcFields.Count; num2++)
                    {
                        IFieldInfo fieldInfo3 = this.fcFields.Get(num2);
                        if (fieldInfo3.FieldType != gviFieldType.gviFieldFID && fieldInfo3.FieldType != gviFieldType.gviFieldGeometry && fieldInfo3.FieldType != gviFieldType.gviFieldBlob)
                        {
                            this.lbc_FieldsAvailable.Items.Add(fieldInfo3.Name);
                            if (fieldInfo3.RegisteredRenderIndex)
                            {
                                this.lbc_FieldsSelected.Items.Add(fieldInfo3.Name);
                            }
                        }
                    }
                }
                if (this.cbe_RenderGeoColumn.Properties.Items.Count > 0)
                {
                    this.cbe_RenderGeoColumn.SelectedItem = this.cbe_RenderGeoColumn.Properties.Items[0];
                }
                //foreach (System.Windows.Forms.Control control in this.xtratp_Index.Controls)
                //{
                //    if (!(control is ComboBoxEdit) && control.Enabled)
                //    {
                //        control.Enabled = flag;
                //    }
                //}
                this.subtypeHash = new System.Collections.Hashtable();
                if (this.fc.HasSubTypes)
                {
                    this.cbe_SubtypeField.Properties.Items.Add(this.strNone);
                    this.cbe_SubtypeField.Properties.Items.Add(this.fc.SubTypeFieldName);
                    this.cbe_SubtypeField.SelectedItem = this.fc.SubTypeFieldName;
                    SubTypeInfo subtypeFormCode = this.GetSubtypeFormCode(this.fc.DefaultSubTypeCode);
                    if (subtypeFormCode != null)
                    {
                        this.cbe_defsubtype.Properties.Items.Add(subtypeFormCode.Name);
                        this.cbe_defsubtype.SelectedIndex = 0;
                    }
                }
                else
                {
                    IFieldInfoCollection fields = this.fc.GetFields();
                    this.cbe_SubtypeField.Properties.Items.Add(this.strNone);
                    for (int num3 = 0; num3 < fields.Count; num3++)
                    {
                        IFieldInfo fieldInfo4 = fields.Get(num3);
                        if (fieldInfo4.FieldType == gviFieldType.gviFieldInt32)
                        {
                            this.cbe_SubtypeField.Properties.Items.Add(fieldInfo4.Name);
                        }
                    }
                    this.cbe_SubtypeField.SelectedItem = this.strNone;
                }
                this.gvFieldList.FocusedRowHandle = this.gvFieldList.RowCount - 1;
                if (this.cbe_geotypeOfEnvelope.Properties.Items.Count > 0)
                {
                    this.cbe_geotypeOfEnvelope.SelectedIndex = 0;
                }
                this.gc_Envelope.Enabled = false;
                this.bActiveInfo = false;
                this.bActiveSubtype = false;

                this.cbe_defsubtype.GotFocus += new System.EventHandler(this.cbe_defsubtype_GotFocus);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            switch (this.xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    {
                        this.bActiveInfo = true;
                        return;
                    }
                case 1:
                    {
                        this.bActiveIndex = true;
                        return;
                    }
                case 2:
                    {
                        this.bActiveSubtype = true;
                        return;
                    }
                case 3:
                    {
                        this.bActiveEnvelope = true;
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        private void FCPropertyForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            //if (this.fc != null)
            //{
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(this.fc);
            //    this.fc = null;
            //}
            if (this.fds != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.fds);
                this.fds = null;
            }
        }
        private void te_fcName_TextChanged(object sender, System.EventArgs e)
        {
            this.fcname = this.te_fcName.Text;
            this.te_fcAlias.Text = this.fcname;
        }
        private void te_fcAlias_TextChanged(object sender, System.EventArgs e)
        {
            this.fcalias = this.te_fcAlias.Text;
            this.bActiveInfo = true;
        }
        private void gvFieldList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string value = this.gvFieldList.GetRowCellValue(e.RowHandle, "FieldName").ToString();
            TextFilter textFilter = new TextFilter(TextType.FdeField);
            if (!textFilter.IsMarch(value))
            {
                XtraMessageBox.Show("名称存在无效字符");
                this.sbtn_OK.Enabled = false;
                return;
            }
            this.sbtn_OK.Enabled = true;
            string text = this.gvFieldList.GetRowCellValue(e.RowHandle, "FieldType").ToString();
            if (!string.IsNullOrEmpty(value) && text.Equals(""))
            {
                System.Guid guid = (System.Guid)this.gvFieldList.GetRowCellValue(e.RowHandle, "FieldGUID");
                this.gvFieldList.SetRowCellValue(e.RowHandle, "FieldType", "Int32");
                System.Data.DataTable propertyDataTable = this.GetPropertyDataTable("Int32");
                this.PropHash.Add(guid, propertyDataTable);
                this.gridControl2.DataSource = propertyDataTable;
            }
            string text2 = this.gvFieldList.GetFocusedRowCellValue("FieldName").ToString();
            if (!this.canEdit || !this.hasDelFieldCap)
            {
                this.sbtn_Del.Enabled = false;
                return;
            }
            if (!text2.Equals("Geometry") && !text2.Equals("Name") && !text2.Equals(""))
            {
                this.sbtn_Del.Enabled = true;
                return;
            }
            this.sbtn_Del.Enabled = false;
        }
        private void gvFieldList_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                if (this.gvFieldList.FocusedColumn.VisibleIndex == 0)
                {
                    if (e.Value.ToString().ToLower().Equals("name"))
                    {
                        e.ErrorText = "不能创建Name字段";
                        e.Valid = false;
                    }
                    else
                    {
                        for (int i = 0; i < this.gvFieldList.RowCount; i++)
                        {
                            if (this.gvFieldList.FocusedRowHandle != i && !e.Value.Equals("") && e.Value.ToString().Equals(this.gvFieldList.GetRowCellValue(i, "FieldName").ToString()))
                            {
                                e.ErrorText = "名称冲突";
                                e.Valid = false;
                            }
                        }
                    }
                }
                else
                {
                    int arg_D6_0 = this.gvFieldList.FocusedColumn.VisibleIndex;
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void gvFieldList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            bool flag = (bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew");
            foreach (GridColumn gridColumn in this.gvFieldList.Columns)
            {
                gridColumn.OptionsColumn.AllowEdit = this.canEdit && flag;
            }
            string text = this.gvFieldList.GetFocusedRowCellValue("FieldName").ToString();
            if (this.canEdit && this.hasDelFieldCap)
            {
                if (!text.Equals("Geometry") && !text.Equals("Name") && !text.Equals(""))
                {
                    this.sbtn_Del.Enabled = true;
                }
                else
                {
                    this.sbtn_Del.Enabled = false;
                }
            }
            else
            {
                this.sbtn_Del.Enabled = false;
            }
            System.Guid guid = (System.Guid)this.gvFieldList.GetRowCellValue(e.FocusedRowHandle, "FieldGUID");
            this.gvFieldList.GetRowCellValue(e.FocusedRowHandle, "FieldType").ToString();
            if (this.PropHash.Contains(guid))
            {
                System.Data.DataTable dataTable = this.PropHash[guid] as System.Data.DataTable;
                this.PropHash[guid] = dataTable;
                this.gridControl2.DataSource = dataTable;
                this.bActiveInfo = true;
                return;
            }
            this.gridControl2.DataSource = null;
        }
        private void gvFieldList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
        }
        private void cbe_fieldType_SelectedValueChanged(object sender, System.EventArgs e)
        {

        }
        private void sbtn_Prev_Click(object sender, System.EventArgs e)
        {
            try
            {
                int focusedRowHandle = this.gvFieldList.FocusedRowHandle;
                if (focusedRowHandle != 0)
                {
                    System.Data.DataRow dataRow = this.gvFieldList.GetDataRow(focusedRowHandle);
                    System.Data.DataRow dataRow2 = this.gvFieldList.GetDataRow(focusedRowHandle - 1);
                    if (dataRow != null && dataRow2 != null)
                    {
                        string text = dataRow["FieldName"].ToString();
                        string value = dataRow2["FieldName"].ToString();
                        string text2 = dataRow["FieldType"].ToString();
                        string value2 = dataRow2["FieldType"].ToString();
                        System.Guid guid = (System.Guid)dataRow["FieldGUID"];
                        System.Guid guid2 = (System.Guid)dataRow2["FieldGUID"];
                        bool flag = (bool)dataRow["FieldIsNew"];
                        bool flag2 = (bool)dataRow2["FieldIsNew"];
                        if (!text.Equals("") || !text2.Equals(""))
                        {
                            dataRow["FieldName"] = value;
                            dataRow2["FieldName"] = text;
                            dataRow["FieldType"] = value2;
                            dataRow2["FieldType"] = text2;
                            dataRow["FieldGUID"] = guid2;
                            dataRow2["FieldGUID"] = guid;
                            dataRow["FieldIsNew"] = flag2;
                            dataRow2["FieldIsNew"] = flag;
                            this.gridControl1.Refresh();
                            this.gridControl1.RefreshDataSource();
                            this.gvFieldList.RefreshData();
                            this.gvFieldList.MovePrev();
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void sbtn_Next_Click(object sender, System.EventArgs e)
        {
            try
            {
                int focusedRowHandle = this.gvFieldList.FocusedRowHandle;
                System.Data.DataRow dataRow = this.gvFieldList.GetDataRow(focusedRowHandle);
                System.Data.DataRow dataRow2 = this.gvFieldList.GetDataRow(focusedRowHandle + 1);
                if (dataRow != null && dataRow2 != null)
                {
                    string text = dataRow["FieldName"].ToString();
                    string text2 = dataRow2["FieldName"].ToString();
                    string text3 = dataRow["FieldType"].ToString();
                    string text4 = dataRow2["FieldType"].ToString();
                    System.Guid guid = (System.Guid)dataRow["FieldGUID"];
                    System.Guid guid2 = (System.Guid)dataRow2["FieldGUID"];
                    bool flag = (bool)dataRow["FieldIsNew"];
                    bool flag2 = (bool)dataRow2["FieldIsNew"];
                    if (!text.Equals("") || !text3.Equals(""))
                    {
                        if (!text2.Equals("") || !text4.Equals(""))
                        {
                            dataRow["FieldName"] = text2;
                            dataRow2["FieldName"] = text;
                            dataRow["FieldType"] = text4;
                            dataRow2["FieldType"] = text3;
                            dataRow["FieldGUID"] = guid2;
                            dataRow2["FieldGUID"] = guid;
                            dataRow["FieldIsNew"] = flag2;
                            dataRow2["FieldIsNew"] = flag;
                            this.gridControl1.Refresh();
                            this.gridControl1.RefreshDataSource();
                            this.gvFieldList.RefreshData();
                            this.gvFieldList.MoveNext();
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void sbtn_Del_Click(object sender, System.EventArgs e)
        {
            bool flag = (bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew");
            System.Guid guid = (System.Guid)this.gvFieldList.GetFocusedRowCellValue("FieldGUID");
            string item = this.gvFieldList.GetFocusedRowCellValue("FieldName").ToString();
            if (!flag)
            {
                this.DelList.Add(item);
                this.bActiveInfo = true;
            }
            if (this.PropHash.Contains(guid))
            {
                this.PropHash.Remove(guid);
                this.gvFieldList.DeleteRow(this.gvFieldList.FocusedRowHandle);
                this.gvFieldList.Focus();
                this.bActiveInfo = true;
            }
        }
        private void gridControl2_Leave(object sender, System.EventArgs e)
        {
            System.Data.DataTable value = this.gridControl2.DataSource as System.Data.DataTable;
            System.Guid guid = (System.Guid)this.gvFieldList.GetRowCellValue(this.gvFieldList.FocusedRowHandle, "FieldGUID");
            if (this.PropHash.Contains(guid))
            {
                this.PropHash[guid] = value;
                this.bActiveInfo = true;
            }
        }
        private void gridControl2_DataSourceChanged(object sender, System.EventArgs e)
        {
            foreach (GridColumn gridColumn in this.gvFieldProperty.Columns)
            {
                gridColumn.OptionsColumn.AllowEdit = false;
            }
        }
        private void gvFieldProperty_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            this.gvFieldProperty.Columns[0].OptionsColumn.AllowEdit = false;
            GridColumn gridColumn = this.gvFieldProperty.Columns[1];
            if (this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname) == null)
            {
                gridColumn.OptionsColumn.AllowEdit = false;
                return;
            }
            else
            {
                if (this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname).ToString() == "空")
                {
                    gridColumn.OptionsColumn.AllowEdit = false;
                    return;
                }
                if (this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname).ToString() == "几何类型")
                {
                    if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                    {
                        gridColumn.OptionsColumn.AllowEdit = true;
                        gridColumn.ColumnEdit = this.cbe_GeoType;
                        return;
                    }
                    gridColumn.OptionsColumn.AllowEdit = false;
                    gridColumn.ColumnEdit = null;
                    return;
                }
                else
                {
                    if (this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname).ToString() == "HasZ")
                    {
                        if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                        {
                            bool flag = false;
                            for (int i = 0; i < this.gvFieldProperty.RowCount; i++)
                            {
                                string text = this.gvFieldProperty.GetRowCellValue(i, this.propvalue).ToString();
                                if (text.Equals("模型") || text.Equals("三角网格"))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                gridColumn.OptionsColumn.AllowEdit = false;
                            }
                            else
                            {
                                gridColumn.OptionsColumn.AllowEdit = true;
                            }
                            gridColumn.ColumnEdit = this.cbe_NotNull;
                            return;
                        }
                        gridColumn.OptionsColumn.AllowEdit = false;
                        gridColumn.ColumnEdit = null;
                        return;
                    }
                    else
                    {
                        if (this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname).ToString() == "HasM")
                        {
                            if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                            {
                                gridColumn.OptionsColumn.AllowEdit = true;
                                gridColumn.ColumnEdit = this.cbe_NotNull;
                                return;
                            }
                            gridColumn.OptionsColumn.AllowEdit = false;
                            gridColumn.ColumnEdit = null;
                            return;
                        }
                        else
                        {
                            if (this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname).ToString() == "HasId")
                            {
                                if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                                {
                                    gridColumn.OptionsColumn.AllowEdit = true;
                                    gridColumn.ColumnEdit = this.cbe_NotNull;
                                    return;
                                }
                                gridColumn.OptionsColumn.AllowEdit = false;
                                gridColumn.ColumnEdit = null;
                                return;
                            }
                            else
                            {
                                if (!(this.gvFieldProperty.GetRowCellValue(e.FocusedRowHandle, this.propname).ToString() == "域"))
                                {
                                    gridColumn.OptionsColumn.AllowEdit = true;
                                    gridColumn.ColumnEdit = null;
                                    return;
                                }
                                this.cbe_Domain.Items.Clear();
                                this.cbe_Domain.Items.Add("");
                                string text2 = this.gvFieldList.GetRowCellValue(this.gvFieldList.FocusedRowHandle, "FieldName").ToString();
                                if (text2.Equals("Name") || text2.Equals("Geometry"))
                                {
                                    gridColumn.OptionsColumn.AllowEdit = false;
                                    gridColumn.ColumnEdit = null;
                                    return;
                                }
                                string type = this.gvFieldList.GetRowCellValue(this.gvFieldList.FocusedRowHandle, "FieldType").ToString();
                                gviFieldType fieldTypeByString = this.GetFieldTypeByString(type);
                                string[] domainNames = this.fds.DataSource.GetDomainNames();
                                for (int j = 0; j < domainNames.Length; j++)
                                {
                                    string domain = domainNames[j];
                                    IDomain domainByName = this.fds.DataSource.GetDomainByName(domain);
                                    if (domainByName.FieldType == fieldTypeByString)
                                    {
                                        this.cbe_Domain.Items.Add(domainByName.Name);
                                    }
                                }
                                gridColumn.OptionsColumn.AllowEdit = true;
                                gridColumn.ColumnEdit = this.cbe_Domain;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void gvFieldProperty_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "字符串长度")
            {
                try
                {
                    int.Parse(e.Value.ToString());
                }
                catch (System.Exception)
                {
                    XtraMessageBox.Show("输入类型错误或超出规定范围");
                    this.gvFieldProperty.SetRowCellValue(e.RowHandle, e.Column, 255);
                    this.gvFieldProperty.FocusedRowHandle = e.RowHandle;
                }
            }
        }
        private void gvFieldProperty_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.VisibleIndex != 1)
            {
                e.Column.OptionsColumn.AllowEdit = false;
                return;
            }
            if (this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "可为空")
            {
                e.Column.OptionsColumn.AllowEdit = false;
                return;
            }
            if (this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "空间列类型")
            {
                if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                {
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.ColumnEdit = this.cbe_GeoType;
                    return;
                }
                e.Column.OptionsColumn.AllowEdit = false;
                e.Column.ColumnEdit = null;
                return;
            }
            else
            {
                if (this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "HasZ")
                {
                    if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                    {
                        bool flag = false;
                        for (int i = 0; i < this.gvFieldProperty.RowCount; i++)
                        {
                            string text = this.gvFieldProperty.GetRowCellValue(i, this.propvalue).ToString();
                            if (text.Equals("模型") || text.Equals("三角网格"))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            e.Column.OptionsColumn.AllowEdit = false;
                        }
                        else
                        {
                            e.Column.OptionsColumn.AllowEdit = true;
                        }
                        e.Column.ColumnEdit = this.cbe_NotNull;
                        return;
                    }
                    e.Column.OptionsColumn.AllowEdit = false;
                    e.Column.ColumnEdit = null;
                    return;
                }
                else
                {
                    if (this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "HasM")
                    {
                        if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                        {
                            e.Column.OptionsColumn.AllowEdit = true;
                            e.Column.ColumnEdit = this.cbe_NotNull;
                            return;
                        }
                        e.Column.OptionsColumn.AllowEdit = false;
                        e.Column.ColumnEdit = null;
                        return;
                    }
                    else
                    {
                        if (this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "HasId")
                        {
                            if ((bool)this.gvFieldList.GetFocusedRowCellValue("FieldIsNew"))
                            {
                                e.Column.OptionsColumn.AllowEdit = true;
                                e.Column.ColumnEdit = this.cbe_NotNull;
                                return;
                            }
                            e.Column.OptionsColumn.AllowEdit = false;
                            e.Column.ColumnEdit = null;
                            return;
                        }
                        else
                        {
                            if (!(this.gvFieldProperty.GetRowCellValue(e.RowHandle, this.propname).ToString() == "域"))
                            {
                                e.Column.OptionsColumn.AllowEdit = true;
                                e.Column.ColumnEdit = null;
                                return;
                            }
                            this.cbe_Domain.Items.Clear();
                            this.cbe_Domain.Items.Add("");
                            string text2 = this.gvFieldList.GetRowCellValue(this.gvFieldList.FocusedRowHandle, "FieldName").ToString();
                            if (text2.Equals("Name") || text2.Equals("Geometry"))
                            {
                                e.Column.OptionsColumn.AllowEdit = false;
                                e.Column.ColumnEdit = null;
                                return;
                            }
                            string type = this.gvFieldList.GetRowCellValue(this.gvFieldList.FocusedRowHandle, "FieldType").ToString();
                            gviFieldType fieldTypeByString = this.GetFieldTypeByString(type);
                            string[] domainNames = this.fds.DataSource.GetDomainNames();
                            for (int j = 0; j < domainNames.Length; j++)
                            {
                                string domain = domainNames[j];
                                IDomain domainByName = this.fds.DataSource.GetDomainByName(domain);
                                if (domainByName.FieldType == fieldTypeByString)
                                {
                                    this.cbe_Domain.Items.Add(domainByName.Name);
                                }
                            }
                            e.Column.OptionsColumn.AllowEdit = true;
                            e.Column.ColumnEdit = this.cbe_Domain;
                            return;
                        }
                    }
                }
            }
        }
        private void sbtn_CreateDBIndex_Click(object sender, System.EventArgs e)
        {
            try
            {
                CreateDBIndexForm createDBIndexForm = new CreateDBIndexForm(this.fcFields, this.lbc_DBIndex);
                if (createDBIndexForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string indexName = createDBIndexForm.IndexName;
                    this.DbIndex = new DbIndexInfoClass();
                    this.DbIndex.Name = indexName;
                    foreach (string field in createDBIndexForm.SelectFields)
                    {
                        this.DbIndex.AppendFieldDefine(field, true);
                    }
                    DbIndexBuffer dbIndexBuffer = new DbIndexBuffer();
                    dbIndexBuffer.data = this.DbIndex;
                    dbIndexBuffer.et = IndexEditType.Create;
                    dbIndexBuffer.bDatabase = false;
                    this.DbIndexHash.Add(indexName, dbIndexBuffer);
                    this.lbc_DBIndex.Items.Add(indexName);
                    this.lbc_DBIndex.SelectedItem = indexName;
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void sbtn_DeleteDBIndex_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.lbc_DBIndex.SelectedItem != null)
                {
                    string text = this.lbc_DBIndex.SelectedItem.ToString();
                    if (this.DbIndexHash.ContainsKey(text))
                    {
                        this.dbbuffer = this.DbIndexHash[text];
                        if (this.dbbuffer.bDatabase)
                        {
                            this.dbbuffer.et = IndexEditType.Delete;
                            this.DbIndexHash[text] = this.dbbuffer;
                        }
                        else
                        {
                            this.DbIndexHash.Remove(text);
                        }
                        this.lbc_DBIndex.Items.Remove(text);
                    }
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void sbtn_CalculateGridIndex_Click(object sender, System.EventArgs e)
        {
            try
            {
                string text = this.cbe_GridGeoColumn.SelectedItem.ToString();
                if (!text.Equals(""))
                {
                    this.GridIndex = this.fc.CalculateDefaultGridIndex(text);
                    if (!this.GridIndexHash.ContainsKey(text))
                    {
                        GridIndexBuffer gridIndexBuffer = new GridIndexBuffer();
                        gridIndexBuffer.bDatabase = false;
                        gridIndexBuffer.et = IndexEditType.Create;
                        gridIndexBuffer.data = this.GridIndex;
                        this.GridIndexHash.Add(text, gridIndexBuffer);
                    }
                    this.te_L1.Text = this.GridIndex.L1.ToString();
                    this.te_L2.Text = this.GridIndex.L2.ToString();
                    this.te_L3.Text = this.GridIndex.L3.ToString();
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void sbtn_DeleteGridIndex_Click(object sender, System.EventArgs e)
        {
            string text = this.cbe_GridGeoColumn.SelectedItem.ToString();
            if (!text.Equals(""))
            {
                if (this.GridIndexHash.ContainsKey(text))
                {
                    this.gibuffer = this.GridIndexHash[text];
                    if (this.gibuffer.bDatabase)
                    {
                        this.gibuffer.et = IndexEditType.Delete;
                        this.te_L1.Text = "";
                        this.te_L2.Text = "";
                        this.te_L3.Text = "";
                        return;
                    }
                    this.GridIndexHash.Remove(text);
                    this.te_L1.Text = "";
                    this.te_L2.Text = "";
                    this.te_L3.Text = "";
                    return;
                }
                else
                {
                    this.te_L1.Text = "";
                    this.te_L2.Text = "";
                    this.te_L3.Text = "";
                }
            }
        }
        private void te_L1_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.te_L1.Text.Equals(""))
            {
                string key = this.cbe_GridGeoColumn.SelectedItem.ToString();
                if (this.GridIndexHash.ContainsKey(key))
                {
                    GridIndexBuffer gridIndexBuffer = this.GridIndexHash[key];
                    gridIndexBuffer.data.L1 = double.Parse(this.te_L1.Text);
                    if (gridIndexBuffer.bDatabase)
                    {
                        gridIndexBuffer.et = IndexEditType.Edit;
                        return;
                    }
                    gridIndexBuffer.et = IndexEditType.Create;
                }
            }
        }
        private void te_L2_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.te_L2.Text.Equals(""))
            {
                string key = this.cbe_GridGeoColumn.SelectedItem.ToString();
                if (this.GridIndexHash.ContainsKey(key))
                {
                    GridIndexBuffer gridIndexBuffer = this.GridIndexHash[key];
                    gridIndexBuffer.data.L2 = double.Parse(this.te_L2.Text);
                    if (gridIndexBuffer.bDatabase)
                    {
                        gridIndexBuffer.et = IndexEditType.Edit;
                        return;
                    }
                    gridIndexBuffer.et = IndexEditType.Create;
                }
            }
        }
        private void te_L3_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.te_L3.Text.Equals(""))
            {
                string key = this.cbe_GridGeoColumn.SelectedItem.ToString();
                if (this.GridIndexHash.ContainsKey(key))
                {
                    GridIndexBuffer gridIndexBuffer = this.GridIndexHash[key];
                    gridIndexBuffer.data.L3 = double.Parse(this.te_L3.Text);
                    if (gridIndexBuffer.bDatabase)
                    {
                        gridIndexBuffer.et = IndexEditType.Edit;
                        return;
                    }
                    gridIndexBuffer.et = IndexEditType.Create;
                }
            }
        }
        private void te_RenderSize_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.te_RenderSize.Text.Equals(""))
            {
                string key = this.cbe_RenderGeoColumn.SelectedItem.ToString();
                if (this.RenderIndexHash.ContainsKey(key))
                {
                    RenderIndexBuffer renderIndexBuffer = this.RenderIndexHash[key];
                    if (renderIndexBuffer.bDatabase)
                    {
                        renderIndexBuffer.et = IndexEditType.Edit;
                    }
                    else
                    {
                        renderIndexBuffer.et = IndexEditType.Create;
                    }
                    double l = 0.0;
                    if (double.TryParse(this.te_RenderSize.Text, out l))
                    {
                        renderIndexBuffer.data.L1 = l;
                    }
                }
            }
        }
        private void cbe_RenderIndex_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                string text = this.cbe_RenderGeoColumn.SelectedItem.ToString();
                if (this.RenderIndexHash.ContainsKey(text))
                {
                    this.te_RenderSize.Text = this.RenderIndexHash[text].data.L1.ToString();
                }
                else
                {
                    this.RenderIndex = new RenderIndexInfoClass();
                    this.RenderIndex.GeoColumnName = text;
                    RenderIndexBuffer renderIndexBuffer = new RenderIndexBuffer();
                    renderIndexBuffer.data = this.RenderIndex;
                    renderIndexBuffer.et = IndexEditType.Create;
                    renderIndexBuffer.bDatabase = false;
                    this.RenderIndexHash.Add(text, renderIndexBuffer);
                    this.te_RenderSize.Text = "";
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void cbe_GridIndex_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                string text = this.cbe_GridGeoColumn.SelectedItem.ToString();
                if (this.GridIndexHash.ContainsKey(text))
                {
                    this.te_L1.Text = this.GridIndexHash[text].data.L1.ToString();
                    this.te_L2.Text = this.GridIndexHash[text].data.L2.ToString();
                    this.te_L3.Text = this.GridIndexHash[text].data.L3.ToString();
                }
                else
                {
                    this.GridIndex = new GridIndexInfoClass();
                    this.GridIndex.GeoColumnName = text;
                    GridIndexBuffer gridIndexBuffer = new GridIndexBuffer();
                    gridIndexBuffer.data = this.GridIndex;
                    gridIndexBuffer.et = IndexEditType.Create;
                    gridIndexBuffer.bDatabase = false;
                    this.GridIndexHash.Add(text, gridIndexBuffer);
                    this.te_L1.Text = "";
                    this.te_L2.Text = "";
                    this.te_L3.Text = "";
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void lbc_DBIndex_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.lbc_DBIndex.SelectedItem != null)
            {
                string text = this.lbc_DBIndex.SelectedItem.ToString();
                if (text.Equals(""))
                {
                    return;
                }
                this.DbIndex = this.GetDBIndex(text);
                if (this.DbIndex != null)
                {
                    this.lbc_IndexFields.Items.Clear();
                    for (int i = 0; i < this.DbIndex.FieldCount; i++)
                    {
                        this.lbc_IndexFields.Items.Add(this.DbIndex.GetFieldName(i));
                    }
                    return;
                }
            }
            else
            {
                this.lbc_IndexFields.Items.Clear();
            }
        }
        private IDbIndexInfo GetDBIndex(string name)
        {
            if (this.DbIndexHash.ContainsKey(name))
            {
                return this.DbIndexHash[name].data;
            }
            return null;
        }
        private void sbtn_Calculate_Click(object sender, System.EventArgs e)
        {
            try
            {
                string text = this.cbe_RenderGeoColumn.SelectedItem.ToString();
                if (!text.Equals(""))
                {
                    this.RenderIndex = this.fc.CalculateDefaultRenderIndex(text);
                    if (!this.RenderIndexHash.ContainsKey(text))
                    {
                        RenderIndexBuffer renderIndexBuffer = new RenderIndexBuffer();
                        renderIndexBuffer.bDatabase = false;
                        renderIndexBuffer.et = IndexEditType.Create;
                        renderIndexBuffer.data = this.RenderIndex;
                        this.RenderIndexHash.Add(text, renderIndexBuffer);
                    }
                    this.te_RenderSize.Text = this.RenderIndex.L1.ToString();
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void sbtn_Add_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.lbc_FieldsAvailable.SelectedItems.Count; i++)
            {
                string text = this.lbc_FieldsAvailable.SelectedItems[i].ToString();
                if (!this.lbc_FieldsSelected.Items.Contains(text))
                {
                    this.lbc_FieldsSelected.Items.Add(text);
                }
            }
        }
        private void sbtn_Remove_Click(object sender, System.EventArgs e)
        {
            int count = this.lbc_FieldsSelected.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                string text = this.lbc_FieldsSelected.SelectedItems[0].ToString();
                this.lbc_FieldsSelected.Items.Remove(text);
            }
        }
        private void sbtn_DeleteRenderIndex_Click(object sender, System.EventArgs e)
        {
            string text = this.cbe_RenderGeoColumn.SelectedItem.ToString();
            if (!text.Equals(""))
            {
                if (this.RenderIndexHash.ContainsKey(text))
                {
                    RenderIndexBuffer renderIndexBuffer = this.RenderIndexHash[text];
                    if (renderIndexBuffer.bDatabase)
                    {
                        renderIndexBuffer.et = IndexEditType.Delete;
                    }
                    else
                    {
                        this.RenderIndexHash.Remove(text);
                    }
                    this.te_RenderSize.Text = "";
                    return;
                }
                this.te_RenderSize.Text = "";
            }
        }
        private void cbe_SubtypeField_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cbe_SubtypeField.Text.Equals(this.strNone))
            {
                this.gridControl3.Enabled = false;
                this.gridControl4.Enabled = false;
                if (this.fc.HasSubTypes)
                {
                    if (XtraMessageBox.Show("确定要删除吗", "", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.gridControl3.DataSource = null;
                        this.gridControl3.Update();
                        this.gridControl4.DataSource = null;
                        this.gridControl4.Tag = null;
                        this.gridControl4.Update();
                        this.fc.SubTypeFieldName = "";
                        this.cbe_defsubtype.Properties.Items.Clear();
                        this.bActiveSubtype = true;
                    }
                    else
                    {
                        this.cbe_SubtypeField.SelectedItem = this.fc.SubTypeFieldName;
                    }
                }
                this.cbe_defsubtype.Text = "";
                return;
            }
            this.gridControl3.Enabled = true;
            this.gridControl4.Enabled = true;
            this.FillSubtypeItem();
        }
        private void cbe_defsubtype_GotFocus(object sender, System.EventArgs e)
        {
            this.cbe_defsubtype.Properties.Items.Clear();
            for (int i = 0; i < this.gv_SubtypeItem.RowCount; i++)
            {
                int num = 0;
                if (int.TryParse(this.gv_SubtypeItem.GetRowCellValue(i, "Colvalue").ToString(), out num))
                {
                    string text = this.gv_SubtypeItem.GetRowCellValue(i, "Colcode").ToString();
                    if (!text.Equals(""))
                    {
                        System.Guid guid = (System.Guid)this.gv_SubtypeItem.GetRowCellValue(i, "Colguid");
                        if (this.subtypeHash.ContainsKey(guid))
                        {
                            this.cbe_defsubtype.Properties.Items.Add(text);
                        }
                    }
                }
            }
        }
        private void gv_SubtypeItem_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (this.canEdit)
                {
                    e.Column.OptionsColumn.AllowEdit = true;
                }
                else
                {
                    e.Column.OptionsColumn.AllowEdit = false;
                }
                System.Guid guid = (System.Guid)this.gv_SubtypeItem.GetRowCellValue(e.RowHandle, "Colguid");
                if (this.subtypeHash.ContainsKey(guid))
                {
                    this.subtype = (this.subtypeHash[guid] as SubTypeInfo);
                }
                else
                {
                    this.subtype = new SubTypeInfoClass();
                    this.subtypeHash.Add(guid, this.subtype);
                }
                this.gridControl4.DataSource = this.FillSubtypeProp(this.subtype);
                this.gridControl4.Tag = guid;
            }
            catch (System.Exception)
            {
            }
        }
        private void gv_SubtypeItem_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Delete)
                {
                    System.Guid guid = (System.Guid)this.gv_SubtypeItem.GetFocusedRowCellValue("Colguid");
                    if (this.subtypeHash.ContainsKey(guid))
                    {
                        this.subtype = (this.subtypeHash[guid] as SubTypeInfo);
                        this.delSubtypes.Add(this.subtype.Code);
                    }
                    this.gv_SubtypeItem.DeleteSelectedRows();
                }
            }
            catch
            {
            }
        }
        private void gv_SubtypeItem_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                if (this.gv_SubtypeItem.FocusedColumn.VisibleIndex == 0)
                {
                    TextFilter textFilter = new TextFilter(TextType.FdeField);
                    if (!textFilter.IsMarch(e.Value.ToString().Trim()))
                    {
                        e.ErrorText = "名称存在无效字符";
                        e.Valid = false;
                    }
                    for (int i = 0; i < this.gv_SubtypeItem.RowCount; i++)
                    {
                        if (this.gv_SubtypeItem.FocusedRowHandle != i && !e.Value.ToString().Trim().Equals("") && e.Value.ToString().Trim().Equals(this.gv_SubtypeItem.GetRowCellValue(i, "Colvalue").ToString().Trim()))
                        {
                            e.ErrorText = "编码冲突";
                            e.Valid = false;
                            break;
                        }
                    }
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void gv_SubtypeItem_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value.ToString() == "")
            {
                return;
            }
            if (e.Column.Name.Equals("Colvalue"))
            {
                this.gv_SubtypeItem.GetDataRow(e.RowHandle)[e.Column.FieldName] = e.Value.ToString().Trim();
            }
            int selectedIndex = this.cbe_defsubtype.SelectedIndex;
            if (e.Column.VisibleIndex == 1 && e.RowHandle == selectedIndex)
            {
                this.cbe_defsubtype.Properties.Items.Clear();
                for (int i = 0; i < this.gv_SubtypeItem.RowCount; i++)
                {
                    string a = this.gv_SubtypeItem.GetRowCellValue(i, "Colvalue").ToString();
                    string text = this.gv_SubtypeItem.GetRowCellValue(i, "Colcode").ToString();
                    if (!(a == "") || !(text == ""))
                    {
                        this.cbe_defsubtype.Properties.Items.Add(text);
                    }
                }
                this.cbe_defsubtype.SelectedIndex = selectedIndex;
            }
        }
        private void gv_SubtypeProp_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals(this.Coldomain))
            {
                this.gv_SubtypeProp.SetRowCellValue(e.RowHandle, e.Column, e.Value);
            }
        }
        private void gv_SubtypeProp_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Value.ToString().Trim()))
                {
                    string type = this.gv_SubtypeProp.GetFocusedRowCellValue(this.Colft).ToString();
                    bool flag = false;
                    this.GetFieldtypeValue(type, e.Value.ToString(), out flag);
                    if (!flag)
                    {
                        e.Valid = false;
                        e.ErrorText = "超出格式范围或输入格式错误";
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void gv_SubtypeProp_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.Equals(this.Coldomain))
                {
                    this.cbe_subtypedom = new RepositoryItemComboBox();
                    this.cbe_subtypedom.Name = "domain";
                    this.cbe_subtypedom.AllowDropDownWhenReadOnly = DefaultBoolean.True;
                    this.cbe_subtypedom.TextEditStyle = TextEditStyles.DisableTextEditor;
                    this.cbe_subtypedom.CycleOnDblClick = true;
                    string name = this.gv_SubtypeProp.GetRowCellValue(e.RowHandle, this.Colfn).ToString();
                    IFieldInfo fieldInfo = this.fc.GetFields().Get(this.fc.GetFields().IndexOf(name));
                    string[] domainNames = this.fc.DataSource.GetDomainNames();
                    string[] array = domainNames;
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i];
                        IDomain domainByName = this.fc.DataSource.GetDomainByName(text);
                        if (fieldInfo.FieldType == domainByName.FieldType)
                        {
                            this.cbe_subtypedom.Items.Add(text);
                        }
                    }
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.ColumnEdit = this.cbe_subtypedom;
                }
                else
                {
                    if (e.Column.FieldName.Equals(this.Coldefvalue))
                    {
                        e.Column.OptionsColumn.AllowEdit = true;
                        if (this.gv_SubtypeProp.GetRowCellValue(e.RowHandle, this.Colft).ToString().Equals("Date"))
                        {
                            e.Column.ColumnEdit = this.de_Datetime;
                        }
                        else
                        {
                            e.Column.ColumnEdit = null;
                        }
                    }
                    else
                    {
                        e.Column.OptionsColumn.AllowEdit = false;
                    }
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void gridControl4_DataSourceChanged(object sender, System.EventArgs e)
        {
            foreach (GridColumn gridColumn in this.gv_SubtypeProp.Columns)
            {
                gridColumn.OptionsColumn.AllowEdit = false;
            }
        }
        private void gridControl4_Leave(object sender, System.EventArgs e)
        {
            try
            {
                this.gv_SubtypeProp.PopulateColumns();
                System.Guid guid = (System.Guid)this.gridControl4.Tag;
                if (this.subtypeHash.Contains(guid))
                {
                    this.subtype = new SubTypeInfoClass();
                    for (int i = 0; i < this.gv_SubtypeProp.RowCount; i++)
                    {
                        FieldDomainInfo fieldDomainInfo = new FieldDomainInfoClass();
                        string fieldName = this.gv_SubtypeProp.GetRowCellValue(i, this.Colfn).ToString();
                        fieldDomainInfo.FieldName = fieldName;
                        bool flag = false;
                        fieldDomainInfo.DefaultValue = this.GetFieldtypeValue(this.gv_SubtypeProp.GetRowCellValue(i, this.Colft).ToString(), this.gv_SubtypeProp.GetRowCellValue(i, this.Coldefvalue).ToString(), out flag);
                        string domain = this.gv_SubtypeProp.GetRowCellValue(i, this.Coldomain).ToString();
                        fieldDomainInfo.Domain = this.fc.DataSource.GetDomainByName(domain);
                        this.subtype.AddFieldDomainInfo(fieldDomainInfo);
                    }
                    this.subtypeHash[guid] = this.subtype;
                    this.bActiveSubtype = true;
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void sbtn_DomainView_Click(object sender, System.EventArgs e)
        {
            DomainViewForm domainViewForm = new DomainViewForm(this.fds.DataSource);
            domainViewForm.ShowDialog();
        }
        private void sbtn_CalculateEnvelope_Click(object sender, System.EventArgs e)
        {
            try
            {
                string text = this.cbe_geotypeOfEnvelope.SelectedItem.ToString();
                this.fc.UpdateExtent(text);
                IFieldInfo fieldInfo = this.GetFieldInfo(text);
                IEnvelope envelope = fieldInfo.GeometryDef.Envelope;
                if (envelope != null)
                {
                    this.te_MinX.Text = envelope.MinX.ToString();
                    this.te_MaxX.Text = envelope.MaxX.ToString();
                    this.te_MinY.Text = envelope.MinY.ToString();
                    this.te_MaxY.Text = envelope.MaxY.ToString();
                    this.te_MinZ.Text = envelope.MinZ.ToString();
                    this.te_MaxZ.Text = envelope.MaxZ.ToString();
                }
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
            }
        }
        private void cbe_geotypeOfEnvelope_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.te_MinX.Text = "";
                this.te_MaxX.Text = "";
                this.te_MinY.Text = "";
                this.te_MaxY.Text = "";
                this.te_MinZ.Text = "";
                this.te_MaxZ.Text = "";
                string name = this.cbe_geotypeOfEnvelope.SelectedItem.ToString();
                IFieldInfo fieldInfo = this.GetFieldInfo(name);
                IEnvelope envelope = fieldInfo.GeometryDef.Envelope;
                if (envelope != null)
                {
                    this.te_MinX.Text = envelope.MinX.ToString();
                    this.te_MaxX.Text = envelope.MaxX.ToString();
                    this.te_MinY.Text = envelope.MinY.ToString();
                    this.te_MaxY.Text = envelope.MaxY.ToString();
                    this.te_MinZ.Text = envelope.MinZ.ToString();
                    this.te_MaxZ.Text = envelope.MaxZ.ToString();
                    if (fieldInfo != null && fieldInfo.GeometryDef != null)
                    {
                        this.te_MinZ.Visible = fieldInfo.GeometryDef.HasZ;
                        this.te_MaxZ.Visible = fieldInfo.GeometryDef.HasZ;
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
        private void sbtn_OK_Click(object sender, System.EventArgs e)
        {
            WaitDialogForm waitDialogForm = null;
            try
            {
                if (this.canEdit)
                {
                    if (this.bActiveInfo)
                    {
                        if (string.IsNullOrEmpty(this.fcalias))
                        {
                            XtraMessageBox.Show("要素类别名不能为空");
                            this.te_fcAlias.Focus();
                            base.DialogResult = System.Windows.Forms.DialogResult.None;
                            return;
                        }
                        this.fc.AliasName = this.fcalias;
                        IFieldInfo fieldInfo = null;
                        foreach (string current in this.DelList)
                        {
                            this.fc.DeleteField(current);
                        }
                        for (int i = 0; i < this.gvFieldList.RowCount; i++)
                        {
                            if (!this.gvFieldList.GetRowCellValue(i, "FieldType").ToString().Equals(""))
                            {
                                bool flag = (bool)this.gvFieldList.GetRowCellValue(i, "FieldIsNew");
                                string text = this.gvFieldList.GetRowCellValue(i, "FieldName").ToString();
                                if (!text.Equals(""))
                                {
                                    int num = this.fcFields.IndexOf(text);
                                    if (num != -1)
                                    {
                                        fieldInfo = this.fcFields.Get(this.fcFields.IndexOf(text));
                                    }
                                    else
                                    {
                                        fieldInfo = new FieldInfoClass();
                                    }
                                    fieldInfo.Name = text;
                                    fieldInfo.FieldType = this.GetFieldTypeByString(this.gvFieldList.GetRowCellValue(i, "FieldType").ToString());
                                    System.Guid guid = (System.Guid)this.gvFieldList.GetRowCellValue(i, "FieldGUID");
                                    System.Data.DataTable dataTable = this.PropHash[guid] as System.Data.DataTable;
                                    foreach (System.Data.DataRow dataRow in dataTable.Rows)
                                    {
                                        string text2 = dataRow[this.propname].ToString();
                                        string text3 = dataRow[this.propvalue].ToString();
                                        if (text2.Equals("别名"))
                                        {
                                            if (text3.Equals(""))
                                            {
                                                fieldInfo.Alias = text;
                                            }
                                            else
                                            {
                                                fieldInfo.Alias = text3;
                                            }
                                        }
                                        else
                                        {
                                            if (text2.Equals("可为空"))
                                            {
                                                fieldInfo.Nullable = text3.Equals("是");
                                            }
                                            else
                                            {
                                                if (text2.Equals("域"))
                                                {
                                                    if (!text3.Equals(""))
                                                    {
                                                        IDomain domainByName = this.fds.DataSource.GetDomainByName(text3);
                                                        fieldInfo.Domain = domainByName;
                                                    }
                                                    else
                                                    {
                                                        fieldInfo.Domain = null;
                                                    }
                                                }
                                                else
                                                {
                                                    if (text2.Equals("字符串长度"))
                                                    {
                                                        int length = fieldInfo.Length;
                                                        if (int.TryParse(text3, out length))
                                                        {
                                                            fieldInfo.Length = length;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (text2.Equals("空间列类型"))
                                                        {
                                                            fieldInfo.GeometryDef = new GeometryDefClass();
                                                            fieldInfo.GeometryDef.GeometryColumnType = this.GetGeometryTypeByString(text3);
                                                        }
                                                        else
                                                        {
                                                            if (text2.Equals("HasZ"))
                                                            {
                                                                fieldInfo.GeometryDef.HasZ = text3.Equals("是");
                                                            }
                                                            else
                                                            {
                                                                if (text2.Equals("HasM"))
                                                                {
                                                                    fieldInfo.GeometryDef.HasM = text3.Equals("是");
                                                                }
                                                                else
                                                                {
                                                                    if (text2.Equals("HasId"))
                                                                    {
                                                                        fieldInfo.GeometryDef.HasId = text3.Equals("是");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (flag)
                                    {
                                        try
                                        {
                                            this.fc.AddField(fieldInfo);
                                            goto IL_427;
                                        }
                                        catch (System.Runtime.InteropServices.COMException ex)
                                        {
                                            if (ex.ErrorCode == -2401)
                                            {
                                                this.fc.ModifyField(fieldInfo);
                                                goto IL_427;
                                            }
                                            LoggingService.Error(ex.Message + "\r\n" + ex.StackTrace);
                                            base.DialogResult = System.Windows.Forms.DialogResult.None;
                                            return;
                                        }
                                    }
                                    this.fc.ModifyField(fieldInfo);
                                }
                            }
                        IL_427: ;
                        }
                    }
                    if (this.bActiveSubtype)
                    {
                        string text4 = this.cbe_SubtypeField.Text;
                        string text5 = this.cbe_defsubtype.Text;
                        if (!text4.Equals(this.strNone))
                        {
                            foreach (int subTypeCode in this.delSubtypes)
                            {
                                this.fc.DeleteSubType(subTypeCode);
                            }
                            if (!text4.Equals(this.fc.SubTypeFieldName))
                            {
                                this.fc.SubTypeFieldName = text4;
                            }
                            for (int j = 0; j < this.gv_SubtypeItem.RowCount; j++)
                            {
                                int num2 = 0;
                                if (int.TryParse(this.gv_SubtypeItem.GetRowCellValue(j, "Colvalue").ToString().Trim(), out num2))
                                {
                                    if (j == 0)
                                    {
                                        int defaultSubTypeCode = num2;
                                        this.fc.DefaultSubTypeCode = defaultSubTypeCode;
                                    }
                                    string text6 = this.gv_SubtypeItem.GetRowCellValue(j, "Colcode").ToString();
                                    if (!text6.Equals(""))
                                    {
                                        if (text6.Equals(text5) && this.fc.DefaultSubTypeCode != num2)
                                        {
                                            this.fc.DefaultSubTypeCode = num2;
                                        }
                                        System.Guid guid2 = (System.Guid)this.gv_SubtypeItem.GetRowCellValue(j, "Colguid");
                                        if (this.subtypeHash.ContainsKey(guid2))
                                        {
                                            this.subtype = (this.subtypeHash[guid2] as SubTypeInfo);
                                        }
                                        if (this.subtype != null)
                                        {
                                            this.subtype.Code = num2;
                                            this.subtype.Name = text6;
                                            if (!this.ContainSubtypeInfo(this.subtype))
                                            {
                                                this.fc.AddSubType(this.subtype);
                                            }
                                            else
                                            {
                                                this.fc.ModifySubType(this.subtype);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.fc.HasSubTypes)
                            {
                                this.fc.SubTypeFieldName = "";
                            }
                        }
                    }
                }
                if ((this.canEdit || this.isArcSDE) && this.bActiveIndex)
                {
                    if (this.CanRebuildIndex())
                    {
                        waitDialogForm = new WaitDialogForm("重建索引,请耐心等待", "提示信息", new System.Drawing.Size(400, 50));
                        waitDialogForm.Show();
                    }
                    foreach (string current2 in this.DbIndexHash.Keys)
                    {
                        this.dbbuffer = this.DbIndexHash[current2];
                        this.DbIndex = this.dbbuffer.data;
                        switch (this.dbbuffer.et)
                        {
                            case IndexEditType.Delete:
                                {
                                    this.fc.DeleteDbIndex(this.DbIndex.Name);
                                    break;
                                }
                            case IndexEditType.Create:
                                {
                                    this.fc.AddDbIndex(this.DbIndex);
                                    this.dbbuffer.et = IndexEditType.Normal;
                                    this.dbbuffer.bDatabase = true;
                                    break;
                                }
                        }
                    }
                    foreach (string current3 in this.GridIndexHash.Keys)
                    {
                        this.gibuffer = this.GridIndexHash[current3];
                        this.GridIndex = this.gibuffer.data;
                        if (this.GridIndex.L1 != 0.0 || this.GridIndex.L2 != 0.0 || this.GridIndex.L3 != 0.0)
                        {
                            switch (this.gibuffer.et)
                            {
                                case IndexEditType.Edit:
                                    {
                                        string arg_84C_0 = this.GridIndex.GeoColumnName;
                                        this.fc.DeleteSpatialIndex(this.GridIndex.GeoColumnName);
                                        this.fc.AddSpatialIndex(this.GridIndex);
                                        break;
                                    }
                                case IndexEditType.Delete:
                                    {
                                        this.fc.DeleteSpatialIndex(this.GridIndex.GeoColumnName);
                                        break;
                                    }
                                case IndexEditType.Create:
                                    {
                                        this.fc.AddSpatialIndex(this.GridIndex);
                                        break;
                                    }
                            }
                        }
                    }
                    bool flag2 = false;
                    for (int k = 0; k < this.fcFields.Count; k++)
                    {
                        IFieldInfo fieldInfo2 = this.fcFields.Get(k);
                        if (fieldInfo2.FieldType != gviFieldType.gviFieldFID && fieldInfo2.FieldType != gviFieldType.gviFieldGeometry && fieldInfo2.FieldType != gviFieldType.gviFieldBlob)
                        {
                            if (this.lbc_FieldsSelected.Items.Contains(fieldInfo2.Name))
                            {
                                if (!fieldInfo2.RegisteredRenderIndex)
                                {
                                    fieldInfo2.RegisteredRenderIndex = true;
                                    this.fc.ModifyField(fieldInfo2);
                                    flag2 = true;
                                }
                            }
                            else
                            {
                                if (fieldInfo2.RegisteredRenderIndex)
                                {
                                    fieldInfo2.RegisteredRenderIndex = false;
                                    this.fc.ModifyField(fieldInfo2);
                                    flag2 = true;
                                }
                            }
                        }
                    }
                    foreach (string current4 in this.RenderIndexHash.Keys)
                    {
                        bool flag3 = false;
                        this.ribuffer = this.RenderIndexHash[current4];
                        this.RenderIndex = this.ribuffer.data;
                        if (this.RenderIndex.L1 != 0.0)
                        {
                            switch (this.ribuffer.et)
                            {
                                case IndexEditType.Edit:
                                    {
                                        if (this.ribuffer.bDatabase)
                                        {
                                            this.fc.DeleteRenderIndex(this.RenderIndex.GeoColumnName);
                                            this.fc.AddRenderIndex(this.RenderIndex);
                                        }
                                        else
                                        {
                                            this.fc.AddRenderIndex(this.RenderIndex);
                                        }
                                        break;
                                    }
                                case IndexEditType.Delete:
                                    {
                                        this.fc.DeleteRenderIndex(this.RenderIndex.GeoColumnName);
                                        break;
                                    }
                                case IndexEditType.Create:
                                    {
                                        this.fc.AddRenderIndex(this.RenderIndex);
                                        break;
                                    }
                                default:
                                    {
                                        flag3 = true;
                                        break;
                                    }
                            }
                            if (flag2 && flag3)
                            {
                                this.fc.RebuildRenderIndex(this.RenderIndex.GeoColumnName, gviRenderIndexRebuildType.gviRenderIndexRebuildWithData);
                            }
                        }
                    }
                    if (waitDialogForm != null)
                    {
                        waitDialogForm.Close();
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex2)
            {
                LoggingService.Error(ex2.Message + "\r\n" + ex2.StackTrace);
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            catch (System.Exception e2)
            {
                LoggingService.Error(e2.Message + "\r\n" + e2.StackTrace);
                XtraMessageBox.Show("保存失败!");
                base.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            finally
            {
                if (base.DialogResult != System.Windows.Forms.DialogResult.None)
                {
                    if (this.fc != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(this.fc);
                        this.fc = null;
                    }
                    if (this.fds != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(this.fds);
                        this.fds = null;
                    }
                }
                if (waitDialogForm != null)
                {
                    waitDialogForm.Close();
                }
            }
        }
        private void sbtn_Cancel_Click(object sender, System.EventArgs e)
        {
            base.Close();
        }
        private System.Data.DataTable FillPropertyTable(IFieldInfo info)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add(this.propname, typeof(string));
            dataTable.Columns.Add(this.propvalue, typeof(string));
            dataTable.Rows.Add(new object[]
			{
				"别名", 
				info.Alias
			});
            dataTable.Rows.Add(new object[]
			{
				"可为空", 
				info.Nullable ? "是" : "否"
			});
            if (info.FieldType == gviFieldType.gviFieldString)
            {
                dataTable.Rows.Add(new object[]
				{
					"字符串长度", 
					info.Length
				});
            }
            if (info.GeometryDef != null)
            {
                dataTable.Rows.Add(new object[]
				{
					"域", 
					this.GetGeometryType2String(info.GeometryDef.GeometryColumnType)
				});
                if (info.FieldType == gviFieldType.gviFieldGeometry)
                {
                    dataTable.Rows.Add(new object[]
					{
						"HasZ", 
						info.GeometryDef.HasZ ? "是" : "否"
					});
                    dataTable.Rows.Add(new object[]
					{
						"HasM", 
						info.GeometryDef.HasM ? "是" : "否"
					});
                    dataTable.Rows.Add(new object[]
					{
						"HasId", 
						info.GeometryDef.HasId ? "是" : "否"
					});
                }
            }
            string text = "";
            if (info.Domain != null)
            {
                text = info.Domain.Name;
            }
            if (info.FieldType != gviFieldType.gviFieldGeometry)
            {
                dataTable.Rows.Add(new object[]
				{
					"域", 
					text
				});
            }
            return dataTable;
        }
        private System.Data.DataTable GetPropertyDataTable(string type)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            if (!type.Equals(""))
            {
                dataTable.Columns.Add(this.propname, typeof(string));
                dataTable.Columns.Add(this.propvalue, typeof(string));
                dataTable.Rows.Add(new object[]
				{
					"别名", 
					""
				});
                dataTable.Rows.Add(new object[]
				{
					"可为空", 
					"是"
				});
                if (type == "String")
                {
                    dataTable.Rows.Add(new object[]
					{
						"字符串长度", 
						255
					});
                }
                if (type == "Geometry")
                {
                    dataTable.Rows.Add(new object[]
					{
						"空间列类型", 
						"模型"
					});
                    dataTable.Rows.Add(new object[]
					{
						"HasZ", 
						"是"
					});
                    dataTable.Rows.Add(new object[]
					{
						"HasM", 
						"否"
					});
                    dataTable.Rows.Add(new object[]
					{
						"HasId", 
						"是"
					});
                }
                else
                {
                    dataTable.Rows.Add(new object[]
					{
						"域", 
						""
					});
                }
                this.bActiveInfo = true;
            }
            return dataTable;
        }
        private string GetGeometryType2String(gviGeometryColumnType type)
        {
            if (type == gviGeometryColumnType.gviGeometryColumnPoint)
            {
                return "点";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnMultiPoint)
            {
                return "多点";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnPolygon)
            {
                return "多边形";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnTriMesh)
            {
                return "三角网格";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnPolyline)
            {
                return "折线";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnPointCloud)
            {
                return "点云";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnCollection)
            {
                return "几何集合";
            }
            if (type == gviGeometryColumnType.gviGeometryColumnModelPoint)
            {
                return "模型";
            }
            return "";
        }
        private gviGeometryColumnType GetGeometryTypeByString(string type)
        {
            if (type == "点")
            {
                return gviGeometryColumnType.gviGeometryColumnPoint;
            }
            if (type == "多点")
            {
                return gviGeometryColumnType.gviGeometryColumnMultiPoint;
            }
            if (type == "多边形")
            {
                return gviGeometryColumnType.gviGeometryColumnPolygon;
            }
            if (type == "折线")
            {
                return gviGeometryColumnType.gviGeometryColumnPolyline;
            }
            if (type == "三角网格")
            {
                return gviGeometryColumnType.gviGeometryColumnTriMesh;
            }
            if (type == "点云")
            {
                return gviGeometryColumnType.gviGeometryColumnPointCloud;
            }
            if (type == "几何集合")
            {
                return gviGeometryColumnType.gviGeometryColumnCollection;
            }
            if (type == "模型")
            {
                return gviGeometryColumnType.gviGeometryColumnModelPoint;
            }
            return gviGeometryColumnType.gviGeometryColumnUnknown;
        }
        private gviFieldType GetFieldTypeByString(string type)
        {
            switch (type)
            {
                case "Geometry":
                    {
                        return gviFieldType.gviFieldGeometry;
                    }
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
                case "Blob":
                    {
                        return gviFieldType.gviFieldBlob;
                    }
                case "UUID":
                    {
                        return gviFieldType.gviFieldUUID;
                    }
                case "String":
                    {
                        return gviFieldType.gviFieldString;
                    }
            }
            return gviFieldType.gviFieldUnknown;
        }
        private string GetGviFieldType2String(gviFieldType type)
        {
            switch (type)
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
                case gviFieldType.gviFieldString:
                    {
                        return "String";
                    }
                case gviFieldType.gviFieldDate:
                    {
                        return "Date";
                    }
                case gviFieldType.gviFieldBlob:
                    {
                        return "Blob";
                    }
                case gviFieldType.gviFieldFID:
                    {
                        break;
                    }
                case gviFieldType.gviFieldUUID:
                    {
                        return "UUID";
                    }
                default:
                    {
                        if (type == gviFieldType.gviFieldGeometry)
                        {
                            return "Geometry";
                        }
                        break;
                    }
            }
            return "";
        }
        private object GetFieldtypeValue(string type, string value, out bool isRightType)
        {
            isRightType = true;
            if (type.Equals("Int16"))
            {
                short num = 0;
                isRightType = short.TryParse(value, out num);
                return num;
            }
            if (type.Equals("Int32"))
            {
                int num2 = 0;
                isRightType = int.TryParse(value, out num2);
                return num2;
            }
            if (type.Equals("Int64"))
            {
                long num3 = 0L;
                isRightType = long.TryParse(value, out num3);
                return num3;
            }
            if (type.Equals("Float"))
            {
                float num4 = 0f;
                isRightType = float.TryParse(value, out num4);
                return num4;
            }
            if (type.Equals("Double"))
            {
                double num5 = 0.0;
                isRightType = double.TryParse(value, out num5);
                return num5;
            }
            if (type.Equals("Date"))
            {
                System.DateTime dateTime = System.DateTime.Parse(value);
                return dateTime;
            }
            if (type.Equals("String"))
            {
                return value;
            }
            return null;
        }
        private bool CanRebuildIndex()
        {
            foreach (string current in this.DbIndexHash.Keys)
            {
                DbIndexBuffer dbIndexBuffer = this.DbIndexHash[current];
                if (dbIndexBuffer.et != IndexEditType.Normal)
                {
                    bool result = true;
                    return result;
                }
            }
            foreach (string current2 in this.GridIndexHash.Keys)
            {
                GridIndexBuffer gridIndexBuffer = this.GridIndexHash[current2];
                if (gridIndexBuffer.et != IndexEditType.Normal && (gridIndexBuffer.et == IndexEditType.Create || gridIndexBuffer.et == IndexEditType.Edit) && (gridIndexBuffer.data.L1 != 0.0 || gridIndexBuffer.data.L2 != 0.0 || gridIndexBuffer.data.L3 != 0.0))
                {
                    bool result = true;
                    return result;
                }
            }
            foreach (string current3 in this.RenderIndexHash.Keys)
            {
                RenderIndexBuffer renderIndexBuffer = this.RenderIndexHash[current3];
                if (renderIndexBuffer.et != IndexEditType.Normal && (renderIndexBuffer.et == IndexEditType.Create || renderIndexBuffer.et == IndexEditType.Edit) && renderIndexBuffer.data.L1 != 0.0)
                {
                    bool result = true;
                    return result;
                }
            }
            return false;
        }
        private void FillSubtypeItem()
        {
            this.subtypeHash.Clear();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Colvalue", typeof(int));
            dataTable.Columns.Add("Colcode", typeof(string));
            dataTable.Columns.Add("Colguid", typeof(System.Guid));
            if (this.fc.SubTypeCount > 0)
            {
                for (int i = 0; i < this.fc.SubTypeCount; i++)
                {
                    System.Guid guid = System.Guid.NewGuid();
                    this.subtype = this.fc.GetSubType(i);
                    dataTable.Rows.Add(new object[]
					{
						this.subtype.Code, 
						this.subtype.Name, 
						guid
					});
                    this.subtypeHash.Add(guid, this.subtype);
                }
                for (int j = 0; j < 100; j++)
                {
                    dataTable.Rows.Add(new object[]
					{
						null, 
						"", 
						System.Guid.NewGuid()
					});
                }
                this.subtype = this.fc.GetSubType(0);
                this.FillSubtypeProp(this.subtype);
                this.cbe_defsubtype.SelectedIndex = 0;
            }
            else
            {
                System.Guid guid2 = System.Guid.NewGuid();
                dataTable.Rows.Add(new object[]
				{
					1, 
					"newSubtype", 
					guid2
				});
                this.subtype = new SubTypeInfoClass();
                this.subtype.Code = 1;
                this.subtype.Name = "newSubtype";
                this.subtypeHash.Add(guid2, this.subtype);
                for (int k = 0; k < 100; k++)
                {
                    guid2 = System.Guid.NewGuid();
                    dataTable.Rows.Add(new object[]
					{
						null, 
						"", 
						guid2
					});
                }
                this.cbe_defsubtype.Properties.Items.Add("newSubtype");
                this.cbe_defsubtype.SelectedIndex = 0;
            }
            this.gridControl3.DataSource = dataTable;
            this.bActiveSubtype = true;
        }
        private int GetSubtypeId(SubTypeInfo stinfo, string fieldname)
        {
            if (stinfo.FieldDomainInfoCount != 0)
            {
                for (int i = 0; i < stinfo.FieldDomainInfoCount; i++)
                {
                    IFieldDomainInfo fieldDomainInfo = stinfo.GetFieldDomainInfo(i);
                    if (fieldname.Equals(fieldDomainInfo.FieldName))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        private System.Data.DataTable FillSubtypeProp(SubTypeInfo stinfo)
        {
            if (stinfo == null)
            {
                return null;
            }
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add(this.Colfn, typeof(string));
            dataTable.Columns.Add(this.Colft, typeof(string));
            dataTable.Columns.Add(this.Coldefvalue, typeof(object));
            dataTable.Columns.Add(this.Coldomain, typeof(string));
            for (int i = 0; i < this.fcFields.Count; i++)
            {
                IFieldInfo fieldInfo = this.fcFields.Get(i);
                if (fieldInfo.FieldType != gviFieldType.gviFieldFID && fieldInfo.FieldType != gviFieldType.gviFieldUUID && fieldInfo.FieldType != gviFieldType.gviFieldGeometry && fieldInfo.FieldType != gviFieldType.gviFieldBlob && !fieldInfo.Name.Equals(this.cbe_SubtypeField.Text) && !fieldInfo.Name.Equals("Name"))
                {
                    dataTable.Rows.Add(new object[]
					{
						fieldInfo.Name, 
						this.GetGviFieldType2String(fieldInfo.FieldType), 
						null, 
						""
					});
                }
            }
            if (stinfo != null)
            {
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    string fieldname = dataTable.Rows[j][this.Colfn].ToString();
                    int subtypeId = this.GetSubtypeId(stinfo, fieldname);
                    if (subtypeId >= 0)
                    {
                        FieldDomainInfo fieldDomainInfo = stinfo.GetFieldDomainInfo(subtypeId);
                        dataTable.Rows[j][this.Coldefvalue] = fieldDomainInfo.DefaultValue;
                        dataTable.Rows[j][this.Coldomain] = ((fieldDomainInfo.Domain == null) ? null : fieldDomainInfo.Domain.Name);
                    }
                }
            }
            this.bActiveSubtype = true;
            return dataTable;
        }
        private IFieldInfo GetFieldInfo(string name)
        {
            IFieldInfo result = null;
            for (int i = 0; i < this.fcFields.Count; i++)
            {
                if (this.fcFields.Get(i).Name.Equals(name))
                {
                    result = this.fcFields.Get(i);
                    break;
                }
            }
            return result;
        }
        private bool ContainSubtypeInfo(SubTypeInfo sub)
        {
            bool result = false;
            for (int i = 0; i < this.fc.SubTypeCount; i++)
            {
                SubTypeInfo subType = this.fc.GetSubType(i);
                if (subType.Code == sub.Code)
                {
                    result = true;
                }
            }
            return result;
        }
        private SubTypeInfo GetSubtypeFormCode(int code)
        {
            SubTypeInfo subTypeInfo = null;
            for (int i = 0; i < this.fc.SubTypeCount; i++)
            {
                subTypeInfo = this.fc.GetSubType(i);
                if (subTypeInfo.Code == code)
                {
                    break;
                }
            }
            return subTypeInfo;
        }
        private SubTypeInfo GetSubtypeFormCode(string name)
        {
            SubTypeInfo subTypeInfo = null;
            for (int i = 0; i < this.fc.SubTypeCount; i++)
            {
                subTypeInfo = this.fc.GetSubType(i);
                if (subTypeInfo.Name.Equals(name))
                {
                    break;
                }
            }
            return subTypeInfo;
        }
        private double DoubleConverter(string value)
        {
            double result = 0.0;
            if (value.Equals(1.7976931348623157E+308.ToString()))
            {
                result = 1.7976931348623157E+308;
            }
            else
            {
                result = System.Convert.ToDouble(value);
            }
            return result;
        }

    }
}
