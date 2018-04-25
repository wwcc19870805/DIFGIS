using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Xml;
using DFDataConfig.Logic;
using DFDataConfig.Tree;
using DFWinForms.LogicTree;
using DevExpress.XtraTreeList.Nodes;
using DFCommon.Class;

namespace DFDataConfig.Frm
{
    public class FormLogicData3DManage : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private SimpleButton btnEdit;
        private SimpleButton btnCreate;
        private TextEdit teClassifyField;
        private TextEdit teAlias;
        private TextEdit teName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private LogicBaseTree logicBaseTree1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogicData3DManage));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.logicBaseTree1 = new DFWinForms.LogicTree.LogicBaseTree();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.teClassifyField = new DevExpress.XtraEditors.TextEdit();
            this.teAlias = new DevExpress.XtraEditors.TextEdit();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teClassifyField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAlias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.logicBaseTree1);
            this.layoutControl1.Controls.Add(this.btnEdit);
            this.layoutControl1.Controls.Add(this.btnCreate);
            this.layoutControl1.Controls.Add(this.teClassifyField);
            this.layoutControl1.Controls.Add(this.teAlias);
            this.layoutControl1.Controls.Add(this.teName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(560, 189, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(398, 313);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // logicBaseTree1
            // 
            this.logicBaseTree1.AllowDragLayer = false;
            this.logicBaseTree1.AllowMultiSelect = false;
            this.logicBaseTree1.AutoHide = false;
            this.logicBaseTree1.Caption = "";
            this.logicBaseTree1.CustomColumnCaption = "${res:Interface_类型}";
            this.logicBaseTree1.ID = null;
            this.logicBaseTree1.IsActive = false;
            this.logicBaseTree1.Location = new System.Drawing.Point(2, 2);
            this.logicBaseTree1.Name = "logicBaseTree1";
            this.logicBaseTree1.Pos = null;
            this.logicBaseTree1.ShowCheckBoxes = false;
            this.logicBaseTree1.ShowCloseButton = false;
            this.logicBaseTree1.ShowColorColumn = false;
            this.logicBaseTree1.ShowColumnHeader = false;
            this.logicBaseTree1.ShowCustomColumn = false;
            this.logicBaseTree1.ShowExtendItem = false;
            this.logicBaseTree1.ShowLayerControlBar = false;
            this.logicBaseTree1.ShowPopMenu = true;
            this.logicBaseTree1.ShowQuickSearchBar = true;
            this.logicBaseTree1.ShowRootLine = true;
            this.logicBaseTree1.Size = new System.Drawing.Size(183, 309);
            this.logicBaseTree1.TabIndex = 10;
            this.logicBaseTree1.Title = null;
            this.logicBaseTree1.OnHitTest += new DFWinForms.LogicTree.OnHitTestHandler<DFWinForms.LogicTree.IBaseLayer>(this.logicBaseTree1_OnHitTest);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(297, 80);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 22);
            this.btnEdit.StyleController = this.layoutControl1;
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "保存";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(189, 80);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(104, 22);
            this.btnCreate.StyleController = this.layoutControl1;
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "创建";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // teClassifyField
            // 
            this.teClassifyField.Location = new System.Drawing.Point(244, 54);
            this.teClassifyField.Name = "teClassifyField";
            this.teClassifyField.Size = new System.Drawing.Size(152, 22);
            this.teClassifyField.StyleController = this.layoutControl1;
            this.teClassifyField.TabIndex = 7;
            // 
            // teAlias
            // 
            this.teAlias.Location = new System.Drawing.Point(244, 28);
            this.teAlias.Name = "teAlias";
            this.teAlias.Size = new System.Drawing.Size(152, 22);
            this.teAlias.StyleController = this.layoutControl1;
            this.teAlias.TabIndex = 6;
            // 
            // teName
            // 
            this.teName.Location = new System.Drawing.Point(244, 2);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(152, 22);
            this.teName.StyleController = this.layoutControl1;
            this.teName.TabIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(398, 313);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(187, 104);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(211, 209);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teName;
            this.layoutControlItem2.CustomizationFormText = "名称：";
            this.layoutControlItem2.Location = new System.Drawing.Point(187, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(211, 26);
            this.layoutControlItem2.Text = "名称：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teAlias;
            this.layoutControlItem3.CustomizationFormText = "别名：";
            this.layoutControlItem3.Location = new System.Drawing.Point(187, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(211, 26);
            this.layoutControlItem3.Text = "别名：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teClassifyField;
            this.layoutControlItem4.CustomizationFormText = "分组字段";
            this.layoutControlItem4.Location = new System.Drawing.Point(187, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(211, 26);
            this.layoutControlItem4.Text = "分组字段:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCreate;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(187, 78);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnEdit;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(295, 78);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.logicBaseTree1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(187, 313);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FormLogicDataManage
            // 
            this.ClientSize = new System.Drawing.Size(398, 313);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormLogicDataManage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "逻辑分组配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogicDataManage_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teClassifyField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAlias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private string _xmlFileName;
        public FormLogicData3DManage()
        {
            InitializeComponent();
            this._xmlFileName = Config.GetConfigValue("LogicDataStructureXmlPath3D");
            LoadData(this._xmlFileName);
            this.logicBaseTree1.OnInitRootPopMenu += new OnInitRootPopMenuHandler(logicBaseTree1_OnInitRootPopMenu);
            this.logicBaseTree1.OnRootPopMenuItemClick += new OnRootPopMenuItemClickHandler(logicBaseTree1_OnRootPopMenuItemClick);
        }

        private void logicBaseTree1_OnRootPopMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "创建组":
                    currentLayer = null;
                    CreateLogicGroup();
                    break;
                case "创建大类":
                    currentLayer = null;
                    CreateMajorClass();
                    break;
                case "全部展开":
                    this.logicBaseTree1.TreeList.ExpandAll();
                    break;
                case "全部折叠":
                    this.logicBaseTree1.TreeList.CollapseAll();
                    break;
            }
        }

        private void logicBaseTree1_OnInitRootPopMenu()
        {
            this.logicBaseTree1.AddMenuItems(new string[] { "创建组", "创建大类", "", "全部展开", "全部折叠" });
        }

        private void RecursiveLoadData(XmlNodeList list, LogicGroup parent, GroupLayerClass parentLayer)
        {
            if (list == null) return;
            foreach (XmlNode node in list)
            {
                if (node.Name == "LogicGroup")
                {
                    string fieldName = "", fieldAliasName = "";
                    if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                    if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                    LogicGroup lg = new LogicGroup(fieldName, fieldAliasName, parent);
                    if (dictGroups.ContainsKey(lg.Name)) continue;
                    dictGroups[lg.Name] = lg;
                    parent.LogicGroups.Add(lg);
                    TreeNodeLogicGroup treenode = new TreeNodeLogicGroup()
                    {
                        Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias,
                        CustomValue = lg
                    };
                    parentLayer.Add2(treenode);
                    RecursiveLoadData(node.ChildNodes, lg, parentLayer);
                }
                if (node.Name == "MajorClass")
                {
                    string fieldName = "", fieldAliasName = "", classifyFieldName = "", fc2D = "", fc3D = "";
                    if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                    if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                    if (node.Attributes["classifyfield"] != null) classifyFieldName = node.Attributes["classifyfield"].Value.Trim();
                    if (node.Attributes["fc2D"] != null) fc2D = node.Attributes["fc2D"].Value.Trim();
                    if (node.Attributes["fc3D"] != null) fc3D = node.Attributes["fc3D"].Value.Trim();
                    MajorClass mc = new MajorClass(fieldName, fieldAliasName, classifyFieldName, fc2D, fc3D, parent);
                    if (dictMCs.ContainsKey(mc.Name)) continue;
                    dictMCs[mc.Name] = mc;
                    parent.MajorClasses.Add(mc);
                    foreach (XmlNode cnode in node.ChildNodes)
                    {
                        if (cnode.Name != "SubClass") continue;
                        string scname = "", scgroupid = "";
                        if (cnode.Attributes["name"] != null) { scname = cnode.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(scname)) continue; }
                        if (cnode.Attributes["groupid"] != null) scgroupid = cnode.Attributes["groupid"].Value.Trim();
                        int groupid = 0;
                        if (!int.TryParse(scgroupid, out groupid)) groupid = 0;
                        SubClass sc = new SubClass(scname, groupid, mc);
                        mc.SubClasses.Add(sc);
                    }
                    TreeNodeMajorClass treenode = new TreeNodeMajorClass()
                    {
                        Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias,
                        CustomValue = mc
                    };
                    parentLayer.Add2(treenode);
                }
            }
        }
        private Dictionary<string, LogicGroup> dictGroups = new Dictionary<string, LogicGroup>();
        private Dictionary<string, MajorClass> dictMCs = new Dictionary<string, MajorClass>();
        public void LoadData(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            try
            {
                if (!File.Exists(fileName))
                {
                    string path = System.IO.Path.GetDirectoryName(fileName) + "\\";
                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                        if (di == null) return;
                    }
                    XmlDocument document = new XmlDocument();
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                    document.AppendChild(newChild);
                    XmlNode node = document.CreateElement("LogicDataStructure");
                    document.AppendChild(node);
                    document.Save(fileName);
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                if (xmlDoc == null) return;
                XmlNode root = xmlDoc.SelectSingleNode("LogicDataStructure");
                if (root == null) return;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name == "LogicGroup")
                    {
                        string fieldName = "", fieldAliasName = "";
                        if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                        if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                        LogicGroup lg = new LogicGroup(fieldName, fieldAliasName, null);
                        if (dictGroups.ContainsKey(lg.Name)) continue;
                        dictGroups[lg.Name] = lg;
                        TreeNodeLogicGroup treenode = new TreeNodeLogicGroup()
                        {
                            Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias,
                            CustomValue = lg
                        };
                        treenode.OwnNode = this.logicBaseTree1.TreeList.AppendNode(new object[] { treenode.Name }, (TreeListNode)null);
                        RecursiveLoadData(node.ChildNodes, lg, treenode);
                    }
                    if (node.Name == "MajorClass")
                    {
                        string fieldName = "", fieldAliasName = "", classifyFieldName = "", fc2D = "", fc3D = "";
                        if (node.Attributes["name"] != null) { fieldName = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                        if (node.Attributes["alias"] != null) fieldAliasName = node.Attributes["alias"].Value.Trim();
                        if (node.Attributes["classifyfield"] != null) classifyFieldName = node.Attributes["classifyfield"].Value.Trim();
                        if (node.Attributes["fc2D"] != null) fc2D = node.Attributes["fc2D"].Value.Trim();
                        if (node.Attributes["fc3D"] != null) fc3D = node.Attributes["fc3D"].Value.Trim();
                        MajorClass mc = new MajorClass(fieldName, fieldAliasName, classifyFieldName, fc2D, fc3D, null);
                        if (dictMCs.ContainsKey(mc.Name)) continue;
                        dictMCs[mc.Name] = mc;
                        foreach (XmlNode cnode in node.ChildNodes)
                        {
                            if (cnode.Name != "SubClass") continue;
                            string scname = "", scgroupid = "";
                            if (cnode.Attributes["name"] != null) { scname = cnode.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(scname)) continue; }
                            if (cnode.Attributes["groupid"] != null) scgroupid = cnode.Attributes["groupid"].Value.Trim();
                            int groupid = 0;
                            if (!int.TryParse(scgroupid, out groupid)) groupid = 0;
                            SubClass sc = new SubClass(scname, groupid, mc);
                            mc.SubClasses.Add(sc);
                        }
                        TreeNodeMajorClass treenode = new TreeNodeMajorClass()
                        {
                            Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias,
                            CustomValue = mc
                        };
                        treenode.OwnNode = this.logicBaseTree1.TreeList.AppendNode(new object[] { treenode.Name }, (TreeListNode)null);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void RecurSaveData(TreeNodeLogicGroup parentLayer, XmlDocument document, XmlNode parentNode)
        {
            if (parentLayer == null || parentNode == null) return;
            for (int i = 0; i < parentLayer.GetLayerCount(); i++)
            {
                IBaseLayer layer = parentLayer.GetLayerByIndex(i);
                if (!(layer is TreeNodeLogicGroup) && !(layer is TreeNodeMajorClass)) continue;
                if (layer is TreeNodeLogicGroup)
                {
                    TreeNodeLogicGroup treenode = layer as TreeNodeLogicGroup;
                    if (!(treenode.CustomValue is LogicGroup)) continue;
                    LogicGroup lg = treenode.CustomValue as LogicGroup;
                    XmlElement element = document.CreateElement("LogicGroup");
                    element.SetAttribute("name", lg.Name);
                    element.SetAttribute("alias", lg.Alias);
                    XmlNode lgNode = parentNode.AppendChild(element);
                    RecurSaveData(treenode, document, lgNode);
                }
                if (layer is TreeNodeMajorClass)
                {
                    TreeNodeMajorClass treenode = layer as TreeNodeMajorClass;
                    if (treenode.CustomValue is MajorClass)
                    {
                        MajorClass mc = treenode.CustomValue as MajorClass;
                        XmlElement element = document.CreateElement("MajorClass");
                        element.SetAttribute("name", mc.Name);
                        element.SetAttribute("alias", mc.Alias);
                        element.SetAttribute("classifyfield", mc.ClassifyField);
                        element.SetAttribute("fc2D", mc.Fc2D);
                        element.SetAttribute("fc3D", mc.Fc3D);
                        XmlNode mcNode = parentNode.AppendChild(element);
                        foreach (SubClass sc in mc.SubClasses)
                        {
                            XmlElement ele1 = document.CreateElement("SubClass");
                            ele1.SetAttribute("name", sc.Name);
                            ele1.SetAttribute("groupid", sc.GroupId.ToString());
                            mcNode.AppendChild(ele1);
                        }
                    }
                }
            }
        }

        private bool SaveData(string fileName)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);
                XmlNode node = document.CreateElement("LogicDataStructure");
                document.AppendChild(node);
                List<IBaseLayer> logicBaseTree1GetRootLayers = this.logicBaseTree1.GetRootLayers();
                if (logicBaseTree1GetRootLayers != null)
                {
                    foreach (IBaseLayer layer in logicBaseTree1GetRootLayers)
                    {
                        if (!(layer is TreeNodeLogicGroup) && !(layer is TreeNodeMajorClass)) continue;
                        if (layer is TreeNodeLogicGroup)
                        {
                            TreeNodeLogicGroup treenode = layer as TreeNodeLogicGroup;
                            if (!(treenode.CustomValue is LogicGroup)) continue;
                            LogicGroup lg = treenode.CustomValue as LogicGroup;
                            XmlElement element = document.CreateElement("LogicGroup");
                            element.SetAttribute("name", lg.Name);
                            element.SetAttribute("alias", lg.Alias);
                            XmlNode lgNode = node.AppendChild(element);
                            RecurSaveData(treenode, document, lgNode);
                        }
                        if (layer is TreeNodeMajorClass)
                        {
                            TreeNodeMajorClass treenode = layer as TreeNodeMajorClass;
                            if (treenode.CustomValue is MajorClass)
                            {
                                MajorClass mc = treenode.CustomValue as MajorClass;
                                XmlElement element = document.CreateElement("MajorClass");
                                element.SetAttribute("name", mc.Name);
                                element.SetAttribute("alias", mc.Alias);
                                element.SetAttribute("classifyfield", mc.ClassifyField);
                                element.SetAttribute("fc2D", mc.Fc2D);
                                element.SetAttribute("fc3D", mc.Fc3D);
                                XmlNode mcNode = node.AppendChild(element);
                                foreach (SubClass sc in mc.SubClasses)
                                {
                                    XmlElement ele1 = document.CreateElement("SubClass");
                                    ele1.SetAttribute("name", sc.Name);
                                    ele1.SetAttribute("groupid", sc.GroupId.ToString());
                                    mcNode.AppendChild(ele1);
                                }
                            }
                        }
                    }
                }
                document.Save(fileName);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("保存数据失败！", "提示");
                return false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            currentLayer = this.logicBaseTree1.GetActiveLayer();
            logicBaseTree1_OnHitTest(System.Windows.Forms.MouseButtons.None, 0, 0, currentLayer);
        }

        private int type = 0;
        private IBaseLayer currentLayer;
        public void CreateLogicGroup()
        {
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.teName.Text = "";
            this.teName.Focus();
            this.teAlias.Text = "";
            type = 1;
        }

        public void CreateMajorClass()
        {
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.teName.Text = "";
            this.teName.Focus();
            this.teAlias.Text = "";
            this.teClassifyField.Text = "";
            type = 2;
        }

        private void logicBaseTree1_OnHitTest(MouseButtons button, int x, int y, IBaseLayer layer)
        {
            currentLayer = layer;
            if (layer is TreeNodeLogicGroup && layer.CustomValue is LogicGroup)
            {
                LogicGroup lg = layer.CustomValue as LogicGroup;
                this.teName.Text = lg.Name;
                this.teAlias.Text = lg.Alias;
                this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (layer is TreeNodeMajorClass && layer.CustomValue is MajorClass)
            {
                MajorClass mc = layer.CustomValue as MajorClass;
                this.teName.Text = mc.Name;
                this.teAlias.Text = mc.Alias;
                this.teClassifyField.Text = mc.ClassifyField;
                this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = this.teName.Text.Trim();
            string alias = this.teAlias.Text.Trim();
            string classifyfield = this.teClassifyField.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                XtraMessageBox.Show("名称不能为空！", "提示");
                return;
            }
            if (type == 1)
            {
                if (dictGroups.ContainsKey(name))
                {
                    XtraMessageBox.Show("已存在该组！", "提示");
                    return;
                }
                LogicGroup lg = null;
                if (currentLayer != null && currentLayer is TreeNodeLogicGroup)
                    lg = new LogicGroup(name, alias, currentLayer.CustomValue as LogicGroup);
                else lg = new LogicGroup(name, alias, null);
                TreeNodeLogicGroup treenode = new TreeNodeLogicGroup()
                {
                    Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias,
                    CustomValue = lg
                };
                if (currentLayer != null && currentLayer is TreeNodeLogicGroup) (currentLayer as GroupLayerClass).Add2(treenode);
                else treenode.OwnNode = this.logicBaseTree1.TreeList.AppendNode(new object[] { treenode.Name }, (TreeListNode)null);
                dictGroups[name] = lg;
            }
            if (type == 2)
            {
                if (dictMCs.ContainsKey(name))
                {
                    XtraMessageBox.Show("已存在该大类！", "提示");
                    return;
                }
                if (string.IsNullOrEmpty(classifyfield))
                {
                    XtraMessageBox.Show("分组字段不能为空！", "提示");
                    return;
                }

                MajorClass mc = null;
                if (currentLayer != null && currentLayer is TreeNodeLogicGroup)
                    mc = new MajorClass(name, alias, classifyfield, "", "", currentLayer.CustomValue as LogicGroup);
                else mc = new MajorClass(name, alias, classifyfield, "", "", null);
                TreeNodeMajorClass treenode = new TreeNodeMajorClass()
                {
                    Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias,
                    CustomValue = mc
                };
                if (currentLayer != null && currentLayer is TreeNodeLogicGroup) (currentLayer as GroupLayerClass).Add2(treenode);
                else treenode.OwnNode = this.logicBaseTree1.TreeList.AppendNode(new object[] { treenode.Name }, (TreeListNode)null);
                dictMCs[name] = mc;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentLayer != null)
            {
                string name = this.teName.Text.Trim();
                string alias = this.teAlias.Text.Trim();
                string classifyfield = this.teClassifyField.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    XtraMessageBox.Show("名称不能为空！", "提示");
                    return;
                }
                if (currentLayer is TreeNodeLogicGroup && currentLayer.CustomValue is LogicGroup)
                {
                    LogicGroup lg = currentLayer.CustomValue as LogicGroup;
                    if (lg.Name != name)
                    {
                        if (dictGroups.ContainsKey(name))
                        {
                            XtraMessageBox.Show("已存在该组！", "提示");
                            return;
                        }
                        dictGroups.Remove(lg.Name);
                    }
                    lg.Name = name;
                    lg.Alias = alias;
                    TreeNodeLogicGroup treenode = currentLayer as TreeNodeLogicGroup;
                    treenode.Name = string.IsNullOrEmpty(lg.Alias) ? lg.Name : lg.Alias;
                    treenode.CustomValue = lg;
                    dictGroups[name] = lg;
                }
                else if (currentLayer is TreeNodeMajorClass && currentLayer.CustomValue is MajorClass)
                {
                    if (string.IsNullOrEmpty(classifyfield))
                    {
                        XtraMessageBox.Show("分组字段不能为空！", "提示");
                        return;
                    }
                    MajorClass mc = currentLayer.CustomValue as MajorClass;
                    if (mc.Name != name)
                    {
                        if (dictMCs.ContainsKey(name))
                        {
                            XtraMessageBox.Show("已存在该大类！", "提示");
                            return;
                        }
                        dictMCs.Remove(mc.Name);
                    }
                    mc.Name = name;
                    mc.Alias = alias;
                    mc.ClassifyField = classifyfield;
                    TreeNodeMajorClass treenode = currentLayer as TreeNodeMajorClass;
                    treenode.Name = string.IsNullOrEmpty(mc.Alias) ? mc.Name : mc.Alias;
                    treenode.CustomValue = mc;
                    dictMCs[name] = mc;
                }
            }
        }

        private void FormLogicDataManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData(this._xmlFileName);
        }

    }
}
