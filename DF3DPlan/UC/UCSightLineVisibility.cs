using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DDraw;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Controls;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Math;

namespace DF3DPlan.UC
{
    public class UCSightLineVisibility : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private SpinEdit seObjZOff;
        private SpinEdit seObserZOff;
        private SpinEdit seObjZ;
        private SpinEdit seObjY;
        private SpinEdit seObjX;
        private SpinEdit seObserZ;
        private SpinEdit seObserY;
        private SpinEdit seObserX;
        private SimpleButton btnDrawLine;
        private SimpleButton btnAnalyse;
        private SimpleButton btnFlyToObjective;
        private SimpleButton btnFlyToOberserve;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.seObjZOff = new DevExpress.XtraEditors.SpinEdit();
            this.seObserZOff = new DevExpress.XtraEditors.SpinEdit();
            this.seObjZ = new DevExpress.XtraEditors.SpinEdit();
            this.seObjY = new DevExpress.XtraEditors.SpinEdit();
            this.seObjX = new DevExpress.XtraEditors.SpinEdit();
            this.seObserZ = new DevExpress.XtraEditors.SpinEdit();
            this.seObserY = new DevExpress.XtraEditors.SpinEdit();
            this.seObserX = new DevExpress.XtraEditors.SpinEdit();
            this.btnDrawLine = new DevExpress.XtraEditors.SimpleButton();
            this.btnAnalyse = new DevExpress.XtraEditors.SimpleButton();
            this.btnFlyToObjective = new DevExpress.XtraEditors.SimpleButton();
            this.btnFlyToOberserve = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seObjZOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seObjZOff);
            this.layoutControl1.Controls.Add(this.seObserZOff);
            this.layoutControl1.Controls.Add(this.seObjZ);
            this.layoutControl1.Controls.Add(this.seObjY);
            this.layoutControl1.Controls.Add(this.seObjX);
            this.layoutControl1.Controls.Add(this.seObserZ);
            this.layoutControl1.Controls.Add(this.seObserY);
            this.layoutControl1.Controls.Add(this.seObserX);
            this.layoutControl1.Controls.Add(this.btnDrawLine);
            this.layoutControl1.Controls.Add(this.btnAnalyse);
            this.layoutControl1.Controls.Add(this.btnFlyToObjective);
            this.layoutControl1.Controls.Add(this.btnFlyToOberserve);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(182, 448);
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
            this.seObjZOff.Location = new System.Drawing.Point(97, 312);
            this.seObjZOff.Name = "seObjZOff";
            this.seObjZOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjZOff.Size = new System.Drawing.Size(71, 22);
            this.seObjZOff.StyleController = this.layoutControl1;
            this.seObjZOff.TabIndex = 9;
            this.seObjZOff.EditValueChanged += new System.EventHandler(this.seObjZOff_EditValueChanged);
            // 
            // seObserZOff
            // 
            this.seObserZOff.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.seObserZOff.Location = new System.Drawing.Point(97, 138);
            this.seObserZOff.Name = "seObserZOff";
            this.seObserZOff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserZOff.Size = new System.Drawing.Size(71, 22);
            this.seObserZOff.StyleController = this.layoutControl1;
            this.seObserZOff.TabIndex = 4;
            this.seObserZOff.EditValueChanged += new System.EventHandler(this.seObserZOff_EditValueChanged);
            // 
            // seObjZ
            // 
            this.seObjZ.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seObjZ.Location = new System.Drawing.Point(30, 286);
            this.seObjZ.Name = "seObjZ";
            this.seObjZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjZ.Size = new System.Drawing.Size(138, 22);
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
            this.seObjY.Location = new System.Drawing.Point(31, 260);
            this.seObjY.Name = "seObjY";
            this.seObjY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjY.Size = new System.Drawing.Size(137, 22);
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
            this.seObjX.Location = new System.Drawing.Point(30, 234);
            this.seObjX.Name = "seObjX";
            this.seObjX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObjX.Size = new System.Drawing.Size(138, 22);
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
            this.seObserZ.Location = new System.Drawing.Point(30, 112);
            this.seObserZ.Name = "seObserZ";
            this.seObserZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserZ.Size = new System.Drawing.Size(138, 22);
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
            this.seObserY.Location = new System.Drawing.Point(31, 86);
            this.seObserY.Name = "seObserY";
            this.seObserY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserY.Size = new System.Drawing.Size(137, 22);
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
            this.seObserX.Location = new System.Drawing.Point(30, 60);
            this.seObserX.Name = "seObserX";
            this.seObserX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seObserX.Size = new System.Drawing.Size(138, 22);
            this.seObserX.StyleController = this.layoutControl1;
            this.seObserX.TabIndex = 1;
            this.seObserX.EditValueChanged += new System.EventHandler(this.seObserX_EditValueChanged);
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Location = new System.Drawing.Point(2, 2);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(178, 22);
            this.btnDrawLine.StyleController = this.layoutControl1;
            this.btnDrawLine.TabIndex = 0;
            this.btnDrawLine.Text = "绘制通视线";
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Location = new System.Drawing.Point(2, 376);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(178, 22);
            this.btnAnalyse.StyleController = this.layoutControl1;
            this.btnAnalyse.TabIndex = 11;
            this.btnAnalyse.Text = "分    析";
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // btnFlyToObjective
            // 
            this.btnFlyToObjective.Location = new System.Drawing.Point(14, 338);
            this.btnFlyToObjective.Name = "btnFlyToObjective";
            this.btnFlyToObjective.Size = new System.Drawing.Size(154, 22);
            this.btnFlyToObjective.StyleController = this.layoutControl1;
            this.btnFlyToObjective.TabIndex = 10;
            this.btnFlyToObjective.Text = "飞向目标点";
            this.btnFlyToObjective.Click += new System.EventHandler(this.btnFlyToObjective_Click);
            // 
            // btnFlyToOberserve
            // 
            this.btnFlyToOberserve.Location = new System.Drawing.Point(14, 164);
            this.btnFlyToOberserve.Name = "btnFlyToOberserve";
            this.btnFlyToOberserve.Size = new System.Drawing.Size(154, 22);
            this.btnFlyToOberserve.StyleController = this.layoutControl1;
            this.btnFlyToOberserve.TabIndex = 5;
            this.btnFlyToOberserve.Text = "飞向观察点";
            this.btnFlyToOberserve.Click += new System.EventHandler(this.btnFlyToOberserve_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem3,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(182, 448);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 400);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(182, 48);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "观察点";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem11});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(182, 174);
            this.layoutControlGroup2.Text = "观察点";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnFlyToOberserve;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.seObserX;
            this.layoutControlItem1.CustomizationFormText = "X:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem1.Text = "X:";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seObserY;
            this.layoutControlItem2.CustomizationFormText = "Y:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem2.Text = "Y:";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(12, 14);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.seObserZ;
            this.layoutControlItem3.CustomizationFormText = "Z:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem3.Text = "Z:";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.seObserZOff;
            this.layoutControlItem11.CustomizationFormText = "高度偏移(m)：";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem11.Text = "高度偏移(m)：";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "目标点";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem12});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 200);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(182, 174);
            this.layoutControlGroup3.Text = "目标点";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnFlyToObjective;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.seObjX;
            this.layoutControlItem5.CustomizationFormText = "X:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem5.Text = "X:";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.seObjY;
            this.layoutControlItem6.CustomizationFormText = "Y:";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem6.Text = "Y:";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(12, 14);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.seObjZ;
            this.layoutControlItem7.CustomizationFormText = "Z:";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem7.Text = "Z:";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(11, 14);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.seObjZOff;
            this.layoutControlItem12.CustomizationFormText = "高度偏移(m)：";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem12.Text = "高度偏移(m)：";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(80, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnAnalyse;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 374);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnDrawLine;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(182, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // UCSightLineVisibility
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCSightLineVisibility";
            this.Size = new System.Drawing.Size(182, 448);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seObjZOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObjX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seObserX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        private AxRenderControl _3DControl;
        private IGeometryFactory _geoFactory;
        private IRenderPolyline _rLine;
        private List<Guid> _listRes;
        public UCSightLineVisibility()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) { this.Enabled = false; return; }
            this._3DControl = app.Current3DMapControl;
            this._geoFactory = new GeometryFactory();
            this._listRes = new List<Guid>();
        }

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
            ClearSightLine();
            ClearRes();
        }
        private void ClearSightLine()
        {
            if (this._rLine != null)
            {
                this._3DControl.ObjectManager.DeleteObject(this._rLine.Guid);
                this._rLine = null;
            }
        }
        public void ClearRes()
        {
            foreach (Guid g in this._listRes)
            {
                this._3DControl.ObjectManager.DeleteObject(g);
            }
            this._listRes.Clear();
        }

        private DrawTool _drawTool;
        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Line);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }

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
            if (this._drawTool != null && this._drawTool.GeoType == DrawType.Line && this._drawTool.GetGeo() != null)
            {
                IPolyline line1 = this._drawTool.GetGeo() as IPolyline;
                IPoint ptStart1 = line1.StartPoint;
                this.seObserX.Text = ptStart1.X.ToString();
                this.seObserY.Text = ptStart1.Y.ToString();
                this.seObserZ.Text = ptStart1.Z.ToString();
                IPoint ptEnd1 = line1.EndPoint;
                this.seObjX.Text = ptEnd1.X.ToString();
                this.seObjY.Text = ptEnd1.Y.ToString();
                this.seObjZ.Text = ptEnd1.Z.ToString();
                this._drawTool.Close();

                DrawSightLine();
            }
        }

        private void DrawSightLine()
        {
            ClearSightLine();
            ClearRes();
            IPoint ptStart = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptStart.X = (double)this.seObserX.Value;
            ptStart.Y = (double)this.seObserY.Value;
            ptStart.Z = (double)this.seObserZ.Value + (double)this.seObserZOff.Value;
            IPoint ptEnd = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            ptEnd.X = (double)this.seObjX.Value;
            ptEnd.Y = (double)this.seObjY.Value;
            ptEnd.Z = (double)this.seObjZ.Value + (double)this.seObjZOff.Value;
            IPolyline line = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            line.AppendPoint(ptStart);
            line.AppendPoint(ptEnd);
            ICurveSymbol cs = new CurveSymbolClass();
            cs.Color = 0xff00ff00;
            cs.Width = -1;
            this._rLine = this._3DControl.ObjectManager.CreateRenderPolyline(line, cs, this._3DControl.ProjectTree.RootID);
        }

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
        }

        private void btnFlyToOberserve_Click(object sender, EventArgs e)
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

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                ClearRes();
                WaitForm.Start("正在分析...", "请稍后");
                if (this._rLine == null) return;
                List<DF3DFeatureClass> list = DF3DFeatureClassManager.Instance.GetAllFeatureClass();
                if (list == null || list.Count == 0) return;
                
                IPolyline line = this._rLine.GetFdeGeometry() as IPolyline;
                ILine l = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryLine, gviVertexAttribute.gviVertexAttributeZ) as ILine;
                l.StartPoint = line.StartPoint;
                l.EndPoint = line.EndPoint;

                ISpatialFilter filter = new SpatialFilter();
                filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
                filter.GeometryField = "Geometry";

                List<InterPt> listInterPts = new List<InterPt>();

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
                    this._rLine.VisibleMask = gviViewportMask.gviViewNone;
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
                    this._rLine.VisibleMask = gviViewportMask.gviViewNone;
                    ICurveSymbol csS = new CurveSymbolClass();
                    csS.Color = 0xff0000ff;
                    csS.Width = -2;
                    IRenderPolyline rLine = this._3DControl.ObjectManager.CreateRenderPolyline(line, csS, this._3DControl.ProjectTree.RootID);
                    this._listRes.Add(rLine.Guid);
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                WaitForm.Stop();
            }
        }

        private void seObserX_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObserY_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObserZ_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObserZOff_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObjX_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObjY_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObjZ_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }

        private void seObjZOff_EditValueChanged(object sender, EventArgs e)
        {
            DrawSightLine();
        }
    }
}
