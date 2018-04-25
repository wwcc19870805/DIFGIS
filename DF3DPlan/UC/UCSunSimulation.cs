using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using DF3DDraw;
using System.Timers;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using DF3DData.Class;
using DFWinForms.Class;

namespace DF3DPlan.UC
{
    public class UCSunSimulation : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private ColorPickEdit ce_color;
        private ComboBoxEdit cbeLunarHolDay;
        private TimeEdit te_time;
        private DateEdit de_data;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private TrackBarControl tbc_Date;
        private TrackBarControl tbc_Time;
        private SpinEdit spe_Int;
        private ComboBoxEdit cmb_simu;
        private TrackBarControl tbc_colorAlpha;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraBars.BarManager barManager1;
        private IContainer components;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bci_Play;
        private DevExpress.XtraBars.BarButtonItem bci_Pause;
        private DevExpress.XtraBars.BarButtonItem bci_Stop;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tbc_Date = new DevExpress.XtraEditors.TrackBarControl();
            this.tbc_Time = new DevExpress.XtraEditors.TrackBarControl();
            this.spe_Int = new DevExpress.XtraEditors.SpinEdit();
            this.cmb_simu = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tbc_colorAlpha = new DevExpress.XtraEditors.TrackBarControl();
            this.ce_color = new DevExpress.XtraEditors.ColorPickEdit();
            this.cbeLunarHolDay = new DevExpress.XtraEditors.ComboBoxEdit();
            this.te_time = new DevExpress.XtraEditors.TimeEdit();
            this.de_data = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bci_Play = new DevExpress.XtraBars.BarButtonItem();
            this.bci_Pause = new DevExpress.XtraBars.BarButtonItem();
            this.bci_Stop = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spe_Int.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_simu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_colorAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_colorAlpha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_color.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeLunarHolDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_data.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_data.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tbc_Date);
            this.layoutControl1.Controls.Add(this.tbc_Time);
            this.layoutControl1.Controls.Add(this.spe_Int);
            this.layoutControl1.Controls.Add(this.cmb_simu);
            this.layoutControl1.Controls.Add(this.tbc_colorAlpha);
            this.layoutControl1.Controls.Add(this.ce_color);
            this.layoutControl1.Controls.Add(this.cbeLunarHolDay);
            this.layoutControl1.Controls.Add(this.te_time);
            this.layoutControl1.Controls.Add(this.de_data);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(253, 444);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tbc_Date
            // 
            this.tbc_Date.EditValue = null;
            this.tbc_Date.Location = new System.Drawing.Point(77, 332);
            this.tbc_Date.Name = "tbc_Date";
            this.tbc_Date.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbc_Date.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbc_Date.Properties.LargeChange = 30;
            this.tbc_Date.Properties.Maximum = 366;
            this.tbc_Date.Properties.TickFrequency = 15;
            this.tbc_Date.Size = new System.Drawing.Size(162, 45);
            this.tbc_Date.StyleController = this.layoutControl1;
            this.tbc_Date.TabIndex = 15;
            this.tbc_Date.EditValueChanged += new System.EventHandler(this.tbc_Date_EditValueChanged);
            // 
            // tbc_Time
            // 
            this.tbc_Time.EditValue = null;
            this.tbc_Time.Location = new System.Drawing.Point(77, 283);
            this.tbc_Time.Name = "tbc_Time";
            this.tbc_Time.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbc_Time.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbc_Time.Properties.LargeChange = 60;
            this.tbc_Time.Properties.Maximum = 1440;
            this.tbc_Time.Properties.TickFrequency = 60;
            this.tbc_Time.Size = new System.Drawing.Size(162, 45);
            this.tbc_Time.StyleController = this.layoutControl1;
            this.tbc_Time.TabIndex = 14;
            this.tbc_Time.EditValueChanged += new System.EventHandler(this.tbc_Time_EditValueChanged);
            // 
            // spe_Int
            // 
            this.spe_Int.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spe_Int.Location = new System.Drawing.Point(77, 257);
            this.spe_Int.Name = "spe_Int";
            this.spe_Int.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spe_Int.Properties.IsFloatValue = false;
            this.spe_Int.Properties.Mask.EditMask = "N00";
            this.spe_Int.Properties.MaxValue = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.spe_Int.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spe_Int.Size = new System.Drawing.Size(162, 22);
            this.spe_Int.StyleController = this.layoutControl1;
            this.spe_Int.TabIndex = 13;
            // 
            // cmb_simu
            // 
            this.cmb_simu.EditValue = "按时间(分钟)";
            this.cmb_simu.Location = new System.Drawing.Point(77, 231);
            this.cmb_simu.Name = "cmb_simu";
            this.cmb_simu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_simu.Size = new System.Drawing.Size(162, 22);
            this.cmb_simu.StyleController = this.layoutControl1;
            this.cmb_simu.TabIndex = 12;
            // 
            // tbc_colorAlpha
            // 
            this.tbc_colorAlpha.EditValue = 255;
            this.tbc_colorAlpha.Location = new System.Drawing.Point(77, 138);
            this.tbc_colorAlpha.Name = "tbc_colorAlpha";
            this.tbc_colorAlpha.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbc_colorAlpha.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbc_colorAlpha.Properties.Maximum = 255;
            this.tbc_colorAlpha.Properties.TickFrequency = 5;
            this.tbc_colorAlpha.Size = new System.Drawing.Size(162, 45);
            this.tbc_colorAlpha.StyleController = this.layoutControl1;
            this.tbc_colorAlpha.TabIndex = 11;
            this.tbc_colorAlpha.Value = 255;
            this.tbc_colorAlpha.EditValueChanged += new System.EventHandler(this.tbc_colorAlpha_EditValueChanged);
            // 
            // ce_color
            // 
            this.ce_color.EditValue = System.Drawing.Color.Black;
            this.ce_color.Location = new System.Drawing.Point(77, 112);
            this.ce_color.Name = "ce_color";
            this.ce_color.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ce_color.Size = new System.Drawing.Size(162, 22);
            this.ce_color.StyleController = this.layoutControl1;
            this.ce_color.TabIndex = 10;
            this.ce_color.EditValueChanged += new System.EventHandler(this.ce_color_EditValueChanged);
            // 
            // cbeLunarHolDay
            // 
            this.cbeLunarHolDay.Location = new System.Drawing.Point(77, 86);
            this.cbeLunarHolDay.Name = "cbeLunarHolDay";
            this.cbeLunarHolDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeLunarHolDay.Size = new System.Drawing.Size(162, 22);
            this.cbeLunarHolDay.StyleController = this.layoutControl1;
            this.cbeLunarHolDay.TabIndex = 9;
            // 
            // te_time
            // 
            this.te_time.EditValue = new System.DateTime(2018, 1, 5, 0, 0, 0, 0);
            this.te_time.Location = new System.Drawing.Point(77, 60);
            this.te_time.Name = "te_time";
            this.te_time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.te_time.Size = new System.Drawing.Size(162, 22);
            this.te_time.StyleController = this.layoutControl1;
            this.te_time.TabIndex = 8;
            this.te_time.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.te_time_EditValueChanging);
            // 
            // de_data
            // 
            this.de_data.EditValue = null;
            this.de_data.Location = new System.Drawing.Point(77, 34);
            this.de_data.Name = "de_data";
            this.de_data.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_data.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_data.Size = new System.Drawing.Size(162, 22);
            this.de_data.StyleController = this.layoutControl1;
            this.de_data.TabIndex = 7;
            this.de_data.EditValueChanged += new System.EventHandler(this.de_data_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.emptySpaceItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(253, 444);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "基本设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(253, 197);
            this.layoutControlGroup2.Text = "基本设置";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.de_data;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem4.Text = "当前日期：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.te_time;
            this.layoutControlItem5.CustomizationFormText = "时间：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem5.Text = "当前时间：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cbeLunarHolDay;
            this.layoutControlItem6.CustomizationFormText = "节气:";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem6.Text = "节气:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.ce_color;
            this.layoutControlItem7.CustomizationFormText = "阴影颜色：";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem7.Text = "阴影颜色：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.tbc_colorAlpha;
            this.layoutControlItem8.CustomizationFormText = "不透明度:";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(229, 49);
            this.layoutControlItem8.Text = "不透明度:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "动态模拟";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 197);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(253, 194);
            this.layoutControlGroup3.Text = "动态模拟";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cmb_simu;
            this.layoutControlItem9.CustomizationFormText = "模拟方式：";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem9.Text = "模拟方式：";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.spe_Int;
            this.layoutControlItem10.CustomizationFormText = "时间间隔：";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem10.Text = "时间间隔：";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.tbc_Time;
            this.layoutControlItem11.CustomizationFormText = "时间：";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(229, 49);
            this.layoutControlItem11.Text = "时间：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.tbc_Date;
            this.layoutControlItem12.CustomizationFormText = "日期:";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 101);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(229, 49);
            this.layoutControlItem12.Text = "日期:";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 391);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(253, 53);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bci_Play,
            this.bci_Pause,
            this.bci_Stop});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bci_Play),
            new DevExpress.XtraBars.LinkPersistInfo(this.bci_Pause),
            new DevExpress.XtraBars.LinkPersistInfo(this.bci_Stop)});
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // bci_Play
            // 
            this.bci_Play.Caption = "播放";
            this.bci_Play.Id = 0;
            this.bci_Play.Name = "bci_Play";
            this.bci_Play.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bci_Play_ItemClick);
            // 
            // bci_Pause
            // 
            this.bci_Pause.Caption = "暂停";
            this.bci_Pause.Id = 1;
            this.bci_Pause.Name = "bci_Pause";
            this.bci_Pause.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bci_Pause_ItemClick);
            // 
            // bci_Stop
            // 
            this.bci_Stop.Caption = "停止";
            this.bci_Stop.Id = 2;
            this.bci_Stop.Name = "bci_Stop";
            this.bci_Stop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bci_Stop_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(253, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 475);
            this.barDockControlBottom.Size = new System.Drawing.Size(253, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 444);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(253, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 444);
            // 
            // UCSunSimulation
            // 
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCSunSimulation";
            this.Size = new System.Drawing.Size(253, 475);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Date)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_Time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spe_Int.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_simu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_colorAlpha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbc_colorAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_color.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeLunarHolDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_data.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_data.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        private string[] LunarHolDayName_CHS = new string[] { 
        "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", 
        "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
     };
        private string[] LunarHolDayName_CHT = new string[] { 
        "小寒", "大寒", "立春", "雨水", "驚蟄", "春分", "清明", "穀雨", "立夏", "小滿", "芒種", "夏至", "小暑", "大暑", "立秋", "處暑", 
        "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
     };
        private string[] LunarHolDayName_EN = new string[] { 
        "xiaohan", "dahan", "lichun", "yushui", "jingzhe", "chunfen", "qingming", "guyu", "lixia", "xiaoman", "mangzhong", "xiazhi", "xiaoshu", "dashu", "liqiu", "chushu", 
        "bailu", "qiufen", "lanlu", "shuangjiang", "lidong", "xiaoxue", "daxue", "dongzhi"
     };

        private AxRenderControl _3DControl;
        private System.Timers.Timer _timer;
        private uint _oldShadowColor;
        public UCSunSimulation()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) { this.Enabled = false; return; }
            this._3DControl = app.Current3DMapControl;
            this._oldShadowColor = this._3DControl.SunConfig.ShadowColor;
            this._3DControl.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeAccordingToGMT;
            
            DateTime dt = DateTime.Now;
            this.de_data.EditValue = dt;
            this.te_time.EditValue = dt;
            this._timer = new System.Timers.Timer();
            this._timer.Interval = 3000;
        }

        public void RestoreEnv()
        {
            this._3DControl.SunConfig.ShadowColor = this._oldShadowColor;
            this._3DControl.SunConfig.EnableShadow(0, false);
            if (_bPlay)
            {
                this._timer.Elapsed -= new ElapsedEventHandler(_timer_Elapsed);
                this._timer.Close();
            }
        }

        private void de_data_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Parse(this.de_data.Text + " " + this.te_time.Text);
            this._3DControl.SunConfig.SetGMT(dt);
        }

        private void te_time_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            DateTime dt = DateTime.Parse(this.de_data.Text + " " + this.te_time.Text);
            this._3DControl.SunConfig.SetGMT(dt);
        }

        private void ce_color_EditValueChanged(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(this.tbc_colorAlpha.Value);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(num, this.ce_color.Color.R, this.ce_color.Color.G, this.ce_color.Color.B);
            uint shadowColor = (uint)color.ToArgb();
            this._3DControl.SunConfig.ShadowColor = shadowColor;
        }

        private void tbc_colorAlpha_EditValueChanged(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(this.tbc_colorAlpha.Value);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(num, this.ce_color.Color.R, this.ce_color.Color.G, this.ce_color.Color.B);
            uint shadowColor = (uint)color.ToArgb();
            this._3DControl.SunConfig.ShadowColor = shadowColor;
        }

        private void tbc_Time_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Parse(this.de_data.Text + " " + this.te_time.Text);
            DateTime dt1 = dt.AddDays(this.tbc_Date.Value).AddMinutes(this.tbc_Time.Value);
            this.de_data.EditValue = dt1;
            this.te_time.EditValue = dt1;
            if (this.tbc_Time.Value >= 1440)
            {
                this.tbc_Time.Value = 0;
                this.tbc_Date.Value += 1;
            }
        }

        private void tbc_Date_EditValueChanged(object sender, EventArgs e)
        {
            if (this.tbc_Date.Value >= 366)
            {
                this.tbc_Date.Value = 0;
                bci_Stop_ItemClick(null, null);
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                base.Invoke(new EventHandler(this.TimerAction), new object[] { sender, e });
            }
            catch (Exception exception)
            {
            }
        }

        private void TimerAction(object sender, EventArgs e)
        {
            this.tbc_Time.Value += (int)this.spe_Int.Value;
        }

        private bool _bPlay;
        private void bci_Play_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_bPlay) return;
            this._timer.Elapsed += new ElapsedEventHandler(this._timer_Elapsed);
            this._timer.Enabled = true;
            this._timer.Start();

            this._3DControl.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeAccordingToGMT;
            DateTime dt = DateTime.Parse(this.de_data.Text + " " + this.te_time.Text);
            DateTime dt1 = dt.AddDays(this.tbc_Date.Value).AddMinutes(this.tbc_Time.Value);
            this._3DControl.SunConfig.SetGMT(dt1);
            this._3DControl.SunConfig.EnableShadow(0, true);

            _bPlay = true;
        }

        private void bci_Pause_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this._timer.Stop();
        }

        private void bci_Stop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_bPlay) return;
            this._3DControl.SunConfig.EnableShadow(0, false);

            this.tbc_Time.Value = 0;
            this.tbc_Date.Value = 0;

            this._timer.Enabled = false;
            this._timer.Elapsed -= new ElapsedEventHandler(this._timer_Elapsed);
            this._timer.Stop();

            DateTime dt = DateTime.Now;
            this.de_data.EditValue = dt;
            this.te_time.EditValue = dt;

            _bPlay = false;
        }

    }
}
