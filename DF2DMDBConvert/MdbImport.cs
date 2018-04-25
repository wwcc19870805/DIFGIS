using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using DFCommon.Class;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Collections;
using DFWinForms.Class;
using System.Xml;
using System.IO;
using DF2DMDBConvert.Class;
using ESRI.ArcGIS.Geometry;
using System.Runtime.InteropServices;


namespace DF2DMDBConvert
{
    public partial class MdbImport : Form
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton te_CreateXml;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Target;
        private DevExpress.XtraEditors.SimpleButton btn_Original;
        private DevExpress.XtraEditors.TextEdit te_Target;
        private DevExpress.XtraEditors.TextEdit te_Original;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;

        OpenFileDialog ofd;
        SaveFileDialog sfd;
        string mdbOriginal;
        string mdbTarget;
        private DevExpress.XtraEditors.SimpleButton btn_Xml;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit te_Xml;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        string xmlName;

        List<string> listDsNames;
        Dictionary<string, List<IField>> dicFields;

    
        public MdbImport()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.te_Original = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.te_Target = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Original = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Target = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.te_CreateXml = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.te_Xml = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Xml = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Original.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Target.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Xml.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Xml);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.te_Xml);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.te_CreateXml);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btn_Target);
            this.layoutControl1.Controls.Add(this.btn_Original);
            this.layoutControl1.Controls.Add(this.te_Target);
            this.layoutControl1.Controls.Add(this.te_Original);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(358, 203);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
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
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlGroup2,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(358, 203);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // te_Original
            // 
            this.te_Original.Location = new System.Drawing.Point(78, 2);
            this.te_Original.Name = "te_Original";
            this.te_Original.Size = new System.Drawing.Size(223, 22);
            this.te_Original.StyleController = this.layoutControl1;
            this.te_Original.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_Original;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(76, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(227, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // te_Target
            // 
            this.te_Target.Location = new System.Drawing.Point(78, 28);
            this.te_Target.Name = "te_Target";
            this.te_Target.Size = new System.Drawing.Size(223, 22);
            this.te_Target.StyleController = this.layoutControl1;
            this.te_Target.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_Target;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(76, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(227, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // btn_Original
            // 
            this.btn_Original.Location = new System.Drawing.Point(305, 2);
            this.btn_Original.Name = "btn_Original";
            this.btn_Original.Size = new System.Drawing.Size(51, 22);
            this.btn_Original.StyleController = this.layoutControl1;
            this.btn_Original.TabIndex = 6;
            this.btn_Original.Text = "...";
            this.btn_Original.Click += new System.EventHandler(this.btn_Original_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_Original;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(303, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(55, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // btn_Target
            // 
            this.btn_Target.Location = new System.Drawing.Point(305, 28);
            this.btn_Target.Name = "btn_Target";
            this.btn_Target.Size = new System.Drawing.Size(51, 22);
            this.btn_Target.StyleController = this.layoutControl1;
            this.btn_Target.TabIndex = 7;
            this.btn_Target.Text = "...";
            this.btn_Target.Click += new System.EventHandler(this.btn_Target_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_Target;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(303, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(55, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "源 数 据 库：";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(2, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "目标数据库：";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelControl2;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(352, 93);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 78);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(358, 125);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // te_CreateXml
            // 
            this.te_CreateXml.Location = new System.Drawing.Point(5, 83);
            this.te_CreateXml.Name = "te_CreateXml";
            this.te_CreateXml.Size = new System.Drawing.Size(188, 22);
            this.te_CreateXml.StyleController = this.layoutControl1;
            this.te_CreateXml.TabIndex = 10;
            this.te_CreateXml.Text = "创建Xml";
            this.te_CreateXml.Click += new System.EventHandler(this.te_CreateXml_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.te_CreateXml;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(192, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(197, 83);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(156, 20);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 11;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.simpleButton2;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(192, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(160, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // te_Xml
            // 
            this.te_Xml.Location = new System.Drawing.Point(78, 54);
            this.te_Xml.Name = "te_Xml";
            this.te_Xml.Size = new System.Drawing.Size(223, 22);
            this.te_Xml.StyleController = this.layoutControl1;
            this.te_Xml.TabIndex = 12;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.te_Xml;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(76, 52);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(227, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(2, 54);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 14);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Xml 配 置：";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.labelControl3;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // btn_Xml
            // 
            this.btn_Xml.Location = new System.Drawing.Point(305, 54);
            this.btn_Xml.Name = "btn_Xml";
            this.btn_Xml.Size = new System.Drawing.Size(51, 22);
            this.btn_Xml.StyleController = this.layoutControl1;
            this.btn_Xml.TabIndex = 14;
            this.btn_Xml.Text = "...";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btn_Xml;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(303, 52);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(55, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // MdbImport
            // 
            this.ClientSize = new System.Drawing.Size(358, 203);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MdbImport";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Original.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Target.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_Xml.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);

        }

        private void btn_Original_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Access file(*.mdb)|*.mdb";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (ofd.FileName.Length > 0)
            {
                mdbOriginal = ofd.FileName;
                te_Original.Text = ofd.FileName;
            }
        }

        private void btn_Target_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Access file(*.mdb)|*.mdb";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (ofd.FileName.Length > 0)
            {
                mdbTarget = ofd.FileName;
                te_Target.Text = ofd.FileName;
            }
        }

        private void te_CreateXml_Click(object sender, EventArgs e)
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "XML file(*.xml)|*.xml";
            if (sfd.ShowDialog() != DialogResult.OK) return;
            if (sfd.FileName.Length > 0)
            {
                xmlName = sfd.FileName;
                te_Xml.Text = sfd.FileName;
            }
            if (this.mdbTarget == null) MessageBox.Show("请选择Cad数据库");
            WaitForm.Start("开始创建XMl数据...");
            ReadTableInMdb();
            CreateXML();
            WaitForm.SetCaption("XMl创建成功");
            WaitForm.Stop();
        }

        private void CreateXML()
        {

        }

        private void ReadTableInMdb()
        {
            WaitForm.SetCaption("开始遍历数据表...");
            IEnumDataset pEnumDs;
            IDataset pDs;
            IDataset pSubDs;
            ITable pTable;
            IFields pFields;
            IField pField;
            string tbName;
            List<IField> listFields;
           

            if (mdbTarget == null) return;
            dicFields = new Dictionary<string, List<IField>>();//初始化（表名-字段列表）字典
            listDsNames = new List<string>();

            try
            {
                IWorkspaceFactory pWsF = new AccessWorkspaceFactory();
                IWorkspace pWs = pWsF.OpenFromFile(mdbTarget, 0);//打开Cad数据库
                pEnumDs = pWs.get_Datasets(esriDatasetType.esriDTContainer);//获得所有数据集
                pEnumDs.Reset();
                while ((pDs = pEnumDs.Next()) != null)//遍历数据集
                {
                    if (pDs.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        listDsNames.Add(pDs.Name);
                        IEnumDataset pESubDataset = pDs.Subsets;
                        while ((pSubDs = pESubDataset.Next()) != null)
                        {
                            IFeatureClass fc = pSubDs as IFeatureClass;
                            string fcName = pSubDs.Name;
                            pFields = fc.Fields;//获得当前数据表字段集
                            listFields = new List<IField>();//初始化字段集列表
                            for (int i = 0; i < pFields.FieldCount; i++)
                            {
                                pField = pFields.Field[i];
                                listFields.Add(pField);//将当前字段添加进字段集列表
                            }
                            dicFields[fcName] = listFields;//更新（表名-字段列表）字典   
                        }
                    }
                    //tbName = pDs.Name; //获得当前数据集名称                                  
                    //WaitForm.SetCaption("正在遍历数据表" + tbName + "字段");
                    //pTable = pDs as ITable;//当前数据集转换为数据表

                    //pFields = pTable.Fields;//获得当前数据表字段集
                    //listFields = new List<IField>();//初始化字段集列表
                    //for (int i = 0; i < pFields.FieldCount; i++)
                    //{
                    //    pField = pFields.Field[i];
                    //    listFields.Add(pField);//将当前字段添加进字段集列表
                    //}
                    //dicFields[tbName] = listFields;//更新（表名-字段列表）字典           
                }

            }
            catch (System.Exception ex)
            {
                WaitForm.Stop();
                MessageBox.Show(ex.Message);

            }
        }
    }
}
