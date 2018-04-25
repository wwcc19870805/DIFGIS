using System.Diagnostics;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.GeoDatabaseDistributed;
using DevExpress.XtraEditors;
using DF2DTool.Class;

namespace DF2DTool.Frm
{
    public partial class FrmSDEBackup : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnSDE;
        private DevExpress.XtraEditors.TextEdit teSDE;
        private DevExpress.XtraEditors.SimpleButton btnMDB;
        private DevExpress.XtraEditors.TextEdit teMDB;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private IWorkspace m_pWorkspace = null;
        private string[] m_versionName;
        string[] featureClassName;
        string[] featureDatasetName;
        private int iMaxCount;
    
        public FrmSDEBackup()
        {
            InitializeComponent();
        }
        public FrmSDEBackup(IWorkspace pWs)
        {
            InitializeComponent();
            m_pWorkspace = pWs;
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnSDE = new DevExpress.XtraEditors.SimpleButton();
            this.teSDE = new DevExpress.XtraEditors.TextEdit();
            this.btnMDB = new DevExpress.XtraEditors.SimpleButton();
            this.teMDB = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teSDE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.btnSDE);
            this.layoutControl1.Controls.Add(this.teSDE);
            this.layoutControl1.Controls.Add(this.btnMDB);
            this.layoutControl1.Controls.Add(this.teMDB);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 135);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(147, 103);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(132, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(5, 103);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(138, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSDE
            // 
            this.btnSDE.Location = new System.Drawing.Point(233, 77);
            this.btnSDE.Name = "btnSDE";
            this.btnSDE.Size = new System.Drawing.Size(46, 22);
            this.btnSDE.StyleController = this.layoutControl1;
            this.btnSDE.TabIndex = 7;
            this.btnSDE.Text = "...";
            this.btnSDE.Click += new System.EventHandler(this.btnSDE_Click);
            // 
            // teSDE
            // 
            this.teSDE.EditValue = "尚未连接SDE网络数据库";
            this.teSDE.Location = new System.Drawing.Point(5, 77);
            this.teSDE.Name = "teSDE";
            this.teSDE.Size = new System.Drawing.Size(224, 22);
            this.teSDE.StyleController = this.layoutControl1;
            this.teSDE.TabIndex = 6;
            // 
            // btnMDB
            // 
            this.btnMDB.Location = new System.Drawing.Point(175, 25);
            this.btnMDB.Name = "btnMDB";
            this.btnMDB.Size = new System.Drawing.Size(104, 22);
            this.btnMDB.StyleController = this.layoutControl1;
            this.btnMDB.TabIndex = 5;
            this.btnMDB.Text = "文件存储路径";
            this.btnMDB.Click += new System.EventHandler(this.btnMDB_Click_1);
            // 
            // teMDB
            // 
            this.teMDB.Location = new System.Drawing.Point(5, 25);
            this.teMDB.Name = "teMDB";
            this.teMDB.Size = new System.Drawing.Size(166, 22);
            this.teMDB.StyleController = this.layoutControl1;
            this.teMDB.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 135);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "备份结果数据库PGDB";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(284, 52);
            this.layoutControlGroup2.Text = "备份结果数据库PGDB";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teMDB;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnMDB;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(170, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "待备份数据库SDE";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(284, 83);
            this.layoutControlGroup3.Text = "待备份数据库SDE";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teSDE;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(228, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSDE;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(228, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(50, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOK;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(142, 31);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(142, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(136, 31);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmSDEBackup
            // 
            this.ClientSize = new System.Drawing.Size(284, 135);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSDEBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SDE备份至PGDB";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teSDE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string checkoutName = System.IO.Path.GetFileNameWithoutExtension(teMDB.Text);//签出版本的名称
            string fileName = teMDB.Text;//文件名称
            Sde2Mdb(m_pWorkspace, featureClassName, featureDatasetName, checkoutName, fileName);            
            MessageBox.Show("完成SDE数据库的备份！", "SDE数据库备份", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            this.Dispose();
        }

        private void btnSDE_Click(object sender, EventArgs e)
        {
            FrmConnect frmConnect = new FrmConnect();
            if (frmConnect.ShowDialog() == DialogResult.OK)
            {
                m_pWorkspace = GetWorkspace(frmConnect.ConnPropertySet);

                if (m_pWorkspace != null)
                {
                    this.teSDE.Text = "连接SDE数据库成功。";
                    featureClassName = CommonFunction.getFeatureClassName(m_pWorkspace); //PublicFunction.getCheckLayer(pMasterWorkspace,pMapControl);//获得图层名称和该图层的要素类的名字
                    featureDatasetName = CommonFunction.getFeatureDataset(m_pWorkspace);
                    m_versionName = Utility.GetAllVersionName(m_pWorkspace);
                }
                else
                {
                    this.teSDE.Text = "连接SDE数据库失败!请重新连接。";
                }
            }          
        }

        /// <summary>
        /// 通过连接属性新建数据连接
        /// </summary>
        /// <param name="connectionPropertySet"></param>
        /// <returns></returns>
        private IWorkspace GetWorkspace(IPropertySet connectionPropertySet)
        {
            IWorkspaceFactory pSdeWorkspaceFactory = new SdeWorkspaceFactoryClass();
            try
            {
                return pSdeWorkspaceFactory.Open(connectionPropertySet, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private void btnMDB_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveMdb = new SaveFileDialog();
                saveMdb.Filter = "个人数据库(*.mdb)|*.mdb|所有文件(*.*)|*.*";
                if (saveMdb.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveMdb.FileName;
                    teMDB.Text = fileName;
                    //canCheckOut();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public void canCheckOut()
        {
            if (btnOK != null)
            {
                bool b = true;
                if (teMDB.Text == "")
                {
                    b = false;
                }

                if (m_versionName == null)
                {
                    MessageBox.Show("SDE数据库版本名称为空！");
                    return;
                }
                for (int i = 0; i < m_versionName.Length; i++)
                {
                    if (m_versionName[i] == "SDE." + System.IO.Path.GetFileNameWithoutExtension(teMDB.Text.Trim()))
                    {
                        b = false;
                        break;
                    }
                }
                btnOK.Enabled = b;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #region 签出数据  修改YuanHY 2009.01.15
        /// <summary>
        /// 签出数据
        /// </summary>
        /// <param name="pMasterWorkspace">源工作空间</param>
        /// <param name="featureClassName">要素类的名字</param>
        /// <param name="featureDatasetName">数据集的名字</param>
        /// <param name="checkoutName">版本名称</param>
        /// <param name="onlySchema">是否只签出结构</param>
        /// <param name="reuseSchema">是否重用已有的结构</param>
        /// <param name="fileName">文件名</param>
        /// <param name="dataExtractionType">数据提取的类型（checkout或只导出数据）</param>
        /// <param name="replicaModelType">简单图层或与源工作空间结构相同</param>
        public void CheckOut(IWorkspace pMasterWorkspace,
                            string[] featureClassName,  //要素类类的名字
                            string[] featureDatasetName,//数据集的名字
                            string checkoutName,
                            bool onlySchema,
                            bool reuseSchema,
                            string fileName,
                            esriDataExtractionType dataExtractionType,
                            esriReplicaModelType replicaModelType)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            bool hasVersion = false;
            string versionName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            IEnumVersionInfo pEnumVersionInfo = (pMasterWorkspace as IVersionedWorkspace).Versions;
            if (pEnumVersionInfo != null)
            {
                pEnumVersionInfo.Reset();
                IVersionInfo pVersionInfo = pEnumVersionInfo.Next();
                while (pVersionInfo != null)
                {
                    if (pVersionInfo.VersionName == "SDE." + checkoutName)
                    {
                        hasVersion = true;
                        break;
                    }
                    pVersionInfo = pEnumVersionInfo.Next();
                }
            }
            if (hasVersion)
            {
                IVersion pVersion = (pMasterWorkspace as IVersionedWorkspace).FindVersion(versionName);
                if (pVersion != null)
                {
                    pVersion.Delete();
                }
            }

            IPropertySet pCheckOutPropSet = new PropertySetClass();
            pCheckOutPropSet.SetProperty("Database", fileName);
            IWorkspaceFactory pCheckOutWorkFact = new AccessWorkspaceFactoryClass();
            string path = System.IO.Path.GetDirectoryName(fileName);
            IWorkspaceName pCheckOutWorkspaceName = pCheckOutWorkFact.Create(path, checkoutName, pCheckOutPropSet, 0);
            IName pName = pCheckOutWorkspaceName as IName;
            IWorkspace pCheckOutWorkspace = pName.Open() as IWorkspace;
            if (pCheckOutWorkspace != null)
            {
                IFeatureWorkspace pMasterFeaWorkspace = pMasterWorkspace as IFeatureWorkspace;
                IFeatureClass pFeaClass;
                IFeatureDataset pFeatureDataset;
                IDataset pDS;
                IEnumNameEdit pEnumNameEdit = new NamesEnumeratorClass();
                IEnumName pEnumName;
                IReplicaDescription pRepDescription = new ReplicaDescriptionClass();

                for (int i = 0; i < featureClassName.Length; i++)
                {
                    pFeaClass = pMasterFeaWorkspace.OpenFeatureClass(featureClassName[i]);
                    pDS = pFeaClass as IDataset;
                    pEnumNameEdit.Add(pDS.FullName as IName);
                }

                for (int i = 0; i < featureDatasetName.Length; i++)
                {
                    pFeatureDataset = pMasterFeaWorkspace.OpenFeatureDataset(featureDatasetName[i]);
                    pDS = pFeatureDataset as IDataset;
                    pEnumNameEdit.Add(pDS.FullName as IName);
                }

                pEnumName = pEnumNameEdit as IEnumName;
                pRepDescription.Init(pEnumName, pCheckOutWorkspaceName, false, dataExtractionType);
                pRepDescription.ReplicaModelType = replicaModelType;//简单图层类型或与SDE数据库一样
                ICheckOut pCheckOut = new CheckOutClass();
                try
                {
                    pCheckOut.CheckOutData(pRepDescription, true, checkoutName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("可能是备份的SDE数据没有注册为版本,或者是由于该文件名上次已经作为一个版本号存在数据库中。" + ex.ToString());
                }
                pCheckOutWorkspace = null;
                GC.Collect();
            }
        }
        #endregion

        #region sde数据库导出到本地mdb
        private void Sde2Mdb(IWorkspace pMasterWorkspace, string[] featureClassName, string[] featureDatasetName, string outName, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            IPropertySet pCheckOutPropSet = new PropertySetClass();
            pCheckOutPropSet.SetProperty("Database", fileName);
            IWorkspaceFactory pCheckOutWorkFact = new AccessWorkspaceFactoryClass();
            string path = System.IO.Path.GetDirectoryName(fileName);
            IWorkspaceName pCheckOutWorkspaceName = pCheckOutWorkFact.Create(path, outName, pCheckOutPropSet, 0);
            IName pName = pCheckOutWorkspaceName as IName;
            IWorkspace pCheckOutWorkspace = pName.Open() as IWorkspace;

            if (pCheckOutWorkspace != null)
            {
                IFeatureWorkspace pMasterFeaWorkspace = pMasterWorkspace as IFeatureWorkspace;
                IFeatureDataset pFeatureDataset;

                for (int i = 0; i < featureClassName.Length; i++)
                {
                    //this.pgbExportstat.PerformStep();
                    ConvertFeatureClass(pMasterWorkspace, pCheckOutWorkspace, featureClassName[i], featureClassName[i]);
                }

                for (int i = 0; i < featureDatasetName.Length; i++)
                {
                    //this.pgbExportstat.PerformStep();
                    pFeatureDataset = pMasterFeaWorkspace.OpenFeatureDataset(featureDatasetName[i]);
                    //在本地创建同名要素数据集
                    IFeatureDataset Temp_LocalDataset = (pCheckOutWorkspace as IFeatureWorkspace).CreateFeatureDataset(featureDatasetName[i], (pFeatureDataset as IGeoDataset).SpatialReference);

                    //批量导入要素类
                    for (int j = 0; j < (pFeatureDataset as IFeatureClassContainer).ClassCount; j++)
                    {
                        string fcName = ((pFeatureDataset as IFeatureClassContainer).get_Class(j) as IDataset).Name;
                        ConvertFeatureClass(pMasterWorkspace, Temp_LocalDataset, fcName, fcName);
                    }

                }
                GC.Collect();
            }

        }
        #endregion

        #region SDE要素类导出mdb(独立要素类)
        /// <summary>
        /// SDE要素类导出mdb
        /// </summary>
        /// <param name="sourceWorkspace">源工作空间</param>
        /// <param name="targetWorkspace">目标工作空间</param>
        /// <param name="nameOfSourceFeatureClass">源要素类名</param>
        /// <param name="nameOfTargetFeatureClass">目标要素类名</param>
        /// <param name="queryFilter"></param>
        /// <returns></returns>
        private bool ConvertFeatureClass(IWorkspace sourceWorkspace, IWorkspace targetWorkspace, string nameOfSourceFeatureClass, string nameOfTargetFeatureClass)
        {
            //创建一个源数据的工作空间的name  
            IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
            IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

            //创建源数据Name作为转换参数   
            IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
            IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
            sourceDatasetName.WorkspaceName = sourceWorkspaceName;
            sourceDatasetName.Name = nameOfSourceFeatureClass;

            //创建目标（导出）数据空间的name 
            IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
            IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;

            //创建目标数据Name作为转换参数   
            IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
            IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
            targetDatasetName.WorkspaceName = targetWorkspaceName;

            //这个命名也很重要，如果是mdb，那么这个命名在数据库中必须是本来不存在的（当然你可以先清空数据库）
            targetDatasetName.Name = nameOfTargetFeatureClass;

            //根据FetureClassName打开数据   
            ESRI.ArcGIS.esriSystem.IName sourceName = (ESRI.ArcGIS.esriSystem.IName)sourceFeatureClassName;
            IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

            //在两个不同的工作空间转换数据要检查字段的有效性
            //一些检查有效性的参数
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields targetFeatureClassFields;
            IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
            IEnumFieldError enumFieldError;
            //设置检查有效性的源数据工作空间    
            fieldChecker.InputWorkspace = sourceWorkspace;
            fieldChecker.ValidateWorkspace = targetWorkspace;
            fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);

            //通过输出字段循环找出几何字段
            IField geometryField;
            //遍历字段
            for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
            {
                //找到定义几何要素类型的字段
                if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    //获取当前字段
                    geometryField = targetFeatureClassFields.get_Field(i);
                    //编辑几何要素类型字段
                    IGeometryDef geometryDef = geometryField.GeometryDef;
                    IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;
                    targetFCGeoDefEdit.GridCount_2 = 1;
                    targetFCGeoDefEdit.set_GridSize(0, 0);
                    //保证空间参考      SpatialReference_2属性字段才是可写的，SpatialReference属性只可读
                    targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                    //开始转换
                    IFeatureDataConverter myConvert = new FeatureDataConverterClass();
                    myConvert.ConvertFeatureClass(sourceFeatureClassName, null, null, targetFeatureClassName, geometryDef, targetFeatureClassFields, "", 1000, 0);
                    return true;
                }

            }
            return false;
        }
        #endregion

        #region SDE要素类导出mdb（要素集下要素类）
        /// <summary>
        /// SDE要素类导出mdb（要素集下要素类）
        /// </summary>
        /// <param name="IN_SourceWorkspace">源工作空间</param>
        /// <param name="IN_TargetWorkspace">目标要素数据集</param>
        /// <param name="IN_SourceFeatureClassName">源要素类名</param>
        /// <param name="IN_TargetFeatureClassName">目标要素类名</param>
        private void ConvertFeatureClass(IWorkspace IN_SourceWorkspace, IFeatureDataset IN_TargetWorkspace, string IN_SourceFeatureClassName, string IN_TargetFeatureClassName)
        {
            #region 环境配置
            //创建源工作空间名  
            IDataset Temp_SourceWorkspaceDataset = (IDataset)IN_SourceWorkspace;
            IWorkspaceName Temp_SourceWorkspaceName = (IWorkspaceName)Temp_SourceWorkspaceDataset.FullName;
            //创建源要素数据集名  
            IFeatureClassName Temp_SourceFeatureClassName = new FeatureClassNameClass();
            IDatasetName Temp_SourceDatasetName = (IDatasetName)Temp_SourceFeatureClassName;
            Temp_SourceDatasetName.WorkspaceName = Temp_SourceWorkspaceName;
            Temp_SourceDatasetName.Name = IN_SourceFeatureClassName;
            //创建目标工作空间名    
            IDataset Temp_TargetWorkspaceDataset = (IDataset)IN_TargetWorkspace.Workspace;
            IWorkspaceName Temp_TargetWorkspaceName = (IWorkspaceName)(Temp_TargetWorkspaceDataset.FullName);
            //创建目标要素类名  
            IFeatureClassName Temp_TargetFeatureClassName = new FeatureClassNameClass();
            IDatasetName Temp_TargetDatasetName = (IDatasetName)Temp_TargetFeatureClassName;
            Temp_TargetDatasetName.WorkspaceName = Temp_TargetWorkspaceName;
            Temp_TargetDatasetName.Name = IN_TargetFeatureClassName;
            //创建目标要素数据集名  
            IFeatureDatasetName Temp_TargetFeatureDatasetName = new FeatureDatasetNameClass();
            IDatasetName Temp_TargetDatasetName2 = (IDatasetName)Temp_TargetFeatureDatasetName;
            Temp_TargetDatasetName2.WorkspaceName = Temp_TargetWorkspaceName;
            Temp_TargetDatasetName2.Name = IN_TargetWorkspace.Name;
            #endregion
            //打开源要素类获取字段定义 
            ESRI.ArcGIS.esriSystem.IName Temp_SourceName = (ESRI.ArcGIS.esriSystem.IName)Temp_SourceFeatureClassName;
            IFeatureClass Temp_SourceFeatureClass = (IFeatureClass)Temp_SourceName.Open();
            //验证字段 
            IFieldChecker Temp_FieldChecker = new FieldCheckerClass();
            IFields Temp_TargetFeatureClassFields;
            IFields Temp_SourceFeatureClassFields = Temp_SourceFeatureClass.Fields;
            IEnumFieldError enumFieldError;
            Temp_FieldChecker.InputWorkspace = IN_SourceWorkspace;
            Temp_FieldChecker.ValidateWorkspace = IN_TargetWorkspace.Workspace;
            Temp_FieldChecker.Validate(Temp_SourceFeatureClassFields, out enumFieldError, out Temp_TargetFeatureClassFields);
            //批量导入 
            IField Temp_GeometryField;
            for (int i = 0; i < Temp_TargetFeatureClassFields.FieldCount; i++)
            {
                if (Temp_TargetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    Temp_GeometryField = Temp_TargetFeatureClassFields.get_Field(i);
                    //获取空间定义           
                    IGeometryDef Temp_GeometryDef = Temp_GeometryField.GeometryDef;
                    IGeometryDefEdit Temp_TargetFCGeoDefEdit = (IGeometryDefEdit)Temp_GeometryDef;
                    Temp_TargetFCGeoDefEdit.GridCount_2 = 1;
                    Temp_TargetFCGeoDefEdit.set_GridSize(0, 0);
                    Temp_TargetFCGeoDefEdit.SpatialReference_2 = Temp_GeometryField.GeometryDef.SpatialReference;
                    //定义筛选条件 
                    IQueryFilter Temp_QueryFilter = new QueryFilterClass();
                    Temp_QueryFilter.WhereClause = "";
                    //导入要素类至要素数据集  
                    IFeatureDataConverter Temp_FeatureDataConverter = new FeatureDataConverterClass();
                    IEnumInvalidObject enumErrors = Temp_FeatureDataConverter.ConvertFeatureClass(Temp_SourceFeatureClassName, Temp_QueryFilter, Temp_TargetFeatureDatasetName, Temp_TargetFeatureClassName, Temp_GeometryDef, Temp_TargetFeatureClassFields, "", 1000, 0);
                    break;
                }
            }

        }
        #endregion

        private void btnMDB_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveMdb = new SaveFileDialog();
                saveMdb.Filter = "个人数据库(*.mdb)|*.mdb|所有文件(*.*)|*.*";
                if (saveMdb.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveMdb.FileName;
                    teMDB.Text = fileName;
                    //canCheckOut();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
