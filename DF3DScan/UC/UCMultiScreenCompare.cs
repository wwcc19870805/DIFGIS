using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;

namespace DF3DScan.UC
{
    public class UCMultiScreenCompare : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnQuad;
        private SimpleButton btnQuadH;
        private SimpleButton btnL2R1;
        private SimpleButton btnL1R2;
        private SimpleButton btnT1M1B1;
        private SimpleButton btnL1M1R1;
        private SimpleButton btnT1B1SingleFrustum;
        private SimpleButton btnL1R1SingleFrustum;
        private SimpleButton btnT1B1;
        private SimpleButton btnL1R1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private CheckEdit ceLink;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
    
        public UCMultiScreenCompare()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnQuad = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuadH = new DevExpress.XtraEditors.SimpleButton();
            this.btnL2R1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnL1R2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnT1M1B1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnL1M1R1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnT1B1SingleFrustum = new DevExpress.XtraEditors.SimpleButton();
            this.btnL1R1SingleFrustum = new DevExpress.XtraEditors.SimpleButton();
            this.btnT1B1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnL1R1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ceLink = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLink.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceLink);
            this.layoutControl1.Controls.Add(this.btnQuad);
            this.layoutControl1.Controls.Add(this.btnQuadH);
            this.layoutControl1.Controls.Add(this.btnL2R1);
            this.layoutControl1.Controls.Add(this.btnL1R2);
            this.layoutControl1.Controls.Add(this.btnT1M1B1);
            this.layoutControl1.Controls.Add(this.btnL1M1R1);
            this.layoutControl1.Controls.Add(this.btnT1B1SingleFrustum);
            this.layoutControl1.Controls.Add(this.btnL1R1SingleFrustum);
            this.layoutControl1.Controls.Add(this.btnT1B1);
            this.layoutControl1.Controls.Add(this.btnL1R1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(243, 457);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnQuad
            // 
            this.btnQuad.Location = new System.Drawing.Point(123, 226);
            this.btnQuad.Name = "btnQuad";
            this.btnQuad.Size = new System.Drawing.Size(106, 22);
            this.btnQuad.StyleController = this.layoutControl1;
            this.btnQuad.TabIndex = 13;
            this.btnQuad.Text = "2×2";
            this.btnQuad.Click += new System.EventHandler(this.btnQuad_Click);
            // 
            // btnQuadH
            // 
            this.btnQuadH.Location = new System.Drawing.Point(14, 226);
            this.btnQuadH.Name = "btnQuadH";
            this.btnQuadH.Size = new System.Drawing.Size(105, 22);
            this.btnQuadH.StyleController = this.layoutControl1;
            this.btnQuadH.TabIndex = 12;
            this.btnQuadH.Text = "水平";
            this.btnQuadH.Click += new System.EventHandler(this.btnQuadH_Click);
            // 
            // btnL2R1
            // 
            this.btnL2R1.Location = new System.Drawing.Point(122, 156);
            this.btnL2R1.Name = "btnL2R1";
            this.btnL2R1.Size = new System.Drawing.Size(107, 22);
            this.btnL2R1.StyleController = this.layoutControl1;
            this.btnL2R1.TabIndex = 11;
            this.btnL2R1.Text = "左2右1";
            this.btnL2R1.Click += new System.EventHandler(this.btnL2R1_Click);
            // 
            // btnL1R2
            // 
            this.btnL1R2.Location = new System.Drawing.Point(14, 156);
            this.btnL1R2.Name = "btnL1R2";
            this.btnL1R2.Size = new System.Drawing.Size(104, 22);
            this.btnL1R2.StyleController = this.layoutControl1;
            this.btnL1R2.TabIndex = 10;
            this.btnL1R2.Text = "左1右2";
            this.btnL1R2.Click += new System.EventHandler(this.btnL1R2_Click);
            // 
            // btnT1M1B1
            // 
            this.btnT1M1B1.Location = new System.Drawing.Point(122, 130);
            this.btnT1M1B1.Name = "btnT1M1B1";
            this.btnT1M1B1.Size = new System.Drawing.Size(107, 22);
            this.btnT1M1B1.StyleController = this.layoutControl1;
            this.btnT1M1B1.TabIndex = 9;
            this.btnT1M1B1.Text = "上中下";
            this.btnT1M1B1.Click += new System.EventHandler(this.btnT1M1B1_Click);
            // 
            // btnL1M1R1
            // 
            this.btnL1M1R1.Location = new System.Drawing.Point(14, 130);
            this.btnL1M1R1.Name = "btnL1M1R1";
            this.btnL1M1R1.Size = new System.Drawing.Size(104, 22);
            this.btnL1M1R1.StyleController = this.layoutControl1;
            this.btnL1M1R1.TabIndex = 8;
            this.btnL1M1R1.Text = "左中右";
            this.btnL1M1R1.Click += new System.EventHandler(this.btnL1M1R1_Click);
            // 
            // btnT1B1SingleFrustum
            // 
            this.btnT1B1SingleFrustum.Location = new System.Drawing.Point(122, 60);
            this.btnT1B1SingleFrustum.Name = "btnT1B1SingleFrustum";
            this.btnT1B1SingleFrustum.Size = new System.Drawing.Size(107, 22);
            this.btnT1B1SingleFrustum.StyleController = this.layoutControl1;
            this.btnT1B1SingleFrustum.TabIndex = 7;
            this.btnT1B1SingleFrustum.Text = "上下(连续)";
            this.btnT1B1SingleFrustum.Click += new System.EventHandler(this.btnT1B1SingleFrustum_Click);
            // 
            // btnL1R1SingleFrustum
            // 
            this.btnL1R1SingleFrustum.Location = new System.Drawing.Point(14, 60);
            this.btnL1R1SingleFrustum.Name = "btnL1R1SingleFrustum";
            this.btnL1R1SingleFrustum.Size = new System.Drawing.Size(104, 22);
            this.btnL1R1SingleFrustum.StyleController = this.layoutControl1;
            this.btnL1R1SingleFrustum.TabIndex = 6;
            this.btnL1R1SingleFrustum.Text = "左右(连续)";
            this.btnL1R1SingleFrustum.Click += new System.EventHandler(this.btnL1R1SingleFrustum_Click);
            // 
            // btnT1B1
            // 
            this.btnT1B1.Location = new System.Drawing.Point(122, 34);
            this.btnT1B1.Name = "btnT1B1";
            this.btnT1B1.Size = new System.Drawing.Size(107, 22);
            this.btnT1B1.StyleController = this.layoutControl1;
            this.btnT1B1.TabIndex = 5;
            this.btnT1B1.Text = "上下";
            this.btnT1B1.Click += new System.EventHandler(this.btnT1B1_Click);
            // 
            // btnL1R1
            // 
            this.btnL1R1.Location = new System.Drawing.Point(14, 34);
            this.btnL1R1.Name = "btnL1R1";
            this.btnL1R1.Size = new System.Drawing.Size(104, 22);
            this.btnL1R1.StyleController = this.layoutControl1;
            this.btnL1R1.TabIndex = 4;
            this.btnL1R1.Text = "左右";
            this.btnL1R1.Click += new System.EventHandler(this.btnL1R1_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem4,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlItem11});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(243, 457);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 285);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(299, 172);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "双屏模式";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(243, 96);
            this.layoutControlGroup2.Text = "双屏模式";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnL1R1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnT1B1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(108, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnL1R1SingleFrustum;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnT1B1SingleFrustum;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(108, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "三屏模式";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 96);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(243, 96);
            this.layoutControlGroup3.Text = "三屏模式";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnL1M1R1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnT1M1B1;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(108, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnL1R2;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnL2R1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(108, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(111, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "四屏模式";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 192);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(243, 70);
            this.layoutControlGroup4.Text = "四屏模式";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnQuadH;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnQuad;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(109, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(110, 26);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // ceLink
            // 
            this.ceLink.Location = new System.Drawing.Point(2, 264);
            this.ceLink.Name = "ceLink";
            this.ceLink.Properties.Caption = "视口联动";
            this.ceLink.Size = new System.Drawing.Size(239, 19);
            this.ceLink.StyleController = this.layoutControl1;
            this.ceLink.TabIndex = 14;
            this.ceLink.CheckedChanged += new System.EventHandler(this.ceLink_CheckedChanged);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ceLink;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 262);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(243, 23);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // UCMultiScreenCompare
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCMultiScreenCompare";
            this.Size = new System.Drawing.Size(243, 457);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLink.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);

        }

        public void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportSinglePerspective;
        }

        private void btnL1R1_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportL1R1;
            ceLink_CheckedChanged(null, null);
        }

        private void btnT1B1_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportT1B1;
            ceLink_CheckedChanged(null, null);
        }

        private void btnL1R1SingleFrustum_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportL1R1SingleFrustum;
            ceLink_CheckedChanged(null, null);
        }

        private void btnT1B1SingleFrustum_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportT1B1SingleFrustum;
            ceLink_CheckedChanged(null, null);
        }

        private void btnL1M1R1_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportL1M1R1;
            ceLink_CheckedChanged(null, null);
        }

        private void btnT1M1B1_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportT1M1B1;
            ceLink_CheckedChanged(null, null);
        }

        private void btnL1R2_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportL1R2;
            ceLink_CheckedChanged(null, null);
        }

        private void btnL2R1_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportL2R1;
            ceLink_CheckedChanged(null, null);
        }

        private void btnQuadH_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportQuadH;
            ceLink_CheckedChanged(null, null);
        }

        private void btnQuad_Click(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            app.Current3DMapControl.Viewport.ViewportMode = Gvitech.CityMaker.RenderControl.gviViewportMode.gviViewportQuad;
            ceLink_CheckedChanged(null, null);
        }

        private void ceLink_CheckedChanged(object sender, EventArgs e)
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this.ceLink.Checked) app.Current3DMapControl.Viewport.CameraViewBindMask = gviViewportMask.gviViewAllNormalView;
            else app.Current3DMapControl.Viewport.CameraViewBindMask = gviViewportMask.gviViewNone;
        }
    }
}
