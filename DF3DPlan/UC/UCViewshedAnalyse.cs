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
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using DF3DData.Class;
using DFCommon.Class;

namespace DF3DPlan.UC
{
    public class UCViewshedAnalyse : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SpinEdit seAngle;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private SpinEdit seObserZOff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private SimpleButton btnAnalyse;
        private SimpleButton btnFlyToObjective;
        private SimpleButton btnFlyToObserver;
        private SimpleButton btnDrawRegion;
        private SpinEdit seObjZ;
        private SpinEdit seObjY;
        private SpinEdit seObjX;
        private SpinEdit seObserZ;
        private SpinEdit seObserY;
        private SpinEdit seObserX;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private SpinEdit seObjZOff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.seObjZOff = new DevExpress.XtraEditors.SpinEdit();
            this.btnAnalyse = new DevExpress.XtraEditors.SimpleButton();
            this.btnFlyToObjective = new DevExpress.XtraEditors.SimpleButton();
            this.btnFlyToObserver = new DevExpress.XtraEditors.SimpleButton();
            this.btnDrawRegion = new DevExpress.XtraEditors.SimpleButton();
            this.seObjZ = new DevExpress.XtraEditors.SpinEdit();
            this.seObjY = new DevExpress.XtraEditors.SpinEdit();
            this.seObjX = new DevExpress.XtraEditors.SpinEdit();
            this.seObserZ = new DevExpress.XtraEditors.SpinEdit();
            this.seObserY = new DevExpress.XtraEditors.SpinEdit();
            this.seObserX = new DevExpress.XtraEditors.SpinEdit();
            this.seObserZOff = new DevExpress.XtraEditors.SpinEdit();
            this.seAngle = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seObjZOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAngle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seObjZOff);
            this.layoutControl1.Controls.Add(this.btnAnalyse);
            this.layoutControl1.Controls.Add(this.btnFlyToObjective);
            this.layoutControl1.Controls.Add(this.btnFlyToObserver);
            this.layoutControl1.Controls.Add(this.btnDrawRegion);
            this.layoutControl1.Controls.Add(this.seObjZ);
            this.layoutControl1.Controls.Add(this.seObjY);
            this.layoutControl1.Controls.Add(this.seObjX);
            this.layoutControl1.Controls.Add(this.seObserZ);
            this.layoutControl1.Controls.Add(this.seObserY);
            this.layoutControl1.Controls.Add(this.seObserX);
            this.layoutControl1.Controls.Add(this.seObserZOff);
            this.layoutControl1.Controls.Add(this.seAngle);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(207, 502);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // seObjZOff
            // 
            this.seObjZOff.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObjZOff.Location = new System.Drawing.Point(97, 382);
            this.seObjZOff.Name = "seObjZOff";
            this.seObjZOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjZOff.Size = new System.Drawing.Size(96, 22);
            this.seObjZOff.StyleController = this.layoutControl1;
            this.seObjZOff.TabIndex = 9;
            this.seObjZOff.EditValueChanged += new System.EventHandler(this.seObjZOff_EditValueChanged);
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Location = new System.Drawing.Point(2, 446);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(203, 22);
            this.btnAnalyse.StyleController = this.layoutControl1;
            this.btnAnalyse.TabIndex = 12;
            this.btnAnalyse.Text = "分          析";
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // btnFlyToObjective
            // 
            this.btnFlyToObjective.Location = new System.Drawing.Point(14, 408);
            this.btnFlyToObjective.Name = "btnFlyToObjective";
            this.btnFlyToObjective.Size = new System.Drawing.Size(179, 22);
            this.btnFlyToObjective.StyleController = this.layoutControl1;
            this.btnFlyToObjective.TabIndex = 10;
            this.btnFlyToObjective.Text = "飞向目标点";
            this.btnFlyToObjective.Click += new System.EventHandler(this.btnFlyToObjective_Click);
            // 
            // btnFlyToObserver
            // 
            this.btnFlyToObserver.Location = new System.Drawing.Point(14, 234);
            this.btnFlyToObserver.Name = "btnFlyToObserver";
            this.btnFlyToObserver.Size = new System.Drawing.Size(179, 22);
            this.btnFlyToObserver.StyleController = this.layoutControl1;
            this.btnFlyToObserver.TabIndex = 5;
            this.btnFlyToObserver.Text = "飞向观察点";
            this.btnFlyToObserver.Click += new System.EventHandler(this.btnFlyToObserver_Click);
            // 
            // btnDrawRegion
            // 
            this.btnDrawRegion.Location = new System.Drawing.Point(2, 2);
            this.btnDrawRegion.Name = "btnDrawRegion";
            this.btnDrawRegion.Size = new System.Drawing.Size(203, 22);
            this.btnDrawRegion.StyleController = this.layoutControl1;
            this.btnDrawRegion.TabIndex = 11;
            this.btnDrawRegion.Text = "绘制视域范围";
            this.btnDrawRegion.Click += new System.EventHandler(this.btnDrawRegion_Click);
            // 
            // seObjZ
            // 
            this.seObjZ.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObjZ.Location = new System.Drawing.Point(30, 356);
            this.seObjZ.Name = "seObjZ";
            this.seObjZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjZ.Size = new System.Drawing.Size(163, 22);
            this.seObjZ.StyleController = this.layoutControl1;
            this.seObjZ.TabIndex = 8;
            this.seObjZ.EditValueChanged += new System.EventHandler(this.seObjZ_EditValueChanged);
            // 
            // seObjY
            // 
            this.seObjY.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObjY.Location = new System.Drawing.Point(31, 330);
            this.seObjY.Name = "seObjY";
            this.seObjY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjY.Size = new System.Drawing.Size(162, 22);
            this.seObjY.StyleController = this.layoutControl1;
            this.seObjY.TabIndex = 7;
            this.seObjY.EditValueChanged += new System.EventHandler(this.seObjY_EditValueChanged);
            // 
            // seObjX
            // 
            this.seObjX.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObjX.Location = new System.Drawing.Point(30, 304);
            this.seObjX.Name = "seObjX";
            this.seObjX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjX.Size = new System.Drawing.Size(163, 22);
            this.seObjX.StyleController = this.layoutControl1;
            this.seObjX.TabIndex = 6;
            this.seObjX.EditValueChanged += new System.EventHandler(this.seObjX_EditValueChanged);
            // 
            // seObserZ
            // 
            this.seObserZ.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObserZ.Location = new System.Drawing.Point(30, 182);
            this.seObserZ.Name = "seObserZ";
            this.seObserZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserZ.Size = new System.Drawing.Size(163, 22);
            this.seObserZ.StyleController = this.layoutControl1;
            this.seObserZ.TabIndex = 3;
            this.seObserZ.EditValueChanged += new System.EventHandler(this.seObserZ_EditValueChanged);
            // 
            // seObserY
            // 
            this.seObserY.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObserY.Location = new System.Drawing.Point(31, 156);
            this.seObserY.Name = "seObserY";
            this.seObserY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserY.Size = new System.Drawing.Size(162, 22);
            this.seObserY.StyleController = this.layoutControl1;
            this.seObserY.TabIndex = 2;
            this.seObserY.EditValueChanged += new System.EventHandler(this.seObserY_EditValueChanged);
            // 
            // seObserX
            // 
            this.seObserX.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObserX.Location = new System.Drawing.Point(30, 130);
            this.seObserX.Name = "seObserX";
            this.seObserX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserX.Size = new System.Drawing.Size(163, 22);
            this.seObserX.StyleController = this.layoutControl1;
            this.seObserX.TabIndex = 1;
            this.seObserX.EditValueChanged += new System.EventHandler(this.seObserX_EditValueChanged);
            // 
            // seObserZOff
            // 
            this.seObserZOff.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.seObserZOff.Location = new System.Drawing.Point(97, 208);
            this.seObserZOff.Name = "seObserZOff";
            this.seObserZOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserZOff.Size = new System.Drawing.Size(96, 22);
            this.seObserZOff.StyleController = this.layoutControl1;
            this.seObserZOff.TabIndex = 4;
            this.seObserZOff.EditValueChanged += new System.EventHandler(this.seObserZOff_EditValueChanged);
            // 
            // seAngle
            // 
            this.seAngle.EditValue = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.seAngle.Location = new System.Drawing.Point(97, 60);
            this.seAngle.Name = "seAngle";
            this.seAngle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seAngle.Properties.MaxValue = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.seAngle.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seAngle.Size = new System.Drawing.Size(96, 22);
            this.seAngle.StyleController = this.layoutControl1;
            this.seAngle.TabIndex = 0;
            this.seAngle.EditValueChanged += new System.EventHandler(this.seAngle_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.emptySpaceItem4,
            this.layoutControlItem13,
            this.layoutControlGroup2,
            this.layoutControlGroup4,
            this.layoutControlItem12});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(207, 502);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "观察点";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem9,
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 96);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(207, 174);
            this.layoutControlGroup3.Text = "观察点";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.seObserX;
            this.layoutControlItem3.CustomizationFormText = "X:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem3.Text = "X:";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.seObserY;
            this.layoutControlItem4.CustomizationFormText = "Y:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem4.Text = "Y:";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(12, 14);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.seObserZ;
            this.layoutControlItem5.CustomizationFormText = "Z:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem5.Text = "Z:";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnFlyToObserver;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seObserZOff;
            this.layoutControlItem2.CustomizationFormText = "观察点高度(m)：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem2.Text = "偏移高度(m)：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(80, 14);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 470);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(207, 32);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnAnalyse;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 444);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem13.Text = "layoutControlItem13";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "参数设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(207, 70);
            this.layoutControlGroup2.Text = "参数设置";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.seAngle;
            this.layoutControlItem1.CustomizationFormText = "水平张角(°):";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem1.Text = "水平张角(°):";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "目标点";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 270);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(207, 174);
            this.layoutControlGroup4.Text = "目标点";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.seObjX;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem6.Text = "X:";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.seObjY;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem7.Text = "Y:";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(12, 14);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.seObjZ;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem8.Text = "Z:";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnFlyToObjective;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.seObjZOff;
            this.layoutControlItem11.CustomizationFormText = "偏移高度(m)：";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem11.Text = "偏移高度(m)：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.btnDrawRegion;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(308, 190);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // UCViewshedAnalyse
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCViewshedAnalyse";
            this.Size = new System.Drawing.Size(207, 502);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seObjZOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seAngle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private AxRenderControl _3DControl;
        private IGeometryFactory _geoFactory;
        public UCViewshedAnalyse()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) { this.Enabled = false; return; }
            this._3DControl = app.Current3DMapControl;
            this._geoFactory = new GeometryFactory();
            this._3DControl.HighlightHelper.Color = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
            this._3DControl.HighlightHelper.VisibleMask = 0;
        }

        public void RestoreEnv()
        {
            this._3DControl.HighlightHelper.VisibleMask = 0;
            this._3DControl.HighlightHelper.SetRegion(null);
            ClearViewshed();
            DetachEvent();
        }
        private IViewshed _vs;
        private void ClearViewshed()
        {
            if (this._vs != null)
            {
                this._3DControl.ObjectManager.DeleteObject(this._vs.Guid);
                this._vs = null;
            }
        }
        private void DrawRegion()
        {
            ClearViewshed();
            this._3DControl.HighlightHelper.VisibleMask = 0;
            this._3DControl.HighlightHelper.SetRegion(null);
            IPoint ptStart = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptStart.X = (double)this.seObserX.Value;
            ptStart.Y = (double)this.seObserY.Value;
            ptStart.Z = (double)this.seObserZ.Value + (double)this.seObserZOff.Value;
            IPoint ptEnd = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptEnd.X = (double)this.seObjX.Value;
            ptEnd.Y = (double)this.seObjY.Value;
            ptEnd.Z = (double)this.seObjZ.Value + (double)this.seObjZOff.Value;
            double HorizontalAngle = (double)this.seAngle.Value;
            this._3DControl.HighlightHelper.VisibleMask = 1;
            this._3DControl.HighlightHelper.SetSectorRegion(ptStart, ptEnd, HorizontalAngle);
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
        private bool bStart;
        void _3DControl_RcMouseClickSelect(object sender, _IRenderControlEvents_RcMouseClickSelectEvent e)
        {
            if (e.intersectPoint == null) return;
            if (e.eventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                if (!bStart)
                {
                    this.seObserX.Text = e.intersectPoint.X.ToString();
                    this.seObserY.Text = e.intersectPoint.Y.ToString();
                    this.seObserZ.Text = e.intersectPoint.Z.ToString();
                    bStart = true;
                }
                else
                {
                    this.seObjX.Text = e.intersectPoint.X.ToString();
                    this.seObjY.Text = e.intersectPoint.Y.ToString();
                    this.seObjZ.Text = e.intersectPoint.Z.ToString();
                    bStart = false;
                    DrawRegion();
                }
            }
            else if (e.eventSender == gviMouseSelectMode.gviMouseSelectMove)
            {
                if (bStart)
                {
                    this.seObjX.Text = e.intersectPoint.X.ToString();
                    this.seObjY.Text = e.intersectPoint.Y.ToString();
                    this.seObjZ.Text = e.intersectPoint.Z.ToString();
                    DrawRegion();
                }
            }
        }

        private void btnDrawRegion_Click(object sender, EventArgs e)
        {
            AttachEvent();
        }

        private void btnFlyToObserver_Click(object sender, EventArgs e)
        {
            IPoint ptStart = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptStart.X = (double)this.seObserX.Value;
            ptStart.Y = (double)this.seObserY.Value;
            ptStart.Z = (double)this.seObserZ.Value + (double)this.seObserZOff.Value;
            IPoint ptEnd = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptEnd.X = (double)this.seObjX.Value;
            ptEnd.Y = (double)this.seObjY.Value;
            ptEnd.Z = (double)this.seObjZ.Value + (double)this.seObjZOff.Value;
            IEulerAngle ang = this._3DControl.Camera.GetAimingAngles2(ptStart, ptEnd);
            this._3DControl.Camera.SetCamera2(ptStart, ang, gviSetCameraFlags.gviSetCameraNoFlags);

        }

        private void btnFlyToObjective_Click(object sender, EventArgs e)
        {
            IPoint ptStart = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptStart.X = (double)this.seObserX.Value;
            ptStart.Y = (double)this.seObserY.Value;
            ptStart.Z = (double)this.seObserZ.Value + (double)this.seObserZOff.Value;
            IPoint ptEnd = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptEnd.X = (double)this.seObjX.Value;
            ptEnd.Y = (double)this.seObjY.Value;
            ptEnd.Z = (double)this.seObjZ.Value + (double)this.seObjZOff.Value;
            IEulerAngle ang = this._3DControl.Camera.GetAimingAngles2(ptEnd, ptStart);
            this._3DControl.Camera.SetCamera2(ptEnd, ang, gviSetCameraFlags.gviSetCameraNoFlags);

        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                WaitForm.Start("正在分析...", "请稍后");

                IPoint ptStart = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                ptStart.X = (double)this.seObserX.Value;
                ptStart.Y = (double)this.seObserY.Value;
                ptStart.Z = (double)this.seObserZ.Value + (double)this.seObserZOff.Value;
                IPoint ptEnd = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                ptEnd.X = (double)this.seObjX.Value;
                ptEnd.Y = (double)this.seObjY.Value;
                ptEnd.Z = (double)this.seObjZ.Value + (double)this.seObjZOff.Value;
                double HorizontalAngle = (double)this.seAngle.Value;
                ClearViewshed();
                this._vs = this._3DControl.ObjectManager.CreateViewshed(ptStart, this._3DControl.ProjectTree.RootID);
                IEulerAngle ang = this._3DControl.Camera.GetAimingAngles2(ptStart, ptEnd); ;
                //ang.Set(ang.Heading, 0, ang.Roll);
                this._vs.AspectRatio = 1;
                this._vs.DisplayMode = gviTVDisplayMode.gviTVShowPicture;
                this._vs.Angle = ang;
                this._vs.FarClip = Math.Sqrt((ptStart.X - ptEnd.X) * (ptStart.X - ptEnd.X) + (ptStart.Y - ptEnd.Y) * (ptStart.Y - ptEnd.Y)
                    + (ptStart.Z - ptEnd.Z) * (ptStart.Z - ptEnd.Z));
                this._vs.FieldOfView = HorizontalAngle;
                this._3DControl.HighlightHelper.VisibleMask = 0;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private void seAngle_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObserX_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObserY_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObserZ_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObserZOff_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObjX_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObjY_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObjZ_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

        private void seObjZOff_EditValueChanged(object sender, EventArgs e)
        {
            DrawRegion();
        }

    }
}
