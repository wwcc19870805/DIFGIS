using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using DF2DControl.Base;
using System.Xml;
using System.IO;
using DFCommon.Class;

namespace DF2DScan.Frm
{
    public partial class FrmCertainLoc : XtraForm
    {
        public FrmCertainLoc()
        {
            InitializeComponent();
        }

        Dictionary<string, IEnvelope> certainLocations = new Dictionary<string, IEnvelope>();
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btn_Cancel;
        private SimpleButton btn_Ok;
        private SimpleButton btn_delLoc;
        private SimpleButton btn_addLoc;
        private ListBoxControl lbc_locList;
        private TextEdit te_locName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        DF2DApplication app = DF2DApplication.Application;
        

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.btn_delLoc = new DevExpress.XtraEditors.SimpleButton();
            this.btn_addLoc = new DevExpress.XtraEditors.SimpleButton();
            this.lbc_locList = new DevExpress.XtraEditors.ListBoxControl();
            this.te_locName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_locList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_locName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_Ok);
            this.layoutControl1.Controls.Add(this.btn_delLoc);
            this.layoutControl1.Controls.Add(this.btn_addLoc);
            this.layoutControl1.Controls.Add(this.lbc_locList);
            this.layoutControl1.Controls.Add(this.te_locName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 262);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(175, 235);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(104, 22);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(175, 209);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(104, 22);
            this.btn_Ok.StyleController = this.layoutControl1;
            this.btn_Ok.TabIndex = 8;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_delLoc
            // 
            this.btn_delLoc.Location = new System.Drawing.Point(175, 57);
            this.btn_delLoc.Name = "btn_delLoc";
            this.btn_delLoc.Size = new System.Drawing.Size(104, 22);
            this.btn_delLoc.StyleController = this.layoutControl1;
            this.btn_delLoc.TabIndex = 7;
            this.btn_delLoc.Text = "删除场景";
            this.btn_delLoc.Click += new System.EventHandler(this.btn_delLoc_Click);
            // 
            // btn_addLoc
            // 
            this.btn_addLoc.Location = new System.Drawing.Point(175, 31);
            this.btn_addLoc.Name = "btn_addLoc";
            this.btn_addLoc.Size = new System.Drawing.Size(104, 22);
            this.btn_addLoc.StyleController = this.layoutControl1;
            this.btn_addLoc.TabIndex = 6;
            this.btn_addLoc.Text = "添加场景";
            this.btn_addLoc.Click += new System.EventHandler(this.btn_addLoc_Click);
            // 
            // lbc_locList
            // 
            this.lbc_locList.Location = new System.Drawing.Point(5, 31);
            this.lbc_locList.Name = "lbc_locList";
            this.lbc_locList.Size = new System.Drawing.Size(166, 226);
            this.lbc_locList.StyleController = this.layoutControl1;
            this.lbc_locList.TabIndex = 5;
            this.lbc_locList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbc_locList_MouseDoubleClick);
            // 
            // te_locName
            // 
            this.te_locName.Location = new System.Drawing.Point(68, 5);
            this.te_locName.Name = "te_locName";
            this.te_locName.Size = new System.Drawing.Size(211, 22);
            this.te_locName.StyleController = this.layoutControl1;
            this.te_locName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 262);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(284, 262);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_locName;
            this.layoutControlItem1.CustomizationFormText = "场景名称：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(278, 26);
            this.layoutControlItem1.Text = "场景名称：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lbc_locList;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(170, 230);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_addLoc;
            this.layoutControlItem3.CustomizationFormText = "添加场景";
            this.layoutControlItem3.Location = new System.Drawing.Point(170, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem3.Text = "添加场景";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_delLoc;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(170, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Ok;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(170, 204);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btn_Cancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(170, 230);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(170, 78);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(108, 126);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(108, 126);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(108, 126);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmCertainLoc
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmCertainLoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特定场景";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmCertainLoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbc_locList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_locName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private void btn_addLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (te_locName.Text != null)
                {
                    string location = te_locName.Text;
                    if (certainLocations.ContainsKey(location))
                    {
                        XtraMessageBox.Show("场景名称已存在，请重新添加", "提示");
                        return;
                    }
                    int index = lbc_locList.Items.Add(location);
                    lbc_locList.SelectedItem = lbc_locList.Items[index];
                    IEnvelope pEnvelope = app.Current2DMapControl.Extent;                
                    certainLocations.Add(location, pEnvelope);


                }
                else
                {
                    MessageBox.Show("请输入位置名称");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_delLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbc_locList.Items.Count > 1)
                {
                    lbc_locList.Items.Remove(lbc_locList.SelectedItem);
                    certainLocations.Remove(lbc_locList.SelectedItem.ToString());


                }
                else if (lbc_locList.Items.Count == 1)
                {
                    lbc_locList.Items.Remove(lbc_locList.SelectedItem);
                    certainLocations.Clear();
                }
                else if (lbc_locList.Items.Count == 0)
                {
                    MessageBox.Show("无可用位置");
                }
                else
                {
                    MessageBox.Show("请选择位置");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (lbc_locList.Items.Count > 0)
            {
                if (!certainLocations.ContainsKey(lbc_locList.SelectedItem.ToString())) return;
                app.Current2DMapControl.ActiveView.Extent = certainLocations[lbc_locList.SelectedItem.ToString()];
                app.Current2DMapControl.ActiveView.Refresh();
            }
            else
            {
                MessageBox.Show("无可用场景");
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            SaveCertainScene();
            this.Close();
        }

        private void SaveCertainScene()
        {
            try
            {
                string localPath = SystemInfo.Instance.LocalDataPath;
                string certainScenePath = localPath + "CertainScene\\";
                if (!Directory.Exists(certainScenePath))
                {
                    Directory.CreateDirectory(certainScenePath);
                }
                string certainScene = System.IO.Path.Combine(certainScenePath, "CertainScene2D.xml");
                if (File.Exists(certainScene))
                {
                    File.Delete(certainScene);
                }
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateElement("CertainScene2D");
                doc.AppendChild(root);
                foreach(KeyValuePair<string, IEnvelope> cl in certainLocations)
                {
                    XmlElement element = doc.CreateElement("Location");
                    element.SetAttribute("Name", cl.Key);
                    element.SetAttribute("XMax", cl.Value.XMax.ToString());
                    element.SetAttribute("XMin", cl.Value.XMin.ToString());
                    element.SetAttribute("YMax", cl.Value.YMax.ToString());
                    element.SetAttribute("YMin", cl.Value.YMin.ToString());
                    root.AppendChild(element);
                }
                doc.Save(certainScene);
                
                //在本地数据目录下再存一份参数配置
               
             
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private void LoadCertainScene()
        {
            try
            {
                this.certainLocations.Clear();
                string localPath = SystemInfo.Instance.LocalDataPath;
                string certainScenePath = localPath + "CertainScene\\";
                if (!Directory.Exists(certainScenePath)) return;
                string certainScene = System.IO.Path.Combine(certainScenePath, "CertainScene2D.xml");
                if (!File.Exists(certainScene)) return;

                XmlDocument doc = new XmlDocument();
                doc.Load(certainScene);
                XmlNode root = doc.SelectSingleNode("CertainScene2D");
                if (root == null) return;
                foreach (XmlElement element in root.ChildNodes)
                {
                    if (element.HasAttributes)
                    {
                        IEnvelope env = new EnvelopeClass();
                        env.XMax = Convert.ToDouble(element.GetAttribute("XMax"));
                        env.XMin = Convert.ToDouble(element.GetAttribute("XMin"));
                        env.YMax = Convert.ToDouble(element.GetAttribute("YMax"));
                        env.YMin = Convert.ToDouble(element.GetAttribute("YMin"));
                        certainLocations[element.GetAttribute("Name")] = env;
                    }
                }
                InitLocList();
            }
            catch (System.Exception ex)
            {
            	
            }
            

        }

        private void InitLocList()
        {
            this.lbc_locList.Items.Clear();
            lbc_locList.Items.AddRange(certainLocations.Keys.ToArray());
        }

        private void FrmCertainLoc_Load(object sender, EventArgs e)
        {
            LoadCertainScene();
        }

        private void lbc_locList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_Ok_Click(null, null);
        }

    }
}
