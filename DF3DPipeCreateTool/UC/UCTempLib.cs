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
using System.IO;
using Gvitech.CityMaker.FdeGeometry;
using System.Runtime.InteropServices;
using DFDataConfig.Class;
using DFCommon.Class;

namespace DF3DPipeCreateTool.UC
{
    public class UCTempLib : XtraUserControl
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
        private LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;

        private void InitializeComponent()
        {
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tePwd = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.buttonEditFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.labelControl1);
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
            // tePwd
            // 
            this.tePwd.Location = new System.Drawing.Point(51, 69);
            this.tePwd.Name = "tePwd";
            this.tePwd.Properties.PasswordChar = '*';
            this.tePwd.Size = new System.Drawing.Size(236, 22);
            this.tePwd.StyleController = this.layoutControl1;
            this.tePwd.TabIndex = 7;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = ((short)(1));
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "新建临时库"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "打开临时库")});
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
            this.layoutControlItem4,
            this.layoutControlItem5});
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
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 127);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(279, 140);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
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
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 121);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(168, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "注：临时库用于导入数据的管理";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 109);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(279, 18);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // UCTempLib
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCTempLib";
            this.Size = new System.Drawing.Size(299, 287);
            this.Load += new System.EventHandler(this.UCTempLib_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        public UCTempLib()
        {
            InitializeComponent();
        }

        private void UCTempLib_Load(object sender, EventArgs e)
        {
            string connStr = Config.GetConfigValue("3DTempDataConnStr");
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
                    CreateTempLib();
                    break;
                case 1:
                    OpenTempLib();
                    break;
            }
        }

        private bool InitTempLib(IDataSource ds)
        {
            if (ds == null)
            {
                return false;
            }
            IFieldInfoCollection fields = null;
            IFieldInfo newVal = null;
            ISpatialCRS spatialCRS = null;
            ITable o = null;
            IFeatureDataSet set = null;
            try
            {
                fields = new FieldInfoCollectionClass();
                newVal = new FieldInfoClass
                {
                    Name = "ID",
                    Alias = "编号",
                    FieldType = gviFieldType.gviFieldFID
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "LayerName",
                    Alias = "图层名称",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 50
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "DataSetName",
                    Alias = "数据集名称",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 100
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "FCName",
                    Alias = "要素类名称",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 100
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "FCGuid",
                    Alias = "要素类GUID",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 100
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "GeoType",
                    Alias = "空间列几何类型",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 50
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "RenderStyle",
                    Alias = "图层渲染样式",
                    FieldType = gviFieldType.gviFieldBlob
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "SourceFile",
                    Alias = "数据来源",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 150
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "GroupId",
                    Alias = "逻辑组ID",
                    FieldType = gviFieldType.gviFieldInt32
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "SourceType",
                    Alias = "数据来源类型",
                    FieldType = gviFieldType.gviFieldString,
                    Length = 50
                };
                fields.Add(newVal);
                newVal = new FieldInfoClass
                {
                    Name = "CreateDate",
                    Alias = "创建日期",
                    FieldType = gviFieldType.gviFieldDate
                };
                fields.Add(newVal);
                o = ds.CreateTable("Tb_TemporaryMgr", "ID", fields);
                if (o != null)
                {
                    IDbIndexInfo index = null;
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "LayerName")
                    };
                    index.AppendFieldDefine("LayerName", false);
                    o.AddDbIndex(index);
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "DatasetName")
                    };
                    index.AppendFieldDefine("DatasetName", false);
                    o.AddDbIndex(index);
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "FCName")
                    };
                    index.AppendFieldDefine("FCName", false);
                    o.AddDbIndex(index);
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "FCGuid")
                    };
                    index.AppendFieldDefine("FCGuid", false);
                    o.AddDbIndex(index);
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "GeoType")
                    };
                    index.AppendFieldDefine("GeoType", false);
                    o.AddDbIndex(index);
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "GroupId")
                    };
                    index.AppendFieldDefine("GroupId", false);
                    o.AddDbIndex(index);
                    index = new DbIndexInfoClass
                    {
                        Name = string.Format("{0}_{1}", "Tb_TemporaryMgr", "SourceType")
                    };
                    index.AppendFieldDefine("SourceType", false);
                    o.AddDbIndex(index);
                    Marshal.ReleaseComObject(o);
                }
                CRSFactory factory = new CRSFactoryClass();
                spatialCRS = factory.CreateFromWKT("UNKNOWNCS[\"unnamed\"]") as ISpatialCRS;
                set = ds.CreateFeatureDataset("FeatureDataSet", spatialCRS);
                if (set != null)
                {
                    Marshal.ReleaseComObject(set);
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private void CreateTempLib()
        {
            IDataSource ds = null;
            IDataSourceFactory factory = null;
            IConnectionInfo connectionInfo = null;
            try
            {
                if (DF3DPipeCreateApp.App.TempLib != null)
                {
                    DF3DPipeCreateApp.App.TempLib.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.TempLib);
                    DF3DPipeCreateApp.App.TempLib = null;
                }
                string fdbPath = this.buttonEditFilePath.Text.ToString().Trim();
                string pwd = this.tePwd.Text;
                if (File.Exists(fdbPath))
                {
                    File.Delete(fdbPath);
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
                    XtraMessageBox.Show("创建临时数据库失败！", "提示");
                    return;
                }
                if (InitTempLib(ds))
                {
                    DF3DPipeCreateApp.App.TempLib = ds;
                    XtraMessageBox.Show("创建临时数据库成功！", "提示");
                    Config.SetConfigValue("3DTempDataConnStr", connectionInfo.ToConnectionString());
                }
                else
                {
                    DF3DPipeCreateApp.App.TempLib = null;
                    XtraMessageBox.Show("创建临时数据库失败！", "提示");
                }
            }
            catch (Exception) { }
        }

        private void OpenTempLib()
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
                    if (DF3DPipeCreateApp.App.TempLib != null)
                    {
                        DF3DPipeCreateApp.App.TempLib.Close();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(DF3DPipeCreateApp.App.TempLib);
                        DF3DPipeCreateApp.App.TempLib = null;
                    }
                    DF3DPipeCreateApp.App.TempLib = dsf.OpenDataSource(connInfo);
                    if (DF3DPipeCreateApp.App.TempLib != null)
                    {
                        XtraMessageBox.Show("设置成功！", "提示");
                        Config.SetConfigValue("3DTempDataConnStr", connInfo.ToConnectionString());
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
