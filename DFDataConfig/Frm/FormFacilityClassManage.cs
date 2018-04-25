using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using DFDataConfig.Class;
using DFDataConfig.Tree;
using DevExpress.XtraTreeList.Nodes;
using DFWinForms.LogicTree;
using DevExpress.XtraEditors;
using DFCommon.Class;
using System.Runtime.InteropServices;

namespace DFDataConfig.Frm
{
    public class FormFacilityClassManage : XtraForm
    {
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbi_Open;
        private DevExpress.XtraBars.BarButtonItem bbi_Save;
        private DevExpress.XtraBars.BarButtonItem bbi_SaveTo;
        private LogicBaseTree logicBaseTree1;
        private UC.UCFieldManage ucFieldManage1;
        private IContainer components;
        private DevExpress.XtraBars.BarButtonItem bbi_2DDataConfig;
        private DevExpress.XtraBars.BarButtonItem bbi_3DDataConfig;
        private DevExpress.XtraBars.BarButtonItem bbi_LogicDataConfig2D;
        private DevExpress.XtraBars.BarButtonItem bbi_LogicDataConfig3D;
        private DevExpress.Utils.ImageCollection imageCollection1;


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFacilityClassManage));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.logicBaseTree1 = new DFWinForms.LogicTree.LogicBaseTree();
            this.ucFieldManage1 = new DFDataConfig.UC.UCFieldManage();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbi_Open = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_Save = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_SaveTo = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_LogicDataConfig2D = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_2DDataConfig = new DevExpress.XtraBars.BarButtonItem();
            this.bbi_3DDataConfig = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.bbi_LogicDataConfig3D = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.logicBaseTree1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucFieldManage1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(755, 406);
            this.splitContainerControl1.SplitterPosition = 219;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // logicBaseTree1
            // 
            this.logicBaseTree1.AllowDragLayer = false;
            this.logicBaseTree1.AllowMultiSelect = false;
            this.logicBaseTree1.AutoHide = false;
            this.logicBaseTree1.Caption = "";
            this.logicBaseTree1.CustomColumnCaption = "${res:Interface_类型}";
            this.logicBaseTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logicBaseTree1.ID = null;
            this.logicBaseTree1.IsActive = false;
            this.logicBaseTree1.Location = new System.Drawing.Point(0, 0);
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
            this.logicBaseTree1.Size = new System.Drawing.Size(219, 406);
            this.logicBaseTree1.TabIndex = 0;
            this.logicBaseTree1.Title = null;
            this.logicBaseTree1.OnHitTest += new DFWinForms.LogicTree.OnHitTestHandler<DFWinForms.LogicTree.IBaseLayer>(this.logicBaseTree1_OnHitTest);
            this.logicBaseTree1.OnLayerDoubleClick += new DFWinForms.LogicTree.OnLayerDoubleClickHandler<DFWinForms.LogicTree.IBaseLayer>(this.logicBaseTree1_OnLayerDoubleClick);
            // 
            // ucFieldManage1
            // 
            this.ucFieldManage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFieldManage1.Location = new System.Drawing.Point(0, 0);
            this.ucFieldManage1.Name = "ucFieldManage1";
            this.ucFieldManage1.NeedSave = false;
            this.ucFieldManage1.Size = new System.Drawing.Size(531, 406);
            this.ucFieldManage1.TabIndex = 5;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbi_Open,
            this.bbi_Save,
            this.bbi_SaveTo,
            this.bbi_2DDataConfig,
            this.bbi_3DDataConfig,
            this.bbi_LogicDataConfig2D,
            this.bbi_LogicDataConfig3D});
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 9;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.bbi_Open, false),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_Save),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbi_SaveTo, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_LogicDataConfig2D, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_LogicDataConfig3D),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_2DDataConfig, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbi_3DDataConfig, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // bbi_Open
            // 
            this.bbi_Open.Caption = "打开";
            this.bbi_Open.Id = 0;
            this.bbi_Open.ImageIndex = 0;
            this.bbi_Open.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            this.bbi_Open.Name = "bbi_Open";
            this.bbi_Open.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_Open_ItemClick);
            // 
            // bbi_Save
            // 
            this.bbi_Save.Caption = "保存";
            this.bbi_Save.Id = 1;
            this.bbi_Save.ImageIndex = 1;
            this.bbi_Save.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.bbi_Save.Name = "bbi_Save";
            this.bbi_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_Save_ItemClick);
            // 
            // bbi_SaveTo
            // 
            this.bbi_SaveTo.Caption = "另存为";
            this.bbi_SaveTo.Id = 2;
            this.bbi_SaveTo.ImageIndex = 2;
            this.bbi_SaveTo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.S));
            this.bbi_SaveTo.Name = "bbi_SaveTo";
            this.bbi_SaveTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_SaveTo_ItemClick);
            // 
            // bbi_LogicDataConfig2D
            // 
            this.bbi_LogicDataConfig2D.Caption = "2D逻辑分类数据管理";
            this.bbi_LogicDataConfig2D.Id = 7;
            this.bbi_LogicDataConfig2D.ImageIndex = 6;
            this.bbi_LogicDataConfig2D.Name = "bbi_LogicDataConfig2D";
            this.bbi_LogicDataConfig2D.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_LogicDataConfig_ItemClick);
            // 
            // bbi_2DDataConfig
            // 
            this.bbi_2DDataConfig.Caption = "二维数据关联";
            this.bbi_2DDataConfig.Id = 5;
            this.bbi_2DDataConfig.ImageIndex = 4;
            this.bbi_2DDataConfig.Name = "bbi_2DDataConfig";
            this.bbi_2DDataConfig.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_2DDataConfig_ItemClick);
            // 
            // bbi_3DDataConfig
            // 
            this.bbi_3DDataConfig.Caption = "三维数据关联";
            this.bbi_3DDataConfig.Id = 6;
            this.bbi_3DDataConfig.ImageIndex = 5;
            this.bbi_3DDataConfig.Name = "bbi_3DDataConfig";
            this.bbi_3DDataConfig.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_3DDataConfig_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(755, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 430);
            this.barDockControlBottom.Size = new System.Drawing.Size(755, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 406);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(755, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 406);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("open_16x16.png", "images/actions/open_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/open_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "open_16x16.png");
            this.imageCollection1.InsertGalleryImage("saveall_16x16.png", "images/save/saveall_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/save/saveall_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "saveall_16x16.png");
            this.imageCollection1.InsertGalleryImage("saveas_16x16.png", "images/save/saveas_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/save/saveas_16x16.png"), 2);
            this.imageCollection1.Images.SetKeyName(2, "saveas_16x16.png");
            this.imageCollection1.InsertGalleryImage("additem_16x16.png", "images/actions/additem_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/additem_16x16.png"), 3);
            this.imageCollection1.Images.SetKeyName(3, "additem_16x16.png");
            this.imageCollection1.Images.SetKeyName(4, "m_2DMode.png");
            this.imageCollection1.Images.SetKeyName(5, "m_3DMode.png");
            this.imageCollection1.InsertGalleryImage("group_16x16.png", "images/actions/group_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/group_16x16.png"), 6);
            this.imageCollection1.Images.SetKeyName(6, "group_16x16.png");
            // 
            // bbi_LogicDataConfig3D
            // 
            this.bbi_LogicDataConfig3D.Caption = "3D逻辑分类数据管理";
            this.bbi_LogicDataConfig3D.Glyph = ((System.Drawing.Image)(resources.GetObject("bbi_LogicDataConfig3D.Glyph")));
            this.bbi_LogicDataConfig3D.Id = 8;
            this.bbi_LogicDataConfig3D.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbi_LogicDataConfig3D.LargeGlyph")));
            this.bbi_LogicDataConfig3D.Name = "bbi_LogicDataConfig3D";
            this.bbi_LogicDataConfig3D.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbi_LogicDataConfig3D_ItemClick);
            // 
            // FormFacilityClassManage
            // 
            this.ClientSize = new System.Drawing.Size(755, 430);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFacilityClassManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设施类管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFacilityClassManage_FormClosed);
            this.Load += new System.EventHandler(this.FormFacilityClassManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        private string _xmlFileName;
        public FormFacilityClassManage()
        {
            InitializeComponent();
            this._xmlFileName = Config.GetConfigValue("FacilityClassXmlPath");
            LoadData(this._xmlFileName);
            this.logicBaseTree1.OnInitRootPopMenu += new OnInitRootPopMenuHandler(logicBaseTree1_OnInitRootPopMenu);
            this.logicBaseTree1.OnRootPopMenuItemClick += new OnRootPopMenuItemClickHandler(logicBaseTree1_OnRootPopMenuItemClick);
        }

        private void logicBaseTree1_OnRootPopMenuItemClick(string caption)
        {
            switch (caption)
            {
                case "创建设施类":
                    CreateNewFC();
                    break;
            }
        }

        private void logicBaseTree1_OnInitRootPopMenu()
        {
            this.logicBaseTree1.AddMenuItems(new string[] { "创建设施类" });
        }

        public void LoadData(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            Dictionary<string, FacilityClass> dict = new Dictionary<string, FacilityClass>();
            try
            {
                if (!File.Exists(fileName))
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
                        XmlNode node = document.CreateElement("FacilityClassManager");
                        document.AppendChild(node);
                        document.Save(fileName);
                    }
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                if (xmlDoc == null) return;
                XmlNode root = xmlDoc.SelectSingleNode("FacilityClassManager");
                if (root == null) return;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name != "FacilityClass") continue;
                    if (node.Attributes["name"] == null) continue;
                    string name = "", alias = "";
                    if (node.Attributes["name"] != null) { name = node.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(name)) continue; }
                    if (node.Attributes["alias"] != null) alias = node.Attributes["alias"].Value.Trim();
                    FacilityClass fc = new FacilityClass(name, alias);
                    if (node.Attributes["fc2D"] != null) fc.Fc2D = node.Attributes["fc2D"].Value.Trim();
                    if (node.Attributes["fc3D"] != null) fc.Fc3D = node.Attributes["fc3D"].Value.Trim();
                    if (dict.ContainsKey(fc.Name)) continue;
                    dict[fc.Name] = fc;
                    foreach (XmlNode cnode in node.ChildNodes)
                    {
                        if (cnode.Name != "StdField") continue;
                        if (cnode.Attributes["name"] == null) continue;
                        string fieldName = "", fieldAliasName = "", dataType = "", fieldSystemName = "", fieldSystemAliasName = "", fieldCanQuery = "", fieldNeedCheck = "", fieldCanStats = "";
                        if (cnode.Attributes["name"] != null) { fieldName = cnode.Attributes["name"].Value.Trim(); if (string.IsNullOrEmpty(fieldName)) continue; }
                        if (cnode.Attributes["alias"] != null) fieldAliasName = cnode.Attributes["alias"].Value.Trim();
                        if (cnode.Attributes["datatype"] != null) dataType = cnode.Attributes["datatype"].Value.Trim();
                        if (cnode.Attributes["systemname"] != null) fieldSystemName = cnode.Attributes["systemname"].Value.Trim();
                        if (cnode.Attributes["systemalias"] != null) fieldSystemAliasName = cnode.Attributes["systemalias"].Value.Trim();
                        if (cnode.Attributes["canquery"] != null) fieldCanQuery = cnode.Attributes["canquery"].Value.Trim();
                        if (cnode.Attributes["needcheck"] != null) fieldNeedCheck = cnode.Attributes["needcheck"].Value.Trim();
                        if (cnode.Attributes["canstats"] != null) fieldCanStats = cnode.Attributes["canstats"].Value.Trim();
                        bool bCanQuery = false;
                        if (!string.IsNullOrEmpty(fieldCanQuery) && fieldCanQuery == "true") bCanQuery = true;
                        bool bNeedCheck = false;
                        if (!string.IsNullOrEmpty(fieldNeedCheck) && fieldNeedCheck == "true") bNeedCheck = true;
                        bool bCanStats = false;
                        if (!string.IsNullOrEmpty(fieldCanStats) && fieldCanStats == "true") bCanStats = true;
                        FieldInfo fi = new FieldInfo(fieldName, fieldAliasName, fieldSystemName, fieldSystemAliasName, bCanQuery, bNeedCheck, bCanStats, dataType);
                        fc.AddFieldInfo(fi);
                    }
                    TreeNodeFacilityClass treenode = new TreeNodeFacilityClass()
                    {
                        Name = string.IsNullOrEmpty(fc.Alias) ? fc.Name : fc.Alias,
                        CustomValue = fc
                    };
                    treenode.OwnNode = this.logicBaseTree1.TreeList.AppendNode(new object[] { treenode.Name }, (TreeListNode)null);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void FormFacilityClassManage_Load(object sender, EventArgs e)
        {
            string systemType = SystemInfo.Instance.SystemType;
            if (systemType == "2D")
            {
                this.bbi_LogicDataConfig3D.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbi_3DDataConfig.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (systemType == "3D")
            {
                this.bbi_LogicDataConfig2D.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bbi_2DDataConfig.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (this.logicBaseTree1.TreeList.FocusedNode != null)
            {
                this.ucFieldManage1.SetData(this.logicBaseTree1.GetActiveLayer().CustomValue as FacilityClass);
            }
        }

        private void FormFacilityClassManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ucFieldManage1.NeedSave)
            {
                if (XtraMessageBox.Show("是否要保存修改的字段信息？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    this.ucFieldManage1.SaveFieldInfo();
                    SaveData(this._xmlFileName);
                }

            }
        }

        private void logicBaseTree1_OnHitTest(MouseButtons button, int x, int y, IBaseLayer layer)
        {
            if (layer is TreeNodeFacilityClass)
            {
                TreeNodeFacilityClass tn = layer as TreeNodeFacilityClass;
                if (layer.CustomValue is FacilityClass) this.ucFieldManage1.SetData(layer.CustomValue as FacilityClass);
            }
        }

        private void logicBaseTree1_OnLayerDoubleClick(IBaseLayer layer)
        {

        }

        private void bbi_Open_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Xml File(*.xml)|*.xml",
                RestoreDirectory = true
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dlg.FileName;

                this.ucFieldManage1.Init();
                this.logicBaseTree1.ClearLayers();
                LoadData(fileName);
                if (this.logicBaseTree1.TreeList.FocusedNode != null)
                {
                    this.ucFieldManage1.SetData(this.logicBaseTree1.GetActiveLayer().CustomValue as FacilityClass);
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
                XmlNode node = document.CreateElement("FacilityClassManager");
                document.AppendChild(node);
                List<IBaseLayer> logicBaseTree1GetRootLayers = this.logicBaseTree1.GetRootLayers();
                if (logicBaseTree1GetRootLayers != null)
                {
                    foreach (IBaseLayer layer in logicBaseTree1GetRootLayers)
                    {
                        if (!(layer is TreeNodeFacilityClass)) continue;
                        TreeNodeFacilityClass tn = layer as TreeNodeFacilityClass;
                        FacilityClass fc = tn.CustomValue as FacilityClass;
                        if (fc == null) continue;
                        XmlElement element = document.CreateElement("FacilityClass");
                        element.SetAttribute("name", fc.Name.Trim());
                        element.SetAttribute("alias", layer.Name.Trim());
                        element.SetAttribute("fc2D", (fc.Fc2D == null) ? "" : fc.Fc2D.Trim());
                        element.SetAttribute("fc3D", (fc.Fc3D == null) ? "" : fc.Fc3D.Trim());
                        XmlNode fcNode = node.AppendChild(element);
                        if (fcNode == null) continue;
                        foreach (FieldInfo fi in fc.FieldInfoCollection)
                        {
                            XmlElement ele = document.CreateElement("StdField");
                            ele.SetAttribute("name", fi.Name.Trim());
                            ele.SetAttribute("alias", fi.Alias.Trim());
                            ele.SetAttribute("datatype", fi.DataType.Trim());
                            ele.SetAttribute("systemname", fi.SystemName.Trim());
                            ele.SetAttribute("systemalias", fi.SystemAlias.Trim());
                            ele.SetAttribute("canquery", fi.CanQuery ? "true" : "false");
                            ele.SetAttribute("needcheck", fi.NeedCheck ? "true" : "false");
                            ele.SetAttribute("canstats", fi.CanStats ? "true" : "false");
                            fcNode.AppendChild(ele);
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

        private void bbi_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ucFieldManage1.NeedSave)
            {
                if (XtraMessageBox.Show("是否要保存修改的字段信息？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    this.ucFieldManage1.SaveFieldInfo();
                else this.ucFieldManage1.NeedSave = false;
            }

            if (SaveData(this._xmlFileName)) XtraMessageBox.Show("保存数据成功！", "提示");
        }

        private void bbi_SaveTo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ucFieldManage1.NeedSave)
            {
                if (XtraMessageBox.Show("是否要保存修改的字段信息？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    this.ucFieldManage1.SaveFieldInfo();
                else this.ucFieldManage1.NeedSave = false;
            }

            SaveFileDialog dlg = new SaveFileDialog()
            {
                Filter = "Xml File(*.xml)|*.xml",
                DefaultExt = "xml"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (SaveData(dlg.FileName)) XtraMessageBox.Show("保存数据成功！", "提示");
            }
        }

        private void CreateNewFC()
        {
            Dictionary<string, FacilityClass> dict = new Dictionary<string, FacilityClass>();
            List<IBaseLayer> logicBaseTree1GetRootLayers = this.logicBaseTree1.GetRootLayers();
            if (logicBaseTree1GetRootLayers != null)
            {
                foreach (IBaseLayer layer in logicBaseTree1GetRootLayers)
                {
                    if (!(layer is TreeNodeFacilityClass)) continue;
                    TreeNodeFacilityClass tn = layer as TreeNodeFacilityClass;
                    FacilityClass fc = tn.CustomValue as FacilityClass;
                    dict[fc.Name] = fc;
                }
            }
            FormCreateFacilityClass dlg = new FormCreateFacilityClass(dict);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FacilityClass fc = new FacilityClass(dlg.FCName, dlg.FCAlias);
                TreeNodeFacilityClass treenode = new TreeNodeFacilityClass()
                {
                    Name = string.IsNullOrEmpty(fc.Alias) ? fc.Name : fc.Alias,
                    CustomValue = fc
                };
                treenode.OwnNode = this.logicBaseTree1.TreeList.AppendNode(new object[] { treenode.Name }, (TreeListNode)null);
            }
        }

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        private void bbi_2DDataConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DF2DDataConfig.exe"))
                WinExec(AppDomain.CurrentDomain.BaseDirectory + "DF2DDataConfig.exe", 1);
        }

        private void bbi_3DDataConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DF3DDataConfig.exe"))
                WinExec(AppDomain.CurrentDomain.BaseDirectory + "DF3DDataConfig.exe", 1);
        }

        private void bbi_LogicDataConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormLogicData2DManage dlg = new FormLogicData2DManage();
            dlg.ShowDialog();
        }

        private void bbi_LogicDataConfig3D_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormLogicData3DManage dlg = new FormLogicData3DManage();
            dlg.ShowDialog();
        }



    }
}
