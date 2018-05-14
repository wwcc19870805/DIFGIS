using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DF2DPipe.Stats.UC;
using DFDataConfig.Logic;
using ESRI.ArcGIS.Geodatabase;
using DFWinForms.Class;
using DF2DData.Class;
using DFDataConfig.Class;
using DF2DPipe.Class;

namespace DF2DPipe.Stats.Frm
{
    public partial class FrmEconomyStatsPolygonByDistrict2D : XtraForm
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnShowProp;
        private ComboBoxEdit cbProperty;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private UCEconomyStatsOutput2D ucStatsOutput1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.ComponentModel.IContainer components;
        private ListBoxControl lbx_district;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

        private string _sysFieldName;
        Dictionary<string, List<IFeature>> _dict;
        DataTable dttemp;
        DataTable dtstats;
        public DataTable DTTemp
        {
            get { return this.dttemp; }
        }
        public DataTable DTStats
        {
            get { return this.dtstats; }
        }
        public FrmEconomyStatsPolygonByDistrict2D()
        {
            InitializeComponent();
        }
        public FrmEconomyStatsPolygonByDistrict2D(string sysFieldName)
        {
            this._sysFieldName = sysFieldName;
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEconomyStatsPolygonByDistrict2D));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lbx_district = new DevExpress.XtraEditors.ListBoxControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ucStatsOutput1 = new DF2DPipe.Stats.UC.UCEconomyStatsOutput2D();
            this.btnShowProp = new DevExpress.XtraEditors.SimpleButton();
            this.cbProperty = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbx_district)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lbx_district);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.ucStatsOutput1);
            this.layoutControl1.Controls.Add(this.btnShowProp);
            this.layoutControl1.Controls.Add(this.cbProperty);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(689, 396);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lbx_district
            // 
            this.lbx_district.Location = new System.Drawing.Point(5, 25);
            this.lbx_district.Name = "lbx_district";
            this.lbx_district.Size = new System.Drawing.Size(180, 288);
            this.lbx_district.StyleController = this.layoutControl1;
            this.lbx_district.TabIndex = 13;
            this.lbx_district.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbx_district_MouseDoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(102, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取     消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucStatsOutput1
            // 
            this.ucStatsOutput1.Location = new System.Drawing.Point(192, 2);
            this.ucStatsOutput1.Name = "ucStatsOutput1";
            this.ucStatsOutput1.Size = new System.Drawing.Size(495, 392);
            this.ucStatsOutput1.TabIndex = 11;
            // 
            // btnShowProp
            // 
            this.btnShowProp.Location = new System.Drawing.Point(5, 369);
            this.btnShowProp.Name = "btnShowProp";
            this.btnShowProp.Size = new System.Drawing.Size(93, 22);
            this.btnShowProp.StyleController = this.layoutControl1;
            this.btnShowProp.TabIndex = 5;
            this.btnShowProp.Text = "显示属性表";
            this.btnShowProp.Click += new System.EventHandler(this.btnShowProp_Click);
            // 
            // cbProperty
            // 
            this.cbProperty.Location = new System.Drawing.Point(5, 343);
            this.cbProperty.Name = "cbProperty";
            this.cbProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProperty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbProperty.Size = new System.Drawing.Size(180, 22);
            this.cbProperty.StyleController = this.layoutControl1;
            this.cbProperty.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(689, 396);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "图层数";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(190, 318);
            this.layoutControlGroup2.Text = "区域列表";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lbx_district;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(184, 292);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "分类字段";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 318);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(190, 78);
            this.layoutControlGroup3.Text = "区域选择";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbProperty;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(184, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnShowProp;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(97, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(87, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.ucStatsOutput1;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(190, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(499, 396);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Group.png");
            this.imageCollection1.Images.SetKeyName(1, "Database.png");
            this.imageCollection1.Images.SetKeyName(2, "3DTileLayer.png");
            this.imageCollection1.Images.SetKeyName(3, "Dataset.png");
            this.imageCollection1.Images.SetKeyName(4, "FeatureLayer_line.png");
            this.imageCollection1.Images.SetKeyName(5, "FeatureLayer_model.png");
            this.imageCollection1.Images.SetKeyName(6, "FeatureLayer_point.png");
            this.imageCollection1.Images.SetKeyName(7, "FeatureLayer_polygon.png");
            this.imageCollection1.Images.SetKeyName(8, "Label_image.png");
            this.imageCollection1.Images.SetKeyName(9, "Label_text.png");
            this.imageCollection1.Images.SetKeyName(10, "Object.png");
            this.imageCollection1.Images.SetKeyName(11, "TerrainLayer.png");
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 370);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(556, 25);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // FrmEconomyStatsPolygonByDistrict2D
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(689, 396);
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "FrmEconomyStatsPolygonByDistrict2D";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "经济指标统计";
            this.Load += new System.EventHandler(this.FrmPipeLineLengthStats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbx_district)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void FrmPipeLineLengthStats_Load(object sender, EventArgs e)
        {
            this.Text = GetFrmText(_sysFieldName);
            WaitForm.Start("正在读取区域图层...","请稍后");
            FacilityClass facDistrict = FacilityClassManager.Instance.GetFacilityClassByName("District");
            FacilityClass fac = FacilityClassManager.Instance.GetFacilityClassByName(_sysFieldName);
            int index = 0;
            if (facDistrict != null && fac != null)
            {
                try
                {
                    IFeatureClass district = GetOnlyFcByFacilityClass(facDistrict);
                    IFeatureClass fc = GetOnlyFcByFacilityClass(fac);
                    if (district == null || fc == null) { WaitForm.Stop(); return; }
                    IFeatureCursor cursor = district.Search(null, true);
                    if (cursor == null) return;
                    IFeature feature;
                    FieldInfo disName = facDistrict.GetFieldInfoBySystemName("Name");
                    if (disName == null) return;


                    index = district.Fields.FindField(disName.Name);
                    if (index == -1) return;
                    while ((feature = cursor.NextFeature()) != null)
                    {
                        string name = feature.get_Value(index).ToString();
                        lbx_district.Items.Add(name);
                        this.cbProperty.Properties.Items.Add(name);
                    }
                    this.cbProperty.SelectedIndex = 0;


                    WaitForm.SetCaption("正在统计...","请稍后");
                    EconomyStats econoyStats = EconomyStatsFactory.CreateEconomyStats(_sysFieldName);
                    econoyStats.SetFeatureClass(district, fc);
                    econoyStats.IndexOfDisName = index;
                    FieldInfo disArea = facDistrict.GetFieldInfoBySystemName("Area");
                    econoyStats.IndexOfDisArea = GetIndexByFieldInfo(district, disArea);
                    FieldInfo area = fac.GetFieldInfoBySystemName("Area2D");
                    econoyStats.IndexOfFcArea = GetIndexByFieldInfo(fc, area);
                    FieldInfo floorArea = fac.GetFieldInfoBySystemName("FloorArea");
                    econoyStats.IndexOfFloorArea = GetIndexByFieldInfo(fc, floorArea);
                    FieldInfo length = fac.GetFieldInfoBySystemName("ShapeLength2D");
                    econoyStats.IndexOfLength = GetIndexByFieldInfo(fc, length);                    
                    this._dict = econoyStats.GetStatsResult();
                    econoyStats.InitUserControl(ucStatsOutput1);
                    WaitForm.Stop();
                }
                catch (System.Exception ex)
                {
                    WaitForm.Stop();
                }
                
                
            
            }
            
        }
        private int GetIndexByFieldInfo(IFeatureClass fc,FieldInfo fi)
        {
            if (fi == null) return -1;
            int index = fc.Fields.FindField(fi.Name);
            return index;
        }
        private IFeatureClass GetOnlyFcByFacilityClass(FacilityClass fac)
        {
            string[] arrayFc2D = fac.Fc2D.Split(';');
            if (arrayFc2D == null) return null;
            foreach (string fc2d in arrayFc2D)
            {
                DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2d);
                if (dffc == null) continue;
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null) return null;
                return fc;
            }
            return null;
        }

        private void btnShowProp_Click(object sender, EventArgs e)
        {
            FrmEconomyStatsPropOutput2D dialog = new FrmEconomyStatsPropOutput2D(_sysFieldName,this.cbProperty.Text,this._dict);
            dialog.ShowDialog();
        }

        private void lbx_district_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.cbProperty.SelectedIndex = lbx_district.SelectedIndex;
            btnShowProp_Click(null, null);
        }
        private string GetFrmText(string sysFieldName)
        {
            string name = "";
            switch (sysFieldName)
            {
                case"Building":
                    name = "建筑物经济指标统计";
                    break;
                case"Structure":
                    name = "构筑物经济指标统计";
                    break;
                case "Green":
                    name = "绿化经济指标统计";
                    break;
                case "Road":
                    name = "道路经济指标统计";
                    break;
                case "Railway":
                    name = "铁路经济指标统计";
                    break;
            }
            return name;
        }

      


    

       

        
        
    }
}
