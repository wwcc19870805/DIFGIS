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
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using DFDataConfig.Logic;
using DF2DData.Class;
using DF2DDataTopological.LogicTree;
using DFDataConfig.Class;
using DFCommon.Class;
using DFWinForms.LogicTree;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using DFWinForms.Class;
using DF2DData.Class;


namespace DF2DDataTopological
{
    public partial class MainForm  : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnSave;
        private DevExpress.XtraTreeList.TreeList treelist;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private RadioGroup radioGroup1;
        private SpinEdit teTolerance;
        private SpinEdit teToleranceZ;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Object;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private IContainer components;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private CheckedListBoxControl clb_list;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;

        List<DF2DFeatureClass> listDF;
        Dictionary<string, Dictionary<IFeatureClass, IFeatureClass>> topoLayers;
        private ComboBoxEdit cbx;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        Dictionary<IFeatureClass, IFeatureClass> pointAndLine;
        ISpatialReference _spatialReference;
        IWorkspaceFactory pWsF = null;
        IFeatureWorkspace pFWs = null;
        ITable tbTopoLayers = null;
        string ObjectId;
        
    
        public MainForm()
        {
            InitializeComponent();
            topoLayers = new Dictionary<string, Dictionary<IFeatureClass, IFeatureClass>>();
            
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbx = new DevExpress.XtraEditors.ComboBoxEdit();
            this.clb_list = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.treelist = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Object = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.teTolerance = new DevExpress.XtraEditors.SpinEdit();
            this.teToleranceZ = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clb_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTolerance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teToleranceZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbx);
            this.layoutControl1.Controls.Add(this.clb_list);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.treelist);
            this.layoutControl1.Controls.Add(this.teTolerance);
            this.layoutControl1.Controls.Add(this.teToleranceZ);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(416, 412);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbx
            // 
            this.cbx.Location = new System.Drawing.Point(197, 25);
            this.cbx.Name = "cbx";
            this.cbx.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx.Size = new System.Drawing.Size(214, 22);
            this.cbx.StyleController = this.layoutControl1;
            this.cbx.TabIndex = 13;
            // 
            // clb_list
            // 
            this.clb_list.Location = new System.Drawing.Point(115, 134);
            this.clb_list.Name = "clb_list";
            this.clb_list.Size = new System.Drawing.Size(296, 247);
            this.clb_list.StyleController = this.layoutControl1;
            this.clb_list.TabIndex = 12;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(197, 77);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "是"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "否")});
            this.radioGroup1.Size = new System.Drawing.Size(214, 27);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(284, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treelist
            // 
            this.treelist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.Object});
            this.treelist.Location = new System.Drawing.Point(5, 25);
            this.treelist.Name = "treelist";
            this.treelist.OptionsBehavior.Editable = false;
            this.treelist.OptionsView.ShowCheckBoxes = true;
            this.treelist.OptionsView.ShowColumns = false;
            this.treelist.Size = new System.Drawing.Size(100, 382);
            this.treelist.TabIndex = 4;
            this.treelist.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treelist_AfterCheckNode);
            // 
            // NodeName
            // 
            this.NodeName.Caption = "名称";
            this.NodeName.FieldName = "NodeName";
            this.NodeName.MinWidth = 32;
            this.NodeName.Name = "NodeName";
            this.NodeName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.NodeName.Visible = true;
            this.NodeName.VisibleIndex = 0;
            // 
            // Object
            // 
            this.Object.Caption = "对象";
            this.Object.FieldName = "Object";
            this.Object.Name = "Object";
            this.Object.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.Object;
            // 
            // teTolerance
            // 
            this.teTolerance.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.teTolerance.Location = new System.Drawing.Point(197, 51);
            this.teTolerance.Name = "teTolerance";
            this.teTolerance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teTolerance.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teTolerance.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.teTolerance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teTolerance.Size = new System.Drawing.Size(214, 22);
            this.teTolerance.StyleController = this.layoutControl1;
            this.teTolerance.TabIndex = 6;
            // 
            // teToleranceZ
            // 
            this.teToleranceZ.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.teToleranceZ.Location = new System.Drawing.Point(197, 108);
            this.teToleranceZ.Name = "teToleranceZ";
            this.teToleranceZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teToleranceZ.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.teToleranceZ.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.teToleranceZ.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.teToleranceZ.Size = new System.Drawing.Size(214, 22);
            this.teToleranceZ.StyleController = this.layoutControl1;
            this.teToleranceZ.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(416, 412);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(110, 412);
            this.layoutControlGroup2.Text = "拓扑目录树";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treelist;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(104, 386);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.layoutControlGroup3.Location = new System.Drawing.Point(110, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(306, 412);
            this.layoutControlGroup3.Text = "拓扑层信息";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teTolerance;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(300, 26);
            this.layoutControlItem3.Text = "XY容差值：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(79, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teToleranceZ;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 83);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(300, 26);
            this.layoutControlItem5.Text = "Z容差值：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(79, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(169, 360);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 360);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(169, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.radioGroup1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(300, 31);
            this.layoutControlItem4.Text = "启用Z容差值：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(79, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.clb_list;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 109);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(300, 251);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cbx;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(300, 26);
            this.layoutControlItem8.Text = "拓扑层名称：";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(79, 14);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Group.png");
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(416, 412);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MainForm";
            this.Text = "拓扑信息配置工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clb_list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTolerance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teToleranceZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        private void LoadPipeLinelayers()
        {
            try
            {
                foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                {
                    TreeListNode node = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias, lg }, null);
                    node.StateImageIndex = 0;
                    foreach (MajorClass mc in lg.MajorClasses)
                    {
                        TreeListNode mcnode = this.treelist.AppendNode(new object[] { string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias, mc }, node);
                        mcnode.StateImageIndex = 0;
                        string[] arrFc2DId = mc.Fc2D.Split(';');
                        if (arrFc2DId == null) continue;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass facc = dffc.GetFacilityClass();
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null||facc == null || facc.Name != "PipeLine"|| facc.Name != "PipeNode") continue;
                            TreeListNode fcnode = this.treelist.AppendNode(new object[] { fc.AliasName, fc }, mcnode);
                            fcnode.StateImageIndex = 0;
                        }
                    }
                }
               
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BuildTree(treelist);
            if (listDF == null || listDF.Count <= 0) return;
            foreach (DF2DFeatureClass dffc in listDF)
            {
                if (dffc.GetFeatureClass().AliasName.Contains("辅助")) continue;
                DevExpress.XtraEditors.Controls.CheckedListBoxItem item = new CheckedListBoxItem();
                item.Description = dffc.GetFeatureClass().AliasName;
                item.Value = dffc;
                this.clb_list.Items.Add(item);             
            }
            
            
        }

        private void treelist_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                //pointAndLine.Clear();
                topoLayers.Clear();
                this.cbx.Properties.Items.Clear();
                foreach (CheckedListBoxItem item in this.clb_list.Items)
                {
                    item.CheckState = CheckState.Unchecked;
                }
                foreach (TreeListNode node in this.treelist.GetAllCheckedNodes())
                {
                    
                    object obj = node.GetValue("Object");                    
                    if (obj != null && obj is MajorClass)
                    {
                        IFeatureClass fcLine = null;
                        IFeatureClass fcPoint = null;
                        MajorClass mc = obj as MajorClass;
                        if (!node.HasChildren) continue;
                        foreach(TreeListNode subNode in node.Nodes)
                        {
                            object obj1 = subNode.GetValue("Object");
                            if (obj1 != null && obj1 is IFeatureClass)
                            {
                                IFeatureClass fc = obj1 as IFeatureClass;
                                if (fc.ShapeType == esriGeometryType.esriGeometryPolyline)
                                {
                                    fcLine = fc;
                                }
                                else if (fc.ShapeType == esriGeometryType.esriGeometryPoint)
                                {
                                    fcPoint = fc;
                                }
                                foreach (CheckedListBoxItem item in this.clb_list.Items)
                                {
                                    if (item.Value is DF2DFeatureClass)
                                    {
                                        DF2DFeatureClass dffc = item.Value as DF2DFeatureClass;
                                        if (dffc.GetFeatureClass().FeatureClassID == fc.FeatureClassID)
                                        {
                                            item.CheckState = CheckState.Checked;
                                            continue;
                                           
                                        }
                                       
                                    }
                                }
                            }
                        }
                        string topoLyrName = mc.Name;
                        pointAndLine = new Dictionary<IFeatureClass, IFeatureClass>();
                        if (pointAndLine.ContainsKey(fcPoint) || pointAndLine.ContainsValue(fcLine))
                            continue;
                        pointAndLine[fcPoint] = fcLine;
                        topoLayers[topoLyrName] = pointAndLine;
                        //if (fcLine != null && fcPoint != null)
                        //{
                        //    bool have = false;
                        //    foreach (KeyValuePair<IFeatureClass,IFeatureClass> kv in pointAndLine)
                        //    {
                        //        if (kv.Key == fcLine || kv.Value == fcPoint)
                        //        {
                        //            have = true;
                        //            break;
                        //        }
                                    
                        //    }
                        //    if (have) continue;
                        //    pointAndLine[fcPoint] = fcLine;
                        //    topoLayers[topoLyrName] = pointAndLine;
                        //}
                       

                    }

                }
                if (topoLayers.Count == 0)
                {
                    this.cbx.Properties.Items.Clear();
                    return;
                }
                this.cbx.Properties.Items.Clear();
                foreach (KeyValuePair<string, Dictionary<IFeatureClass, IFeatureClass>> s in topoLayers)
                {
                    this.cbx.Properties.Items.Add(s.Key);
                    
                }
                this.cbx.SelectedItem = 0;

                
            }
            catch
            {
            	
            }
           
        }

        private void BuildTree(DevExpress.XtraTreeList.TreeList parentTree)
        {
            try
            {
                if (treelist == null) return;
                IMapDocument pMapDoc = new MapDocument();
                string pFileName = Config.GetConfigValue("2DMxdPipe");
                pMapDoc.Open(pFileName, null);
                IMap pMap = pMapDoc.get_Map(0);
                this._spatialReference = pMap.SpatialReference;
                //Dictionary<string, DF2DFeatureClass> dict = new Dictionary<string ,DF2DFeatureClass>();
                listDF = new List<DF2DFeatureClass>();
                for (int i = 0; i < pMap.LayerCount; i++)//对地图图层进行遍历
                {
                    ILayer layer = pMap.get_Layer(i);
                    ReadMapLayer(layer, listDF);//读取该图层，并更新字典
                }
                if (listDF == null || listDF.Count == 0) return;
                foreach (LogicGroup lg in LogicDataStructureManage2D.Instance.RootLogicGroups)
                {
             
                    foreach (MajorClass mc in lg.MajorClasses)
                    {
                        TreeListNode tnmc = parentTree.AppendNode(new object[] { string.IsNullOrEmpty(lg.Alias) ? mc.Name : mc.Alias,mc }, (TreeListNode)null);                       

                        if (listDF.Count <= 0) return;

                        string[] arrayFc2D = mc.Fc2D.Split(';');
                        if (arrayFc2D == null || arrayFc2D.Count() == 0) return;
                        foreach (string fc2DID in arrayFc2D)
                        {
                            foreach (DF2DFeatureClass dffc in listDF)
                            {
                                IFeatureClass fc = dffc.GetFeatureClass();
                                if (fc.FeatureClassID.ToString() == fc2DID)
                                {
                                    if (fc.AliasName.Contains("辅助")) continue;
                                    TreeListNode tnsc = parentTree.AppendNode(new object[] {  fc.AliasName, fc }, tnmc);
                                }
                            }

                        }
                                               
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private void ReadMapLayer(ILayer layer, List<DF2DFeatureClass> list)
        {
            
            if (layer is ESRI.ArcGIS.Carto.IGroupLayer)
            {
                ICompositeLayer comLayer = layer as ICompositeLayer;
                for (int i = 0; i < comLayer.Count; i++)
                {
                    ILayer lyr = comLayer.get_Layer(i);
                    ReadMapLayer(lyr, list);
                }
            }
            if (layer is IGeoFeatureLayer)
            {
                IGeoFeatureLayer geoFtLayer = layer as IGeoFeatureLayer;
                if (geoFtLayer == null) return;
                if (geoFtLayer.FeatureClass == null) return;
                IFeatureClass fc = geoFtLayer.FeatureClass;
                string fcID = fc.FeatureClassID.ToString();
                list.Add(new DF2DFeatureClass(fc));
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            WaitForm.Start("准备建立拓扑，请稍后...",new Size(340,50));
            string path = Config.GetConfigValue("2DMdbTEMP");
            foreach (KeyValuePair<string, Dictionary<IFeatureClass, IFeatureClass>> tp in topoLayers)
            {
                foreach (KeyValuePair<IFeatureClass, IFeatureClass> kv in tp.Value)
                {
                    this.ObjectId = "Topo_" + kv.Key.FeatureClassID.ToString();
                    try
                    {
                        pWsF = new AccessWorkspaceFactory();
                        pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                        tbTopoLayers = pFWs.OpenTable("tbTopoLayers");
                        CreateTopoInMdb(tp.Key, kv.Key, kv.Value, path, this.ObjectId);
                    }
                    catch (System.Exception ex)
                    {
                        CreateTopoLayersTable(path);
                        tbTopoLayers = pFWs.OpenTable("tbTopoLayers");
                        CreateTopoInMdb(tp.Key, kv.Key, kv.Value, path, this.ObjectId);
                    }
                }
            }
            //foreach (KeyValuePair<IFeatureClass, IFeatureClass> kv in topoLayers[cbx.SelectedItem.ToString()])
            //{

            //    //DataTable dt = CreateTopo(cbx.SelectedItem.ToString(),kv.Key,kv.Value);
            //    //DataTable dt = CreateTopoBySpatial(cbx.SelectedItem.ToString(), kv.Key, kv.Value);
            //    this.ObjectId = "Topo_" + kv.Key.FeatureClassID.ToString();

            //    try
            //    {
            //        pWsF = new AccessWorkspaceFactory();
            //        pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
            //        tbTopoLayers = pFWs.OpenTable("tbTopoLayers");
            //        CreateTopoInMdb(cbx.SelectedItem.ToString(), kv.Key, kv.Value, path, this.ObjectId);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        CreateTopoLayersTable(path);
            //        tbTopoLayers = pFWs.OpenTable("tbTopoLayers");
            //    }
            //    CreateTableInMdb(dt, path, this.ObjectId);
            //}
            
        }
       
        private DataTable CreateTopo(string name,IFeatureClass fcPoint,IFeatureClass fcLine)
        {
            try
            {
                WaitForm.SetCaption("正在建立【" + name + "】拓扑表");
                DataTable dttemp = new DataTable();
                dttemp.TableName = name + "_Topo";
                dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("oid"), new DataColumn("GroupID"),new DataColumn("A_FC"),new DataColumn("Edge"),new DataColumn("P_FC"),
                                        new DataColumn("PNode"),new DataColumn("PDis"),new DataColumn("E_FC"),new DataColumn("ENode"),new DataColumn("EDis"),new DataColumn("ResistanceA"),
                                        new DataColumn("ResistanceB"),new DataColumn("Length"),new DataColumn("Geometry",typeof(IGeometry))});
                IQueryFilter pFilter = new QueryFilter();
                IFeature pFeature = null;
                IFeatureCursor pFCLine = fcLine.Search(pFilter, false);
                int rowCount = fcLine.FeatureCount(pFilter);

                IFeatureCursor pFCPoint = fcPoint.Search(pFilter, false);
                Dictionary<string, string> IDOid = new Dictionary<string, string>();
                while ((pFeature = pFCPoint.NextFeature()) != null)
                {
                    IFields pFields = fcPoint.Fields;
                    int indexID = pFields.FindField("DETECTID");
                    object objID = pFeature.get_Value(indexID);
                    int indexOid = pFields.FindField("OBJECTID");
                    //object objOid = pFeature.OID;
                    object objOid = pFeature.get_Value(indexOid);
                    IDOid[objID.ToString()] = objOid.ToString();
                }
                string a_FC = fcLine.FeatureClassID.ToString();
                int num = 1;
                IGeometry pGeo = null;
                while ((pFeature = pFCLine.NextFeature()) != null)
                {
                    string pOid = null ;
                    string eOid = null;
                    IFields pFields = fcLine.Fields;
                    int indexPNode = pFields.FindField("SNODEID");
                    object objPNode = pFeature.get_Value(indexPNode);
                    int indexENode = pFields.FindField("ENODEID");
                    object objENode = pFeature.get_Value(indexENode);
                    //int indexGeometry = pFields.FindField("SHAPE");
                    //object objGeometry = pFeature.get_Value(indexGeometry);
                    pGeo = pFeature.ShapeCopy;

                    if (IDOid.ContainsKey(objPNode.ToString()))
                    {
                        pOid = IDOid[objPNode.ToString()];
                    }
                    if (IDOid.ContainsKey(objENode.ToString()))
                    {
                        eOid = IDOid[objENode.ToString()];
                    }                   

                    DataRow dr = dttemp.NewRow();
                    dr["oid"] = num;
                    dr["GroupID"] = null;
                    dr["A_FC"] = a_FC;
                    dr["Edge"] = num;
                    dr["P_FC"] = fcPoint.FeatureClassID;
                    dr["PNode"] = pOid;
                    dr["PDis"] = 0;
                    dr["E_FC"] = fcPoint.FeatureClassID;
                    dr["ENode"] = eOid;
                    dr["EDis"] = 0;
                    dr["ResistanceA"] = null;
                    dr["ResistanceB"] = null;
                    dr["Length"] = null;
                    dr["Geometry"] = pGeo;

                    dttemp.Rows.Add(dr);
                    num++;                  
                }
                return dttemp;
            }
            catch
            {
                return null;
            }
        }

        private DataTable CreateTopoBySpatial(string name, IFeatureClass fcPoint,IFeatureClass fcLine)
        {
            IFeature pFeature = null;
            IPoint fromPoint = null;
            IPoint toPoint = null;
            IGeometry geo = null;
            IFeatureCursor pFCLine = null;
            try
            {
                WaitForm.SetCaption("正在建立【" + name + "】拓扑表");
                DataTable dttemp = new DataTable();
                dttemp.TableName = name + "_Topo";
                dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("oid"), new DataColumn("GroupID"),new DataColumn("A_FC"),new DataColumn("Edge"),new DataColumn("P_FC"),
                                        new DataColumn("PNode"),new DataColumn("PDis"),new DataColumn("E_FC"),new DataColumn("ENode"),new DataColumn("EDis"),new DataColumn("ResistanceA"),
                                        new DataColumn("ResistanceB"),new DataColumn("Length"),new DataColumn("Geometry",typeof(IGeometry))});
                
                ISpatialFilter filter = new SpatialFilter();
                double tolerance = 0.0;
                Double.TryParse(this.teTolerance.Text,out tolerance);
                pFCLine = fcLine.Search(null, false);
                int rowCount = fcLine.FeatureCount(null);
                int num = 0;
                while ((pFeature = pFCLine.NextFeature()) != null)
                {
                    try
                    {
                        num++;
                        WaitForm.SetCaption("正在构建【" + name + "】拓扑表(" + num + "/" + rowCount + ")");
                        DataRow dr = dttemp.NewRow();
                        dr["oid"] = num;
                        dr["GroupID"] = null;
                        dr["A_FC"] = fcLine.FeatureClassID.ToString();
                        dr["Edge"] = num;
                        dr["P_FC"] = fcPoint.FeatureClassID;
                        dr["ResistanceA"] = null;
                        dr["ResistanceB"] = null;
                        dr["Length"] = null;
                        dr["Geometry"] = pFeature.ShapeCopy;
                        if (pFeature.Shape is IPolyline)
                        {
                            IFeature feature = null;
                            IFeatureCursor cursor = null;
                            IPolyline polyline = pFeature.Shape as IPolyline;
                            fromPoint = polyline.FromPoint;
                            toPoint = polyline.ToPoint;
                            geo = DoBuffer(fromPoint, tolerance);

                            filter.Geometry = geo;
                            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            cursor = fcPoint.Search(filter, false);
                            //int count = fcPoint.FeatureCount(filter);
                            if ((feature = cursor.NextFeature()) != null)
                            {
                                dr["P_FC"] = fcPoint.FeatureClassID;
                                dr["PNode"] = feature.OID;
                                dr["PDis"] = tolerance;
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;

                            }
                            else
                            {
                                dr["P_FC"] = fcPoint.FeatureClassID;
                                dr["PNode"] = null;
                                dr["PDis"] = tolerance;
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;

                            }

                            geo = DoBuffer(toPoint, tolerance);
                            filter.Geometry = geo;
                            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            cursor = fcPoint.Search(filter, false);
                            //count = fcPoint.FeatureCount(filter);
                            if ((feature = cursor.NextFeature()) != null)
                            {
                                dr["E_FC"] = fcPoint.FeatureClassID;
                                dr["ENode"] = feature.OID;
                                dr["EDis"] = tolerance;
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;

                            }
                            else
                            {
                                dr["E_FC"] = fcPoint.FeatureClassID;
                                dr["ENode"] = null;
                                dr["EDis"] = tolerance;
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;

                            }


                        }
                        dttemp.Rows.Add(dr);
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                  
                }
                return dttemp;
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("创建拓扑信息出错。\r\n错误信息：" + ex.Message, "提示");
                return null;
            }
            finally
            {
              
                if (pFCLine != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCLine);
                    pFCLine = null;
                }
            }
        }

        private void CreateTopoInMdb(string name, IFeatureClass fcPoint, IFeatureClass fcLine, string path,string objectId)
        {
            IFeature pFeature = null;
            IPoint fromPoint = null;
            IPoint toPoint = null;
            IGeometry geo = null;
            IFeatureCursor pFCLine = null;
            IQueryFilter pFilter = null;
            IWorkspaceFactory pWsFt = null;
            IFeatureWorkspace pWs = null;
            ITable table = null;
            IFeatureClass pFc = null;
            int rowCount1 = fcLine.FeatureCount(null);
            try
            {
                WaitForm.SetCaption("正在建立【" + name + "】拓扑表");
                DataTable dttemp = new DataTable();
                dttemp.TableName = name + "_Topo";
                dttemp.Columns.AddRange(new DataColumn[]{new DataColumn("oid"), new DataColumn("GroupID"),new DataColumn("A_FC"),new DataColumn("Edge"),new DataColumn("P_FC"),
                                        new DataColumn("PNode"),new DataColumn("PDis"),new DataColumn("E_FC"),new DataColumn("ENode"),new DataColumn("EDis"),new DataColumn("ResistanceA"),
                                        new DataColumn("ResistanceB"),new DataColumn("Length"),new DataColumn("Geometry",typeof(IGeometry))});
                #region 构建FeatureClass字段


                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = pGeoDef as IGeometryDefEdit;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                pGeoDefEdit.SpatialReference_2 = this._spatialReference;
                pGeoDefEdit.HasZ_2 = true;
                pGeoDefEdit.HasM_2 = true;


                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                IField pField0 = new FieldClass();
                IFieldEdit pFieldEdit0 = pField0 as IFieldEdit;
                pFieldEdit0.Name_2 = "OBJECTID";
                pFieldEdit0.Type_2 = esriFieldType.esriFieldTypeOID;
                pFieldsEdit.AddField(pField0);
                foreach (DataColumn dc in dttemp.Columns)
                {
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Name_2 = dc.ColumnName;
                    switch (dc.ColumnName)
                    {
                        case "oid":
                        case "Edge":
                        case "P_FC":
                        case "PNode":
                        case "E_FC":
                        case "ENode":
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                            break;
                        case "GroupID":
                        case "A_FC":
                        case "PDis":
                        case "EDis":
                        case "ResistanceA":
                        case "ResistanceB":
                        case "Length":
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                            break;
                        case "Geometry":
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                            pFieldEdit.GeometryDef_2 = pGeoDef;
                            break;
                    }
                    pFieldsEdit.AddField(pField);
                }
                #endregion

                pWsFt = new AccessWorkspaceFactory();
                pWs = pWsFt.OpenFromFile(path, 0) as IFeatureWorkspace;//获得数据库工作空间
                try
                {
                    pFc = pWs.CreateFeatureClass(dttemp.TableName, pFields, null, null, esriFeatureType.esriFTSimple, "Geometry", null);
                }
                catch (System.Exception ex)
                {
                    pFc = pWs.OpenFeatureClass(dttemp.TableName);
                    if (pFc != null)
                    {
                        IDataset pDataset = pFc as IDataset;
                        pDataset.Delete();
                    }
                    pFc = pWs.CreateFeatureClass(dttemp.TableName, pFields, null, null, esriFeatureType.esriFTSimple, "Geometry", null);
                }

                ISpatialFilter filter = new SpatialFilter();
                double tolerance = 0.0;
                Double.TryParse(this.teTolerance.Text, out tolerance);

                pFilter = new QueryFilter();
                pFilter.WhereClause = "UPORDOWN = " + 2;
                pFCLine = fcLine.Search(pFilter, true);
                int rowCount = fcLine.FeatureCount(pFilter);
                int num = 0;

                #region 空间查询线段起止点，并写入数据库
                while ((pFeature = pFCLine.NextFeature()) != null)
                {
                    try
                    {
                        num++;
                        WaitForm.SetCaption("正在构建【" + name + "】拓扑表(" + num + "/" + rowCount + ")");
                        //DataRow dr = dttemp.NewRow();
                        IFeature featuretemp = null;
                        featuretemp = pFc.CreateFeature(); 
                        featuretemp.set_Value(1, num);
                        featuretemp.set_Value(2, null);
                        featuretemp.set_Value(3, fcLine.FeatureClassID.ToString());
                        featuretemp.set_Value(4, num);
                        featuretemp.set_Value(5, fcPoint.FeatureClassID.ToString());
                        featuretemp.set_Value(8, fcPoint.FeatureClassID.ToString());
                        featuretemp.set_Value(11, null);
                        featuretemp.set_Value(12, null);
                        featuretemp.set_Value(13, null);
                        featuretemp.set_Value(14, pFeature.ShapeCopy);                    
                        if (pFeature.Shape is IPolyline)
                        {
                            IFeature feature = null;
                            IFeatureCursor cursor = null;
                            IPolyline polyline = pFeature.Shape as IPolyline;
                            fromPoint = polyline.FromPoint;
                            toPoint = polyline.ToPoint;
                            geo = DoBuffer(fromPoint, tolerance);

                            filter.Geometry = geo;
                            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            filter.WhereClause = "UPORDOWN = " + 2; ;
                            cursor = fcPoint.Search(filter, true);
                            //int count = fcPoint.FeatureCount(filter);
                            double dis = 0.0;
                            IFeature ftemp = null;
                            feature = cursor.NextFeature();
                            if (feature != null)
                            {
                                dis = GetDistance(fromPoint, feature.Shape as IPoint);
                                int oid1 = feature.OID;
                                while ((ftemp = cursor.NextFeature()) != null)
                                {
                                    double distemp = 0.0;
                                    IPoint point = feature.Shape as IPoint;
                                    distemp = GetDistance(fromPoint, point);
                                    if (distemp < dis)
                                    {
                                        dis = distemp;
                                        feature = ftemp;
                                    }
                                }

                                featuretemp.set_Value(6, oid1);
                                featuretemp.set_Value(7, tolerance);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;
                            }
                            else
                            {
                                featuretemp.set_Value(6, null);
                                featuretemp.set_Value(7, tolerance);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;
                            }
                            
                            
                            geo = DoBuffer(toPoint, tolerance);
                            filter.Geometry = geo;
                            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            filter.WhereClause = "UPORDOWN = " + 2; ;
                            cursor = fcPoint.Search(filter, true);
                            //count = fcPoint.FeatureCount(filter);
                            dis = 0.0;
                            
                            feature = cursor.NextFeature();
                            if (feature != null)
                            {
                                dis = GetDistance(toPoint, feature.Shape as IPoint);
                                int oid2 = feature.OID;
                                while ((ftemp = cursor.NextFeature()) != null)
                                {
                                    double distemp = 0.0;
                                    IPoint point = feature.Shape as IPoint;
                                    distemp = GetDistance(fromPoint, point);
                                    if (distemp < dis)
                                    {
                                        dis = distemp;
                                        feature = ftemp;
                                    }
                                }

                                featuretemp.set_Value(9, oid2);
                                featuretemp.set_Value(10, tolerance);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                
                                cursor = null;
                                
                            }                          
                            else
                            {
                                featuretemp.set_Value(9, null);
                                featuretemp.set_Value(10, tolerance);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);                                
                                cursor = null;

                            }
                            featuretemp.Store();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(featuretemp);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(ftemp);
                            ftemp = null;
                            featuretemp = null;

                        }
             
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }

                }
                #endregion

                IQueryFilter filtertemp = new QueryFilter();
                filtertemp.WhereClause = "OID = '" + objectId + "'";
                ICursor pCursor = tbTopoLayers.Search(filtertemp, true);
                IRow row = null;
                while ((row = pCursor.NextRow()) != null)
                {
                    int index = row.Fields.FindField("OID");
                    if (row.get_Value(index).ToString() == objectId)
                    {
                        row.Delete();
                        break;
                    }
                }

                row = tbTopoLayers.CreateRow();
                row.set_Value(1, objectId);
                row.set_Value(2, dttemp.TableName);
                row.set_Value(3, this.teTolerance.Text);
                row.set_Value(4, this.teToleranceZ.Text);
                if (this.radioGroup1.SelectedIndex == 0) row.set_Value(5, true);
                else row.set_Value(5, false);
                row.set_Value(6, dttemp.TableName);
                row.set_Value(7, null);
                row.Store();



                WaitForm.SetCaption("【" + dttemp.TableName + "】拓扑表建立成功");
                WaitForm.Stop();
            }
            catch
            {

            }
        }
        
        private void CreateTopoLayersTable(string path)
        {
            try
            {
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IFeatureWorkspace pFWs = pWsF.OpenFromFile(path, 0) as IFeatureWorkspace;
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                IField pField0 = new FieldClass();
                IFieldEdit pFieldEdit0 = pField0 as IFieldEdit;
                pFieldEdit0.Name_2 = "OBJECTID";
                pFieldEdit0.Type_2 = esriFieldType.esriFieldTypeOID;        
                pFieldsEdit.AddField(pField0);

                string[] columns = new string[] { "OID", "TopoLayerName", "Tolerance", "ToleranceZ", "IgnoreZ", "TopoTableName", "Comment" };
                foreach (string col in columns)
                {
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Name_2 = col;
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    pFieldsEdit.AddField(pField);
                }
                pFWs.CreateTable("tbTopoLayers", pFields, null, null, null);
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }
        private  void CreateTableInMdb(DataTable dt, string path,string objectId)
        {
            IWorkspaceFactory pWsFt = null;
            IFeatureWorkspace pWs = null;
            ITable table = null;
            IFeatureClass pFc = null;
            try
            {
                
                WaitForm.SetCaption("正在将【" + dt.TableName + "】拓扑表写入数据库");

                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = pGeoDef as IGeometryDefEdit;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                pGeoDefEdit.SpatialReference_2 = this._spatialReference;
                pGeoDefEdit.HasZ_2 = true;
                pGeoDefEdit.HasM_2 = true;
                

                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                IField pField0 = new FieldClass();
                IFieldEdit pFieldEdit0 = pField0 as IFieldEdit;
                pFieldEdit0.Name_2 = "OBJECTID";
                pFieldEdit0.Type_2 = esriFieldType.esriFieldTypeOID;
                pFieldsEdit.AddField(pField0);

                
                //IField pField1 = new FieldClass();
                //IFieldEdit pFieldEdit1 = pField1 as IFieldEdit;
                //pFieldEdit1.Name_2 = "SHAPE";
                //pFieldEdit1.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //pFieldEdit1.GeometryDef_2 = pGeoDef;
                //pFieldsEdit.AddField(pField1);
                
             

                foreach (DataColumn dc in dt.Columns)
                {
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Name_2 = dc.ColumnName;
                    switch (dc.ColumnName)
                    {
                        case "oid":
                        case "Edge":
                        case "P_FC":
                        case "PNode":
                        case "E_FC":
                        case "ENode":
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                            break;
                        case "GroupID":
                        case "A_FC":
                        case "PDis":
                        case "EDis":
                        case "ResistanceA":
                        case "ResistanceB":
                        case "Length":
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                            break;
                        case "Geometry":
                            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                            pFieldEdit.GeometryDef_2 = pGeoDef;
                            break;
                    }
                    pFieldsEdit.AddField(pField);
                }

                pWsFt = new AccessWorkspaceFactory();
                pWs = pWsFt.OpenFromFile(path, 0) as IFeatureWorkspace;
                WaitForm.SetCaption("正在将已有【" + dt.TableName + "】拓扑表删除");
                

               try
               {
                   pFc = pWs.CreateFeatureClass(dt.TableName, pFields, null, null, esriFeatureType.esriFTSimple, "Geometry", null);
               }
               catch (System.Exception ex)
               {
                   pFc = pWs.OpenFeatureClass(dt.TableName);
                   if (pFc != null)
                   {
                       IDataset pDataset = pFc as IDataset;
                       pDataset.Delete();
                   }
                   pFc = pWs.CreateFeatureClass(dt.TableName, pFields, null, null, esriFeatureType.esriFTSimple, "Geometry", null);
               }
               

                
                int n = 0;
                int nn = dt.Rows.Count;
                             
                foreach (DataRow dr in dt.Rows)
                {
                    n++;
                    WaitForm.SetCaption("正在将【" + dt.TableName + "】拓扑表写入数据库(" + n + "/" + nn + ")");
                    pFc = pWs.OpenFeatureClass(dt.TableName); 
                    IFeature pFeature = pFc.CreateFeature();
                    //int indexShape = pFeature.Fields.FindField(pFc.ShapeFieldName);
                    //IGeometryDef pGeometryDef = pFeature.Fields.get_Field(indexShape).GeometryDef as IGeometryDef;
                    try
                    {
                        for (int i = 1; i < pFeature.Fields.FieldCount - 1; i++)
                        {
                            string name = pFeature.Fields.Field[i].Name;
                            pFeature.set_Value(i, dr[i - 1]);
                            pFeature.Store();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                   
                    
                }
                if (this.tbTopoLayers == null)
                {
                    XtraMessageBox.Show("未成功构建图层拓扑信息对照表", "提示");
                    return;
                }
                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = "OID = '" + objectId + "'";
                ICursor pCursor = tbTopoLayers.Search(filter, false);
                IRow row = null;
                while ((row = pCursor.NextRow()) != null)
                {
                    int index = row.Fields.FindField("OID");
                    if (row.get_Value(index).ToString() == objectId)
                    {
                        row.Delete();
                        break;
                    }
                }
            
                row = tbTopoLayers.CreateRow();
                row.set_Value(1, objectId);
                row.set_Value(2, dt.TableName);
                row.set_Value(3, this.teTolerance.Text);
                row.set_Value(4, this.teToleranceZ.Text);
                if (this.radioGroup1.SelectedIndex == 0) row.set_Value(5, true);
                else row.set_Value(5, false);
                row.set_Value(6, dt.TableName);
                row.set_Value(7, null);
                row.Store();
            
              

                WaitForm.SetCaption("【" + dt.TableName + "】拓扑表建立成功");
                WaitForm.Stop();
                
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show("【" + dt.TableName + "】创建拓扑信息出错。\r\n错误信息：" + ex.Message, "提示");
            }
            finally
            {
               
                if (pWs != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pWs);
                    pWs = null;
                }
                if (pFc != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFc);
                    pFc = null;
                }
                
            }
        }

        private static IGeometry DoBuffer(IGeometry pGeo, double distance)
        {
            try
            {
                ITopologicalOperator ptopo;
                ptopo = pGeo as ITopologicalOperator;
                if (ptopo != null)
                {
                    IGeometry geo = ptopo.Buffer(distance);
                    return geo;
                }
            }
            catch
            {
                //                //“线”“面”上如果几个点坐标相同，buffer就会失败
                //                Console.WriteLine(ex.Message+"\r\n"+ex.StackTrace);
                //                ShowGeometryAllPoints(pGeo);
            }
            return null;
        }

        private double GetDistance(IPoint point1, IPoint point2)
        {
            double dis;
            double x1 = point1.X;
            double y1 = point1.Y;
            double x2 = point2.X;
            double y2 = point2.Y;

            return dis = Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
        }
       
    }
}
