using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using DFCommon.Class;
using DevExpress.XtraTreeList.Nodes;
using DFDataConfig.Class;
using System.Xml;
using DF3DData.Class;
using DFDataConfig.Logic;
using DFWinForms.Class;

namespace DF3DDataConfig
{
    public class MainForm : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private SimpleButton btn_quit;
        private SimpleButton btn_ok;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFeatureClassAlias;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFeatureClassName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFacilityClass;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMajorClass;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFeatureClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private CheckEdit ceSelectAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private CheckEdit ceConnectMajorClass;
        private CheckEdit ceConnectFacility;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceConnectMajorClass = new DevExpress.XtraEditors.CheckEdit();
            this.ceConnectFacility = new DevExpress.XtraEditors.CheckEdit();
            this.ceSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnFeatureClassAlias = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFeatureClassName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFacilityClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnMajorClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnFeatureClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btn_quit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ok = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceConnectMajorClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceConnectFacility.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelectAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceConnectMajorClass);
            this.layoutControl1.Controls.Add(this.ceConnectFacility);
            this.layoutControl1.Controls.Add(this.ceSelectAll);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btn_quit);
            this.layoutControl1.Controls.Add(this.btn_ok);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(600, 392);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceConnectMajorClass
            // 
            this.ceConnectMajorClass.EditValue = true;
            this.ceConnectMajorClass.Location = new System.Drawing.Point(194, 368);
            this.ceConnectMajorClass.Name = "ceConnectMajorClass";
            this.ceConnectMajorClass.Properties.Caption = "关联大类";
            this.ceConnectMajorClass.Size = new System.Drawing.Size(103, 19);
            this.ceConnectMajorClass.StyleController = this.layoutControl1;
            this.ceConnectMajorClass.TabIndex = 10;
            // 
            // ceConnectFacility
            // 
            this.ceConnectFacility.EditValue = true;
            this.ceConnectFacility.Location = new System.Drawing.Point(105, 368);
            this.ceConnectFacility.Name = "ceConnectFacility";
            this.ceConnectFacility.Properties.Caption = "关联设施类";
            this.ceConnectFacility.Size = new System.Drawing.Size(85, 19);
            this.ceConnectFacility.StyleController = this.layoutControl1;
            this.ceConnectFacility.TabIndex = 9;
            // 
            // ceSelectAll
            // 
            this.ceSelectAll.EditValue = true;
            this.ceSelectAll.Location = new System.Drawing.Point(2, 368);
            this.ceSelectAll.Name = "ceSelectAll";
            this.ceSelectAll.Properties.Caption = "全选/取消全选";
            this.ceSelectAll.Size = new System.Drawing.Size(99, 19);
            this.ceSelectAll.StyleController = this.layoutControl1;
            this.ceSelectAll.TabIndex = 8;
            this.ceSelectAll.CheckedChanged += new System.EventHandler(this.ceSelectAll_CheckedChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(596, 362);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnFeatureClassAlias,
            this.gridColumnFeatureClassName,
            this.gridColumnFacilityClass,
            this.gridColumnMajorClass,
            this.gridColumnFeatureClass,
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnFeatureClassAlias
            // 
            this.gridColumnFeatureClassAlias.Caption = "要素类别名";
            this.gridColumnFeatureClassAlias.FieldName = "FeatureClassAlias";
            this.gridColumnFeatureClassAlias.Name = "gridColumnFeatureClassAlias";
            this.gridColumnFeatureClassAlias.OptionsColumn.AllowEdit = false;
            this.gridColumnFeatureClassAlias.Visible = true;
            this.gridColumnFeatureClassAlias.VisibleIndex = 1;
            this.gridColumnFeatureClassAlias.Width = 253;
            // 
            // gridColumnFeatureClassName
            // 
            this.gridColumnFeatureClassName.Caption = "要素类名称";
            this.gridColumnFeatureClassName.FieldName = "FeatureClassName";
            this.gridColumnFeatureClassName.Name = "gridColumnFeatureClassName";
            this.gridColumnFeatureClassName.OptionsColumn.AllowEdit = false;
            this.gridColumnFeatureClassName.Visible = true;
            this.gridColumnFeatureClassName.VisibleIndex = 2;
            this.gridColumnFeatureClassName.Width = 253;
            // 
            // gridColumnFacilityClass
            // 
            this.gridColumnFacilityClass.Caption = "所属设施类";
            this.gridColumnFacilityClass.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumnFacilityClass.FieldName = "FacilityClass";
            this.gridColumnFacilityClass.Name = "gridColumnFacilityClass";
            this.gridColumnFacilityClass.Visible = true;
            this.gridColumnFacilityClass.VisibleIndex = 3;
            this.gridColumnFacilityClass.Width = 253;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gridColumnMajorClass
            // 
            this.gridColumnMajorClass.Caption = "所属大类";
            this.gridColumnMajorClass.ColumnEdit = this.repositoryItemComboBox2;
            this.gridColumnMajorClass.FieldName = "MajorClass";
            this.gridColumnMajorClass.Name = "gridColumnMajorClass";
            this.gridColumnMajorClass.Visible = true;
            this.gridColumnMajorClass.VisibleIndex = 4;
            this.gridColumnMajorClass.Width = 261;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // gridColumnFeatureClass
            // 
            this.gridColumnFeatureClass.Caption = "要素类对象";
            this.gridColumnFeatureClass.FieldName = "FeatureClass";
            this.gridColumnFeatureClass.Name = "gridColumnFeatureClass";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = " ";
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "CheckState";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSize = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 24;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit1_CheckedChanged);
            // 
            // btn_quit
            // 
            this.btn_quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_quit.Location = new System.Drawing.Point(507, 368);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(91, 22);
            this.btn_quit.StyleController = this.layoutControl1;
            this.btn_quit.TabIndex = 6;
            this.btn_quit.Text = "退出";
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(409, 368);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(94, 22);
            this.btn_ok.StyleController = this.layoutControl1;
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
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
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(600, 392);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(299, 366);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(108, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_ok;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(407, 366);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_quit;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(505, 366);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(95, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(600, 366);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ceSelectAll;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 366);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ceConnectFacility;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(103, 366);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ceConnectMajorClass;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(192, 366);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(107, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btn_ok;
            this.CancelButton = this.btn_quit;
            this.ClientSize = new System.Drawing.Size(600, 392);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三维数据关联";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceConnectMajorClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceConnectFacility.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelectAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitFacility()
        {
            List<FacilityClass> list = FacilityClassManager.Instance.GetAllFacilityClass();
            if (list != null)
            {
                foreach (FacilityClass fc in list)
                {
                    this.repositoryItemComboBox1.Items.Add(fc);
                }
            }
        }

        private void InitMajorClass()
        {
            List<MajorClass> list = LogicDataStructureManage3D.Instance.GetAllMajorClass();
            if (list != null)
            {
                foreach (MajorClass mc in list)
                {
                    this.repositoryItemComboBox2.Items.Add(mc);
                }
            }
        }

        private XmlDocument _facDoc;
        private string _xmlFacFilePath;
        private XmlDocument _logicDoc; 
        private string _xmlLogicFilePath;
        private DataTable _dt;
        private void LoadData(IConnectionInfo connInfo)
        {
            try
            {
                IDataSourceFactory dsFactory = new DataSourceFactory();
                if (dsFactory.HasDataSource(connInfo))
                {
                    IDataSource ds = dsFactory.OpenDataSource(connInfo);
                    if (ds != null)
                    {
                        string[] strfdss = ds.GetFeatureDatasetNames();
                        if (strfdss == null) return;
                        foreach (string strfds in strfdss)
                        {
                            IFeatureDataSet fds = ds.OpenFeatureDataset(strfds);
                            if (fds == null) continue;
                            string[] fcnames = fds.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                            if (fcnames == null) continue;
                            foreach (string fcname in fcnames)
                            {
                                IFeatureClass fc = fds.OpenFeatureClass(fcname);
                                if (fc == null) continue;
                                DataRow dr = this._dt.NewRow();
                                dr["FeatureClassAlias"] = fc.AliasName;
                                dr["FeatureClassName"] = fc.Name;
                                dr["FeatureClass"] = fc;
                                dr["CheckState"] = true;
                                FacilityClass facC = DF3DData.Class.Dictionary3DTable.Instance.GetFacilityClassByDFFeatureClassID(fc.Guid.ToString());
                                if (facC != null) dr["FacilityClass"] = facC;
                                else
                                {
                                    foreach (FacilityClass facTemp in FacilityClassManager.Instance.GetAllFacilityClass())
                                    {
                                        if (fc.AliasName.Contains(facTemp.Alias))
                                        {
                                            dr["FacilityClass"] = facTemp;
                                            break;
                                        }
                                    }
                                }
                                MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fc.Guid.ToString());
                                if (mc != null) dr["MajorClass"] = mc;
                                else
                                {
                                    foreach (MajorClass mcTemp in LogicDataStructureManage3D.Instance.GetAllMajorClass())
                                    {
                                        if (fc.AliasName.Contains(mcTemp.Alias))
                                        {
                                            dr["MajorClass"] = mcTemp;
                                            break;
                                        }
                                    }
                                }
                                this._dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this._dt = new DataTable();
                this._dt.Columns.AddRange(new DataColumn[] { new DataColumn("FeatureClassAlias"), new DataColumn("FeatureClassName"),
                    new DataColumn("FacilityClass",typeof(object)), new DataColumn("MajorClass",typeof(object)), new DataColumn("FeatureClass",typeof(object)),new DataColumn("CheckState",typeof(bool)) });
                this.gridControl1.DataSource = this._dt;

                this._xmlFacFilePath = Config.GetConfigValue("FacilityClassXmlPath");
                if (!System.IO.File.Exists(this._xmlFacFilePath)) return;
                this._facDoc = new XmlDocument();
                this._facDoc.Load(this._xmlFacFilePath);

                InitFacility();

                this._xmlLogicFilePath = Config.GetConfigValue("LogicDataStructureXmlPath3D");
                if (!System.IO.File.Exists(this._xmlLogicFilePath)) return;
                this._logicDoc = new XmlDocument();
                this._logicDoc.Load(this._xmlLogicFilePath);
                InitMajorClass();

                string str3DThematicDataConn = Config.GetConfigValue("3DThematicDataConnStr");
                if (!string.IsNullOrEmpty(str3DThematicDataConn))
                {
                    IConnectionInfo ci = new ConnectionInfo();
                    ci.FromConnectionString(str3DThematicDataConn);
                    LoadData(ci);
                }
                string str3DBaseDataConn = Config.GetConfigValue("3DBaseDataConnStr");
                if (!string.IsNullOrEmpty(str3DBaseDataConn))
                {
                    IConnectionInfo ci = new ConnectionInfo();
                    ci.FromConnectionString(str3DBaseDataConn);
                    LoadData(ci);
                }
                string str3DPipeDataConn = Config.GetConfigValue("3DPipeDataConnStr");
                if (!string.IsNullOrEmpty(str3DPipeDataConn))
                {
                    IConnectionInfo ciPipe = new ConnectionInfo();
                    ciPipe.FromConnectionString(str3DPipeDataConn);
                    LoadData(ciPipe);
                }             
            }
            catch (Exception) { }
        }
        private HashSet<FacilityClass> _checkFacCList = new HashSet<FacilityClass>();
        private HashSet<MajorClass> _checkMCList = new HashSet<MajorClass>();
        private void CheckRowGroup()
        {
            this._checkFacCList.Clear();
            this._checkMCList.Clear();
            if (this._dt == null || this._dt.Rows.Count == 0) return;
            foreach (DataRow dr in this._dt.Rows)
            {
                if (!(bool)dr["CheckState"]) continue;
                if (dr["FacilityClass"] != null && dr["FacilityClass"] is FacilityClass) _checkFacCList.Add(dr["FacilityClass"] as FacilityClass);
                if (dr["MajorClass"] != null && dr["MajorClass"] is MajorClass) _checkMCList.Add(dr["MajorClass"] as MajorClass);
            }
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dt.Rows.Count == 0) return;
                if (!this.ceConnectMajorClass.Checked && !this.ceConnectFacility.Checked) return;
                WaitForm.Start("三维数据关联设置启动...", "请稍后", new Size(400, 50));
                CheckRowGroup();
                WaitForm.SetCaption("设施类关联设置进行中...");
                // 设置设施类
                HashSet<FacilityClass> listFac = _checkFacCList;
                if (listFac != null && this.ceConnectFacility.Checked)
                {
                    foreach (FacilityClass fac in listFac)
                    {
                        string strTemp = "";
                        foreach (DataRow dr in this._dt.Rows)
                        {
                            if (!(bool)dr["CheckState"]) continue;
                            IFeatureClass featureClass = dr["FeatureClass"] as IFeatureClass;
                            if (featureClass == null) continue;
                            FacilityClass fcc = dr["FacilityClass"] as FacilityClass;
                            if (fcc == fac)
                            {
                                strTemp += featureClass.GuidString + ";";
                            }
                        }
                        if (this._facDoc != null)
                        {
                            XmlNode node = this._facDoc.SelectSingleNode(".//FacilityClass[@name=\"" + fac.Name + "\"]");
                            if (node != null)
                            {
                                if (node.Attributes["fc3D"] == null)
                                {
                                    XmlAttribute attr = this._facDoc.CreateAttribute("fc3D");
                                    node.Attributes.Append(attr);
                                }
                                node.Attributes["fc3D"].Value = strTemp;
                            }
                            this._facDoc.Save(this._xmlFacFilePath);
                        }
                    }
                }
                //设施大类所属
                WaitForm.SetCaption("所属大类关联设置进行中...");
                HashSet<MajorClass> listMC = _checkMCList;
                if (listMC != null && this.ceConnectMajorClass.Checked)
                {
                    foreach (MajorClass mc in listMC)
                    {
                        string strTemp = "";
                        List<IFeatureClass> listFc = new List<IFeatureClass>();
                        foreach (DataRow dr in this._dt.Rows)
                        {
                            if (!(bool)dr["CheckState"]) continue;
                            IFeatureClass featureClass = dr["FeatureClass"] as IFeatureClass;
                            if (featureClass == null) continue;
                            MajorClass mcc = dr["MajorClass"] as MajorClass;
                            if (mcc == mc)
                            {
                                strTemp += featureClass.GuidString + ";";
                                listFc.Add(featureClass);
                            }
                        }
                        if (this._logicDoc != null)
                        {
                            XmlNode node = this._logicDoc.SelectSingleNode(".//MajorClass[@name=\"" + mc.Name + "\"]");
                            if (node != null)
                            {
                                if (node.Attributes["fc3D"] == null)
                                {
                                    XmlAttribute attr = this._logicDoc.CreateAttribute("fc3D");
                                    node.Attributes.Append(attr);
                                }
                                node.Attributes["fc3D"].Value = strTemp;
                            }
                            for (int i = node.ChildNodes.Count -1; i >=0 ; i--)
                            {
                                XmlNode childNode = node.ChildNodes[i];
                                node.RemoveChild(childNode);
                            }
                                //foreach (XmlNode childNode in node.ChildNodes)
                                //{
                                //    node.RemoveChild(childNode);
                                //}

                            // 二级分组
                            WaitForm.SetCaption("小类分组中...");
                            IQueryFilter filter = null;
                            IFdeCursor cursor = null;
                            IRowBuffer row = null;
                            HashSet<string> hsValues = new HashSet<string>();
                            try
                            {
                                foreach (IFeatureClass fc in listFc)
                                {
                                    WaitForm.SetCaption("要素类【" + (string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName) + "】分组中...");
                                    if (fc.GetFields().IndexOf(mc.ClassifyField) == -1) continue;
                                    if (fc.GetFields().IndexOf("GroupId") == -1) continue;
                                    filter = new QueryFilter();
                                    filter.SubFields = mc.ClassifyField;
                                    cursor = fc.Search(filter, true);

                                    while ((row = cursor.NextRow()) != null)
                                    {
                                        if (!row.IsNull(0))
                                        {
                                            hsValues.Add(row.GetValue(0).ToString());
                                        }
                                    }
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
                                    fc.SubTypeFieldName = ""; 
                                    fc.SubTypeFieldName = "GroupId";
                                }
                                // 重设groupid
                                WaitForm.SetCaption("启动重置GroupId...");
                                foreach (IFeatureClass fc in listFc)
                                {
                                    string strfcname = (string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName);
                                    WaitForm.SetCaption("要素类【" + strfcname + "】重置GroupId中...");
                                    if (fc.GetFields().IndexOf(mc.ClassifyField) == -1) continue;
                                    if (fc.GetFields().IndexOf("GroupId") == -1) continue;
                                    filter = new QueryFilter();
                                    filter.SubFields = "GroupId,oid";
                                    int groupid = -1;
                                    foreach (string str in hsValues)
                                    {
                                        groupid++;
                                        filter.WhereClause = mc.ClassifyField + " = '" + str + "'";
                                        int fccount = fc.GetCount(filter);
                                        if (fccount == 0) continue;
                                        int counttemp = 0;
                                        cursor = fc.Update(filter);
                                        while ((row = cursor.NextRow()) != null)
                                        {
                                            row.SetValue(0, groupid);
                                            cursor.UpdateRow(row);
                                            counttemp++;
                                            WaitForm.SetCaption("要素类【" + strfcname + "】子类【" + str + "】(" + counttemp + "/" + fccount + ")重置GroupId中...");
                                        }
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
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
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
                                if (filter != null)
                                {
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(filter);
                                    row = null;
                                }
                            }
                            WaitForm.SetCaption("分组信息写入配置文件中...");
                            int index = -1;
                            foreach (string str in hsValues)
                            {
                                index++;
                                XmlElement ele = this._logicDoc.CreateElement("SubClass");
                                ele.SetAttribute("name", str);
                                ele.SetAttribute("groupid", index.ToString());
                                node.AppendChild(ele);
                                foreach (IFeatureClass fc in listFc)
                                {
                                    if (fc.GetFields().IndexOf(mc.ClassifyField) == -1) continue;
                                    if (fc.GetFields().IndexOf("GroupId") == -1) continue;
                                    SubTypeInfo sti = new SubTypeInfo();
                                    sti.Code = index;
                                    sti.Name = str;
                                    fc.AddSubType(sti);
                                }
                            }
                            //XmlElement ele1 = this._logicDoc.CreateElement("SubClass");
                            //ele1.SetAttribute("name", "其他");
                            //ele1.SetAttribute("groupid", index.ToString());
                            //node.AppendChild(ele1);

                            this._logicDoc.Save(this._xmlLogicFilePath);
                        }
                    }
                }
                WaitForm.Stop();
                XtraMessageBox.Show("设置成功！", "提示");
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
                XtraMessageBox.Show("设置失败！", "提示");
            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ce = sender as CheckEdit;
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (ce != null && dr != null)
            {
                if (ce.Checked) dr["CheckState"] = true;
                else dr["CheckState"] = false;
            }
        }

        private void ceSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this._dt == null || this._dt.Rows.Count == 0) return;
            foreach (DataRow dr in this._dt.Rows)
            {
                dr["CheckState"] = this.ceSelectAll.Checked;
            }

        }


    }
}
