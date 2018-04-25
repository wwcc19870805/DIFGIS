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
using Gvitech.CityMaker.FdeCore;
using DFDataConfig.Class;
using DF3DControl.Base;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.FdeGeometry;
using DFCommon.Class;
using DFWinForms.Class;
using DF3DDraw;

namespace DF3DScan.UC
{
    public class UCLocByRoad : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private SimpleButton btnSearch;
        private TextEdit teRoadName;
        private ComboBoxEdit cmbFCs;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLocByRoad));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.teRoadName = new DevExpress.XtraEditors.TextEdit();
            this.cmbFCs = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teRoadName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFCs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.teRoadName);
            this.layoutControl1.Controls.Add(this.cmbFCs);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(420, 144, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(243, 507);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 54);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(239, 451);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "定位";
            this.gridColumn1.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 127;
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
            // gridColumn2
            // 
            this.gridColumn2.Caption = "道路名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 521;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "fid";
            this.gridColumn3.FieldName = "fid";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "geo";
            this.gridColumn5.FieldName = "geo";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "fcName";
            this.gridColumn4.FieldName = "fcName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSearch.Location = new System.Drawing.Point(215, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(26, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // teRoadName
            // 
            this.teRoadName.Location = new System.Drawing.Point(65, 28);
            this.teRoadName.Name = "teRoadName";
            this.teRoadName.Size = new System.Drawing.Size(146, 22);
            this.teRoadName.StyleController = this.layoutControl1;
            this.teRoadName.TabIndex = 1;
            // 
            // cmbFCs
            // 
            this.cmbFCs.Location = new System.Drawing.Point(65, 2);
            this.cmbFCs.Name = "cmbFCs";
            this.cmbFCs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFCs.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFCs.Size = new System.Drawing.Size(176, 22);
            this.cmbFCs.StyleController = this.layoutControl1;
            this.cmbFCs.TabIndex = 0;
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
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(243, 507);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbFCs;
            this.layoutControlItem1.CustomizationFormText = "道路类型：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(243, 26);
            this.layoutControlItem1.Text = "道路类型：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teRoadName;
            this.layoutControlItem2.CustomizationFormText = "道路名称：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(213, 26);
            this.layoutControlItem2.Text = "道路名称：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSearch;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(213, 26);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(243, 455);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // UCLocByRoad
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCLocByRoad";
            this.Size = new System.Drawing.Size(243, 507);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teRoadName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFCs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        private DataTable _dt;
        private List<Guid> _listRender;
        public UCLocByRoad()
        {
            InitializeComponent();
            this._dt = new DataTable();
            this._dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("geo", typeof(object)), new DataColumn("fcName"), new DataColumn("fid") });
            this.gridControl1.DataSource = this._dt;
            List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName(new string[] { "Road" });
            if (list != null && list.Count != 0)
            {
                foreach (DF3DFeatureClass dffc in list)
                {
                    IFeatureClass fc = dffc.GetFeatureClass();
                    if (fc != null)
                    {
                        this.cmbFCs.Properties.Items.Add(dffc);
                    }
                }
            }
            if (this.cmbFCs.Properties.Items.Count > 0) this.cmbFCs.SelectedIndex = 0;
            this._listRender = new List<Guid>();
        }

        public void RestoreEnv()
        {
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null) return;
            foreach (Guid g in this._listRender)
            {
                app.Current3DMapControl.ObjectManager.DeleteObject(g);
            }
            this._listRender.Clear();
        }

        private void SearchRes(IFeatureClass fc, string fieldName, string fieldValue)
        {
            IFdeCursor cursor = null;
            IRowBuffer row = null;
            try
            {
                int fidIndex = fc.GetFields().IndexOf(fc.FidFieldName);
                if (fidIndex == -1) return;
                int nameIndex = fc.GetFields().IndexOf(fieldName);
                if (nameIndex == -1) return;
                int geoIndex = fc.GetFields().IndexOf("Geometry");
                if (geoIndex == -1) return;

                IQueryFilter filter = new QueryFilter();
                filter.WhereClause = fieldName + " like '%" + fieldValue + "%'";
                cursor = fc.Search(filter, false);
                while ((row = cursor.NextRow()) != null)
                {
                    DataRow dr = this._dt.NewRow();
                    dr["geo"] = row.GetValue(geoIndex);
                    dr["fid"] = row.GetValue(fidIndex);
                    dr["Name"] = row.GetValue(nameIndex);
                    dr["fcName"] = string.IsNullOrEmpty(fc.AliasName) ? fc.Name : fc.AliasName;
                    this._dt.Rows.Add(dr);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RestoreEnv();
            this._dt.Rows.Clear();
            string str = this.teRoadName.Text.Trim();
            object obj = this.cmbFCs.SelectedItem;
            WaitForm.Start("正在搜索...", "请稍后");
            try
            {
                if (obj == null)
                {
                    List<DF3DFeatureClass> list = Dictionary3DTable.Instance.GetFeatureClassByFacilityClassName("Road");
                    if (list != null && list.Count != 0)
                    {
                        foreach (DF3DFeatureClass dffc in list)
                        {
                            IFeatureClass fc = dffc.GetFeatureClass();
                            FacilityClass fac = dffc.GetFacilityClass();
                            if (fc != null && fac != null)
                            {
                                SearchRes(fc, fac.GetFieldInfoNameBySystemName("Name"), str);
                            }
                        }
                    }
                }
                else
                {
                    if (obj is DF3DFeatureClass)
                    {
                        DF3DFeatureClass dffc = obj as DF3DFeatureClass;
                        IFeatureClass fc = dffc.GetFeatureClass();
                        FacilityClass fac = dffc.GetFacilityClass();
                        if (fc != null && fac != null)
                        {
                            SearchRes(fc, fac.GetFieldInfoNameBySystemName("Name"), str);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            WaitForm.Stop();
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
                    if (dr["geo"] != null && dr["Name"] != null && dr["fcName"]!= null)
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
                        IPoint pt = null;
                        if (objGeo.GeometryType == gviGeometryType.gviGeometryMultiPolyline)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;
                            IMultiPolyline mPolyline = objGeo as IMultiPolyline;
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
                            pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pt.X = x;
                            pt.Y = y;
                            pt.Z = z;
                            IRenderMultiPolyline rMPolyline = app.Current3DMapControl.ObjectManager.CreateRenderMultiPolyline(mPolyline, cs, app.Current3DMapControl.ProjectTree.RootID);
                            rMPolyline.HeightStyle = gviHeightStyle.gviHeightOnEverything;
                            rMPolyline.Glow(8000);
                            this._listRender.Add(rMPolyline.Guid);
                        }
                        else if (objGeo.GeometryType == gviGeometryType.gviGeometryMultiPolygon)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;
                            IMultiPolygon mPolygon = objGeo as IMultiPolygon;
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
                            pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            pt.X = x;
                            pt.Y = y;
                            pt.Z = z;
                            IRenderMultiPolygon rMPolygon = app.Current3DMapControl.ObjectManager.CreateRenderMultiPolygon(mPolygon, ss, app.Current3DMapControl.ProjectTree.RootID);
                            rMPolygon.HeightStyle = gviHeightStyle.gviHeightOnEverything;
                            rMPolygon.Glow(8000);
                            this._listRender.Add(rMPolygon.Guid);
                        }
                        else if (objGeo.GeometryType == gviGeometryType.gviGeometryPolyline)
                        {
                            IPolyline polyline = objGeo as IPolyline;
                            pt = polyline.Midpoint;
                            IRenderPolyline rPolyline = app.Current3DMapControl.ObjectManager.CreateRenderPolyline(polyline, cs, app.Current3DMapControl.ProjectTree.RootID);
                            rPolyline.HeightStyle = gviHeightStyle.gviHeightOnEverything;
                            rPolyline.Glow(8000);
                            this._listRender.Add(rPolyline.Guid);
                        }
                        else if (objGeo.GeometryType == gviGeometryType.gviGeometryPoint)
                        {
                            IPoint point = objGeo as IPoint;
                            pt = point;
                            IRenderPoint rPoint = app.Current3DMapControl.ObjectManager.CreateRenderPoint(point, ps, app.Current3DMapControl.ProjectTree.RootID);
                            rPoint.Glow(8000);
                            this._listRender.Add(rPoint.Guid);
                        }
                        else if (objGeo.GeometryType == gviGeometryType.gviGeometryPolygon)
                        {
                            IPolygon polygon = objGeo as IPolygon;
                            pt = polygon.Centroid;
                            IRenderPolygon rPolygon = app.Current3DMapControl.ObjectManager.CreateRenderPolygon(polygon, ss, app.Current3DMapControl.ProjectTree.RootID);
                            rPolygon.HeightStyle = gviHeightStyle.gviHeightOnEverything;
                            rPolygon.Glow(8000);
                            this._listRender.Add(rPolygon.Guid);
                        }
                        else return;

                        ITableLabel tl = DrawTool.CreateTableLabel1(1);
                        tl.TitleText = dr["fcName"].ToString();
                        tl.SetRecord(0, 0, dr["Name"].ToString());
                        tl.Position = pt;

                        this._listRender.Add(tl.Guid);

                        app.Current3DMapControl.Camera.FlyToObject(tl.Guid, gviActionCode.gviActionFlyTo);
                    }
                    break;
            }
        }


    }
}
