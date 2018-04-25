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
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using DF3DData.Class;
using DFDataConfig.Logic;
using Gvitech.CityMaker.FdeGeometry;
using DFCommon.Class;
using Gvitech.CityMaker.FdeCore;
using DFWinForms.Class;
using DFDataConfig.Class;
using DF3DPipe.Analysis.Class;
using DF3DPipe.Analysis.Frm;

namespace DF3DPipe.Analysis.UC
{
    public class UCPipeVertical : XtraUserControl
    {
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private RadioGroup radioGroup1;
        private SimpleButton btnAnalysis;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPipeVertical));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnAnalysis = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.btnAnalysis);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(254, 468);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 67);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(250, 399);
            this.gridControl1.TabIndex = 2;
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
            this.gridColumn6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "序号";
            this.gridColumn1.FieldName = "Num";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 171;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "管线类型";
            this.gridColumn2.FieldName = "PipeType";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 436;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "操作";
            this.gridColumn3.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 437;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "删除", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Fid";
            this.gridColumn4.FieldName = "Fid";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "FeatureClass";
            this.gridColumn5.FieldName = "FeatureClass";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "InterPoint";
            this.gridColumn6.FieldName = "InterPoint";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(2, 2);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "点选"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "线选")});
            this.radioGroup1.Size = new System.Drawing.Size(250, 35);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Location = new System.Drawing.Point(2, 41);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(250, 22);
            this.btnAnalysis.StyleController = this.layoutControl1;
            this.btnAnalysis.TabIndex = 1;
            this.btnAnalysis.Text = "分      析";
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(254, 468);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.radioGroup1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(254, 39);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 65);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(254, 403);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnAnalysis;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(254, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // UCPipeVertical
            // 
            this.Controls.Add(this.layoutControl1);
            this.Name = "UCPipeVertical";
            this.Size = new System.Drawing.Size(254, 468);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        private DrawTool _drawTool;
        private DataTable _dt;
        private int _num;
        private Dictionary<int, Guid> _dict;
        private Dictionary<int, Guid> _dictLabel;
        private AxRenderControl d3;
        public UCPipeVertical()
        {
            InitializeComponent();
            DF3DApplication app = DF3DApplication.Application;
            if (app == null || app.Current3DMapControl == null)
            {
                this.Enabled = false;
                return;
            }
            d3 = app.Current3DMapControl;

            this._dt = new DataTable();
            this._dt.Columns.AddRange(new DataColumn[] { new DataColumn("Num"), new DataColumn("PipeType",typeof(object)), 
                new DataColumn("Fid"), new DataColumn("FeatureClass",typeof(object)), new DataColumn("InterPoint",typeof(object)) });
            this.gridControl1.DataSource = this._dt;
            this._num = 0;
            _dict = new Dictionary<int, Guid>();
            _dictLabel = new Dictionary<int, Guid>();
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
        }

        private void Clear()
        {
            if (this._drawTool != null)
            {
                this._drawTool.Close();
            }
            foreach (KeyValuePair<int, Guid> kv in this._dict)
            {
                d3.ObjectManager.DeleteObject(kv.Value);
            }
            this._dict.Clear();
            foreach (KeyValuePair<int, Guid> kv in this._dictLabel)
            {
                d3.ObjectManager.DeleteObject(kv.Value);
            }
            this._dictLabel.Clear();
        }

        private void OnStartDraw()
        {
            if (this._drawTool != null)
            {
                //Clear();
            }
        }

        private void OnFinishedDraw()
        {
            if (this._drawTool != null)
            {
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    if (this._drawTool.GetSelectFeatureLayerPickResult() != null && this._drawTool.GetSelectPoint() != null)
                    {
                        IFeatureLayerPickResult pr = this._drawTool.GetSelectFeatureLayerPickResult();
                        string g = pr.FeatureLayer.FeatureClassId.ToString();
                        string facName = Dictionary3DTable.Instance.GetFacilityClassNameByDFFeatureClassID(g);
                        if (facName != "PipeLine")
                        {
                            XtraMessageBox.Show("您选择的不是管线设施。", "提示");
                            return;
                        }
                        DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(g);
                        if (dffc == null) return;
                        int fid = pr.FeatureId;
                        MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(g);
                        if (mc == null) return;
                        _num++;
                        
                        IPoint interPoint = this._drawTool.GetSelectPoint();
                        ISimplePointSymbol sps = new SimplePointSymbol();
                        sps.FillColor = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
                        sps.Size = SystemInfo.Instance.SymbolSize;
                        IRenderPoint rp = d3.ObjectManager.CreateRenderPoint(interPoint, sps, d3.ProjectTree.RootID);
                        this._dict[_num] = rp.Guid;

                        ILabel label = d3.ObjectManager.CreateLabel(d3.ProjectTree.RootID);
                        IPoint pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        pt.SetCoords(interPoint.X, interPoint.Y, interPoint.Z + 1, 0, 0);
                        label.Position = pt;
                        label.Text = _num.ToString();
                        TextSymbol ts = new TextSymbol();
                        TextAttribute ta = new TextAttribute();
                        ta.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                        ta.TextSize =SystemInfo.Instance.TextSize;
                        ts.TextAttribute = ta;
                        label.TextSymbol = ts;
                        this._dictLabel[_num] = label.Guid;

                        DataRow dr = this._dt.NewRow();
                        dr["Num"] = _num;
                        dr["PipeType"] = mc;
                        dr["Fid"] = fid;
                        dr["FeatureClass"] = dffc.GetFeatureClass();
                        dr["InterPoint"] = interPoint;
                        this._dt.Rows.Add(dr);
                    }
                }
                if (this.radioGroup1.SelectedIndex == 1)
                {
                    if (this._drawTool.GetGeo() != null)
                    {
                        WaitForm.Start("正在进行相交查询...", "请稍后");
                        IPolyline line = this._drawTool.GetGeo() as IPolyline;
                        IPolyline l = line.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
                        List<DF3DFeatureClass> listdffc = DF3DFeatureClassManager.Instance.GetFeatureClassByFacilityClassName("PipeLine");
                        if (listdffc == null) return;
                        foreach (DF3DFeatureClass dffc in listdffc)
                        {
                            IFeatureClass fc = dffc.GetFeatureClass();
                            if (fc == null) continue;
                            MajorClass mc = LogicDataStructureManage3D.Instance.GetMajorClassByDFFeatureClassID(fc.GuidString);
                            if (mc == null) continue;
                            if (this._dt.Rows.Count > 0)
                            {
                                if (this._dt.Rows[0]["PipeType"] != null && this._dt.Rows[0]["PipeType"] is MajorClass)
                                {
                                    if (mc.Name != (this._dt.Rows[0]["PipeType"] as MajorClass).Name)
                                    {
                                        continue;
                                    }
                                }
                            }
                            ISpatialFilter filter = new SpatialFilter();
                            filter.Geometry = l;
                            filter.GeometryField = "FootPrint";
                            filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                            int counttemp = fc.GetCount(filter);
                            if (counttemp == 0) continue;
                            IFdeCursor cursor = null;
                            IRowBuffer row = null;
                            try
                            {
                                IFieldInfoCollection fields = fc.GetFields();
                                cursor = fc.Search(filter, true);
                                while ((row = cursor.NextRow()) != null)
                                {
                                    IPolyline geo = row.GetValue(fields.IndexOf("FootPrint")) as IPolyline;
                                    IPolyline geoShape = row.GetValue(fields.IndexOf("Shape")) as IPolyline;
                                    if (geo == null || geoShape == null || geo.GeometryType != gviGeometryType.gviGeometryPolyline || geoShape.GeometryType != gviGeometryType.gviGeometryPolyline) continue;
                                    if ((l as IRelationalOperator2D).Intersects2D(geo))
                                    {
                                        IPoint intersectPointTemp = (l as ITopologicalOperator2D).Intersection2D(geo) as IPoint;
                                        double height = geoShape.StartPoint.Z + (geoShape.EndPoint.Z - geoShape.StartPoint.Z)
                                            * Math.Sqrt((geo.StartPoint.X - intersectPointTemp.X) * (geo.StartPoint.X - intersectPointTemp.X) + (geo.StartPoint.Y - intersectPointTemp.Y) * (geo.StartPoint.Y - intersectPointTemp.Y)) / geo.Length;
                                        IPoint intersectPoint = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                        intersectPoint.Z = height;
                                        intersectPoint.X = intersectPointTemp.X;
                                        intersectPoint.Y = intersectPointTemp.Y;
                                        int fid = int.Parse(row.GetValue(0).ToString());
                                        _num++;

                                        ISimplePointSymbol sps = new SimplePointSymbol();
                                        sps.FillColor = Convert.ToUInt32(SystemInfo.Instance.FillColor, 16);
                                        sps.Size = SystemInfo.Instance.SymbolSize;
                                        IRenderPoint rp = d3.ObjectManager.CreateRenderPoint(intersectPoint, sps, d3.ProjectTree.RootID);
                                        this._dict[_num] = rp.Guid;

                                        ILabel label = d3.ObjectManager.CreateLabel(d3.ProjectTree.RootID);
                                        IPoint pt = (new GeometryFactory()).CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                        pt.SetCoords(intersectPoint.X, intersectPoint.Y, intersectPoint.Z + 1, 0, 0);
                                        label.Position = pt;
                                        label.Text = _num.ToString();
                                        TextSymbol ts = new TextSymbol();
                                        TextAttribute ta = new TextAttribute();
                                        ta.TextColor = Convert.ToUInt32(SystemInfo.Instance.TextColor, 16);
                                        ta.TextSize = SystemInfo.Instance.TextSize;
                                        ts.TextAttribute = ta;
                                        label.TextSymbol = ts;
                                        this._dictLabel[_num] = label.Guid;

                                        DataRow dr = this._dt.NewRow();
                                        dr["Num"] = _num;
                                        dr["PipeType"] = mc;
                                        dr["Fid"] = fid;
                                        dr["FeatureClass"] = fc;
                                        dr["InterPoint"] = intersectPoint;
                                        this._dt.Rows.Add(dr);
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
                        WaitForm.Stop();
                    }
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    if (this._drawTool != null)
                    {
                        this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                        this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                        this._drawTool.End();
                    } 
                    this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.SelectOne);
                    if (this._drawTool != null)
                    {
                        this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                        this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                        this._drawTool.Start();
                    }
                    break;
                case 1:
                    if (this._drawTool != null)
                    {
                        this._drawTool.OnStartDraw -= new OnStartDraw(this.OnStartDraw);
                        this._drawTool.OnFinishedDraw -= new OnFinishedDraw(this.OnFinishedDraw);
                        this._drawTool.End();
                    } 
                    this._drawTool = DrawToolService.Instance.CreateDrawTool(DrawType.Line);
                    if (this._drawTool != null)
                    {
                        this._drawTool.OnStartDraw += new OnStartDraw(this.OnStartDraw);
                        this._drawTool.OnFinishedDraw += new OnFinishedDraw(this.OnFinishedDraw);
                        this._drawTool.Start();
                    }
                    break;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.Controls.EditorButton btn = e.Button;
            switch (btn.Caption)
            {
                case "删除":
                    int focusedRowHandle = this.gridView1.FocusedRowHandle;
                    if (focusedRowHandle == -1) return;
                    DataRow dr = this.gridView1.GetDataRow(focusedRowHandle);
                    int n = int.Parse(dr["Num"].ToString());
                    if (this._dict.ContainsKey(n) && this._dict[n] != null) d3.ObjectManager.DeleteObject(this._dict[n]);
                    if (this._dictLabel.ContainsKey(n) && this._dictLabel[n] != null) d3.ObjectManager.DeleteObject(this._dictLabel[n]);
                    this._dict.Remove(n);
                    this._dictLabel.Remove(n);
                    this._dt.Rows.Remove(dr);
                    this.gridView1.RefreshRow(focusedRowHandle);
                    break;
            }
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._dt.Rows.Count < 2)
                {
                    XtraMessageBox.Show("点数少于2个", "提示");
                    return;
                }
                IPoint startPt = this._dt.Rows[0]["InterPoint"] as IPoint;
                if (startPt == null) return;

                WaitForm.Start("正在进行纵断面分析...", "请稍后");
                DF3DApplication app = DF3DApplication.Application;
                if (app == null || app.Current3DMapControl == null) return;

                string road1 = "";
                string road2 = "";
                bool bAlert = false;
                double hmax = double.MinValue;
                double hmin = double.MaxValue;
                List<PPLine> pplines = new List<PPLine>();
                foreach (DataRow dr in this._dt.Rows)
                {
                    IFeatureClass fc = dr["FeatureClass"] as IFeatureClass;
                    MajorClass mc = dr["PipeType"] as MajorClass;
                    if (fc == null || mc == null) continue;
                    DF3DFeatureClass dffc = DF3DFeatureClassManager.Instance.GetFeatureClassByID(fc.GuidString);
                    if (dffc == null) continue;
                    FacilityClass fac = dffc.GetFacilityClass();
                    if (fac == null || fac.Name != "PipeLine") continue;

                    IFieldInfoCollection fields = fc.GetFields();
                    int indexShape = fields.IndexOf("Shape");
                    if (indexShape == -1) continue;
                    int indexFootPrint = fields.IndexOf("FootPrint");
                    if (indexFootPrint == -1) continue;
                    DFDataConfig.Class.FieldInfo fiDiameter = fac.GetFieldInfoBySystemName("Diameter");
                    if (fiDiameter == null) continue;
                    int indexDiameter = fields.IndexOf(fiDiameter.Name);
                    if (indexDiameter == -1) continue;
                    DFDataConfig.Class.FieldInfo fiRoad = fac.GetFieldInfoBySystemName("Road");
                    int indexRoad = -1;
                    if (fiRoad != null) indexRoad = fields.IndexOf(fiRoad.Name);
                    DFDataConfig.Class.FieldInfo fiHLB = fac.GetFieldInfoBySystemName("HLB");
                    int indexHLB = -1;
                    if (fiHLB != null) indexHLB = fields.IndexOf(fiHLB.Name);
                    int indexClassify = fields.IndexOf(mc.ClassifyField);

                    int fid = int.Parse(dr["Fid"].ToString());
                    IFdeCursor cursor = null;
                    IRowBuffer row = null;
                    try
                    {
                        IQueryFilter filter = new QueryFilter();
                        filter.WhereClause = "oid=" + fid;
                        cursor = fc.Search(filter, false);
                        if ((row = cursor.NextRow()) != null)
                        {
                            if (indexRoad != -1 && !row.IsNull(indexRoad))
                            {
                                if (road2 == "")
                                {
                                    road1 = row.GetValue(indexRoad).ToString();
                                    road2 = row.GetValue(indexRoad).ToString();
                                }
                                else
                                {
                                    road1 = row.GetValue(indexRoad).ToString();
                                    if (road1 != road2)
                                    {
                                        if (!bAlert)
                                        {
                                            XtraMessageBox.Show("跨越多条道路，当前只绘制在【" + road2 + "】上的管线纵断面图。", "提示");
                                            bAlert = true;
                                        }
                                        continue;
                                    }
                                }

                            }


                            double startSurfHeight = double.MaxValue;
                            double endSurfHeight = double.MaxValue;
                            if (!app.Current3DMapControl.Terrain.IsRegistered)
                            {
                                DFDataConfig.Class.FieldInfo fiStartSurfHeight = fac.GetFieldInfoBySystemName("StartSurfHeight");
                                if (fiStartSurfHeight == null) continue;
                                int indexStartSurfHeight = fields.IndexOf(fiStartSurfHeight.Name);
                                if (indexStartSurfHeight == -1) continue;
                                DFDataConfig.Class.FieldInfo fiEndSurfHeight = fac.GetFieldInfoBySystemName("EndSurfHeight");
                                if (fiEndSurfHeight == null) continue;
                                int indexEndSurfHeight = fields.IndexOf(fiEndSurfHeight.Name);
                                if (indexEndSurfHeight == -1) continue;
                                if (!row.IsNull(indexStartSurfHeight)) startSurfHeight = double.Parse(row.GetValue(indexStartSurfHeight).ToString());
                                if (!row.IsNull(indexEndSurfHeight)) endSurfHeight = double.Parse(row.GetValue(indexEndSurfHeight).ToString());
                            }
                            if (!row.IsNull(indexShape) && !row.IsNull(indexFootPrint))
                            {
                                object objFootPrint = row.GetValue(indexFootPrint);
                                object objShape = row.GetValue(indexShape);
                                if (objFootPrint is IPolyline && objShape is IPolyline)
                                {
                                    IPolyline polylineFootPrint = objFootPrint as IPolyline;
                                    IPolyline polylineShape = objShape as IPolyline;
                                    PPLine ppline = new PPLine();
                                    if (indexClassify == -1 || row.IsNull(indexClassify)) ppline.facType = mc.Name;
                                    else ppline.facType = row.GetValue(indexClassify).ToString();
                                    if (!row.IsNull(indexDiameter))
                                    {
                                        string diameter = row.GetValue(indexDiameter).ToString();
                                        if (diameter.Trim() == "") continue;
                                        ppline.dia = diameter;
                                        int indexDia = diameter.IndexOf('*');
                                        if (indexDia != -1)
                                        {
                                            ppline.isrect = true;
                                            int iDia1;
                                            bool bDia1 = int.TryParse(diameter.Substring(0, indexDia), out iDia1);
                                            if (!bDia1) continue;
                                            int iDia2;
                                            bool bDia2 = int.TryParse(diameter.Substring(indexDia + 1, diameter.Length - indexDia - 1), out iDia2);
                                            if (!bDia2) continue;
                                            ppline.gj.Add(iDia1);
                                            ppline.gj.Add(iDia2);
                                        }
                                        else
                                        {
                                            ppline.isrect = false;
                                            int iDia;
                                            bool bDia = int.TryParse(diameter, out iDia);
                                            if (!bDia) continue;
                                            ppline.gj.Add(iDia);
                                            ppline.gj.Add(iDia);
                                        }
                                    }
                                    int hlb = 0;
                                    if (indexHLB != -1 && !row.IsNull(indexHLB))
                                    {
                                        string strhlb = row.GetValue(indexHLB).ToString();
                                        if (strhlb.Contains("内"))
                                        {
                                            hlb = 1;
                                        }
                                        else if (strhlb.Contains("外"))
                                        {
                                            hlb = -1;
                                        }
                                        else hlb = 0;
                                        ppline.hlb = hlb;
                                    }
                                    IPoint ptIntersect = dr["InterPoint"] as IPoint;
                                    ppline.interPoint = new PPPoint(ptIntersect.X, ptIntersect.Y, ptIntersect.Z);
                                    ppline.clh = GetInterPointHeight(ptIntersect, polylineShape, polylineFootPrint);
                                    if (ppline.clh > hmax) hmax = ppline.clh;
                                    if (ppline.clh < hmin) hmin = ppline.clh;
                                    if (app.Current3DMapControl.Terrain.IsRegistered)
                                    {
                                        ppline.cgh = app.Current3DMapControl.Terrain.GetElevation(ptIntersect.X, ptIntersect.Y, Gvitech.CityMaker.RenderControl.gviGetElevationType.gviGetElevationFromDatabase);
                                    }
                                    else
                                    {
                                        ppline.cgh = startSurfHeight + (endSurfHeight - startSurfHeight)
                                            * Math.Sqrt((polylineFootPrint.StartPoint.X - ptIntersect.X) * (polylineFootPrint.StartPoint.X - ptIntersect.X)
                                            + (polylineFootPrint.StartPoint.Y - ptIntersect.Y) * (polylineFootPrint.StartPoint.Y - ptIntersect.Y)) / polylineFootPrint.Length;
                                    }
                                    if (ppline.cgh > hmax) hmax = ppline.cgh;
                                    if (ppline.cgh < hmin) hmin = ppline.cgh;
                                    // 辅助画图
                                    ppline.startPt = new PPPoint(startPt.X, startPt.Y, startPt.Z);
                                    pplines.Add(ppline);
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }
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
                WaitForm.Stop();
                if (pplines.Count < 2)
                {
                    XtraMessageBox.Show("点数少于2个", "提示");
                    return;
                }
                pplines.Sort(new PPLineCompare());
                double spacesum = 0.0;
                for (int i = 1; i < pplines.Count; i++)
                {
                    PPLine line1 = pplines[i - 1];
                    PPLine line2 = pplines[i];
                    line2.space = Math.Sqrt((line1.interPoint.X - line2.interPoint.X) * (line1.interPoint.X - line2.interPoint.X)
                        + (line1.interPoint.Y - line2.interPoint.Y) * (line1.interPoint.Y - line2.interPoint.Y));
                    spacesum += line2.space;
                };
                var str1 = (pplines[0].interPoint.X / 1000).ToString("0.00");
                var str2 = (pplines[0].interPoint.Y / 1000).ToString("0.00");
                string mapNum = str2 + "-" + str1;
                string mapName = SystemInfo.Instance.SystemFullName + "纵断面图";
                FrmSectionAnalysis dialog = new FrmSectionAnalysis("纵断面分析结果", 1);
                dialog.SetInfo(mapName, mapNum, pplines, hmax, hmin, spacesum, road2);
                dialog.Show();
            }
            catch (Exception ex)
            {
                WaitForm.Stop();
            }
            finally
            {
            }
        }

        private double GetInterPointHeight(IPoint interPoint, IPolyline geoShape, IPolyline geo)
        {
            if (interPoint == null || geoShape == null || geoShape == null) return 0.0;
            if (geoShape.PointCount == 2)
            {
                return geoShape.StartPoint.Z + (geoShape.EndPoint.Z - geoShape.StartPoint.Z) * Math.Sqrt((geo.StartPoint.X - interPoint.X) * (geo.StartPoint.X - interPoint.X)
                    + (geo.StartPoint.Y - interPoint.Y) * (geo.StartPoint.Y - interPoint.Y)) / geo.Length;
            }
            for (int i = 0; i < geoShape.PointCount - 1; i++)
            {
                IPoint pt1 = geoShape.GetPoint(i);
                IPoint pt2 = geoShape.GetPoint(i + 1);
                IPoint ptFootPrint1 = geo.GetPoint(i);
                IPoint ptFootPrint2 = geo.GetPoint(i + 1);
                double len = Math.Sqrt((ptFootPrint1.X - ptFootPrint2.X) * (ptFootPrint1.X - ptFootPrint2.X)
                    + (ptFootPrint1.Y - ptFootPrint2.Y) * (ptFootPrint1.Y - ptFootPrint2.Y));
                double x = interPoint.X;
                double y = interPoint.Y;
                double x1 = ptFootPrint1.X;
                double y1 = ptFootPrint1.Y;
                double z1 = pt1.Z;
                double x2 = ptFootPrint2.X;
                double y2 = ptFootPrint2.Y;
                double z2 = pt2.Z;
                double det = 0.5;
                bool b1 = (x >= x1 || Math.Abs(x - x1) < det) && (x <= x2 || Math.Abs(x - x2) < det) && (y <= y1 || Math.Abs(y - y1) < det) && (y >= y2 || Math.Abs(y - y2) < det);
                bool b2 = (x <= x1 || Math.Abs(x - x1) < det) && (x >= x2 || Math.Abs(x - x2) < det) && (y >= y1 || Math.Abs(y - y1) < det) && (y <= y2 || Math.Abs(y - y2) < det);
                bool b3 = (x >= x1 || Math.Abs(x - x1) < det) && (x <= x2 || Math.Abs(x - x2) < det) && (y >= y1 || Math.Abs(y - y1) < det) && (y >= y2 || Math.Abs(y - y2) < det);
                bool b4 = (x <= x1 || Math.Abs(x - x1) < det) && (x >= x2 || Math.Abs(x - x2) < det) && (y <= y1 || Math.Abs(y - y1) < det) && (y >= y2 || Math.Abs(y - y2) < det);
                if (b1 || b2 || b3 || b4)
                {
                    double res = z1 + (z2 - z1) * Math.Sqrt((x1 - x) * (x1 - x)
                    + (y1 - y) * (y1 - y)) / len;
                    return res;

                    //            var detx1 = x1 - x;
                    //            var dety1 = y1 - y;
                    //            var detx2 = x2 - x;
                    //            var dety2 = y2 - y;
                    //            var temp = dety2 * detx1 + dety1 * detx2;
                    //            if (Math.abs(temp) < 0.00001) {
                    //            }
                }
            }
            return 0.0;
        }


    }
}
