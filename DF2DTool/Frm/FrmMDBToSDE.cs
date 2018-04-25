using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using DF2DTool.Class;
using ESRI.ArcGIS.Geometry;

namespace DF2DTool.Frm
{
    public partial class FrmMDBToSDE : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit teSurveyUnit;
        private DevExpress.XtraEditors.TextEdit teUpdateUnit;
        private DevExpress.XtraEditors.TextEdit teUpdater;
        private DevExpress.XtraEditors.TextEdit teProjectID;
        private DevExpress.XtraEditors.SimpleButton btnMDB;
        private DevExpress.XtraEditors.TextEdit teMDB;
        private DevExpress.XtraEditors.SimpleButton btnSDE;
        private DevExpress.XtraEditors.TextEdit teSDE;
        private DevExpress.XtraEditors.CheckEdit ceSelOtherSDE;
        private DevExpress.XtraEditors.CheckEdit ceSelCurSDE;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit teSurveyTime;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit teUpdateTime;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;

        private IWorkspace m_pSDEWorkspace = null;
        private IWorkspace m_pMdbWorkspace = null;
        public static string filePath;
        public static IArray pArrErrorMdb = new ArrayClass();
        public static IArray pArrErrorFeature = new ArrayClass();
        public static IArray pArrErrorFeatureClass = new ArrayClass();
        public static IArray pArrNoFeatureClass = new ArrayClass();
        public static IArray pArrMdbNoFeatureClass = new ArrayClass();
    
        public FrmMDBToSDE()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.teSurveyTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.teUpdateTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.teSurveyUnit = new DevExpress.XtraEditors.TextEdit();
            this.teUpdateUnit = new DevExpress.XtraEditors.TextEdit();
            this.teUpdater = new DevExpress.XtraEditors.TextEdit();
            this.teProjectID = new DevExpress.XtraEditors.TextEdit();
            this.btnMDB = new DevExpress.XtraEditors.SimpleButton();
            this.teMDB = new DevExpress.XtraEditors.TextEdit();
            this.btnSDE = new DevExpress.XtraEditors.SimpleButton();
            this.teSDE = new DevExpress.XtraEditors.TextEdit();
            this.ceSelOtherSDE = new DevExpress.XtraEditors.CheckEdit();
            this.ceSelCurSDE = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teSurveyTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSurveyUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdateUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdater.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProjectID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSDE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelOtherSDE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelCurSDE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl5);
            this.layoutControl1.Controls.Add(this.teSurveyTime);
            this.layoutControl1.Controls.Add(this.labelControl4);
            this.layoutControl1.Controls.Add(this.teUpdateTime);
            this.layoutControl1.Controls.Add(this.labelControl6);
            this.layoutControl1.Controls.Add(this.labelControl3);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.teSurveyUnit);
            this.layoutControl1.Controls.Add(this.teUpdateUnit);
            this.layoutControl1.Controls.Add(this.teUpdater);
            this.layoutControl1.Controls.Add(this.teProjectID);
            this.layoutControl1.Controls.Add(this.btnMDB);
            this.layoutControl1.Controls.Add(this.teMDB);
            this.layoutControl1.Controls.Add(this.btnSDE);
            this.layoutControl1.Controls.Add(this.teSDE);
            this.layoutControl1.Controls.Add(this.ceSelOtherSDE);
            this.layoutControl1.Controls.Add(this.ceSelCurSDE);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(452, 281);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(238, 227);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.StyleController = this.layoutControl1;
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "测量时间：";
            // 
            // teSurveyTime
            // 
            this.teSurveyTime.Enabled = false;
            this.teSurveyTime.Location = new System.Drawing.Point(302, 227);
            this.teSurveyTime.Name = "teSurveyTime";
            this.teSurveyTime.Size = new System.Drawing.Size(145, 22);
            this.teSurveyTime.StyleController = this.layoutControl1;
            this.teSurveyTime.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(237, 201);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.StyleController = this.layoutControl1;
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "入库时间：";
            // 
            // teUpdateTime
            // 
            this.teUpdateTime.Enabled = false;
            this.teUpdateTime.Location = new System.Drawing.Point(301, 201);
            this.teUpdateTime.Name = "teUpdateTime";
            this.teUpdateTime.Size = new System.Drawing.Size(146, 22);
            this.teUpdateTime.StyleController = this.layoutControl1;
            this.teUpdateTime.TabIndex = 22;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 227);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.StyleController = this.layoutControl1;
            this.labelControl6.TabIndex = 21;
            this.labelControl6.Text = "测量单位：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 201);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.StyleController = this.layoutControl1;
            this.labelControl3.TabIndex = 18;
            this.labelControl3.Text = "入库单位：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(237, 175);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "入库人员：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 175);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "项目名称：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(228, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(222, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(2, 256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(222, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // teSurveyUnit
            // 
            this.teSurveyUnit.Location = new System.Drawing.Point(69, 227);
            this.teSurveyUnit.Name = "teSurveyUnit";
            this.teSurveyUnit.Size = new System.Drawing.Size(165, 22);
            this.teSurveyUnit.StyleController = this.layoutControl1;
            this.teSurveyUnit.TabIndex = 13;
            // 
            // teUpdateUnit
            // 
            this.teUpdateUnit.Location = new System.Drawing.Point(69, 201);
            this.teUpdateUnit.Name = "teUpdateUnit";
            this.teUpdateUnit.Size = new System.Drawing.Size(164, 22);
            this.teUpdateUnit.StyleController = this.layoutControl1;
            this.teUpdateUnit.TabIndex = 12;
            // 
            // teUpdater
            // 
            this.teUpdater.Location = new System.Drawing.Point(301, 175);
            this.teUpdater.Name = "teUpdater";
            this.teUpdater.Size = new System.Drawing.Size(146, 22);
            this.teUpdater.StyleController = this.layoutControl1;
            this.teUpdater.TabIndex = 11;
            // 
            // teProjectID
            // 
            this.teProjectID.Location = new System.Drawing.Point(69, 175);
            this.teProjectID.Name = "teProjectID";
            this.teProjectID.Size = new System.Drawing.Size(164, 22);
            this.teProjectID.StyleController = this.layoutControl1;
            this.teProjectID.TabIndex = 10;
            // 
            // btnMDB
            // 
            this.btnMDB.Location = new System.Drawing.Point(302, 123);
            this.btnMDB.Name = "btnMDB";
            this.btnMDB.Size = new System.Drawing.Size(145, 22);
            this.btnMDB.StyleController = this.layoutControl1;
            this.btnMDB.TabIndex = 9;
            this.btnMDB.Text = "选择修测数据库MDB";
            this.btnMDB.Click += new System.EventHandler(this.btnMDB_Click);
            // 
            // teMDB
            // 
            this.teMDB.EditValue = "尚未选择修测数据库";
            this.teMDB.Location = new System.Drawing.Point(5, 123);
            this.teMDB.Name = "teMDB";
            this.teMDB.Size = new System.Drawing.Size(293, 22);
            this.teMDB.StyleController = this.layoutControl1;
            this.teMDB.TabIndex = 8;
            // 
            // btnSDE
            // 
            this.btnSDE.Location = new System.Drawing.Point(303, 71);
            this.btnSDE.Name = "btnSDE";
            this.btnSDE.Size = new System.Drawing.Size(144, 22);
            this.btnSDE.StyleController = this.layoutControl1;
            this.btnSDE.TabIndex = 7;
            this.btnSDE.Text = "选择目标数据库SDE";
            this.btnSDE.Click += new System.EventHandler(this.btnSDE_Click);
            // 
            // teSDE
            // 
            this.teSDE.EditValue = "尚未连接SDE网络数据库";
            this.teSDE.Location = new System.Drawing.Point(5, 71);
            this.teSDE.Name = "teSDE";
            this.teSDE.Size = new System.Drawing.Size(294, 22);
            this.teSDE.StyleController = this.layoutControl1;
            this.teSDE.TabIndex = 6;
            // 
            // ceSelOtherSDE
            // 
            this.ceSelOtherSDE.Location = new System.Drawing.Point(5, 48);
            this.ceSelOtherSDE.Name = "ceSelOtherSDE";
            this.ceSelOtherSDE.Properties.Caption = "选择SDE数据库";
            this.ceSelOtherSDE.Size = new System.Drawing.Size(442, 19);
            this.ceSelOtherSDE.StyleController = this.layoutControl1;
            this.ceSelOtherSDE.TabIndex = 5;
            this.ceSelOtherSDE.CheckedChanged += new System.EventHandler(this.ceSelOtherSDE_CheckedChanged);
            // 
            // ceSelCurSDE
            // 
            this.ceSelCurSDE.Location = new System.Drawing.Point(5, 25);
            this.ceSelCurSDE.Name = "ceSelCurSDE";
            this.ceSelCurSDE.Properties.Caption = "当前工作空间对应的数据库";
            this.ceSelCurSDE.Size = new System.Drawing.Size(442, 19);
            this.ceSelCurSDE.StyleController = this.layoutControl1;
            this.ceSelCurSDE.TabIndex = 4;
            this.ceSelCurSDE.CheckedChanged += new System.EventHandler(this.ceSelCurSDE_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(452, 281);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "目标数据库SDE";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(452, 98);
            this.layoutControlGroup2.Text = "目标数据库SDE";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ceSelCurSDE;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(446, 23);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ceSelOtherSDE;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(446, 23);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teSDE;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(298, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSDE;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(298, 46);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(148, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "修测数据库MDB";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 98);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(452, 52);
            this.layoutControlGroup3.Text = "修测数据库MDB";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teMDB;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(297, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnMDB;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(297, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(149, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "更新信息";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem18,
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem19,
            this.layoutControlItem20});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 150);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(452, 104);
            this.layoutControlGroup4.Text = "更新信息";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.teProjectID;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(64, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(168, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teUpdater;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(296, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(150, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teUpdateUnit;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(64, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(168, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.teSurveyUnit;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(64, 52);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(169, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.labelControl1;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.labelControl2;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(232, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.labelControl3;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.labelControl6;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem18.Text = "layoutControlItem18";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.teUpdateTime;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(296, 26);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(150, 26);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.labelControl4;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(232, 26);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.teSurveyTime;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(297, 52);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(149, 26);
            this.layoutControlItem19.Text = "layoutControlItem19";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.labelControl5;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(233, 52);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnOK;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 254);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(226, 27);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btnCancel;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(226, 254);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(226, 27);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // FrmMDBToSDE
            // 
            this.ClientSize = new System.Drawing.Size(452, 281);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMDBToSDE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修测数据MDB导入SDE";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teSurveyTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSurveyUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdateUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUpdater.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teProjectID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teMDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teSDE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelOtherSDE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelCurSDE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            this.ResumeLayout(false);

        }

        public FrmMDBToSDE(IWorkspace pWs)
        {
            InitializeComponent();
            if (pWs != null)
            {
                this.ceSelCurSDE.Checked = true;
                this.teSDE.Enabled = false;
                this.btnSDE.Enabled = false;
                m_pSDEWorkspace = pWs;
            }
            else
            {
                this.ceSelOtherSDE.Checked = true;
                this.ceSelCurSDE.Enabled = false;
            }
        }

        private void btnSDE_Click(object sender, EventArgs e)
        {
            FrmConnect connect = new FrmConnect();
            if (connect.ShowDialog() == DialogResult.OK)
            {
                m_pSDEWorkspace = GetWorkspace(connect.ConnPropertySet);

                if (m_pSDEWorkspace != null)
                {
                    this.teSDE.Text = "连接SDE数据库成功。";
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

        private void ceSelOtherSDE_CheckedChanged(object sender, EventArgs e)
        {
            if (ceSelOtherSDE.Checked)
            {
                ceSelCurSDE.Checked = false;
                teSDE.Enabled = true;
                btnSDE.Enabled = true;
            }
            else
            {
                ceSelCurSDE.Checked = true;
                teSDE.Enabled = false;
                btnSDE.Enabled = false;
            }
        }

        private void ceSelCurSDE_CheckedChanged(object sender, EventArgs e)
        {
            if (ceSelCurSDE.Checked)
            {
                ceSelOtherSDE.Checked = false;
                teSDE.Enabled = false;
                btnSDE.Enabled = true;
            }
            else
            {
                ceSelOtherSDE.Checked = true;
                teSDE.Enabled = true;
                btnSDE.Enabled = true;
            }
        }

        private void btnMDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "mdb";
            dlg.Filter = "修测数据库(*.mdb)|*.mdb||";
            dlg.Title = "请选择修测数据库MDB";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                teMDB.Text = dlg.FileName; ;
            }

            if (teMDB.Text == teSDE.Text)
            {
                MessageBox.Show("修测数据库与目标数据库相同，请重新选择!", "修测数据库选择错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                teMDB.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int iFCCount;
            int iCount;
            int iProBarCount = 0;

            string strSDEFCName = null;
            string strMDBFCName = null;

            IFeatureClass pFeatureClassSDE = null;
            IFeatureClass pFeatureClassMDB = null;
            string strSDEFile = m_pSDEWorkspace.PathName;
            string strMDBFile = teMDB.Text;
            IWorkspaceFactory pGDBWorkspcaeFactory = new AccessWorkspaceFactoryClass();
            try
            {
                m_pMdbWorkspace = pGDBWorkspcaeFactory.OpenFromFile(strMDBFile, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            IArray pArraySDEFCName = new ArrayClass(); //目标SDE数据库
            IArray pArrayMDBFCName = new ArrayClass(); //修测MDB数据库
            pArraySDEFCName = Utility.GetFeactureClassName_From_AccessWorkSpace(m_pSDEWorkspace);//获得目标SDE数据库的要素类
            pArrayMDBFCName = Utility.GetFeactureClassName_From_AccessWorkSpace(m_pMdbWorkspace);//获得修测MDB数据库的要素类

            //计算prBarFeactureClass进度条的最大值
            //iProBarCount = Utility.GetFeactureClassName_From_AccessWorkSpace(m_pMdbWorkspace).Count;
            //prBarFeactureClass.Maximum = iProBarCount;

            //遍历修测数据库中的FeatureClass，在目标数据库中寻找与之匹配的FeatureClass，并进行合并
            for (iFCCount = 0; iFCCount < pArrayMDBFCName.Count; iFCCount++)
            {
                //Console.WriteLine(iFCCount.ToString());
                strMDBFCName = (string)pArrayMDBFCName.get_Element(iFCCount);
                try
                {
                    pFeatureClassMDB = (m_pMdbWorkspace as IFeatureWorkspace).OpenFeatureClass(strMDBFCName);  //打开目标数据库中的要素类
                }
                catch
                {
                    MessageBox.Show("无法打开修测数据库中的要素类:" + strMDBFCName + "!");
                    return;
                }
                //如果为简单要素类或注记类，则合并
                if (pFeatureClassMDB.FeatureType == esriFeatureType.esriFTSimple || pFeatureClassMDB.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    for (iCount = 0; iCount < pArraySDEFCName.Count; iCount++)//遍历目标数据库的要素类
                    {
                        strSDEFCName = (string)pArraySDEFCName.get_Element(iCount);
                        int index = strSDEFCName.LastIndexOf('.');
                        string sdeTemp = strSDEFCName.Substring(index + 1);
                        if (sdeTemp == strMDBFCName.ToUpper())  //如果找到了
                        {
                            try
                            {
                                pFeatureClassSDE = (m_pSDEWorkspace as IFeatureWorkspace).OpenFeatureClass(strSDEFCName);  //打开修测数据库中的要素类
                            }
                            catch
                            {
                                MessageBox.Show("无法打开待合并数据库中的要素类！");
                            }

                            LoadFeatures(pFeatureClassSDE, pFeatureClassMDB, this.teMDB.Text);

                            break;
                        }

                    }
                    if (iCount == pArraySDEFCName.Count)//如果找到最后还是没有找到
                    {
                        pArrMdbNoFeatureClass.Add(strSDEFile);
                        pArrNoFeatureClass.Add(strMDBFCName);
                    }

                }
                //prBarFeactureClass.PerformStep();
            }
            //设置错误信息窗体
            string strErrorInfo = this.GetErrorInfo();
            pArrMdbNoFeatureClass.RemoveAll();
            pArrNoFeatureClass.RemoveAll();

            string strlblInfo = "导入完成!";

            FrmErrorInfo formMDBToMDBInfo = new FrmErrorInfo(strErrorInfo, strlblInfo);
            formMDBToMDBInfo.Text = "修测数据库导入目标数据库信息";
            formMDBToMDBInfo.ShowDialog();

            this.Close();
            this.Dispose();
        }

        #region//为要素添填充字段
        /// <summary>
        /// 为要素添填充属性
        /// </summary>
        /// <param name="featureBuffer">要素缓存</param>
        /// <param name="feature">待填充的要素</param>
        private static void AddFields(IFeatureBuffer featureBuffer, IFeature feature)
        {
            IRowBuffer rowBuffer = (IRowBuffer)featureBuffer;
            IFields fieldsNew = rowBuffer.Fields;

            int i;
            int intFieldIndex;
            IFields fields = feature.Fields;
            IField field;

            for (i = 0; i < fieldsNew.FieldCount; i++)
            {
                field = fieldsNew.get_Field(i);
                //				if (field.Editable == true && (field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeOID) 
                //					&& (field.Name != "SHAPE_Length") && (field.Name != "SHAPE_Area"))
                if (field.Editable == true)
                {
                    intFieldIndex = feature.Fields.FindField(field.Name);
                    if (intFieldIndex != -1)
                    {
                        featureBuffer.set_Value(i, feature.get_Value(intFieldIndex));
                    }
                }

            }
        }
        #endregion

        #region//使用insert cursor插入要素
        /// <summary>
        /// 使用insert cursor插入要素
        /// </summary>
        /// <param name="string pObjectFeatureClass">目标数据库的要素类</param>
        /// <param name="string pSourceFeatureClass">修测数据库的要素类</param>
        private static void LoadFeatures(IFeatureClass pObjectFeatureClass, IFeatureClass pSourceFeatureClass, string strMergedb)
        {
            Console.WriteLine(pObjectFeatureClass.AliasName);
            int intFeatureCount = 0;

            IFeatureCursor featureCursorInsert = pObjectFeatureClass.Insert(true);
            IFeatureBuffer featureBufferInsert = pObjectFeatureClass.CreateFeatureBuffer();

            //遍历待合并数据库要素类中的所有要素
            IFeatureCursor featureCursorSearch = pSourceFeatureClass.Search(null, true);
            IFeature feature = featureCursorSearch.NextFeature();
            while (feature != null)
            {
                try
                {
                    featureBufferInsert.Shape = feature.Shape;
                }
                catch
                {

                    try
                    {
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon || feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            IPointCollection pPointC;
                            IPoint pPoint;
                            pPointC = (IPointCollection)feature.Shape;
                            for (int i = 0; i < pPointC.PointCount; i++)
                            {
                                pPoint = pPointC.get_Point(i);
                                pPoint.M = 0;
                                pPointC.UpdatePoint(i, pPoint);
                            }
                            feature.Shape = (IGeometry)pPointC;
                            featureBufferInsert.Shape = feature.Shape;
                        }
                    }
                    catch
                    {
                        string strFeatureID = feature.OID.ToString();
                        pArrErrorFeatureClass.Add(pObjectFeatureClass.AliasName);
                        pArrErrorFeature.Add(strFeatureID);
                        pArrErrorMdb.Add(strMergedb);

                        //指向下一个要素
                        feature = featureCursorSearch.NextFeature();
                        continue;
                    }

                }

                AddFields(featureBufferInsert, feature);

                featureCursorInsert.InsertFeature(featureBufferInsert);

                // 每添加100个要素刷新一次feature Cursor
                if (++intFeatureCount == 100)
                {
                    featureCursorInsert.Flush();
                    intFeatureCount = 0;
                }

                // 指向下一个要素
                feature = featureCursorSearch.NextFeature();
            }

            // 刷新feature Cursor
            featureCursorInsert.Flush();
        }
        #endregion

        #region//获得错误信息
        private string GetErrorInfo()
        {
            string strDetail = null;
            strDetail = ">>修测数据";
            strDetail += teMDB.Text + "\r\n";

            strDetail += "已导入到数据库" + filePath;
            strDetail += "\r\n\r\n";

            if (pArrNoFeatureClass.Count != 0)
            {
                strDetail += ">>错误信息:\r\n";
                strDetail += "下列要素类匹配失败\r\n";
                for (int j = 0; j < pArrNoFeatureClass.Count; j++)
                {
                    strDetail += "数据库";
                    strDetail += pArrMdbNoFeatureClass.get_Element(j).ToString();
                    strDetail += "中不存在要素类";
                    strDetail += pArrNoFeatureClass.get_Element(j).ToString();
                    strDetail += "\r\n";
                }
            }
            strDetail += "\r\n\r\n";

            if (pArrErrorFeature.Count != 0)
            {
                strDetail += "下列要素导入失败\r\n";
                for (int j = 0; j < pArrErrorFeature.Count; j++)
                {
                    strDetail += "数据库";
                    strDetail += pArrErrorMdb.get_Element(j).ToString();
                    strDetail += "中要素类";
                    strDetail += pArrErrorFeatureClass.get_Element(j).ToString();
                    strDetail += "中ObjectID为:";
                    strDetail += pArrErrorFeature.get_Element(j).ToString();
                    strDetail += "\r\n";
                }

            }
            return strDetail;
        }
        #endregion

     

    }
}
