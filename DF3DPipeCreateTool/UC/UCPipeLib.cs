using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeCore;
using DF3DPipeCreateTool.Class;
using Gvitech.CityMaker.FdeGeometry;
using System.Runtime.InteropServices;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCPipeLib : XtraUserControl
    {
        private SimpleButton btnSet;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private ButtonEdit buttonEditFilePath;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private TextEdit tePwd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;

        private void InitializeComponent()
        {
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.buttonEditFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tePwd = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(151, 95);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(136, 22);
            this.btnSet.StyleController = this.layoutControl1;
            this.btnSet.TabIndex = 5;
            this.btnSet.Text = "设置";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tePwd);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.btnSet);
            this.layoutControl1.Controls.Add(this.buttonEditFilePath);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(299, 287);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = ((short)(1));
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "新建管线库"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "打开管线库")});
            this.radioGroup1.Size = new System.Drawing.Size(275, 27);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 6;
            // 
            // buttonEditFilePath
            // 
            this.buttonEditFilePath.Location = new System.Drawing.Point(51, 43);
            this.buttonEditFilePath.Name = "buttonEditFilePath";
            this.buttonEditFilePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditFilePath.Size = new System.Drawing.Size(236, 22);
            this.buttonEditFilePath.StyleController = this.layoutControl1;
            this.buttonEditFilePath.TabIndex = 4;
            this.buttonEditFilePath.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditFilePath_ButtonPressed);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(299, 287);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 83);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(139, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.buttonEditFilePath;
            this.layoutControlItem1.CustomizationFormText = "路径：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(279, 26);
            this.layoutControlItem1.Text = "路径：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSet;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(139, 83);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(140, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.radioGroup1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(279, 31);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 109);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(279, 158);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tePwd
            // 
            this.tePwd.Location = new System.Drawing.Point(51, 69);
            this.tePwd.Name = "tePwd";
            this.tePwd.Properties.PasswordChar = '*';
            this.tePwd.Size = new System.Drawing.Size(236, 22);
            this.tePwd.StyleController = this.layoutControl1;
            this.tePwd.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tePwd;
            this.layoutControlItem4.CustomizationFormText = "密码：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 57);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(279, 26);
            this.layoutControlItem4.Text = "密码：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(36, 14);
            // 
            // UCPipeLib
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPipeLib";
            this.Size = new System.Drawing.Size(299, 287);
            this.Load += new System.EventHandler(this.UCPipeLib_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        public UCPipeLib()
        {
            InitializeComponent();
        }

        private void UCPipeLib_Load(object sender, EventArgs e)
        {
            string connStr = Config.GetConfigValue("3DPipeDataConnStr");
            IConnectionInfo connInfo = new ConnectionInfo();
            connInfo.FromConnectionString(connStr);
            if (connInfo != null && connInfo.ConnectionType == gviConnectionType.gviConnectionFireBird2x)
            {
                this.buttonEditFilePath.Text = connInfo.Database;
                this.tePwd.Text = connInfo.Password;
            }
        }

        private void buttonEditFilePath_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    SaveFileDialog sdlg = new SaveFileDialog()
                    {
                        Filter = "FDB File(*.fdb)|*.fdb",
                        DefaultExt = "fdb",
                        RestoreDirectory = true
                    };
                    if (sdlg.ShowDialog() == DialogResult.OK)
                    {
                        this.buttonEditFilePath.EditValue = sdlg.FileName;
                    }
                    break;
                case 1:
                    OpenFileDialog odlg = new OpenFileDialog()
                    {
                        Filter = "FDB File(*.fdb)|*.fdb",
                        RestoreDirectory = true
                    };
                    if (odlg.ShowDialog() == DialogResult.OK)
                    {
                        this.buttonEditFilePath.EditValue = odlg.FileName;
                    }
                    break;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.buttonEditFilePath.Text.ToString().Trim()))
            {
                XtraMessageBox.Show("路径不能为空！", "提示");
                return;
            }
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    CreatePipeLib();
                    break;
                case 1:
                    OpenPipeLib();
                    break;
            }

        }

        public bool InitPipeLib(IDataSource ds)
        {
            if (ds == null)
            {
                return false;
            }
            IFeatureDataSet dataset = null;
            IObjectClass oc = null;
            IFieldInfoCollection fields = null;
            string[] arrDBIndex = null;
            try
            {
                //IDomainFactory o = null;
                //ICodedValueDomain domain = null;
                //string[] array = null;
                //o = new DomainFactoryClass();
                //CRSFactoryClass crsFactory = new CRSFactoryClass();
                //array = ds.GetDomainNames();
                //if ((array == null) || (Array.IndexOf<string>(array, "Domain_GroupInfo") == -1))
                //{
                //    domain = o.CreateCodedValueDomain("Domain_GroupInfo", gviFieldType.gviFieldInt32);
                //    domain.Description = "逻辑图层分组";
                //    ds.AddDomain(domain);
                //}
                //Marshal.ReleaseComObject(o);
                CRSFactoryClass crsFactory = new CRSFactoryClass();
                string wKT = "UNKNOWNCS[\"unnamed\"]";
                ISpatialCRS spatialCRS = crsFactory.CreateFromWKT(wKT) as ISpatialCRS;
                if (spatialCRS != null)
                {
                    if (DataProvider.Instance.TryCeateFeatureDataSet(ds, "DataSet_BIZ", spatialCRS, out dataset) == -1)
                    {
                        return false;
                    }
                    dataset.Alias = "业务数据集";
                    Marshal.ReleaseComObject(dataset);
                    if (DataProvider.Instance.TryCeateFeatureDataSet(ds, "DataSet_GEO_Actuality", spatialCRS, out dataset) == -1)
                    {
                        return false;
                    }
                    dataset.Alias = "现状空间数据集";
                    Marshal.ReleaseComObject(dataset);

                    if (!DataProvider.Instance.TryOpenFeatureDataSet(ds, "DataSet_BIZ".ToString(), out dataset))
                    {
                        return false;
                    }
                    fields = DataModel.GetDataModel("OC_FacilityClass", out arrDBIndex);
                    if (fields != null)
                    {
                        switch (DataProvider.Instance.TryCeateObjectClass(dataset, "OC_FacilityClass", fields, arrDBIndex, out oc))
                        {
                            case -1:
                                return false;
                            case 1:
                                oc.AliasName = "设施类注册表";
                                Marshal.ReleaseComObject(oc);
                                return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception exception4)
            {
                return false;
            }
        }

        private void CreatePipeLib()
        {
            IDataSource ds = null;
            IDataSourceFactory factory = null;
            IConnectionInfo connectionInfo = null;
            try
            {
                if (DF3DPipeCreateApp.App.PipeLib != null)
                {
                    DF3DPipeCreateApp.App.PipeLib.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.PipeLib);
                    DF3DPipeCreateApp.App.PipeLib = null;
                }
                string fdbPath = this.buttonEditFilePath.Text.ToString().Trim();
                string pwd = this.tePwd.Text;
                if (System.IO.File.Exists(fdbPath))
                {
                    System.IO.File.Delete(fdbPath);
                }
                factory = new DataSourceFactoryClass();
                connectionInfo = new ConnectionInfoClass
                {
                    ConnectionType = gviConnectionType.gviConnectionFireBird2x,
                    Database = fdbPath
                };
                ds = factory.CreateDataSource(connectionInfo, pwd);
                if (ds == null)
                {
                    XtraMessageBox.Show("创建管线库数据库失败！", "提示");
                    return;
                }

                if (InitPipeLib(ds))
                {
                    DF3DPipeCreateApp.App.PipeLib = ds;
                    XtraMessageBox.Show("创建管线库数据库成功！", "提示");
                    Config.SetConfigValue("3DPipeDataConnStr", connectionInfo.ToConnectionString());
                }
                else
                {
                    DF3DPipeCreateApp.App.PipeLib = null;
                    XtraMessageBox.Show("创建管线库数据库失败！", "提示");
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void OpenPipeLib()
        {
            try
            {
                IConnectionInfo connInfo = new ConnectionInfo();
                connInfo.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                connInfo.Database = this.buttonEditFilePath.Text.ToString().Trim();
                connInfo.Password = this.tePwd.Text;
                IDataSourceFactory dsf = new DataSourceFactory();
                if (dsf.HasDataSource(connInfo))
                {
                    if (DF3DPipeCreateApp.App.PipeLib != null)
                    {
                        DF3DPipeCreateApp.App.PipeLib.Close();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.PipeLib);
                        DF3DPipeCreateApp.App.PipeLib = null;
                    }
                    DF3DPipeCreateApp.App.PipeLib = dsf.OpenDataSource(connInfo);
                    if (DF3DPipeCreateApp.App.PipeLib != null)
                    {
                        XtraMessageBox.Show("设置成功！", "提示");
                        Config.SetConfigValue("3DPipeDataConnStr", connInfo.ToConnectionString());
                    }
                    else XtraMessageBox.Show("设置失败！", "提示");
                }
                else
                {
                    XtraMessageBox.Show("数据连接失败！", "提示");
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
