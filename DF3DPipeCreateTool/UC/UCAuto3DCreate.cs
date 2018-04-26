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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Gvitech.CityMaker.Common;
using System.IO;
using DFDataConfig.Class;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.FdeGeometry;
using DF3DPipeCreateTool.ParamModeling;
using DFCommon.Class;
using DFWinForms.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCAuto3DCreate : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private CheckEdit chkSBackhind;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTreeList.TreeList treeFacStyle;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private CheckEdit chkSelctAllDefault;
        private SimpleButton btnConfirm;
        private CheckEdit chkSelOrUnAll;
        private CheckEdit chkEBackhind;
        private CheckEdit chkUpdateModel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private SimpleButton btnSearch;
        private TextEdit txtFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAuto3DCreate));
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkSBackhind = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtFilter = new DevExpress.XtraEditors.TextEdit();
            this.treeFacStyle = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.chkSelctAllDefault = new DevExpress.XtraEditors.CheckEdit();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.chkSelOrUnAll = new DevExpress.XtraEditors.CheckEdit();
            this.chkEBackhind = new DevExpress.XtraEditors.CheckEdit();
            this.chkUpdateModel = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSBackhind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeFacStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelctAllDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelOrUnAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEBackhind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUpdateModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chkSBackhind;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8 ";
            this.layoutControlItem8.Location = new System.Drawing.Point(225, 0);
            this.layoutControlItem8.Name = "layoutControlItem8 ";
            this.layoutControlItem8.Size = new System.Drawing.Size(143, 23);
            this.layoutControlItem8.Text = "layoutControlItem8 ";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // chkSBackhind
            // 
            this.chkSBackhind.EditValue = true;
            this.chkSBackhind.Location = new System.Drawing.Point(227, 2);
            this.chkSBackhind.Name = "chkSBackhind";
            this.chkSBackhind.Properties.Caption = "管线起点退让";
            this.chkSBackhind.Size = new System.Drawing.Size(139, 19);
            this.chkSBackhind.StyleController = this.layoutControl1;
            this.chkSBackhind.TabIndex = 12;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.txtFilter);
            this.layoutControl1.Controls.Add(this.treeFacStyle);
            this.layoutControl1.Controls.Add(this.chkSelctAllDefault);
            this.layoutControl1.Controls.Add(this.btnConfirm);
            this.layoutControl1.Controls.Add(this.chkSBackhind);
            this.layoutControl1.Controls.Add(this.chkSelOrUnAll);
            this.layoutControl1.Controls.Add(this.chkEBackhind);
            this.layoutControl1.Controls.Add(this.chkUpdateModel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(521, 450);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(194, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(26, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(5, 25);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(185, 22);
            this.txtFilter.StyleController = this.layoutControl1;
            this.txtFilter.TabIndex = 14;
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            // 
            // treeFacStyle
            // 
            this.treeFacStyle.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeFacStyle.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeFacStyle.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.treeFacStyle.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeFacStyle.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeFacStyle.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeFacStyle.Location = new System.Drawing.Point(5, 51);
            this.treeFacStyle.Name = "treeFacStyle";
            this.treeFacStyle.OptionsBehavior.Editable = false;
            this.treeFacStyle.OptionsView.ShowCheckBoxes = true;
            this.treeFacStyle.OptionsView.ShowColumns = false;
            this.treeFacStyle.OptionsView.ShowRoot = false;
            this.treeFacStyle.Size = new System.Drawing.Size(215, 348);
            this.treeFacStyle.TabIndex = 0;
            this.treeFacStyle.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeFacStyle_AfterCheckNode);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "设施类风格";
            this.treeListColumn1.FieldName = "设施类风格";
            this.treeListColumn1.MinWidth = 32;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // chkSelctAllDefault
            // 
            this.chkSelctAllDefault.Location = new System.Drawing.Point(5, 403);
            this.chkSelctAllDefault.Name = "chkSelctAllDefault";
            this.chkSelctAllDefault.Properties.Caption = "选择\\取消选择默认风格";
            this.chkSelctAllDefault.Size = new System.Drawing.Size(215, 19);
            this.chkSelctAllDefault.StyleController = this.layoutControl1;
            this.chkSelctAllDefault.TabIndex = 9;
            this.chkSelctAllDefault.CheckedChanged += new System.EventHandler(this.chkSelctAllDefault_CheckedChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(393, 48);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(116, 22);
            this.btnConfirm.StyleController = this.layoutControl1;
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chkSelOrUnAll
            // 
            this.chkSelOrUnAll.Location = new System.Drawing.Point(5, 426);
            this.chkSelOrUnAll.Name = "chkSelOrUnAll";
            this.chkSelOrUnAll.Properties.Caption = "全选\\取消全选";
            this.chkSelOrUnAll.Size = new System.Drawing.Size(215, 19);
            this.chkSelOrUnAll.StyleController = this.layoutControl1;
            this.chkSelOrUnAll.TabIndex = 10;
            this.chkSelOrUnAll.CheckedChanged += new System.EventHandler(this.chkSelOrUnAll_CheckedChanged);
            // 
            // chkEBackhind
            // 
            this.chkEBackhind.EditValue = true;
            this.chkEBackhind.Location = new System.Drawing.Point(370, 2);
            this.chkEBackhind.Name = "chkEBackhind";
            this.chkEBackhind.Properties.Caption = "管线终点退让";
            this.chkEBackhind.Size = new System.Drawing.Size(139, 19);
            this.chkEBackhind.StyleController = this.layoutControl1;
            this.chkEBackhind.TabIndex = 13;
            // 
            // chkUpdateModel
            // 
            this.chkUpdateModel.EditValue = true;
            this.chkUpdateModel.Location = new System.Drawing.Point(227, 25);
            this.chkUpdateModel.Name = "chkUpdateModel";
            this.chkUpdateModel.Properties.Caption = "增量更新（增量更新只更新新添加模型）";
            this.chkUpdateModel.Size = new System.Drawing.Size(282, 19);
            this.chkUpdateModel.StyleController = this.layoutControl1;
            this.chkUpdateModel.TabIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem8,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(521, 450);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "设施类风格列表";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem7});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(225, 450);
            this.layoutControlGroup2.Text = "设施类风格列表";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.treeFacStyle;
            this.layoutControlItem5.CustomizationFormText = "设施类风格列表";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(219, 352);
            this.layoutControlItem5.Text = "风格列表";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkSelOrUnAll;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 401);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(219, 23);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkSelctAllDefault;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 378);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(219, 23);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtFilter;
            this.layoutControlItem4.CustomizationFormText = "设施类查找：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(189, 26);
            this.layoutControlItem4.Text = "设施类查找：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSearch;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(189, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(511, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 450);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkUpdateModel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(225, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(286, 23);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnConfirm;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(391, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(120, 404);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(225, 46);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(166, 404);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.chkEBackhind;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(368, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(143, 23);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // UCAuto3DCreate
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCAuto3DCreate";
            this.Size = new System.Drawing.Size(521, 450);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSBackhind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeFacStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelctAllDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelOrUnAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEBackhind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUpdateModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        private string _nullText;
        private IDataSource _dsPipe;
        private IDataSource _dsTemplate;
        private List<TreeListNode> _defaultNodes = new List<TreeListNode>();
        private Dictionary<FacClassReg, List<FacStyleClass>> _dicStyles = new Dictionary<FacClassReg,List<FacStyleClass>>();   // 注册设施类+对应设施风格

        public UCAuto3DCreate()
        {
            InitializeComponent();
        }
        private List<FacClassReg> GetAllFacClassReg()
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
                filter.WhereClause = "1=1";

                cursor = oc.Search(filter, false);
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
        private List<FacStyleClass> GetFacStyleByFacClassCode(string fcCode)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {

                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityStyle");
                if (oc == null) return null;

                IQueryFilter filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacClassCode = '{0}'", fcCode)
                };
                cursor = oc.Search(filter, true);
                List<FacStyleClass> list = new List<FacStyleClass>();
                while ((row = cursor.NextRow()) != null)
                {
                    StyleType type;
                    FacStyleClass fs = null;
                    if (row.FieldIndex("StyleType") >= 0 && Enum.TryParse<StyleType>(row.GetValue(row.FieldIndex("StyleType")).ToString(), out type))
                    {
                        Dictionary<string, string> dictionary = null;
                        if (row.FieldIndex("StyleInfo") >= 0)
                        {
                            object obj = row.GetValue(row.FieldIndex("StyleInfo"));
                            if (obj != null)
                            {
                                IBinaryBuffer buffer2 = row.GetValue(row.FieldIndex("StyleInfo")) as IBinaryBuffer;
                                if (buffer2 != null)
                                {
                                    dictionary = JsonTool.JsonToObject<Dictionary<string, string>>(Encoding.UTF8.GetString(buffer2.AsByteArray()));
                                }
                            }
                        }
                        switch (type)
                        {
                            case StyleType.PipeNodeStyle:
                                fs = new PipeNodeStyleClass(dictionary);
                                break;
                            case StyleType.PipeLineStyle:
                                fs = new PipeLineStyleClass(dictionary);
                                break;
                            case StyleType.PipeBuildStyle:
                                fs = new PipeBuildStyleClass(dictionary);
                                break;
                        }
                    }
                    if (fs == null) continue;
                    if (row.FieldIndex("oid") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("oid"));
                        if (obj != null) fs.Id = int.Parse(obj.ToString());
                    }
                    if (row.FieldIndex("ObjectId") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("ObjectId"));
                        if (obj != null) fs.ObjectId = obj.ToString();
                    }
                    if (row.FieldIndex("FacClassCode") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("FacClassCode"));
                        if (obj != null) fs.FacClassCode = obj.ToString();
                    }
                    if (row.FieldIndex("Name") >= 0)
                    {
                        object obj = row.GetValue(row.FieldIndex("Name"));
                        if (obj != null) fs.Name = obj.ToString();
                    }
                    int index = row.FieldIndex("Thumbnail");
                    if (index != -1 && !row.IsNull(index))
                    {
                        IBinaryBuffer b = row.GetValue(index) as IBinaryBuffer;
                        if (row != null)
                        {
                            MemoryStream stream = new MemoryStream(b.AsByteArray());
                            fs.Thumbnail = Image.FromStream(stream);
                        }
                    }
                    list.Add(fs);
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
            //try
            //{
            //    ILicenseServer lic = new LicenseServer();
            //    long pval1;
            //    bool pval2;
            //    lic.InternalGetData(out pval1, out pval2);
            //    if (!pval2)
            //    {
            //        this.Enabled = false;
            //        XtraMessageBox.Show("此功能需要授权。", "提示");
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Enabled = false;
            //    XtraMessageBox.Show("此功能需要授权。", "提示");
            //    return;
            //}

            this.treeFacStyle.ClearNodes();
            try
            {
                this._dsPipe = DF3DPipeCreateApp.App.PipeLib;
                if (this._dsPipe == null) { this.Enabled = false; return; }
                this._dsTemplate = DF3DPipeCreateApp.App.TemplateLib;
                if (this._dsTemplate == null) { this.Enabled = false; return; }
                WaitForm.Start("正在加载数据...", "请稍后");
                this._nullText = "全部设施类";
                this.txtFilter.Properties.NullText = this._nullText;
                List<FacClassReg> list1 = GetAllFacClassReg();
                if (list1 != null)
                {
                    TreeListNode parentNode = null;
                    TreeListNode item = null;
                    foreach (FacClassReg reg in list1)
                    {
                        parentNode = this.treeFacStyle.AppendNode(new object[] { reg.Name }, (TreeListNode)null);
                        parentNode.Tag = reg;
                        List<FacStyleClass> facilityStyles = GetFacStyleByFacClassCode(reg.FacClassCode);
                        this._dicStyles.Add(reg, facilityStyles);
                        if (facilityStyles != null)
                        {
                            foreach (FacStyleClass style in facilityStyles)
                            {
                                item = this.treeFacStyle.AppendNode(new object[] { style.Name }, parentNode);
                                item.Tag = style;
                            }
                        }
                        if (reg.FacilityType.Name == "PipeNode")
                        {
                            item = this.treeFacStyle.AppendNode(new object[] { "默认风格" }, parentNode);
                            item.Tag = "-1";
                            this._defaultNodes.Add(item);
                        }
                        parentNode.Expanded = true;
                    }
                    this.treeFacStyle.FocusedNode = null;
                }
            }
            catch (Exception exception)
            {
            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private void CheckNodeState(TreeListNode curNode, int flag)
        {
            try
            {
                TreeListNode parentNode;
                TreeListNode nextNode;
                List<string> list = new List<string>();
                if (curNode != null)
                {
                    parentNode = null;
                    nextNode = null;
                    switch (flag)
                    {
                        case 0:
                            parentNode = curNode.ParentNode;
                            if (parentNode == null)
                            {
                                return;
                            }
                            nextNode = parentNode.FirstNode;
                            goto Label_005C;

                        case 1:
                            nextNode = curNode.FirstNode;
                            goto Label_0103;
                    }
                }
                return;
            Label_003F:
                list.Add(nextNode.CheckState.ToString());
                nextNode = nextNode.NextNode;
            Label_005C:
                if (nextNode != null)
                {
                    goto Label_003F;
                }
                if (list.IndexOf("Indeterminate") >= 0)
                {
                    parentNode.CheckState = CheckState.Indeterminate;
                }
                else if ((list.IndexOf("Checked") >= 0) && (list.IndexOf("Unchecked") == -1))
                {
                    parentNode.CheckState = CheckState.Checked;
                }
                else if ((list.IndexOf("Unchecked") >= 0) && (list.IndexOf("Checked") == -1))
                {
                    parentNode.CheckState = CheckState.Unchecked;
                }
                else
                {
                    parentNode.CheckState = CheckState.Indeterminate;
                }
                this.CheckNodeState(parentNode, 0);
                return;
            Label_00DA:
                if (nextNode.CheckState != curNode.CheckState)
                {
                    nextNode.CheckState = curNode.CheckState;
                }
                this.CheckNodeState(nextNode, 1);
                nextNode = nextNode.NextNode;
            Label_0103:
                if (nextNode != null)
                {
                    goto Label_00DA;
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void treeFacStyle_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                TreeListNode curNode = e.Node;
                if (curNode != null)
                {
                    this.CheckNodeState(curNode, 0);
                    this.CheckNodeState(curNode, 1);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void chkSelctAllDefault_CheckedChanged(object sender, EventArgs e)
        {
            if ((this._defaultNodes != null) && (this._defaultNodes.Count != 0))
            {
                foreach (TreeListNode node in this._defaultNodes)
                {
                    node.Checked = this.chkSelctAllDefault.Checked;
                }
            }
        }

        private void chkSelOrUnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.treeFacStyle.Nodes.Count != 0)
            {
                foreach (TreeListNode node in this.treeFacStyle.Nodes)
                {
                    node.Checked = this.chkSelOrUnAll.Checked;
                    this.treeFacStyle_AfterCheckNode(null, new NodeEventArgs(node));
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.treeFacStyle.Nodes.Clear();
            if ((this._dicStyles != null) && (this._dicStyles.Count != 0))
            {
                string text = this.txtFilter.Text;
                try
                {
                    FacClassReg key = null;
                    List<FacStyleClass> list = null;
                    TreeListNode parentNode = null;
                    TreeListNode item = null;
                    foreach (KeyValuePair<FacClassReg, List<FacStyleClass>> pair in this._dicStyles)
                    {
                        key = pair.Key;
                        if ((text == this._nullText) || key.Name.Contains(text))
                        {
                            list = pair.Value;
                            parentNode = this.treeFacStyle.AppendNode(new object[] { key.Name }, (TreeListNode)null);
                            parentNode.Tag = key;
                            if (list != null)
                            {
                                foreach (FacStyleClass style in list)
                                {
                                    item = this.treeFacStyle.AppendNode(new object[] { style.Name }, parentNode);
                                    item.Tag = style;
                                }
                            }
                            if (key.FacilityType.Name == "PipeNode")
                            {
                                item = this.treeFacStyle.AppendNode(new object[] { "默认风格" }, parentNode);
                                item.StateImageIndex = 2;
                                item.Tag = "-1";
                                this._defaultNodes.Add(item);
                            }
                            parentNode.Expanded = true;
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.btnSearch_Click(null, null);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int max = 0;
            int num2 = 0;
            FacClassReg key = null;
            IFeatureClass class2 = null;
            Dictionary<FacClassReg, List<object>> stylesInfo = null;   // 要素类+风格对象
            WaitForm.Start("启动管线三维化...", "请稍后", new Size(450, 50));
            try
            {
                if (this.treeFacStyle.Nodes.Count != 0)
                {
                    int count = 0;
                    FacStyleClass item = null;
                    List<object> list = null;
                    stylesInfo = new Dictionary<FacClassReg, List<object>>();
                    IQueryFilter filter = new QueryFilterClass();
                    foreach (TreeListNode node in this.treeFacStyle.Nodes)
                    {
                        key = node.Tag as FacClassReg;
                        if (key != null)
                        {
                            class2 = key.GetFeatureClass();
                            if ((class2 != null) && (node.Nodes.Count != 0))
                            {
                                list = new List<object>();
                                foreach (TreeListNode node2 in node.Nodes)
                                {
                                    if (!node2.Checked || (node2.Tag == null))
                                    {
                                        continue;
                                    }
                                    num2++;
                                    item = node2.Tag as FacStyleClass;

                                    if ((node2.Tag == "-1") || (item != null))
                                    {
                                        if (item != null)
                                        {
                                            filter.WhereClause = string.Format("StyleId = '{0}'", item.ObjectId.ToString());
                                            count = class2.GetCount(filter);
                                            node2.SetValue(0, string.Format("{0}({1})", item.Name, count));
                                            list.Add(item);
                                            WaitForm.SetCaption("统计【" + key.Name + "】【" + item.Name + "】风格记录数...");
                                        }
                                        else
                                        {
                                            filter.WhereClause = string.Format("StyleId = '{0}'", "-1");
                                            count = class2.GetCount(filter);
                                            node2.SetValue(0, string.Format("{0}({1})", "默认风格", count));
                                            list.Add(-1);
                                            WaitForm.SetCaption("统计【" + key.Name + "】【默认】风格记录数...");
                                        }
                                        if (count > 0)
                                        {
                                            max += count;
                                        }
                                    }
                                } 
                                if (list.Count > 0)
                                {
                                    stylesInfo.Add(key, list);
                                }
                            }
                        }
                    }
                    if (num2 == 0)
                    {
                        XtraMessageBox.Show("请勾选要三维化的设施类风格!", "提示");
                    }
                    else if (max == 0)
                    {
                        XtraMessageBox.Show("无数据需要三维化", "提示");
                    }
                    else if (DialogResult.Yes == XtraMessageBox.Show("是否开始对勾选的设施风格进行三维化？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        List<string> resultlist = UpdateFacilityModel(stylesInfo, this.chkUpdateModel.Checked, this.chkSBackhind.Checked, this.chkEBackhind.Checked);
                        XtraMessageBox.Show(string.Format("设施三维化完成，共：{0}，成功：{1}。", max, (resultlist == null) ? -1 : resultlist.Count), "提示");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            WaitForm.Stop();
        }

        private TopoClass GetTopoClassByObjectId(string objectId)
        {
            IFdeCursor o = null;
            IRowBuffer row = null;
            IQueryFilter filter = null;

            try
            {
                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_TopoManage");
                if (oc == null) return null;
                filter = new QueryFilterClass
                {
                    WhereClause = string.Format("ObjectId = '{0}'", objectId)
                };
                o = oc.Search(filter, true);
                row = o.NextRow();
                if (row != null)
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
                    return tc;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (row != null)
                {
                    Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }
        private TopoClass GetTopoClassByFacClassCode(string facClassCode)
        {
            IFdeCursor o = null;
            IRowBuffer buffer = null;
            IQueryFilter filter = null;
            try
            {
                IFeatureDataSet fds = this._dsTemplate.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return null;
                filter = new QueryFilterClass
                {
                    WhereClause = string.Format("Code = '{0}'", facClassCode),
                    SubFields = "TopoLayerId"
                };
                o = oc.Search(filter, true);
                buffer = o.NextRow();
                if (buffer != null)
                {
                    if (buffer.IsNull(0))
                    {
                        return null;
                    }
                    return GetTopoClassByObjectId(buffer.GetValue(0).ToString());
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (buffer != null)
                {
                    Marshal.ReleaseComObject(buffer);
                    buffer = null;
                }
            }
        }
        public List<string> GetFacClassCodesByTopoLayerId(string objectId)
        {
            if (DF3DPipeCreateApp.App.TemplateLib == null) return null;
            IFdeCursor o = null;
            IRowBuffer buffer = null;
            IQueryFilter filter = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.TemplateLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_Catalog");
                if (oc == null) return null;
                filter = new QueryFilterClass
                {
                    WhereClause = string.Format("FacilityType <> 'UnKnown' and TopoLayerId = '{0}'", objectId),
                    SubFields = "Code"
                };
                List<string> list = new List<string>();
                o = oc.Search(filter, true);
                while ((buffer = o.NextRow()) != null)
                {
                    if (!buffer.IsNull(0))
                    {
                        list.Add(buffer.GetValue(0).ToString());
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (buffer != null)
                {
                    Marshal.ReleaseComObject(buffer);
                    buffer = null;
                }
            }
        }
        private List<FacClassReg> GetFacClassRegByTopoClass(TopoClass tc)
        {
            if (tc == null) return null;
            List<string> list = GetFacClassCodesByTopoLayerId(tc.ObjectId);
            if (list == null || list.Count == 0) return null;

            IFdeCursor o = null;
            IRowBuffer row = null;
            IQueryFilter filter = null;
            try
            {
                IFeatureDataSet fds = this._dsPipe.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return null;
                IObjectClass oc = fds.OpenObjectClass("OC_FacilityClass");
                if (oc == null) return null;
                string str = "";
                foreach(string temp in list)
                {
                    str += "'" + temp + "',";
                }
                str = str.Remove(str.Length - 1);
                filter = new QueryFilterClass()
                {
                    WhereClause = string.Format("FacClassCode in ({0})", str)
                };
                o = oc.Search(filter, true);
                List<FacClassReg> list1 = new List<FacClassReg>();
                while ((row = o.NextRow()) != null)
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
                    list1.Add(fc);
                }
                return list1;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (row != null)
                {
                    Marshal.ReleaseComObject(row);
                    row = null;
                }
            }
        }

        private bool RebuildIndex(FacClassReg facClassReg)
        {
            IFeatureClass class2 = null;
            IFieldInfoCollection fields = null;
            IGridIndexInfo indexInfo = null;
            IRenderIndexInfo info2 = null;
            string[] strArray = new string[] { "Geometry", "Shape", "FootPrint" };
            try
            {
                class2 = facClassReg.GetFeatureClass();
                if (class2 == null)
                {
                    return false;
                }
                fields = class2.GetFields();
                foreach (string str in strArray)
                {
                    if (fields.IndexOf(str) != -1)
                    {
                        try
                        {
                            indexInfo = class2.CalculateDefaultGridIndex(str);
                            if (indexInfo != null)
                            {
                                class2.AddSpatialIndex(indexInfo);
                            }
                        }
                        catch (Exception exception)
                        {
                        }
                        try
                        {
                            info2 = class2.CalculateDefaultRenderIndex(str);
                            if (info2 != null)
                            {
                                class2.AddRenderIndex(info2);
                            }
                        }
                        catch (Exception exception2)
                        {
                        }
                    }
                }
                return true;
            }
            catch (Exception exception3)
            {
                return false;
            }
        }
        private List<string> UpdateFacilityModel(Dictionary<FacClassReg, List<object>> stylesInfo, bool isIncrease, bool isSBackhind, bool isEBackhind)
        {
            List<string> list = new List<string>();
            FacClassReg facClassReg = null;
            Fac fac = null;
            FacStyleClass style = null;
            string objectId = "";
            TopoClass topoClass = null;
            IFeatureClass fc = null;
            IFeatureDataSet dataSet = null;
            IResourceManager manager = null;
            IQueryFilter filter = null;
            IFdeCursor o = null;
            IRowBuffer r = null;
            string item = "";
            object obj2 = null;
            IGeometry geometry = null;
            IModelPoint mp = null;
            IModel finemodel = null;
            IModel simplemodel = null;
            string name = "";
            try
            {
                foreach (KeyValuePair<FacClassReg, List<object>> pair in stylesInfo)
                {
                    facClassReg = pair.Key;
                    if (facClassReg == null) continue;
                    WaitForm.SetCaption("正在进行【" + facClassReg.Name + "】三维化...");
                    
                    fc = facClassReg.GetFeatureClass();
                    if (fc == null) continue;
                    dataSet = fc.FeatureDataSet;
                    manager = dataSet as IResourceManager;
                    topoClass = GetTopoClassByFacClassCode(facClassReg.FacClassCode);

                    foreach (object obj3 in pair.Value)
                    {
                        #region 获取风格
                        style = null;
                        string styleName = "";
                        if (!obj3.Equals(-1))
                        {
                            style = obj3 as FacStyleClass;
                            if (style == null) continue;
                            objectId = style.ObjectId.ToString();
                            styleName = style.Name;
                        }
                        else
                        {
                            objectId = "-1";
                            styleName = "默认风格";
                            if (facClassReg.FacilityType.Name == "PipeNode")
                            {
                                if (topoClass == null) continue;
                                List<FacClassReg> facilityClassReg = GetFacClassRegByTopoClass(topoClass);
                                if (facilityClassReg == null) continue;
                                foreach (FacClassReg reg2 in facilityClassReg)
                                {
                                    if (reg2.FacilityType.Name == "PipeLine")
                                    {
                                        List<FacStyleClass> facilityStyles = GetFacStyleByFacClassCode(reg2.FacClassCode);
                                        if ((facilityStyles != null) && (facilityStyles.Count > 0))
                                        {
                                            style = facilityStyles[0];
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (style == null)
                        {
                            continue;
                        }
                        else
                        {
                            style.InitResource();// 初始化资源
                        }
                        #endregion
                        WaitForm.SetCaption("正在进行【" + facClassReg.Name + "】【" + styleName + "】风格的三维化...");

                        #region 风格中的图片加入纹理库
                        if (style.Images != null)
                        {
                            string[] allKeys = style.Images.GetAllKeys();
                            if ((allKeys != null) && (allKeys.Length > 0))
                            {
                                foreach (string str6 in allKeys)
                                {
                                    try
                                    {
                                        IImage property = style.Images.GetProperty(str6) as IImage;
                                        if (property != null)
                                        {
                                            if (manager.ImageExist(str6))
                                            {
                                                manager.UpdateImage(str6, property);
                                            }
                                            else
                                            {
                                                manager.AddImage(str6, property);
                                            }
                                            //DrawGeometry.Ocx.RefreshImage(dataSet, str6);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        #endregion
                        filter = new QueryFilterClass();
                        filter.WhereClause = string.Format("{0} = '{1}'", new object[] { "StyleId", objectId });
                        try
                        {
                            this._dsPipe.StartEditing();
                        }
                        catch
                        {
                            this._dsPipe.StopEditing(false);
                            this._dsPipe.StartEditing();
                        }
                        finally
                        {
                            o = fc.Update(filter);
                        }
                        int num2 = 0;
                        int successcount = 0;
                        int count = fc.GetCount(filter);
                        while ((r = o.NextRow()) != null)
                        {
                            num2++;
                            try
                            {
                                #region 重建几何模型
                                item = r.GetValue(0).ToString();
                                int geoindex = fc.GetFields().IndexOf("Geometry");
                                if (geoindex == -1) continue;
                                obj2 = r.GetValue(geoindex);
                                if (((obj2 != null) && ((geometry = (IGeometry)obj2) != null)) && ((mp = geometry as IModelPoint) != null))
                                {
                                    if (isIncrease)
                                    {
                                        continue;
                                    }
                                }
                                switch (facClassReg.FacilityType.Name)
                                {
                                    case "PipeNode":
                                        fac = new PipeNodeFac(facClassReg, style, r, topoClass);
                                        break;
                                    case "PipeLine":
                                        fac = new PipeLineFac(facClassReg, style, r, topoClass, isSBackhind, isEBackhind);
                                        if (topoClass != null)
                                        {
                                            IObjectClass ocTopo = fac.Reg.GetFeatureClass();
                                            IQueryFilter ocFilter = new QueryFilterClass
                                            {
                                                WhereClause = string.Format("A_FacClass = '{0}' and Edge = {1}", fac.Reg.FeatureClassId, fac.FeatureId),
                                                SubFields = "oid,P_FacClass,PNode,P_IsFusu,E_FacClass,ENode,E_IsFusu"
                                            };
                                            IRowBuffer ocRow = ocTopo.Search(ocFilter, true).NextRow();
                                            if (ocRow != null)
                                            {
                                                if (!ocRow.IsNull(3))
                                                {
                                                    string styleID = ocRow.GetValue(3).ToString();
                                                    if (styleID.Equals("no"))
                                                    {
                                                        (fac as PipeLineFac).IsSBackhind = true;
                                                    }
                                                    else
                                                    {
                                                        (fac as PipeLineFac).IsSBackhind = false;
                                                    }
                                                }
                                                else
                                                {
                                                    (fac as PipeLineFac).IsSBackhind = false;
                                                }

                                                if (!ocRow.IsNull(6))
                                                {
                                                    string styleID = ocRow.GetValue(6).ToString();
                                                    if (styleID.Equals("no"))
                                                    {
                                                        (fac as PipeLineFac).IsEBackhind = true;
                                                    }
                                                    else
                                                    {
                                                        (fac as PipeLineFac).IsEBackhind = false;
                                                    }
                                                }
                                                else
                                                {
                                                    (fac as PipeLineFac).IsEBackhind = false;
                                                }
                                            }
                                            else
                                            {
                                                (fac as PipeLineFac).IsSBackhind = isSBackhind;
                                                (fac as PipeLineFac).IsEBackhind = isEBackhind;
                                            }
                                        }
                                        else
                                        {
                                            (fac as PipeLineFac).IsSBackhind = isSBackhind;
                                            (fac as PipeLineFac).IsEBackhind = isEBackhind;
                                        }
                                        break;
                                    case "PipeBuild":
                                    case "PipeBuild1":
                                        fac = new PipeBuildFac(facClassReg, style, r);
                                        break;
                                }                                
                                if (!RebuildModel(fac, style, out mp, out finemodel, out simplemodel, out name))
                                {
                                    Console.WriteLine("RebuildModel Error:" + (string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName) + "(" + item + ")");
                                    if ((num2 != 0) && ((num2 % 0x1388) == 0))
                                    {
                                        this._dsPipe.StopEditing(true);
                                        this._dsPipe.StartEditing();
                                        filter.ResultBeginIndex = num2;
                                        filter.ResultLimit = 0x1388;
                                        o = fc.Update(filter);
                                    }
                                    if (num2 == count)
                                    {
                                        this._dsPipe.StopEditing(true);
                                        break;
                                    }
                                    continue;
                                }
                                #endregion

                                #region 模型加入数据库
                                if (finemodel == null) continue;
                                mp.ModelEnvelope = finemodel.Envelope;
                                r.SetValue(geoindex, mp);
                                int mnindex = fc.GetFields().IndexOf("ModelName");
                                if (mnindex != -1) r.SetValue(mnindex, mp.ModelName);
                                o.UpdateRow(r);
                                if (!string.IsNullOrEmpty(mp.ModelName))
                                {
                                    if (!manager.ModelExist(mp.ModelName))
                                    {
                                        if (manager.AddModel(mp.ModelName, finemodel, simplemodel))
                                        {
                                            list.Add(item);
                                            successcount++;
                                            if ((num2 != 0) && ((num2 % 0x1388) == 0))
                                            {
                                                this._dsPipe.StopEditing(true);
                                                this._dsPipe.StartEditing();
                                                filter.ResultBeginIndex = num2;
                                                filter.ResultLimit = 0x1388;
                                                o = fc.Update(filter);
                                            }
                                            if (num2 == count)
                                            {
                                                this._dsPipe.StopEditing(true);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (manager.UpdateModel(mp.ModelName, finemodel) && manager.UpdateSimplifiedModel(mp.ModelName, simplemodel))
                                        {
                                            list.Add(item);
                                            successcount++;
                                            if ((num2 != 0) && ((num2 % 0x1388) == 0))
                                            {
                                                this._dsPipe.StopEditing(true);
                                                this._dsPipe.StartEditing();
                                                filter.ResultBeginIndex = num2;
                                                filter.ResultLimit = 0x1388;
                                                o = fc.Update(filter);
                                            }
                                            if (num2 == count)
                                            {
                                                this._dsPipe.StopEditing(true);
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            catch (Exception ex) { }
                            finally
                            {
                            }
                            WaitForm.SetCaption("正在进行【" + facClassReg.Name + "】【" + styleName + "】风格(" + successcount + "/" + num2 + "/" + count + ")的三维化...");
                        }
                        if (r != null)
                        {
                            Marshal.ReleaseComObject(r);
                            r = null;
                        }
                        if (o != null)
                        {
                            Marshal.ReleaseComObject(o);
                            o = null;
                        }
                        RebuildIndex(facClassReg);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (this._dsPipe != null && this._dsPipe.IsEditing) this._dsPipe.StopEditing(true);
               
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                if (r != null)
                {
                    Marshal.ReleaseComObject(r);
                    r = null;
                }
            }
            return list;
        }

        #region 重建模型
        public static bool RebuildModel(Fac fac, FacStyleClass style, out IModelPoint mp, out IModel finemodel, out IModel simplemodel, out string name)
        {
            mp = null;
            finemodel = null;
            simplemodel = null;
            name = "";
            if (fac.Reg.FacilityType.Name == "PipeNode")
            {
                if (fac.Reg.LocationType == LocationType.UnderGround)
                {
                    return RebuildModelUnderGround((PipeNodeFac)fac, style, out mp, out finemodel, out simplemodel, out name);
                }
                else if (fac.Reg.LocationType == LocationType.OverHead)
                {
                    return RebuildModelOverHead((PipeNodeFac)fac, style, out mp, out finemodel, out simplemodel, out name);
                }
            }
            if (fac.Reg.FacilityType.Name == "PipeLine")
            {
                return RebuildPipeLineModel((PipeLineFac)fac, style, out mp, out finemodel, out simplemodel, out name);
            }
            if (fac.Reg.FacilityType.Name ==  "PipeBuild")
            {
                return RebuildPipeBuildModel1((PipeBuildFac)fac, style, out mp, out finemodel, out simplemodel, out name);
            }
            if (fac.Reg.FacilityType.Name == "PipeBuild1")
            {
                return RebuildPipeBuildModel((PipeBuildFac)fac, style, out mp, out finemodel, out simplemodel, out name);
            }
            return false;
        }

        private static bool RebuildModelUnderGround(PipeNodeFac fac, FacStyleClass style, out IModelPoint mp, out IModel finemodel, out IModel simplemodel, out string name)
        {
            bool flag = false;
            mp = null;
            finemodel = null;
            simplemodel = null;
            name = "未知";
            if (fac == null) return false;
            int num = 0;
            IPipeSection[] sections = null;
            Vector[][] vtx = null;
            Vector[] vectorArray2 = null;
            Vector[] vectorArray3 = null;
            IDrawGeometry geometry = null;
            FacStyleClass connectline = null;
            try
            {
                List<PipeLineFac> pipelineFacs = fac.GetTopoPipeLine();
                if (pipelineFacs != null && pipelineFacs.Count > 0)
                {
                    sections = new IPipeSection[pipelineFacs.Count];
                    vtx = new Vector[pipelineFacs.Count][];
                    vectorArray2 = new Vector[pipelineFacs.Count];
                    vectorArray3 = new Vector[pipelineFacs.Count];
                    for (int i = 0; i < pipelineFacs.Count; i++)
                    {
                        PipeLineFac line = pipelineFacs[i];
                        if (line == null) continue;
                        connectline = line.Style;
                        if (!line.GetPipeNodeParameterUnder(line.Tag, out vtx[i], out vectorArray3[i], out sections[i]))
                        {
                            return flag;
                        }
                        vectorArray2[i] = vtx[i][0];
                        num++;
                    }
                }
                #region 管点有模型情况下
                if (style is PipeNodeStyleClass)
                {
                    PipeNodeStyleClass style2 = style as PipeNodeStyleClass;
                    style2.InitResource();
                    if (!style2.IsBlendPipe)
                    {
                        IDrawStaticModel model = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.StaticModel, fac.ModelName) as IDrawStaticModel;
                        model.Directions = vectorArray3;
                        model.ConnectPoint = vectorArray2;
                        model.Sections = sections;
                        model.FineModel = style2.FineModel;
                        model.SimpleModel = style2.SimpleModel;
                        model.SetParameter(fac.CenterPosition, fac.IsValve, fac.IsWell, fac.SurfH, fac.TopH, fac.BottomH, style2.IsFollowDir, style2.IsFollowDia, style2.IsFollowSurfH, style2.IsStretchZ, style2.IsRotateZ);
                        geometry = model;
                    }
                    else
                    {
                        // 自动为模型生成融合管线 20161111
                        IDrawConnBlend blend = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.ConnBlend, fac.ModelName) as IDrawConnBlend;
                        blend.SetBaseModel(style2.FineModel, style2.SimpleModel);
                        // 设置融合管线颜色
                        if (connectline != null)
                        {
                            PipeLineStyleClass pipestyle = connectline as PipeLineStyleClass;
                            // 加载风格资源，初始化ColorArray
                            if (!pipestyle.InitResource())
                            {
                                blend.SetColorRender(new uint[] { 0xffaaaaaa, 0xffaaaaaa, 0xffaaaaaa });
                            }
                            else
                            {
                                blend.SetColorRender(new uint[] { pipestyle.ColorArray[0], pipestyle.ColorArray[1], pipestyle.ColorArray[0] });
                            }
                        }
                        else
                        {
                            blend.SetColorRender(new uint[] { 0xffaaaaaa, 0xffaaaaaa, 0xffaaaaaa });
                        }
                        // 设置融合管线坐标
                        Vector[][] vtx1 = new Vector[1][];
                        Vector[] vtx2 = new Vector[2];
                        if (vtx[0].Length > 0)
                        {
                            double height = sections[0].Height == 0.0 ? sections[0].Width * 0.5 : sections[0].Height * 0.5;
                            vtx2[0] = vtx[0][0];
                            vtx2[0].Z = vtx2[0].Z - height;
                        }
                        else
                        {
                            vtx2[0] = new Vector(fac.CenterPosition.X, fac.CenterPosition.Y, fac.CenterPosition.Z);
                        }
                        vtx2[1] = new Vector(fac.CenterPosition.X, fac.CenterPosition.Y, fac.SurfH);
                        vtx1[0] = vtx2;
                        blend.SetParameter(new Vector(fac.CenterPosition.X, fac.CenterPosition.Y, fac.SurfH), vtx1, sections);
                        geometry = blend;
                    }
                }
                #endregion

                #region 管点无模型默认风格
                if (style is PipeLineStyleClass)
                {
                    // 同一图层不同管线风格 管点默认风格不同 一一对应 20160724
                    // 判断默认风格管点与连接管线风格颜色是否一致
                    if (connectline != null)
                    {
                        if (style.ObjectId != connectline.ObjectId)
                        {
                            style = connectline;
                            // 加载风格资源
                            if (!style.InitResource())
                            {
                            }
                            if (style.Images != null)
                            {
                                string[] allKeys = style.Images.GetAllKeys();
                                if ((allKeys != null) && (allKeys.Length > 0))
                                {
                                    foreach (string str6 in allKeys)
                                    {
                                        try
                                        {
                                            IImage property = style.Images.GetProperty(str6) as IImage;
                                            IFeatureDataSet dataSet = fac.Reg.GetFeatureClass().FeatureDataSet;
                                            IResourceManager manager = dataSet as IResourceManager;
                                            if (property != null)
                                            {
                                                if (manager.ImageExist(str6))
                                                {
                                                    manager.UpdateImage(str6, property);
                                                }
                                                else
                                                {
                                                    manager.AddImage(str6, property);
                                                }
                                                //DrawGeometry.Ocx.RefreshImage(dataSet, str6);
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }

                    PipeLineStyleClass style3 = style as PipeLineStyleClass;
                    IDrawJoint joint = null;
                    switch (num)
                    {
                        case 0:
                            return false;
                        case 1:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointEnd, fac.ModelName) as IDrawJointEnd;
                            break;
                        case 2:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointTwo, fac.ModelName) as IDrawJointTwo;
                            break;
                        case 3:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointThree, fac.ModelName) as IDrawJointThree;
                            break;
                        case 4:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointFour, fac.ModelName) as IDrawJointFour;
                            break;
                        default:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointMulti, fac.ModelName) as IDrawJointMulti;
                            break;
                    }
                    if (joint != null)
                    {
                        joint.SetParameter(fac.CenterPosition, vtx, sections);
                        if (style3.RenderType == RenderType.Color)
                        {
                            joint.SetColorRender(new uint[] { style3.ColorArray[0], style3.ColorArray[1], style3.ColorArray[0] });
                        }
                        else
                        {
                            joint.SetTextureRender(new string[] { style3.TextureArray[0], style3.TextureArray[1], style3.TextureArray[0] });
                        }
                        geometry = joint;
                    }
                }
                #endregion

                if (geometry != null)
                {
                    name = geometry.ModelType.ToString();
                    flag = geometry.Draw(out mp, out finemodel, out simplemodel);
                }
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { }
        }

        private static double FindSpatialDis(PipeLineFac line, PipeNodeFac node)
        {
            double dis = 999999;
            IQueryFilter filter = null;
            IRowBuffer r = null;
            try
            {
                IFeatureDataSet fds = DF3DPipeCreateApp.App.PipeLib.OpenFeatureDataset("DataSet_BIZ");
                if (fds == null) return 999999;
                IFeatureClass topoclass = fds.OpenFeatureClass(node.TopoLayer.TopoTable);
                if (topoclass == null) return 999999;

                if (topoclass != null)
                {
                    filter = new QueryFilterClass
                    {
                        WhereClause = string.Format("Edge= {0} and PNode = {1}", line.FeatureId, node.FeatureId),
                        SubFields = "PDis"
                    };
                    if (topoclass.GetCount(filter) == 1)
                    {
                        r = topoclass.Search(filter, true).NextRow();
                        if (r != null)
                        {
                            dis = Convert.ToDouble(r.GetValue(0));
                        }
                    }
                    else
                    {
                        filter = new QueryFilterClass
                        {
                            WhereClause = string.Format("Edge= {0} and ENode = {1}", line.FeatureId, node.FeatureId),
                            SubFields = "EDis"
                        };
                        if (topoclass.GetCount(filter) == 1)
                        {
                            r = topoclass.Search(filter, true).NextRow();
                            if (r != null)
                            {
                                dis = Convert.ToDouble(r.GetValue(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
            return dis;
        }

        private static bool RebuildModelOverHead(PipeNodeFac fac, FacStyleClass style, out IModelPoint mp, out IModel finemodel, out IModel simplemodel, out string name)
        {
            int FLAG = 0; // 判断阀门是竖or横,1为竖，-1为横，0为无拓扑
            bool flag = false;
            mp = null;
            finemodel = null;
            simplemodel = null;
            name = "未知";
            if (fac == null) return false;
            int num = 0;
            PipeLineFac line = null;
            IPipeSection[] sections = null;
            Vector[][] vtx = null;
            Vector[] vectorArray2 = null;
            Vector[] vectorArray3 = null;
            IDrawGeometry geometry = null;
            FacStyleClass connectline = null;
            try
            {
                List<PipeLineFac> pipelineFacs1 = fac.GetTopoPipeLine();
                List<PipeLineFac> pipelineFacs = null;
                if (pipelineFacs1 != null && pipelineFacs1.Count > 0)
                {
                    pipelineFacs = new List<PipeLineFac>();
                    if (pipelineFacs1.Count == 1)
                    {
                        pipelineFacs = pipelineFacs1;
                    }
                    else
                    {
                        double MinDis = 9999;
                        int MinIndex = -1;
                        Dictionary<int, double> Index_Dis = new Dictionary<int, double>();
                        for (int i = 0; i < pipelineFacs.Count; i++)
                        {
                            PipeLineFac pipeLine = pipelineFacs[i] as PipeLineFac;
                            double dis1 = FindSpatialDis(pipeLine, fac);
                            Index_Dis.Add(i, dis1);
                            if (dis1 < MinDis)
                            {
                                MinDis = dis1;
                                MinIndex = i;
                            }
                        }
                        pipelineFacs.Add(pipelineFacs1[MinIndex]);
                        for (int i = 0; i < pipelineFacs1.Count; i++)
                        {
                            if (i != MinIndex)
                            {
                                double dis2 = 9999;
                                Index_Dis.TryGetValue(i, out dis2);
                                if (Math.Abs(dis2 - MinDis) < 0.003)
                                {
                                    pipelineFacs.Add(pipelineFacs1[i]);
                                }
                            }
                        }
                    }
                }
                if ((pipelineFacs != null) && (pipelineFacs.Count > 0))
                {
                    sections = new IPipeSection[pipelineFacs.Count];
                    vtx = new Vector[pipelineFacs.Count][];
                    vectorArray2 = new Vector[pipelineFacs.Count];
                    vectorArray3 = new Vector[pipelineFacs.Count];
                    for (int i = 0; i < pipelineFacs.Count; i++)
                    {
                        line = pipelineFacs[i] as PipeLineFac;
                        if (line == null) continue;
                        connectline = line.Style;
                        if (line != null)
                        {
                            if (!line.GetPipeNodeParameterOver(line.Tag, out vtx[i], out vectorArray3[i], out sections[i], out FLAG))
                            {
                                return flag;
                            }
                            vectorArray2[i] = vtx[i][0];
                            num++;
                        }
                    }
                }
                #region 管点有模型情况下
                if (style is PipeNodeStyleClass)
                {
                    PipeNodeStyleClass style2 = style as PipeNodeStyleClass;
                    style2.InitResource();
                    if (!style2.IsBlendPipe)
                    {
                        IDrawStaticModel model = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.StaticModel, fac.ModelName) as IDrawStaticModel;
                        model.Directions = vectorArray3;
                        model.ConnectPoint = vectorArray2;
                        model.Sections = sections;
                        model.FineModel = style2.FineModel;
                        model.SimpleModel = style2.SimpleModel;
                        model.FLAG = FLAG;
                        model.Pitch = (fac.Pitch / 180) * Math.PI;
                        model.Yaw = (fac.Yaw / 180) * Math.PI;
                        model.SetParameter(fac.CenterPosition, fac.IsValve, fac.IsWell, fac.SurfH, fac.TopH, fac.BottomH, style2.IsFollowDir, style2.IsFollowDia, style2.IsFollowSurfH, style2.IsStretchZ, style2.IsRotateZ);
                        geometry = model;
                    }
                    else
                    {
                        DrawConnBlend blend = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.ConnBlend, fac.ModelName) as DrawConnBlend;
                        blend.SetBaseModel(style2.FineModel, style2.SimpleModel);
                        blend.SetColorRender(new uint[] { 0xffaaaaaa, 0xffaaaaaa, 0xffaaaaaa });
                        blend.SetParameter(new Vector(fac.CenterPosition.X, fac.CenterPosition.Y, fac.SurfH), vtx, sections);
                        geometry = blend;
                        
                    }
                }
                #endregion

                #region 管点无模型默认风格
                if (style is PipeLineStyleClass)
                {                   
                    // 同一图层不同管线风格 管点默认风格不同 一一对应 20160724
                    // 判断默认风格管点与连接管线风格颜色是否一致
                    if (connectline != null)
                    {
                        if (style.ObjectId != connectline.ObjectId)
                        {
                            style = connectline;
                            // 加载风格资源
                            if (!style.InitResource())
                            {
                            }
                            if (style.Images != null)
                            {
                                string[] allKeys = style.Images.GetAllKeys();
                                if ((allKeys != null) && (allKeys.Length > 0))
                                {
                                    foreach (string str6 in allKeys)
                                    {
                                        try
                                        {
                                            IImage property = style.Images.GetProperty(str6) as IImage;
                                            IFeatureDataSet dataSet = fac.Reg.GetFeatureClass().FeatureDataSet;
                                            IResourceManager manager = dataSet as IResourceManager;
                                            if (property != null)
                                            {
                                                if (manager.ImageExist(str6))
                                                {
                                                    manager.UpdateImage(str6, property);
                                                }
                                                else
                                                {
                                                    manager.AddImage(str6, property);
                                                }
                                                //DrawGeometry.Ocx.RefreshImage(dataSet, str6);
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }

                    PipeLineStyleClass style3 = style as PipeLineStyleClass;
                    IDrawJoint joint = null;
                    switch (num)
                    {
                        case 0:
                            return false;
                        case 1:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointEnd, fac.ModelName) as IDrawJointEnd;
                            break;
                        case 2:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointTwo, fac.ModelName) as IDrawJointTwo;
                            break;
                        case 3:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointThree, fac.ModelName) as IDrawJointThree;
                            break;
                        case 4:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointFour, fac.ModelName) as IDrawJointFour;
                            break;
                        default:
                            joint = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.JointMulti, fac.ModelName) as IDrawJointMulti;
                            break;
                    }
                    if (joint != null)
                    {
                        joint.SetParameter(fac.CenterPosition, vtx, sections);
                        if (style3.RenderType == RenderType.Color)
                        {
                            joint.SetColorRender(new uint[] { style3.ColorArray[0], style3.ColorArray[1], style3.ColorArray[0] });
                        }
                        else
                        {
                            joint.SetTextureRender(new string[] { style3.TextureArray[0], style3.TextureArray[1], style3.TextureArray[0] });
                        }
                        geometry = joint;
                    }
                }
                #endregion

                if (geometry != null)
                {
                    name = geometry.ModelType.ToString();
                    if (geometry is DrawStaticModel) flag = geometry.DrawOver(out mp, out finemodel, out simplemodel);
                    else flag = geometry.Draw(out mp, out finemodel, out simplemodel);
                }
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { }
        }

        private static bool RebuildPipeLineModel(PipeLineFac fac, FacStyleClass style, out IModelPoint mp, out IModel finemodel, out IModel simplemodel, out string name)
        {
            bool flag = false;
            mp = null;
            finemodel = null;
            simplemodel = null;
            name = "未知";
            if (fac == null || !(style is PipeLineStyleClass)) return false;
            try
            {
                PipeLineStyleClass style2 = style as PipeLineStyleClass;
                style2.InitResource();
                List<Vector> list;
                IDrawPipeLine line = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.PipeLine, fac.ModelName) as IDrawPipeLine;
                if (style2.RenderType == RenderType.Color)
                {
                    line.SetColorRender(new uint[] { style2.ColorArray[0], style2.ColorArray[1], style2.ColorArray[0] });
                }
                else
                {
                    line.SetTextureRender(new string[] { style2.TextureArray[0], style2.TextureArray[1], style2.TextureArray[0] });
                }
                IPipeSection section = null;
                if (!fac.GetPipeLineParameter(style2.PipeThick, out list, out section))
                {
                    return false;
                }
                line.SetParameter(section, list, fac.Reg.TurnerStyle, fac.Reg.LocationType);
                name = line.ModelType.ToString();
                return line.Draw(out mp, out finemodel, out simplemodel);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        // - polygon
        private static  bool RebuildPipeBuildModel(PipeBuildFac fac, FacStyleClass style, out IModelPoint mp, out IModel finemodel, out IModel simplemodel, out string name)
        {
            bool flag = false;
            mp = null;
            finemodel = null;
            simplemodel = null;
            name = "未知";
            if (fac == null || !(style is PipeBuildStyleClass)) return false;
            try
            {
                PipeBuildStyleClass style2 = style as PipeBuildStyleClass;
                style2.InitResource();
                IDrawHeevenWell well = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.HeevenWell, fac.ModelName) as IDrawHeevenWell;
                if (style2.RenderType == RenderType.Color)
                {
                    well.SetColorRender(new uint[] { style2.ColorArray[0], style2.ColorArray[1] });
                }
                else
                {
                    well.SetTextureRender(new string[] { style2.TextureArray[0], style2.TextureArray[1] });
                }
                well.SetParameter(fac.Geo2D, fac.TopHeight, fac.BottomHeight);
                name = well.ModelType.ToString();
                return well.Draw(out mp, out finemodel, out simplemodel);
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        // - polyline
        private static bool RebuildPipeBuildModel1(PipeBuildFac fac, FacStyleClass style, out IModelPoint mp, out IModel finemodel, out IModel simplemodel, out string name)
        {
            bool flag = false;
            mp = null;
            finemodel = null;
            simplemodel = null;
            name = "未知";
            if (fac == null || !(style is PipeBuildStyleClass)) return false;
            try
            {
                PipeBuildStyleClass style2 = style as PipeBuildStyleClass;
                style2.InitResource();
                IDrawHeevenWell well = ParamModelFactory.Instance.CreateGeometryDraw(ModelType.HeevenWell, fac.ModelName) as IDrawHeevenWell;
                if (style2.RenderType == RenderType.Color)
                {
                    well.SetColorRender(new uint[] { style2.ColorArray[0], style2.ColorArray[1] });
                }
                else
                {
                    well.SetTextureRender(new string[] { style2.TextureArray[0], style2.TextureArray[1] });
                }
                well.SetParameter1(fac.Geo2D, fac.TopHeight, fac.BottomHeight);
                name = well.ModelType.ToString();
                return well.Draw(out mp, out finemodel, out simplemodel);
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        #endregion

    }
}
