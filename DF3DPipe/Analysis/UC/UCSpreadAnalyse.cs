using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.FdeGeometry;
using DF3DDraw;
using Gvitech.CityMaker.RenderControl;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using System.IO;
using DFDataConfig.Logic;
using Gvitech.CityMaker.FdeCore;
using DFCommon.Class;
using DF3DData.Class;
using DFDataConfig.Class;
using DF3DPipe.Analysis.Class;
using Gvitech.CityMaker.Controls;

namespace DF3DPipe.Analysis.UC
{
    public class UCSpreadAnalyse : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl1;
        private TextEdit teTimeSpend;
        private TextEdit teTimeCurrent;
        private TextEdit teTimeStart;
        private TextEdit teCarrierType;
        private ComboBoxEdit cbeWindDirection;
        private SpinEdit spWindSpeed;
        private SpinEdit spLeekSpeed;
        private SimpleButton btnStop;
        private SimpleButton btnPause;
        private SimpleButton btnStart;
        private SimpleButton btnSetLeak;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Timer timer1;
        private IContainer components;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private CheckEdit ceFlyToRes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private AxRenderControl d3;
        public UCSpreadAnalyse()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null)
            {
                this.Enabled = false;
                return;
            }
            d3 = app.Current3DMapControl;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceFlyToRes = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.teTimeSpend = new DevExpress.XtraEditors.TextEdit();
            this.teTimeCurrent = new DevExpress.XtraEditors.TextEdit();
            this.teTimeStart = new DevExpress.XtraEditors.TextEdit();
            this.teCarrierType = new DevExpress.XtraEditors.TextEdit();
            this.cbeWindDirection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spWindSpeed = new DevExpress.XtraEditors.SpinEdit();
            this.spLeekSpeed = new DevExpress.XtraEditors.SpinEdit();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.btnPause = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetLeak = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceFlyToRes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTimeSpend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTimeCurrent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTimeStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCarrierType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeWindDirection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWindSpeed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spLeekSpeed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceFlyToRes);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.teTimeSpend);
            this.layoutControl1.Controls.Add(this.teTimeCurrent);
            this.layoutControl1.Controls.Add(this.teTimeStart);
            this.layoutControl1.Controls.Add(this.teCarrierType);
            this.layoutControl1.Controls.Add(this.cbeWindDirection);
            this.layoutControl1.Controls.Add(this.spWindSpeed);
            this.layoutControl1.Controls.Add(this.spLeekSpeed);
            this.layoutControl1.Controls.Add(this.btnStop);
            this.layoutControl1.Controls.Add(this.btnPause);
            this.layoutControl1.Controls.Add(this.btnStart);
            this.layoutControl1.Controls.Add(this.btnSetLeak);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(252, 422);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceFlyToRes
            // 
            this.ceFlyToRes.Location = new System.Drawing.Point(2, 324);
            this.ceFlyToRes.Name = "ceFlyToRes";
            this.ceFlyToRes.Properties.Caption = "动态查看模拟效果";
            this.ceFlyToRes.Size = new System.Drawing.Size(248, 19);
            this.ceFlyToRes.StyleController = this.layoutControl1;
            this.ceFlyToRes.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(212, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 14);
            this.labelControl2.StyleController = this.layoutControl1;
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "m³/s";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(218, 86);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(20, 14);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "m/s";
            // 
            // teTimeSpend
            // 
            this.teTimeSpend.Enabled = false;
            this.teTimeSpend.Location = new System.Drawing.Point(69, 260);
            this.teTimeSpend.Name = "teTimeSpend";
            this.teTimeSpend.Size = new System.Drawing.Size(169, 22);
            this.teTimeSpend.StyleController = this.layoutControl1;
            this.teTimeSpend.TabIndex = 7;
            // 
            // teTimeCurrent
            // 
            this.teTimeCurrent.Enabled = false;
            this.teTimeCurrent.Location = new System.Drawing.Point(69, 234);
            this.teTimeCurrent.Name = "teTimeCurrent";
            this.teTimeCurrent.Size = new System.Drawing.Size(169, 22);
            this.teTimeCurrent.StyleController = this.layoutControl1;
            this.teTimeCurrent.TabIndex = 6;
            // 
            // teTimeStart
            // 
            this.teTimeStart.Enabled = false;
            this.teTimeStart.Location = new System.Drawing.Point(69, 208);
            this.teTimeStart.Name = "teTimeStart";
            this.teTimeStart.Size = new System.Drawing.Size(169, 22);
            this.teTimeStart.StyleController = this.layoutControl1;
            this.teTimeStart.TabIndex = 5;
            // 
            // teCarrierType
            // 
            this.teCarrierType.Enabled = false;
            this.teCarrierType.Location = new System.Drawing.Point(69, 138);
            this.teCarrierType.Name = "teCarrierType";
            this.teCarrierType.Size = new System.Drawing.Size(169, 22);
            this.teCarrierType.StyleController = this.layoutControl1;
            this.teCarrierType.TabIndex = 4;
            // 
            // cbeWindDirection
            // 
            this.cbeWindDirection.EditValue = "南风";
            this.cbeWindDirection.Location = new System.Drawing.Point(69, 112);
            this.cbeWindDirection.Name = "cbeWindDirection";
            this.cbeWindDirection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeWindDirection.Properties.Items.AddRange(new object[] {
            "南风",
            "西南风",
            "西风",
            "西北风",
            "北风",
            "东北风",
            "东风",
            "东南风"});
            this.cbeWindDirection.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbeWindDirection.Size = new System.Drawing.Size(169, 22);
            this.cbeWindDirection.StyleController = this.layoutControl1;
            this.cbeWindDirection.TabIndex = 3;
            // 
            // spWindSpeed
            // 
            this.spWindSpeed.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.spWindSpeed.Location = new System.Drawing.Point(69, 86);
            this.spWindSpeed.Name = "spWindSpeed";
            this.spWindSpeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spWindSpeed.Size = new System.Drawing.Size(145, 22);
            this.spWindSpeed.StyleController = this.layoutControl1;
            this.spWindSpeed.TabIndex = 2;
            // 
            // spLeekSpeed
            // 
            this.spLeekSpeed.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spLeekSpeed.Location = new System.Drawing.Point(69, 60);
            this.spLeekSpeed.Name = "spLeekSpeed";
            this.spLeekSpeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spLeekSpeed.Size = new System.Drawing.Size(139, 22);
            this.spLeekSpeed.StyleController = this.layoutControl1;
            this.spLeekSpeed.TabIndex = 1;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(166, 298);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(84, 22);
            this.btnStop.StyleController = this.layoutControl1;
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "停止分析";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(83, 298);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(79, 22);
            this.btnPause.StyleController = this.layoutControl1;
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "暂停分析";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(2, 298);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(77, 22);
            this.btnStart.StyleController = this.layoutControl1;
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "开始分析";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSetLeak
            // 
            this.btnSetLeak.Location = new System.Drawing.Point(2, 2);
            this.btnSetLeak.Name = "btnSetLeak";
            this.btnSetLeak.Size = new System.Drawing.Size(248, 22);
            this.btnSetLeak.StyleController = this.layoutControl1;
            this.btnSetLeak.TabIndex = 0;
            this.btnSetLeak.Text = "鼠标点击管线设置泄漏点";
            this.btnSetLeak.Click += new System.EventHandler(this.btnSetLeak_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup2,
            this.emptySpaceItem4,
            this.layoutControlGroup3,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem14});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(252, 422);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSetLeak;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(252, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "参数设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem12,
            this.layoutControlItem13});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(252, 148);
            this.layoutControlGroup2.Text = "参数设置";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spLeekSpeed;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(198, 26);
            this.layoutControlItem5.Text = "泄漏速度:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spWindSpeed;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(204, 26);
            this.layoutControlItem6.Text = "风速:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cbeWindDirection;
            this.layoutControlItem7.CustomizationFormText = "风向:";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(228, 26);
            this.layoutControlItem7.Text = "风向:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teCarrierType;
            this.layoutControlItem8.CustomizationFormText = "载体类型:";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(228, 26);
            this.layoutControlItem8.Text = "载体类型:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.labelControl1;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(204, 26);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(24, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.labelControl2;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(198, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 345);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(252, 77);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "时间设置";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 174);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(252, 122);
            this.layoutControlGroup3.Text = "时间变化";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teTimeStart;
            this.layoutControlItem9.CustomizationFormText = "开始时间:";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(228, 26);
            this.layoutControlItem9.Text = "开始时间:";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.teTimeCurrent;
            this.layoutControlItem10.CustomizationFormText = "当前时间:";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(228, 26);
            this.layoutControlItem10.Text = "当前时间:";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.teTimeSpend;
            this.layoutControlItem11.CustomizationFormText = "持续时间:";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(228, 26);
            this.layoutControlItem11.Text = "持续时间:";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnStart;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 296);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnPause;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(81, 296);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnStop;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(164, 296);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(88, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.ceFlyToRes;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 322);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(252, 23);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UCSpreadAnalyse
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCSpreadAnalyse";
            this.Size = new System.Drawing.Size(252, 422);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceFlyToRes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTimeSpend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTimeCurrent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTimeStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCarrierType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeWindDirection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWindSpeed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spLeekSpeed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            this.ResumeLayout(false);

        }

        private int dTime = 0;
        private DateTime startTime;
        private IGeometry _oldGeo = null;
        private int _alpha = 255;
        private List<Guid> _renderObjs = new List<Guid>();
        private bool _isStartAnlyse = false;
        private bool _isPause = false;
        private DrawTool _drawTool;
        private IRenderPoint _rPoint;
        private IParticleEffect _particleEffect;

        public void RestoreEnv()
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Close();
                this._drawTool.End();
            }
        }

        private void Clear()
        {
            if (_rPoint != null)
            {
                d3.ObjectManager.DeleteObject(_rPoint.Guid);
                _rPoint = null;
            }
            if (_particleEffect != null)
            {
                d3.ObjectManager.DeleteObject(_particleEffect.Guid);
                _particleEffect = null;
            }

            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            _isStartAnlyse = false;
            _isPause = false;
            foreach (Guid guid in this._renderObjs)
            {
                d3.ObjectManager.DeleteObject(guid);
            }
            this._renderObjs.Clear();
            this.timer1.Stop();
        }

        private void clearPolygon()
        {
            foreach (Guid guid in this._renderObjs)
            {
                d3.ObjectManager.DeleteObject(guid);
            }
            this._renderObjs.Clear();
            this._oldGeo = null;
        }

        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.SelectOne
                && this._drawTool.GetSelectFeatureLayerPickResult() != null && this._drawTool.GetSelectPoint() != null)
            {

                try
                {
                    IFeatureLayer fl = this._drawTool.GetSelectFeatureLayerPickResult().FeatureLayer;
                    if (fl == null) return;
                    FacilityClass facc = Dictionary3DTable.Instance.GetFacilityClassByDFFeatureClassID(fl.FeatureClassId.ToString());
                    if (facc == null || facc.Name != "PipeLine")
                    {
                        XtraMessageBox.Show("您选中的不是管线设施，请选择管线设施。", "提示");
                        return;
                    }
                    DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fl.FeatureClassId.ToString());
                    if (dffc == null || dffc.GetFeatureClass() == null) return;
                    IImagePointSymbol ips = new ImagePointSymbol();
                    ips.Size = SystemInfo.Instance.SymbolSize;
                    ips.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\POI\\accidentPoint.png");

                    IPoint intersectPoint = this._drawTool.GetSelectPoint();

                    _rPoint = d3.ObjectManager.CreateRenderPoint(intersectPoint, ips, d3.ProjectTree.RootID);
                    _rPoint.MinVisibleDistance = 499;
                    _rPoint.MaxVisibleDistance = 99999;

                    _particleEffect = d3.ObjectManager.CreateParticleEffect(d3.ProjectTree.RootID);
                    MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fl.FeatureClassId.ToString());
                    if (mc != null) this.teCarrierType.Text = mc.ToString();
                    if (mc != null && (mc.Name == "PS" || mc.Name == "GS"))
                    {
                        _particleEffect.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\ParticleEffect\\water.png");

                        _particleEffect.ParticleBillboardType = gviParticleBillboardType.gviParticleBillboardOrientedMoveDirection;
                        //_particleEffect.setTextureTileRange(8, 8, 63, 63); 
                        _particleEffect.EmissionMinRate = 1600;
                        _particleEffect.EmissionMaxRate = 1300;
                        _particleEffect.EmissionMinAngle = 0;
                        _particleEffect.EmissionMaxAngle = 0.025 * Math.PI;   // 改成一个圆
                        _particleEffect.EmissionMinMoveSpeed = 25;
                        _particleEffect.EmissionMaxMoveSpeed = 30;
                        _particleEffect.EmissionMinRotationSpeed = 0;
                        _particleEffect.EmissionMaxRotationSpeed = 0;

                        _particleEffect.ParticleMinLifeTime = 4.5;
                        _particleEffect.ParticleMaxLifeTime = 5.5;
                        _particleEffect.EmissionMinParticleSize = 0.2;
                        _particleEffect.EmissionMaxParticleSize = 0.25;

                        _particleEffect.EmissionMinScaleSpeed = 0;
                        _particleEffect.EmissionMaxScaleSpeed = 0;
                        _particleEffect.ParticleBirthColor = 0xffffffff;
                        _particleEffect.ParticleDeathColor = 0x00ffffff;
                        _particleEffect.VerticalAcceleration = 5;
                        _particleEffect.Damping = 0.5;
                        _particleEffect.WindAcceleration = 0;
                        _particleEffect.WindDirection = 0;
                        IEulerAngle v3t = new EulerAngle();
                        v3t.Set(90, 45, 0);
                        _particleEffect.EmissionDirectionEulerAngle = v3t;
                    }
                    else
                    {
                        _particleEffect.ImageName = Path.Combine(Application.StartupPath, "..\\Resource\\Images\\ParticleEffect\\smoke1.png");
                        _particleEffect.ParticleBillboardType = gviParticleBillboardType.gviParticleBillboardOrientedCamera;
                        _particleEffect.SetTextureTileRange(8, 8, 63, 63);  // 58,58位置的图有问题
                        _particleEffect.EmissionMinRate = 20;
                        _particleEffect.EmissionMaxRate = 30;
                        _particleEffect.EmissionMinAngle = 0;
                        _particleEffect.EmissionMaxAngle = 3.14 * 2.0;   // 改成一个圆
                        _particleEffect.EmissionMinMoveSpeed = 0;
                        _particleEffect.EmissionMaxMoveSpeed = 1;
                        _particleEffect.EmissionMinRotationSpeed = -1;
                        _particleEffect.EmissionMaxRotationSpeed = 1;

                        _particleEffect.ParticleMinLifeTime = 10;
                        _particleEffect.ParticleMaxLifeTime = 12;
                        _particleEffect.EmissionMinParticleSize = 0.75;
                        _particleEffect.EmissionMaxParticleSize = 0.9;

                        _particleEffect.EmissionMinScaleSpeed = 1.5;
                        _particleEffect.EmissionMaxScaleSpeed = 1.85;
                        _particleEffect.ParticleBirthColor = 0xffffffff;
                        _particleEffect.ParticleDeathColor = 0x00ffffff;
                        _particleEffect.VerticalAcceleration = -2;
                        _particleEffect.Damping = 0;
                        _particleEffect.WindAcceleration = 0;
                        _particleEffect.WindDirection = 0;
                    }
                    _particleEffect.MinVisibleDistance = 0;
                    _particleEffect.MaxVisibleDistance = 500;

                    _particleEffect.Start(-1);
                    IPoint pttemp = (new GeometryFactoryClass()).CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    IVector3 v3 = new Vector3();
                    v3.Set(intersectPoint.X, intersectPoint.Y, intersectPoint.Z);
                    pttemp.Position = v3;
                    _particleEffect.SetCircleEmitter(pttemp, 0);

                    this._drawTool.End();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            }
        }

        private void btnSetLeak_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.SelectOne);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            clearPolygon();
            dTime = 0;
            _alpha = 255;
            if (_particleEffect == null)
            {
                XtraMessageBox.Show("请先鼠标点击确认事故泄漏点！", "提示");
                return;
            }

            startTime = DateTime.Now;
            this.teTimeStart.Text = startTime.ToLongTimeString() + " " + startTime.ToLongDateString();
            _isStartAnlyse = true;
            _isPause = false;
            _elipse.timeSpan = 5;

            this.timer1.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_isStartAnlyse)
            {
                _isPause = !_isPause;
                if (_isPause) this.btnPause.Text = "继续分析";
                else this.btnPause.Text = "暂停分析";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _isStartAnlyse = false;
            _isPause = false;
            clearPolygon();
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_isStartAnlyse && !_isPause)
            {
                _particleEffect.WindAcceleration = double.Parse(this.spWindSpeed.Text);
                _particleEffect.WindDirection = this.cbeWindDirection.SelectedIndex * 45;
                IPoint position = _particleEffect.GetPosition();
                _elipse.x = position.X;
                _elipse.y = position.Y;
                _elipse.windDirection = (90 - this.cbeWindDirection.SelectedIndex * 45) * Math.PI / 180;
                _elipse.windSpeed = double.Parse(this.spWindSpeed.Text);
                _elipse.rate = double.Parse(this.spLeekSpeed.Text);
                _elipse.segCount = 32;
                _elipse.dAngle = Math.PI * 2 / _elipse.segCount;

                int num = 5;
                dTime += num;
                int minite = (int)Math.Floor(dTime / 60.0);
                int second = dTime - minite * 60;
                if (minite > 0) this.teTimeSpend.Text = minite + "分" + second + "秒";
                else this.teTimeSpend.Text = second + "秒";

                DateTime curNow = startTime.Add(new TimeSpan(0, 0, dTime));

                this.teTimeCurrent.Text = curNow.ToLongTimeString() + " " + curNow.ToLongDateString();
                int count = 8;
                int span = num;
                PolygonExpand(count, span);
            }
        }

        private uint colorFromARGB(int a, int r, int g, int b)
        {
            return (uint)(a << 24 | r << 16 | g << 8 | b);  // 通过右移转成无符号整数
        }

        private void PolygonExpand(int count, int span)
        {
            if (_alpha == 0) return;
            int num = 25;
            for (int i = 0; i < count; i++)
            {
                double[] numArray = _elipse.drawElipse();
                _elipse.timeSpan += span;
                if (numArray != null && numArray.Length > 0)
                {
                    List<IPoint> list = new List<IPoint>();
                    for (int j = 0; j < ((numArray.Length / 2) - 1); j++)
                    {
                        if (numArray[j * 2] == double.NaN)
                        {
                            break;
                        }
                        IPoint item = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        item.X = numArray[j * 2];
                        item.Y = numArray[(j * 2) + 1];
                        item.Z = num; //0.0 25
                        list.Add(item);
                        if (j == ((numArray.Length / 2) - 2))
                        {
                            list.Add(list[0]);
                        }
                    }
                    if (list.Count > 0)
                    {
                        IPolygon geometry = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                        for (int k = 0; k < list.Count; k++)
                        {
                            geometry.ExteriorRing.AppendPoint(list[k]);
                        }
                        IPolygon fdeValue = null;
                        IPolygon polygon3 = null;
                        if (i == 0)
                        {
                            polygon3 = geometry;
                        }
                        else if (_oldGeo != null)
                        {
                            polygon3 = (geometry as ITopologicalOperator2D).SymmetricDifference2D(_oldGeo) as IPolygon;
                            if (polygon3 != null)
                            {
                                fdeValue = (new GeometryFactory()).CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                                for (int m = 0; m < polygon3.ExteriorRing.PointCount; m++)
                                {
                                    IPoint point = polygon3.ExteriorRing.GetPoint(m);
                                    IPoint pointValue = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                    pointValue.X = point.X;
                                    pointValue.Y = point.Y;
                                    pointValue.Z = num;
                                    fdeValue.ExteriorRing.AppendPoint(pointValue);
                                }
                            }
                        }
                        else if (polygon3 == null)
                        {
                            _oldGeo = geometry;
                            continue;
                        }
                        if ((polygon3 != null) && (fdeValue != null))
                        {
                            ISurfaceSymbol ss = new SurfaceSymbol();
                            ss.Color = colorFromARGB(_alpha, 255, 0, 0);
                            ICurveSymbol bs = new CurveSymbol();
                            bs.Color = colorFromARGB(_alpha, 175, 0, 0);
                            ss.BoundarySymbol = bs;
                            IRenderPolygon rPolygon = d3.ObjectManager.CreateRenderPolygon(fdeValue, ss, d3.ProjectTree.RootID);
                            rPolygon.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                            if (this.ceFlyToRes.Checked) d3.Camera.FlyToObject(rPolygon.Guid, gviActionCode.gviActionFlyTo);
                            _renderObjs.Add(rPolygon.Guid);
                            if (_alpha > 0) _alpha = _alpha - 3;
                            if (_alpha < 0) _alpha = 0;
                        }
                    }
                }
            }
        }

    }
}
