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
using DF3DControl.Base;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using DF3DData.Class;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using Gvitech.CityMaker.Math;
using DFDataConfig.Class;
using DFWinForms.Class;
using DFCommon.Class;

namespace DF3DPlan.UC
{
    public class UCHeightControl : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnOutputExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private SimpleButton btnDrawPolygon;
        private SpinEdit seLimitHeight;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private SimpleButton btnDrawRect;
        private SimpleButton btnDrawCircle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private SimpleButton btnAnalyse;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;

        private DataTable _dt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private List<Guid> _listRender;

        public UCHeightControl()
        {
            InitializeComponent();
            this._dt = new DataTable();
            this._dt.TableName = "限高结果表";
            
            this._dt.Columns.AddRange(new DataColumn[] { 
                new DataColumn("Name"), new DataColumn("geo", typeof(object)), new DataColumn("fcName"), new DataColumn("fc", typeof(object)),
                new DataColumn("fid"),new DataColumn("OverHeight") , new DataColumn("TerrainHeight"), new DataColumn("Height"), 
                new DataColumn("Address"), new DataColumn("Contact")
            });

            this.gridControl1.DataSource = this._dt;
            this._listRender = new List<Guid>();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCHeightControl));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnAnalyse = new DevExpress.XtraEditors.SimpleButton();
            this.btnDrawRect = new DevExpress.XtraEditors.SimpleButton();
            this.btnDrawCircle = new DevExpress.XtraEditors.SimpleButton();
            this.btnOutputExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDrawPolygon = new DevExpress.XtraEditors.SimpleButton();
            this.seLimitHeight = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seLimitHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnAnalyse);
            this.layoutControl1.Controls.Add(this.btnDrawRect);
            this.layoutControl1.Controls.Add(this.btnDrawCircle);
            this.layoutControl1.Controls.Add(this.btnOutputExcel);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnDrawPolygon);
            this.layoutControl1.Controls.Add(this.seLimitHeight);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(292, 554);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Location = new System.Drawing.Point(2, 106);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(288, 22);
            this.btnAnalyse.StyleController = this.layoutControl1;
            this.btnAnalyse.TabIndex = 10;
            this.btnAnalyse.Text = "分    析";
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // btnDrawRect
            // 
            this.btnDrawRect.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawRect.Image")));
            this.btnDrawRect.Location = new System.Drawing.Point(97, 72);
            this.btnDrawRect.Name = "btnDrawRect";
            this.btnDrawRect.Size = new System.Drawing.Size(94, 30);
            this.btnDrawRect.StyleController = this.layoutControl1;
            this.btnDrawRect.TabIndex = 9;
            this.btnDrawRect.Text = "矩形";
            this.btnDrawRect.Click += new System.EventHandler(this.btnDrawRect_Click);
            // 
            // btnDrawCircle
            // 
            this.btnDrawCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawCircle.Image")));
            this.btnDrawCircle.Location = new System.Drawing.Point(2, 72);
            this.btnDrawCircle.Name = "btnDrawCircle";
            this.btnDrawCircle.Size = new System.Drawing.Size(91, 30);
            this.btnDrawCircle.StyleController = this.layoutControl1;
            this.btnDrawCircle.TabIndex = 8;
            this.btnDrawCircle.Text = "圆形";
            this.btnDrawCircle.Click += new System.EventHandler(this.btnDrawCircle_Click);
            // 
            // btnOutputExcel
            // 
            this.btnOutputExcel.Location = new System.Drawing.Point(14, 518);
            this.btnOutputExcel.Name = "btnOutputExcel";
            this.btnOutputExcel.Size = new System.Drawing.Size(264, 22);
            this.btnOutputExcel.StyleController = this.layoutControl1;
            this.btnOutputExcel.TabIndex = 7;
            this.btnOutputExcel.Text = "导出Excel";
            this.btnOutputExcel.Click += new System.EventHandler(this.btnOutputExcel_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(14, 164);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(264, 350);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn1,
            this.gridColumn9,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn10});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "定位";
            this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 31;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "定位", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "定位", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "名称";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 81;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "地面高(m)";
            this.gridColumn9.FieldName = "TerrainHeight";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 64;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "高度(m)";
            this.gridColumn3.FieldName = "Height";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 56;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "超高(m)";
            this.gridColumn4.FieldName = "OverHeight";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 53;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "地址";
            this.gridColumn2.FieldName = "Address";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            this.gridColumn2.Width = 137;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "联系人";
            this.gridColumn5.FieldName = "Contact";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 278;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "fcName";
            this.gridColumn6.FieldName = "fcName";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "fid";
            this.gridColumn7.FieldName = "fid";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "fc";
            this.gridColumn10.FieldName = "fc";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // btnDrawPolygon
            // 
            this.btnDrawPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawPolygon.Image")));
            this.btnDrawPolygon.Location = new System.Drawing.Point(195, 72);
            this.btnDrawPolygon.Name = "btnDrawPolygon";
            this.btnDrawPolygon.Size = new System.Drawing.Size(95, 30);
            this.btnDrawPolygon.StyleController = this.layoutControl1;
            this.btnDrawPolygon.TabIndex = 5;
            this.btnDrawPolygon.Text = "多边形";
            this.btnDrawPolygon.Click += new System.EventHandler(this.btnDrawPolygon_Click);
            // 
            // seLimitHeight
            // 
            this.seLimitHeight.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.seLimitHeight.Location = new System.Drawing.Point(89, 34);
            this.seLimitHeight.Name = "seLimitHeight";
            this.seLimitHeight.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.seLimitHeight.Properties.Appearance.Options.UseBackColor = true;
            this.seLimitHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seLimitHeight.Size = new System.Drawing.Size(189, 22);
            this.seLimitHeight.StyleController = this.layoutControl1;
            this.seLimitHeight.TabIndex = 4;
            this.seLimitHeight.EditValueChanged += new System.EventHandler(this.seLimitHeight_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem2,
            this.layoutControlGroup3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(292, 554);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "参数设置";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(292, 70);
            this.layoutControlGroup2.Text = "参数设置";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.seLimitHeight;
            this.layoutControlItem1.CustomizationFormText = "限高高度(m):";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(268, 26);
            this.layoutControlItem1.Text = "限高高度(m):";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnDrawPolygon;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(193, 70);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(99, 34);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "超限结果";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 130);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(292, 424);
            this.layoutControlGroup3.Text = "超限结果";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(268, 354);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOutputExcel;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 354);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(268, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnDrawCircle;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 70);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(95, 34);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnDrawRect;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(95, 70);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(98, 34);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnAnalyse;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(292, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // UCHeightControl
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCHeightControl";
            this.Size = new System.Drawing.Size(292, 554);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seLimitHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

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
                this._drawTool = null;
            }
        }

        private DrawTool _drawTool;

        public void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            this._dt.Rows.Clear();

            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            if (this._renderBox != null)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(this._renderBox.Guid);
                this._renderBox = null;
            }
            foreach (Guid g in this._listRender)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(g);
            }
            this._listRender.Clear();

            app.Current3DMapControl.HighlightHelper.VisibleMask = 0;
            app.Current3DMapControl.HighlightHelper.SetRegion(null);
        }

        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.End();
                this._drawTool = null;
            }

            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Circle);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void btnDrawRect_Click(object sender, EventArgs e)
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.End();
                this._drawTool = null;
            }

            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Rectangle);
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.Start();
            }
        }

        private void btnDrawPolygon_Click(object sender, EventArgs e)
        {
            Clear();
            if (this._drawTool != null)
            {
                this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                this._drawTool.End();
                this._drawTool = null;
            }

            this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Polygon);
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
            if (this._drawTool != null &&  this._drawTool.GetGeo() != null)
            {
                DrawControlRegion();
            }
        }

        private IRenderMultiPolygon _renderBox;
        private void DrawControlRegion()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null || !app.Current3DMapControl.Terrain.IsRegistered) return;
            if (this._renderBox != null)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(this._renderBox.Guid);
                this._renderBox = null;
            }

            IGeometry geo = this._drawTool.GetGeo();
            if (geo == null || geo.GeometryType != gviGeometryType.gviGeometryPolygon) return;
            IPolygon polygon = geo as IPolygon;
            double terrainHeight = 0.0;
            if (app.Current3DMapControl.Terrain.IsRegistered)
            {
                terrainHeight = app.Current3DMapControl.Terrain.GetElevation(polygon.Centroid.X, polygon.Centroid.Y, gviGetElevationType.gviGetElevationFromDatabase);
            }
            double height = double.Parse(this.seLimitHeight.Value.ToString()) + terrainHeight;

            IGeometryFactory geoFact = new GeometryFactoryClass();

            IMultiPolygon multiPolygon = geoFact.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            IPolygon polygonZ = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
            {
                IPoint ptTemp = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                IPoint pt1 = polygon.ExteriorRing.GetPoint(i);
                ptTemp.X = pt1.X;
                ptTemp.Y = pt1.Y;
                ptTemp.Z = height;
                polygonZ.ExteriorRing.AppendPoint(ptTemp);
            }
            multiPolygon.AddPolygon(polygonZ);
            for (int i = 0; i < polygon.ExteriorRing.PointCount - 1; i++)
            {
                IPoint pt0 = polygon.ExteriorRing.GetPoint(i);
                IPoint pt1 = polygon.ExteriorRing.GetPoint(i + 1);
                IPoint pt00 = polygonZ.ExteriorRing.GetPoint(i);
                IPoint pt11 = polygonZ.ExteriorRing.GetPoint(i + 1);
                IPolygon polygon1 = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon1.ExteriorRing.AppendPoint(pt0);
                polygon1.ExteriorRing.AppendPoint(pt1);
                polygon1.ExteriorRing.AppendPoint(pt11);
                polygon1.ExteriorRing.AppendPoint(pt00);
                multiPolygon.AddPolygon(polygon1);
            }
            IPoint ptL0 = polygon.ExteriorRing.GetPoint(0);
            IPoint ptL1 = polygon.ExteriorRing.GetPoint(polygon.ExteriorRing.PointCount - 1);
            IPoint ptL00 = polygonZ.ExteriorRing.GetPoint(0);
            IPoint ptL11 = polygonZ.ExteriorRing.GetPoint(polygon.ExteriorRing.PointCount - 1);
            IPolygon polygon2 = geoFact.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            polygon2.ExteriorRing.AppendPoint(ptL00);
            polygon2.ExteriorRing.AppendPoint(ptL11);
            polygon2.ExteriorRing.AppendPoint(ptL1);
            polygon2.ExteriorRing.AppendPoint(ptL0);
            multiPolygon.AddPolygon(polygon2);

            ISurfaceSymbol ss1 = new SurfaceSymbolClass();
            ss1.Color = 0x88FFFFFF;
            ICurveSymbol cs1 = new CurveSymbolClass();
            cs1.Color = 0xffffffff;
            ss1.BoundarySymbol = cs1;
            this._renderBox = app.Current3DMapControl.ObjectManager.CreateRenderMultiPolygon(multiPolygon, ss1, app.Current3DMapControl.ProjectTree.RootID);

            app.Current3DMapControl.HighlightHelper.Color = 0xAAFF0000;
            app.Current3DMapControl.HighlightHelper.SetRegion(geo);
            app.Current3DMapControl.HighlightHelper.MinZ = height;
            app.Current3DMapControl.HighlightHelper.VisibleMask = 15;
        }

        private void seLimitHeight_EditValueChanged(object sender, EventArgs e)
        {
            DrawControlRegion();
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                this._dt.Rows.Clear();
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null || !app.Current3DMapControl.Terrain.IsRegistered) return;
                foreach (Guid g in this._listRender)
                {
                    app.Current3DMapControl.ObjectManager.DeleteObject(g);
                }
                this._listRender.Clear();

                WaitForm.Start("正在分析...", "请稍后");

                if (this._drawTool == null) return;
                IGeometry geo = this._drawTool.GetGeo();
                if (geo == null || geo.GeometryType != gviGeometryType.gviGeometryPolygon) return;
                IPolygon polygon = geo as IPolygon;
                IPoint pt = polygon.ExteriorRing.Midpoint;
                double height =  double.Parse(this.seLimitHeight.Value.ToString());

                List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName(new string[] { "Building", "Structure" });
                if (list != null && list.Count != 0)
                {
                    foreach (DF3DFeatureClass dffc in list)
                    {
                        IFeatureClass fc = dffc.GetFeatureClass();
                        IFeatureLayer fl = dffc.GetFeatureLayer();
                        FacilityClass fac = dffc.GetFacilityClass();
                        if (fl != null)
                        {
                            if (fl.VisibleMask == gviViewportMask.gviViewNone) continue;
                        }
                        if (fc != null)
                        {
                            IFieldInfoCollection fiCol = fc.GetFields();
                            int indexGeo = fiCol.IndexOf("Geometry");
                            if (indexGeo == -1) continue;
                            int indexFid = fiCol.IndexOf(fc.FidFieldName);
                            if (indexFid == -1) continue;
                            int indexName = -1;
                            int indexAddress = -1;
                            int indexContact = -1;
                            if (fac != null)
                            {
                                indexName = fiCol.IndexOf(fac.GetFieldInfoNameBySystemName("Name"));
                                indexAddress = fiCol.IndexOf(fac.GetFieldInfoNameBySystemName("Address"));
                                indexContact = fiCol.IndexOf(fac.GetFieldInfoNameBySystemName("Contact"));
                            }

                            IResourceManager resManager = fc.FeatureDataSet as IResourceManager;
                            if (resManager == null) continue;

                            IRowBuffer row = null;
                            IFdeCursor cursor = null;
                            try
                            {
                                ISpatialFilter filter = new SpatialFilter();
                                filter.Geometry = geo;
                                filter.GeometryField = "Geometry";
                                filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                                cursor = fc.Search(filter, true);
                                while ((row = cursor.NextRow()) != null)
                                {
                                    IGeometry geoRow = row.GetValue(indexGeo) as IGeometry;
                                    if (geoRow != null && geoRow.GeometryType == gviGeometryType.gviGeometryModelPoint)
                                    {
                                        IModelPoint mp = geoRow as IModelPoint;
                                        IVector3 v3 = mp.AsMatrix().GetScale();
                                        //string name = mp.ModelName;
                                        //IModel m = resManager.GetModel(name);
                                        IEnvelope env = mp.ModelEnvelope;
                                        double terrainHeight = 0.0;
                                        if (app.Current3DMapControl.Terrain.IsRegistered)
                                        {
                                            terrainHeight = app.Current3DMapControl.Terrain.GetElevation(mp.X, mp.Y, gviGetElevationType.gviGetElevationFromDatabase);
                                        }
                                        double mpHeigth = env.Depth * v3.Z;
                                        if (mpHeigth > height)
                                        {
                                            DataRow dr = this._dt.NewRow();
                                            if (indexName != -1)
                                            {
                                                dr["Name"] = row.GetValue(indexName);
                                            }
                                            dr["fcName"] = string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName;
                                            dr["fid"] = row.GetValue(indexFid);
                                            dr["Geo"] = row.GetValue(indexGeo);
                                            if (indexAddress != -1) dr["Address"] = row.GetValue(indexAddress);
                                            if (indexContact != -1) dr["Contact"] = row.GetValue(indexContact);
                                            dr["TerrainHeight"] = terrainHeight.ToString("0.00");
                                            dr["Height"] = env.MaxZ.ToString("0.00");
                                            dr["OverHeight"] = (mpHeigth - height).ToString("0.00");
                                            dr["fc"] = fc;
                                            this._dt.Rows.Add(dr);
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
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                WaitForm.Stop();
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.Controls.EditorButton btn = e.Button;
            switch (btn.Caption)
            {
                case "定位":
                    DF3DApplication app = DF3DApplication.Application;
                    if (app == null || app.Current3DMapControl == null) return;
                    int focusedRowHandle = this.gridView1.FocusedRowHandle;
                    if (focusedRowHandle == -1) return;
                    DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
                    if (dr["geo"] != null && dr["Name"] != null && dr["fcName"] != null && dr["fc"] != null)
                    {
                        ISurfaceSymbol ss = new SurfaceSymbolClass();
                        ss.Color = 0xcc00ff00;
                        ICurveSymbol cs = new CurveSymbolClass();
                        cs.Color = 0xff00ff00;
                        cs.Width = -5;
                        ss.BoundarySymbol = cs;
                        ISimplePointSymbol ps = new SimplePointSymbol();
                        ps.Size = SystemInfo.Instance.SymbolSize;
                        ps.FillColor = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
                        IGeometry objGeo = dr["geo"] as IGeometry;
                        if (objGeo.GeometryType == gviGeometryType.gviGeometryModelPoint)
                        {
                            IModelPoint mp = objGeo as IModelPoint;
                            IModelPointSymbol mps = new ModelPointSymbol();
                            mps.SetResourceDataSet((dr["fc"] as IFeatureClass).FeatureDataSet);
                            mps.Color = 0xffffff00;
                            IRenderModelPoint rMP = app.Current3DMapControl.ObjectManager.CreateRenderModelPoint(mp, mps, app.Current3DMapControl.ProjectTree.RootID);
                            rMP.Glow(8000);
                            app.Current3DMapControl.ObjectManager.DelayDelete(rMP.Guid, 8000);

                            IGeometryFactory geoFact = new GeometryFactory();
                            IPoint pt = geoFact.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pt.X = mp.X; pt.Y = mp.Y; pt.Z = mp.Z;

                            ITableLabel tl = DrawTool.CreateTableLabel1(1);
                            tl.TitleText = dr["fcName"].ToString();
                            tl.SetRecord(0, 0, dr["Name"].ToString());
                            tl.Position = pt;
                          
                            this._listRender.Add(tl.Guid);

                            app.Current3DMapControl.Camera.FlyToObject(tl.Guid, gviActionCode.gviActionFlyTo);
                        }
                    }
                    break;
            }

        }

        private void btnOutputExcel_Click(object sender, EventArgs e)
        {
            if (this._dt == null || this._dt.Rows.Count == 0) return;
            SaveFileDialog dialog = new SaveFileDialog
                {
                    RestoreDirectory = true,
                    Filter = "Excel Files (*.xls)|*.xls"
                };
            if (DialogResult.OK == dialog.ShowDialog())
            {
                string fileName = dialog.FileName;

                DataTable dt = this._dt.Copy();
                dt.TableName = this.seLimitHeight.Value.ToString("0.00") + "m限高结果表";
                dt.Columns["Name"].Caption = "名称";
                dt.Columns["fcName"].Caption = "类型";
                dt.Columns["OverHeight"].Caption = "超高(m)";
                dt.Columns["TerrainHeight"].Caption = "地面高(m)";
                dt.Columns["Height"].Caption = "高度(m)";
                dt.Columns["Address"].Caption = "地址";
                dt.Columns["Contact"].Caption = "联系人";
                dt.Columns.Remove("fid");
                dt.Columns.Remove("fc");
                dt.Columns.Remove("geo");

                WaitForm.Start("正在导出...", "请稍后");
                bool bExport = ExcelOper.ExportExcel(dt, fileName);
                if (bExport)
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("导出成功", "提示");
                }
                else
                {
                    WaitForm.Stop();
                    XtraMessageBox.Show("导出失败", "提示");
                }
            }
        }
        
    }
}
