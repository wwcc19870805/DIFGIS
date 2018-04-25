using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DevExpress.XtraTreeList.Nodes;
using Gvitech.CityMaker.FdeGeometry;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCTopoCreate : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private CheckEdit chkPt2Line;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnConfirm;
        private RadioGroup rdgTopoType;
        private DevExpress.XtraTreeList.TreeList treeTopoLayer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcName;
        private CheckEdit btnSelOrUnAll;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;


        private void InitializeComponent()
        {
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkPt2Line = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.rdgTopoType = new DevExpress.XtraEditors.RadioGroup();
            this.treeTopoLayer = new DevExpress.XtraTreeList.TreeList();
            this.tcName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tcObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnSelOrUnAll = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPt2Line.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdgTopoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeTopoLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelOrUnAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkPt2Line;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(194, 38);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(352, 23);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // chkPt2Line
            // 
            this.chkPt2Line.Location = new System.Drawing.Point(196, 40);
            this.chkPt2Line.Name = "chkPt2Line";
            this.chkPt2Line.Properties.Caption = "提取点地面高程到线";
            this.chkPt2Line.Size = new System.Drawing.Size(348, 19);
            this.chkPt2Line.StyleController = this.layoutControl1;
            this.chkPt2Line.TabIndex = 9;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkPt2Line);
            this.layoutControl1.Controls.Add(this.btnConfirm);
            this.layoutControl1.Controls.Add(this.rdgTopoType);
            this.layoutControl1.Controls.Add(this.treeTopoLayer);
            this.layoutControl1.Controls.Add(this.btnSelOrUnAll);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(546, 446);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(390, 63);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(154, 22);
            this.btnConfirm.StyleController = this.layoutControl1;
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "创建";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // rdgTopoType
            // 
            this.rdgTopoType.EditValue = 0;
            this.rdgTopoType.Location = new System.Drawing.Point(263, 2);
            this.rdgTopoType.Name = "rdgTopoType";
            this.rdgTopoType.Properties.Columns = 2;
            this.rdgTopoType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "属性建拓扑"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "空间建拓扑")});
            this.rdgTopoType.Size = new System.Drawing.Size(281, 34);
            this.rdgTopoType.StyleController = this.layoutControl1;
            this.rdgTopoType.TabIndex = 6;
            // 
            // treeTopoLayer
            // 
            this.treeTopoLayer.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeTopoLayer.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.treeTopoLayer.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeTopoLayer.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcName,
            this.tcObject});
            this.treeTopoLayer.Location = new System.Drawing.Point(5, 25);
            this.treeTopoLayer.Name = "treeTopoLayer";
            this.treeTopoLayer.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeTopoLayer.OptionsView.ShowCheckBoxes = true;
            this.treeTopoLayer.OptionsView.ShowColumns = false;
            this.treeTopoLayer.Size = new System.Drawing.Size(184, 393);
            this.treeTopoLayer.TabIndex = 4;
            // 
            // tcName
            // 
            this.tcName.Caption = "名称";
            this.tcName.FieldName = "Name";
            this.tcName.MinWidth = 32;
            this.tcName.Name = "tcName";
            this.tcName.OptionsColumn.AllowEdit = false;
            this.tcName.OptionsColumn.AllowFocus = false;
            this.tcName.OptionsColumn.AllowSort = false;
            this.tcName.OptionsFilter.AllowFilter = false;
            this.tcName.Visible = true;
            this.tcName.VisibleIndex = 0;
            // 
            // tcObject
            // 
            this.tcObject.Caption = "对象";
            this.tcObject.FieldName = "NodeObject";
            this.tcObject.Name = "tcObject";
            // 
            // btnSelOrUnAll
            // 
            this.btnSelOrUnAll.Location = new System.Drawing.Point(5, 422);
            this.btnSelOrUnAll.Name = "btnSelOrUnAll";
            this.btnSelOrUnAll.Properties.Caption = "全选\\取消全选";
            this.btnSelOrUnAll.Size = new System.Drawing.Size(184, 19);
            this.btnSelOrUnAll.StyleController = this.layoutControl1;
            this.btnSelOrUnAll.TabIndex = 5;
            this.btnSelOrUnAll.CheckedChanged += new System.EventHandler(this.btnSelOrUnAll_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(546, 446);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "拓扑层";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(194, 446);
            this.layoutControlGroup2.Text = "拓扑层";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeTopoLayer;
            this.layoutControlItem1.CustomizationFormText = "拓扑层";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(188, 397);
            this.layoutControlItem1.Text = "拓扑层";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSelOrUnAll;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 397);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(188, 23);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.rdgTopoType;
            this.layoutControlItem3.CustomizationFormText = "建拓扑方式:";
            this.layoutControlItem3.Location = new System.Drawing.Point(194, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(352, 38);
            this.layoutControlItem3.Text = "建拓扑方式:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(64, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnConfirm;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(388, 61);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(194, 87);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(352, 359);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(194, 61);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(194, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // UCTopoCreate
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCTopoCreate";
            this.Size = new System.Drawing.Size(546, 446);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPt2Line.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdgTopoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeTopoLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelOrUnAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        private IDataSource _dsTemplate;
        private IDataSource _dsPipe;
        private IDataSource _dsTemp;
        public UCTopoCreate()
        {
            InitializeComponent();
        }
        private List<TopoClass> GetAllTopoLayers()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
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
        private List<FacClass> GetAllFacClasses()
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
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

        public void Init()
        {
            try
            {
                this._dsTemplate = DF3DPipeCreateApp.App.TemplateLib;
                if (this._dsTemplate == null) { this.Enabled = false; return; }
                this._dsPipe = DF3DPipeCreateApp.App.PipeLib;
                if (this._dsPipe == null) { this.Enabled = false; return; }
                this._dsTemp = DF3DPipeCreateApp.App.TempLib;
                if (this._dsTemp == null) { this.Enabled = false; return; }

                WaitForm.Start("正在加载数据...", "请稍后");

                this.treeTopoLayer.Nodes.Clear();
                List<FacClass> list = GetAllFacClasses();
                List<TopoClass> list1 = GetAllTopoLayers();
                if (list1 != null && list != null)
                {
                    foreach (TopoClass tc in list1)
                    {
                        TreeListNode tln = this.treeTopoLayer.AppendNode(new object[] { tc.Name, tc }, null);
                        foreach (FacClass fc in list)
                        {
                            if (fc.TopoLayerId == tc.ObjectId)
                            {
                                this.treeTopoLayer.AppendNode(new object[] { fc.Name, fc }, tln);
                            }
                        }
                        tln.ExpandAll();
                    }
                    this.treeTopoLayer.SetFocusedNode(null);
                }
            }
            catch (Exception ex) { }
            finally
            {
                WaitForm.Stop();
            }
        }

        private void btnSelOrUnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.btnSelOrUnAll.Checked) this.treeTopoLayer.CheckAll();
            else this.treeTopoLayer.UncheckAll();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool bHave = false;
            foreach (TreeListNode node in this.treeTopoLayer.Nodes)
            {
                if (node.Checked)
                {
                    bHave = true;
                }
            }
            if (!bHave)
            {
                XtraMessageBox.Show("请勾选要重建拓扑的图层！", "提示");
                return;
            }
            try
            {
                foreach (TreeListNode node2 in this.treeTopoLayer.Nodes)
                {
                    if (node2.Checked)
                    {
                        object objTC = node2.GetValue("NodeObject");
                        if (objTC == null || !(objTC is TopoClass)) continue;
                        TopoClass tc = objTC as TopoClass;
                        if (node2.Nodes == null || node2.Nodes.Count < 2) continue;
                        List<FacClass> listFCs = new List<FacClass>();
                        foreach (TreeListNode node3 in node2.Nodes)
                        {
                            object objFC = node3.GetValue("NodeObject");
                            if (objFC == null || !(objFC is FacClass)) continue;
                            listFCs.Add(objFC as FacClass);
                        }
                        if (listFCs.Count < 2) continue;

                        if (this.rdgTopoType.SelectedIndex == 0)
                        {
                            CreateTopoByAttribute(tc, listFCs, this.chkPt2Line.Checked);
                        }
                        else
                        {
                            CreateTopoBySpatial(tc, listFCs, this.chkPt2Line.Checked);
                        }
                    }
                }
                DeleteTopoTempFeatureClass();
            }
            catch (Exception ex)
            {

            }
        }

        private List<FacClassReg> GetFacClassRegByCodes(string codes)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "FacClassCode in (" + codes + ")";

                cursor = oc.Search(filter, true);
                List<FacClassReg> list = new List<FacClassReg>();
                while ((row = cursor.NextRow()) != null)
                {
                    FacClassReg fc = new FacClassReg();
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fc.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fc.Name = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fc.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("DataSetName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataSetName"));
                        if (obj != null) fc.DataSetName = obj.ToString();
                    }
                    if (row.FieldIndex("FeatureClassId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FeatureClassId"));
                        if (obj != null) fc.FeatureClassId = obj.ToString();
                    }
                    if (row.FieldIndex("FcName") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FcName"));
                        if (obj != null) fc.FcName = obj.ToString();
                    }
                    if (row.FieldIndex("DataType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("DataType"));
                        if (obj != null)
                        {
                            DataLifeCyle ts = 0;
                            if (Enum.TryParse<DataLifeCyle>(obj.ToString(), out ts))
                                fc.DataType = ts;
                            else fc.DataType = 0;
                        }
                    }
                    if (row.FieldIndex("Comment") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Comment"));
                        if (obj != null) fc.Comment = obj.ToString();
                    }
                    if (row.FieldIndex("TurnerStyle") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("TurnerStyle"));
                        if (obj != null)
                        {
                            TurnerStyle ts = 0;
                            if (Enum.TryParse<TurnerStyle>(obj.ToString(), out ts))
                                fc.TurnerStyle = ts;
                            else fc.TurnerStyle = 0;
                        }
                    }
                    if (row.FieldIndex("FacilityType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacilityType"));
                        if (obj != null) fc.FacilityType = FacilityClassManager.Instance.GetFacilityClassByName(obj.ToString());
                    }
                    if (row.FieldIndex("LocationType") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("LocationType"));
                        if (obj != null)
                        {
                            LocationType lt = 0;
                            if (Enum.TryParse<LocationType>(obj.ToString(), out lt))
                                fc.LocationType = lt;
                            else fc.LocationType = 0;
                        }
                    }
                    list.Add(fc);
                }
                return list;
            }
            catch (Exception ex)
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

        public void CreateTopoByAttribute(TopoClass tc, List<FacClass> listFCs, bool bTerrainHeight)
        {
            if (tc == null || listFCs == null || listFCs.Count < 2) return;
            string facCCodes = "'";
            foreach (FacClass facc in listFCs)
            {
                facCCodes += facc.Code + "','";
            }
            facCCodes = facCCodes.Substring(0, facCCodes.Length - 2);
            List<FacClassReg> listFCRs = GetFacClassRegByCodes(facCCodes);
            if (listFCRs == null || listFCRs.Count == 0) return;
            List<FacClassReg> listPipeNode = new List<FacClassReg>();
            List<FacClassReg> listPipeLine = new List<FacClassReg>();
            foreach (FacClassReg fcr in listFCRs)
            {
                if (fcr.FacilityType.Name == "PipeLine")
                {
                    listPipeLine.Add(fcr);
                }
                else if (fcr.FacilityType.Name == "PipeNode")
                {
                    listPipeNode.Add(fcr);
                }
            }
            if ((listPipeNode.Count == 0) || (listPipeLine.Count == 0)) return;

            // 删除空间索引和数据
            IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
            if (fds == null) return;
            IFeatureClass fcTopo = fds.OpenFeatureClass(tc.TopoTable);
            if (fcTopo == null) return;

            try
            {
                WaitForm.Start("根据属性建拓扑，未开始...", "请稍候", new Size(340, 50));
                WaitForm.SetCaption("删除【" + tc.Name + "】拓扑信息空间索引...");
                if (fcTopo.GetSpatialIndexInfos() != null && fcTopo.GetSpatialIndexInfos().Count > 0) fcTopo.DeleteSpatialIndex("Geometry");
                WaitForm.SetCaption("删除【" + tc.Name + "】拓扑数据...");
                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                fcTopo.Delete(filter);

                #region 管点信息
                WaitForm.SetCaption("建立【" + tc.Name + "】拓扑信息管点字典...");
                Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>();
                foreach (FacClassReg fcrPipeNode in listPipeNode)
                {
                    string[] array = null;
                    if (!bTerrainHeight)
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("DetectNo");
                        if (fi != null) array = new string[] { fi.Name };
                    }
                    else
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("DetectNo");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("SurfHeight");
                        if (fi != null && fi1 != null) array = new string[] { fi.Name, fi1.Name };
                    }
                    if (array != null)
                    {
                        IFeatureDataSet fdsFCR = this._dsPipe.OpenFeatureDataset(fcrPipeNode.DataSetName);
                        if (fdsFCR != null)
                        {
                            IFeatureClass fcNode = fdsFCR.OpenFeatureClass(fcrPipeNode.FcName);
                            if (fcNode != null)
                            {
                                IQueryFilter filter1 = new QueryFilterClass();
                                if (!bTerrainHeight)
                                {
                                    filter1.SubFields = string.Format("oid,{0}", array[0]);
                                }
                                else
                                {
                                    filter1.SubFields = string.Format("oid,{0},{1}", array[0], array[1]);
                                }
                                IFdeCursor cursor = new FdeCursorClass();
                                cursor = fcNode.Search(filter1, true);
                                IRowBuffer row = null;
                                while ((row = cursor.NextRow()) != null)
                                {
                                    if (!row.IsNull(1))
                                    {
                                        int num4 = Convert.ToInt32(row.GetValue(0));
                                        if (!row.IsNull(1))
                                        {
                                            string key = row.GetValue(1).ToString();
                                            if ((key != "") && !dictionary.ContainsKey(key))
                                            {
                                                if (!bTerrainHeight)
                                                {
                                                    dictionary.Add(key, new object[] { num4, fcrPipeNode.FeatureClassId });
                                                }
                                                else
                                                {
                                                    dictionary.Add(key, new object[] { num4, fcrPipeNode.FeatureClassId, row.GetValue(2) });
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }

                }
                #endregion

                #region topo信息
                IRowBuffer rowTopo = fcTopo.CreateRowBuffer();
                IFdeCursor cursorTopo = fcTopo.Insert();
                WaitForm.SetCaption("写入【" + tc.Name + "】拓扑数据...");
                foreach (FacClassReg fcrPipeLine in listPipeLine)
                {
                    string[] array = null;
                    if (!bTerrainHeight)
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("StartNo");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("EndNo");
                        if (fi != null && fi1 != null) array = new string[] { fi.Name, fi1.Name };
                    }
                    else
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("StartNo");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("EndNo");
                        DFDataConfig.Class.FieldInfo fi2 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("StartHeight");
                        DFDataConfig.Class.FieldInfo fi3 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("EndHeight");
                        if (fi != null && fi1 != null && fi2 != null && fi3 != null) array = new string[] { fi.Name, fi1.Name, fi2.Name, fi3.Name };
                    }
                    if (array != null)
                    {
                        if (array != null)
                        {
                            IFeatureDataSet fdsFCR = this._dsPipe.OpenFeatureDataset(fcrPipeLine.DataSetName);
                            if (fdsFCR != null)
                            {
                                IFeatureClass fcPipeLine = fdsFCR.OpenFeatureClass(fcrPipeLine.FcName);
                                if (fcPipeLine != null)
                                {
                                    IQueryFilter filter1 = new QueryFilterClass();
                                    if (!bTerrainHeight)
                                    {
                                        filter1.SubFields = string.Format("oid,{0},{1},Shape", array[0], array[1]);
                                    }
                                    else
                                    {
                                        filter1.SubFields = string.Format("oid,{0},{1},Shape,{2},{3}", array[0], array[1], array[2], array[3]);
                                    }
                                    IFdeCursor cursor = new FdeCursorClass();
                                    int count = fcPipeLine.GetCount(filter1);
                                    int icount = 0;
                                    if (bTerrainHeight)
                                    {
                                        cursor = fcPipeLine.Update(filter1);
                                    }
                                    else
                                    {
                                        cursor = fcPipeLine.Search(filter1, true);
                                    }
                                    IRowBuffer row = null;
                                    while ((row = cursor.NextRow()) != null)
                                    {
                                        icount++;
                                        WaitForm.SetCaption("写入【" + tc.Name + "】拓扑数据(" + icount + "/" + count + ")...");
                                        rowTopo.SetValue(rowTopo.FieldIndex("A_FacClass"), fcrPipeLine.FeatureClassId);
                                        rowTopo.SetValue(rowTopo.FieldIndex("Edge"), row.GetValue(0));
                                        rowTopo.SetValue(rowTopo.FieldIndex("Geometry"), row.GetValue(3));
                                        string newVal = "";
                                        if ((!row.IsNull(1) && !string.IsNullOrEmpty(row.GetValue(1).ToString())))
                                        {
                                            string temp = row.GetValue(1).ToString();
                                            object[] objArray;
                                            if (dictionary.TryGetValue(temp, out objArray))
                                            {
                                                rowTopo.SetValue(rowTopo.FieldIndex("PNode"), objArray[0]);
                                                rowTopo.SetValue(rowTopo.FieldIndex("P_FacClass"), objArray[1]);
                                                if (bTerrainHeight)
                                                {
                                                    row.SetValue(4, objArray[2]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            rowTopo.SetNull(rowTopo.FieldIndex("PNode"));
                                            rowTopo.SetNull(rowTopo.FieldIndex("P_FacClass"));
                                            newVal = newVal + "未连接前点;";
                                        }
                                        if ((!row.IsNull(2) && !string.IsNullOrEmpty(row.GetValue(2).ToString())))
                                        {
                                            string temp = row.GetValue(2).ToString();
                                            object[] objArray;
                                            if (dictionary.TryGetValue(temp, out objArray))
                                            {
                                                rowTopo.SetValue(rowTopo.FieldIndex("ENode"), objArray[0]);
                                                rowTopo.SetValue(rowTopo.FieldIndex("E_FacClass"), objArray[1]);
                                                if (bTerrainHeight)
                                                {
                                                    row.SetValue(5, objArray[2]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            rowTopo.SetNull(rowTopo.FieldIndex("ENode"));
                                            rowTopo.SetNull(rowTopo.FieldIndex("E_FacClass"));
                                            newVal = newVal + "未连接后;";
                                        }
                                        rowTopo.SetValue(rowTopo.FieldIndex("Topo_Error"), newVal);
                                        rowTopo.SetValue(rowTopo.FieldIndex("LaseUpdate"), DateTime.Now);
                                        if (bTerrainHeight)
                                        {
                                            cursor.UpdateRow(row);
                                        }
                                        cursorTopo.InsertRow(rowTopo);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                WaitForm.SetCaption("重建【" + tc.Name + "】拓扑信息空间索引...");
                IGridIndexInfo indexInfo = fcTopo.CalculateDefaultGridIndex("Geometry");
                if (indexInfo != null)
                {
                    fcTopo.AddSpatialIndex(indexInfo);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("【" + tc.Name + "】创建拓扑信息出错。\r\n错误信息：" + ex.Message, "提示");
            }
            finally
            {
                WaitForm.Stop();
            }
        }

        public void CreateTopoBySpatial(TopoClass tc, List<FacClass> listFCs, bool bTerrainHeight)
        {
            if (tc == null || listFCs == null || listFCs.Count < 2) return;
            IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
            if (fds == null) return;
            IFeatureClass fcTopo = fds.OpenFeatureClass(tc.TopoTable);
            if (fcTopo == null) return;
            try
            { // 删除空间索引和数据
                WaitForm.Start("根据空间建拓扑，未开始...", "请稍候", new Size(340, 50));
                WaitForm.SetCaption("删除【" + tc.Name + "】拓扑信息空间索引...");
                if (fcTopo.GetSpatialIndexInfos() != null && fcTopo.GetSpatialIndexInfos().Count > 0) fcTopo.DeleteSpatialIndex("Geometry");
                WaitForm.SetCaption("删除【" + tc.Name + "】拓扑数据...");
                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                fcTopo.Delete(filter);

                string facCCodes = "'";
                foreach (FacClass facc in listFCs)
                {
                    facCCodes += facc.Code + "','";
                }
                facCCodes = facCCodes.Substring(0, facCCodes.Length - 2);
                List<FacClassReg> listFCRs = GetFacClassRegByCodes(facCCodes);
                if (listFCRs == null || listFCRs.Count == 0) return;
                List<FacClassReg> listPipeNode = new List<FacClassReg>();
                List<FacClassReg> listPipeLine = new List<FacClassReg>();
                foreach (FacClassReg fcr in listFCRs)
                {
                    if (fcr.FacilityType.Name == "PipeLine")
                    {
                        listPipeLine.Add(fcr);
                    }
                    else if (fcr.FacilityType.Name == "PipeNode")
                    {
                        listPipeNode.Add(fcr);
                    }
                }
                if ((listPipeNode.Count == 0) || (listPipeLine.Count == 0)) return;

                #region 管点信息
                WaitForm.SetCaption("建立【" + tc.Name + "】拓扑信息管点临时数据库...");
                IFeatureClass fcTopoTemp = CreateTopoTempFeatureClass();
                if (fcTopoTemp == null) return;
                foreach (FacClassReg fcrPipeNode in listPipeNode)
                {
                    string[] array = null;
                    if (!bTerrainHeight)
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("Mark");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("DetectNo");
                        if (fi != null) array = new string[] { fi.Name, fi1.Name };
                    }
                    else
                    {
                        DFDataConfig.Class.FieldInfo fi0 = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("Mark");
                        DFDataConfig.Class.FieldInfo fi = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("DetectNo");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeNode.FacilityType.GetFieldInfoBySystemName("SurfHeight");
                        if (fi != null && fi1 != null) array = new string[] { fi0.Name, fi.Name, fi1.Name };
                    }
                    if (array != null)
                    {
                        IFeatureDataSet fdsFCR = this._dsPipe.OpenFeatureDataset(fcrPipeNode.DataSetName);
                        if (fdsFCR != null)
                        {
                            IFeatureClass fcPipeNode = fdsFCR.OpenFeatureClass(fcrPipeNode.FcName);
                            if (fcPipeNode != null)
                            {
                                IQueryFilter filterPipeNode = new QueryFilterClass();
                                if (!bTerrainHeight)
                                {
                                    filterPipeNode.SubFields = string.Format("oid,{0},Shape,StyleId,{1}", array[1], array[0]);
                                }
                                else
                                {
                                    filterPipeNode.SubFields = string.Format("oid,{0},Shape,StyleId,{1},{2}", array[1], array[0], array[2]);
                                }
                                IFdeCursor cursorPipeNode = fcPipeNode.Search(filterPipeNode, true);
                                int count = fcPipeNode.GetCount(filterPipeNode);
                                int icount = 0;
                                IRowBuffer row = null;
                                IFdeCursor cursorTemp = null;
                                while ((row = cursorPipeNode.NextRow()) != null)
                                {
                                    icount++;
                                    WaitForm.SetCaption("建立【" + tc.Name + "】管点临时数据库(" + icount + "/" + count + ")...");
                                    if (!row.IsNull(2))
                                    {

                                        IRowBuffer rowTemp = fcTopoTemp.CreateRowBuffer();
                                        cursorTemp = fcTopoTemp.Insert();
                                        rowTemp.SetValue(1, row.GetValue(1));
                                        rowTemp.SetValue(2, row.GetValue(0));
                                        rowTemp.SetValue(3, fcrPipeNode.FeatureClassId);
                                        rowTemp.SetValue(5, row.GetValue(2));
                                        rowTemp.SetValue(6, row.GetValue(3));
                                        rowTemp.SetValue(7, row.GetValue(4));
                                        if (bTerrainHeight)
                                        {
                                            rowTemp.SetValue(4, row.GetValue(5));
                                        }
                                        cursorTemp.InsertRow(rowTemp);
                                    }

                                }
                            }
                        }
                    }
                }
                if (fcTopoTemp.GetCount(null) == 0) return;
                try
                {
                    try
                    {
                        fcTopoTemp.DeleteSpatialIndex("Geometry");
                    }
                    catch (Exception e1)
                    {
                    }
                    IGridIndexInfo indexInfo = fcTopoTemp.CalculateDefaultGridIndex("Geometry");
                    if (indexInfo != null) fcTopoTemp.AddSpatialIndex(indexInfo);
                }
                catch (Exception e1)
                {
                }
                #endregion

                #region topo信息
                IRowBuffer rowTopo = fcTopo.CreateRowBuffer();
                IFdeCursor cursorTopo = fcTopo.Insert();
                WaitForm.SetCaption("写入【" + tc.Name + "】拓扑数据...");
                foreach (FacClassReg fcrPipeLine in listPipeLine)
                {
                    string[] array = null;
                    if (!bTerrainHeight)
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("StartNo");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("EndNo");
                        if (fi != null && fi1 != null) array = new string[] { fi.Name, fi1.Name };
                    }
                    else
                    {
                        DFDataConfig.Class.FieldInfo fi = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("StartNo");
                        DFDataConfig.Class.FieldInfo fi1 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("EndNo");
                        DFDataConfig.Class.FieldInfo fi2 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("StartHeight");
                        DFDataConfig.Class.FieldInfo fi3 = fcrPipeLine.FacilityType.GetFieldInfoBySystemName("EndHeight");
                        if (fi != null && fi1 != null && fi2 != null && fi3 != null) array = new string[] { fi.Name, fi1.Name, fi2.Name, fi3.Name };
                    }
                    if (array != null)
                    {
                        if (array != null)
                        {
                            IFeatureDataSet fdsFCR = this._dsPipe.OpenFeatureDataset(fcrPipeLine.DataSetName);
                            if (fdsFCR != null)
                            {
                                IFeatureClass fcPipeLine = fdsFCR.OpenFeatureClass(fcrPipeLine.FcName);
                                if (fcPipeLine != null)
                                {
                                    IQueryFilter filter1 = new QueryFilterClass();
                                    if (!bTerrainHeight)
                                    {
                                        filter1.SubFields = string.Format("oid,{0},{1},Shape", array[0], array[1]);
                                    }
                                    else
                                    {
                                        filter1.SubFields = string.Format("oid,{0},{1},Shape,{2},{3}", new object[] { array[0], array[1], array[2], array[3] });
                                    }
                                    int count = fcPipeLine.GetCount(filter1);
                                    int icount = 0;
                                    IFdeCursor cursor = new FdeCursorClass();
                                    if (bTerrainHeight)
                                    {
                                        cursor = fcPipeLine.Update(filter1);
                                    }
                                    else
                                    {
                                        cursor = fcPipeLine.Search(filter1, true);
                                    }
                                    IRowBuffer row = null;
                                    while ((row = cursor.NextRow()) != null)
                                    {
                                        icount++;
                                        WaitForm.SetCaption("写入【" + tc.Name + "】拓扑数据(" + icount + "/" + count + ")...");

                                        if (row.IsNull(3) || row.GetValue(3) == null || !(row.GetValue(3) is ICurve))
                                        {

                                        }
                                        else
                                        {
                                            ICurve curve = row.GetValue(3) as ICurve;
                                            IGeometry geo = curve as IGeometry;
                                            rowTopo.SetValue(rowTopo.FieldIndex("Geometry"), geo.Clone2(gviVertexAttribute.gviVertexAttributeZ));
                                            rowTopo.SetValue(rowTopo.FieldIndex("A_FacClass"), fcrPipeLine.FeatureClassId);
                                            rowTopo.SetValue(rowTopo.FieldIndex("Edge"), row.GetValue(0));
                                            #region 管线起点
                                            string newVal = "";
                                            ISpatialFilter spatialFilter = new SpatialFilterClass()
                                            {
                                                SpatialRel = gviSpatialRel.gviSpatialRelIntersects,
                                                GeometryField = "Geometry",
                                                Geometry = (curve.StartPoint as ITopologicalOperator2D).Buffer2D(tc.Tolerance, gviBufferStyle.gviBufferCapround) as IPolygon
                                            };
                                            IFdeCursor cursorSpatial = fcTopoTemp.Search(spatialFilter, false);
                                            int iMarkIndex = fcTopoTemp.GetFields().IndexOf("Mark");
                                            IRowBuffer rowSpatial = null;
                                            IRowBuffer buffer4 = null;
                                            IRowBuffer buffer5 = null;
                                            double num5 = 99999.0;
                                            double num6 = 99999.0;
                                            while ((rowSpatial = cursorSpatial.NextRow()) != null)
                                            {
                                                if (!rowSpatial.IsNull(iMarkIndex) && rowSpatial.GetValue(iMarkIndex).ToString().Contains("重复"))
                                                {
                                                    continue;
                                                }
                                                IPoint point = rowSpatial.GetValue(5) as IPoint;
                                                double dis = this.DisP2P(curve.StartPoint, point, tc.IgnoreZ);
                                                if ((dis < num5) && (tc.IgnoreZ || (Math.Abs((double)(curve.StartPoint.Z - point.Z)) <= tc.ToleranceZ)))
                                                {
                                                    num5 = dis;
                                                    buffer4 = rowSpatial;
                                                }
                                                //// 梅钢数据中井在一般点的附近,需挑选出来,由于井的Z值与线顶点坐标的Z值差距大,需从二维比较
                                                if (fcrPipeLine.LocationType == LocationType.UnderGround)
                                                {
                                                    string styleid = rowSpatial.GetValue(6).ToString();
                                                    if (!string.IsNullOrEmpty(styleid) && styleid != "-1")
                                                    {
                                                        double dis1 = this.DisP2P(curve.StartPoint, point, true);
                                                        if (dis1 < num6)
                                                        {
                                                            buffer5 = rowSpatial;
                                                            num6 = dis1;
                                                        }
                                                    }
                                                }

                                            }
                                            if (buffer4 != null || buffer5 != null)
                                            {
                                                if (buffer5 != null)
                                                {
                                                    row.SetValue(1, buffer5.GetValue(1));
                                                    if (bTerrainHeight)
                                                    {
                                                        row.SetValue(4, buffer5.GetValue(4));
                                                    }
                                                    rowTopo.SetValue(rowTopo.FieldIndex("PNode"), buffer5.GetValue(2));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("P_FacClass"), buffer5.GetValue(3));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("PDis"), num6);
                                                }
                                                else
                                                {
                                                    row.SetValue(1, buffer4.GetValue(1));
                                                    if (bTerrainHeight)
                                                    {
                                                        row.SetValue(4, buffer4.GetValue(4));
                                                    }
                                                    rowTopo.SetValue(rowTopo.FieldIndex("PNode"), buffer4.GetValue(2));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("P_FacClass"), buffer4.GetValue(3));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("PDis"), num5);
                                                }
                                            }
                                            else
                                            {
                                                rowTopo.SetNull(rowTopo.FieldIndex("PNode"));
                                                rowTopo.SetNull(rowTopo.FieldIndex("P_FacClass"));
                                                rowTopo.SetNull(rowTopo.FieldIndex("PDis"));
                                                newVal = newVal + "未连接前点;";
                                            }
                                            #endregion

                                            #region 管线终点
                                            spatialFilter = new SpatialFilterClass()
                                            {
                                                SpatialRel = gviSpatialRel.gviSpatialRelIntersects,
                                                GeometryField = "Geometry",
                                                Geometry = (curve.EndPoint as ITopologicalOperator2D).Buffer2D(tc.Tolerance, gviBufferStyle.gviBufferCapround) as IPolygon
                                            };
                                            cursorSpatial = fcTopoTemp.Search(spatialFilter, false);
                                            buffer4 = null;
                                            buffer5 = null;
                                            num5 = 99999.0;
                                            num6 = 99999.0;
                                            while ((rowSpatial = cursorSpatial.NextRow()) != null)
                                            {
                                                if (!rowSpatial.IsNull(iMarkIndex) && rowSpatial.GetValue(iMarkIndex).ToString().Contains("重复"))
                                                {
                                                    continue;
                                                }
                                                IPoint point = rowSpatial.GetValue(5) as IPoint;
                                                double dis = this.DisP2P(curve.EndPoint, point, tc.IgnoreZ);
                                                if ((dis < num5) && (tc.IgnoreZ || (Math.Abs((double)(curve.EndPoint.Z - point.Z)) <= tc.ToleranceZ)))
                                                {
                                                    num5 = dis;
                                                    buffer4 = rowSpatial;
                                                }
                                                //// 梅钢数据中井在一般点的附近,需挑选出来,由于井的Z值与线顶点坐标的Z值差距大,需从二维比较 
                                                if (fcrPipeLine.LocationType == LocationType.UnderGround)
                                                {
                                                    string styleid = rowSpatial.GetValue(6).ToString();
                                                    if (!string.IsNullOrEmpty(styleid) && styleid != "-1")
                                                    {
                                                        double dis1 = this.DisP2P(curve.EndPoint, point, true);
                                                        if (dis1 < num6)
                                                        {
                                                            buffer5 = rowSpatial;
                                                            num6 = dis1;
                                                        }
                                                    }
                                                }

                                            }
                                            if (buffer4 != null || buffer5 != null)
                                            {
                                                if (buffer5 != null)
                                                {
                                                    row.SetValue(1, buffer5.GetValue(1));
                                                    if (bTerrainHeight)
                                                    {
                                                        row.SetValue(4, buffer5.GetValue(4));
                                                    }
                                                    rowTopo.SetValue(rowTopo.FieldIndex("ENode"), buffer5.GetValue(2));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("E_FacClass"), buffer5.GetValue(3));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("EDis"), num6);
                                                }
                                                else
                                                {
                                                    row.SetValue(1, buffer4.GetValue(1));
                                                    if (bTerrainHeight)
                                                    {
                                                        row.SetValue(4, buffer4.GetValue(4));
                                                    }
                                                    rowTopo.SetValue(rowTopo.FieldIndex("ENode"), buffer4.GetValue(2));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("E_FacClass"), buffer4.GetValue(3));
                                                    rowTopo.SetValue(rowTopo.FieldIndex("EDis"), num5);
                                                }
                                            }
                                            else
                                            {
                                                rowTopo.SetNull(rowTopo.FieldIndex("ENode"));
                                                rowTopo.SetNull(rowTopo.FieldIndex("E_FacClass"));
                                                rowTopo.SetNull(rowTopo.FieldIndex("EDis"));
                                                newVal = newVal + "未连接后点;";
                                            }
                                            rowTopo.SetValue(rowTopo.FieldIndex("Topo_Error"), newVal);
                                            rowTopo.SetValue(rowTopo.FieldIndex("LaseUpdate"), DateTime.Now);
                                            if (bTerrainHeight)
                                            {
                                                cursor.UpdateRow(row);
                                            }
                                            cursorTopo.InsertRow(rowTopo);
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                WaitForm.SetCaption("重建【" + tc.Name + "】拓扑信息空间索引...");
                IGridIndexInfo info2 = fcTopo.CalculateDefaultGridIndex("Geometry");
                if (info2 != null)
                {
                    fcTopo.AddSpatialIndex(info2);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("【" + tc.Name + "】创建拓扑信息出错。\r\n错误信息：" + ex.Message, "提示");
            }
            finally
            {
                WaitForm.Stop();
            }

        }

        private double DisP2P(IPoint pt1, IPoint pt2, bool ignoreZ)
        {
            if ((pt1 == null) || (pt2 == null))
            {
                return 9999999.999;
            }
            if (ignoreZ)
            {
                return Math.Sqrt(((pt2.X - pt1.X) * (pt2.X - pt1.X)) + ((pt2.Y - pt1.Y) * (pt2.Y - pt1.Y)));
            }
            return Math.Sqrt((((pt2.X - pt1.X) * (pt2.X - pt1.X)) + ((pt2.Y - pt1.Y) * (pt2.Y - pt1.Y))) + ((pt2.Z - pt1.Z) * (pt2.Z - pt1.Z)));
        }

        private IFeatureClass CreateTopoTempFeatureClass()
        {
            try
            {
                IFeatureDataSet fds = this._dsTemp.OpenFeatureDataset("FeatureDataSet");
                if (fds == null) return null;
                string[] namesByType = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                if ((namesByType != null) && (Array.IndexOf<string>(namesByType, "TopoTempFc") > -1))
                {
                    IFeatureClass fc = fds.OpenFeatureClass("TopoTempFc");
                    if (fc != null)
                    {
                        fc.Truncate();
                        return fc;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    IFieldInfoCollection fields = new FieldInfoCollectionClass();
                    IFieldInfo newVal = null;
                    newVal = new FieldInfoClass
                    {
                        Name = "EXP_NO",
                        Alias = "物探点号",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 50
                    };
                    fields.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FeatureId",
                        Alias = "要素ID",
                        FieldType = gviFieldType.gviFieldInt32
                    };
                    fields.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "FacClassId",
                        Alias = "设施类ID",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 80
                    };
                    fields.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "SurfH",
                        Alias = "地面高程",
                        FieldType = gviFieldType.gviFieldDouble
                    };
                    fields.Add(newVal);
                    newVal = new FieldInfoClass
                    {
                        Name = "Geometry",
                        Alias = "空间列",
                        RegisteredRenderIndex = true,
                        FieldType = gviFieldType.gviFieldGeometry
                    };
                    IGeometryDef def = new GeometryDefClass
                    {
                        GeometryColumnType = gviGeometryColumnType.gviGeometryColumnPoint,
                        HasZ = true
                    };
                    newVal.GeometryDef = def;
                    fields.Add(newVal);
                    newVal = new FieldInfoClass         
                    {
                        Name = "StyleId",
                        Alias = "风格编号",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 50
                    };
                    fields.Add(newVal);
                    newVal = new FieldInfoClass      
                    {
                        Name = "Mark",
                        Alias = "备注",
                        FieldType = gviFieldType.gviFieldString,
                        Length = 50
                    };
                    fields.Add(newVal);
                    return fds.CreateFeatureClass("TopoTempFc", fields);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void DeleteTopoTempFeatureClass()
        {
            try
            {
                IFeatureDataSet fds = this._dsTemp.OpenFeatureDataset("FeatureDataSet");
                if (fds == null) return;

                string[] namesByType = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                bool bDelete = false;
                if ((namesByType != null) && (Array.IndexOf<string>(namesByType, "TopoTempFc") > -1))
                {
                    bDelete = fds.DeleteByName("TopoTempFc");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
