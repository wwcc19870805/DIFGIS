using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DF3DData.Class;
using DFDataConfig.Logic;
using Gvitech.CityMaker.FdeCore;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using DFDataConfig.Class;
using DFCommon.Class;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using DF3DDraw;

namespace DF3DPipe.Query.UC
{
    public class UCPropertyInfo : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarManager barManager1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private ComboBoxEdit cbLayer;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiFirstOne;
        private DevExpress.XtraBars.BarButtonItem bbiPreviousOne;
        private DevExpress.XtraBars.BarButtonItem bbiNextOne;
        private DevExpress.XtraBars.BarButtonItem bbiLastOne;
        private DevExpress.XtraBars.BarStaticItem bsiInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    
        public UCPropertyInfo()
        {
            InitializeComponent();
            _dtShow = new DataTable();
            _dtShow.Columns.AddRange(new DataColumn[] { new DataColumn("Property"), new DataColumn("Value") });
            this.gridControl1.DataSource = _dtShow;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPropertyInfo));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiFirstOne = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPreviousOne = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNextOne = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLastOne = new DevExpress.XtraBars.BarButtonItem();
            this.bsiInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbLayer);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(284, 392);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbLayer
            // 
            this.cbLayer.Location = new System.Drawing.Point(65, 2);
            this.cbLayer.Name = "cbLayer";
            this.cbLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLayer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbLayer.Size = new System.Drawing.Size(217, 22);
            this.cbLayer.StyleController = this.layoutControl1;
            this.cbLayer.TabIndex = 0;
            this.cbLayer.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 28);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(280, 362);
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
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
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
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiFirstOne,
            this.bbiPreviousOne,
            this.bbiNextOne,
            this.bbiLastOne,
            this.bsiInfo});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 14;
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFirstOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPreviousOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNextOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLastOne),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiInfo)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            // 
            // bbiFirstOne
            // 
            this.bbiFirstOne.Caption = "第一个";
            this.bbiFirstOne.Id = 9;
            this.bbiFirstOne.ImageIndex = 0;
            this.bbiFirstOne.Name = "bbiFirstOne";
            this.bbiFirstOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiFirstOne_ItemClick);
            // 
            // bbiPreviousOne
            // 
            this.bbiPreviousOne.Caption = "上一个";
            this.bbiPreviousOne.Id = 10;
            this.bbiPreviousOne.ImageIndex = 1;
            this.bbiPreviousOne.Name = "bbiPreviousOne";
            this.bbiPreviousOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPreviousOne_ItemClick);
            // 
            // bbiNextOne
            // 
            this.bbiNextOne.Caption = "下一个";
            this.bbiNextOne.Id = 11;
            this.bbiNextOne.ImageIndex = 2;
            this.bbiNextOne.Name = "bbiNextOne";
            this.bbiNextOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNextOne_ItemClick);
            // 
            // bbiLastOne
            // 
            this.bbiLastOne.Caption = "最后一个";
            this.bbiLastOne.Id = 12;
            this.bbiLastOne.ImageIndex = 3;
            this.bbiLastOne.Name = "bbiLastOne";
            this.bbiLastOne.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLastOne_ItemClick);
            // 
            // bsiInfo
            // 
            this.bsiInfo.Caption = "浏览 0/0";
            this.bsiInfo.Id = 13;
            this.bsiInfo.Name = "bsiInfo";
            this.bsiInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(284, 24);
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
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 392);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(284, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 392);
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
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(284, 392);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(284, 366);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cbLayer;
            this.layoutControlItem2.CustomizationFormText = "当前图层：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(284, 26);
            this.layoutControlItem2.Text = "当前图层：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // UCPropertyInfo
            // 
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCPropertyInfo";
            this.Size = new System.Drawing.Size(284, 416);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString() ;
            }
        }
        public void Init()
        {
            this.cbLayer.Properties.Items.Clear();
            _dtShow.Rows.Clear();
            this.bsiInfo.Caption = "浏览 0/0";
        }

        private SubClass _currentClass;
        private int _num;
        private int _count;
        private Dictionary<SubClass, string> _dict;
        private DataTable _dtShow;
        private IGeometry _geo;
        private string _facType;
        public void SetPropertyInfo(Dictionary<SubClass, string> dict, string facType, IGeometry geo = null)
        {
            if (dict == null || dict.Count == 0) return;
            foreach (KeyValuePair<SubClass, string> kp in dict)
            {
                this.cbLayer.Properties.Items.Add(kp.Key);
            }
            if (_dict != null) _dict.Clear();
            _dict = dict;
            _facType = facType;
            _geo = geo;
            this.cbLayer.SelectedIndex = 0;
        }

        private void GetData()
        {
            string whereClause = _dict[_currentClass];
            SubClass sc = _currentClass;
            if (sc.Parent == null) return;
            string[] arrFc3DId = sc.Parent.Fc3D.Split(';');
            if (arrFc3DId == null) return;
            foreach (string fc3DId in arrFc3DId)
            {
                DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc3DId);
                if (dffc == null) continue;
                FacilityClass facc = dffc.GetFacilityClass();
                IFeatureClass fc = dffc.GetFeatureClass();
                if (fc == null || facc == null || facc.Name != this._facType) continue;
                List<DFDataConfig.Class.FieldInfo> facFields = facc.FieldInfoCollection;
                if (facFields == null || facFields.Count == 0) return;

                ISpatialFilter filter = new SpatialFilter();
                filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                filter.Geometry = this._geo;
                filter.GeometryField = "FootPrint";
                filter.WhereClause = whereClause;
                _count = fc.GetCount(filter);
                if (_num > _count || _count == 0) return;

                filter.ResultBeginIndex = _num - 1;
                filter.ResultLimit = 1;
                IFdeCursor cursor = null;
                IRowBuffer row = null;
                try
                {
                    cursor = fc.Search(filter, false);
                    if ((row = cursor.NextRow()) != null)
                    {
                        #region 定位
                        int geoindex = row.FieldIndex("Geometry");
                        if (geoindex == -1 || !(row.GetValue(geoindex) is IModelPoint)) continue;
                        DF3DApplication app = DF3DApplication.Application;
                        if (app != null && app.Current3DMapControl != null)  
                        {
                            IModelPoint geo = row.GetValue(geoindex) as IModelPoint;
                            IModelPointSymbol mps = new ModelPointSymbol();
                            mps.SetResourceDataSet(fc.FeatureDataSet);
                            IRenderGeometry render = app.Current3DMapControl.ObjectManager.CreateRenderModelPoint(geo, mps, app.Current3DMapControl.ProjectTree.RootID);
                            //app.Current3DMapControl.Camera.FlyToObject(render.Guid, gviActionCode.gviActionJump);
                            render.Glow(5000);
                            app.Current3DMapControl.ObjectManager.DelayDelete(render.Guid, 5000);

                            ITableLabel tl = DrawTool.CreateTableLabel1(1);                           
                            tl.TitleText = "属性查询";
                            tl.SetRecord(0, 0, dffc.ToString());
                            IPoint ptTL = null;
                            int geoshapeindex = row.FieldIndex("Shape");
                            if (geoshapeindex != -1)
                            {
                                if (!row.IsNull(geoshapeindex))
                                {
                                    IGeometry geoShape = row.GetValue(geoshapeindex) as IGeometry;
                                    if (geoShape.GeometryType == gviGeometryType.gviGeometryMultiPolyline)
                                    {
                                        double x = 0;
                                        double y = 0;
                                        double z = 0;
                                        IMultiPolyline mPolyline = geoShape as IMultiPolyline;
                                        for (int m = 0; m < mPolyline.GeometryCount; m++)
                                        {
                                            IPolyline polyline = mPolyline.GetPolyline(m);
                                            IPoint pttemp = polyline.Midpoint;
                                            x += pttemp.X;
                                            y += pttemp.Y;
                                            z += pttemp.Z;
                                        }
                                        x = x / mPolyline.GeometryCount;
                                        y = y / mPolyline.GeometryCount;
                                        z = z / mPolyline.GeometryCount;
                                        ptTL = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                        ptTL.X = x;
                                        ptTL.Y = y;
                                        ptTL.Z = z;
                                    }
                                    else if (geoShape.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                                    {
                                        double x = 0;
                                        double y = 0;
                                        double z = 0;
                                        IMultiPolygon mPolygon = geoShape as IMultiPolygon;
                                        for (int m = 0; m < mPolygon.GeometryCount; m++)
                                        {
                                            IPolygon polygon = mPolygon.GetPolygon(m);
                                            IPoint pttemp = polygon.Centroid;
                                            x += pttemp.X;
                                            y += pttemp.Y;
                                            z += pttemp.Z;
                                        }
                                        x = x / mPolygon.GeometryCount;
                                        y = y / mPolygon.GeometryCount;
                                        z = z / mPolygon.GeometryCount;
                                        ptTL = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                        ptTL.X = x;
                                        ptTL.Y = y;
                                        ptTL.Z = z;
                                    }
                                    else if (geoShape.GeometryType == gviGeometryType.gviGeometryPolyline)
                                    {
                                        ptTL = (geoShape as IPolyline).Midpoint;
                                    }
                                    else if (geoShape.GeometryType == gviGeometryType.gviGeometryPoint)
                                    {
                                        ptTL = geoShape as IPoint;
                                    }
                                    else if (geoShape.GeometryType == gviGeometryType.gviGeometryPolygon)
                                    {
                                        ptTL = (geoShape as IPolygon).Centroid;
                                    }
                                }
                            }
                            if (ptTL != null) tl.Position = ptTL;
                            else tl.Position = geo;
                            app.Current3DMapControl.Camera.FlyToObject(tl.Guid, gviActionCode.gviActionFlyTo);
                            app.Current3DMapControl.ObjectManager.DelayDelete(tl.Guid, 5000);
                        }

                        #endregion

                        foreach (DFDataConfig.Class.FieldInfo facField in facFields)
                        {
                            if (!facField.CanQuery) continue;
                            int index = row.FieldIndex(facField.Name);
                            if (index != -1 && !row.IsNull(index))
                            {
                                object obj = row.GetValue(index);
                                string str = "";
                                IFieldInfo fiFC = row.Fields.Get(index);
                                switch (fiFC.FieldType)
                                {
                                    case gviFieldType.gviFieldBlob:
                                    case gviFieldType.gviFieldUnknown:
                                    case gviFieldType.gviFieldGeometry:
                                        continue;
                                    case gviFieldType.gviFieldFloat:
                                    case gviFieldType.gviFieldDouble:
                                        double d;
                                        if (double.TryParse(obj.ToString(), out d))
                                        {
                                            str = d.ToString("0.00");
                                        }
                                        break;
                                    default:
                                        str = obj.ToString();
                                        break;
                                }
                                DataRow dr = this._dtShow.NewRow();
                                dr["Property"] = facField.ToString();
                                dr["Value"] = str;
                                this._dtShow.Rows.Add(dr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    if (cursor != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                        cursor = null;
                    }
                    if (row != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(row);
                        row = null;
                    }
                }
            }
        }

        private void ShowProperty()
        {
            try
            {
                if (_currentClass == null) return;
                _dtShow.Rows.Clear();
                this.bsiInfo.Caption = "浏览 0/0";

                GetData();

                if (_count == 0)
                {
                    this.bbiFirstOne.Enabled = false;
                    this.bbiPreviousOne.Enabled = false;
                    this.bbiNextOne.Enabled = false;
                    this.bbiLastOne.Enabled = false;
                    return;
                }
                else
                {
                    if (_num == 1)
                    {
                        this.bbiFirstOne.Enabled = false;
                        this.bbiPreviousOne.Enabled = false;
                        this.bbiNextOne.Enabled = true;
                        this.bbiLastOne.Enabled = true;
                    }
                    else if (_num == _count)
                    {
                        this.bbiFirstOne.Enabled = true;
                        this.bbiPreviousOne.Enabled = true;
                        this.bbiNextOne.Enabled = false;
                        this.bbiLastOne.Enabled = false;
                    }
                    else
                    {
                        this.bbiFirstOne.Enabled = true;
                        this.bbiPreviousOne.Enabled = true;
                        this.bbiNextOne.Enabled = true;
                        this.bbiLastOne.Enabled = true;
                    }
                }
                this.bsiInfo.Caption = "浏览 " + _num + "/" + _count;
            }
            catch (Exception ex)
            {
            }
        }

        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentClass = this.cbLayer.SelectedItem as SubClass;
            _num = 1;
            ShowProperty();
        }

        private void bbiFirstOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _num = 1;
            ShowProperty();
        }

        private void bbiPreviousOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_num == 1) return;
            _num--;
            ShowProperty();
        }

        private void bbiNextOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_num == _count) return;
            _num++;
            ShowProperty();
        }

        private void bbiLastOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _num = _count;
            ShowProperty();
        }
    }
}
