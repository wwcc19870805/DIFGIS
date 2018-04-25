using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using DFCommon.Class;
using Gvitech.CityMaker.Math;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using DFWinForms.Class;

namespace DF3DPlan.UC
{
    public class UCAlongRoadVisibility : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private CheckEdit ceRepeat;
        private SimpleButton btnStop;
        private SimpleButton btnPlay;
        private SpinEdit spFlySpeed;
        private SimpleButton btnAnalysis;
        private SimpleButton btnLocObjectivePoint;
        private CheckEdit ceShowLinkLine;
        private SpinEdit spObjectiveHOff;
        private SimpleButton btnDrawLine;
        private SpinEdit spObserverInter;
        private SpinEdit spObserverHOff;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceRepeat = new DevExpress.XtraEditors.CheckEdit();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.btnPlay = new DevExpress.XtraEditors.SimpleButton();
            this.spFlySpeed = new DevExpress.XtraEditors.SpinEdit();
            this.btnAnalysis = new DevExpress.XtraEditors.SimpleButton();
            this.btnLocObjectivePoint = new DevExpress.XtraEditors.SimpleButton();
            this.ceShowLinkLine = new DevExpress.XtraEditors.CheckEdit();
            this.spObjectiveHOff = new DevExpress.XtraEditors.SpinEdit();
            this.btnDrawLine = new DevExpress.XtraEditors.SimpleButton();
            this.spObserverInter = new DevExpress.XtraEditors.SpinEdit();
            this.spObserverHOff = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceRepeat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spFlySpeed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowLinkLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spObjectiveHOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spObserverInter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spObserverHOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceRepeat);
            this.layoutControl1.Controls.Add(this.btnStop);
            this.layoutControl1.Controls.Add(this.btnPlay);
            this.layoutControl1.Controls.Add(this.spFlySpeed);
            this.layoutControl1.Controls.Add(this.btnAnalysis);
            this.layoutControl1.Controls.Add(this.btnLocObjectivePoint);
            this.layoutControl1.Controls.Add(this.ceShowLinkLine);
            this.layoutControl1.Controls.Add(this.spObjectiveHOff);
            this.layoutControl1.Controls.Add(this.btnDrawLine);
            this.layoutControl1.Controls.Add(this.spObserverInter);
            this.layoutControl1.Controls.Add(this.spObserverHOff);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(255, 431);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceRepeat
            // 
            this.ceRepeat.Location = new System.Drawing.Point(14, 327);
            this.ceRepeat.Name = "ceRepeat";
            this.ceRepeat.Properties.Caption = "循环";
            this.ceRepeat.Size = new System.Drawing.Size(55, 19);
            this.ceRepeat.StyleController = this.layoutControl1;
            this.ceRepeat.TabIndex = 8;
            this.ceRepeat.CheckedChanged += new System.EventHandler(this.ceRepeat_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(174, 327);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(67, 22);
            this.btnStop.StyleController = this.layoutControl1;
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(105, 327);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(65, 22);
            this.btnPlay.StyleController = this.layoutControl1;
            this.btnPlay.TabIndex = 9;
            this.btnPlay.Text = "播放";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // spFlySpeed
            // 
            this.spFlySpeed.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spFlySpeed.Location = new System.Drawing.Point(101, 301);
            this.spFlySpeed.Name = "spFlySpeed";
            this.spFlySpeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spFlySpeed.Size = new System.Drawing.Size(140, 22);
            this.spFlySpeed.StyleController = this.layoutControl1;
            this.spFlySpeed.TabIndex = 7;
            this.spFlySpeed.EditValueChanged += new System.EventHandler(this.spFlySpeed_EditValueChanged);
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Location = new System.Drawing.Point(2, 243);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(251, 22);
            this.btnAnalysis.StyleController = this.layoutControl1;
            this.btnAnalysis.TabIndex = 6;
            this.btnAnalysis.Text = "分        析";
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // btnLocObjectivePoint
            // 
            this.btnLocObjectivePoint.Location = new System.Drawing.Point(14, 205);
            this.btnLocObjectivePoint.Name = "btnLocObjectivePoint";
            this.btnLocObjectivePoint.Size = new System.Drawing.Size(227, 22);
            this.btnLocObjectivePoint.StyleController = this.layoutControl1;
            this.btnLocObjectivePoint.TabIndex = 5;
            this.btnLocObjectivePoint.Text = "定位目标点";
            this.btnLocObjectivePoint.Click += new System.EventHandler(this.btnLocObjectivePoint_Click);
            // 
            // ceShowLinkLine
            // 
            this.ceShowLinkLine.EditValue = true;
            this.ceShowLinkLine.Location = new System.Drawing.Point(14, 182);
            this.ceShowLinkLine.Name = "ceShowLinkLine";
            this.ceShowLinkLine.Properties.Caption = "显示连接线";
            this.ceShowLinkLine.Size = new System.Drawing.Size(227, 19);
            this.ceShowLinkLine.StyleController = this.layoutControl1;
            this.ceShowLinkLine.TabIndex = 4;
            this.ceShowLinkLine.CheckedChanged += new System.EventHandler(this.ceShowLinkLine_CheckedChanged);
            // 
            // spObjectiveHOff
            // 
            this.spObjectiveHOff.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spObjectiveHOff.Location = new System.Drawing.Point(101, 156);
            this.spObjectiveHOff.Name = "spObjectiveHOff";
            this.spObjectiveHOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spObjectiveHOff.Size = new System.Drawing.Size(140, 22);
            this.spObjectiveHOff.StyleController = this.layoutControl1;
            this.spObjectiveHOff.TabIndex = 3;
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Location = new System.Drawing.Point(14, 86);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(227, 22);
            this.btnDrawLine.StyleController = this.layoutControl1;
            this.btnDrawLine.TabIndex = 2;
            this.btnDrawLine.Text = "绘制通视路线";
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // spObserverInter
            // 
            this.spObserverInter.EditValue = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.spObserverInter.Location = new System.Drawing.Point(101, 60);
            this.spObserverInter.Name = "spObserverInter";
            this.spObserverInter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spObserverInter.Size = new System.Drawing.Size(140, 22);
            this.spObserverInter.StyleController = this.layoutControl1;
            this.spObserverInter.TabIndex = 1;
            this.spObserverInter.EditValueChanged += new System.EventHandler(this.spObserverInter_EditValueChanged);
            // 
            // spObserverHOff
            // 
            this.spObserverHOff.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spObserverHOff.Location = new System.Drawing.Point(101, 34);
            this.spObserverHOff.Name = "spObserverHOff";
            this.spObserverHOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spObserverHOff.Size = new System.Drawing.Size(140, 22);
            this.spObserverHOff.StyleController = this.layoutControl1;
            this.spObserverHOff.TabIndex = 0;
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
            this.layoutControlItem7,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(255, 431);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "线路设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(255, 122);
            this.layoutControlGroup2.Text = "线路设置";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spObserverHOff;
            this.layoutControlItem2.CustomizationFormText = "高度偏移(m):";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem2.Text = "高度偏移(m):";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spObserverInter;
            this.layoutControlItem3.CustomizationFormText = "观察点间隔(m):";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem3.Text = "观察点间隔(m):";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnDrawLine;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "目标点";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 122);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(255, 119);
            this.layoutControlGroup3.Text = "目标点";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spObjectiveHOff;
            this.layoutControlItem4.CustomizationFormText = "高度偏移(m):";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem4.Text = "高度偏移(m):";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ceShowLinkLine;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(231, 23);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnLocObjectivePoint;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "通视动画";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.emptySpaceItem3});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 267);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(255, 96);
            this.layoutControlGroup4.Text = "通视动画";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.spFlySpeed;
            this.layoutControlItem8.CustomizationFormText = "飞行速度(m/s):";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(231, 26);
            this.layoutControlItem8.Text = "飞行速度(m/s):";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnPlay;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(91, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnStop;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(160, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(71, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ceRepeat;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(59, 26);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(59, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(32, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnAnalysis;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 241);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(255, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 363);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(255, 68);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // UCAlongRoadVisibility
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCAlongRoadVisibility";
            this.Size = new System.Drawing.Size(255, 431);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceRepeat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spFlySpeed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowLinkLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spObjectiveHOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spObserverInter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spObserverHOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private AxRenderControl _3DControl;
        private IGeometryFactory _geoFactory;
        public UCAlongRoadVisibility()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) { this.Enabled = false; return; }
            this._3DControl = app.Current3DMapControl;

            this._listRenders = new List<Guid>();
            this._listRenderPoints = new List<IRenderPoint>();
            this._listRenderSightlines = new List<IRenderPolyline>();
            this._listRes = new List<Guid>();
            this._geoFactory = new GeometryFactory();
        }

        public void RestoreEnv()
        {
            ClearLine();
            ClearLinePoints();
            ClearSightlines();
            ClearPoint();
            ClearRes();
            ClearTour();
            DetachEvent();
        }

        public void ClearRes()
        {
            foreach (Guid g in this._listRes)
            {
                this._3DControl.ObjectManager.DeleteObject(g);
            }
            this._listRes.Clear();
        }

        public void ClearLine()
        {
            if (_rLine != null)
            {
                this._3DControl.ObjectManager.DeleteObject(this._rLine.Guid);
                this._rLine = null;
                this._line = null;
            }
        }
        public void ClearLinePoints()
        {
            foreach (IRenderPoint rpt in this._listRenderPoints)
            {
                this._3DControl.ObjectManager.DeleteObject(rpt.Guid);
            }
            this._listRenderPoints.Clear();
        }
        public void ClearSightlines()
        {
            foreach (IRenderPolyline rline in this._listRenderSightlines)
            {
                this._3DControl.ObjectManager.DeleteObject(rline.Guid);
            }
            this._listRenderSightlines.Clear();
        }
        public void ClearPoint()
        {
            if (_rPoint != null)
            {
                this._3DControl.ObjectManager.DeleteObject(this._rPoint.Guid);
                this._rPoint = null;
            }
        }
        private bool bAttach;
        private void AttachEvent()
        {
            if (bAttach) return;
            this._3DControl.RcMouseClickSelect += new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
            this._3DControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractSelect;
            this._3DControl.MouseSelectMode = Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectClick | Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectMove;
            this._3DControl.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectAll;
            bAttach = true;
        }

        private void DetachEvent()
        {
            if (bAttach)
            {
                this._3DControl.RcMouseClickSelect -= new Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseClickSelectEventHandler(_3DControl_RcMouseClickSelect);
                this._3DControl.InteractMode = Gvitech.CityMaker.RenderControl.gviInteractMode.gviInteractNormal;
                this._3DControl.MouseSelectMode = Gvitech.CityMaker.RenderControl.gviMouseSelectMode.gviMouseSelectClick;
                this._3DControl.MouseSelectObjectMask = Gvitech.CityMaker.RenderControl.gviMouseSelectObjectMask.gviSelectAll;
                bAttach = false;
            }
        }

        private IRenderPoint _rPoint;
        private IRenderPolyline _rLine;
        private List<Guid> _listRenders;
        private List<IRenderPoint> _listRenderPoints;
        private List<IRenderPolyline> _listRenderSightlines;
        private List<Guid> _listRes;

        private IPolyline _line;
        private bool _bStartDrawLine;
        private int _iDrawType;

        void _3DControl_RcMouseClickSelect(object sender, _IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            if (e.intersectPoint == null) return;
            IPoint pt1 = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            pt1.X = e.intersectPoint.X;
            pt1.Y = e.intersectPoint.Y;
            pt1.Z = e.intersectPoint.Z;
            if (e.eventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                if (_iDrawType == 1)
                {
                    if (!_bStartDrawLine)
                    {
                        ClearLine();
                        ClearLinePoints();
                        ClearSightlines();
                        this._line = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                        ICurveSymbol cs = new CurveSymbol();
                        cs.Color = 0xff00ff00;
                        cs.Width = -2;
                        this._line.AppendPoint(pt1);
                        IPoint pt2 = pt1.Clone() as IPoint;
                        pt2.X += 0.000001;
                        this._line.AppendPoint(pt2);
                        this._rLine = this._3DControl.ObjectManager.CreateRenderPolyline(this._line, cs, this._3DControl.ProjectTree.RootID);
                        _bStartDrawLine = true;
                    }
                    else
                    {
                        _bStartDrawLine = false;
                        this._line.UpdatePoint(1, pt1);
                        this._rLine.SetFdeGeometry(this._line);
                        List<IPoint> list = GetDispersePoints(this._line, (double)this.spObserverInter.Value, (double)this.spObserverHOff.Value);
                        if (list != null && list.Count > 0)
                        {
                            ClearLine();
                            ClearSightlines();
                            ICurveSymbol cs = new CurveSymbolClass();
                            cs.Color = 0xff00ff00;
                            cs.Width = -1;
                            ISimplePointSymbol sps = new SimplePointSymbol();
                            sps.Size = 5;
                            sps.FillColor = 0xff00ff00;
                            sps.Style = gviSimplePointStyle.gviSimplePointCircle;
                            foreach (IPoint pttemp in list)
                            {
                                IRenderPoint rptTemp = this._3DControl.ObjectManager.CreateRenderPoint(pttemp, sps, this._3DControl.ProjectTree.RootID);
                                this._listRenderPoints.Add(rptTemp);

                                if (this._rPoint != null)
                                {
                                    IPoint drawPoint = this._rPoint.GetFdeGeometry() as IPoint;
                                    IPolyline sightline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                                    sightline.AppendPoint(pttemp);
                                    sightline.AppendPoint(drawPoint);
                                    IRenderPolyline rSightline = this._3DControl.ObjectManager.CreateRenderPolyline(sightline, cs, this._3DControl.ProjectTree.RootID);
                                    if (this.ceShowLinkLine.Checked) rSightline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                                    else rSightline.VisibleMask = gviViewportMask.gviViewNone;
                                    this._listRenderSightlines.Add(rSightline);
                                }
                            }
                        }
                    }
                }
                if (_iDrawType == 2)
                {
                    ClearPoint();
                    DetachEvent();
                    ISimplePointSymbol sps = new SimplePointSymbol();
                    sps.Size = 5;
                    sps.FillColor = 0xff00ff00;
                    sps.Style = gviSimplePointStyle.gviSimplePointCircle;
                    IPoint objectPt = pt1.Clone() as IPoint;
                    objectPt.Z += (double)this.spObjectiveHOff.Value;
                    this._rPoint = this._3DControl.ObjectManager.CreateRenderPoint(objectPt, sps, this._3DControl.ProjectTree.RootID);
                    if (this._listRenderPoints.Count > 0)
                    {
                        ClearSightlines();
                        IPoint drawPoint = this._rPoint.GetFdeGeometry() as IPoint;
                        ICurveSymbol cs = new CurveSymbolClass();
                        cs.Color = 0xff00ff00;
                        cs.Width = -1;

                        foreach (IRenderPoint rpttemp in this._listRenderPoints)
                        {
                            IPoint pttemp = rpttemp.GetFdeGeometry() as IPoint;
                            IPolyline sightline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                            sightline.AppendPoint(pttemp);
                            sightline.AppendPoint(drawPoint);
                            IRenderPolyline rSightline = this._3DControl.ObjectManager.CreateRenderPolyline(sightline, cs, this._3DControl.ProjectTree.RootID);
                            if (this.ceShowLinkLine.Checked) rSightline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                            else rSightline.VisibleMask = gviViewportMask.gviViewNone;
                            this._listRenderSightlines.Add(rSightline);
                        }
                    }
                }
            }
            else if (e.eventSender == gviMouseSelectMode.gviMouseSelectMove)
            {
                if (_iDrawType == 1)
                {
                    if (_bStartDrawLine)
                    {
                        this._line.UpdatePoint(1, pt1);
                        this._rLine.SetFdeGeometry(this._line);
                    }
                }
                if (_iDrawType == 2)
                {
                     this._rPoint.SetFdeGeometry(pt1);
                }
            }
        }

        private List<IPoint> GetDispersePoints(IPolyline line,double inter,double height)
        {
            if (line == null || line.PointCount != 2) return null;
            List<IPoint> list = new List<IPoint>();
            IPoint ptStart = line.StartPoint;
            IPoint ptEnd = line.EndPoint;
            double len = line.Length;
            IEulerAngle ang = this._3DControl.Camera.GetAimingAngles2(ptStart, ptEnd);
            int loop = (int)Math.Floor(len / inter);
            IPoint ptStart1 = ptStart.Clone() as IPoint;
            ptStart1.Z += height;
            list.Add(ptStart1);
            for (int i = 1; i <= loop; i++)
            {
                IPoint pt = this._3DControl.Camera.GetAimingPoint2(ptStart, ang, i * inter);
                pt.Z += height;
                list.Add(pt);
            }
            IPoint ptEnd1 = ptEnd.Clone() as IPoint;
            ptEnd1.Z += height;
            list.Add(ptEnd1);
            return list;
        }
        
        private void spObserverInter_EditValueChanged(object sender, EventArgs e)
        {
            if (this.spObserverInter.Value < 1) this.spObserverInter.Value = 1;
        }

        private void spFlySpeed_EditValueChanged(object sender, EventArgs e)
        {
            if (this.spFlySpeed.Value <= 0)
            {
                this.spFlySpeed.Value = 1;
            }
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            _iDrawType = 1;
            ClearRes();
            AttachEvent();
        }

        private void ceShowLinkLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ceShowLinkLine.Checked)
            {
                foreach (IRenderPolyline rLine in this._listRenderSightlines)
                {
                    rLine.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }
            else
            {
                foreach (IRenderPolyline rLine in this._listRenderSightlines)
                {
                    rLine.VisibleMask = gviViewportMask.gviViewNone;
                }
            }
        }

        private void btnLocObjectivePoint_Click(object sender, EventArgs e)
        {
            ClearPoint();
            ClearRes();
            _iDrawType = 2;
            ISimplePointSymbol sps = new SimplePointSymbol();
            sps.Size = 5;
            sps.FillColor = 0xff00ff00;
            sps.Style = gviSimplePointStyle.gviSimplePointCircle;
            IPoint pt = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            this._rPoint = this._3DControl.ObjectManager.CreateRenderPoint(pt, sps, this._3DControl.ProjectTree.RootID);
            AttachEvent();
        }

        private class InterPt 
        {
            public IPoint pt;
            public double dis;
        }
        private class CmpInterPt : Comparer<InterPt> 
        {
            public override int Compare(InterPt x, InterPt y)
            {
                if (x.dis > y.dis) return 1;
                else if (x.dis == y.dis) return 0;
                else return -1;
            }
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                ClearRes();
                WaitForm.Start("正在分析...", "请稍后");
                if (this._listRenderSightlines.Count == 0) return;
                List<DF3DFeatureClass> list = DF3DFeatureClassManager.Instance.GetAllFeatureClass();
                if (list == null || list.Count == 0) return;
                ISpatialFilter filter = new SpatialFilter();
                filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
                filter.GeometryField = "Geometry";
                int slcount = this._listRenderSightlines.Count;
                int num = 0;
                List<InterPt> listInterPts = new List<InterPt>();
                foreach (IRenderPolyline rline in this._listRenderSightlines)
                {
                    num++;
                    WaitForm.Start("正在进行第【" + num + "/" + slcount + "】条通视线分析...", "请稍后");
                    IPolyline line = rline.GetFdeGeometry() as IPolyline;
                    ILine l = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryLine, gviVertexAttribute.gviVertexAttributeZ) as ILine;
                    l.StartPoint = line.StartPoint;
                    l.EndPoint = line.EndPoint;
                    listInterPts.Clear();
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        IFeatureClass fc = dffc.GetFeatureClass();
                        if (fc == null) continue;
                        IFeatureLayer fl = dffc.GetFeatureLayer();
                        if (fl != null)
                        {
                            if (fl.VisibleMask == gviViewportMask.gviViewNone) continue;
                        }
                        int indexGeo = fc.GetFields().IndexOf("Geometry");
                        if (indexGeo == -1) continue;

                        filter.Geometry = l;
                        IFdeCursor cursor = null;
                        IRowBuffer row = null;
                        try
                        {
                            cursor = fc.Search(filter, false);
                            while ((row = cursor.NextRow()) != null)
                            {
                                if (!row.IsNull(indexGeo))
                                {
                                    IGeometry geo = row.GetValue(indexGeo) as IGeometry;
                                    if (geo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                                    {
                                        IModelPoint modelPoint = geo as IModelPoint;
                                        IModel model = (fc.FeatureDataSet as IResourceManager).GetModel(modelPoint.ModelName);
                                        IGeometryConvertor gc = new GeometryConvertor();
                                        IMultiTriMesh triMesh = gc.ModelPointToTriMesh(model, modelPoint, false);
                                        if (triMesh != null)
                                        {
                                            IVector3 v3 = triMesh.LineSegmentIntersect(l);
                                            if (v3 != null)
                                            {
                                                IPoint pttemp = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                                pttemp.X = v3.X;
                                                pttemp.Y = v3.Y;
                                                pttemp.Z = v3.Z;
                                                double dis = (pttemp.X - line.StartPoint.X) * (pttemp.X - line.StartPoint.X) +
                                                    (pttemp.Y - line.StartPoint.Y) * (pttemp.Y - line.StartPoint.Y) +
                                                    (pttemp.Z - line.StartPoint.Z) * (pttemp.Z - line.StartPoint.Z);
                                                InterPt ip = new InterPt();
                                                ip.pt = pttemp;
                                                ip.dis = dis;
                                                listInterPts.Add(ip);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            if (row != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                                row = null;
                            }
                            if (cursor != null)
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                cursor = null;
                            }
                        }
                    }
                    if (listInterPts.Count > 0)
                    {
                        rline.VisibleMask = gviViewportMask.gviViewNone;
                        listInterPts.Sort(new CmpInterPt());
                        IPoint pt = listInterPts[0].pt;
                        IPolyline lineStart = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                        lineStart.AppendPoint(line.StartPoint);
                        lineStart.AppendPoint(pt);
                        ICurveSymbol csS = new CurveSymbolClass();
                        csS.Color = 0xff0000ff;
                        csS.Width = -2;
                        IRenderPolyline rLineStart = this._3DControl.ObjectManager.CreateRenderPolyline(lineStart, csS, this._3DControl.ProjectTree.RootID);
                        this._listRes.Add(rLineStart.Guid);

                        IPolyline lineEnd = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                        lineEnd.AppendPoint(line.EndPoint);
                        lineEnd.AppendPoint(pt);
                        ICurveSymbol csE = new CurveSymbolClass();
                        csE.Color = 0xffff0000;
                        csE.Width = -2;
                        IRenderPolyline rLineEnd = this._3DControl.ObjectManager.CreateRenderPolyline(lineEnd, csE, this._3DControl.ProjectTree.RootID);
                        this._listRes.Add(rLineEnd.Guid);
                    }
                    else
                    {
                        rline.VisibleMask = gviViewportMask.gviViewNone;
                        ICurveSymbol csS = new CurveSymbolClass();
                        csS.Color = 0xff0000ff;
                        csS.Width = -2;
                        IRenderPolyline rLine = this._3DControl.ObjectManager.CreateRenderPolyline(line, csS, this._3DControl.ProjectTree.RootID);
                        this._listRes.Add(rLine.Guid);
                    }

                }
            }
            catch (Exception ex) { }
            finally
            {
                WaitForm.Stop();
            }
        }

        private ICameraTour _tour;
        private void ceRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (_tour == null) return;
            if (this.ceRepeat.Checked) _tour.AutoRepeat = true;
            else _tour.AutoRepeat = false;
        }

        public void ClearTour()
        {
            if (this._tour != null)
            {
                this._tour.Stop();
                this._3DControl.ObjectManager.DeleteObject(this._tour.Guid);
                this._tour = null;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this._listRenderPoints.Count == 0 || this._rPoint == null) return;
            IPoint objectPt = this._rPoint.GetFdeGeometry() as IPoint;
            ClearTour();
            DetachEvent();
            this._tour = this._3DControl.ObjectManager.CreateCameraTour(this._3DControl.ProjectTree.RootID);
            foreach (IRenderPoint rpt in this._listRenderPoints)
            {
                IPoint pt = rpt.GetFdeGeometry() as IPoint;
                IEulerAngle ang = this._3DControl.Camera.GetAimingAngles2(pt, objectPt);
                double second = (double)(this.spObserverInter.Value) / (double)(this.spFlySpeed.Value);
                this._tour.AddWaypoint2(pt, ang, second, gviCameraTourMode.gviCameraTourLinear);
            }
            ceRepeat_CheckedChanged(null, null);
            this._tour.Play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this._tour != null) this._tour.Stop();
        }

    }
}
