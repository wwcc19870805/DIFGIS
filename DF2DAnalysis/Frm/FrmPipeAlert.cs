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
using ESRI.ArcGIS.Controls;
using DevExpress.XtraGrid.Views.Base;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
/*using Excel = Microsoft.Office.Interop.Excel;*/
using DF2DPipe.Class;
using DevExpress.XtraGrid;
using DF2DAnalysis.Commands;
/*using Microsoft.Office.Interop.Excel;*/

namespace DF2DAnalysis.Frm
{
    public partial class FrmPipeAlert : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private IMapControl2 m_pMapControl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private SimpleButton simpleButton2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private SimpleButton simpleButton3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Dictionary<IFeatureClass, DataTable> dict;
        private SimpleButton simpleButton4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private string statsType = "pipeline";
        private TrackBarControl trackBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DataTable table;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;

        private double Str = CmdPipeAlert.yearXtime;

       
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.trackBarControl1);
            this.layoutControl1.Controls.Add(this.simpleButton4);
            this.layoutControl1.Controls.Add(this.simpleButton3);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(890, 461);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 38);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "2年内超限"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "5年内超限"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "已经超限")});
            this.radioGroup1.Size = new System.Drawing.Size(866, 39);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 11;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = null;
            this.trackBarControl1.Location = new System.Drawing.Point(120, 38);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBarControl1.Size = new System.Drawing.Size(323, 45);
            this.trackBarControl1.StyleController = this.layoutControl1;
            this.trackBarControl1.TabIndex = 10;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(672, 427);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(65, 22);
            this.simpleButton4.StyleController = this.layoutControl1;
            this.simpleButton4.TabIndex = 9;
            this.simpleButton4.Text = "快捷查询";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(741, 427);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(59, 22);
            this.simpleButton3.StyleController = this.layoutControl1;
            this.simpleButton3.TabIndex = 8;
            this.simpleButton3.Text = "统计图表";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(804, 427);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(35, 22);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "导出";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(843, 427);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(35, 22);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "关闭";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 81);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(866, 342);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "要素id";
            this.gridColumn1.FieldName = "FeatureID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "管线材质";
            this.gridColumn2.FieldName = "Matter";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "状态";
            this.gridColumn3.FieldName = "UseState";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "已使用时间（单位：年）";
            this.gridColumn4.FieldName = "UseTime";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "管线到期时间；超出年限时间（单位：年）";
            this.gridColumn5.FieldName = "TimeAlert";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 7;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "要素";
            this.gridColumn6.FieldName = "FeatureClass";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "开始使用时间(单位：年)";
            this.gridColumn7.FieldName = "StartTime";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "权属单位";
            this.gridColumn8.FieldName = "OwnerBy";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "所在道路";
            this.gridColumn9.FieldName = "Proad";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "管径规格";
            this.gridColumn10.FieldName = "Standard";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "管线长度";
            this.gridColumn11.FieldName = "PipeLength";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 8;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(12, 12);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(866, 22);
            this.comboBoxEdit1.StyleController = this.layoutControl1;
            this.comboBoxEdit1.TabIndex = 4;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.trackBarControl1;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(435, 49);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(105, 14);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(890, 461);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.comboBoxEdit1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(870, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 69);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(870, 346);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(831, 415);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButton2;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(792, 415);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(39, 26);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton3;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(729, 415);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 415);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(660, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton4;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(660, 415);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.radioGroup1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(870, 43);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // FrmPipeAlert
            // 
            this.ClientSize = new System.Drawing.Size(890, 461);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmPipeAlert";
            this.Text = "管线预警";
            this.Load += new System.EventHandler(this.FrmPipeAlert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        public FrmPipeAlert(Dictionary<IFeatureClass, DataTable> dt, IMapControl2 current2DMapControl)
        {
            InitializeComponent();
            dict = dt;
            m_pMapControl = current2DMapControl;
        }

        private void FrmPipeAlert_Load(object sender, EventArgs e)
        {
           
            this.gridControl1.MainView = this.gridView1;
            gridView1.Columns["TimeAlert"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns["TimeAlert"].SummaryItem.DisplayFormat = "合计：{0}";
            gridView1.ExpandAllGroups();
            
            foreach (IFeatureClass fc in dict.Keys)
            {
                this.comboBoxEdit1.Properties.Items.Add(fc.AliasName);
            }
            this.comboBoxEdit1.SelectedIndex = 0; 
// .              thisradioGroup1.EditValue = 0;
//               this.radioGroup1.SelectedIndex = 0;
            
                 
       
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    DataTable dt = GetDataTable(this.comboBoxEdit1.SelectedItem.ToString());
                    int n = 0;
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                       
                        DataTable dtNew;

                        DataRow[] drr = dt.Select("(UseTime<='20')and(TimeAlert<=2)");
//                         DataRow[] drr4 = dt.Select("(TimeAlert>2)and(TimeAlert<=5)");
//                         DataRow[] drr5 = dt.Select("TimeAlert>5");
                        dtNew = dt.Clone();
                       
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                                Console.WriteLine(n++);
                            }
                        this.gridControl1.DataSource = dtNew;
                    }

                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        DataTable dtNew1;
                        DataRow[] drr1 = dt.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;   
                    }
                    else if (this.radioGroup1.SelectedIndex == 2)
                    {
                        DataTable dtNew2;
                        DataRow[] drr2 = dt.Select("(UseTime>'20')and(TimeAlert)>'5'");
                        dtNew2 = dt.Clone();
                        int n1 = 0;
                        foreach (DataRow row in drr2)
                        {                            
                            dtNew2.ImportRow(row);                           
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;
                    }
                    break;
                case 1:
                    DataTable dt1 = GetDataTable(this.comboBoxEdit1.Text);
                    this.gridControl1.DataSource = dt1;
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        DataTable dtNew;

                        DataRow[] drr = dt1.Select("(UseTime<='20')and(TimeAlert>'0')and(TimeAlert<='2')");
                        dtNew = dt1.Clone();
                        for (int j = 0; j < drr.Length; j++)
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew;
                    }

                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        DataTable dtNew1;
                        DataRow[] drr1 = dt1.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt1.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;
                    }
                    else if (this.radioGroup1.SelectedIndex == 2)
                    {
                        DataTable dtNew2;
                        DataRow[] drr2 = dt1.Select("(UseTime>'20')or(TimeAlert>'5')");
                        dtNew2 = dt1.Clone();
                       
                        foreach (DataRow row in drr2)
                        {
                            dtNew2.ImportRow(row);
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;
                    }



//                     this.gridControl1.MainView = this.gridView1;
//                     gridView1.Columns["TimeAlert"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
//                     gridView1.Columns["TimeAlert"].SummaryItem.DisplayFormat = "合计：{0}";
//                     gridView1.ExpandAllGroups();
                    break;
                case 2:
                    DataTable dt2 = GetDataTable(this.comboBoxEdit1.Text);
                    this.gridControl1.DataSource = dt2;

                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        DataTable dtNew;

                        DataRow[] drr = dt2.Select("(UseTime<='20')and(TimeAlert>'0')and(TimeAlert<='2')");
                        dtNew = dt2.Clone();
                        for (int j = 0; j < drr.Length; j++)
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew;
                    }

                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        DataTable dtNew1;
                        DataRow[] drr1 = dt2.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt2.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;
                    }
                    else if (this.radioGroup1.SelectedIndex == 2)
                    {
                        DataTable dtNew2;
                        DataRow[] drr2 = dt2.Select("(UseTime>'20')or(TimeAlert>'5')");
                        dtNew2 = dt2.Clone();
                        
                        foreach (DataRow row in drr2)
                        {
                            dtNew2.ImportRow(row);
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;
                    }
//                     this.gridControl1.MainView = this.gridView1;
//                     gridView1.Columns["TimeAlert"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
//                     gridView1.Columns["TimeAlert"].SummaryItem.DisplayFormat = "合计：{0}";
//                     gridView1.ExpandAllGroups();
                    break;
                case 3:
                    DataTable dt3 = GetDataTable(this.comboBoxEdit1.Text);
                    this.gridControl1.DataSource = dt3;
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        DataTable dtNew;

                        DataRow[] drr = dt3.Select("(UseTime<='20')and(TimeAlert>'0')and(TimeAlert<='2')");
                        dtNew = dt3.Clone();
                        for (int j = 0; j < drr.Length; j++)
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew;
                    }

                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        DataTable dtNew1;
                        DataRow[] drr1 = dt3.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt3.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;
                    }
                    else if (this.radioGroup1.SelectedIndex == 2)
                    {
                        DataTable dtNew2;
                        DataRow[] drr2 = dt3.Select("(UseTime>'20')or(TimeAlert>'5')");
                        dtNew2 = dt3.Clone();
                       
                        foreach (DataRow row in drr2)
                        {
                            dtNew2.ImportRow(row);
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;
                    }
//                     this.gridControl1.MainView = this.gridView1;
//                     gridView1.Columns["TimeAlert"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
//                     gridView1.Columns["TimeAlert"].SummaryItem.DisplayFormat = "合计：{0}";
//                     gridView1.ExpandAllGroups();
                    break;
                case 4:
                    DataTable dt4 = GetDataTable(this.comboBoxEdit1.Text);
                    this.gridControl1.DataSource = dt4;
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        DataTable dtNew;

                        DataRow[] drr = dt4.Select("(UseTime<='20')and(TimeAlert>'0')and(TimeAlert<='2')");
                        dtNew = dt4.Clone();
                        for (int j = 0; j < drr.Length; j++)
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew;
                    }

                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        DataTable dtNew1;
                        DataRow[] drr1 = dt4.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt4.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;
                    }
                    else if (this.radioGroup1.SelectedIndex == 2)
                    {
                        DataTable dtNew2;
                        DataRow[] drr2 = dt4.Select("(UseTime>'20')or(TimeAlert>'5')");
                        dtNew2 = dt4.Clone();
                      
                        foreach (DataRow row in drr2)
                        {
                            dtNew2.ImportRow(row);
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;
                    }
//                     this.gridControl1.MainView = this.gridView1;
//                     gridView1.Columns["TimeAlert"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
//                     gridView1.Columns["TimeAlert"].SummaryItem.DisplayFormat = "合计：{0}";
//                     gridView1.ExpandAllGroups();
                    break;
                case 5:
                    DataTable dt5 = GetDataTable(this.comboBoxEdit1.Text);
                    this.gridControl1.DataSource = dt5;
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        DataTable dtNew;

                        DataRow[] drr = dt5.Select("(UseTime<='20')and(TimeAlert>'0')and(TimeAlert<='2')");
                        dtNew = dt5.Clone();
                        for (int j = 0; j < drr.Length; j++)
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew;
                    }

                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        DataTable dtNew1;
                        DataRow[] drr1 = dt5.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt5.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;
                    }
                    else if (this.radioGroup1.SelectedIndex == 2)
                    {
                        DataTable dtNew2;
                        DataRow[] drr2 = dt5.Select("(UseTime>'20')or(TimeAlert>'5')");
                        dtNew2 = dt5.Clone();

                        foreach (DataRow row in drr2)
                        {
                            dtNew2.ImportRow(row);
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;
                       
                    }
//                     this.gridControl1.MainView = this.gridView1;
//                     gridView1.Columns["TimeAlert"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
//                     gridView1.Columns["TimeAlert"].SummaryItem.DisplayFormat = "合计：{0}";
//                     gridView1.ExpandAllGroups();
                    break;

            }


           
            
        }

        private DataTable GetDataTable(string fcname)
        {
            foreach (IFeatureClass fc in dict.Keys)
            {
                if (fc.AliasName == fcname) return dict[fc];
            }
            return null;

        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            if (this.gridView1.FocusedRowHandle == -1) return;
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (dr["FeatureClass"] != null && dr["FeatureClass"] is IFeatureClass)
            {
                IFeatureClass fc = dr["FeatureClass"] as IFeatureClass;
                //声明ColumnView对象
                var columnView = (ColumnView)gridControl1.FocusedView;
                //得到选中的行索引
                int id = columnView.FocusedRowHandle;
                if (id < 0) return;
                string SID = gridView1.GetRowCellValue(id, "FeatureID").ToString();//在GridControl中读出要素的ID
                IFeature pFture = null;
                IFeatureCursor pFeaCursor;
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = "OBJECTID= " + SID;
                pFeaCursor = fc.Search(pFilter, true);
                if (pFeaCursor != null)
                {
                    pFture = pFeaCursor.NextFeature();

                    if (pFture != null)
                    {
                        MoveToFeature(m_pMapControl, pFture);//定位
                        ShowSelectionFeature(m_pMapControl, pFture);//高亮
                    }
                }
            }

        }


        #region  //定位要素

        private void MoveToFeature(IMapControl2 current2DMapControl, IFeature pFeature)
        {
            IEnvelope pEnvelope = new EnvelopeClass();
            IEnvelope pGeoEnv;
            IPoint pPoint = new PointClass();
            IGeometry pGeometry = pFeature.Shape;
            pGeoEnv = pGeometry.Envelope;
            pPoint.X = (pGeoEnv.XMin + pGeoEnv.XMax) / 2;
            pPoint.Y = (pGeoEnv.YMin + pGeoEnv.YMax) / 2;
            double xWidth;
            double yWidth;
            xWidth = (pGeoEnv.XMax - pGeoEnv.XMin);
            yWidth = (pGeoEnv.XMax - pGeoEnv.XMin);
            pEnvelope.XMin = pPoint.X - (xWidth / 2);
            pEnvelope.XMax = pPoint.X + (xWidth / 2);
            pEnvelope.YMin = pPoint.Y - (yWidth / 2);
            pEnvelope.YMax = pPoint.Y + (yWidth / 2);
            pEnvelope.Expand(5, 5, false);
            current2DMapControl.Extent = pEnvelope;
            current2DMapControl.ActiveView.Refresh();

        }
        #endregion


        #region //高亮显示选中要素
        /// <summary>
        /// 高亮显示选择
        /// </summary>
        /// <returns>选择的要素高亮显示</returns>
        public static void ShowSelectionFeature(IMapControl2 current2DMapControl, IFeature pFeature)
        {
            //分点、线、面，给新的symbol
            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
                pSymbol = (IMarkerSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new MarkerElementClass();
                pElement.Geometry = pFeature.Shape;
                IMarkerElement pMarkerElement;
                pMarkerElement = (IMarkerElement)pElement;
                pMarkerElement.Symbol = pSymbol;

                current2DMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                pSymbol = (ILineSymbol)GetDefaultSymbol(pFeature.Shape.GeometryType);

                IElement pElement = new LineElementClass();
                pElement.Geometry = pFeature.Shape;
                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;

                current2DMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);

            }
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IElement pElement = new LineElementClass();
                IPolyline pLine;
                pLine = GetPolygonBoundary((IPolygon)pFeature.Shape);
                pElement.Geometry = pLine;

                ILineSymbol pSymbol = new SimpleLineSymbolClass();
                pSymbol = (ILineSymbol)GetDefaultSymbol(pLine.GeometryType);
                ILineElement pLineElement;
                pLineElement = (ILineElement)pElement;
                pLineElement.Symbol = pSymbol;
                current2DMapControl.ActiveView.GraphicsContainer.AddElement(pElement, 0);
            }

            current2DMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, current2DMapControl.ActiveView.Extent);//视图刷新
        }
        #endregion
        #region //得到一个Polygon对象的轮廓线
        /// <summary>
        /// 得到一个Polygon对象的轮廓线
        /// </summary>
        /// <param name="pPolygon">一个Polygon对象</param>
        /// <returns>一个Polyline对象</returns>
        public static IPolyline GetPolygonBoundary(IPolygon pPolygon)
        {
            //通过ITopologicalOperator 对象转换成线
            ITopologicalOperator pTopo;
            IPolyline pPolyline;

            pTopo = (ITopologicalOperator)pPolygon;
            pPolyline = (IPolyline)pTopo.Boundary;
            return pPolyline;
        }
        #endregion

        #region 根据Geometry类型生成一个默认符号
        /// <summary>
        /// 根据Geometry类型生成一个默认符号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISymbol GetDefaultSymbol(esriGeometryType type)
        {
            ISymbol sym = null;

            IRgbColor mCor = new RgbColorClass();
            mCor.Green = 255;
            mCor.Blue = 255;
            IRgbColor lCor = new RgbColorClass();
            lCor.Red = 0;
            lCor.Green = 0;
            lCor.Blue = 255;
            IRgbColor fCor = new RgbColorClass();
            fCor.Green = 255;
            fCor.Blue = 255;

            IMarkerSymbol mark = new SimpleMarkerSymbolClass();
            mark.Color = mCor;
            mark.Size = 16;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 4;
            line.Color = lCor;

            IFillSymbol fill = new SimpleFillSymbolClass();
            fill.Color = fCor;
            fill.Outline = line;

            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    sym = (ISymbol)mark;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    sym = (ISymbol)line;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    sym = (ISymbol)fill;
                    break;
            }

            return sym;
        }
        #endregion

        //关闭按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();

        }

        //导出为excel
        private void simpleButton2_Click(object sender, EventArgs e)

        {

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Excel Files(*.xls)|*.xls|Excel Files(*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = saveFileDialog.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string filePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                bool suc = ExportToExcel((DataTable)this.gridControl1.DataSource, localFilePath);
                if (suc)
                {
                    XtraMessageBox.Show("导出成功！");
                }
            }

        }

        private bool ExportToExcel(DataTable dt, string path)
        {
            bool succeed = false;
            if (dt != null)
            {
                Microsoft.Office.Interop.Excel.Application xlApp = null;
                try
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                    object oMissing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;
                    xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                    //xlSheet.Name = dt.TableName;
                    int rowIndex = 1;
                    int colIndex = 1;
                    int colCount = dt.Columns.Count;
                    int rowCount = dt.Rows.Count;

                    for (int i = 0; i < colCount; i++)
                    {
                        string str = dt.Columns[i].ColumnName;
                        switch (str)
                        {
                            

                            case "Matter":
                                str = "管线材质";
                                break;

                            case "UseState":
                                str = "状态";
                                break;

                            case "StartTime":
                                str = "开始使用时间";
                                break;

                            case "UseTime":
                                str = "使用年限";
                                break;

                            case "TimeAlert":
                                str = "年限预警";
                                break;

                            case "OwnerBy":
                                str = "权属单位";
                                break;

                            case "Proad":
                                str = "所属道路";
                                break;

                            case "Standard":
                                str = "管径规格";
                                break;


                        }

                        xlSheet.Cells[rowIndex, colIndex] = str;
                        colIndex++;
                    }
                    //xlSheet.get_Range(xlSheet.Cells[rowIndex, 1], xlSheet.Cells[rowIndex, colCount]).Font.Bold = true;
                    //xlSheet.get_Range(xlSheet.Cells[rowIndex, 1], xlSheet.Cells[rowCount + 1, colCount]).Font.Name = "Arial";
                    //xlSheet.get_Range(xlSheet.Cells[rowIndex, 1], xlSheet.Cells[rowCount + 1, colCount]).Font.Size = "10";
                    rowIndex++;

                    for (int i = 0; i < rowCount; i++)
                    {
                        colIndex = 1;
                        for (int j = 0; j < colCount; j++)
                        {
                            xlSheet.Cells[rowIndex, colIndex] = dt.Rows[i][j].ToString();
                            colIndex++;
                        }
                        rowIndex++;
                    }

                  /*  MergeCell_Second(ref xlSheet, 2, dt.Rows.Count, "A");*/
                    //MergeCell_Second(ref xlSheet, 2, dt.Rows.Count, "B");
                    //MergeCell_Second(ref xlSheet, 2, dt.Rows.Count, "E");
                    xlSheet.Cells.EntireColumn.AutoFit();
                    xlApp.DisplayAlerts = false;
                    xlBook.SaveCopyAs(path);
                    xlBook.Close(false, null, null);
                    xlApp.Workbooks.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                    xlBook = null;
                    succeed = true;

                }
                catch (System.Exception ex)
                {
                    succeed = false;
                }
                finally
                {
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    xlApp = null;

                }
            }
            return succeed;
        }


        //统计图表
        private void simpleButton3_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)this.gridControl1.DataSource;
            FrmPipeAlertChart2D dialog = new FrmPipeAlertChart2D((DataTable)this.gridControl1.DataSource, statsType);
//             if (dialog.ShowDialog() == DialogResult.OK)
//             {
                dialog.ShowDialog();
           /* }*/
            
        }


        //快捷查询
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
            FrmQuery frmquickCheck = new FrmQuery(dict, m_pMapControl);
            if (frmquickCheck.ShowDialog() == DialogResult.OK)
            {                
              DataTable dtNew;
                foreach (IFeatureClass fc in dict.Keys)
                {
                    if (fc.AliasName == frmquickCheck.str1)
                    {
                        table = dict[fc];

                        DataRow[] drr = table.Select(frmquickCheck.str);
                        dtNew = table.Clone();
                        for (int j = 0; j < drr.Length; j++)
                            foreach (DataRow row in drr)
                        {
                            dtNew.ImportRow(row);                            
                        }
                        this.gridControl1.DataSource = dtNew;
                        int N = dtNew.Rows.Count;                        
                    }

                }

            }

        }

        public DataTable dTable
        {

            get
            {
                return null;
            }
            set
            {
                dTable = value;
            }
        }

        //关闭按钮
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        //年限按钮选择
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup1.SelectedIndex)
            {
                case 0:
                 DataTable dt= GetDataTable(this.comboBoxEdit1.Text);
                     DataTable dtNew;
                     DataRow[] drr = dt.Select("(UseTime<'20')and(TimeAlert<='2')");
                        dtNew = dt.Clone();
                       
                            foreach (DataRow row in drr)
                            {
                                dtNew.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew;

                       break;
                case 1:
                    DataTable dt1= GetDataTable(this.comboBoxEdit1.Text);
                    DataTable dtNew1;
                    DataRow[] drr1 = dt1.Select("(UseTime<='20')and(TimeAlert>'2')and(TimeAlert<='5')");
                        dtNew1 = dt1.Clone();
                        for (int j = 0; j < drr1.Length; j++)
                            foreach (DataRow row in drr1)
                            {
                                dtNew1.ImportRow(row);
                            }
                        this.gridControl1.DataSource = dtNew1;
                        int N1 = dtNew1.Rows.Count;

                        break;
                case 2:
                        DataTable dt2 = GetDataTable(this.comboBoxEdit1.Text);
                        DataTable dtNew2;
                        DataRow[] drr2 = dt2.Select("(UseTime>'20')or(TimeAlert>'5')");
                        dtNew2 = dt2.Clone();
                        int n = 0;
                        foreach (DataRow row in drr2)
                        {
                            dtNew2.ImportRow(row);
                        }
                        this.gridControl1.DataSource = dtNew2;
                        int N2 = dtNew2.Rows.Count;

                        break;

            }
        }

       
       
    }
}
