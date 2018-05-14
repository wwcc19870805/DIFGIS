using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DFCommon.Class;
using System.IO;
using System.Xml;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Controls;
using ICSharpCode.Core;
using DFUser.Class;

namespace DFUser.Frm
{
    public class FrmRoleManage : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private LabelControl lcInfo;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        string xmlPath2D ;
        string xmlPath3D;
        private ComboBoxEdit cmb_Role;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        string xmlPathGeneral;
        AuthService authService;
        private TextEdit te_RoleName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        Dictionary<string, string> dicRoleID;
        Dictionary<string, List<string>> dicRoleXml;
        XmlDocument menu2D;
        XmlDocument menu3D;
        XmlDocument general;
        string msg = string.Empty;
    
        public FrmRoleManage()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.te_RoleName = new DevExpress.XtraEditors.TextEdit();
            this.cmb_Role = new DevExpress.XtraEditors.ComboBoxEdit();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.NodeObject = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.lcInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_RoleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_Role.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.te_RoleName);
            this.layoutControl1.Controls.Add(this.cmb_Role);
            this.layoutControl1.Controls.Add(this.treeList1);
            this.layoutControl1.Controls.Add(this.lcInfo);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(296, 328);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // te_RoleName
            // 
            this.te_RoleName.Location = new System.Drawing.Point(65, 44);
            this.te_RoleName.Name = "te_RoleName";
            this.te_RoleName.Size = new System.Drawing.Size(229, 22);
            this.te_RoleName.StyleController = this.layoutControl1;
            this.te_RoleName.TabIndex = 11;
            // 
            // cmb_Role
            // 
            this.cmb_Role.Location = new System.Drawing.Point(65, 70);
            this.cmb_Role.Name = "cmb_Role";
            this.cmb_Role.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_Role.Size = new System.Drawing.Size(229, 22);
            this.cmb_Role.StyleController = this.layoutControl1;
            this.cmb_Role.TabIndex = 10;
            this.cmb_Role.SelectedIndexChanged += new System.EventHandler(this.cmb_Role_SelectedIndexChanged);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName,
            this.NodeObject});
            this.treeList1.Location = new System.Drawing.Point(2, 96);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeList1.OptionsSelection.InvertSelection = true;
            this.treeList1.OptionsSelection.MultiSelect = true;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.Size = new System.Drawing.Size(292, 204);
            this.treeList1.TabIndex = 9;
            // 
            // NodeName
            // 
            this.NodeName.Caption = "名称";
            this.NodeName.FieldName = "NodeName";
            this.NodeName.MinWidth = 32;
            this.NodeName.Name = "NodeName";
            this.NodeName.OptionsColumn.AllowEdit = false;
            this.NodeName.Visible = true;
            this.NodeName.VisibleIndex = 0;
            // 
            // NodeObject
            // 
            this.NodeObject.Caption = "对象";
            this.NodeObject.FieldName = "NodeObject";
            this.NodeObject.Name = "NodeObject";
            // 
            // lcInfo
            // 
            this.lcInfo.Location = new System.Drawing.Point(2, 304);
            this.lcInfo.Name = "lcInfo";
            this.lcInfo.Size = new System.Drawing.Size(138, 14);
            this.lcInfo.StyleController = this.layoutControl1;
            this.lcInfo.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(226, 304);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "退  出";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(154, 304);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(2, 2);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "添加角色"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "修改角色"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "删除角色")});
            this.radioGroup1.Size = new System.Drawing.Size(292, 38);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 5;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(296, 328);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(296, 42);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(152, 302);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(224, 302);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(142, 302);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lcInfo;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 302);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(142, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.treeList1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 94);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(296, 208);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cmb_Role;
            this.layoutControlItem6.CustomizationFormText = "角色名称：";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(296, 26);
            this.layoutControlItem6.Text = "角色列表：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.te_RoleName;
            this.layoutControlItem7.CustomizationFormText = "角色名称：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(296, 26);
            this.layoutControlItem7.Text = "角色名称：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // FrmRoleManage
            // 
            this.ClientSize = new System.Drawing.Size(296, 328);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmRoleManage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "角色管理";
            this.Load += new System.EventHandler(this.FrmRoleManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_RoleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_Role.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string roleID = "";
                string addin2D;
                string addin3D;
                string addinGen;
                if (radioGroup1.SelectedIndex == 0)
                {
                    BuildAddIn();
                    if (SystemInfo.Instance.SystemType == "2D")
                    {
                        addin2D = menu2D.OuterXml;
                        addin3D = "";
                        addinGen = general.OuterXml;
                    }
                    else if (SystemInfo.Instance.SystemType == "3D")
                    {
                        addin2D = "";
                        addin3D = menu3D.OuterXml;
                        addinGen = general.OuterXml;
                    }
                    else
                    {
                        addin2D = menu2D.OuterXml;
                        addin3D = menu3D.OuterXml;
                        addinGen = general.OuterXml;
                    }
                    
                    if (AddRole(te_RoleName.Text, null, "2D3D", "CS", addin2D, addin3D, addinGen))
                    {
                        LoadAllRoles();
                        this.lcInfo.Text = msg;
                        //XtraMessageBox.Show("添加角色成功", "提示");
                    }

                }
                else if (radioGroup1.SelectedIndex == 1)
                {
                    BuildAddIn();
                    if (SystemInfo.Instance.SystemType == "2D")
                    {
                        addin2D = menu2D.OuterXml;
                        addin3D = "";
                        addinGen = general.OuterXml;
                    }
                    else if (SystemInfo.Instance.SystemType == "3D")
                    {
                        addin2D = "";
                        addin3D = menu3D.OuterXml;
                        addinGen = general.OuterXml;
                    }
                    else
                    {
                        addin2D = menu2D.OuterXml;
                        addin3D = menu3D.OuterXml;
                        addinGen = general.OuterXml;
                    }
                    if (dicRoleID.ContainsKey(cmb_Role.Text)) roleID = dicRoleID[cmb_Role.Text];
                    if (EditRole(roleID, null, "2D3D", "CS", addin2D, addin3D, addinGen))
                    {
                        this.lcInfo.Text = msg;
                    }
                    //XtraMessageBox.Show("修改角色成功", "提示");

                }
                else
                {
                    if (authService == null) return;
                    if (dicRoleID == null || dicRoleID.Count == 0) return;
                    if (dicRoleID.ContainsKey(cmb_Role.Text))
                        roleID = dicRoleID[cmb_Role.Text];
                    if (authService.deleteRole(roleID, "2D3D", "CS", out msg))
                    {
                        LoadAllRoles();
                        this.lcInfo.Text = msg;
                        //XtraMessageBox.Show("删除角色成功", "提示");
                    }
                    else
                        this.lcInfo.Text = msg;
                    //XtraMessageBox.Show("删除角色失败", "提示");

                }
            }
            catch (System.Exception ex)
            {
            	
            }
          
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 根据树菜单勾选创建AddIn文件，保存在本地路径
        /// </summary>
        private void BuildAddIn()
        {
            string localDataPath = SystemInfo.Instance.LocalDataPath;
            string addInPath = Path.Combine(localDataPath, "AddIns");
            if (!Directory.Exists(addInPath))
            {
                Directory.CreateDirectory(addInPath);
            }
            string xmlPath;
            //if (xmlPath2D == null || xmlPath3D == null || xmlPathGeneral == null) return;
            try
            {
                TreeListNodes nodes = this.treeList1.Nodes;
                foreach (TreeListNode node in nodes)
                {
                    if (node.ParentNode == null)
                    {
                        if (node.GetValue("NodeName") == "二维菜单")
                        {
                            menu2D = new XmlDocument();
                            xmlPath = addInPath + "\\Menu2D.addin";
                            if (File.Exists(xmlPath))
                            {
                                File.Delete(xmlPath);
                            }
                            menu2D.Load(xmlPath2D);
                            WriteXml(menu2D,node);
                            menu2D.Save(xmlPath); 
                        }
                        else if (node.GetValue("NodeName") == "三维菜单")
                        {
                            menu3D = new XmlDocument();
                            xmlPath = addInPath + "\\Menu3D.addin";
                            if (File.Exists(xmlPath))
                            {
                                File.Delete(xmlPath);
                            }
                            menu3D.Load(xmlPath3D);
                            WriteXml(menu3D, node);
                            menu3D.Save(xmlPath);    
                        }
                        else
                        {
                            general = new XmlDocument();
                            xmlPath = addInPath + "\\General.addin";
                            if (File.Exists(xmlPath))
                            {
                                File.Delete(xmlPath);
                            }
                            general.Load(xmlPathGeneral);
                            WriteXml(general, node);
                            general.Save(xmlPath); 
                        }
                    }
                }            
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /// <summary>
        /// 根据树列表，更新已有的xml文档
        /// </summary>
        /// <param name="doc">xml文档</param>
        /// <param name="node">树列表节点</param>
        private void WriteXml(XmlDocument doc,TreeListNode node)
        {
            XmlNode path = doc.SelectSingleNode("AddIn/Path");//选择AddIn文件的Path节点
            if (path == null) return ;
            XmlNode addIn = doc.SelectSingleNode("AddIn");//选择AddIn文件的AddIn节点
            if (addIn == null) return ;
            addIn.RemoveChild(path);//将AddIn节点内的Path节点部分移除
            CreateXmlNode(node, addIn, doc);//更新AddIn文件

        }
        private bool EditRole(string roleid, string funcodes, string systemtype, string systemclient, string addin2D, string addin3D, string addinGen)
        {
            return authService.editRole(roleid, funcodes, systemtype, systemclient, addin2D, addin3D, addinGen,out msg);
        }

        private bool  AddRole(string rolename,string funcodes,string systemtype,string systemclient,string addin2D,string addin3D,string addinGen)
        {

            return authService.addRole(rolename, funcodes, systemtype, systemclient, addin2D, addin3D, addinGen,out msg);
        }
        
    
        /// <summary>
        /// 根据树节点，更新AddIn文件
        /// </summary>
        /// <param name="treeNode">树节点</param>
        /// <param name="parent">AddIn根节点</param>
        /// <param name="doc">AddIn文件</param>
        private void CreateXmlNode(TreeListNode treeNode, XmlNode parent,XmlDocument doc)
        {
            if (treeNode.CheckState == CheckState.Unchecked) return;//如果该树节点未选择，返回
            object obj = treeNode.GetValue("NodeObject");//得到该树节点对象
            if (obj != null && obj is XmlNode)//判断该对象是否为xmlNode
            {
                XmlNode node = obj as XmlNode;
                //XmlNode newNode = node.CloneNode(false);
                XmlElement element = doc.CreateElement(node.Name);//为该AddIn文件创建新的节点，内容为该树节点对应的xmlNode对象
                foreach (XmlAttribute attr in node.Attributes)
                {
                    element.SetAttribute(attr.Name, attr.Value);//将树节点对象的属性添加到新创建的节点中
                }
                parent.AppendChild(element);//将新创建好的节点添加为AddIn根节点的子节点
                if (treeNode.Nodes == null) return;
                foreach (TreeListNode child in treeNode.Nodes)//根据树节点的子节点，以及新创建好的xml文档子节点，进行递归
                {
                    CreateXmlNode(child,element,doc);
                }
                
            }
        }

        private void FrmRoleManage_Load(object sender, EventArgs e)
        {
            this.cmb_Role.Enabled = false;
            this.cmb_Role.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            try
            {
                if (SystemInfo.Instance.SystemType == "2D")
                {
                    xmlPath2D = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\Menu2D.addin");
                    xmlPathGeneral = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\General.addin");
                    if (xmlPath2D == null || xmlPathGeneral == null) return;
                    dicRoleID = new Dictionary<string, string>();//创建角色ID字典
                    dicRoleXml = new Dictionary<string, List<string>>();//创建角色AddIn文档字典
                    LoadAllRoles();//加载所有角色
                    BuildTree(xmlPath2D, "二维菜单");//创建二维菜单树
                    BuildTree(xmlPathGeneral, "系统管理");//创建系统菜单树
                }
                else if (SystemInfo.Instance.SystemType == "3D")
                {
                    xmlPath3D = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\Menu3D.addin");
                    xmlPathGeneral = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\General.addin");
                    if ( xmlPath3D == null || xmlPathGeneral == null) return;
                    dicRoleID = new Dictionary<string, string>();//创建角色ID字典
                    dicRoleXml = new Dictionary<string, List<string>>();//创建角色AddIn文档字典
                    LoadAllRoles();//加载所有角色
                    BuildTree(xmlPath3D, "三维菜单");//创建三维菜单树
                    BuildTree(xmlPathGeneral, "系统管理");//创建系统菜单树
                }
                else
                {
                    xmlPath2D = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\Menu2D.addin");
                    xmlPath3D = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\Menu3D.addin");
                    xmlPathGeneral = Path.Combine(FileUtility.ApplicationRootPath, "..\\AddIns\\General.addin");
                    if (xmlPath2D == null || xmlPath3D == null || xmlPathGeneral == null) return;
                    dicRoleID = new Dictionary<string, string>();//创建角色ID字典
                    dicRoleXml = new Dictionary<string, List<string>>();//创建角色AddIn文档字典
                    LoadAllRoles();//加载所有角色
                    BuildTree(xmlPath2D, "二维菜单");//创建二维菜单树
                    BuildTree(xmlPath3D, "三维菜单");//创建三维菜单树
                    BuildTree(xmlPathGeneral, "系统管理");//创建系统菜单树
                }
               
            }
            catch (System.Exception ex)
            {
            	
            }
           

        }
        /// <summary>
        /// 加载所有角色
        /// </summary>
        private void LoadAllRoles()
        {
            if (this.authService == null) return;
            if (dicRoleID == null) return;
            dicRoleID.Clear();
            dicRoleXml.Clear();
            cmb_Role.Properties.Items.Clear();
            DataTable dtRole = authService.queryAllRoles("2D3D", "CS");//查询所有CS用户角色
            if (dtRole == null || dtRole.Rows.Count == 0) return;
            foreach (DataRow dr in dtRole.Rows)
            {
                this.cmb_Role.Properties.Items.Add(dr["RoleName"].ToString());
                dicRoleID[dr["RoleName"].ToString()] = dr["RoleID"].ToString();
                List<string> xmls = new List<string>();
                xmls.AddRange(new string[] { dr["Menu2D"].ToString(), dr["Menu3D"].ToString(), dr["General"].ToString() });
                dicRoleXml[dr["RoleName"].ToString()] = xmls;             
            }
            this.cmb_Role.SelectedIndex = 0;
            this.cmb_Role.Refresh();
        }

        /// <summary>
        /// 构建软件功能树
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="rootName"></param>
        private void BuildTree(string xmlPath,string rootName)
        {
            try
            {
                if (!File.Exists(xmlPath)) return;
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                if (doc == null) return;
                XmlNode root = doc.SelectSingleNode("AddIn/Path");
                if (root == null) return;
                TreeListNode rootNode = this.treeList1.AppendNode(new object[] { rootName, root }, null);//创建根节点
                foreach (XmlNode page in root.ChildNodes)
                {
                    AddTreeListNode(rootNode, page);                
                }               
                
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        /// <summary>
        /// 根据AddIn文件中的菜单节点，构建功能菜单树
        /// </summary>
        /// <param name="treeNode"> 树父节点</param>
        /// <param name="xmlNode">AddIn文件菜单节点</param>
        private void AddTreeListNode(TreeListNode treeNode, XmlNode xmlNode)
        {
            if (xmlNode.Attributes == null) return;
            if (xmlNode.Name == "Condition" || xmlNode.Name == "TerrainCondition")//如果该节点是条件节点
            {
                XmlElement element = xmlNode as XmlElement;
                if (element.HasAttribute("name"))
                {
                    TreeListNode node = this.treeList1.AppendNode(new object[] { element.GetAttribute("name"), xmlNode }, treeNode);//为当前父节点添加子节点，子节点名为条件节点name属性
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        AddTreeListNode(node, child);//递归，为新添加的树节点，添加子节点
                    }
                }
                else
                {
                    TreeListNode node = this.treeList1.AppendNode(new object[] { xmlNode.Name, xmlNode }, treeNode);
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        AddTreeListNode(node, child);
                    }
                }
               
            }
            else
            {
                if (xmlNode.Attributes["type"].Value.Trim() == "RibbonPage")
                {
                    if (xmlNode.Attributes["label"] == null) return;
                    TreeListNode node = this.treeList1.AppendNode(new object[] { xmlNode.Attributes["label"].Value.Trim(), xmlNode }, treeNode);
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        AddTreeListNode(node, child);
                    }
                }
                if (xmlNode.Attributes["type"].Value.Trim() == "RibbonPageGroup")
                {
                    if (xmlNode.Attributes["label"] == null) return;
                    TreeListNode node = this.treeList1.AppendNode(new object[] { xmlNode.Attributes["label"].Value.Trim(), xmlNode }, treeNode);
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        AddTreeListNode(node, child);
                    }
                }
                if (xmlNode.Attributes["type"].Value.Trim() == "SubItemCommand")
                {
                    if (xmlNode.Attributes["label"] == null) return;
                    TreeListNode node = this.treeList1.AppendNode(new object[] { xmlNode.Attributes["label"].Value.Trim(), xmlNode }, treeNode);
                    foreach (XmlNode child in xmlNode.ChildNodes)
                    {
                        AddTreeListNode(node, child);
                    }
                }
                if (xmlNode.Attributes["type"].Value.Trim() == "CheckCommand" || xmlNode.Attributes["type"].Value.Trim() == "ButtonCommand" || xmlNode.Attributes["type"].Value.Trim() == "RadioCommand" || xmlNode.Attributes["type"].Value.Trim() == "ComboBoxCommand" || xmlNode.Attributes["type"].Value.Trim() == "CheckBoxCommand" || xmlNode.Attributes["type"].Value.Trim() == "MenuTrackBarCommand")
                {
                    if (xmlNode.Attributes["label"] == null) return;
                    TreeListNode node = this.treeList1.AppendNode(new object[] { xmlNode.Attributes["label"].Value.Trim(), xmlNode }, treeNode);
                }
            }
            
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(radioGroup1.SelectedIndex)
            {
                case 0:
                    this.cmb_Role.Enabled = false;
                    this.treeList1.Enabled = true;
                    this.te_RoleName.Enabled = true;
                    this.treeList1.UncheckAll();
                    break;
                case 1:
                    this.cmb_Role.Enabled = true;
                    this.treeList1.Enabled = true;
                    this.te_RoleName.Enabled = false;
                    this.cmb_Role_SelectedIndexChanged(null, null);
                    break;
                case 2:
                    this.cmb_Role.Enabled = true;
                    this.treeList1.Enabled = true;
                    this.te_RoleName.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 角色列表更新时，初始化菜单树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_Role_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!dicRoleXml.ContainsKey(cmb_Role.SelectedItem.ToString())) return;
                List<string> xmls = dicRoleXml[cmb_Role.SelectedItem.ToString()];//获得该角色名所对应的xml字符串
                if (xmls == null) return;
                treeList1.UncheckAll();              
                XmlDocument doc = new XmlDocument();
                for (int i = 0; i < xmls.Count; i++)
                {
                    doc.LoadXml(xmls[i]);//加载当前树节点对应的AddIn文件
                    XmlNode xmlNode = doc.SelectSingleNode("AddIn/Path");
                    if (xmlNode == null) return;
                    InitTreeList(xmlNode,this.treeList1.Nodes[i]);//初始化当前树节点下的菜单树
                }
                 
            }
            catch (System.Exception ex)
            {
            	
            }
            
                

        }

        /// <summary>
        /// 根据当前菜单树节点及对应的AddIn文件进行初始化
        /// </summary>
        /// <param name="xmlNode">当前AddIn文件的菜单节点</param>
        /// <param name="node">当前菜单树节点</param>  
        private void InitTreeList(XmlNode xmlNode,TreeListNode node)
        {
            if (!xmlNode.HasChildNodes) return ;
            foreach (XmlNode child in xmlNode.ChildNodes)//遍历菜单列表
            {
                XmlElement element = child as XmlElement;
                if (element.HasAttribute("label")||element.HasAttribute("name"))
                {
                    string label;
                    if(element.HasAttribute("name")) label = element.GetAttribute("name") ;//如果是条件节点，则取其name属性
                    else label = element.GetAttribute("label");//功能节点就取其label属性

                    node.CheckState = CheckState.Indeterminate;//将当前树节点检查状态置为未定
                    bool have = false;
                    CurseTreeListNodes(node, label,ref have);//在当前树节点中寻找和菜单节点对应的子树节点，有没有找到have表示
                    int count = 0;
                    foreach (TreeListNode subNode in node.Nodes)
                    {
                        if (subNode.CheckState == CheckState.Checked)
                            count++;
                    }
                    if (count == node.Nodes.Count) node.CheckState = CheckState.Checked;    //如果当前树节点的子节点全部已检查，则将当前节点检查状态置为已查               
                }
                if (child.HasChildNodes)
                {
                    InitTreeList(child, node);//根据当前菜单节点的子节点进行递归
                }
            }
        }

        /// <summary>
        /// 在当前树节点中寻找和菜单节点对应的子树节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="label"></param>
        /// <param name="have"></param>
        private void CurseTreeListNodes(TreeListNode treeNode,string label,ref bool have)
        {
            
            foreach (TreeListNode node in treeNode.Nodes)
            {
                if (have) return;
                //node.CheckState = CheckState.Unchecked;
                string nodeName = node.GetValue("NodeName").ToString();
                if (label == nodeName)
                {
                    have = true;//若已找到
                    if (node.HasChildren) node.CheckState = CheckState.Indeterminate; //如果还有子节点，就将状态置为未定                                 
                    else node.CheckState = CheckState.Checked;//若无子节点，则置为已查
                    return;
                }
                else if (node.HasChildren)//当前树节点不是要找的树节点，就在其子节点中继续寻找
                {
                    //node.CheckState = CheckState.Indeterminate;
                    CurseTreeListNodes(node, label,ref have);
                    int count = 0;
                    foreach (TreeListNode child in node.Nodes)
                    {
                        if (child.CheckState == CheckState.Checked)
                            count++;
                    }
                    if (count == node.Nodes.Count) node.CheckState = CheckState.Checked;
                }
            }
        }

    }
}
