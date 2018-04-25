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
using ESRI.ArcGIS.Carto;
using DF2DTool.Class;

namespace DF2DTool.Frm
{
    public partial class FrmCoordConvert : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private TextEdit te_C3;
        private TextEdit te_A3;
        private TextEdit te_C2;
        private TextEdit te_B2;
        private TextEdit te_A2;
        private TextEdit te_C1;
        private TextEdit te_B1;
        private TextEdit te_A1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private SimpleButton btn_Cancel;
        private SimpleButton btn_Ok;
        private SimpleButton btn_mdb;
        private TextEdit te_MdbPath;
        private SimpleButton btn_SavePara;
        private SimpleButton btn_LoadPara;
        private CheckEdit ce_RotationNumAnno;
        private CheckEdit ce_RotationTextAnno;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        string m_Name;
        string m_path;
        IMap m_Map;
        bool IsRotationTextAnno;
        bool IsRotationNumAnno;
        double[] convertPara;
    
        public FrmCoordConvert()
        {
            InitializeComponent();
        }
        public FrmCoordConvert(IMap map)
        {
            this.m_Map = map;
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.btn_mdb = new DevExpress.XtraEditors.SimpleButton();
            this.te_MdbPath = new DevExpress.XtraEditors.TextEdit();
            this.btn_SavePara = new DevExpress.XtraEditors.SimpleButton();
            this.btn_LoadPara = new DevExpress.XtraEditors.SimpleButton();
            this.ce_RotationNumAnno = new DevExpress.XtraEditors.CheckEdit();
            this.ce_RotationTextAnno = new DevExpress.XtraEditors.CheckEdit();
            this.te_C3 = new DevExpress.XtraEditors.TextEdit();
            this.te_A3 = new DevExpress.XtraEditors.TextEdit();
            this.te_C2 = new DevExpress.XtraEditors.TextEdit();
            this.te_B2 = new DevExpress.XtraEditors.TextEdit();
            this.te_A2 = new DevExpress.XtraEditors.TextEdit();
            this.te_C1 = new DevExpress.XtraEditors.TextEdit();
            this.te_B1 = new DevExpress.XtraEditors.TextEdit();
            this.te_A1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te_MdbPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_RotationNumAnno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_RotationTextAnno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_C3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_A3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_C2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_B2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_A2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_C1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_B1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_A1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Cancel);
            this.layoutControl1.Controls.Add(this.btn_Ok);
            this.layoutControl1.Controls.Add(this.btn_mdb);
            this.layoutControl1.Controls.Add(this.te_MdbPath);
            this.layoutControl1.Controls.Add(this.btn_SavePara);
            this.layoutControl1.Controls.Add(this.btn_LoadPara);
            this.layoutControl1.Controls.Add(this.ce_RotationNumAnno);
            this.layoutControl1.Controls.Add(this.ce_RotationTextAnno);
            this.layoutControl1.Controls.Add(this.te_C3);
            this.layoutControl1.Controls.Add(this.te_A3);
            this.layoutControl1.Controls.Add(this.te_C2);
            this.layoutControl1.Controls.Add(this.te_B2);
            this.layoutControl1.Controls.Add(this.te_A2);
            this.layoutControl1.Controls.Add(this.te_C1);
            this.layoutControl1.Controls.Add(this.te_B1);
            this.layoutControl1.Controls.Add(this.te_A1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(360, 288);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(183, 248);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(162, 22);
            this.btn_Cancel.StyleController = this.layoutControl1;
            this.btn_Cancel.TabIndex = 20;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(15, 248);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(164, 22);
            this.btn_Ok.StyleController = this.layoutControl1;
            this.btn_Ok.TabIndex = 19;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_mdb
            // 
            this.btn_mdb.Location = new System.Drawing.Point(270, 214);
            this.btn_mdb.Name = "btn_mdb";
            this.btn_mdb.Size = new System.Drawing.Size(75, 24);
            this.btn_mdb.StyleController = this.layoutControl1;
            this.btn_mdb.TabIndex = 18;
            this.btn_mdb.Text = "...";
            this.btn_mdb.Click += new System.EventHandler(this.btn_mdb_Click);
            // 
            // te_MdbPath
            // 
            this.te_MdbPath.Location = new System.Drawing.Point(15, 214);
            this.te_MdbPath.Name = "te_MdbPath";
            this.te_MdbPath.Size = new System.Drawing.Size(251, 22);
            this.te_MdbPath.StyleController = this.layoutControl1;
            this.te_MdbPath.TabIndex = 17;
            // 
            // btn_SavePara
            // 
            this.btn_SavePara.Location = new System.Drawing.Point(181, 113);
            this.btn_SavePara.Name = "btn_SavePara";
            this.btn_SavePara.Size = new System.Drawing.Size(164, 22);
            this.btn_SavePara.StyleController = this.layoutControl1;
            this.btn_SavePara.TabIndex = 16;
            this.btn_SavePara.Text = "保存转换参数";
            this.btn_SavePara.Click += new System.EventHandler(this.btn_SavePara_Click);
            // 
            // btn_LoadPara
            // 
            this.btn_LoadPara.Location = new System.Drawing.Point(15, 113);
            this.btn_LoadPara.Name = "btn_LoadPara";
            this.btn_LoadPara.Size = new System.Drawing.Size(162, 22);
            this.btn_LoadPara.StyleController = this.layoutControl1;
            this.btn_LoadPara.TabIndex = 15;
            this.btn_LoadPara.Text = "读取转换参数";
            this.btn_LoadPara.Click += new System.EventHandler(this.btn_LoadPara_Click);
            // 
            // ce_RotationNumAnno
            // 
            this.ce_RotationNumAnno.Location = new System.Drawing.Point(169, 165);
            this.ce_RotationNumAnno.Name = "ce_RotationNumAnno";
            this.ce_RotationNumAnno.Properties.Caption = "旋转高程注记";
            this.ce_RotationNumAnno.Size = new System.Drawing.Size(176, 19);
            this.ce_RotationNumAnno.StyleController = this.layoutControl1;
            this.ce_RotationNumAnno.TabIndex = 14;
            // 
            // ce_RotationTextAnno
            // 
            this.ce_RotationTextAnno.Location = new System.Drawing.Point(15, 165);
            this.ce_RotationTextAnno.Name = "ce_RotationTextAnno";
            this.ce_RotationTextAnno.Properties.Caption = "旋转文字注记";
            this.ce_RotationTextAnno.Size = new System.Drawing.Size(150, 19);
            this.ce_RotationTextAnno.StyleController = this.layoutControl1;
            this.ce_RotationTextAnno.TabIndex = 13;
            // 
            // te_C3
            // 
            this.te_C3.Location = new System.Drawing.Point(145, 87);
            this.te_C3.Name = "te_C3";
            this.te_C3.Size = new System.Drawing.Size(82, 22);
            this.te_C3.StyleController = this.layoutControl1;
            this.te_C3.TabIndex = 11;
            // 
            // te_A3
            // 
            this.te_A3.Location = new System.Drawing.Point(37, 87);
            this.te_A3.Name = "te_A3";
            this.te_A3.Size = new System.Drawing.Size(82, 22);
            this.te_A3.StyleController = this.layoutControl1;
            this.te_A3.TabIndex = 10;
            // 
            // te_C2
            // 
            this.te_C2.Location = new System.Drawing.Point(253, 61);
            this.te_C2.Name = "te_C2";
            this.te_C2.Size = new System.Drawing.Size(92, 22);
            this.te_C2.StyleController = this.layoutControl1;
            this.te_C2.TabIndex = 9;
            // 
            // te_B2
            // 
            this.te_B2.Location = new System.Drawing.Point(145, 61);
            this.te_B2.Name = "te_B2";
            this.te_B2.Size = new System.Drawing.Size(82, 22);
            this.te_B2.StyleController = this.layoutControl1;
            this.te_B2.TabIndex = 8;
            // 
            // te_A2
            // 
            this.te_A2.Location = new System.Drawing.Point(37, 61);
            this.te_A2.Name = "te_A2";
            this.te_A2.Size = new System.Drawing.Size(82, 22);
            this.te_A2.StyleController = this.layoutControl1;
            this.te_A2.TabIndex = 7;
            // 
            // te_C1
            // 
            this.te_C1.Location = new System.Drawing.Point(253, 35);
            this.te_C1.Name = "te_C1";
            this.te_C1.Size = new System.Drawing.Size(92, 22);
            this.te_C1.StyleController = this.layoutControl1;
            this.te_C1.TabIndex = 6;
            // 
            // te_B1
            // 
            this.te_B1.Location = new System.Drawing.Point(145, 35);
            this.te_B1.Name = "te_B1";
            this.te_B1.Size = new System.Drawing.Size(82, 22);
            this.te_B1.StyleController = this.layoutControl1;
            this.te_B1.TabIndex = 5;
            // 
            // te_A1
            // 
            this.te_A1.Location = new System.Drawing.Point(37, 35);
            this.te_A1.Name = "te_A1";
            this.te_A1.Size = new System.Drawing.Size(82, 22);
            this.te_A1.StyleController = this.layoutControl1;
            this.te_A1.TabIndex = 4;
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
            this.layoutControlGroup5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(360, 288);
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
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem3,
            this.layoutControlItem12,
            this.layoutControlItem13});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(340, 130);
            this.layoutControlGroup2.Text = "转换参数";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.te_A1;
            this.layoutControlItem1.CustomizationFormText = "A1:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem1.Text = "A1:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.te_B1;
            this.layoutControlItem2.CustomizationFormText = "B1:";
            this.layoutControlItem2.Location = new System.Drawing.Point(108, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem2.Text = "B1:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.te_C1;
            this.layoutControlItem3.CustomizationFormText = "C1:";
            this.layoutControlItem3.Location = new System.Drawing.Point(216, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(118, 26);
            this.layoutControlItem3.Text = "C1:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.te_A2;
            this.layoutControlItem4.CustomizationFormText = "A2:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem4.Text = "A2:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.te_B2;
            this.layoutControlItem5.CustomizationFormText = "B2:";
            this.layoutControlItem5.Location = new System.Drawing.Point(108, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem5.Text = "B2:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.te_C2;
            this.layoutControlItem6.CustomizationFormText = "C2:";
            this.layoutControlItem6.Location = new System.Drawing.Point(216, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(118, 26);
            this.layoutControlItem6.Text = "C2:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.te_A3;
            this.layoutControlItem7.CustomizationFormText = "A3:";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem7.Text = "A3:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(19, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.te_C3;
            this.layoutControlItem8.CustomizationFormText = "C3:";
            this.layoutControlItem8.Location = new System.Drawing.Point(108, 52);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem8.Text = "C3:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(19, 14);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(216, 52);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(118, 26);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btn_LoadPara;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(166, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btn_SavePara;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(166, 78);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(168, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 130);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(340, 49);
            this.layoutControlGroup3.Text = "旋转注记";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.ce_RotationTextAnno;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(154, 23);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(154, 23);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(154, 23);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ce_RotationNumAnno;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(154, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(180, 23);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem14,
            this.layoutControlItem15});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 179);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(340, 54);
            this.layoutControlGroup4.Text = "新生成地理数据库";
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.te_MdbPath;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(255, 28);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.btn_mdb;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(255, 0);
            this.layoutControlItem15.MaxSize = new System.Drawing.Size(79, 28);
            this.layoutControlItem15.MinSize = new System.Drawing.Size(79, 28);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(79, 28);
            this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem16,
            this.layoutControlItem17});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 233);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(340, 35);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.btn_Ok;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(168, 26);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(168, 26);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(168, 29);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.btn_Cancel;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(168, 0);
            this.layoutControlItem17.MaxSize = new System.Drawing.Size(166, 26);
            this.layoutControlItem17.MinSize = new System.Drawing.Size(166, 26);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(166, 29);
            this.layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // FrmCoordConvert
            // 
            this.ClientSize = new System.Drawing.Size(360, 288);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmCoordConvert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "坐标系转换";
            this.Load += new System.EventHandler(this.FrmCoordConvert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te_MdbPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_RotationNumAnno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_RotationTextAnno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_C3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_A3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_C2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_B2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_A2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_C1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_B1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_A1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            this.ResumeLayout(false);

        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.te_A1.Text == "" || this.te_B1.Text == "" || this.te_C1.Text == "" ||
                    this.te_A2.Text == "" || this.te_B2.Text == "" || this.te_C2.Text == "" ||
                    this.te_A3.Text == "" || this.te_C3.Text == "")
                {
                    XtraMessageBox.Show("请填写转换参数", "坐标转换");
                    return;
                }
                else
                {
                    convertPara = new double[8];
                    double para = Double.MaxValue;
                    if (Transfer(te_A1, ref para)) { convertPara[0] = para; } else { return; }
                    if (Transfer(te_B1, ref para)) { convertPara[1] = para; } else { return; }
                    if (Transfer(te_C1, ref para)) { convertPara[2] = para; } else { return; }
                    if (Transfer(te_A2, ref para)) { convertPara[3] = para; } else { return; }
                    if (Transfer(te_B2, ref para)) { convertPara[4] = para; } else { return; }
                    if (Transfer(te_C2, ref para)) { convertPara[5] = para; } else { return; }
                    if (Transfer(te_A3, ref para)) { convertPara[6] = para; } else { return; }
                    if (Transfer(te_C3, ref para)) { convertPara[7] = para; } else { return; }
 
                }
               

                if (te_MdbPath.Text == "")
                {
                    XtraMessageBox.Show("请指定文件存放路径", "坐标转换");
                }
                else
                {
                    m_Name = System.IO.Path.GetFileName(te_MdbPath.Text);
                    if (m_Name.Length > 4)
                    {
                        if (m_Name.Substring(m_Name.Length - 4) == ".mdb")
                        {
                            m_path = System.IO.Path.GetDirectoryName(te_MdbPath.Text);
                        }
                    }
                    else
                    {
                        m_path = "";
                    }

                                      
                }
                IsRotationTextAnno = ce_RotationTextAnno.Checked;
                IsRotationNumAnno = ce_RotationNumAnno.Checked;
                if (m_path == "") { XtraMessageBox.Show("请重新指定文件存放路径", "坐标转换"); return; }
                CoordConvert coordConvert = new CoordConvert(convertPara, m_path,m_Name, IsRotationTextAnno, IsRotationNumAnno, m_Map);
                coordConvert.CoordConvertMap();
                XtraMessageBox.Show("坐标系转换完成！", "提示");
                btn_Cancel_Click(null, null);
               
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示");
                return;
            }
        }
        private bool Transfer(TextEdit te,ref double para)
        {
            if (!Double.TryParse(te.Text, out para))
            {
                XtraMessageBox.Show(te.Name.Substring(te.Name.Length - 2) + "参数格式错误", "提示");
                return false;
            }
            else
            {
                return true;
            }
            
        }

        private void FrmCoordConvert_Load(object sender, EventArgs e)
        {
            string localPath = SystemInfo.Instance.LocalDataPath;
            string toolPath = localPath + "Tool\\";
            if (!Directory.Exists(toolPath))
            {
                Directory.CreateDirectory(toolPath);
            }
            if (!Directory.Exists(toolPath)) return;
            string coordConvertPath = Path.Combine(toolPath, "CoordConvert.xml");
            if (File.Exists(coordConvertPath))
            {
                LoadParaments(coordConvertPath);
            }

        }

        private void btn_LoadPara_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.RestoreDirectory = true;
                ofd.Filter = "Xml Files|*.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadParaments(ofd.FileName.ToString());
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_SavePara_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.RestoreDirectory = true;
                sfd.Filter = "Xml Files|*.xml";
                sfd.DefaultExt = "xml";
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName.ToString();
                    XmlNode root = doc.CreateElement("CoordConvert");
                    doc.AppendChild(root);
                    XmlElement element = doc.CreateElement("Parament");
                    element.SetAttribute("A1", this.te_A1.Text);
                    element.SetAttribute("B1", this.te_B1.Text);
                    element.SetAttribute("C1", this.te_C1.Text);
                    element.SetAttribute("A2", this.te_A2.Text);
                    element.SetAttribute("B2", this.te_B2.Text);
                    element.SetAttribute("C2", this.te_C2.Text);
                    element.SetAttribute("A3", this.te_A3.Text);
                    element.SetAttribute("C3", this.te_C3.Text);
                    root.AppendChild(element);
                    doc.Save(path);                  
                }
                //在本地数据目录下再存一份参数配置
                string localPath = SystemInfo.Instance.LocalDataPath;
                string toolPath = localPath + "Tool\\";
                if (!Directory.Exists(toolPath))
                {
                    Directory.CreateDirectory(toolPath);
                }
                if (!Directory.Exists(toolPath)) return;
                string coordConvertPath = Path.Combine(toolPath, "CoordConvert.xml");
                if (File.Exists(coordConvertPath))
                {
                    File.Delete(coordConvertPath);
                    doc.Save(coordConvertPath);

                }
                else
                {
                    doc.Save(coordConvertPath);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_mdb_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Filter = "Access|*.mdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.te_MdbPath.Text = ofd.FileName.ToString();
            }
        }
        private void LoadParaments(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode root = doc.SelectSingleNode("CoordConvert");
            if (root == null)
            { XtraMessageBox.Show("所选文件无效，请重新选择", "提示"); return; }
            XmlElement element = root.FirstChild as XmlElement;
            if (element.HasAttributes)
            {
                this.te_A1.Text = element.GetAttribute("A1");
                this.te_B1.Text = element.GetAttribute("B1");
                this.te_C1.Text = element.GetAttribute("C1");
                this.te_A2.Text = element.GetAttribute("A2");
                this.te_B2.Text = element.GetAttribute("B2");
                this.te_C2.Text = element.GetAttribute("C2");
                this.te_A3.Text = element.GetAttribute("A3");
                this.te_C3.Text = element.GetAttribute("C3");
            }
                    
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


    }
}
