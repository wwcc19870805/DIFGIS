using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DF3DControl.Base;
using Gvitech.CityMaker.Math;
using DevExpress.XtraEditors;
using Gvitech.CityMaker.RenderControl;

namespace DF3DPlan.Frm
{
    public class FrmSunSimulation : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEdit1;
        private DevExpress.XtraEditors.SpinEdit seTilt;
        private DevExpress.XtraEditors.SpinEdit seHeading;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

        private static FrmSunSimulation instance = null;
        private static readonly object syncRoot = new object();

        public static FrmSunSimulation Instance
        {
            get
            {
                if (FrmSunSimulation.instance == null)
                {
                    lock (syncRoot)
                    {
                        if (FrmSunSimulation.instance == null)
                        {
                            FrmSunSimulation.instance = new FrmSunSimulation();
                        }
                    }
                }
                return FrmSunSimulation.instance;
            }
        }

        private DF3DApplication app;
        private FrmSunSimulation()
        {
            InitializeComponent();
            app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null)
            {
                this.Enabled = false;
                return;
            }
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.colorPickEdit1 = new DevExpress.XtraEditors.ColorPickEdit();
            this.seTilt = new DevExpress.XtraEditors.SpinEdit();
            this.seHeading = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTilt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seHeading.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.trackBarControl1);
            this.layoutControl1.Controls.Add(this.colorPickEdit1);
            this.layoutControl1.Controls.Add(this.seTilt);
            this.layoutControl1.Controls.Add(this.seHeading);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(293, 103);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = 220;
            this.trackBarControl1.Location = new System.Drawing.Point(65, 28);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBarControl1.Properties.Maximum = 255;
            this.trackBarControl1.Properties.TickFrequency = 5;
            this.trackBarControl1.Size = new System.Drawing.Size(226, 45);
            this.trackBarControl1.StyleController = this.layoutControl1;
            this.trackBarControl1.TabIndex = 7;
            this.trackBarControl1.Value = 220;
            this.trackBarControl1.EditValueChanged += new System.EventHandler(this.trackBarControl1_EditValueChanged);
            // 
            // colorPickEdit1
            // 
            this.colorPickEdit1.EditValue = System.Drawing.Color.Black;
            this.colorPickEdit1.Location = new System.Drawing.Point(65, 2);
            this.colorPickEdit1.Name = "colorPickEdit1";
            this.colorPickEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEdit1.Size = new System.Drawing.Size(226, 22);
            this.colorPickEdit1.StyleController = this.layoutControl1;
            this.colorPickEdit1.TabIndex = 6;
            this.colorPickEdit1.EditValueChanged += new System.EventHandler(this.colorPickEdit1_EditValueChanged);
            // 
            // seTilt
            // 
            this.seTilt.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTilt.Location = new System.Drawing.Point(215, 77);
            this.seTilt.Name = "seTilt";
            this.seTilt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTilt.Properties.IsFloatValue = false;
            this.seTilt.Properties.Mask.EditMask = "N00";
            this.seTilt.Properties.MaxValue = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.seTilt.Size = new System.Drawing.Size(76, 22);
            this.seTilt.StyleController = this.layoutControl1;
            this.seTilt.TabIndex = 5;
            this.seTilt.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.seTilt_EditValueChanging);
            // 
            // seHeading
            // 
            this.seHeading.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seHeading.Location = new System.Drawing.Point(65, 77);
            this.seHeading.Name = "seHeading";
            this.seHeading.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seHeading.Properties.IsFloatValue = false;
            this.seHeading.Properties.Mask.EditMask = "N00";
            this.seHeading.Properties.MaxValue = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.seHeading.Properties.MinValue = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.seHeading.Size = new System.Drawing.Size(83, 22);
            this.seHeading.StyleController = this.layoutControl1;
            this.seHeading.TabIndex = 4;
            this.seHeading.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.seHeading_EditValueChanging);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(293, 103);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.seHeading;
            this.layoutControlItem1.CustomizationFormText = "方位角：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(150, 28);
            this.layoutControlItem1.Text = "方位角：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.colorPickEdit1;
            this.layoutControlItem3.CustomizationFormText = "阴影颜色：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(293, 26);
            this.layoutControlItem3.Text = "阴影颜色：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seTilt;
            this.layoutControlItem2.CustomizationFormText = "高度角：";
            this.layoutControlItem2.Location = new System.Drawing.Point(150, 75);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(143, 28);
            this.layoutControlItem2.Text = "高度角：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.trackBarControl1;
            this.layoutControlItem4.CustomizationFormText = "不透明度：";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(293, 49);
            this.layoutControlItem4.Text = "不透明度：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // FrmSunSimulation
            // 
            this.ClientSize = new System.Drawing.Size(293, 103);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(5, 180);
            this.Name = "FrmSunSimulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "日照模拟";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSunSimulation_FormClosed);
            this.Load += new System.EventHandler(this.FrmSunSimulation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTilt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seHeading.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }


        private void seHeading_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (app == null || app.Current3DMapControl == null) return;
            IEulerAngle ang = new EulerAngle();
            ang.Set(Convert.ToDouble(this.seHeading.EditValue), Convert.ToDouble(this.seTilt.EditValue), 0);
            app.Current3DMapControl.SunConfig.SetSunEuler(ang);
        }

        private void seTilt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (app == null || app.Current3DMapControl == null) return;
            IEulerAngle ang = new EulerAngle();
            ang.Set(Convert.ToDouble(this.seHeading.EditValue), Convert.ToDouble(this.seTilt.EditValue), 0);
            app.Current3DMapControl.SunConfig.SetSunEuler(ang);
        }

        private void colorPickEdit1_EditValueChanged(object sender, EventArgs e)
        {
            System.Drawing.Color color = this.colorPickEdit1.Color;
            this.colorPickEdit1.Color = System.Drawing.Color.FromArgb(this.trackBarControl1.Value, color.R, color.G, color.B);
            System.Drawing.Color color1 = System.Drawing.Color.FromArgb(this.trackBarControl1.Value, color.R, color.G, color.B);
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.SunConfig.ShadowColor = (uint)color1.ToArgb();
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            System.Drawing.Color color = this.colorPickEdit1.Color;
            this.colorPickEdit1.Color = System.Drawing.Color.FromArgb(this.trackBarControl1.Value, color.R, color.G, color.B);

            if (app == null || app.Current3DMapControl == null) return;
            System.Drawing.Color color1 = System.Drawing.Color.FromArgb(this.trackBarControl1.Value, color.R, color.G, color.B);
            app.Current3DMapControl.SunConfig.ShadowColor = (uint)color1.ToArgb();
        }

        private void FrmSunSimulation_Load(object sender, EventArgs e)
        {
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.SunConfig.SunCalculateMode = gviSunCalculateMode.gviSunModeUserDefined;
            System.Drawing.Color color = this.colorPickEdit1.Color;
            System.Drawing.Color color1 = System.Drawing.Color.FromArgb(this.trackBarControl1.Value, color.R, color.G, color.B);
            app.Current3DMapControl.SunConfig.ShadowColor = (uint)color1.ToArgb();
            IEulerAngle ang = new EulerAngle();
            ang.Set(Convert.ToDouble(this.seHeading.EditValue), Convert.ToDouble(this.seTilt.EditValue), 0);
            app.Current3DMapControl.SunConfig.SetSunEuler(ang);
            app.Current3DMapControl.SunConfig.EnableShadow(0, true);
        }

        private void FrmSunSimulation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.SunConfig.EnableShadow(0, false);
            instance = null;
        }
    }
}
