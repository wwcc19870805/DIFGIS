using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DF2DPipe.Stats.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using DFWinForms.Class;
using DF2DData.Class;
using DFDataConfig.Class;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmPipeLineLengthStats2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnStats;
        private ComboBoxEdit cbProperty;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.TreeList treelist;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private UCPipeLineStatsOutput ucStatsOutput1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.ComponentModel.IContainer components;

        private string _sysFieldName;

        public DataTable DTTemp
        {
            get { return this.dttemp; }
        }
        public DataTable DTStats
        {
            get { return this.dtstats; }
        }
        public FrmPipeLineLengthStats2D()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPipeLineLengthStats2D));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ucStatsOutput1 = new DF2DPipe.Stats.UC.UCPipeLineStatsOutput();
            this.treelist = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnStats = new DevExpress.XtraEditors.SimpleButton();
            this.cbProperty = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.ucStatsOutput1);
            this.layoutControl1.Controls.Add(this.treelist);
            this.layoutControl1.Controls.Add(this.btnStats);
            this.layoutControl1.Controls.Add(this.cbProperty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(689, 396);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(102, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取     消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucStatsOutput1
            // 
            this.ucStatsOutput1.Location = new System.Drawing.Point(192, 2);
            this.ucStatsOutput1.Name = "ucStatsOutput1";
            this.ucStatsOutput1.Size = new System.Drawing.Size(495, 392);
            this.ucStatsOutput1.TabIndex = 11;
            // 
            // treelist
            // 
            this.treelist.Appearance.FocusedCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.treelist.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treelist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treelist.Location = new System.Drawing.Point(5, 25);
            this.treelist.Name = "treelist";
            this.treelist.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treelist.OptionsView.ShowCheckBoxes = true;
            this.treelist.OptionsView.ShowColumns = false;
            this.treelist.OptionsView.ShowIndicator = false;
            this.treelist.OptionsView.ShowVertLines = false;
            this.treelist.Size = new System.Drawing.Size(180, 288);
            this.treelist.StateImageList = this.imageCollection1;
            this.treelist.TabIndex = 7;
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
            this.imageCollection1.Images.SetKeyName(1, "Database.png");
            this.imageCollection1.Images.SetKeyName(2, "3DTileLayer.png");
            this.imageCollection1.Images.SetKeyName(3, "Dataset.png");
            this.imageCollection1.Images.SetKeyName(4, "FeatureLayer_line.png");
            this.imageCollection1.Images.SetKeyName(5, "FeatureLayer_model.png");
            this.imageCollection1.Images.SetKeyName(6, "FeatureLayer_point.png");
            this.imageCollection1.Images.SetKeyName(7, "FeatureLayer_polygon.png");
            this.imageCollection1.Images.SetKeyName(8, "Label_image.png");
            this.imageCollection1.Images.SetKeyName(9, "Label_text.png");
            this.imageCollection1.Images.SetKeyName(10, "Object.png");
            this.imageCollection1.Images.SetKeyName(11, "TerrainLayer.png");
            // 
            // btnStats
            // 
            this.btnStats.Location = new System.Drawing.Point(5, 369);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(93, 22);
            this.btnStats.StyleController = this.layoutControl1;
            this.btnStats.TabIndex = 5;
            this.btnStats.Text = "统      计";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // cbProperty
            // 
            this.cbProperty.Location = new System.Drawing.Point(5, 343);
            this.cbProperty.Name = "cbProperty";
            this.cbProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProperty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbProperty.Size = new System.Drawing.Size(180, 22);
            this.cbProperty.StyleController = this.layoutControl1;
            this.cbProperty.TabIndex = 4;
            this.cbProperty.SelectedIndexChanged += new System.EventHandler(this.cbProperty_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(689, 396);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层数";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(190, 318);
            this.layoutControlGroup2.Text = "图层树";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.treelist;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(184, 292);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "分类字段";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 318);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(190, 78);
            this.layoutControlGroup3.Text = "分类字段";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbProperty;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(184, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnStats;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(97, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.ucStatsOutput1;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(499, 396);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 370);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(556, 25);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // FrmPipeLineLengthStats2D
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(689, 396);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmPipeLineLengthStats2D";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管线长度统计";
            this.Load += new System.EventHandler(this.FrmPipeLineLengthStats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

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
                        if (!sc.Visible2D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 0;
                    }
                }
            }
        }

        private void BuildTree()
        {
            foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
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
                        if (!sc.Visible2D) continue;
                        TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                        scnode.StateImageIndex = 0;
                    }
                }
            }

            foreach (MajorClass mc in LogicDataStructureManage2D.Instance.RootMajorClasses)
            {
                TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, null);
                mcnode.StateImageIndex = 0;
                foreach (SubClass sc in mc.SubClasses)
                {
                    if (!sc.Visible2D) continue;
                    TreeListNode scnode = this.treelist.AppendNode(new object[] { sc.Name, sc }, mcnode);
                    scnode.StateImageIndex = 0;
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void FrmPipeLineLengthStats_Load(object sender, EventArgs e)
        {
            BuildTree();
            FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName("PipeLine");
            if (fac != null)
            {
                foreach (DFDataConfig.Class.FieldInfo fi in fac.FieldInfoCollection)
                {
                    if (fi.CanStats) this.cbProperty.Properties.Items.Add(fi);
                }
                this.cbProperty.SelectedIndex = 0;
            }
        }


        private void btnStats_Click(object sender, EventArgs e)
        {
            //string chartTitle = "管线长度统计图" + "单位：米";
            DoStats();
            this.ucStatsOutput1.SetData(dttemp);
            //this.ucStatsOutput1.SetData1(dtstats);
        }
        DataTable dttemp;
        DataTable dtstats;
        HashSet<string> list = new HashSet<string>();
        private void DoStats()
        {
            if (string.IsNullOrEmpty(this._sysFieldName)) return ;
            //string value = this.cbProperty.Text.Trim();
            //if (value.Length > 1)
            //{
            //    int lastindex = value.LastIndexOf(';');
            //    if (lastindex == (value.Length - 1))
            //        value = value.Substring(0, value.Length - 1);
            //}
            dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),
                                new DataColumn("FIELDNAME"),new DataColumn("PVALUE"),
                                new DataColumn("LENGTH",typeof(double)),new DataColumn("TOTALLENGTH",typeof(double))});
            dtstats = new DataTable();
            dtstats.Columns.AddRange(new DataColumn[]{new DataColumn("PIPELINETYPE"),new DataColumn("FIELDNAME"),
                                new DataColumn("LENGTH",typeof(double))});

            if (this.treelist.GetAllCheckedNodes() != null)
            {
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        double subclasslength = 0.0;
                        int indexStart = dttemp.Rows.Count;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facc == null || facc.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi = facc.GetFieldInfoBySystemName(this._sysFieldName);
                            DFDataConfig.Class.FieldInfo fiPipeLength = facc.GetFieldInfoBySystemName("PipeLength2D");
                            if (fi == null || fiPipeLength == null) continue;
                            int index = fc.Fields.FindField(fi.Name);
                            if (index == -1) continue;
                            int indexPipeLength = fc.Fields.FindField(fiPipeLength.Name);
                            if (indexPipeLength == -1) continue;
                            IField fcfi = fc.Fields.get_Field(index);
                            IQueryFilter filter = new QueryFilter();
                            filter.SubFields = fiPipeLength.Name;

                            //string[] arrvalue = value.Split(';');
                            if (list.Count <= 0) return;
                            foreach (string strValue in list)
                            {
                                if (string.IsNullOrEmpty(strValue)) continue;
                                switch (fcfi.Type)
                                {
                                    case esriFieldType.esriFieldTypeBlob:
                                    case esriFieldType.esriFieldTypeGeometry:
                                    case esriFieldType.esriFieldTypeRaster:
                                        continue;
                                }
                                filter.WhereClause = UpOrDown.DecorateWhereClasuse(fc) + sc.Parent.ClassifyField + " =  '" + sc.Name + "' and " + fi.Name + " = " + strValue;


                                IFeatureCursor pFeatureCursor = null;
                                IFeature pFeature = null;
                                double subfieldlength = 0.0;
                                bool bHave = false;
                                #region
                                try
                                {
                                    pFeatureCursor = fc.Search(filter, true);
                                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                                    {

                                        object tempobj = pFeature.get_Value(indexPipeLength);
                                        double dtemp = 0.0;
                                        if (tempobj != null && double.TryParse(tempobj.ToString(), out dtemp))
                                        {
                                            bHave = true;
                                            subfieldlength += dtemp;
                                        }
                                    }
                                }


                                catch { }
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
                                }
                                #endregion
                                if (bHave)
                                {
                                    DataRow dr = dttemp.NewRow();
                                    dr["PIPELINETYPE"] = sc;
                                    dr["FIELDNAME"] = fi;
                                    dr["PVALUE"] = strValue;
                                    subclasslength += subfieldlength;
                                    dr["LENGTH"] = subfieldlength.ToString("0.00");
                                    dttemp.Rows.Add(dr);

                                    DataRow dr1 = dtstats.NewRow();
                                    dr1["PIPELINETYPE"] = sc;
                                    dr1["FIELDNAME"] = strValue;
                                    dr1["LENGTH"] = subfieldlength.ToString("0.00");
                                    dtstats.Rows.Add(dr1);
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
           
            
        }

        private void treelist_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            
            IFeatureCursor pFeatureCursor = null;
            IFeature pFeature = null;
            if (string.IsNullOrEmpty(this._sysFieldName)) return;
            try
            {
                WaitForm.Start("正在加载列表...", "请稍后");
                
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    object obj = node.GetValue("NodeObject");
                    if (obj != null && obj is SubClass)
                    {
                        SubClass sc = obj as SubClass;
                        if (sc.Parent == null) continue;
                        string[] arrFc2DId = sc.Parent.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facClass = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null || facClass == null || facClass.Name != "PipeLine") continue;
                            DFDataConfig.Class.FieldInfo fi = facClass.GetFieldInfoBySystemName(this._sysFieldName);
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

        private void cbProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._sysFieldName = "";
            if (this.cbProperty.SelectedItem != null)
            {
                DFDataConfig.Class.FieldInfo fi = this.cbProperty.SelectedItem as DFDataConfig.Class.FieldInfo;
                this._sysFieldName = fi.SystemName;
            }
            treelist_AfterCheckNode(null, null);
            
        }

       

        
        
    }
}
