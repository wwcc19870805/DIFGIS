using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using System.Collections;
using DFDataConfig.Class;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCTopoLib : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_Name;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private CheckedListBoxControl chkFacClass;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private RadioGroup rgIgnoreZ;
        private TextEdit txtName;
        private SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup lgcTopoInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private SpinEdit spinTolerance;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tl_Object;
        private SpinEdit spinToleranceZ;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
    
        private void InitializeComponent()
        {
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.tl_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tl_Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkFacClass = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rgIgnoreZ = new DevExpress.XtraEditors.RadioGroup();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.spinTolerance = new DevExpress.XtraEditors.SpinEdit();
            this.spinToleranceZ = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lgcTopoInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFacClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgIgnoreZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTolerance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinToleranceZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgcTopoInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.treeList1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(186, 421);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tl_Name,
            this.tl_Object});
            this.treeList1.Location = new System.Drawing.Point(10, 30);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowRoot = false;
            this.treeList1.Size = new System.Drawing.Size(182, 417);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // tl_Name
            // 
            this.tl_Name.Caption = "Name";
            this.tl_Name.FieldName = "Name";
            this.tl_Name.MinWidth = 33;
            this.tl_Name.Name = "tl_Name";
            this.tl_Name.Visible = true;
            this.tl_Name.VisibleIndex = 0;
            // 
            // tl_Object
            // 
            this.tl_Object.Caption = "Object";
            this.tl_Object.FieldName = "Object";
            this.tl_Object.Name = "tl_Object";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.chkFacClass;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 107);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(327, 278);
            this.layoutControlItem6.Text = "管线设施类：";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // chkFacClass
            // 
            this.chkFacClass.CheckOnClick = true;
            this.chkFacClass.Location = new System.Drawing.Point(207, 142);
            this.chkFacClass.Name = "chkFacClass";
            this.chkFacClass.Size = new System.Drawing.Size(323, 274);
            this.chkFacClass.StyleController = this.layoutControl1;
            this.chkFacClass.TabIndex = 7;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.treeList1);
            this.layoutControl1.Controls.Add(this.rgIgnoreZ);
            this.layoutControl1.Controls.Add(this.chkFacClass);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.spinTolerance);
            this.layoutControl1.Controls.Add(this.spinToleranceZ);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(545, 457);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rgIgnoreZ
            // 
            this.rgIgnoreZ.EditValue = 1;
            this.rgIgnoreZ.Location = new System.Drawing.Point(289, 87);
            this.rgIgnoreZ.Name = "rgIgnoreZ";
            this.rgIgnoreZ.Properties.Columns = 2;
            this.rgIgnoreZ.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "否")});
            this.rgIgnoreZ.Size = new System.Drawing.Size(241, 25);
            this.rgIgnoreZ.StyleController = this.layoutControl1;
            this.rgIgnoreZ.TabIndex = 10;
            this.rgIgnoreZ.SelectedIndexChanged += new System.EventHandler(this.rgIgnoreZ_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(289, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(241, 22);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(330, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "创建";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // spinTolerance
            // 
            this.spinTolerance.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.spinTolerance.Location = new System.Drawing.Point(289, 61);
            this.spinTolerance.Name = "spinTolerance";
            this.spinTolerance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTolerance.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinTolerance.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spinTolerance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinTolerance.Size = new System.Drawing.Size(241, 22);
            this.spinTolerance.StyleController = this.layoutControl1;
            this.spinTolerance.TabIndex = 9;
            // 
            // spinToleranceZ
            // 
            this.spinToleranceZ.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.spinToleranceZ.Location = new System.Drawing.Point(289, 116);
            this.spinToleranceZ.Name = "spinToleranceZ";
            this.spinToleranceZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinToleranceZ.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinToleranceZ.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinToleranceZ.Size = new System.Drawing.Size(241, 22);
            this.spinToleranceZ.StyleController = this.layoutControl1;
            this.spinToleranceZ.TabIndex = 11;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lgcTopoInfo});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.ShowInCustomizationForm = false;
            this.layoutControlGroup1.Size = new System.Drawing.Size(545, 457);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "拓扑层";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(192, 447);
            this.layoutControlGroup2.Text = "拓扑目录树";
            // 
            // lgcTopoInfo
            // 
            this.lgcTopoInfo.CustomizationFormText = "拓扑层信息";
            this.lgcTopoInfo.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.lgcTopoInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.emptySpaceItem1,
            this.layoutControlItem7,
            this.layoutControlItem6});
            this.lgcTopoInfo.Location = new System.Drawing.Point(192, 0);
            this.lgcTopoInfo.Name = "lgcTopoInfo";
            this.lgcTopoInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lgcTopoInfo.Size = new System.Drawing.Size(343, 447);
            this.lgcTopoInfo.Text = "拓扑层信息";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtName;
            this.layoutControlItem3.CustomizationFormText = "拓扑层名称：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(327, 26);
            this.layoutControlItem3.Text = "拓扑层名称：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(79, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.spinTolerance;
            this.layoutControlItem10.CustomizationFormText = "XY坐标容差值：";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(327, 26);
            this.layoutControlItem10.Text = "XY容差值：";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(79, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.rgIgnoreZ;
            this.layoutControlItem11.CustomizationFormText = "是否启用Z容差值：";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(327, 29);
            this.layoutControlItem11.Text = "启用Z容差值：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(79, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.spinToleranceZ;
            this.layoutControlItem12.CustomizationFormText = "Z容差值：";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 81);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(327, 26);
            this.layoutControlItem12.Text = "Z容差值：";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(79, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 385);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(123, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(123, 385);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(204, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // UCTopoLib
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCTopoLib";
            this.Size = new System.Drawing.Size(545, 457);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFacClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgIgnoreZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTolerance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinToleranceZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgcTopoInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        private ContextMenuStrip _menuTopoLayer1;
        private ContextMenuStrip _menuTopoLayer2;
        private IDataSource _ds;
        private bool _isCreate;

        public UCTopoLib()
        {
            InitializeComponent();
            this._menuTopoLayer1 = new ContextMenuStrip();
            this._menuTopoLayer2 = new ContextMenuStrip();
            this._menuTopoLayer1.Items.Add("创建拓扑层").Click += new EventHandler(this.itemCreate_Click);
            this._menuTopoLayer1.Items.Add(new ToolStripSeparator());
            this._menuTopoLayer1.Items.Add("全部展开").Click += new EventHandler(this.itemExpand_Click);
            this._menuTopoLayer1.Items.Add("全部折叠").Click += new EventHandler(this.itemCollapse_Click);
            //this._menuTopoLayer2.Items.Add("编辑拓扑层").Click += new EventHandler(this.itemEdit_Click);
            this._menuTopoLayer2.Items.Add("删除拓扑层").Click += new EventHandler(this.itemDel_Click);
        }

        public void Init()
        {
            try
            {
                this._ds = DF3DPipeCreateApp.App.TemplateLib;
                if (this._ds == null) { this.Enabled = false; return; }

                WaitForm.Start("正在加载数据...", "请稍后");

                this.chkFacClass.Items.Clear();
                List<FacClass> list = GetAllFacClasses();
                if (list != null)
                {
                    foreach (FacClass fc in list)
                    {
                        this.chkFacClass.Items.Add(fc);
                    }
                }

                this.treeList1.ClearNodes();
                List<TopoClass> list1 = GetAllTopoLayers();
                if (list1 != null)
                {
                    foreach (TopoClass tc in list1)
                    {
                        TreeListNode tln = this.treeList1.AppendNode(new object[] { tc.Name, tc }, null);
                        foreach (FacClass fc in list)
                        {
                            if (fc.TopoLayerId == tc.ObjectId)
                            {
                                this.treeList1.AppendNode(new object[] { fc.Name, fc }, tln);
                            }
                        }
                        tln.ExpandAll();
                    }
                    this.treeList1.SetFocusedNode(null);
                }
            }
            catch (Exception ex) { }
            finally
            {
                WaitForm.Stop();
            }
        }

        private List<FacClass> GetAllFacClasses()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacilityType <> '{0}'", "UnKnown"),
                    PostfixClause = "order by OrderBy asc"
                };
                cursor = oc.Search(filter, true);
                List<FacClass> list = new List<FacClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    FacClass fc = new FacClass();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Code") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Code"));
                        if (obj != null) fc.Code = obj.ToString();
                    }
                    if (row.FieldIndex("PCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("PCode"));
                        if (obj != null) fc.PCode = obj.ToString();
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("FacilityType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacilityType"));
                        if (obj != null)
                        {
                            fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName(obj.ToString());
                        }
                    }
                    if (row.FieldIndex("LocationType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("LocationType"));
                        if (obj != null)
                        {
                            fc.LocationType = (LocationType)Enum.Parse(typeof(LocationType), obj.ToString());
                        }
                    }
                    if (row.FieldIndex("TurnerStyle") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TurnerStyle"));
                        if (obj != null)
                        {
                            fc.TurnerStyle = (TurnerStyle)Enum.Parse(typeof(TurnerStyle), obj.ToString());
                        }
                    }
                    if (row.FieldIndex("TopoLayerId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoLayerId"));
                        if (obj != null) fc.TopoLayerId = obj.ToString();
                    }
                    list.Add(fc);
                }
                return list;
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

        private List<TopoClass> GetAllTopoLayers()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                cursor = oc.Search(filter, true);
                List<TopoClass> list = new List<TopoClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    TopoClass tc = new TopoClass();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) tc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) tc.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("TopoLayerName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoLayerName"));
                        if (obj != null) tc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("Tolerance") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Tolerance"));
                        if (obj != null) tc.Tolerance = double.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ToleranceZ") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ToleranceZ"));
                        if (obj != null) tc.ToleranceZ = double.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("IgnoreZ") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("IgnoreZ"));
                        if (obj != null) tc.IgnoreZ = obj.ToString() == "1" ? true : false;
                    }
                    if (row.FieldIndex("TopoTableName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TopoTableName"));
                        if (obj != null) tc.TopoTable = obj.ToString();
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) tc.Comment = obj.ToString();
                    }
                    list.Add(tc);
                }
                return list;
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

        private void itemCollapse_Click(object sender, EventArgs e)
        {
            this.treeList1.CollapseAll();
        }
        private void itemExpand_Click(object sender, EventArgs e)
        {
            this.treeList1.ExpandAll();
        }

        private void itemCreate_Click(object sender, EventArgs e)
        {
            this.lgcTopoInfo.Enabled = true;
            this.btnSave.Text = "创建";
            this._isCreate = true;
            this.txtName.Text = "";
            this.txtName.Enabled = true;
            this.spinTolerance.Text = "0.05";
            this.rgIgnoreZ.SelectedIndex = 0;
            this.spinToleranceZ.Text = "0.05";
            this.chkFacClass.UnCheckAll();
            rgIgnoreZ_SelectedIndexChanged(null, null);
        }

        private void itemEdit_Click(object sender, EventArgs e)
        {
            this.lgcTopoInfo.Enabled = true;
            this.btnSave.Text = "保存";
            this._isCreate = false;
        }

        private void itemDel_Click(object sender, EventArgs e)
        {
            if (this.treeList1.FocusedNode == null) return;
            object obj = this.treeList1.FocusedNode.GetValue("Object");
            if (!(obj is TopoClass)) return;
            TopoClass tc = obj as TopoClass;
            if (!DeleteTopoClass(tc)) { XtraMessageBox.Show("删除失败", "提示"); }
            else
            {
                this.treeList1.DeleteNode(this.treeList1.FocusedNode);
                this.treeList1.SetFocusedNode(null);
            }
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                TreeList control = sender as TreeList;
                TreeListHitInfo info = control.CalcHitInfo(e.Location);
                if (info != null)
                {
                    TreeListNode node = info.Node;
                    this.treeList1.SetFocusedNode(node);
                    if ((control != null) && (((e.Button == MouseButtons.Right) && (Control.ModifierKeys == Keys.None)) && (control.State == TreeListState.Regular)))
                    {
                        if (node != null)
                        {
                            if (node.Level == 0)
                            {
                                this._menuTopoLayer2.Show(control, new Point(e.X, e.Y));
                            }
                        }
                        else
                        {
                            this._menuTopoLayer1.Show(control, new Point(e.X, e.Y));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }

        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (this.treeList1.FocusedNode == null) this.lgcTopoInfo.Enabled = false;
            else
            {
                object obj = this.treeList1.FocusedNode.GetValue("Object");
                if (!(obj is TopoClass)) this.lgcTopoInfo.Enabled = false;
                else
                {
                    this.lgcTopoInfo.Enabled = true;
                    this.btnSave.Text = "保存";
                    this._isCreate = false;

                    TopoClass tc = obj as TopoClass;
                    this.txtName.Text = tc.Name;
                    this.spinTolerance.Text = tc.Tolerance.ToString();
                    if (tc.IgnoreZ)
                    {
                        this.rgIgnoreZ.SelectedIndex = 1;
                        this.spinToleranceZ.Enabled = false;
                    }
                    else
                    {
                        this.rgIgnoreZ.SelectedIndex = 0;
                        this.spinToleranceZ.Enabled = true;
                        this.spinToleranceZ.Text = tc.ToleranceZ.ToString();
                    }

                    this.chkFacClass.UnCheckAll();

                    for (int index = 0; index < this.chkFacClass.Items.Count; index++)
                    {
                        object obj2 = this.chkFacClass.Items[index].Value;
                        if (obj2 != null && obj2 is FacClass)
                        {
                            FacClass fc2 = obj2 as FacClass;
                            TreeListNodes nodes = this.treeList1.FocusedNode.Nodes;
                            foreach (TreeListNode node in nodes)
                            {
                                object obj1 = node.GetValue("Object");
                                FacClass fc1 = obj1 as FacClass;
                                if (obj1 != null && obj1 is FacClass)
                                {
                                    if (fc1.Code == fc2.Code)
                                    {
                                        this.chkFacClass.SetItemChecked(index, true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rgIgnoreZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rgIgnoreZ.SelectedIndex == 0) this.spinToleranceZ.Enabled = true;
            else this.spinToleranceZ.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this._isCreate)
            {
                if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    XtraMessageBox.Show("请输入拓扑层名称！", "提示");
                    this.txtName.Focus();
                    return;
                }
                TopoClass tc = new TopoClass();
                tc.ObjectId = BitConverter.ToString(ObjectIdGenerator.Generate()).Replace("-", string.Empty).ToLowerInvariant();
                tc.TopoTable = string.Format("TOPO_OC_{0}", DateTime.Now.Ticks);
                tc.Name = this.txtName.Text.Trim();
                tc.Tolerance = Convert.ToDouble(this.spinTolerance.Value);
                tc.IgnoreZ = this.rgIgnoreZ.SelectedIndex == 0 ? false : true;
                if (!tc.IgnoreZ)
                {
                    tc.ToleranceZ = Convert.ToDouble(this.spinToleranceZ.Value);
                }
                else
                {
                    tc.ToleranceZ = 0.0;
                }
                ArrayList arr = new ArrayList();
                for (int i = 0; i < this.chkFacClass.CheckedItems.Count; i++)
                {
                    object obj = this.chkFacClass.CheckedItems[i];
                    if (obj is FacClass)
                    {
                        FacClass fc = obj as FacClass;
                        arr.Add(fc.Code);                        
                    }
                }
                //写入数据库
                if (CreateTopoClass(tc, (string[])arr.ToArray(typeof(string))))
                {
                    TreeListNode pnode = this.treeList1.AppendNode(new object[] { tc.Name, tc }, null);
                    if (pnode == null) return;
                    this.spinToleranceZ.Text = tc.ToleranceZ.ToString();
                    for (int i = 0; i < this.chkFacClass.CheckedItems.Count; i++)
                    {
                        object obj = this.chkFacClass.CheckedItems[i];
                        if (obj is FacClass)
                        {
                            FacClass fc = obj as FacClass;
                            this.treeList1.AppendNode(new object[] { fc.Name, fc }, pnode);
                        }
                    }
                    pnode.ExpandAll();
                    this.treeList1.SetFocusedNode(pnode);
                    XtraMessageBox.Show("创建拓扑信息成功！", "提示");
                }
                else XtraMessageBox.Show("创建拓扑信息失败！", "提示");
            }
            else
            {
                if (this.treeList1.FocusedNode == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    XtraMessageBox.Show("请输入拓扑层名称！", "提示");
                    this.txtName.Focus();
                    return;
                }
                object obj = this.treeList1.FocusedNode.GetValue("Object");
                if (obj is TopoClass)
                {
                    TopoClass tc = obj as TopoClass;
                    tc.Name = this.txtName.Text.Trim();
                    tc.Tolerance = Convert.ToDouble(this.spinTolerance.Value);
                    tc.IgnoreZ = this.rgIgnoreZ.SelectedIndex == 0 ? false : true;
                    if (!tc.IgnoreZ)
                    {
                        tc.ToleranceZ = Convert.ToDouble(this.spinToleranceZ.Value);
                    }
                    else
                    {
                        tc.ToleranceZ = 0.0;
                    }
                    ArrayList arr = new ArrayList();
                    for (int i = 0; i < this.chkFacClass.CheckedItems.Count; i++)
                    {
                        object obj1 = this.chkFacClass.CheckedItems[i];
                        if (obj1 is FacClass)
                        {
                            FacClass fc = obj1 as FacClass;
                            arr.Add(fc.Code);
                        }
                    }
                    //写入数据库
                    if (UpdateTopoClass(tc,(string[])arr.ToArray(typeof(string))))
                    {
                        this.treeList1.FocusedNode.SetValue("Name", tc.Name);
                        this.treeList1.FocusedNode.SetValue("Object", tc);
                        this.spinToleranceZ.Text = tc.ToleranceZ.ToString();
                        this.treeList1.FocusedNode.Nodes.Clear();
                        for (int i = 0; i < this.chkFacClass.CheckedItems.Count; i++)
                        {
                            object obj1 = this.chkFacClass.CheckedItems[i];
                            if (obj1 is FacClass)
                            {
                                FacClass fc = obj1 as FacClass;
                                this.treeList1.AppendNode(new object[] { fc.Name, fc }, this.treeList1.FocusedNode);
                            }
                        }
                        XtraMessageBox.Show("保存拓扑信息成功！", "提示");
                    }
                    else XtraMessageBox.Show("保存拓扑信息失败！", "提示");
                }
            }
        }

        private bool DeleteTopoClass(TopoClass tc)
        {
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = string.Format("ObjectId = '{0}'", tc.ObjectId);
                oc.Delete(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool UpdateTopoClass(TopoClass tc, string[] strs)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return false;

                IQueryFilter filter = new QueryFilter()
                {
                    WhereClause = string.Format("ObjectId = '{0}'", tc.ObjectId),
                    SubFields = "oid,TopoLayerName,Tolerance,ToleranceZ,IgnoreZ"
                };
                cursor = oc.Update(filter);
                row = cursor.NextRow();
                if (row != null)
                {
                    row.SetValue(1, tc.Name);
                    row.SetValue(2, tc.Tolerance);
                    row.SetValue(3, tc.ToleranceZ);
                    row.SetValue(4, tc.IgnoreZ ? 1 : 0);
                    cursor.UpdateRow(row);
                    if (UpdataFacClass(strs, tc.ObjectId)) return true;
                    else return false;
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

        private bool UpdataFacClass(string[] strs, string topoObjectId)
        {
            if (strs == null || strs.Length == 0) return true;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return false;

                string temp = "";
                foreach(string str in strs)
                {
                    temp += "Code = '" + str + "' or ";
                }
                temp = temp.Substring(0, temp.Length - 3);

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = temp;

                cursor = oc.Update(filter);
                while ((row = cursor.NextRow()) != null)
                {
                    row.SetValue(row.FieldIndex("TopoLayerId"), topoObjectId);
                    cursor.UpdateRow(row);
                }
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

        private bool CreateTopoClass(TopoClass tc,string[] strs)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._ds.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return false;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return false;

                row = oc.CreateRowBuffer();
                cursor = oc.Insert();
                if (row != null)
                {
                    row.SetValue(row.FieldIndex("TopoLayerName"), tc.Name);
                    row.SetValue(row.FieldIndex("ObjectId"), tc.ObjectId);
                    row.SetValue(row.FieldIndex("Tolerance"), tc.Tolerance);
                    row.SetValue(row.FieldIndex("ToleranceZ"), tc.ToleranceZ);
                    row.SetValue(row.FieldIndex("IgnoreZ"), tc.IgnoreZ ? 1 : 0);
                    row.SetValue(row.FieldIndex("TopoTableName"), tc.TopoTable);
                    cursor.InsertRow(row);
                    tc.Id = cursor.LastInsertId;
                    if (UpdataFacClass(strs, tc.ObjectId)) return true;
                    else return false;
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
