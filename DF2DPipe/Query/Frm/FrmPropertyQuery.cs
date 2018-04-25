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
using DFWinForms.Class;
using DF2DData.Class;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geodatabase;
using DFDataConfig.Class;
using DFDataConfig.Logic;


namespace DF2DPipe.Query.Frm
{
    public partial class FrmPropertyQuery : XtraForm
    {
       private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ListBoxControl listBoxControlValues;
        private TextEdit teValue;
        private SimpleButton btnCancel;
        private SimpleButton btnQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private LabelControl labelControl1;
        private PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.TreeList treeLayer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private IContainer components;
        
        private PictureEdit pictureEdit1;
        private LabelControl labelControl3;
        private LabelControl labelControl2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private SimpleButton btnReverseSelect;
        private SimpleButton btnSelectAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

    

        public FrmPropertyQuery(string title, string sysFieldName)
        {
            InitializeComponent();
            this.Text = title;
            this.labelControl3.Text = title;
            this._sysFieldName = sysFieldName;
           
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPropertyQuery));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnReverseSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.treeLayer = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.listBoxControlValues = new DevExpress.XtraEditors.ListBoxControl();
            this.teValue = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnReverseSelect);
            this.layoutControl1.Controls.Add(this.btnSelectAll);
            this.layoutControl1.Controls.Add(this.treeLayer);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.listBoxControlValues);
            this.layoutControl1.Controls.Add(this.teValue);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnQuery);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(419, 417);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnReverseSelect
            // 
            this.btnReverseSelect.Location = new System.Drawing.Point(98, 364);
            this.btnReverseSelect.Name = "btnReverseSelect";
            this.btnReverseSelect.Size = new System.Drawing.Size(90, 22);
            this.btnReverseSelect.StyleController = this.layoutControl1;
            this.btnReverseSelect.TabIndex = 12;
            this.btnReverseSelect.Text = "反选";
            this.btnReverseSelect.Click += new System.EventHandler(this.btnReverseSelect_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(5, 364);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(89, 22);
            this.btnSelectAll.StyleController = this.layoutControl1;
            this.btnSelectAll.TabIndex = 11;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // treeLayer
            // 
            this.treeLayer.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treeLayer.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeLayer.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treeLayer.Location = new System.Drawing.Point(5, 93);
            this.treeLayer.Name = "treeLayer";
            this.treeLayer.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeLayer.OptionsView.ShowCheckBoxes = true;
            this.treeLayer.OptionsView.ShowColumns = false;
            this.treeLayer.OptionsView.ShowIndicator = false;
            this.treeLayer.OptionsView.ShowVertLines = false;
            this.treeLayer.Size = new System.Drawing.Size(183, 267);
            this.treeLayer.StateImageList = this.imageCollection1;
            this.treeLayer.TabIndex = 5;
            this.treeLayer.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeLayer_AfterCheckNode);
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
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(198, 93);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(216, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "选择或者输入属性值";
            // 
            // listBoxControlValues
            // 
            this.listBoxControlValues.Location = new System.Drawing.Point(198, 137);
            this.listBoxControlValues.Name = "listBoxControlValues";
            this.listBoxControlValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxControlValues.Size = new System.Drawing.Size(216, 249);
            this.listBoxControlValues.StyleController = this.layoutControl1;
            this.listBoxControlValues.TabIndex = 9;
            this.listBoxControlValues.SelectedIndexChanged += new System.EventHandler(this.listBoxControlValues_SelectedIndexChanged);
            // 
            // teValue
            // 
            this.teValue.Location = new System.Drawing.Point(198, 111);
            this.teValue.Name = "teValue";
            this.teValue.Size = new System.Drawing.Size(216, 22);
            this.teValue.StyleController = this.layoutControl1;
            this.teValue.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(308, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(195, 393);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(109, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(415, 64);
            this.panelControl1.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(215, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "labelControl3";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(183, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "属性查询";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(104, 5);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(48, 48);
            this.pictureEdit1.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(419, 417);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(419, 68);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层树";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 68);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(193, 323);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeLayer;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(187, 271);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSelectAll;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 271);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnReverseSelect;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(93, 271);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "查询条件";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(193, 68);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(226, 323);
            this.layoutControlGroup3.Text = "查询条件";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.labelControl1;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(220, 18);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teValue;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.listBoxControlValues;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(220, 253);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 391);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(193, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(306, 391);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(113, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnQuery;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(193, 391);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(113, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmPropertyQuery
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(419, 417);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmPropertyQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmPropertyQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        private string _sysFieldName;
        private Dictionary<string, DataTable> _dict;
        public Dictionary<string, DataTable> Dict
        {
            get { return this._dict; }
        }


        private void FrmPropertyQuery_Load(object sender, EventArgs e)
        {

            BuildTree();
            this._dict = new Dictionary<string, DataTable>();
        }

        private void BuildTree()
        {
            foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
            {
                TreeListNode node = this.treeLayer.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, null);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treeLayer.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible2D) continue;
                        TreeListNode scnode = this.treeLayer.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 1;
                    }
                }
            }

            foreach (MajorClass mc in LogicDataStructureManage2D.Instance.RootMajorClasses)
            {
                TreeListNode mcnode = this.treeLayer.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, null);
                mcnode.StateImageIndex = 0;
                foreach (SubClass sc in mc.SubClasses)
                {
                    if (!sc.Visible2D) continue;
                    TreeListNode scnode = this.treeLayer.AppendNode(new object[] { sc.Name, sc }, mcnode);
                    scnode.StateImageIndex = 1;
                }
            }
        }


        private void RecursiveBuildTree(TreeListNode parentNode, List<LogicGroup> listLG)
        {
            foreach (LogicGroup lg in listLG)
            {   
                TreeListNode node = this.treeLayer.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, parentNode);
                node.StateImageIndex = 0;
                RecursiveBuildTree(node, lg.LogicGroups);
                foreach (MajorClass mc in lg.MajorClasses)
                {
                    TreeListNode mcnode = this.treeLayer.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                    mcnode.StateImageIndex = 0;
                    foreach (SubClass sc in mc.SubClasses)
                    {
                        if (!sc.Visible2D) continue;
                        TreeListNode scnode = this.treeLayer.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 1;
                    }
                }
            }
        }

        private void DoQuery()
        {
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            string value = this.teValue.Text.Trim();
            if (value.Length > 1)
            {
                int lastindex = value.LastIndexOf(';');
                if (lastindex == (value.Length - 1))
                    value = value.Substring(0, value.Length - 1);
            }
            string[] arr1 = value.Split(';');
            if (arr1 == null || arr1.Length == 0) return;
            string temp1 = "";
            foreach (string str1 in arr1) temp1 += str1;
            if (this.treeLayer.GetAllCheckedNodes() != null)
            {
                DF2DPipe.Class.Query query = new DF2DPipe.Class.Query();
                
                foreach (TreeListNode node in this.treeLayer.GetAllCheckedNodes())
                {
                     object obj = node.GetValue("NodeObject");
                     if (obj != null && obj is SubClass)
                     {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string classifyField = sc.Parent.ClassifyField;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            IFeatureClass fc = dffc.GetFeatureClass();
                            FacilityClass facc = dffc.GetFacilityClass();
                            if (this._sysFieldName == "Additional")
                            {
                                if (fc == null || facc == null || facc.Name != "PipeNode") continue;
                            }
                            else
                            {
                                if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            }
                                
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            if (fc == null || facc == null || fi == null) continue;
                            int index = fc.FindField(fi.Name);
                            if (index == -1) continue;
                            IFields pFields = fc.Fields;
                            IField pField = pFields.get_Field(index);

                            
                            string whereClause = UpOrDown.DecorateWhereClasuse(fc) +  classifyField + "='" + sc.Name + "'" + " AND ";
                            
                            foreach (string str1 in arr1)
                            {
                                whereClause += pField.Name + "=" + str1 + " OR ";
                            }
                            whereClause = whereClause.Substring(0, whereClause.Length - 3);
                            IFeature pFeature = null;
                            IFeatureCursor pFeatureCusor = null;
                            try
                            {
                                IQueryFilter pQueryFilter = new QueryFilterClass();
                                pQueryFilter.WhereClause = whereClause;
                                pFeatureCusor = fc.Search(pQueryFilter, true);
                                DataTable dt = new DataTable();
                                dt.TableName = facc.Name;
                                DataColumn oidcol = new DataColumn();
                                oidcol.ColumnName = "oid";
                                oidcol.Caption = "ID";
                                dt.Columns.Add(oidcol);
                                foreach (DFDataConfig.Class.FieldInfo fitemp in facc.FieldInfoCollection)
                                {
                                    if (!fitemp.CanQuery) continue;
                                    DataColumn col = new DataColumn();
                                    col.ColumnName = fitemp.Name;
                                    col.Caption = fitemp.Alias;
                                    dt.Columns.Add(col);
                                }

                                while ((pFeature = pFeatureCusor.NextFeature()) != null)
                                {
                                    DataRow dtRow = dt.NewRow();
                                    dtRow["oid"] = pFeature.get_Value(pFeature.Fields.FindField("OBJECTID"));
                                    foreach (DataColumn col in dt.Columns)
                                    {
                                        int index1 = pFeature.Fields.FindField(col.ColumnName);
                                        if (index1 < 0) continue;
                                        object obj1 = pFeature.get_Value(index1);
                                        string str = "";
                                        if (obj1 != null)
                                        {
                                            IField field = pFeature.Fields.get_Field(index1);
                                            switch (field.Type)
                                            {
                                                case esriFieldType.esriFieldTypeBlob:
                                                case esriFieldType.esriFieldTypeGeometry:
                                                case esriFieldType.esriFieldTypeRaster:
                                                    continue;
                                                case esriFieldType.esriFieldTypeDouble:
                                               
                                                    double d;
                                                    if (double.TryParse(obj1.ToString(), out d))
                                                    {
                                                        str = d.ToString("0.00");
                                                    }
                                                    break;
                                                default:
                                                    str = obj1.ToString();
                                                    break;

                                            }
                                        }
                                        dtRow[col.ColumnName] = str;
                                    }
                                    dt.Rows.Add(dtRow);
                                }
                                if (dt.Rows.Count > 0) this._dict[sc.Name] = dt;
                            }
                            catch 
                            {
                            }
                            finally
                            {
                                if (pFeatureCusor != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCusor);
                                    pFeatureCusor = null;
                                }
                                if (pFeature!= null)
                                {
                                    pFeature = null;
                                }
                            }
                            
                                
                            

                            
                        }
                     }
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm.Start("正在查询...", "请稍后", false);
                DoQuery();
                WaitForm.Stop();
                //if (_dt == null || _dt.Rows.Count == 0)
                //{
                //    XtraMessageBox.Show("查询结果为空！", "提示");
                //    return;
                //}
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("查询出错！", "提示");
                WaitForm.Stop();
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
            this.treeLayer.CheckAll();
            treeLayer_AfterCheckNode(null, null);
        }

        private void btnReverseSelect_Click(object sender, EventArgs e)
        {
            foreach (TreeListNode tln in this.treeLayer.Nodes)
            {
                if (tln.Checked) tln.Checked = false;
                else tln.Checked = true;
            }
            treeLayer_AfterCheckNode(null, null);
        }

        private void treeLayer_AfterCheckNode(object sender, NodeEventArgs e)
        {
            this.teValue.Text = "";
            this.listBoxControlValues.Items.Clear();
            IFeatureCursor pFeatureCursor = null;
            IFeature pFeature = null;
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            try
            {
                WaitForm.Start("正在加载列表...", "请稍后");
                HashSet<string> list = new HashSet<string>();
                foreach (TreeListNode node in this.treeLayer.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if(sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if(arrFc2DId == null) continue;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (this._sysFieldName == "Additional")
                            {
                                if (fc == null || facc == null || facc.Name != "PipeNode") continue;
                            }
                            else
                            {
                                if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            }                           
                            
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            if (fi == null) continue;

                            IFields pFields = fc.Fields;
                            int index = pFields.FindField(fi.Name);
                            if (index < 0) continue;
                            IField pField = pFields.get_Field(index);


                            IQueryFilter pQueryFilter = new QueryFilterClass();
                            pQueryFilter.SubFields = pField.Name;
                            pQueryFilter.WhereClause = sc.Parent.ClassifyField + " = " + "'" + sc.Name + "'";

                            pFeatureCursor = fc.Search(pQueryFilter, false);
                            pFeature = pFeatureCursor.NextFeature();

                            while (pFeature != null)
                            {
                                object temp = pFeature.get_Value(index);
                                if (temp == null) continue;
                                string strtemp = "";
                                switch (pField.Type)
                                {
                                    case esriFieldType.esriFieldTypeDouble:
                                    case esriFieldType.esriFieldTypeInteger:
                                    case esriFieldType.esriFieldTypeOID:
                                    case esriFieldType.esriFieldTypeSingle:
                                    case esriFieldType.esriFieldTypeSmallInteger:
                                        strtemp = temp.ToString();
                                        break;
                                    case esriFieldType.esriFieldTypeDate:
                                    case esriFieldType.esriFieldTypeString:
                                    case esriFieldType.esriFieldTypeGUID:
                                        strtemp = "'" + temp.ToString() + "'";
                                        break;
                                    case esriFieldType.esriFieldTypeBlob:
                                    case esriFieldType.esriFieldTypeGeometry:
                                    case esriFieldType.esriFieldTypeGlobalID:
                                    case esriFieldType.esriFieldTypeRaster:
                                    case esriFieldType.esriFieldTypeXML:
                                    default:
                                        continue;
                                }
                                if (temp != null) list.Add(strtemp);
                                pFeature = pFeatureCursor.NextFeature();
                            }    
                        }
                    }
                }
                foreach (string str2 in list)
                {
                    //if (!(string.IsNullOrEmpty(str2)))
                    //{
                    this.listBoxControlValues.Items.Add(str2);
                    //}
                }
            }
            catch
            {

            }
            finally
            {
                if (pFeatureCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                    pFeatureCursor = null;
                }
                if (pFeature != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeature);
                    pFeature = null;
                }
                WaitForm.Stop();
            }
           

        }
       
        
    }
}
