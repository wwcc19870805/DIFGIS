using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DFDataConfig.Logic;
using DFWinForms.Class;
using DFCommon.Class;

namespace DF3DPipe.Stats.Frm
{
    public class FrmPropertyStats : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private SimpleButton btnCancel;
        private LabelControl labelControl1;
        private SimpleButton btnStats;
        private ListBoxControl listBoxControlValues;
        private TextEdit teValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraTreeList.TreeList treelist;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private IContainer components;
        private LabelControl labelControl3;
        private PictureEdit pictureEdit1;
        private LabelControl labelControl2;
        private SimpleButton btnUnSelectAll;
        private SimpleButton btnSelectAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
    

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPropertyStats));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnUnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.treelist = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnStats = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxControlValues = new DevExpress.XtraEditors.ListBoxControl();
            this.teValue = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnUnSelectAll);
            this.layoutControl1.Controls.Add(this.btnSelectAll);
            this.layoutControl1.Controls.Add(this.treelist);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnStats);
            this.layoutControl1.Controls.Add(this.listBoxControlValues);
            this.layoutControl1.Controls.Add(this.teValue);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(419, 417);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Location = new System.Drawing.Point(99, 364);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(91, 22);
            this.btnUnSelectAll.StyleController = this.layoutControl1;
            this.btnUnSelectAll.TabIndex = 2;
            this.btnUnSelectAll.Text = "全不选";
            this.btnUnSelectAll.Click += new System.EventHandler(this.btnUnSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(5, 364);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(90, 22);
            this.btnSelectAll.StyleController = this.layoutControl1;
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // treelist
            // 
            this.treelist.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treelist.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treelist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treelist.Location = new System.Drawing.Point(5, 92);
            this.treelist.Name = "treelist";
            this.treelist.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treelist.OptionsFilter.AllowColumnMRUFilterList = false;
            this.treelist.OptionsView.ShowCheckBoxes = true;
            this.treelist.OptionsView.ShowColumns = false;
            this.treelist.OptionsView.ShowIndicator = false;
            this.treelist.OptionsView.ShowVertLines = false;
            this.treelist.Size = new System.Drawing.Size(185, 268);
            this.treelist.StateImageList = this.imageCollection1;
            this.treelist.TabIndex = 0;
            this.treelist.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treelist_AfterCheckNode);
            // 
            // NodeName
            // 
            this.NodeName.Caption = "名称";
            this.NodeName.FieldName = "NodeName";
            this.NodeName.MinWidth = 49;
            this.NodeName.Name = "NodeName";
            this.NodeName.OptionsColumn.AllowEdit = false;
            this.NodeName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.NodeName.Visible = true;
            this.NodeName.VisibleIndex = 0;
            // 
            // NodeObject
            // 
            this.NodeObject.Caption = "对象";
            this.NodeObject.FieldName = "NodeObject";
            this.NodeObject.Name = "NodeObject";
            this.NodeObject.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Object;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Group.png");
            this.imageCollection1.Images.SetKeyName(1, "FeatureLayer_model.png");
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(303, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(200, 92);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(214, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "选择或者输入属性值";
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(197, 393);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(102, 22);
            this.btnStats.StyleController = this.layoutControl1;
            this.btnStats.TabIndex = 5;
            this.btnStats.Text = "统计";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // listBoxControlValues
            // 
            this.listBoxControlValues.Location = new System.Drawing.Point(200, 136);
            this.listBoxControlValues.Name = "listBoxControlValues";
            this.listBoxControlValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxControlValues.Size = new System.Drawing.Size(214, 250);
            this.listBoxControlValues.StyleController = this.layoutControl1;
            this.listBoxControlValues.TabIndex = 4;
            this.listBoxControlValues.SelectedIndexChanged += new System.EventHandler(this.listBoxControlValues_SelectedIndexChanged);
            // 
            // teValue
            // 
            this.teValue.Location = new System.Drawing.Point(200, 110);
            this.teValue.Name = "teValue";
            this.teValue.Size = new System.Drawing.Size(214, 22);
            this.teValue.StyleController = this.layoutControl1;
            this.teValue.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(415, 63);
            this.panelControl1.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(207, 38);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "labelControl3";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(96, 9);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(48, 48);
            this.pictureEdit1.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(175, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "属性统计";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup2,
            this.emptySpaceItem4,
            this.layoutControlGroup3,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(419, 417);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(419, 67);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层树";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 67);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(195, 324);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.treelist;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(189, 272);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSelectAll;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 272);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnUnSelectAll;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(94, 272);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(95, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 391);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(195, 26);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "统计条件";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(195, 67);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(224, 324);
            this.layoutControlGroup3.Text = "统计条件";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.listBoxControlValues;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(218, 254);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(218, 18);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teValue;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(218, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnStats;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(195, 391);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(106, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(301, 391);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(118, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmPropertyStats
            // 
            this.AcceptButton = this.btnStats;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(419, 417);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmPropertyStats";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmPropertyStats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        private string _sysFieldName;
        private string _facType;
        public FrmPropertyStats(string title, string sysFieldName, string facType)
        {
            InitializeComponent();
            this.Text = title;
            this.labelControl3.Text = title;
            this._sysFieldName = sysFieldName;
            this._facType = facType;
        }
        private void RecursiveBuildTree(TreeListNode parentNode, List<LogicGroup> listLG)
        {
            foreach (LogicGroup lg in listLG)
            {
                TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, parentNode);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible3D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 1;
                    }
                }
            }
        }

        private void BuildTree()
        {
            foreach (LogicGroup lg in LogicDataStructureManage3D.Instance.RootLogicGroups)
            {
                TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, null);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible3D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 1;
                    }
                }
            }

            foreach (MajorClass mc in LogicDataStructureManage3D.Instance.RootMajorClasses)
            {
                TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, null);
                mcnode.StateImageIndex = 0;
                foreach (SubClass sc in mc.SubClasses)
                {
                    if (!sc.Visible3D) continue;
                    TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                    scnode.StateImageIndex = 1;
                }
            }
        }

        private void FrmPropertyStats_Load(object sender, EventArgs e)
        {
            BuildTree();
        }

        private DataTable DoPipeNodeStats()
        {
            if (string.IsNullOrEmpty(this._sysFieldName)) return null;
            string value = this.teValue.Text.Trim();
            if (value.Length > 1)
            {
                int lastindex = value.LastIndexOf(';');
                if (lastindex == (value.Length - 1))
                    value = value.Substring(0, value.Length - 1);
            }
            DataTable dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPENODETYPE"),new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
            new DataColumn("NUMBER",typeof(long)),new DataColumn("TOTALNUMBER",typeof(long))});

            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
                        if (arrFc3DId == null) continue;
                        long subclasstotal = 0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc3DId in arrFc3DId)
                        {
                            DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != this._facType) continue;
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            if (fi == null) continue;
                            int index = fc.GetFields().IndexOf(fi.Name);
                            if (index == -1) continue;
                            IFieldInfo fcfi = fc.GetFields().Get(index);
                            IQueryFilter filter = new QueryFilter();

                            string[] arrvalue = value.Split(';');
                            foreach (string strValue in arrvalue)
                            {
                                if (string.IsNullOrEmpty(strValue)) continue;
                                switch (fcfi.FieldType)
                                {
                                    case gviFieldType.gviFieldBlob:
                                    case gviFieldType.gviFieldGeometry:
                                    case gviFieldType.gviFieldUnknown:
                                        continue;
                                }
                                filter.WhereClause = "GroupId = " + sc.GroupId + " and " + fi.Name + " = " + strValue;
                                int count = fc.GetCount(filter);
                                if (count == 0) continue;
                                bool bHave = true;
                               
                                if (bHave)
                                {
                                    DataRow dr = dttemp.NewRow();
                                    dr["PIPENODETYPE"] = sc;
                                    dr["FIELDNAME"] = fi;
                                    dr["PVALUE"] = strValue;
                                    subclasstotal += count;
                                    dr["NUMBER"] = count;
                                    dttemp.Rows.Add(dr);
                                }
                            }
                        }
                        int indexEnd = dttemp.Rows.Count;
                        for (int i = indexStart; i < indexEnd; i++)
                        {
                            DataRow dr = dttemp.Rows[i];
                            dr["TOTALNUMBER"] = subclasstotal;
                        }
                    }
                }
            }
            return dttemp;

        }
        private DataTable DoPipeLineStats()
        {
            if (string.IsNullOrEmpty(this._sysFieldName)) return null;
            string value = this.teValue.Text.Trim();
            if (value.Length > 1)
            {
                int lastindex = value.LastIndexOf(';');
                if (lastindex == (value.Length - 1))
                    value = value.Substring(0, value.Length - 1);
            }
            DataTable dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});

            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
                        if (arrFc3DId == null) continue;
                        double subclasslength = 0.0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc3DId in arrFc3DId)
                        {
                            DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != this._facType) continue;
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            if (fi == null ) continue;
                            int index = fc.GetFields().IndexOf(fi.Name);
                            if (index == -1) continue;
                            DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength");
                            if (fiPipeLength == null) continue;
                            int indexPipeLength = fc.GetFields().IndexOf(fiPipeLength.Name);
                            if(indexPipeLength == -1) continue;
                            IFieldInfo fcfi = fc.GetFields().Get(index);
                            IQueryFilter filter = new QueryFilter();
                            filter.SubFields = fiPipeLength.Name;

                            string[] arrvalue = value.Split(';');
                            foreach (string strValue in arrvalue)
                            {
                                if (string.IsNullOrEmpty(strValue)) continue;
                                switch (fcfi.FieldType)
                                {
                                    case gviFieldType.gviFieldBlob:
                                    case gviFieldType.gviFieldGeometry:
                                    case gviFieldType.gviFieldUnknown:
                                        continue;
                                }
                                filter.WhereClause = "GroupId = " + sc.GroupId + " and " + fi.Name + " = " + strValue;
                                int count = fc.GetCount(filter);
                                if (count == 0) continue;
                                bool bHave = false;
                                double subfieldlength = 0.0;
                                int loop = (int)Math.Ceiling(count / 800.0);
                                for (int k = 1; k <= loop; k++)
                                {
                                    if (k == 1)
                                    {
                                        filter.ResultBeginIndex = 0;
                                    }
                                    else
                                    {
                                        filter.ResultBeginIndex = (k - 1) * 800;
                                    }
                                    filter.ResultLimit = 800;
                                    IFdeCursor cursor = null;
                                    IRowBuffer row = null;
                                    #region
                                    try
                                    {
                                        cursor = fc.Search(filter, true);
                                        while ((row = cursor.NextRow()) != null)
                                        {
                                            if (!row.IsNull(0))
                                            {
                                                object tempobj = row.GetValue(0);
                                                double dtemp = 0.0;
                                                if (tempobj != null && double.TryParse(tempobj.ToString(), out dtemp))
                                                {
                                                    bHave = true;
                                                    subfieldlength += dtemp;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
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
                                    #endregion
                                }
                                if (bHave)
                                {
                                    DataRow dr = dttemp.NewRow();
                                    dr["PIPELINETYPE"] = sc;
                                    dr["FIELDNAME"] = fi;
                                    dr["PVALUE"] = strValue;
                                    subclasslength += subfieldlength;
                                    dr["LENGTH"] = subfieldlength.ToString("0.00");
                                    dttemp.Rows.Add(dr);
                                }
                            }
                        }
                        int indexEnd = dttemp.Rows.Count;
                        for (int i = indexStart; i < indexEnd; i++)
                        {
                            DataRow dr = dttemp.Rows[i];
                            dr["TOTALLENGTH"] = subclasslength.ToString("0.00");
                        }
                    }
                }
            }
            return dttemp;
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            string value = this.teValue.Text.Trim();
            if (value == "")
            {
                XtraMessageBox.Show("统计条件为空，请查看。", "提示");
                return;
            }
            try
            {
                WaitForm.Start("正在统计...", "请稍后", false);
                DataTable dt = null;
                if (this._facType == "PipeLine") dt = DoPipeLineStats();
                else if (this._facType == "PipeNode") dt = DoPipeNodeStats();
                if (dt == null || dt.Rows.Count == 0)
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("统计结果为空！", "提示");
                    return;
                }
                WaitForm.Stop();
                if (this._facType == "PipeLine")
                {
                    FrmPipeLineStatsOutput dlg = new FrmPipeLineStatsOutput();
                    dlg.SetData(dt);
                    dlg.ShowDialog();
                }
                else if (this._facType == "PipeNode")
                {
                    FrmPipeNodeStatsOutput dlg = new FrmPipeNodeStatsOutput();
                    dlg.SetData(dt);
                    dlg.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                WaitForm.Stop(); 
                XtraMessageBox.Show("统计出错！", "提示");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void listBoxControlValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControlValues.SelectedItem == null || this.listBoxControlValues.SelectedItems == null) return;
            this.teValue.Text = "";
            foreach (object obj in this.listBoxControlValues.SelectedItems)
            {
                this.teValue.Text = this.teValue.Text + obj.ToString() + ";";
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.treelist.CheckAll();
            treelist_AfterCheckNode(null, null);
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.treelist.UncheckAll();
            treelist_AfterCheckNode(null, null);
        }

        private void treelist_AfterCheckNode(object sender, NodeEventArgs e)
        {
            this.teValue.Text = "";
            this.listBoxControlValues.Items.Clear();
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                WaitForm.Start("正在加载列表...", "请稍后");
                HashSet<string> list = new HashSet<string>();
                bool bBreak = false;
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if(sc.Parent == null) continue;
                        string cacheType = sc.Parent.Name + "_" + sc.GroupId + "_3D_" + this._sysFieldName;
                        object objCache = CacheHelper.GetCache(cacheType);
                        if (objCache != null && objCache is HashSet<string>)
                        {
                            HashSet<string> temphs = objCache as HashSet<string>;
                            foreach (string tempstr in temphs) { list.Add(tempstr); }
                            continue;
                        }
                        HashSet<string> listsc = new HashSet<string>(); 
                        string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
                        if(arrFc3DId == null) continue;
                        foreach (string fc3DId in arrFc3DId)
                        {
                            DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                            if (dffc == null) continue;
                            FacilityClass facClass = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facClass == null || facClass.Name != this._facType) continue;
                            DFDataConfig.Class.FieldInfo fi = facClass.GetFieldInfoBySystemName(this._sysFieldName);
                            IFieldInfoCollection fiCol = fc.GetFields();
                            int index = fiCol.IndexOf(fi.Name);
                            if (index < 0) continue;
                            Gvitech.CityMaker.FdeCore.FieldInfo gfi = (Gvitech.CityMaker.FdeCore.FieldInfo)fiCol.Get(index);
                            IQueryFilter filter = new QueryFilter();
                            filter.SubFields = gfi.Name;
                            filter.ResultBeginIndex = 0;
                            filter.ResultLimit = 1;
                            while (true)
                            {
                                string strTempClause = gfi.Name + " is not null and ";
                                string fClause = strTempClause;
                                foreach (string strtemp in listsc)
                                {
                                    fClause += gfi.Name + " <> " + strtemp + " and ";
                                }
                                fClause = fClause.Substring(0, fClause.Length - 5);
                                filter.WhereClause = "GroupId = " + sc.GroupId + " and " + fClause;

                                cursor = fc.Search(filter, true);
                                if ((row = cursor.NextRow()) != null)
                                {
                                    if (row.IsNull(0)) break;
                                    object temp = row.GetValue(0);
                                    string strtemp = "";
                                    switch (gfi.FieldType)
                                    {
                                        case gviFieldType.gviFieldFID:
                                        case gviFieldType.gviFieldFloat:
                                        case gviFieldType.gviFieldDouble:
                                        case gviFieldType.gviFieldInt16:
                                        case gviFieldType.gviFieldInt32:
                                        case gviFieldType.gviFieldInt64:
                                            strtemp = temp.ToString();
                                            break;
                                        case gviFieldType.gviFieldDate:
                                        case gviFieldType.gviFieldString:
                                        case gviFieldType.gviFieldUUID:
                                            strtemp = "'" + temp.ToString() + "'";
                                            break;
                                        case gviFieldType.gviFieldBlob:
                                        case gviFieldType.gviFieldGeometry:
                                        case gviFieldType.gviFieldUnknown:
                                        default:
                                            continue;
                                    }
                                    if (temp != null)
                                    {
                                        list.Add(strtemp);
                                        listsc.Add(strtemp);
                                        if (list.Count > 10)
                                        {
                                            bBreak = true;
                                            break;// 列举10个
                                        }
                                    }
                                }
                                else break;
                            }
                            if (bBreak) break;
                        }
                        CacheHelper.SetCache(cacheType, listsc);
                    }
                    if (bBreak) break;
                }
                foreach (string str2 in list)
                {
                    //if (!(string.IsNullOrEmpty(str2)))
                    //{
                        this.listBoxControlValues.Items.Add(str2);
                    //}
                }
            }
            catch (Exception ex)
            {
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
                WaitForm.Stop();
            }
        }

    }
}
