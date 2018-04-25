using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DF2DPipe.Frm;
using ESRI.ArcGIS.Geodatabase;
using DF2DData.Class;
using DFDataConfig.Logic;
using DF2DControl.Base;
using DFDataConfig.Class;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;


namespace DF2DPipe.Query.UC
{
    public class UCPropertyInfo2D : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarManager barManager1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbiFirst;
        private DevExpress.XtraBars.BarButtonItem bbiPreviousOne;
        private DevExpress.XtraBars.BarButtonItem bbiNextOne;
        private DevExpress.XtraBars.BarButtonItem bbiLastOne;
        private DevExpress.XtraBars.BarStaticItem bsiInfo;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private ComboBoxEdit cbLayer;
        private LayoutControlItem layoutControlItem3;
        private EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;


       
       
        

        public UCPropertyInfo2D()
        {
            InitializeComponent();
            //DataTable _dtShow;
            _dtShow = new DataTable();
            _dtShow.Columns.AddRange(new DataColumn[] { new DataColumn("Property"), new DataColumn("Value") });
            this.gridControl1.DataSource = _dtShow;
        }

        //public UCPropertyInfo2D(FrmSimpleConditionQuery2D dialog)
        //{
        //    InitializeComponent();
        //    _dtShow = new DataTable();
        //    _dtShow.Columns.AddRange(new DataColumn[] { new DataColumn("Property"), new DataColumn("Value") });
        //    this.gridControl1.DataSource = _dtShow;
            

        //    ////IFeatureClass fc;
        //    //Dictionary<DF2DFeatureClass, DataTable> dic = dialog.QueryDTByLayer;
        //    //DataTable _dt = null;
            
        //    //if (dialog.SelectFeatureClass != null)
        //    //{
        //    //    for (int i = 0; i < dialog.SelectFeatureClass.Count; i++)
        //    //    {
        //    //        this.cbLayer.Properties.Items.Add(dialog.SelectFeatureClass[i].GetFeatureClass().AliasName);
        //    //    }
        //    //    this.cbLayer.SelectedItem = this.cbLayer.Properties.Items[0];
        //    //}
        //    //if (this.cbLayer.SelectedItem != null)
        //    //{

           
                
        //    //    foreach (KeyValuePair<DF2DFeatureClass, DataTable> v in dic)
        //    //    {
        //    //        if (v.Key.GetFeatureClass().AliasName == this.cbLayer.SelectedItem.ToString())
        //    //        {
        //    //            _dt = v.Value;
        //    //        }
        //    //        continue;

                        
        //    //    }
        //    //}
        //    //if (_dt != null)
        //    //{
               
        //    //    int _num = 1;
        //    //    ShowProperty(_dtShow, _dt, _num);
        //    //}
           
        //}
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPropertyInfo2D));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiFirst = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPreviousOne = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNextOne = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLastOne = new DevExpress.XtraBars.BarButtonItem();
            this.bsiInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbLayer);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(327, 210, 543, 464);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 385);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbLayer
            // 
            this.cbLayer.Location = new System.Drawing.Point(75, 2);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLayer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbLayer.Size = new System.Drawing.Size(197, 22);
            this.cbLayer.StyleController = this.layoutControl1;
            this.cbLayer.TabIndex = 0;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(2, 28);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(280, 345);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 30;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "属性";
            this.gridColumn1.FieldName = "Property";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "属性值";
            this.gridColumn2.FieldName = "Value";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
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
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiFirst,
            this.bbiPreviousOne,
            this.bbiNextOne,
            this.bbiLastOne,
            this.bsiInfo});
            this.barManager1.MaxItemId = 9;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(13, 164);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFirst),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPreviousOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNextOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLastOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiInfo)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.Text = "Tools";
            // 
            // bbiFirst
            // 
            this.bbiFirst.Caption = "第一个";
            this.bbiFirst.Id = 2;
            this.bbiFirst.ImageIndex = 0;
            this.bbiFirst.ImageIndexDisabled = 4;
            this.bbiFirst.Name = "bbiFirst";
            this.bbiFirst.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiFirst_ItemClick);
            // 
            // bbiPreviousOne
            // 
            this.bbiPreviousOne.Caption = "上一个";
            this.bbiPreviousOne.Id = 3;
            this.bbiPreviousOne.ImageIndex = 1;
            this.bbiPreviousOne.ImageIndexDisabled = 5;
            this.bbiPreviousOne.Name = "bbiPreviousOne";
            this.bbiPreviousOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPreviousOne_ItemClick);
            // 
            // bbiNextOne
            // 
            this.bbiNextOne.Caption = "下一个";
            this.bbiNextOne.Id = 4;
            this.bbiNextOne.ImageIndex = 2;
            this.bbiNextOne.ImageIndexDisabled = 6;
            this.bbiNextOne.Name = "bbiNextOne";
            this.bbiNextOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNextOne_ItemClick);
            // 
            // bbiLastOne
            // 
            this.bbiLastOne.Caption = "最后一个";
            this.bbiLastOne.Id = 5;
            this.bbiLastOne.ImageIndex = 3;
            this.bbiLastOne.ImageIndexDisabled = 7;
            this.bbiLastOne.Name = "bbiLastOne";
            this.bbiLastOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLastOne_ItemClick);
            // 
            // bsiInfo
            // 
            this.bsiInfo.Caption = "浏览 0/0";
            this.bsiInfo.Id = 8;
            this.bsiInfo.Name = "bsiInfo";
            this.bsiInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(284, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 416);
            this.barDockControlBottom.Size = new System.Drawing.Size(284, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 385);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(284, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 385);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("first_16x16.png", "images/arrows/first_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/first_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "first_16x16.png");
            this.imageCollection1.InsertGalleryImage("prev_16x16.png", "images/arrows/prev_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/prev_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "prev_16x16.png");
            this.imageCollection1.InsertGalleryImage("next_16x16.png", "images/arrows/next_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/next_16x16.png"), 2);
            this.imageCollection1.Images.SetKeyName(2, "next_16x16.png");
            this.imageCollection1.InsertGalleryImage("last_16x16.png", "images/arrows/last_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/last_16x16.png"), 3);
            this.imageCollection1.Images.SetKeyName(3, "last_16x16.png");
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 385);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 375);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(284, 10);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(284, 349);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(274, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbLayer;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(264, 26);
            this.layoutControlItem3.Text = "当前图层：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // UCPropertyInfo2D
            // 
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCPropertyInfo2D";
            this.Size = new System.Drawing.Size(284, 416);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        //DataTable _dtShow;
        //DataTable _dt;
        //List<DataTable> temp = new List<DataTable>();
        //int _num;
        private string _currentClass;
        private int _num;
        private Dictionary<string, DataTable> _dict;
        private DataTable _dtShow;


        public void Init()
        {
            this.cbLayer.Properties.Items.Clear();
            _dtShow.Rows.Clear();
            this.bsiInfo.Caption = "浏览 0/0";
        }

        //public void SetPropertyInfo(Dictionary<DF2DFeatureClass, DataTable> dictDT)
        //{
        //    if (dictDT == null || dictDT.Count == 0) return;
        //    this.cbLayer.Properties.Items.Clear();
        //    this.temp.Clear();
        //    //List<DataTable> temp = new List<DataTable>();
        //    foreach (KeyValuePair<DF2DFeatureClass, DataTable> kp in dictDT)
        //    {
        //        this.cbLayer.Properties.Items.Add(kp.Key.GetFeatureClass().AliasName);
                
        //        temp.Add(kp.Value);
        //    }
        //    this.cbLayer.SelectedIndex = 0;
        //    if (temp.Count > 0)
        //        _dt = temp[0];
            
           
        //    _num = 1;
        //}

        public void SetPropertyInfo(Dictionary<string, DataTable> dict)
        {
            if (dict == null || dict.Count == 0) return;
            foreach (KeyValuePair<string, DataTable> kp in dict)
            {
                this.cbLayer.Properties.Items.Add(kp.Key);
            }
            _dict = dict;
            this.cbLayer.SelectedIndex = 0;
            cbLayer_SelectedIndexChanged(null, null);
        }
       
        //public void ShowProperty()
        //{
        //    try
        //    {
        //        _dtShow.Rows.Clear();
                
        //        if (_dt == null || _dt.Rows.Count < _num) return;
        //        this.bsiInfo.Caption = "浏览 " + _num + "/" + _dt.Rows.Count;
        //        DataRow dr = _dt.Rows[_num - 1];
        //        for (int i = 0; i < _dt.Columns.Count; i++)
        //        {
        //            String columnName = _dt.Columns[i].ColumnName;
        //            if (columnName == null) continue;
                    
        //            if (dr[columnName] == null || dr[columnName].ToString() == "") continue;
        //            string value = dr[columnName].ToString();
        //            double dTemp = 0.0;
        //            string dStrTemp = "";
        //            bool bDouble = double.TryParse(value, out dTemp);
        //            if (bDouble) dStrTemp = dTemp.ToString("0.00");
        //            else dStrTemp = value;
        //            DataRow drnew = _dtShow.NewRow();
        //            drnew["Property"] = columnName;
        //            drnew["Value"] = dStrTemp;
        //            _dtShow.Rows.Add(drnew);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
            	
        //    }
        //}
        private void ShowProperty()
        {
            try
            {
                if (_currentClass == null) return;
                _dtShow.Rows.Clear();
                DataTable dt = _dict[_currentClass];
                if (dt == null || dt.Rows.Count < _num) return;
                if (dt.Rows.Count == 0 || dt.Rows.Count == 1)
                {
                    this.bbiFirst.Enabled = false;
                    this.bbiPreviousOne.Enabled = false;
                    this.bbiNextOne.Enabled = false;
                    this.bbiLastOne.Enabled = false;
                }
                else
                {
                    if (_num == 1)
                    {
                        this.bbiFirst.Enabled = false;
                        this.bbiPreviousOne.Enabled = false;
                        this.bbiNextOne.Enabled = true;
                        this.bbiLastOne.Enabled = true;
                    }
                    else if (_num == dt.Rows.Count)
                    {
                        this.bbiFirst.Enabled = true;
                        this.bbiPreviousOne.Enabled = true;
                        this.bbiNextOne.Enabled = false;
                        this.bbiLastOne.Enabled = false;
                    }
                    else
                    {
                        this.bbiFirst.Enabled = true;
                        this.bbiPreviousOne.Enabled = true;
                        this.bbiNextOne.Enabled = true;
                        this.bbiLastOne.Enabled = true;
                    }
                }
                this.bsiInfo.Caption = "浏览 " + _num + "/" + dt.Rows.Count;
                DataRow dr = dt.Rows[_num - 1];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataRow drnew = _dtShow.NewRow();
                    drnew["Property"] = dt.Columns[i].Caption;
                    drnew["Value"] = dr[dt.Columns[i].ColumnName];
                    _dtShow.Rows.Add(drnew);
                }
                #region 定位
                DF2DApplication app = DF2DApplication.Application;
                if (app == null || app.Current2DMapControl == null) return;
                if (_currentClass == "建筑物" || _currentClass == "构筑物")
                {
                    FacilityClass fac;
                    if (_currentClass == "建筑物")
                    {
                        fac = FacilityClassManager.Instance.GetFacilityClassByName("Building"); 
                    }
                    else
                    {
                        fac = FacilityClassManager.Instance.GetFacilityClassByName("Structure");
                    }
                    if (fac != null)
                    {
                        string[] fc2D = fac.Fc2D.Split(';');
                        DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2D[0]);
                        if (dffc == null) return;
                        IFeatureClass fc = dffc.GetFeatureClass();
                        IFeatureLayer fl = dffc.GetFeatureLayer();
                        if (fc == null) return;
                        int oid = int.Parse(dr["oid"].ToString());
                        IFeature feature = fc.GetFeature(oid);
                        app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
                        app.Current2DMapControl.ActiveView.FocusMap.SelectFeature(fl as ILayer, feature);
                        app.Current2DMapControl.ActiveView.Refresh();
                        IGeometry geo = feature.Shape;
                        IEnvelope pEnv = geo.Envelope;
                        IPoint pPoint = new PointClass();
                        pPoint.PutCoords((pEnv.XMax + pEnv.XMin) / 2, (pEnv.YMax + pEnv.YMin) / 2);
                        app.Current2DMapControl.MapScale = 500;
                        app.Current2DMapControl.CenterAt(pPoint);
                    }
                }
                else
                {
                    MajorClass mc = LogicDataStructureManage2D.Instance.GetMajorClassBySubClassName(_currentClass);
                    if (mc != null && !string.IsNullOrEmpty(mc.Fc2D))
                    {
                        string[] arrFc2DId = mc.Fc2D.Split(';');
                        if (arrFc2DId == null) return;
                        foreach (string fc2DId in arrFc2DId)
                        {
                            DF2DFeatureClass dffc = DF2DFeatureClassManager.Instance.GetFeatureClassByID(fc2DId);
                            if (dffc == null) continue;
                            FacilityClass fac = dffc.GetFacilityClass();
                            if (fac == null || fac.Name != dt.TableName) continue;
                            IFeatureClass fc = dffc.GetFeatureClass();
                            IFeatureLayer fl = dffc.GetFeatureLayer();
                            if (fc == null) continue;
                            int oid = int.Parse(dr["oid"].ToString());
                            IFeature feature = fc.GetFeature(oid);
                            app.Current2DMapControl.ActiveView.FocusMap.ClearSelection();
                            app.Current2DMapControl.ActiveView.FocusMap.SelectFeature(fl as ILayer, feature);
                            app.Current2DMapControl.ActiveView.Refresh();
                            IGeometry geo = feature.Shape;
                            IEnvelope pEnv = geo.Envelope;
                            IPoint pPoint = new PointClass();
                            pPoint.PutCoords((pEnv.XMax + pEnv.XMin) / 2, (pEnv.YMax + pEnv.YMin) / 2);
                            app.Current2DMapControl.MapScale = 500;
                            app.Current2DMapControl.CenterAt(pPoint);
                        }
                    }
                }
               
                #endregion

            }
            catch (Exception ex)
            {
            }
        }



        private void bbiFirst_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _num = 1;
            ShowProperty();
        }

        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_num = 1;
            //if (temp.Count > 0 && temp.Count == cbLayer.Properties.Items.Count)
            //{
            //    _dt = temp[cbLayer.SelectedIndex];
            //    ShowProperty();
            //}
                
            //else return;
            _currentClass = this.cbLayer.SelectedItem.ToString();
            _num = 1;
            ShowProperty();
        }

        private void bbiNextOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (_num == _dt.Rows.Count) return;
            //_num++;
            if (_num == _dict[_currentClass].Rows.Count) return;
            _num++;
            ShowProperty();
        }

        private void bbiPreviousOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_num == 1) return;
            _num--;
            ShowProperty();
        }

        private void bbiLastOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_num = _dt.Rows.Count;
            _num = _dict[_currentClass].Rows.Count;
            ShowProperty();
        }
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
            mark.Size = 8;

            ILineSymbol line = new SimpleLineSymbolClass();
            line.Width = 20;
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
       



    }
}
